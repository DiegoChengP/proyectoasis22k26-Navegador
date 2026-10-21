# Movimientos, Cotizaciones y Cierre — 20/10/2026

## Base de datos y conexión

Los tres modelos usan ODBC y el DSN `EmbutidosS.A`. Se puede sustituir mediante la variable de entorno `VENTAS_ODBC` o la clave `VentasConexion` de `appSettings` en la configuración del ejecutable. No se incluyen contraseñas ni conexiones de pruebas en los proyectos.

Desde la raíz del repositorio, instalar el esquema en la base que seleccione el DSN:

```powershell
.\scripts\modulos\ventas\Instalar_Ventas_Funcional.ps1
```

El instalador aplica `scripts/modulos/ventas/Actualizar_Ventas_Funcional_20261020.sql`. Conserva las tablas existentes y omite los triggers que ya estén instalados. No cambia automáticamente de base, no borra registros y no carga clientes ni operaciones de ejemplo. Requiere las tablas de Seguridad `tblempleado` y `tblusuario`. Los DDL de MySQL confirman cada sentencia: si falla una, corregir la causa y ejecutar nuevamente el instalador.

Los catálogos deben contener datos reales antes de capturar: monedas, clientes, establecimientos emisores, vendedores, productos, listas y precios. Las cotizaciones requieren una asignación en `tblclientelistaprecios` por cliente y moneda; la moneda de la lista debe coincidir. Por ejemplo, tras crear los registros correspondientes, la asignación se registra con los IDs reales:

```sql
INSERT INTO tblclientelistaprecios(idCliente,idMoneda,idListaPrecio)
VALUES (@idCliente,@idMoneda,@idListaPrecio);
```

Los formularios muestran un selector de responsable basado en los usuarios activos de Seguridad. No se selecciona un usuario arbitrariamente. Un módulo que ya tenga sesión puede establecer la propiedad pública `VentasIdUsuario` antes de mostrar el formulario.

`Infraestructura` contiene fuentes enlazadas en las tres capas Modelo y Vista. No genera otra DLL ni un proyecto de recursos. Los recursos originales morados siguen integrados en cada Vista.

## Movimientos de clientes y pagos

- Estado de cuenta por cliente, moneda y fechas: saldo inicial, cargos, abonos, saldo final y libro con saldo acumulado. Los filtros de tipo/estado cambian las filas visibles; los indicadores siguen representando todo el período.
- La pestaña **Recibo** conserva el nombre solicitado y registra encabezados internos de CxC, notas y ajustes. El registro de factura aquí es interno, sin detalle de productos ni certificación DTE. Las notas requieren la referencia exacta `serie-numero` de una factura del mismo cliente y moneda.
- **Recepción de pagos** genera el recibo al guardar y permite aplicar el dinero a una o varias facturas, con pagos parciales. En esta etapa, el recibo debe quedar totalmente aplicado: no se registran anticipos sin factura.
- Los correlativos se generan al dejar el número vacío. El servidor verifica saldos, fechas, cliente, moneda, referencias, duplicados y períodos cerrados dentro de una transacción. Los cobros concurrentes no pueden consumir dos veces el mismo saldo. Un pago retroactivo tampoco puede dejar saldos negativos en fechas posteriores.
- Consulta de recibos y aplicaciones por número de recibo o referencia de depósito; vista previa de impresión para estado de cuenta, documentos internos, recibos y preparación de depósitos.
- `AnularRecibo` está disponible en Controlador para la integración: genera movimientos opuestos, conserva el historial y rechaza recibos asignados a depósitos. El botón Cancelar del formulario descarta la captura; no anula un recibo guardado.

### Bancos

Los catálogos de empresa, banco, cuenta y concepto siguen perteneciendo a Bancos. El formulario expone `VentasMetConfigurarCatalogosBancos(Empresas, Bancos, Cuentas, Conceptos, Modo)`; cada tabla debe contener `Id` y `Nombre`, y el adaptador debe suministrar combinaciones válidas de empresa/banco/cuenta/moneda. No se crean catálogos bancarios duplicados.

