@echo off
:: ============================================================================
:: BUILD GLOBAL - CONSULTAS, NAVEGADOR, SEGURIDAD Y RRHH
:: VERSION 2 PORTABLE: FUNCIONA EN CUALQUIER COMPUTADORA, SIN EDITAR RUTAS
:: ============================================================================
::
:: QUE HACE ESTE SCRIPT:
:: 1. Busca el repositorio donde esta guardado este .bat (no usa rutas fijas).
:: 2. Busca MSBuild de Visual Studio automaticamente (cualquier edicion).
:: 3. Compila en este orden: consultas, navegador, seguridad y RRHH
::    (codigo\modulos\rrhh, si existe en el repositorio; si no, lo omite).
:: 4. Dentro de cada componente compila PROYECTO POR PROYECTO en orden de capas:
::        CapaModelo -> CapaControlador -> librerias (Control_*) -> CapaVista -> ejecutables.
::    Esto es necesario porque cada capa referencia la DLL de la capa anterior
::    (referencia a archivo), y en un clone nuevo esa DLL todavia no existe.
:: 5. Antes de compilar un componente revisa sus referencias a DLL. Si una DLL
::    no existe y su proyecto esta en el repositorio (por ejemplo CapaVista_Bancos
::    o CapaVista_Rrhh, que usa el MDI de seguridad), compila ese proyecto primero.
:: 6. Si algun proyecto falla, repite todo el ciclo (hasta 3 rondas), porque
::    navegador y seguridad se referencian entre si.
:: 7. Copia las DLL de bin\Debug a bin\Release de cada proyecto compilado.
:: 8. Deja los logs en %TEMP%\build_global (no ensucia el repositorio) y un
::    resumen al final.
::
:: DONDE COLOCARLO:
::   En cualquier carpeta DENTRO del repositorio clonado.
::
:: SI ALGO FALLA, REVISA:
::   %TEMP%\build_global\fallidos.txt                  proyectos que no compilaron
::   %TEMP%\build_global\errores_NombreProyecto.txt    errores de ese proyecto
::   %TEMP%\build_global\build_log.txt                 resultado completo
::
:: IMPORTANTE: guardar con saltos de linea de Windows (CRLF) y sin tildes.
:: ============================================================================

setlocal enableextensions
chcp 65001 >nul
color 0A
title Compilacion global - Consultas, Navegador, Seguridad y RRHH

:: --------------------------------------------------------------------------
:: OPCIONES (normalmente NO hay que tocar nada)
:: --------------------------------------------------------------------------
:: Dejar vacio para detectar MSBuild automaticamente. Solo si la deteccion
:: falla, escribir la ruta completa de MSBuild.exe, por ejemplo:
::   set "MSBUILD_MANUAL=C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
set "MSBUILD_MANUAL="

:: 1 = tambien compilar los ejecutables (Ejecucion_*, Ejecutable_Rrhh) de todos
::     los componentes.
:: 0 = solo las capas (el ejecutable se compila solo en seguridad y rrhh).
set "EJECUTABLES_TODOS=1"

:INICIO
set /a total=0
set /a ok=0
set /a fail=0
set /a P_OK=0
set /a P_FAIL=0

echo ============================================
echo COMPILACION GLOBAL - CONSULTAS, NAVEGADOR, SEGURIDAD, RRHH
echo ============================================

:: --------------------------------------------------------------------------
:: 1) CARPETA DE ESTE .BAT Y CARPETA DE LOGS
:: --------------------------------------------------------------------------
set "ROOT_DIR=%~dp0"
cd /d "%ROOT_DIR%"
set "LOG_DIR=%TEMP%\build_global"
if not exist "%LOG_DIR%" mkdir "%LOG_DIR%"
set "LOG_FILE=%LOG_DIR%\build_log.txt"
set "FALLIDOS=%LOG_DIR%\fallidos.txt"
set "FALL_PASS=%LOG_DIR%\fallidos_pasada.txt"
set "FALT=%LOG_DIR%\referencias_faltantes.txt"
set "FALT_DET=%LOG_DIR%\referencias_faltantes_detalle.txt"
if exist "%FALLIDOS%" del "%FALLIDOS%"
if exist "%FALL_PASS%" del "%FALL_PASS%"
echo Compilacion iniciada: %date% %time% > "%LOG_FILE%"

