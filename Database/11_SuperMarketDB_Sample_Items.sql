/* =====================================================================
   11_SuperMarketDB_Sample_Items.sql
   Sample SuperMarket items (small demo catalogue) so POS Billing,
   Purchase and Inventory have something to work with.

   Same two tables FrmSmItem writes:
       tbl_StockDtl    header  - St_StockID_n = item code, identity PK
       tbl_MultiStock  rates   - one line per item, St_StNumber_i = stock line

   FrmSmPosBilling.LoadItems() reads:
       tbl_StockDtl s INNER JOIN tbl_MultiStock m ON m.St_StockID_n = s.St_StockID_n
       WHERE ISNULL(m.St_Show_vc,'YES')='YES'
   so every sample row is written to both tables with St_Show_vc='YES'.

   Categories / brands / units must already exist (04_SuperMarketDB_Seed.sql).
   GST % is split evenly into CGST / SGST, matching the Item Master form.

   Idempotent: an item is inserted only when its
   Name + Category + Brand is not already in tbl_StockDtl.

   Target DB : SuperMarketDB
   Run with  : sqlcmd -S .\SQLEXPRESS -E -d SuperMarketDB -f 65001 -i 11_SuperMarketDB_Sample_Items.sql
               (-f 65001 = UTF-8 input; without it sqlcmd reads this file in the
                console code page and the Tamil names are stored as '?')
   ===================================================================== */
SET NOCOUNT ON;
GO

DECLARE @Seed TABLE (
    Ord       int IDENTITY(1,1),
    ProName   nvarchar(300),
    Tamil     nvarchar(300),
    CatName   nvarchar(100),
    BrandName nvarchar(100),
    Unit      nvarchar(20),
    HSN       varchar(20),
    GST       numeric(18,2),   -- total GST %, split evenly CGST / SGST
    MRP       numeric(18,2),
    PurRate   numeric(18,2),
    SaleRate  numeric(18,2),
    Qty       numeric(18,3),
    Reorder   int,
    Barcode   varchar(30)
);

INSERT INTO @Seed (ProName, Tamil, CatName, BrandName, Unit, HSN, GST, MRP, PurRate, SaleRate, Qty, Reorder, Barcode) VALUES
 (N'Sample Product',              N'மாதிரி பொருள்',        N'Grocery',       N'Local',              'NOS', '0000',  0, 100,  80, 100, 100,  5, 'SMP100'),
 (N'Rice Sona Masoori 1kg',       N'சோனா மசூரி அரிசி 1கி',  N'Grocery',       N'Local',              'KG',  '1006',  0,  70,  55,  65,  50, 10, 'RICESON1KG'),
 (N'Toor Dal 1kg',                N'துவரம் பருப்பு 1கி',    N'Grocery',       N'Local',              'KG',  '0713',  0, 150, 120, 140,  40, 10, 'TOORDAL1KG'),
 (N'Sunflower Oil 1L',            N'சூரியகாந்தி எண்ணெய் 1லி', N'Grocery',     N'Local',              'LTR', '1512',  0, 150, 125, 145,  30,  6, 'SUNOIL1L'),
 (N'Sugar 1kg',                   N'சர்க்கரை 1கி',          N'Grocery',       N'Local',              'KG',  '1701',  0,  50,  40,  48,  60, 10, 'SUGAR1KG'),
 (N'Aashirvaad Atta 5kg',         N'ஆசிர்வாத் மாவு 5கி',    N'Grocery',       N'ITC',                'BAG', '1101',  0, 300, 255, 285,  20,  5, 'AASHATTA5KG'),
 (N'Amul Milk 500ml',             N'அமுல் பால் 500மிலி',    N'Dairy',         N'Amul',               'PKT', '0401',  5,  28,  24,  27,  40, 10, 'AMULMILK500'),
 (N'Amul Butter 100g',            N'அமுல் வெண்ணெய் 100கி',  N'Dairy',         N'Amul',               'PCS', '0405',  5,  60,  52,  58,  25,  6, 'AMULBUT100'),
 (N'Britannia Marie Gold 250g',   N'பிரிட்டானியா மேரி 250கி', N'Bakery',       N'Britannia',          'PKT', '1905',  5,  35,  28,  33,  50, 10, 'BRITMARIE250'),
 (N'Tata Tea Gold 250g',          N'டாடா டீ கோல்ட் 250கி',   N'Beverages',    N'Tata',               'PKT', '0902', 12, 140, 115, 135,  30,  6, 'TATATEA250'),
 (N'Lays Classic Salted',         N'லேஸ் உப்பு சிப்ஸ்',      N'Snacks',       N'Local',              'PKT', '2005', 12,  20,  15,  20, 100, 20, 'LAYSCLASSIC'),
 (N'Colgate Strong Teeth 100g',   N'கோல்கேட் பற்பசை 100கி',  N'Personal Care', N'Colgate',           'NOS', '3306', 18,  60,  48,  58,  24,  6, 'COLGATE100'),
 (N'Surf Excel Easy Wash 1kg',    N'சர்ஃப் எக்செல் 1கி',    N'Household',    N'Hindustan Unilever', 'PKT', '3402', 18, 130, 105, 125,  20,  5, 'SURFEXCEL1');