El modelo guarda preparaciones en `tbldepositoventas` y su detalle. Un recibo puede distribuirse entre varios depósitos sin superar su disponible. Guardar una preparación no modifica saldos bancarios ni afirma que Bancos recibió el dinero. Mientras no exista el adaptador real, los selectores de catálogos de Bancos quedan vacíos y el registro externo sigue pendiente.

## Cotizaciones

Seleccionar cliente y moneda carga la lista asignada y los productos/precios vigentes. Se captura cantidad, descuento como importe de la línea y tipo de cambio; los impuestos se calculan con la tasa configurada del producto, sin una tasa fija en el código.

Guardar como **Borrador** permite seguir editando. Guardar como **Vigente** emite y congela el documento. El servidor verifica los precios de la lista antes de guardar. Una cotización emitida conserva su detalle aunque después cambie el catálogo. Consultar con el botón de lupa o pulsar Enter en su número.

Solo una cotización emitida y vigente se convierte. **A pedido** crea encabezado y detalle con sus precios originales. **A factura** prepara una factura en estado BORRADOR con los mismos importes, cliente y trazabilidad; emitir/certificar la factura y afectar inventarios corresponden al módulo de Facturas. La conversión es única: repetir el mismo destino devuelve el documento ya creado; no permite crear otro destino ni convertir una vencida.

## Cierre de movimientos

Seleccionar moneda, mes/año o año completo y pulsar Consultar. Revisar los saldos, marcar la constancia y confirmar. El período debe haber finalizado. Los cierres mensuales son consecutivos; el anual requiere todos los meses operados del año hasta diciembre. Si las operaciones comenzaron a mitad del año, no exige cierres anteriores al primer mes registrado.

El cierre conserva saldos por factura y una fotografía por cliente con saldo anterior, cargos/abonos del período, cargos/abonos acumulados del año y saldo actual. Los saldos pendientes no impiden cerrar. No se mezclan monedas. El historial conserva códigos/nombres aunque se editen los catálogos.

Los triggers protegen el libro, los períodos y las fotografías. Un recibo de fecha posterior puede abonar una factura de un mes cerrado sin modificar su fotografía anterior. No se implementa reapertura sin versionado.

### Contabilidad

La partida resumen se prepara desde los movimientos del período cerrado. Las cuentas se suministran mediante `tblcuentacontablecxc`, por moneda/tipo de movimiento; cuando faltan, la vista muestra PENDIENTE y no permite trasladar.

Para conectar la DLL del equipo de Contabilidad, configurar el formulario con:

```csharp
formulario.VentasMetConfigurarEnlaceContable(
    (clave, idMoneda, fechaCorte, partida) =>
    {
        // Invocar aquí el servicio real de Contabilidad.
        // Debe deduplicar por clave y devolver su referencia confirmada.
        return servicioContable.Registrar(clave, idMoneda, fechaCorte, partida);
    });
```

`partida` contiene `Cuenta`, `Descripcion`, `Debe`, `Haber`. `tbltrasladocxc` y su detalle conservan la clave y la partida para reintentar tras fallos. La llamada externa se hace sin mantener una transacción SQL abierta. Una referencia confirmada se guarda y no vuelve a enviarse. Si la confirmación no llega, el receptor debe devolver la misma referencia al reintentar la misma clave. Sin adaptador real, el botón permanece deshabilitado; no se inventan cuentas ni pólizas confirmadas.

## Verificación

```powershell
.\codigo\modulos\ventas\Pruebas\Verificar_Ventas.ps1
```

La prueba crea una base aislada `dbventas_pruebas_*`, copia únicamente la estructura de empleado/usuario, instala el esquema y usa datos artificiales allí. Verifica persistencia, transacciones, concurrencia, monedas, pagos, depósitos, cotizaciones, cierres, integración contable con receptor de prueba y carga de los formularios. La base temporal se elimina al terminar; `-ConservarBase` permite inspeccionarla. El ejecutable rechaza bases ajenas al prefijo de pruebas y bases ya pobladas. Nunca carga fixtures en `dbsistemaembutidos`.

Después de modificar código, cerrar la aplicación y ejecutar `build_ventas.bat` para reunir las DLL actualizadas. No es necesario modificar Blazor, Reportes ni la estructura MVC del resto de componentes.
