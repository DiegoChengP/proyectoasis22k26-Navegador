# Organización de Ventas

Cada componente tiene una carpeta, tres proyectos MVC y una solución de Visual Studio. La separación conserva los formularios y su lógica actual.

| Carpeta | Solución | Capas |
|---|---|---|
| Facturas | `Facturas/Facturas.sln` | `CapaModelo_Facturas`, `CapaControlador_Facturas`, `CapaVista_Facturas` |
| ListadoPrecios | `ListadoPrecios/ListadoPrecios.sln` | `CapaModelo_ListadoPrecios`, `CapaControlador_ListadoPrecios`, `CapaVista_ListadoPrecios` |
| Cotizaciones | `Cotizaciones/Cotizaciones.sln` | `CapaModelo_Cotizaciones`, `CapaControlador_Cotizaciones`, `CapaVista_Cotizaciones` |
| MovimientosClientes | `MovimientosClientes/MovimientosClientes.sln` | `CapaModelo_MovimientosClientes`, `CapaControlador_MovimientosClientes`, `CapaVista_MovimientosClientes` |
| CierreMovimientos | `CierreMovimientos/CierreMovimientos.sln` | `CapaModelo_CierreMovimientos`, `CapaControlador_CierreMovimientos`, `CapaVista_CierreMovimientos` |
| Reportes | `Reportes/Reportes.sln` | `CapaModelo_Reportes`, `CapaControlador_Reportes`, `CapaVista_Reportes` |
| Mantenimientos | `Mantenimientos/Mantenimientos.sln` | `CapaModelo_Mantenimientos`, `CapaControlador_Mantenimientos`, `CapaVista_Mantenimientos` |
| ComisionVendedores | `ComisionVendedores/ComisionVendedores.sln` | Las tres capas existentes de ComisionVendedores |
| Pedidos | `Pedidos/Pedidos.sln` | Las tres capas existentes de Pedidos |
| Ventas y CxC | `Ventas y CxC/Ventas y CxC.sln` | MDI y capas comunes de Ventas |

En cada componente, Vista referencia Controlador y Controlador referencia Modelo. Las capas nuevas de los formularios que eran prototipos quedan preparadas para implementar su lógica posteriormente; esta reorganización no agrega operaciones de negocio.

## Ejecutar el sistema

Abrir **`Ejecutable_Ventas/Ejecutable_Ventas.sln`** y ejecutar el proyecto `Ejecutable_Ventas`. Las soluciones de cada componente contienen bibliotecas, igual que Pedidos; sirven para trabajar y compilar sus tres capas.

El MDI carga la DLL de cada módulo al pulsar su botón. No tiene referencias de compilación a los proyectos de las otras vistas. Facturas conserva su ventana independiente; las demás opciones mantienen su presentación modal.

Ejecutar primero **`build_ventas.bat`** desde esta carpeta. Se compilan en paralelo las nueve soluciones MVC, las capas comunes y los ejecutables; después se reúnen las DLL de los módulos y sus dependencias junto a `Ejecutable_Ventas.exe`. Las compilaciones posteriores reutilizan los proyectos que no cambiaron.

Uso desde una terminal:

```bat
build_ventas.bat Debug /sinpausa
build_ventas.bat Release /sinpausa
```

Con doble clic compila en Debug y muestra un panel con los colores de Ventas si la consola admite ANSI. Al terminar presenta el resultado y el tiempo de compilación, con opciones para repetir, cambiar entre Debug y Release, abrir la aplicación, consultar el log o salir. La salida muestra el resumen de MSBuild, las advertencias y los errores; el detalle completo queda en el log.

El modo `/sinpausa` conserva el uso desde terminales y automatizaciones: compila sin menú, colores ni limpieza de pantalla y devuelve el código de salida. El script busca MSBuild mediante `vswhere`; también admite definir `VENTAS_MSBUILD` con la ruta de `MSBuild.exe`. El log queda en `logs/build_ventas_Debug.log` o `logs/build_ventas_Release.log`.

El BAT utiliza `build_ventas.proj` de la misma carpeta para organizar la compilación en paralelo y copiar las bibliotecas. Conservar ambos archivos juntos.

Después puede abrirse el ejecutable o iniciarse desde Visual Studio. Si se compila un módulo por separado, volver a ejecutar el BAT para actualizar su DLL en la carpeta del ejecutable. Al faltar una DLL, el MDI muestra un aviso para ejecutar el BAT.

El acceso a Listado de precios utiliza la vista MVC que ya existía. Su namespace es `CapaVista_ListadoPrecios`; se retiró del MDI la copia anterior del formulario. La lógica de consulta, cálculo y guardado de ese componente se conserva.

Movimientos de clientes y Recepción de pagos comparten `CapaVista_MovimientosClientes`. El segundo acceso abre la pestaña de pagos del mismo diseño, conservando la unificación acordada en la reunión.

Comisiones conserva sus dos formularios existentes en la misma capa Vista. La solución anterior `Ejecutor_ComisionVendedores.sln` y su ejecutable siguen disponibles; las dependencias entre capas ahora son referencias de proyecto.

## Recursos y paquetes

Cada Vista tiene sus propios `Properties/Resources.resx` y `Resources.Designer.cs`, con enlaces a los archivos originales de `recursos/modulos/ventas`. Las imágenes se integran en la DLL de esa Vista durante la compilación. No hay proyecto ni DLL adicional para recursos; se conservan los colores, tamaños y diseños.

ListadoPrecios y Reportes tienen `NuGet.Config` para reutilizar la carpeta existente `Ventas y CxC/packages`. Si faltan paquetes en otra computadora, restaurar los paquetes NuGet de la solución correspondiente.

Las definiciones RDLC y las dependencias nativas de SQL Server que estaban en la vista común ahora pertenecen a `Reportes/CapaVista_Reportes`.

Blazor conserva su solución y su estructura independientes.
