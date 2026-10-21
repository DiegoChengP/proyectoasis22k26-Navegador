using System;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using CapaModelo_MovimientosClientes;
using CapaModelo_Cotizaciones;
using CapaModelo_CierreMovimientos;

internal static class PruebasVentas
{
    private static int Correctas;
    private static OdbcConnection Conexion;
    private static void Comprobar(bool Condicion, string Mensaje)
    { if (!Condicion) throw new Exception("FALLO: " + Mensaje); Correctas++; Console.WriteLine("OK " + Mensaje); }
    private static object Sql(string Texto)
    {
        using (var Cmd = new OdbcCommand(Texto, Conexion)) return Cmd.ExecuteScalar();
    }
    private static void Rechaza(Action Accion, string Mensaje)
    {
        bool Rechazado = false;
        try { Accion(); } catch (InvalidOperationException) { Rechazado = true; } catch (OdbcException) { Rechazado = true; }
        Comprobar(Rechazado, Mensaje);
    }
    private static DataTable Aplicaciones(params object[] Pares)
    {
        var T = new DataTable(); T.Columns.Add("idFactura", typeof(int)); T.Columns.Add("Importe", typeof(decimal));
        for (int I = 0; I < Pares.Length; I += 2) T.Rows.Add(Pares[I], Pares[I + 1]); return T;
    }
    private static DataTable Lineas(decimal Precio = 100m)
    {
        var T = new DataTable(); T.Columns.Add("idProducto", typeof(int));
        foreach (string C in new[] { "Cantidad", "Precio", "Descuento" }) T.Columns.Add(C, typeof(decimal));
        T.Rows.Add(1, 2m, Precio, 10m); return T;
    }
    private static decimal Saldo(int Factura) { return Convert.ToDecimal(Sql("SELECT COALESCE(SUM(cargo-abono),0) FROM tblmovimientocxc WHERE idFactura=" + Factura)); }
    private static void Catalogos()
    {
        // Datos artificiales SOLO en la base aislada. Nunca se ejecutan en dbsistemaembutidos.
        Sql("INSERT INTO tblempleado(idEmpleado,codigoEmpleado,dpiEmpleado,nitEmpleado,nombresEmpleado,apellidosEmpleado,puestoEmpleado,generoEmpleado,fechaNacimientoEmpleado,fechaContratacionEmpleado,telefonoEmpleado,correoEmpleado) VALUES (1,'PRUEBA','0000000000000','PRUEBA','Persona','Pruebas','Pruebas','M','2000-01-01','2020-01-01','0','prueba@example.invalid')");
        Sql("INSERT INTO tblusuario(idUsuario,idEmpleado,nombreUsuario,contrasenaUsuario,ultimoAccesoUsuario) VALUES (1,1,'prueba','sin-credencial',CURRENT_TIMESTAMP)");
        Sql("INSERT INTO tblmonedaventas(idMoneda,codigoMoneda,nombreMoneda,simboloMoneda) VALUES (1,'GTQ','Quetzal','Q'),(2,'USD','Dolar','$')");
        Sql("INSERT INTO tblestablecimientoventas(idEstablecimiento,codigoEstablecimiento,nombreEstablecimiento,nitEmisor,direccionEstablecimiento) VALUES (1,'PRUEBA','Emisor de prueba','PRUEBA','Pruebas')");
        Sql("INSERT INTO tblcliente(idCliente,codigoCliente,tipoIdentificacion,nombreCliente,idMonedaCredito,diasCredito) VALUES (1,'C1','CF','Cliente uno de prueba',1,30),(2,'C2','CF','Cliente dos de prueba',1,0)");
        Sql("INSERT INTO tblvendedor(idVendedor,codigoVendedor,idEmpleado) VALUES (1,'V1',1)");
        Sql("INSERT INTO tblproductoventas(idProducto,codigoProducto,nombreProducto,tipoItem,unidadMedida,porcentajeImpuesto) VALUES (1,'P1','Producto prueba','B','Unidad',12)");
        Sql("INSERT INTO tbllistaprecios(idListaPrecio,codigoListaPrecio,nombreListaPrecio,idMoneda,vigenteDesde) VALUES (1,'PRUEBA','Lista prueba',1,'2000-01-01')");
        Sql("INSERT INTO tbllistapreciosdetalle(idListaPrecio,idProducto,precioUnitario) VALUES (1,1,100)");
        Sql("INSERT INTO tblclientelistaprecios(idCliente,idMoneda,idListaPrecio) VALUES (1,1,1)");
    }
    private static void Probar()
    {
        var Mov = new ClsMovimientosRepositorio(); var Cot = new ClsCotizacionesRepositorio(); var Cie = new ClsCierreRepositorio();
        DateTime Enero = new DateTime(DateTime.Today.Year - 1, 1, 1);
        Catalogos();
        Comprobar(Mov.Catalogo("Clientes").Rows.Count == 2, "catalogos reales mediante ODBC");
        string Factura = Mov.RegistrarDocumento(1, 1, 1, 1, "F-UNO", 0, Enero.AddDays(4), Enero.AddDays(30), 1000m, "", "Factura interna de prueba");
        int F1 = Convert.ToInt32(Sql("SELECT idFactura FROM tblfactura WHERE numeroFactura='F-UNO'"));
        Comprobar(Saldo(F1) == 1000m, "factura y cargo se registran juntos");
        Mov.RegistrarDocumento(1, 1, 1, 0, "", 1, Enero.AddDays(5), Enero.AddDays(30), 100m, "CXC-F-UNO", "Nota de credito de prueba");
        Comprobar(Saldo(F1) == 900m, "nota de credito reduce deuda");
        Rechaza(() => Mov.RegistrarDocumento(1, 1, 1, 1, Factura, 0, Enero.AddDays(4), Enero.AddDays(30), 50m, "", "Duplicado"), "factura duplicada rechazada");
        Comprobar(Convert.ToInt32(Sql("SELECT COUNT(*) FROM tblfactura")) == 1, "duplicado no deja encabezados huerfanos");
        string Recibo = Mov.RegistrarRecibo(1, 1, 1, "", Enero.AddDays(9), 250m, 1m, "EFECTIVO", "", "Prueba", Aplicaciones(F1, 250m));
        int Pago = Convert.ToInt32(Sql("SELECT idPago FROM tblpago WHERE numeroRecibo='" + Recibo + "'"));
        Comprobar(Saldo(F1) == 650m, "pago parcial y recibo persistidos");
        Rechaza(() => Mov.RegistrarRecibo(1, 1, 1, "R-MAL", Enero.AddDays(10), 700m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 700m)), "sobrepago rechazado con saldo bloqueado");
        Rechaza(() => Mov.RegistrarRecibo(1, 1, 1, "R-MAL", Enero.AddDays(10), 300m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 250m)), "recibo incompletamente aplicado rechazado");
        Rechaza(() => Mov.RegistrarRecibo(2, 1, 1, "R-MAL", Enero.AddDays(10), 50m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 50m)), "factura de otro cliente rechazada");
        Rechaza(() => Mov.RegistrarRecibo(1, 2, 1, "R-MAL", Enero.AddDays(10), 50m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 50m)), "mezcla de monedas rechazada");
        Rechaza(() => Mov.RegistrarRecibo(1, 1, 1, Recibo, Enero.AddDays(10), 50m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 50m)), "numero de recibo unico");
        Comprobar(Convert.ToInt32(Sql("SELECT COUNT(*) FROM tblpago")) == 1 && Saldo(F1) == 650m, "fallos revierten pago y libro sin persistencia parcial");
        Mov.RegistrarDocumento(1, 1, 1, 1, "F-DOS", 0, Enero.AddDays(4), Enero.AddDays(30), 500m, "", "Segunda factura");
        int F2 = Convert.ToInt32(Sql("SELECT idFactura FROM tblfactura WHERE numeroFactura='F-DOS'"));
        string Multiple = Mov.RegistrarRecibo(1, 1, 1, "", Enero.AddDays(11), 500m, 1m, "TRANSFERENCIA", "REF-TEST", "", Aplicaciones(F1, 300m, F2, 200m));
        int PagoMultiple = Convert.ToInt32(Sql("SELECT idPago FROM tblpago WHERE numeroRecibo='" + Multiple + "'"));
        Comprobar(Saldo(F1) == 350m && Saldo(F2) == 300m, "un recibo se aplica a varias facturas");
        var D = new DataTable(); D.Columns.Add("idPago", typeof(int)); D.Columns.Add("Importe", typeof(decimal)); D.Rows.Add(Pago, 150m);
        string Deposito = Mov.PrepararDeposito(1, 1, Enero.AddDays(14), "", "EMP-TEST", "BANCO-TEST", "CUENTA-TEST", "CONCEPTO-TEST", "", D);
        D.Rows[0]["Importe"] = 100m; Mov.PrepararDeposito(1, 1, Enero.AddDays(15), "", "EMP-TEST", "BANCO-TEST", "CUENTA-TEST", "CONCEPTO-TEST", "", D);
        Comprobar(Convert.ToDecimal(Sql("SELECT SUM(importe) FROM tbldepositoventasdetalle WHERE idPago=" + Pago)) == 250m, "recibo distribuido entre dos depositos");
        D.Rows[0]["Importe"] = 1m;
        Rechaza(() => Mov.PrepararDeposito(1, 1, Enero.AddDays(16), "", "E", "B", "C", "D", "", D), "deposito no supera disponible");
        Comprobar(Mov.ConsultarRecibos("", Deposito).Rows.Count == 1, "consulta integrada por referencia de deposito");
        Rechaza(() => Mov.AnularRecibo(Pago, Enero.AddDays(16), 1, "Prueba"), "recibo depositado no se anula unilateralmente");
        Mov.AnularRecibo(PagoMultiple, Enero.AddDays(17), 1, "Reverso de prueba");
        Comprobar(Saldo(F1) == 650m && Saldo(F2) == 500m, "anulacion crea reversos sin borrar el historial");
        var Carreras = new[] { "RACE-A", "RACE-B" }.Select(N => Task.Run(() =>
        {
            try { Mov.RegistrarRecibo(1, 1, 1, N, Enero.AddDays(20), 400m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 400m)); return 1; }
            catch (InvalidOperationException) { return 0; }
        })).ToArray();
        Task.WaitAll(Carreras);
        Comprobar(Carreras.Sum(T => T.Result) == 1 && Saldo(F1) == 250m, "dos pagos concurrentes no consumen el mismo saldo");
        DataTable Revision = Cie.Revisar(1, "MENSUAL", Enero, Enero.AddMonths(1).AddDays(-1));
        Comprobar((decimal)Revision.Rows[0]["Actual"] == 750m, "seis saldos de cierre calculados desde el libro");
        string Cierre = Cie.Confirmar(1, 1, "MENSUAL", Enero, Enero.AddMonths(1).AddDays(-1), true);
        Comprobar(Cie.Historial(1, 1, Enero.ToString("yyyy-MM")).Rows.Count == 1, "historico persistido por cliente y periodo");
        Rechaza(() => Cie.Confirmar(1, 1, "MENSUAL", Enero, Enero.AddMonths(1).AddDays(-1), true), "cierre duplicado rechazado");
        Rechaza(() => Cie.Confirmar(1, 1, "MENSUAL", Enero.AddMonths(2), Enero.AddMonths(3).AddDays(-1), true), "meses no consecutivos rechazados");
        Rechaza(() => Mov.RegistrarDocumento(1, 1, 1, 1, "ATRAS", 0, Enero.AddDays(10), Enero.AddDays(30), 10m, "", "Atrasado"), "nuevos documentos no se fechan en periodo cerrado");
        Rechaza(() => Mov.RegistrarRecibo(1, 1, 1, "ATRAS", Enero.AddDays(25), 10m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 10m)), "pagos retroactivos bloqueados");
        Rechaza(() => Sql("INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro) VALUES (" + F1 + ",'" + Enero.ToString("yyyy-MM-dd") + "','PAGO','Prueba',0,1,1)"), "la base bloquea tambien inserciones directas en periodo cerrado");
        Rechaza(() => Sql("UPDATE tblmovimientocxc SET cargo=1 WHERE idMovimiento=1"), "libro inmutable en base de datos");
        Rechaza(() => Sql("UPDATE tblcierresaldocliente SET saldoActual=0"), "fotografia historica inmutable");
        Rechaza(() => Sql("UPDATE tblfactura SET total=1 WHERE idFactura=" + F1), "cabecera de factura cerrada congelada");
        Rechaza(() => Sql("UPDATE tblcierremovimientos SET estadoCierre='REABIERTO'"), "no se reabre el cierre sin versionar el historico");
        Rechaza(() => Sql("UPDATE tblcierremovimientosdetalle SET saldoFinal=1"), "historico por factura inmutable");
        Mov.RegistrarRecibo(1, 1, 1, "FEB", Enero.AddMonths(1).AddDays(3), 50m, 1m, "EFECTIVO", "", "", Aplicaciones(F1, 50m));
        Comprobar(Saldo(F1) == 200m && (decimal)Cie.Historial(1, 1, Enero.ToString("yyyy-MM")).Rows[0]["Actual"] == 750m, "pago posterior permitido sin alterar fotografia anterior");
        Rechaza(() => Cie.Confirmar(1, 1, "ANUAL", Enero, Enero.AddYears(1).AddDays(-1), true), "anual exige doce cierres mensuales");
        for (int Mes = 1; Mes < 12; Mes++) Cie.Confirmar(1, 1, "MENSUAL", Enero.AddMonths(Mes), Enero.AddMonths(Mes + 1).AddDays(-1), true);
        Cie.Confirmar(1, 1, "ANUAL", Enero, Enero.AddYears(1).AddDays(-1), true);
        Comprobar(Cie.Historial(1, 1, Enero.Year.ToString()).Rows.Count == 13, "cierre anual y doce mensuales conservados");
        int IdCierre = Convert.ToInt32(Sql("SELECT idCierre FROM tblcierremovimientos WHERE numeroCierre='" + Cierre + "'"));
        DataTable Partida = Cie.Partida(IdCierre);
        Comprobar(Partida.AsEnumerable().Sum(F => (decimal)F["Debe"]) == Partida.AsEnumerable().Sum(F => (decimal)F["Haber"]), "partida resumen balanceada aunque existan deudas pendientes");
        Comprobar(Partida.AsEnumerable().Any(F => ((string)F["Cuenta"]).StartsWith("PENDIENTE")), "no se inventan cuentas contables");
        Rechaza(() => Cie.PrepararTraslado(IdCierre, 1), "traslado exige configurar cuentas contables");
        Sql("INSERT INTO tblcuentacontablecxc(idMoneda,tipoMovimiento,cuentaClientes,cuentaContrapartida) SELECT DISTINCT 1,tipoMovimiento,'CUENTA-PRUEBA-CXC',CONCAT('PRUEBA-',tipoMovimiento) FROM tblmovimientocxc");
        var Enlace = new CapaControlador_CierreMovimientos.ClsCierreControlador();
        Rechaza(() => Enlace.Trasladar(IdCierre, 1, null), "no se simula un enlace contable ausente");
        Rechaza(() => Enlace.Trasladar(IdCierre, 1, (Clave, Moneda, Fecha, Filas) => { throw new InvalidOperationException("Fallo simulado del receptor de prueba"); }), "fallo del enlace conserva la partida pendiente para reintento");
        Comprobar((string)Cie.EstadoTraslado(IdCierre).Rows[0]["estado"] == "PENDIENTE", "no hay confirmacion contable falsa");
        Sql("UPDATE tblcuentacontablecxc SET cuentaContrapartida='OTRA-CUENTA-PRUEBA'");
        int Llamadas = 0;
        string Poliza = Enlace.Trasladar(IdCierre, 1, (Clave, Moneda, Fecha, Filas) =>
        {
            Comprobar(Clave == "VENTAS-CXC-CIERRE-" + IdCierre && !Filas.AsEnumerable().Any(F => (string)F["Cuenta"] == "OTRA-CUENTA-PRUEBA"), "reintento conserva clave y partida originales");
            Llamadas++; return "POLIZA-RECEPTOR-PRUEBA";
        });
        Comprobar(Poliza == "POLIZA-RECEPTOR-PRUEBA" && (string)Cie.EstadoTraslado(IdCierre).Rows[0]["estado"] == "CONFIRMADO", "se persiste solo la confirmacion devuelta por el receptor");
        Enlace.Trasladar(IdCierre, 1, (Clave, Moneda, Fecha, Filas) => { Llamadas++; return "NO-DEBE-LLAMARSE"; });
        Comprobar(Llamadas == 1, "traslado confirmado no se envia otra vez");
        Rechaza(() => Cie.ConfirmarTraslado(IdCierre, 1, "OTRA-REFERENCIA"), "referencia contable confirmada es estable");
        Mov.RegistrarDocumento(1, 2, 1, 1, "USD-UNO", 0, Enero.AddDays(4), Enero.AddDays(30), 1000m, "", "Prueba de moneda separada");
        int Usd = Convert.ToInt32(Sql("SELECT idFactura FROM tblfactura WHERE numeroFactura='USD-UNO'"));
        Mov.RegistrarRecibo(1, 2, 1, "USD-REC", Enero.AddDays(14), 800m, 1m, "EFECTIVO", "", "", Aplicaciones(Usd, 800m));
        Mov.RegistrarDocumento(1, 2, 1, 0, "USD-NOTA", 2, Enero.AddDays(19), Enero.AddDays(30), 600m, "CXC-USD-UNO", "Debito posterior");
        Rechaza(() => Mov.RegistrarRecibo(1, 2, 1, "RETRO", Enero.AddDays(9), 700m, 1m, "EFECTIVO", "", "", Aplicaciones(Usd, 700m)), "retroactivo no crea saldos negativos en fechas intermedias");
        Mov.RegistrarDocumento(1, 2, 1, 1, "CXC-00000001", 0, Enero.AddDays(29), Enero.AddDays(30), 1m, "", "Numero manual reservado");
        string Auto = Mov.RegistrarDocumento(1, 2, 1, 1, "", 0, Enero.AddDays(29), Enero.AddDays(30), 1m, "", "Correlativo automatico");
        Comprobar(Auto == "CXC-00000002", "correlativo automatico salta numeros introducidos manualmente");
        Sql("UPDATE tblcliente SET nombreCliente='Cliente Jose con acento: José' WHERE idCliente=1");
        Comprobar(Convert.ToString(Mov.Catalogo("Clientes").AsEnumerable().Single(F => Convert.ToInt32(F["Id"]) == 1)["Nombre"]).Contains("José"), "ODBC conserva caracteres de nombres en espanol");
        Comprobar((string)Cie.Historial(1, 1, Enero.ToString("yyyy-MM")).Rows[0]["Nombre"] == "Cliente uno de prueba", "cambiar el catalogo no altera nombres historicos del cierre");
        Sql("INSERT INTO tblmonedaventas(idMoneda,codigoMoneda,nombreMoneda,simboloMoneda) VALUES (3,'EUR','Euro','EUR')");
        DateTime Julio = Enero.AddMonths(6);
        Mov.RegistrarDocumento(1, 3, 1, 1, "EUR-UNO", 0, Julio, Julio.AddDays(30), 10m, "", "Inicio de operaciones a mitad de ano");
        for (int Mes = 0; Mes < 6; Mes++) Cie.Confirmar(3, 1, "MENSUAL", Julio.AddMonths(Mes), Julio.AddMonths(Mes + 1).AddDays(-1), true);
        Cie.Confirmar(3, 1, "ANUAL", Enero, Enero.AddYears(1).AddDays(-1), true);
        Comprobar(Cie.Historial(3, 1, Enero.Year.ToString()).Rows.Count == 7, "cierre anual admite el primer ano iniciado a mitad del calendario");
        Rechaza(() => Mov.RegistrarDocumento(1, 1, 1, 1, "DESBORDE", 0, DateTime.Today, DateTime.Today, 10000000000000000m, "", "Prueba de rango"), "importes fuera del rango DECIMAL no se truncan");
        Rechaza(() => Mov.RegistrarRecibo(1, 1, 1, "CAMBIO-MAL", DateTime.Today, 1m, 1.1234567m, "EFECTIVO", "", "", Aplicaciones(F1, 1m)), "tipo de cambio con precision excesiva rechazado");
        string Numero = Cot.Guardar(0, "", 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(30), 1m, "EMITIDA", Lineas());
        DataSet Cotizacion = Cot.Consultar(Numero); int IdCot = Convert.ToInt32(Cotizacion.Tables[0].Rows[0]["idCotizacion"]);
        Comprobar((decimal)Cotizacion.Tables[0].Rows[0]["total"] == 212.8m, "cotizacion calcula descuento e impuesto parametrizado");
        Sql("UPDATE tbllistapreciosdetalle SET precioUnitario=150");
        string Pedido = Cot.Convertir(IdCot, false, 0, 1);
        Comprobar(Convert.ToDecimal(Sql("SELECT precioUnitario FROM tblpedidodetalle WHERE idPedido=(SELECT idPedido FROM tblpedido WHERE numeroPedido='" + Pedido + "')")) == 100m, "conversion conserva precios originales aunque cambie la lista");
        Comprobar(Cot.Convertir(IdCot, false, 0, 1) == Pedido, "reintentar conversion devuelve el mismo pedido");
        Rechaza(() => Cot.Convertir(IdCot, true, 1, 1), "una cotizacion no genera dos destinos diferentes");
        Rechaza(() => Cot.Guardar(IdCot, Numero, 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(30), 1m, "EMITIDA", Lineas(150m)), "cotizacion convertida no se reescribe");
        Rechaza(() => Cot.Guardar(0, "MANIPULADA", 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(30), 1m, "EMITIDA", Lineas(1m)), "modelo verifica los precios de la lista sin confiar en la vista");
        string Borrador = Cot.Guardar(0, "", 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(10), 1m, "BORRADOR", Lineas(150m));
        int IdBorrador = Convert.ToInt32(Cot.Consultar(Borrador).Tables[0].Rows[0]["idCotizacion"]);
        Rechaza(() => Cot.Convertir(IdBorrador, false, 0, 1), "borrador no se convierte antes de emitirse");
        Cot.Guardar(IdBorrador, Borrador, 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(10), 1m, "EMITIDA", Lineas(150m));
        string NuevaFactura = Cot.Convertir(IdBorrador, true, 1, 1);
        Comprobar(NuevaFactura.StartsWith("COT-") && Convert.ToString(Sql("SELECT estadoFactura FROM tblfactura WHERE serieFactura='COT' ORDER BY idFactura DESC LIMIT 1")) == "BORRADOR", "conversion prepara factura sin simular emision fiscal ni inventarios");
        Comprobar(Convert.ToDecimal(Sql("SELECT precioUnitario FROM tblfacturadetalle ORDER BY idFacturaDetalle DESC LIMIT 1")) == 150m, "detalle de factura copiado con trazabilidad al origen");
        string Vencida = Cot.Guardar(0, "", 1, 1, 1, 1, DateTime.Today, DateTime.Today.AddDays(1), 1m, "EMITIDA", Lineas(150m));
        int IdVencida = Convert.ToInt32(Cot.Consultar(Vencida).Tables[0].Rows[0]["idCotizacion"]);
        Sql("UPDATE tblcotizacion SET vigenteHasta=DATE_SUB(CURRENT_DATE(),INTERVAL 1 DAY) WHERE idCotizacion=" + IdVencida);
        Rechaza(() => Cot.Convertir(IdVencida, false, 0, 1), "vencida rechazada en modelo aunque la vista solicite conversion");
        Rechaza(() => Mov.RegistrarDocumento(1, 1, 0, 1, "SIN-USUARIO", 0, DateTime.Today, DateTime.Today, 10m, "", "Prueba"), "responsable de Seguridad obligatorio");
        ProbarFormularios(Numero, Enero);
        Console.WriteLine("TOTAL: " + Correctas + " verificaciones correctas.");
    }
    private static T Control<T>(Form F, string Nombre) where T : class
    { return F.GetType().GetField(Nombre, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(F) as T; }
    private static void Invocar(Form F, string Nombre, params object[] Valores)
    { F.GetType().GetMethod(Nombre, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(F, Valores); }
    private static void Captura(Form F, string Nombre)
    {
        string Carpeta = Environment.GetEnvironmentVariable("VENTAS_CAPTURAS");
        if (string.IsNullOrEmpty(Carpeta)) return;
        Directory.CreateDirectory(Carpeta);
        using (var Imagen = new Bitmap(F.Width, F.Height)) { F.DrawToBitmap(Imagen, new Rectangle(Point.Empty, Imagen.Size)); Imagen.Save(Path.Combine(Carpeta, Nombre + ".png")); }
    }
    private static void Mostrar(Form F)
    { F.StartPosition = FormStartPosition.Manual; F.Location = new Point(-16000, -16000); F.ShowInTaskbar = false; F.Show(); Application.DoEvents(); }
    private static void ProbarFormularios(string Cotizacion, DateTime Enero)
    {
        using (var F = new CapaVista_MovimientosClientes.FrmMovimientosClientesVentas())
        {
            Mostrar(F);
            Control<ComboBox>(F, "VentasCboUsuarioRegistro").SelectedValue = 1;
            Control<ComboBox>(F, "VentasCboCliente").SelectedValue = 1;
            Comprobar(Control<DataGridView>(F, "VentasDgvMovimientos").Rows.Count == 0, "cuenta del periodo actual usa el saldo inicial de periodos anteriores");
            F.VentasMetMostrarRecepcionPagos();
            Comprobar(Control<DataGridView>(F, "VentasDgvDocumentos").Rows.Count == 2, "vista de pagos carga facturas pendientes reales");
            Captura(F, "recepcion-pagos");
            F.VentasMetMostrarConsultaRecibos("", "");
            Comprobar(Control<DataGridView>(F, "VentasDgvConsultaRecibos").Rows.Count >= 4, "vista consulta recibos y aplicaciones guardadas");
            Captura(F, "consulta-recibos"); F.Close();
        }
        using (var F = new CapaVista_Cotizaciones.FrmCotizacionVentas())
        {
            Mostrar(F); Invocar(F, "VentasMetConsultar", Cotizacion);
            Comprobar(Control<DataGridView>(F, "VentasDgvCotProductos").Rows.Count == 1 && !Control<Button>(F, "VentasBtnCotGuardar").Enabled, "vista recupera cotizacion y protege el detalle emitido");
            Captura(F, "cotizacion-consultada"); F.Close();
        }
        using (var F = new CapaVista_CierreMovimientos.FrmCierreMovimientosVentas())
        {
            Mostrar(F); Control<ComboBox>(F, "VentasCboMonedaCierre").SelectedValue = 1;
            Control<NumericUpDown>(F, "VentasNudAnioCierre").Value = Enero.Year;
            Control<ComboBox>(F, "VentasCboMesCierre").SelectedIndex = 0;
            Invocar(F, "VentasMetRevisar");
            Comprobar(Control<DataGridView>(F, "VentasDgvRevisionCierre").Rows.Count == 1 && !Control<Button>(F, "VentasBtnConfirmar").Enabled, "vista muestra el cierre historico y evita volverlo a confirmar");
            Comprobar(Convert.ToDecimal(Control<DataGridView>(F, "VentasDgvRevisionCierre").Rows[0].Cells[2].Value) == 0m && Convert.ToDecimal(Control<DataGridView>(F, "VentasDgvRevisionCierre").Rows[0].Cells[7].Value) == 750m, "cada saldo se muestra en la columna correcta del cierre");
            Captura(F, "cierre-confirmado"); F.Close();
        }
    }
    [STAThread]
    public static int Main()
    {
        try
        {
            string Cadena = Environment.GetEnvironmentVariable("VENTAS_ODBC");
            if (string.IsNullOrWhiteSpace(Cadena)) throw new Exception("Defina VENTAS_ODBC con una base AISLADA de pruebas.");
            using (Conexion = new OdbcConnection(Cadena))
            {
                Conexion.Open(); string Nombre = Convert.ToString(Sql("SELECT DATABASE()"));
                if (!Nombre.StartsWith("dbventas_pruebas_", StringComparison.Ordinal)) throw new Exception("Se rechaza ejecutar fixtures fuera de dbventas_pruebas_*.");
                if (Convert.ToInt32(Sql("SELECT COUNT(*) FROM tblusuario")) != 0) throw new Exception("Las pruebas requieren una base nueva y vacia.");
                Application.EnableVisualStyles();
                Task.Delay(30000).ContinueWith(T => { Console.Error.WriteLine("Tiempo limite de pruebas: se cerro solo este proceso de verificacion."); Environment.Exit(124); });
                Probar();
            }
            return 0;
        }
        catch (Exception Error) { Console.Error.WriteLine(Error); return 1; }
    }
}
