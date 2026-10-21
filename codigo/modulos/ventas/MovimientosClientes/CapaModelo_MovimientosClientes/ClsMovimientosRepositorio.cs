// Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
using System;
using System.Data;
using System.Linq;
using VentasDatos;

namespace CapaModelo_MovimientosClientes
{
    public sealed class ClsMovimientosRepositorio
    {
        public DataTable Catalogo(string Nombre) { using (var Db = new ClsDatosVentas()) return Db.Catalogo(Nombre); }
        private const string FacturaNombre = "CONCAT(f.serieFactura,'-',f.numeroFactura)";

        public DataSet EstadoCuenta(int Cliente, int Moneda, DateTime Desde, DateTime Hasta)
        {
            ClsDatosVentas.Exigir(Cliente > 0 && Moneda > 0, "Seleccione cliente y moneda.");
            ClsDatosVentas.Exigir(Desde.Date <= Hasta.Date, "La fecha inicial supera la fecha final.");
            using (var Db = new ClsDatosVentas())
            {
                var Datos = new DataSet();
                Datos.Tables.Add(Db.Tabla("SELECT COALESCE(SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END),0) Inicial," +
                    "COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END),0) Cargos," +
                    "COALESCE(SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END),0) Abonos " +
                    "FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura WHERE f.idCliente=? AND f.idMoneda=? AND m.fechaMovimiento<=?",
                    Desde.Date, Desde.Date, Desde.Date, Cliente, Moneda, Hasta.Date));
                DataTable Libro = Db.Tabla("SELECT m.idMovimiento,m.fechaMovimiento Fecha," + FacturaNombre + " Documento,m.tipoMovimiento Tipo,m.descripcionMovimiento Descripcion,m.cargo Cargo,m.abono Abono," +
                    "CASE WHEN c.fechaUltimoCierre IS NOT NULL AND m.fechaMovimiento<=c.fechaUltimoCierre THEN 'CERRADO' ELSE 'APLICADO' END Estado " +
                    "FROM tblmovimientocxc m JOIN tblfactura f ON f.idFactura=m.idFactura LEFT JOIN tblcontrolperiodocxc c ON c.idMoneda=f.idMoneda " +
                    "WHERE f.idCliente=? AND f.idMoneda=? AND m.fechaMovimiento BETWEEN ? AND ? ORDER BY m.fechaMovimiento,m.idMovimiento", Cliente, Moneda, Desde.Date, Hasta.Date);
                Libro.Columns.Add("Saldo", typeof(decimal));
                decimal Saldo = ClsDatosVentas.Decimal(Datos.Tables[0].Rows[0]["Inicial"]);
                foreach (DataRow Fila in Libro.Rows) { Saldo += (decimal)Fila["Cargo"] - (decimal)Fila["Abono"]; Fila["Saldo"] = Saldo; }
                Datos.Tables.Add(Libro);
                return Datos;
            }
        }

        public DataTable Pendientes(int Cliente, int Moneda)
        {
            using (var Db = new ClsDatosVentas()) return Db.Tabla(
                "SELECT f.idFactura," + FacturaNombre + " Documento,f.fechaEmision Fecha,f.fechaVencimiento Vencimiento,mo.codigoMoneda Moneda,SUM(m.cargo-m.abono) Saldo " +
                "FROM tblfactura f JOIN tblmovimientocxc m ON m.idFactura=f.idFactura JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda " +
                "WHERE f.idCliente=? AND f.idMoneda=? GROUP BY f.idFactura,f.serieFactura,f.numeroFactura,f.fechaEmision,f.fechaVencimiento,mo.codigoMoneda HAVING SUM(m.cargo-m.abono)>0 ORDER BY f.fechaVencimiento,f.idFactura", Cliente, Moneda);
        }

        private static void Movimiento(ClsDatosVentas Db, int Factura, DateTime Fecha, string Tipo, string Descripcion, decimal Cargo, decimal Abono, int Usuario, int? Aplicacion = null, int? Nota = null, int? Origen = null)
        {
            Db.Ejecutar("INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro,idAplicacionPago,idNota,idMovimientoOrigen) VALUES (?,?,?,?,?,?,?,?,?,?)",
                Factura, Fecha.Date, Tipo, Descripcion, Cargo, Abono, Usuario, Aplicacion, Nota, Origen);
        }

