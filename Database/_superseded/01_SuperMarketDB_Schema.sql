/* =====================================================================================
   SuperMarket Management & POS Billing  -  Database Schema
   -------------------------------------------------------------------------------------
   Target   : Microsoft SQL Server 2008 R2
   Database : SuperMarketDB   (dedicated SuperMarket database)
   Conventions : tbl_<Name>  with column suffixes
                 _i  = int            _n  = numeric/decimal
                 _vc = varchar/nvarchar  _d  = datetime
                 _b  = bit            _im = image
   Reuse    : table / column names follow the existing VISTAWIN "RETAILPOSDB" retail
              schema so existing code patterns and reports stay compatible.
   Safe     : every object is created only if it does not already exist.
              This script never drops or alters an existing table.
   Run with : sqlcmd -S .\SQLEXPRESS -E -i 01_SuperMarketDB_Schema.sql
   ===================================================================================== */

SET NOCOUNT ON;
GO

/* ------------------------------------------------------------------ create database */
IF DB_ID('SuperMarketDB') IS NULL
BEGIN
    CREATE DATABASE [SuperMarketDB];
END
GO

USE [SuperMarketDB];
GO

/* =====================================================================================
   SECTION 1 : COMPANY / SETTINGS / SERIES
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_Companyprofile','U') IS NULL
CREATE TABLE dbo.tbl_Companyprofile(
    Cmp_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Cmp_Name_vc          nvarchar(200)  NULL,
    Cmp_Caption_vc       nvarchar(200)  NULL,
    Cmp_Proprietor_vc    nvarchar(200)  NULL,
    Cmp_LicenseNo_vc     varchar(50)    NULL,
    Cmp_Address_vc       nvarchar(500)  NULL,
    Cmp_District_vc      nvarchar(100)  NULL,
    Cmp_State_vc         varchar(50)    NULL,
    Cmp_Pincode_vc       varchar(8)     NULL,
    Cmp_PhoneNo_vc       varchar(30)    NULL,
    Cmp_MobileNo_vc      varchar(50)    NULL,
    Cmp_Email_vc         varchar(100)   NULL,
    Cmp_Website_vc       varchar(50)    NULL,
    Cmp_GSTNo_vc         varchar(20)    NULL,
    Cmp_TINNo_vc         varchar(20)    NULL,
    Cmp_Logo_im          image          NULL,
    Cmp_BankDetails_vc   nvarchar(500)  NULL,
    Cmp_ReportHead_vc    nvarchar(500)  NULL,
    Cmp_ToMail_vc        varchar(50)    NULL,
    Cmp_UpiID_vc         varchar(100)   NULL,
    Cmp_UpiName_vc       varchar(100)   NULL,
    Cmp_Note_vc          nvarchar(500)  NULL,
    Cmp_GSTcal_vc        varchar(20)    NULL,
    Cmp_Barcode_vc       varchar(10)    NULL,
    Cmp_SaleRound_vc     varchar(10)    NULL,
    Cmp_PurRound_vc      varchar(10)    NULL,
    Cmp_PW1_vc           varchar(20)    NULL,
    Cmp_MastPassword_vc  nvarchar(50)   NULL,
    Cmp_BackupPath_vc    varchar(500)   NULL,
    Cmp_ScanningPath_vc  varchar(500)   NULL,
    Cmp_ServerName_vc    varchar(50)    NULL,
    Cmp_DatabaseName_vc  varchar(50)    NULL,
    Cmp_ProjectType_vc   varchar(20)    NULL,
    Ent_date_d           datetime       NULL,
    User_ID_vc           varchar(20)    NULL,
    CONSTRAINT PK_tbl_Companyprofile PRIMARY KEY CLUSTERED (Cmp_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_GeneralSettings','U') IS NULL
CREATE TABLE dbo.tbl_GeneralSettings(
    GS_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    GS_Name_vc           varchar(100)  NULL,
    GS_Value_vc          nvarchar(500) NULL,
    GS_Type_vc           varchar(50)   NULL,
    GS_Show_vc           varchar(5)    NULL,
    CONSTRAINT PK_tbl_GeneralSettings PRIMARY KEY CLUSTERED (GS_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_BillSeries','U') IS NULL
CREATE TABLE dbo.tbl_BillSeries(
    BS_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    BS_Type_vc           varchar(20)   NOT NULL,   -- SALES / PURCHASE / RECEIPT / PAYMENT / SALE_RETURN / PUR_RETURN
    BS_Series_vc         varchar(20)   NULL,       -- prefix e.g. "SM"
    BS_FY_vc             varchar(9)    NULL,       -- financial year 2026-2027
    BS_Prefix_vc         varchar(10)   NULL,
    BS_FromNo_i          int           NULL,
    BS_ToNo_i            int           NULL,
    BS_CurrNo_i          int           NULL,
    BS_Show_vc           varchar(5)    NULL,
    CONSTRAINT PK_tbl_BillSeries PRIMARY KEY CLUSTERED (BS_RecordNo_i),
    CONSTRAINT UQ_tbl_BillSeries UNIQUE (BS_Type_vc, BS_FY_vc)
);
GO

IF OBJECT_ID('dbo.tbl_DayClosing','U') IS NULL
CREATE TABLE dbo.tbl_DayClosing(
    DC_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    DC_Date_d            datetime      NULL,
    DC_OpeningCash_n     numeric(18,2) NULL,
    DC_CashSale_n        numeric(18,2) NULL,
    DC_CardSale_n        numeric(18,2) NULL,
    DC_UpiSale_n         numeric(18,2) NULL,
    DC_CreditSale_n      numeric(18,2) NULL,
    DC_SaleReturn_n      numeric(18,2) NULL,
    DC_Expense_n         numeric(18,2) NULL,
    DC_ClosingCash_n     numeric(18,2) NULL,
    DC_Remarks_vc        nvarchar(300) NULL,
    User_id_vc           varchar(20)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_DayClosing PRIMARY KEY CLUSTERED (DC_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 2 : MASTER ENTRY
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_Category','U') IS NULL
CREATE TABLE dbo.tbl_Category(
    Category_Code_n      numeric(18,0) IDENTITY(1,1) NOT NULL,
    Category_Name_vc     nvarchar(100) NOT NULL,
    Cat_AliasName_vc     nvarchar(50)  NULL,
    Cat_GSTPer_n         numeric(5,2)  NULL,
    Cat_Show_vc          varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Category PRIMARY KEY CLUSTERED (Category_Code_n)
);
GO

IF OBJECT_ID('dbo.tbl_SubCategory','U') IS NULL
CREATE TABLE dbo.tbl_SubCategory(
    SubCat_RecordNo_i    int IDENTITY(1,1) NOT NULL,
    SubCat_Name_vc       nvarchar(100) NOT NULL,
    SubCat_CatID_n       numeric(18,0) NULL,
    SubCat_Show_vc       varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_SubCategory PRIMARY KEY CLUSTERED (SubCat_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Brand','U') IS NULL
CREATE TABLE dbo.tbl_Brand(
    Brand_Code_n         numeric(18,0) IDENTITY(1,1) NOT NULL,
    Brand_Name_vc        nvarchar(100) NULL,
    Brand_Show_vc        varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Brand PRIMARY KEY CLUSTERED (Brand_Code_n)
);
GO

IF OBJECT_ID('dbo.tbl_UnitList','U') IS NULL
CREATE TABLE dbo.tbl_UnitList(
    U_RecordNo_i         int IDENTITY(1,1) NOT NULL,
    U_EName_vc           nvarchar(50)  NULL,
    U_OrderNo_i          int           NULL,
    U_Show_vc            varchar(5)    NULL,
    CONSTRAINT PK_tbl_UnitList PRIMARY KEY CLUSTERED (U_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Rack','U') IS NULL
CREATE TABLE dbo.tbl_Rack(
    Rack_RecordNo_i      int IDENTITY(1,1) NOT NULL,
    Rack_Name_vc         nvarchar(50)  NULL,
    Rack_Show_vc         varchar(5)    NULL,
    CONSTRAINT PK_tbl_Rack PRIMARY KEY CLUSTERED (Rack_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PaymentMode','U') IS NULL
CREATE TABLE dbo.tbl_PaymentMode(
    Pay_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Pay_Name_vc          nvarchar(50)  NULL,
    Pay_Show_vc          varchar(5)    NULL,
    CONSTRAINT PK_tbl_PaymentMode PRIMARY KEY CLUSTERED (Pay_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_TaxMaster','U') IS NULL
CREATE TABLE dbo.tbl_TaxMaster(
    Tax_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Tax_Name_vc          nvarchar(50)  NULL,     -- GST 0 / GST 5 / GST 12 / GST 18 / GST 28
    Tax_CGSTPer_n        numeric(5,2)  NULL,
    Tax_SGSTPer_n        numeric(5,2)  NULL,
    Tax_IGSTPer_n        numeric(5,2)  NULL,
    Tax_Type_vc          varchar(20)   NULL,     -- Inclusive / Exclusive
    Tax_HSN_vc           varchar(20)   NULL,
    Tax_Show_vc          varchar(5)    NULL,
    CONSTRAINT PK_tbl_TaxMaster PRIMARY KEY CLUSTERED (Tax_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Stockist','U') IS NULL   -- Supplier master
CREATE TABLE dbo.tbl_Stockist(
    Sup_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Sup_Name_vc          nvarchar(150) NULL,
    Sup_Contact_vc       nvarchar(100) NULL,
    Sup_Address_vc       nvarchar(300) NULL,
    Sup_Phone_vc         varchar(30)   NULL,
    Sup_Mobile_vc        varchar(15)   NULL,
    Sup_Email_vc         varchar(100)  NULL,
    Sup_GSTNo_vc         varchar(20)   NULL,
    Sup_DLNo_vc          varchar(30)   NULL,
    Sup_OpBal_n          numeric(18,2) NULL,
    Sup_OpBalType_vc     varchar(5)    NULL,     -- DR / CR
    Sup_Show_vc          varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Stockist PRIMARY KEY CLUSTERED (Sup_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_CustDetails','U') IS NULL   -- Customer master
CREATE TABLE dbo.tbl_CustDetails(
    Cus_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Cus_Name_vc          nvarchar(150) NULL,
    Cus_Address_vc       nvarchar(300) NULL,
    Cus_Mobile_vc        varchar(15)   NULL,
    Cus_Phone_vc         varchar(30)   NULL,
    Cus_Email_vc         varchar(100)  NULL,
    Cus_GSTNo_vc         varchar(20)   NULL,
    Cus_OpBal_n          numeric(18,2) NULL,
    Cus_OpBalType_vc     varchar(5)    NULL,
    Cus_Area_vc          nvarchar(100) NULL,
    Cus_Show_vc          varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_CustDetails PRIMARY KEY CLUSTERED (Cus_RecordNo_i)
);
GO

/* ---- Product / Stock -------------------------------------------------------------- */
IF OBJECT_ID('dbo.tbl_StockDtl','U') IS NULL
CREATE TABLE dbo.tbl_StockDtl(
    St_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    St_StNumber_i        numeric(18,0) NULL,     -- product code
    St_ProName_vc        nvarchar(150) NOT NULL,
    St_ProTamil_vc       nvarchar(150) NULL,
    St_Unit_vc           nvarchar(20)  NULL,
    St_HSN_vc            varchar(20)   NULL,
    St_CatID_n           numeric(18,0) NULL,
    St_SubCatID_n        numeric(18,0) NULL,
    St_BrandID_n         numeric(18,0) NULL,
    St_TaxID_i           int           NULL,
    St_CGSTPer_n         numeric(5,2)  NULL,
    St_SGSTPer_n         numeric(5,2)  NULL,
    St_IGSTPer_n         numeric(5,2)  NULL,
    St_PurRate_n         numeric(18,2) NULL,
    St_SaleRate_n        numeric(18,2) NULL,
    St_MRPRate_n         numeric(18,2) NULL,
    St_DisPer_n          numeric(5,2)  NULL,
    St_OpStock_n         numeric(18,3) NULL,
    St_MinStock_n        numeric(18,3) NULL,
    St_Rack_vc           nvarchar(50)  NULL,
    St_Shelf_vc          nvarchar(50)  NULL,
    St_Row_vc            nvarchar(50)  NULL,
    St_Barcode_vc        nvarchar(50)  NULL,
    St_Expiry_b          bit           NULL,     -- track expiry
    St_Remark_vc         nvarchar(300) NULL,
    St_Show_vc           varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_StockDtl PRIMARY KEY CLUSTERED (St_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Barcode','U') IS NULL
CREATE TABLE dbo.tbl_Barcode(
    Bar_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Bar_Barcode_vc       nvarchar(50)  NOT NULL,
    Bar_StockID_i        int           NULL,
    Bar_Unit_vc          nvarchar(20)  NULL,
    Bar_MRP_n            numeric(18,2) NULL,
    Bar_SaleRate_n       numeric(18,2) NULL,
    Bar_Show_vc          varchar(5)    NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Barcode PRIMARY KEY CLUSTERED (Bar_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_MultiStock','U') IS NULL   -- batch wise stock
CREATE TABLE dbo.tbl_MultiStock(
    St_RecordNo2_i       int IDENTITY(1,1) NOT NULL,
    St_StNumber_i        numeric(18,0) NULL,
    St_StockID_n         numeric(18,0) NULL,
    St_ProName_vc        nvarchar(150) NULL,
    St_BatchNo_vc        varchar(30)   NULL,
    St_EMonth_i           int          NULL,
    St_EYear_vc          varchar(5)    NULL,
    St_CurrStock_i       numeric(18,3) NULL,
    St_PurRate_n         numeric(18,2) NULL,
    St_SaleRate_n        numeric(18,2) NULL,
    St_MRPRate_n         numeric(18,2) NULL,
    St_SaleRate2_n       numeric(18,2) NULL,
    St_MarPer1_n         numeric(5,2)  NULL,
    St_MarPer2_n         numeric(5,2)  NULL,
    St_Barcode_vc        nvarchar(50)  NULL,
    St_UID_vc            varchar(10)   NULL,
    St_Show_vc           varchar(5)    NULL,
    St_EDate_d           datetime      NULL,
    St_UpDate_d          datetime      NULL,
    CONSTRAINT PK_tbl_MultiStock PRIMARY KEY CLUSTERED (St_RecordNo2_i)
);
GO

IF OBJECT_ID('dbo.tbl_StockLedger','U') IS NULL
CREATE TABLE dbo.tbl_StockLedger(
    SL_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    SL_Date_d            datetime      NULL,
    SL_StockID_i         int           NULL,
    SL_BatchNo_vc        varchar(30)   NULL,
    SL_TransType_vc      varchar(20)   NULL,     -- OPENING / PURCHASE / SALE / SALE_RETURN / PUR_RETURN / ADJUST / DAMAGE / WASTAGE / TRANSFER
    SL_TransID_i         int           NULL,
    SL_TransNo_vc        varchar(30)   NULL,
    SL_InQty_n           numeric(18,3) NULL,
    SL_OutQty_n          numeric(18,3) NULL,
    SL_Rate_n            numeric(18,2) NULL,
    SL_BalQty_n          numeric(18,3) NULL,
    SL_Narration_vc      nvarchar(200) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_StockLedger PRIMARY KEY CLUSTERED (SL_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_StockAdjustment','U') IS NULL
CREATE TABLE dbo.tbl_StockAdjustment(
    SA_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    SA_Date_d            datetime      NULL,
    SA_Type_vc           varchar(20)   NULL,     -- INCREASE / DECREASE
    SA_Reason_vc         nvarchar(200) NULL,
    SA_TotalQty_n        numeric(18,3) NULL,
    SA_TotalAmt_n        numeric(18,2) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_StockAdjustment PRIMARY KEY CLUSTERED (SA_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_StockAdjustmentDtl','U') IS NULL
CREATE TABLE dbo.tbl_StockAdjustmentDtl(
    SAD_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    SAD_SAID_i           int           NULL,
    SAD_StockID_i        int           NULL,
    SAD_BatchNo_vc       varchar(30)   NULL,
    SAD_Qty_n            numeric(18,3) NULL,
    SAD_Rate_n           numeric(18,2) NULL,
    SAD_Amt_n            numeric(18,2) NULL,
    CONSTRAINT PK_tbl_StockAdjustmentDtl PRIMARY KEY CLUSTERED (SAD_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_StockTransfer','U') IS NULL
CREATE TABLE dbo.tbl_StockTransfer(
    ST_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    ST_Date_d            datetime      NULL,
    ST_FromRack_vc       nvarchar(50)  NULL,
    ST_ToRack_vc         nvarchar(50)  NULL,
    ST_Remarks_vc        nvarchar(200) NULL,
    ST_TotalQty_n        numeric(18,3) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_StockTransfer PRIMARY KEY CLUSTERED (ST_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_StockTransferDtl','U') IS NULL
CREATE TABLE dbo.tbl_StockTransferDtl(
    STD_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    STD_STID_i           int           NULL,
    STD_StockID_i        int           NULL,
    STD_BatchNo_vc       varchar(30)   NULL,
    STD_Qty_n            numeric(18,3) NULL,
    CONSTRAINT PK_tbl_StockTransferDtl PRIMARY KEY CLUSTERED (STD_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_DamageDetail','U') IS NULL
CREATE TABLE dbo.tbl_DamageDetail(
    Dam_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Dam_Date_d           datetime      NULL,
    Dam_Reason_vc        nvarchar(200) NULL,
    Dam_TotalQty_n       numeric(18,3) NULL,
    Dam_TotalAmt_n       numeric(18,2) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_DamageDetail PRIMARY KEY CLUSTERED (Dam_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_DamageParticular','U') IS NULL
CREATE TABLE dbo.tbl_DamageParticular(
    DP_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    DP_DamID_i           int           NULL,
    DP_StockID_i         int           NULL,
    DP_BatchNo_vc        varchar(30)   NULL,
    DP_Qty_n             numeric(18,3) NULL,
    DP_Rate_n            numeric(18,2) NULL,
    DP_Amt_n             numeric(18,2) NULL,
    CONSTRAINT PK_tbl_DamageParticular PRIMARY KEY CLUSTERED (DP_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 3 : PURCHASE
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_PurchDetail','U') IS NULL
CREATE TABLE dbo.tbl_PurchDetail(
    Pur_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Pur_BillNo_vc        varchar(30)   NULL,
    Pur_BillDate_d       datetime      NULL,
    Pur_SupID_i          int           NULL,
    Pur_SupInvNo_vc      varchar(30)   NULL,
    Pur_SupInvDate_d     datetime      NULL,
    Pur_NetAmt_n         numeric(18,2) NULL,
    Pur_DiscAmt_n        numeric(18,2) NULL,
    Pur_CGSTAmt_n        numeric(18,2) NULL,
    Pur_SGSTAmt_n        numeric(18,2) NULL,
    Pur_IGSTAmt_n        numeric(18,2) NULL,
    Pur_GSTAmt_n         numeric(18,2) NULL,
    Pur_RoundOff_n       numeric(18,2) NULL,
    Pur_TotalAmt_n       numeric(18,2) NULL,
    Pur_PaidAmt_n        numeric(18,2) NULL,
    Pur_Balance_n        numeric(18,2) NULL,
    Pur_Type_vc          varchar(20)   NULL,     -- CASH / CREDIT
    Pur_Series_vc        varchar(20)   NULL,
    Pur_Remarks_vc       nvarchar(300) NULL,
    Pur_Status_vc        varchar(20)   NULL,     -- ACTIVE / CANCELLED
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_PurchDetail PRIMARY KEY CLUSTERED (Pur_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PurchParticular','U') IS NULL
CREATE TABLE dbo.tbl_PurchParticular(
    PP_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    PP_PurID_i           int           NULL,
    PP_StockID_i         int           NULL,
    PP_ProName_vc        nvarchar(150) NULL,
    PP_BatchNo_vc        varchar(30)   NULL,
    PP_EMonth_i          int           NULL,
    PP_EYear_vc          varchar(5)    NULL,
    PP_Qty_n             numeric(18,3) NULL,
    PP_FreeQty_n         numeric(18,3) NULL,
    PP_PurRate_n         numeric(18,2) NULL,
    PP_SaleRate_n        numeric(18,2) NULL,
    PP_MRPRate_n         numeric(18,2) NULL,
    PP_DisPer_n          numeric(5,2)  NULL,
    PP_CGSTPer_n         numeric(5,2)  NULL,
    PP_SGSTPer_n         numeric(5,2)  NULL,
    PP_IGSTPer_n         numeric(5,2)  NULL,
    PP_Amt_n             numeric(18,2) NULL,
    CONSTRAINT PK_tbl_PurchParticular PRIMARY KEY CLUSTERED (PP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PurchaseReturnDetail','U') IS NULL
CREATE TABLE dbo.tbl_PurchaseReturnDetail(
    PR_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    PR_ReturnNo_vc       varchar(30)   NULL,
    PR_Date_d            datetime      NULL,
    PR_SupID_i           int           NULL,
    PR_PurID_i           int           NULL,
    PR_TotalAmt_n        numeric(18,2) NULL,
    PR_GSTAmt_n          numeric(18,2) NULL,
    PR_Remarks_vc        nvarchar(300) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_PurchaseReturnDetail PRIMARY KEY CLUSTERED (PR_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PurchaseReturnParticular','U') IS NULL
CREATE TABLE dbo.tbl_PurchaseReturnParticular(
    PRP_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    PRP_PRID_i           int           NULL,
    PRP_StockID_i        int           NULL,
    PRP_BatchNo_vc       varchar(30)   NULL,
    PRP_Qty_n            numeric(18,3) NULL,
    PRP_Rate_n           numeric(18,2) NULL,
    PRP_CGSTPer_n        numeric(5,2)  NULL,
    PRP_SGSTPer_n        numeric(5,2)  NULL,
    PRP_Amt_n            numeric(18,2) NULL,
    CONSTRAINT PK_tbl_PurchaseReturnParticular PRIMARY KEY CLUSTERED (PRP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PODetail','U') IS NULL
CREATE TABLE dbo.tbl_PODetail(
    PO_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    PO_No_vc             varchar(30)   NULL,
    PO_Date_d            datetime      NULL,
    PO_SupID_i           int           NULL,
    PO_TotalAmt_n        numeric(18,2) NULL,
    PO_Status_vc         varchar(20)   NULL,     -- OPEN / RECEIVED / CANCELLED
    PO_Remarks_vc        nvarchar(300) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_PODetail PRIMARY KEY CLUSTERED (PO_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_POParticular','U') IS NULL
CREATE TABLE dbo.tbl_POParticular(
    POP_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    POP_POID_i           int           NULL,
    POP_StockID_i        int           NULL,
    POP_Qty_n            numeric(18,3) NULL,
    POP_Rate_n           numeric(18,2) NULL,
    POP_Amt_n            numeric(18,2) NULL,
    CONSTRAINT PK_tbl_POParticular PRIMARY KEY CLUSTERED (POP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_PaymentInvWise','U') IS NULL   -- supplier / customer payment allocation
CREATE TABLE dbo.tbl_PaymentInvWise(
    PIW_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    PIW_Date_d           datetime      NULL,
    PIW_Type_vc          varchar(20)   NULL,     -- SUPPLIER / CUSTOMER
    PIW_PartyID_i        int           NULL,
    PIW_InvID_i          int           NULL,
    PIW_InvNo_vc         varchar(30)   NULL,
    PIW_InvAmt_n         numeric(18,2) NULL,
    PIW_PaidAmt_n        numeric(18,2) NULL,
    PIW_Mode_vc          varchar(20)   NULL,
    PIW_RefNo_vc         varchar(30)   NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_PaymentInvWise PRIMARY KEY CLUSTERED (PIW_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 4 : SALES / POS
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_SalDetail','U') IS NULL
CREATE TABLE dbo.tbl_SalDetail(
    Sa_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    Sa_BillNo_vc         varchar(30)   NULL,
    Sa_BillDate_d        datetime      NULL,
    Sa_CusID_i           int           NULL,
    Sa_NetAmt_n          numeric(18,2) NULL,
    Sa_DiscPer_n         numeric(5,2)  NULL,
    Sa_DiscAmt_n         numeric(18,2) NULL,
    Sa_CGSTAmt_n         numeric(18,2) NULL,
    Sa_SGSTAmt_n         numeric(18,2) NULL,
    Sa_IGSTAmt_n         numeric(18,2) NULL,
    Sa_GSTAmt_n          numeric(18,2) NULL,
    Sa_RoundOff_n        numeric(18,2) NULL,
    Sa_TotalAmt_n        numeric(18,2) NULL,
    Sa_PaidAmt_n         numeric(18,2) NULL,
    Sa_Balance_n         numeric(18,2) NULL,
    Sa_Type_vc           varchar(20)   NULL,     -- CASH / CREDIT / CARD / UPI
    Sa_Series_vc         varchar(20)   NULL,
    Sa_Status_vc         varchar(20)   NULL,     -- ACTIVE / CANCELLED / HOLD
    Sa_Profit_n          numeric(18,2) NULL,
    Sa_Remarks_vc        nvarchar(300) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_SalDetail PRIMARY KEY CLUSTERED (Sa_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_SalParticular','U') IS NULL
CREATE TABLE dbo.tbl_SalParticular(
    Sa_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    Sa_SalID_i           int           NULL,
    Sa_StockID_i         int           NULL,
    Sa_ProName_vc        nvarchar(150) NULL,
    Sa_BatchNo_vc        varchar(30)   NULL,
    Sa_Qty_n             numeric(18,3) NULL,
    Sa_QType_vc          nvarchar(20)  NULL,
    Sa_Rate_n            numeric(18,2) NULL,
    Sa_MRPRate_n         numeric(18,2) NULL,
    Sa_PurRate_n         numeric(18,2) NULL,
    Sa_DisPer_n          numeric(5,2)  NULL,
    Sa_DisAmt_n          numeric(18,2) NULL,
    Sa_CGSTPer_n         numeric(5,2)  NULL,
    Sa_SGSTPer_n         numeric(5,2)  NULL,
    Sa_IGSTPer_n         numeric(5,2)  NULL,
    Sa_GSTAmt_n          numeric(18,2) NULL,
    Sa_Amt_n             numeric(18,2) NULL,
    Sa_Profit_n          numeric(18,2) NULL,
    CONSTRAINT PK_tbl_SalParticular PRIMARY KEY CLUSTERED (Sa_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_SalesReturnDetail','U') IS NULL
CREATE TABLE dbo.tbl_SalesReturnDetail(
    SR_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    SR_ReturnNo_vc       varchar(30)   NULL,
    SR_Date_d            datetime      NULL,
    SR_CusID_i           int           NULL,
    SR_BillID_i          int           NULL,
    SR_TotalAmt_n        numeric(18,2) NULL,
    SR_GSTAmt_n          numeric(18,2) NULL,
    SR_RefundAmt_n       numeric(18,2) NULL,
    SR_Mode_vc           varchar(20)   NULL,
    SR_Remarks_vc        nvarchar(300) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_SalesReturnDetail PRIMARY KEY CLUSTERED (SR_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_SalesReturnParticular','U') IS NULL
CREATE TABLE dbo.tbl_SalesReturnParticular(
    SRP_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    SRP_SRID_i           int           NULL,
    SRP_StockID_i        int           NULL,
    SRP_BatchNo_vc       varchar(30)   NULL,
    SRP_Qty_n            numeric(18,3) NULL,
    SRP_Rate_n           numeric(18,2) NULL,
    SRP_CGSTPer_n        numeric(5,2)  NULL,
    SRP_SGSTPer_n        numeric(5,2)  NULL,
    SRP_Amt_n            numeric(18,2) NULL,
    CONSTRAINT PK_tbl_SalesReturnParticular PRIMARY KEY CLUSTERED (SRP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_HoldBill','U') IS NULL
CREATE TABLE dbo.tbl_HoldBill(
    HB_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    HB_TokenNo_vc        varchar(20)   NULL,
    HB_Date_d            datetime      NULL,
    HB_CusID_i           int           NULL,
    HB_TotalAmt_n        numeric(18,2) NULL,
    HB_Remarks_vc        nvarchar(200) NULL,
    HB_Status_vc         varchar(20)   NULL,     -- HOLD / RECALLED
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_HoldBill PRIMARY KEY CLUSTERED (HB_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_HoldBillParticular','U') IS NULL
CREATE TABLE dbo.tbl_HoldBillParticular(
    HBP_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    HBP_HBID_i           int           NULL,
    HBP_StockID_i        int           NULL,
    HBP_BatchNo_vc       varchar(30)   NULL,
    HBP_Qty_n            numeric(18,3) NULL,
    HBP_Rate_n           numeric(18,2) NULL,
    HBP_DisPer_n         numeric(5,2)  NULL,
    HBP_Amount_n         numeric(18,2) NULL,
    CONSTRAINT PK_tbl_HoldBillParticular PRIMARY KEY CLUSTERED (HBP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_BillPayment','U') IS NULL   -- split payments
CREATE TABLE dbo.tbl_BillPayment(
    BP_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    BP_SaleID_i          int           NULL,
    BP_PayMode_vc        varchar(20)   NULL,     -- CASH / CARD / UPI / WALLET / CREDIT
    BP_Amount_n          numeric(18,2) NULL,
    BP_RefNo_vc          varchar(50)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_BillPayment PRIMARY KEY CLUSTERED (BP_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_ReceiptSummary','U') IS NULL
CREATE TABLE dbo.tbl_ReceiptSummary(
    RS_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    RS_ReceiptNo_vc      varchar(30)   NULL,
    RS_Date_d            datetime      NULL,
    RS_PartyType_vc      varchar(20)   NULL,     -- CUSTOMER / SUPPLIER
    RS_PartyID_i         int           NULL,
    RS_Amount_n          numeric(18,2) NULL,
    RS_Mode_vc           varchar(20)   NULL,
    RS_RefNo_vc          varchar(30)   NULL,
    RS_Narration_vc      nvarchar(300) NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_ReceiptSummary PRIMARY KEY CLUSTERED (RS_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_ReceiptDtl','U') IS NULL
CREATE TABLE dbo.tbl_ReceiptDtl(
    RD_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    RD_RSID_i            int           NULL,
    RD_InvID_i           int           NULL,
    RD_InvNo_vc          varchar(30)   NULL,
    RD_InvAmt_n          numeric(18,2) NULL,
    RD_AdjAmt_n          numeric(18,2) NULL,
    CONSTRAINT PK_tbl_ReceiptDtl PRIMARY KEY CLUSTERED (RD_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 5 : ACCOUNTS
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_AccountGroup','U') IS NULL
CREATE TABLE dbo.tbl_AccountGroup(
    AG_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    AG_Name_vc           nvarchar(50)  NULL,
    AG_Type_vc           varchar(20)   NULL,     -- INCOME / EXPENSE / ASSET / LIABILITY
    AG_Show_vc           varchar(5)    NULL,
    CONSTRAINT PK_tbl_AccountGroup PRIMARY KEY CLUSTERED (AG_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Accountledgers','U') IS NULL
CREATE TABLE dbo.tbl_Accountledgers(
    Acc_Code_i           int IDENTITY(1,1) NOT NULL,
    Acc_LedgerName_vc    nvarchar(100) NULL,
    Acc_Group_vc         nvarchar(50)  NULL,
    Acc_OpBal_n          numeric(18,2) NULL,
    Acc_OpBalType_vc     varchar(5)    NULL,
    Acc_Show_vc          varchar(5)    NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Accountledgers PRIMARY KEY CLUSTERED (Acc_Code_i)
);
GO

IF OBJECT_ID('dbo.tbl_Expenses','U') IS NULL
CREATE TABLE dbo.tbl_Expenses(
    Exp_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Exp_Date_d           datetime      NULL,
    Exp_Ledger_vc        nvarchar(100) NULL,
    Exp_Amount_n         numeric(18,2) NULL,
    Exp_Mode_vc          varchar(20)   NULL,     -- CASH / BANK
    Exp_RefNo_vc         varchar(30)   NULL,
    Exp_Narration_vc     nvarchar(300) NULL,
    Exp_Status_vc        varchar(20)   NULL,
    User_id_vc           varchar(10)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_Expenses PRIMARY KEY CLUSTERED (Exp_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_CashBook','U') IS NULL
CREATE TABLE dbo.tbl_CashBook(
    CB_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    Receiptdate_d        datetime      NULL,
    ReceiptNo_vc         varchar(30)   NULL,
    ReceiptSeries_vc     varchar(20)   NULL,
    RecType_vc           varchar(30)   NULL,     -- RECEIPT / PAYMENT / CASH DEPOSIT / CASH WITHDRAW / EXPENSE / SALE_RETURN
    LedgerName_vc        nvarchar(100) NULL,
    Particulars_vc       nvarchar(300) NULL,
    Expentiture_n        numeric(18,2) NULL,
    Income_n             numeric(18,2) NULL,
    Mode_vc              varchar(20)   NULL,
    CashOrBank_vc        varchar(10)   NULL,
    SGST_n               numeric(18,2) NULL,
    ChequeNo_vc          varchar(30)   NULL,
    ChequeDate_d         datetime      NULL,
    SNo_i                int           NULL,
    UserName_vc          varchar(50)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_CashBook PRIMARY KEY CLUSTERED (CB_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_OpBalance','U') IS NULL
CREATE TABLE dbo.tbl_OpBalance(
    OB_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    OB_Date_d            datetime      NULL,
    OB_Type_vc           varchar(20)   NULL,     -- CUSTOMER / SUPPLIER / CASH / BANK
    OB_PartyID_i         int           NULL,
    OB_Amount_n          numeric(18,2) NULL,
    OB_DrCr_vc           varchar(5)    NULL,
    OB_FY_vc             varchar(9)    NULL,
    CONSTRAINT PK_tbl_OpBalance PRIMARY KEY CLUSTERED (OB_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 6 : SECURITY
   ===================================================================================== */

IF OBJECT_ID('dbo.tbl_UserList','U') IS NULL
CREATE TABLE dbo.tbl_UserList(
    U_RecordNumber_i     int IDENTITY(1,1) NOT NULL,
    U_EmpID_i            int           NOT NULL,
    U_EmpName_vc         nvarchar(100) NOT NULL,
    U_UserName_vc        varchar(50)   NULL,
    U_Password_vc        nvarchar(50)  NULL,
    U_Type_vc            varchar(20)   NULL,     -- ADMIN / MANAGER / CASHIER
    U_Show_vc            varchar(5)    NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_UserList PRIMARY KEY CLUSTERED (U_RecordNumber_i)
);
GO

IF OBJECT_ID('dbo.tbl_UserRoles','U') IS NULL
CREATE TABLE dbo.tbl_UserRoles(
    Role_RecordNo_i      int IDENTITY(1,1) NOT NULL,
    Role_Name_vc         nvarchar(50)  NULL,
    Role_Show_vc         varchar(5)    NULL,
    CONSTRAINT PK_tbl_UserRoles PRIMARY KEY CLUSTERED (Role_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_UserPermission','U') IS NULL
CREATE TABLE dbo.tbl_UserPermission(
    Perm_RecordNo_i      int IDENTITY(1,1) NOT NULL,
    Perm_RoleID_i        int           NULL,
    Perm_FormName_vc     varchar(100)  NULL,
    Perm_View_b          bit           NULL,
    Perm_Add_b           bit           NULL,
    Perm_Edit_b          bit           NULL,
    Perm_Delete_b        bit           NULL,
    CONSTRAINT PK_tbl_UserPermission PRIMARY KEY CLUSTERED (Perm_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_MenuMaster','U') IS NULL
CREATE TABLE dbo.tbl_MenuMaster(
    MenuID               int IDENTITY(1,1) NOT NULL,
    MenuName             varchar(100)  NULL,
    FormName             varchar(100)  NULL,
    FormState            varchar(20)   NULL,
    ParentID             int           NULL,
    MenuNameOrder        int           NULL,
    FormNameOrder        int           NULL,
    CONSTRAINT PK_tbl_MenuMaster PRIMARY KEY CLUSTERED (MenuID)
);
GO

IF OBJECT_ID('dbo.tbl_UserRoleMenu','U') IS NULL
CREATE TABLE dbo.tbl_UserRoleMenu(
    URM_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    URM_EmpID_i          int           NULL,
    URM_MenuID_i         int           NULL,
    CONSTRAINT PK_tbl_UserRoleMenu PRIMARY KEY CLUSTERED (URM_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_ActivityLog','U') IS NULL
CREATE TABLE dbo.tbl_ActivityLog(
    AL_RecordNo_i        int IDENTITY(1,1) NOT NULL,
    AL_Date_d            datetime      NULL,
    AL_UserName_vc       varchar(50)   NULL,
    AL_Form_vc           varchar(100)  NULL,
    AL_Action_vc         varchar(50)   NULL,
    AL_Details_vc        nvarchar(500) NULL,
    AL_Machine_vc        varchar(50)   NULL,
    Ent_date_d           datetime      NULL,
    CONSTRAINT PK_tbl_ActivityLog PRIMARY KEY CLUSTERED (AL_RecordNo_i)
);
GO

IF OBJECT_ID('dbo.tbl_Settings','U') IS NULL
CREATE TABLE dbo.tbl_Settings(
    Set_RecordNo_i       int IDENTITY(1,1) NOT NULL,
    Set_TblName_vc       nvarchar(100) NULL,
    Set_Particular_vc    nvarchar(200) NULL,
    Set_Condition_vc     varchar(50)   NULL,
    Set_SetShow_vc       varchar(10)   NULL,
    CONSTRAINT PK_tbl_Settings PRIMARY KEY CLUSTERED (Set_RecordNo_i)
);
GO

/* =====================================================================================
   SECTION 7 : INDEXES
   ===================================================================================== */

IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_StockDtl_Barcode')
    CREATE INDEX IX_StockDtl_Barcode ON dbo.tbl_StockDtl(St_Barcode_vc);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_StockDtl_Name')
    CREATE INDEX IX_StockDtl_Name ON dbo.tbl_StockDtl(St_ProName_vc);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_Barcode_Code')
    CREATE INDEX IX_Barcode_Code ON dbo.tbl_Barcode(Bar_Barcode_vc);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_MultiStock_StockID')
    CREATE INDEX IX_MultiStock_StockID ON dbo.tbl_MultiStock(St_StockID_n);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SalDetail_BillNo')
    CREATE INDEX IX_SalDetail_BillNo ON dbo.tbl_SalDetail(Sa_BillNo_vc);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SalDetail_Date')
    CREATE INDEX IX_SalDetail_Date ON dbo.tbl_SalDetail(Sa_BillDate_d);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_SalParticular_SalID')
    CREATE INDEX IX_SalParticular_SalID ON dbo.tbl_SalParticular(Sa_SalID_i);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_PurchDetail_BillNo')
    CREATE INDEX IX_PurchDetail_BillNo ON dbo.tbl_PurchDetail(Pur_BillNo_vc);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_PurchParticular_PurID')
    CREATE INDEX IX_PurchParticular_PurID ON dbo.tbl_PurchParticular(PP_PurID_i);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_StockLedger_StockID')
    CREATE INDEX IX_StockLedger_StockID ON dbo.tbl_StockLedger(SL_StockID_i);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_CashBook_Date')
    CREATE INDEX IX_CashBook_Date ON dbo.tbl_CashBook(Receiptdate_d);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name='IX_UserList_User')
    CREATE UNIQUE INDEX IX_UserList_User ON dbo.tbl_UserList(U_UserName_vc);
GO

/* =====================================================================================
   SECTION 8 : FOREIGN KEYS  (guarded - created only when missing)
   ===================================================================================== */

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_SubCategory_Category')
   AND OBJECT_ID('dbo.tbl_SubCategory') IS NOT NULL
    ALTER TABLE dbo.tbl_SubCategory ADD CONSTRAINT FK_SubCategory_Category
        FOREIGN KEY (SubCat_CatID_n) REFERENCES dbo.tbl_Category(Category_Code_n);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_StockDtl_Category')
    ALTER TABLE dbo.tbl_StockDtl ADD CONSTRAINT FK_StockDtl_Category
        FOREIGN KEY (St_CatID_n) REFERENCES dbo.tbl_Category(Category_Code_n);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_StockDtl_Brand')
    ALTER TABLE dbo.tbl_StockDtl ADD CONSTRAINT FK_StockDtl_Brand
        FOREIGN KEY (St_BrandID_n) REFERENCES dbo.tbl_Brand(Brand_Code_n);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_PurchParticular_PurchDetail')
    ALTER TABLE dbo.tbl_PurchParticular ADD CONSTRAINT FK_PurchParticular_PurchDetail
        FOREIGN KEY (PP_PurID_i) REFERENCES dbo.tbl_PurchDetail(Pur_RecordNo_i);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_SalParticular_SalDetail')
    ALTER TABLE dbo.tbl_SalParticular ADD CONSTRAINT FK_SalParticular_SalDetail
        FOREIGN KEY (Sa_SalID_i) REFERENCES dbo.tbl_SalDetail(Sa_RecordNo_i);
GO

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name='FK_BillPayment_SalDetail')
    ALTER TABLE dbo.tbl_BillPayment ADD CONSTRAINT FK_BillPayment_SalDetail
        FOREIGN KEY (BP_SaleID_i) REFERENCES dbo.tbl_SalDetail(Sa_RecordNo_i);
GO

PRINT 'SuperMarketDB schema created / verified successfully.';
GO
