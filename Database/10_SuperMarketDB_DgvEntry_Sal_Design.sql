/* =====================================================================
   SuperMarketDB - POS billing screen design rows
   (FrmSmPosBilling grid + header/totals panel).

   These are the same design tables the legacy FrmDgvSalesRetail reads.

   Convention used here (identical for both tables):
       BD_ColumnName_vc  = the name the code works with
                           - grid rows  -> DataGridView column Name
                             ("ItemName" is what the row maths uses)
                           - header rows-> the sales column the field is
                             bound to ("Sa_FinalTotal_n")
       BD_AliasName_vc   = what the user sees (column header / label text)
       BD_SQLColumn_vc   = the physical expression behind the field.  For grid
                           rows it is used by LoadSavedBill() to build the
                           SELECT that re-opens a saved bill, so it must name
                           real columns of the billing join:
                               tbl_SalDetail sa
                                 LEFT JOIN tbl_SalParticular sp ON sp.Sa_InvNo_vc = sa.Sa_InvNo_vc
                                 LEFT JOIN tbl_MultiStock     m  ON m.St_StNumber_i  = sp.Sa_StNumber_n
                                 LEFT JOIN tbl_StockDtl       s  ON s.St_StockID_n   = sp.Sa_StCode_n
                           An empty value means "not read back" (computed).
       BD_ColOrderby_i   = display order
       BD_ColShow_vc     = YES -> visible (NO also works for grid columns,
                           which are created hidden instead of skipped)
       BD_ControlType_vc = TEXTBOX / COMBO / DATE
       BD_ReadOnly_vc    = YES / NO
       BD_Width_i        = pixel width, or FILL to take the remaining width
       BD_ComboSQL_vc    = for COMBO: a SELECT returning one value per row
       BD_Format_vc      = N0..N4 / STRING / dd/MM/yyyy
       BD_Alignment_vc   = LEFT / CENTER / RIGHT

   FrmSmPosBilling reads them in LoadDgvBilling(), LoadPayableAdd() and
   LoadSavedBill().  Both tables may be emptied - the form then falls back to
   the columns / captions already defined in the designer, so a missing design
   row can never leave the billing screen unusable.

   Idempotent: safe to run more than once.
   Run with:  sqlcmd -S .\SQLEXPRESS -E -i 10_SuperMarketDB_DgvEntry_Sal_Design.sql
   ===================================================================== */
USE SuperMarketDB;
GO
SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------
   1. Billing grid (tbl_DgvEntry_Sal).

   StNo carries the stock number used to merge a repeated scan of the same
   item; it stays hidden.  SaleRate / Qty / DisPer / Remark are the only
   editable columns, the rest is calculated by RecalcRow().
   --------------------------------------------------------------------- */
/* The DELETE lists every identifier this script has ever owned, so re-running
   it after the naming convention changed also removes the older rows. */
DELETE FROM tbl_DgvEntry_Sal
WHERE BD_ColumnName_vc IN ('Sa_SNo_i','SNoB','Code','Sa_StCode_n','StNo','Sa_StNumber_n',
                           'ItemName','St_ProName_vc','Unit','St_Unit_vc','MRP','Sa_MRP_n',
                           'SaleRate','Sa_SaleRate_n','Qty','Sa_Qty_i','DisPer','Sa_ProDPer_n',
                           'DisAmt','Sa_ProDAmt_n','GSTPer','Sa_GSTPer_n','CGSTAmt','Sa_CGSTAmt_n',
                           'SGSTAmt','Sa_SGSTAmt_n','NetRate','Sa_NetRate_n','Amount','Sa_ProTotal_n',
                           'Remark','Sa_PRemark_vc')
   OR BD_AliasName_vc IN ('SNoB','Code','StNo','ItemName','Unit','MRP','SaleRate','Qty','DisPer',
                          'DisAmt','GSTPer','CGSTAmt','SGSTAmt','NetRate','Amount','Remark',
                          'SNo','Item Name','Rate','Dis %','Disc','GST %','CGST','SGST','Net Rate');