        public string RegistrarDocumento(int Cliente, int Moneda, int Usuario, int Establecimiento, string Numero, int Tipo, DateTime Fecha, DateTime Vencimiento, decimal Importe, string Referencia, string Motivo)
        {
            ClsDatosVentas.Exigir(Tipo >= 0 && Tipo <= 4, "Seleccione el tipo de documento.");
            ClsDatosVentas.Exigir(Importe > 0 && Importe <= ClsDatosVentas.ImporteMaximo && Importe == ClsDatosVentas.Redondear(Importe), "El importe debe ser positivo, estar dentro del rango y tener hasta dos decimales.");
            Numero = ClsDatosVentas.Texto(Numero, 40, "numero de documento", false);
            Motivo = ClsDatosVentas.Texto(Motivo, 255, "descripcion / motivo");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                DataTable Clientes = Db.Tabla("SELECT * FROM tblcliente WHERE idCliente=? AND is_active=1", Cliente);
                ClsDatosVentas.Exigir(Clientes.Rows.Count == 1, "Seleccione un cliente activo.");
                if (Numero.Length == 0) Numero = Db.Numero(Tipo == 0 ? "CXC" : "NOT");
                int Factura;
                if (Tipo == 0)
                {
                    ClsDatosVentas.Exigir(Vencimiento.Date >= Fecha.Date, "El vencimiento es anterior a la emision.");
                    ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblestablecimientoventas WHERE idEstablecimiento=? AND is_active=1", Establecimiento)) == 1, "Seleccione un establecimiento emisor activo.");
                    DataRow C = Clientes.Rows[0];
                    Db.Ejecutar("INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idMoneda,fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,subtotal,total,estadoFactura,observaciones,idUsuarioRegistro) " +
                        "VALUES ('CXC',?,?,?,?,?,?,'INTERNO_CXC',?,?,?,?,?,?,?,?,'EMITIDA',?,?)", Numero, Establecimiento, Cliente, Moneda, Fecha.Date, Vencimiento.Date,
                        Vencimiento.Date == Fecha.Date ? "CONTADO" : "CREDITO", (Vencimiento.Date - Fecha.Date).Days,
                        C["tipoIdentificacion"], C["numeroIdentificacion"] == DBNull.Value ? "CF" : C["numeroIdentificacion"], C["nombreCliente"], C["direccionCliente"], Importe, Importe, Motivo, Usuario);
                    Factura = Db.Id();
                    Movimiento(Db, Factura, Fecha, "FACTURA", Motivo, Importe, 0, Usuario);
                }
                else
                {
                    Referencia = ClsDatosVentas.Texto(Referencia, 80, "factura relacionada (serie-numero)");
                    DataTable Facturas = Db.Tabla("SELECT f.idFactura,f.fechaEmision FROM tblfactura f WHERE f.idCliente=? AND f.idMoneda=? AND " + FacturaNombre + "=? AND f.estadoFactura='EMITIDA' FOR UPDATE", Cliente, Moneda, Referencia);
                    ClsDatosVentas.Exigir(Facturas.Rows.Count == 1, "La referencia debe identificar una sola factura emitida del cliente y moneda seleccionados.");
                    Factura = Convert.ToInt32(Facturas.Rows[0]["idFactura"]);
                    ClsDatosVentas.Exigir(Fecha.Date >= Convert.ToDateTime(Facturas.Rows[0]["fechaEmision"]).Date, "El ajuste es anterior a la factura.");
                    bool Abono = Tipo == 1 || Tipo == 4;
                    if (Abono) ClsDatosVentas.Exigir(Importe <= Disponible(Db, Factura, Fecha), "El abono supera el saldo disponible de la factura.");
                    Db.Ejecutar("INSERT INTO tblnotacxc(numeroNota,idFactura,tipoNota,fechaNota,monto,motivo,estadoNota,idUsuarioRegistro) VALUES (?,?,?,?,?,?,'APLICADA',?)", Numero, Factura, Abono ? "CREDITO" : "DEBITO", Fecha.Date, Importe, Motivo, Usuario);
                    int Nota = Db.Id();
                    string[] Tipos = { "FACTURA", "NOTA_CREDITO", "NOTA_DEBITO", "AJUSTE_POSITIVO", "AJUSTE_NEGATIVO" };
                    Movimiento(Db, Factura, Fecha, Tipos[Tipo], Motivo, Abono ? 0 : Importe, Abono ? Importe : 0, Usuario, null, Nota);
                }
                Db.Confirmar(); return Numero;
            }
        }