:: --------------------------------------------------------------------------
:: 2) LOCALIZAR LA CARPETA "componentes" DEL REPOSITORIO
:: --------------------------------------------------------------------------
set "COMPONENTES_DIR="
set "CUR=%~dp0"
if "%CUR:~-1%"=="\" set "CUR=%CUR:~0,-1%"

:BUSCAR_RAIZ
if exist "%CUR%\codigo\componentes\" set "COMPONENTES_DIR=%CUR%\codigo\componentes" & goto RAIZ_LISTA
if exist "%CUR%\componentes\" set "COMPONENTES_DIR=%CUR%\componentes" & goto RAIZ_LISTA
if exist "%CUR%\navegador\" if exist "%CUR%\seguridad\" set "COMPONENTES_DIR=%CUR%" & goto RAIZ_LISTA
for %%P in ("%CUR%\..") do set "PADRE=%%~fP"
if "%PADRE:~-1%"=="\" set "PADRE=%PADRE:~0,-1%"
if /I "%PADRE%"=="%CUR%" goto RAIZ_NO_ENCONTRADA
set "CUR=%PADRE%"
goto BUSCAR_RAIZ

:RAIZ_NO_ENCONTRADA
:: Respaldo: rutas habituales del repositorio (por si el .bat esta fuera de el).
if exist "C:\proyectoasis22k26\codigo\componentes\" set "COMPONENTES_DIR=C:\proyectoasis22k26\codigo\componentes" & goto RAIZ_LISTA
if exist "C:\proyectoasis22k26-Seguridad\codigo\componentes\" set "COMPONENTES_DIR=C:\proyectoasis22k26-Seguridad\codigo\componentes" & goto RAIZ_LISTA
echo.
echo [ERROR] No se encontro la carpeta "componentes" del repositorio.
echo         Guarda este .bat dentro del repositorio clonado y vuelve a ejecutarlo.
echo         Carpeta desde la que se busco: %~dp0
goto FIN_ERROR

:RAIZ_LISTA
for %%i in ("%COMPONENTES_DIR%\..") do set "CODIGO_DIR=%%~fi"
for %%i in ("%CODIGO_DIR%\..") do set "REPO_ROOT=%%~fi"
set "MODULOS_DIR=%CODIGO_DIR%\modulos"
echo Repositorio (componentes): %COMPONENTES_DIR%

:: --------------------------------------------------------------------------
:: 3) LOCALIZAR MSBUILD (CUALQUIER EDICION DE VISUAL STUDIO)
:: --------------------------------------------------------------------------
set "MSBUILD_PATH="
if defined MSBUILD_MANUAL if exist "%MSBUILD_MANUAL%" set "MSBUILD_PATH=%MSBUILD_MANUAL%"
if defined MSBUILD_PATH goto MSBUILD_LISTO

:: 3a) vswhere (herramienta oficial de Microsoft). La salida se guarda en un
:: archivo temporal para evitar problemas de comillas en el bucle FOR.
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if not exist "%VSWHERE%" goto MSBUILD_FALLBACK
set "VSW_OUT=%TEMP%\vswhere_msbuild.txt"

:: Primero Visual Studio 2022 (version 17).
if exist "%VSW_OUT%" del "%VSW_OUT%"
"%VSWHERE%" -version "[17.0,18.0)" -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > "%VSW_OUT%" 2>nul
if exist "%VSW_OUT%" set /p MSBUILD_PATH=<"%VSW_OUT%"
if defined MSBUILD_PATH if not exist "%MSBUILD_PATH%" set "MSBUILD_PATH="
if defined MSBUILD_PATH goto MSBUILD_LISTO