GO

INSERT INTO tbl_DgvEntry_Sal
    (BD_ColumnName_vc, BD_AliasName_vc, BD_SQLColumn_vc, BD_ColOrderby_i, BD_ColShow_vc,
     BD_ControlType_vc, BD_ReadOnly_vc, BD_BackColor_vc, BD_ForeColor_vc, BD_Width_i,
     BD_Height_i, BD_ComboSQL_vc, BD_Format_vc, BD_Alignment_vc, BD_Mandatory_vc, BD_MaxLength_i)
VALUES
    ('SNoB',     'SNo',       '',                         1,  'YES','TEXTBOX','YES', '', '', '50',   '', '', 'STRING','CENTER','NO',''),
    ('Code',     'Code',      'sp.Sa_StCode_n',           2,  'YES','TEXTBOX','YES', '', '', '65',   '', '', 'STRING','LEFT',  'NO',''),
    ('StNo',     'StNo',      'sp.Sa_StNumber_n',         3,  'NO', 'TEXTBOX','YES', '', '', '60',   '', '', 'STRING','LEFT',  'NO',''),
    ('ItemName', 'Item Name', 's.St_ProName_vc',          4,  'YES','TEXTBOX','YES', '', '', 'FILL', '', '', 'STRING','LEFT',  'NO',''),
    ('Unit',     'Unit',      's.St_Unit_vc',             5,  'YES','TEXTBOX','YES', '', '', '60',   '', '', 'STRING','CENTER','NO',''),
    ('MRP',      'MRP',       'sp.Sa_MRP_n',              6,  'YES','TEXTBOX','YES', '', '', '75',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('SaleRate', 'Rate',      'sp.Sa_SaleRate_n',         7,  'YES','TEXTBOX','NO',  '', '', '80',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('Qty',      'Qty',       'sp.Sa_Qty_i',              8,  'YES','TEXTBOX','NO',  '', '', '70',   '', '', 'N3',    'RIGHT', 'NO',''),
    ('DisPer',   'Dis %',     'sp.Sa_ProDPer_n',          9,  'YES','TEXTBOX','NO',  '', '', '65',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('DisAmt',   'Disc',      'sp.Sa_ProDAmt_n',          10, 'YES','TEXTBOX','YES', '', '', '80',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('GSTPer',   'GST %',     'sp.Sa_CGSTP_n + sp.Sa_SGSTP_n', 11, 'YES','TEXTBOX','YES', '', '', '65',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('CGSTAmt',  'CGST',      'sp.Sa_CGSTAmt_n',          12, 'YES','TEXTBOX','YES', '', '', '80',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('SGSTAmt',  'SGST',      'sp.Sa_SGSTAmt_n',          13, 'YES','TEXTBOX','YES', '', '', '80',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('NetRate',  'Net Rate',  'sp.Sa_NetRate_n',          14, 'YES','TEXTBOX','YES', '', '', '85',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('Amount',   'Amount',    'sp.Sa_ProTotal_n',         15, 'YES','TEXTBOX','YES', '', '', '95',   '', '', 'N2',    'RIGHT', 'NO',''),
    ('Remark',   'Remark',    'sp.Sa_PRemark_vc',         16, 'YES','TEXTBOX','NO',  '', '', '120',  '', '', 'STRING','LEFT',  'NO','');
GO

/* ---------------------------------------------------------------------
   2. Header / totals panel (tbl_DgvEntry_SalPayDtl).

   BD_ColumnName_vc is the key LoadPayableAdd() matches on and
   BD_AliasName_vc the caption it writes to the label.  The Bill Type
   combo takes its items from BD_ComboSQL_vc.
   --------------------------------------------------------------------- */
DELETE FROM tbl_DgvEntry_SalPayDtl
WHERE BD_ColumnName_vc IN ('Sa_SaveType_vc','Sa_BillDate_d','Sa_BillNo_n','Sa_CusID_n',
                           'Sa_CusName_vc','Sa_CusMobile_vc','TotalItem','TotalQty',
                           'Sa_BefDisAmt_n','Sa_DiscountAmt_n','Sa_Taxable_n','Sa_GSTTotal_n',
                           'Sa_CGSTAmt_n','Sa_SGSTAmt_n','Sa_RoAmt_n','Sa_FinalTotal_n');
GO

INSERT INTO tbl_DgvEntry_SalPayDtl
    (BD_ColumnName_vc, BD_AliasName_vc, BD_SQLColumn_vc, BD_ColOrderby_i, BD_ColShow_vc,
     BD_ControlType_vc, BD_ReadOnly_vc, BD_BackColor_vc, BD_ForeColor_vc, BD_Width_i,
     BD_Height_i, BD_ComboSQL_vc, BD_Format_vc, BD_Alignment_vc, BD_Mandatory_vc, BD_MaxLength_i)
VALUES
    ('Sa_SaveType_vc',   'Bill Type',   'Sa_SaveType_vc',   1,  'YES','COMBO',  'NO',  '', '', '170', '',
        'SELECT ''INVOICE'' UNION ALL SELECT ''ESTIMATE'' UNION ALL SELECT ''SALES RETURN''',
        'STRING','LEFT',  'YES',''),
    ('Sa_BillDate_d',    'Bill Date',   'sa.Sa_BillDate_d', 2,  'YES','DATE',   'NO',  '', '', '150', '',
        '', 'dd/MM/yyyy','LEFT','YES',''),
    ('Sa_BillNo_n',      'Bill No',     'Sa_BillNo_n',      3,  'YES','TEXTBOX','YES', '', '', '150', '',
        '', 'STRING','LEFT','NO',''),
    ('Sa_CusID_n',       'Customer',    'Sa_CusID_n',       4,  'YES','COMBO',  'NO',  '', '', '420', '',
        '', 'STRING','LEFT','NO',''),
    ('Sa_CusName_vc',    'Name',        'C_C_Name_vc',      5,  'YES','TEXTBOX','NO',  '', '', '320', '',
        '', 'STRING','LEFT','NO',''),
    ('Sa_CusMobile_vc',  'Mobile',      'C_C_Mobile_vc',    6,  'YES','TEXTBOX','NO',  '', '', '160', '',
        '', 'STRING','LEFT','NO',''),
    ('TotalItem',        'Items',       '',                 7,  'YES','TEXTBOX','YES', '', '', '80',  '',
        '', 'N0',    'RIGHT','NO',''),
    ('TotalQty',         'Total Qty',   '',                 8,  'YES','TEXTBOX','YES', '', '', '100', '',
        '', 'N3',    'RIGHT','NO',''),
    ('Sa_BefDisAmt_n',   'Sub Total',   'Sa_BefDisAmt_n',   9,  'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_DiscountAmt_n', 'Discount',    'Sa_DiscountAmt_n', 10, 'YES','TEXTBOX','NO',  '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_Taxable_n',     'Taxable',     'Sa_Taxable_n',     11, 'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_GSTTotal_n',    'Total Tax',   'Sa_GSTTotal_n',    12, 'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_CGSTAmt_n',     'CGST',        'Sa_CGSTAmt_n',     13, 'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_SGSTAmt_n',     'SGST',        'Sa_SGSTAmt_n',     14, 'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_RoAmt_n',       'Round Off',   'Sa_RoAmt_n',       15, 'YES','TEXTBOX','YES', '', '', '120', '',
        '', 'N2',    'RIGHT','NO',''),
    ('Sa_FinalTotal_n',  'Bill Amount', 'Sa_FinalTotal_n',  16, 'YES','TEXTBOX','YES', '', '', '150', '',
        '', 'N2',    'RIGHT','NO','');
GO

PRINT '10_SuperMarketDB_DgvEntry_Sal_Design.sql completed.';
GO
