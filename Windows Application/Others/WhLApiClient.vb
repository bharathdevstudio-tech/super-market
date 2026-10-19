Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web.Script.Serialization
Imports WindowsApplication.Utils

Namespace Api

    Public Class WhatsAppApiClient

        Private _baseUrl As String
        Private _serializer As JavaScriptSerializer

        Public Sub New(ByVal baseUrl As String)
            If String.IsNullOrEmpty(baseUrl) Then
                _baseUrl = "http://localhost:8080/api/"
                '_baseUrl = "https://orangered-monkey-474827.hostingersite.com/"
            Else
                _baseUrl = baseUrl.TrimEnd("/") & "/"
            End If
            _serializer = New JavaScriptSerializer()
            _serializer.MaxJsonLength = Integer.MaxValue
        End Sub

        Public Property BaseUrl As String
            Get
                Return _baseUrl
            End Get
            Set(ByVal value As String)
                If String.IsNullOrEmpty(value) Then
                    _baseUrl = "http://localhost:8080/api/"
                    '_baseUrl = "https://orangered-monkey-474827.hostingersite.com/"
                Else
                    _baseUrl = value.TrimEnd("/") & "/"
                End If
            End Set
        End Property

        Public Function GetStatus() As Models.ServerStatus
            Dim url As String = _baseUrl & "status"
            Dim responseString As String = ExecuteGetRequest(url)
            Dim status As Models.ServerStatus = _serializer.Deserialize(Of Models.ServerStatus)(responseString)
            Return status
        End Function

        Public Function GetQRCode() As String
            Dim status As Models.ServerStatus = GetStatus()
            If status IsNot Nothing AndAlso Not String.IsNullOrEmpty(status.qr) Then
                Return status.qr
            End If
            Return Nothing
        End Function

        Public Function SendDocument(ByVal filePath As String, _
                                      ByVal numbers As List(Of String), _
                                      ByVal message As String, _
                                      ByVal delay As Integer) As String
            Dim url As String = _baseUrl & "send"
            Dim responseString As String = ExecuteMultipartPost(url, filePath, numbers, message, delay)
            Return responseString
        End Function

        Public Function StopSending() As String
            Dim url As String = _baseUrl & "stop"
            Dim responseString As String = ExecutePostRequest(url, "{}")
            Return responseString
        End Function

        Public Function Logout() As String
            Dim url As String = _baseUrl & "logout"
            Dim responseString As String = ExecutePostRequest(url, "{}")
            Return responseString
        End Function

        Private Function ExecuteGetRequest(ByVal url As String) As String

            Dim request As HttpWebRequest = _
                CType(WebRequest.Create(url), HttpWebRequest)

            request.Method = "GET"
            request.ContentType = "application/json"

            request.Timeout = 1000
            request.ReadWriteTimeout = 1000

            request.Accept = "application/json"
            request.KeepAlive = False

            If url.ToLower().Contains("localhost") OrElse _
               url.Contains("127.0.0.1") Then

                request.Proxy = Nothing

            End If

            Using response As HttpWebResponse = _
                CType(request.GetResponse(), HttpWebResponse)

                Using reader As New StreamReader( _
                    response.GetResponseStream(), _
                    Encoding.UTF8)

                    Return reader.ReadToEnd()

                End Using

            End Using

        End Function
        Private Function ExecutePostRequest(ByVal url As String, ByVal jsonBody As String) As String
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "application/json"
            request.Timeout = 3000
            request.ReadWriteTimeout = 3000
            request.Proxy = Nothing
            request.Accept = "application/json"

            Dim bodyBytes As Byte() = Encoding.UTF8.GetBytes(jsonBody)
            request.ContentLength = bodyBytes.Length

            Using requestStream As Stream = request.GetRequestStream()
                requestStream.Write(bodyBytes, 0, bodyBytes.Length)
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function

        Private Function ExecuteMultipartPost(ByVal url As String, _
                                               ByVal filePath As String, _
                                               ByVal numbers As List(Of String), _
                                               ByVal message As String, _
                                               ByVal delay As Integer) As String
            Dim boundary As String = "----WebKitFormBoundary" & Guid.NewGuid().ToString("N")
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "POST"
            request.ContentType = "multipart/form-data; boundary=" & boundary
            request.Timeout = System.Threading.Timeout.Infinite
            request.Accept = "application/json"

            Using memStream As New MemoryStream()
                Dim encoding As Encoding = Encoding.UTF8

                If Not String.IsNullOrEmpty(filePath) AndAlso File.Exists(filePath) Then
                    Dim fileBytes As Byte() = FileUtils.ReadFileBytes(filePath)
                    Dim fileName As String = Path.GetFileName(filePath)
                    Dim contentType As String = FileUtils.GetContentType(filePath)

                    AppendLine(memStream, encoding, "--" & boundary)
                    AppendLine(memStream, encoding, "Content-Disposition: form-data; name=""document""; filename=""" & fileName & """")
                    AppendLine(memStream, encoding, "Content-Type: " & contentType)
                    AppendLine(memStream, encoding, "")
                    memStream.Write(fileBytes, 0, fileBytes.Length)
                    AppendLine(memStream, encoding, "")
                End If

                Dim numbersJson As String = _serializer.Serialize(numbers)
                AppendLine(memStream, encoding, "--" & boundary)
                AppendLine(memStream, encoding, "Content-Disposition: form-data; name=""numbers""")
                AppendLine(memStream, encoding, "")
                AppendLine(memStream, encoding, numbersJson)

                AppendLine(memStream, encoding, "--" & boundary)
                AppendLine(memStream, encoding, "Content-Disposition: form-data; name=""message""")
                AppendLine(memStream, encoding, "")
                AppendLine(memStream, encoding, If(message, ""))

                AppendLine(memStream, encoding, "--" & boundary)
                AppendLine(memStream, encoding, "Content-Disposition: form-data; name=""delay""")
                AppendLine(memStream, encoding, "")
                AppendLine(memStream, encoding, delay.ToString())

                AppendLine(memStream, encoding, "--" & boundary & "--")
                AppendLine(memStream, encoding, "")

                Dim bodyBytes As Byte() = memStream.ToArray()
                request.ContentLength = bodyBytes.Length

                Using requestStream As Stream = request.GetRequestStream()
                    requestStream.Write(bodyBytes, 0, bodyBytes.Length)
                End Using
            End Using

            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                Using reader As New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using
        End Function

        Private Sub AppendLine(ByVal stream As MemoryStream, ByVal encoding As Encoding, ByVal text As String)
            Dim bytes As Byte() = encoding.GetBytes(text & vbCrLf)
            stream.Write(bytes, 0, bytes.Length)
        End Sub

    End Class

End Namespace
