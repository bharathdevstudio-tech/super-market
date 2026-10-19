/* =====================================================================
   SuperMarketDB - Purchase + Inventory menu rows, grants and the
   PURCHASE RETURN bill series.
   Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 09_SuperMarketDB_Menu_Purchase_Stock.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   1. Bill series: the purchase module saves two document types.
      PURCHASE        already exists (series PM).
      PURCHASE RETURN is added here (series PR) for every financial
      year that is already present, plus the current year.
   --------------------------------------------------------------------- */
INSERT INTO tbl_BillSeries (B_AcYear_vc, B_BillType_vc, B_BillSeries_vc, B_StartNo_i)
SELECT DISTINCT b.B_AcYear_vc, 'PURCHASE RETURN', 'PR', 1
FROM tbl_BillSeries b
WHERE NOT EXISTS (SELECT 1 FROM tbl_BillSeries x
                  WHERE x.B_AcYear_vc = b.B_AcYear_vc
                    AND x.B_BillType_vc = 'PURCHASE RETURN');
GO

DECLARE @fy varchar(20) =
    CASE WHEN MONTH(GETDATE()) <= 3
         THEN CONVERT(varchar(4), YEAR(GETDATE()) - 1) + '-' + CONVERT(varchar(4), YEAR(GETDATE()))
         ELSE CONVERT(varchar(4), YEAR(GETDATE())) + '-' + CONVERT(varchar(4), YEAR(GETDATE()) + 1)
    END;
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE B_AcYear_vc = @fy AND B_BillType_vc = 'PURCHASE RETURN')
    INSERT INTO tbl_BillSeries (B_AcYear_vc, B_BillType_vc, B_BillSeries_vc, B_StartNo_i)
    VALUES (@fy, 'PURCHASE RETURN', 'PR', 1);
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE B_AcYear_vc = @fy AND B_BillType_vc = 'PURCHASE')
    INSERT INTO tbl_BillSeries (B_AcYear_vc, B_BillType_vc, B_BillSeries_vc, B_StartNo_i)
    VALUES (@fy, 'PURCHASE', 'PM', 1);
GO

/* ---------------------------------------------------------------------
   2. Menu rows: a PURCHASE group and an INVENTORY group.
      Parent rows carry an empty FormName, exactly like MASTERS.
   --------------------------------------------------------------------- */
DECLARE @purchaseParent int, @purchaseEntry int, @inventoryParent int, @stockForm int;

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE MenuName = 'PURCHASE' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0)
BEGIN
    SELECT @purchaseParent = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@purchaseParent, 'PURCHASE', '', 'Normal', 0, 3, 0, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
ELSE
    SELECT @purchaseParent = MenuID FROM tbl_MenuMaster
    WHERE MenuName = 'PURCHASE' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0;

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmPurchase')
BEGIN
    SELECT @purchaseEntry = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@purchaseEntry, 'Purchase Entry', 'FrmSmPurchase', 'Normal', @purchaseParent, 3, 1, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE MenuName = 'INVENTORY' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0)
BEGIN
    SELECT @inventoryParent = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@inventoryParent, 'INVENTORY', '', 'Normal', 0, 4, 0, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
ELSE
    SELECT @inventoryParent = MenuID FROM tbl_MenuMaster
    WHERE MenuName = 'INVENTORY' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0;

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmStock')
BEGIN
    SELECT @stockForm = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@stockForm, 'Stock / Inventory', 'FrmSmStock', 'Normal', @inventoryParent, 4, 1, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

/* ---------------------------------------------------------------------
   3. Grants: give every existing user the new menu entries.
   --------------------------------------------------------------------- */
INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i)
SELECT u.U_EmpID_i, m.MenuID
FROM tbl_UserList u
CROSS JOIN tbl_MenuMaster m
WHERE (m.FormName IN ('FrmSmPurchase', 'FrmSmStock')
       OR (ISNULL(m.FormName,'') = '' AND m.MenuName IN ('PURCHASE', 'INVENTORY') AND ISNULL(m.ParentID,0) = 0))
  AND NOT EXISTS (SELECT 1 FROM tbl_UserRoleMenu r
                  WHERE r.URM_EmpID_i = u.U_EmpID_i AND r.URM_MenuID_i = m.MenuID);
GO

PRINT '09_SuperMarketDB_Menu_Purchase_Stock.sql completed.';
GO
