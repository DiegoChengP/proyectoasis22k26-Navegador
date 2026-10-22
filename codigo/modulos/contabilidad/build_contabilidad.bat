:: ============================================================================
:: BUILD CONTABILIDAD - VISUAL STUDIO 2022
:: ============================================================================
:: Compila Modelo -> Controlador -> Vista -> Ejecucion, con referencias DLL.
:: Contabilidad usa el login y la pantalla de carga de Seguridad.
::
:: REQUISITO: CapaControlador_Contabilidad referencia CapaControlador_Seguridad.dll y el
:: ejecutable usa CapaVista_Seguridad.dll (login y MDI), por lo que primero hay que
:: haber ejecutado codigo\componentes\seguridad\build_Seguridad_GLOBAL.bat, que
:: tambien compila las tres capas de Contabilidad (Seguridad referencia su Vista).
:: Este .bat sirve para recompilar solo Contabilidad y su ejecutable.
::
:: Salida: Ejecucion_Contabilidad\Ejecucion_Contabilidad\bin\Debug\Ejecucion_Contabilidad.exe
:: ============================================================================
@echo off
setlocal

:: Si tienes Professional o Enterprise, cambia "Community" por la edicion.
set "MSBUILD_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
set "COMP=%~dp0..\..\componentes"
set "MOD=%~dp0"

if not exist "%MSBUILD_PATH%" (
    echo [ERROR] No se encontro MSBuild en: %MSBUILD_PATH%
    echo         Ajusta MSBUILD_PATH al inicio de este archivo.
    goto FIN_ERROR
)
if not exist "%COMP%\seguridad\Seguridad\CapaControlador_Seguridad\bin\Debug\CapaControlador_Seguridad.dll" (
    echo [ERROR] Falta CapaControlador_Seguridad.dll. Ejecuta primero:
    echo         %COMP%\seguridad\build_Seguridad_GLOBAL.bat
    goto FIN_ERROR
)
if not exist "%COMP%\seguridad\Seguridad\CapaVista_Seguridad\bin\Debug\CapaVista_Seguridad.dll" (
    echo [ERROR] Falta CapaVista_Seguridad.dll. Ejecuta primero:
    echo         %COMP%\seguridad\build_Seguridad_GLOBAL.bat
    goto FIN_ERROR
)

echo Compilando las tres capas de Contabilidad...
"%MSBUILD_PATH%" "%MOD%Contabilidad\Contabilidad.sln" /restore /t:Rebuild /p:Configuration=Debug /v:minimal /nologo
if errorlevel 1 goto FIN_ERROR

echo Compilando Ejecucion_Contabilidad...
"%MSBUILD_PATH%" "%MOD%Ejecucion_Contabilidad\Ejecucion_Contabilidad.sln" /restore /t:Rebuild /p:Configuration=Debug /v:minimal /nologo
if errorlevel 1 goto FIN_ERROR

echo.
echo [OK] %MOD%Ejecucion_Contabilidad\Ejecucion_Contabilidad\bin\Debug\Ejecucion_Contabilidad.exe
exit /b 0

:FIN_ERROR
echo.
pause
exit /b 1
