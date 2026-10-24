/* Runtime behaviour of the outbox procedures, against the 4 events that
   tools\verify-enqueue.ps1 committed. Safe: scratch database only. */
SET NOCOUNT ON;

DECLARE @e1 uniqueidentifier, @e2 uniqueidentifier, @e3 uniqueidentifier, @e4 uniqueidentifier;
SELECT @e1 = EventID FROM dbo.SM_SyncOutbox WHERE OutboxID = 1;
SELECT @e2 = EventID FROM dbo.SM_SyncOutbox WHERE OutboxID = 2;
SELECT @e3 = EventID FROM dbo.SM_SyncOutbox WHERE OutboxID = 3;
SELECT @e4 = EventID FROM dbo.SM_SyncOutbox WHERE OutboxID = 4;

PRINT '--- after claim: all SENDING, attempt 1 ---';
SELECT OutboxID, Status, AttemptCount, LockedBy FROM dbo.SM_SyncOutbox ORDER BY OutboxID;

PRINT '--- MarkSent #1 ---';
EXEC dbo.SM_SyncOutbox_MarkSent @OutboxID = 1, @EventID = @e1, @HttpStatus = 200, @Message = 'ok';

PRINT '--- MarkFailed #2 with 30s backoff ---';
EXEC dbo.SM_SyncOutbox_MarkFailed @OutboxID = 2, @EventID = @e2, @BackoffSeconds = 30, @MaxAttempts = 20, @HttpStatus = 503, @Message = 'worker unavailable';

PRINT '--- state ---';
SELECT OutboxID, Status, AttemptCount,
       CASE WHEN NextAttemptAtUtc > GETUTCDATE() THEN 'FUTURE' ELSE 'DUE' END AS NextAttempt
FROM dbo.SM_SyncOutbox ORDER BY OutboxID;

PRINT '--- claim immediately: #2 not due, #3/#4 still locked -> 0 rows ---';
EXEC dbo.SM_SyncOutbox_Claim @WorkerId = 'w2', @BatchSize = 10, @StaleMinutes = 10;

PRINT '--- simulate crashed worker on #3 (lock older than stale window) ---';
UPDATE dbo.SM_SyncOutbox SET LockedAtUtc = DATEADD(MINUTE, -20, GETUTCDATE()) WHERE OutboxID = 3;

PRINT '--- claim again: only #3 should be reclaimed ---';
EXEC dbo.SM_SyncOutbox_Claim @WorkerId = 'w2', @BatchSize = 10, @StaleMinutes = 10;

PRINT '--- attempt ceiling: #3 at 19 attempts, one more failure -> DEAD ---';
UPDATE dbo.SM_SyncOutbox SET AttemptCount = 20 WHERE OutboxID = 3;
EXEC dbo.SM_SyncOutbox_MarkFailed @OutboxID = 3, @EventID = @e3, @BackoffSeconds = 60, @MaxAttempts = 20, @HttpStatus = 400, @Message = 'poison';

PRINT '--- final state ---';
SELECT OutboxID, Status, AttemptCount, LastError FROM dbo.SM_SyncOutbox ORDER BY OutboxID;

PRINT '--- log ---';
SELECT OutboxID, AttemptNo, Result, HttpStatus, Message FROM dbo.SM_SyncOutboxLog ORDER BY LogID;

PRINT '--- stats ---';
EXEC dbo.SM_SyncOutbox_Stats;

PRINT '--- DEAD row must never be claimed again ---';
EXEC dbo.SM_SyncOutbox_Claim @WorkerId = 'w3', @BatchSize = 10, @StaleMinutes = 10;
