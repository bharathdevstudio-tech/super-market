/* =====================================================================
   SuperMarketDB - ONLINE menu row for the semi-online (hosting) sync.
   Adds:   ONLINE
             +-- Online Sync  ->  FrmHostingSync
   Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 12_SuperMarketDB_Menu_OnlineSync.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   1. Menu rows.
      The parent carries an empty FormName, exactly like MASTERS,
      PURCHASE and INVENTORY.
   --------------------------------------------------------------------- */
DECLARE @onlineParent int;

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster
               WHERE MenuName = 'ONLINE' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0)
BEGIN
    SELECT @onlineParent = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@onlineParent, 'ONLINE', '', 'Normal', 0, 5, 0, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
ELSE
    SELECT @onlineParent = MenuID FROM tbl_MenuMaster
    WHERE MenuName = 'ONLINE' AND ISNULL(FormName,'') = '' AND ISNULL(ParentID,0) = 0;

IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE FormName = 'FrmHostingSync')
BEGIN
    DECLARE @syncForm int;
    SELECT @syncForm = ISNULL(MAX(MenuID), 0) + 1 FROM tbl_MenuMaster;
    SET IDENTITY_INSERT tbl_MenuMaster ON;
    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc)
    VALUES (@syncForm, 'Online Sync', 'FrmHostingSync', 'Normal', @onlineParent, 5, 1, 'YES');
    SET IDENTITY_INSERT tbl_MenuMaster OFF;
END
GO

/* ---------------------------------------------------------------------
   2. Grants: give every existing user the new menu entries.
   --------------------------------------------------------------------- */
INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i)
SELECT u.U_EmpID_i, m.MenuID
FROM tbl_UserList u
CROSS JOIN tbl_MenuMaster m
WHERE (m.FormName = 'FrmHostingSync'
       OR (ISNULL(m.FormName,'') = '' AND m.MenuName = 'ONLINE' AND ISNULL(m.ParentID,0) = 0))
  AND NOT EXISTS (SELECT 1 FROM tbl_UserRoleMenu r
                  WHERE r.URM_EmpID_i = u.U_EmpID_i AND r.URM_MenuID_i = m.MenuID);
GO

PRINT '12_SuperMarketDB_Menu_OnlineSync.sql completed.';
GO
