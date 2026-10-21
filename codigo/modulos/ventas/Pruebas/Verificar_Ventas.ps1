param([switch]$ConservarBase)
$ErrorActionPreference = 'Stop'
$ventasVswhere = Join-Path ${env:ProgramFiles(x86)} 'Microsoft Visual Studio\Installer\vswhere.exe'
$ventasMsbuild = $env:VENTAS_MSBUILD
if (-not $ventasMsbuild -and (Test-Path -LiteralPath $ventasVswhere)) {
    $ventasMsbuild = & $ventasVswhere -latest -products '*' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1
}
if (-not $ventasMsbuild) { throw 'Defina VENTAS_MSBUILD con la ruta de MSBuild.exe.' }
& $ventasMsbuild (Join-Path $PSScriptRoot 'PruebasVentas.csproj') /m /nologo /v:quiet
if ($LASTEXITCODE -ne 0) { throw 'No se pudieron compilar las pruebas.' }
$ventasConexionOriginal = $env:VENTAS_ODBC
$ventasCadena = if ($ventasConexionOriginal) { $ventasConexionOriginal } else { 'DSN=EmbutidosS.A;Connection Timeout=5' }
$ventasTestBase = 'dbventas_pruebas_' + (Get-Date -Format 'yyyyMMdd_HHmmss_fff')
$ventasDb = New-Object System.Data.Odbc.OdbcConnection($ventasCadena)
$ventasCreada = $false
try {
    $ventasDb.Open(); $ventasCmd = $ventasDb.CreateCommand()
    $ventasCmd.CommandText = 'SELECT DATABASE()'; $ventasOrigen = [string]$ventasCmd.ExecuteScalar()
    if ($ventasOrigen -notmatch '^\w+$') { throw 'Seleccione una base valida que tenga las tablas de Seguridad.' }
    $ventasCmd.CommandText = "CREATE DATABASE $ventasTestBase CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci"
    [void]$ventasCmd.ExecuteNonQuery(); $ventasCreada = $true
    foreach ($ventasTabla in @('tblempleado', 'tblusuario')) {
        $ventasCmd.CommandText = "CREATE TABLE $ventasTestBase.$ventasTabla LIKE $ventasOrigen.$ventasTabla"
        [void]$ventasCmd.ExecuteNonQuery()
    }
    $ventasBuilder = New-Object System.Data.Odbc.OdbcConnectionStringBuilder($ventasCadena)
    $ventasBuilder['DATABASE'] = $ventasTestBase
    $env:VENTAS_ODBC = $ventasBuilder.ConnectionString
    & (Join-Path $PSScriptRoot '..\..\..\..\scripts\modulos\ventas\Instalar_Ventas_Funcional.ps1') -Conexion $env:VENTAS_ODBC
    & (Join-Path $PSScriptRoot 'bin\Debug\PruebasVentas.exe')
    if ($LASTEXITCODE -ne 0) { throw 'Una verificacion de integracion fallo.' }
    Write-Output "Verificacion terminada en base aislada: $ventasTestBase"
}
finally {
    $env:VENTAS_ODBC = $ventasConexionOriginal
    if ($ventasCreada -and -not $ConservarBase -and $ventasTestBase -match '^dbventas_pruebas_\d{8}_\d{6}_\d{3}$') {
        $ventasCmd.CommandText = "DROP DATABASE $ventasTestBase"
        [void]$ventasCmd.ExecuteNonQuery()
    }
    $ventasDb.Dispose()
}
