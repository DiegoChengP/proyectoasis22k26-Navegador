:: ============================================================================
:: BUILD NAVEGADOR - VISUAL STUDIO 2022
:: ============================================================================
:: Compila, en una sola pasada y en el orden correcto de dependencias, todo lo
:: que necesita el Navegador y el modulo de RRHH:
::
::   1. BANCOS          : MovimientosBancarios, Bancos
::   2. CONSULTAS       : Consultas_Componentes -> Consultas_Mantenimiento -> Consultas
::   3. SEGURIDAD       : CapaModelo_Seguridad -> CapaControlador_Seguridad
::   4. NAVEGADOR       : CapaModelo -> CapaControlador -> CapaVista
::   5. RRHH            : Empleados, Capacitacion, Asistencia, Permisos y Vacaciones,
::                        Evaluacion de Desempeno, Nominas, Cierre de Nominas
::                        -> Recursos Humanos (MDI)
::   6. SEGURIDAD VISTA : CapaVista_Seguridad (usa Navegador y el MDI de RRHH)
::   7. EJECUTABLES     : Ejecucion_Navegador, Ejecucion_Seguridad,
::                        Ejecucion_Consultas, Ejecutable_Rrhh
::
:: CapaVista_Rrhh busca CapaControlador_Seguridad.dll dentro de
:: CapaVista_Seguridad\bin\Debug, pero CapaVista_Seguridad a su vez usa
:: CapaVista_Rrhh. Para romper ese circulo, despues del paso 3 se copian las
:: DLL de Seguridad a esa carpeta y asi RRHH compila antes que la vista.
::
:: Si un proyecto todavia no esta en el repo (no se ha aceptado su pull) sale
:: como [PENDIENTE] y se sigue con lo demas.
::
:: Funciona en cualquier compu:
::   - Busca el repositorio solo (subiendo desde donde este este .bat, o en
::     C:\proyectoasis22k26 / C:\proyectoasis22k26-Seguridad). Si no lo
::     encuentra, pide la ruta.
::   - Usa el MSBuild de Visual Studio 2022 Community. Si no esta en la ruta
::     normal lo busca con vswhere.
::
:: REPORTEADOR queda fuera a proposito (Navegador no lo usa).
::
:: Las capas (1 a 6) detienen el proceso al primer error de compilacion.
:: Los ejecutables (7) se compilan todos y se reporta cuales fallaron.
:: ============================================================================

:INICIO
@echo off
setlocal enabledelayedexpansion
color 0A

