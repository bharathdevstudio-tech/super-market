Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Windows.Forms

''' <summary>
''' SuperMarket module shared helper class.
''' Follows the existing VistawinFunctions pattern (Public Class with Shared members)
''' and always talks to the shared VistawinFunctions.ServerConnection.
''' New SQL in this class is always parameterized.
''' </summary>
Public Class VSuperMarket

    ' ---- logged-in user context ------------------------------------------------------
    Public Shared CurrentUserID As Integer = 0
    Public Shared CurrentUserName As String = ""
    Public Shared CurrentUserType As String = ""      ' ADMIN / MANAGER / CASHIER
    Public Shared CurrentMachine As String = Environment.MachineName
    Public Shared CompanyName As String = "SUPER MARKET"

    ' ---- open the shared connection if it is closed ---------------------------------
    Public Shared Sub EnsureConnection()
        If VistawinFunctions.ServerConnection Is Nothing Then
            Throw New Exception("Database connection is not initialised. Please log in again.")
        End If
        If VistawinFunctions.ServerConnection.State <> ConnectionState.Open Then
            VistawinFunctions.ServerConnection.Open()
        End If
    End Sub

    ' ---- financial year  (Apr-Mar), matches BVistawinSolution.YearSeries ------------
    Public Shared Function GetFY(ByVal d As Date) As String
        If d.Month <= 3 Then
            Return (d.Year - 1).ToString & "-" & d.Year.ToString
        Else
            Return d.Year.ToString & "-" & (d.Year + 1).ToString
        End If
    End Function

    Public Shared ReadOnly Property CurrentFY() As String
        Get
            Return GetFY(Date.Today)
        End Get
    End Property

    ''' <summary>
    ''' Result of a bill-number reservation (see NextBillNumber).
    ''' </summary>
    Public Class BillNumber
        Public AcYear As String = ""      ' e.g. 2026-2027
        Public Series As String = ""      ' prefix from tbl_BillSeries
        Public BillNo As Decimal = 0      ' running number inside the year
        Public InvoiceNo As String = ""   ' Series & BillNo
    End Class

    ''' <summary>
    ''' Reserves the next invoice number for the given bill type inside the
    ''' supplied transaction, using exactly the same scheme as the legacy
    ''' retail POS (FrmDgvSalesRetail):
    '''   * series read from tbl_BillSeries (B_BillSeries_vc / B_StartNo_i)
    '''   * next number = start number for the first bill of the year, else
    '''     MAX(Sa_BillNo_n) + 1 in tbl_SalDetail for the same type + year.
    ''' MUST be called with an open transaction; UPDLOCK/HOLDLOCK serialises it.
    ''' </summary>
    Public Shared Function NextBillNumber(ByVal trans As SqlTransaction, ByVal saveType As String) As BillNumber
        Return NextBillNumber(trans, saveType, "tbl_SalDetail")
    End Function

    ''' <summary>
    ''' Same numbering scheme as NextBillNumber, but for a different header
    ''' table: tbl_SalDetail (sales / POS) or tbl_PurchDetail (purchase).
    ''' The table name is validated against a fixed whitelist.
    ''' </summary>
    Public Shared Function NextBillNumber(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal headerTable As String) As BillNumber
        EnsureConnection()
        If headerTable <> "tbl_PurchDetail" Then headerTable = "tbl_SalDetail"

        Dim fy As String = CurrentFY
        Dim bn As New BillNumber()
        bn.AcYear = fy
        Dim startNo As Decimal = 1D

        Using cmd As New SqlCommand("SELECT TOP 1 ISNULL(B_BillSeries_vc,'') AS BillSeries, ISNULL(B_StartNo_i,1) AS StartNo " & _
                                    "FROM tbl_BillSeries WITH (UPDLOCK,HOLDLOCK) WHERE B_BillType_vc=@T AND B_AcYear_vc=@FY", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@T", saveType)
            cmd.Parameters.AddWithValue("@FY", fy)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If rd.Read() Then
                    bn.Series = Convert.ToString(rd("BillSeries")).Trim()
                    Decimal.TryParse(Convert.ToString(rd("StartNo")), startNo)
                End If
            End Using
        End Using

        Dim billCount As Integer
        Using cmd As New SqlCommand("SELECT COUNT(*) FROM " & headerTable & " WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY", VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@T", saveType)
            cmd.Parameters.AddWithValue("@FY", fy)
            billCount = Convert.ToInt32(cmd.ExecuteScalar())
        End Using

        If billCount = 0 Then
            bn.BillNo = startNo
        Else
            Using cmd As New SqlCommand("SELECT ISNULL(MAX(CONVERT(NUMERIC(18,0),Sa_BillNo_n)),0)+1 FROM " & headerTable & " WITH (UPDLOCK,HOLDLOCK) " & _
                                        "WHERE Sa_SaveType_vc=@T AND Sa_AcYear_vc=@FY", VistawinFunctions.ServerConnection, trans)
                cmd.Parameters.AddWithValue("@T", saveType)
                cmd.Parameters.AddWithValue("@FY", fy)
                bn.BillNo = Convert.ToDecimal(cmd.ExecuteScalar())
            End Using
        End If

        bn.InvoiceNo = bn.Series & bn.BillNo.ToString("0")
        Return bn
    End Function

    ' ---- money / tax helpers ----------------------------------------------------------
    Public Shared Function RoundOff(ByVal amt As Decimal) As Decimal
        Return Math.Round(amt, 0, MidpointRounding.AwayFromZero)
    End Function

    Public Shared Function RoundOffDiff(ByVal amt As Decimal) As Decimal
        Return RoundOff(amt) - amt
    End Function

    ''' <summary>For an inclusive tax rate, returns the taxable value inside the amount.</summary>
    Public Shared Function TaxableFromInclusive(ByVal inclusiveAmt As Decimal, ByVal taxPer As Decimal) As Decimal
        If taxPer <= 0 Then Return inclusiveAmt
        Return Math.Round(inclusiveAmt * 100 / (100 + taxPer), 2)
    End Function

    ''' <summary>Returns tax component on a taxable value.</summary>
    Public Shared Function TaxAmount(ByVal taxableAmt As Decimal, ByVal taxPer As Decimal) As Decimal
        Return Math.Round(taxableAmt * taxPer / 100, 2)
    End Function

    ' ---- small data helpers (parameterized by design) --------------------------------
    Public Shared Function GetScalar(ByVal sql As String, ByVal ParamArray p As SqlParameter()) As Object
        EnsureConnection()
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            If p IsNot Nothing Then cmd.Parameters.AddRange(p)
            Return cmd.ExecuteScalar()
        End Using
    End Function

    Public Shared Function GetInt(ByVal sql As String, ByVal ParamArray p As SqlParameter()) As Integer
        Dim o As Object = GetScalar(sql, p)
        If o Is Nothing OrElse IsDBNull(o) Then Return 0
        Return CInt(o)
    End Function

    Public Shared Function Exec(ByVal sql As String, ByVal ParamArray p As SqlParameter()) As Integer
        EnsureConnection()
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection)
            If p IsNot Nothing Then cmd.Parameters.AddRange(p)
            Return cmd.ExecuteNonQuery()
        End Using
    End Function

    Public Shared Function Param(ByVal name As String, ByVal value As Object) As SqlParameter
        Return New SqlParameter(name, If(value Is Nothing, DBNull.Value, value))
    End Function

    ' ---- activity log -----------------------------------------------------------------
    Public Shared Sub LogActivity(ByVal formName As String, ByVal action As String, ByVal details As String)
        Try
            EnsureConnection()
            Using cmd As New SqlCommand("INSERT INTO tbl_ActivityLog (AL_Date_d,AL_UserName_vc,AL_Form_vc,AL_Action_vc,AL_Details_vc,AL_Machine_vc,Ent_date_d) " & _
                                        "VALUES (GETDATE(),@U,@F,@A,@D,@M,GETDATE())", VistawinFunctions.ServerConnection)
                cmd.Parameters.AddWithValue("@U", CurrentUserName)
                cmd.Parameters.AddWithValue("@F", formName)
                cmd.Parameters.AddWithValue("@A", action)
                cmd.Parameters.AddWithValue("@D", If(details, ""))
                cmd.Parameters.AddWithValue("@M", CurrentMachine)
                cmd.ExecuteNonQuery()
            End Using
        Catch
            ' never let logging break a business transaction
        End Try
    End Sub

    ' ---- permission check -------------------------------------------------------------
    ' Priority:
    '   1. ADMIN users may do everything.
    '   2. An explicit row in tbl_SmUserRights (SuperMarket Security module)
    '      decides the individual VIEW / ADD / EDIT / DELETE flag.
    '   3. Otherwise fall back to the legacy role menu grant
    '      (tbl_MenuMaster.FormName -> tbl_UserRoleMenu).
    '   4. If the form is not mapped to any menu, access is allowed.
    Public Shared Function HasPermission(ByVal formName As String, ByVal action As String) As Boolean
        Try
            If String.Compare(CurrentUserType, "ADMIN", True) = 0 Then Return True

            ' fixed whitelist -> safe to embed the column name
            Dim col As String
            Select Case action.ToUpper
                Case "VIEW" : col = "UR_View_b"
                Case "ADD" : col = "UR_Add_b"
                Case "EDIT" : col = "UR_Edit_b"
                Case "DELETE" : col = "UR_Delete_b"
                Case Else : Return True
            End Select

            If CurrentUserID > 0 Then
                Dim hasRow As Integer = GetInt("SELECT COUNT(*) FROM tbl_SmUserRights WHERE UR_UserID_i=@U AND UR_Form_vc=@F", _
                                               Param("@U", CurrentUserID), Param("@F", formName))
                If hasRow > 0 Then
                    Return GetInt("SELECT CASE WHEN " & col & "=1 THEN 1 ELSE 0 END FROM tbl_SmUserRights WHERE UR_UserID_i=@U AND UR_Form_vc=@F", _
                                  Param("@U", CurrentUserID), Param("@F", formName)) = 1
                End If

                ' legacy role menu grant
                Dim menuId As Integer = GetInt("SELECT TOP 1 MenuID FROM tbl_MenuMaster WHERE FormName=@F", Param("@F", formName))
                If menuId > 0 Then
                    Return GetInt("SELECT COUNT(*) FROM tbl_UserRoleMenu WHERE URM_EmpID_i=@E AND URM_MenuID_i=@M", _
                                  Param("@E", CurrentUserID), Param("@M", menuId)) > 0
                End If
            End If

            Return True   ' form not mapped to a menu -> not restricted
        Catch
            Return True
        End Try
    End Function

End Class
