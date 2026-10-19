/* =====================================================================================
   SuperMarket Management & POS Billing  -  Seed / Default Configuration
   -------------------------------------------------------------------------------------
   Database : SuperMarketDB
   Purpose  : inserts only CONFIGURATION / LOOKUP rows required for the app to start.
              No business data (no products, no bills, no customers) is inserted.
   Safe     : guarded inserts - running twice does not duplicate rows.
   Run with : sqlcmd -S .\SQLEXPRESS -E -d SuperMarketDB -i 02_SuperMarketDB_Seed.sql
   ===================================================================================== */

SET NOCOUNT ON;
USE [SuperMarketDB];
GO

/* ---- Units ------------------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM tbl_UnitList)
INSERT INTO tbl_UnitList (U_EName_vc, U_OrderNo_i, U_Show_vc) VALUES
 ('NOS',1,'YES'),('KG',2,'YES'),('GM',3,'YES'),('LTR',4,'YES'),('ML',5,'YES'),
 ('PCS',6,'YES'),('PKT',7,'YES'),('BOX',8,'YES'),('DOZ',9,'YES'),('BAG',10,'YES'),('MTR',11,'YES');
GO

/* ---- GST Tax master --------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_TaxMaster)
INSERT INTO tbl_TaxMaster (Tax_Name_vc, Tax_CGSTPer_n, Tax_SGSTPer_n, Tax_IGSTPer_n, Tax_Type_vc, Tax_HSN_vc, Tax_Show_vc) VALUES
 ('GST EXEMPT', 0.00,  0.00,  0.00,  'Exclusive', '',    'YES'),
 ('GST 5',      2.50,  2.50,  5.00,  'Exclusive', '',    'YES'),
 ('GST 12',     6.00,  6.00,  12.00, 'Exclusive', '',    'YES'),
 ('GST 18',     9.00,  9.00,  18.00, 'Exclusive', '',    'YES'),
 ('GST 28',     14.00, 14.00, 28.00, 'Exclusive', '',    'YES');
GO

/* ---- Payment modes ---------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_PaymentMode)
INSERT INTO tbl_PaymentMode (Pay_Name_vc, Pay_Show_vc) VALUES
 ('CASH','YES'),('CARD','YES'),('UPI','YES'),('CREDIT','YES'),('WALLET','YES');
GO

/* ---- Account groups --------------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_AccountGroup)
INSERT INTO tbl_AccountGroup (AG_Name_vc, AG_Type_vc, AG_Show_vc) VALUES
 ('Purchase','EXPENSE','YES'),('Sales','INCOME','YES'),('Direct Expense','EXPENSE','YES'),
 ('Indirect Expense','EXPENSE','YES'),('Cash','ASSET','YES'),('Bank','ASSET','YES'),
 ('Sundry Debtors','ASSET','YES'),('Sundry Creditors','LIABILITY','YES');
GO

/* ---- Default expense ledgers ------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM tbl_Accountledgers)
INSERT INTO tbl_Accountledgers (Acc_LedgerName_vc, Acc_Group_vc, Acc_OpBal_n, Acc_OpBalType_vc, Acc_Show_vc) VALUES
 ('Salary','Indirect Expense',0,'DR','YES'),
 ('Rent','Indirect Expense',0,'DR','YES'),
 ('Electricity','Indirect Expense',0,'DR','YES'),
 ('Transport','Direct Expense',0,'DR','YES'),
 ('Packing','Direct Expense',0,'DR','YES'),
 ('Misc Expense','Indirect Expense',0,'DR','YES');
GO

/* ---- Financial-year bill series (FY 2026-2027) ------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE BS_Type_vc='SALES'      AND BS_FY_vc='2026-2027')
INSERT INTO tbl_BillSeries (BS_Type_vc,BS_Series_vc,BS_FY_vc,BS_Prefix_vc,BS_FromNo_i,BS_ToNo_i,BS_CurrNo_i,BS_Show_vc)
VALUES ('SALES','SM','2026-2027','SM',1,999999,0,'YES');
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE BS_Type_vc='PURCHASE'   AND BS_FY_vc='2026-2027')
INSERT INTO tbl_BillSeries (BS_Type_vc,BS_Series_vc,BS_FY_vc,BS_Prefix_vc,BS_FromNo_i,BS_ToNo_i,BS_CurrNo_i,BS_Show_vc)
VALUES ('PURCHASE','PM','2026-2027','PM',1,999999,0,'YES');
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE BS_Type_vc='SALE_RETURN' AND BS_FY_vc='2026-2027')
INSERT INTO tbl_BillSeries (BS_Type_vc,BS_Series_vc,BS_FY_vc,BS_Prefix_vc,BS_FromNo_i,BS_ToNo_i,BS_CurrNo_i,BS_Show_vc)
VALUES ('SALE_RETURN','SR','2026-2027','SR',1,999999,0,'YES');
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE BS_Type_vc='PUR_RETURN'  AND BS_FY_vc='2026-2027')
INSERT INTO tbl_BillSeries (BS_Type_vc,BS_Series_vc,BS_FY_vc,BS_Prefix_vc,BS_FromNo_i,BS_ToNo_i,BS_CurrNo_i,BS_Show_vc)
VALUES ('PUR_RETURN','PR','2026-2027','PR',1,999999,0,'YES');
IF NOT EXISTS (SELECT 1 FROM tbl_BillSeries WHERE BS_Type_vc='RECEIPT'     AND BS_FY_vc='2026-2027')
INSERT INTO tbl_BillSeries (BS_Type_vc,BS_Series_vc,BS_FY_vc,BS_Prefix_vc,BS_FromNo_i,BS_ToNo_i,BS_CurrNo_i,BS_Show_vc)
VALUES ('RECEIPT','RC','2026-2027','RC',1,999999,0,'YES');
GO

/* ---- Default security roles ------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_UserRoles)
INSERT INTO tbl_UserRoles (Role_Name_vc, Role_Show_vc) VALUES
 ('ADMIN','YES'),('MANAGER','YES'),('CASHIER','YES');
GO

/* ---- Default admin user ----------------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_UserList)
INSERT INTO tbl_UserList (U_EmpID_i, U_EmpName_vc, U_UserName_vc, U_Password_vc, U_Type_vc, U_Show_vc, Ent_date_d)
VALUES (1, 'SOFTWARE', 'admin', 'admin', 'ADMIN', 'YES', GETDATE());
GO

/* ---- Default company profile row -------------------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_Companyprofile)
INSERT INTO tbl_Companyprofile (Cmp_Name_vc, Cmp_Caption_vc, Cmp_Address_vc, Cmp_State_vc, Cmp_GSTcal_vc, Cmp_Barcode_vc, Ent_date_d)
VALUES ('SUPER MARKET', 'SUPER MARKET', '', '', 'Exclusive', 'YES', GETDATE());
GO

/* ---- Menu master : SuperMarket module forms --------------------------------------- */
IF NOT EXISTS (SELECT 1 FROM tbl_MenuMaster WHERE MenuName='SuperMarket')
BEGIN
    INSERT INTO tbl_MenuMaster (MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder)
    VALUES ('SuperMarket', '', 'GROUP', 0, 1, 0);

    DECLARE @Root int = SCOPE_IDENTITY();

    INSERT INTO tbl_MenuMaster (MenuName, FormName, FormState, ParentID, MenuNameOrder, FormNameOrder) VALUES
     ('Dashboard',        'FrmSmDashboard',  'Dashboard',  @Root, 1, 1),
     ('Category Master',  'FrmSmCategory',   'Master',     @Root, 2, 1),
     ('Sub Category',     'FrmSmSubCategory','Master',     @Root, 2, 2),
     ('Brand Master',     'FrmSmBrand',      'Master',     @Root, 2, 3),
     ('Unit Master',      'FrmSmUnit',       'Master',     @Root, 2, 4),
     ('Tax Master',       'FrmSmTax',        'Master',     @Root, 2, 5),
     ('Rack Master',      'FrmSmRack',       'Master',     @Root, 2, 6),
     ('Payment Mode',     'FrmSmPaymentMode','Master',     @Root, 2, 7),
     ('Barcode Master',   'FrmSmBarcode',    'Master',     @Root, 2, 8),
     ('Product Master',   'FrmSmProduct',    'Master',     @Root, 2, 9),
     ('Supplier Master',  'FrmSmSupplier',   'Master',     @Root, 2, 10),
     ('Customer Master',  'FrmSmCustomer',   'Master',     @Root, 2, 11),
     ('User Master',      'FrmSmUser',       'Security',   @Root, 2, 12),
     ('User Rights',      'FrmSmUserRights', 'Security',   @Root, 2, 13),
     ('POS Billing',      'FrmSmPos',        'Transaction',@Root, 3, 1),
     ('Purchase Entry',   'FrmSmPurchase',   'Transaction',@Root, 3, 2),
     ('Purchase Return',  'FrmSmPurchaseReturn','Transaction',@Root,3,3),
     ('Sales Return',     'FrmSmSalesReturn','Transaction',@Root, 3, 4),
     ('Stock Adjustment', 'FrmSmStockAdjustment','Inventory',@Root,4,1),
     ('Stock Transfer',   'FrmSmStockTransfer','Inventory',@Root,4,2),
     ('Damage Entry',     'FrmSmDamage',     'Inventory',  @Root, 4, 3),
     ('Stock Report',     'FrmSmStockReport','Inventory',  @Root, 4, 4),
     ('Expense Entry',    'FrmSmExpense',    'Accounts',   @Root, 5, 1),
     ('Cash Book',        'FrmSmCashBook',   'Accounts',   @Root, 5, 2),
     ('Day Closing',      'FrmSmDayClosing', 'Accounts',   @Root, 5, 3),
     ('Sales Report',     'FrmSmSalesReport','Reports',    @Root, 6, 1),
     ('Purchase Report',  'FrmSmPurchaseReport','Reports', @Root, 6, 2),
     ('Profit Report',    'FrmSmProfitReport','Reports',   @Root, 6, 3),
     ('GST Report',       'FrmSmGstReport',  'Reports',    @Root, 6, 4);
END
GO

/* ---- Grant every existing menu to ADMIN -------------------------------------------- */
INSERT INTO tbl_UserRoleMenu (URM_EmpID_i, URM_MenuID_i)
SELECT 1, m.MenuID FROM tbl_MenuMaster m
WHERE NOT EXISTS (SELECT 1 FROM tbl_UserRoleMenu u WHERE u.URM_EmpID_i=1 AND u.URM_MenuID_i=m.MenuID);
GO

PRINT 'SuperMarketDB seed data inserted successfully.';
GO
