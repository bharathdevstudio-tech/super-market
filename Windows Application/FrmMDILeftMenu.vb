Imports System.IO
Imports System.Data.SqlClient

Public Class FrmMDILeftMenu
    Dim BVistawinSolution1 As New BVistawinSolution
    Dim Trans As SqlTransaction
    Dim Adapter As New SqlDataAdapter
    Dim Dataset, Dataset1 As New WinDataSet
    Dim Ds, Ds1, Ds2 As New DataSet
    Dim Cmd, Cmd1, command As New SqlCommand

    Public UserNameChk As String

    Private pnlLeftMenu As Panel
    Private pnlMenuHeader As Panel
    Private pnlMenu As Panel
    Private btnMenuToggle As Button

    Private MenuTop As Integer = 0

    Private Const ExpandedWidth As Integer = 230
    Private Const CollapsedWidth As Integer = 55

    Private Const MainMenuHeight As Integer = 44
    Private Const SubMenuHeight As Integer = 36

    Private SidebarCollapsed As Boolean = False

    Private ExpandedParentPanel As Panel = Nothing
    Private ExpandedParentButton As Button = Nothing

    Private SelectedMenuButton As Button = Nothing
    Private Sub FrmMDICommon_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Dim info As New BVistawinSolution
        VistawinFunctions.backupmessage = True
        If info.dbfullbackup1(VistawinFunctions.ServerConnection) = True Then
            'MsgBox("Backup Successfully Created")
        Else
            'MsgBox("Backup Already Created")
        End If
        VistawinFunctions.backupmessage = False

    End Sub
    Private Sub FrmMDICommon_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        GrpDashBoard.Hide()

        MenuStrip1.Visible = False

        CreateLeftSidebar()
        LoadLeftMenu()

    
        If VistawinFunctions.ProjectTypechk = "CLINIC" Then
            Me.ClinicCmpDetail()
        ElseIf VistawinFunctions.ProjectTypechk = "LODGE" Then
            Me.InvenCmpDetail()
            Me.LodgeSettingDetail()
            ShowMain()
        ElseIf VistawinFunctions.ProjectTypechk = "FRUITS" Then
            Me.InvenCmpDetail()
        Else
            Me.InvenCmpDetail()
            Me.InvenSettingDetail()
            ShowMain()
        End If

        Timer1.Interval = 1000
        Timer1.Start()

        Dim iconPath As String = ""

        For Each drive As String In {"C:\", "D:\", "E:\", "F:\", "G:\"}
            Dim file As String = ""

            Select Case VistawinFunctions.CmpDatabaseName
                Case "HALHOTELDATA"
                    file = IO.Path.Combine(drive, "VISTAWIN\HTHLoginLogo.ico")
                Case "HDRHOTELDATA"
                    file = IO.Path.Combine(drive, "VISTAWIN\HDRLoginLogo.ico")
                Case "MAMMANSIONDATA"
                    file = IO.Path.Combine(drive, "VISTAWIN\MAMLoginLogo.ico")
                Case "BALAJICOMPLEXDBNEW"
                    file = IO.Path.Combine(drive, "VISTAWIN\letterb.ico")
                Case "MAMSITEDB"
                    file = IO.Path.Combine(drive, "VISTAWIN\MAM2Logo.ico")
            End Select

            If file <> "" AndAlso IO.File.Exists(file) Then
                iconPath = file
                Exit For
            End If
        Next

        If iconPath <> "" Then
            Me.Icon = New Icon(iconPath)
        End If

        EnsureVistawinFolders()

    End Sub
    Private Sub CreateLeftSidebar()

        pnlLeftMenu = New Panel()

        pnlLeftMenu.Name = "pnlLeftMenu"

        pnlLeftMenu.Dock = DockStyle.Left

        pnlLeftMenu.Width = ExpandedWidth

        pnlLeftMenu.BackColor =
            Color.FromArgb(32, 42, 52)

        Me.Controls.Add(pnlLeftMenu)


        '-----------------------------
        ' HEADER
        '-----------------------------

        pnlMenuHeader = New Panel()

        pnlMenuHeader.Name = "pnlMenuHeader"

        pnlMenuHeader.Dock = DockStyle.Top

        pnlMenuHeader.Height = 50

        pnlMenuHeader.BackColor =
            Color.FromArgb(25, 35, 45)

        pnlLeftMenu.Controls.Add(pnlMenuHeader)


        '-----------------------------
        ' MENU TOGGLE
        '-----------------------------

        btnMenuToggle = New Button()

        btnMenuToggle.Name = "btnMenuToggle"

        btnMenuToggle.Text = "☰"

        btnMenuToggle.Width = 50

        btnMenuToggle.Height = 50

        btnMenuToggle.Left = 0

        btnMenuToggle.Top = 0

        btnMenuToggle.FlatStyle = FlatStyle.Flat

        btnMenuToggle.FlatAppearance.BorderSize = 0

        btnMenuToggle.Font =
            New Font("Segoe UI Symbol", 16.0!, FontStyle.Bold)

        btnMenuToggle.ForeColor = Color.White

        btnMenuToggle.BackColor =
            Color.FromArgb(25, 35, 45)

        btnMenuToggle.Cursor = Cursors.Hand

        AddHandler btnMenuToggle.Click,
                  AddressOf MenuToggleClick

        pnlMenuHeader.Controls.Add(btnMenuToggle)


        '-----------------------------
        ' MENU PANEL
        '-----------------------------

        pnlMenu = New Panel()

        pnlMenu.Name = "pnlMenu"

        pnlMenu.Dock = DockStyle.Fill

        pnlMenu.AutoScroll = True

        pnlMenu.BackColor =
            Color.FromArgb(32, 42, 52)

        pnlLeftMenu.Controls.Add(pnlMenu)

        pnlMenu.BringToFront()

    End Sub
    Private Sub LoadLeftMenu()

        If pnlMenu Is Nothing Then Exit Sub

        pnlMenu.Controls.Clear()

        MenuTop = 0

        Dim Dt As New DataTable

        Dim Sql As String = ""

        Sql = "SELECT M.* FROM tbl_MenuMaster M " & _
              "INNER JOIN tbl_UserRoleMenu URM " & _
              "ON M.MenuID = URM.URM_MenuID_i " & _
              "INNER JOIN tbl_UserList UR " & _
              "ON URM.URM_EmpID_i = UR.U_EmpID_i " & _
              "WHERE Menu_Show_vc='YES' AND UR.U_UserName_vc=@Role " & _
              "ORDER BY MenuNameOrder,FormNameOrder"

        Using Cmd As New SqlCommand(Sql, _
                                    VistawinFunctions.ServerConnection)

            Cmd.Parameters.AddWithValue("@Role", UserNameChk)

            Dim Da As New SqlDataAdapter(Cmd)

            Da.Fill(Dt)

        End Using


        For Each Dr As DataRow In _
            Dt.Select("ParentID=0", _
                     "MenuNameOrder,FormNameOrder")

            Dim MenuName As String = _
                Dr("MenuName").ToString().Trim()

            If MenuName.StartsWith("LineChk", _
                                   StringComparison.OrdinalIgnoreCase) Then
                Continue For
            End If


            Dim MenuID As Integer = _
                Convert.ToInt32(Dr("MenuID"))


            Dim ChildRows() As DataRow = _
                Dt.Select("ParentID=" & MenuID, _
                         "MenuNameOrder,FormNameOrder")


            '---------------------------------
            ' PARENT BUTTON
            '---------------------------------

            Dim ParentButton As Button = _
                CreateParentButton(Dr, _
                                   ChildRows.Length > 0)

            ParentButton.Top = MenuTop

            pnlMenu.Controls.Add(ParentButton)

            MenuTop += MainMenuHeight


            '---------------------------------
            ' CHILD PANEL
            '---------------------------------

            If ChildRows.Length > 0 Then

                Dim ChildPanel As Panel = _
                    CreateChildPanel(Dr, Dt)

                ChildPanel.Top = MenuTop

                pnlMenu.Controls.Add(ChildPanel)

                ChildPanel.Visible = False


                '---------------------------------
                ' IMPORTANT
                ' Save ChildPanel + Original MenuName
                '---------------------------------

                'ParentButton.Tag = _
                '    Dr("FormName").ToString() & _
                '    "|" & _
                '    Dr("FormState").ToString() & _
                '    "|PARENT|" & _
                '    ChildPanel.Name & _
                '    "|" & _
                '    MenuName

                ParentButton.Tag = _
    Dr("FormName").ToString() & _
    "|" & _
    Dr("FormState").ToString() & _
    "|PARENT|" & _
    ChildPanel.Name & _
    "|" & _
    MenuName

            Else

                '---------------------------------
                ' No Child
                '---------------------------------

                ParentButton.Tag = _
                    Dr("FormName").ToString() & _
                    "|" & _
                    Dr("FormState").ToString() & _
                    "|MENU|" & _
                    MenuName

            End If

        Next

    End Sub
    Private Function CreateParentButton(ByVal Dr As DataRow,
                                    ByVal HasChild As Boolean) As Button

        Dim Btn As New Button()

        Dim MenuName As String = _
            Dr("MenuName").ToString().Trim()


        '---------------------------------
        ' BUTTON TEXT
        '---------------------------------

        If HasChild Then

            Btn.Text = "  " & _
                       GetMenuIcon(MenuName) & _
                       "  " & _
                       MenuName & _
                       "        ▼"

        Else

            Btn.Text = "  " & _
                       GetMenuIcon(MenuName) & _
                       "  " & _
                       MenuName

        End If


        '---------------------------------
        ' BUTTON NAME
        '---------------------------------

        Btn.Name = _
            "btnMenu_" & _
            Dr("MenuID").ToString()


        '---------------------------------
        ' SIZE
        '---------------------------------

        Btn.Width = ExpandedWidth - 5

        Btn.Height = MainMenuHeight

        Btn.Left = 0


        '---------------------------------
        ' STYLE
        '---------------------------------

        Btn.FlatStyle = FlatStyle.Flat

        Btn.FlatAppearance.BorderSize = 0

        Btn.TextAlign = _
            ContentAlignment.MiddleLeft

        Btn.Font = _
            New Font("Segoe UI", 10.0!, FontStyle.Bold)

        Btn.ForeColor = Color.White

        Btn.BackColor = _
            Color.FromArgb(32, 42, 52)

        Btn.Cursor = Cursors.Hand


        '---------------------------------
        ' IMPORTANT
        ' Original Menu Name
        '---------------------------------

        If HasChild Then

            'Temporary Tag
            'LoadLeftMenu() will add ChildPanel.Name
            Btn.Tag = _
                Dr("FormName").ToString() & _
                "|" & _
                Dr("FormState").ToString() & _
                "|PARENT||" & _
                MenuName

        Else

            Btn.Tag = _
                Dr("FormName").ToString() & _
                "|" & _
                Dr("FormState").ToString() & _
                "|MENU|" & _
                MenuName

        End If


        '---------------------------------
        ' EVENTS
        '---------------------------------

        AddHandler Btn.Click, _
                  AddressOf ParentMenuClick

        AddHandler Btn.MouseEnter, _
                  AddressOf MenuMouseEnter

        AddHandler Btn.MouseLeave, _
                  AddressOf MenuMouseLeave


        Return Btn

    End Function
    Private Function GetMenuIcon(ByVal MenuName As String) As String

        Dim N As String =
            MenuName.Trim().ToUpper()

        If N.Contains("MASTER") Then
            Return "◆"

        ElseIf N.Contains("TRANSACTION") Then
            Return "▣"

        ElseIf N.Contains("REPORT") Then
            Return "▤"

        ElseIf N.Contains("SETTING") Then
            Return "⚙"

        ElseIf N.Contains("DASH") Then
            Return "⌂"

        ElseIf N.Contains("SALE") Then
            Return "₹"

        ElseIf N.Contains("PURCHASE") Then
            Return "▣"

        ElseIf N.Contains("CUSTOMER") Then
            Return "●"

        ElseIf N.Contains("SUPPLIER") Then
            Return "●"

        ElseIf N.Contains("PRODUCT") Then
            Return "◆"

        Else
            Return "•"
        End If

    End Function
    Private Function CreateChildPanel(ByVal ParentDr As DataRow,
                                   ByVal Dt As DataTable) As Panel

        Dim Pnl As New Panel()

        Pnl.Name =
            "ChildPanel_" &
            ParentDr("MenuID").ToString()

        Pnl.Left = 0

        Pnl.Width = ExpandedWidth - 5

        Pnl.BackColor =
            Color.FromArgb(42, 52, 62)


        Dim ChildTop As Integer = 0

        AddChildButtons(Pnl,
                        Dt,
                        Convert.ToInt32(ParentDr("MenuID")),
                        ChildTop,
                        0)


        Pnl.Height = ChildTop

        Return Pnl

    End Function
    Private Sub AddChildButtons(ByVal Pnl As Panel,
                            ByVal Dt As DataTable,
                            ByVal ParentID As Integer,
                            ByRef ChildTop As Integer,
                            ByVal Level As Integer)

        Dim Rows() As DataRow =
            Dt.Select("ParentID=" & ParentID,
                      "MenuNameOrder,FormNameOrder")


        For Each Dr As DataRow In Rows

            Dim MenuName As String =
                Dr("MenuName").ToString().Trim()


            If MenuName.StartsWith("LineChk",
                                   StringComparison.OrdinalIgnoreCase) Then

                Dim Sep As New Label()

                Sep.Left = 15

                Sep.Top = ChildTop + 3

                Sep.Width = Pnl.Width - 30

                Sep.Height = 1

                Sep.BackColor = Color.Gray

                Pnl.Controls.Add(Sep)

                ChildTop += 8

                Continue For

            End If


            Dim MenuID As Integer =
                Convert.ToInt32(Dr("MenuID"))


            Dim GrandRows() As DataRow =
                Dt.Select("ParentID=" & MenuID,
                         "MenuNameOrder,FormNameOrder")


            Dim Btn As New Button()


            If GrandRows.Length > 0 Then

                Btn.Text =
                    Space(4 + Level * 2) &
                    MenuName &
                    "        ▼"

            Else

                Btn.Text =
                    Space(6 + Level * 2) &
                    MenuName

            End If


            Btn.Name =
                "btnSubMenu_" &
                Dr("MenuID").ToString()


            Btn.Tag =
                Dr("FormName").ToString() &
                "|" &
                Dr("FormState").ToString()


            Btn.Left = 0

            Btn.Top = ChildTop

            Btn.Width = Pnl.Width

            Btn.Height = SubMenuHeight


            Btn.FlatStyle = FlatStyle.Flat

            Btn.FlatAppearance.BorderSize = 0


            Btn.TextAlign =
                ContentAlignment.MiddleLeft


            Btn.Font =
                New Font("Segoe UI", 9.0!, FontStyle.Regular)


            Btn.ForeColor = Color.Gainsboro

            Btn.BackColor =
                Color.FromArgb(42, 52, 62)


            Btn.Cursor = Cursors.Hand


            AddHandler Btn.Click,
                      AddressOf ChildMenuClick

            AddHandler Btn.MouseEnter,
                      AddressOf MenuMouseEnter

            AddHandler Btn.MouseLeave,
                      AddressOf MenuMouseLeave


            Pnl.Controls.Add(Btn)


            ChildTop += SubMenuHeight


            'Grand Child

            If GrandRows.Length > 0 Then

                AddChildButtons(Pnl,
                                Dt,
                                MenuID,
                                ChildTop,
                                Level + 1)

            End If

        Next

    End Sub
    Private Sub ParentMenuClick(ByVal sender As Object,
                            ByVal e As EventArgs)

        Dim Btn As Button = CType(sender, Button)

        If Btn.Tag Is Nothing Then Exit Sub

        Dim Parts() As String = _
            Btn.Tag.ToString().Split("|"c)

        If Parts.Length < 4 Then Exit Sub

        Dim MenuType As String = Parts(2).ToString().ToUpper().Trim()


        '=================================================
        ' MENU WITHOUT CHILD
        ' Direct Form Open
        '=================================================

        If MenuType = "MENU" Then

            SetSelectedMenu(Btn)

            OpenMenuForm( _
                Parts(0).ToString(), _
                Parts(1).ToString())

            Exit Sub

        End If


        '=================================================
        ' PARENT MENU
        '=================================================

        If MenuType <> "PARENT" Then Exit Sub

        If Parts.Length < 5 Then Exit Sub

        Dim ChildPanelName As String = Parts(3).ToString()

        Dim ChildPanel As Panel = Nothing

        For Each C As Control In pnlMenu.Controls

            If TypeOf C Is Panel Then

                If C.Name = ChildPanelName Then

                    ChildPanel = CType(C, Panel)

                    Exit For

                End If

            End If

        Next


        If ChildPanel Is Nothing Then Exit Sub


        '=================================================
        ' SAME PARENT CLICK
        ' Collapse
        '=================================================

        If ExpandedParentPanel Is ChildPanel Then

            CloseExpandedParent()

            Exit Sub

        End If


        '=================================================
        ' CLOSE PREVIOUS PARENT
        '=================================================

        CloseExpandedParent()


        '=================================================
        ' OPEN NEW PARENT
        '=================================================

        ChildPanel.Visible = True

        ExpandedParentPanel = ChildPanel

        ExpandedParentButton = Btn


        ' Arrow change
        Dim MenuName As String = _
            GetOriginalMenuName(Btn)

        Btn.Text = "  " & _
                   GetMenuIcon(MenuName) & _
                   "  " & _
                   MenuName & _
                   "        ▲"


        RearrangeLeftMenu()

    End Sub

    Private Sub CloseExpandedParent()

        If ExpandedParentPanel Is Nothing Then Exit Sub

        ExpandedParentPanel.Visible = False


        If ExpandedParentButton IsNot Nothing Then

            Dim MenuName As String = _
                GetOriginalMenuName(ExpandedParentButton)

            ExpandedParentButton.Text = _
                "  " & _
                GetMenuIcon(MenuName) & _
                "  " & _
                MenuName & _
                "        ▼"

        End If


        ExpandedParentPanel = Nothing

        ExpandedParentButton = Nothing

        RearrangeLeftMenu()

    End Sub
    Private Sub RearrangeLeftMenu()

        Dim CurrentTop As Integer = 0


        For Each C As Control In pnlMenu.Controls

            C.Top = CurrentTop

            CurrentTop += C.Height


            If TypeOf C Is Panel Then

                Dim P As Panel =
                    CType(C, Panel)

                If Not P.Visible Then

                    CurrentTop -= P.Height

                End If

            End If

        Next

    End Sub
    Private Sub ChildMenuClick(ByVal sender As Object,
                           ByVal e As EventArgs)

        Dim Btn As Button =
            CType(sender, Button)


        If Btn.Tag Is Nothing Then Exit Sub


        Dim Arr() As String =
            Btn.Tag.ToString().Split("|"c)


        Dim FormName As String =
            Arr(0).Trim()


        Dim FormState As String = "Normal"


        If Arr.Length > 1 Then

            FormState =
                Arr(1).Trim()

        End If


        If FormName = "" Then Exit Sub


        'Selected child

        SetSelectedMenu(Btn)


        OpenMenuForm(FormName,
                     FormState)

    End Sub
    Private Sub SetSelectedMenu(ByVal Btn As Button)

        If SelectedMenuButton IsNot Nothing Then

            SelectedMenuButton.BackColor =
                Color.FromArgb(42, 52, 62)

        End If


        SelectedMenuButton = Btn


        Btn.BackColor =
            Color.FromArgb(55, 75, 95)


        'Blue left indicator

        AddSelectedIndicator(Btn)

    End Sub
    Private Sub AddSelectedIndicator(ByVal Btn As Button)

        Dim IndicatorName As String =
            "SelectedIndicator_" &
            Btn.Name


        For Each C As Control In Btn.Controls

            If C.Name = IndicatorName Then

                Return

            End If

        Next


        Dim Indicator As New Panel()

        Indicator.Name = IndicatorName

        Indicator.Dock = DockStyle.Left

        Indicator.Width = 4

        Indicator.BackColor =
            Color.FromArgb(30, 144, 255)


        Btn.Controls.Add(Indicator)

        Indicator.BringToFront()

    End Sub
    Private Sub MenuMouseEnter(ByVal sender As Object,
                           ByVal e As EventArgs)

        Dim Btn As Button =
            CType(sender, Button)


        If Btn Is SelectedMenuButton Then

            Btn.BackColor =
                Color.FromArgb(55, 75, 95)

        Else

            Btn.BackColor =
                Color.FromArgb(50, 65, 80)

        End If

    End Sub
    Private Sub MenuMouseLeave(ByVal sender As Object,
                           ByVal e As EventArgs)

        Dim Btn As Button =
            CType(sender, Button)


        If Btn Is SelectedMenuButton Then

            Btn.BackColor =
                Color.FromArgb(55, 75, 95)

        Else

            If Btn.Name.StartsWith("btnMenu_") Then

                If Btn Is ExpandedParentButton Then

                    Btn.BackColor =
                        Color.FromArgb(45, 95, 150)

                Else

                    Btn.BackColor =
                        Color.FromArgb(32, 42, 52)

                End If

            Else

                Btn.BackColor =
                    Color.FromArgb(42, 52, 62)

            End If

        End If

    End Sub
    Private Function RemoveArrow(ByVal TextValue As String) As String

        TextValue =
            TextValue.Replace("▼", "")

        TextValue =
            TextValue.Replace("▲", "")

        Return TextValue.TrimEnd()

    End Function
    Private Sub MenuToggleClick(ByVal sender As Object,
                            ByVal e As EventArgs)

        If SidebarCollapsed = False Then

            SidebarCollapsed = True

            pnlLeftMenu.Width =
                CollapsedWidth

            btnMenuToggle.Text = "☰"

            HideMenuTexts()

        Else

            SidebarCollapsed = False

            pnlLeftMenu.Width =
                ExpandedWidth

            btnMenuToggle.Text = "☰"

            ShowMenuTexts()

        End If

    End Sub
    Private Sub HideMenuTexts()

        For Each C As Control In pnlMenu.Controls

            If TypeOf C Is Button Then

                Dim Btn As Button =
                    CType(C, Button)

                Dim MenuName As String =
                    GetOriginalMenuName(Btn)


                Btn.Width =
                    CollapsedWidth - 5

                Btn.Text =
                    GetMenuIcon(MenuName)

                Btn.TextAlign =
                    ContentAlignment.MiddleCenter

            ElseIf TypeOf C Is Panel Then

                C.Visible = False

            End If

        Next

    End Sub
    Private Function GetCollapsedIcon(ByVal Btn As Button) As String

        Dim T As String =
            Btn.Text.Trim()


        If T.Contains("Master") Then
            Return "◆"

        ElseIf T.Contains("Transaction") Then
            Return "▣"

        ElseIf T.Contains("Reports") Then
            Return "▤"

        ElseIf T.Contains("Settings") Then
            Return "⚙"

        Else
            Return "•"

        End If

    End Function
    Private Function GetOriginalMenuName(ByVal Btn As Button) As String

        If Btn Is Nothing Then Return ""

        If Btn.Tag Is Nothing Then
            Return Btn.Text.Trim()
        End If

        Dim Parts() As String = _
            Btn.Tag.ToString().Split("|"c)

        ' PARENT
        ' FormName|FormState|PARENT|ChildPanel|MenuName
        If Parts.Length >= 5 Then
            Return Parts(4)
        End If

        ' MENU
        ' FormName|FormState|MENU|MenuName
        If Parts.Length >= 4 Then
            Return Parts(3)
        End If

        Return Btn.Text.Trim()

    End Function
   
    Private Sub ShowMenuTexts()

        For Each C As Control In pnlMenu.Controls

            If TypeOf C Is Button Then

                Dim Btn As Button =
                    CType(C, Button)

                Dim MenuName As String =
                    GetOriginalMenuName(Btn)


                Btn.Width =
                    ExpandedWidth - 5


                If Btn.Name.StartsWith("btnMenu_") Then

                    Btn.Text =
                        "  " &
                        GetMenuIcon(MenuName) &
                        "  " &
                        MenuName &
                        "   ▼"

                Else

                    Btn.Text =
                        "      " &
                        MenuName

                End If


                Btn.TextAlign =
                    ContentAlignment.MiddleLeft

            End If

        Next

        RearrangeLeftMenu()

    End Sub
    Private Sub OpenMenuForm(ByVal FormName As String,
                         ByVal FormState As String)

        If FormName = "FrmLodCmpProfile" OrElse
           FormName = "FrmCmpProfile" OrElse
           FormName = "FrmCmpProfileInven" OrElse
           FormName = "FrmBarCompanyProfile" Then

            If BVistawinSolution1.Permission() = False Then
                Exit Sub
            End If

        End If

        If FormName = "DashBoard" Then

            GrpDashBoard.Show()
            GrpDashBoard.Dock = DockStyle.Fill

            RoomOccupiedDetails()
            BlocksOccupiedDtl()

            Exit Sub

        End If

        Dim FrmType As Type =
            Application.OpenForms(0).GetType().Assembly.GetType(
            Application.ProductName & "." & FormName)

        If FrmType Is Nothing Then

            MsgBox("Form Not Found : " & FormName)

            Exit Sub

        End If

        For Each F As Form In Me.MdiChildren

            If F.GetType Is FrmType Then

                F.Activate()

                If FormState.ToUpper() = "MAXIMIZED" Then
                    F.WindowState = FormWindowState.Maximized
                End If

                Exit Sub

            End If

        Next

        Dim Frm As Form =
            CType(Activator.CreateInstance(FrmType), Form)

        Frm.MdiParent = Me

        Select Case FormState.ToUpper()

            Case "MAXIMIZED"

                Frm.WindowState = FormWindowState.Maximized

            Case "MINIMIZED"

                Frm.WindowState = FormWindowState.Minimized

            Case Else

                Frm.WindowState = FormWindowState.Normal

        End Select

        Frm.Show()

    End Sub
    Private Sub EnsureVistawinFolders()
        Try
            Dim backupPath As String = VistawinFunctions.BackupPath
            Dim scanningPath As String = VistawinFunctions.ScanningPath

            Dim CusBillChk As String = System.IO.Path.Combine(scanningPath, "Cus_Bill")
            Dim CusImageChk As String = System.IO.Path.Combine(scanningPath, "Cus_Image")
            Dim ExpImageChk As String = System.IO.Path.Combine(scanningPath, "Exp_Image")
            Dim ProImageChk As String = System.IO.Path.Combine(scanningPath, "Pro_Image")
            Dim DisImageChk As String = System.IO.Path.Combine(scanningPath, "Disease_Image")
            Dim PlanImgChk As String = System.IO.Path.Combine(scanningPath, "Pat_PlanImg")

            Dim VistawinPath As String = System.IO.Directory.GetParent(scanningPath).FullName
            Dim SourceLogin As String = System.IO.Path.Combine(Application.StartupPath, "loginimg.jpg")
            Dim SourceTreat As String = System.IO.Path.Combine(Application.StartupPath, "TreatNothingimg.jpg")

            Dim DestLogin As String = System.IO.Path.Combine(VistawinPath, "loginimg.jpg")
            Dim DestTreat As String = System.IO.Path.Combine(VistawinPath, "TreatNothingimg.jpg")

            Select Case VistawinFunctions.ProjectTypechk
                Case "CLINIC"

                    If Not System.IO.Directory.Exists(DisImageChk) Then
                        System.IO.Directory.CreateDirectory(DisImageChk)
                    End If

                    If Not System.IO.Directory.Exists(PlanImgChk) Then
                        System.IO.Directory.CreateDirectory(PlanImgChk)
                    End If

                    If System.IO.File.Exists(SourceTreat) Then
                        If Not System.IO.File.Exists(DestTreat) Then
                            System.IO.File.Copy(SourceTreat, DestTreat, False)
                        End If
                    End If

                Case "INVEN", "FRUITS"

                    If Not System.IO.Directory.Exists(ProImageChk) Then
                        System.IO.Directory.CreateDirectory(ProImageChk)
                    End If

            End Select

            If Not System.IO.Directory.Exists(backupPath) Then
                System.IO.Directory.CreateDirectory(backupPath)
            End If

            If Not System.IO.Directory.Exists(scanningPath) Then
                System.IO.Directory.CreateDirectory(scanningPath)
            End If

            If Not System.IO.Directory.Exists(CusBillChk) Then
                System.IO.Directory.CreateDirectory(CusBillChk)
            End If

            If Not System.IO.Directory.Exists(CusImageChk) Then
                System.IO.Directory.CreateDirectory(CusImageChk)
            End If

            If Not System.IO.Directory.Exists(ExpImageChk) Then
                System.IO.Directory.CreateDirectory(ExpImageChk)
            End If

            If System.IO.File.Exists(SourceLogin) Then
                If Not System.IO.File.Exists(DestLogin) Then
                    System.IO.File.Copy(SourceLogin, DestLogin, False)
                End If
            End If

        Catch ex As Exception
            MessageBox.Show("Unable to create VISTAWIN folders." & vbCrLf & ex.Message, "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Public Sub ShowMain()

        Select Case VistawinFunctions.CmpDatabaseName
            Case "HUSSAINLODGETVLDB", "MAMSITEDB", "HOTELAVLODGETSIDB", "HOTELLGLODGECOUMDB", "BALAJICOMPLEXDBNEW"
                ShowUniversalBookView()
            Case "MAMPLAYSCHOOLDB"
                PSShowMain()
            Case "JMMAHALDATA"
                JMahalShowMain()
            Case "KNLSBARNEWDB"
                BarShowMain()
            Case "BALAJIMANSIONTVLDB"
                BMShowMain()
        End Select
    End Sub
    Private Sub ShowUniversalBookView()
        Try
            Dim Frm As FrmUniversalBookView = Nothing

            For Each F As Form In Me.MdiChildren
                If TypeOf F Is FrmUniversalBookView Then
                    Frm = DirectCast(F, FrmUniversalBookView)
                    Exit For
                End If
            Next

            If Frm Is Nothing Then
                Frm = New FrmUniversalBookView()
                Frm.MdiParent = Me
                Frm.WindowState = FormWindowState.Maximized
                Frm.Show()
            Else
                Frm.WindowState = FormWindowState.Maximized
                Frm.Activate()
                Frm.BringToFront()
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub
    Private Sub ClinicCmpDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_Companyprofile", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.TodayDate = Date.Now
        VistawinFunctions.CmpName = If(IsDBNull(dr("Cmp_Name_vc")), "", dr("Cmp_Name_vc").ToString())
        VistawinFunctions.CmpAddress = If(IsDBNull(dr("Cmp_Address_vc")), "", dr("Cmp_Address_vc").ToString())
        VistawinFunctions.CmpCaption = If(IsDBNull(dr("Cmp_Caption_vc")), "", dr("Cmp_Caption_vc").ToString())
        VistawinFunctions.CmpDistrict = If(IsDBNull(dr("Cmp_District_vc")), "", dr("Cmp_District_vc").ToString())
        VistawinFunctions.CmpState = If(IsDBNull(dr("Cmp_State_vc")), "", dr("Cmp_State_vc").ToString())
        VistawinFunctions.CmpPhCode = If(IsDBNull(dr("Cmp_Pincode_vc")), "", dr("Cmp_Pincode_vc").ToString())
        VistawinFunctions.CmpMobileNo = If(IsDBNull(dr("Cmp_MobileNo_n")), "", dr("Cmp_MobileNo_n").ToString())
        VistawinFunctions.CmpEmailID = If(IsDBNull(dr("Cmp_Email_vc")), "", dr("Cmp_Email_vc").ToString())
        VistawinFunctions.CmpWebSite = If(IsDBNull(dr("Cmp_Website_vc")), "", dr("Cmp_Website_vc").ToString())
        VistawinFunctions.CmpPhoneNo = If(IsDBNull(dr("Cmp_PhoneNo_vc")), "", dr("Cmp_PhoneNo_vc").ToString())
        VistawinFunctions.CmpProprietor = If(IsDBNull(dr("Cmp_Proprietor_vc")), "", dr("Cmp_Proprietor_vc").ToString())
        VistawinFunctions.CmpLicenseNo = If(IsDBNull(dr("Cmp_LicenseNo_vc")), "", dr("Cmp_LicenseNo_vc").ToString())
        VistawinFunctions.MastPassword = If(IsDBNull(dr("Mast_Password_vc")), "", dr("Mast_Password_vc").ToString())
        VistawinFunctions.BackupPath = If(IsDBNull(dr("BackupPath_vc")), "", dr("BackupPath_vc").ToString())
        VistawinFunctions.ScanningPath = If(IsDBNull(dr("ScanningPath_vc")), "", dr("ScanningPath_vc").ToString())
        VistawinFunctions.SendSMS = If(IsDBNull(dr("SendSMS_vc")), "", dr("SendSMS_vc").ToString())
        VistawinFunctions.SMSSenderID = If(IsDBNull(dr("SMS_SenderID_vc")), "", dr("SMS_SenderID_vc").ToString())
        VistawinFunctions.SMSUserID = If(IsDBNull(dr("SMS_UserID_vc")), "", dr("SMS_UserID_vc").ToString())
        VistawinFunctions.SMSPassword = If(IsDBNull(dr("SMS_Password_vc")), "", dr("SMS_Password_vc").ToString())
        VistawinFunctions.CmpServerName = If(IsDBNull(dr("Cmp_ServerName_vc")), "", dr("Cmp_ServerName_vc").ToString())
        VistawinFunctions.CmpDatabaseName = If(IsDBNull(dr("Cmp_DataBaseName_vc")), "", dr("Cmp_DataBaseName_vc").ToString())
        VistawinFunctions.CmpBankDetails = If(IsDBNull(dr("Cmp_BankDetails_vc")), "", dr("Cmp_BankDetails_vc").ToString())
        VistawinFunctions.CmpMovingWord = If(IsDBNull(dr("Moving_Word_vc")), "", dr("Moving_Word_vc").ToString())
        VistawinFunctions.CmpReportHead = If(IsDBNull(dr("Cmp_ReportHead_vc")), "", dr("Cmp_ReportHead_vc").ToString())
        VistawinFunctions.CmpPuGST = If(IsDBNull(dr("Cmp_PurGST_vc")), "", dr("Cmp_PurGST_vc").ToString())
        VistawinFunctions.CmpPuRound = If(IsDBNull(dr("Cmp_PurRound_vc")), "", dr("Cmp_PurRound_vc").ToString())
        VistawinFunctions.CmpSaRound = If(IsDBNull(dr("Cmp_SaleRound_vc")), "", dr("Cmp_SaleRound_vc").ToString())
        VistawinFunctions.CmpPAutoRound = If(IsDBNull(dr("Cmp_PAutoRound_vc")), "", dr("Cmp_PAutoRound_vc").ToString())
        VistawinFunctions.CmpSAutoRound = If(IsDBNull(dr("Cmp_SAutoRound_vc")), "", dr("Cmp_SAutoRound_vc").ToString())
        VistawinFunctions.CmpBarcode = If(IsDBNull(dr("Cmp_Barcode_vc")), "", dr("Cmp_Barcode_vc").ToString())
        VistawinFunctions.CmpToEmail = If(IsDBNull(dr("Cmp_ToMail_vc")), "", dr("Cmp_ToMail_vc").ToString())
        VistawinFunctions.PrintingType = If(IsDBNull(dr("BillingType_vc")), "DEFAULT", dr("BillingType_vc").ToString())
        VistawinFunctions.SaveImage = If(IsDBNull(dr("Cmp_SaveImage_vc")), "NO", dr("Cmp_SaveImage_vc").ToString())
        VistawinFunctions.CmpBZS = If(IsDBNull(dr("Bill_ZeroStock_vc")), "NO", dr("Bill_ZeroStock_vc").ToString())
        VistawinFunctions.CmpBAfP = If(IsDBNull(dr("Bill_AftPrint_vc")), "NO", dr("Bill_AftPrint_vc").ToString())
        VistawinFunctions.GStCal = If(IsDBNull(dr("Cmp_GSTCal_vc")), "INCLUDING", dr("Cmp_GSTCal_vc").ToString())
        VistawinFunctions.CmpExpDays = If(IsDBNull(dr("Cmp_ExpDays_i")), 0, Convert.ToInt32(dr("Cmp_ExpDays_i")))
        VistawinFunctions.CmpPharmacyName = If(IsDBNull(dr("Cmp_PharmacyName_vc")), "", dr("Cmp_PharmacyName_vc").ToString())
        VistawinFunctions.CmpNote = If(IsDBNull(dr("Cmp_Note_vc")), "", dr("Cmp_Note_vc").ToString())
        VistawinFunctions.ServerApiChk = If(IsDBNull(dr("cmp_ServerApi_vc")), "", dr("cmp_ServerApi_vc").ToString())

        If Not IsDBNull(dr("Cmp_Logo_im")) Then
            VistawinFunctions.CmpLogo = DirectCast(dr("Cmp_Logo_im"), Byte())
        End If

        If Not IsDBNull(dr("Cmp_BackImage_im")) Then
            Try
                Dim imgBytes() As Byte = DirectCast(dr("Cmp_BackImage_im"), Byte())
                If Me.BackgroundImage IsNot Nothing Then
                    Me.BackgroundImage.Dispose()
                    Me.BackgroundImage = Nothing
                End If

                Using ms As New MemoryStream(imgBytes)
                    Using img As Image = Image.FromStream(ms)
                        Me.BackgroundImage = New Bitmap(img)
                    End Using
                End Using
                VistawinFunctions.CmpBackImage = imgBytes
            Catch ex As Exception
                'MessageBox.Show("Invalid background image." & vbCrLf & ex.Message)
                Me.BackgroundImage = Nothing
            End Try
        Else
            Me.BackgroundImage = Nothing
        End If
    End Sub
    Private Sub ClinicSettingDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_Medi_GeneralSettings", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.StProAny = If(IsDBNull(dr("St_ProAny_vc")), "YES", dr("St_ProAny_vc").ToString())
        VistawinFunctions.PurProAny = If(IsDBNull(dr("Pur_ProAny_vc")), "YES", dr("Pur_ProAny_vc").ToString())
        VistawinFunctions.SalProAny = If(IsDBNull(dr("Sal_ProAny_vc")), "YES", dr("Sal_ProAny_vc").ToString())
        VistawinFunctions.SaleBill = If(IsDBNull(dr("GS_SaleBill_vc")), "PDF", dr("GS_SaleBill_vc").ToString())
        VistawinFunctions.FeesOpt = If(IsDBNull(dr("GS_FeesOpt_vc")), "PDF", dr("GS_FeesOpt_vc").ToString())
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ToolStripStatusLabel4.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")
    End Sub

    Private Sub ToolStripStatusLabel3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripStatusLabel3.Click
        Dim url As String = "https://vistawin.online/"
        Process.Start(url)
    End Sub
    Private Sub InvenCmpDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_Companyprofile", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.TodayDate = Date.Now
        VistawinFunctions.CmpName = If(IsDBNull(dr("Cmp_Name_vc")), "", dr("Cmp_Name_vc").ToString())
        VistawinFunctions.CmpAddress = If(IsDBNull(dr("Cmp_Address_vc")), "", dr("Cmp_Address_vc").ToString())
        VistawinFunctions.CmpCaption = If(IsDBNull(dr("Cmp_Caption_vc")), "", dr("Cmp_Caption_vc").ToString())
        VistawinFunctions.CmpDistrict = If(IsDBNull(dr("Cmp_District_vc")), "", dr("Cmp_District_vc").ToString())
        VistawinFunctions.CmpState = If(IsDBNull(dr("Cmp_State_vc")), "", dr("Cmp_State_vc").ToString())
        VistawinFunctions.CmpMobileNo = If(IsDBNull(dr("Cmp_MobileNo_n")), "", dr("Cmp_MobileNo_n").ToString())
        VistawinFunctions.CmpEmailID = If(IsDBNull(dr("Cmp_Email_vc")), "", dr("Cmp_Email_vc").ToString())
        VistawinFunctions.CmpPhoneNo = If(IsDBNull(dr("Cmp_PhoneNo_vc")), "", dr("Cmp_PhoneNo_vc").ToString())
        VistawinFunctions.CmpProprietor = If(IsDBNull(dr("Cmp_Proprietor_vc")), "", dr("Cmp_Proprietor_vc").ToString())
        VistawinFunctions.CmpLicenseNo = If(IsDBNull(dr("Cmp_LicenseNo_vc")), "", dr("Cmp_LicenseNo_vc").ToString())
        VistawinFunctions.MastPassword = If(IsDBNull(dr("Mast_Password_vc")), "", dr("Mast_Password_vc").ToString())
        VistawinFunctions.BackupPath = If(IsDBNull(dr("BackupPath_vc")), "", dr("BackupPath_vc").ToString())
        VistawinFunctions.ScanningPath = If(IsDBNull(dr("ScanningPath_vc")), "", dr("ScanningPath_vc").ToString())
        VistawinFunctions.CmpDatabaseName = If(IsDBNull(dr("Cmp_DataBaseName_vc")), "", dr("Cmp_DataBaseName_vc").ToString())
        VistawinFunctions.CmpBankDetails = If(IsDBNull(dr("Cmp_BankDetails_vc")), "", dr("Cmp_BankDetails_vc").ToString())
        VistawinFunctions.CmpMovingWord = If(IsDBNull(dr("Moving_Word_vc")), "", dr("Moving_Word_vc").ToString())
        VistawinFunctions.CmpReportHead = If(IsDBNull(dr("Cmp_ReportHead_vc")), "", dr("Cmp_ReportHead_vc").ToString())
        VistawinFunctions.CmpToEmail = If(IsDBNull(dr("Cmp_ToMail_vc")), "", dr("Cmp_ToMail_vc").ToString())
        VistawinFunctions.PrintingType = If(IsDBNull(dr("BillingType_vc")), "DEFAULT", dr("BillingType_vc").ToString())
        VistawinFunctions.SaveImage = If(IsDBNull(dr("Cmp_SaveImage_vc")), "NO", dr("Cmp_SaveImage_vc").ToString())
        VistawinFunctions.ServerApiChk = If(IsDBNull(dr("cmp_ServerApi_vc")), "", dr("cmp_ServerApi_vc").ToString())

        If VistawinFunctions.CmpDatabaseName <> "BALAJICOMPLEXDBNEW" And VistawinFunctions.CmpDatabaseName <> "KNLSBARNEWDB" Then

            VistawinFunctions.CmpBZS = If(IsDBNull(dr("Bill_ZeroStock_vc")), "NO", dr("Bill_ZeroStock_vc").ToString())
            VistawinFunctions.CmpBAfP = If(IsDBNull(dr("Bill_AftPrint_vc")), "NO", dr("Bill_AftPrint_vc").ToString())
            VistawinFunctions.GStCal = If(IsDBNull(dr("Cmp_GSTCal_vc")), "INCLUDING", dr("Cmp_GSTCal_vc").ToString())
            VistawinFunctions.CmpExpDays = If(IsDBNull(dr("Cmp_ExpDays_i")), 0, Convert.ToInt32(dr("Cmp_ExpDays_i")))
            VistawinFunctions.GDrivePath = If(IsDBNull(dr("Cmp_GDrivePath_vc")), "", dr("Cmp_GDrivePath_vc").ToString())
            VistawinFunctions.UpiName = If(IsDBNull(dr("Cmp_UpiName_vc")), "", dr("Cmp_UpiName_vc").ToString())
            VistawinFunctions.UpiID = If(IsDBNull(dr("Cmp_UpiID_vc")), "", dr("Cmp_UpiID_vc").ToString())
        Else
        End If

        If Not IsDBNull(dr("Cmp_Logo_im")) Then
            VistawinFunctions.CmpLogo = DirectCast(dr("Cmp_Logo_im"), Byte())
        End If

        If Not IsDBNull(dr("Cmp_BackImage_im")) Then
            Try
                Dim imgBytes() As Byte = DirectCast(dr("Cmp_BackImage_im"), Byte())
                If Me.BackgroundImage IsNot Nothing Then
                    Me.BackgroundImage.Dispose()
                    Me.BackgroundImage = Nothing
                End If

                Using ms As New MemoryStream(imgBytes)
                    Using img As Image = Image.FromStream(ms)
                        Me.BackgroundImage = New Bitmap(img)
                    End Using
                End Using
                VistawinFunctions.CmpBackImage = imgBytes
            Catch ex As Exception
                'MessageBox.Show("Invalid background image." & vbCrLf & ex.Message)
                Me.BackgroundImage = Nothing
            End Try
        Else
            Me.BackgroundImage = Nothing
        End If
    End Sub
    Private Sub InvenSettingDetail()


    End Sub
    Private Sub LodgeSettingDetail()

        Dim dt As New DataTable
        Using da As New SqlDataAdapter("SELECT TOP 1 * FROM tbl_GeneralSettings", VistawinFunctions.ServerConnection)
            da.Fill(dt)
        End Using

        If dt.Rows.Count = 0 Then Exit Sub

        Dim dr As DataRow = dt.Rows(0)

        VistawinFunctions.StayDay = If(IsDBNull(dr("GS_StayDelete_vc")), "SINGLE", dr("GS_StayDelete_vc").ToString())
        VistawinFunctions.ChkDateChange = If(IsDBNull(dr("GS_ChkOutDateChange_d")), "YES", dr("GS_ChkOutDateChange_d").ToString())
        VistawinFunctions.AdvZeroAdd = If(IsDBNull(dr("GS_AdvZeroAdd_vc")), "YES", dr("GS_AdvZeroAdd_vc").ToString())
        VistawinFunctions.HKFormshow = If(IsDBNull(dr("GS_HKFormshow_vc")), "YES", dr("GS_HKFormshow_vc").ToString())
        VistawinFunctions.HKEmp = If(IsDBNull(dr("GS_HKEmp_vc")), "NO", dr("GS_HKEmp_vc").ToString())
        VistawinFunctions.Supervisor = If(IsDBNull(dr("GS_Supervisor_vc")), "NO", dr("GS_Supervisor_vc").ToString())
        VistawinFunctions.RoomBoy = If(IsDBNull(dr("GS_RoomBoy_vc")), "NO", dr("GS_RoomBoy_vc").ToString())
        VistawinFunctions.GSTCol = If(IsDBNull(dr("GS_GSTCol_vc")), "YES", dr("GS_GSTCol_vc").ToString())
        VistawinFunctions.BankSel = If(IsDBNull(dr("Room_BankSel_vc")), "YES", dr("Room_BankSel_vc").ToString())
        VistawinFunctions.GraceType = If(IsDBNull(dr("Room_GraceType_vc")), "AFTER", dr("Room_GraceType_vc").ToString())
        VistawinFunctions.GraceTime = If(IsDBNull(dr("Room_GraceTime_vc")), "00,00", dr("Room_GraceTime_vc").ToString())
        VistawinFunctions.RofAmt = If(IsDBNull(dr("Sal_RofAmt_n")), "0.50", dr("Sal_RofAmt_n").ToString())
        VistawinFunctions.AdvBillCopy = If(IsDBNull(dr("Book_AdvBillCopy_i")), "1", dr("Book_AdvBillCopy_i").ToString())
        VistawinFunctions.CbVocImg = If(IsDBNull(dr("CB_VocImgShow_vc")), "NO", dr("CB_VocImgShow_vc").ToString())
        VistawinFunctions.ChkAutoAmt = If(IsDBNull(dr("Chkout_AutoAmt_vc")), "NO", dr("Chkout_AutoAmt_vc").ToString())
        VistawinFunctions.AdvBillForm = If(IsDBNull(dr("GS_AdvBill_vc")), "PDF", dr("GS_AdvBill_vc").ToString())
        VistawinFunctions.ChkoutBillForm = If(IsDBNull(dr("GS_ChkoutBill_vc")), "PDF", dr("GS_ChkoutBill_vc").ToString())
        VistawinFunctions.DBillForm = If(IsDBNull(dr("GS_DBill_vc")), "PDF", dr("GS_DBill_vc").ToString())
    End Sub
    Private Sub RoomOccupiedDetails()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT a.Room_Type_vc AS 'RoomType',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_Type_vc=a.Room_Type_vc AND Room_Available_vc='YES' AND Room_Block_vc='NO'),0) AS 'Available',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_Type_vc=a.Room_Type_vc AND Room_Available_vc='NO'),0) AS 'Occupied',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_Type_vc=a.Room_Type_vc AND Room_Block_vc='YES'),0) AS 'RoomBlockA',ISNULL(COUNT(a.Room_Number_vc),0) AS 'Total'  FROM tbl_RoomDetails a GROUP BY a.Room_Type_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "RoomDetails")
        DGVRoomDetails.Rows.Clear()
        If Ds.Tables("RoomDetails").Rows.Count > 0 Then
            Dim RoowIndex, Occupied1, Available1, RoomBlock1, Total1 As Decimal
            RoowIndex = 0
            Available1 = 0
            Occupied1 = 0
            RoomBlock1 = 0
            Total1 = 0
            For Each Dr As DataRow In Ds.Tables("RoomDetails").Rows
                DGVRoomDetails.Rows.Add()
                DGVRoomDetails.Rows(RoowIndex).Cells("SNo").Value = RoowIndex + 1
                DGVRoomDetails.Rows(RoowIndex).Cells("RoomType").Value = Dr.Item("RoomType")
                DGVRoomDetails.Rows(RoowIndex).Cells("Available").Value = Dr.Item("Available")
                DGVRoomDetails.Rows(RoowIndex).Cells("Occupied").Value = Dr.Item("Occupied")
                DGVRoomDetails.Rows(RoowIndex).Cells("RoomBlockA").Value = Dr.Item("RoomBlockA")
                DGVRoomDetails.Rows(RoowIndex).Cells("Total").Value = Dr.Item("Total")
                Available1 += Dr.Item("Available")
                Occupied1 += Dr.Item("Occupied")
                RoomBlock1 += Dr.Item("RoomBlockA")
                Total1 += Dr.Item("Total")
                RoowIndex += 1
            Next
            DGVRoomDetails.Rows.Add()
            DGVRoomDetails.Rows(RoowIndex).DefaultCellStyle.Font = New System.Drawing.Font("Bookman Old Style", 14.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            DGVRoomDetails.Rows(RoowIndex).DefaultCellStyle.ForeColor = Color.Red
            DGVRoomDetails.Rows(RoowIndex).Cells("RoomType").Style.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVRoomDetails.Rows(RoowIndex).Cells("RoomType").Value = "Total"
            DGVRoomDetails.Rows(RoowIndex).Cells("Available").Value = Available1
            DGVRoomDetails.Rows(RoowIndex).Cells("Occupied").Value = Occupied1
            DGVRoomDetails.Rows(RoowIndex).Cells("RoomBlockA").Value = RoomBlock1
            DGVRoomDetails.Rows(RoowIndex).Cells("Total").Value = Total1

            Dim SetGriHeisht2 As Decimal = 220
            For RoowIndex = 0 To DGVRoomDetails.Rows.Count - 1
                DGVRoomDetails.Rows(RoowIndex).Height += 15
                SetGriHeisht2 += 15
            Next
            DGVRoomDetails.Size = New System.Drawing.Size(620, SetGriHeisht2)
        End If
    End Sub
    Private Sub BlocksOccupiedDtl()

        Ds = New DataSet
        Adapter = New SqlDataAdapter("SELECT a.Room_BlockName_vc AS 'RoomTypeB',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_BlockName_vc=a.Room_BlockName_vc AND Room_Available_vc='YES' AND Room_Block_vc='NO'),0) AS 'AvailableB',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_BlockName_vc=a.Room_BlockName_vc AND Room_Available_vc='NO'),0) AS 'OccupiedB',ISNULL((SELECT COUNT(Room_Available_vc) FROM tbl_RoomDetails WHERE Room_BlockName_vc=a.Room_BlockName_vc AND Room_Block_vc='YES'),0) AS 'RoomBlockB',ISNULL(COUNT(a.Room_Number_vc),0) AS 'TotalB'  FROM tbl_RoomDetails a GROUP BY a.Room_BlockName_vc", VistawinFunctions.ServerConnection)
        Adapter.Fill(Ds, "FillDataB")
        DgvBlocksList.Rows.Clear()
        If Ds.Tables("FillDataB").Rows.Count > 0 Then
            Dim RoowIndex, Occupied1, Available1, RoomBlock1, Total1 As Decimal
            RoowIndex = 0
            Available1 = 0
            Occupied1 = 0
            RoomBlock1 = 0
            Total1 = 0
            For Each Dr As DataRow In Ds.Tables("FillDataB").Rows
                DgvBlocksList.Rows.Add()
                DgvBlocksList.Rows(RoowIndex).Cells("SNoB").Value = RoowIndex + 1
                DgvBlocksList.Rows(RoowIndex).Cells("RoomTypeB").Value = Dr.Item("RoomTypeB")
                DgvBlocksList.Rows(RoowIndex).Cells("AvailableB").Value = Dr.Item("AvailableB")
                DgvBlocksList.Rows(RoowIndex).Cells("OccupiedB").Value = Dr.Item("OccupiedB")
                DgvBlocksList.Rows(RoowIndex).Cells("RoomBlockB").Value = Dr.Item("RoomBlockB")
                DgvBlocksList.Rows(RoowIndex).Cells("TotalB").Value = Dr.Item("TotalB")
                Available1 += Dr.Item("AvailableB")
                Occupied1 += Dr.Item("OccupiedB")
                RoomBlock1 += Dr.Item("RoomBlockB")
                Total1 += Dr.Item("TotalB")
                RoowIndex += 1
            Next
            DgvBlocksList.Rows.Add()
            DgvBlocksList.Rows(RoowIndex).DefaultCellStyle.Font = New System.Drawing.Font("Bookman Old Style", 14.0!, FontStyle.Bold, GraphicsUnit.Point, CType(0, Byte))
            DgvBlocksList.Rows(RoowIndex).DefaultCellStyle.ForeColor = Color.Red
            DgvBlocksList.Rows(RoowIndex).Cells("RoomTypeB").Style.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvBlocksList.Rows(RoowIndex).Cells("RoomTypeB").Value = "Total"
            DgvBlocksList.Rows(RoowIndex).Cells("AvailableB").Value = Available1
            DgvBlocksList.Rows(RoowIndex).Cells("OccupiedB").Value = Occupied1
            DgvBlocksList.Rows(RoowIndex).Cells("RoomBlockB").Value = RoomBlock1
            DgvBlocksList.Rows(RoowIndex).Cells("TotalB").Value = Total1

            Dim SetGriHeisht2 As Decimal = 220
            For RoowIndex = 0 To DgvBlocksList.Rows.Count - 1
                DgvBlocksList.Rows(RoowIndex).Height += 15
                SetGriHeisht2 += 15
            Next
            DgvBlocksList.Size = New System.Drawing.Size(620, SetGriHeisht2)
        End If
    End Sub
    Private Sub BtnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        GrpDashBoard.Hide()
    End Sub
    Public Sub BMShowMain()
        ' Removed: industry-specific book view (SuperMarket edition).
    End Sub
    Public Sub BarShowMain()
        ' Removed: industry-specific book view (SuperMarket edition).
    End Sub
    Public Sub PSShowMain()
        ' Removed: industry-specific book view (SuperMarket edition).
    End Sub
    Public Sub JMahalShowMain()
        ' Removed: industry-specific book view (SuperMarket edition).
    End Sub
End Class