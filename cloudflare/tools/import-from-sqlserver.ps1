<#
.SYNOPSIS
    Copies data from the shop's SQL Server database into the Bharath SuperMarket
    D1 database through the Worker's /api/sync endpoints.

.DESCRIPTION
    This script is READ-ONLY on SQL Server. It never writes, never drops, never
    updates. The existing SQL Server database remains untouched and usable, which
    is what makes the rollback plan possible: if the cloud side is wrong, stop
    using it and keep billing on SQL Server.

    What it does:
      1. asks the API for each table's column list (so it can never send a column
         D1 does not have)
      2. reads only those columns from SQL Server
      3. POSTs them to /api/sync/import in chunks
      4. seeds the bill-number sequences from the highest legacy bill numbers
      5. compares row counts and money totals between SQL Server and D1

.EXAMPLE
    .\import-from-sqlserver.ps1 -ApiBase https://bharath-supermarket-api.example.workers.dev

.NOTES
    Requires:
      * the Worker deployed and migrated (see docs/04-deployment.md)
      * an admin API user (create one with the bootstrap SQL in that doc)
      * Windows PowerShell with System.Data.SqlClient (built into .NET Framework)
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $true)]
    [string] $ApiBase,

    [string] $SqlServer = '.\SQLEXPRESS',
    [string] $Database  = 'SuperMarketDB',

    [string] $ApiUser = 'admin',
    [System.Security.SecureString] $ApiPassword,

    # Rows per HTTP request. 500 keeps the request well under Worker limits.
    [int] $ChunkSize = 500,

    # Report what would be sent without writing anything.
    [switch] $DryRun
)

$ErrorActionPreference = 'Stop'
[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12

# Masters first, then transactions. /api/sync/import rejects anything not in this
# list, so the two must be kept in step.
$Tables = @(
    'tbl_Companyprofile',
    'tbl_AreaDivision',
    'tbl_Area',
    'tbl_Category',
    'tbl_Brand',
    'tbl_UnitList',
    'tbl_GSTTariff',
    'tbl_Denomination',
    'tbl_DenoDtl',
    'tbl_BankList',
    'tbl_Settings',
    'tbl_GeneralSettings',
    'tbl_RptMaster',
    'tbl_DashBoard',
    'tbl_UserList',
    'tbl_UserRoles',
    'tbl_UserRoleMenu',
    'tbl_MenuMaster',
    'tbl_SmUserRights',
    'tbl_UserPermission',
    'tbl_CusProfile',
    'tbl_StockDtl',
    'tbl_MultiStock',
    'tbl_BillSeries',
    'tbl_SalDetail',
    'tbl_SalParticular',
    'tbl_PurchDetail',
    'tbl_PurchParticular',
    'tbl_StockModify',
    'tbl_ActivityLog'
)

function Write-Step($text) {
    Write-Host ''
    Write-Host "== $text" -ForegroundColor Cyan
}

function Get-PlainValue($value) {
    if ($null -eq $value -or $value -is [System.DBNull]) { return $null }

    switch ($value) {
        { $_ -is [datetime] } { return $_.ToString('yyyy-MM-ddTHH:mm:ss') }
        { $_ -is [bool] }     { return $(if ($_) { 1 } else { 0 }) }
        { $_ -is [decimal] }  { return [double]$_ }
        default               { return $value }
    }
}

# ---------------------------------------------------------------------------
# Connect
# ---------------------------------------------------------------------------

Write-Step "Connecting to SQL Server $SqlServer / $Database"
$connectionString = "Server=$SqlServer;Database=$Database;Integrated Security=True;TrustServerCertificate=True"
$sql = New-Object System.Data.SqlClient.SqlConnection $connectionString
$sql.Open()
Write-Host "connected" -ForegroundColor Green

# ---------------------------------------------------------------------------
# Authenticate against the API
# ---------------------------------------------------------------------------

if (-not $ApiPassword) {
    $ApiPassword = Read-Host -AsSecureString "API password for '$ApiUser'"
}

$plain = [Runtime.InteropServices.Marshal]::PtrToStringAuto(
    [Runtime.InteropServices.Marshal]::SecureStringToBSTR($ApiPassword))

Write-Step 'Signing in to the API'

$loginBody = @{ username = $ApiUser; password = $plain } | ConvertTo-Json
$login = Invoke-RestMethod -Method Post -Uri "$ApiBase/api/auth/login" `
    -ContentType 'application/json' -Body $loginBody

$headers = @{ Authorization = "Bearer $($login.token)" }
Write-Host "signed in as $($login.user.username) ($($login.user.role))" -ForegroundColor Green

# ---------------------------------------------------------------------------
# Import
# ---------------------------------------------------------------------------

$report = @()

foreach ($table in $Tables) {

    # Ask D1 which columns it has; only those are sent.
    $shape = Invoke-RestMethod -Method Get -Uri "$ApiBase/api/sync/export?table=$table&limit=1" -Headers $headers
    $columns = @($shape.columns)

    if ($columns.Count -eq 0) {
        Write-Host "  $table : no columns reported, skipped" -ForegroundColor Yellow
        continue
    }

    $columnList = ($columns | ForEach-Object { "[$_]" }) -join ', '
    $command = $sql.CreateCommand()
    $command.CommandText = "SELECT $columnList FROM [$table]"
    $command.CommandTimeout = 300

    $reader = $command.ExecuteReader()
    $rows = New-Object System.Collections.ArrayList

    while ($reader.Read()) {
        $row = [ordered]@{}
        for ($i = 0; $i -lt $reader.FieldCount; $i++) {
            $row[$reader.GetName($i)] = Get-PlainValue $reader.GetValue($i)
        }
        [void]$rows.Add([pscustomobject]$row)
    }

    $reader.Close()
    $command.Dispose()

    $sqlCount = $rows.Count

    if ($DryRun) {
        Write-Host ("  {0,-22} {1,7} rows (dry run)" -f $table, $sqlCount)
        $report += [pscustomobject]@{ Table = $table; SqlServer = $sqlCount; D1 = $null; Sent = 0 }
        continue
    }

    $sent = 0
    for ($offset = 0; $offset -lt $rows.Count; $offset += $ChunkSize) {
        $end = [Math]::Min($offset + $ChunkSize, $rows.Count) - 1
        $chunk = @($rows[$offset..$end])

        $payload = @{
            table = $table
            mode  = 'replace'
            rows  = $chunk
        } | ConvertTo-Json -Depth 6 -Compress

        try {
            $result = Invoke-RestMethod -Method Post -Uri "$ApiBase/api/sync/import" `
                -Headers $headers -ContentType 'application/json' -Body $payload

            $sent += [int]$result.written
        }
        catch {
            $detail = $_.ErrorDetails.Message
            throw "Import failed for $table at row $offset. $detail"
        }
    }

    Write-Host ("  {0,-22} {1,7} rows sent" -f $table, $sent) -ForegroundColor Green
    $report += [pscustomobject]@{ Table = $table; SqlServer = $sqlCount; D1 = $null; Sent = $sent }
}

