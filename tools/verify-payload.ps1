# Validates the JSON/hash helpers in SmSyncQueue.vb by invoking them on the
# already-built assembly. Read-only: nothing connects to a database.
#
#   powershell -NoProfile -ExecutionPolicy Bypass -File tools\verify-payload.ps1

$ErrorActionPreference = 'Stop'

$exe = Join-Path $PSScriptRoot '..\Windows Application\bin\Debug\BharathSuperMarket.exe'
$exe = (Resolve-Path $exe).Path

$asm = [Reflection.Assembly]::LoadFrom($exe)
$t = $asm.GetType('WindowsApplication.SmSyncQueue')
if ($null -eq $t) { throw 'SmSyncQueue type not found in assembly' }

$flags = [Reflection.BindingFlags]'NonPublic,Public,Static'

function Invoke-Static($name, $argv) {
    $m = $t.GetMethod($name, $flags)
    if ($null -eq $m) { throw "method $name not found" }
    return $m.Invoke($null, [object[]]$argv)
}

$results = @()

# ---- 1. SHA-256 of "abc" (well-known vector) --------------------------------
$hash = Invoke-Static 'Sha256Hex' @('abc')
$expected = 'ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad'
$results += [pscustomobject]@{
    Check  = 'Sha256Hex("abc")'
    Result = if ($hash -eq $expected) { 'PASS' } else { 'FAIL' }
    Detail = $hash
}

# ---- 2. JSON escaping of quotes, backslashes, newlines, unicode ------------
$nasty = "He said ""hi"" \ path" + [char]10 + "Tamil: " + [char]0x0BAE + [char]0x0BBF
$jsonText = Invoke-Static 'JsonText' @($nasty)
try {
    $round = $jsonText | ConvertFrom-Json
    $ok = ($round -eq $nasty)
} catch {
    $ok = $false
}
$results += [pscustomobject]@{
    Check  = 'JsonText round-trip'
    Result = if ($ok) { 'PASS' } else { 'FAIL' }
    Detail = $jsonText
}

# ---- 3. null handling -------------------------------------------------------
$nullText = Invoke-Static 'JsonText' @($null)
$results += [pscustomobject]@{
    Check  = 'JsonText(null)'
    Result = if ($nullText -eq 'null') { 'PASS' } else { 'FAIL' }
    Detail = $nullText
}

# ---- 4. full payload builds and parses -------------------------------------
$snapType = $t.GetNestedType('SyncSnapshot', [Reflection.BindingFlags]'NonPublic')
$lineType = $t.GetNestedType('SyncLine', [Reflection.BindingFlags]'NonPublic')
$snap = [Activator]::CreateInstance($snapType, $true)

function Set-Field($obj, $name, $value) {
    $f = $obj.GetType().GetField($name, [Reflection.BindingFlags]'NonPublic,Public,Instance')
    if ($null -eq $f) { throw "field $name not found" }
    $f.SetValue($obj, $value)
}

Set-Field $snap 'SaveType' 'INVOICE'
Set-Field $snap 'InvoiceNo' 'INV42'
Set-Field $snap 'BillNo' ([decimal]42)
Set-Field $snap 'AcYear' '2026-2027'
Set-Field $snap 'BillDate' ([Nullable[datetime]][datetime]'2026-10-23')
Set-Field $snap 'BillTime' '10:45'
Set-Field $snap 'PartyId' 3
Set-Field $snap 'PartyName' 'CASH'
Set-Field $snap 'BType' 'CASH'
Set-Field $snap 'GstTotal' ([decimal]12.50)
Set-Field $snap 'TotalAmt' ([decimal]100)
Set-Field $snap 'RoType' 'ROUND OFF'
Set-Field $snap 'RoAmt' ([decimal]0.20)
Set-Field $snap 'FinalTotal' ([decimal]100.20)
Set-Field $snap 'MrpTotal' ([decimal]110)
Set-Field $snap 'BefDisAmt' ([decimal]100)
Set-Field $snap 'DiscountAmt' ([decimal]0)
Set-Field $snap 'UserId' 'ADMIN'
Set-Field $snap 'EnteredAt' ([Nullable[datetime]][datetime]'2026-10-23 10:45:00')
Set-Field $snap 'UpdatedAt' ([Nullable[datetime]][datetime]'2026-10-23 10:45:00')

