:: ============================================================================
:: BUILD NAVEGADOR - VISUAL STUDIO 2022
:: ============================================================================
:: Compila, en una sola pasada y en el orden correcto de dependencias, todo lo
:: que necesita el Navegador:
::
::   1. CONSULTAS      : CapaModelo -> CapaControlador -> CapaVista
::   2. SEGURIDAD      : CapaModelo_Seguridad -> CapaControlador_Seguridad
::   3. NAVEGADOR      : Control_Usuarios -> CapaModelo -> CapaControlador -> CapaVista
::   4. SEGURIDAD VISTA: CapaVista_Seguridad (depende de CapaVista_Navegador)
::   5. EJECUTABLES    : Ejecucion_Navegador, Ejecucion_Seguridad, Ejecucion_Consultas
:: ============================================================================

:INICIO
@echo off
setlocal enabledelayedexpansion
color 0A

:: ---------------------------------------------------------------------------
:: RUTAS
:: ---------------------------------------------------------------------------
set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"

:: Sube un solo nivel desde 'navegador' para situarse en 'codigo\componentes'
pushd "%~dp0.."
set "COMP=%CD%"
popd

set "ROOT_DIR=%~dp0"
cd /d "%ROOT_DIR%"
if not exist "logs" mkdir logs
set "LOG=%ROOT_DIR%logs\build_navegador_log.txt"
echo ==== Build Navegador %DATE% %TIME% ==== > "%LOG%"

echo ============================================
echo COMPILACION NAVEGADOR (CONSULTAS + SEGURIDAD)
echo ============================================
echo Componentes en: %COMP%
echo.

if not exist "%MSBUILD_PATH%" (
    echo [ERROR] No se encontro MSBuild en:
    echo         %MSBUILD_PATH%
    echo         Ajusta MSBUILD_PATH al inicio de este archivo.
    goto FIN_ERROR
)
if not exist "%COMP%" (
    echo [ERROR] No existe la carpeta de componentes:
    echo         %COMP%
    goto FIN_ERROR
)

set /a total=0
set /a ok=0
set /a fail=0

:: ==========================================================
:: 1) CONSULTAS
:: ==========================================================
echo.
echo ===== 1) CONSULTAS =====
call :Build "%COMP%\consultas\Consultas\CapaModelo_Consultas\CapaModelo_Consultas.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas\CapaControlador_Consultas\CapaControlador_Consultas.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas\CapaVista_Consultas\CapaVista_Consultas.csproj" || goto RESUMEN

:: ==========================================================
:: 2) SEGURIDAD (capas que NO dependen del Navegador)
:: ==========================================================
echo.
echo ===== 2) SEGURIDAD: MODELO Y CONTROLADOR =====
call :Build "%COMP%\seguridad\Seguridad\CapaModelo_Seguridad\CapaModelo_Seguridad.csproj" || goto RESUMEN
call :Build "%COMP%\seguridad\Seguridad\CapaControlador_Seguridad\CapaControlador_Seguridad.csproj" || goto RESUMEN

:: ==========================================================
:: 3) NAVEGADOR
:: ==========================================================
echo.
echo ===== 3) NAVEGADOR =====
if exist "%COMP%\navegador\Navegador\Control_Usuarios\Control_Usuarios.csproj" (
    call :Build "%COMP%\navegador\Navegador\Control_Usuarios\Control_Usuarios.csproj" || goto RESUMEN
)
call :Build "%COMP%\navegador\Navegador\CapaModelo_Navegador\CapaModelo_Navegador.csproj" || goto RESUMEN
call :Build "%COMP%\navegador\Navegador\CapaControlador_Navegador\CapaControlador_Navegador.csproj" || goto RESUMEN
call :Build "%COMP%\navegador\Navegador\CapaVista_Navegador\CapaVista_Navegador.csproj" || goto RESUMEN

:: ==========================================================
:: 4) SEGURIDAD VISTA (usa el control Navegador, por eso va despues)
:: ==========================================================
echo.
echo ===== 4) SEGURIDAD: VISTA =====
call :Build "%COMP%\seguridad\Seguridad\CapaVista_Seguridad\CapaVista_Seguridad.csproj" || goto RESUMEN

