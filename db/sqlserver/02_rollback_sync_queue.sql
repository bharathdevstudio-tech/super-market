/* ============================================================================
   02_rollback_sync_queue.sql
   ----------------------------------------------------------------------------
   Removes ONLY the objects created by 01_create_sync_queue.sql.

   It does NOT drop, alter or restore any existing table. Running this returns
   the database to exactly the state it was in before Phase 2.

   WARNING: this destroys the outbox. Any bill that is committed locally but
   not yet uploaded will never be uploaded. Run SM_SyncOutbox_Stats first and
   only proceed when PendingCount + FailedCount + SendingCount = 0, or when you
   have a full database backup.

   Safe to re-run.
   ============================================================================ */

SET NOCOUNT ON;
GO

/* Procedures first (they reference the tables). */
IF OBJECT_ID(N'dbo.SM_SyncOutbox_Stats', N'P') IS NOT NULL
    DROP PROCEDURE dbo.SM_SyncOutbox_Stats;
GO
IF OBJECT_ID(N'dbo.SM_SyncOutbox_MarkFailed', N'P') IS NOT NULL
    DROP PROCEDURE dbo.SM_SyncOutbox_MarkFailed;
GO
IF OBJECT_ID(N'dbo.SM_SyncOutbox_MarkSent', N'P') IS NOT NULL
    DROP PROCEDURE dbo.SM_SyncOutbox_MarkSent;
GO
IF OBJECT_ID(N'dbo.SM_SyncOutbox_Claim', N'P') IS NOT NULL
    DROP PROCEDURE dbo.SM_SyncOutbox_Claim;
GO

/* Tables. Indexes and constraints go with them. */
IF OBJECT_ID(N'dbo.SM_SyncOutboxLog', N'U') IS NOT NULL
    DROP TABLE dbo.SM_SyncOutboxLog;
GO
IF OBJECT_ID(N'dbo.SM_SyncMeta', N'U') IS NOT NULL
    DROP TABLE dbo.SM_SyncMeta;
GO
IF OBJECT_ID(N'dbo.SM_SyncOutbox', N'U') IS NOT NULL
    DROP TABLE dbo.SM_SyncOutbox;
GO

/* Nothing else in the schema is touched. Verify with 03_verify_sync_queue.sql. */
PRINT 'SM_Sync* objects removed. Existing tables untouched.';
GO
