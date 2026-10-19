Imports System.IO
Imports System.Data.SqlClient
Imports System.Data
Imports System.Reflection

Public Class FrmUniversalBookView
    Private CurrentViewCode As String = ""
    Private CurrentLogic As String = ""
    Private CheckInFormName As String = ""
    Private CheckOutFormName As String = ""
    Private HouseKeepingFormName As String = ""
    Private ShowAdvance As String = "NO"
    Public BookViewCode As String = ""
    Private Cmd As SqlCommand
    Private Class RoomGridInfo
        Public ViewCode As String
        Public BlockName As String
        Public ColumnCount As Integer
        Public HeaderHeight As Integer
        Public DetailHeight As Integer
        Public HeaderMode As String
    End Class
    Private Sub FrmUniversalBookView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
       Try
            Me.WindowState = FormWindowState.Maximized
            CurrentViewCode = Trim(BookViewCode)
            If CurrentViewCode = "" Then
                CurrentViewCode = GetDefaultBookViewCode()
            End If
            LoadBookViewMaster()
            CreateDynamicRoomGrids()

            If ShowAdvance = "YES" Then
                DgvAdvBookDtl.Visible = True
                DgvAdvBookDtl.Dock = DockStyle.Bottom
                DgvAdvBookDtl.Height = 220
                DgvAdvBookDtl.BringToFront()
                AdvBookingDtl()
            Else
                DgvAdvBookDtl.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString(),"Universal Book View", MessageBoxButtons.OK,MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub RefreshRooms()
        Try
            CreateDynamicRoomGrids()
            If ShowAdvance = "YES" Then
                DgvAdvBookDtl.Visible = True
                AdvBookingDtl()
            Else
                DgvAdvBookDtl.Visible = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub AdvBookingDtl()
        Try
            DgvAdvBookDtl.Rows.Clear()
            Dim Dt As New DataTable
            Dim Sql As String = "SELECT Adv_ArrivalDate_d AS ArrDate, a.Cus_ID_n AS CusID, ISNULL(a.Cus_Name_vc,'') AS CusName, ISNULL(a.Cus_Place_vc,'') AS CusPlace, ISNULL(b.C_Mobile_vc,'') AS Mobile1, Adv_AdvID_n AS ReservID, ISNULL(Adv_RoomNumber_vc,'') AS RoomNoA, ISNULL(Adv_AdvAmt_n,0) AS AdvAmt, ISNULL(Cus_Purpose_vc,'') AS Purpose FROM tbl_Lod_AdvBooking a LEFT JOIN tbl_Lod_CusProfile b ON (a.Cus_ID_n=b.C_ID_n AND b.C_Type_vc='CUSTOMER') ORDER BY Adv_ArrivalDate_d ASC"
            Using Da As New SqlDataAdapter(Sql, VistawinFunctions.ServerConnection)
                Da.Fill(Dt)
            End Using
            Dim RowNo As Integer = 0
            For Each Dr As DataRow In Dt.Rows
                RowNo = DgvAdvBookDtl.Rows.Add()
                With DgvAdvBookDtl.Rows(RowNo)
                    .Cells("SNo1").Value = RowNo + 1
                    .Cells("ArrDate").Value = Dr("ArrDate")
                    .Cells("CusID").Value = Dr("CusID")
                    .Cells("CusName").Value = Dr("CusName")
                    .Cells("CusPlace").Value = Dr("CusPlace")
                    .Cells("Mobile1").Value = Dr("Mobile1")
                    .Cells("ReservID").Value = Dr("ReservID")
                    .Cells("RoomNoA").Value = Dr("RoomNoA")
                    .Cells("AdvAmt").Value = Dr("AdvAmt")
                    .Cells("Purpose").Value = Dr("Purpose")
                    If Not IsDBNull(Dr("ArrDate")) Then
                        If CDate(Dr("ArrDate")).Date <> CDate(VistawinFunctions.TodayDate).Date Then
                            .DefaultCellStyle.ForeColor = Color.Black
                            .DefaultCellStyle.BackColor = Color.LightSalmon
                        End If
                    End If
                End With
            Next
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Advance Booking",MessageBoxButtons.OK,MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub CloseUniversalBookView()
        Try
            Me.Close()
        Catch
        End Try
    End Sub
    Private Function GetDefaultBookViewCode() As String
        Try
            If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
                VistawinFunctions.ServerConnection.Open()
            End If
            Dim Sql As String ="SELECT TOP 1 BV_Code_vc FROM tbl_BookViewMaster WHERE ISNULL(BV_Active_vc,'YES')='YES' AND ISNULL(BV_Default_vc,'NO')='YES'"
            Using CmdGet As New SqlCommand(Sql,VistawinFunctions.ServerConnection)
                Dim Result As Object =  CmdGet.ExecuteScalar()
                If Result IsNot Nothing AndAlso _
                   Not IsDBNull(Result) Then
                    Return Trim(Result.ToString())
                End If
            End Using

            Sql = "SELECT COUNT(*) FROM tbl_BookViewMaster WHERE ISNULL(BV_Active_vc,'YES')='YES'"
            Dim ActiveCount As Integer = 0
            Using CmdCount As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                ActiveCount = Convert.ToInt32(CmdCount.ExecuteScalar())
            End Using
            If ActiveCount = 1 Then
                Sql = "SELECT TOP 1 BV_Code_vc FROM tbl_BookViewMaster WHERE ISNULL(BV_Active_vc,'YES')='YES'"
                Using CmdOne As New SqlCommand( Sql,VistawinFunctions.ServerConnection)
                    Dim Result As Object = CmdOne.ExecuteScalar()

                    If Result IsNot Nothing AndAlso _
                       Not IsDBNull(Result) Then
                        Return Trim(Result.ToString())
                    End If
                End Using
            End If
            Return ""
        Catch ex As Exception
            MessageBox.Show( ex.Message,"Book View Configuration")
            Return ""
        End Try
    End Function
    Private Sub LoadBookViewMaster()
        Try
            Dim Dt As New DataTable()
            Dim Sql As String = "SELECT BV_Title_vc,BV_Logic_vc, BV_CheckInForm_vc, BV_CheckOutForm_vc, BV_HouseKeepingForm_vc, BV_ShowAdvance_vc FROM tbl_BookViewMaster WHERE BV_Code_vc=@ViewCode AND ISNULL(BV_Active_vc,'YES')='YES'"
            Using Da As New SqlDataAdapter( Sql, VistawinFunctions.ServerConnection)
                Da.SelectCommand.Parameters.AddWithValue("@ViewCode", CurrentViewCode)
                Da.Fill(Dt)
            End Using

            If Dt.Rows.Count = 0 Then
                Throw New Exception("Book View configuration not found : " & CurrentViewCode)
            End If
            Dim Dr As DataRow = Dt.Rows(0)
            Me.Text = Convert.ToString(Dr("BV_Title_vc"))
            CurrentLogic = Convert.ToString(Dr("BV_Logic_vc")).Trim.ToUpper()
            CheckInFormName = Convert.ToString(Dr("BV_CheckInForm_vc")).Trim()
            CheckOutFormName = Convert.ToString(Dr("BV_CheckOutForm_vc")).Trim()
            HouseKeepingFormName =Convert.ToString(Dr("BV_HouseKeepingForm_vc")).Trim()
            ShowAdvance = Convert.ToString(Dr("BV_ShowAdvance_vc")).Trim.ToUpper()
            If ShowAdvance <> "YES" Then
                ShowAdvance = "NO"
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Private Sub CreateDynamicRoomGrids()
        Try
            PnlRoomHost.Controls.Clear()
            Dim Dt As New DataTable()
            Dim Sql As String = "SELECT BG_Order_i, BG_BlockName_vc, BG_ColumnCount_i, BG_HeaderHeight_i, BG_DetailHeight_i, BG_GridHeight_i, BG_HeaderMode_vc FROM tbl_BookViewGridSetup WHERE BG_ViewCode_vc=@ViewCode AND ISNULL(BG_Visible_vc,'YES')='YES' ORDER BY BG_Order_i"
            Using Da As New SqlDataAdapter(Sql,VistawinFunctions.ServerConnection)
                Da.SelectCommand.Parameters.AddWithValue( "@ViewCode", CurrentViewCode)
                Da.Fill(Dt)
            End Using
            If Dt.Rows.Count = 0 Then
                MessageBox.Show("No DGV setup found for " & CurrentViewCode)
                Exit Sub
            End If
            Dim Layout As New TableLayoutPanel()
            Layout.Dock = DockStyle.Fill
            Layout.ColumnCount = 1
            Layout.RowCount = Dt.Rows.Count
            Layout.Margin = New Padding(0)
            Layout.Padding = New Padding(0)
            Layout.ColumnStyles.Add( New ColumnStyle( SizeType.Percent, 100.0F))

            Dim GridNo As Integer = 0
            For Each Dr As DataRow In Dt.Rows
                GridNo += 1
                Dim Info As New RoomGridInfo()
                Info.ViewCode = CurrentViewCode
                Info.BlockName = Convert.ToString(Dr("BG_BlockName_vc")).Trim()
                Info.ColumnCount =Convert.ToInt32( Dr("BG_ColumnCount_i"))
                Info.HeaderHeight =Convert.ToInt32(Dr("BG_HeaderHeight_i"))
                Info.DetailHeight =Convert.ToInt32( Dr("BG_DetailHeight_i"))
                Info.HeaderMode = Convert.ToString(Dr("BG_HeaderMode_vc")).Trim.ToUpper()

                Dim GridHeight As Integer = Convert.ToInt32( Dr("BG_GridHeight_i"))
                If GridNo = Dt.Rows.Count Then
                    Layout.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
                Else
                    If GridHeight <= 0 Then
                        GridHeight = 250
                    End If
                    Layout.RowStyles.Add( New RowStyle( SizeType.Absolute, CSng(GridHeight)))
                End If
                Dim Dgv As DataGridView =  CreateRoomDataGrid(Info)
                Layout.Controls.Add( Dgv, 0, GridNo - 1)
                LoadRoomGrid(Dgv)
            Next
            PnlRoomHost.Controls.Add(Layout)
        Catch ex As Exception
            MessageBox.Show(ex.ToString(),"Book View")
        End Try
    End Sub
    Private Function CreateRoomDataGrid(ByVal Info As RoomGridInfo) As DataGridView
        Dim Dgv As New DataGridView()

        Dgv.Name = "DGVRoom_" & Info.BlockName.Replace(" ", "_")
        Dgv.Tag = Info
        Dgv.Dock = DockStyle.Fill
        Dgv.AllowUserToAddRows = False
        Dgv.AllowUserToDeleteRows = False
        Dgv.AllowUserToResizeColumns = False
        Dgv.AllowUserToResizeRows = False
        Dgv.RowHeadersVisible = False
        Dgv.ColumnHeadersVisible = False
        Dgv.BackgroundColor = Color.White
        Dgv.BorderStyle = BorderStyle.None
        Dgv.ReadOnly = True
        Dgv.MultiSelect = False

        Dgv.SelectionMode = DataGridViewSelectionMode.CellSelect
        Dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        For i As Integer = 1 To Info.ColumnCount
            Dim Col As New DataGridViewTextBoxColumn()
            Col.Name ="RoomCol" & i.ToString()
            Col.HeaderText = ""
            Col.ReadOnly = True
            Col.SortMode = DataGridViewColumnSortMode.NotSortable
            Col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            Col.DefaultCellStyle.WrapMode = DataGridViewTriState.True
            Dgv.Columns.Add(Col)
        Next
        AddHandler Dgv.CellMouseDoubleClick,AddressOf DynamicRoomGrid_DoubleClick
        Return Dgv
    End Function
    Private Sub LoadRoomGrid(ByVal Dgv As DataGridView)
        Try
            Dgv.Rows.Clear()
            Dim Info As RoomGridInfo = TryCast(Dgv.Tag, RoomGridInfo)
            If Info Is Nothing Then Exit Sub
            Dim Dt As New DataTable()

            Dim Sql As String = "SELECT * FROM tbl_RoomDetails WHERE REPLACE(LTRIM(RTRIM(Room_BlockName_vc)),'  ',' ') = REPLACE(LTRIM(RTRIM(@BlockName)),'  ',' ') ORDER BY Room_OrderBy_i"
            Using Da As New SqlDataAdapter( Sql, VistawinFunctions.ServerConnection)
                Da.SelectCommand.Parameters.AddWithValue("@BlockName", Info.BlockName)
                Da.Fill(Dt)
            End Using

            If Dt.Rows.Count = 0 Then Exit Sub
            Dim GridColumn As Integer = Dgv.Columns.Count
            Dim RowIndex As Integer = 0
            Dim ColIndex As Integer = 0
            For Each Dr As DataRow In Dt.Rows
                If ColIndex = 0 Then
                    Dgv.Rows.Add(2)
                    Dgv.Rows(RowIndex).Height = Info.HeaderHeight
                    Dgv.Rows(RowIndex + 1).Height = Info.DetailHeight
                    Dgv.Rows(RowIndex).DefaultCellStyle.Font = New Font("Times New Roman",10, FontStyle.Bold)
                    Dgv.Rows(RowIndex + 1).DefaultCellStyle.Font = New Font("Segoe UI", 8, FontStyle.Bold)
                End If
                FillRoomCell( Dgv, Dr, RowIndex, ColIndex, Info)
                ColIndex += 1
                If ColIndex >= GridColumn Then
                    ColIndex = 0
                    RowIndex += 2
                End If
            Next
            Dgv.ClearSelection()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub FillRoomCell( ByVal Dgv As DataGridView,ByVal Dr As DataRow, ByVal RowIndex As Integer, ByVal ColIndex As Integer,ByVal Info As RoomGridInfo)
        Try
            Dim RoomNo As String = Convert.ToString(Dr("Room_Number_vc")).Trim()
            Dim RoomType As String = ""
            If Dr.Table.Columns.Contains("Room_Type_vc") Then
                RoomType = Convert.ToString(Dr("Room_Type_vc")).Trim()
            End If
            Dim HeaderCell As DataGridViewCell = Dgv.Rows(RowIndex).Cells(ColIndex)
            Dim DetailCell As DataGridViewCell = Dgv.Rows(RowIndex + 1).Cells(ColIndex)

            If Info.HeaderMode = "ROOMTYPE" Then
                HeaderCell.Value = RoomNo & " - " & RoomType
            Else
                HeaderCell.Value = RoomNo
            End If
            HeaderCell.ToolTipText = RoomNo
            HeaderCell.Tag = RoomNo
            HeaderCell.Style.BackColor = Color.White
            HeaderCell.Style.ForeColor = Color.Black

            Select Case CurrentLogic
                Case "LODGE"
                    FillLodgeRoomDetail( DetailCell,Dr, RoomNo)
                Case "SITE"
                    FillSiteRoomDetail(DetailCell, Dr, RoomNo)
                Case "COMPLEX"
                    FillSiteRoomDetail(DetailCell, Dr, RoomNo)
                Case Else
                    FillSimpleRoomDetail(DetailCell, Dr, RoomNo)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub FillSiteRoomDetail( ByVal DetailCell As DataGridViewCell, ByVal Dr As DataRow, ByVal RoomNo As String)
        Dim IsAvailable As Boolean = Convert.ToString( Dr("Room_available_vc")).Trim.ToUpper() = "YES"
        If IsAvailable Then
            DetailCell.Value = ""
            DetailCell.Style.BackColor = Color.Green
            DetailCell.Style.ForeColor = Color.White
        Else
            DetailCell.Value = GetCustomerInfo(RoomNo)
            DetailCell.Style.BackColor = Color.Blue
            DetailCell.Style.ForeColor = Color.White
        End If
    End Sub
    Private Sub FillLodgeRoomDetail(ByVal DetailCell As DataGridViewCell, ByVal Dr As DataRow, ByVal RoomNo As String)
        Try
            Dim IsAvailable As Boolean = Convert.ToString(Dr("Room_available_vc")).Trim.ToUpper() = "YES"
            Dim IsBlocked As Boolean = Convert.ToString(Dr("Room_Block_vc")).Trim.ToUpper() = "YES"
            Dim IsHK As Boolean = Convert.ToString(Dr("Room_HKComplete_vc")).Trim.ToUpper() = "YES"

            If IsBlocked Then
                DetailCell.Value =Convert.ToString( Dr("Room_BlockReason_vc"))
                DetailCell.Style.BackColor = GetRoomColor("BLOCK",  Color.Red)
                DetailCell.Style.ForeColor = Color.White
            ElseIf IsAvailable AndAlso IsHK Then
                DetailCell.Value = ""
                DetailCell.Style.BackColor =  GetRoomColor( "AVAILABLE", Color.Green)
                DetailCell.Style.ForeColor =Color.White
            ElseIf IsAvailable AndAlso Not IsHK Then
                DetailCell.Value = "HouseKeeping"
                DetailCell.Style.BackColor = GetRoomColor( "HOUSEKEEPING", Color.Purple)
                DetailCell.Style.ForeColor = Color.White
            Else
                DetailCell.Value = GetCustomerInfo(RoomNo)
                DetailCell.Style.BackColor = Color.Blue
                DetailCell.Style.ForeColor =Color.White
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub FillSimpleRoomDetail(ByVal DetailCell As DataGridViewCell,ByVal Dr As DataRow, ByVal RoomNo As String)
        Dim Available As Boolean = Convert.ToString(Dr("Room_available_vc")).Trim.ToUpper() = "YES"
        If Available Then
            DetailCell.Value = ""
            DetailCell.Style.BackColor = Color.Green
        Else
            DetailCell.Value = GetCustomerInfo(RoomNo)
            DetailCell.Style.BackColor = Color.Blue
        End If
        DetailCell.Style.ForeColor = Color.White
    End Sub
    Private Function GetRoomColor(ByVal Particular As String,ByVal DefaultColor As Color) As Color
        Try
            Using CmdColor As New SqlCommand("SELECT ColorName FROM tbl_RoomStayColor WHERE Particulars=@Particular", VistawinFunctions.ServerConnection)
                CmdColor.Parameters.AddWithValue( "@Particular",  Particular)
                Dim Result As Object = CmdColor.ExecuteScalar()

                If Result IsNot Nothing AndAlso Not IsDBNull(Result) Then
                    Return Color.FromName(Result.ToString())
                End If
            End Using
        Catch
        End Try
        Return DefaultColor
    End Function
    Private Function GetCustomerInfo( ByVal RoomNo As String) As String
        Try
            Dim Sql As String = ""
            Select Case CurrentLogic
                Case "LODGE"
                    Sql = "SELECT ISNULL(a.C_Name_vc,'') + '  ' + ISNULL(a.C_Mobile_vc,'') + '  ' + ISNULL(a.C_Place_vc,'') FROM tbl_Lod_CusProfile a LEFT JOIN tbl_BookingDetails b ON a.C_ID_n=b.Book_CustID_n AND a.C_Type_vc='CUSTOMER' WHERE b.Book_RoomCheckOut_vc='NO' AND b.Room_Number_vc=@RoomNo"
                Case "SITE"
                    Sql = "SELECT ISNULL(a.C_Name_vc,'') + '   ' + ISNULL(a.C_MobNo1_vc,'') + '   ' + ISNULL(a.C_Place_vc,'') + '   Type of Work: ' + ISNULL(b.Bo_WorkType_vc,'') FROM tbl_Cust a LEFT JOIN tbl_BookingDetails b ON a.C_CustID_n=b.Bo_CustID_n WHERE b.Bo_RoomChkOut_vc='NO' AND b.Bo_RoomNo_vc=@RoomNo AND b.Bo_CustName_vc<>'Again'"
                Case "COMPLEX"
                    Sql = "SELECT ISNULL(CustName,'')+' - '+ISNULL(CompanyName,'')+' - Outstanding Amt:'+CONVERT(VARCHAR,(ISNULL(PayableAmount,0.00)-ISNULL(PaidAmount,0.00))) FROM CustOutstanding WHERE RoomChkOut='NO' AND RoomNo=@RoomNo"
                Case Else
                    Return ""
            End Select

            Using CmdInfo As New SqlCommand(Sql, VistawinFunctions.ServerConnection)
                CmdInfo.Parameters.AddWithValue("@RoomNo", RoomNo)
                Dim Result As Object = CmdInfo.ExecuteScalar()
                If Result Is Nothing OrElse IsDBNull(Result) Then
                    Return ""
                End If
                Return Result.ToString()
            End Using
        Catch
            Return ""
        End Try
    End Function
    Private Sub DynamicRoomGrid_DoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellMouseEventArgs)
        Try
            If e.RowIndex < 0 Then Exit Sub
            If e.ColumnIndex < 0 Then Exit Sub
            Dim Dgv As DataGridView = CType(sender, DataGridView)

            Dim HeaderRow As Integer
            If e.RowIndex Mod 2 = 0 Then
                HeaderRow = e.RowIndex
            Else
                HeaderRow = e.RowIndex - 1
            End If
            Dim RoomNo As String = Convert.ToString(Dgv.Rows(HeaderRow).Cells(e.ColumnIndex). ToolTipText).Trim()
            If RoomNo = "" Then Exit Sub
            VistawinFunctions.roomno = RoomNo
            OpenConfiguredRoom(RoomNo)
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
    Private Sub OpenConfiguredRoom(ByVal RoomNo As String)
        Try
            Select Case CurrentLogic
                Case "LODGE"
                    OpenLodgeRoom(RoomNo)
                Case "SITE"
                    OpenSiteRoom(RoomNo)
                Case "COMPLEX"
                    OpenComplexRoom(RoomNo)
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub OpenFormByName(ByVal FormName As String, Optional ByVal DialogMode As Boolean = False)
        Try
            If Trim(FormName) = "" Then Exit Sub
            Dim TargetType As Type = Nothing
            Dim Types() As Type = Assembly.GetExecutingAssembly().GetTypes()
            For Each T As Type In Types
                If String.Compare(T.Name, FormName, True) = 0 Then
                    If GetType(Form).IsAssignableFrom(T) Then
                        TargetType = T
                        Exit For
                    End If
                End If
            Next
            If TargetType Is Nothing Then
                MessageBox.Show("Form not found : " & FormName)
                Exit Sub
            End If
            Dim F As Form = CType(Activator.CreateInstance(TargetType), Form)
            If DialogMode Then
                F.ShowDialog()
            Else
                F.MdiParent = FrmMDICommon
                F.WindowState = FormWindowState.Maximized
                F.Show()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
   
    Private Sub OpenSiteRoom(ByVal RoomNo As String)
        Try
            Using CmdCheck As New SqlCommand("SELECT COUNT(*) FROM tbl_RoomDetails WHERE Room_Number_vc=@RoomNo AND Room_available_vc='YES'", VistawinFunctions.ServerConnection)
                CmdCheck.Parameters.AddWithValue("@RoomNo",RoomNo)
                Dim Available As Integer = Convert.ToInt32(CmdCheck.ExecuteScalar())
                If Available > 0 Then
                    OpenFormByName(CheckInFormName)
                Else
                    OpenFormByName(CheckOutFormName)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    
    Private Sub OpenLodgeRoom(ByVal RoomNo As String)
        Try
            Using C As New SqlCommand( "SELECT COUNT(*) FROM tbl_RoomDetails WHERE Room_Number_vc=@RoomNo AND Room_Block_vc='YES'", VistawinFunctions.ServerConnection)
                C.Parameters.AddWithValue("@RoomNo", RoomNo)
                If Convert.ToInt32(C.ExecuteScalar()) > 0 Then
                    Exit Sub
                End If
            End Using

            Using C As New SqlCommand( "SELECT COUNT(*) FROM tbl_RoomDetails WHERE Room_Number_vc=@RoomNo AND Room_HKComplete_vc='NO'", VistawinFunctions.ServerConnection)
                C.Parameters.AddWithValue("@RoomNo", RoomNo)
                If Convert.ToInt32(C.ExecuteScalar()) > 0 Then
                    If HouseKeepingFormName <> "" Then
                        OpenFormByName(HouseKeepingFormName,True)
                    End If
                    Exit Sub
                End If
            End Using

            Using C As New SqlCommand("SELECT COUNT(*) FROM tbl_RoomDetails WHERE Room_Number_vc=@RoomNo AND Room_available_vc='YES' AND Room_Block_vc='NO'", VistawinFunctions.ServerConnection)
                C.Parameters.AddWithValue("@RoomNo", RoomNo)
                Dim Available As Integer = Convert.ToInt32(C.ExecuteScalar())
                If Available > 0 Then
                    OpenFormByName(CheckInFormName)
                Else
                    OpenFormByName(CheckOutFormName)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    Private Sub OpenComplexRoom(ByVal RoomNo As String)
        Try
            Using CmdCheck As New SqlCommand("SELECT COUNT(*) FROM tbl_RoomDetails WHERE Room_Number_vc=@RoomNo AND Room_available_vc='YES'", VistawinFunctions.ServerConnection)
                CmdCheck.Parameters.AddWithValue("@RoomNo", RoomNo)
                Dim Available As Integer = Convert.ToInt32(CmdCheck.ExecuteScalar())
                If Available > 0 Then
                    OpenFormByName(CheckInFormName)
                Else
                    OpenFormByName(CheckOutFormName)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class