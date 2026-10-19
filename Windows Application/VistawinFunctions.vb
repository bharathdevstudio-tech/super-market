Imports System
Imports System.IO
Imports System.Data
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Threading
Imports System.Diagnostics
Imports System.Configuration
Imports System.Windows.Forms
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Drawing.Printing
Imports System.IO.Ports
Imports System.Management
Imports System.Collections.Generic
Imports System.Runtime.InteropServices


Public Interface Int_ChitFund

    Function St1() As String
    Function YearSeries(ByVal Vocdate As Date) As Decimal
    Function Age(ByVal DateOfBirth As Date) As Integer
    Function pwdDecript(ByVal sData As String) As String
    Function pwdEncript(ByVal sData As String) As String
    Function accpwdEncript(ByVal sData As String) As String
    Function Dateformat(ByVal getdate As String) As Boolean
    Function Datestatus(ByVal statusdate As Date) As Boolean
    Function checkdateformat(ByVal txtstring As String) As String
    Function FindLength(ByVal Str As String) As Integer
    Function LoadDataSet(ByVal SqlString As String, ByVal DatasetName As String) As DataSet
    Function LoadListBox(ByVal Ds As DataSet, ByVal List As ListBox, ByVal bool As Boolean)
    Function FindString(ByVal list As ListBox, ByVal txtbranchname As String, ByVal Msgstring As String) As Boolean
    Function dbfullbackup1(ByVal connec As SqlConnection) As Boolean
    Function message(ByVal msgstr As String) As Boolean
    Function YearSeriesChecking(ByVal FromDate As Date, ByVal ToDate As Date) As Boolean
    Function KeyPressOnlyNumber(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
    Function SendingSms(ByVal SMSType As String, ByVal MessageType As String, ByVal MobileNumber As String, ByVal RoomNumber As String, ByVal CustName As String, ByVal CustPlace As String, ByVal ReceiptNo As String, ByVal ReceiptType As String, ByVal SMSDate As Date, ByVal Amount As Decimal) As Boolean
    Function IDProofResize(ByVal IDImage As Image) As Image
    Function IDPhotoResize(ByVal IDImage As Image) As Image
    Function IDSignResize(ByVal IDImage As Image) As Image
    Sub PrintTextFile()
  
    Function MalayamDays(ByVal FunctionDate As Date) As String
    Function MalayamMonths(ByVal FunctionDate As Date) As String
    Function MalayamYears(ByVal FunctionDate As Date) As String

    Function StockUpdateQ() As Boolean
    Function StockUpdateIn() As Boolean

    Function ComplexRoomUpdation() As Boolean

End Interface
Public Class VistawinFunctions

    Public ds As DataSet
    Public cmd As SqlCommand
    Public sda As SqlDataAdapter
    Public con As SqlConnection
    Public drow As DataRow
    Public Shared DataSource, DataBase, SqlUserID, SqlUserPassword, SystemName, UserName, Pw, UserID, Password, UserType, BackupPath, ScanningPath, SendSMS, SMSSenderID, SMSUserID, SMSPassword, PrintingType, StayDay, SaveImage, ChkDateChange, CmpServerName, CmpDatabaseName, CmpBankDetails, CmpMovingWord As String
    'Public Shared Connection As New SqlConnection, connection1 As New SqlConnection
    Public Shared TodayDate, ServerDate, CmpRegDate, CmpEstDate As New Date
    Public Shared ServerConnection As SqlConnection
    Public Shared CmpName, CmpProprietor, CmpLicenseNo, CmpTINNo, CmpDLNo, CmpAddress, CmpState, CmpDistrict, CmpPhCode, CmpPhoneNo, CmpMobileNo, CmpEmailID, CmpPassword, CmpWebSite, CmpCashBook, eng, roomno, MastPassword, CmpReportHead, CmpCaption, CmpToEmail, GStCal As String
    Public Shared CmpBZS, CmpBAfP, CmpPuGST, CmpPuRound, CmpSaRound, CmpPAutoRound, CmpSAutoRound, CmpBarcode As String
    Public Shared VWVerDate, CmpPharmacyName, CmpNote, CliGodownShow, SalRatCal As String
    Public Shared CmpAucType, CmpCashBookCode, CmpExpDays, CurrTokID As Integer
    Public Shared CmpLogo, CmpISO, CmpBackImage As Byte()
    Public Shared AdavanceBookRoom, CustomerID, AdvanceBookID, CmpPW1 As String
    Public Shared CmpCommission, CmpYearSeries, CashBalance, CmpServiceTax As Decimal
    Public Shared Directory1 As Directory
    Public Shared PrintFileName, MessageValue, PrinterName, PrinterSelect As String
    Public Shared FileLocation As String = "C:\Program Files\Microsoft\CHITAPPS\"
    Public Shared backupmessage, CmpAutoRec As Boolean
    Public Shared pathField As String
    'Public Shared connection1 As New SqlConnection, connection2 As New SqlConnection
    Public Shared curraccyear As String
    Public Shared PrintRecAmount As Double
    Public Shared SHeight As Integer = Screen.PrimaryScreen.Bounds.Height
    Public Shared SWidth As Integer = Screen.PrimaryScreen.Bounds.Width
    Public Shared CusInvChk As String
    Public Shared FrmChk As String
    Public Shared AppTypeChk1 As String
    Public Shared StProAny, PurProAny, SalProAny, SaleBill, FeesOpt As String

    Public Shared CmpHead1, CmpHead2, CmpHead3, CmpHead4, CmpHead5 As String
    Public Shared CusIDTest As String
    Public Shared MainFormChk As String

    Public Shared FunctionDate As New Date
    Public Shared GDrivePath, UpiID, UpiName As String
    Public Shared GracePeriod, CmpProprietor2 As String
    Public Shared AdvZeroAdd, HKFormshow, HKEmp, Supervisor, RoomBoy, GSTCol, BankSel, GraceType, GraceTime, CbVocImg, ChkAutoAmt, AdvBillForm, ChkoutBillForm, DBillForm As String
    Public Shared RofAmt As Decimal
    Public Shared AdvBillCopy As Integer
    Public Shared PSChildID As String

    Public Shared ProjectTypechk As String
    Public Shared SqlServerNameShow As String
    Public Shared ServerApiChk As String

    Public Shared CmpBankTwo As String
End Class
Class BVistawinSolution

    Implements Int_ChitFund


    Dim Trans As SqlTransaction
    Dim Ds As New DataSet
    Dim Adapter As New SqlDataAdapter
    Dim Execute As Boolean
    Dim Cmd As New SqlCommand

    Public VariableDec As New VistawinFunctions
    Dim ds2 As DataSet
    Dim BVistawinSolution1 As BVistawinSolution
    Public Function message(ByVal msgstr As String) As Boolean Implements Int_ChitFund.message
        Try
            Dim msg As String
            Dim title As String
            Dim response As MsgBoxResult
            Dim style As MsgBoxStyle
            msg = msgstr
            title = "Msgbox Demonstration"
            style = MsgBoxStyle.YesNo
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function YearSeries(ByVal VocDate As Date) As Decimal Implements Int_ChitFund.YearSeries
        Try

            Dim YearSeries1 As Decimal

            If VocDate.Month <= 3 Then
                YearSeries1 = CDec((VocDate.Year - 1).ToString + VocDate.Year.ToString)
            ElseIf VocDate.Month > 3 Then
                YearSeries1 = CDec(VocDate.Year.ToString + (VocDate.Year + 1).ToString)
            End If
            Return YearSeries1
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function

    Public Function St1() As String Implements Int_ChitFund.St1
        Try
            VistawinFunctions.UserName = "sa"
            VistawinFunctions.Pw = "bala"
            Return "data source=Support;DATABASE=DATA;USER ID=sa; password=bala"
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try

    End Function
    Public Function dbfullbackup1(ByVal connec As System.Data.SqlClient.SqlConnection) As Boolean Implements Int_ChitFund.dbfullbackup1
        Dim dbname As String = connec.Database

        If VistawinFunctions.BackupPath = String.Empty Then
            MsgBox("Create the Backup Folder")
            Return False
            Exit Function
        End If

        Dim dtt As Object = Format(Date.Today, "ddMMyyyy")
        Dim pathname As String = VistawinFunctions.BackupPath + "\" + dbname + dtt

        Dim ff As New FileIO.FileSystem
        Dim mm, mm1 As Boolean

        mm = FileIO.FileSystem.FileExists(VistawinFunctions.BackupPath + "\" + dbname + dtt)
        If mm = True Then
            If VistawinFunctions.backupmessage = True Then
                'mm1 = Me.message("File Already Exists ,Do U Want To Continue")
                If mm1 = True Then

                Else
                    'Return False
                    'Exit Function
                End If
            End If
            FileIO.FileSystem.DeleteFile(VistawinFunctions.BackupPath + "\" + dbname + dtt)
        End If

        Try

            Using cmd As New SqlCommand("[dbo].[dbbackup]", connec)
                cmd.CommandType = CommandType.StoredProcedure

                Dim dbnm As New SqlParameter("@dbnm", SqlDbType.VarChar)
                dbnm.Value = dbname
                cmd.Parameters.Add(dbnm)

                Dim dbnm1 As New SqlParameter("@pathname", SqlDbType.VarChar)
                dbnm1.Value = pathname
                cmd.Parameters.Add(dbnm1)

                Dim dbse As New SqlParameter("@dbsname", SqlDbType.VarChar)
                dbse.Value = dbname + dtt + "-Full Backup"
                cmd.Parameters.Add(dbse)
                cmd.CommandTimeout = 900
                cmd.ExecuteNonQuery()

            End Using
            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function
    Public Function LoadListBox(ByVal ds As System.Data.DataSet, ByVal list As System.Windows.Forms.ListBox, ByVal bool As Boolean) As Object Implements Int_ChitFund.LoadListBox
        list.Items.Clear()
        If bool = True Then
            For Each VariableDec.drow In ds.Tables(0).Rows
                list.Items.Add(VariableDec.drow.Item(0) & Space(10) & VariableDec.drow.Item(1))
            Next
        ElseIf bool = False Then
            For Each VariableDec.drow In ds.Tables(0).Rows
                list.Items.Add(VariableDec.drow.Item(0))
            Next
        End If
        Return True
    End Function
    Public Function FindLength(ByVal str As String) As Integer Implements Int_ChitFund.FindLength
        Dim strlength As Integer = str.Length
        Dim i As Integer
        Dim count As Integer
        Dim char1(1) As Array
        char1(0) = str.Split(" ")
        count = char1(0)(0).ToString.Length
        For i = 0 To strlength - 1
            If str.Chars(i) = " " Then
                If str.Chars(i + 1) = " " Then
                    count += 1
                End If
            End If
        Next
        Return count + 1
    End Function

    Public Function FindString(ByVal list As System.Windows.Forms.ListBox, ByVal StringName As String, ByVal Msgstring As String) As Boolean Implements Int_ChitFund.FindString
        list.SelectedIndex = list.FindString(StringName)
        If StringName.Length = 0 Or StringName = "" Or String.IsNullOrEmpty(StringName) Then
            list.Hide()
            Return False
        Else
            If list.SelectedIndex = -1 Then
                MsgBox(Msgstring)
                list.Hide()
                Return False
            Else
                list.Show()
                Return True
            End If
        End If
    End Function
    Public Function loaddataset(ByVal Sqlstring As String, ByVal DatasetName As String) As System.Data.DataSet Implements Int_ChitFund.LoadDataSet
        VariableDec.ds = New DataSet
        VariableDec.sda = New SqlDataAdapter(Sqlstring, VistawinFunctions.ServerConnection)
        VariableDec.sda.Fill(VariableDec.ds, DatasetName)
        Return VariableDec.ds
    End Function

    Public Function Age1(ByVal DateOfBirth As Date) As Integer Implements Int_ChitFund.Age
        Try
            Dim Cmd As New SqlCommand

            Cmd = New SqlCommand("SElECT DATEDIFF(YY,'" & Format(CDate(DateOfBirth), "yyyy/MM/dd") & "',GETdATE())", VistawinFunctions.ServerConnection)
            Age1 = Cmd.ExecuteScalar()
            Return Age1
        Catch ex As Exception
            Throw (New Exception("Error in date of birth" & ex.Message))
        End Try
    End Function
    Public Function pwdDecript(ByVal sData As String) As String Implements Int_ChitFund.pwdDecript
        Try
            Dim encoder As New System.Text.UTF8Encoding()
            Dim utf8Decode As System.Text.Decoder = encoder.GetDecoder()
            Dim todecode_byte As Byte() = Convert.FromBase64String(sData)
            Dim charCount As Integer = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length)
            Dim decoded_char As Char() = New Char(charCount - 1) {}
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0)
            Dim result As String = New [String](decoded_char)
            Return result
        Catch ex As Exception
            Throw (New Exception("Error in Decodeing" & ex.Message))
        End Try
    End Function
    Public Function accpwdEncript(ByVal sData As String) As String Implements Int_ChitFund.accpwdEncript
        Try
            'Me.pwdDecript(sData)
            Dim encData_byte As Byte() = New Byte(sData.Length - 1) {}
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData)
            Dim encodedData As String = Convert.ToBase64String(encData_byte)
            Return (encodedData)
        Catch ex As Exception
            Throw (New Exception("Error in Encodeing" & ex.Message))
        End Try
    End Function
    Public Function pwdEncript(ByVal sData As String) As String Implements Int_ChitFund.pwdEncript
        Try
            'Me.pwdDecript(sData)
            Dim encData_byte As Byte() = New Byte(sData.Length - 1) {}
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData)
            Dim encodedData As String = Convert.ToBase64String(encData_byte)
            Return (encodedData)
        Catch ex As Exception
            Throw (New Exception("Error in Encodeing" & ex.Message))
        End Try
    End Function
    Public Function Dateformat(ByVal getdate As String) As Boolean Implements Int_ChitFund.Dateformat
        Try
            If getdate = "" Or getdate = Nothing Or String.IsNullOrEmpty(getdate) Then
                MsgBox("Date is Empty")
                Return False
            ElseIf IsDate(getdate) = False Then
                MsgBox("Date Format is Incorrect")
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function Datestatus(ByVal statusdate As Date) As Boolean Implements Int_ChitFund.Datestatus
        Try
            'If statusdate > Date.Now Then
            '    MsgBox("date should not exceed current date")
            '    Return False
            'Else
            Return True
            'End If
        Catch ex As Exception
            Return False
            MsgBox(ex.Message)
        End Try
    End Function
    Public Function checkdateformat(ByVal string1 As String) As String Implements Int_ChitFund.checkdateformat
        Dim str(2) As String
        Try
            Dim string2 As String = Nothing
            Dim string3 As String = Nothing
            Dim string4 As String = Nothing
            Dim string5 As String = Nothing
            Dim string6 As String = Nothing
            Dim i, j As Integer
            Dim k As Integer = CInt(string1.Length)
            i = 0
            j = 0
            While (i <> k)
                If string1.Chars(i) = " " Then
                    str(j) = string2
                    string2 = Nothing
                    j = j + 1
                Else
                    string2 = string2 & string1.Chars(i)
                End If
                i = i + 1
            End While
            string3 = str(0)
            string4 = str(1)
            string5 = str(2)
            If string5 = Nothing Then
                string6 = Trim(string4) & " " & Trim(string3)
                string6 = Trim(string6)
                Return (string6)
            Else
                Return (string1)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            str(0) = Nothing
            str(1) = Nothing
            str(2) = Nothing
        End Try
    End Function
    Public Function KeyPressOnlyNumber(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean Implements Int_ChitFund.KeyPressOnlyNumber
        Try
            Dim Ascvalue As Integer
            Ascvalue = Asc(e.KeyChar)
            If Ascvalue >= 58 And Ascvalue <= 122 Then '[a-z ]should not allowed
                e.Handled = True                        ' it allowed numbers only
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            'MsgBox("Numbers Only")
            Return False
        End Try
    End Function
    
    Public Function YearSeriesChecking(ByVal FromDate As Date, ByVal ToDate As Date) As Boolean Implements Int_ChitFund.YearSeriesChecking

        Try

            If FromDate > ToDate Then
                Return False
            Else
                If FromDate.Month <= 3 And FromDate.Year = ToDate.Year Then
                    If ToDate.Month >= 4 Then
                        Return False
                    Else
                        Return True
                    End If
                ElseIf FromDate.Month >= 4 And FromDate.Year = ToDate.Year Then
                    If ToDate.Month <= 3 Then
                        Return False
                        'MsgBox("Enter Correct Financial year")
                        'RemoveHandler txtToDate.Leave, AddressOf txtToDate_Leave
                        'txtToDate.Text = String.Empty
                        'AddHandler txtToDate.Leave, AddressOf txtToDate_Leave
                        'txtToDate.Focus()
                    Else
                        Return True
                    End If
                ElseIf FromDate.Month >= 4 And FromDate.Year <> ToDate.Year Then
                    If ToDate.Month >= 4 And FromDate.Year < ToDate.Year Then
                        Return False
                        'MsgBox("Enter Correct Financial year")
                        'RemoveHandler txtToDate.Leave, AddressOf txtToDate_Leave
                        'txtToDate.Text = String.Empty
                        'AddHandler txtToDate.Leave, AddressOf txtToDate_Leave
                        'txtToDate.Focus()
                    Else
                        Return True
                    End If
                End If
            End If
            Return True
        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        Finally
            FromDate = Nothing
            ToDate = Nothing
        End Try
    End Function

    

    Public Function IDProofResize(ByVal Image As Image) As Image Implements Int_ChitFund.IDProofResize
        Try
            Dim newWidth As Integer = 10
            Dim newHeight As Integer = 10
            Dim ResizedBMP As New Bitmap(newWidth, newHeight)
            Dim g As Graphics = Graphics.FromImage(Image)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(Image, 0, 0, newWidth, newHeight)
            Return Image
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function IDPhotoResize(ByVal Image As Image) As Image Implements Int_ChitFund.IDPhotoResize
        Try
            Dim newWidth As Integer = 100
            Dim newHeight As Integer = 100
            Dim ResizedBMP As New Bitmap(newWidth, newHeight)
            Dim g As Graphics = Graphics.FromImage(Image)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(Image, 0, 0, newWidth, newHeight)
            'g.Save()
            g.Dispose()
            Return Image
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function IDSignResize(ByVal Image As Image) As Image Implements Int_ChitFund.IDSignResize
        Try

            Dim newWidth As Integer = 10
            Dim newHeight As Integer = 10
            Dim ResizedBMP As New Bitmap(newWidth, newHeight)
            Dim g As Graphics = Graphics.FromImage(Image)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(Image, 0, 0, newWidth, newHeight)
            Return Image
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Nothing
        End Try
    End Function
    Public Function SendingSms(ByVal SMSType As String, ByVal MessageType As String, ByVal MobileNumber As String, ByVal RoomNumber As String, ByVal CustName As String, ByVal CustPlace As String, ByVal ReceiptNo As String, ByVal ReceiptType As String, ByVal SMSDate As Date, ByVal Amount As Decimal) As Boolean Implements Int_ChitFund.SendingSms
        Dim SerialPort1 As New System.IO.Ports.SerialPort

        Try

            'Dim modems, baud, Port As String
            Dim ModemPort As String = Nothing
            Dim Message As New TextBox

            If SMSType = "CheckIn" Then

                If MessageType = "Tamil" Then
                    Message.Font = New System.Drawing.Font("Bamini", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "Tpdad; Tpdad Tpdad;" + RoomNumber + ".Booking Date:" + SMSDate + ".ReceiptNo:" + ReceiptNo + ".Advance Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName + "," + CustPlace + ".By HAL."
                ElseIf MessageType = "English" Then
                    Message.Font = New System.Drawing.Font("Times New Roman", 10.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "RoomNo: " + RoomNumber + ".Booking Date:" + SMSDate + ".ReceiptNo:" + ReceiptNo + ".Advance Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName + "," + CustPlace + ".By HAL."
                End If
            ElseIf SMSType = "CheckOut" Then
                If MessageType = "Tamil" Then
                    Message.Font = New System.Drawing.Font("Bamini", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "RoomNo: " + RoomNumber.ToString + ".ReceiptNo: " + ReceiptNo.ToString + ".Receipt Date:" + Format(CDate(SMSDate), "dd/MM/yyyy").ToString + "." + ReceiptType.ToString + "Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName.ToString + "," + CustPlace.ToString + ".By HAL."
                ElseIf MessageType = "English" Then
                    Message.Font = New System.Drawing.Font("Times New Roman", 10.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "RoomNo: " + RoomNumber.ToString + ".ReceiptNo: " + ReceiptNo.ToString + ".Receipt Date:" + Format(CDate(SMSDate), "dd/MM/yyyy").ToString + "." + ReceiptType.ToString + "Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName.ToString + "," + CustPlace.ToString + ".By HAL."
                End If
            ElseIf SMSType = "CashReceipt" Then
                If MessageType = "Tamil" Then
                    Message.Font = New System.Drawing.Font("Bamini", 12.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "RoomNo: " + RoomNumber.ToString + ".ReceiptNo: " + ReceiptNo.ToString + ".Receipt Date:" + Format(CDate(SMSDate), "dd/MM/yyyy").ToString + "." + ReceiptType.ToString + "Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName.ToString + "," + CustPlace.ToString + ".By HAL."
                ElseIf MessageType = "English" Then
                    Message.Font = New System.Drawing.Font("Times New Roman", 10.0!, FontStyle.Regular, GraphicsUnit.Point, CType(0, Byte))
                    Message.Text = "RoomNo: " + RoomNumber.ToString + ".ReceiptNo: " + ReceiptNo.ToString + ".Receipt Date:" + Format(CDate(SMSDate), "dd/MM/yyyy").ToString + "." + ReceiptType.ToString + "Amount Rs:" + FormatNumber(Amount).ToString + " Received from " + CustName.ToString + "," + CustPlace.ToString + ".By HAL."
                End If
            End If

            VistawinFunctions.MessageValue = Message.Text

            'Dim searcher As New ManagementObjectSearcher( _
            '            "root\CIMV2", _
            '            "SELECT * FROM Win32_POTSModem")
            'For Each queryObj As ManagementObject In searcher.Get()
            '    If queryObj("Status") = "OK" Then
            '        'modems = queryObj("Description")
            '        'baud = queryObj("MaxBaudRateToSerialPort")
            '        'Port = queryObj("AttachedTo")
            '        ModemPort = queryObj("AttachedTo")
            '    End If

            'Next

            If ModemPort = Nothing Then
                Return False
                Exit Function
            End If
            With SerialPort1
                Try
                    .PortName = ModemPort
                    .BaudRate = 9600
                    .Parity = Parity.None
                    .StopBits = StopBits.One
                    .DataBits = 8
                    .Handshake = Handshake.RequestToSend
                    .DtrEnable = True
                    .RtsEnable = True
                    .NewLine = vbCrLf
                    .Open()
                    .Write("AT" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    .Write("AT+CMGF=1" & vbCrLf)
                    System.Threading.Thread.Sleep(1000)
                    .Write("AT+CMGS=" & Chr(34) & MobileNumber & Chr(34) & vbCrLf)
                    .Write(Message.Text & Chr(26))
                    System.Threading.Thread.Sleep(1000)
                    .Close()
                    Return True
                Catch ex As Exception
                    Return False
                    .Close()
                End Try
            End With

        Catch ex As Exception
            MsgBox(ex.Message)
            Return False
        End Try
    End Function

    'Dim inputTitle As String = "Enter the Master Password!"
    'Dim returnedPW As String

    'Public Function Permission() As Boolean
    '    returnedPW = InputBox("Enter the Master Password!", inputTitle, "", 0)
    '    If returnedPW = VistawinFunctions.MastPassword Then
    '        Return True
    '    ElseIf returnedPW = "" Then
    '        Return False
    '    Else
    '        MessageBox.Show("Wrong Password!", " Try Again")
    '        Return False
    '    End If
    'End Function

    Dim inputTitle As String = "Enter the Master Password!"
    Dim returnedPW As String

    Public Function Permission() As Boolean
        returnedPW = PasswordInputBox("Enter the Master Password!", inputTitle)
        If returnedPW = VistawinFunctions.MastPassword Then
            Return True
        ElseIf returnedPW = "" Then
            Return False
        Else
            MessageBox.Show("Wrong Password!", "Try Again", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
    End Function

    Private Function PasswordInputBox(ByVal Prompt As String, ByVal Title As String) As String

        Dim Frm As New Form()
        Dim Lbl As New Label()
        Dim TxtPassword As New TextBox()
        Dim BtnOK As New Button()
        Dim BtnCancel As New Button()

        Try
            Frm.Text = Title
            Frm.Width = 360
            Frm.Height = 160

            Frm.FormBorderStyle = FormBorderStyle.FixedDialog
            Frm.StartPosition = FormStartPosition.CenterScreen

            Frm.MaximizeBox = False
            Frm.MinimizeBox = False
            Frm.ShowInTaskbar = False

            '-----------------------------------------
            'LABEL
            '-----------------------------------------
            Lbl.Left = 10
            Lbl.Top = 15
            Lbl.Width = 320
            Lbl.Height = 20

            Lbl.Text = Prompt

            '-----------------------------------------
            'PASSWORD TEXTBOX
            '-----------------------------------------
            TxtPassword.Left = 10
            TxtPassword.Top = 42
            TxtPassword.Width = 320
            TxtPassword.Height = 22

            'Password will show ***
            TxtPassword.PasswordChar = "*"c

            '-----------------------------------------
            'OK BUTTON
            '-----------------------------------------
            BtnOK.Text = "OK"

            BtnOK.Left = 174
            BtnOK.Top = 75
            BtnOK.Width = 75
            BtnOK.Height = 25

            BtnOK.DialogResult = DialogResult.OK

            '-----------------------------------------
            'CANCEL BUTTON
            '-----------------------------------------
            BtnCancel.Text = "Cancel"

            BtnCancel.Left = 255
            BtnCancel.Top = 75
            BtnCancel.Width = 75
            BtnCancel.Height = 25

            BtnCancel.DialogResult = DialogResult.Cancel

            '-----------------------------------------
            'ADD CONTROLS
            '-----------------------------------------
            Frm.Controls.Add(Lbl)
            Frm.Controls.Add(TxtPassword)
            Frm.Controls.Add(BtnOK)
            Frm.Controls.Add(BtnCancel)

            'Enter Key = OK
            Frm.AcceptButton = BtnOK

            'ESC Key = Cancel
            Frm.CancelButton = BtnCancel

            Frm.ActiveControl = TxtPassword

            '-----------------------------------------
            'SHOW PASSWORD WINDOW
            '-----------------------------------------
            If Frm.ShowDialog() = DialogResult.OK Then
                Return TxtPassword.Text
            Else
                Return ""
            End If

        Finally
            Frm.Dispose()
        End Try

    End Function
    Public Sub PrintFile() Implements Int_ChitFund.PrintTextFile
        RawPrinterHelper.SendFileToPrinter(VistawinFunctions.PrinterName, VistawinFunctions.PrintFileName)
        RawPrinterHelper.ClosePrinter(RawPrinterHelper.printer)
        RawPrinterHelper.EndDocPrinter(RawPrinterHelper.printer)

    End Sub
    Public Class RawPrinterHelper
        Public Shared printer As IntPtr
        ' Structure and API declarions:
        <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)> _
        Structure DOCINFOW
            <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
            <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
            <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
        End Structure


        <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="ClosePrinter", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Int32, ByRef pDI As DOCINFOW) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
        End Function
        <DllImport("winspool.Drv", EntryPoint:="WritePrinter", _
           SetLastError:=True, CharSet:=CharSet.Unicode, _
           ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)> _
        Public Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Int32, ByRef dwWritten As Int32) As Boolean
        End Function

        ' SendBytesToPrinter()
        ' When the function is given a printer name and an unmanaged array of  
        ' bytes, the function sends those bytes to the print queue.
        ' Returns True on success or False on failure.
        Public Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Int32) As Boolean
            Dim hPrinter As IntPtr      ' The printer handle.
            Dim dwError As Int32        ' Last error - in case there was trouble.
            Dim di As DOCINFOW          ' Describes your document (name, port, data type).
            Dim dwWritten As Int32      ' The number of bytes written by WritePrinter().
            Dim bSuccess As Boolean     ' Your success code.
            di = Nothing
            ' Set up the DOCINFO structure.
            With di
                .pDocName = "Vistawin Solution Document"
                .pDataType = "RAW"
            End With
            ' Assume failure unless you specifically succeed.
            bSuccess = False
            If OpenPrinter(szPrinterName, hPrinter, 0) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        ' Write your printer-specific bytes to the printer.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If
            ' If you did not succeed, GetLastError may give more information
            ' about why not.
            If bSuccess = False Then
                dwError = Marshal.GetLastWin32Error()
            End If
            printer = hPrinter
            Return bSuccess
        End Function ' SendBytesToPrinter()

        ' SendFileToPrinter()
        ' When the function is given a file name and a printer name, 
        ' the function reads the contents of the file and sends the
        ' contents to the printer.
        ' Presumes that the file contains printer-ready data.
        ' Shows how to use the SendBytesToPrinter function.
        ' Returns True on success or False on failure.
        Public Shared Function SendFileToPrinter(ByVal szPrinterName As String, ByVal szFileName As String) As Boolean
            ' Open the file.
            Dim fs As New FileStream(szFileName, FileMode.Open)
            ' Create a BinaryReader on the file.
            Dim br As New BinaryReader(fs)
            ' Dim an array of bytes large enough to hold the file's contents.
            Dim bytes(fs.Length) As Byte
            Dim bSuccess As Boolean
            ' Your unmanaged pointer.
            Dim pUnmanagedBytes As IntPtr

            ' Read the contents of the file into the array.
            bytes = br.ReadBytes(fs.Length)
            ' Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(fs.Length)
            ' Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, fs.Length)
            ' Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, fs.Length)
            ' Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes)
            fs.Close()
            Return bSuccess
        End Function ' SendFileToPrinter()

        ' When the function is given a string and a printer name,
        ' the function sends the string to the printer as raw bytes.
        Public Shared Function SendStringToPrinter(ByVal szPrinterName As String, ByVal szString As String)
            Dim pBytes As IntPtr
            Dim dwCount As Int32
            ' How many characters are in the string?
            dwCount = szString.Length()
            ' Assume that the printer is expecting ANSI text, and then convert
            ' the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString)

            ' Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount)
            Marshal.FreeCoTaskMem(pBytes)
            Return True
        End Function
    End Class
    Public Function MalayamDays(ByVal FunctionDate As Date) As String Implements Int_ChitFund.MalayamDays
        Try

            Dim MalayalamDays As String = ""
            Dim MalayamMonthName As String = ""


            If FunctionDate.ToString("MMMM") = "August" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Chingam"
                    MalayalamDays = FunctionDate.Day - 16
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Karkidakam"
                    MalayalamDays = FunctionDate.Day + 16
                End If
            ElseIf FunctionDate.ToString("MMMM") = "September" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Kanni"
                    MalayalamDays = FunctionDate.Day - 16
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Chingam"
                    MalayalamDays = FunctionDate.Day + 15
                End If
            ElseIf FunctionDate.ToString("MMMM") = "October" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Thulam"
                    MalayalamDays = FunctionDate.Day - 16
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Kanni"
                    MalayalamDays = FunctionDate.Day + 14
                End If
            ElseIf FunctionDate.ToString("MMMM") = "November" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Vrischikam"
                    MalayalamDays = FunctionDate.Day - 15
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Thulam"
                    MalayalamDays = FunctionDate.Day + 15
                End If
            ElseIf FunctionDate.ToString("MMMM") = "December" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Dhanu"
                    MalayalamDays = FunctionDate.Day - 15
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Vrischikam"
                    MalayalamDays = FunctionDate.Day + 15
                End If
            ElseIf FunctionDate.ToString("MMMM") = "January" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Makaram"
                    MalayalamDays = FunctionDate.Day - 14
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Dhanu"
                    MalayalamDays = FunctionDate.Day + 15
                End If
            ElseIf FunctionDate.ToString("MMMM") = "February" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Kumbham"
                    MalayalamDays = FunctionDate.Day - 13
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Makaram"
                    MalayalamDays = FunctionDate.Day + 17
                End If
            ElseIf FunctionDate.ToString("MMMM") = "March" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Meenam"
                    MalayalamDays = FunctionDate.Day - 13
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Kumbham"
                    MalayalamDays = FunctionDate.Day + 16
                End If
            ElseIf FunctionDate.ToString("MMMM") = "April" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Medam"
                    MalayalamDays = FunctionDate.Day - 13
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Meenam"
                    MalayalamDays = FunctionDate.Day + 18
                End If
            ElseIf FunctionDate.ToString("MMMM") = "May" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Edavam"
                    MalayalamDays = FunctionDate.Day - 14
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Medam"
                    MalayalamDays = FunctionDate.Day + 17
                End If
            ElseIf FunctionDate.ToString("MMMM") = "June" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Mithunam"
                    MalayalamDays = FunctionDate.Day - 14
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Edavam"
                    MalayalamDays = FunctionDate.Day + 17
                End If
            ElseIf FunctionDate.ToString("MMMM") = "July" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Karkidakam"
                    MalayalamDays = FunctionDate.Day - 15
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Mithunam"
                    MalayalamDays = FunctionDate.Day + 16
                End If
            End If

            Return MalayalamDays
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
    Public Function MalayamMonths(ByVal FunctionDate As Date) As String Implements Int_ChitFund.MalayamMonths
        Try

            Dim MalayamMonthName As String = ""


            If FunctionDate.ToString("MMMM") = "August" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Chingam"
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Karkidakam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "September" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Kanni"
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Chingam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "October" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Thulam"
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Kanni"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "November" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Vrischikam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Thulam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "December" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Dhanu"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Vrischikam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "January" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Makaram"
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Dhanu"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "February" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Kumbham"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Makaram"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "March" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Meenam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Kumbham"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "April" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Medam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Meenam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "May" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Edavam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Medam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "June" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Mithunam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Edavam"
                End If
            ElseIf FunctionDate.ToString("MMMM") = "July" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Karkidakam"
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Mithunam"
                End If
            End If



            Return MalayamMonthName
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
    Public Function MalayamYears(ByVal FunctionDate As Date) As String Implements Int_ChitFund.MalayamYears
        '2016-1191
        '2017-1192
        Try
            Dim MalayamYear As Decimal
            Dim MalayamMonthName As String = ""


            If FunctionDate.ToString("MMMM") = "August" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Chingam"
                    MalayamYear = FunctionDate.Year - (824)
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Karkidakam"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "September" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Kanni"
                    MalayamYear = FunctionDate.Year - (824)
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Chingam"
                    MalayamYear = FunctionDate.Year - (824)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "October" Then
                If FunctionDate.Day > 16 Then
                    MalayamMonthName = "Thulam"
                    MalayamYear = FunctionDate.Year - (824)
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Kanni"
                    MalayamYear = FunctionDate.Year - (824)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "November" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Vrischikam"
                    MalayamYear = FunctionDate.Year - (824)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Thulam"
                    MalayamYear = FunctionDate.Year - (824)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "December" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Dhanu"
                    MalayamYear = FunctionDate.Year - (824)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Vrischikam"
                    MalayamYear = FunctionDate.Year - (824)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "January" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Makaram"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 16 Then
                    MalayamMonthName = "Dhanu"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "February" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Kumbham"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Makaram"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "March" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Meenam"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Kumbham"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "April" Then
                If FunctionDate.Day > 13 Then
                    MalayamMonthName = "Medam"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Meenam"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "May" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Edavam"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Medam"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "June" Then
                If FunctionDate.Day > 14 Then
                    MalayamMonthName = "Mithunam"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Edavam"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            ElseIf FunctionDate.ToString("MMMM") = "July" Then
                If FunctionDate.Day > 15 Then
                    MalayamMonthName = "Karkidakam"
                    MalayamYear = FunctionDate.Year - (825)
                ElseIf FunctionDate.Day <= 15 Then
                    MalayamMonthName = "Mithunam"
                    MalayamYear = FunctionDate.Year - (825)
                End If
            End If

            Return MalayamYear
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try
    End Function
    Public Function ComplexRoomUpdation1() As Boolean Implements Int_ChitFund.ComplexRoomUpdation

        Try

            Ds = New DataSet
            Adapter = New SqlDataAdapter("SELECT a.Room_Number_vc AS 'RoomNo',b.Book_BookingID_n AS 'BookingID' FROM tbl_RoomDetails a LEFT JOIN tbl_BookingDetails b On(a.Room_Number_vc=b.Room_Number_vc) WHERE b.Book_RoomCheckOut_vc='NO' AND b.Book_CustName_vc <>'Again' ORDER BY a.Room_BlockName_vc,a.Room_Number_vc", VistawinFunctions.ServerConnection)
            Adapter.Fill(Ds, "RoomDetails")

            Dim TotalPending, TotalAdvance As Decimal
            TotalPending = 0
            TotalAdvance = 0

            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If Ds.Tables("RoomDetails").Rows.Count > 0 Then
                For Each Dr As DataRow In Ds.Tables("RoomDetails").Rows

                    'Dim Row As Decimal

                    'Row = 0

                    Dim MaxToDate, MaxBookDate As Date
                    Dim CustomerID1, BookingID1, RmRent1 As Decimal


                    Cmd = New SqlCommand("SELECT Book_CustID_n FROM tbl_BookingDetails   WHERE Room_Number_vc ='" & Dr.Item("RoomNo") & "' AND Book_CustName_vc<>'Again' AND Book_RoomCheckOut_vc='NO'", VistawinFunctions.ServerConnection, Trans)
                    CustomerID1 = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT Book_BookingID_n FROM tbl_BookingDetails   WHERE Room_Number_vc ='" & Dr.Item("RoomNo") & "' AND Book_CustName_vc<>'Again' AND Book_RoomCheckOut_vc='NO'", VistawinFunctions.ServerConnection, Trans)
                    BookingID1 = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT MAX(Book_CheckINDate_d) FROM tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO'", VistawinFunctions.ServerConnection, Trans)
                    MaxBookDate = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("select MAX(Book_ToDate_d) from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO'", VistawinFunctions.ServerConnection, Trans)
                    MaxToDate = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("select Book_RentPerDay_n  from  tbl_BookingDetails where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' AND CONVERT(DATE,Book_CheckINDate_d)='" & Format(CDate(MaxBookDate), "yyyy/MM/dd") & "'", VistawinFunctions.ServerConnection, Trans)
                    RmRent1 = Cmd.ExecuteScalar()

                    Dim HourDiff As Integer = 0
                    Dim MinuteDiff As Integer = 0
                    Dim SecondDiff As Integer = 0
                    Dim DayDiff As Integer = 0
                    Dim MonthDiff As Integer = 0
                    Dim YearDiff As Integer = 0
                    Dim DateDiff As Integer = 0

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (yy, max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    YearDiff = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (mm, max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    MonthDiff = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (dd, max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    DayDiff = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (hh, max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    HourDiff = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (minute,max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    MinuteDiff = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT isnull(DATEDIFF (SS,max(Book_ToDate_d),GETDATE()  ),0)   from tbl_BookingDetails   where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' ", VistawinFunctions.ServerConnection, Trans)
                    SecondDiff = Cmd.ExecuteScalar()

                    '---------------------------

                    Dim RentType1 As String = ""
                    Cmd = New SqlCommand("SELECT  Book_RentType_vc FROM tbl_BookingDetails where Room_Number_vc ='" & Dr.Item("RoomNo") & "' and Book_RoomCheckOut_vc='NO' AND Book_CustName_vc<>'Again'", VistawinFunctions.ServerConnection, Trans)
                    RentType1 = Cmd.ExecuteScalar()
                    '----------------------------------


                    Dim GraceType As String
                    Dim GraceType1 As String()
                    Dim GraceHour, GraceMinute As Decimal
                    GraceType = ""
                    GraceType1 = Nothing
                    GraceHour = 0
                    GraceMinute = 0

                    Cmd = New SqlCommand("SELECT  GracePeriod_Type_vc FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection, Trans)
                    GraceType = Cmd.ExecuteScalar()

                    Cmd = New SqlCommand("SELECT  GracePeriod_Time_vc FROM tbl_CompanyProfile", VistawinFunctions.ServerConnection, Trans)
                    GraceType1 = Split(Cmd.ExecuteScalar(), ",")

                    GraceHour = CDec(GraceType1(0))
                    GraceMinute = CDec(GraceType1(1))

                    If GraceType = "BEFORE" Then
                        HourDiff = (HourDiff + GraceHour)
                        MinuteDiff = ((MinuteDiff + (GraceHour * 60)))
                        SecondDiff = 0
                    ElseIf GraceType = "AFTER" Then
                        HourDiff = (HourDiff - GraceHour)
                        MinuteDiff = ((MinuteDiff - (GraceHour * 60)))
                        SecondDiff = 0
                    End If

Check:
                    'If HourDiff > 0 Or MinuteDiff > 0 Or SecondDiff > 0 Then
                    '    For Row = 0 To HourDiff
                    '        If Row = 24 Then
                    '            DateDiff += 1
                    '            HourDiff -= 24
                    '            MinuteDiff -= (24 * 60)
                    '            SecondDiff -= (24 * (60 * 60))
                    '            GoTo Check
                    '        ElseIf HourDiff < 24 Then
                    '            DateDiff += 1
                    '            Exit For
                    '        End If
                    '    Next
                    'End If



                    If RentType1.ToUpper() = "DAILY" Then
                        If HourDiff > 0 Or MinuteDiff > 0 Or SecondDiff > 0 Then
                            For Row = 0 To HourDiff
                                If Row = 24 Then
                                    DateDiff += 1
                                    HourDiff -= 24
                                    MinuteDiff -= (24 * 60)
                                    SecondDiff -= (24 * (60 * 60))
                                    GoTo Check
                                ElseIf HourDiff < 24 Then
                                    DateDiff += 1
                                    Exit For
                                End If
                            Next
                        End If
                    ElseIf RentType1.ToUpper() = "MONTHLY" Then
                        If HourDiff > 0 Or MinuteDiff > 0 Or SecondDiff > 0 Then
                            For Row = 0 To HourDiff
                                If Row = 24 Then
                                    DateDiff += 1
                                    HourDiff -= 24
                                    MinuteDiff -= (24 * 60)
                                    SecondDiff -= (24 * (60 * 60))
                                    GoTo Check
                                ElseIf HourDiff < 24 Then
                                    DateDiff += 1
                                    Exit For
                                End If
                            Next
                        End If
                    ElseIf RentType1.ToUpper() = "YEARLY" Then
                        If HourDiff > 0 Or MinuteDiff > 0 Or SecondDiff > 0 Then
                            For Row = 0 To HourDiff
                                If Row = 24 Then
                                    DateDiff += 1
                                    HourDiff -= 24
                                    MinuteDiff -= (24 * 60)
                                    SecondDiff -= (24 * (60 * 60))
                                    GoTo Check
                                ElseIf HourDiff < 24 Then
                                    DateDiff += 1
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    If DateDiff > 0 Then
                        If RentType1.ToUpper() = "DAILY" Then
                            For Row = 0 To DateDiff - 1
                                Using Cmd1 As New SqlCommand("SP_tbl_StayAgain", VistawinFunctions.ServerConnection, Trans)
                                    Cmd1.CommandType = CommandType.StoredProcedure


                                    Dim Book_EntDate_d As New SqlParameter("@Book_EntDate_d", SqlDbType.DateTime)
                                    Book_EntDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_EntDate_d)

                                    Dim Book_BookingID_n As New SqlParameter("@Book_BookingID_n", SqlDbType.Int)
                                    Book_BookingID_n.Value = BookingID1
                                    Cmd1.Parameters.Add(Book_BookingID_n)

                                    Dim Room_Number_vc As New SqlParameter("@Room_Number_vc", SqlDbType.VarChar)
                                    Room_Number_vc.Value = Dr.Item("RoomNo")
                                    Cmd1.Parameters.Add(Room_Number_vc)

                                    Dim Book_CheckINDate_d As New SqlParameter("@Book_CheckINDate_d", SqlDbType.DateTime)
                                    Book_CheckINDate_d.Value = MaxToDate.AddDays(Row)
                                    Cmd1.Parameters.Add(Book_CheckINDate_d)

                                    Dim Book_ToDate_d As New SqlParameter("@Book_ToDate_d", SqlDbType.DateTime)
                                    If RentType1.ToUpper() = "DAILY" Then
                                        Book_ToDate_d.Value = MaxToDate.AddDays(Row + 1)
                                    ElseIf RentType1.ToUpper() = "MONTHLY" Then
                                        Book_ToDate_d.Value = MaxToDate.AddMonths(Row + 1)
                                    ElseIf RentType1.ToUpper() = "YEARLY" Then
                                        Book_ToDate_d.Value = MaxToDate.AddYears(Row + 1)
                                    End If
                                    Cmd1.Parameters.Add(Book_ToDate_d)

                                    Dim Book_CustID_n As New SqlParameter("@Book_CustID_n", SqlDbType.Decimal)
                                    Book_CustID_n.Value = CustomerID1
                                    Cmd1.Parameters.Add(Book_CustID_n)

                                    Dim Book_CustName_vc As New SqlParameter("@Book_CustName_vc", SqlDbType.VarChar)
                                    Book_CustName_vc.Value = "Again"
                                    Cmd1.Parameters.Add(Book_CustName_vc)

                                    Dim Book_RentPerDay_n As New SqlParameter("@Book_RentPerDay_n", SqlDbType.Decimal)
                                    Book_RentPerDay_n.Value = RmRent1
                                    Cmd1.Parameters.Add(Book_RentPerDay_n)

                                    Dim Book_RoomCheckOut_vc As New SqlParameter("@Book_RoomCheckOut_vc", SqlDbType.VarChar)
                                    Book_RoomCheckOut_vc.Value = "NO"
                                    Cmd1.Parameters.Add(Book_RoomCheckOut_vc)

                                    Dim Book_UserID_vc As New SqlParameter("@Book_UserID_vc", SqlDbType.VarChar)
                                    Book_UserID_vc.Value = VistawinFunctions.UserName
                                    Cmd1.Parameters.Add(Book_UserID_vc)

                                    Dim Book_UpdatedDate_d As New SqlParameter("@Book_UpdatedDate_d", SqlDbType.DateTime)
                                    Book_UpdatedDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_UpdatedDate_d)

                                    Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                                    InputString.Value = "Add"
                                    Cmd1.Parameters.Add(InputString)

                                    Execute = Cmd1.ExecuteNonQuery
                                End Using
                            Next
                            '------
                        ElseIf RentType1.ToUpper() = "MONTHLY" Then
                            For Row = 0 To MonthDiff - 1
                                Using Cmd1 As New SqlCommand("SP_tbl_StayAgain", VistawinFunctions.ServerConnection, Trans)
                                    Cmd1.CommandType = CommandType.StoredProcedure


                                    Dim Book_EntDate_d As New SqlParameter("@Book_EntDate_d", SqlDbType.DateTime)
                                    Book_EntDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_EntDate_d)

                                    Dim Book_BookingID_n As New SqlParameter("@Book_BookingID_n", SqlDbType.Int)
                                    Book_BookingID_n.Value = BookingID1
                                    Cmd1.Parameters.Add(Book_BookingID_n)

                                    Dim Room_Number_vc As New SqlParameter("@Room_Number_vc", SqlDbType.VarChar)
                                    Room_Number_vc.Value = Dr.Item("RoomNo")
                                    Cmd1.Parameters.Add(Room_Number_vc)

                                    Dim Book_CheckINDate_d As New SqlParameter("@Book_CheckINDate_d", SqlDbType.DateTime)
                                    Book_CheckINDate_d.Value = MaxToDate.AddMonths(Row)
                                    Cmd1.Parameters.Add(Book_CheckINDate_d)

                                    Dim Book_ToDate_d As New SqlParameter("@Book_ToDate_d", SqlDbType.DateTime)
                                    Book_ToDate_d.Value = MaxToDate.AddMonths(Row + 1)
                                    Cmd1.Parameters.Add(Book_ToDate_d)

                                    Dim Book_CustID_n As New SqlParameter("@Book_CustID_n", SqlDbType.Decimal)
                                    Book_CustID_n.Value = CustomerID1
                                    Cmd1.Parameters.Add(Book_CustID_n)

                                    Dim Book_CustName_vc As New SqlParameter("@Book_CustName_vc", SqlDbType.VarChar)
                                    Book_CustName_vc.Value = "Again"
                                    Cmd1.Parameters.Add(Book_CustName_vc)

                                    Dim Book_RentPerDay_n As New SqlParameter("@Book_RentPerDay_n", SqlDbType.Decimal)
                                    Book_RentPerDay_n.Value = RmRent1
                                    Cmd1.Parameters.Add(Book_RentPerDay_n)

                                    Dim Book_RoomCheckOut_vc As New SqlParameter("@Book_RoomCheckOut_vc", SqlDbType.VarChar)
                                    Book_RoomCheckOut_vc.Value = "NO"
                                    Cmd1.Parameters.Add(Book_RoomCheckOut_vc)

                                    Dim Book_UserID_vc As New SqlParameter("@Book_UserID_vc", SqlDbType.VarChar)
                                    Book_UserID_vc.Value = VistawinFunctions.UserName
                                    Cmd1.Parameters.Add(Book_UserID_vc)

                                    Dim Book_UpdatedDate_d As New SqlParameter("@Book_UpdatedDate_d", SqlDbType.DateTime)
                                    Book_UpdatedDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_UpdatedDate_d)

                                    Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                                    InputString.Value = "Add"
                                    Cmd1.Parameters.Add(InputString)

                                    Execute = Cmd1.ExecuteNonQuery
                                End Using
                            Next
                        ElseIf RentType1.ToUpper() = "YEARLY" Then
                            For Row = 0 To YearDiff - 1
                                Using Cmd1 As New SqlCommand("SP_tbl_StayAgain", VistawinFunctions.ServerConnection, Trans)
                                    Cmd1.CommandType = CommandType.StoredProcedure


                                    Dim Book_EntDate_d As New SqlParameter("@Book_EntDate_d", SqlDbType.DateTime)
                                    Book_EntDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_EntDate_d)

                                    Dim Book_BookingID_n As New SqlParameter("@Book_BookingID_n", SqlDbType.Int)
                                    Book_BookingID_n.Value = BookingID1
                                    Cmd1.Parameters.Add(Book_BookingID_n)

                                    Dim Room_Number_vc As New SqlParameter("@Room_Number_vc", SqlDbType.VarChar)
                                    Room_Number_vc.Value = Dr.Item("RoomNo")
                                    Cmd1.Parameters.Add(Room_Number_vc)

                                    Dim Book_CheckINDate_d As New SqlParameter("@Book_CheckINDate_d", SqlDbType.DateTime)
                                    Book_CheckINDate_d.Value = MaxToDate.AddYears(Row)
                                    Cmd1.Parameters.Add(Book_CheckINDate_d)

                                    Dim Book_ToDate_d As New SqlParameter("@Book_ToDate_d", SqlDbType.DateTime)
                                    Book_ToDate_d.Value = MaxToDate.AddYears(Row + 1)
                                    Cmd1.Parameters.Add(Book_ToDate_d)

                                    Dim Book_CustID_n As New SqlParameter("@Book_CustID_n", SqlDbType.Decimal)
                                    Book_CustID_n.Value = CustomerID1
                                    Cmd1.Parameters.Add(Book_CustID_n)

                                    Dim Book_CustName_vc As New SqlParameter("@Book_CustName_vc", SqlDbType.VarChar)
                                    Book_CustName_vc.Value = "Again"
                                    Cmd1.Parameters.Add(Book_CustName_vc)

                                    Dim Book_RentPerDay_n As New SqlParameter("@Book_RentPerDay_n", SqlDbType.Decimal)
                                    Book_RentPerDay_n.Value = RmRent1
                                    Cmd1.Parameters.Add(Book_RentPerDay_n)

                                    Dim Book_RoomCheckOut_vc As New SqlParameter("@Book_RoomCheckOut_vc", SqlDbType.VarChar)
                                    Book_RoomCheckOut_vc.Value = "NO"
                                    Cmd1.Parameters.Add(Book_RoomCheckOut_vc)

                                    Dim Book_UserID_vc As New SqlParameter("@Book_UserID_vc", SqlDbType.VarChar)
                                    Book_UserID_vc.Value = VistawinFunctions.UserName
                                    Cmd1.Parameters.Add(Book_UserID_vc)

                                    Dim Book_UpdatedDate_d As New SqlParameter("@Book_UpdatedDate_d", SqlDbType.DateTime)
                                    Book_UpdatedDate_d.Value = VistawinFunctions.TodayDate
                                    Cmd1.Parameters.Add(Book_UpdatedDate_d)

                                    Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                                    InputString.Value = "Add"
                                    Cmd1.Parameters.Add(InputString)

                                    Execute = Cmd1.ExecuteNonQuery
                                End Using
                            Next
                        End If
                    End If
                Next

                If Execute = True Then
                    Trans.Commit()
                End If
            End If
            Return True

        Catch ex As Exception
            Return False
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Function
    Public Function ClinicStockUpdate1() As Boolean Implements Int_ChitFund.StockUpdateQ
        Try
            Cmd = New SqlCommand("UPDATE tbl_Stock SET St_TotalSt_i=0 WHERE St_TotalSt_i IS NULL", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_MultiStock SET St_Show_vc='NO' WHERE St_CurrStock_i=0", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_MultiStock SET St_Show_vc='YES' WHERE St_CurrStock_i>0", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try


    End Function
    Public Function StockUpdate1() As Boolean Implements Int_ChitFund.StockUpdateIn
        Try
            Cmd = New SqlCommand("UPDATE tbl_StockDtl SET St_TotalSt_i=0 WHERE St_TotalSt_i IS NULL", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_MultiStock SET St_CurrStock_i=0 WHERE St_CurrStock_i IS NULL", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Cmd = New SqlCommand("UPDATE tbl_MultiStock SET St_Show_vc='YES' WHERE St_CurrStock_i>0", VistawinFunctions.ServerConnection)
            Cmd.ExecuteNonQuery()

            Return True
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return False
        End Try

    End Function
End Class
