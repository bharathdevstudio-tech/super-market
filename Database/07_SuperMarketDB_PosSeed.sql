/* =====================================================================
   SuperMarketDB - POS seed data.
   Ensures a default walk-in CASH customer exists so billing works the
   moment the app is installed. Idempotent.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 07_SuperMarketDB_PosSeed.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

IF NOT EXISTS (SELECT 1 FROM tbl_CusProfile WHERE C_Type_vc = 'CUSTOMER' AND C_Name_vc = 'CASH')
BEGIN
    DECLARE @AllID int = ISNULL((SELECT MAX(C_AllID_n) FROM tbl_CusProfile), 0) + 1;
    DECLARE @ID    int = ISNULL((SELECT MAX(C_ID_n) FROM tbl_CusProfile WHERE C_Type_vc = 'CUSTOMER'), 0) + 1;

    INSERT INTO tbl_CusProfile
        (C_EDate_d, C_AllID_n, C_Type_vc, C_Type3_vc, C_ID_n, C_GrpID_i, C_Name_vc,
         C_OpBal_n, C_OpType_vc, C_Show_vc, C_OrderBy_i, C_UserID_vc, C_UpDate_d)
    VALUES
        (GETDATE(), @AllID, 'CUSTOMER', 'Customer', @ID, 0, 'CASH',
         0, 'Debit', 'YES', 1, 'SYSTEM', GETDATE());
END
GO

PRINT '07_SuperMarketDB_PosSeed.sql completed.';
GO
