Imports System.Data
Imports System.Data.SqlClient

Module VRenewalchk

    Public Function RenewalUpdate() As Boolean

        Try

            Dim sql As String = ""

            sql &= "IF COL_LENGTH('dbo.tbl_Companyprofile','Updated_Date_d') IS NULL "
            sql &= "ALTER TABLE dbo.tbl_Companyprofile ADD Updated_Date_d DATETIME NULL;"

            'License date = Today + 1 Year
            sql &= "UPDATE dbo.tbl_Companyprofile "
            'sql &= "SET Updated_Date_d = DATEADD(YEAR, 1, GETDATE());"
            sql &= "SET Updated_Date_d = DATEADD(YEAR, 1, Updated_Date_d);"

            If VistawinFunctions.ServerConnection.State <>
               ConnectionState.Open Then

                VistawinFunctions.ServerConnection.Open()

            End If


            Using cmd As New SqlCommand(
                sql,
                VistawinFunctions.ServerConnection)

                cmd.ExecuteNonQuery()

            End Using


            Return True


        Catch ex As Exception

            MessageBox.Show(
                "License update error." &
                vbCrLf &
                ex.Message,
                "License Update",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

            Return False

        End Try


    End Function
   
    Private Sub JMChit()

        Try

            If VistawinFunctions.ServerConnection.State <>
               ConnectionState.Open Then

                VistawinFunctions.ServerConnection.Open()

            End If


            '=================================================
            ' Q2 - CREATE TABLE
            '=================================================

            Dim Q2 As String = ""

            Q2 &= "IF OBJECT_ID('dbo.tbl_Transactions20262027','U') IS NULL "
            Q2 &= "BEGIN "

            Q2 &= "CREATE TABLE [dbo].[tbl_Transactions20262027]("
            Q2 &= "[Ent_Date_d] [datetime] NULL,"
            Q2 &= "[Acc_ID_i] [numeric](18,0) IDENTITY(1,1) NOT NULL,"
            Q2 &= "[Acc_VocDate_d] [datetime] NULL,"
            Q2 &= "[Acc_VocSeries_vc] [varchar](20) NOT NULL,"
            Q2 &= "[Acc_VocType_vc] [varchar](20) NOT NULL,"
            Q2 &= "[Acc_VocNumber_n] [numeric](18,0) NOT NULL,"
            Q2 &= "[Acc_LedgerCode_n] [numeric](18,0) NOT NULL,"
            Q2 &= "[Acc_AccountType1_vc] [varchar](6) NULL,"
            Q2 &= "[Acc_OtherCode_n] [numeric](18,0) NOT NULL,"
            Q2 &= "[Acc_AccountType2_vc] [varchar](6) NULL,"
            Q2 &= "[Acc_TrnAmount_n] [numeric](18,2) NULL,"
            Q2 &= "[Chq_number_vc] [varchar](20) NULL,"
            Q2 &= "[Acc_PayMode_vc] [varchar](50) NULL,"
            Q2 &= "[Chq_Date_d] [datetime] NULL,"
            Q2 &= "[Chq_Bank_vc] [varchar](40) NULL,"
            Q2 &= "[Chq_BankPlace_vc] [varchar](40) NULL,"
            Q2 &= "[Grp_Name_vc] [varchar](20) NULL,"
            Q2 &= "[Auc_No_i] [int] NULL,"
            Q2 &= "[Acc_Narration_vc] [varchar](250) NULL,"
            Q2 &= "[User_Id_vc] [varchar](10) NULL"
            Q2 &= ") "

            Q2 &= "END"


            Using Cmd As New SqlCommand(
                Q2,
                VistawinFunctions.ServerConnection)

                Cmd.ExecuteNonQuery()

            End Using


            '=================================================
            ' Q1 - CREATE / ALTER STORED PROCEDURE
            '=================================================

            Dim Q1 As String = ""

            Q1 &= "CREATE OR ALTER PROCEDURE [dbo].[SP_tbl_Transactions20262027] "
            Q1 &= "("
            Q1 &= "@Ent_Date_d datetime,"
            Q1 &= "@Acc_ID_i int,"
            Q1 &= "@Acc_VocDate_d datetime,"
            Q1 &= "@Acc_VocSeries_vc varchar(20),"
            Q1 &= "@Acc_VocNumber_n numeric(18,0),"
            Q1 &= "@Acc_Ledgercode_n numeric(18,0),"
            Q1 &= "@Acc_Othercode_n numeric(18,0),"
            Q1 &= "@Acc_TrnAmount_n numeric(18,2),"
            Q1 &= "@Acc_Accounttype1_vc varchar(6),"
            Q1 &= "@Acc_Accounttype2_vc varchar(6),"
            Q1 &= "@Chq_number_vc varchar(20),"
            Q1 &= "@Chq_date_d datetime,"
            Q1 &= "@Chq_bank_vc varchar(40),"
            Q1 &= "@Chq_bankplace_vc varchar(40),"
            Q1 &= "@Acc_Narration_vc varchar(250),"
            Q1 &= "@User_id_vc varchar(10),"
            Q1 &= "@Inputstring varchar(15)"
            Q1 &= ") "
            Q1 &= "AS "
            Q1 &= "BEGIN "
            Q1 &= "TRAN "

            Q1 &= "DECLARE @OldVocAmt Decimal(18,2) "
            Q1 &= "SET @OldVocAmt = "
            Q1 &= "(SELECT Acc_TrnAmount_n "
            Q1 &= "FROM tbl_Transactions20262027 "
            Q1 &= "WHERE Acc_VocSeries_vc=@Acc_VocSeries_vc "
            Q1 &= "AND Acc_VocNumber_n=@Acc_VocNumber_n) "

            '-----------------------------------------------
            ' ADD
            '-----------------------------------------------

            Q1 &= "IF @Inputstring='Add' "
            Q1 &= "INSERT INTO tbl_Transactions20262027 "
            Q1 &= "(Ent_Date_d,Acc_VocDate_d,Acc_VocType_vc,"
            Q1 &= "Acc_VocSeries_vc,Acc_VocNumber_n,"
            Q1 &= "Acc_Othercode_n,Acc_Ledgercode_n,"
            Q1 &= "Acc_TrnAmount_n,Acc_Accounttype1_vc,"
            Q1 &= "Acc_Accounttype2_vc,Chq_number_vc,"
            Q1 &= "Chq_date_d,Chq_bank_vc,Chq_bankplace_vc,"
            Q1 &= "Acc_Narration_vc,User_id_vc) "

            Q1 &= "VALUES("
            Q1 &= "@Ent_Date_d,@Acc_VocDate_d,@Acc_VocSeries_vc,"
            Q1 &= "@Acc_VocSeries_vc,@Acc_VocNumber_n,"
            Q1 &= "@Acc_Othercode_n,@Acc_Ledgercode_n,"
            Q1 &= "@Acc_TrnAmount_n,@Acc_Accounttype1_vc,"
            Q1 &= "@Acc_Accounttype2_vc,@Chq_number_vc,"
            Q1 &= "@Chq_date_d,@Chq_bank_vc,@Chq_bankplace_vc,"
            Q1 &= "@Acc_Narration_vc,@User_id_vc) "


            '-----------------------------------------------
            ' EDIT
            '-----------------------------------------------

            Q1 &= "IF @Inputstring='Edit' "
            Q1 &= "UPDATE tbl_Transactions20262027 SET "
            Q1 &= "Ent_Date_d=@Ent_Date_d,"
            Q1 &= "Acc_VocDate_d=@Acc_VocDate_d,"
            Q1 &= "Acc_VocType_vc=@Acc_VocSeries_vc,"
            Q1 &= "Acc_Othercode_n=@Acc_Othercode_n,"
            Q1 &= "Acc_Ledgercode_n=@Acc_Ledgercode_n,"
            Q1 &= "Acc_TrnAmount_n=@Acc_TrnAmount_n,"
            Q1 &= "Acc_Accounttype1_vc=@Acc_Accounttype1_vc,"
            Q1 &= "Acc_Accounttype2_vc=@Acc_Accounttype2_vc,"
            Q1 &= "Chq_number_vc=@Chq_number_vc,"
            Q1 &= "Chq_date_d=@Chq_date_d,"
            Q1 &= "Chq_bank_vc=@Chq_bank_vc,"
            Q1 &= "Chq_bankplace_vc=@Chq_bankplace_vc,"
            Q1 &= "Acc_Narration_vc=@Acc_Narration_vc,"
            Q1 &= "User_id_vc=@User_id_vc "
            Q1 &= "WHERE Acc_VocSeries_vc=@Acc_VocSeries_vc "
            Q1 &= "AND Acc_VocNumber_n=@Acc_VocNumber_n "


            '-----------------------------------------------
            ' DELETE
            '-----------------------------------------------

            Q1 &= "IF @Inputstring='Delete' "
            Q1 &= "DELETE FROM tbl_Transactions20262027 "
            Q1 &= "WHERE Acc_VocSeries_vc=@Acc_VocSeries_vc "
            Q1 &= "AND Acc_VocNumber_n=@Acc_VocNumber_n "


            Q1 &= "COMMIT "
            Q1 &= "END"


            Using Cmd As New SqlCommand(
                Q1,
                VistawinFunctions.ServerConnection)

                Cmd.ExecuteNonQuery()

            End Using


            MessageBox.Show(
                "JM Chit Q1 / Q2 database update completed.",
                "Database Update",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information)


        Catch ex As Exception

            MessageBox.Show(
                "JM Chit Update Error" &
                vbCrLf &
                ex.Message,
                "Database Update",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        End Try

    End Sub
    Private Sub SBEElec()

    End Sub
    Private Sub NaveenPharma()

    End Sub
End Module