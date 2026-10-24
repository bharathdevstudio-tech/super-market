# End-to-end test of the REAL enqueue code path.
#
#   * loads the built BharathSuperMarket.exe (x86 -> run under 32-bit PowerShell)
#   * points VistawinFunctions.ServerConnection at the SM_SyncVerify scratch DB
#   * calls SmSyncQueue.EnsurePrepared / EnqueueSale / EnqueueSaleDeleted /
#     EnqueuePurchase exactly as the forms do, inside a real transaction
#   * verifies the committed outbox rows, their JSON and their SHA-256 hashes
#
# Nothing touches SuperMarketDB. Run tools\setup-sync-scratch.sql first.

$ErrorActionPreference = 'Stop'

$exe = (Resolve-Path (Join-Path $PSScriptRoot '..\Windows Application\bin\Debug\BharathSuperMarket.exe')).Path
$asm = [Reflection.Assembly]::LoadFrom($exe)

$flagsInstanceStatic = [Reflection.BindingFlags]'NonPublic,Public,Static'
$qn = 'Server=.\SQLEXPRESS;Initial Catalog=SM_SyncVerify;Integrated Security=True'

# --- point the app's shared connection at the scratch DB ---------------------
$vf = $asm.GetType('WindowsApplication.VistawinFunctions')
if ($null -eq $vf) { throw 'VistawinFunctions type not found' }
$connField = $vf.GetField('ServerConnection', $flagsInstanceStatic)
if ($null -eq $connField) { throw 'ServerConnection field not found' }

$conn = New-Object System.Data.SqlClient.SqlConnection $qn
$conn.Open()
$connField.SetValue($null, $conn)

$t = $asm.GetType('WindowsApplication.SmSyncQueue')
if ($null -eq $t) { throw 'SmSyncQueue type not found' }

$ensure = $t.GetMethod('EnsurePrepared', $flagsInstanceStatic)
$ensure.Invoke($null, @())

$isAvailable = $t.GetProperty('IsAvailable', $flagsInstanceStatic).GetValue($null, $null)
$results = @()
$results += [pscustomobject]@{
    Check  = 'EnsurePrepared / IsAvailable'
    Result = if ($isAvailable -eq $true) { 'PASS' } else { 'FAIL' }
    Detail = "IsAvailable=$isAvailable"
}
if ($isAvailable -ne $true) { $results | Format-Table -AutoSize; exit 1 }

# --- enqueue exactly like the forms do --------------------------------------
$enqueueSale = $t.GetMethod('EnqueueSale', $flagsInstanceStatic)
$enqueueSaleDel = $t.GetMethod('EnqueueSaleDeleted', $flagsInstanceStatic)
$enqueuePurch = $t.GetMethod('EnqueuePurchase', $flagsInstanceStatic)

$trans = $conn.BeginTransaction()
try {
    $enqueueSale.Invoke($null, @($trans, 'INVOICE', 'INV42', 'INSERT'))
    $enqueueSale.Invoke($null, @($trans, 'INVOICE', 'INV42', 'UPDATE'))
    $enqueueSaleDel.Invoke($null, @($trans, 'INVOICE', 'INV43'))
    $enqueuePurch.Invoke($null, @($trans, 'PURCHASE', 'PUR7', 'INSERT'))
    $trans.Commit()
} catch {
    try { $trans.Rollback() } catch { }
    throw
} finally {
    $trans.Dispose()
}

# --- read back ---------------------------------------------------------------
$cmd = $conn.CreateCommand()
$cmd.CommandText = "SELECT OutboxID, EventID, EntityType, EntityKey, Operation, SaveType, AcYear, BillNo, Payload, ContentHash, Status, AttemptCount, StoreID FROM dbo.SM_SyncOutbox ORDER BY OutboxID"
$reader = $cmd.ExecuteReader()
$rows = @()
while ($reader.Read()) {
    $rows += [pscustomobject]@{
        OutboxID = $reader.GetInt64(0)
        EventID  = $reader.GetGuid(1).ToString()
        Entity   = $reader.GetString(2)
        Key      = $reader.GetString(3)
        Op       = $reader.GetString(4)
        SaveType = if ($reader.IsDBNull(5)) { '' } else { $reader.GetString(5) }
        AcYear   = if ($reader.IsDBNull(6)) { '' } else { $reader.GetString(6) }
        BillNo   = if ($reader.IsDBNull(7)) { 0 } else { $reader.GetDecimal(7) }
        Payload  = $reader.GetString(8)
        Hash     = $reader.GetString(9)
        Status   = $reader.GetString(10)
        Attempts = $reader.GetInt32(11)
        StoreID  = $reader.GetString(12)
    }
}
$reader.Close()

$results += [pscustomobject]@{
    Check  = 'row count'
    Result = if ($rows.Count -eq 4) { 'PASS' } else { 'FAIL' }
    Detail = "$($rows.Count) rows"
}

