-- Generated from the live SQL Server catalog by tools/generate-d1-schema.js
-- Source: docs/schema/columns.psv (do not edit by hand; re-run the generator)
--
-- Type mapping:
--   int/bigint/smallint/tinyint/bit  -> INTEGER
--   numeric/decimal/money/float      -> REAL   (values rounded to 2dp at the API boundary)
--   datetime/date                    -> TEXT   (ISO-8601 UTC)
--   image/varbinary                  -> BLOB
--   everything else                  -> TEXT

PRAGMA foreign_keys = ON;

-- tbl_Area: 5 columns, 0 identity, effective key: Area_Code_n
CREATE TABLE IF NOT EXISTS tbl_Area (
  Ent_date_d TEXT,
  Area_Code_n REAL PRIMARY KEY,
  Area_Name_vc TEXT,
  User_id_vc TEXT,
  Di_DiviID_i INTEGER
);

-- tbl_AreaDivision: 6 columns, 1 identity, effective key: Di_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_AreaDivision (
  Di_EDate_d TEXT,
  Di_RecordNo_i INTEGER PRIMARY KEY,
  Di_DiviID_i INTEGER,
  Di_DivisionName_vc TEXT,
  Di_UID_vc TEXT,
  Di_UpDate_d TEXT
);

-- tbl_BankList: 8 columns, 1 identity, effective key: Ba_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_BankList (
  Ba_EDate_d TEXT,
  Ba_RecordNo_i INTEGER PRIMARY KEY,
  Ba_BankID_i INTEGER,
  Ba_BankName_vc TEXT,
  Ba_OpBal_n REAL,
  Ba_OpBalType_vc TEXT,
  Ba_UID_vc TEXT,
  Ba_UpDate_d TEXT
);

-- tbl_BillSeries: 5 columns, 1 identity, effective key: B_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_BillSeries (
  B_RecordNo_i INTEGER PRIMARY KEY,
  B_AcYear_vc TEXT NOT NULL,
  B_BillType_vc TEXT NOT NULL,
  B_BillSeries_vc TEXT NOT NULL,
  B_StartNo_i INTEGER
);

-- tbl_Brand: 5 columns, 0 identity, effective key: Brand_Code_n
CREATE TABLE IF NOT EXISTS tbl_Brand (
  Ent_date_d TEXT,
  Brand_Code_n REAL PRIMARY KEY,
  Brand_Name_vc TEXT,
  User_id_vc TEXT,
  Brand_Show_vc TEXT
);

-- tbl_Category: 8 columns, 0 identity, effective key: Category_Code_n
CREATE TABLE IF NOT EXISTS tbl_Category (
  Ent_date_d TEXT,
  Category_Code_n REAL PRIMARY KEY,
  Category_Name_vc TEXT,
  User_id_vc TEXT,
  Cat_AliasName_vc TEXT,
  Cat_MediRoute_vc TEXT,
  Cat_GSTPer_n REAL,
  Cat_Show_vc TEXT
);

