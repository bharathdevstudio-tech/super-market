Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports CrystalDecisions.Web
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.CrystalReports
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime
Public Class SubFrmInstantQty
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Public ToStockCode As String
    Public ToStNumber As String
    Public ToProName As String
    Public ToCurStock As String

    Private Sub FrmInstantQty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtStCodeU.Text = ToStockCode
        txtStNumberU.Text = ToStNumber
        txtProNameU.Text = ToProName
        txtCurQty.Text = ToCurStock
        txtTotQty.Text = ToCurStock

        txtNewQty.Focus()
    End Sub
    Private Sub BtnStUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnStUpdate.Click
        Me.StUpdate()
    End Sub
    Private Sub StUpdate()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If txtNewQty.Text = String.Empty Then
                MsgBox("Enter Stock Qty")
                txtNewQty.Focus()
                Exit Sub
            End If

            Using Cmd1 As New SqlCommand("SP_tbl_StockQtyUpdate", VistawinFunctions.ServerConnection, Trans)
                Cmd1.CommandType = CommandType.StoredProcedure

                Dim St_StockID_n As New SqlParameter("@St_StockID_n", SqlDbType.Decimal)
                St_StockID_n.Value = txtStCodeU.Text
                Cmd1.Parameters.Add(St_StockID_n)

                Dim St_StNumber_i As New SqlParameter("@St_StNumber_i", SqlDbType.Decimal)
                St_StNumber_i.Value = txtStNumberU.Text
                Cmd1.Parameters.Add(St_StNumber_i)

                Dim St_CurrStock_i As New SqlParameter("@St_CurrStock_i", SqlDbType.Decimal)
                St_CurrStock_i.Value = txtNewQty.Text
                Cmd1.Parameters.Add(St_CurrStock_i)

                Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                InputString.Value = "Update"
                Cmd1.Parameters.Add(InputString)

                Execute = Cmd1.ExecuteNonQuery
            End Using

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
                Me.Close()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub txtNewQty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNewQty.KeyDown
        If e.KeyCode = Keys.Enter Then
            BtnStUpdate.PerformClick()
        End If
    End Sub
    Private Sub txtNewQty_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNewQty.TextChanged

        Dim CQty1, NQty1, TQty1 As Decimal

        If txtCurQty.Text = String.Empty Then
            CQty1 = 0
        Else
            CQty1 = txtCurQty.Text
        End If

        If txtNewQty.Text = String.Empty Then
            NQty1 = 0
        Else
            NQty1 = txtNewQty.Text
        End If

        If txtTotQty.Text = String.Empty Then
            TQty1 = 0
        Else
            TQty1 = txtTotQty.Text
        End If

        TQty1 = CQty1 + NQty1
        txtTotQty.Text = TQty1
    End Sub
    Private Sub BtnGrpStClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnGrpStClose.Click
        Me.Close()
    End Sub
End Class