:: Si no hay 2022, la version mas reciente instalada.
if exist "%VSW_OUT%" del "%VSW_OUT%"
"%VSWHERE%" -latest -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe > "%VSW_OUT%" 2>nul
if exist "%VSW_OUT%" set /p MSBUILD_PATH=<"%VSW_OUT%"
if defined MSBUILD_PATH if not exist "%MSBUILD_PATH%" set "MSBUILD_PATH="
if defined MSBUILD_PATH goto MSBUILD_LISTO

:MSBUILD_FALLBACK
:: 3b) Respaldo: rutas habituales de cada edicion.
for %%V in (2022 2019) do for %%E in (Enterprise Professional Community BuildTools) do if not defined MSBUILD_PATH if exist "%ProgramFiles%\Microsoft Visual Studio\%%V\%%E\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_PATH=%ProgramFiles%\Microsoft Visual Studio\%%V\%%E\MSBuild\Current\Bin\MSBuild.exe"
if defined MSBUILD_PATH goto MSBUILD_LISTO

echo.
echo [ERROR] No se encontro MSBuild. Instala Visual Studio 2022 con la carga de
echo         trabajo "Desarrollo de escritorio con .NET", o escribe la ruta de
echo         MSBuild.exe en la variable MSBUILD_MANUAL al inicio de este archivo.
goto FIN_ERROR

:MSBUILD_LISTO
echo MSBuild: %MSBUILD_PATH%
echo MSBuild: %MSBUILD_PATH% >> "%LOG_FILE%"

:: --------------------------------------------------------------------------
:: 4) COMPILAR EN ORDEN: CONSULTAS, NAVEGADOR, SEGURIDAD
:: --------------------------------------------------------------------------
:: El segundo parametro indica si tambien se compilan los proyectos "Ejecucion*"
:: de ese componente (1 = si, 0 = no). En seguridad siempre se compilan.
:: NOTA: navegador y seguridad se necesitan mutuamente (referencias a DLL):
::   CapaModelo_Navegador      -> CapaModelo_Seguridad
::   CapaVista_Navegador       -> CapaControlador_Seguridad
::   CapaVista_Seguridad       -> CapaVista_Navegador
:: Para evitar errores de la primera ronda, antes de empezar se compilan
:: CapaModelo_Seguridad y CapaControlador_Seguridad (no dependen de nadie).
:: Aun asi, si algo falla se repite TODO el ciclo (maximo 3 rondas).
echo.
echo ============================================
echo PRE-COMPILACION: capas base de Seguridad (las usa Navegador)
echo ============================================
set "RES_DIR=*NINGUNO*"
call :BuscarProyecto "CapaModelo_Seguridad"
call :BuscarProyecto "CapaControlador_Seguridad"
set "FALL_PREV=%LOG_DIR%\fallidos_prev.txt"
if exist "%FALL_PREV%" del "%FALL_PREV%"
set "RONDA=1"

:RONDA
set /a total=0
set /a ok=0
set /a fail=0
if exist "%FALLIDOS%" del "%FALLIDOS%"
call :CompilarComponente consultas %EJECUTABLES_TODOS%
call :CompilarComponente navegador %EJECUTABLES_TODOS%
call :CompilarComponente seguridad 1
if exist "%MODULOS_DIR%\rrhh\" call :CompilarComponente rrhh 1 "%MODULOS_DIR%\rrhh"
if not exist "%MODULOS_DIR%\rrhh\" echo [PENDIENTE] No existe %MODULOS_DIR%\rrhh : se omite RRHH.
if %fail% EQU 0 goto RONDA_FIN
if %RONDA% GEQ 3 goto RONDA_FIN
if exist "%FALL_PREV%" fc /b "%FALLIDOS%" "%FALL_PREV%" >nul 2>&1 && goto RONDA_SIN_PROGRESO
copy /y "%FALLIDOS%" "%FALL_PREV%" >nul 2>&1
set /a RONDA+=1
echo.
echo ============================================
echo Hubo errores. Se repite todo el ciclo: RONDA %RONDA% de 3
echo (navegador y seguridad dependen entre si)
echo ============================================
goto RONDA

