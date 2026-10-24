/* Builds a scratch database that has the REAL column shapes of the bill tables
   (cloned with SELECT TOP 0 from SuperMarketDB, so types and nullability match)
   plus the SM_Sync* outbox. Used only for the end-to-end enqueue test. */
SET NOCOUNT ON;
GO

IF DB_ID('SM_SyncVerify') IS NOT NULL
BEGIN
    ALTER DATABASE SM_SyncVerify SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SM_SyncVerify;
END
GO
CREATE DATABASE SM_SyncVerify;
GO

USE SM_SyncVerify;
GO

SELECT TOP 0 * INTO dbo.tbl_SalDetail        FROM SuperMarketDB.dbo.tbl_SalDetail;
SELECT TOP 0 * INTO dbo.tbl_SalParticular    FROM SuperMarketDB.dbo.tbl_SalParticular;
SELECT TOP 0 * INTO dbo.tbl_PurchDetail      FROM SuperMarketDB.dbo.tbl_PurchDetail;
SELECT TOP 0 * INTO dbo.tbl_PurchParticular  FROM SuperMarketDB.dbo.tbl_PurchParticular;
GO

/* sample sales bill */
INSERT INTO dbo.tbl_SalDetail
    (Sa_EntDate_d, Sa_Shift_vc, Sa_SaveType_vc, Sa_BillDate_d, Sa_BillTime_vc,
     Sa_InvNo_vc, Sa_BillNo_n, Sa_BType_vc, Sa_CusID_n, Sa_CusName_vc,
     Sa_GSTTotal_n, Sa_TotalAmt_n, Sa_RoType_vc, Sa_RoAmt_n, Sa_FinalTotal_n,
     Sa_Words_vc, Sa_GSTWords_vc, Sa_AcYear_vc, Sa_MRPTotal_n, Sa_UserID_vc,
     Sa_UpDate_d, Sa_BefDisAmt_n, Sa_DiscountAmt_n)
VALUES
    (GETDATE(), '', 'INVOICE', '2026-10-23', '10:45',
     'INV42', 42, 'CASH', 3, 'CASH',
     12.50, 100.00, 'ROUND OFF', 0.20, 100.20,
     '', '', '2026-2027', 110.00, 'ADMIN',
     GETDATE(), 100.00, 0.00);

INSERT INTO dbo.tbl_SalParticular
    (Sa_SaveTy_vc, Sa_BillDate_d, Sa_InvNo_vc, Sa_CusID_n, Sa_StNumber_n, Sa_StCode_n,
     Sa_PurRate_n, Sa_MRP_n, Sa_SaleRate_n, Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n,
     Sa_Qty_i, Sa_FQty_i, Sa_TQty_i, Sa_QType_vc, Sa_Pack_vc, Sa_Taxable_n,
     Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n, Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n,
     Sa_ProTotal_n, Sa_PRemark_vc)
VALUES
    ('INVOICE', '2026-10-23', 'INV42', 3, 9001, 501,
     80.00, 110.00, 100.00, 0.00, 0.00, 100.00,
     1.000, 0.000, 1.000, 'NOS', '', 88.50,
     6.00, 6.00, 0.00, 6.25, 6.25, 0.00,
     100.00, ''),
    ('INVOICE', '2026-10-23', 'INV42', 3, 9002, 502,
     40.00, 55.00, 50.00, 0.00, 0.00, 50.00,
     2.000, 0.000, 2.000, 'NOS', '', 44.25,
     6.00, 6.00, 0.00, 3.75, 3.75, 0.00,
     50.00, '');

/* a second bill that will be deleted */
INSERT INTO dbo.tbl_SalDetail
    (Sa_EntDate_d, Sa_SaveType_vc, Sa_BillDate_d, Sa_BillTime_vc, Sa_InvNo_vc,
     Sa_BillNo_n, Sa_BType_vc, Sa_CusID_n, Sa_CusName_vc, Sa_GSTTotal_n,
     Sa_TotalAmt_n, Sa_RoType_vc, Sa_RoAmt_n, Sa_FinalTotal_n, Sa_AcYear_vc,
     Sa_MRPTotal_n, Sa_UserID_vc, Sa_UpDate_d, Sa_BefDisAmt_n, Sa_DiscountAmt_n)
VALUES
    (GETDATE(), 'INVOICE', '2026-10-23', '11:00', 'INV43',
     43, 'CASH', 0, 'CASH', 0.00,
     20.00, 'ROUND OFF', 0.00, 20.00, '2026-2027',
     20.00, 'ADMIN', GETDATE(), 20.00, 0.00);

/* a purchase bill */
INSERT INTO dbo.tbl_PurchDetail
    (Sa_EntDate_d, Sa_SaveType_vc, Sa_BillDate_d, Sa_BillTime_vc, Sa_InvNo_vc,
     Sa_BillNo_n, Sa_BType_vc, Sa_CusID_n, Sa_CusName_vc, Sa_GSTTotal_n,
     Sa_TotalAmt_n, Sa_RoType_vc, Sa_RoAmt_n, Sa_FinalTotal_n, Sa_AcYear_vc,
     Sa_MRPTotal_n, Sa_UserID_vc, Sa_UpDate_d)
VALUES
    (GETDATE(), 'PURCHASE', '2026-10-23', '11:30', 'PUR7',
     7, 'CASH', 9, 'SUPPLIER', 18.00,
     300.00, 'ROUND OFF', 0.00, 300.00, '2026-2027',
     330.00, 'ADMIN', GETDATE());

INSERT INTO dbo.tbl_PurchParticular
    (Sa_SaveTy_vc, Sa_BillDate_d, Sa_InvNo_vc, Sa_CusID_n, Sa_StNumber_n, Sa_StCode_n,
     Sa_PurRate_n, Sa_MRP_n, Sa_SaleRate_n, Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n,
     Sa_Qty_i, Sa_FQty_i, Sa_TQty_i, Sa_QType_vc, Sa_Pack_vc, Sa_Taxable_n,
     Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n, Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n,
     Sa_ProTotal_n, Sa_PRemark_vc)
VALUES
    ('PURCHASE', '2026-10-23', 'PUR7', 9, 9001, 501,
     80.00, 110.00, 100.00, 0.00, 0.00, 80.00,
     10.000, 0.000, 10.000, '', '', 300.00,
     9.00, 9.00, 0.00, 9.00, 9.00, 0.00,
     300.00, '');
GO

PRINT 'scratch business tables ready';
GO