$expectedOps = @('INSERT', 'UPDATE', 'DELETE', 'INSERT')
$expectedKeys = @('INV42', 'INV42', 'INV43', 'PUR7')
for ($i = 0; $i -lt $rows.Count; $i++) {
    $r = $rows[$i]
    $results += [pscustomobject]@{
        Check  = "row $i key/op"
        Result = if ($r.Key -eq $expectedKeys[$i] -and $r.Op -eq $expectedOps[$i]) { 'PASS' } else { 'FAIL' }
        Detail = "$($r.Entity)/$($r.Key)/$($r.Op)"
    }
}

# --- hash must match the payload bytes --------------------------------------
$sha = [System.Security.Cryptography.SHA256]::Create()
foreach ($r in $rows) {
    $bytes = [System.Text.Encoding]::UTF8.GetBytes($r.Payload)
    $hashBytes = $sha.ComputeHash($bytes)
    $hex = ($hashBytes | ForEach-Object { $_.ToString('x2') }) -join ''
    $results += [pscustomobject]@{
        Check  = "hash $($r.Key)/$($r.Op)"
        Result = if ($hex -eq $r.Hash) { 'PASS' } else { 'FAIL' }
        Detail = ''
    }
}

# --- payload must be valid JSON with the expected shape ---------------------
foreach ($r in $rows) {
    try {
        $p = $r.Payload | ConvertFrom-Json
        $ok = ($p.eventId -eq $r.EventID) -and
              ($p.operation -eq $r.Op) -and
              ($p.bill.invoiceNo -eq $r.Key) -and
              ([string]$p.bill.saveType -eq $r.SaveType) -and
              ([string]$p.bill.acYear -eq $r.AcYear) -and
              ([decimal]$p.bill.billNo -eq $r.BillNo)
        if ($r.Op -eq 'DELETE') {
            $ok = $ok -and ($p.lines.Count -eq 0)
        } else {
            $ok = $ok -and ($p.lines.Count -gt 0)
        }
        $results += [pscustomobject]@{
            Check  = "payload $($r.Key)/$($r.Op)"
            Result = if ($ok) { 'PASS' } else { 'FAIL' }
            Detail = "lines=$($p.lines.Count)"
        }
    } catch {
        $results += [pscustomobject]@{ Check = "payload $($r.Key)/$($r.Op)"; Result = 'FAIL'; Detail = $_.Exception.Message }
    }
}

# --- line values survived the round trip -----------------------------------
$insert42 = $rows | Where-Object { $_.Key -eq 'INV42' -and $_.Op -eq 'INSERT' }
$p42 = $insert42[0].Payload | ConvertFrom-Json
$results += [pscustomobject]@{
    Check  = 'INV42 line detail'
    Result = if ($p42.lines.Count -eq 2 -and
                   [decimal]$p42.lines[0].stockNumber -eq 9001 -and
                   [decimal]$p42.lines[0].qty -eq 1 -and
                   [decimal]$p42.lines[1].stockNumber -eq 9002 -and
                   [decimal]$p42.bill.finalTotal -eq 100.20 -and
                   [decimal]$p42.bill.discountAmt -eq 0) { 'PASS' } else { 'FAIL' }
    Detail = "lines=$($p42.lines.Count)"
}

$purch = $rows | Where-Object { $_.Entity -eq 'PURCHASE' }
$pp = $purch[0].Payload | ConvertFrom-Json
$results += [pscustomobject]@{
    Check  = 'purchase payload'
    Result = if ($pp.entity -eq 'PURCHASE' -and $pp.lines.Count -eq 1 -and
                   [decimal]$pp.lines[0].qty -eq 10 -and
                   $null -eq $pp.bill.discountAmt) { 'PASS' } else { 'FAIL' }
    Detail = "lines=$($pp.lines.Count), hasDiscount=$($null -ne $pp.bill.discountAmt)"
}

# --- exactly one outbox row per event, unique ids ---------------------------
$uniqueIds = ($rows | Select-Object -ExpandProperty EventID -Unique).Count
$results += [pscustomobject]@{
    Check  = 'unique EventIDs'
    Result = if ($uniqueIds -eq $rows.Count) { 'PASS' } else { 'FAIL' }
    Detail = "$uniqueIds unique of $($rows.Count)"
}

# --- capability check must be honest ---------------------------------------
$results += [pscustomobject]@{
    Check  = 'status/attempt seeds'
    Result = if (($rows | Where-Object { $_.Status -ne 'PENDING' -or $_.Attempts -ne 0 }).Count -eq 0) { 'PASS' } else { 'FAIL' }
    Detail = ''
}

$conn.Close()

$results | Format-Table -AutoSize
$failed = $results | Where-Object { $_.Result -ne 'PASS' }
if ($failed) {
    Write-Host ('FAILED: ' + $failed.Count) -ForegroundColor Red
    exit 1
}
Write-Host 'All enqueue checks passed.' -ForegroundColor Green
