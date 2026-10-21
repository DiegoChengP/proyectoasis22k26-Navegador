param(
    [string]$Conexion = $env:VENTAS_ODBC
)
$ErrorActionPreference = 'Stop'
if ([string]::IsNullOrWhiteSpace($Conexion)) { $Conexion = 'DSN=EmbutidosS.A;Connection Timeout=5' }
$ventasArchivo = Join-Path $PSScriptRoot 'Actualizar_Ventas_Funcional_20261020.sql'
$ventasDb = New-Object System.Data.Odbc.OdbcConnection($Conexion)
try {
    $ventasDb.Open()
    $ventasComando = $ventasDb.CreateCommand()
    $ventasComando.CommandTimeout = 30
    $ventasComando.CommandText = 'SELECT DATABASE()'
    $ventasBase = $ventasComando.ExecuteScalar()
    if ([string]::IsNullOrWhiteSpace($ventasBase)) { throw 'La conexion debe seleccionar una base de datos.' }
    Write-Output "Actualizando Ventas en: $ventasBase"
    $ventasDelimitador = ';'
    $ventasSentencia = ''
    $ventasAplicadas = 0
    foreach ($ventasLinea in [IO.File]::ReadAllLines($ventasArchivo, [Text.Encoding]::UTF8)) {
        if ($ventasLinea.TrimStart().StartsWith('--') -or [string]::IsNullOrWhiteSpace($ventasLinea)) { continue }
        if ($ventasLinea -match '^DELIMITER\s+(.+)$') { $ventasDelimitador = $Matches[1].Trim(); continue }
        $ventasSentencia += $ventasLinea + "`n"
        if (-not $ventasLinea.TrimEnd().EndsWith($ventasDelimitador)) { continue }
        $ventasSql = $ventasSentencia.TrimEnd()
        $ventasSql = $ventasSql.Substring(0, $ventasSql.Length - $ventasDelimitador.Length)
        $ventasSentencia = ''
        if ($ventasSql -match '^CREATE TRIGGER\s+(\w+)') {
            $ventasTrigger = $Matches[1]
            $ventasComando.Parameters.Clear()
            $ventasComando.CommandText = 'SELECT COUNT(*) FROM information_schema.TRIGGERS WHERE TRIGGER_SCHEMA=DATABASE() AND TRIGGER_NAME=?'
            [void]$ventasComando.Parameters.AddWithValue('?', $ventasTrigger)
            if ([int]$ventasComando.ExecuteScalar() -gt 0) { continue }
        }
        $ventasComando.Parameters.Clear()
        $ventasComando.CommandText = $ventasSql
        [void]$ventasComando.ExecuteNonQuery()
        $ventasAplicadas++
    }
    if (-not [string]::IsNullOrWhiteSpace($ventasSentencia)) { throw 'La ultima sentencia SQL esta incompleta.' }
    Write-Output "Actualizacion completada. Sentencias aplicadas: $ventasAplicadas. Los datos existentes se conservaron."
}
finally { $ventasDb.Dispose() }
