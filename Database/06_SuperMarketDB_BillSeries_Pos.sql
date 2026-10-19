/* =====================================================================
   SuperMarketDB - reconcile tbl_BillSeries with the legacy sales types
   used by tbl_SalDetail.Sa_SaveType_vc ('INVOICE', 'SALES RETURN', ...).
   Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 06_SuperMarketDB_BillSeries_Pos.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

DECLARE @FY varchar(20);
SET @FY = CASE WHEN MONTH(GETDATE()) <= 3
               THEN (YEAR(GETDATE()) - 1) * 10000 + YEAR(GETDATE())
               ELSE YEAR(GETDATE()) * 10000 + (YEAR(GETDATE()) + 1) END;
DECLARE @FYText varchar(20);
SET @FYText = CONVERT(varchar(4), @FY / 10000) + '-' + CONVERT(varchar(4), @FY % 10000);

/* rename the stray 'SALES' type to the legacy 'INVOICE' */
UPDATE tbl_BillSeries
SET B_BillType_vc = 'INVOICE'
WHERE B_BillType_vc = 'SALES'
  AND NOT EXISTS (SELECT 1 FROM tbl_BillSeries x WHERE x.B_AcYear_vc = tbl_BillSeries.B_AcYear_vc AND x.B_BillType_vc = 'INVOICE');

/* ensure the current year has every type the POS uses */
INSERT INTO tbl_BillSeries (B_AcYear_vc, B_BillType_vc, B_BillSeries_vc, B_StartNo_i)
SELECT @FYText, t.BillType, t.Prefix, 1
FROM (VALUES ('INVOICE', 'SM'), ('SALES RETURN', 'SR'), ('PURCHASE', 'PM'), ('ESTIMATE', 'ES')) AS t(BillType, Prefix)
WHERE NOT EXISTS (SELECT 1 FROM tbl_BillSeries b WHERE b.B_AcYear_vc = @FYText AND b.B_BillType_vc = t.BillType);
GO

PRINT '06_SuperMarketDB_BillSeries_Pos.sql completed.';
GO
