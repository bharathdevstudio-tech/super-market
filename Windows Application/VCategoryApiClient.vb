Imports System
Imports System.Globalization
Imports System.IO
Imports System.Net
Imports System.Text

''' <summary>
''' Calls the PHP API that writes to tbl_Category on Hostinger.
''' All calls are synchronous and must be made from a background thread.
''' </summary>
Public NotInheritable Class VCategoryApiClient

    ' >>> CHANGE THIS to your own domain, e.g. "https://www.mydomain.lk/api/" <<<
    ' The constant below is only the fallback. At start-up ResolveBaseUrl() looks for
    '   1) the environment variable CATEGORYENTRY_API_URL
    '   2) a text file named "api_url.txt" next to the EXE (handy: no rebuild needed)
    '   3) the ApiBaseUrl constant
    Public Const ApiBaseUrl As String = "https://vistawin.store/Inven-sbe/vb-dir-dataapi/"

    Private Const TimeoutMs As Integer = 30000

    Private Shared _resolvedBaseUrl As String = Nothing

    ''' <summary>Returns the API root that is actually used for every request.</summary>
    Public Shared Function ResolveBaseUrl() As String
        If _resolvedBaseUrl IsNot Nothing Then
            Return _resolvedBaseUrl
        End If

        Dim candidate As String = Environment.GetEnvironmentVariable("CATEGORYENTRY_API_URL")

        If String.IsNullOrEmpty(candidate) Then
            Try
                Dim sideCar As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api_url.txt")

                If File.Exists(sideCar) Then
                    For Each rawLine As String In File.ReadAllLines(sideCar)
                        Dim line As String = rawLine.Trim()

                        If line.Length > 0 AndAlso Not line.StartsWith("#", StringComparison.Ordinal) Then
                            candidate = line
                            Exit For
                        End If
                    Next
                End If
            Catch ex As Exception
                ' A missing or unreadable file simply means "use the constant".
            End Try
        End If

        If String.IsNullOrEmpty(candidate) OrElse candidate.Trim().Length = 0 Then
            candidate = ApiBaseUrl
        End If

        _resolvedBaseUrl = candidate.Trim()
        Return _resolvedBaseUrl
    End Function

    Public Sub New()
        ' .NET 3.5 does not know Tls12 as an enum member, use the numeric value 3072.
        ' Without this, newer hosts fail with "The underlying connection was closed".
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
    End Sub

    ''' <summary>INSERT one row into tbl_Category and returns the raw JSON response.</summary>
    Public Function SaveCategory(ByVal entryDate As String,
                                 ByVal code As String,
                                 ByVal name As String,
                                 ByVal aliasName As String,
                                 ByVal userId As String) As String
        Dim body As New StringBuilder()
        body.Append("{")
        AppendField(body, "ent_date_d", entryDate)
        AppendField(body, "category_code_n", code)
        AppendField(body, "category_name_vc", name)
        AppendField(body, "cat_aliasname_vc", aliasName)
        AppendField(body, "user_id_vc", userId)
        body.Append("}")

        Return HttpPost(BuildUrl("save_category.php"), body.ToString())
    End Function

    ''' <summary>DELETE one row of tbl_Category and returns the raw JSON response.</summary>
    Public Function DeleteCategory(ByVal entryDate As String,
                                  ByVal code As String,
                                  ByVal name As String,
                                  ByVal userId As String) As String
        Dim body As New StringBuilder()
        body.Append("{")
        AppendField(body, "ent_date_d", entryDate)
        AppendField(body, "category_code_n", code)
        AppendField(body, "category_name_vc", name)
        AppendField(body, "user_id_vc", userId)
        body.Append("}")

        Return HttpPost(BuildUrl("delete_category.php"), body.ToString())
    End Function

    ''' <summary>Reads the last rows of tbl_Category and returns the raw JSON response.</summary>
    Public Function ListCategories(ByVal limit As Integer) As String
        Dim url As String = BuildUrl("list_categories.php") & "?limit=" & limit.ToString(CultureInfo.InvariantCulture)
        Return HttpGet(url)
    End Function

    Private Shared Sub AppendField(ByVal body As StringBuilder, ByVal name As String, ByVal value As String)
        If body.Length > 1 Then
            body.Append(",")
        End If
        body.Append(VCategoryMiniJson.Quote(name)).Append(":").Append(VCategoryMiniJson.Quote(If(value, String.Empty)))
    End Sub

    Private Shared Function BuildUrl(ByVal fileName As String) As String
        Dim root As String = ResolveBaseUrl()
        If Not root.EndsWith("/", StringComparison.Ordinal) Then
            root &= "/"
        End If
        Return root & fileName
    End Function

    Private Shared Function HttpPost(ByVal url As String, ByVal json As String) As String
        Dim data As Byte() = Encoding.UTF8.GetBytes(json)

        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "POST"
        request.ContentType = "application/json; charset=utf-8"
        request.Accept = "application/json"
        request.ContentLength = data.Length
        request.KeepAlive = False
        request.Timeout = TimeoutMs
        request.ReadWriteTimeout = TimeoutMs
        request.UserAgent = "CategoryEntry/1.0"

        ' Some shared hosts wait for a 100-Continue reply, so the header is switched off.
        Dim servicePoint As ServicePoint = request.ServicePoint
        servicePoint.Expect100Continue = False

        Using requestStream As Stream = request.GetRequestStream()
            requestStream.Write(data, 0, data.Length)
        End Using

        Return ReadResponse(request)
    End Function

    Private Shared Function HttpGet(ByVal url As String) As String
        Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest)
        request.Method = "GET"
        request.Accept = "application/json"
        request.KeepAlive = False
        request.Timeout = TimeoutMs
        request.ReadWriteTimeout = TimeoutMs
        request.UserAgent = "CategoryEntry/1.0"

        Return ReadResponse(request)
    End Function

    ''' <summary>
    ''' The API answers with JSON even for 4xx / 5xx, so the error body is
    ''' returned to the caller and handled by MiniJson. Anything that is not
    ''' JSON (wrong URL, hosting error page) is reported as a clear message.
    ''' </summary>
    Private Shared Function ReadResponse(ByVal request As HttpWebRequest) As String
        Dim response As HttpWebResponse = Nothing
        Dim body As String = String.Empty
        Dim status As String = "200 OK"

        Try
            response = DirectCast(request.GetResponse(), HttpWebResponse)
            status = CStr(CInt(response.StatusCode)) & " " & response.StatusDescription
            body = ReadStream(response)
        Catch ex As WebException
            Dim errorResponse As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)

            If errorResponse IsNot Nothing Then
                status = CStr(CInt(errorResponse.StatusCode)) & " " & errorResponse.StatusDescription
                body = ReadStream(errorResponse)
            Else
                status = CStr(ex.Status)
            End If

            If String.IsNullOrEmpty(body) Then
                Throw New ApplicationException("No answer from the server (" & status & "): " & ex.Message, ex)
            End If
        Finally
            If response IsNot Nothing Then
                response.Close()
            End If
        End Try

        Dim trimmed As String = body.Trim()

        If Not trimmed.StartsWith("{", StringComparison.Ordinal) Then
            Dim snippet As String = If(trimmed.Length > 200, trimmed.Substring(0, 200) & "...", trimmed)

            Throw New ApplicationException(
                "The server did not answer with JSON (HTTP " & status & ")." & Environment.NewLine &
                "Is the PHP API uploaded? Expected: " & ResolveBaseUrl() & Environment.NewLine &
                "If the URL is right, create the folder in hPanel and upload api.zip." & Environment.NewLine &
                "Server said: " & snippet)
        End If

        Return body
    End Function

    Private Shared Function ReadStream(ByVal response As HttpWebResponse) As String
        Dim responseStream As Stream = response.GetResponseStream()

        If responseStream Is Nothing Then
            Return String.Empty
        End If

        Using responseStream
            Using reader As New StreamReader(responseStream, Encoding.UTF8)
                Return reader.ReadToEnd()
            End Using
        End Using
    End Function

End Class