:RONDA_SIN_PROGRESO
echo.
echo Los mismos proyectos siguen fallando que en la ronda anterior:
echo no se repite mas. Revisa los errores de abajo.

:RONDA_FIN

echo.
echo ============================================
echo COMPILACION GLOBAL FINALIZADA
echo Total de proyectos: %total%
echo Correctos: %ok%
echo Con errores: %fail%
echo Log completo: %LOG_FILE%
echo ============================================
if exist "%FALLIDOS%" echo.
if exist "%FALLIDOS%" echo PROYECTOS QUE NO COMPILARON:
if exist "%FALLIDOS%" type "%FALLIDOS%"
if exist "%FALLIDOS%" echo (errores en %LOG_DIR%\errores_NombreProyecto.txt)

:: --------------------------------------------------------------------------
:: 5) VERIFICAR DLL Y REFERENCIAS PENDIENTES
:: --------------------------------------------------------------------------
echo.
echo ============================================
echo VERIFICANDO DLLs (bin\Debug)
echo ============================================
call :VerificarComponente consultas
call :VerificarComponente navegador
call :VerificarComponente seguridad
if exist "%MODULOS_DIR%\rrhh\" call :VerificarComponente rrhh "%MODULOS_DIR%\rrhh"

echo.
echo ============================================
echo REFERENCIAS A DLL QUE SIGUEN SIN EXISTIR
echo ============================================
call :ListarFaltantes consultas
call :ListarFaltantes navegador
call :ListarFaltantes seguridad
if exist "%MODULOS_DIR%\rrhh\" call :ListarFaltantes rrhh "%MODULOS_DIR%\rrhh"

echo.
echo ============================================
echo COMPILACION FINALIZADA COMPLETAMENTE
echo ============================================
if /I "%~1"=="/auto" goto FIN_OK
echo.
echo Presiona una opcion:
echo [R] Recompilar
echo [S] Salir
choice /c RS /n /m "Seleccion: "
if errorlevel 2 goto FIN_OK
if errorlevel 1 goto INICIO

:FIN_OK
endlocal
exit /b 0

:FIN_ERROR
echo.
pause
endlocal
exit /b 1


:: ==========================================================================
:: FUNCION: COMPILAR UN COMPONENTE COMPLETO (con una segunda pasada si falla)
:: Uso: call :CompilarComponente CARPETA 1o0 [RUTA_COMPLETA]
:: ==========================================================================
:CompilarComponente
set "COMP=%~1"
set "COMP_DIR=%COMPONENTES_DIR%\%COMP%"
if not "%~3"=="" set "COMP_DIR=%~3"
set "INCLUIR_EXE=%~2"
echo.
echo ============================================
echo COMPONENTE: %COMP%
echo ============================================
if not exist "%COMP_DIR%\" goto CC_FALTA

set /a P_OK=0
set /a P_FAIL=0
if exist "%FALL_PASS%" del "%FALL_PASS%"

:: Primero compilar los proyectos externos de los que depende este componente.
call :ResolverReferencias "%COMP_DIR%"

set "PASADA=1"
:CC_PASADA
call :PasadaComponente "%COMP_DIR%"
if %P_FAIL% EQU 0 goto CC_FIN
echo.
echo   Hubo errores en %COMP% (se reintenta en la siguiente ronda).

:CC_FIN
set /a total+=P_OK+P_FAIL
set /a ok+=P_OK
set /a fail+=P_FAIL
if exist "%FALL_PASS%" type "%FALL_PASS%" >> "%FALLIDOS%"
exit /b 0

