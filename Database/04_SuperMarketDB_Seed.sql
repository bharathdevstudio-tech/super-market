/* =====================================================================
   04_SuperMarketDB_Seed.sql
   Minimal, real configuration so the SuperMarket application starts and
   the three built masters round-trip.  Idempotent-ish: each block only
   inserts when the target rows are absent.
   Target DB : SuperMarketDB
   ===================================================================== */
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   1. Company profile  (single row - InvenCmpDetail / login read it)
   cmp_ProjectType_vc = 'INVEN' puts the app on the standard retail path.
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_Companyprofile WHERE Cmp_ID_i = 1)
BEGIN
    INSERT INTO tbl_Companyprofile
    ( Cmp_ID_i, Ent_Date_d, Cmp_Name_vc, Cmp_Caption_vc, Cmp_Proprietor_vc,
      Cmp_Address_vc, Cmp_District_vc, Cmp_State_vc, Cmp_Pincode_vc,
      Cmp_PhoneNo_vc, Cmp_MobileNo_n, Cmp_Email_vc, Cmp_Website_vc,
      BillingType_vc, BackupPath_vc, ScanningPath_vc, Cmp_DatabaseName_vc,
      Cmp_SaveImage_vc, Cmp_GSTcal_vc, Cmp_ExpDays_i, Cmp_GDrivePath_vc,
      Cmp_UpiID_vc, Cmp_UpiName_vc, Mast_Password_vc, cmp_ProjectType_vc,
      Bill_ZeroStock_vc, Bill_AftPrint_vc )
    VALUES
    ( 1, GETDATE(), N'SUPER MARKET', N'SUPER MARKET', N'Proprietor',
      N'Main Road', N'City', 'Tamil Nadu', '600001',
      '', '', '', '',
      'DEFAULT', 'E:\VISTAWIN\BACKUP', 'E:\VISTAWIN\SUPERMARKET', 'SuperMarketDB',
      'NO', 'INCLUDING', 0, '',
      '', '', 'master', 'INVEN',
      'NO', 'NO' );
END
GO

