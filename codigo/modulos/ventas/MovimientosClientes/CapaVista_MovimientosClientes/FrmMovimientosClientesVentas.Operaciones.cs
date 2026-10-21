using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_MovimientosClientes;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_MovimientosClientes
{
    public partial class FrmMovimientosClientesVentas
    {
        private readonly ClsMovimientosControlador _Controlador = new ClsMovimientosControlador();
        private bool _Cargando = true;
        private bool _Inicializado;
        private readonly bool[] _Guardados = new bool[5];
        private DataSet _EstadoCuenta;
        private int _UsuarioInicial;
        public int VentasIdUsuario
        {
            get { return V.Id(VentasCboUsuarioRegistro); }
            set { _UsuarioInicial = value; if (VentasCboUsuarioRegistro.DataSource != null) VentasCboUsuarioRegistro.SelectedValue = value; }
        }
        private int Cliente { get { return V.Id(VentasCboCliente); } }
        private int Moneda { get { return V.Id(VentasCboMoneda); } }
        private string CodigoMoneda { get { var F = V.Fila(VentasCboMoneda); return F == null ? "" : Convert.ToString(F["codigoMoneda"]); } }
        private void VentasMetConectarEventos()
        {
            foreach (var Grid in new[] { VentasDgvMovimientos, VentasDgvDocumentos, VentasDgvConsultaRecibos, VentasDgvConsultaAplicaciones }) V.Fechas(Grid);
            Load += (s, e) => V.Ejecutar(this, VentasMetCargar);
            VentasBtnConsultar.Click += (s, e) => V.Ejecutar(this, () => { if (!_Inicializado) VentasMetCargar(); else VentasMetConsultarActual(); });
            VentasBtnGuardar.Click += (s, e) => V.Ejecutar(this, VentasMetGuardar);
            VentasBtnIngresar.Click += (s, e) => V.Ejecutar(this, VentasMetNuevo);
            VentasBtnCancelar.Click += (s, e) => V.Ejecutar(this, VentasMetNuevo);
            VentasBtnCerrar.Click += (s, e) => Close();
            VentasBtnImprimir.Click += (s, e) => V.Ejecutar(this, VentasMetImprimir);
            VentasCboCliente.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetClienteCambio); };
            VentasCboMoneda.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetConsultarActual); };
            VentasCboMonedaDeposito.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetDisponibles); };
            VentasCboTipoConsulta.SelectedIndexChanged += (s, e) => { if (!_Cargando && _EstadoCuenta != null) VentasMetMostrarEstado(); };
            VentasCboEstadoConsulta.SelectedIndexChanged += (s, e) => { if (!_Cargando && _EstadoCuenta != null) VentasMetMostrarEstado(); };
            VentasNudMontoPago.ValueChanged += (s, e) => VentasMetTotalesPago();
            foreach (var Grid in new[] { VentasDgvDocumentos, VentasDgvRecibosDeposito })
            {
                Grid.CurrentCellDirtyStateChanged += (s, e) => { if (Grid.IsCurrentCellDirty && Grid.CurrentCell is DataGridViewCheckBoxCell) Grid.CommitEdit(DataGridViewDataErrorContexts.Commit); };
                Grid.DataError += (s, e) => { e.ThrowException = false; V.Informar(this, "Ingrese un importe numerico valido."); };
            }
            VentasDgvDocumentos.CellValueChanged += VentasMetAplicacionCambio;
            VentasDgvRecibosDeposito.CellValueChanged += VentasMetDepositoCambio;
            VentasDgvConsultaRecibos.SelectionChanged += (s, e) => { if (!_Cargando) V.Ejecutar(this, VentasMetAplicacionesConsulta); };
            VentasCboEstadoDocumento.Items.Clear(); VentasCboEstadoDocumento.Items.Add("Aplicado"); VentasCboEstadoDocumento.SelectedIndex = 0; VentasCboEstadoDocumento.Enabled = false;
            VentasCboEstadoDeposito.Items.Clear(); VentasCboEstadoDeposito.Items.Add("Preparacion local"); VentasCboEstadoDeposito.SelectedIndex = 0; VentasCboEstadoDeposito.Enabled = false;
            VentasTxtEstadoEnvio.Text = "Pendiente de enlace con Bancos"; VentasTxtModoActualizacion.Text = "Lo define Bancos; sin afectar saldos bancarios";
            VentasDtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            VentasDtpHasta.Value = DateTime.Today;
            VentasNudTipoCambioPago.Value = 1;
            VentasTtpAcciones.SetToolTip(VentasTxtNumeroDocumento, "Numero interno. Si se deja vacio, se genera un correlativo. No emite DTE.");
            VentasTtpAcciones.SetToolTip(VentasTxtRecibo, "Dejar vacio para generar el numero del recibo al guardar.");
            VentasLblDocumentoRecibo.Text = "Para emitir y aplicar un recibo utilice Recepcion de pagos. La pestana Recibo registra documentos y ajustes internos de CxC.";
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(VentasCboCliente, _Controlador.Catalogo("Clientes"));
                DataTable Monedas = _Controlador.Catalogo("Monedas");
                V.Combo(VentasCboMoneda, Monedas); V.Combo(VentasCboMonedaDeposito, Monedas.Copy());
                V.Combo(VentasCboEstablecimientoDocumento, _Controlador.Catalogo("Establecimientos"));
                VentasCboTipoConsulta.SelectedIndex = 0; VentasCboEstadoConsulta.SelectedIndex = 0;
                _Inicializado = true;
                VentasLblEtapaBancos.Text = VentasCboCliente.Items.Count == 0 ? "Registre clientes, monedas y emisor en los catalogos de Ventas para comenzar." : "Seleccione cliente y moneda. Los recibos se generan al guardar Recepcion de pagos.";
            }
            finally { _Cargando = false; VentasMetRefrescarBotones(); }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpConsultaRecibos) VentasMetConsultarRecibos();
        }
        private void VentasMetClienteCambio()
        {
            DataRow C = V.Fila(VentasCboCliente);
            VentasTxtCondicion.Text = C == null ? "" : C["diasCredito"] + " dias; limite " + Convert.ToDecimal(C["limiteCredito"]).ToString("N2", V.Cultura);
            if (C != null) VentasCboMoneda.SelectedValue = Convert.ToInt32(C["idMonedaCredito"]);
            VentasMetLimpiarResultados();
            if (Cliente > 0 && Moneda > 0) VentasMetConsultarActual();
        }
        private void VentasMetLimpiarResultados()
        {
            _EstadoCuenta = null; VentasDgvMovimientos.Rows.Clear(); VentasDgvDocumentos.Rows.Clear();
            VentasLblValorInicial.Text = VentasLblValorCargos.Text = VentasLblValorAbonos.Text = VentasLblValorFinal.Text = V.Dinero(0, CodigoMoneda);
        }
        private void VentasMetConsultarActual()
        {
            if (_Cargando) return;
            if (VentasTbcMovimientos.SelectedTab == VentasTbpConsultaRecibos) { VentasMetConsultarRecibos(); return; }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpDepositos) { VentasMetDisponibles(); return; }
            if (Cliente <= 0 || Moneda <= 0) { VentasMetLimpiarResultados(); return; }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpCuenta)
            { _EstadoCuenta = _Controlador.EstadoCuenta(Cliente, Moneda, VentasDtpDesde.Value, VentasDtpHasta.Value); VentasMetMostrarEstado(); }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpRecibos) VentasMetPendientes();
        }
        private void VentasMetMostrarEstado()
        {
            VentasDgvMovimientos.Rows.Clear(); DataRow R = _EstadoCuenta.Tables[0].Rows[0];
            decimal Inicial = Convert.ToDecimal(R["Inicial"]), Cargos = Convert.ToDecimal(R["Cargos"]), Abonos = Convert.ToDecimal(R["Abonos"]);
            VentasLblValorInicial.Text = V.Dinero(Inicial, CodigoMoneda); VentasLblValorCargos.Text = V.Dinero(Cargos, CodigoMoneda);
            VentasLblValorAbonos.Text = V.Dinero(Abonos, CodigoMoneda); VentasLblValorFinal.Text = V.Dinero(Inicial + Cargos - Abonos, CodigoMoneda);
            string[] Tipos = { "", "FACTURA", "PAGO", "NOTA_CREDITO", "NOTA_DEBITO", "AJUSTE_POSITIVO", "AJUSTE_NEGATIVO" };
            string Tipo = Tipos[Math.Max(0, VentasCboTipoConsulta.SelectedIndex)];
            foreach (DataRow F in _EstadoCuenta.Tables[1].Rows)
            {
                if (Tipo.Length > 0 && (string)F["Tipo"] != Tipo) continue;
                if (VentasCboEstadoConsulta.SelectedIndex > 0 && !string.Equals((string)F["Estado"], VentasCboEstadoConsulta.Text, StringComparison.OrdinalIgnoreCase)) continue;
                int I = VentasDgvMovimientos.Rows.Add(F["Fecha"], F["Documento"], F["Tipo"], F["Descripcion"], F["Cargo"], F["Abono"], F["Saldo"], F["Estado"]);
                VentasDgvMovimientos.Rows[I].Tag = F;
            }
            VentasLblRegistros.Text = VentasDgvMovimientos.Rows.Count + " movimientos mostrados";
            VentasLblEstadoCierre.Text = "Los totales representan todo el periodo; los filtros solo cambian las filas mostradas.";
        }
        private void VentasMetPendientes()
        {
            _Cargando = true;
            try
            {
                VentasDgvDocumentos.Rows.Clear();
                foreach (DataRow F in _Controlador.Pendientes(Cliente, Moneda).Rows)
                {
                    int I = VentasDgvDocumentos.Rows.Add(false, F["Documento"], F["Fecha"], F["Vencimiento"], F["Moneda"], F["Saldo"], 0m);
                    VentasDgvDocumentos.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            VentasMetTotalesPago();
        }
        private void VentasMetAplicacionCambio(object Sender, DataGridViewCellEventArgs E)
        {
            if (_Cargando || E.RowIndex < 0) return;
            V.Ejecutar(this, () =>
            {
                if (E.ColumnIndex == 0 && Convert.ToBoolean(VentasDgvDocumentos.Rows[E.RowIndex].Cells[0].Value))
                {
                    var F = VentasDgvDocumentos.Rows[E.RowIndex];
                    if (V.Decimal(F.Cells[6].Value) == 0) F.Cells[6].Value = VentasNudMontoPago.Value == 0 ? V.Decimal(F.Cells[5].Value) : Math.Min(V.Decimal(F.Cells[5].Value), Math.Max(0, VentasNudMontoPago.Value - VentasMetSumar(VentasDgvDocumentos, 6)));
                }
                VentasMetTotalesPago();
            });
        }
        private static decimal VentasMetSumar(DataGridView Grid, int Columna)
        { return Grid.Rows.Cast<DataGridViewRow>().Where(F => F.Cells[0].Value is bool && (bool)F.Cells[0].Value).Sum(F => V.Decimal(F.Cells[Columna].Value)); }
        private void VentasMetTotalesPago()
        {
            if (_Cargando) return;
            decimal Aplicado = VentasMetSumar(VentasDgvDocumentos, 6);
            VentasLblValorRecibido.Text = V.Dinero(VentasNudMontoPago.Value, CodigoMoneda);
            VentasLblValorAplicado.Text = V.Dinero(Aplicado, CodigoMoneda);
            VentasLblValorDisponible.Text = V.Dinero(VentasNudMontoPago.Value - Aplicado, CodigoMoneda);
        }
        private static DataTable VentasMetSeleccionados(DataGridView Grid, string Id, int Importe)
        {
            if (!Grid.EndEdit()) throw new InvalidOperationException("Corrija la celda en edicion.");
            var Tabla = new DataTable(); Tabla.Columns.Add(Id, typeof(int)); Tabla.Columns.Add("Importe", typeof(decimal));
            foreach (DataGridViewRow F in Grid.Rows)
                if (F.Cells[0].Value is bool && (bool)F.Cells[0].Value && F.Tag is DataRow)
                    Tabla.Rows.Add(((DataRow)F.Tag)[Id], V.Decimal(F.Cells[Importe].Value));
            return Tabla;
        }
        private void VentasMetGuardar()
        {
            int Tab = VentasTbcMovimientos.SelectedIndex;
            if (_Guardados[Tab]) throw new InvalidOperationException("El registro ya se guardo. Use Ingresar para comenzar otro.");
            string Numero;
            if (Tab == 1)
            {
                Numero = _Controlador.RegistrarDocumento(Cliente, Moneda, VentasIdUsuario, V.Id(VentasCboEstablecimientoDocumento), VentasTxtNumeroDocumento.Text, VentasCboTipoDocumento.SelectedIndex, VentasDtpFechaDocumento.Value, VentasDtpVencimientoDocumento.Value, VentasNudImporteDocumento.Value, VentasTxtFacturaReferencia.Text, VentasTxtDescripcionDocumento.Text);
                VentasTxtNumeroDocumento.Text = Numero;
                V.Informar(this, "Documento interno de CxC registrado: " + Numero);
            }
            else if (Tab == 2)
            {
                string[] Formas = { "EFECTIVO", "TRANSFERENCIA", "DEPOSITO", "CHEQUE", "TARJETA" };
                if (VentasCboFormaPago.SelectedIndex < 0) throw new InvalidOperationException("Seleccione la forma de pago.");
                Numero = _Controlador.RegistrarRecibo(Cliente, Moneda, VentasIdUsuario, VentasTxtRecibo.Text, VentasDtpFechaPago.Value, VentasNudMontoPago.Value, VentasNudTipoCambioPago.Value, Formas[VentasCboFormaPago.SelectedIndex], VentasTxtReferenciaPago.Text, VentasTxtObservacionesPago.Text, VentasMetSeleccionados(VentasDgvDocumentos, "idFactura", 6));
                VentasTxtRecibo.Text = Numero; VentasMetPendientes();
                V.Informar(this, "Recibo generado y aplicado correctamente: " + Numero);
            }
            else if (Tab == 3)
            {
                Numero = _Controlador.PrepararDeposito(V.Id(VentasCboMonedaDeposito), VentasIdUsuario, VentasDtpFechaDeposito.Value, VentasTxtCodigoDeposito.Text, VentasMetReferencia(VentasCboEmpresaDeposito), VentasMetReferencia(VentasCboBancoDestino), VentasMetReferencia(VentasCboCuentaDestino), VentasMetReferencia(VentasCboConceptoBancario), VentasTxtReferenciaDeposito.Text, VentasMetSeleccionados(VentasDgvRecibosDeposito, "idPago", 7));
                VentasTxtCodigoDeposito.Text = Numero; VentasMetDisponibles();
                V.Informar(this, "Deposito preparado localmente: " + Numero + ". El registro en Bancos requiere su enlace.");
            }
            else throw new InvalidOperationException("Seleccione una pestana de captura.");
            _Guardados[Tab] = true; VentasMetRefrescarBotones();
        }
        private void VentasMetNuevo()
        {
            if (VentasTbcMovimientos.SelectedIndex == 0) VentasTbcMovimientos.SelectedTab = VentasTbpDocumento;
            int Tab = VentasTbcMovimientos.SelectedIndex; _Guardados[Tab] = false;
            if (Tab == 1) { VentasTxtNumeroDocumento.Clear(); VentasTxtFacturaReferencia.Clear(); VentasTxtDescripcionDocumento.Clear(); VentasNudImporteDocumento.Value = 0; }
            if (Tab == 2) { VentasTxtRecibo.Clear(); VentasTxtReferenciaPago.Clear(); VentasTxtObservacionesPago.Clear(); VentasNudMontoPago.Value = 0; VentasMetPendientes(); }
            if (Tab == 3) { VentasTxtCodigoDeposito.Clear(); VentasTxtReferenciaDeposito.Clear(); VentasMetDisponibles(); }
            VentasMetRefrescarBotones();
        }
        private void VentasMetRefrescarBotones()
        {
            int Tab = VentasTbcMovimientos.SelectedIndex;
            VentasBtnGuardar.Enabled = _Inicializado && Tab > 0 && Tab < 4 && !_Guardados[Tab];
            VentasBtnIngresar.Enabled = _Inicializado && Tab < 4; VentasBtnCancelar.Enabled = VentasBtnIngresar.Enabled;
        }
        public void VentasMetConfigurarCatalogosBancos(DataTable Empresas, DataTable Bancos, DataTable Cuentas, DataTable Conceptos, string Modo)
        {
            // Tablas con Id y Nombre suministradas por el modulo dueno de esos catalogos.
            V.Combo(VentasCboEmpresaDeposito, Empresas); V.Combo(VentasCboBancoDestino, Bancos);
            V.Combo(VentasCboCuentaDestino, Cuentas); V.Combo(VentasCboConceptoBancario, Conceptos);
            VentasTxtModoActualizacion.Text = Modo;
        }
        private static string VentasMetReferencia(ComboBox Combo)
        { return Combo.SelectedValue == null ? "" : Convert.ToString(Combo.SelectedValue); }
        private void VentasMetDisponibles()
        {
            _Cargando = true;
            try
            {
                VentasDgvRecibosDeposito.Rows.Clear();
                foreach (DataRow F in _Controlador.RecibosDisponibles(V.Id(VentasCboMonedaDeposito)).Rows)
                {
                    int I = VentasDgvRecibosDeposito.Rows.Add(false, F["Recibo"], F["Cliente"], F["Moneda"], F["Monto"], F["Depositado"], F["Disponible"], 0m);
                    VentasDgvRecibosDeposito.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            VentasTxtTotalDeposito.Text = "0.00";
        }
        private void VentasMetDepositoCambio(object Sender, DataGridViewCellEventArgs E)
        {
            if (_Cargando || E.RowIndex < 0) return;
            V.Ejecutar(this, () =>
            {
                var F = VentasDgvRecibosDeposito.Rows[E.RowIndex];
                if (E.ColumnIndex == 0 && Convert.ToBoolean(F.Cells[0].Value) && V.Decimal(F.Cells[7].Value) == 0) F.Cells[7].Value = F.Cells[6].Value;
                VentasTxtTotalDeposito.Text = VentasMetSumar(VentasDgvRecibosDeposito, 7).ToString("N2", V.Cultura);
            });
        }
        private void VentasMetConsultarRecibos()
        {
            _Cargando = true;
            try
            {
                VentasDgvConsultaRecibos.Rows.Clear(); VentasDgvConsultaAplicaciones.Rows.Clear(); VentasTxtMovimientoBancario.Clear();
                foreach (DataRow F in _Controlador.ConsultarRecibos(VentasTxtConsultaRecibo.Text.Trim(), VentasTxtConsultaDeposito.Text.Trim()).Rows)
                {
                    int I = VentasDgvConsultaRecibos.Rows.Add(F["Recibo"], F["Fecha"], F["Cliente"], F["Moneda"], F["Monto"], F["Estado"], F["Deposito"], F["Depositado"]);
                    VentasDgvConsultaRecibos.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            VentasMetAplicacionesConsulta();
        }
        private void VentasMetAplicacionesConsulta()
        {
            DataRow F = VentasDgvConsultaRecibos.CurrentRow == null ? null : VentasDgvConsultaRecibos.CurrentRow.Tag as DataRow;
            VentasDgvConsultaAplicaciones.Rows.Clear(); if (F == null) return;
            VentasTxtMovimientoBancario.Text = Convert.ToString(F["MovimientoBancario"]);
            foreach (DataRow A in _Controlador.Aplicaciones(Convert.ToInt32(F["idPago"])).Rows) VentasDgvConsultaAplicaciones.Rows.Add(A["Factura"], A["Fecha"], A["Importe"], A["Estado"]);
        }
        private void VentasMetImprimir()
        {
            if (VentasTbcMovimientos.SelectedTab == VentasTbpCuenta)
            { V.Imprimir(this, "Estado de cuenta", VentasCboCliente.Text + " / " + CodigoMoneda, _EstadoCuenta == null ? null : _EstadoCuenta.Tables[1]); return; }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpDocumento)
            { V.Imprimir(this, "Documento interno de CxC", CodigoMoneda, _Controlador.Documento(Cliente, Moneda, V.Id(VentasCboEstablecimientoDocumento), VentasTxtNumeroDocumento.Text.Trim(), VentasCboTipoDocumento.SelectedIndex == 0)); return; }
            if (VentasTbcMovimientos.SelectedTab == VentasTbpDepositos)
            {
                DataSet D = _Controlador.Deposito(VentasTxtCodigoDeposito.Text.Trim()); DataRow H = D.Tables[0].Rows[0];
                V.Imprimir(this, "Preparacion local " + H["codigoDeposito"], "Estado: " + H["estadoDeposito"] + " / Referencia de cuenta: " + H["cuentaReferencia"] + " / Total: " + H["total"], D.Tables[1]); return;
            }
            bool Recepcion = VentasTbcMovimientos.SelectedTab == VentasTbpRecibos;
            DataTable Recibos = _Controlador.ConsultarRecibos(Recepcion ? VentasTxtRecibo.Text.Trim() : VentasTxtConsultaRecibo.Text.Trim(), Recepcion ? "" : VentasTxtConsultaDeposito.Text.Trim());
            if (Recibos.Rows.Count != 1) throw new InvalidOperationException("Consulte un solo recibo para imprimir su comprobante.");
            DataRow R = Recibos.Rows[0];
            V.Imprimir(this, "Recibo " + R["Recibo"], R["Cliente"] + " / " + R["Moneda"] + " / Total: " + Convert.ToDecimal(R["Monto"]).ToString("N2", V.Cultura) + " / " + R["Estado"], _Controlador.Aplicaciones(Convert.ToInt32(R["idPago"])));
        }
    }
}