:CC_FALTA
echo [FALTA] No existe la carpeta: %COMP_DIR%
>>"%LOG_FILE%" echo [FALTA] No existe la carpeta: %COMP_DIR%
>>"%FALLIDOS%" echo %COMP% (carpeta no encontrada)
set /a fail+=1
exit /b 1


:: ==========================================================================
:: FUNCION: UNA PASADA DE UN COMPONENTE, EN ORDEN DE CAPAS
:: Uso: call :PasadaComponente "RUTA"
:: ==========================================================================
:PasadaComponente
if %PASADA% GEQ 2 set /a P_OK=0
if %PASADA% GEQ 2 set /a P_FAIL=0
if %PASADA% GEQ 2 if exist "%FALL_PASS%" del "%FALL_PASS%"
:: 1) Modelo y Controlador
for %%P in (CapaModelo_ CapaControlador_) do for /r "%~1" %%f in (%%P*.csproj) do call :CompilarProyecto "%%~ff"
:: 2) Librerias auxiliares (ej. Control_Usuarios): proyectos que NO son capa y son Library
for /r "%~1" %%f in (*.csproj) do call :CompilarNoCapa "%%~ff" LIB
:: 3) Vista
for %%P in (CapaVista_) do for /r "%~1" %%f in (%%P*.csproj) do call :CompilarProyecto "%%~ff"
:: 4) Ejecutables (WinExe/Exe), solo si INCLUIR_EXE=1
for /r "%~1" %%f in (*.csproj) do call :CompilarNoCapa "%%~ff" EXE
exit /b 0

:: Proyectos que no son CapaModelo/CapaControlador/CapaVista.
:: %2 = LIB (solo librerias) o EXE (solo ejecutables)
:CompilarNoCapa
echo %~nx1| findstr /b /i /c:"CapaModelo_" /c:"CapaControlador_" /c:"CapaVista_" >nul && exit /b 0
echo %~1| findstr /i /l /c:"\packages" >nul && exit /b 0
findstr /i /c:"<OutputType>Library</OutputType>" "%~1" >nul
if errorlevel 1 (set "ES_LIB=0") else (set "ES_LIB=1")
if "%~2"=="LIB" if "%ES_LIB%"=="0" exit /b 0
if "%~2"=="EXE" if "%ES_LIB%"=="1" exit /b 0
if "%~2"=="EXE" if not "%INCLUIR_EXE%"=="1" exit /b 0
call :CompilarProyecto "%~1"
exit /b 0


:: ==========================================================================
:: FUNCION: COMPILAR UN .CSPROJ
:: /restore + RestorePackagesConfig -> baja los paquetes NuGet de un clone nuevo
:: /t:Rebuild                       -> recompila desde cero
:: ==========================================================================
:CompilarProyecto
set "PROJ=%~1"
set "PROJ_NAME=%~n1"
set "PROJ_DIR=%~dp1"
set "PLOG=%LOG_DIR%\proy_%PROJ_NAME%.log"
echo   Compilando %PROJ_NAME% ...
call :BuscarSln "%PROJ_DIR%"
set "SDARG="
if not defined SLN_DIR set "SLN_DIR=%PROJ_DIR%..\"
set SDARG=/p:SolutionDir="%SLN_DIR%\"
"%MSBUILD_PATH%" "%PROJ%" /restore /p:RestorePackagesConfig=true %SDARG% /t:Rebuild /p:Configuration=Debug /nologo /verbosity:minimal > "%PLOG%" 2>&1
set "RC=%errorlevel%"
>>"%LOG_FILE%" echo ------ %PROJ%
type "%PLOG%" >> "%LOG_FILE%"
if not "%RC%"=="0" goto CP_ERROR
echo   [OK] %PROJ_NAME%
set /a P_OK+=1
call :CopiarDebugARelease "%PROJ_DIR%"
exit /b 0

