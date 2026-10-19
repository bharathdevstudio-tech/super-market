/* =====================================================================
   SuperMarketDB - POS Billing menu row (+ grants).
   Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 08_SuperMarketDB_Menu_Pos.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmSmPosBilling')
BEGIN
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (8, 'POS Billing', 'FrmSmPosBilling', 'Normal', 0, 2, 0, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i)
SELECT u.U_EmpID_i, m.MenuID
FROM tbl_UserList u
CROSS JOIN tbl_MenuMaster m
WHERE m.FormName = 'FrmSmPosBilling'
  AND NOT EXISTS (SELECT 1 FROM tbl_UserRoleMenu r
                  WHERE r.URM_EmpID_i = u.U_EmpID_i AND r.URM_MenuID_i = m.MenuID);
GO

PRINT '08_SuperMarketDB_Menu_Pos.sql completed.';
GO
