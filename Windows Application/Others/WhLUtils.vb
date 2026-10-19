Imports System.Text.RegularExpressions

Namespace Utils

    Public Module PhoneNumberUtils

        ''' <summary>
        ''' Removes spaces, dashes, parentheses, plus signs, and other non-numeric characters.
        ''' </summary>
        Public Function CleanPhoneNumber(ByVal rawNumber As String) As String
            If String.IsNullOrEmpty(rawNumber) Then Return String.Empty
            Dim cleaned As String = rawNumber.Trim()
            cleaned = cleaned.Replace(" ", "")
            cleaned = cleaned.Replace("-", "")
            cleaned = cleaned.Replace("(", "")
            cleaned = cleaned.Replace(")", "")
            cleaned = cleaned.Replace("+", "")
            cleaned = cleaned.Replace(".", "")
            cleaned = cleaned.Replace(",", "")
            cleaned = cleaned.Replace("#", "")
            Return cleaned.Trim()
        End Function

        ''' <summary>
        ''' Returns a unique, cleaned list of phone numbers.
        ''' </summary>
        Public Function GetUniqueNumbers(ByVal rawText As String) As List(Of String)
            Dim result As New List(Of String)
            If String.IsNullOrEmpty(rawText) Then Return result

            Dim lines As String() = rawText.Split(New String() {vbCrLf, vbLf, vbCr}, StringSplitOptions.RemoveEmptyEntries)

            For Each line As String In lines
                Dim cleaned As String = CleanPhoneNumber(line)
                If cleaned.Length > 0 AndAlso Not result.Contains(cleaned) Then
                    result.Add(cleaned)
                End If
            Next

            Return result
        End Function

        ''' <summary>
        ''' Validates a phone number (must be numeric and at least 7 digits).
        ''' </summary>
        Public Function IsValidPhoneNumber(ByVal number As String) As Boolean
            If String.IsNullOrEmpty(number) Then Return False
            If number.Length < 7 Then Return False
            Return Regex.IsMatch(number, "^\d+$")
        End Function

        ''' <summary>
        ''' Returns only valid numbers from a list.
        ''' </summary>
        Public Function FilterValidNumbers(ByVal numbers As List(Of String)) As List(Of String)
            Dim valid As New List(Of String)
            For Each num As String In numbers
                If IsValidPhoneNumber(num) Then
                    valid.Add(num)
                End If
            Next
            Return valid
        End Function

    End Module

    Public Module FileUtils

        ''' <summary>
        ''' Reads all bytes from a file.
        ''' </summary>
        Public Function ReadFileBytes(ByVal filePath As String) As Byte()
            If Not System.IO.File.Exists(filePath) Then
                Throw New System.IO.FileNotFoundException("File not found: " & filePath)
            End If
            Return System.IO.File.ReadAllBytes(filePath)
        End Function

        ''' <summary>
        ''' Gets the MIME content type based on file extension.
        ''' </summary>
        Public Function GetContentType(ByVal filePath As String) As String
            Dim ext As String = System.IO.Path.GetExtension(filePath).ToLower()
            Select Case ext
                Case ".pdf"
                    Return "application/pdf"
                Case ".doc"
                    Return "application/msword"
                Case ".docx"
                    Return "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                Case ".xls"
                    Return "application/vnd.ms-excel"
                Case ".xlsx"
                    Return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                Case ".txt"
                    Return "text/plain"
                Case ".jpg", ".jpeg"
                    Return "image/jpeg"
                Case ".png"
                    Return "image/png"
                Case ".zip"
                    Return "application/zip"
                Case Else
                    Return "application/octet-stream"
            End Select
        End Function

    End Module

End Namespace
