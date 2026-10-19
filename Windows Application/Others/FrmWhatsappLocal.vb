Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports WindowsApplication.Models
Imports WindowsApplication.Api
Imports WindowsApplication.Utils
Imports System.Data.SqlClient

Public Class FrmWhatsappLocal

    Private Const DefaultApiUrl As String = "http://localhost:8080/vistawin1/api/"
    'Private Const DefaultApiUrl As String = "https://orangered-monkey-474827.hostingersite.com/"
    'Private Const ServerApiUrl As String = "https://orangered-monkey-474827.hostingersite.com/"

    Private apiClient As WhatsAppApiClient
    Private isSending As Boolean = False
    Private isStopping As Boolean = False
    Private allNumbers As List(Of String) = Nothing
    Private serverProcess As Process = Nothing

    Public WhCusID As String = ""
    Public WhCusName As String = ""
    Public WhCusMobNo As String = ""
    Public WhMsg As String = ""
    Public WhFilePath As String = ""

    Dim Ds As New DataSet
    Dim Cmd As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Trans As SqlTransaction
    Dim Execute As Boolean
    Dim Row, RowIndex As Decimal
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmWhatsappLocal_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        grpMore.Hide()
        grpMore.BringToFront()
        grpMore.Location = New System.Drawing.Point(17, 26)
        grpMore.Size = New System.Drawing.Size(795, 381)

        txtMessage.Text = WhMsg
        txtMobileNo.Text = WhCusMobNo
        txtFile.Text = WhFilePath

        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)

        'Dim ApiUrlToUse As String = DefaultApiUrl
        'If CbServerOnline.Checked = True Then
        '    If Not String.IsNullOrEmpty(ServerApiUrl) Then
        '        ApiUrlToUse = ServerApiUrl
        '    End If
        'End If
        'apiClient = New WhatsAppApiClient(ApiUrlToUse)

        apiClient = New WhatsAppApiClient(DefaultApiUrl)

        cmbCustomer.Text = DefaultApiUrl
        apiClient.BaseUrl = DefaultApiUrl

        AddHandler backgroundWorkerSend.DoWork, _
                   AddressOf BackgroundWorkerSend_DoWork

        AddHandler backgroundWorkerSend.ProgressChanged, _
                   AddressOf BackgroundWorkerSend_ProgressChanged

        AddHandler backgroundWorkerSend.RunWorkerCompleted, _
                   AddressOf BackgroundWorkerSend_RunWorkerCompleted

        AddHandler timerStatus.Tick, _
                   AddressOf TimerStatus_Tick

        AddHandler cmbCustomer.SelectedIndexChanged, _
                   AddressOf cmbCustomer_BaseUrlChanged

        AddHandler cmbCustomer.TextChanged, _
                   AddressOf cmbCustomer_BaseUrlChanged

        UpdateSendButtonState(True)
        LoadSettings()

        'IsNodeServerRunning()

        'Direct server check/start
        'ServerStart()

        'RefreshQR()

        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                LblFileType.Hide()
                PnlFileType.Hide()
                LblSelect.Hide()
                CbPlanImage.Hide()
                TabControl1.TabPages.Remove(TabPage2)
                PnlPerson.Hide()
                PicPlanImage.Hide()
            Case "CLINIC"
                LblFileType.Show()
                PnlFileType.Show()
                LblSelect.Show()
                CbPlanImage.Show()
                PnlPerson.Show()
                PicPlanImage.Show()
        End Select
    End Sub
    Private Sub FrmWhatsappLocal_FormClosing(ByVal sender As Object, _
                               ByVal e As FormClosingEventArgs) _
                               Handles MyBase.FormClosing

        timerStatus.Stop()
        SaveSettings()

    End Sub
    Private Sub StartStatusPolling()

        timerStatus.Interval = 500

        If Not timerStatus.Enabled Then
            timerStatus.Start()
        End If

    End Sub

    Private Sub btnCheckConnection_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCheckConnection.Click
        CheckServerStatus()
    End Sub

    Private Sub CheckServerStatus()

        btnCheckConnection.Enabled = False

        lblServerStatusValue.Text = "Checking..."
        lblServerStatusValue.ForeColor = Color.Orange

        Try

            Dim Url As String

            If String.IsNullOrEmpty(cmbCustomer.Text.Trim()) Then

                Url = DefaultApiUrl

            Else

                Url = NormalizeBaseUrl(cmbCustomer.Text.Trim())

            End If

            'localhost avoid செய்யவும்
            Url = Url.Replace("localhost", "127.0.0.1")

            cmbCustomer.Text = Url
            apiClient.BaseUrl = Url

            Dim status As ServerStatus = apiClient.GetStatus()

            If status Is Nothing Then

                lblServerStatusValue.Text = _
                    ChrW(10060) & " Invalid Response"

                lblServerStatusValue.ForeColor = Color.Red
                Exit Sub

            End If

            lblServerStatusValue.Text = _
                ChrW(9989) & " Server Running"

            lblServerStatusValue.ForeColor = _
                Color.FromArgb(37, 211, 102)

            UpdateWhatsAppStatus(status)

            If status.connected Then
                timerStatus.Stop()
            Else
                StartStatusPolling()
            End If

        Catch ex As WebException

            lblServerStatusValue.Text = _
                ChrW(10060) & " Server Unavailable"

            lblServerStatusValue.ForeColor = Color.Red

            MessageBox.Show( _
                ex.Message, _
                "Node Server", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Warning)

        Catch ex As Exception

            lblServerStatusValue.Text = _
                ChrW(10060) & " Error"

            lblServerStatusValue.ForeColor = Color.Red

            MessageBox.Show( _
                ex.Message, _
                "Error", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)

        Finally

            btnCheckConnection.Enabled = True

        End Try

    End Sub



    Private Sub PopulateCustomers()

    End Sub

    Private Sub cmbCustomer_BaseUrlChanged(ByVal sender As Object, ByVal e As EventArgs)
        If apiClient IsNot Nothing AndAlso Not String.IsNullOrEmpty(cmbCustomer.Text) Then
            apiClient.BaseUrl = NormalizeBaseUrl(cmbCustomer.Text)
        End If
    End Sub

    Private Sub LoadSettings()
        Try
            If txtDelay.Text.Trim() = "" Then
                txtDelay.Text = "2000"
            End If
        Catch
            txtDelay.Text = "2000"
        End Try

    End Sub
    Private Sub SaveSettings()
        Try
            Dim DelayValue As Integer = 2000
            If Not Integer.TryParse(txtDelay.Text.Trim(), DelayValue) Then
                DelayValue = 2000
            End If
            If DelayValue < 0 Then
                DelayValue = 2000
            End If
            txtDelay.Text = DelayValue.ToString()
        Catch
        End Try

    End Sub
    Private Sub btnRefreshQR_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefreshQR.Click
        RefreshQR()
    End Sub

    Private Sub RefreshQR()

        btnRefreshQR.Enabled = False

        Try

            Dim Url As String = NormalizeBaseUrl(cmbCustomer.Text.Trim())

            Url = Url.Replace("localhost", "127.0.0.1")

            apiClient.BaseUrl = Url

            Dim status As ServerStatus = apiClient.GetStatus()

            If status Is Nothing Then

                MessageBox.Show( _
                    "No response from Node Server.", _
                    "WhatsApp", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Warning)

                Exit Sub

            End If

            UpdateWhatsAppStatus(status)

        Catch ex As WebException

            MessageBox.Show( _
                "Node Server connection failed." & vbCrLf & vbCrLf & _
                ex.Message, _
                "WhatsApp", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)

        Catch ex As Exception

            MessageBox.Show( _
                "Failed to refresh QR code." & vbCrLf & vbCrLf & _
                ex.Message, _
                "WhatsApp", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)

        Finally

            btnRefreshQR.Enabled = True

        End Try

    End Sub

    Private Sub TimerStatus_Tick(ByVal sender As Object, _
                             ByVal e As EventArgs) _
                             Handles timerStatus.Tick

        Try

            If apiClient Is Nothing Then Exit Sub

            Dim status As ServerStatus = apiClient.GetStatus()

            If status Is Nothing Then Exit Sub

            UpdateWhatsAppStatus(status)

            If status.connected Then

                lblServerStatusValue.Text = _
                    ChrW(9989) & " Server Running"

                lblServerStatusValue.ForeColor = _
                    Color.FromArgb(37, 211, 102)

                timerStatus.Stop()

            End If

        Catch
            'Server starting - next 500ms tick-ல் மீண்டும் check
        End Try

    End Sub
    Private Function GreenCircle() As String
        Return ChrW(&HD83D) & ChrW(&HDFE2)
    End Function

    Private Sub UpdateWhatsAppStatus(ByVal status As ServerStatus)
        If status.connected Then
            lblWhatsAppStatusValue.Text = GreenCircle() & " WhatsApp Connected"
            lblWhatsAppStatusValue.ForeColor = Color.FromArgb(37, 211, 102)
            picQR.Image = Nothing
            picQR.Visible = False
            lblQRPlaceholder.Text = GreenCircle() & " WhatsApp Connected"
            lblQRPlaceholder.ForeColor = Color.FromArgb(37, 211, 102)
            lblQRPlaceholder.Visible = True
            timerStatus.Stop()

            If Not String.IsNullOrEmpty(status.phone) Then
                lblConnectedNumber.Text = "Connected Number: +" & status.phone
            Else
                lblConnectedNumber.Text = "Connected Number: ---"
            End If
            lblConnectedNumber.ForeColor = Color.Red
        ElseIf Not String.IsNullOrEmpty(status.qr) Then
            lblWhatsAppStatusValue.Text = "Scan QR Code"
            lblWhatsAppStatusValue.ForeColor = Color.Orange
            lblConnectedNumber.Text = "Connected Number: ---"
            lblConnectedNumber.ForeColor = Color.Red
            DisplayQRCode(status.qr)
        Else
            lblWhatsAppStatusValue.Text = "Waiting for QR..."
            lblWhatsAppStatusValue.ForeColor = Color.Gray
            lblConnectedNumber.Text = "Connected Number: ---"
            lblConnectedNumber.ForeColor = Color.Red
            picQR.Image = Nothing
            lblQRPlaceholder.Text = "Waiting for WhatsApp QR..."
            lblQRPlaceholder.Visible = True
            picQR.Visible = False
        End If
    End Sub

    Private Sub DisplayQRCode(ByVal qrData As String)
        Try
            Dim base64Image As String = qrData

            If base64Image.StartsWith("data:image") Then
                Dim commaIndex As Integer = base64Image.IndexOf(",")
                If commaIndex >= 0 Then
                    base64Image = base64Image.Substring(commaIndex + 1)
                End If
            End If

            Dim imageBytes As Byte() = Convert.FromBase64String(base64Image)
            Using ms As New MemoryStream(imageBytes)
                Dim originalImage As Image = Image.FromStream(ms)
                Dim clonedImage As Image = originalImage.Clone()
                originalImage.Dispose()

                If picQR.Image IsNot Nothing Then
                    picQR.Image.Dispose()
                End If
                picQR.Image = clonedImage
            End Using

            picQR.Visible = True
            lblQRPlaceholder.Visible = False
        Catch ex As Exception
            lblQRPlaceholder.Text = "Failed to load QR code"
            lblQRPlaceholder.ForeColor = Color.Red
            lblQRPlaceholder.Visible = True
            picQR.Visible = False
        End Try
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowse.Click
        If openFileDialog.ShowDialog() = DialogResult.OK Then
            txtFile.Text = openFileDialog.FileName
        End If
    End Sub

    Private Sub txtNumbers_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtMobileNo.TextChanged
        Dim numbers As List(Of String) = PhoneNumberUtils.GetUniqueNumbers(txtMobileNo.Text)
        lblTotalContacts.Text = "Total Contacts: " & numbers.Count.ToString()
    End Sub

    Private Sub btnSend_Click(ByVal sender As Object,
                          ByVal e As EventArgs) Handles btnSend.Click

        Try

            If isSending Then Exit Sub

            '-----------------------------------------
            ' SINGLE CUSTOMER
            '-----------------------------------------
            If RbSIngleCus.Checked = True Then

                If txtMobileNo.Text.Trim() = "" Then

                    MessageBox.Show(
                        "Please enter mobile number.",
                        "Mobile Number",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

                    txtMobileNo.Focus()
                    Exit Sub

                End If

                allNumbers =
                    PhoneNumberUtils.GetUniqueNumbers(
                        txtMobileNo.Text)

            End If
            If RbSIngleCus.Checked Then

                Dim MobileNo As String =
                    txtMobileNo.Text.Trim()

                MobileNo = MobileNo.Replace("+", "")
                MobileNo = MobileNo.Replace(" ", "")
                MobileNo = MobileNo.Replace("-", "")

                If MobileNo.StartsWith("91") AndAlso
                   MobileNo.Length = 12 Then

                    MobileNo = MobileNo.Substring(2)

                End If

                If MobileNo.Length = 10 Then

                    MobileNo = "91" & MobileNo

                End If

                allNumbers = New List(Of String)
                allNumbers.Add(MobileNo)

            End If

            '-----------------------------------------
            ' MULTI CUSTOMER
            '-----------------------------------------
            If RbMultiCus.Checked = True Then

                allNumbers =
                    GetSelectedCustomerNumbers()

            End If


            '-----------------------------------------
            ' COMMON VALIDATION
            '-----------------------------------------
            If allNumbers Is Nothing OrElse
               allNumbers.Count = 0 Then

                MessageBox.Show(
                    "Please select at least one customer.",
                    "No Valid Contacts",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning)

                Exit Sub

            End If


            '-----------------------------------------
            ' FILE VALIDATION
            '-----------------------------------------
            If txtFile.Text.Trim() <> "" Then

                If Not File.Exists(
                    txtFile.Text.Trim()) Then

                    MessageBox.Show(
                        "Selected file not found.",
                        "File",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

                    Exit Sub

                End If

            Else

                If txtMessage.Text.Trim() = "" Then

                    MessageBox.Show(
                        "Please enter message.",
                        "Message",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning)

                    Exit Sub

                End If

            End If


            '-----------------------------------------
            ' DELAY
            '-----------------------------------------
            Dim Delay As Integer = 5000

            If Not Integer.TryParse(
                txtDelay.Text.Trim(),
                Delay) Then

                Delay = 5000

            End If

            If Delay < 0 Then
                Delay = 0
            End If


            '-----------------------------------------
            ' START SEND
            '-----------------------------------------
            isSending = True
            isStopping = False

            UpdateSendButtonState(False)

            ResetProgress(allNumbers.Count)

            lstResults.Items.Clear()


            Dim Params As Object() = {
                txtFile.Text.Trim(),
                allNumbers,
                txtMessage.Text,
                Delay
            }


            backgroundWorkerSend.RunWorkerAsync(Params)


        Catch ex As Exception

            isSending = False
            isStopping = False

            UpdateSendButtonState(True)

            MessageBox.Show(
                ex.Message,
                "WhatsApp",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error)

        End Try

    End Sub
    Private Function GetSelectedCustomerNumbers() As List(Of String)

        Dim Numbers As New List(Of String)

        For Each Row As DataGridViewRow In DgvTreatList1.Rows

            If Row.IsNewRow Then Continue For

            Dim TickValue As String =
                Convert.ToString(Row.Cells("Tick1").Value).Trim().ToUpper()

            If TickValue = "YES" OrElse TickValue = "TRUE" Then

                Dim MobileNo As String =
                    Convert.ToString(Row.Cells("CusMobile1").Value).Trim()

                'Remove +91 / 91 / spaces
                MobileNo = MobileNo.Replace("+", "")
                MobileNo = MobileNo.Replace(" ", "")
                MobileNo = MobileNo.Replace("-", "")

                If MobileNo.StartsWith("91") AndAlso
                   MobileNo.Length = 12 Then

                    MobileNo = MobileNo.Substring(2)

                End If

                '10 digit Indian mobile number
                If MobileNo.Length = 10 Then

                    MobileNo = "91" & MobileNo

                    If Not Numbers.Contains(MobileNo) Then
                        Numbers.Add(MobileNo)
                    End If

                End If

            End If

        Next

        Return Numbers

    End Function
    Private Sub btnStop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnStop.Click
        If Not isSending Then Return

        isStopping = True
        btnStop.Enabled = False

        Try
            apiClient.StopSending()
            AddResultItem("Stop request sent to server.", Color.Orange)
        Catch ex As Exception
            AddResultItem("Failed to send stop request: " & ex.Message, Color.Red)
        End Try
    End Sub

    Private Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Dim answer As DialogResult = MessageBox.Show( _
            "Log out from WhatsApp?" & vbCrLf & vbCrLf & _
            "The linked session will be cleared and a fresh QR code will be shown for re-linking.", _
            "Logout Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)

        If answer <> DialogResult.Yes Then Return

        btnLogout.Enabled = False
        Try
            Dim responseString As String = apiClient.Logout()
            AddResultItem("Logout requested. Wait a few seconds for a fresh QR code.", Color.Orange)
            lblServerStatusValue.Text = ChrW(10060) & " Logged out"
            lblServerStatusValue.ForeColor = Color.Red
        Catch ex As Exception
            AddResultItem("Logout failed: " & ex.Message, Color.Red)
        Finally
            btnLogout.Enabled = True
        End Try
    End Sub

    Private Sub BackgroundWorkerSend_ProgressChanged(
        ByVal sender As Object,
        ByVal e As ProgressChangedEventArgs)

        If e.UserState Is Nothing Then Exit Sub


        Dim state As Object() =
            CType(e.UserState, Object())


        Dim action As String =
            CStr(state(0))

        Dim current As Integer =
            CInt(state(1))

        Dim total As Integer =
            CInt(state(2))

        Dim number As String =
            CStr(state(3))


        progressBar.Value =
            Math.Min(
                Math.Max(e.ProgressPercentage, 0),
                100)


        lblTotalValue.Text =
            total.ToString()


        Select Case action


            Case "sending"

                lblPendingValue.Text =
                    (total - current).ToString()

                AddResultItem(
                    "Sending to " & number & "...",
                    Color.Gray)


            Case "sent"

                lblSentValue.Text =
                    current.ToString()

                Dim pending As Integer =
                    total - current

                If pending < 0 Then
                    pending = 0
                End If

                lblPendingValue.Text =
                    pending.ToString()


                AddResultItem(
                    ChrW(10004) &
                    " " &
                    number &
                    " - Sent",
                    Color.FromArgb(
                        37,
                        211,
                        102))


            Case "failed"

                Dim failCount As Integer =
                    CInt(state(4))


                lblFailedValue.Text =
                    failCount.ToString()


                Dim pending As Integer =
                    total - current

                If pending < 0 Then
                    pending = 0
                End If

                lblPendingValue.Text =
                    pending.ToString()


                Dim errMsg As String = ""

                If state.Length > 5 Then

                    errMsg =
                        Convert.ToString(
                            state(5))

                End If


                If errMsg <> "" Then
                    errMsg =
                        " (" & errMsg & ")"
                End If


                AddResultItem(
                    ChrW(10008) &
                    " " &
                    number &
                    " - Failed" &
                    errMsg,
                    Color.Red)

        End Select

    End Sub

    Private Sub BackgroundWorkerSend_DoWork(
    ByVal sender As Object,
    ByVal e As DoWorkEventArgs)

        Dim worker As BackgroundWorker =
            CType(sender, BackgroundWorker)

        Try

            Dim params As Object() =
                CType(e.Argument, Object())

            Dim filePath As String =
                CStr(params(0))

            Dim numbers As List(Of String) =
                CType(params(1), List(Of String))

            Dim message As String =
                CStr(params(2))

            Dim delay As Integer =
                CInt(params(3))


            If numbers Is Nothing OrElse
               numbers.Count = 0 Then

                e.Result = New Object() {
                    0,
                    0,
                    0,
                    "No numbers"
                }

                Exit Sub

            End If


            '------------------------------------------
            ' SEND
            '------------------------------------------

            Dim responseString As String =
                apiClient.SendDocument(
                    filePath,
                    numbers,
                    message,
                    delay)


            Dim sent As Integer = 0
            Dim failed As Integer = 0


            If responseString Is Nothing OrElse
               responseString.Trim() = "" Then

                failed = numbers.Count

                e.Result = New Object() {
                    numbers.Count,
                    sent,
                    failed,
                    "No response from WhatsApp server"
                }

                Exit Sub

            End If


            '------------------------------------------
            ' READ RESPONSE
            '------------------------------------------

            Dim serializer As New JavaScriptSerializer()

            Dim response As SendResponse = Nothing

            Try

                response =
                    serializer.Deserialize(Of SendResponse)(
                        responseString)

            Catch exJson As Exception

                failed = numbers.Count

                e.Result = New Object() {
                    numbers.Count,
                    sent,
                    failed,
                    "Invalid API response"
                }

                Exit Sub

            End Try


            '------------------------------------------
            ' PROCESS RESULTS
            '------------------------------------------

            '------------------------------------------
            ' PROCESS RESULTS
            '------------------------------------------

            If response IsNot Nothing AndAlso
               response.results IsNot Nothing AndAlso
               response.results.Count > 0 Then

                Dim resultCount As Integer =
                    response.results.Count

                For i As Integer = 0 To resultCount - 1

                    Dim item As ContactResultItem =
                        response.results(i)

                    If item Is Nothing Then
                        Continue For
                    End If


                    Dim mobile As String =
                        Convert.ToString(
                            item.number).Trim()


                    Dim status As String =
                        Convert.ToString(
                            item.status).Trim().ToLower()


                    Dim remark As String = ""


                    '--------------------------------------
                    ' SAFE PROGRESS
                    '--------------------------------------

                    Dim progress As Integer = 0

                    If resultCount > 0 Then

                        progress =
                            CInt(
                                (CDbl(i + 1) /
                                 CDbl(resultCount)) * 100)

                    End If

                    If progress > 100 Then
                        progress = 100
                    End If


                    '--------------------------------------
                    ' SENT
                    '--------------------------------------

                    If status = "sent" Then

                        sent += 1

                        remark = "Message Sent"


                        InsertWhatsappRpt(
                            mobile,
                            message,
                            filePath,
                            "Successful",
                            remark)


                        worker.ReportProgress(
                            progress,
                            New Object() {
                                "sent",
                                sent,
                                resultCount,
                                mobile
                            })


                        '--------------------------------------
                        ' FAILED
                        '--------------------------------------

                    Else

                        failed += 1


                        remark =
                            Convert.ToString(
                                item.error).Trim()


                        If remark = "" Then
                            remark = "Send Failed"
                        End If


                        InsertWhatsappRpt(
                            mobile,
                            message,
                            filePath,
                            "Failed",
                            remark)


                        worker.ReportProgress(
                            progress,
                            New Object() {
                                "failed",
                                sent + failed,
                                resultCount,
                                mobile,
                                failed,
                                remark
                            })

                    End If

                Next


            Else

                '------------------------------------------
                ' NO RESULTS
                '------------------------------------------

                failed = numbers.Count


                For Each mobile As String In numbers

                    InsertWhatsappRpt(
                        mobile,
                        message,
                        filePath,
                        "Failed",
                        "No result from server")

                Next


                worker.ReportProgress(
                    100,
                    New Object() {
                        "failed",
                        failed,
                        numbers.Count,
                        "",
                        failed,
                        "No result from server"
                    })

            End If


            e.Result = New Object() {
                numbers.Count,
                sent,
                failed,
                "Completed"
            }


        Catch ex As Exception

            e.Result = New Object() {
                0,
                0,
                0,
                "Error : " & ex.Message
            }

        End Try

    End Sub
    Private Sub BackgroundWorkerSend_RunWorkerCompleted(
    ByVal sender As Object,
    ByVal e As RunWorkerCompletedEventArgs)

        Try

            If e.Result IsNot Nothing Then

                Dim result As Object() =
                    CType(e.Result, Object())


                Dim total As Integer =
                    CInt(result(0))

                Dim sent As Integer =
                    CInt(result(1))

                Dim failed As Integer =
                    CInt(result(2))

                Dim statusMsg As String =
                    CStr(result(3))


                lblTotalValue.Text =
                    total.ToString()

                lblSentValue.Text =
                    sent.ToString()

                lblFailedValue.Text =
                    failed.ToString()

                lblPendingValue.Text =
                    "0"

                progressBar.Value = 100


                If isStopping Then

                    AddResultItem(
                        "--- Stopped by user ---",
                        Color.Orange)


                ElseIf statusMsg = "Completed" Then

                    AddResultItem(
                        "--- All done: " &
                        sent.ToString() &
                        " sent, " &
                        failed.ToString() &
                        " failed ---",
                        Color.FromArgb(
                            37,
                            211,
                            102))


                    MessageBox.Show(
                        "WhatsApp Sending Completed." &
                        vbCrLf & vbCrLf &
                        "Total : " &
                        total.ToString() &
                        vbCrLf &
                        "Sent : " &
                        sent.ToString() &
                        vbCrLf &
                        "Failed : " &
                        failed.ToString(),
                        "WhatsApp",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information)


                ElseIf statusMsg.StartsWith("Error") Then

                    AddResultItem(
                        "--- " &
                        statusMsg &
                        " ---",
                        Color.Red)


                Else

                    AddResultItem(
                        "--- " &
                        statusMsg &
                        " ---",
                        Color.Orange)

                End If

            End If


        Catch ex As Exception

            AddResultItem(
                "Completed Error : " &
                ex.Message,
                Color.Red)

        Finally

            isSending = False
            isStopping = False

            UpdateSendButtonState(True)

        End Try

    End Sub
    Private Sub InsertWhatsappRpt(
    ByVal MobileNo As String,
    ByVal Message As String,
    ByVal FilePath As String,
    ByVal SendStatus As String,
    ByVal Remark As String)

        Try

            If VistawinFunctions.ServerConnection Is Nothing Then
                Exit Sub
            End If


            If VistawinFunctions.ServerConnection.State <>
               ConnectionState.Open Then

                VistawinFunctions.ServerConnection.Open()

            End If


            Dim MsgType As String = "WhatsApp"


            If FilePath.Trim() <> "" Then

                If VistawinFunctions.ProjectTypechk =
                   "CLINIC" Then

                    MsgType = "Prescription"

                Else

                    MsgType = "INVOICE"

                End If

            End If


            Dim FileType As String = ""

            If FilePath.Trim() <> "" Then

                FileType =
                    Path.GetExtension(
                        FilePath).Replace(
                            ".",
                            "").ToUpper()

            End If


            Using Cmd As New SqlCommand( _
                "INSERT INTO tbl_WhatsappRpt " &
                "(Wh_SendDate_d, " &
                "Wh_MsgType_vc, " &
                "Wh_TreatID_n, " &
                "Wh_InvNo_vc, " &
                "Wh_CusID_i, " &
                "Wh_PatDisplayID_vc, " &
                "Wh_MobileNo_vc, " &
                "Wh_Message_vc, " &
                "Wh_FileType_vc, " &
                "Wh_FilePath_vc, " &
                "Wh_SendStatus_vc, " &
                "Wh_Remark_vc, " &
                "Wh_UserID_vc) " &
                "VALUES " &
                "(GETDATE(), " &
                "@MsgType, " &
                "@TreatID, " &
                "@InvNo, " &
                "@CusID, " &
                "@PatDisplayID, " &
                "@MobileNo, " &
                "@Message, " &
                "@FileType, " &
                "@FilePath, " &
                "@SendStatus, " &
                "@Remark, " &
                "@UserID)", _
                VistawinFunctions.ServerConnection)


                Cmd.Parameters.AddWithValue(
                    "@MsgType",
                    MsgType)

                Cmd.Parameters.AddWithValue(
                    "@TreatID",
                    "0")

                Cmd.Parameters.AddWithValue(
                    "@InvNo",
                    "0")

                Cmd.Parameters.AddWithValue(
                    "@CusID",
                    GetReportCustomerID())

                Cmd.Parameters.AddWithValue(
                    "@PatDisplayID",
                    "0")

                Cmd.Parameters.AddWithValue(
                    "@MobileNo",
                    MobileNo)

                Cmd.Parameters.AddWithValue(
                    "@Message",
                    Message)

                Cmd.Parameters.AddWithValue(
                    "@FileType",
                    FileType)

                Cmd.Parameters.AddWithValue(
                    "@FilePath",
                    FilePath)

                Cmd.Parameters.AddWithValue(
                    "@SendStatus",
                    SendStatus)

                Cmd.Parameters.AddWithValue(
                    "@Remark",
                    Remark)

                Cmd.Parameters.AddWithValue(
                    "@UserID",
                    VistawinFunctions.UserName)


                Cmd.ExecuteNonQuery()

            End Using


        Catch ex As Exception

            'Report error should not stop WhatsApp sending
            Debug.WriteLine(
                "WhatsApp Report Error : " &
                ex.Message)

        End Try

    End Sub
    Private Function GetReportCustomerID() As String

        If WhCusID Is Nothing Then
            Return "0"
        End If

        If WhCusID.Trim() = "" Then
            Return "0"
        End If

        Return WhCusID.Trim()

    End Function
    Private Sub UpdateSendButtonState(ByVal enabled As Boolean)
        btnSend.Enabled = enabled
        btnStop.Enabled = Not enabled
    End Sub

    Private Sub ServerSetRunning()
        btnServerStart.Enabled = False
    End Sub

    Private Sub ServerSetStopped()
        btnServerStart.Enabled = True
    End Sub

    Private ReadOnly Property ServerHost As String
        Get
            Try
                Dim u As Uri = New Uri(apiClient.BaseUrl)
                Return u.Scheme & "://" & u.Host & ":" & EffectivePort(u).ToString()
            Catch
                Return "http://localhost:8080"
            End Try
        End Get
    End Property

    Private Function EffectivePort(ByVal u As Uri) As Integer
        If u.Port > 0 Then Return u.Port
        If u.Scheme = "https" Then Return 443
        Return 8080
    End Function

    Private Function NormalizeBaseUrl(ByVal raw As String) As String
        Dim s As String = ""
        If raw IsNot Nothing Then s = raw.Trim()
        If s.Length = 0 Then Return DefaultApiUrl
        If s.IndexOf("://") < 0 Then s = "http://" & s
        Try
            Dim u As Uri = New Uri(s)
            Return u.Scheme & "://" & u.Host & ":" & EffectivePort(u).ToString() & u.PathAndQuery
        Catch
            Return s
        End Try
    End Function

    Private Function FindNodeExe() As String
        Dim candidates As String() = New String() {
            "C:\Program Files\nodejs\node.exe",
            "C:\Program Files (x86)\nodejs\node.exe",
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "npm\node.exe")
        }
        For Each c As String In candidates
            If File.Exists(c) Then Return c
        Next
        Return "node"
    End Function

    Private Function FindServerScript() As String
        'Dim preferred As String() = New String() {
        '    "D:\VISTAWIN\WhatsAppDocumentServer\server.js"
        '}

        Dim Drives() As String = {"C:\", "D:\", "E:\", "F:\", "G:\"}
        For Each Drive As String In Drives

            Dim preferred As String() = New String() {
                Path.Combine(Drive, "VISTAWIN\WhatsAppDocumentServer\server.js") _
            }

            For Each p As String In preferred
                If File.Exists(p) Then Return p
            Next
        Next


        Dim current As DirectoryInfo = New DirectoryInfo(Application.StartupPath)
        For level As Integer = 0 To 6
            If current Is Nothing Then Exit For
            Dim candidates As String() = New String() {
                "server\server.js",
                "WhatsAppDocumentServer\server.js",
                "server.js"
            }
            For Each c As String In candidates
                Dim full As String = Path.GetFullPath(Path.Combine(current.FullName, c))
                If File.Exists(full) Then Return full
            Next
            current = current.Parent
        Next
        Return Nothing
    End Function
    Private Function IsServerReachable() As Boolean

        Try

            Dim request As HttpWebRequest = _
                CType(WebRequest.Create("http://127.0.0.1:8080/hello"),  _
                      HttpWebRequest)

            request.Method = "GET"

            request.Timeout = 500
            request.ReadWriteTimeout = 500

            request.Proxy = Nothing
            request.KeepAlive = False

            Using response As HttpWebResponse = _
                CType(request.GetResponse(), HttpWebResponse)

                Return response.StatusCode = HttpStatusCode.OK

            End Using

        Catch
            Return False
        End Try

    End Function
    Private Function RunNpmInstall(ByVal workDir As String) As Boolean
        Try
            Dim psi As New ProcessStartInfo()
            Dim npmCli As String = Path.Combine(Path.GetDirectoryName(FindNodeExe()), "node_modules\npm\bin\npm-cli.js")
            If File.Exists(npmCli) Then
                psi.FileName = FindNodeExe()
                psi.Arguments = """" & npmCli & """ install --no-audit --no-fund"
            Else
                psi.FileName = "npm.cmd"
                psi.Arguments = "install --no-audit --no-fund"
            End If
            psi.WorkingDirectory = workDir
            psi.UseShellExecute = True
            psi.WindowStyle = ProcessWindowStyle.Hidden

            Dim npmProc As Process = Process.Start(psi)
            While Not npmProc.HasExited
                Application.DoEvents()
                If Me.IsDisposed Then Exit While
                System.Threading.Thread.Sleep(200)
            End While
            npmProc.WaitForExit()

            If npmProc.ExitCode <> 0 Then
                AddResultItem("npm install failed (exit code " & npmProc.ExitCode.ToString() & ").", Color.Red)
                MessageBox.Show("npm install failed (exit code " & npmProc.ExitCode.ToString() & ")." & vbCrLf & _
                                "Open the server folder and run 'npm install' manually, then try again.", _
                                "Server Start", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False
            End If
            AddResultItem("Dependencies are ready.", Color.FromArgb(37, 211, 102))
            Return True
        Catch ex As Exception
            AddResultItem("npm install error: " & ex.Message, Color.Red)
            MessageBox.Show("Failed to run npm install: " & ex.Message, _
                            "Server Start", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function
    Private Sub btnServerStart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnServerStart.Click
        Me.ServerStart()
    End Sub
    Private Sub ServerStart()

        Dim port As Integer = 8080

        '---------------------------------------
        '1. Existing Node server?
        '---------------------------------------

        If IsServerReachable() Then

            ServerSetRunning()

            lblServerStatusValue.Text = _
                ChrW(9989) & " Server Running"

            lblServerStatusValue.ForeColor = _
                Color.FromArgb(37, 211, 102)

            AddResultItem( _
                "Existing WhatsApp server detected.", _
                Color.Green)

            'Immediately check WhatsApp
            CheckServerStatus()

            StartStatusPolling()

            Return

        End If

        '---------------------------------------
        '2. 8080 occupied by unknown process?
        '---------------------------------------
        If IsPortInUse(port) Then

            ServerSetStopped()

            lblServerStatusValue.Text = "Port 8080 Busy"
            lblServerStatusValue.ForeColor = Color.Red

            MessageBox.Show( _
                "Port 8080 is already used by another application." & _
                vbCrLf & vbCrLf & _
                "WhatsApp server could not be identified.", _
                "Server Start", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Warning)

            Return

        End If


        '---------------------------------------
        '3. No server -> start new Node server
        '---------------------------------------

        'உங்கள் remaining existing code...


        '--------------------------------------------------
        '4. Find server.js
        '--------------------------------------------------
        Dim script As String = FindServerScript()

        If String.IsNullOrEmpty(script) Then

            MessageBox.Show( _
                "server.js not found.", _
                "Server Start", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Warning)

            ServerSetStopped()

            Return

        End If


        Dim scriptDir As String = _
            Path.GetDirectoryName(script)


        '--------------------------------------------------
        '5. npm install only first time
        '--------------------------------------------------
        If NeedNpmInstall(scriptDir) Then

            AddResultItem( _
                "First time setup - Installing dependencies...", _
                Color.Orange)

            lblServerStatusValue.Text = _
                "Installing dependencies..."

            lblServerStatusValue.ForeColor = _
                Color.Orange


            If Not RunNpmInstall(scriptDir) Then

                ServerSetStopped()

                lblServerStatusValue.Text = _
                    ChrW(10060) & " npm install failed"

                lblServerStatusValue.ForeColor = _
                    Color.Red

                Return

            End If

        Else

            AddResultItem( _
                "Dependencies already available.", _
                Color.Gray)

        End If


        '--------------------------------------------------
        '6. Start Node Server
        '--------------------------------------------------
        Try

            Dim psi As New ProcessStartInfo()

            psi.FileName = FindNodeExe()
            psi.Arguments = """" & script & """"
            psi.WorkingDirectory = scriptDir

            psi.UseShellExecute = False
            psi.CreateNoWindow = True

            'Node/Baileys logs redirect வேண்டாம்
            psi.RedirectStandardOutput = False
            psi.RedirectStandardError = False

            serverProcess = Process.Start(psi)


            If serverProcess Is Nothing Then

                MessageBox.Show( _
                    "Unable to start Node Server.", _
                    "Server Start", _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Error)

                ServerSetStopped()

                Return

            End If


            AddResultItem( _
                "WhatsApp Server process started.", _
                Color.Orange)

            lblServerStatusValue.Text = _
                "Waiting for WhatsApp connection..."

            lblServerStatusValue.ForeColor = _
                Color.Orange


            'Background status checking
            StartStatusPolling()


            Return


        Catch ex As Exception

            MessageBox.Show( _
                "Failed to start Node Server:" & _
                vbCrLf & _
                ex.Message, _
                "Server Start", _
                MessageBoxButtons.OK, _
                MessageBoxIcon.Error)

            ServerSetStopped()

            Return

        End Try


    End Sub

    Private Sub ResetProgress(ByVal total As Integer)
        lblTotalValue.Text = total.ToString()
        lblSentValue.Text = "0"
        lblFailedValue.Text = "0"
        lblPendingValue.Text = total.ToString()
        progressBar.Value = 0
        progressBar.Maximum = 100
    End Sub

    Private Sub AddResultItem(ByVal text As String, ByVal color As Color)
        If lstResults.InvokeRequired Then
            lstResults.Invoke(Sub() AddResultItem(text, color))
            Return
        End If
        lstResults.Items.Add(text)
        lstResults.TopIndex = lstResults.Items.Count - 1
    End Sub
    Private Function NeedNpmInstall(ByVal workDir As String) As Boolean

        Dim nodeModules As String = _
            Path.Combine(workDir, "node_modules")

        Return Not Directory.Exists(nodeModules)

    End Function
    Private Function IsPortInUse(ByVal port As Integer) As Boolean

        Try

            Dim props As System.Net.NetworkInformation.IPGlobalProperties = _
                System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties()

            Dim listeners() As System.Net.IPEndPoint = _
                props.GetActiveTcpListeners()

            For Each ep As System.Net.IPEndPoint In listeners

                If ep.Port = port Then
                    Return True
                End If

            Next

        Catch
        End Try

        Return False

    End Function

    Private Sub BtnNodeStart_Click(ByVal sender As System.Object, _
                               ByVal e As System.EventArgs) _
                               Handles BtnNodeStart.Click

        Try

            If IsNodeServerRunning() Then

                lblServerStatusValue.Text = _
                    ChrW(9989) & " Server Running"

                lblServerStatusValue.ForeColor = Color.Green

                Exit Sub

            End If


            Dim VbsPath As String = _
                FindWhatsAppVbs("Node_start.vbs")


            If String.IsNullOrEmpty(VbsPath) Then

                MsgBox("Node_start.vbs not found!", _
                       MsgBoxStyle.Exclamation, _
                       "WhatsApp Server")

                Exit Sub

            End If


            Process.Start( _
                "wscript.exe", _
                """" & VbsPath & """")


            lblServerStatusValue.Text = "Starting Server..."
            lblServerStatusValue.ForeColor = Color.Orange

            StartStatusPolling()


        Catch ex As Exception

            MsgBox("Unable to start Node Server." & _
                   vbCrLf & ex.Message, _
                   MsgBoxStyle.Critical, _
                   "WhatsApp Server")

        End Try

    End Sub
    Private Function FindWhatsAppVbs(ByVal FileName As String) As String

        For Each Dr As IO.DriveInfo In IO.DriveInfo.GetDrives()

            Try

                If Dr.DriveType = IO.DriveType.Fixed AndAlso Dr.IsReady Then

                    Dim Path As String = _
                        IO.Path.Combine( _
                            Dr.RootDirectory.FullName, _
                            "VISTAWIN\WhatsAppDocumentServer\" & FileName)

                    If IO.File.Exists(Path) Then
                        Return Path
                    End If

                End If

            Catch
                'Skip inaccessible drive
            End Try

        Next

        Return String.Empty

    End Function
   
    Private Function IsNodeServerRunning() As Boolean

        Try

            Dim Req As Net.HttpWebRequest = CType(Net.WebRequest.Create("http://127.0.0.1:8080/vistawin1/api/status"), Net.HttpWebRequest)
            'Dim Req As Net.HttpWebRequest = CType(Net.WebRequest.Create("https://orangered-monkey-474827.hostingersite.com/"), Net.HttpWebRequest)

            Req.Proxy = Nothing
            Req.Timeout = 1500
            Req.KeepAlive = False

            Using Resp As Net.HttpWebResponse = _
                CType(Req.GetResponse(), Net.HttpWebResponse)

                Return Resp.StatusCode = Net.HttpStatusCode.OK

            End Using

        Catch

            Return False

        End Try

    End Function
    Private Sub BtnNodeStop_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) _
                              Handles BtnNodeStop.Click

        Try

            If Not IsNodeServerRunning() Then
                MsgBox("WhatsApp Server is not running.", _
                       MsgBoxStyle.Information, _
                       "WhatsApp Server")
                Exit Sub
            End If

            Dim VbsPath As String = _
                FindWhatsAppVbs("Node_stop.vbs")

            If String.IsNullOrEmpty(VbsPath) Then

                MsgBox("Node_stop.vbs not found!", _
                       MsgBoxStyle.Exclamation, _
                       "WhatsApp Server")

                Exit Sub

            End If

            Dim Psi As New ProcessStartInfo()

            Psi.FileName = "wscript.exe"
            Psi.Arguments = """" & VbsPath & """"
            Psi.WorkingDirectory = IO.Path.GetDirectoryName(VbsPath)
            Psi.UseShellExecute = True
            Psi.WindowStyle = ProcessWindowStyle.Hidden

            Process.Start(Psi)

            timerStatus.Stop()

            lblServerStatusValue.Text = "Server Stopped"
            lblServerStatusValue.ForeColor = Color.Red

        Catch ex As Exception

            MsgBox("Unable to stop Node Server." & vbCrLf & _
                   ex.Message, _
                   MsgBoxStyle.Critical, _
                   "WhatsApp Server")

        End Try

    End Sub

    Private Sub BtnMore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnMore.Click
        grpMore.Show()

    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub GrpClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GrpClose.Click
        grpMore.Hide()
    End Sub

    
    

    Private Sub BtnReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnReport.Click
        Dim frm As New RptWhatsappReport
        frm.ShowDialog()
    End Sub

    Private Sub BtnPreview1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview1.Click

        Select Case VistawinFunctions.ProjectTypechk
            Case "INVEN"
                Me.InvenCusList()
            Case "CLINIC"
                Me.ClinicCusList()
        End Select

    End Sub
    Private Sub InvenCusList()

        DgvTreatList1.Rows.Clear()
        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT DISTINCT C_ID_n AS 'CustID1',C_Name_vc AS 'CusName1',C_Mobile_vc AS 'CusMobile1' FROM tbl_CusProfile WHERE C_Type_vc = 'CUSTOMER' AND C_ID_n <50  AND C_Mobile_vc IS NOT NULL  AND LEN(LTRIM(RTRIM(C_Mobile_vc))) = 10  AND LTRIM(RTRIM(C_Mobile_vc)) NOT LIKE '%[^0-9]%'", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        Row = 0
        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvTreatList1.Rows.Add()
                DgvTreatList1.Rows(Row).Cells("SNo1").Value = Row + 1
                DgvTreatList1.Rows(Row).Cells("CustID1").Value = Dr.Item("CustID1")
                DgvTreatList1.Rows(Row).Cells("CusName1").Value = Dr.Item("CusName1")
                DgvTreatList1.Rows(Row).Cells("CusMobile1").Value = Dr.Item("CusMobile1")
                DgvTreatList1.Rows(Row).Cells("Tick1").Value = "NO"
                Row += 1
            Next
        Else
            MsgBox("No Data")
        End If

    End Sub
    Private Sub ClinicCusList()

        DgvTreatList1.Rows.Clear()
        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT DISTINCT P_PatID_n AS 'CustID1',P_PatName_vc AS 'CusName1',P_MobNo_vc AS 'CusMobile1',PT_RemEngMsg_vc AS 'MessageEng1',PT_NextTDate_d as 'NextTreatdate1' FROM tbl_PatientTreat pt  LEFT JOIN tbl_Patient p ON(PT_PatID_n=P_PatID_n) WHERE PT_NextTreat_vc='YES' AND PT_RemDate_d='" & Format(CDate(VistawinFunctions.TodayDate), "yyyy/MM/dd") & "' AND P_MobNo_vc IS NOT NULL AND LEN(LTRIM(RTRIM(P_MobNo_vc))) = 10  AND LTRIM(RTRIM(P_MobNo_vc)) NOT LIKE '%[^0-9]%'", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        Row = 0
        If Ds.Tables("FillData").Rows.Count > 0 Then
            For Each Dr As DataRow In Ds.Tables("FillData").Rows
                DgvTreatList1.Rows.Add()
                DgvTreatList1.Rows(Row).Cells("SNo1").Value = Row + 1
                DgvTreatList1.Rows(Row).Cells("CustID1").Value = Dr.Item("CustID1")
                DgvTreatList1.Rows(Row).Cells("CusName1").Value = Dr.Item("CusName1")
                DgvTreatList1.Rows(Row).Cells("CusMobile1").Value = Dr.Item("CusMobile1")
                DgvTreatList1.Rows(Row).Cells("MessageEng1").Value = Dr.Item("MessageEng1")
                DgvTreatList1.Rows(Row).Cells("NextTreatdate1").Value = Dr.Item("NextTreatdate1")
                DgvTreatList1.Rows(Row).Cells("Tick1").Value = "NO"
                Row += 1
            Next
            CBSelect.Checked = True
        Else
            MsgBox("No Data")
        End If
    End Sub
    Private Sub CBSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBSelect.CheckedChanged
        If CBSelect.Checked = True Then
            For Each Dr As DataGridViewRow In DgvTreatList1.Rows
                Dr.Cells("Tick1").Value = "YES"
            Next
        ElseIf CBSelect.Checked = False Then
            For Each Dr As DataGridViewRow In DgvTreatList1.Rows
                Dr.Cells("Tick1").Value = "NO"
            Next
        End If
    End Sub
    Private Sub RbPdf_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbPdf.CheckedChanged
        FileTypeChoose()
    End Sub

    Private Sub RbImage_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbImage.CheckedChanged
        FileTypeChoose()
    End Sub
    Private Sub FileTypeChoose()

        If RbPdf.Checked = True Then
            'LblSelect.Hide()
            'CbPlanImage.Hide()

        ElseIf RbImage.Checked = True Then
            LblSelect.Show()
            CbPlanImage.Show()
            LoadPlanImages()
        End If
    End Sub
    Private Sub LoadPlanImages()

        CbPlanImage.Items.Clear()
        Dim Drives() As String = {"C:\", "D:\", "E:\"}

        For Each Drive As String In Drives
            Dim FolderPath As String = IO.Path.Combine(Drive, "VISTAWIN\Scanningfile\Pat_PlanImg")

            If IO.Directory.Exists(FolderPath) Then
                Dim Files() As String = IO.Directory.GetFiles(FolderPath)

                For Each FilePath As String In Files
                    Dim Ext As String = IO.Path.GetExtension(FilePath).ToLower()
                    If Ext = ".jpg" OrElse Ext = ".jpeg" OrElse Ext = ".png" OrElse Ext = ".bmp" Then
                        CbPlanImage.Items.Add(IO.Path.GetFileNameWithoutExtension(FilePath))
                    End If
                Next
                Exit For
            End If
        Next

    End Sub
    Private Sub CbPlanImage_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CbPlanImage.SelectedIndexChanged
        Try
            If CbPlanImage.SelectedIndex = -1 Then Exit Sub

            Dim ImageName As String = CbPlanImage.Text.Trim()
            If ImageName = "" Then Exit Sub

            Dim ImagePath As String = ""

            Dim Drives() As String = {"C:\", "D:\", "E:\"}

            For Each Drive As String In Drives

                Dim FolderPath As String = IO.Path.Combine(Drive, "VISTAWIN\Scanningfile\Pat_PlanImg")
                If Not IO.Directory.Exists(FolderPath) Then
                    Continue For
                End If

                Dim Extensions() As String = {".jpg", ".jpeg", ".png", ".bmp"}
                For Each Ext As String In Extensions
                    Dim TempPath As String = IO.Path.Combine(FolderPath, ImageName & Ext)

                    If IO.File.Exists(TempPath) Then
                        ImagePath = TempPath
                        Exit For
                    End If
                Next
                If ImagePath <> "" Then Exit For
            Next

            If ImagePath = "" Then
                txtFile.Clear()
                PicPlanImage.Image = Nothing
                Exit Sub
            End If

            txtFile.Text = ImagePath

            If PicPlanImage.Image IsNot Nothing Then
                PicPlanImage.Image.Dispose()
                PicPlanImage.Image = Nothing
            End If

            Using Img As Image = Image.FromFile(ImagePath)
                PicPlanImage.Image = New Bitmap(Img)
            End Using
            PicPlanImage.SizeMode = PictureBoxSizeMode.Zoom

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Plan Image", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
End Class