-- tbl_Companyprofile: 67 columns, 0 identity, effective key: Cmp_ID_i
CREATE TABLE IF NOT EXISTS tbl_Companyprofile (
  Ent_Date_d TEXT,
  Cmp_ID_i INTEGER PRIMARY KEY,
  Cmp_Name_vc TEXT,
  Cmp_Caption_vc TEXT,
  Estd_Date_d TEXT,
  Cmp_Proprietor_vc TEXT,
  Cmp_LicenseNo_vc TEXT,
  Cmp_RegDate_d TEXT,
  Cmp_Address_vc TEXT,
  Cmp_District_vc TEXT,
  Cmp_State_vc TEXT,
  Cmp_Pincode_vc TEXT,
  Cmp_PhoneNo_vc TEXT,
  Cmp_MobileNo_n TEXT,
  Cmp_Email_vc TEXT,
  Cmp_Website_vc TEXT,
  User_ID_vc TEXT,
  Updated_Date_d TEXT,
  cmp_accesspw_vc TEXT,
  Mast_Password_vc TEXT,
  Cmp_Logo_im BLOB,
  BillingType_vc TEXT,
  BackupPath_vc TEXT,
  ScanningPath_vc TEXT,
  SendSMS_vc TEXT,
  SMS_SenderID_vc TEXT,
  SMS_UserID_vc TEXT,
  SMS_Password_vc TEXT,
  Cmp_ServerName_vc TEXT,
  Cmp_DatabaseName_vc TEXT,
  Cmp_SaveImage_vc TEXT,
  Cmp_BankDetails_vc TEXT,
  Moving_Word_vc TEXT,
  Cmp_ISO_im BLOB,
  Cmp_ReportHead_vc TEXT,
  Sms_APIkey_vc TEXT,
  Cmp_ToMail_vc TEXT,
  Cmp_BackImage_im BLOB,
  Cmp_SumOpBal_n REAL,
  Bill_ZeroStock_vc TEXT,
  Bill_AftPrint_vc TEXT,
  Cmp_GSTcal_vc TEXT,
  Cmp_StockSel_vc TEXT,
  Cmp_ExpDays_i INTEGER,
  Cmp_PurGST_vc TEXT,
  Cmp_PurRound_vc TEXT,
  Cmp_SaleRound_vc TEXT,
  Cmp_PAutoRound_vc TEXT,
  Cmp_SAutoRound_vc TEXT,
  Cmp_Barcode_vc TEXT,
  Cmp_MoveMonth_i INTEGER,
  Cmp_Head1_vc TEXT,
  Cmp_Head2_vc TEXT,
  Cmp_Head3_vc TEXT,
  Cmp_Head4_vc TEXT,
  Cmp_Head5_vc TEXT,
  Cmp_Exp_vc TEXT,
  Cmp_MoveType_vc TEXT,
  Cmp_GDrivePath_vc TEXT,
  Cmp_UpiID_vc TEXT,
  Cmp_UpiName_vc TEXT,
  Cmp_PharmacyName_vc TEXT,
  Cmp_Note_vc TEXT,
  Cmp_PW1_vc TEXT,
  Cmp_CoverAmt_n REAL,
  cmp_ServerApi_vc TEXT,
  cmp_ProjectType_vc TEXT
);

-- tbl_CusProfile: 46 columns, 1 identity, effective key: C_RecordNumber_i
CREATE TABLE IF NOT EXISTS tbl_CusProfile (
  C_EDate_d TEXT,
  C_RecordNumber_i INTEGER PRIMARY KEY,
  C_AllID_n INTEGER NOT NULL,
  C_Type_vc TEXT NOT NULL,
  C_Type2_vc TEXT,
  C_Type3_vc TEXT,
  C_ID_n INTEGER NOT NULL,
  C_GrpID_i INTEGER,
  C_Name_vc TEXT NOT NULL,
  C_Mobile_vc TEXT,
  C_Email_vc TEXT,
  C_Add1_vc TEXT,
  C_Add2_vc TEXT,
  C_Add3_vc TEXT,
  C_Pincode_vc TEXT,
  C_StateCode_vc TEXT,
  C_CmpName_vc TEXT,
  C_GSTIN_vc TEXT,
  C_PANNo_vc TEXT,
  C_Prop_vc TEXT,
  C_AreaCode_i INTEGER,
  C_RefBy_vc TEXT,
  C_OpBal_n REAL,
  C_OpType_vc TEXT,
  C_Show_vc TEXT,
  C_OrderBy_i INTEGER,
  C_UserID_vc TEXT,
  C_UpDate_d TEXT,
  C_Gender_vc TEXT,
  C_DOB_D TEXT,
  C_Age_i INTEGER,
  C_JoinDate_d TEXT,
  C_Desig_vc TEXT,
  C_Salary_n REAL,
  C_User_vc TEXT,
  C_UserName_vc TEXT,
  C_Pw_vc TEXT,
  C_DPer_n REAL,
  Emp_ID_i INTEGER,
  Emp_IDProof1_im BLOB,
  Emp_IDProof2_im BLOB,
  Emp_Photo_im BLOB,
  C_CLimit_n REAL,
  C_EmpType_vc TEXT,
  C_DueDays_i INTEGER,
  C_AllowBill_i INTEGER
);

