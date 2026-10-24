Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Transactional outbox writer for offline-first cloud sync.
'''
''' Every method here is called INSIDE the transaction that writes the bill,
''' after all bill rows are written and before Commit. That is the whole point:
''' a committed bill always has exactly one committed outbox row, and a
''' rolled-back bill leaves no outbox row behind.
'''
''' Nothing in here touches billing maths, tax, rounding, printing, reports or
''' any existing table. It only reads the rows that were just written and
''' inserts into SM_SyncOutbox.
'''
''' Failure policy: if the outbox insert fails, the caller's transaction rolls
''' back and the bill is not saved. That is deliberate - a silently unsynced
''' bill is worse than a visible error. To keep that from becoming a new failure
''' point, every insert is guarded by a capability check (EnsurePrepared) so the
''' app still bills normally when the SM_Sync* objects have not been deployed.
''' </summary>
Friend NotInheritable Class SmSyncQueue

    ' 0 = not checked yet, 1 = outbox present, -1 = outbox absent
    Private Shared _available As Integer = 0
    Private Shared _storeId As String = "STORE-1"

    Private Sub New()
    End Sub

    ' ------------------------------------------------------------------ setup

    ''' <summary>
    ''' Probes for the outbox tables once per process. MUST be called outside a
    ''' transaction (the probe runs on the shared connection). Safe to call from
    ''' any form; subsequent calls are free.
    ''' </summary>
    Friend Shared Sub EnsurePrepared()
        If _available <> 0 Then Exit Sub

        Try
            Using cmd As New SqlCommand( _
                "SELECT CASE WHEN " & _
                "OBJECT_ID(N'dbo.SM_SyncOutbox', N'U') IS NOT NULL AND " & _
                "OBJECT_ID(N'dbo.SM_SyncOutboxLog', N'U') IS NOT NULL AND " & _
                "OBJECT_ID(N'dbo.SM_SyncMeta', N'U') IS NOT NULL " & _
                "THEN 1 ELSE 0 END", VistawinFunctions.ServerConnection)
                _available = If(Convert.ToInt32(cmd.ExecuteScalar()) = 1, 1, -1)
            End Using
        Catch
            ' no connection, no permission, no database - billing must still work
            _available = -1
        End Try

        If _available <> 1 Then Exit Sub

        Try
            Using cmd As New SqlCommand( _
                "SELECT MetaValue FROM dbo.SM_SyncMeta WHERE MetaKey = 'StoreID'", _
                VistawinFunctions.ServerConnection)
                Dim v As Object = cmd.ExecuteScalar()
                If v IsNot Nothing AndAlso Not Convert.IsDBNull(v) Then
                    Dim s As String = Convert.ToString(v).Trim()
                    If s <> "" Then _storeId = s
                End If
            End Using
        Catch
            ' keep the default store id
        End Try
    End Sub

    ''' <summary>True when the outbox exists and can be written.</summary>
    Friend Shared ReadOnly Property IsAvailable() As Boolean
        Get
            Return _available = 1
        End Get
    End Property

    ''' <summary>
    ''' True when the background uploader is allowed to run. Read fresh every
    ''' time (it is a single small query and it is checked once per sync cycle).
    ''' </summary>
    Friend Shared Function IsUploadEnabled(ByVal trans As SqlTransaction) As Boolean
        If _available <> 1 Then Return False
        Try
            Dim sql As String = "SELECT MetaValue FROM dbo.SM_SyncMeta WHERE MetaKey = 'SyncEnabled'"
            Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
                Dim v As Object = cmd.ExecuteScalar()
                If v Is Nothing OrElse Convert.IsDBNull(v) Then Return False
                Return Convert.ToString(v).Trim() = "1"
            End Using
        Catch
            Return False
        End Try
    End Function

    ' ------------------------------------------------------------------ sales

    ''' <summary>
    ''' Enqueues the sale bill identified by (saveType, invNo) exactly as it now
    ''' sits in tbl_SalDetail / tbl_SalParticular inside <paramref name="trans"/>.
    ''' Call after the lines are written, before Commit.
    ''' operation = "INSERT" for a new bill, "UPDATE" for a re-saved bill.
    ''' </summary>
    Friend Shared Sub EnqueueSale(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String, ByVal operation As String)
        If Not IsAvailable OrElse trans Is Nothing OrElse invNo = "" Then Exit Sub

        Dim snap As SyncSnapshot = ReadSaleHeader(trans, saveType, invNo)
        If snap Is Nothing Then Exit Sub
        snap.Lines = ReadSaleLines(trans, saveType, invNo)

        Dim eventId As Guid = Guid.NewGuid()
        Dim payload As String = BuildSalePayload(eventId, operation, snap)
        InsertEvent(trans, eventId, "SALE", invNo, operation, snap.SaveType, snap.AcYear, snap.BillNo, payload)
    End Sub

    ''' <summary>
    ''' Enqueues a bill deletion. Call while the header row still exists (before
    ''' the DELETE statements), so the event can carry the bill identity. If the
    ''' transaction rolls back, the event rolls back with it.
    ''' </summary>
    Friend Shared Sub EnqueueSaleDeleted(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String)
        If Not IsAvailable OrElse trans Is Nothing OrElse invNo = "" Then Exit Sub

        Dim snap As SyncSnapshot = ReadSaleHeader(trans, saveType, invNo)
        If snap Is Nothing Then Exit Sub
        snap.Lines = New List(Of SyncLine)()

        Dim eventId As Guid = Guid.NewGuid()
        Dim payload As String = BuildSalePayload(eventId, "DELETE", snap)
        InsertEvent(trans, eventId, "SALE", invNo, "DELETE", snap.SaveType, snap.AcYear, snap.BillNo, payload)
    End Sub

    ' --------------------------------------------------------------- purchase

    ''' <summary>
    ''' Enqueues the purchase bill identified by (saveType, invNo). Call after the
    ''' lines are written, before Commit.
    ''' </summary>
    Friend Shared Sub EnqueuePurchase(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String, ByVal operation As String)
        If Not IsAvailable OrElse trans Is Nothing OrElse invNo = "" Then Exit Sub

        Dim snap As SyncSnapshot = ReadPurchaseHeader(trans, saveType, invNo)
        If snap Is Nothing Then Exit Sub
        snap.Lines = ReadPurchaseLines(trans, saveType, invNo)

        Dim eventId As Guid = Guid.NewGuid()
        Dim payload As String = BuildPurchasePayload(eventId, operation, snap)
        InsertEvent(trans, eventId, "PURCHASE", invNo, operation, snap.SaveType, snap.AcYear, snap.BillNo, payload)
    End Sub

    ' ------------------------------------------------------------------ reads

    Private Shared Function ReadSaleHeader(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String) As SyncSnapshot
        Dim sql As String = _
            "SELECT TOP 1 Sa_SaveType_vc, Sa_InvNo_vc, Sa_BillNo_n, Sa_AcYear_vc, " & _
            "Sa_BillDate_d, Sa_BillTime_vc, Sa_CusID_n, Sa_CusName_vc, Sa_BType_vc, " & _
            "Sa_GSTTotal_n, Sa_TotalAmt_n, Sa_RoType_vc, Sa_RoAmt_n, Sa_FinalTotal_n, " & _
            "Sa_MRPTotal_n, Sa_BefDisAmt_n, Sa_DiscountAmt_n, Sa_UserID_vc, " & _
            "Sa_EntDate_d, Sa_UpDate_d " & _
            "FROM tbl_SalDetail WHERE Sa_InvNo_vc = @I AND Sa_SaveType_vc = @T " & _
            "ORDER BY Sa_RecordNo_i DESC"

        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@T", saveType)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If Not rd.Read() Then Return Nothing
                Dim snap As New SyncSnapshot()
                snap.SaveType = Txt(rd(0))
                snap.InvoiceNo = Txt(rd(1))
                snap.BillNo = Dec(rd(2))
                snap.AcYear = Txt(rd(3))
                snap.BillDate = Dt(rd(4))
                snap.BillTime = Txt(rd(5))
                snap.PartyId = CInt(Dec(rd(6)))
                snap.PartyName = Txt(rd(7))
                snap.BType = Txt(rd(8))
                snap.GstTotal = Dec(rd(9))
                snap.TotalAmt = Dec(rd(10))
                snap.RoType = Txt(rd(11))
                snap.RoAmt = Dec(rd(12))
                snap.FinalTotal = Dec(rd(13))
                snap.MrpTotal = Dec(rd(14))
                snap.BefDisAmt = Dec(rd(15))
                snap.DiscountAmt = Dec(rd(16))
                snap.UserId = Txt(rd(17))
                snap.EnteredAt = Dt(rd(18))
                snap.UpdatedAt = Dt(rd(19))
                Return snap
            End Using
        End Using
    End Function

    Private Shared Function ReadSaleLines(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String) As List(Of SyncLine)
        Dim sql As String = _
            "SELECT Sa_StNumber_n, Sa_StCode_n, Sa_PurRate_n, Sa_MRP_n, Sa_SaleRate_n, " & _
            "Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n, Sa_Qty_i, Sa_FQty_i, Sa_TQty_i, " & _
            "Sa_QType_vc, Sa_Pack_vc, Sa_Taxable_n, Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n, " & _
            "Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n, Sa_ProTotal_n " & _
            "FROM tbl_SalParticular WHERE Sa_InvNo_vc = @I AND Sa_SaveTy_vc = @T " & _
            "ORDER BY Sa_RecordNo2_i"

        Dim rows As New List(Of SyncLine)()
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@T", saveType)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    Dim l As New SyncLine()
                    l.StNumber = Dec(rd(0))
                    l.ItemCode = CInt(Dec(rd(1)))
                    l.PurRate = Dec(rd(2))
                    l.Mrp = Dec(rd(3))
                    l.SaleRate = Dec(rd(4))
                    l.DiscountPercent = Dec(rd(5))
                    l.DiscountAmount = Dec(rd(6))
                    l.NetRate = Dec(rd(7))
                    l.Qty = Dec(rd(8))
                    l.FreeQty = Dec(rd(9))
                    l.TotalQty = Dec(rd(10))
                    l.QtyType = Txt(rd(11))
                    l.Pack = Txt(rd(12))
                    l.Taxable = Dec(rd(13))
                    l.CgstPercent = Dec(rd(14))
                    l.SgstPercent = Dec(rd(15))
                    l.IgstPercent = Dec(rd(16))
                    l.CgstAmount = Dec(rd(17))
                    l.SgstAmount = Dec(rd(18))
                    l.IgstAmount = Dec(rd(19))
                    l.LineTotal = Dec(rd(20))
                    rows.Add(l)
                End While
            End Using
        End Using
        Return rows
    End Function

    Private Shared Function ReadPurchaseHeader(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String) As SyncSnapshot
        ' tbl_PurchDetail has no Sa_BefDisAmt_n / Sa_DiscountAmt_n columns; do not
        ' share the sales query, or this would fail on a real database.
        Dim sql As String = _
            "SELECT TOP 1 Sa_SaveType_vc, Sa_InvNo_vc, Sa_BillNo_n, Sa_AcYear_vc, " & _
            "Sa_BillDate_d, Sa_BillTime_vc, Sa_CusID_n, Sa_CusName_vc, Sa_BType_vc, " & _
            "Sa_GSTTotal_n, Sa_TotalAmt_n, Sa_RoType_vc, Sa_RoAmt_n, Sa_FinalTotal_n, " & _
            "Sa_MRPTotal_n, Sa_UserID_vc, Sa_EntDate_d, Sa_UpDate_d " & _
            "FROM tbl_PurchDetail WHERE Sa_InvNo_vc = @I AND Sa_SaveType_vc = @T " & _
            "ORDER BY Sa_RecordNo_i DESC"

        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@T", saveType)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                If Not rd.Read() Then Return Nothing
                Dim snap As New SyncSnapshot()
                snap.SaveType = Txt(rd(0))
                snap.InvoiceNo = Txt(rd(1))
                snap.BillNo = Dec(rd(2))
                snap.AcYear = Txt(rd(3))
                snap.BillDate = Dt(rd(4))
                snap.BillTime = Txt(rd(5))
                snap.PartyId = CInt(Dec(rd(6)))
                snap.PartyName = Txt(rd(7))
                snap.BType = Txt(rd(8))
                snap.GstTotal = Dec(rd(9))
                snap.TotalAmt = Dec(rd(10))
                snap.RoType = Txt(rd(11))
                snap.RoAmt = Dec(rd(12))
                snap.FinalTotal = Dec(rd(13))
                snap.MrpTotal = Dec(rd(14))
                snap.UserId = Txt(rd(15))
                snap.EnteredAt = Dt(rd(16))
                snap.UpdatedAt = Dt(rd(17))
                Return snap
            End Using
        End Using
    End Function

    Private Shared Function ReadPurchaseLines(ByVal trans As SqlTransaction, ByVal saveType As String, ByVal invNo As String) As List(Of SyncLine)
        Dim sql As String = _
            "SELECT Sa_StNumber_n, Sa_StCode_n, Sa_PurRate_n, Sa_MRP_n, Sa_SaleRate_n, " & _
            "Sa_ProDPer_n, Sa_ProDAmt_n, Sa_NetRate_n, Sa_Qty_i, Sa_FQty_i, Sa_TQty_i, " & _
            "Sa_QType_vc, Sa_Pack_vc, Sa_Taxable_n, Sa_CGSTP_n, Sa_SGSTP_n, Sa_IGSTP_n, " & _
            "Sa_CGSTAmt_n, Sa_SGSTAmt_n, Sa_IGSTAmt_n, Sa_ProTotal_n " & _
            "FROM tbl_PurchParticular WHERE Sa_InvNo_vc = @I AND Sa_SaveTy_vc = @T " & _
            "ORDER BY Sa_RecordNo2_i"

        Dim rows As New List(Of SyncLine)()
        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@I", invNo)
            cmd.Parameters.AddWithValue("@T", saveType)
            Using rd As SqlDataReader = cmd.ExecuteReader()
                While rd.Read()
                    Dim l As New SyncLine()
                    l.StNumber = Dec(rd(0))
                    l.ItemCode = CInt(Dec(rd(1)))
                    l.PurRate = Dec(rd(2))
                    l.Mrp = Dec(rd(3))
                    l.SaleRate = Dec(rd(4))
                    l.DiscountPercent = Dec(rd(5))
                    l.DiscountAmount = Dec(rd(6))
                    l.NetRate = Dec(rd(7))
                    l.Qty = Dec(rd(8))
                    l.FreeQty = Dec(rd(9))
                    l.TotalQty = Dec(rd(10))
                    l.QtyType = Txt(rd(11))
                    l.Pack = Txt(rd(12))
                    l.Taxable = Dec(rd(13))
                    l.CgstPercent = Dec(rd(14))
                    l.SgstPercent = Dec(rd(15))
                    l.IgstPercent = Dec(rd(16))
                    l.CgstAmount = Dec(rd(17))
                    l.SgstAmount = Dec(rd(18))
                    l.IgstAmount = Dec(rd(19))
                    l.LineTotal = Dec(rd(20))
                    rows.Add(l)
                End While
            End Using
        End Using
        Return rows
    End Function

    ' ------------------------------------------------------------------ write

    Private Shared Sub InsertEvent(ByVal trans As SqlTransaction, ByVal eventId As Guid, _
                                   ByVal entityType As String, ByVal entityKey As String, ByVal operation As String, _
                                   ByVal saveType As String, ByVal acYear As String, ByVal billNo As Decimal, _
                                   ByVal payload As String)
        Dim sql As String = _
            "INSERT INTO dbo.SM_SyncOutbox " & _
            "(EventID, EntityType, EntityKey, Operation, SaveType, AcYear, BillNo, " & _
            " PayloadVersion, Payload, ContentHash, StoreID, OccurredAtUtc, Status, " & _
            " AttemptCount, NextAttemptAtUtc) " & _
            "VALUES (@EventID, @EntityType, @EntityKey, @Operation, @SaveType, @AcYear, @BillNo, " & _
            " 1, @Payload, @ContentHash, @StoreID, @OccurredAtUtc, 'PENDING', 0, @OccurredAtUtc)"

        Dim nowUtc As DateTime = DateTime.UtcNow

        Using cmd As New SqlCommand(sql, VistawinFunctions.ServerConnection, trans)
            cmd.Parameters.AddWithValue("@EventID", eventId)
            cmd.Parameters.AddWithValue("@EntityType", Fit(entityType, 30))
            cmd.Parameters.AddWithValue("@EntityKey", Fit(entityKey, 120))
            cmd.Parameters.AddWithValue("@Operation", Fit(operation, 10))
            If saveType Is Nothing Then
                cmd.Parameters.AddWithValue("@SaveType", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@SaveType", Fit(saveType, 30))
            End If
            If acYear Is Nothing Then
                cmd.Parameters.AddWithValue("@AcYear", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@AcYear", Fit(acYear, 20))
            End If
            If billNo = 0 Then
                cmd.Parameters.AddWithValue("@BillNo", DBNull.Value)
            Else
                cmd.Parameters.AddWithValue("@BillNo", billNo)
            End If
            cmd.Parameters.AddWithValue("@Payload", payload)
            cmd.Parameters.AddWithValue("@ContentHash", Sha256Hex(payload))
            cmd.Parameters.AddWithValue("@StoreID", Fit(_storeId, 50))
            cmd.Parameters.AddWithValue("@OccurredAtUtc", nowUtc)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' --------------------------------------------------------------- payloads

    Private Shared Function BuildSalePayload(ByVal eventId As Guid, ByVal operation As String, ByVal s As SyncSnapshot) As String
        Dim sb As New StringBuilder()
        sb.Append("{")
        Head(sb, eventId, "SALE", operation)
        sb.Append(",""bill"":{")
        BillFields(sb, s, True)
        sb.Append("},""lines"":[")
        Dim i As Integer = 0
        For Each l As SyncLine In s.Lines
            If i > 0 Then sb.Append(",")
            sb.Append("{")
            LineFields(sb, l)
            sb.Append("}")
            i += 1
        Next
        sb.Append("]}")
        Return sb.ToString()
    End Function

    Private Shared Function BuildPurchasePayload(ByVal eventId As Guid, ByVal operation As String, ByVal s As SyncSnapshot) As String
        Dim sb As New StringBuilder()
        sb.Append("{")
        Head(sb, eventId, "PURCHASE", operation)
        sb.Append(",""bill"":{")
        BillFields(sb, s, False)
        sb.Append("},""lines"":[")
        Dim i As Integer = 0
        For Each l As SyncLine In s.Lines
            If i > 0 Then sb.Append(",")
            sb.Append("{")
            LineFields(sb, l)
            sb.Append("}")
            i += 1
        Next
        sb.Append("]}")
        Return sb.ToString()
    End Function

    Private Shared Sub Head(ByVal sb As StringBuilder, ByVal eventId As Guid, ByVal entity As String, ByVal operation As String)
        sb.Append("""eventId"":""").Append(eventId.ToString("D")).Append("""")
        sb.Append(",""storeId"":").Append(JsonText(_storeId))
        sb.Append(",""entity"":").Append(JsonText(entity))
        sb.Append(",""operation"":").Append(JsonText(operation))
        sb.Append(",""occurredAtUtc"":").Append(JsonText(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)))
        sb.Append(",""schemaVersion"":1")
    End Sub

    Private Shared Sub BillFields(ByVal sb As StringBuilder, ByVal s As SyncSnapshot, ByVal isSale As Boolean)
        sb.Append("""saveType"":").Append(JsonText(s.SaveType))
        sb.Append(",""invoiceNo"":").Append(JsonText(s.InvoiceNo))
        sb.Append(",""billNo"":").Append(JsonNumber(s.BillNo))
        sb.Append(",""acYear"":").Append(JsonText(s.AcYear))
        sb.Append(",""billDate"":").Append(JsonText(DateText(s.BillDate)))
        sb.Append(",""billTime"":").Append(JsonText(s.BillTime))
        sb.Append(",""bType"":").Append(JsonText(s.BType))
        sb.Append(",""partyId"":").Append(s.PartyId.ToString(CultureInfo.InvariantCulture))
        sb.Append(",""partyName"":").Append(JsonText(s.PartyName))
        sb.Append(",""gstTotal"":").Append(JsonNumber(s.GstTotal))
        sb.Append(",""totalAmt"":").Append(JsonNumber(s.TotalAmt))
        If isSale Then
            sb.Append(",""beforeDiscountAmt"":").Append(JsonNumber(s.BefDisAmt))
            sb.Append(",""discountAmt"":").Append(JsonNumber(s.DiscountAmt))
        End If
        sb.Append(",""roundOffType"":").Append(JsonText(s.RoType))
        sb.Append(",""roundOffAmt"":").Append(JsonNumber(s.RoAmt))
        sb.Append(",""finalTotal"":").Append(JsonNumber(s.FinalTotal))
        sb.Append(",""mrpTotal"":").Append(JsonNumber(s.MrpTotal))
        sb.Append(",""userId"":").Append(JsonText(s.UserId))
        sb.Append(",""enteredAt"":").Append(JsonText(DateText(s.EnteredAt)))
        sb.Append(",""updatedAt"":").Append(JsonText(DateText(s.UpdatedAt)))
        sb.Append(",""isReturn"":").Append(If(IsReturnType(s.SaveType), "true", "false"))
    End Sub

    Private Shared Sub LineFields(ByVal sb As StringBuilder, ByVal l As SyncLine)
        sb.Append("""stockNumber"":").Append(JsonNumber(l.StNumber))
        sb.Append(",""itemCode"":").Append(l.ItemCode.ToString(CultureInfo.InvariantCulture))
        sb.Append(",""purchaseRate"":").Append(JsonNumber(l.PurRate))
        sb.Append(",""mrp"":").Append(JsonNumber(l.Mrp))
        sb.Append(",""saleRate"":").Append(JsonNumber(l.SaleRate))
        sb.Append(",""discountPercent"":").Append(JsonNumber(l.DiscountPercent))
        sb.Append(",""discountAmount"":").Append(JsonNumber(l.DiscountAmount))
        sb.Append(",""netRate"":").Append(JsonNumber(l.NetRate))
        sb.Append(",""qty"":").Append(JsonNumber(l.Qty))
        sb.Append(",""freeQty"":").Append(JsonNumber(l.FreeQty))
        sb.Append(",""totalQty"":").Append(JsonNumber(l.TotalQty))
        sb.Append(",""qtyType"":").Append(JsonText(l.QtyType))
        sb.Append(",""pack"":").Append(JsonText(l.Pack))
        sb.Append(",""taxable"":").Append(JsonNumber(l.Taxable))
        sb.Append(",""cgstPercent"":").Append(JsonNumber(l.CgstPercent))
        sb.Append(",""sgstPercent"":").Append(JsonNumber(l.SgstPercent))
        sb.Append(",""igstPercent"":").Append(JsonNumber(l.IgstPercent))
        sb.Append(",""cgstAmount"":").Append(JsonNumber(l.CgstAmount))
        sb.Append(",""sgstAmount"":").Append(JsonNumber(l.SgstAmount))
        sb.Append(",""igstAmount"":").Append(JsonNumber(l.IgstAmount))
        sb.Append(",""lineTotal"":").Append(JsonNumber(l.LineTotal))
    End Sub

    ' ------------------------------------------------------------------- json

    Private Shared Function JsonText(ByVal value As String) As String
        If value Is Nothing Then Return "null"
        Dim sb As New StringBuilder(value.Length + 2)
        sb.Append(""""c)
        For Each ch As Char In value
            Select Case ch
                Case """"c
                    sb.Append("\""")
                Case "\"c
                    sb.Append("\\")
                Case vbBack
                    sb.Append("\b")
                Case vbFormFeed
                    sb.Append("\f")
                Case vbLf
                    sb.Append("\n")
                Case vbCr
                    sb.Append("\r")
                Case vbTab
                    sb.Append("\t")
                Case Else
                    If AscW(ch) < 32 Then
                        sb.Append("\u").Append(AscW(ch).ToString("x4", CultureInfo.InvariantCulture))
                    Else
                        sb.Append(ch)
                    End If
            End Select
        Next
        sb.Append(""""c)
        Return sb.ToString()
    End Function

    Private Shared Function JsonNumber(ByVal value As Decimal) As String
        Return value.ToString(CultureInfo.InvariantCulture)
    End Function

    ' ------------------------------------------------------------------ utils

    Private Shared Function Txt(ByVal v As Object) As String
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return ""
        Return Convert.ToString(v)
    End Function

    Private Shared Function Dec(ByVal v As Object) As Decimal
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return 0D
        Return Convert.ToDecimal(v, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function Dt(ByVal v As Object) As DateTime?
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return Nothing
        Return Convert.ToDateTime(v, CultureInfo.InvariantCulture)
    End Function

    Private Shared Function DateText(ByVal v As DateTime?) As String
        If Not v.HasValue Then Return Nothing
        Return v.Value.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function IsReturnType(ByVal saveType As String) As Boolean
        If saveType Is Nothing Then Return False
        Return saveType.IndexOf("RETURN", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Shared Function Fit(ByVal value As String, ByVal maxLen As Integer) As String
        If value Is Nothing Then Return ""
        If value.Length <= maxLen Then Return value
        Return value.Substring(0, maxLen)
    End Function

    Private Shared Function Sha256Hex(ByVal text As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes As Byte() = Encoding.UTF8.GetBytes(text)
            Dim hash As Byte() = sha.ComputeHash(bytes)
            Dim sb As New StringBuilder(hash.Length * 2)
            For Each b As Byte In hash
                sb.Append(b.ToString("x2", CultureInfo.InvariantCulture))
            Next
            Return sb.ToString()
        End Using
    End Function

    ' ----------------------------------------------------------------- models

    Private Class SyncSnapshot
        Friend SaveType As String = ""
        Friend InvoiceNo As String = ""
        Friend BillNo As Decimal = 0
        Friend AcYear As String = ""
        Friend BillDate As DateTime? = Nothing
        Friend BillTime As String = ""
        Friend PartyId As Integer = 0
        Friend PartyName As String = ""
        Friend BType As String = ""
        Friend GstTotal As Decimal = 0
        Friend TotalAmt As Decimal = 0
        Friend RoType As String = ""
        Friend RoAmt As Decimal = 0
        Friend FinalTotal As Decimal = 0
        Friend MrpTotal As Decimal = 0
        Friend BefDisAmt As Decimal = 0
        Friend DiscountAmt As Decimal = 0
        Friend UserId As String = ""
        Friend EnteredAt As DateTime? = Nothing
        Friend UpdatedAt As DateTime? = Nothing
        Friend Lines As List(Of SyncLine) = New List(Of SyncLine)()
    End Class

    Private Class SyncLine
        Friend StNumber As Decimal = 0
        Friend ItemCode As Integer = 0
        Friend PurRate As Decimal = 0
        Friend Mrp As Decimal = 0
        Friend SaleRate As Decimal = 0
        Friend DiscountPercent As Decimal = 0
        Friend DiscountAmount As Decimal = 0
        Friend NetRate As Decimal = 0
        Friend Qty As Decimal = 0
        Friend FreeQty As Decimal = 0
        Friend TotalQty As Decimal = 0
        Friend QtyType As String = ""
        Friend Pack As String = ""
        Friend Taxable As Decimal = 0
        Friend CgstPercent As Decimal = 0
        Friend SgstPercent As Decimal = 0
        Friend IgstPercent As Decimal = 0
        Friend CgstAmount As Decimal = 0
        Friend SgstAmount As Decimal = 0
        Friend IgstAmount As Decimal = 0
        Friend LineTotal As Decimal = 0
    End Class

End Class