$sql.Close()

if ($DryRun) {
    Write-Step 'Dry run complete, nothing was written'
    $report | Format-Table -AutoSize
    return
}

# ---------------------------------------------------------------------------
# Bill sequences
# ---------------------------------------------------------------------------

Write-Step 'Seeding bill-number sequences from the legacy maximum'
$sequences = Invoke-RestMethod -Method Post -Uri "$ApiBase/api/sync/sequences" -Headers $headers
Write-Host "  $($sequences.seeded) sequence(s) seeded" -ForegroundColor Green

# ---------------------------------------------------------------------------
# Reconcile
# ---------------------------------------------------------------------------

Write-Step 'Reconciling SQL Server against D1'

$reconcile = Invoke-RestMethod -Method Get -Uri "$ApiBase/api/sync/reconcile" -Headers $headers
$failures = @()

foreach ($entry in $report) {
    $d1Count = [int]$reconcile.counts.($entry.Table)
    $entry.D1 = $d1Count

    if ($d1Count -ne $entry.SqlServer) {
        $failures += "$($entry.Table): SQL Server $($entry.SqlServer) rows, D1 $d1Count rows"
    }
}

$report | Format-Table -AutoSize | Out-String | Write-Host

# Money totals are compared independently of row counts: a truncated import can
# still have the right number of rows if a chunk was replaced by an older one.
$sql.Open()
$sqlMoney = (New-Object System.Data.SqlClient.SqlCommand(
    "SELECT ISNULL(SUM(Sa_FinalTotal_n),0) FROM tbl_SalDetail", $sql)).ExecuteScalar()
$sql.Close()

$d1Money = [double]$reconcile.salesTotals.finalTotal
$sqlMoney = [double]$sqlMoney

if ([Math]::Abs($sqlMoney - $d1Money) -gt 0.01) {
    $failures += "Sales total: SQL Server $sqlMoney, D1 $d1Money"
}

Write-Step 'Result'

if ($failures.Count -eq 0) {
    Write-Host "RECONCILED: row counts and sales total match" -ForegroundColor Green
    Write-Host "  sales total: $d1Money"
    Write-Host ''
    Write-Host 'Next: see docs/06-migration-checklist.md before switching any user over.'
}
else {
    Write-Host "MISMATCH - do not cut over yet:" -ForegroundColor Red
    $failures | ForEach-Object { Write-Host "  - $_" -ForegroundColor Red }
    exit 1
}
