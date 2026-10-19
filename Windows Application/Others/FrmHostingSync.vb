Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Windows.Forms

Imports System.IO
Imports System.Data.SqlClient
Public Class FrmHostingSync
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Command As New SqlCommand
    Dim Adapter As New SqlDataAdapter
    Dim Execute, DateFormat, AddOrEdit As Boolean
    Dim FromBool, ToBool As Boolean
    Dim Row, RowIndex As Decimal
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Dataset1, Dataset As New WinDataSet


    Private _startMode As HostingSyncMode = HostingSyncMode.ServerToVB
    Private _loading As Boolean = False

    Public Property StartMode() As HostingSyncMode
        Get
            Return _startMode
        End Get
        Set(ByVal value As HostingSyncMode)
            _startMode = value
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmHostingSync_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        cboMode.Items.Clear()
        cboMode.Items.Add("Hostinger -> SQL Server")
        cboMode.Items.Add("SQL Server -> Hostinger")

        If _startMode = HostingSyncMode.VBToServer Then
            cboMode.SelectedIndex = 1
        Else
            cboMode.SelectedIndex = 0
        End If

        RefreshTableList()


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
    Private Function CurrentMode() As HostingSyncMode
        If cboMode.SelectedIndex = 1 Then Return HostingSyncMode.VBToServer
        Return HostingSyncMode.ServerToVB
    End Function

    Private Function SelectedTable() As String
        If cboTables.SelectedItem Is Nothing Then Return ""
        Return cboTables.SelectedItem.ToString().Trim()
    End Function

    Private Sub cboMode_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboMode.SelectedIndexChanged
        UpdateModeUI()

        If Me.IsHandleCreated AndAlso Not _loading Then
            dgvData.DataSource = Nothing
            RefreshTableList()
        End If
    End Sub

    Private Sub UpdateModeUI()
        If CurrentMode() = HostingSyncMode.ServerToVB Then
            lblTitle.Text = "Hostinger -> SQL Server"
            btnSync.Text = "Insert to SQL Server"
        Else
            lblTitle.Text = "SQL Server -> Hostinger"
            btnSync.Text = "Push to Hostinger"
        End If

        Me.Text = lblTitle.Text & " - " & Convert.ToString(VistawinFunctions.CmpDatabaseName)
    End Sub

    Private Sub SetBusy(ByVal busy As Boolean, ByVal statusText As String)
        _loading = busy

        cboMode.Enabled = Not busy
        cboTables.Enabled = Not busy
        btnRefreshTables.Enabled = Not busy
        btnLoad.Enabled = Not busy
        btnSync.Enabled = Not busy

        lblStatus.Text = statusText
        Me.UseWaitCursor = busy
        Me.Refresh()
    End Sub

    Private Sub RefreshTableList()
        Try
            SetBusy(True, "Loading table list...")

            cboTables.Items.Clear()
            dgvData.DataSource = Nothing

            Dim tables As List(Of String)

            If CurrentMode() = HostingSyncMode.ServerToVB Then
                tables = VHostingSync.FetchRemoteTableList()
            Else
                tables = VHostingSync.FetchLocalTableList()
            End If

            If tables IsNot Nothing AndAlso tables.Count > 0 Then
                cboTables.Items.AddRange(tables.ToArray())
                cboTables.SelectedIndex = 0
                lblStatus.Text = tables.Count.ToString() & " table(s) found."
            Else
                lblStatus.Text = "No tables found."
            End If

        Catch ex As Exception
            dgvData.DataSource = Nothing
            lblStatus.Text = "Error: " & ex.Message
            MessageBox.Show(ex.Message, "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False, lblStatus.Text)
        End Try
    End Sub

    Private Sub btnRefreshTables_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRefreshTables.Click
        RefreshTableList()
    End Sub
    Private Sub btnLoad_Click(ByVal sender As Object,
                          ByVal e As EventArgs) Handles btnLoad.Click

        Dim tableName As String = SelectedTable()

        If tableName.Length = 0 Then
            MessageBox.Show("Select a table first.",
                            "Hosting Sync",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try

            SetBusy(True, "Loading " & tableName & "...")

            Dim dt As DataTable

            '---------------------------------------------
            ' SALES TABLES - DATE WISE
            '---------------------------------------------
            If String.Equals(tableName, "tbl_SalDetail",
                             StringComparison.OrdinalIgnoreCase) OrElse _
               String.Equals(tableName, "tbl_SalParticular",
                             StringComparison.OrdinalIgnoreCase) Then

                Dim fromDate As DateTime
                Dim toDate As DateTime

                If Not DateTime.TryParse(txtFromDate.Text, fromDate) Then
                    Throw New Exception("Invalid From Date.")
                End If

                If Not DateTime.TryParse(txtToDate.Text, toDate) Then
                    Throw New Exception("Invalid To Date.")
                End If

                If fromDate.Date > toDate.Date Then
                    Throw New Exception("From Date cannot be greater than To Date.")
                End If

                If CurrentMode() = HostingSyncMode.ServerToVB Then

                    dt = VHostingSync.FetchRemoteDataByDate(
                            tableName,
                            fromDate,
                            toDate)

                Else

                    dt = VHostingSync.FetchLocalDataByDate(
                            tableName,
                            fromDate,
                            toDate)

                End If

            Else

                '---------------------------------------------
                ' NORMAL TABLES
                '---------------------------------------------
                If CurrentMode() = HostingSyncMode.ServerToVB Then

                    dt = VHostingSync.FetchRemoteData(tableName)

                Else

                    dt = VHostingSync.FetchLocalData(tableName)

                End If

            End If

            dgvData.DataSource = dt

            lblStatus.Text = tableName & " : " &
                             dt.Rows.Count.ToString() &
                             " row(s) loaded."

        Catch ex As Exception

            dgvData.DataSource = Nothing

            lblStatus.Text = "Error: " & ex.Message

            MessageBox.Show(ex.Message,
                            "Hosting Sync",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)

        Finally

            SetBusy(False, lblStatus.Text)

        End Try

    End Sub
    'Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLoad.Click
    '    Dim tableName As String = SelectedTable()

    '    If tableName.Length = 0 Then
    '        MessageBox.Show("Select a table first.", "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    Try
    '        SetBusy(True, "Loading " & tableName & "...")

    '        Dim dt As DataTable

    '        If CurrentMode() = HostingSyncMode.ServerToVB Then
    '            dt = VHostingSync.FetchRemoteData(tableName)
    '        Else
    '            dt = VHostingSync.FetchLocalData(tableName)
    '        End If

    '        dgvData.DataSource = dt
    '        lblStatus.Text = tableName & " : " & dt.Rows.Count.ToString() & " row(s) loaded."

    '    Catch ex As Exception
    '        dgvData.DataSource = Nothing
    '        lblStatus.Text = "Error: " & ex.Message
    '        MessageBox.Show(ex.Message, "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        SetBusy(False, lblStatus.Text)
    '    End Try
    'End Sub

    Private Sub btnSync_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSync.Click
        Dim tableName As String = SelectedTable()
        Dim dt As DataTable = TryCast(dgvData.DataSource, DataTable)

        If tableName.Length = 0 Then
            MessageBox.Show("Select a table first.", "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            MessageBox.Show("No data loaded. Click Load Data first.", "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If CurrentMode() = HostingSyncMode.VBToServer Then
            Dim msg As String = "Hostinger table '" & tableName & "' will be cleared and replaced with " & _
                                dt.Rows.Count.ToString() & " row(s)." & vbCrLf & vbCrLf & _
                                "Continue?"

            If MessageBox.Show(msg, "Confirm Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then
                Exit Sub
            End If
        End If

        Try
            SetBusy(True, "Syncing " & tableName & "...")

            Dim count As Integer

            If CurrentMode() = HostingSyncMode.ServerToVB Then
                count = VHostingSync.CreateAndInsertLocal(dt, tableName)
                lblStatus.Text = "Done. " & count.ToString() & " new row(s) inserted into SQL Server."
            Else
                count = VHostingSync.PushDataToMySQL(dt, tableName)
                lblStatus.Text = "Done. " & count.ToString() & " row(s) pushed to Hostinger."
            End If

            MessageBox.Show(lblStatus.Text, "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            lblStatus.Text = "Error: " & ex.Message
            MessageBox.Show(ex.Message, "Hosting Sync", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False, lblStatus.Text)
        End Try

        If VistawinFunctions.CmpDatabaseName = "KIRUTHICKCLINICTCRDB" Then
            If cboMode.Text = "Hostinger -> SQL Server" Then
                Me.TransStockUpdate()
            End If
        End If

    End Sub
    Private Sub TransStockUpdate()

        Dim Trans As SqlTransaction = Nothing

        Try
            If dgvData.Rows.Count = 0 Then
                MsgBox("Add any Medicine Name")
                dgvData.Focus()
                Exit Sub
            End If

            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If

            Trans = VistawinFunctions.ServerConnection.BeginTransaction()

            Dim SavedCount As Integer = 0

            For Each Dr As DataGridViewRow In dgvData.Rows
                If Dr.IsNewRow Then Continue For
                If Dr.Cells("St_StNumber_i").Value Is Nothing Then Continue For

                Dim StNumber As Integer = CInt(Val(Convert.ToString(Dr.Cells("St_StNumber_i").Value)))

                If StNumber = 0 Then Continue For

                Using Cmd1 As New SqlCommand("SP_tbl_TransStUpdate", VistawinFunctions.ServerConnection, Trans)
                    Cmd1.CommandType = CommandType.StoredProcedure
                    Cmd1.Parameters.AddWithValue("@SF_StockID_n", CDec(Val(Convert.ToString(Dr.Cells("StockID").Value))))
                    Cmd1.Parameters.AddWithValue("@SF_StNumber_i", CDec(Val(Convert.ToString(Dr.Cells("St_StNumber_i").Value))))
                    Cmd1.Parameters.AddWithValue("@SF_TransQty_i", CInt(Val(Convert.ToString(Dr.Cells("Currstock").Value))))
                    Cmd1.Parameters.AddWithValue("@InputString", "Update")
                    Cmd1.ExecuteNonQuery()
                    SavedCount += 1
                End Using
            Next

            If SavedCount = 0 Then
                Throw New Exception("No medicine found to save.")
            End If

            Trans.Commit()
            Trans = Nothing
            MsgBox("Successfully Saved")
            BVistawinSolution1.ClinicStockUpdate1()
        Catch ex As Exception
            If Trans IsNot Nothing Then
                Try
                    Trans.Rollback()
                Catch
                End Try
            End If
            MessageBox.Show(ex.Message, "Stock Transfer", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If Trans IsNot Nothing Then
                Trans.Dispose()
            End If
        End Try
    End Sub

End Class