:CP_ERROR
echo   [ERROR] %PROJ_NAME%  (codigo %RC%)
set "EFILE=%LOG_DIR%\errores_%PROJ_NAME%.txt"
findstr /i /c:": error " /c:"error MSB" /c:"error NU" /c:"MSB3245" /c:"MSB3243" "%PLOG%" > "%EFILE%"
:: Mostrar solo las primeras lineas de error para no llenar la pantalla.
powershell -NoProfile -Command "$l=@(Get-Content -LiteralPath '%EFILE%' -TotalCount 8 -Encoding UTF8); if($l.Count -eq 0){$l=Get-Content -LiteralPath '%PLOG%' -Tail 8 -Encoding UTF8}; $l" 2>nul
echo   (mas detalle en %LOG_DIR%\errores_%PROJ_NAME%.txt)
>>"%FALL_PASS%" echo %PROJ_NAME%
set /a P_FAIL+=1
exit /b 1


:: ==========================================================================
:: FUNCION: BUSCAR EL .SLN MAS CERCANO (hacia arriba) -> SLN_DIR (con "\" final)
:: Hace falta porque los .csproj usan $(SolutionDir) para restaurar NuGet y,
:: compilados sueltos, MSBuild no sabe cual es la solucion.
:: ==========================================================================
:BuscarSln
set "SLN_DIR="
set "WD=%~1"
:BS_LOOP
if exist "%WD%*.sln" set "SLN_DIR=%WD%" & exit /b 0
for %%p in ("%WD%..") do set "NWD=%%~fp"
if not "%NWD:~-1%"=="\" set "NWD=%NWD%\"
if /I "%NWD%"=="%WD%" exit /b 0
set "WD=%NWD%"
goto BS_LOOP


:: ==========================================================================
:: FUNCION: COPIAR LAS DLL DE bin\Debug A bin\Release DE UN PROYECTO
:: ==========================================================================
:CopiarDebugARelease
set "CD_SRC=%~1bin\Debug"
if not exist "%CD_SRC%\" exit /b 0
set "CD_DST=%~1bin\Release"
if not exist "%CD_DST%\" mkdir "%CD_DST%"
copy /Y "%CD_SRC%\*.dll" "%CD_DST%\" >nul 2>&1
exit /b 0


:: ==========================================================================
:: FUNCION: ENCONTRAR REFERENCIAS A DLL QUE NO EXISTEN
:: Lee las lineas HintPath de cada .csproj y anota en referencias_faltantes.txt
:: el nombre de las DLL cuyo archivo todavia no existe.
:: ==========================================================================
:EscanearFaltantes
if exist "%FALT%" del "%FALT%"
if exist "%FALT_DET%" del "%FALT_DET%"
for /r "%~1" %%c in (*.csproj) do call :RevisarProyectoRefs "%%~fc"
exit /b 0

:RevisarProyectoRefs
set "RP_DIR=%~dp1"
set "RP_PROJ=%~nx1"
:: El archivo lo lee TYPE (comando interno, soporta enie en el nombre) y
:: findstr solo filtra el texto que le llega por tuberia. Asi nunca se le
:: pasa a findstr un nombre de archivo con caracteres especiales.
for /f "usebackq delims=" %%L in (`type "%~1" ^| findstr /i /c:"<HintPath>"`) do call :RevisarHint "%RP_DIR%" "%%L"
exit /b 0

:RevisarHint
set "H=%~2"
for /f "tokens=2 delims=>" %%a in ("%H%") do set "H=%%a"
set "H=%H:</HintPath=%"
echo %H%| findstr /l /c:"$(" >nul && exit /b 0
echo %H%| findstr /i /l /c:"\packages" >nul && exit /b 0
pushd "%~1" 2>nul || exit /b 0
for %%i in ("%H%") do set "ABS=%%~fi" & set "BASE=%%~ni" & set "EXT=%%~xi"
popd
if exist "%ABS%" exit /b 0
if /I not "%EXT%"==".dll" exit /b 0
>>"%FALT_DET%" echo %BASE%   ^(lo pide %RP_PROJ%: %H%^)
findstr /x /i /c:"%BASE%" "%FALT%" >nul 2>&1 && exit /b 0
>>"%FALT%" echo %BASE%
exit /b 0


:: ==========================================================================
:: FUNCION: COMPILAR ANTES LOS PROYECTOS DE LOS QUE DEPENDE UN COMPONENTE
:: Si falta CapaVista_Bancos, busca en el repositorio CapaModelo_Bancos,
:: CapaControlador_Bancos y CapaVista_Bancos y los compila en ese orden.
:: ==========================================================================
:ResolverReferencias
set "RES_DIR=%~1"
call :EscanearFaltantes "%~1"
if not exist "%FALT%" exit /b 0
echo   Referencias a DLL que aun no existen; se buscan sus proyectos en el repositorio...
for /f "usebackq delims=" %%n in ("%FALT%") do call :BuscarYCompilarModulo "%%n"
exit /b 0

:BuscarYCompilarModulo
set "NOMBRE=%~1"
set "PREF="
set "MOD="
set "ENCONTRADO=0"
for /f "tokens=1,* delims=_" %%a in ("%NOMBRE%") do set "PREF=%%a" & set "MOD=%%b"
if /I "%PREF%"=="CapaModelo" goto BM_CAPAS
if /I "%PREF%"=="CapaControlador" goto BM_CAPAS
if /I "%PREF%"=="CapaVista" goto BM_CAPAS
call :BuscarProyecto "%NOMBRE%"
goto BM_FIN
:BM_CAPAS
call :BuscarProyecto "CapaModelo_%MOD%"
call :BuscarProyecto "CapaControlador_%MOD%"
call :BuscarProyecto "CapaVista_%MOD%"
:BM_FIN
if "%ENCONTRADO%"=="0" echo   [AVISO] No hay ningun proyecto llamado %NOMBRE% en el repositorio.
exit /b 0

:BuscarProyecto
set "BP=%~1"
for /r "%REPO_ROOT%" %%c in (%BP%.csproj) do call :CompilarExterno "%%~fc"
exit /b 0

:CompilarExterno
if not exist "%~1" exit /b 0
echo %~1| findstr /i /l /c:"\packages" >nul && exit /b 0
set "ENCONTRADO=1"
echo %~1| findstr /b /i /l /c:"%RES_DIR%" >nul && exit /b 0
echo   Dependencia externa: %~n1
call :CompilarProyecto "%~1"
exit /b 0


:: ==========================================================================
:: FUNCIONES DE VERIFICACION FINAL
:: ==========================================================================
:VerificarComponente
set "COMP=%~1"
set "VC_DIR=%COMPONENTES_DIR%\%COMP%"
if not "%~2"=="" set "VC_DIR=%~2"
set /a DLLS_COMP=0
if not exist "%VC_DIR%\" goto VC_RESULTADO
for /r "%VC_DIR%" %%d in (Capa*.dll) do call :ContarDll "%%~fd"
:VC_RESULTADO
if %DLLS_COMP% GTR 0 echo [OK] %COMP%: %DLLS_COMP% DLL de capas en bin\Debug
if %DLLS_COMP% EQU 0 echo [FALTA] %COMP%: no se encontraron DLL de capas en bin\Debug
exit /b 0

:ContarDll
echo %~1| findstr /i /l /c:"\bin\Debug" >nul || exit /b 0
set /a DLLS_COMP+=1
exit /b 0

:ListarFaltantes
set "LF_DIR=%COMPONENTES_DIR%\%~1"
if not "%~2"=="" set "LF_DIR=%~2"
if not exist "%LF_DIR%\" exit /b 0
call :EscanearFaltantes "%LF_DIR%"
if not exist "%FALT%" echo [OK] %~1: todas las referencias a DLL existen
if exist "%FALT%" echo [FALTA] %~1: estas DLL referenciadas no existen:
if exist "%FALT_DET%" type "%FALT_DET%"
exit /b 0
