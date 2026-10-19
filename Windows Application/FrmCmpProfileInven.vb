Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports

Imports System.Drawing
Imports System.Drawing.Imaging.ImageFormat
Imports System.Drawing.Drawing2D
Imports System.Drawing.Graphics

Imports System.IO.Ports
Imports System.Management
Imports System.Threading
Imports System.Windows.Forms
Public Class FrmCmpProfileInven

    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Adapter As New SqlDataAdapter
    Dim Dataset, Dataset1 As New WinDataSet
    Dim Ds, Ds1 As New DataSet
    Dim Cmd, Cmd1, command As New SqlCommand

    Dim Str As String
    Dim Row, RowIndex, iColumnindex, irowindex As Integer
    Dim DateFormat, Execute As Boolean
    Dim FromBool, ToBool, DateBo3, DateBo4, DateBo5 As Boolean
    Dim Opbal As Decimal = 0
    Dim Expenditure1, Income1 As Decimal
    Dim REdit As Boolean = False

    Dim Bool, Bool1 As Boolean

    Dim OpenFD, OpenFD2, OpenFD3 As New OpenFileDialog
    Dim StrFileName, StrFileName2, StrFileName3 As String
    Dim ImageData1, ImageData2, ImageData3 As Byte()
    Dim MS1, MS2, MS3 As New MemoryStream()
    Dim NMS1, NMS2, NMS3 As New MemoryStream()
    Dim fbd As New FolderBrowserDialog
    Dim fbd1 As New FolderBrowserDialog
    Dim fbd2 As FileDialog
    Dim fbd3 As FileDialog
    Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, ByVal keyData As System.Windows.Forms.Keys) As Boolean
        Try
            If msg.WParam.ToInt32 = CInt(Keys.Escape) Then
                Dim MsgStr As DialogResult = MessageBox.Show("Do you want close ?", "Confirmation", MessageBoxButtons.YesNo)
                If MsgStr = 6 Then
                    Me.Close()
                Else
                    Return True
                    Exit Function
                End If
            ElseIf msg.WParam.ToInt32 = CInt(Keys.F4) Then
                Return True
            End If
            Return MyBase.ProcessCmdKey(msg, keyData)
        Catch ex As Exception
            Return False
        End Try
    End Function

    Private Sub FrmCmpProfile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.WindowState = FormWindowState.Normal
        Me.DateOfIntall()
        Me.CompanyProfile()

        txtInstallDate.Focus()
    End Sub
    Private Sub DateOfIntall()

        Try
            Dim DT As New DataTable
            Using DA As New SqlDataAdapter("SELECT Ent_Date_d FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection)
                DA.Fill(DT)
            End Using

            If DT.Rows.Count = 0 Then Exit Sub

            Dim InstallDate As Date = DT.Rows(0)("Ent_Date_d")
            Dim TotalDays As Integer = DateDiff(DateInterval.Day, InstallDate, Today)

            txtInstallDate.Text = Format(InstallDate, "dd/MM/yyyy")
            txtYearIns.Text = TotalDays \ 364
            txtMonthIns.Text = (TotalDays \ 30) - ((TotalDays \ 364) * 12)
            txtDaysIns.Text = TotalDays Mod 30
            txtTotalDays.Text = TotalDays

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub CompanyProfileNew()

        Try

            Dim dt As New DataTable

            Using da As New SqlDataAdapter( _
                "SELECT * FROM tbl_CompanyProfile", _
                VistawinFunctions.ServerConnection)

                da.Fill(dt)

            End Using

            If dt.Rows.Count = 0 Then Exit Sub

            Dim dr As DataRow = dt.Rows(0)


            '========================================
            ' SQL COLUMN -> CONTROL
            '========================================
            Dim Map As New Dictionary(Of String, Control)

            Map.Add("Cmp_ID_i", txtCompanyID)
            Map.Add("Cmp_Proprietor_vc", txtProprietor)
            Map.Add("Cmp_LicenseNo_vc", txtLicenseNo)
            Map.Add("Cmp_Name_vc", txtCompanyName)
            Map.Add("Cmp_Caption_vc", txtCaption)
            Map.Add("Cmp_Address_vc", txtAddress1)
            Map.Add("Cmp_District_vc", txtDistrict)
            Map.Add("Cmp_Pincode_vc", txtPincode)
            Map.Add("Cmp_PhoneNo_vc", txtPhoneNo)
            Map.Add("Cmp_MobileNo_n", txtMobileNo)
            Map.Add("Cmp_EMail_vc", txtEmailID)
            Map.Add("Cmp_Website_vc", txtWebsite)

            Map.Add("Mast_Password_vc", txtMastPassword)

            Map.Add("BackupPath_vc", txtBackupPath)
            Map.Add("ScanningPath_vc", txtScanPath)

            Map.Add("SMS_SenderID_vc", txtSMSSenderID)
            Map.Add("SMS_UserID_vc", txtSMSUserID)
            Map.Add("SMS_Password_vc", txtSMSPassword)
            Map.Add("SendSMS_vc", CbCType)

            Map.Add("Cmp_ServerName_vc", txtServerName)
            Map.Add("Cmp_DatabaseName_vc", txtDatabaseName)

            Map.Add("Cmp_BankDetails_vc", txtBankDetails)
            Map.Add("Moving_Word_vc", txtMovingWord)
            Map.Add("Cmp_ReportHead_vc", txtReportHead)
            Map.Add("Cmp_ToMail_vc", txtToMail)

            Map.Add("Bill_ZeroStock_vc", CbBZeroS)
            Map.Add("Bill_AftPrint_vc", CbBAfterP)

            Map.Add("Cmp_ExpDays_i", txtExpDays)
            Map.Add("Cmp_GDrivePath_vc", txtGDrivePath)

            Map.Add("Cmp_UpiID_vc", txtUPIID)
            Map.Add("Cmp_UpiName_vc", txtUpiName)


            '========================================
            ' AUTO LOAD
            '========================================
            For Each item As KeyValuePair(Of String, Control) In Map

                If dt.Columns.Contains(item.Key) Then

                    If dr.IsNull(item.Key) Then
                        item.Value.Text = ""
                    Else
                        item.Value.Text = dr(item.Key).ToString()
                    End If

                End If

            Next


            '========================================
            ' IMAGES
            '========================================
            LoadDbImage(dr, "Cmp_Logo_im", PictureBox1)
            LoadDbImage(dr, "Cmp_ISO_im", PBISOImage)
            LoadDbImage(dr, "Cmp_BackImage_im", PictureBox3)


            '========================================
            ' PASSWORD
            '========================================
            txtMastPassword.UseSystemPasswordChar = _
                Not CBPassword.Checked


        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub
    Private Sub LoadDbImage(ByVal dr As DataRow, _
                        ByVal ColumnName As String, _
                        ByVal Pic As PictureBox)

        Try

            If Not dr.Table.Columns.Contains(ColumnName) Then Exit Sub

            If dr.IsNull(ColumnName) Then

                Pic.BackgroundImage = Nothing
                Exit Sub

            End If


            Dim Data() As Byte = _
                DirectCast(dr(ColumnName), Byte())


            Using ms As New MemoryStream(Data)

                Using img As Image = Image.FromStream(ms)

                    Pic.BackgroundImage = _
                        DirectCast(img.Clone(), Image)

                End Using

            End Using


        Catch

            Pic.BackgroundImage = Nothing

        End Try

    End Sub
    Private Sub CompanyProfile()

        Try
            Ds1 = New DataSet
            Adapter = New SqlDataAdapter("SELECT * FROM tbl_CompanyProfile ", VistawinFunctions.ServerConnection)
            Adapter.Fill(Ds1, "BindCompany")
            If Ds1.Tables("BindCompany").Rows.Count > 0 Then

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ID_i")) Then
                    txtCompanyID.Text = String.Empty
                Else
                    txtCompanyID.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ID_i")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Proprietor_vc")) Then
                    txtProprietor.Text = String.Empty
                Else
                    txtProprietor.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Proprietor_vc")
                End If
                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_LicenseNo_vc")) Then
                    txtLicenseNo.Text = String.Empty
                Else
                    txtLicenseNo.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_LicenseNo_vc")
                End If
                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Name_vc")) Then
                    txtCompanyName.Text = String.Empty
                Else
                    txtCompanyName.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Name_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Caption_vc")) Then
                    txtCaption.Text = String.Empty
                Else
                    txtCaption.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Caption_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Address_vc")) Then
                    txtAddress1.Text = String.Empty
                Else
                    txtAddress1.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Address_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_District_vc")) Then
                    txtDistrict.Text = String.Empty
                Else
                    txtDistrict.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_District_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Pincode_vc")) Then
                    txtPincode.Text = String.Empty
                Else
                    txtPincode.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Pincode_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_PhoneNo_vc")) Then
                    txtPhoneNo.Text = String.Empty
                Else
                    txtPhoneNo.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_PhoneNo_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_MobileNo_n")) Then
                    txtMobileNo.Text = String.Empty
                Else
                    txtMobileNo.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_MobileNo_n")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_EMail_vc")) Then
                    txtEmailID.Text = String.Empty
                Else
                    txtEmailID.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_EMail_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Website_vc")) Then
                    txtWebsite.Text = String.Empty
                Else
                    txtWebsite.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Website_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Mast_Password_vc")) Then
                    txtMastPassword.Text = String.Empty
                Else
                    txtMastPassword.Text = Ds1.Tables("BindCompany").Rows(0).Item("Mast_Password_vc")
                    If CBPassword.Checked = False Then
                        txtMastPassword.UseSystemPasswordChar = True
                    End If
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Logo_im")) Then
                    PictureBox1.BackgroundImage = Nothing
                    ImageData1 = Nothing
                Else
                    ImageData1 = DirectCast(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_Logo_im"), Byte())
                    Using NMS1 As New MemoryStream(ImageData1, 0, ImageData1.Length)
                        NMS1.Write(ImageData1, 0, ImageData1.Length)
                        PictureBox1.BackgroundImage = Image.FromStream(NMS1, True)
                        NMS1.Close()
                        NMS1.Dispose()
                    End Using
                    'ImageData = Nothing
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("BackupPath_vc")) Then
                    txtBackupPath.Text = String.Empty
                Else
                    txtBackupPath.Text = Ds1.Tables("BindCompany").Rows(0).Item("BackupPath_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("ScanningPath_vc")) Then
                    txtScanPath.Text = String.Empty
                Else
                    txtScanPath.Text = Ds1.Tables("BindCompany").Rows(0).Item("ScanningPath_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("SMS_SenderID_vc")) Then
                    txtSMSSenderID.Text = String.Empty
                Else
                    txtSMSSenderID.Text = Ds1.Tables("BindCompany").Rows(0).Item("SMS_SenderID_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("SMS_UserID_vc")) Then
                    txtSMSUserID.Text = String.Empty
                Else
                    txtSMSUserID.Text = Ds1.Tables("BindCompany").Rows(0).Item("SMS_UserID_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("SMS_Password_vc")) Then
                    txtSMSPassword.Text = String.Empty
                Else
                    txtSMSPassword.Text = Ds1.Tables("BindCompany").Rows(0).Item("SMS_Password_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("SendSMS_vc")) Then
                    CbCType.Text = String.Empty
                Else
                    CbCType.Text = Ds1.Tables("BindCompany").Rows(0).Item("SendSMS_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ServerName_vc")) Then
                    txtServerName.Text = String.Empty
                Else
                    txtServerName.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ServerName_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_DatabaseName_vc")) Then
                    txtDatabaseName.Text = String.Empty
                Else
                    txtDatabaseName.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_DatabaseName_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_BankDetails_vc")) Then
                    txtBankDetails.Text = String.Empty
                Else
                    txtBankDetails.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_BankDetails_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Moving_Word_vc")) Then
                    txtMovingWord.Text = String.Empty
                Else
                    txtMovingWord.Text = Ds1.Tables("BindCompany").Rows(0).Item("Moving_Word_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ISO_im")) Then
                    PBISOImage.BackgroundImage = Nothing
                    ImageData2 = Nothing
                Else
                    ImageData2 = DirectCast(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ISO_im"), Byte())
                    Using NMS2 As New MemoryStream(ImageData2, 0, ImageData2.Length)
                        NMS2.Write(ImageData2, 0, ImageData2.Length)
                        PBISOImage.BackgroundImage = Image.FromStream(NMS2, True)
                        NMS2.Close()
                        NMS2.Dispose()
                    End Using
                    'ImageData = Nothing
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ReportHead_vc")) Then
                    txtReportHead.Text = String.Empty
                Else
                    txtReportHead.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ReportHead_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ToMail_vc")) Then
                    txtToMail.Text = String.Empty
                Else
                    txtToMail.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ToMail_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_BackImage_im")) Then
                    PictureBox3.BackgroundImage = Nothing
                    ImageData3 = Nothing
                Else
                    ImageData3 = DirectCast(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_BackImage_im"), Byte())
                    Using NMS3 As New MemoryStream(ImageData3, 0, ImageData3.Length)
                        NMS3.Write(ImageData3, 0, ImageData3.Length)
                        PictureBox3.BackgroundImage = Image.FromStream(NMS3, True)
                        NMS3.Close()
                        NMS3.Dispose()
                    End Using
                    'ImageData3 = Nothing
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Bill_ZeroStock_vc")) Then
                    CbBZeroS.Text = String.Empty
                Else
                    CbBZeroS.Text = Ds1.Tables("BindCompany").Rows(0).Item("Bill_ZeroStock_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Bill_AftPrint_vc")) Then
                    CbBAfterP.Text = String.Empty
                Else
                    CbBAfterP.Text = Ds1.Tables("BindCompany").Rows(0).Item("Bill_AftPrint_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ExpDays_i")) Then
                    txtExpDays.Text = String.Empty
                Else
                    txtExpDays.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_ExpDays_i")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_GDrivePath_vc")) Then
                    txtGDrivePath.Text = String.Empty
                Else
                    txtGDrivePath.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_GDrivePath_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_UpiID_vc")) Then
                    txtUPIID.Text = String.Empty
                Else
                    txtUPIID.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_UpiID_vc")
                End If

                If IsDBNull(Ds1.Tables("BindCompany").Rows(0).Item("Cmp_UpiName_vc")) Then
                    txtUpiName.Text = String.Empty
                Else
                    txtUpiName.Text = Ds1.Tables("BindCompany").Rows(0).Item("Cmp_UpiName_vc")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub UpateCmpany()

        Try
            DS = New DataSet
            Adapter = New SqlDataAdapter("SElECT *,GETDATE() AS'TodayDate' FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection)
            Adapter.Fill(DS, "Company")
            If DS.Tables("Company").Rows.Count > 0 Then

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Name_vc")) Then
                    VistawinFunctions.CmpName = DS.Tables("Company").Rows(0).Item("Cmp_Name_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Address_vc")) Then
                    VistawinFunctions.CmpAddress = DS.Tables("Company").Rows(0).Item("Cmp_Address_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_District_vc")) Then
                    VistawinFunctions.CmpDistrict = DS.Tables("Company").Rows(0).Item("Cmp_District_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_State_vc")) Then
                    VistawinFunctions.CmpState = DS.Tables("Company").Rows(0).Item("Cmp_State_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Pincode_vc")) Then
                    VistawinFunctions.CmpPhCode = DS.Tables("Company").Rows(0).Item("Cmp_Pincode_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_MobileNo_n")) Then
                    VistawinFunctions.CmpMobileNo = DS.Tables("Company").Rows(0).Item("Cmp_MobileNo_n")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Email_vc")) Then
                    VistawinFunctions.CmpEmailID = DS.Tables("Company").Rows(0).Item("Cmp_Email_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Website_vc")) Then
                    VistawinFunctions.CmpWebSite = DS.Tables("Company").Rows(0).Item("Cmp_Website_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Proprietor_vc")) Then
                    VistawinFunctions.CmpProprietor = DS.Tables("Company").Rows(0).Item("Cmp_Proprietor_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_LicenseNo_vc")) Then
                    VistawinFunctions.CmpLicenseNo = DS.Tables("Company").Rows(0).Item("Cmp_LicenseNo_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_PhoneNo_vc")) Then
                    VistawinFunctions.CmpPhoneNo = DS.Tables("Company").Rows(0).Item("Cmp_PhoneNo_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("TodayDate")) Then
                    VistawinFunctions.TodayDate = DS.Tables("Company").Rows(0).Item("TodayDate")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Mast_Password_vc")) Then
                    VistawinFunctions.MastPassword = DS.Tables("Company").Rows(0).Item("Mast_Password_vc")
                End If
                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_Logo_im")) Then
                    VistawinFunctions.CmpLogo = DS.Tables("Company").Rows(0).Item("Cmp_Logo_im")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("BackupPath_vc")) Then
                    VistawinFunctions.BackupPath = DS.Tables("Company").Rows(0).Item("BackupPath_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("ScanningPath_vc")) Then
                    VistawinFunctions.ScanningPath = DS.Tables("Company").Rows(0).Item("ScanningPath_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("SendSMS_vc")) Then
                    VistawinFunctions.SendSMS = DS.Tables("Company").Rows(0).Item("SendSMS_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("SMS_SenderID_vc")) Then
                    VistawinFunctions.SMSSenderID = DS.Tables("Company").Rows(0).Item("SMS_SenderID_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("SMS_UserID_vc")) Then
                    VistawinFunctions.SMSUserID = DS.Tables("Company").Rows(0).Item("SMS_UserID_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("SMS_Password_vc")) Then
                    VistawinFunctions.SMSPassword = DS.Tables("Company").Rows(0).Item("SMS_Password_vc")
                End If
                If IsDBNull(DS.Tables("Company").Rows(0).Item("BillingType_vc")) Then
                    VistawinFunctions.PrintingType = "DEFAULT"
                Else
                    VistawinFunctions.PrintingType = DS.Tables("Company").Rows(0).Item("BillingType_vc")
                End If

                If IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_SaveImage_vc")) Then
                    VistawinFunctions.SaveImage = "NO"
                Else
                    VistawinFunctions.SaveImage = DS.Tables("Company").Rows(0).Item("Cmp_SaveImage_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_ServerName_vc")) Then
                    VistawinFunctions.CmpServerName = DS.Tables("Company").Rows(0).Item("Cmp_ServerName_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_DataBaseName_vc")) Then
                    VistawinFunctions.CmpDatabaseName = DS.Tables("Company").Rows(0).Item("Cmp_DataBaseName_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_BankDetails_vc")) Then
                    VistawinFunctions.CmpBankDetails = DS.Tables("Company").Rows(0).Item("Cmp_BankDetails_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Moving_Word_vc")) Then
                    VistawinFunctions.CmpMovingWord = DS.Tables("Company").Rows(0).Item("Moving_Word_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_ISO_im")) Then
                    VistawinFunctions.CmpISO = DS.Tables("Company").Rows(0).Item("Cmp_ISO_im")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_ReportHead_vc")) Then
                    VistawinFunctions.CmpReportHead = DS.Tables("Company").Rows(0).Item("Cmp_ReportHead_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_ToMail_vc")) Then
                    VistawinFunctions.CmpToEmail = DS.Tables("Company").Rows(0).Item("Cmp_ToMail_vc")
                End If

                If Not IsDBNull(DS.Tables("Company").Rows(0).Item("Cmp_BackImage_im")) Then
                    VistawinFunctions.CmpBackImage = DS.Tables("Company").Rows(0).Item("Cmp_BackImage_im")
                End If

                If IsDBNull(Ds.Tables("Company").Rows(0).Item("Bill_ZeroStock_vc")) Then
                    VistawinFunctions.CmpBZS = "NO"
                Else
                    VistawinFunctions.CmpBZS = Ds.Tables("Company").Rows(0).Item("Bill_ZeroStock_vc")
                End If

                If IsDBNull(Ds.Tables("Company").Rows(0).Item("Bill_AftPrint_vc")) Then
                    VistawinFunctions.CmpBAfP = "NO"
                Else
                    VistawinFunctions.CmpBAfP = Ds.Tables("Company").Rows(0).Item("Bill_AftPrint_vc")
                End If

                If IsDBNull(Ds.Tables("Company").Rows(0).Item("Cmp_ExpDays_i")) Then
                    VistawinFunctions.CmpExpDays = 0
                Else
                    VistawinFunctions.CmpExpDays = Ds.Tables("Company").Rows(0).Item("Cmp_ExpDays_i")
                End If

                If Not IsDBNull(Ds.Tables("Company").Rows(0).Item("Cmp_GDrivePath_vc")) Then
                    VistawinFunctions.GDrivePath = Ds.Tables("Company").Rows(0).Item("Cmp_GDrivePath_vc")
                End If

                If Not IsDBNull(Ds.Tables("Company").Rows(0).Item("Cmp_UpiID_vc")) Then
                    VistawinFunctions.UpiID = Ds.Tables("Company").Rows(0).Item("Cmp_UpiID_vc")
                End If

                If Not IsDBNull(Ds.Tables("Company").Rows(0).Item("Cmp_UpiName_vc")) Then
                    VistawinFunctions.UpiName = Ds.Tables("Company").Rows(0).Item("Cmp_UpiName_vc")
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If txtCompanyName.Text = String.Empty Then
            MsgBox("Company Name is Empty")
            TabControl1.SelectedIndex = 0
            txtCompanyName.Focus()
            Exit Sub
        ElseIf txtAddress1.Text = String.Empty Then
            MsgBox("Company Address is Empty")
            TabControl1.SelectedIndex = 0
            txtAddress1.Focus()
            Exit Sub
        ElseIf txtBackupPath.Text = String.Empty Then
            MsgBox("Backup Path is Empty")
            'TabControl1.SelectedTab = TabPage2
            TabControl1.SelectedIndex = 1
            btnBrowseBackup.Focus()
            Exit Sub
        ElseIf txtScanPath.Text = String.Empty Then
            MsgBox("Scanning Path is Empty")
            TabControl1.SelectedIndex = 1
            btnBrowseScan.Focus()
            Exit Sub
        End If

        Me.CompanyAddORUpdate()
        MsgBox("Once Close the Application and Again Open!")

        FrmMDICommon.Close()

    End Sub

    Private Sub CompanyAddORUpdate()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Using cmd As New SqlCommand("[dbo].[SP_tbl_CompanyProfile]", VistawinFunctions.ServerConnection, Trans)
                cmd.CommandType = CommandType.StoredProcedure

                Dim En_Date_d As New SqlParameter("@Ent_Date_d", SqlDbType.DateTime)
                En_Date_d.Value = Date.Today
                cmd.Parameters.Add(En_Date_d)

                Dim Cmp_ID_i As New SqlParameter("@Cmp_ID_i", SqlDbType.Int)
                Cmp_ID_i.Value = CInt(txtCompanyID.Text)
                cmd.Parameters.Add(Cmp_ID_i)

                Dim Company_Name_vc As New SqlParameter("@Cmp_Name_vc", SqlDbType.NVarChar)
                Company_Name_vc.Value = txtCompanyName.Text
                cmd.Parameters.Add(Company_Name_vc)

                Dim Estd_Date_d As New SqlParameter("@Estd_Date_d", SqlDbType.DateTime)
                Estd_Date_d.Value = DBNull.Value
                cmd.Parameters.Add(Estd_Date_d)

                Dim Cmp_RegDate_d As New SqlParameter("@Cmp_RegDate_d", SqlDbType.DateTime)
                Cmp_RegDate_d.Value = DBNull.Value
                cmd.Parameters.Add(Cmp_RegDate_d)

                Dim Cmp_Caption_vc As New SqlParameter("@Cmp_Caption_vc", SqlDbType.NVarChar)
                If txtCaption.Text = String.Empty Then
                    Cmp_Caption_vc.Value = DBNull.Value
                Else
                    Cmp_Caption_vc.Value = txtCaption.Text
                End If
                cmd.Parameters.Add(Cmp_Caption_vc)

                Dim Cmp_Proprietor_vc As New SqlParameter("@Cmp_Proprietor_vc", SqlDbType.NVarChar)
                If txtProprietor.Text = String.Empty Then
                    Cmp_Proprietor_vc.Value = DBNull.Value
                Else
                    Cmp_Proprietor_vc.Value = txtProprietor.Text
                End If
                cmd.Parameters.Add(Cmp_Proprietor_vc)

                Dim Cmp_LicenseNo_vc As New SqlParameter("@Cmp_LicenseNo_vc", SqlDbType.VarChar)
                If txtLicenseNo.Text = String.Empty Then
                    Cmp_LicenseNo_vc.Value = DBNull.Value
                Else
                    Cmp_LicenseNo_vc.Value = txtLicenseNo.Text
                End If
                cmd.Parameters.Add(Cmp_LicenseNo_vc)

                Dim Cmp_Addr1_vc As New SqlParameter("@Cmp_Address_vc", SqlDbType.NVarChar)
                If txtAddress1.Text = String.Empty Then
                    Cmp_Addr1_vc.Value = DBNull.Value
                Else
                    Cmp_Addr1_vc.Value = txtAddress1.Text
                End If
                cmd.Parameters.Add(Cmp_Addr1_vc)

                Dim Cmp_District_vc As New SqlParameter("@Cmp_District_vc", SqlDbType.NVarChar)
                Cmp_District_vc.Value = txtDistrict.Text
                cmd.Parameters.Add(Cmp_District_vc)

                Dim Cmp_Pincode_vc As New SqlParameter("@Cmp_Pincode_vc", SqlDbType.VarChar)
                Cmp_Pincode_vc.Value = txtPincode.Text
                cmd.Parameters.Add(Cmp_Pincode_vc)

                Dim Cmp_PhoneNo_vc As New SqlParameter("@Cmp_PhoneNo_vc", SqlDbType.VarChar)
                If txtPhoneNo.Text = String.Empty Then
                    Cmp_PhoneNo_vc.Value = DBNull.Value
                Else
                    Cmp_PhoneNo_vc.Value = txtPhoneNo.Text
                End If
                cmd.Parameters.Add(Cmp_PhoneNo_vc)

                Dim Cmp_MobileNo_n As New SqlParameter("@Cmp_MobileNo_n", SqlDbType.VarChar)
                If txtMobileNo.Text = String.Empty Then
                    Cmp_MobileNo_n.Value = DBNull.Value
                Else
                    Cmp_MobileNo_n.Value = txtMobileNo.Text
                End If
                cmd.Parameters.Add(Cmp_MobileNo_n)

                Dim Cmp_EMail_vc As New SqlParameter("@Cmp_EMail_vc", SqlDbType.VarChar)
                If txtEmailID.Text = String.Empty Then
                    Cmp_EMail_vc.Value = DBNull.Value
                Else
                    Cmp_EMail_vc.Value = txtEmailID.Text
                End If
                cmd.Parameters.Add(Cmp_EMail_vc)

                Dim Cmp_Website_vc As New SqlParameter("@Cmp_Website_vc", SqlDbType.VarChar)
                If txtWebsite.Text = String.Empty Then
                    Cmp_Website_vc.Value = DBNull.Value
                Else
                    Cmp_Website_vc.Value = txtWebsite.Text
                End If
                cmd.Parameters.Add(Cmp_Website_vc)

                Dim Mast_Password_vc As New SqlParameter("@Mast_Password_vc", SqlDbType.NVarChar)
                If txtMastPassword.Text = String.Empty Then
                    Mast_Password_vc.Value = DBNull.Value
                Else
                    Mast_Password_vc.Value = txtMastPassword.Text
                End If
                cmd.Parameters.Add(Mast_Password_vc)

                Dim Cmp_Logo_im As New SqlParameter("@Cmp_Logo_im", SqlDbType.Image)
                If ImageData1 Is Nothing Then
                    Cmp_Logo_im.Value = DBNull.Value
                Else
                    Cmp_Logo_im.Value = ImageData1
                End If
                cmd.Parameters.Add(Cmp_Logo_im)

                Dim BackupPath_vc As New SqlParameter("@BackupPath_vc", SqlDbType.VarChar)
                If txtBackupPath.Text = String.Empty Then
                    BackupPath_vc.Value = DBNull.Value
                Else
                    BackupPath_vc.Value = txtBackupPath.Text
                End If
                cmd.Parameters.Add(BackupPath_vc)

                Dim ScanningPath_vc As New SqlParameter("@ScanningPath_vc", SqlDbType.VarChar)
                If txtScanPath.Text = String.Empty Then
                    ScanningPath_vc.Value = DBNull.Value
                Else
                    ScanningPath_vc.Value = txtScanPath.Text
                End If
                cmd.Parameters.Add(ScanningPath_vc)

                Dim SendSMS_vc As New SqlParameter("@SendSMS_vc", SqlDbType.VarChar)
                If CbCType.Text = String.Empty Then
                    SendSMS_vc.Value = "NO"
                Else
                    SendSMS_vc.Value = CbCType.Text
                End If
                cmd.Parameters.Add(SendSMS_vc)

                Dim SMS_SenderID_vc As New SqlParameter("@SMS_SenderID_vc", SqlDbType.VarChar)
                If txtSMSSenderID.Text = String.Empty Then
                    SMS_SenderID_vc.Value = DBNull.Value
                Else
                    SMS_SenderID_vc.Value = txtSMSSenderID.Text
                End If
                cmd.Parameters.Add(SMS_SenderID_vc)

                Dim SMS_UserID_vc As New SqlParameter("@SMS_UserID_vc", SqlDbType.VarChar)
                If txtSMSUserID.Text = String.Empty Then
                    SMS_UserID_vc.Value = DBNull.Value
                Else
                    SMS_UserID_vc.Value = txtSMSUserID.Text
                End If
                cmd.Parameters.Add(SMS_UserID_vc)

                Dim SMS_Password_vc As New SqlParameter("@SMS_Password_vc", SqlDbType.VarChar)
                If txtSMSPassword.Text = String.Empty Then
                    SMS_Password_vc.Value = DBNull.Value
                Else
                    SMS_Password_vc.Value = txtSMSPassword.Text
                End If
                cmd.Parameters.Add(SMS_Password_vc)

                Dim BillingType_vc As New SqlParameter("@BillingType_vc", SqlDbType.VarChar)
                BillingType_vc.Value = "DEFAULT"
                cmd.Parameters.Add(BillingType_vc)

                Dim Cmp_SaveImage_vc As New SqlParameter("@Cmp_SaveImage_vc", SqlDbType.VarChar)
                Cmp_SaveImage_vc.Value = "YES"
                cmd.Parameters.Add(Cmp_SaveImage_vc)

                Dim Cmp_ServerName_vc As New SqlParameter("@Cmp_ServerName_vc", SqlDbType.VarChar)
                If txtServerName.Text = String.Empty Then
                    Cmp_ServerName_vc.Value = DBNull.Value
                Else
                    Cmp_ServerName_vc.Value = txtServerName.Text
                End If
                cmd.Parameters.Add(Cmp_ServerName_vc)

                Dim Cmp_DatabaseName_vc As New SqlParameter("@Cmp_DatabaseName_vc", SqlDbType.VarChar)
                If txtDatabaseName.Text = String.Empty Then
                    Cmp_DatabaseName_vc.Value = DBNull.Value
                Else
                    Cmp_DatabaseName_vc.Value = txtDatabaseName.Text
                End If
                cmd.Parameters.Add(Cmp_DatabaseName_vc)

                Dim Cmp_BankDetails_vc As New SqlParameter("@Cmp_BankDetails_vc", SqlDbType.NVarChar)
                If txtBankDetails.Text = String.Empty Then
                    Cmp_BankDetails_vc.Value = DBNull.Value
                Else
                    Cmp_BankDetails_vc.Value = txtBankDetails.Text
                End If
                cmd.Parameters.Add(Cmp_BankDetails_vc)

                Dim Moving_Word_vc As New SqlParameter("@Moving_Word_vc", SqlDbType.NVarChar)
                If txtMovingWord.Text = String.Empty Then
                    Moving_Word_vc.Value = DBNull.Value
                Else
                    Moving_Word_vc.Value = txtMovingWord.Text
                End If
                cmd.Parameters.Add(Moving_Word_vc)

                Dim Cmp_ISO_im As New SqlParameter("@Cmp_ISO_im", SqlDbType.Image)
                If ImageData2 Is Nothing Then
                    Cmp_ISO_im.Value = DBNull.Value
                Else
                    Cmp_ISO_im.Value = ImageData2
                End If
                cmd.Parameters.Add(Cmp_ISO_im)

                Dim Cmp_ReportHead_vc As New SqlParameter("@Cmp_ReportHead_vc", SqlDbType.NVarChar)
                If txtReportHead.Text = String.Empty Then
                    Cmp_ReportHead_vc.Value = DBNull.Value
                Else
                    Cmp_ReportHead_vc.Value = txtReportHead.Text
                End If
                cmd.Parameters.Add(Cmp_ReportHead_vc)

                Dim Cmp_ToMail_vc As New SqlParameter("@Cmp_ToMail_vc", SqlDbType.VarChar)
                If txtToMail.Text = String.Empty Then
                    Cmp_ToMail_vc.Value = DBNull.Value
                Else
                    Cmp_ToMail_vc.Value = txtToMail.Text
                End If
                cmd.Parameters.Add(Cmp_ToMail_vc)

                Dim Cmp_BackImage_im As New SqlParameter("@Cmp_BackImage_im", SqlDbType.Image)
                If ImageData3 Is Nothing Then
                    Cmp_BackImage_im.Value = DBNull.Value
                Else
                    Cmp_BackImage_im.Value = ImageData3
                End If
                cmd.Parameters.Add(Cmp_BackImage_im)

                Dim Bill_ZeroStock_vc As New SqlParameter("@Bill_ZeroStock_vc", SqlDbType.VarChar)
                If CbBZeroS.Text = String.Empty Then
                    Bill_ZeroStock_vc.Value = "NO"
                Else
                    Bill_ZeroStock_vc.Value = CbBZeroS.Text
                End If
                cmd.Parameters.Add(Bill_ZeroStock_vc)

                Dim Bill_AftPrint_vc As New SqlParameter("@Bill_AftPrint_vc", SqlDbType.VarChar)
                If CbBAfterP.Text = String.Empty Then
                    Bill_AftPrint_vc.Value = "NO"
                Else
                    Bill_AftPrint_vc.Value = CbBAfterP.Text
                End If
                cmd.Parameters.Add(Bill_AftPrint_vc)

                Dim Cmp_ExpDays_i As New SqlParameter("@Cmp_ExpDays_i", SqlDbType.Int)
                If txtExpDays.Text = String.Empty Then
                    Cmp_ExpDays_i.Value = 0
                Else
                    Cmp_ExpDays_i.Value = txtExpDays.Text
                End If
                cmd.Parameters.Add(Cmp_ExpDays_i)

                Dim Cmp_GDrivePath_vc As New SqlParameter("@Cmp_GDrivePath_vc", SqlDbType.VarChar)
                If txtGDrivePath.Text = String.Empty Then
                    Cmp_GDrivePath_vc.Value = DBNull.Value
                Else
                    Cmp_GDrivePath_vc.Value = txtGDrivePath.Text
                End If
                cmd.Parameters.Add(Cmp_GDrivePath_vc)

                Dim Cmp_UpiID_vc As New SqlParameter("@Cmp_UpiID_vc", SqlDbType.VarChar)
                If txtUPIID.Text = String.Empty Then
                    Cmp_UpiID_vc.Value = DBNull.Value
                Else
                    Cmp_UpiID_vc.Value = txtUPIID.Text
                End If
                cmd.Parameters.Add(Cmp_UpiID_vc)

                Dim Cmp_UpiName_vc As New SqlParameter("@Cmp_UpiName_vc", SqlDbType.VarChar)
                If txtUpiName.Text = String.Empty Then
                    Cmp_UpiName_vc.Value = DBNull.Value
                Else
                    Cmp_UpiName_vc.Value = txtUpiName.Text
                End If
                cmd.Parameters.Add(Cmp_UpiName_vc)

                Dim User_ID_vc As New SqlParameter("@User_ID_vc", SqlDbType.VarChar)
                User_ID_vc.Value = VistawinFunctions.UserName
                cmd.Parameters.Add(User_ID_vc)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "UPDATE"
                cmd.Parameters.Add(InputString)
                cmd.CommandTimeout = 1000
                Bool = cmd.ExecuteNonQuery
            End Using
            If Bool = True Then
                Trans.Commit()
                MsgBox("Updated Successfully")
                Me.UpateCmpany()
                Me.LoginCmpDetail()
            ElseIf Bool = True Then
                MsgBox("Edit Failed")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        If VistawinFunctions.ScanningPath = String.Empty Then
            MsgBox("Create the Scanning Folder")
            Exit Sub
        End If

        StrFileName = Nothing
        PictureBox1.BackgroundImage = Nothing
        OpenFD.Reset()
        MS1 = New MemoryStream()
        OpenFD.InitialDirectory = VistawinFunctions.ScanningPath + "\"
        OpenFD.Title = "Open an Image File"
        'OpenFD.Filter = "jpg|*."
        OpenFD.ShowDialog()
        StrFileName = OpenFD.FileName

        If StrFileName = "" Then
            PictureBox1.Image = Nothing
        Else
            StrFileName = OpenFD.FileName
            PictureBox1.BackgroundImage = Image.FromFile(StrFileName)
            PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
            OpenFD.Reset()
            PictureBox1.BackgroundImage.Save(MS1, PictureBox1.BackgroundImage.RawFormat)
            ImageData1 = MS1.GetBuffer()
        End If
    End Sub

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click

        StrFileName = Nothing
        PictureBox1.BackgroundImage = Nothing
        OpenFD.Reset()
        ImageData1 = Nothing
    End Sub

    Private Sub btnBrowseBackup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseBackup.Click
        fbd = New FolderBrowserDialog
        fbd.Description = "Choose the location"
        fbd.ShowDialog()
        txtBackupPath.Text = fbd.SelectedPath
    End Sub

    Private Sub btnBrowseScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseScan.Click
        fbd1 = New FolderBrowserDialog
        fbd1.Description = "Choose the location"
        fbd1.ShowDialog()
        txtScanPath.Text = fbd1.SelectedPath
    End Sub
    Private Sub BtnISOOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnISOOpen.Click
        If VistawinFunctions.ScanningPath = String.Empty Then
            MsgBox("Create the Scanning Folder")
            Exit Sub
        End If

        StrFileName2 = Nothing
        PBISOImage.BackgroundImage = Nothing
        OpenFD2.Reset()
        MS2 = New MemoryStream()
        OpenFD2.InitialDirectory = VistawinFunctions.ScanningPath + "\"
        OpenFD2.Title = "Open an Image File"
        'OpenFD.Filter = "jpg|*."
        OpenFD2.ShowDialog()
        StrFileName2 = OpenFD2.FileName

        If StrFileName2 = "" Then
            PBISOImage.Image = Nothing
        Else
            StrFileName2 = OpenFD2.FileName
            PBISOImage.BackgroundImage = Image.FromFile(StrFileName2)
            PBISOImage.SizeMode = PictureBoxSizeMode.StretchImage
            OpenFD2.Reset()
            PBISOImage.BackgroundImage.Save(MS2, PBISOImage.BackgroundImage.RawFormat)
            ImageData2 = MS2.GetBuffer()
        End If
    End Sub

    Private Sub BtnISOClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnISOClose.Click
        StrFileName2 = Nothing
        PBISOImage.BackgroundImage = Nothing
        OpenFD2.Reset()
        ImageData2 = Nothing

    End Sub

    Private Sub CBPassword_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBPassword.CheckedChanged

        If CBPassword.Checked = True Then
            txtMastPassword.UseSystemPasswordChar = False
        ElseIf CBPassword.Checked = False Then
            'txtMastPassword.PasswordChar = "*"
            txtMastPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub BtnBIopen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBIopen.Click

        StrFileName3 = Nothing
        PictureBox3.BackgroundImage = Nothing
        OpenFD3.Reset()
        MS3 = New MemoryStream()
        OpenFD3.InitialDirectory = VistawinFunctions.ScanningPath + "\"
        OpenFD3.Title = "Open an Image File"
        'OpenFD3.Filter = "jpg|*."
        OpenFD3.ShowDialog()
        StrFileName3 = OpenFD3.FileName

        If StrFileName3 = "" Then
            PictureBox3.Image = Nothing
        Else
            StrFileName3 = OpenFD3.FileName
            PictureBox3.BackgroundImage = Image.FromFile(StrFileName3)
            PictureBox3.SizeMode = PictureBoxSizeMode.StretchImage
            OpenFD3.Reset()
            PictureBox3.BackgroundImage.Save(MS3, PictureBox3.BackgroundImage.RawFormat)
            ImageData3 = MS3.GetBuffer()
        End If
    End Sub

    Private Sub BtnBIclear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBIclear.Click

        StrFileName3 = Nothing
        PictureBox3.BackgroundImage = Nothing
        OpenFD3.Reset()
        ImageData3 = Nothing
    End Sub
    Private Sub BtnClose_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub LoginCmpDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_Companyprofile", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.TodayDate = Date.Now
        VistawinFunctions.CmpName = If(IsDBNull(dr("Cmp_Name_vc")), "", dr("Cmp_Name_vc").ToString())
        VistawinFunctions.CmpAddress = If(IsDBNull(dr("Cmp_Address_vc")), "", dr("Cmp_Address_vc").ToString())
        VistawinFunctions.CmpCaption = If(IsDBNull(dr("Cmp_Caption_vc")), "", dr("Cmp_Caption_vc").ToString())
        VistawinFunctions.CmpDistrict = If(IsDBNull(dr("Cmp_District_vc")), "", dr("Cmp_District_vc").ToString())
        VistawinFunctions.CmpState = If(IsDBNull(dr("Cmp_State_vc")), "", dr("Cmp_State_vc").ToString())
        VistawinFunctions.CmpPhCode = If(IsDBNull(dr("Cmp_Pincode_vc")), "", dr("Cmp_Pincode_vc").ToString())
        VistawinFunctions.CmpMobileNo = If(IsDBNull(dr("Cmp_MobileNo_n")), "", dr("Cmp_MobileNo_n").ToString())
        VistawinFunctions.CmpEmailID = If(IsDBNull(dr("Cmp_Email_vc")), "", dr("Cmp_Email_vc").ToString())
        VistawinFunctions.CmpWebSite = If(IsDBNull(dr("Cmp_Website_vc")), "", dr("Cmp_Website_vc").ToString())
        VistawinFunctions.CmpPhoneNo = If(IsDBNull(dr("Cmp_PhoneNo_vc")), "", dr("Cmp_PhoneNo_vc").ToString())
        VistawinFunctions.CmpProprietor = If(IsDBNull(dr("Cmp_Proprietor_vc")), "", dr("Cmp_Proprietor_vc").ToString())
        VistawinFunctions.CmpLicenseNo = If(IsDBNull(dr("Cmp_LicenseNo_vc")), "", dr("Cmp_LicenseNo_vc").ToString())
        VistawinFunctions.MastPassword = If(IsDBNull(dr("Mast_Password_vc")), "", dr("Mast_Password_vc").ToString())
        VistawinFunctions.BackupPath = If(IsDBNull(dr("BackupPath_vc")), "", dr("BackupPath_vc").ToString())
        VistawinFunctions.ScanningPath = If(IsDBNull(dr("ScanningPath_vc")), "", dr("ScanningPath_vc").ToString())
        VistawinFunctions.SendSMS = If(IsDBNull(dr("SendSMS_vc")), "", dr("SendSMS_vc").ToString())
        VistawinFunctions.SMSSenderID = If(IsDBNull(dr("SMS_SenderID_vc")), "", dr("SMS_SenderID_vc").ToString())
        VistawinFunctions.SMSUserID = If(IsDBNull(dr("SMS_UserID_vc")), "", dr("SMS_UserID_vc").ToString())
        VistawinFunctions.SMSPassword = If(IsDBNull(dr("SMS_Password_vc")), "", dr("SMS_Password_vc").ToString())
        VistawinFunctions.CmpServerName = If(IsDBNull(dr("Cmp_ServerName_vc")), "", dr("Cmp_ServerName_vc").ToString())
        VistawinFunctions.CmpDatabaseName = If(IsDBNull(dr("Cmp_DataBaseName_vc")), "", dr("Cmp_DataBaseName_vc").ToString())
        VistawinFunctions.CmpBankDetails = If(IsDBNull(dr("Cmp_BankDetails_vc")), "", dr("Cmp_BankDetails_vc").ToString())
        VistawinFunctions.CmpMovingWord = If(IsDBNull(dr("Moving_Word_vc")), "", dr("Moving_Word_vc").ToString())
        VistawinFunctions.CmpReportHead = If(IsDBNull(dr("Cmp_ReportHead_vc")), "", dr("Cmp_ReportHead_vc").ToString())
        VistawinFunctions.CmpToEmail = If(IsDBNull(dr("Cmp_ToMail_vc")), "", dr("Cmp_ToMail_vc").ToString())
        VistawinFunctions.PrintingType = If(IsDBNull(dr("BillingType_vc")), "DEFAULT", dr("BillingType_vc").ToString())
        VistawinFunctions.SaveImage = If(IsDBNull(dr("Cmp_SaveImage_vc")), "NO", dr("Cmp_SaveImage_vc").ToString())
     

        If VistawinFunctions.CmpDatabaseName <> "BALAJICOMPLEXDBNEW" Then

            VistawinFunctions.CmpBZS = If(IsDBNull(dr("Bill_ZeroStock_vc")), "NO", dr("Bill_ZeroStock_vc").ToString())
            VistawinFunctions.CmpBAfP = If(IsDBNull(dr("Bill_AftPrint_vc")), "NO", dr("Bill_AftPrint_vc").ToString())
            VistawinFunctions.GStCal = If(IsDBNull(dr("Cmp_GSTCal_vc")), "INCLUDING", dr("Cmp_GSTCal_vc").ToString())
            VistawinFunctions.CmpExpDays = If(IsDBNull(dr("Cmp_ExpDays_i")), 0, Convert.ToInt32(dr("Cmp_ExpDays_i")))
            VistawinFunctions.GDrivePath = If(IsDBNull(dr("Cmp_GDrivePath_vc")), "", dr("Cmp_GDrivePath_vc").ToString())
            VistawinFunctions.UpiName = If(IsDBNull(dr("Cmp_UpiName_vc")), "", dr("Cmp_UpiName_vc").ToString())
            VistawinFunctions.UpiID = If(IsDBNull(dr("Cmp_UpiID_vc")), "", dr("Cmp_UpiID_vc").ToString())
            VistawinFunctions.CmpPuGST = If(IsDBNull(dr("Cmp_PurGST_vc")), "", dr("Cmp_PurGST_vc").ToString())
            VistawinFunctions.CmpPuRound = If(IsDBNull(dr("Cmp_PurRound_vc")), "", dr("Cmp_PurRound_vc").ToString())
            VistawinFunctions.CmpSaRound = If(IsDBNull(dr("Cmp_SaleRound_vc")), "", dr("Cmp_SaleRound_vc").ToString())
            VistawinFunctions.CmpPAutoRound = If(IsDBNull(dr("Cmp_PAutoRound_vc")), "", dr("Cmp_PAutoRound_vc").ToString())
            VistawinFunctions.CmpSAutoRound = If(IsDBNull(dr("Cmp_SAutoRound_vc")), "", dr("Cmp_SAutoRound_vc").ToString())

            VistawinFunctions.CmpBarcode = If(IsDBNull(dr("Cmp_Barcode_vc")), "", dr("Cmp_Barcode_vc").ToString())
            VistawinFunctions.CmpHead1 = If(IsDBNull(dr("Cmp_Head1_vc")), "", dr("Cmp_Head1_vc").ToString())
            VistawinFunctions.CmpHead2 = If(IsDBNull(dr("Cmp_Head2_vc")), "", dr("Cmp_Head2_vc").ToString())
            VistawinFunctions.CmpHead3 = If(IsDBNull(dr("Cmp_Head3_vc")), "", dr("Cmp_Head3_vc").ToString())
            VistawinFunctions.CmpHead4 = If(IsDBNull(dr("Cmp_Head4_vc")), "", dr("Cmp_Head4_vc").ToString())
            VistawinFunctions.CmpHead5 = If(IsDBNull(dr("Cmp_Head5_vc")), "", dr("Cmp_Head5_vc").ToString())
        Else

        End If

        If Not IsDBNull(dr("Cmp_Logo_im")) Then
            VistawinFunctions.CmpLogo = DirectCast(dr("Cmp_Logo_im"), Byte())
        End If

        If Not IsDBNull(dr("Cmp_ISO_im")) Then
            VistawinFunctions.CmpISO = DirectCast(dr("Cmp_ISO_im"), Byte())
        End If

        If Not IsDBNull(dr("Cmp_BackImage_im")) Then
            Try
                Dim imgBytes() As Byte = DirectCast(dr("Cmp_BackImage_im"), Byte())
                If Me.BackgroundImage IsNot Nothing Then
                    Me.BackgroundImage.Dispose()
                    Me.BackgroundImage = Nothing
                End If

                Using ms As New MemoryStream(imgBytes)
                    Using img As Image = Image.FromStream(ms)
                        Me.BackgroundImage = New Bitmap(img)
                    End Using
                End Using
                VistawinFunctions.CmpBackImage = imgBytes
            Catch ex As Exception
                'MessageBox.Show("Invalid background image." & vbCrLf & ex.Message)
                Me.BackgroundImage = Nothing
            End Try
        Else
            Me.BackgroundImage = Nothing
        End If
    End Sub
End Class