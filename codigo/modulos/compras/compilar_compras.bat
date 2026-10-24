@echo off
setlocal DisableDelayedExpansion
rem La expansion retardada queda desactivada para conservar rutas con !.
set "CC_BASE=%~dp0"
set "CC_SELF=%~f0"
set "CC_CONFIG=%~1"
set "CC_MORE=%2"
pushd "%~dp0"
if errorlevel 1 exit /b 1
rem PowerShell forma parte de Windows; el archivo contiene su propio coordinador.
"%SystemRoot%\System32\WindowsPowerShell\v1.0\powershell.exe" -NoLogo -NoProfile -ExecutionPolicy Bypass -Command "$s=[IO.File]::ReadAllText($env:CC_SELF,[Text.Encoding]::UTF8);$p=[regex]::Split($s,'(?m)^#--COMPRAS-POWERSHELL--\r?$');& ([scriptblock]::Create($p[1]))"
set "CC_EXIT=%errorlevel%"
popd
endlocal & exit /b %CC_EXIT%

#--COMPRAS-POWERSHELL--
$ErrorActionPreference = 'Stop'
$base = [IO.Path]::GetFullPath($env:CC_BASE)
$steps = New-Object 'System.Collections.Generic.List[object]'
$projects = @{}
$temporaryTargets = New-Object 'System.Collections.Generic.List[string]'
$result = 1
$current = $null
$shortDrive = $null

function FullPath($folder, $relative) {
    [IO.Path]::GetFullPath([IO.Path]::Combine($folder, $relative))
}

# No se siguen enlaces ni se buscan soluciones en salidas, paquetes o respaldos.
function Find-Files($root, [string[]]$names) {
    $queue = New-Object 'System.Collections.Generic.Queue[string]'
    $queue.Enqueue($root)
    while ($queue.Count) {
        $dir = $queue.Dequeue()
        foreach ($entry in Get-ChildItem -LiteralPath $dir -Force) {
            if ($entry.Attributes -band [IO.FileAttributes]::ReparsePoint) { continue }
            if ($entry.PSIsContainer) {
                if ($entry.Name -match '^(\.git|\.vs|\.svn|\.hg|bin|obj|logs|packages|node_modules|TestResults|temp|tmp)$' -or
                    $entry.Name -match '(?i)(^|[ ._-])(backups?|respaldos?|archives?|copias?)([ ._-]|$)') { continue }
                $queue.Enqueue($entry.FullName)
            } elseif ($names -contains $entry.Name) { $entry.FullName }
        }
    }
}