$lines = New-Object 'System.Collections.Generic.List[object]'
$line = [Activator]::CreateInstance($lineType, $true)
Set-Field $line 'StNumber' ([decimal]9001)
Set-Field $line 'ItemCode' 501
Set-Field $line 'PurRate' ([decimal]80)
Set-Field $line 'Mrp' ([decimal]110)
Set-Field $line 'SaleRate' ([decimal]100)
Set-Field $line 'DiscountPercent' ([decimal]0)
Set-Field $line 'DiscountAmount' ([decimal]0)
Set-Field $line 'NetRate' ([decimal]100)
Set-Field $line 'Qty' ([decimal]1)
Set-Field $line 'FreeQty' ([decimal]0)
Set-Field $line 'TotalQty' ([decimal]1)
Set-Field $line 'QtyType' 'NOS'
Set-Field $line 'Pack' ''
Set-Field $line 'Taxable' ([decimal]88.50)
Set-Field $line 'CgstPercent' ([decimal]6)
Set-Field $line 'SgstPercent' ([decimal]6)
Set-Field $line 'IgstPercent' ([decimal]0)
Set-Field $line 'CgstAmount' ([decimal]6.25)
Set-Field $line 'SgstAmount' ([decimal]6.25)
Set-Field $line 'IgstAmount' ([decimal]0)
Set-Field $line 'LineTotal' ([decimal]100)
$lines.Add($line)

# the real field type is List(Of SyncLine); build it reflectively
$listType = [System.Collections.Generic.List``1].MakeGenericType($lineType)
$typedList = [Activator]::CreateInstance($listType)
$typedList.Add($line)
Set-Field $snap 'Lines' $typedList

$eventId = [Guid]::NewGuid()
$payload = Invoke-Static 'BuildSalePayload' @($eventId, 'INSERT', $snap)

try {
    $parsed = $payload | ConvertFrom-Json
    $ok = ($parsed.bill.invoiceNo -eq 'INV42') -and
          ([decimal]$parsed.bill.billNo -eq 42) -and
          ($parsed.operation -eq 'INSERT') -and
          ($parsed.entity -eq 'SALE') -and
          ($parsed.lines.Count -eq 1) -and
          ($parsed.lines[0].itemCode -eq 501) -and
          ($parsed.lines[0].stockNumber -eq 9001)
} catch {
    $ok = $false
    $results += [pscustomobject]@{ Check = 'payload parse error'; Result = 'FAIL'; Detail = $_.Exception.Message }
}
$results += [pscustomobject]@{
    Check  = 'BuildSalePayload parses'
    Result = if ($ok) { 'PASS' } else { 'FAIL' }
    Detail = ''
}

# ---- 5. delete payload has no lines ---------------------------------------
$delSnap = [Activator]::CreateInstance($snapType, $true)
Set-Field $delSnap 'SaveType' 'INVOICE'
Set-Field $delSnap 'InvoiceNo' 'INV42'
Set-Field $delSnap 'BillNo' ([decimal]42)
Set-Field $delSnap 'AcYear' '2026-2027'
Set-Field $delSnap 'Lines' ([Activator]::CreateInstance($listType))
$delPayload = Invoke-Static 'BuildSalePayload' @([Guid]::NewGuid(), 'DELETE', $delSnap)
$delParsed = $delPayload | ConvertFrom-Json
$results += [pscustomobject]@{
    Check  = 'DELETE payload'
    Result = if ($delParsed.operation -eq 'DELETE' -and $delParsed.lines.Count -eq 0) { 'PASS' } else { 'FAIL' }
    Detail = ''
}

$results | Format-Table -AutoSize

$failed = $results | Where-Object { $_.Result -ne 'PASS' }
if ($failed) {
    Write-Host ''
    Write-Host ('FAILED: ' + $failed.Count) -ForegroundColor Red
    $payload | Out-File -FilePath (Join-Path $env:TEMP 'sm_payload.txt') -Encoding UTF8
    Write-Host ('payload saved to ' + (Join-Path $env:TEMP 'sm_payload.txt'))
    exit 1
}
Write-Host ''
Write-Host 'All payload checks passed.' -ForegroundColor Green
$payload | Out-File -FilePath (Join-Path $env:TEMP 'sm_payload.txt') -Encoding UTF8
