
Imports System.Data.SqlClient

Public Class SubFrmCommonSearch

    Public Query As String = ""
    Public ReturnColumn As String = ""
    Public SelectedValue As String = ""
    Private dt As New DataTable
    Public SelectedCode As Integer = 0

    Public CatChk As String = ""
    Public BranQuery As String = ""
    Public AreasQuery As String = ""
    Public EmployeeQuery As String = ""
    Public ProcedureQuery As String = ""
    Public MolIDQuery As String = ""
    Public CompanyIdQuery As String = ""
    Public SupplierNameQuery As String = ""
    Public PharmaNameQuery As String = ""
    Public UnitQuery As String = ""
    Public CusQuery As String = ""
    Public DoctorQuery As String = ""
    Public BankQuery As String = ""

    Private Sub FrmSearch_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

        If CatChk = "Category1" Then
            LblSearchName.Text = "Search Category"
        ElseIf CatChk = "Brand1" Then
            LblSearchName.Text = "Search Brand"
        ElseIf CatChk = "Areas1" Then
            LblSearchName.Text = "Search Area"
        ElseIf CatChk = "Employee1" Then
            LblSearchName.Text = "Search Employee"
        ElseIf CatChk = "Procedure1" Then
            LblSearchName.Text = "Search Procedure"
        ElseIf CatChk = "MolID1" Then
            LblSearchName.Text = "Search MolID"
        ElseIf CatChk = "CompanyId1" Then
            LblSearchName.Text = "Search CompanyId"
        ElseIf CatChk = "SupplierName1" Then
            LblSearchName.Text = "Search Supplier Name"
        ElseIf CatChk = "PharmaName1" Then
            LblSearchName.Text = "Search Pharma Name"
        ElseIf CatChk = "Unit1" Then
            LblSearchName.Text = "Search Pharma Name"
        ElseIf CatChk = "Cus1" Then
            LblSearchName.Text = "Search Pharma Name"
        ElseIf CatChk = "DoctorName1" Then
            LblSearchName.Text = "Search Doctor Name"
        ElseIf CatChk = "DoctorName1" Then
            LblSearchName.Text = "Search Doctor Name"
        End If
        LoadData("")
        TxtSearch.Focus()
    End Sub

    Private Sub TxtSearch_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtSearch.TextChanged
        LoadData(TxtSearch.Text)
    End Sub
    Private Sub LoadData(ByVal SearchText As String)

        Try
            dt.Clear()

            If CatChk = "Category1" Then
                Using da As New SqlDataAdapter(Query, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Brand1" Then
                Using da As New SqlDataAdapter(BranQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Areas1" Then
                Using da As New SqlDataAdapter(AreasQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Employee1" Then
                Using da As New SqlDataAdapter(EmployeeQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Procedure1" Then
                Using da As New SqlDataAdapter(ProcedureQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "MolID1" Then
                Using da As New SqlDataAdapter(MolIDQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "CompanyId1" Then
                Using da As New SqlDataAdapter(CompanyIdQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "SupplierName1" Then
                Using da As New SqlDataAdapter(SupplierNameQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "PharmaName1" Then
                Using da As New SqlDataAdapter(PharmaNameQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Unit1" Then
                Using da As New SqlDataAdapter(UnitQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "Cus1" Then
                Using da As New SqlDataAdapter(CusQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "DoctorName1" Then
                Using da As New SqlDataAdapter(DoctorQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            ElseIf CatChk = "BankName1" Then
                Using da As New SqlDataAdapter(BankQuery, VistawinFunctions.ServerConnection)
                    da.SelectCommand.Parameters.AddWithValue("@Search", "%" & SearchText & "%")
                    da.Fill(dt)
                End Using
            End If

            DgvSearch.DataSource = dt
            'DgvSearch.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            DgvSearch.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            DgvSearch.Columns(0).ReadOnly = True
            DgvSearch.Columns(0).Width = 120
            DgvSearch.Columns(1).ReadOnly = True
            DgvSearch.Columns(1).Width = 200
            DgvSearch.MultiSelect = False

            For Each col As DataGridViewColumn In DgvSearch.Columns
                col.SortMode = DataGridViewColumnSortMode.NotSortable
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub DgvSearch_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles DgvSearch.KeyDown

        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            SelectCurrentRow()
        End If
    End Sub

    Private Sub DgvSearch_CellDoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DgvSearch.CellDoubleClick

        If e.RowIndex >= 0 Then
            SelectCurrentRow()
        End If

    End Sub

    Private Sub SelectCurrentRow()

        If CatChk = "Category1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Category_Code_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Category_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Brand1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Brand_Code_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Brand_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Areas1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Area_Code_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Area_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Employee1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("C_ID_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("C_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Procedure1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Pr_ProcedureID_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Pr_ProcedureName_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "MolID1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Mo_Code_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Mo_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "CompanyId1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("SC_CompID_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("SC_CompName_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "SupplierName1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("C_ID_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("C_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "PharmaName1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Pharma_Code_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Pharma_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Unit1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("U_RecordNo_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("U_EName_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "Cus1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("C_ID_n").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("C_Name_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "DoctorName1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Dr_DrID_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Dr_DrName_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        ElseIf CatChk = "BankName1" Then
            If DgvSearch.CurrentRow Is Nothing Then Exit Sub
            SelectedCode = Val(DgvSearch.CurrentRow.Cells("Ba_BankID_i").Value)
            SelectedValue = DgvSearch.CurrentRow.Cells("Ba_BankName_vc").Value.ToString()
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub
    Private Sub TxtSearch_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles TxtSearch.KeyDown

        If e.KeyCode = Keys.Down Then
            If DgvSearch.Rows.Count > 0 Then
                DgvSearch.Focus()
                DgvSearch.CurrentCell = DgvSearch.Rows(0).Cells(0)
            End If
        End If
    End Sub

End Class