function Unique-Path($name, [object[]]$paths, [bool]$optional = $false) {
    $matches = @($paths | Where-Object { [IO.Path]::GetFileName($_) -ieq $name })
    if (!$matches.Count -and $optional) { return $null }
    if (!$matches.Count) { throw "No se encontro la dependencia obligatoria: $name" }
    if ($matches.Count -gt 1) { throw "Varias coincidencias para ${name}:`n$($matches -join "`n")" }
    $matches[0]
}

function Add-Step($path) {
    $step = [pscustomobject]@{ Path=$path; Name=[IO.Path]::GetFileName($path); Platform=$null;
        Projects=@(); Ordered=@(); State='No ejecutada'; Code=$null; Packages=$false }
    $steps.Add($step)
    $step
}

function Invoke-Query($path, $configuration, $platform) {
    # /getItem:Reference calcula FullPath incluso para nombres de ensamblado.
    # Consultar copias con identidad corta evita MAX_PATH sin cambiar los proyectos.
    if (!$script:queryTargets) {
        $script:queryTargets = Join-Path ([IO.Path]::GetTempPath()) ('Compras_Query_' + [guid]::NewGuid().ToString('N') + '.targets')
        $temporaryTargets.Add($script:queryTargets)
        $queryXml = @'
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Target Name="ConsultarCompras">
    <ItemGroup>
      <_ComprasReference Include="Reference" Condition="'@(Reference)' != ''">
        <OriginalIdentity>%(Reference.Identity)</OriginalIdentity>
        <HintPath>%(Reference.HintPath)</HintPath>
      </_ComprasReference>
    </ItemGroup>
  </Target>
</Project>
'@
        [IO.File]::WriteAllText($script:queryTargets,$queryXml,[Text.UTF8Encoding]::new($false))
    }
    # Windows PowerShell convierte stderr en errores: capturarlo completo antes de fallar.
    $previousPreference = $ErrorActionPreference
    try {
        $ErrorActionPreference = 'Continue'
        $output = & $msbuild $path /nologo "/p:Configuration=$configuration" "/p:Platform=$platform" `
            /t:ConsultarCompras "/p:CustomAfterMicrosoftCommonTargets=$script:queryTargets" `
            /getProperty:TargetPath,PlatformTarget,TargetFrameworkVersion,OutputType,Prefer32Bit `
            /getItem:_ComprasReference,ProjectReference,PackageReference 2>&1
        $code = $LASTEXITCODE
    } finally {
        $ErrorActionPreference = $previousPreference
    }
    if ($code -ne 0) { throw "No se pudo evaluar $path (codigo $code):`n$($output -join "`n")" }
    $data = ($output -join "`n") | ConvertFrom-Json
    $references = @($data.Items._ComprasReference | ForEach-Object {
        [pscustomobject]@{ Identity=$_.OriginalIdentity; HintPath=$_.HintPath }
    })
    $data.Items | Add-Member -NotePropertyName Reference -NotePropertyValue $references
    $data
}

function Add-Project($path, $configuration, $platform, $step) {
    $path = [IO.Path]::GetFullPath($path)
    if ($projects.ContainsKey($path)) {
        # Seguridad comparte modelo/controlador con el paso previo que necesita Navegador.
        # Reutilizar su productor; la solucion completa compila los proyectos restantes.
        if ($projects[$path].Configuration -ne $configuration -or $projects[$path].Platform -ne $platform) {
            throw "Proyecto incluido con configuraciones incompatibles: $path"
        }
        return $projects[$path]
    }
    if (!(Test-Path -LiteralPath $path -PathType Leaf)) { throw "Falta el proyecto: $path" }
    $data = Invoke-Query $path $configuration $platform
    if ($data.Properties.TargetFrameworkVersion -ne 'v4.8') { throw "Framework no previsto en $path" }
    $project = [pscustomobject]@{ Path=$path; Configuration=$configuration; Platform=$platform; Step=$step;
        Data=$data; Output=$data.Properties.TargetPath; Dependencies=@(); Visiting=$false; Visited=$false }
    $projects[$path] = $project
    $step.Projects += $project
    $packages = Join-Path (Split-Path $path) 'packages.config'
    if ((Test-Path -LiteralPath $packages) -or @($data.Items.PackageReference).Count) { $step.Packages=$true }
    $project
}

function Order-Project($project, $step) {
    if ($project.Visiting) { throw "Dependencia circular: $($project.Path)" }
    if ($project.Visited) { return }
    $project.Visiting = $true
    foreach ($dependency in $project.Dependencies) {
        if ($dependency.Step -eq $step) { Order-Project $dependency $step }
    }
    $project.Visiting = $false
    $project.Visited = $true
    $step.Ordered += $project
}

function XmlEscape([string]$text) { [Security.SecurityElement]::Escape($text) }
function MsbuildEscape([string]$text) {
    foreach ($char in @('%','$','@',';','?','*',"'",'(',')')) {
        $text = $text.Replace($char,('%{0:X2}' -f [int][char]$char))
    }
    $text
}

try {
    # NuGet packages.config todavia puede fallar con MAX_PATH. SUBST solo crea
    # un alias temporal: no mueve archivos ni cambia la configuracion del equipo.
    # Incluir tambien ayuda y los demas recursos hermanos de codigo.
    $repositoryRoot = FullPath $base '..\..\..'
    $subst = Join-Path $env:SystemRoot 'System32\subst.exe'
    foreach ($letter in 90..68) {
        $drive = ([char]$letter).ToString() + ':'
        if (Test-Path -LiteralPath ($drive + '\')) { continue }
        & $subst $drive $repositoryRoot 2>$null
        if ($LASTEXITCODE -eq 0) { $shortDrive=$drive; break }
    }
    if (!$shortDrive) { throw 'No se pudo crear una unidad temporal libre (D: a Z:) para evitar rutas largas de NuGet.' }
    $base = FullPath ($shortDrive + '\') 'codigo\modulos\compras'
    Write-Host "Ruta temporal de compilacion: $base (los archivos permanecen en su ubicacion original)."
    $configuration = $env:CC_CONFIG
    if (!$configuration) { $configuration='Debug' }
    if ($env:CC_MORE -or $configuration -notin @('Debug','Release')) {
        throw 'Uso: compilar_compras.bat [Debug|Release]. Se permite un solo argumento.'
    }
    if ($configuration -ieq 'Debug') { $configuration='Debug' } else { $configuration='Release' }

    # Primero se localizan TODAS las soluciones del modulo.
    $moduleNames = @('Componentes','MovimientosProveedores','Polizas','OrdenesCompra','PagosProveedores','AprobacionFinanciera','CierreMovimientos','Compras')
    foreach ($name in $moduleNames) { $null = Add-Step "$name.sln" }
    $expected = @($moduleNames | ForEach-Object { "$_.sln" })
    $execution = @('Compras') + @($moduleNames | Where-Object { $_ -notin @('Compras','Componentes') })
    $expected += @($execution | ForEach-Object { "Ejecucion_$_.sln" })
    $found = @(Find-Files $base $expected)
    $local = @{}
    foreach ($name in $moduleNames) { $local[$name] = Unique-Path "$name.sln" $found }
    $executables = @{}
    foreach ($name in $execution) { $executables[$name] = Unique-Path "Ejecucion_$name.sln" $found $true }

    # Busca los productores externos desde los padres, hasta la raiz del repositorio.
    $externalNames = @('Consultas.sln','Navegador.sln','CapaControlador_Seguridad.csproj')
    $ancestor = (Get-Item -LiteralPath $base).Parent
    $external = @()
    while ($ancestor) {
        $external = @(Find-Files $ancestor.FullName $externalNames | Where-Object { $_ -match '[\\/]componentes[\\/]' })
        if (@($external | ForEach-Object { [IO.Path]::GetFileName($_) } | Sort-Object -Unique).Count -eq 3) { break }
        if (Test-Path -LiteralPath (Join-Path $ancestor.FullName '.git')) { break }
        $ancestor = $ancestor.Parent
    }
    $consultas = Unique-Path 'Consultas.sln' $external
    $seguridad = Unique-Path 'CapaControlador_Seguridad.csproj' $external
    $seguridadSolution = Join-Path (Split-Path (Split-Path $seguridad)) 'Seguridad.sln'
    if (!(Test-Path -LiteralPath $seguridadSolution -PathType Leaf)) { throw "Falta la solucion: $seguridadSolution" }
    $navegador = Unique-Path 'Navegador.sln' $external
    # Reportes esta dos niveles arriba; en este repositorio se llama reporteador.
    $reportsRoot = FullPath $base '..\..\componentes\reportes'
    if (!(Test-Path -LiteralPath $reportsRoot -PathType Container)) {
        $reportsRoot = FullPath $base '..\..\componentes\reporteador'
    }
    if (!(Test-Path -LiteralPath $reportsRoot -PathType Container)) {
        throw 'No se encontro componentes\reportes ni componentes\reporteador dos niveles arriba de Compras.'
    }
    # Actualizar debe preceder a Guardar; todos los botones preceden a Reporteador.
    $reportNames = @('BtnActualizar_Reporteador','BtnBusqueda_Reporteador','BtnEditar_Reporteador',
        'BtnEliminar_Reporteador','BtnGuardar_Reporteador','BtnLimpiar_Reporteador',
        'BtnRuta_Reporteador','BtnVerReporte_Reporteador','Reporteador','Ejecucion_Reporteador')
    $reportFiles = @(Find-Files $reportsRoot @($reportNames | ForEach-Object { "$_.sln" }))
    $reportSolutions = @($reportNames | ForEach-Object { Unique-Path "$_.sln" $reportFiles })
    $steps.Clear()
    $null = Add-Step $consultas
    # La vista de Seguridad depende de Compras: aqui solo se necesita modelo/controlador.
    $null = Add-Step $seguridad
    foreach ($solution in $reportSolutions) { $null = Add-Step $solution }
    $null = Add-Step $navegador
    foreach ($name in $moduleNames) {
        $null = Add-Step $local[$name]
        if ($executables.ContainsKey($name) -and $executables[$name]) { $null = Add-Step $executables[$name] }
    }
    $null = Add-Step $seguridadSolution

    # VS 2022, incluidas Build Tools; PATH es un segundo mecanismo, no dotnet build.
    $candidates = @()
    $whereTools = @(@(${env:ProgramFiles(x86)},$env:ProgramFiles) | Where-Object { $_ } | ForEach-Object {
        Join-Path $_ 'Microsoft Visual Studio\Installer\vswhere.exe'
    })
    $whereTools += @(Get-Command vswhere.exe -ErrorAction SilentlyContinue | ForEach-Object { $_.Source })
    foreach ($where in $whereTools | Select-Object -Unique) {
        if (Test-Path -LiteralPath $where) {
            $candidates += @(& $where -latest -products '*' -version '[17.0,18.0)' -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe')
        }
    }
    $candidates += @(Get-Command MSBuild.exe -CommandType Application -All -ErrorAction SilentlyContinue | ForEach-Object { $_.Source })
    $msbuild = $null
    foreach ($candidate in $candidates | Select-Object -Unique) {
        if (!(Test-Path -LiteralPath $candidate -PathType Leaf)) { continue }
        $versionOutput = & $candidate /nologo /version 2>&1
        $toolCode = $LASTEXITCODE
        $versionMatch = [regex]::Match(($versionOutput -join "`n"),'(?m)^17\.\d+(?:\.\d+){1,2}\s*$')
        # Las consultas sin compilar /getProperty y /getItem requieren 17.8 o posterior.
        if ($toolCode -eq 0 -and $versionMatch.Success -and [version]$versionMatch.Value.Trim() -ge [version]'17.8') {
            $msbuild=$candidate; break
        }
    }
    if (!$msbuild) { throw 'Se requiere MSBuild.exe de Visual Studio 2022/Build Tools 17.8 o posterior, con herramientas .NET Framework 4.8.' }
    $targetPacks = @(@(${env:ProgramFiles(x86)},$env:ProgramFiles) | Where-Object { $_ } | ForEach-Object {
        Join-Path $_ 'Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\mscorlib.dll'
    } | Where-Object { Test-Path -LiteralPath $_ -PathType Leaf })
    if (!$targetPacks.Count) { throw 'Falta el Developer/Targeting Pack de .NET Framework 4.8.' }
    Write-Host "MSBuild: $msbuild"
    Write-Host "Configuracion: $configuration. Validando proyectos y dependencias antes de compilar..."

    foreach ($step in $steps) {
        $current = $step
        Write-Host "Validando: $($step.Name)"
        if ([IO.Path]::GetExtension($step.Path) -eq '.csproj') {
            [xml]$xml = Get-Content -LiteralPath $step.Path -Raw
            $platforms = @($xml.SelectNodes('//*[local-name()="PropertyGroup"]/@Condition') | ForEach-Object {
                if ($_.Value -match "==\s*'$configuration\|([^']+)'") { $Matches[1] }
            })
            $step.Platform = @('x64','AnyCPU','x86' | Where-Object { $_ -in $platforms } | Select-Object -First 1)[0]
            if (!$step.Platform) { throw "Configuracion no admitida: $($step.Path)" }
            $null = Add-Project $step.Path $configuration $step.Platform $step
            # Este productor tiene un ProjectReference real al modelo.
            for ($i=0; $i -lt $step.Projects.Count; $i++) {
                foreach ($reference in @($step.Projects[$i].Data.Items.ProjectReference)) {
                    $null = Add-Project (FullPath (Split-Path $step.Projects[$i].Path) $reference.Identity) $configuration $step.Platform $step
                }
            }
        } else {
            $sln = [IO.File]::ReadAllText($step.Path)
            $section = [regex]::Match($sln,'(?s)GlobalSection\(SolutionConfigurationPlatforms\).*?EndGlobalSection').Value
            $platforms = @([regex]::Matches($section,"(?m)^\s*$configuration\|([^=\r\n]+)\s*=") | ForEach-Object { $_.Groups[1].Value.Trim() })
            $step.Platform = @('x64','Any CPU','x86' | Where-Object { $_ -in $platforms } | Select-Object -First 1)[0]
            if (!$step.Platform) { throw "No hay plataforma compatible con $configuration en $($step.Path)" }
            foreach ($match in [regex]::Matches($sln,'(?m)^Project\("[^"\r\n]+"\) = "[^"]+", "([^"]+\.csproj)", "(\{[^}]+\})"')) {
                $path = FullPath (Split-Path $step.Path) $match.Groups[1].Value
                $key = [regex]::Escape($match.Groups[2].Value + ".$configuration|" + $step.Platform)
                if ($sln -notmatch "(?m)^\s*$key\.Build\.0\s*=") { throw "Proyecto excluido de compilacion: $path" }
                $mapping = [regex]::Match($sln,"(?m)^\s*$key\.ActiveCfg\s*=\s*([^|\r\n]+)\|([^\r\n]+)")
                if (!$mapping.Success) { throw "Falta el mapeo de plataforma: $path" }
                $null = Add-Project $path $mapping.Groups[1].Value.Trim() $mapping.Groups[2].Value.Trim().Replace('Any CPU','AnyCPU') $step
            }
            if (!$step.Projects.Count) { throw "Solucion sin proyectos C# admitidos: $($step.Path)" }
        }
    }

    $outputs = @{}
    foreach ($project in $projects.Values) {
        if (!$project.Output) { throw "Salida no definida: $($project.Path)" }
        if ($outputs.ContainsKey($project.Output)) { throw "Dos productores para $($project.Output)" }
        $outputs[$project.Output] = $project
    }
    foreach ($project in $projects.Values) {
        $current=$project.Step
        foreach ($reference in @($project.Data.Items.Reference)) {
            if (!$reference.PSObject.Properties['HintPath'] -or !$reference.HintPath) { continue }
            $hint = FullPath (Split-Path $project.Path) $reference.HintPath
            if ($outputs.ContainsKey($hint)) { $project.Dependencies += $outputs[$hint] }
            elseif ($hint -notmatch '[\\/]packages[\\/]') { throw "Referencia sin productor para $configuration en $($project.Path): $hint" }
        }
        foreach ($reference in @($project.Data.Items.ProjectReference)) {
            $path = FullPath (Split-Path $project.Path) $reference.Identity
            if (!$projects.ContainsKey($path)) { throw "ProjectReference fuera del plan: $path" }
            $project.Dependencies += $projects[$path]
        }
        foreach ($dependency in $project.Dependencies) {
            if ($steps.IndexOf($dependency.Step) -gt $steps.IndexOf($project.Step)) { throw "Orden incompatible: $($project.Path) requiere $($dependency.Path)" }
            if ($project.Data.Properties.PlatformTarget -eq 'x86' -and $dependency.Data.Properties.PlatformTarget -eq 'x64') {
                throw "Arquitecturas incompatibles: $($project.Path) (x86) -> $($dependency.Path) (x64)"
            }
        }
    }
    foreach ($step in $steps) { foreach ($project in $step.Projects) { Order-Project $project $step } }
    $current=$null
    $steps | Select-Object Name,Path,Platform | Format-Table -AutoSize | Out-String | Write-Host

    for ($index=0; $index -lt $steps.Count; $index++) {
        $step=$steps[$index]; $current=$step
        Write-Host "[$($index+1)/$($steps.Count)] $($step.Name) - $configuration | $($step.Platform)"
        $arguments = @($step.Path,'/t:Build','/nologo','/verbosity:minimal',"/p:Configuration=$configuration", "/p:Platform=$($step.Platform)",'/warnAsError:MSB3245')
        if ([IO.Path]::GetExtension($step.Path) -eq '.sln') {
            # Orden interno por DLL/ProjectReference, sin alterar los .sln originales.
            # Un target previo compila el grafo una vez y vacia la lista del Build normal.
            # DirectorySolutionTargetsPath es un punto de extension de MSBuild.
            $targets = Join-Path ([IO.Path]::GetTempPath()) ('Compras_' + [guid]::NewGuid().ToString('N') + '.targets')
            $temporaryTargets.Add($targets)
            $body = '<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003"><Target Name="CompilarComprasEnOrden" BeforeTargets="Build">'
            foreach ($project in $step.Ordered) {
                $properties = "Configuration=$($project.Configuration);Platform=$($project.Platform);BuildProjectReferences=false"
                # El ejecutable AnyCPU debe poder cargar las bibliotecas x64 de Compras.
                if ($project.Data.Properties.OutputType -in @('Exe','WinExe') -and $project.Data.Properties.PlatformTarget -eq 'AnyCPU') { $properties += ';Prefer32Bit=false' }
                $body += '<MSBuild Projects="'+(XmlEscape (MsbuildEscape $project.Path))+'" Targets="Build" Properties="'+(XmlEscape $properties)+'" BuildInParallel="false" StopOnFirstFailure="true" />'
            }
            $body += '<ItemGroup><ProjectReference Remove="@(ProjectReference)" /></ItemGroup></Target></Project>'
            [IO.File]::WriteAllText($targets,$body,[Text.UTF8Encoding]::new($false))
            $arguments += "/p:DirectorySolutionTargetsPath=$targets"
        }
        if ($step.Packages) {
            $arguments += @('/restore','/p:RestorePackagesConfig=true')
            $packageRoot = Join-Path (Split-Path $step.Path) 'packages'
            if ([IO.Path]::GetExtension($step.Path) -eq '.csproj') { $packageRoot=Join-Path (Split-Path (Split-Path $step.Path)) 'packages' }
            $arguments += "/p:RestoreRepositoryPath=$packageRoot"
        }
        & $msbuild @arguments
        $code = $LASTEXITCODE
        $step.Code=$code
        if ($code -ne 0) { $step.State='Fallida'; throw "Fallo $($step.Name), codigo $code." }
        foreach ($project in $step.Projects) {
            if (!(Test-Path -LiteralPath $project.Output -PathType Leaf)) { throw "MSBuild termino sin la salida esperada: $($project.Output)" }
        }
        $step.State='Correcta'
        Write-Host "[$($index+1)/$($steps.Count)] $($step.Name): CORRECTA"
    }
    $result=0
} catch {
    if ($current -and $current.State -ne 'Correcta') { $current.State='Fallida' }
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
} finally {
    $summary = @(
        ('Correctas: {0}' -f @($steps | Where-Object State -eq 'Correcta').Count)
        ('Fallidas: {0}' -f @($steps | Where-Object State -eq 'Fallida').Count)
        ('No ejecutadas: {0}' -f @($steps | Where-Object State -eq 'No ejecutada').Count)
    )
    $summary += @($steps | ForEach-Object { '{0}: {1}' -f $_.State,$_.Name })
    $summary | ForEach-Object { Write-Host $_ }
    foreach ($targets in $temporaryTargets) {
        try { [IO.File]::Delete($targets) } catch { Write-Warning "No se pudo eliminar el archivo temporal: $targets" }
    }
    if ($shortDrive) {
        & $subst $shortDrive /D
        if ($LASTEXITCODE -ne 0) { Write-Warning "No se pudo quitar el alias temporal $shortDrive" }
    }
}
exit $result
