Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text

''' <summary>
''' Minimal JSON reader/writer. .NET 3.5 has no JSON library, so the small amount
''' of JSON the PHP API returns is parsed here.
''' Values are returned as: Dictionary(Of String, Object) for an object,
''' List(Of Object) for an array, String / Double / Boolean / Nothing for scalars.
''' </summary>
Public NotInheritable Class VCategoryMiniJson

    Private _text As String
    Private _pos As Integer

    Private Sub New()
    End Sub

    ' ---------------------------------------------------------------- writing

    ''' <summary>Returns the JSON representation (with quotes) of a value.</summary>
    Public Shared Function Quote(ByVal value As Object) As String
        Dim sb As New StringBuilder()
        AppendQuoted(sb, value)
        Return sb.ToString()
    End Function

    Private Shared Sub AppendQuoted(ByVal sb As StringBuilder, ByVal value As Object)
        If value Is Nothing OrElse TypeOf value Is DBNull Then
            sb.Append("null")
        ElseIf TypeOf value Is Boolean Then
            If DirectCast(value, Boolean) Then
                sb.Append("true")
            Else
                sb.Append("false")
            End If
        ElseIf TypeOf value Is DateTime Then
            sb.Append("""").Append(DirectCast(value, DateTime).ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)).Append("""")
        ElseIf TypeOf value Is String Then
            AppendEscaped(sb, DirectCast(value, String))
        ElseIf TypeOf value Is IFormattable Then
            sb.Append(DirectCast(value, IFormattable).ToString(Nothing, CultureInfo.InvariantCulture))
        Else
            AppendEscaped(sb, value.ToString())
        End If
    End Sub

    Private Shared Sub AppendEscaped(ByVal sb As StringBuilder, ByVal text As String)
        Dim quote As String = Chr(34)
        Dim index As Integer = 0

        sb.Append(quote)

        While index < text.Length
            ' AscW is signed, so keep the value inside 0..65535.
            Dim code As Integer = CInt(AscW(text(index)) And &HFFFF&)

            Select Case code
                Case 34
                    sb.Append("\").Append(quote)
                Case 92
                    sb.Append("\\")
                Case 47
                    sb.Append("\/")
                Case 8
                    sb.Append("\b")
                Case 12
                    sb.Append("\f")
                Case 10
                    sb.Append("\n")
                Case 13
                    sb.Append("\r")
                Case 9
                    sb.Append("\t")
                Case Else
                    If code < 32 Then
                        sb.Append("\u").Append(code.ToString("x4", CultureInfo.InvariantCulture))
                    Else
                        sb.Append(text(index))
                    End If
            End Select

            index += 1
        End While

        sb.Append(quote)
    End Sub

    ' ---------------------------------------------------------------- reading

    ''' <summary>Parses a JSON document.</summary>
    Public Shared Function Parse(ByVal json As String) As Object
        If json Is Nothing Then
            Throw New FormatException("Empty JSON response.")
        End If

        Dim parser As New VCategoryMiniJson()
        parser._text = json
        parser._pos = 0
        parser.SkipWhitespace()

        Dim value As Object = parser.ReadValue()
        parser.SkipWhitespace()

        If parser._pos < parser._text.Length Then
            Throw New FormatException("Unexpected character '" & parser._text(parser._pos) & "' at position " & parser._pos.ToString(CultureInfo.InvariantCulture) & ".")
        End If

        Return value
    End Function

    Private Sub SkipWhitespace()
        While _pos < _text.Length AndAlso Char.IsWhiteSpace(_text(_pos))
            _pos += 1
        End While
    End Sub

    Private Function Peek() As Char
        If _pos >= _text.Length Then
            Throw New FormatException("Unexpected end of JSON.")
        End If
        Return _text(_pos)
    End Function

    Private Function ReadValue() As Object
        Select Case Peek()
            Case "{"c
                Return ReadObject()
            Case "["c
                Return ReadArray()
            Case """"c
                Return ReadString()
            Case "t"c
                ExpectLiteral("true")
                Return True
            Case "f"c
                ExpectLiteral("false")
                Return False
            Case "n"c
                ExpectLiteral("null")
                Return Nothing
            Case Else
                Return ReadNumber()
        End Select
    End Function

    Private Sub ExpectLiteral(ByVal literal As String)
        If _pos + literal.Length > _text.Length OrElse
           String.CompareOrdinal(_text, _pos, literal, 0, literal.Length) <> 0 Then
            Throw New FormatException("Invalid JSON literal at position " & _pos.ToString(CultureInfo.InvariantCulture) & ".")
        End If
        _pos += literal.Length
    End Sub

    Private Function ReadObject() As Object
        Dim map As New Dictionary(Of String, Object)(StringComparer.Ordinal)
        _pos += 1
        SkipWhitespace()

        If Peek() = "}"c Then
            _pos += 1
            Return map
        End If

        While True
            SkipWhitespace()
            If Peek() <> """"c Then
                Throw New FormatException("Expected a property name at position " & _pos.ToString(CultureInfo.InvariantCulture) & ".")
            End If

            Dim key As String = ReadString()

            SkipWhitespace()
            If Peek() <> ":"c Then
                Throw New FormatException("Expected ':' at position " & _pos.ToString(CultureInfo.InvariantCulture) & ".")
            End If
            _pos += 1
            SkipWhitespace()

            map(key) = ReadValue()

            SkipWhitespace()
            Dim separator As Char = Peek()
            _pos += 1

            If separator = "}"c Then
                Return map
            End If
            If separator <> ","c Then
                Throw New FormatException("Expected ',' or '}' at position " & (_pos - 1).ToString(CultureInfo.InvariantCulture) & ".")
            End If
        End While

        Throw New FormatException("Unexpected end of JSON object.")
    End Function

    Private Function ReadArray() As Object
        Dim list As New List(Of Object)()
        _pos += 1
        SkipWhitespace()

        If Peek() = "]"c Then
            _pos += 1
            Return list
        End If

        While True
            SkipWhitespace()
            list.Add(ReadValue())
            SkipWhitespace()

            Dim separator As Char = Peek()
            _pos += 1

            If separator = "]"c Then
                Return list
            End If
            If separator <> ","c Then
                Throw New FormatException("Expected ',' or ']' at position " & (_pos - 1).ToString(CultureInfo.InvariantCulture) & ".")
            End If
        End While

        Throw New FormatException("Unexpected end of JSON array.")
    End Function

    Private Function ReadString() As String
        _pos += 1
        Dim sb As New StringBuilder()

        While True
            If _pos >= _text.Length Then
                Throw New FormatException("Unterminated string in JSON.")
            End If

            Dim ch As Char = _text(_pos)
            _pos += 1

            If ch = """"c Then
                Return sb.ToString()
            End If

            If ch <> "\"c Then
                sb.Append(ch)
                Continue While
            End If

            If _pos >= _text.Length Then
                Throw New FormatException("Unterminated escape sequence in JSON.")
            End If

            Dim esc As Char = _text(_pos)
            _pos += 1

            Select Case esc
                Case """"c
                    sb.Append(""""c)
                Case "\"c
                    sb.Append("\"c)
                Case "/"c
                    sb.Append("/"c)
                Case "b"c
                    sb.Append(ChrW(8))
                Case "f"c
                    sb.Append(ChrW(12))
                Case "n"c
                    sb.Append(ChrW(10))
                Case "r"c
                    sb.Append(ChrW(13))
                Case "t"c
                    sb.Append(ChrW(9))
                Case "u"c
                    If _pos + 4 > _text.Length Then
                        Throw New FormatException("Invalid \u escape in JSON.")
                    End If
                    sb.Append(ChrW(Convert.ToInt32(_text.Substring(_pos, 4), 16)))
                    _pos += 4
                Case Else
                    Throw New FormatException("Invalid escape sequence in JSON.")
            End Select
        End While

        Throw New FormatException("Unterminated string in JSON.")
    End Function

    Private Function ReadNumber() As Object
        Dim start As Integer = _pos

        While _pos < _text.Length AndAlso
              (Char.IsDigit(_text(_pos)) OrElse
               _text(_pos) = "-"c OrElse
               _text(_pos) = "+"c OrElse
               _text(_pos) = "."c OrElse
               _text(_pos) = "e"c OrElse
               _text(_pos) = "E"c)
            _pos += 1
        End While

        Dim raw As String = _text.Substring(start, _pos - start)
        Dim number As Double

        If Not Double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, number) Then
            Throw New FormatException("Invalid number '" & raw & "' in JSON.")
        End If

        Return number
    End Function

    ' -------------------------------------------------------------- accessors

    ''' <summary>Value of a property, or Nothing when it is missing.</summary>
    Public Shared Function GetProperty(ByVal container As Object, ByVal name As String) As Object
        Dim map As Dictionary(Of String, Object) = TryCast(container, Dictionary(Of String, Object))

        If map Is Nothing Then
            Return Nothing
        End If

        Dim result As Object = Nothing
        If map.TryGetValue(name, result) Then
            Return result
        End If

        Return Nothing
    End Function

    Public Shared Function GetString(ByVal container As Object, ByVal name As String) As String
        Return AsString(GetProperty(container, name))
    End Function

    Public Shared Function GetBool(ByVal container As Object, ByVal name As String) As Boolean
        Dim value As Object = GetProperty(container, name)

        If value Is Nothing Then
            Return False
        End If
        If TypeOf value Is Boolean Then
            Return DirectCast(value, Boolean)
        End If

        Dim text As String = AsString(value).ToLowerInvariant()
        Return text = "true" OrElse text = "1"
    End Function

    Public Shared Function GetArray(ByVal container As Object, ByVal name As String) As List(Of Object)
        Return TryCast(GetProperty(container, name), List(Of Object))
    End Function

    Public Shared Function AsString(ByVal value As Object) As String
        If value Is Nothing OrElse TypeOf value Is DBNull Then
            Return String.Empty
        End If
        If TypeOf value Is Boolean Then
            If DirectCast(value, Boolean) Then
                Return "true"
            End If
            Return "false"
        End If

        Dim formattable As IFormattable = TryCast(value, IFormattable)
        If formattable IsNot Nothing Then
            Return formattable.ToString(Nothing, CultureInfo.InvariantCulture)
        End If

        Return value.ToString()
    End Function

End Class
