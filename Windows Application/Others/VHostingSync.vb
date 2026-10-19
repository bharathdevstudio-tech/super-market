Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net
Imports System.Text
Imports System.Text.RegularExpressions
Imports System.Web.Script.Serialization

Public Enum HostingSyncMode
    ServerToVB = 0
    VBToServer = 1
End Enum

Public Module VHostingSync

    Private Const KIRU_PUSH_API As String = "https://vistawin.store/kiruthick-clinic/vbtoserver-insert/insert_api.php"
    Private Const KIRU_PULL_API As String = "https://vistawin.store/kiruthick-clinic/servertovb-recieve/api_data.php"

    Private Const SLM_PUSH_API As String = "https://vistawin.store/Inven-slm/vbtoserver-insert/insert_api.php"
    Private Const SLM_PULL_API As String = "https://vistawin.store/Inven-slm/servertovb-recieve/api_data.php"

    Private Const SBE_PUSH_API As String = "https://vistawin.store/Inven-sbe/vbtoserver-insert/insert_api.php"
    Private Const SBE_PULL_API As String = "https://vistawin.store/Inven-sbe/servertovb-recieve/api_data.php"

    Private Const HTTP_TIMEOUT As Integer = 30000

    Private Class TimeoutWebClient
        Inherits WebClient

        Protected Overrides Function GetWebRequest(ByVal address As Uri) As WebRequest
            Dim req As WebRequest = MyBase.GetWebRequest(address)
            req.Timeout = HTTP_TIMEOUT

            Dim httpReq As HttpWebRequest = TryCast(req, HttpWebRequest)
            If httpReq IsNot Nothing Then
                httpReq.ReadWriteTimeout = HTTP_TIMEOUT
            End If

            Return req
        End Function
    End Class

    Private Function CurrentDatabase() As String
        Return Convert.ToString(VistawinFunctions.CmpDatabaseName).Trim().ToUpperInvariant()
    End Function

    Private Function GetOpenSqlConnection() As SqlConnection

        If VistawinFunctions.ServerConnection Is Nothing Then
            Throw New Exception("SQL Server connection is not initialized.")
        End If

        Dim conn As SqlConnection = VistawinFunctions.ServerConnection
        If conn.State = ConnectionState.Broken Then
            conn.Close()
        End If

        If conn.State = ConnectionState.Closed Then
            conn.Open()
        End If
        Return conn

    End Function

    Private Function IsKiruDatabase() As Boolean
        Dim db As String = CurrentDatabase()
        Return db = "KIRUTHICKCLINICDB" OrElse db = "KIRUTHICKCLINICTCRDB"
    End Function

    Private Function GetPullApiUrl() As String

        Select Case CurrentDatabase()
            Case "KIRUTHICKCLINICDB", "KIRUTHICKCLINICTCRDB"
                Return KIRU_PULL_API

            Case "SLMARKDB"
                Return SLM_PULL_API

            Case "BHAVANIELECSURDB"
                Return SBE_PULL_API

            Case Else
                Throw New Exception("Hosting Server -> VB sync is not configured for database: " & CurrentDatabase())
        End Select
    End Function

    Private Function GetPushApiUrl() As String

        Select Case CurrentDatabase()
            Case "KIRUTHICKCLINICDB", "KIRUTHICKCLINICTCRDB"
                Return KIRU_PUSH_API

            Case "SLMARKDB"
                Return SLM_PUSH_API

            Case "BHAVANIELECSURDB"
                Return SBE_PUSH_API

            Case Else
                Throw New Exception("VB -> Hosting Server sync is not configured for database: " & CurrentDatabase())
        End Select
    End Function

    Private Sub EnsureTls12()
        'TLS 1.2 value for .NET 3.5 where SecurityProtocolType may not expose Tls12.
        ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
        ServicePointManager.DefaultConnectionLimit = 10
    End Sub

    Private Function NewJsonSerializer() As JavaScriptSerializer
        Dim js As New JavaScriptSerializer()
        js.MaxJsonLength = Integer.MaxValue
        Return js
    End Function

    Private Function IsSafeSqlName(ByVal value As String) As Boolean
        If value Is Nothing Then Return False
        Return Regex.IsMatch(value, "^[A-Za-z0-9_]+$")
    End Function

    Private Function Q(ByVal name As String) As String
        Return "[" & name.Replace("]", "]]") & "]"
    End Function

    Private Function GetDictionaryArray(ByVal json As String) As List(Of Dictionary(Of String, Object))
        Dim result As New List(Of Dictionary(Of String, Object))()

        Dim js As JavaScriptSerializer = NewJsonSerializer()
        Dim rootObj As Object = js.DeserializeObject(json)
        Dim root As Dictionary(Of String, Object) = TryCast(rootObj, Dictionary(Of String, Object))

        If root Is Nothing OrElse Not root.ContainsKey("data") OrElse root("data") Is Nothing Then
            Return result
        End If

        Dim items As IEnumerable = TryCast(root("data"), IEnumerable)
        If items Is Nothing Then Return result

        For Each item As Object In items
            Dim dict As Dictionary(Of String, Object) = TryCast(item, Dictionary(Of String, Object))
            If dict IsNot Nothing Then result.Add(dict)
        Next

        Return result
    End Function

    Public Function FetchRemoteTableList() As List(Of String)
        Dim tables As New List(Of String)()
        Dim apiUrl As String = GetPullApiUrl()

        Try
            EnsureTls12()

            Using client As New TimeoutWebClient()
                client.Encoding = Encoding.UTF8
                Dim json As String = client.DownloadString(apiUrl & "?action=tables")
                Dim rows As List(Of Dictionary(Of String, Object)) = GetDictionaryArray(json)

                For Each row As Dictionary(Of String, Object) In rows
                    If Not row.ContainsKey("name") OrElse row("name") Is Nothing Then Continue For

                    Dim tableName As String = row("name").ToString().Trim()
                    If tableName.Length = 0 Then Continue For
                    If Not IsSafeSqlName(tableName) Then Continue For

                    If IsKiruDatabase() Then
                        If String.Equals(tableName, "users", StringComparison.OrdinalIgnoreCase) Then Continue For
                        If String.Equals(tableName, "tbl_Stock", StringComparison.OrdinalIgnoreCase) Then Continue For
                    End If

                    If Not ContainsText(tables, tableName) Then tables.Add(tableName)
                Next
            End Using

        Catch ex As Exception
            Throw New Exception("API Error (table list): " & ex.Message, ex)
        End Try

        Return tables
    End Function
   
    Public Function FetchRemoteData(ByVal tableName As String) As DataTable
        ValidateTableName(tableName)

        Dim dt As New DataTable()
        Dim apiUrl As String = GetPullApiUrl()

        Try
            EnsureTls12()

            Using client As New TimeoutWebClient()
                client.Encoding = Encoding.UTF8
                Dim url As String = apiUrl & "?action=data&table=" & Uri.EscapeDataString(tableName)
                Dim json As String = client.DownloadString(url)
                Dim rows As List(Of Dictionary(Of String, Object)) = GetDictionaryArray(json)

                'Create all columns first so later rows with extra keys are also supported.
                For Each row As Dictionary(Of String, Object) In rows
                    For Each kvp As KeyValuePair(Of String, Object) In row
                        Dim colName As String = kvp.Key
                        If colName Is Nothing OrElse colName.Trim().Length = 0 Then Continue For
                        If colName.Length > 128 Then Throw New Exception("Column name is too long: " & colName)

                        If Not ContainsColumnIgnoreCase(dt, colName) Then
                            dt.Columns.Add(colName, GetType(String))
                        End If
                    Next
                Next

                For Each row As Dictionary(Of String, Object) In rows
                    Dim dr As DataRow = dt.NewRow()

                    For Each kvp As KeyValuePair(Of String, Object) In row
                        Dim actualCol As String = FindColumnName(dt, kvp.Key)
                        If actualCol.Length = 0 Then Continue For

                        If kvp.Value Is Nothing Then
                            dr(actualCol) = DBNull.Value
                        Else
                            dr(actualCol) = kvp.Value.ToString()
                        End If
                    Next

                    dt.Rows.Add(dr)
                Next
            End Using

        Catch ex As Exception
            Throw New Exception("API Error (data): " & ex.Message, ex)
        End Try

        Return dt
    End Function

    Public Function FetchLocalTableList() As List(Of String)
        Dim tables As New List(Of String)()

        Dim conn As SqlConnection = GetOpenSqlConnection()

        Dim sql As String

            Select Case CurrentDatabase()
                Case "KIRUTHICKCLINICDB", "KIRUTHICKCLINICTCRDB"
                'sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' AND TABLE_NAME='tbl_Stock' ORDER BY TABLE_NAME"
                sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE'  ORDER BY TABLE_NAME"
                Case "SLMARKDB"
                sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' UNION SELECT 'COLLSHEET' ORDER BY TABLE_NAME"

            Case "BHAVANIELECSURDB"
                sql = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE='BASE TABLE' ORDER BY TABLE_NAME"

                Case Else
                    Throw New Exception("VB -> Hosting Server sync is not configured for database: " & CurrentDatabase())
            End Select

        Using cmd As New SqlCommand(sql, conn)
            Using rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    Dim name As String = Convert.ToString(rdr(0)).Trim()
                    If name.Length > 0 AndAlso Not ContainsText(tables, name) Then tables.Add(name)
                End While
            End Using
        End Using

        Return tables
    End Function

    Public Function FetchLocalData(ByVal tableName As String) As DataTable
        ValidateTableName(tableName)

        If IsKiruDatabase() AndAlso String.Equals(tableName, "tbl_Stock", StringComparison.OrdinalIgnoreCase) Then
            Return FetchKiruStock()
        End If

        If CurrentDatabase() = "SLMARKDB" AndAlso String.Equals(tableName, "COLLSHEET", StringComparison.OrdinalIgnoreCase) Then
            Return FetchSlmCollSheet()
        End If

        Dim dt As New DataTable()
        Dim selectCols As String = GetAllowedColumns(tableName, "*")

        Dim conn As SqlConnection = GetOpenSqlConnection()

        Using cmd As New SqlCommand("SELECT " & selectCols & " FROM " & Q(tableName), conn)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Private Function FetchKiruStock() As DataTable

        Dim dt As New DataTable()
        Dim sql As String = ""
        sql = "SELECT s.St_StockID_n AS 'StockID',St_StNumber_i,s.St_ProName_vc AS 'ProName',St_EMonth_i,St_EYear_vc,St_BatchNo_vc,St_SaleRate_n,St_GoCurSt_i,St_MediCurSt_i,St_CurrStock_i AS Currstock,'1' AS 'SyncStatus' FROM tbl_Stock s LEFT JOIN tbl_MultiStock ms ON(s.St_StockID_n=ms.St_StockID_n) WHERE St_GoCurSt_i>0 AND (CONVERT(INT, St_EYear_vc) >@CurrentYear OR (CONVERT(INT, St_EYear_vc) = @CurrentYear AND ISNULL(St_EMonth_i,0) >= @CurrentMonth))  ORDER BY s.St_ProName_vc, St_EYear_vc, St_EMonth_i"

        Dim conn As SqlConnection = GetOpenSqlConnection()
        Using cmd As New SqlCommand(sql, conn)
            cmd.Parameters.AddWithValue("@CurrentYear", DateTime.Now.Year)
            cmd.Parameters.AddWithValue("@CurrentMonth", DateTime.Now.Month)

            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Private Function FetchSlmCollSheet() As DataTable

        Dim dt As New DataTable()

        Dim sql As String = _
            "SELECT C_Name_vc AS CusName, C_ID_n AS CusID, C_Mobile_vc AS MobileNo, " & _
            "co.CustID AS CoCustID, ISNULL((PayableAmount-PaidAmount),0.00) AS COutAmt, " & _
            "BillDate, InvNo, FinalTotal, PaidAmt, ISNULL(BalAmt,0.00) AS BalAmt, " & _
            "cf.DelayDays, ar.Area_Code_n, Area_Name_vc AS AreaName " & _
            "FROM tbl_CusProfile cu " & _
            "LEFT JOIN CustOutstanding co ON (C_Type_vc='CUSTOMER' AND co.CustID=cu.C_ID_n) " & _
            "LEFT JOIN CollSheetOut cs ON (cs.CusTID=co.CusTID) " & _
            "LEFT JOIN collFinal cf ON (cf.CusID=cu.C_ID_n) " & _
            "LEFT JOIN tbl_Area ar ON (cu.C_AreaCode_i=ar.Area_Code_n) " & _
            "WHERE C_Type_vc='CUSTOMER' " & _
            "AND C_ID_n IN (1287,957,1112,794,1291,805,24,650,1387,360,65,27,1364,35,111,239,1385,2,616,434,1280,1360,636,677,206,656,32,1386,36,1335,44,474,1331,78,432,99,28,1380,87,1346,108,120,320,48,221,322,355,742) " & _
            "ORDER BY C_Name_vc, BillDate, InvNo"

        Dim conn As SqlConnection = GetOpenSqlConnection()
        Using cmd As New SqlCommand(sql, conn)
            Using da As New SqlDataAdapter(cmd)
                da.Fill(dt)
            End Using
        End Using

        Return dt
    End Function

    Public Function PushDataToMySQL(ByVal dt As DataTable, ByVal tableName As String) As Integer
        ValidateTableName(tableName)

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return 0

        Dim allowedCols As List(Of String) = GetAllowedColumnList(tableName)
        Dim rows As New List(Of Dictionary(Of String, Object))()

        For Each dr As DataRow In dt.Rows
            Dim row As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

            For Each col As DataColumn In dt.Columns
                If allowedCols Is Nothing OrElse ContainsText(allowedCols, col.ColumnName) Then
                    If dr(col) Is DBNull.Value Then
                        row(col.ColumnName) = ""
                    Else
                        row(col.ColumnName) = dr(col).ToString()
                    End If
                End If
            Next

            If row.Count > 0 Then rows.Add(row)
        Next

        If rows.Count = 0 Then
            Throw New Exception("No allowed columns found for table: " & tableName)
        End If

        Dim payload As New Dictionary(Of String, Object)()
        payload("data") = rows

        Dim js As JavaScriptSerializer = NewJsonSerializer()
        Dim jsonBody As String = js.Serialize(payload)
        Dim apiUrl As String = GetPushApiUrl()

        Try
            EnsureTls12()

            'The current PHP API uses clear + insert. The form asks for confirmation
            'before this destructive step.
            ClearRemoteTable(tableName, apiUrl)

            Using client As New TimeoutWebClient()
                client.Encoding = Encoding.UTF8
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")

                Dim url As String = apiUrl & "?action=insert&table=" & Uri.EscapeDataString(tableName)
                Dim response As String = client.UploadString(url, "POST", jsonBody)

                Dim respObj As Object = js.DeserializeObject(response)
                Dim resp As Dictionary(Of String, Object) = TryCast(respObj, Dictionary(Of String, Object))

                If resp IsNot Nothing AndAlso resp.ContainsKey("status") AndAlso _
                   String.Equals(Convert.ToString(resp("status")), "success", StringComparison.OrdinalIgnoreCase) Then

                    If resp.ContainsKey("inserted") Then
                        Return Convert.ToInt32(resp("inserted"))
                    End If

                    Return rows.Count
                End If

                Throw New Exception(GetApiMessage(resp, "Unknown API error"))
            End Using

        Catch ex As Exception
            Throw New Exception("API Error: " & ex.Message, ex)
        End Try
    End Function

    Private Sub ClearRemoteTable(ByVal tableName As String, ByVal apiUrl As String)
        EnsureTls12()

        Using client As New TimeoutWebClient()
            client.Encoding = Encoding.UTF8
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json; charset=utf-8")

            Dim url As String = apiUrl & "?action=clear&table=" & Uri.EscapeDataString(tableName)
            Dim response As String = client.UploadString(url, "POST", "{}")

            Dim js As JavaScriptSerializer = NewJsonSerializer()
            Dim respObj As Object = js.DeserializeObject(response)
            Dim resp As Dictionary(Of String, Object) = TryCast(respObj, Dictionary(Of String, Object))

            If resp IsNot Nothing AndAlso resp.ContainsKey("status") AndAlso _
               String.Equals(Convert.ToString(resp("status")), "success", StringComparison.OrdinalIgnoreCase) Then
                Return
            End If

            Throw New Exception(GetApiMessage(resp, "Unknown clear error"))
        End Using
    End Sub

    Private Function GetApiMessage(ByVal resp As Dictionary(Of String, Object), ByVal defaultMessage As String) As String
        If resp IsNot Nothing AndAlso resp.ContainsKey("message") AndAlso resp("message") IsNot Nothing Then
            Dim msg As String = resp("message").ToString().Trim()
            If msg.Length > 0 Then Return msg
        End If

        Return defaultMessage
    End Function

    Public Function CreateAndInsertLocal(ByVal dt As DataTable, ByVal tableName As String) As Integer
        ValidateTableName(tableName)

        If dt Is Nothing OrElse dt.Rows.Count = 0 OrElse dt.Columns.Count = 0 Then Return 0

        Dim insertedCount As Integer = 0

        Dim conn As SqlConnection = GetOpenSqlConnection()
        Dim tran As SqlTransaction = conn.BeginTransaction()

        Try
                Dim tableExists As Boolean = LocalTableExists(conn, tran, tableName)

                If Not tableExists Then
                    CreateLocalTable(conn, tran, tableName, dt)
                End If

                Dim targetColumns As List(Of String) = GetLocalTableColumns(conn, tran, tableName)
                Dim commonColumns As New List(Of String)()

                For Each col As DataColumn In dt.Columns
                    Dim targetCol As String = FindText(targetColumns, col.ColumnName)
                    If targetCol.Length > 0 Then commonColumns.Add(targetCol)
                Next

                If commonColumns.Count = 0 Then
                    Throw New Exception("No matching columns found in SQL Server table: " & tableName)
                End If

                Dim keyColumn As String = GetKeyColumn(tableName)
                If keyColumn.Length = 0 Then keyColumn = commonColumns(0)

                keyColumn = FindText(commonColumns, keyColumn)

                For Each dr As DataRow In dt.Rows
                    If keyColumn.Length > 0 Then
                        Dim sourceKeyCol As String = FindColumnName(dt, keyColumn)

                        If sourceKeyCol.Length > 0 AndAlso dr(sourceKeyCol) IsNot DBNull.Value Then
                            Dim keyValue As String = dr(sourceKeyCol).ToString().Trim()

                            If keyValue.Length > 0 AndAlso LocalRowExists(conn, tran, tableName, keyColumn, keyValue) Then
                                Continue For
                            End If
                        End If
                    End If

                    InsertLocalRow(conn, tran, tableName, dt, dr, commonColumns)
                    insertedCount += 1
                Next

                tran.Commit()

            Catch
                Try
                    tran.Rollback()
                Catch
                End Try
                Throw
        Finally
            tran.Dispose()
        End Try

        Return insertedCount
    End Function

    Private Function LocalTableExists(ByVal conn As SqlConnection, ByVal tran As SqlTransaction, ByVal tableName As String) As Boolean
        Dim sql As String = "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='dbo' AND TABLE_TYPE='BASE TABLE' AND TABLE_NAME=@TableName"

        Using cmd As New SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@TableName", tableName)
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Sub CreateLocalTable(ByVal conn As SqlConnection, ByVal tran As SqlTransaction, _
                                 ByVal tableName As String, ByVal dt As DataTable)
        Dim sb As New StringBuilder()
        sb.Append("CREATE TABLE ").Append(Q(tableName)).Append(" (")

        For i As Integer = 0 To dt.Columns.Count - 1
            If i > 0 Then sb.Append(", ")
            sb.Append(Q(dt.Columns(i).ColumnName)).Append(" NVARCHAR(500) NULL")
        Next

        sb.Append(")")

        Using cmd As New SqlCommand(sb.ToString(), conn, tran)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function GetLocalTableColumns(ByVal conn As SqlConnection, ByVal tran As SqlTransaction, _
                                          ByVal tableName As String) As List(Of String)
        Dim cols As New List(Of String)()
        Dim sql As String = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA='dbo' AND TABLE_NAME=@TableName ORDER BY ORDINAL_POSITION"

        Using cmd As New SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@TableName", tableName)

            Using rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    cols.Add(Convert.ToString(rdr(0)))
                End While
            End Using
        End Using

        Return cols
    End Function

    Private Function LocalRowExists(ByVal conn As SqlConnection, ByVal tran As SqlTransaction, _
                                    ByVal tableName As String, ByVal keyColumn As String, _
                                    ByVal keyValue As String) As Boolean
        Dim sql As String = "SELECT COUNT(*) FROM " & Q(tableName) & _
                            " WHERE CAST(" & Q(keyColumn) & " AS NVARCHAR(500))=@KeyValue"

        Using cmd As New SqlCommand(sql, conn, tran)
            cmd.Parameters.AddWithValue("@KeyValue", keyValue)
            Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
        End Using
    End Function

    Private Sub InsertLocalRow(ByVal conn As SqlConnection, ByVal tran As SqlTransaction, _
                               ByVal tableName As String, ByVal dt As DataTable, _
                               ByVal dr As DataRow, ByVal commonColumns As List(Of String))
        Dim names As New StringBuilder()
        Dim values As New StringBuilder()

        Using cmd As New SqlCommand()
            cmd.Connection = conn
            cmd.Transaction = tran

            For i As Integer = 0 To commonColumns.Count - 1
                If i > 0 Then
                    names.Append(", ")
                    values.Append(", ")
                End If

                Dim targetCol As String = commonColumns(i)
                Dim sourceCol As String = FindColumnName(dt, targetCol)
                Dim pName As String = "@P" & i.ToString()

                names.Append(Q(targetCol))
                values.Append(pName)

                If sourceCol.Length = 0 OrElse dr(sourceCol) Is DBNull.Value Then
                    cmd.Parameters.AddWithValue(pName, DBNull.Value)
                Else
                    cmd.Parameters.AddWithValue(pName, dr(sourceCol))
                End If
            Next

            cmd.CommandText = "INSERT INTO " & Q(tableName) & " (" & names.ToString() & ") VALUES (" & values.ToString() & ")"
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Function GetKeyColumn(ByVal tableName As String) As String
        Select Case tableName.Trim().ToLowerInvariant()
            Case "tbl_area"
                Return "Area_Code_n"
            Case "tbl_stockdtl"
                Return "St_StockID_n"
            Case "tbl_multistock"
                Return "St_StockID_n"
            Case "tbl_quodetail"
                Return "Sa_invNo_vc"
            Case "tbl_stock"
                Return "Stock_Code_n"
            Case Else
                Return ""
        End Select
    End Function

    Private Function GetAllowedColumnList(ByVal tableName As String) As List(Of String)
        Dim allowed As New List(Of String)()
        Dim t As String = tableName.Trim().ToLowerInvariant()

        If IsKiruDatabase() Then
            Select Case t
                Case "tbl_stock"
                    allowed.Add("St_StNumber_i")
                    allowed.Add("ProName")
                    allowed.Add("St_EMonth_i")
                    allowed.Add("St_EYear_vc")
                    allowed.Add("St_BatchNo_vc")
                    allowed.Add("St_SaleRate_n")
                    allowed.Add("St_GoCurSt_i")
                    allowed.Add("St_MediCurSt_i")
                    allowed.Add("Currstock")
                    allowed.Add("SyncStatus")
                    Return allowed

                Case "collsheet"
                    allowed.Add("CusName")
                    allowed.Add("CusID")
                    Return allowed

                Case Else
                    Return Nothing
            End Select
        End If

        If CurrentDatabase() = "SLMARKDB" Then
            Select Case t
                Case "tbl_area"
                    allowed.Add("Area_Code_n")
                    allowed.Add("Area_Name_vc")
                    Return allowed

                Case "tbl_multistock"
                    allowed.Add("St_StockID_n")
                    allowed.Add("St_ProName_vc")
                    allowed.Add("St_PurRate_n")
                    allowed.Add("St_SaleRate_n")
                    Return allowed

                Case "collsheet"
                    allowed.Add("CusName")
                    allowed.Add("CusID")
                    allowed.Add("MobileNo")
                    allowed.Add("CoCustID")
                    allowed.Add("COutAmt")
                    allowed.Add("BillDate")
                    allowed.Add("InvNo")
                    allowed.Add("FinalTotal")
                    allowed.Add("PaidAmt")
                    allowed.Add("BalAmt")
                    allowed.Add("DelayDays")
                    allowed.Add("Area_Code_n")
                    allowed.Add("AreaName")
                    Return allowed

                Case Else
                    Return Nothing
            End Select
        End If

        If CurrentDatabase() = "BHAVANIELECSURDB" Then
            Select Case t
                Case "tbl_area"
                    allowed.Add("Area_Code_n")
                    allowed.Add("Area_Name_vc")
                    Return allowed

                Case "tbl_multistock"
                    allowed.Add("St_StockID_n")
                    allowed.Add("St_ProName_vc")
                    allowed.Add("St_PurRate_n")
                    allowed.Add("St_SaleRate_n")
                    Return allowed

                Case Else
                    Return Nothing
            End Select
        End If

        Return Nothing
    End Function

    Private Function GetAllowedColumns(ByVal tableName As String, ByVal allClause As String) As String
        Dim allowed As List(Of String) = GetAllowedColumnList(tableName)
        If allowed Is Nothing Then Return allClause

        Dim parts As New List(Of String)()
        For Each c As String In allowed
            parts.Add(Q(c))
        Next

        Return String.Join(", ", parts.ToArray())
    End Function

    Private Sub ValidateTableName(ByVal tableName As String)
        If tableName Is Nothing OrElse tableName.Trim().Length = 0 Then
            Throw New Exception("Table name is empty.")
        End If

        If Not IsSafeSqlName(tableName) Then
            Throw New Exception("Invalid table name: " & tableName)
        End If
    End Sub

    Private Function ContainsText(ByVal items As List(Of String), ByVal value As String) As Boolean
        Return FindText(items, value).Length > 0
    End Function

    Private Function FindText(ByVal items As List(Of String), ByVal value As String) As String
        If items Is Nothing OrElse value Is Nothing Then Return ""

        For Each item As String In items
            If String.Equals(item, value, StringComparison.OrdinalIgnoreCase) Then Return item
        Next

        Return ""
    End Function

    Private Function ContainsColumnIgnoreCase(ByVal dt As DataTable, ByVal columnName As String) As Boolean
        Return FindColumnName(dt, columnName).Length > 0
    End Function

    Private Function FindColumnName(ByVal dt As DataTable, ByVal columnName As String) As String
        If dt Is Nothing OrElse columnName Is Nothing Then Return ""

        For Each col As DataColumn In dt.Columns
            If String.Equals(col.ColumnName, columnName, StringComparison.OrdinalIgnoreCase) Then
                Return col.ColumnName
            End If
        Next

        Return ""
    End Function

    Public Function FetchLocalDataByDate(ByVal TableName As String,
                                     ByVal FromDate As DateTime,
                                     ByVal ToDate As DateTime) As DataTable

        ValidateTableName(TableName)

        If Not String.Equals(TableName, "tbl_SalDetail",
                             StringComparison.OrdinalIgnoreCase) AndAlso _
           Not String.Equals(TableName, "tbl_SalParticular",
                             StringComparison.OrdinalIgnoreCase) Then

            Throw New Exception("Date filter available only for Sales tables.")
        End If

        Dim Dt As New DataTable()

        Dim Conn As SqlConnection = GetOpenSqlConnection()

        Dim Sql As String = _
            "SELECT * FROM " & Q(TableName) & _
            " WHERE [Sa_BillDate_d] >= @FromDate " & _
            " AND [Sa_BillDate_d] < @ToDate"

        Using Cmd As New SqlCommand(Sql, Conn)

            Cmd.Parameters.AddWithValue("@FromDate", FromDate.Date)
            Cmd.Parameters.AddWithValue("@ToDate", ToDate.Date.AddDays(1))

            Using Da As New SqlDataAdapter(Cmd)
                Da.Fill(Dt)
            End Using

        End Using

        Return Dt

    End Function
    
    Public Function FetchRemoteDataByDate(ByVal TableName As String,
                                      ByVal FromDate As DateTime,
                                      ByVal ToDate As DateTime) As DataTable

        If Not String.Equals(TableName, "tbl_SalDetail",
                             StringComparison.OrdinalIgnoreCase) AndAlso _
           Not String.Equals(TableName, "tbl_SalParticular",
                             StringComparison.OrdinalIgnoreCase) Then

            Throw New Exception("Date filter available only for Sales tables.")
        End If

        Dim SourceDt As DataTable = FetchRemoteData(TableName)

        If Not SourceDt.Columns.Contains("Sa_BillDate_d") Then
            Throw New Exception("Sa_BillDate_d column not found in " & TableName)
        End If

        Dim ResultDt As DataTable = SourceDt.Clone()

        For Each Dr As DataRow In SourceDt.Rows

            If Dr("Sa_BillDate_d") Is DBNull.Value Then Continue For

            Dim BillDate As DateTime

            If DateTime.TryParse(Dr("Sa_BillDate_d").ToString(), BillDate) Then

                If BillDate.Date >= FromDate.Date AndAlso _
                   BillDate.Date <= ToDate.Date Then

                    ResultDt.ImportRow(Dr)

                End If

            End If

        Next

        Return ResultDt

    End Function

End Module
