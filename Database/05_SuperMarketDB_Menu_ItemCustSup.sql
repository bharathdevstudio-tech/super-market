/* =====================================================================
   SuperMarketDB - incremental menu rows for the Item / Customer / Supplier
   masters. Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 05_SuperMarketDB_Menu_ItemCustSup.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   1. Menu rows (only when absent)
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmItem')
BEGIN
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (5, 'Item Master', 'FrmSmItem', 'Normal', 1, 1, 4, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmCustomer')
BEGIN
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (6, 'Customer Master', 'FrmSmCustomer', 'Normal', 1, 1, 5, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmSupplier')
BEGIN
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (7, 'Supplier Master', 'FrmSmSupplier', 'Normal', 1, 1, 6, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

/* ---------------------------------------------------------------------
   2. Grant the new menus to every existing user (admin = U_EmpID_i 1)
   --------------------------------------------------------------------- */
INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i)
SELECT u.U_EmpID_i, m.MenuID
FROM tbl_UserList u
CROSS JOIN tbl_MenuMaster m
WHERE m.FormName IN ('FrmSmItem', 'FrmSmCustomer', 'FrmSmSupplier')
  AND NOT EXISTS (SELECT 1 FROM tbl_UserRoleMenu r
                  WHERE r.URM_EmpID_i = u.U_EmpID_i AND r.URM_MenuID_i = m.MenuID);
GO

PRINT '05_SuperMarketDB_Menu_ItemCustSup.sql completed.';
GO