-- tbl_DashBoard: 5 columns, 1 identity, effective key: D_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_DashBoard (
  D_RecordNo_i INTEGER PRIMARY KEY,
  D_Name_vc TEXT NOT NULL,
  D_Particular_vc TEXT,
  D_OrderNo_i INTEGER,
  D_Show_vc TEXT
);

-- tbl_DenoDtl: 6 columns, 1 identity, effective key: Deno_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_DenoDtl (
  Deno_RecordNo_i INTEGER PRIMARY KEY,
  Deno_CusID_i INTEGER,
  Deno_Billdate_d TEXT,
  Deno_SaveType_vc TEXT,
  Deno_Code_n REAL,
  Deno_Qty_vc INTEGER
);

-- tbl_Denomination: 5 columns, 0 identity, effective key: Deno_Code_n
CREATE TABLE IF NOT EXISTS tbl_Denomination (
  Deno_Entdate_d TEXT,
  Deno_Code_n REAL PRIMARY KEY,
  Deno_Name_vc TEXT,
  Deno_Qty_vc INTEGER,
  Deno_Particulars_vc TEXT
);

-- tbl_GSTTariff: 4 columns, 1 identity, effective key: GST_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_GSTTariff (
  GST_RecordNo_i INTEGER PRIMARY KEY,
  GST_CGSTP_n REAL,
  GST_SGSTP_n REAL,
  GST_Show_vc TEXT
);

-- tbl_GeneralSettings: 15 columns, 1 identity, effective key: GS_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_GeneralSettings (
  GS_RecordNo_i INTEGER PRIMARY KEY,
  GS_EstiGSTPer_vc TEXT,
  GS_PurForm_vc TEXT,
  GS_FrmMain_vc TEXT,
  GS_BillEditDate_d TEXT,
  GS_BillEditDays_i INTEGER,
  GS_RecBillPrint_vc TEXT,
  GS_InvBankPrint_vc TEXT,
  GS_EstiBankPrint_vc TEXT,
  PDFA4P_Bank_vc TEXT,
  PDFA4L_Bank_vc TEXT,
  PDFA5P_Bank_vc TEXT,
  MDIC_Backup_vc TEXT,
  GS_WeekDay_vc TEXT,
  GS_DGVMultiStock_vc TEXT
);

-- tbl_MenuMaster: 8 columns, 1 identity, effective key: MenuID
CREATE TABLE IF NOT EXISTS tbl_MenuMaster (
  MenuID INTEGER PRIMARY KEY,
  MenuName TEXT,
  FormName TEXT,
  FormState TEXT,
  ParentID INTEGER,
  MenuNameOrder INTEGER,
  FormNameOrder INTEGER,
  Menu_Show_vc TEXT
);