/* ---------------------------------------------------------------------
   2. Users  (admin - login auto-fills admin/admin on this machine)
   U_EmpID_i must match tbl_UserRoleMenu.URM_EmpID_i for the menu query.
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_UserList WHERE U_EmpID_i = 1)
BEGIN
    INSERT INTO tbl_UserList (U_EmpID_i, U_EmpName_vc, U_UserName_vc, U_Password_vc)
    VALUES (1, N'SOFTWARE', 'admin', N'admin');
END
GO

/* ---------------------------------------------------------------------
   3. Menu + role grants  (only forms that actually exist are switched on;
   further rows are added as each module is built).
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster)
BEGIN
    SET IDENTITY_INSERT tbl_MenuMaster ON;

    INSERT INTO tbl_MenuMaster (MenuID, MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder, Menu_Show_vc) VALUES
      ( 1, 'MASTERS',        '',              'Normal', 0, 1, 0, 'YES'),
      ( 2, 'Category Master','FrmSmCategory', 'Normal', 1, 1, 1, 'YES'),
      ( 3, 'Unit Master',    'FrmSmUnit',     'Normal', 1, 1, 2, 'YES'),
      ( 4, 'Brand Master',   'FrmSmBrand',    'Normal', 1, 1, 3, 'YES');

    SET IDENTITY_INSERT tbl_MenuMaster OFF;

    INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i) VALUES
      (1, 1), (1, 2), (1, 3), (1, 4);
END
GO

/* ---------------------------------------------------------------------
   4. Units
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_UnitList)
BEGIN
    INSERT INTO tbl_UnitList (U_EName_vc, U_TName_vc, U_OrderNo_i, U_Show_vc) VALUES
      ('NOS', N'எண்ணிக்கை', 1, 'YES'),
      ('PCS', N'துண்டு',    2, 'YES'),
      ('KG',  N'கிலோ',      3, 'YES'),
      ('GM',  N'கிராம்',    4, 'YES'),
      ('LTR', N'லிட்டர்',   5, 'YES'),
      ('ML',  N'மில்லி',    6, 'YES'),
      ('PKT', N'பாக்கெட்',  7, 'YES'),
      ('BOX', N'பெட்டி',    8, 'YES'),
      ('DOZ', N'டஜன்',      9, 'YES'),
      ('BAG', N'மூட்டை',   10, 'YES'),
      ('MTR', N'மீட்டர்',  11, 'YES'),
      ('BTL', N'பாட்டில்', 12, 'YES');
END
GO

/* ---------------------------------------------------------------------
   5. Categories  (Category_Code_n is NOT identity -> supply codes)
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_Category)
BEGIN
    INSERT INTO tbl_Category (Category_Code_n, Category_Name_vc, Cat_AliasName_vc, Cat_GSTPer_n, Cat_Show_vc, User_id_vc, Ent_date_d) VALUES
      ( 1, N'Grocery',       N'மளிகை',        0,  'YES', 'admin', GETDATE()),
      ( 2, N'Vegetables',    N'காய்கறி',        0,  'YES', 'admin', GETDATE()),
      ( 3, N'Fruits',        N'பழங்கள்',        0,  'YES', 'admin', GETDATE()),
      ( 4, N'Dairy',         N'பால் பொருட்கள்',  5,  'YES', 'admin', GETDATE()),
      ( 5, N'Bakery',        N'பேக்கரி',        5,  'YES', 'admin', GETDATE()),
      ( 6, N'Beverages',     N'பானங்கள்',      12,  'YES', 'admin', GETDATE()),
      ( 7, N'Snacks',        N'சிற்றுண்டி',     12,  'YES', 'admin', GETDATE()),
      ( 8, N'Frozen',        N'உறைந்த உணவு',   12,  'YES', 'admin', GETDATE()),
      ( 9, N'Household',     N'வீட்டு உபயோகம்', 18, 'YES', 'admin', GETDATE()),
      (10, N'Personal Care', N'தனிநபர் பராமரிப்பு', 18, 'YES', 'admin', GETDATE()),
      (11, N'Stationery',    N'எழுதுபொருள்',   18,  'YES', 'admin', GETDATE()),
      (12, N'Others',        N'இதர',            0,  'YES', 'admin', GETDATE());
END
GO

/* ---------------------------------------------------------------------
   6. Brands  (Brand_Code_n is NOT identity -> supply codes)
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_Brand)
BEGIN
    INSERT INTO tbl_Brand (Brand_Code_n, Brand_Name_vc, Brand_Show_vc, User_id_vc, Ent_date_d) VALUES
      (1, N'Amul',                'YES', 'admin', GETDATE()),
      (2, N'Nestle',              'YES', 'admin', GETDATE()),
      (3, N'Britannia',           'YES', 'admin', GETDATE()),
      (4, N'Tata',                'YES', 'admin', GETDATE()),
      (5, N'ITC',                 'YES', 'admin', GETDATE()),
      (6, N'Hindustan Unilever',  'YES', 'admin', GETDATE()),
      (7, N'Colgate',             'YES', 'admin', GETDATE()),
      (8, N'Procter & Gamble',    'YES', 'admin', GETDATE()),
      (9, N'Local',               'YES', 'admin', GETDATE());
END
GO

/* ---------------------------------------------------------------------
   7. GST tariff slabs  (CGST + SGST halves of 0/5/12/18/28)
   --------------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_GSTTariff)
BEGIN
    INSERT INTO tbl_GSTTariff (GST_CGSTP_n, GST_SGSTP_n, GST_Show_vc) VALUES
      ( 0.00,  0.00, 'YES'),
      ( 2.50,  2.50, 'YES'),
      ( 6.00,  6.00, 'YES'),
      ( 9.00,  9.00, 'YES'),
      (14.00, 14.00, 'YES');
END
GO

/* ---------------------------------------------------------------------
   8. Bill series for the current financial year (Apr-Mar, e.g. 2026-2027)
   B_BillSeries_vc = prefix, B_BillType_vc = save type, B_StartNo_i = first no.
   --------------------------------------------------------------------- */
DECLARE @FY varchar(20);
SET @FY = CASE WHEN MONTH(GETDATE()) <= 3
               THEN CONVERT(varchar(4), YEAR(GETDATE()) - 1) + '-' + CONVERT(varchar(4), YEAR(GETDATE()))
               ELSE CONVERT(varchar(4), YEAR(GETDATE()))     + '-' + CONVERT(varchar(4), YEAR(GETDATE()) + 1)
          END;

IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE B_AcYear_vc = @FY)
BEGIN
    INSERT INTO tbl_BillSeries (B_AcYear_vc, B_BillType_vc, B_BillSeries_vc, B_StartNo_i) VALUES
      (@FY, 'INVOICE',       'SM', 1),
      (@FY, 'SALES RETURN',  'SR', 1),
      (@FY, 'PURCHASE',      'PM', 1),
      (@FY, 'ESTIMATE',      'ES', 1);
END
GO

PRINT '04_SuperMarketDB_Seed applied.';
GO