        private static decimal Saldo(ClsDatosVentas Db, int Factura, DateTime? Fecha)
        {
            return ClsDatosVentas.Decimal(Fecha.HasValue
                ? Db.Escalar("SELECT COALESCE(SUM(cargo-abono),0) FROM tblmovimientocxc WHERE idFactura=? AND fechaMovimiento<=?", Factura, Fecha.Value.Date)
                : Db.Escalar("SELECT COALESCE(SUM(cargo-abono),0) FROM tblmovimientocxc WHERE idFactura=?", Factura));
        }
        private static decimal Disponible(ClsDatosVentas Db, int Factura, DateTime Fecha)
        {
            decimal AlDia = Saldo(Db, Factura, Fecha);
            // Un pago retroactivo tampoco puede dejar negativos los saldos de fechas posteriores.
            object Minimo = Db.Escalar("SELECT MIN(Saldo) FROM (SELECT fechaMovimiento,SUM(cargo-abono) OVER (ORDER BY fechaMovimiento,idMovimiento ROWS UNBOUNDED PRECEDING) Saldo FROM tblmovimientocxc WHERE idFactura=?) s WHERE fechaMovimiento>?", Factura, Fecha.Date);
            return Minimo == null || Minimo == DBNull.Value ? AlDia : Math.Min(AlDia, Convert.ToDecimal(Minimo));
        }