-- tbl_MultiStock: 38 columns, 1 identity, effective key: St_RecordNo2_i
CREATE TABLE IF NOT EXISTS tbl_MultiStock (
  St_EDate_d TEXT,
  St_RecordNo2_i INTEGER PRIMARY KEY,
  St_StNumber_i REAL NOT NULL,
  St_StockID_n REAL NOT NULL,
  St_ProName_vc TEXT,
  St_CurrStock_i REAL,
  St_BatchNo_vc TEXT,
  St_EMonth_i INTEGER,
  St_EYear_vc TEXT,
  St_PurRate_n REAL,
  St_SaleRate_n REAL,
  St_MRPRate_n REAL,
  St_UpDate_d TEXT,
  St_UID_vc TEXT,
  St_Show_vc TEXT,
  St_Barcode_vc TEXT,
  St_MarPer1_n REAL,
  St_MarPer2_n REAL,
  St_SaleRate2_n REAL,
  St_GoCurSt_i REAL,
  St_MediCurSt_i REAL,
  St_SupID_i INTEGER,
  St_PharID_i INTEGER,
  St_MFR_vc TEXT,
  St_Packchar_vc TEXT,
  Old_MRPRate_n REAL,
  Old_SaleRate_n REAL,
  MRP_LessPer_n REAL,
  MRP_LessAmt_n REAL,
  St_ExpStatus_vc TEXT,
  St_MulDisPer_n REAL,
  St_ProSize_vc TEXT,
  St_ProColor_vc TEXT,
  St_ModelName_vc TEXT,
  St_Pack_i INTEGER,
  St_NoteInt_i INTEGER,
  St_NoteNum_n REAL,
  St_NoteChar_vc TEXT
);

-- tbl_PurchDetail: 31 columns, 1 identity, effective key: Sa_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_PurchDetail (
  Sa_EntDate_d TEXT,
  Sa_RecordNo_i INTEGER PRIMARY KEY,
  Sa_Shift_vc TEXT,
  Sa_SaveType_vc TEXT,
  Sa_BillDate_d TEXT,
  Sa_BillTime_vc TEXT,
  Sa_InvNo_vc TEXT,
  Sa_BillNo_n REAL,
  Sa_BType_vc TEXT,
  Sa_CusID_n INTEGER,
  Sa_CusName_vc TEXT,
  Sa_GSTTotal_n REAL,
  Sa_TotalAmt_n REAL,
  Sa_RoType_vc TEXT,
  Sa_RoAmt_n REAL,
  Sa_FinalTotal_n REAL,
  Sa_Words_vc TEXT,
  Sa_GSTWords_vc TEXT,
  Sa_AcYear_vc TEXT,
  Sa_BillPoints_n REAL,
  Sa_TodaySave_n REAL,
  Sa_MRPTotal_n REAL,
  Sa_UserID_vc TEXT,
  Sa_UpDate_d TEXT,
  Sa_EmpID_i INTEGER,
  Sa_EmpPer_n REAL,
  Sa_EmpAmt_n REAL,
  Sa_COutAmt_n REAL,
  Sa_OldBal_n REAL,
  Sa_ComType_vc TEXT,
  Sa_GstCal_vc TEXT
);

-- tbl_PurchParticular: 32 columns, 1 identity, effective key: Sa_RecordNo2_i
CREATE TABLE IF NOT EXISTS tbl_PurchParticular (
  Sa_RecordNo2_i INTEGER PRIMARY KEY,
  Sa_SaveTy_vc TEXT,
  Sa_BillDate_d TEXT,
  Sa_InvNo_vc TEXT,
  Sa_CusID_n INTEGER,
  Sa_StNumber_n REAL,
  Sa_StCode_n INTEGER NOT NULL,
  Sa_PurRate_n REAL,
  Sa_MRP_n REAL,
  Sa_SaleRate_n REAL,
  Sa_ProDPer_n REAL,
  Sa_ProDAmt_n REAL,
  Sa_NetRate_n REAL,
  Sa_Qty_i REAL,
  Sa_FQty_i REAL,
  Sa_TQty_i REAL,
  Sa_QType_vc TEXT,
  Sa_Pack_vc TEXT,
  Sa_Taxable_n REAL,
  Sa_CGSTP_n REAL,
  Sa_SGSTP_n REAL,
  Sa_IGSTP_n REAL,
  Sa_CGSTAmt_n REAL,
  Sa_SGSTAmt_n REAL,
  Sa_IGSTAmt_n REAL,
  Sa_ProTotal_n REAL,
  Sa_PRemark_vc TEXT,
  Sa_SRTInv_vc TEXT,
  Sa_SchemeAmt_n REAL,
  Sa_WhSaleRate_n REAL,
  Sa_Cal1_n REAL,
  Sa_Cal2_n REAL
);

