Imports System
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

''' <summary>
''' Splits a SQL script into individual batches using the GO batch separator.
''' GO is detected only when it appears alone on a line (case-insensitive).
''' Compatible with .NET Framework 3.5 / VB.NET / Visual Studio 2010.
''' </summary>
Public Class VSqlBatchSplitter

    ''' <summary>
    ''' Splits the given SQL script text into batches separated by GO statements.
    ''' Returns only non-empty batches.
    ''' </summary>
    ''' <param name="sqlScript">The full text content of a .sql file.</param>
    ''' <returns>An array of SQL batch strings.</returns>
    Public Shared Function Split(ByVal sqlScript As String) As String()
        Dim batches As New List(Of String)()

        If String.IsNullOrEmpty(sqlScript) Then
            Return batches.ToArray()
        End If

        ' Split on lines that contain only GO (with optional surrounding whitespace),
        ' case-insensitive, multiline mode so ^ and $ match line boundaries.
        Dim pattern As String = "^\s*GO\s*$"
        Dim regexOptions As RegexOptions = RegexOptions.Multiline Or RegexOptions.IgnoreCase

        Dim parts As String() = Regex.Split(sqlScript, pattern, regexOptions)

        For Each part As String In parts
            Dim trimmed As String = part.Trim()
            If Not String.IsNullOrEmpty(trimmed) Then
                batches.Add(trimmed)
            End If
        Next

        Return batches.ToArray()
    End Function

End Class
