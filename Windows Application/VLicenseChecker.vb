Imports System.Net
Imports System.IO
Imports System.Windows.Forms

Public Class VLicenseChecker

    ' -----------------------------------------------------------------------
    ' Set your Hostinger domain and the shared API key (must match PHP file)
    ' -----------------------------------------------------------------------
    Private Const API_BASE As String = "https://vistawin.online/vb-renewal/check_renewal.php"
    Private Const API_KEY As String = "renewal"
    ' DB name must match c_dbname_vc value stored in tbl_ownclient
    'Private Const DB_NAME As String = "BHAVANIELECSURDB"
    Private Shared ReadOnly DB_NAME As String = VistawinFunctions.CmpDatabaseName
    ' -----------------------------------------------------------------------

    ''' <summary>
    ''' Checks renewal status from Hostinger MySQL via PHP API.
    ''' Returns True  = licence is valid  → allow app to continue.
    ''' Returns False = expired / not found / error → block app.
    ''' </summary>
    Public Shared Function CheckLicense() As Boolean


        Dim url As String = API_BASE &
                            "?dbname=" & Uri.EscapeDataString(DB_NAME) &
                            "&key=" & Uri.EscapeDataString(API_KEY)
        Dim json As String = ""

        ' ── Call the PHP API ────────────────────────────────────────────────
        Try
            ' Force TLS 1.2 — required by modern hosts (Hostinger rejects TLS 1.0)
            System.Net.ServicePointManager.SecurityProtocol = CType(3072, System.Net.SecurityProtocolType)

            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.Timeout = 8000   ' 8 seconds
            request.UserAgent = "LoginSystem/1.0"

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream())
                    json = reader.ReadToEnd().Trim()
                End Using
            End Using

        Catch ex As WebException
            ' Network / server unreachable — fail open with a warning
            Dim msg As String =
                "Could not reach the licence server." & Environment.NewLine &
                "Please check your internet connection." & Environment.NewLine & Environment.NewLine &
                ex.Message
            MessageBox.Show(msg, "Licence Check Warning",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ' Decide: True = allow offline use, False = block
            ' Change to False if you want strict online enforcement
            Return True
        Catch ex As Exception
            MessageBox.Show("Licence check error: " & ex.Message,
                            "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return True
        End Try

        ' ── Parse JSON manually (no external library needed) ────────────────
        ' .NET 3.5 has no built-in JSON parser, so we use simple string parsing
        Dim status As String = GetJsonValue(json, "status")
        Dim message As String = GetJsonValue(json, "message")
        Dim company As String = GetJsonValue(json, "company")
        Dim renewalDate As String = GetJsonValue(json, "renewal_date")
        Dim daysLeftStr As String = GetJsonValue(json, "days_left")
        Dim remark As String = GetJsonValue(json, "remark")
        ' Bank details
        Dim bankAcName As String = GetJsonValue(json, "bank_acname")
        Dim bankAccNo As String = GetJsonValue(json, "bank_accno")
        Dim bankName As String = GetJsonValue(json, "bank_name")
        Dim bankBranch As String = GetJsonValue(json, "bank_branch")
        Dim bankIfsc As String = GetJsonValue(json, "bank_ifsc")
        ' QR code image URL
        Dim qrUrl As String = GetJsonValue(json, "qr_url")

        ' ── React to status ─────────────────────────────────────────────────
        Select Case status.ToLower()

            Case "ok"
                ' Active — warn if expiring within 7 days
                Dim daysLeft As Integer = 0
                Integer.TryParse(daysLeftStr, daysLeft)

                If daysLeft <= 7 Then
                    Dim warnMsg As String =
                        "WARNING: Your licence expires in " & daysLeft & " day(s)." &
                        Environment.NewLine & Environment.NewLine &
                        "Company      : " & company &
                        Environment.NewLine &
                        "Renewal Date : " & renewalDate &
                        Environment.NewLine & Environment.NewLine &
                        "Please contact your Admin to renew."
                    MessageBox.Show(warnMsg, "Licence Expiring Soon",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
                Return True

            Case "expired"
                Dim daysOver As Integer = 0
                Integer.TryParse(daysLeftStr, daysOver)
                Dim overDays As String = Math.Abs(daysOver).ToString()

                ' Show custom alert form with bank details + QR image
                Using frm As New VExpiredAlertForm(
                        company, renewalDate, overDays, remark,
                        bankAcName, bankAccNo, bankName, bankBranch, bankIfsc,
                        qrUrl)
                    frm.ShowDialog()
                End Using
                Return False

            Case "notfound"
                'Dim nfMsg As String =
                '    "ACCESS DENIED  —  Not Registered" &
                '    Environment.NewLine &
                '    "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" &
                '    Environment.NewLine & Environment.NewLine &
                '    "This application (DB: " & DB_NAME & ") is not" &
                '    Environment.NewLine &
                '    "registered in the client list." &
                '    Environment.NewLine & Environment.NewLine &
                '    "Please Contact Your Admin."
                'MessageBox.Show(nfMsg, "Not Registered — Access Denied",
                '                MessageBoxButtons.OK, MessageBoxIcon.Error)
                'Return False

                Return True

            Case "error"
                Dim errMsg As String =
                    "Licence server returned an error:" &
                    Environment.NewLine & Environment.NewLine &
                    message &
                    Environment.NewLine & Environment.NewLine &
                    "Please Contact Your Admin."
                MessageBox.Show(errMsg, "Server Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return False

            Case Else
                ' Unknown / unparseable response — fail open
                Return True

        End Select

    End Function

    ' ────────────────────────────────────────────────────────────────────────
    ' Minimal JSON string value extractor — no external dependency needed.
    ' Handles:  "key": "value"   and   "key": 123
    ' ────────────────────────────────────────────────────────────────────────
    Private Shared Function GetJsonValue(ByVal json As String,
                                         ByVal key As String) As String
        ' Look for  "key":
        Dim searchKey As String = """" & key & """"
        Dim keyIdx As Integer = json.IndexOf(searchKey)
        If keyIdx < 0 Then Return ""

        Dim colonIdx As Integer = json.IndexOf(":"c, keyIdx + searchKey.Length)
        If colonIdx < 0 Then Return ""

        ' Skip whitespace after colon
        Dim valueStart As Integer = colonIdx + 1
        Do While valueStart < json.Length AndAlso
                 (json(valueStart) = " "c OrElse
                  json(valueStart) = Chr(9) OrElse
                  json(valueStart) = Chr(13) OrElse
                  json(valueStart) = Chr(10))
            valueStart += 1
        Loop

        If valueStart >= json.Length Then Return ""

        ' String value (starts with quote)
        If json(valueStart) = """"c Then
            Dim valueEnd As Integer = json.IndexOf(""""c, valueStart + 1)
            If valueEnd < 0 Then Return ""
            ' Unescape JSON forward slashes: \/ → /
            Dim raw As String = json.Substring(valueStart + 1, valueEnd - valueStart - 1)
            Return raw.Replace("\/", "/")
        End If

        ' Numeric / boolean value (ends at , or })
        Dim numEnd As Integer = valueStart
        Do While numEnd < json.Length AndAlso
                 json(numEnd) <> ","c AndAlso
                 json(numEnd) <> "}"c
            numEnd += 1
        Loop
        Return json.Substring(valueStart, numEnd - valueStart).Trim()

    End Function

End Class
