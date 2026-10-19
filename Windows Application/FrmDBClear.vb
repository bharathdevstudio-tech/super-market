Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class FrmDBClear
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet

    Private Sub FrmDBClear_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.TableList()
    End Sub

    Private Sub TableList()
        Ds = New DataSet

        If CbAll.Checked = True Then
            Adapter = New SqlDataAdapter("SELECT DISTINCT OBJ.name AS 'TableName1',PART.rows AS 'RowCount1' FROM sys.partitions AS PART INNER JOIN sys.dm_db_partition_stats AS STAT  ON PART.partition_id = STAT.partition_id AND PART.partition_number = STAT.partition_number INNER JOIN sys.objects AS OBJ ON STAT.object_id = OBJ.object_id INNER JOIN sys.schemas AS SCH ON OBJ.schema_id = SCH.schema_id INNER JOIN sys.indexes AS INDX ON STAT.object_id = INDX.object_id AND STAT.index_id = INDX.index_id INNER JOIN sys.data_spaces AS DS ON INDX.data_space_id = DS.data_space_id WHERE SCH.name='dbo' ORDER BY PART.rows DESC", VistawinFunctions.ServerConnection)
        ElseIf CbAll.Checked = False Then
            Adapter = New SqlDataAdapter("SELECT DISTINCT OBJ.name AS 'TableName1',PART.rows AS 'RowCount1' FROM sys.partitions AS PART INNER JOIN sys.dm_db_partition_stats AS STAT  ON PART.partition_id = STAT.partition_id AND PART.partition_number = STAT.partition_number INNER JOIN sys.objects AS OBJ ON STAT.object_id = OBJ.object_id INNER JOIN sys.schemas AS SCH ON OBJ.schema_id = SCH.schema_id INNER JOIN sys.indexes AS INDX ON STAT.object_id = INDX.object_id AND STAT.index_id = INDX.index_id INNER JOIN sys.data_spaces AS DS ON INDX.data_space_id = DS.data_space_id WHERE SCH.name='dbo' AND PART.rows>0 AND OBJ.name NOT IN('tbl_Settings','tbl_MedicalTest','tbl_UserPermission','tbl_Denomination','tbl_UserRoles','tbl_UnitList','tbl_PaperSize','tbl_AccLedger','tbl_Companyprofile','tbl_UserList') ORDER BY PART.rows DESC", VistawinFunctions.ServerConnection)
        End If
        Adapter.Fill(Ds, "FillData")

        RowIndex = 0
        DgvTableList.Rows.Clear()
        For Each Dr As DataRow In Ds.Tables("FillData").Rows
            DgvTableList.Rows.Add()
            DgvTableList.Rows(RowIndex).Cells("SNo1").Value = RowIndex + 1
            DgvTableList.Rows(RowIndex).Cells("TableName1").Value = Dr.Item("TableName1")
            DgvTableList.Rows(RowIndex).Cells("RowCount1").Value = Dr.Item("RowCount1")
            'DgvUserList.Rows(RowIndex).Cells("Delete1").Value = Dr.Item("Delete1")
            DgvTableList.Rows(RowIndex).Cells("Tick1").Value = "NO"
            RowIndex += 1
        Next
    End Sub
    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Me.DeleteTable1() '
    End Sub

    Private Sub DeleteTable1() '
        Try
            Trans = VistawinFunctions.ServerConnection.BeginTransaction

            Dim RowCheck As Boolean = False
            For Each Dr As DataGridViewRow In DgvTableList.Rows
                If Dr.Cells("Tick1").Value = "YES" Then
                    RowCheck = True
                End If
            Next
            If RowCheck = False Then
                MsgBox("Select Anyone")
                DgvTableList.Focus()
                Exit Sub
            End If

            Dim MsgStr As DialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo)
            If MsgStr = 6 Then
                If BVistawinSolution1.Permission() = False Then
                    Exit Sub
                End If
            Else
                Exit Sub
            End If

            For Each Dr As DataGridViewRow In DgvTableList.Rows
                If Dr.Cells("Tick1").Value = "YES" Then
                    Cmd = New SqlCommand("DELETE FROM " & Dr.Cells("TableName1").Value & "", VistawinFunctions.ServerConnection, Trans)
                    Execute = Cmd.ExecuteNonQuery()
                End If
            Next
            If Execute = True Then
                Trans.Commit()
                MsgBox("Successfully Deleted")
            End If
            Me.TableList()

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

    Private Sub CBSelectM_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles CBSelectM.CheckedChanged

        If CBSelectM.Checked = True Then
            For Each Dr As DataGridViewRow In DgvTableList.Rows
                Dr.Cells("Tick1").Value = "YES"
            Next
        ElseIf CBSelectM.Checked = False Then
            For Each Dr As DataGridViewRow In DgvTableList.Rows
                Dr.Cells("Tick1").Value = "NO"
            Next
        End If
    End Sub

    Private Sub CbAll_CheckedChanged(sender As Object, e As System.EventArgs) Handles CbAll.CheckedChanged
        Me.TableList()
    End Sub
End Class