-- tbl_RptMaster: 7 columns, 1 identity, effective key: RM_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_RptMaster (
  RM_RecordNo_i INTEGER PRIMARY KEY,
  RM_Particulars_vc TEXT,
  RM_ParID_i INTEGER NOT NULL,
  RM_OrderBy_i INTEGER,
  RM_category_vc TEXT,
  RM_RptType_vc TEXT,
  RM_Show_vc TEXT
);

-- tbl_SalDetail: 40 columns, 1 identity, effective key: Sa_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_SalDetail (
  Sa_EntDate_d TEXT,
  Sa_RecordNo_i INTEGER PRIMARY KEY,
  Sa_Shift_vc TEXT,
  Sa_SaveType_vc TEXT,
  Sa_BillDate_d TEXT,
  Sa_BillTime_vc TEXT,
  Sa_InvNo_vc TEXT,
  Sa_BillNo_n REAL,
  Sa_BType_vc TEXT,
  Sa_CusID_n INTEGER,
  Sa_CusName_vc TEXT,
  Sa_GSTTotal_n REAL,
  Sa_TotalAmt_n REAL,
  Sa_RoType_vc TEXT,
  Sa_RoAmt_n REAL,
  Sa_FinalTotal_n REAL,
  Sa_Words_vc TEXT,
  Sa_GSTWords_vc TEXT,
  Sa_AcYear_vc TEXT,
  Sa_BillPoints_n REAL,
  Sa_TodaySave_n REAL,
  Sa_MRPTotal_n REAL,
  Sa_UserID_vc TEXT,
  Sa_UpDate_d TEXT,
  Sa_EmpID_i INTEGER,
  Sa_EmpPer_n REAL,
  Sa_EmpAmt_n REAL,
  Sa_COutAmt_n REAL,
  Sa_OldBal_n REAL,
  Sa_ComType_vc TEXT,
  Sa_MarPer_n REAL,
  Sa_MarAmt_n REAL,
  Sa_DueDate_d TEXT,
  Sa_Remark_vc TEXT,
  Sa_DeliAdd_vc TEXT,
  Sa_VehicleNo_vc TEXT,
  Sa_RecNo_i INTEGER,
  Sa_OldInvNo_vc TEXT,
  Sa_BefDisAmt_n REAL,
  Sa_DiscountAmt_n REAL
);

-- tbl_SalParticular: 33 columns, 1 identity, effective key: Sa_RecordNo2_i
CREATE TABLE IF NOT EXISTS tbl_SalParticular (
  Sa_RecordNo2_i INTEGER PRIMARY KEY,
  Sa_SaveTy_vc TEXT,
  Sa_BillDate_d TEXT,
  Sa_InvNo_vc TEXT,
  Sa_CusID_n INTEGER,
  Sa_StNumber_n REAL,
  Sa_StCode_n INTEGER NOT NULL,
  Sa_PurRate_n REAL,
  Sa_MRP_n REAL,
  Sa_SaleRate_n REAL,
  Sa_ProDPer_n REAL,
  Sa_ProDAmt_n REAL,
  Sa_NetRate_n REAL,
  Sa_Qty_i REAL,
  Sa_FQty_i REAL,
  Sa_TQty_i REAL,
  Sa_QType_vc TEXT,
  Sa_Pack_vc TEXT,
  Sa_Taxable_n REAL,
  Sa_CGSTP_n REAL,
  Sa_SGSTP_n REAL,
  Sa_IGSTP_n REAL,
  Sa_CGSTAmt_n REAL,
  Sa_SGSTAmt_n REAL,
  Sa_IGSTAmt_n REAL,
  Sa_ProTotal_n REAL,
  Sa_PRemark_vc TEXT,
  Sa_SRTInv_vc TEXT,
  Sa_DumRate_n REAL,
  Sa_SaleMar_n REAL,
  Sa_SaleMarAmt_n REAL,
  Sa_Delivery_vc TEXT,
  Sa_DisCalType_vc TEXT
);

