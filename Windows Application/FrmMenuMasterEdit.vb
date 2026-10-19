Imports System.IO
Imports System.Data.SqlClient

Public Class FrmMenuMasterEdit
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat, AddOrEdit As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet
    Private Sub FrmMenuMasterEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GrpAdd.Hide()

        CbFormState.SelectedIndex = 0

    End Sub
    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click
        Me.MenuMaster()
    End Sub
    Private Sub MenuMaster()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("Select MenuID,MenuName, FormName,FormState, ParentID, MenuNameOrder, FormNameOrder From tbl_MenuMaster ORDER BY MenuID", VistawinFunctions.ServerConnection)

        Adapter.Fill(Ds, "FillData")
        RowIndex = 0
        DgvMenuMaster.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvMenuMaster.Rows.Add()
            DgvMenuMaster.Rows(RowIndex).Cells("SNo").Value = RowIndex + 1
            DgvMenuMaster.Rows(RowIndex).Cells("MenuID").Value = Dr.Item("MenuID")
            DgvMenuMaster.Rows(RowIndex).Cells("MenuName").Value = Dr.Item("MenuName")
            DgvMenuMaster.Rows(RowIndex).Cells("FormName").Value = Dr.Item("FormName")
            DgvMenuMaster.Rows(RowIndex).Cells("FormState").Value = Dr.Item("FormState")
            DgvMenuMaster.Rows(RowIndex).Cells("ParentID").Value = Dr.Item("ParentID")
            DgvMenuMaster.Rows(RowIndex).Cells("MenuNameOrder").Value = Dr.Item("MenuNameOrder")
            DgvMenuMaster.Rows(RowIndex).Cells("FormNameOrder").Value = Dr.Item("FormNameOrder")
            DgvMenuMaster.Rows(RowIndex).Cells("Tick").Value = "NO"
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
        Me.SaveMenu()
    End Sub
    Private Sub SaveMenu()

        Try
            If DgvMenuMaster.Rows.Count = 0 Then
                MsgBox("Add Anyone")
                DgvMenuMaster.Focus()
                Exit Sub
            End If

            Trans = VistawinFunctions.ServerConnection.BeginTransaction()
            Dim Saved As Boolean = False

            For Each Dr As DataGridViewRow In DgvMenuMaster.Rows

                If Dr.IsNewRow Then Continue For

                If Dr.Cells("FormName").Value Is Nothing OrElse Trim(Dr.Cells("FormName").Value.ToString()) = "" Then
                    Continue For
                End If

                Using Cmd As New SqlCommand("SP_tbl_MenuMaster_Edit", VistawinFunctions.ServerConnection, Trans)
                    Cmd.CommandType = CommandType.StoredProcedure

                    Cmd.Parameters.AddWithValue("@MenuID", If(Dr.Cells("MenuID").Value Is Nothing OrElse IsDBNull(Dr.Cells("MenuID").Value), 0, Val(Dr.Cells("MenuID").Value)))
                    Cmd.Parameters.AddWithValue("@FormNameOrder", If(Dr.Cells("FormNameOrder").Value Is Nothing OrElse IsDBNull(Dr.Cells("FormNameOrder").Value), 0, Val(Dr.Cells("FormNameOrder").Value)))
                    Cmd.Parameters.AddWithValue("@MenuNameOrder", If(Dr.Cells("MenuNameOrder").Value Is Nothing OrElse IsDBNull(Dr.Cells("MenuNameOrder").Value), 0, Val(Dr.Cells("MenuNameOrder").Value)))

                    If Dr.Cells("FormState").Value Is Nothing OrElse Trim(Dr.Cells("FormState").Value.ToString()) = "" Then
                        Cmd.Parameters.AddWithValue("@FormState", DBNull.Value)
                    Else
                        Cmd.Parameters.AddWithValue("@FormState", Dr.Cells("FormState").Value.ToString())
                    End If
                    Cmd.ExecuteNonQuery()
                    Saved = True
                End Using
            Next

            If Saved Then
                Trans.Commit()
                MsgBox("Saved Successfully", MsgBoxStyle.Information)
                MenuMaster()
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
    Private Sub BtnClose2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose2.Click
        Me.Close()
    End Sub
    Private Sub Btnadd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btnadd.Click
        GrpAdd.Show()
        GrpAdd.BringToFront()
        txtMenuName.Focus()
    End Sub
    Private Sub BtnAddSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAddSave.Click
        Trans = VistawinFunctions.ServerConnection.BeginTransaction
        Try
            If txtMenuName.Text = String.Empty Then
                MsgBox("Enter Menu Name")
                txtMenuName.Focus()
                Exit Sub
            End If
            If txtFormName.Text = String.Empty Then
                MsgBox("Enter From Name")
                txtFormName.Focus()
                Exit Sub
            End If

            Command = New SqlCommand("INSERT INTO tbl_MenuMaster(MenuName, FormName, FormState, ParentID, MenuNameOrder,FormNameOrder) VALUES ('" & txtMenuName.Text & "','" & txtFormName.Text & "','" & CbFormState.Text & "','" & txtParentID.Text & "','" & txtMenuOrder.Text & "','" & txtFromOrder.Text & "')", VistawinFunctions.ServerConnection, Trans)
            Execute = Command.ExecuteNonQuery()

            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Saved")
            End If
            Me.Clear()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
    Private Sub Clear()
        txtLedCode.Text = String.Empty
        txtMenuName.Text = String.Empty
        txtFormName.Text = String.Empty
        txtParentID.Text = String.Empty
        txtMenuOrder.Text = String.Empty
        txtFromOrder.Text = String.Empty
        txtMenuName.Focus()
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        GrpAdd.Hide()
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim SelectBool As Boolean = False
            For Each Dr As DataGridViewRow In DgvMenuMaster.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    SelectBool = True
                End If
            Next
            If SelectBool = False Then
                MsgBox("Select Any one")
                Exit Sub
            End If

            Dim MastID As String = ""
            For Each Dr As DataGridViewRow In DgvMenuMaster.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    If MastID = "" Then
                        MastID = "" & Dr.Cells("MenuID").Value & "" + ","
                    Else
                        MsgBox("Select Only One")
                        DgvMenuMaster.Focus()
                        Exit Sub
                    End If
                End If
            Next

            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                If BVistawinSolution1.Permission() = False Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            For Each Dr As DataGridViewRow In DgvMenuMaster.Rows
                If Dr.Cells("Tick").Value = "YES" Then
                    Cmd = New SqlCommand("DELETE tbl_MenuMaster WHERE MenuID=" & Dr.Cells("MenuID").Value & "", VistawinFunctions.ServerConnection, Trans)
                    Execute = Cmd.ExecuteNonQuery
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
                Me.MenuMaster()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Trans.Rollback()
        Finally
            Trans.Dispose()
        End Try
    End Sub
End Class