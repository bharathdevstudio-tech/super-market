# Negative path: with the SM_Sync* tables absent, SmSyncQueue must report
# IsAvailable=False and every enqueue call must be a silent no-op. Billing must
# not be able to fail because sync was never deployed.
#
# Uses 'master' (a database with no SM_Sync* tables) and a fresh process, so the
# cached capability flag from verify-enqueue.ps1 does not leak in.

$ErrorActionPreference = 'Stop'

$exe = (Resolve-Path (Join-Path $PSScriptRoot '..\Windows Application\bin\Debug\BharathSuperMarket.exe')).Path
$asm = [Reflection.Assembly]::LoadFrom($exe)

$flags = [Reflection.BindingFlags]'NonPublic,Public,Static'

$vf = $asm.GetType('WindowsApplication.VistawinFunctions')
$conn = New-Object System.Data.SqlClient.SqlConnection 'Server=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True'
$conn.Open()
$vf.GetField('ServerConnection', $flags).SetValue($null, $conn)

$t = $asm.GetType('WindowsApplication.SmSyncQueue')
$t.GetMethod('EnsurePrepared', $flags).Invoke($null, @())

$available = $t.GetProperty('IsAvailable', $flags).GetValue($null, $null)

$results = @()
$results += [pscustomobject]@{
    Check  = 'IsAvailable on a DB without outbox'
    Result = if ($available -eq $false) { 'PASS' } else { 'FAIL' }
    Detail = "IsAvailable=$available"
}

$trans = $conn.BeginTransaction()
$threw = $false
try {
    $t.GetMethod('EnqueueSale', $flags).Invoke($null, @($trans, 'INVOICE', 'NOPE1', 'INSERT'))
    $t.GetMethod('EnqueueSaleDeleted', $flags).Invoke($null, @($trans, 'INVOICE', 'NOPE2'))
    $t.GetMethod('EnqueuePurchase', $flags).Invoke($null, @($trans, 'PURCHASE', 'NOPE3', 'INSERT'))
    $trans.Commit()
} catch {
    $threw = $true
    try { $trans.Rollback() } catch { }
} finally {
    $trans.Dispose()
}

$results += [pscustomobject]@{
    Check  = 'enqueue calls are no-ops'
    Result = if (-not $threw) { 'PASS' } else { 'FAIL' }
    Detail = if ($threw) { 'an enqueue call threw' } else { 'no exception' }
}

$conn.Close()
$results | Format-Table -AutoSize
$failed = $results | Where-Object { $_.Result -ne 'PASS' }
if ($failed) {
    Write-Host ('FAILED: ' + $failed.Count) -ForegroundColor Red
    exit 1
}
Write-Host 'Negative path passed: billing cannot be blocked by missing sync tables.' -ForegroundColor Green
