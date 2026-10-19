Imports System.Data
Imports System.Data.SqlClient
Module VAmtWord

    Public RecentForms As New List(Of String)

    Public MainListBox As ListBox

    Public Sub AddRecentForm(ByVal FormName As String)

        If RecentForms.Contains(FormName) Then
            RecentForms.Remove(FormName)
        End If

        RecentForms.Insert(0, FormName)

        If RecentForms.Count > 10 Then
            RecentForms.RemoveAt(10)
        End If

        If MainListBox IsNot Nothing Then

            MainListBox.Items.Clear()

            For Each item As String In RecentForms
                MainListBox.Items.Add(item)
            Next

        End If

    End Sub
    Public Function VistFormatQty(ByVal val As Decimal) As String
        If val = Decimal.Truncate(val) Then
            Return val.ToString("0")
        Else
            Return val.ToString("0.000")
        End If
    End Function
    Public Function AmountInWords(ByVal amt As Object) As Object

        Dim Result As String = ""

        Dim FIGURE As Object
        'Dim LENFIG As Integer
        Dim FIGLEN As Integer
        Dim i As Integer
        Dim WORDs(19) As String
        Dim tens(9) As String
        WORDs(1) = "One"
        WORDs(2) = "Two"
        WORDs(3) = "Three"
        WORDs(4) = "Four"
        WORDs(5) = "Five"
        WORDs(6) = "Six"
        WORDs(7) = "Seven"
        WORDs(8) = "Eight"
        WORDs(9) = "Nine"
        WORDs(10) = "Ten"
        WORDs(11) = "Eleven"
        WORDs(12) = "Twelve"
        WORDs(13) = "Thirteen"
        WORDs(14) = "Fourteen"
        WORDs(15) = "Fifteen"
        WORDs(16) = "Sixteen"
        WORDs(17) = "Seventeen"
        WORDs(18) = "Eighteen"
        WORDs(19) = "Nineteen"
        tens(2) = "Twenty"
        tens(3) = "Thirty"
        tens(4) = "Fourty"
        tens(5) = "Fifty"
        tens(6) = "Sixty"
        tens(7) = "Seventy"
        tens(8) = "Eighty"
        tens(9) = "Ninety"
        FIGURE = amt
        FIGURE = Format(FIGURE, "FIXED")
        FIGLEN = Len(FIGURE)
        If FIGLEN < 12 Then
            FIGURE = Space(12 - FIGLEN) & FIGURE
        End If
        For i = 1 To 3
            If Val(Microsoft.VisualBasic.Left(FIGURE, 2)) < 20 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
                Result = Result & WORDs(Val(Microsoft.VisualBasic.Left(FIGURE, 2)))
            ElseIf Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 19 Then
                Result = Result & tens(Val(Microsoft.VisualBasic.Left(FIGURE, 1)))
                Result = Result & WORDs(Val(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(FIGURE, 2), 1)))
            End If
            If i = 1 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
                Result = Result & " Crore "
            ElseIf i = 2 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
                Result = Result & " Lakh "
            ElseIf i = 3 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
                Result = Result & " Thousand "
            End If
            FIGURE = Mid(FIGURE, 3)
        Next i
        If Val(Microsoft.VisualBasic.Left(FIGURE, 1)) > 0 Then
            Result = Result & WORDs(Val(Microsoft.VisualBasic.Left(FIGURE, 1))) + " Hundred "
        End If
        FIGURE = Mid(FIGURE, 2)
        If Val(Microsoft.VisualBasic.Left(FIGURE, 2)) < 20 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
            Result = Result & WORDs(Val(Microsoft.VisualBasic.Left(FIGURE, 2)))
        ElseIf Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 19 Then
            Result = Result & tens(Val(Microsoft.VisualBasic.Left(FIGURE, 1)))
            Result = Result & WORDs(Val(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(FIGURE, 2), 1)))
        End If
        FIGURE = Mid(FIGURE, 4)
        If Val(Microsoft.VisualBasic.Left(FIGURE, 9)) > 1 Then
            Result = Result & " Rupees "
        ElseIf Val(Microsoft.VisualBasic.Left(FIGURE, 9)) = 1 Then
            Result = Result & " Rupee "
        End If
        If Val(FIGURE) > 0 Then
            Result = Result & " And "
            If Val(Microsoft.VisualBasic.Left(FIGURE, 2)) < 20 And Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 0 Then
                Result = Result & WORDs(Val(Microsoft.VisualBasic.Left(FIGURE, 2)))
            ElseIf Val(Microsoft.VisualBasic.Left(FIGURE, 2)) > 19 Then
                Result = Result & tens(Val(Microsoft.VisualBasic.Left(FIGURE, 1)))
                Result = Result & WORDs(Val(Microsoft.VisualBasic.Right(Microsoft.VisualBasic.Left(FIGURE, 2), 1)))
            End If
            Result = Result & " Paise "
        End If
        FIGURE = amt
        FIGURE = Format(FIGURE, "FIXED")
        If Val(FIGURE) > 0 Then
            'Result = Result & " Only "
            Return Result & " Only"
        Else
            Return "Null"
        End If
    End Function
    Public Sub ExpiryUpdate()

        Dim currYear As Integer = Year(CDate(VistawinFunctions.TodayDate))
        Dim currMonth As Integer = Month(CDate(VistawinFunctions.TodayDate))

        Using cmd As New SqlCommand("", VistawinFunctions.ServerConnection)

            cmd.CommandText = "UPDATE tbl_MultiStock SET St_ExpStatus_vc='Expired',St_show_vc='NO' WHERE St_EYear_vc < @Year"
            cmd.Parameters.AddWithValue("@Year", currYear)
            cmd.ExecuteNonQuery()

            cmd.CommandText = "UPDATE tbl_MultiStock SET St_ExpStatus_vc='Expired',St_show_vc='NO' WHERE St_EYear_vc = @Year AND St_EMonth_i < @Month"
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@Year", currYear)
            cmd.Parameters.AddWithValue("@Month", currMonth)
            cmd.ExecuteNonQuery()

        End Using

    End Sub
    


End Module