        public string RegistrarRecibo(int Cliente, int Moneda, int Usuario, string Numero, DateTime Fecha, decimal Monto, decimal TipoCambio, string Forma, string Referencia, string Observaciones, DataTable Aplicaciones)
        {
            ClsDatosVentas.Exigir(Monto > 0 && Monto <= ClsDatosVentas.ImporteMaximo && Monto == ClsDatosVentas.Redondear(Monto) && TipoCambio > 0 && TipoCambio <= 999999999999.999999m && TipoCambio == Math.Round(TipoCambio, 6), "Monto o tipo de cambio invalido.");
            ClsDatosVentas.Exigir(new[] { "EFECTIVO", "TRANSFERENCIA", "DEPOSITO", "CHEQUE", "TARJETA" }.Contains(Forma), "Seleccione la forma de pago.");
            Referencia = ClsDatosVentas.Texto(Referencia, 100, "referencia", Forma != "EFECTIVO");
            Numero = ClsDatosVentas.Texto(Numero, 40, "numero de recibo", false);
            Observaciones = ClsDatosVentas.Texto(Observaciones, 500, "observaciones", false);
            ClsDatosVentas.Exigir(Aplicaciones != null && Aplicaciones.Rows.Count > 0, "Seleccione las facturas a pagar.");
            var Filas = Aplicaciones.AsEnumerable().OrderBy(F => Convert.ToInt32(F["idFactura"])).ToList();
            ClsDatosVentas.Exigir(Filas.Select(F => Convert.ToInt32(F["idFactura"])).Distinct().Count() == Filas.Count, "Una factura se repite en las aplicaciones.");
            ClsDatosVentas.Exigir(Filas.Sum(F => Convert.ToDecimal(F["Importe"])) == Monto, "Aplique el monto completo del recibo a las facturas; no se admiten anticipos en esta etapa.");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tblcliente WHERE idCliente=? AND is_active=1", Cliente)) == 1, "Seleccione un cliente activo.");
                if (Numero.Length == 0) Numero = Db.Numero("REC");
                // Validar y bloquear todas las facturas antes de registrar el encabezado.
                foreach (DataRow Fila in Filas)
                {
                    int Factura = Convert.ToInt32(Fila["idFactura"]); decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    DataTable Datos = Db.Tabla("SELECT fechaEmision FROM tblfactura WHERE idFactura=? AND idCliente=? AND idMoneda=? AND estadoFactura='EMITIDA' FOR UPDATE", Factura, Cliente, Moneda);
                    ClsDatosVentas.Exigir(Datos.Rows.Count == 1, "Una factura no pertenece al cliente/moneda o no esta emitida.");
                    ClsDatosVentas.Exigir(Fecha.Date >= Convert.ToDateTime(Datos.Rows[0][0]).Date, "El pago es anterior a una factura.");
                    ClsDatosVentas.Exigir(Importe > 0 && Importe == ClsDatosVentas.Redondear(Importe), "Cada aplicacion debe ser positiva y tener hasta dos decimales.");
                    ClsDatosVentas.Exigir(Importe <= Disponible(Db, Factura, Fecha), "El saldo de una factura cambio o el importe lo supera. Consulte nuevamente.");
                }
                Db.Ejecutar("INSERT INTO tblpago(numeroRecibo,idCliente,idMoneda,fechaPago,montoPago,tipoCambio,formaPago,referenciaPago,estadoPago,observaciones,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,'CONFIRMADO',?,?)", Numero, Cliente, Moneda, Fecha.Date, Monto, TipoCambio, Forma, Referencia, Observaciones, Usuario);
                int Pago = Db.Id();
                foreach (DataRow Fila in Filas)
                {
                    int Factura = Convert.ToInt32(Fila["idFactura"]); decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    Db.Ejecutar("INSERT INTO tblaplicacionpago(idPago,idFactura,fechaAplicacion,montoAplicado,estadoAplicacion,idUsuarioRegistro) VALUES (?,?,?,?,'APLICADA',?)", Pago, Factura, Fecha.Date, Importe, Usuario);
                    Movimiento(Db, Factura, Fecha, "PAGO", "Recibo " + Numero, 0, Importe, Usuario, Db.Id());
                }
                Db.Confirmar(); return Numero;
            }
        }

        public DataTable ConsultarRecibos(string Recibo, string Deposito)
        {
            using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT p.idPago,p.numeroRecibo Recibo,p.fechaPago Fecha,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,p.estadoPago Estado,COALESCE(SUM(dd.importe),0) Depositado," +
                "COALESCE(GROUP_CONCAT(DISTINCT d.codigoDeposito ORDER BY d.codigoDeposito SEPARATOR ', '),'') Deposito,COALESCE(GROUP_CONCAT(DISTINCT d.movimientoBancario SEPARATOR ', '),'') MovimientoBancario " +
                "FROM tblpago p JOIN tblcliente c ON c.idCliente=p.idCliente JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda " +
                "LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago LEFT JOIN tbldepositoventas d ON d.idDeposito=dd.idDeposito " +
                "WHERE (?='' OR p.numeroRecibo=?) AND (?='' OR EXISTS(SELECT 1 FROM tbldepositoventasdetalle x JOIN tbldepositoventas y ON y.idDeposito=x.idDeposito WHERE x.idPago=p.idPago AND y.codigoDeposito=?)) " +
                "GROUP BY p.idPago,p.numeroRecibo,p.fechaPago,c.nombreCliente,mo.codigoMoneda,p.montoPago,p.estadoPago ORDER BY p.fechaPago DESC,p.idPago DESC", Recibo ?? "", Recibo ?? "", Deposito ?? "", Deposito ?? "");
        }
        public DataTable Aplicaciones(int Pago)
        {
            using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT " + FacturaNombre + " Factura,a.fechaAplicacion Fecha,a.montoAplicado Importe,a.estadoAplicacion Estado FROM tblaplicacionpago a JOIN tblfactura f ON f.idFactura=a.idFactura WHERE a.idPago=? ORDER BY a.idAplicacionPago", Pago);
        }
        public DataTable Documento(int Cliente, int Moneda, int Establecimiento, string Numero, bool Factura)
        {
            using (var Db = new ClsDatosVentas())
                return Factura
                    ? Db.Tabla("SELECT CONCAT(serieFactura,'-',numeroFactura) Documento,fechaEmision Fecha,fechaVencimiento Vencimiento,nombreReceptor Cliente,total Importe,estadoFactura Estado,observaciones Descripcion FROM tblfactura WHERE idCliente=? AND idMoneda=? AND idEstablecimiento=? AND serieFactura='CXC' AND numeroFactura=?", Cliente, Moneda, Establecimiento, Numero)
                    : Db.Tabla("SELECT n.numeroNota Documento,n.fechaNota Fecha,n.tipoNota Tipo,f.nombreReceptor Cliente,n.monto Importe,n.estadoNota Estado,n.motivo Descripcion FROM tblnotacxc n JOIN tblfactura f ON f.idFactura=n.idFactura WHERE f.idCliente=? AND f.idMoneda=? AND n.numeroNota=?", Cliente, Moneda, Numero);
        }
        public DataSet Deposito(string Numero)
        {
            using (var Db = new ClsDatosVentas())
            {
                var D = new DataSet();
                D.Tables.Add(Db.Tabla("SELECT * FROM tbldepositoventas WHERE codigoDeposito=?", Numero));
                ClsDatosVentas.Exigir(D.Tables[0].Rows.Count == 1, "Consulte el codigo de un deposito preparado.");
                D.Tables.Add(Db.Tabla("SELECT p.numeroRecibo Recibo,c.nombreCliente Cliente,dd.importe Importe FROM tbldepositoventasdetalle dd JOIN tblpago p ON p.idPago=dd.idPago JOIN tblcliente c ON c.idCliente=p.idCliente WHERE dd.idDeposito=? ORDER BY p.idPago", D.Tables[0].Rows[0]["idDeposito"]));
                return D;
            }
        }
        public DataTable RecibosDisponibles(int Moneda)
        {
            using (var Db = new ClsDatosVentas()) return Db.Tabla("SELECT p.idPago,p.numeroRecibo Recibo,c.nombreCliente Cliente,mo.codigoMoneda Moneda,p.montoPago Monto,COALESCE(SUM(dd.importe),0) Depositado,p.montoPago-COALESCE(SUM(dd.importe),0) Disponible FROM tblpago p " +
                "JOIN tblcliente c ON c.idCliente=p.idCliente JOIN tblmonedaventas mo ON mo.idMoneda=p.idMoneda LEFT JOIN tbldepositoventasdetalle dd ON dd.idPago=p.idPago " +
                "WHERE p.idMoneda=? AND p.estadoPago='CONFIRMADO' GROUP BY p.idPago,p.numeroRecibo,c.nombreCliente,mo.codigoMoneda,p.montoPago HAVING Disponible>0 ORDER BY p.idPago", Moneda);
        }
        public string PrepararDeposito(int Moneda, int Usuario, DateTime Fecha, string Numero, string Empresa, string Banco, string Cuenta, string Concepto, string Referencia, DataTable Recibos)
        {
            Empresa = ClsDatosVentas.Texto(Empresa, 100, "empresa suministrada por Bancos");
            Banco = ClsDatosVentas.Texto(Banco, 100, "banco destino"); Cuenta = ClsDatosVentas.Texto(Cuenta, 100, "cuenta de la empresa");
            Concepto = ClsDatosVentas.Texto(Concepto, 100, "concepto bancario"); Referencia = ClsDatosVentas.Texto(Referencia, 100, "referencia", false);
            Numero = ClsDatosVentas.Texto(Numero, 40, "codigo de deposito", false);
            ClsDatosVentas.Exigir(Recibos != null && Recibos.Rows.Count > 0, "Seleccione recibos para preparar el deposito.");
            var Filas = Recibos.AsEnumerable().OrderBy(F => Convert.ToInt32(F["idPago"])).ToList();
            ClsDatosVentas.Exigir(Filas.Select(F => Convert.ToInt32(F["idPago"])).Distinct().Count() == Filas.Count, "No repita un recibo.");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Fecha);
                decimal Total = 0;
                foreach (DataRow Fila in Filas)
                {
                    DataTable P = Db.Tabla("SELECT montoPago,fechaPago FROM tblpago WHERE idPago=? AND idMoneda=? AND estadoPago='CONFIRMADO' FOR UPDATE", Fila["idPago"], Moneda);
                    ClsDatosVentas.Exigir(P.Rows.Count == 1 && Fecha.Date >= Convert.ToDateTime(P.Rows[0]["fechaPago"]).Date, "Recibo no confirmado, moneda o fecha incorrecta.");
                    decimal Importe = Convert.ToDecimal(Fila["Importe"]);
                    decimal Usado = ClsDatosVentas.Decimal(Db.Escalar("SELECT COALESCE(SUM(importe),0) FROM tbldepositoventasdetalle WHERE idPago=?", Fila["idPago"]));
                    ClsDatosVentas.Exigir(Importe > 0 && Importe == ClsDatosVentas.Redondear(Importe) && Importe <= (decimal)P.Rows[0]["montoPago"] - Usado, "Importe de deposito invalido o superior al disponible."); Total += Importe;
                }
                if (Numero.Length == 0) Numero = Db.Numero("DEP");
                ClsDatosVentas.Exigir(Total <= ClsDatosVentas.ImporteMaximo, "El deposito supera la capacidad del documento.");
                Db.Ejecutar("INSERT INTO tbldepositoventas(codigoDeposito,fechaDeposito,empresaReferencia,bancoReferencia,cuentaReferencia,conceptoReferencia,referenciaDeposito,idMoneda,total,idUsuarioRegistro) VALUES (?,?,?,?,?,?,?,?,?,?)", Numero, Fecha.Date, Empresa, Banco, Cuenta, Concepto, Referencia, Moneda, Total, Usuario);
                int Id = Db.Id();
                foreach (DataRow Fila in Filas) Db.Ejecutar("INSERT INTO tbldepositoventasdetalle(idDeposito,idPago,importe) VALUES (?,?,?)", Id, Fila["idPago"], Fila["Importe"]);
                Db.Confirmar(); return Numero;
            }
        }
        public void AnularRecibo(int Pago, DateTime Fecha, int Usuario, string Motivo)
        {
            Motivo = ClsDatosVentas.Texto(Motivo, 180, "motivo de anulacion");
            using (var Db = new ClsDatosVentas())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                object Moneda = Db.Escalar("SELECT idMoneda FROM tblpago WHERE idPago=?", Pago);
                ClsDatosVentas.Exigir(Moneda != null, "No existe el recibo."); Db.PeriodoAbierto(Convert.ToInt32(Moneda), Fecha);
                DataTable P = Db.Tabla("SELECT * FROM tblpago WHERE idPago=? FOR UPDATE", Pago);
                ClsDatosVentas.Exigir((string)P.Rows[0]["estadoPago"] == "CONFIRMADO" && Fecha.Date >= Convert.ToDateTime(P.Rows[0]["fechaPago"]).Date, "El recibo ya esta anulado o la fecha es anterior al pago.");
                ClsDatosVentas.Exigir(Convert.ToInt32(Db.Escalar("SELECT COUNT(*) FROM tbldepositoventasdetalle WHERE idPago=?", Pago)) == 0, "El recibo pertenece a un deposito; coordine su reverso con Bancos.");
                DataTable Movimientos = Db.Tabla("SELECT m.* FROM tblmovimientocxc m JOIN tblaplicacionpago a ON a.idAplicacionPago=m.idAplicacionPago WHERE a.idPago=? AND a.estadoAplicacion='APLICADA' ORDER BY m.idFactura FOR UPDATE", Pago);
                foreach (DataRow M in Movimientos.Rows) Movimiento(Db, Convert.ToInt32(M["idFactura"]), Fecha, "REVERSO", "Anulacion: " + Motivo, (decimal)M["abono"], (decimal)M["cargo"], Usuario, null, null, Convert.ToInt32(M["idMovimiento"]));
                Db.Ejecutar("UPDATE tblaplicacionpago SET estadoAplicacion='REVERTIDA' WHERE idPago=?", Pago);
                Db.Ejecutar("UPDATE tblpago SET estadoPago='ANULADO' WHERE idPago=?", Pago);
                Db.Confirmar();
            }
        }
    }
}
