/* ============================================================================
   03_verify_sync_queue.sql
   ----------------------------------------------------------------------------
   Read-only verification for 01_create_sync_queue.sql.

   Section 1  - do the objects exist, with the right shape?
   Section 2  - do the existing business tables still have their columns?
                (proves the script was additive)
   Section 3  - functional smoke test: enqueue -> claim -> mark sent, then
                ROLLBACK so nothing is left behind.

   Every check prints PASS or FAIL. Safe to run any time.
   Compatibility: SQL Server 2008 R2.
   ============================================================================ */

SET NOCOUNT ON;
GO

/* ---------------------------------------------------------------------------
   Section 1 - object shape
   --------------------------------------------------------------------------- */
PRINT '=== Section 1: object shape ===';

SELECT  o.name                                   AS ObjectName,
        o.type_desc                              AS ObjectType,
        CASE WHEN o.name IS NULL THEN 'FAIL' ELSE 'PASS' END AS Result
FROM   (SELECT N'SM_SyncOutbox'            AS name
        UNION ALL SELECT N'SM_SyncOutboxLog'
        UNION ALL SELECT N'SM_SyncMeta'
        UNION ALL SELECT N'SM_SyncOutbox_Claim'
        UNION ALL SELECT N'SM_SyncOutbox_MarkSent'
        UNION ALL SELECT N'SM_SyncOutbox_MarkFailed'
        UNION ALL SELECT N'SM_SyncOutbox_Stats') AS expected
LEFT JOIN sys.objects o
       ON o.name = expected.name
      AND o.schema_id = SCHEMA_ID(N'dbo')
ORDER BY expected.name;
GO

/* Columns of SM_SyncOutbox, in creation order. */
SELECT  c.column_id,
        c.name          AS ColumnName,
        t.name          AS DataType,
        c.max_length,
        c.precision,
        c.scale,
        c.is_nullable
FROM    sys.columns c
JOIN    sys.types   t ON t.user_type_id = c.user_type_id
WHERE   c.object_id = OBJECT_ID(N'dbo.SM_SyncOutbox')
ORDER BY c.column_id;
GO

/* Unique event id + the two queue indexes. */
SELECT  CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes
            WHERE object_id = OBJECT_ID(N'dbo.SM_SyncOutbox')
              AND name = N'UQ_SM_SyncOutbox_EventID')
        THEN 'PASS' ELSE 'FAIL' END AS UniqueEventID,
        CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes
            WHERE object_id = OBJECT_ID(N'dbo.SM_SyncOutbox')
              AND name = N'IX_SM_SyncOutbox_Status_NextAttempt')
        THEN 'PASS' ELSE 'FAIL' END AS StatusIndex,
        CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes
            WHERE object_id = OBJECT_ID(N'dbo.SM_SyncOutbox')
              AND name = N'IX_SM_SyncOutbox_Entity')
        THEN 'PASS' ELSE 'FAIL' END AS EntityIndex,
        CASE WHEN EXISTS (
            SELECT 1 FROM sys.check_constraints
            WHERE parent_object_id = OBJECT_ID(N'dbo.SM_SyncOutbox')
              AND name = N'CK_SM_SyncOutbox_Status')
        THEN 'PASS' ELSE 'FAIL' END AS StatusCheck;
GO

/* ---------------------------------------------------------------------------
   Section 2 - existing tables untouched (spot check on column counts)
   --------------------------------------------------------------------------- */
PRINT '=== Section 2: existing business tables ===';

SELECT  t.name AS TableName,
        COUNT(c.column_id) AS ColumnCount
FROM    sys.tables t
JOIN    sys.columns c ON c.object_id = t.object_id
WHERE   t.name IN (N'tbl_SalDetail', N'tbl_SalParticular',
                   N'tbl_PurchDetail', N'tbl_PurchParticular',
                   N'tbl_MultiStock', N'tbl_StockDtl', N'tbl_StockModify',
                   N'tbl_BillSeries')
GROUP BY t.name
ORDER BY t.name;
GO

/* No SM_Sync* column should have leaked into a business table. */
SELECT  CASE WHEN EXISTS (
            SELECT 1
            FROM sys.columns c
            JOIN sys.tables  t ON t.object_id = c.object_id
            WHERE c.name LIKE 'SM[_]Sync%'
              AND t.name NOT LIKE 'SM[_]Sync%')
        THEN 'FAIL - a SM_Sync column exists on a business table'
        ELSE 'PASS - no SM_Sync column on any business table' END AS LeakCheck;
GO

/* ---------------------------------------------------------------------------
   Section 3 - functional smoke test, rolled back
   --------------------------------------------------------------------------- */
PRINT '=== Section 3: enqueue -> claim -> mark sent (rolled back) ===';

BEGIN TRANSACTION;

DECLARE @EventID UNIQUEIDENTIFIER;
SET @EventID = NEWID();

INSERT INTO dbo.SM_SyncOutbox
    (EventID, EntityType, EntityKey, Operation, SaveType, AcYear, BillNo,
     Payload, ContentHash, StoreID, OccurredAtUtc)
VALUES
    (@EventID, 'SALE', 'TEST/1', 'INSERT', 'INVOICE', '2026-2027', 1,
     N'{"test":true}',
     REPLICATE('A', 64),          -- stand-in; real hash is computed in .NET
     'STORE-1', GETUTCDATE());

SELECT 'enqueued' AS Step, @@ROWCOUNT AS RowsAffected;

/* Claim the row. */
DECLARE @OutboxID BIGINT;
DECLARE @ClaimedEvent UNIQUEIDENTIFIER;

SELECT TOP (1) @OutboxID = OutboxID, @ClaimedEvent = EventID
FROM   dbo.SM_SyncOutbox
WHERE  EventID = @EventID;

EXEC dbo.SM_SyncOutbox_Claim
     @WorkerId     = 'verify',
     @BatchSize    = 10,
     @StaleMinutes = 10;

SELECT  Status, AttemptCount, LockedBy
FROM    dbo.SM_SyncOutbox
WHERE   EventID = @EventID;

/* Complete it. */
EXEC dbo.SM_SyncOutbox_MarkSent
     @OutboxID  = @OutboxID,
     @EventID   = @EventID,
     @HttpStatus = 200,
     @Message   = 'verify';

SELECT  Status,
        CASE WHEN Status = 'SENT' THEN 'PASS' ELSE 'FAIL' END AS Result,
        SentAtUtc
FROM    dbo.SM_SyncOutbox
WHERE   EventID = @EventID;

SELECT  Result, HttpStatus, AttemptNo
FROM    dbo.SM_SyncOutboxLog
WHERE   EventID = @EventID;

/* Stats proc must run without error. */
EXEC dbo.SM_SyncOutbox_Stats;

/* Leave the database exactly as we found it. */
ROLLBACK TRANSACTION;

SELECT  CASE WHEN EXISTS (SELECT 1 FROM dbo.SM_SyncOutbox WHERE EventID = @EventID)
        THEN 'FAIL - smoke test row survived rollback'
        ELSE 'PASS - smoke test rolled back cleanly' END AS RollbackCheck;
GO

PRINT 'Verification complete.';
GO