DECLARE @Ord int = 1, @MaxOrd int = (SELECT MAX(Ord) FROM @Seed);
DECLARE @NextCode numeric(18,0) = ISNULL((SELECT MAX(CONVERT(numeric(18,0), St_StockID_n))   FROM tbl_StockDtl),   0);
DECLARE @NextSN   numeric(18,0) = ISNULL((SELECT MAX(CONVERT(numeric(18,0), St_StNumber_i)) FROM tbl_MultiStock), 0);

DECLARE @ProName nvarchar(300), @Tamil nvarchar(300), @CatName nvarchar(100), @BrandName nvarchar(100),
        @Unit nvarchar(20), @HSN varchar(20), @GST numeric(18,2), @MRP numeric(18,2),
        @PurRate numeric(18,2), @SaleRate numeric(18,2), @Qty numeric(18,3), @Reorder int, @Barcode varchar(30);
DECLARE @CatId int, @BrandId int, @Added int = 0;

WHILE @Ord <= @MaxOrd
BEGIN
    SELECT @ProName = ProName, @Tamil = Tamil, @CatName = CatName, @BrandName = BrandName,
           @Unit = Unit, @HSN = HSN, @GST = GST, @MRP = MRP, @PurRate = PurRate,
           @SaleRate = SaleRate, @Qty = Qty, @Reorder = Reorder, @Barcode = Barcode
    FROM @Seed WHERE Ord = @Ord;

    SET @CatId   = (SELECT TOP 1 Category_Code_n FROM tbl_Category WHERE Category_Name_vc = @CatName);
    SET @BrandId = (SELECT TOP 1 Brand_Code_n     FROM tbl_Brand    WHERE Brand_Name_vc   = @BrandName);
    IF @CatId   IS NULL SET @CatId   = (SELECT TOP 1 Category_Code_n FROM tbl_Category);   -- fall back to first category
    IF @BrandId IS NULL SET @BrandId = (SELECT TOP 1 Brand_Code_n     FROM tbl_Brand);      -- fall back to first brand

    IF NOT EXISTS (SELECT 1 FROM tbl_StockDtl
                   WHERE St_ProName_vc = @ProName
                     AND ISNULL(St_CatID_i, 0)   = ISNULL(@CatId, 0)
                     AND ISNULL(St_BrandID_i, 0) = ISNULL(@BrandId, 0))
    BEGIN
        SET @NextCode = @NextCode + 1;
        SET @NextSN   = @NextSN + 1;

        INSERT INTO tbl_StockDtl
            (St_StockID_n, St_ProName_vc, St_ProTamil_vc, St_CatID_i, St_BrandID_i, St_Unit_vc,
             St_HSN_vc, St_CGSTPer_n, St_SGSTPer_n, St_IGSTPer_n, St_RemiStock_i, St_Shelf_vc,
             St_TotalSt_i, St_GoTotalSt_i, St_Decrease_vc, St_EntDate_d)
        VALUES
            (@NextCode, @ProName, @Tamil, @CatId, @BrandId, @Unit,
             @HSN, @GST / 2, @GST / 2, @GST, @Reorder, '',
             @Qty, @Qty, 'YES', GETDATE());

        INSERT INTO tbl_MultiStock
            (St_StNumber_i, St_StockID_n, St_ProName_vc, St_MRPRate_n, St_PurRate_n, St_SaleRate_n,
             St_CurrStock_i, St_Barcode_vc, St_Show_vc, St_UpDate_d, St_UID_vc)
        VALUES
            (@NextSN, @NextCode, @ProName, @MRP, @PurRate, @SaleRate,
             @Qty, @Barcode, 'YES', GETDATE(), 'ADMIN');

        SET @Added = @Added + 1;
    END

    SET @Ord = @Ord + 1;
END

PRINT 'Sample items inserted: ' + CAST(@Added AS varchar(10));
GO
