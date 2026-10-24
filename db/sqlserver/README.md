# SQL Server side of offline-first sync

Scripts here add a **transactional outbox** to the existing SQL Server 2008 R2
database. They are additive: no existing table, column, index, trigger, view or
stored procedure is altered, dropped or renamed.

| File | Purpose |
|---|---|
| `01_create_sync_queue.sql` | Creates `SM_SyncOutbox`, `SM_SyncOutboxLog`, `SM_SyncMeta` and the `SM_SyncOutbox_*` procedures. |
| `02_rollback_sync_queue.sql` | Drops only the `SM_Sync*` objects. Leaves the database as it was before Phase 2. |
| `03_verify_sync_queue.sql` | Read-only checks: object shape, "existing tables untouched", and a rolled-back enqueue/claim/mark-sent smoke test. |

## Apply

```bat
sqlcmd -S ".\SQLEXPRESS" -E -d YourDatabaseName -b -i 01_create_sync_queue.sql
sqlcmd -S ".\SQLEXPRESS" -E -d YourDatabaseName -b -i 03_verify_sync_queue.sql
```

Both scripts are idempotent. `03` leaves no rows behind: the smoke test runs in a
transaction that is rolled back.

## Rollback

Take a backup first, then:

```bat
sqlcmd -S ".\SQLEXPRESS" -E -d YourDatabaseName -b -i 02_rollback_sync_queue.sql
```

Rolling back deletes unsent events. Check `EXEC dbo.SM_SyncOutbox_Stats;` first
and only proceed when `PendingCount`, `FailedCount` and `SendingCount` are 0.

## Status values

| Status | Meaning |
|---|---|
| `PENDING` | Waiting to be uploaded. |
| `SENDING` | Claimed by a sync worker. Reclaimed automatically if the worker dies (stale lock). |
| `SENT` | Accepted by the cloud. |
| `FAILED` | Upload failed; will retry after `NextAttemptAtUtc`. |
| `DEAD` | Attempt ceiling reached. Never claimed again; visible for inspection. |

`EventID` is a `UNIQUEIDENTIFIER` generated in .NET and uniquely constrained, so
it is the idempotency key end to end.

## Compatibility

Verified on **Microsoft SQL Server 2008 R2 SP2 (10.50.4000.0)**. The scripts
avoid `OFFSET/FETCH`, `MERGE`, `THROW`, `SEQUENCE`, `IIF`, `CONCAT` and
`HASHBYTES 'SHA2_256'` (all unavailable or unreliable on 2008 R2).
`ContentHash` is SHA-256 hex produced by .NET and passed in.
