Imports System.IO
Imports System.Data.SqlClient

Public Class RptWhatsappReport
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Adapter As New SqlDataAdapter
    Dim Dataset, Dataset1 As New WinDataSet
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Cmd1, command, Cmd2 As New SqlCommand
    Dim Execute As Boolean
    Dim Row As Integer
    Dim FromBool, ToBool As Boolean
    Private Sub RptWhatsappReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MonthCalendar1.Hide()
        txtFromDate.Text = "01/" + VistawinFunctions.TodayDate.Month.ToString + "/" + VistawinFunctions.TodayDate.Year.ToString
        txtToDate.Text = Format(CDate(VistawinFunctions.TodayDate), "dd/MM/yyyy")

    End Sub
    Private Sub MonthCalendar1_DateSelected(ByVal sender As Object, ByVal e As System.Windows.Forms.DateRangeEventArgs) Handles MonthCalendar1.DateSelected
        If FromBool = True Then
            If Format(CDate(txtToDate.Text), "yyyy/MM/dd") < Format(CDate(MonthCalendar1.SelectionRange.Start), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtFromDate.Text = Format(CDate(MonthCalendar1.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar1.Hide()
        ElseIf ToBool = True Then
            If Format(CDate(MonthCalendar1.SelectionRange.Start), "yyyy/MM/dd") < Format(CDate(txtFromDate.Text), "yyyy/MM/dd") Then
                MsgBox("Please,Check the date")
                Exit Sub
            End If
            txtToDate.Text = Format(CDate(MonthCalendar1.SelectionRange.Start), "dd/MM/yyyy")
            MonthCalendar1.Hide()
        End If
    End Sub
    Private Sub btnFrom4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrom4.Click
        FromBool = True
        ToBool = False
        MonthCalendar1.Show()
        MonthCalendar1.BringToFront()
        MonthCalendar1.Location = New System.Drawing.Point(txtFromDate.Location.X, txtFromDate.Location.Y + 30)
    End Sub

    Private Sub btnTo4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTo4.Click
        ToBool = True
        FromBool = False
        MonthCalendar1.Show()
        MonthCalendar1.BringToFront()
        MonthCalendar1.Location = New System.Drawing.Point(txtToDate.Location.X, txtToDate.Location.Y + 30)
    End Sub
    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click

        Me.SendRpt()
    End Sub
    Private Sub SendRpt()
        Try
            Ds = New DataSet
            Dim Sql As String = ""
            Sql = "SELECT Wh_SendDate_d AS 'SendDate', Wh_MsgType_vc AS 'MsgType', Wh_MobileNo_vc AS 'MobileNo', Wh_Message_vc AS 'WhMessage', Wh_FileType_vc AS 'FileType', Wh_FilePath_vc AS 'FilePath', Wh_SendStatus_vc AS 'SendStatus' FROM tbl_WhatsappRpt WHERE CONVERT(DATE,Wh_SendDate_d) BETWEEN @FromDate AND @ToDate"

            Using Cmd As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                Cmd.Parameters.Add("@FromDate", SqlDbType.Date).Value = DateTime.Parse(txtFromDate.Text.Trim()).Date
                Cmd.Parameters.Add("@ToDate", SqlDbType.Date).Value = DateTime.Parse(txtToDate.Text.Trim()).Date
                Adapter = New SqlDataAdapter(Cmd)
                Adapter.Fill(Ds, "FillData")
            End Using

            DgvOutstanding.Rows.Clear()
            If Ds.Tables("FillData").Rows.Count = 0 Then
                MsgBox("No Data")
                Exit Sub
            End If

            DgvOutstanding.SuspendLayout()

            Dim Row As Integer = 0
            For Each dr As DataRow In Ds.Tables("FillData").Rows

                DgvOutstanding.Rows.Add()
                With DgvOutstanding.Rows(Row)
                    .Cells("SNo").Value = Row + 1
                    'If Not IsDBNull(dr("SendDate")) Then
                    '    .Cells("SendDate").Value = CDate(dr("SendDate")).ToString("dd/MM/yyyy")
                    'Else
                    '    .Cells("SendDate").Value = ""
                    'End If
                    .Cells("SendDate").Value = dr("SendDate").ToString()
                    .Cells("MsgType").Value = dr("MsgType").ToString()
                    .Cells("MobileNo").Value = dr("MobileNo").ToString()
                    .Cells("WhMessage").Value = dr("WhMessage").ToString()
                    .Cells("FileType").Value = dr("FileType").ToString()
                    .Cells("FilePath").Value = dr("FilePath").ToString()
                    .Cells("SendStatus").Value = dr("SendStatus").ToString()
                End With
                Row += 1
            Next
            DgvOutstanding.ResumeLayout()
        Catch ex As Exception
            DgvOutstanding.ResumeLayout()
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
End Class