-- tbl_Settings: 5 columns, 1 identity, effective key: Set_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_Settings (
  Set_RecordNo_i INTEGER PRIMARY KEY,
  Set_TblName_vc TEXT,
  Set_Particular_vc TEXT,
  Set_Condition_vc TEXT,
  Set_SetShow_vc TEXT
);

-- tbl_SmUserRights: 8 columns, 1 identity, effective key: UR_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_SmUserRights (
  UR_RecordNo_i INTEGER PRIMARY KEY,
  UR_UserID_i INTEGER NOT NULL,
  UR_Form_vc TEXT NOT NULL,
  UR_View_b INTEGER NOT NULL,
  UR_Add_b INTEGER NOT NULL,
  UR_Edit_b INTEGER NOT NULL,
  UR_Delete_b INTEGER NOT NULL,
  Ent_date_d TEXT
);

-- tbl_StockDtl: 41 columns, 1 identity, effective key: St_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_StockDtl (
  St_RecordNo_i INTEGER PRIMARY KEY,
  St_StockID_n REAL NOT NULL,
  St_ProName_vc TEXT,
  St_ProTamil_vc TEXT,
  St_Unit_vc TEXT,
  St_HSN_vc TEXT,
  St_CGSTPer_n REAL,
  St_SGSTPer_n REAL,
  St_IGSTPer_n REAL,
  St_TotalSt_i REAL,
  St_Shelf_vc TEXT,
  St_Row_vc TEXT,
  St_RemiStock_i INTEGER,
  St_Remark_vc TEXT,
  St_Decrease_vc TEXT,
  St_CatID_i INTEGER,
  St_BrandID_i INTEGER,
  St_ElCom_n REAL,
  St_DisPer_n REAL,
  St_EntDate_d TEXT,
  St_GoTotalSt_i REAL,
  St_MediTotalSt_i REAL,
  St_ProcedureID_i INTEGER,
  St_NillDate_d TEXT,
  St_MolID_i INTEGER,
  St_OpStockDate_d TEXT,
  Old_CGSTPer_n REAL,
  Old_SGSTPer_n REAL,
  St_MediType_vc TEXT,
  St_ProPic_im BLOB,
  St_ValType_vc TEXT,
  Old_MRPRate_n REAL,
  MRP_LessPer_n REAL,
  MRP_LessAmt_n REAL,
  St_Margin1Cal_vc TEXT,
  St_Margin2Cal_vc TEXT,
  St_CmpID_i INTEGER,
  st_Guarantee_n REAL,
  St_Warranty_n REAL,
  St_PackCal_vc TEXT,
  St_Liquor_vc TEXT
);

-- tbl_StockModify: 40 columns, 1 identity, effective key: St_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_StockModify (
  St_EDate_d TEXT,
  St_RecordNo_i INTEGER PRIMARY KEY,
  St_PharmacyID_n INTEGER,
  St_PharmacyName_vc TEXT,
  St_StockID_n REAL NOT NULL,
  St_MediID_n INTEGER,
  St_MediName_vc TEXT,
  St_BatchNo_vc TEXT,
  St_HSN_vc TEXT,
  St_EMonth_i INTEGER,
  St_EYear_vc TEXT,
  St_MRPRate_n REAL,
  St_SaleRate_n REAL,
  St_PurRate_n REAL,
  St_CGSTPer_n REAL,
  St_SGSTPer_n REAL,
  St_IGSTPer_n REAL,
  St_GodownSt_i REAL,
  St_ShopSt_i REAL,
  St_TotalSt_i REAL,
  St_Show_vc TEXT,
  St_RemiStock_i INTEGER,
  St_Shelf_vc TEXT,
  St_Row_vc TEXT,
  St_NillDate_d TEXT,
  St_Pack_vc TEXT,
  St_Remark_vc TEXT,
  St_Decrease_vc TEXT,
  St_UID_vc TEXT,
  St_UpDate_d TEXT,
  St_Barcode_vc TEXT,
  St_ValType_vc TEXT,
  St_ProTamil_vc TEXT,
  St_ProPic_im BLOB,
  St_Unit_vc TEXT,
  St_CatID_i INTEGER,
  St_BrandID_i INTEGER,
  St_Form_vc TEXT,
  St_Modify_vc TEXT,
  St_InvNo_vc TEXT
);