:: ---------------------------------------------------------------------------
:: MSBUILD
:: ---------------------------------------------------------------------------
set "MSBUILD_PATH="
:: Primero la Community, que es la que usamos todos
if exist "%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_PATH=%ProgramFiles%\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if defined MSBUILD_PATH goto MSBUILD_MANUAL
:: Si no esta en la ruta normal, se busca con vswhere
set "VSWHERE=%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if not exist "%VSWHERE%" goto MSBUILD_MANUAL
for /f "usebackq delims=" %%m in (`"%VSWHERE%" -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do if not defined MSBUILD_PATH set "MSBUILD_PATH=%%m"

:MSBUILD_MANUAL
for %%e in (Community Professional Enterprise BuildTools) do if not defined MSBUILD_PATH if exist "%ProgramFiles%\Microsoft Visual Studio\2022\%%e\MSBuild\Current\Bin\MSBuild.exe" set "MSBUILD_PATH=%ProgramFiles%\Microsoft Visual Studio\2022\%%e\MSBuild\Current\Bin\MSBuild.exe"

:: ---------------------------------------------------------------------------
:: REPOSITORIO
:: ---------------------------------------------------------------------------
if defined REPO goto REPO_LISTO
for %%r in ("%~dp0." "%~dp0.." "%~dp0..\.." "%~dp0..\..\.." "C:\proyectoasis22k26" "C:\proyectoasis22k26-Seguridad") do if not defined REPO if exist "%%~r\codigo\componentes" set "REPO=%%~fr"
if defined REPO goto REPO_LISTO
echo No se encontro el repositorio automaticamente.
set /p "REPO=Escribe la ruta del repositorio (ej. C:\proyectoasis22k26): "
set "REPO=!REPO:"=!"

:REPO_LISTO
if "!REPO:~-1!"=="\" set "REPO=!REPO:~0,-1!"
set "COMP=!REPO!\codigo\componentes"
set "MOD=!REPO!\codigo\modulos"

set "ROOT_DIR=%~dp0"
cd /d "%ROOT_DIR%"
if not exist "logs" mkdir logs
set "LOG=%ROOT_DIR%logs\build_navegador_log.txt"
echo ==== Build Navegador %DATE% %TIME% ==== > "%LOG%"

echo ============================================
echo COMPILACION NAVEGADOR + RRHH
echo Repositorio: !REPO!
echo ============================================

if not defined MSBUILD_PATH (
    echo [ERROR] No se encontro MSBuild. Instala Visual Studio 2022
    echo         con la carga de trabajo de escritorio .NET.
    goto FIN_ERROR
)
if not exist "!MSBUILD_PATH!" (
    echo [ERROR] No se encontro MSBuild en:
    echo         !MSBUILD_PATH!
    goto FIN_ERROR
)
if not exist "!COMP!" (
    echo [ERROR] No existe la carpeta de componentes:
    echo         !COMP!
    set "REPO="
    goto FIN_ERROR
)
if not exist "!MOD!" (
    echo [ERROR] No existe la carpeta de modulos:
    echo         !MOD!
    set "REPO="
    goto FIN_ERROR
)
echo MSBuild: !MSBUILD_PATH!

set /a total=0
set /a ok=0
set /a fail=0
set /a pend=0

:: ==========================================================
:: 1) BANCOS
:: ==========================================================
echo.
echo ===== 1.1) MOVIMINETOS BANCARIOS =====
call :Build "%MOD%\bancos\Movimientos_Bancarios\CapaModelo_MovimientosBancarios\CapaModelo_MovimientosBancarios.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Movimientos_Bancarios\CapaControlador_MovimientosBancarios\CapaControlador_MovimientosBancarios.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Movimientos_Bancarios\CapaVista_MovimientosBancarios\CapaVista_MovimientosBancarios.csproj" || goto RESUMEN

echo.
echo ===== 1.2) AUTORIZAR ORDE DE COMPRA =====
call :Build "%MOD%\bancos\Autorizacion_OrdenesCompra\CapaModelo_AutorizacionOrdenes\CapaModelo_AutorizacionOrdenes.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Autorizacion_OrdenesCompra\CapaControlador_AutorizacionOrdenes\CapaControlador_AutorizacionOrdenes.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Autorizacion_OrdenesCompra\CapaVista_AutorizacionOrdenes\CapaVista_AutorizacionOrdenes.csproj" || goto RESUMEN

echo.
echo ===== 1.4) DISPONIBILIDAD DIARIA =====
call :Build "%MOD%\bancos\DisponibilidadDiaria\CapaModelo_DisponibilidadDiaria\CapaModelo_DisponibilidadDiaria.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\DisponibilidadDiaria\CapaControlador_DisponibilidadDiaria\CapaControlador_DisponibilidadDiaria.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\DisponibilidadDiaria\CapaVista_DisponibilidadDiaria\CapaVista_DisponibilidadDiaria.csproj" || goto RESUMEN

echo.
echo ===== 1.5) CAMBIO DE MONEDA =====
call :Build "%MOD%\bancos\TipoCambio\CapaModelo_TipoCambio\CapaModelo_TipoCambio.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\TipoCambio\CapaControlador_TipoCambio\CapaControlador_TipoCambio.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\TipoCambio\CapaVista_TipoCambio\CapaVista_TipoCambio.csproj" || goto RESUMEN

echo.
echo ===== 1.6) ENLACE A OTROS MODULOS =====
call :Build "%MOD%\bancos\Enlace_Otros_Modulos\CapaModelo_EnlaceB\CapaModelo_EnlaceB.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Enlace_Otros_Modulos\CapaControlador_EnlaceB\CapaControlador_EnlaceB.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Enlace_Otros_Modulos\CapaVista_EnlaceB\CapaVista_EnlaceB.csproj" || goto RESUMEN

echo.
echo ===== 1.?) MDI BANCOS =====
call :Build "%MOD%\bancos\Bancos\CapaModelo_Bancos\CapaModelo_Bancos.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Bancos\CapaControlador_Bancos\CapaControlador_Bancos.csproj" || goto RESUMEN
call :Build "%MOD%\bancos\Bancos\CapaVista_Bancos\CapaVista_Bancos.csproj" || goto RESUMEN

:: ==========================================================
:: 2) CONSULTAS
:: ==========================================================
echo.
echo ===== 2) CONSULTAS =====
call :Build "%COMP%\consultas\Consultas_Componentes\CapaModelo_Componentes\CapaModelo_Componentes.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas_Componentes\CapaControlador_Componentes\CapaControlador_Componentes.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas_Componentes\CapaVista_Componentes\CapaVista_Componentes.csproj" || goto RESUMEN

call :Build "%COMP%\consultas\Consultas_Mantenimiento\CapaModelo_Mantenimiento\CapaModelo_Mantenimiento.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas_Mantenimiento\CapaControlador_Mantenimiento\CapaControlador_Mantenimiento.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas_Mantenimiento\CapaVista_Mantenimiento\CapaVista_Mantenimiento.csproj" || goto RESUMEN

call :Build "%COMP%\consultas\Consultas\CapaModelo_Consultas\CapaModelo_Consultas.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas\CapaControlador_Consultas\CapaControlador_Consultas.csproj" || goto RESUMEN
call :Build "%COMP%\consultas\Consultas\CapaVista_Consultas\CapaVista_Consultas.csproj" || goto RESUMEN

:: ==========================================================
:: 3) SEGURIDAD (capas que NO dependen del Navegador)
:: ==========================================================
echo.
echo ===== 3) SEGURIDAD: MODELO Y CONTROLADOR =====
call :Build "%COMP%\seguridad\Seguridad\CapaModelo_Seguridad\CapaModelo_Seguridad.csproj" || goto RESUMEN
call :Build "%COMP%\seguridad\Seguridad\CapaControlador_Seguridad\CapaControlador_Seguridad.csproj" || goto RESUMEN

:: RRHH busca estas DLL en la carpeta de la vista de Seguridad
set "SEG_VISTA_BIN=%COMP%\seguridad\Seguridad\CapaVista_Seguridad\bin\Debug"
if not exist "%SEG_VISTA_BIN%" mkdir "%SEG_VISTA_BIN%"
xcopy /y /q "%COMP%\seguridad\Seguridad\CapaControlador_Seguridad\bin\Debug\*.dll" "%SEG_VISTA_BIN%\" >> "%LOG%" 2>&1

:: ==========================================================
:: 4) NAVEGADOR
:: ==========================================================
echo.
echo ===== 4) NAVEGADOR =====
call :Build "%COMP%\navegador\Navegador\CapaModelo_Navegador\CapaModelo_Navegador.csproj" || goto RESUMEN
call :Build "%COMP%\navegador\Navegador\CapaControlador_Navegador\CapaControlador_Navegador.csproj" || goto RESUMEN
call :Build "%COMP%\navegador\Navegador\CapaVista_Navegador\CapaVista_Navegador.csproj" || goto RESUMEN

:: ==========================================================
:: 5) RRHH
:: Los submodulos primero, el MDI usa sus DLL
:: ==========================================================
echo.
echo ===== 5) RRHH: EMPLEADOS =====
call :Build "%MOD%\rrhh\Empleados\CapaModelo_Empleado\CapaModelo_Empleado.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Empleados\CapaControlador_Empleado\CapaControlador_Empleado.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Empleados\CapaVista_Empleado\CapaVista_Empleado.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: CAPACITACION =====
call :Build "%MOD%\rrhh\Capacitacion Empleados\CapaModelo_CapacitacionEmpleado\CapaModelo_CapacitacionEmpleado.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Capacitacion Empleados\CapaControlador_CapacitacionEmpleado\CapaControlador_CapacitacionEmpleado.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Capacitacion Empleados\CapaVista_CapacitacionEmpleado\CapaVista_CapacitacionEmpleado.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: ASISTENCIA =====
call :Build "%MOD%\rrhh\Asistencia\CapaModelo_Asistencia\CapaModelo_Asistencia.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Asistencia\CapaControlador_Asistencia\CapaControlador_Asistencia.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Asistencia\CapaVista_Asistencia\CapaVista_Asistencia.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: PERMISOS Y VACACIONES =====
call :Build "%MOD%\rrhh\PermisosVacaciones\CapaModelo_PermisosVacaciones\CapaModelo_PermisosVacaciones.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\PermisosVacaciones\CapaControlador_PermisosVacaciones\CapaControlador_PermisosVacaciones.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\PermisosVacaciones\CapaVista_PermisosVacaciones\CapaVista_PermisosVacaciones.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: EVALUACION DE DESEMPENO =====
:: La carpeta lleva enie y el .bat no la lee bien, por eso se busca con *
set "EVAL="
for /d %%d in ("%MOD%\rrhh\EvaluacionDesempe*") do set "EVAL=%%~fd"
if not defined EVAL call :Build "%MOD%\rrhh\EvaluacionDesempeno\CapaModelo_EvaluacionDesempeno.csproj"
if defined EVAL for %%c in (CapaModelo CapaControlador CapaVista) do for /d %%p in ("!EVAL!\%%c_*") do for %%f in ("%%p\*.csproj") do call :Build "%%~ff" || goto RESUMEN

echo.
echo ===== 5) RRHH: NOMINAS =====
call :Build "%MOD%\rrhh\NominasEmpleados\CapaModelo_Nominas\CapaModelo_Nominas.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\NominasEmpleados\CapaControlador_Nomina\CapaControlador_Nomina.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\NominasEmpleados\CapaVista_Nomina\CapaVista_Nomina.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: CIERRE DE NOMINAS =====
call :Build "%MOD%\rrhh\Cierre de Nominas\CapaModelo_CierreNominas\CapaModelo_CierreNominas.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Cierre de Nominas\CapaContolador_CierreNominas\CapaControlador_CierreNominas.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Cierre de Nominas\CapaVista_CierreNominas\CapaVista_CierreNominas.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: LIQUIDACION DE EMPLEADOS =====
call :Build "%MOD%\rrhh\Liquidacion_Empleados_Rrhh\CapaModelo_Liquidacion_Rrhh\CapaModelo_Liquidacion_Rrhh.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Liquidacion_Empleados_Rrhh\CapaControlador_Liquidacion_Rrhh\CapaControlador_Liquidacion_Rrhh.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Liquidacion_Empleados_Rrhh\CapaVista_Liquidacion_Rrhh\CapaVista_Liquidacion_Rrhh.csproj" || goto RESUMEN

echo.
echo ===== 5) RRHH: RECURSOS HUMANOS (MDI) =====
call :Build "%MOD%\rrhh\Recursos Humanos\CapaModelo_Rrhh\CapaModelo_Rrhh.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Recursos Humanos\CapaControlador_Rrhh\CapaControlador_Rrhh.csproj" || goto RESUMEN
call :Build "%MOD%\rrhh\Recursos Humanos\CapaVista_Rrhh\CapaVista_Rrhh.csproj" || goto RESUMEN

:: ==========================================================
:: 6) SEGURIDAD VISTA (usa Navegador y el MDI de RRHH, por eso va despues)
:: ==========================================================
echo.
echo ===== 6) SEGURIDAD: VISTA =====
call :Build "%COMP%\seguridad\Seguridad\CapaVista_Seguridad\CapaVista_Seguridad.csproj" || goto RESUMEN

:: ==========================================================
:: 7) EJECUTABLES (no detienen el proceso si fallan)
:: ==========================================================
echo.
echo ===== 7) EJECUTABLES =====
call :Build "%COMP%\navegador\Ejecucion_Navegador\Ejecucion_Navegador.sln"
call :Build "%COMP%\seguridad\Ejecucion_Seguridad\Ejecucion_Seguridad.sln"
call :Build "%COMP%\consultas\Ejecucion_Consultas\Ejecucion_Consultas.sln"
call :Build "%MOD%\rrhh\Ejecutable_Rrhh\Ejecutable_Rrhh.sln"

:RESUMEN
echo.
echo ============================================
echo RESUMEN
echo Total: %total%   Correctos: %ok%   Errores: %fail%   Pendientes: %pend%
echo Log completo: %LOG%
echo ============================================

:: ==========================================================
:: VERIFICAR DLL / EXE GENERADOS
:: ==========================================================
echo.
echo ===== VERIFICANDO SALIDAS =====
set "SALIDAS="
set "SALIDAS=%SALIDAS% consultas\Consultas_Componentes\CapaVista_Componentes\bin\Debug\CapaVista_Componentes.dll"
set "SALIDAS=%SALIDAS% consultas\Consultas_Mantenimiento\CapaVista_Mantenimiento\bin\Debug\CapaVista_Mantenimiento.dll"
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
    if exist "!COMP!\%%s" (
        echo [OK]    %%s
    ) else (
        echo [FALTA] %%s
    )
)

:: Las de RRHH van con comillas porque hay carpetas con espacios
set SALIDAS_RRHH="rrhh\Empleados\CapaVista_Empleado\bin\Debug\CapaVista_Empleado.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\Capacitacion Empleados\CapaVista_CapacitacionEmpleado\bin\Debug\CapaVista_CapacitacionEmpleado.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\Asistencia\CapaVista_Asistencia\bin\Debug\CapaVista_Asistencia.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\PermisosVacaciones\CapaVista_PermisosVacaciones\bin\Debug\CapaVista_PermisosVacaciones.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\NominasEmpleados\CapaVista_Nomina\bin\Debug\CapaVista_Nomina.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\Cierre de Nominas\CapaVista_CierreNominas\bin\Debug\CapaVista_CierreNominas.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\Recursos Humanos\CapaVista_Rrhh\bin\Debug\CapaVista_Rrhh.dll"
set SALIDAS_RRHH=%SALIDAS_RRHH% "rrhh\Ejecutable_Rrhh\Ejecutable_Rrhh\bin\Debug\Ejecutable_Rrhh.exe"

for %%s in (%SALIDAS_RRHH%) do (
    if exist "!MOD!\%%~s" (
        echo [OK]    %%~s
    ) else (
        echo [FALTA] %%~s
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
if not exist "%~1" (
    echo [PENDIENTE] %~nx1  ^(todavia no esta en el repo^)
    echo [PENDIENTE] No existe: %~1 >> "%LOG%"
    set /a pend+=1
    exit /b 0
)
if /i "%~x1"==".csproj" (
    "!MSBUILD_PATH!" "%~1" /restore /p:RestorePackagesConfig=true /p:SolutionDir="%~dp1..\\" /t:Rebuild /p:Configuration=Debug /v:minimal >> "%LOG%" 2>&1
) else (
    "!MSBUILD_PATH!" "%~1" /restore /t:Rebuild /p:Configuration=Debug /v:minimal >> "%LOG%" 2>&1
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
