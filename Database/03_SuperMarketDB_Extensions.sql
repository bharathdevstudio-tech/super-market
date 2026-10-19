/* =====================================================================
   03_SuperMarketDB_Extensions.sql
   SuperMarket module - additive extensions on top of the legacy retail
   schema (tbl_*).  Idempotent: safe to run many times.
   Target DB : SuperMarketDB  (SQL Server 2008 R2 Express)
   ===================================================================== */
SET NOCOUNT ON;
GO

/* ---- tbl_Category : GST % + visibility flag ------------------------- */
IF COL_LENGTH('tbl_Category','Cat_GSTPer_n') IS NULL
    ALTER TABLE tbl_Category ADD Cat_GSTPer_n numeric(18,2) NULL;
GO
IF COL_LENGTH('tbl_Category','Cat_Show_vc') IS NULL
    ALTER TABLE tbl_Category ADD Cat_Show_vc varchar(10) NULL;
GO

/* ---- tbl_Brand : visibility flag ------------------------------------ */
IF COL_LENGTH('tbl_Brand','Brand_Show_vc') IS NULL
    ALTER TABLE tbl_Brand ADD Brand_Show_vc varchar(10) NULL;
GO

/* ---- tbl_BillSeries : numbering stays exactly like the legacy retail
   POS  (series from B_BillSeries_vc + MAX(Sa_BillNo_n)+1), so no extra
   counter column is required.  Drop it if an earlier revision added it. */
IF COL_LENGTH('tbl_BillSeries','BS_CurrNo_i') IS NOT NULL
    ALTER TABLE tbl_BillSeries DROP COLUMN BS_CurrNo_i;
GO

/* ---- tbl_SmUserRights : per-user per-form action rights -------------
   The legacy schema only stores menu grants (tbl_UserRoleMenu).  This
   table adds real VIEW / ADD / EDIT / DELETE rights for the SuperMarket
   Security module; when a user has no row the code falls back to the
   legacy menu grant.                                                */
IF OBJECT_ID('tbl_SmUserRights','U') IS NULL
BEGIN
    CREATE TABLE tbl_SmUserRights
    (
        UR_RecordNo_i int IDENTITY(1,1) NOT NULL,
        UR_UserID_i   int          NOT NULL,
        UR_Form_vc    varchar(100) NOT NULL,
        UR_View_b     bit          NOT NULL CONSTRAINT DF_tbl_SmUserRights_View   DEFAULT (1),
        UR_Add_b      bit          NOT NULL CONSTRAINT DF_tbl_SmUserRights_Add    DEFAULT (1),
        UR_Edit_b     bit          NOT NULL CONSTRAINT DF_tbl_SmUserRights_Edit   DEFAULT (1),
        UR_Delete_b   bit          NOT NULL CONSTRAINT DF_tbl_SmUserRights_Delete DEFAULT (1),
        Ent_date_d    datetime     NULL,
        CONSTRAINT PK_tbl_SmUserRights PRIMARY KEY (UR_RecordNo_i)
    );
END
GO

/* ---- tbl_ActivityLog : audit trail (not present in legacy schema) --- */
IF OBJECT_ID('tbl_ActivityLog','U') IS NULL
BEGIN
    CREATE TABLE tbl_ActivityLog
    (
        AL_RecordNo_i  int IDENTITY(1,1) NOT NULL,
        AL_Date_d      datetime      NULL,
        AL_UserName_vc varchar(50)   NULL,
        AL_Form_vc     varchar(100)  NULL,
        AL_Action_vc   varchar(50)   NULL,
        AL_Details_vc  varchar(500)  NULL,
        AL_Machine_vc  varchar(50)   NULL,
        Ent_date_d     datetime      NULL,
        CONSTRAINT PK_tbl_ActivityLog PRIMARY KEY (AL_RecordNo_i)
    );
END
GO

PRINT '03_SuperMarketDB_Extensions applied.';
GO