-- tbl_UnitList: 5 columns, 1 identity, effective key: U_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_UnitList (
  U_RecordNo_i INTEGER PRIMARY KEY,
  U_EName_vc TEXT NOT NULL,
  U_TName_vc TEXT,
  U_OrderNo_i INTEGER,
  U_Show_vc TEXT
);

-- tbl_UserPermission: 5 columns, 1 identity, effective key: Menu_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_UserPermission (
  Ent_Date_d TEXT,
  Menu_Name_vc TEXT,
  Display_Name_vc TEXT,
  admin_vc TEXT,
  Menu_RecordNo_i INTEGER PRIMARY KEY
);

-- tbl_UserRoleMenu: 3 columns, 1 identity, effective key: URM_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_UserRoleMenu (
  URM_RecordNo_i INTEGER PRIMARY KEY,
  URM_EmpID_i INTEGER,
  URM_MenuID_i INTEGER
);

-- tbl_UserRoles: 4 columns, 1 identity, effective key: Ur_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_UserRoles (
  Ur_RecordNo_i INTEGER PRIMARY KEY,
  Ur_FormName_vc TEXT,
  Ur_Particular_vc TEXT,
  admin_vc TEXT
);

-- tbl_UserList: 5 columns, 1 identity, effective key: U_RecordNumber_i
CREATE TABLE IF NOT EXISTS tbl_UserList (
  U_RecordNumber_i INTEGER PRIMARY KEY,
  U_EmpID_i INTEGER NOT NULL,
  U_EmpName_vc TEXT NOT NULL,
  U_UserName_vc TEXT,
  U_Password_vc TEXT
);

-- tbl_ActivityLog: 8 columns, 1 identity, effective key: AL_RecordNo_i
CREATE TABLE IF NOT EXISTS tbl_ActivityLog (
  AL_RecordNo_i INTEGER PRIMARY KEY,
  AL_Date_d TEXT,
  AL_UserName_vc TEXT,
  AL_Form_vc TEXT,
  AL_Action_vc TEXT,
  AL_Details_vc TEXT,
  AL_Machine_vc TEXT,
  Ent_date_d TEXT
);

-- Indexes for the access paths the application actually uses.
CREATE INDEX IF NOT EXISTS ix_saldetail_date   ON tbl_SalDetail (Sa_BillDate_d);
CREATE INDEX IF NOT EXISTS ix_saldetail_no     ON tbl_SalDetail (Sa_InvNo_vc);
CREATE INDEX IF NOT EXISTS ix_salpart_inv      ON tbl_SalParticular (Sa_InvNo_vc);
CREATE INDEX IF NOT EXISTS ix_purchdetail_date ON tbl_PurchDetail (Sa_BillDate_d);
CREATE INDEX IF NOT EXISTS ix_purchpart_inv    ON tbl_PurchParticular (Sa_InvNo_vc);
CREATE INDEX IF NOT EXISTS ix_stockdtl_code    ON tbl_StockDtl (St_StockID_n);
CREATE INDEX IF NOT EXISTS ix_multistock_code  ON tbl_MultiStock (St_StockID_n);
CREATE INDEX IF NOT EXISTS ix_cusprofile_name  ON tbl_CusProfile (C_Name_vc);