:: ==========================================================
:: 5) EJECUTABLES (no detienen el proceso si fallan)
:: ==========================================================
echo.
echo ===== 5) EJECUTABLES =====
call :Build "%COMP%\navegador\Ejecucion_Navegador\Ejecucion_Navegador.sln"
call :Build "%COMP%\seguridad\Ejecucion_Seguridad\Ejecucion_Seguridad.sln"
call :Build "%COMP%\consultas\Ejecucion_Consultas\Ejecucion_Consultas.sln"

:RESUMEN
echo.
echo ============================================
echo RESUMEN
echo Total: %total%   Correctos: %ok%   Errores: %fail%
echo Log completo: %LOG%
echo ============================================

:: ==========================================================
:: VERIFICAR DLL / EXE GENERADOS
:: ==========================================================
echo.
echo ===== VERIFICANDO SALIDAS =====
set "SALIDAS="
set "SALIDAS=%SALIDAS% consultas\Consultas\CapaVista_Consultas\bin\Debug\CapaVista_Consultas.dll"
set "SALIDAS=%SALIDAS% seguridad\Seguridad\CapaModelo_Seguridad\bin\Debug\CapaModelo_Seguridad.dll"
set "SALIDAS=%SALIDAS% seguridad\Seguridad\CapaControlador_Seguridad\bin\Debug\CapaControlador_Seguridad.dll"
set "SALIDAS=%SALIDAS% navegador\Navegador\CapaModelo_Navegador\bin\Debug\CapaModelo_Navegador.dll"
set "SALIDAS=%SALIDAS% navegador\Navegador\CapaControlador_Navegador\bin\Debug\CapaControlador_Navegador.dll"
set "SALIDAS=%SALIDAS% navegador\Navegador\CapaVista_Navegador\bin\Debug\CapaVista_Navegador.dll"
set "SALIDAS=%SALIDAS% seguridad\Seguridad\CapaVista_Seguridad\bin\Debug\CapaVista_Seguridad.dll"
set "SALIDAS=%SALIDAS% navegador\Ejecucion_Navegador\Ejecucion_Navegador\bin\Debug\Ejecucion_Navegador.exe"
set "SALIDAS=%SALIDAS% seguridad\Ejecucion_Seguridad\Ejecucion_Seguridad\bin\Debug\Ejecucion_Seguridad.exe"
set "SALIDAS=%SALIDAS% consultas\Ejecucion_Consultas\Ejecucion_Consultas\bin\Debug\Ejecucion_Consultas.exe"

for %%s in (%SALIDAS%) do (
    if exist "%COMP%\%%s" (
        echo [OK]    %%s
    ) else (
        echo [FALTA] %%s
    )
)

echo.
echo ============================================
echo [R] Recompilar     [S] Salir
echo ============================================
choice /c RS /n /m "Seleccion: "
if errorlevel 2 goto FIN
if errorlevel 1 goto INICIO

:FIN_ERROR
echo.
pause
exit /b 1

:FIN
exit /b 0


:: ==========================================================
:: FUNCION :Build  -> %1 = ruta del .csproj o .sln
:: Restaura paquetes NuGet, recompila en Debug y registra el resultado.
:: Devuelve errorlevel 0 si compilo, 1 si fallo.
:: ==========================================================
:Build
set /a total+=1
echo ------------------------------------------------
echo Compilando: %~nx1
echo ------------------------------------------------
echo. >> "%LOG%"
echo ##### %~1 >> "%LOG%"
if /i "%~x1"==".csproj" (
    "%MSBUILD_PATH%" "%~1" /restore /p:RestorePackagesConfig=true /p:SolutionDir="%~dp1..\\" /t:Rebuild /p:Configuration=Debug /v:minimal >> "%LOG%" 2>&1
) else (
    "%MSBUILD_PATH%" "%~1" /restore /t:Rebuild /p:Configuration=Debug /v:minimal >> "%LOG%" 2>&1
)
if errorlevel 1 (
    echo [ERROR] %~nx1  ^(ver log^)
    echo [ERROR] %~1 >> "%LOG%"
    set /a fail+=1
    exit /b 1
)
echo [OK] %~nx1
echo [OK] %~1 >> "%LOG%"
set /a ok+=1
exit /b 0