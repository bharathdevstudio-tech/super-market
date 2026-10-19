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
Public Class FrmSettings
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat, AddOrEdit As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet
    Private Sub FrmSettingsNav_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Select Case VistawinFunctions.ProjectTypechk
            Case "LODGE"
                TabControl1.TabPages.Remove(TabPage1)
                TabControl1.TabPages.Remove(TabPage2)
            Case Else
                Me.SettingDtl()
                Me.PaperSizeList()
        End Select

        Me.BillSeriesDtl()
    End Sub

    Private Sub SettingDtl()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT Set_RecordNo_i AS 'RecordNo',Set_TblName_vc AS 'TblName',Set_Particular_vc 'Particular',Set_Condition_vc AS 'Condi' FROM tbl_Settings WHERE Set_SetShow_vc='YES' ORDER BY Set_RecordNo_i", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvSettings.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvSettings.Rows.Add()
            DgvSettings.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
            DgvSettings.Rows(RowIndex).Cells("RecordNo").Value = Dr.Item("RecordNo")
            DgvSettings.Rows(RowIndex).Cells("TblName").Value = Dr.Item("TblName")
            DgvSettings.Rows(RowIndex).Cells("Particular").Value = Dr.Item("Particular")

            If Dr.Item("Particular") = "ProName" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("Normal")
                    .Items.Add("Upper")
                    .Items.Add("Lower")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 0) = xPeople1

            ElseIf Dr.Item("Particular") = "RateCompare" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 1) = xPeople1

            ElseIf Dr.Item("Particular") = "CrLimitWarning" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 2) = xPeople1

            ElseIf Dr.Item("Particular") = "MinusStock Alert" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 3) = xPeople1

            ElseIf Dr.Item("Particular") = "DGV Margin Allow" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 4) = xPeople1

            ElseIf Dr.Item("Particular") = "DistributorRateShow" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 6) = xPeople1

            ElseIf Dr.Item("Particular") = "Inv Select" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("First")
                    .Items.Add("Second")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 7) = xPeople1


            ElseIf Dr.Item("Particular") = "DGV WHMargin Allow" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 8) = xPeople1

            ElseIf Dr.Item("Particular") = "PurRateWarn" Then
                Dim xPeople1 As New DataGridViewComboBoxCell
                With xPeople1
                    .Items.Add("YES")
                    .Items.Add("NO")
                    .Value = .Items(0)
                End With
                DgvSettings(4, 9) = xPeople1
            End If
            DgvSettings.Rows(RowIndex).Cells("Condi").Value = Dr.Item("Condi")
            RowIndex += 1
        Next

    End Sub

    Private Sub DgvSettings_CellFormatting(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellFormattingEventArgs) Handles DgvSettings.CellFormatting
        ''If e.ColumnIndex = 4 And e.RowIndex > -1 Then
        ''    If e.Value IsNot Nothing Then
        ''        e.Value = New String("#", e.Value.ToString().Length)
        ''    End If
        ''End If
    End Sub
    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click

        Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
        If MsgStr = 6 Then
            If BVistawinSolution1.Permission() = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Me.SettSave()
    End Sub

    Private Sub SettSave()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If DgvSettings.Rows.Count = 0 Then
                MsgBox("Add Anyone")
                DgvSettings.Focus()
                Exit Sub
            End If

            For Each Dr As DataGridViewRow In DgvSettings.Rows
                If Dr.Cells("TblName").Value <> Nothing Then
                    Using Cmd1 As New SqlCommand("SP_tbl_Settings", VistawinFunctions.ServerConnection, Trans)
                        Cmd1.CommandType = CommandType.StoredProcedure

                        Dim Set_RecordNo_i As New SqlParameter("@Set_RecordNo_i", SqlDbType.Int)
                        If IsDBNull(Dr.Cells("RecordNo").Value) Then
                            Set_RecordNo_i.Value = 0
                        ElseIf Dr.Cells("RecordNo").Value = Nothing Then
                            Set_RecordNo_i.Value = 0
                        ElseIf Dr.Cells("RecordNo").Value = 0 Then
                            Set_RecordNo_i.Value = 0
                        Else
                            Set_RecordNo_i.Value = Dr.Cells("RecordNo").Value
                        End If
                        Cmd1.Parameters.Add(Set_RecordNo_i)

                        Dim Set_TblName_vc As New SqlParameter("@Set_TblName_vc", SqlDbType.NVarChar)
                        If IsDBNull(Dr.Cells("TblName").Value) Then
                            Set_TblName_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("TblName").Value = Nothing Then
                            Set_TblName_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("TblName").Value = "" Then
                            Set_TblName_vc.Value = DBNull.Value
                        Else
                            Set_TblName_vc.Value = Dr.Cells("TblName").Value
                        End If
                        Cmd1.Parameters.Add(Set_TblName_vc)

                        Dim Set_Particular_vc As New SqlParameter("@Set_Particular_vc", SqlDbType.NVarChar)
                        If IsDBNull(Dr.Cells("Particular").Value) Then
                            Set_Particular_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("Particular").Value = Nothing Then
                            Set_Particular_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("Particular").Value = "" Then
                            Set_Particular_vc.Value = DBNull.Value
                        Else
                            Set_Particular_vc.Value = Dr.Cells("Particular").Value
                        End If
                        Cmd1.Parameters.Add(Set_Particular_vc)

                        Dim Set_Condition_vc As New SqlParameter("@Set_Condition_vc", SqlDbType.NVarChar)
                        If IsDBNull(Dr.Cells("Condi").Value) Then
                            Set_Condition_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("Condi").Value = Nothing Then
                            Set_Condition_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("Condi").Value = "" Then
                            Set_Condition_vc.Value = DBNull.Value
                        Else
                            Set_Condition_vc.Value = Dr.Cells("Condi").Value
                        End If
                        Cmd1.Parameters.Add(Set_Condition_vc)

                        Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                        Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_Settings WHERE Set_RecordNo_i='" & Dr.Cells("RecordNo").Value & "'", VistawinFunctions.ServerConnection, Trans)
                        If Cmd.ExecuteScalar = 0 Then
                            InputString.Value = "Add"
                        ElseIf Cmd.ExecuteScalar > 0 Then
                            InputString.Value = "Edit"
                        End If
                        Cmd1.Parameters.Add(InputString)
                        Execute = Cmd1.ExecuteNonQuery
                    End Using
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Saved Successfully")
                Me.SettingDtl()
            Else
                MsgBox("Insert Failed")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub
    Private Sub PaperSizeList()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT PS_RecordNo_i AS 'RecordNoP',Ps_FormName_vc AS 'FormNameP',PS_PaperSize_vc AS 'PaperSizeP', PS_OrderNo_i AS 'OrderNoP',PS_Show_vc AS 'ShowP' FROM tbl_PaperSize ORDER BY Ps_FormName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvPaperSize.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvPaperSize.Rows.Add()
            DgvPaperSize.Rows(RowIndex).Cells("SNoP").Value = RowIndex + 1
            DgvPaperSize.Rows(RowIndex).Cells("RecordNoP").Value = Dr.Item("RecordNoP")
            DgvPaperSize.Rows(RowIndex).Cells("FormNameP").Value = Dr.Item("FormNameP")
            DgvPaperSize.Rows(RowIndex).Cells("PaperSizeP").Value = Dr.Item("PaperSizeP")
            DgvPaperSize.Rows(RowIndex).Cells("OrderNoP").Value = Dr.Item("OrderNoP")
            DgvPaperSize.Rows(RowIndex).Cells("ShowP").Value = Dr.Item("ShowP")
            RowIndex += 1
        Next
    End Sub
    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click

        Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
        If MsgStr = 6 Then
            If BVistawinSolution1.Permission() = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Me.PaperSizeSave()
    End Sub
    Private Sub PaperSizeSave()

        Try
            If DgvPaperSize.Rows.Count = 0 Then
                MsgBox("Add Anyone")
                DgvPaperSize.Focus()
                Exit Sub
            End If

            Trans = VistawinFunctions.ServerConnection.BeginTransaction()
            Dim Saved As Boolean = False

            For Each Dr As DataGridViewRow In DgvPaperSize.Rows

                If Dr.IsNewRow Then Continue For

                If Dr.Cells("FormNameP").Value Is Nothing OrElse Trim(Dr.Cells("FormNameP").Value.ToString()) = "" Then
                    Continue For
                End If

                Using Cmd As New SqlCommand("SP_tbl_PaperSize", VistawinFunctions.ServerConnection, Trans)
                    Cmd.CommandType = CommandType.StoredProcedure

                    Cmd.Parameters.AddWithValue("@PS_RecordNo_i", If(Dr.Cells("RecordNoP").Value Is Nothing OrElse IsDBNull(Dr.Cells("RecordNoP").Value), 0, Val(Dr.Cells("RecordNoP").Value)))
                    Cmd.Parameters.AddWithValue("@PS_OrderNo_i", If(Dr.Cells("OrderNoP").Value Is Nothing OrElse IsDBNull(Dr.Cells("OrderNoP").Value), 1, Val(Dr.Cells("OrderNoP").Value)))
                    If Dr.Cells("ShowP").Value Is Nothing OrElse Trim(Dr.Cells("ShowP").Value.ToString()) = "" Then
                        Cmd.Parameters.AddWithValue("@PS_Show_vc", DBNull.Value)
                    Else
                        Cmd.Parameters.AddWithValue("@PS_Show_vc", Dr.Cells("ShowP").Value.ToString())
                    End If
                    Cmd.Parameters.AddWithValue("@InputString", "Edit")
                    Cmd.ExecuteNonQuery()
                    Saved = True
                End Using
            Next

            If Saved Then
                Trans.Commit()
                MsgBox("Saved Successfully", MsgBoxStyle.Information)
                PaperSizeList()
            Else
                Trans.Rollback()
                MsgBox("No Data Found To Save", MsgBoxStyle.Exclamation)
            End If

        Catch ex As Exception
            If Trans IsNot Nothing Then
                Trans.Rollback()
            End If
            MessageBox.Show(ex.Message)
        Finally
            If Trans IsNot Nothing Then
                Trans.Dispose()
            End If
        End Try

    End Sub
    Private Sub PaperSizeSaveold()
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            If DgvPaperSize.Rows.Count = 0 Then
                MsgBox("Add Anyone")
                DgvPaperSize.Focus()
                Exit Sub
            End If

            For Each Dr As DataGridViewRow In DgvPaperSize.Rows
                If Dr.Cells("FormNameP").Value <> Nothing Then
                    Using Cmd1 As New SqlCommand("SP_tbl_PaperSize", VistawinFunctions.ServerConnection, Trans)
                        Cmd1.CommandType = CommandType.StoredProcedure

                        Dim PS_RecordNo_i As New SqlParameter("@PS_RecordNo_i", SqlDbType.Int)
                        If IsDBNull(Dr.Cells("RecordNoP").Value) Then
                            PS_RecordNo_i.Value = 0
                        ElseIf Dr.Cells("RecordNoP").Value = Nothing Then
                            PS_RecordNo_i.Value = 0
                        ElseIf Dr.Cells("RecordNoP").Value = 0 Then
                            PS_RecordNo_i.Value = 0
                        Else
                            PS_RecordNo_i.Value = Dr.Cells("RecordNoP").Value
                        End If
                        Cmd1.Parameters.Add(PS_RecordNo_i)

                        Dim PS_OrderNo_i As New SqlParameter("@PS_OrderNo_i", SqlDbType.Int)
                        If IsDBNull(Dr.Cells("OrderNoP").Value) Then
                            PS_OrderNo_i.Value = 1
                        ElseIf Dr.Cells("OrderNoP").Value = Nothing Then
                            PS_OrderNo_i.Value = 1
                        Else
                            PS_OrderNo_i.Value = Dr.Cells("OrderNoP").Value
                        End If
                        Cmd1.Parameters.Add(PS_OrderNo_i)

                        Dim PS_Show_vc As New SqlParameter("@PS_Show_vc", SqlDbType.VarChar)
                        If IsDBNull(Dr.Cells("ShowP").Value) Then
                            PS_Show_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("ShowP").Value = Nothing Then
                            PS_Show_vc.Value = DBNull.Value
                        ElseIf Dr.Cells("ShowP").Value = "" Then
                            PS_Show_vc.Value = DBNull.Value
                        Else
                            PS_Show_vc.Value = Dr.Cells("ShowP").Value
                        End If
                        Cmd1.Parameters.Add(PS_Show_vc)

                        Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
                        InputString.Value = "Edit"
                        Cmd1.Parameters.Add(InputString)
                        Execute = Cmd1.ExecuteNonQuery
                    End Using
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Saved Successfully")
                Me.PaperSizeList()
            Else
                MsgBox("Insert Failed")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub

    Private Sub BtnClose2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose2.Click
        Me.Close()
    End Sub
    Private Sub BillSeriesDtl()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT B_RecordNo_i AS 'RecordNoB',B_AcYear_vc AS 'AcYearB',B_BillType_vc AS 'BillTypeB',B_BillSeries_vc AS 'BillSeriesB',B_StartNo_i AS 'StartNoB' FROM tbl_BillSeries ORDER BY B_BillType_vc,B_AcYear_vc,B_BillSeries_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvBillSeries.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvBillSeries.Rows.Add()
            DgvBillSeries.Rows(RowIndex).Cells("SNoB").Value = RowIndex + 1
            DgvBillSeries.Rows(RowIndex).Cells("RecordNoB").Value = Dr.Item("RecordNoB")
            DgvBillSeries.Rows(RowIndex).Cells("AcYearB").Value = Dr.Item("AcYearB")
            DgvBillSeries.Rows(RowIndex).Cells("BillTypeB").Value = Dr.Item("BillTypeB")
            DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").Value = Dr.Item("BillSeriesB")
            DgvBillSeries.Rows(RowIndex).Cells("StartNoB").Value = Dr.Item("StartNoB")

            If Dr.Item("BillTypeB") = "INVOICE" Or Dr.Item("BillTypeB") = "ESTIMATE" Or Dr.Item("BillTypeB") = "SALES RETURN" Then

                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_SalDetail WHERE Sa_SaveType_vc='" & Dr.Item("BillTypeB") & "' AND Sa_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            ElseIf Dr.Item("BillTypeB") = "PURCHASE RETURN" Then

                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_PurchDetail WHERE Sa_SaveType_vc='" & Dr.Item("BillTypeB") & "' AND Sa_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            ElseIf Dr.Item("BillTypeB") = "SAMPLE" Or Dr.Item("BillTypeB") = "WASTAGE" Or Dr.Item("BillTypeB") = "DAMAGE" Then

                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_WastageDtl WHERE Sa_SaveType_vc='" & Dr.Item("BillTypeB") & "' AND Sa_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            ElseIf Dr.Item("BillTypeB") = "TRANSPORT" Then

                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_TransDetail WHERE Sa_SaveType_vc='" & Dr.Item("BillTypeB") & "' AND Sa_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            ElseIf Dr.Item("BillTypeB") = "ADVANCE" Or Dr.Item("BillTypeB") = "RENT" Or Dr.Item("BillTypeB") = "REFUND" Then


                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_Lod_ReceiptDetail WHERE Rec_BillType_vc='" & Dr.Item("BillTypeB") & "' AND Rec_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            ElseIf Dr.Item("BillTypeB") = "CHECKOUT" Then


                Cmd = New SqlCommand("SELECT COUNT(*) FROM tbl_Lod_CheckOutDetail WHERE Chkt_AcYear_vc='" & Dr.Item("AcYearB") & "'", VistawinFunctions.ServerConnection)
                Cmd.ExecuteScalar()

                If Cmd.ExecuteScalar() > 0 Then
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.BackColor = Color.Green
                    DgvBillSeries.Rows(RowIndex).DefaultCellStyle.ForeColor = Color.White

                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = True
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = True
                Else
                    DgvBillSeries.Rows(RowIndex).Cells("BillSeriesB").ReadOnly = False
                    DgvBillSeries.Rows(RowIndex).Cells("StartNoB").ReadOnly = False
                End If

            End If

            RowIndex += 1
        Next
    End Sub

    Private Sub BtnAcSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAcSave.Click

        Dim MsgStr As DialogResult = MessageBox.Show("Are you sure ?", "Confirmation", MessageBoxButtons.YesNo)
        If MsgStr = 6 Then
            If BVistawinSolution1.Permission() = False Then
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Me.BillSeriesSave()
    End Sub
    Private Sub BillSeriesSave()

        Try
            If DgvBillSeries.Rows.Count = 0 Then
                MsgBox("Add Anyone")
                Exit Sub
            End If
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            For Each Dr As DataGridViewRow In DgvBillSeries.Rows
                If Dr.Cells("AcYearB").Value Is Nothing Then Continue For

                Using Cmd As New SqlCommand("SP_tbl_BillSeries", VistawinFunctions.ServerConnection, Trans)
                    Cmd.CommandType = CommandType.StoredProcedure
                    Cmd.Parameters.AddWithValue("@B_RecordNo_i", Val(Dr.Cells("RecordNoB").Value))
                    Cmd.Parameters.AddWithValue("@B_BillSeries_vc", Trim(Dr.Cells("BillSeriesB").Value & ""))
                    Cmd.Parameters.AddWithValue("@B_StartNo_i", Val(Dr.Cells("StartNoB").Value))
                    Cmd.Parameters.AddWithValue("@InputString", "Edit")
                    Cmd.ExecuteNonQuery()
                End Using
            Next
            Trans.Commit()
            MsgBox("Saved Successfully")
            BillSeriesDtl()

        Catch ex As Exception
            If Trans IsNot Nothing Then Trans.Rollback()
            MsgBox(ex.Message)
        Finally
            If Trans IsNot Nothing Then Trans.Dispose()
        End Try

    End Sub

    'Private Sub BillSeriesSaveold()
    '    Try
    '        Trans = VistawinFunctions.ServerConnection.BeginTransaction

    '        If DgvBillSeries.Rows.Count = 0 Then
    '            MsgBox("Add Anyone")
    '            DgvBillSeries.Focus()
    '            Exit Sub
    '        End If

    '        For Each Dr As DataGridViewRow In DgvBillSeries.Rows
    '            If Dr.Cells("AcYearB").Value <> Nothing Then
    '                Using Cmd1 As New SqlCommand("SP_tbl_BillSeries", VistawinFunctions.ServerConnection, Trans)
    '                    Cmd1.CommandType = CommandType.StoredProcedure

    '                    Dim B_RecordNo_i As New SqlParameter("@B_RecordNo_i", SqlDbType.Int)
    '                    If IsDBNull(Dr.Cells("RecordNoB").Value) Then
    '                        B_RecordNo_i.Value = 0
    '                    ElseIf Dr.Cells("RecordNoB").Value = Nothing Then
    '                        B_RecordNo_i.Value = 0
    '                    ElseIf Dr.Cells("RecordNoB").Value = 0 Then
    '                        B_RecordNo_i.Value = 0
    '                    Else
    '                        B_RecordNo_i.Value = Dr.Cells("RecordNoB").Value
    '                    End If
    '                    Cmd1.Parameters.Add(B_RecordNo_i)

    '                    Dim B_BillSeries_vc As New SqlParameter("@B_BillSeries_vc", SqlDbType.VarChar)
    '                    If IsDBNull(Dr.Cells("BillSeriesB").Value) Then
    '                        B_BillSeries_vc.Value = DBNull.Value
    '                    ElseIf Dr.Cells("BillSeriesB").Value = Nothing Then
    '                        B_BillSeries_vc.Value = DBNull.Value
    '                    ElseIf Dr.Cells("BillSeriesB").Value = "" Then
    '                        B_BillSeries_vc.Value = DBNull.Value
    '                    Else
    '                        B_BillSeries_vc.Value = Dr.Cells("BillSeriesB").Value
    '                    End If
    '                    Cmd1.Parameters.Add(B_BillSeries_vc)

    '                    Dim B_StartNo_i As New SqlParameter("@B_StartNo_i", SqlDbType.Int)
    '                    If IsDBNull(Dr.Cells("StartNoB").Value) Then
    '                        B_StartNo_i.Value = 1
    '                    ElseIf Dr.Cells("StartNoB").Value = Nothing Then
    '                        B_StartNo_i.Value = 1
    '                    ElseIf Dr.Cells("StartNoB").Value = 0 Then
    '                        B_StartNo_i.Value = 1
    '                    Else
    '                        B_StartNo_i.Value = Dr.Cells("StartNoB").Value
    '                    End If
    '                    Cmd1.Parameters.Add(B_StartNo_i)

    '                    Dim InputString As New SqlParameter("@InputString", SqlDbType.VarChar)
    '                    InputString.Value = "Edit"
    '                    Cmd1.Parameters.Add(InputString)
    '                    Execute = Cmd1.ExecuteNonQuery
    '                End Using
    '            End If
    '        Next
    '        If Execute = True Then
    '            Trans.Commit()
    '            MsgBox("Saved Successfully")
    '            Me.BillSeriesDtl()
    '        Else
    '            MsgBox("Insert Failed")
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '        Trans.Rollback()
    '    Finally
    '        Trans.Dispose()
    '    End Try
    'End Sub

    Private Sub BtnClose1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose1.Click
        Me.Close()
    End Sub
End Class