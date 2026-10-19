Imports System.IO
Imports System.Data.SqlClient
Public Class SubFrmColOrderCommon
    Public ColOrderFormChk As String
    Private Sub SubFrmTokRptColOrder_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        CbType.Items.Clear()

        If VistawinFunctions.ProjectTypechk = "INVEN" Then
            If ColOrderFormChk = "" Then
                CbType.Items.Add("STOCK")
                CbType.Items.Add("MULTI STOCK")
                CbType.Items.Add("-------------")
                CbType.Items.Add("STOCK REPORT")
                CbType.Items.Add("CUSTOMER REPORT")
            ElseIf ColOrderFormChk = "SALES ENTRY" Then
                CbType.Items.Add("SALES")
                CbType.Items.Add("SALES-PAY")
            End If

        ElseIf VistawinFunctions.ProjectTypechk = "CLINIC" Then
            If ColOrderFormChk = "TOKEN" Then
                CbType.Items.Add("TOKEN")
                CbType.Items.Add("TOKEN REPORT")
            ElseIf ColOrderFormChk = "STOCK REPORT" Then
                CbType.Items.Add("STOCK")
                CbType.Items.Add("MULTI STOCK")
                CbType.Items.Add("-------------")
                CbType.Items.Add("STOCK REPORT")
            End If
        End If
            CbType.SelectedIndex = 0

    End Sub
    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click
        Select Case CbType.Text.Trim().ToUpperInvariant()
            Case "STOCK", "MULTI STOCK", "TOKEN", "SALES", "SALES-PAY"
                Me.EntryColumnOrder()
            Case "CUSTOMER REPORT", "TOKEN REPORT", "STOCK REPORT"
                Me.ReportColumnOrder()
        End Select
    End Sub
    Private Sub ReportColumnOrder()
        Try
            Dim dt As New DataTable

            Dim sql As String = ""
            If CbType.Text = "CUSTOMER REPORT" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc FROM tbl_DgvSubRpt_Cus WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "TOKEN REPORT" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc FROM tbl_DgvSubRpt_Token WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "STOCK REPORT" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_ColWidth_vc FROM tbl_DgvSubRpt_Stock WHERE ISNULL(BD_StockDtl_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            End If
            Using da As New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
                da.Fill(dt)
            End Using

            DGVOrder.DataSource = dt

            DGVOrder.AllowUserToAddRows = False
            DGVOrder.AllowUserToDeleteRows = False
            DGVOrder.MultiSelect = False
            DGVOrder.SelectionMode = DataGridViewSelectionMode.CellSelect

            If DGVOrder.Columns.Contains("BD_RecordNo_i") Then
                DGVOrder.Columns("BD_RecordNo_i").Visible = False
            End If

            DGVOrder.Columns("BD_ColumnName_vc").HeaderText = "Column Name"
            DGVOrder.Columns("BD_AliasName_vc").HeaderText = "Display Name"
            DGVOrder.Columns("BD_ColWidth_vc").HeaderText = "Width"
            'DGVOrder.Columns("BD_Datatype_vc").HeaderText = "Data Type"
            'DGVOrder.Columns("BD_ColAlign_vc").HeaderText = "Alignment"
            DGVOrder.Columns("BD_ColOrderby_i").HeaderText = "Order"

            DGVOrder.Columns("BD_ColumnName_vc").ReadOnly = True
            DGVOrder.Columns("BD_AliasName_vc").ReadOnly = True
            DGVOrder.Columns("BD_ColOrderby_i").ReadOnly = True

            DGVOrder.Columns("BD_ColumnName_vc").DefaultCellStyle.BackColor = Color.LightGray
            DGVOrder.Columns("BD_AliasName_vc").DefaultCellStyle.BackColor = Color.LightGray
            DGVOrder.Columns("BD_ColOrderby_i").DefaultCellStyle.BackColor = Color.LightGray

            DGVOrder.Columns("BD_ColumnName_vc").Width = 180
            DGVOrder.Columns("BD_AliasName_vc").Width = 180
            DGVOrder.Columns("BD_ColOrderby_i").Width = 70

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub EntryColumnOrder()
        Try
            Dim dt As New DataTable

            Dim sql As String = ""
            If CbType.Text = "STOCK" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_Width_i FROM tbl_DgvEntry_Stock WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "MULTI STOCK" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_Width_i FROM tbl_DgvEntry_MultiStock WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "TOKEN" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_Width_i FROM tbl_DgvEntry_Token WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "SALES" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_Width_i FROM tbl_DgvEntry_Sal WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            ElseIf CbType.Text = "SALES-PAY" Then
                sql = "SELECT BD_RecordNo_i, BD_ColOrderby_i,BD_ColumnName_vc,BD_AliasName_vc,BD_Width_i FROM tbl_DgvEntry_SalPayDtl WHERE ISNULL(BD_ColShow_vc,'NO')='YES' ORDER BY BD_ColOrderby_i,BD_RecordNo_i"
            End If
            Using da As New SqlDataAdapter(sql, VistawinFunctions.ServerConnection)
                da.Fill(dt)
            End Using

            DGVOrder.DataSource = dt

            DGVOrder.AllowUserToAddRows = False
            DGVOrder.AllowUserToDeleteRows = False
            DGVOrder.MultiSelect = False
            DGVOrder.SelectionMode = DataGridViewSelectionMode.CellSelect

            If DGVOrder.Columns.Contains("BD_RecordNo_i") Then
                DGVOrder.Columns("BD_RecordNo_i").Visible = False
            End If

            DGVOrder.Columns("BD_ColumnName_vc").HeaderText = "Column Name"
            DGVOrder.Columns("BD_AliasName_vc").HeaderText = "Display Name"
            DGVOrder.Columns("BD_Width_i").HeaderText = "Width"
            'DGVOrder.Columns("BD_Datatype_vc").HeaderText = "Data Type"
            'DGVOrder.Columns("BD_ColAlign_vc").HeaderText = "Alignment"
            DGVOrder.Columns("BD_ColOrderby_i").HeaderText = "Order"

            DGVOrder.Columns("BD_ColumnName_vc").ReadOnly = True
            DGVOrder.Columns("BD_ColOrderby_i").ReadOnly = True

            DGVOrder.Columns("BD_ColumnName_vc").DefaultCellStyle.BackColor = Color.LightGray
            DGVOrder.Columns("BD_ColOrderby_i").DefaultCellStyle.BackColor = Color.LightGray

            DGVOrder.Columns("BD_ColumnName_vc").Width = 180
            DGVOrder.Columns("BD_AliasName_vc").Width = 180
            DGVOrder.Columns("BD_ColOrderby_i").Width = 70

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        Select Case CbType.Text.Trim().ToUpperInvariant()
            Case "STOCK", "MULTI STOCK", "TOKEN", "SALES", "SALES-PAY"
                Me.EntrySaveDetail()
            Case "CUSTOMER REPORT", "TOKEN REPORT", "STOCK REPORT"
                Me.ReportSaveDetail()
        End Select
    End Sub
    Private Sub ReportSaveDetail()
        Try
            DGVOrder.EndEdit()

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Dim Tran As SqlTransaction = VistawinFunctions.ServerConnection.BeginTransaction()

            Try
                Dim OrderNo As Integer = 1
                For Each row As DataGridViewRow In DGVOrder.Rows
                    If row.IsNewRow Then Continue For

                    Dim RecordNo As Integer = Convert.ToInt32(row.Cells("BD_RecordNo_i").Value)
                    Dim AliasName As String = ""
                    If row.Cells("BD_AliasName_vc").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("BD_AliasName_vc").Value) Then
                        AliasName = Trim(row.Cells("BD_AliasName_vc").Value.ToString())
                    End If
                    Dim ColWidth As Integer = Convert.ToInt32(row.Cells("BD_ColWidth_vc").Value)
                    Dim sql As String = ""
                    If CbType.Text = "CUSTOMER REPORT" Then
                        sql = "UPDATE tbl_DgvSubRpt_Cus SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_ColWidth_vc=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "TOKEN REPORT" Then
                        sql = "UPDATE tbl_DgvSubRpt_Token SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_ColWidth_vc=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "STOCK REPORT" Then
                        sql = "UPDATE tbl_DgvSubRpt_Stock SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_ColWidth_vc=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    End If


                    Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, Tran)
                        cmd.Parameters.AddWithValue("@OrderNo", OrderNo)
                        cmd.Parameters.AddWithValue("@AliasName", AliasName)
                        cmd.Parameters.AddWithValue("@ColWidth", ColWidth)
                        cmd.Parameters.AddWithValue("@RecordNo", RecordNo)
                        cmd.ExecuteNonQuery()
                    End Using
                    OrderNo += 1
                Next

                Tran.Commit()
                MessageBox.Show("Updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ReportColumnOrder()

            Catch ex As Exception
                Tran.Rollback()
                Throw
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub EntrySaveDetail()
        Try
            DGVOrder.EndEdit()

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Dim Tran As SqlTransaction = VistawinFunctions.ServerConnection.BeginTransaction()

            Try
                Dim OrderNo As Integer = 1
                For Each row As DataGridViewRow In DGVOrder.Rows
                    If row.IsNewRow Then Continue For

                    Dim RecordNo As Integer = Convert.ToInt32(row.Cells("BD_RecordNo_i").Value)
                    Dim AliasName As String = ""
                    If row.Cells("BD_AliasName_vc").Value IsNot Nothing AndAlso Not IsDBNull(row.Cells("BD_AliasName_vc").Value) Then
                        AliasName = Trim(row.Cells("BD_AliasName_vc").Value.ToString())
                    End If
                    Dim ColWidth As Integer = Convert.ToInt32(row.Cells("BD_Width_i").Value)
                    Dim sql As String = ""
                    If CbType.Text = "STOCK" Then
                        sql = "UPDATE tbl_DgvEntry_Stock SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_Width_i=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "MULTI STOCK" Then
                        sql = "UPDATE tbl_DgvEntry_MultiStock SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_Width_i=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "TOKEN" Then
                        sql = "UPDATE tbl_DgvEntry_Token SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_Width_i=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "SALES-PAY" Then
                        sql = "UPDATE tbl_DgvEntry_SalPayDtl SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_Width_i=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    ElseIf CbType.Text = "SALES" Then
                        sql = "UPDATE tbl_DgvEntry_Sal SET BD_ColOrderby_i=@OrderNo, BD_AliasName_vc=@AliasName,BD_Width_i=@ColWidth WHERE BD_RecordNo_i=@RecordNo"
                    End If


                    Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, Tran)
                        cmd.Parameters.AddWithValue("@OrderNo", OrderNo)
                        cmd.Parameters.AddWithValue("@AliasName", AliasName)
                        cmd.Parameters.AddWithValue("@ColWidth", ColWidth)
                        cmd.Parameters.AddWithValue("@RecordNo", RecordNo)
                        cmd.ExecuteNonQuery()
                    End Using
                    OrderNo += 1
                Next

                Tran.Commit()
                MessageBox.Show("Updated successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
                EntryColumnOrder()
            Catch ex As Exception
                Tran.Rollback()
                Throw
            End Try

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub SwapRows(ByVal Row1 As Integer, ByVal Row2 As Integer)
        Try
            If Row1 < 0 OrElse Row2 < 0 Then Exit Sub
            If Row1 >= DGVOrder.Rows.Count OrElse Row2 >= DGVOrder.Rows.Count Then Exit Sub

            For Each col As DataGridViewColumn In DGVOrder.Columns
                If col.Name = "BD_ColOrderby_i" Then Continue For

                Dim Temp As Object = DGVOrder.Rows(Row1).Cells(col.Name).Value
                DGVOrder.Rows(Row1).Cells(col.Name).Value = DGVOrder.Rows(Row2).Cells(col.Name).Value
                DGVOrder.Rows(Row2).Cells(col.Name).Value = Temp
            Next

            Dim i As Integer
            For i = 0 To DGVOrder.Rows.Count - 1
                If Not DGVOrder.Rows(i).IsNewRow Then
                    DGVOrder.Rows(i).Cells("BD_ColOrderby_i").Value = i + 1
                End If
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub
    Private Sub BtnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUp.Click
        If DGVOrder.CurrentRow Is Nothing Then Exit Sub
        Dim Index As Integer = DGVOrder.CurrentRow.Index
        If Index <= 0 Then Exit Sub
        SwapRows(Index, Index - 1)
        DGVOrder.ClearSelection()
        DGVOrder.Rows(Index - 1).Selected = True
        DGVOrder.CurrentCell = DGVOrder.Rows(Index - 1).Cells("BD_AliasName_vc")
    End Sub
    Private Sub BtnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDown.Click

        If DGVOrder.CurrentRow Is Nothing Then Exit Sub
        Dim Index As Integer = DGVOrder.CurrentRow.Index

        If Index >= DGVOrder.Rows.Count - 1 Then Exit Sub
        SwapRows(Index, Index + 1)

        DGVOrder.ClearSelection()

        DGVOrder.Rows(Index + 1).Selected = True
        DGVOrder.CurrentCell = DGVOrder.Rows(Index + 1).Cells("BD_AliasName_vc")
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
End Class