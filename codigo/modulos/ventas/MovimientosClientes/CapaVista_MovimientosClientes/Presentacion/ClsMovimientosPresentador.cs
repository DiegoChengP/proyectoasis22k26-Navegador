using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_MovimientosClientes;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_MovimientosClientes
{
    internal sealed partial class ClsMovimientosPresentador
    {
        private readonly FrmMovimientosClientesVentas _Vista;
        internal ClsMovimientosPresentador(FrmMovimientosClientesVentas Vista)
        {
            _Vista = Vista;
            VentasMetConectarEventos();
        }

        private readonly ClsMovimientosControlador _Controlador = new ClsMovimientosControlador();
        private bool _Cargando = true;
        private bool _Inicializado;
        private readonly bool[] _Guardados = new bool[5];
        private DataSet _EstadoCuenta;
        private int _UsuarioInicial;
        public int VentasIdUsuario
        {
            get { return _UsuarioInicial > 0 ? _UsuarioInicial : V.Id(_Vista.VentasCboUsuarioRegistro); }
            set
            {
                _UsuarioInicial = value;
                _Vista.VentasCboUsuarioRegistro.Enabled = value <= 0;
                if (_Vista.VentasCboUsuarioRegistro.DataSource != null) _Vista.VentasCboUsuarioRegistro.SelectedValue = value;
            }
        }
        private int Cliente { get { return V.Id(_Vista.VentasCboCliente); } }
        private int Moneda { get { return V.Id(_Vista.VentasCboMoneda); } }
        private string CodigoMoneda { get { var F = V.Fila(_Vista.VentasCboMoneda); return F == null ? "" : Convert.ToString(F["codigoMoneda"]); } }
        private void VentasMetConectarEventos()
        {
            foreach (var Grid in new[] { _Vista.VentasDgvMovimientos, _Vista.VentasDgvDocumentos, _Vista.VentasDgvConsultaRecibos, _Vista.VentasDgvConsultaAplicaciones }) V.Fechas(Grid);
            _Vista.Load += (s, e) => V.Ejecutar(_Vista, VentasMetCargar);
            _Vista.VentasBtnConsultar.Click += (s, e) => V.Ejecutar(_Vista, () => { if (!_Inicializado) VentasMetCargar(); else VentasMetConsultarCaptura(); });
            _Vista.VentasBtnModificar.Click += (s,e) => V.Ejecutar(_Vista, VentasMetModificar);
            _Vista.VentasBtnEliminar.Click += (s,e) => V.Ejecutar(_Vista, VentasMetAnular);
            _Vista.VentasBtnRefrescar.Click += (s,e) => V.Ejecutar(_Vista, () =>
            {
                if (_Vista.VentasTbcMovimientos.SelectedIndex == 1 && _DocumentoActual != null) VentasMetCargarDocumento(Convert.ToInt32(_DocumentoActual["Id"]),Convert.ToString(_DocumentoActual["Clase"]));
                else if (_Vista.VentasTbcMovimientos.SelectedIndex == 2 && _ReciboActual != null) VentasMetCargarRecibo(Convert.ToInt32(_ReciboActual["idPago"]));
                else VentasMetConsultarActual();
            });
            _Vista.VentasDgvMovimientos.CellDoubleClick += (s,e) => { if(e.RowIndex>=0) V.Ejecutar(_Vista,VentasMetAbrirSeleccionActual); };
            _Vista.VentasDgvConsultaRecibos.CellDoubleClick += (s,e) => { if(e.RowIndex>=0) V.Ejecutar(_Vista,VentasMetAbrirSeleccionActual); };
            foreach(var Combo in new[] { _Vista.VentasCboEmpresaDeposito, _Vista.VentasCboBancoDestino, _Vista.VentasCboCuentaDestino, _Vista.VentasCboConceptoBancario }) Combo.DropDownStyle=ComboBoxStyle.DropDown;
            _Vista.VentasBtnGuardar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetGuardar);
            _Vista.VentasBtnIngresar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetNuevo);
            _Vista.VentasBtnCancelar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetNuevo);
            _Vista.VentasBtnCerrar.Click += (s, e) => _Vista.Close();
            _Vista.VentasBtnImprimir.Click += (s, e) => V.Ejecutar(_Vista, VentasMetImprimir);
            _Vista.VentasCboCliente.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetClienteCambio); };
            _Vista.VentasCboMoneda.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetConsultarActual); };
            _Vista.VentasCboMonedaDeposito.SelectedIndexChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetDisponibles); };
            _Vista.VentasCboTipoConsulta.SelectedIndexChanged += (s, e) => { if (!_Cargando && _EstadoCuenta != null) VentasMetMostrarEstado(); };
            _Vista.VentasCboEstadoConsulta.SelectedIndexChanged += (s, e) => { if (!_Cargando && _EstadoCuenta != null) VentasMetMostrarEstado(); };
            _Vista.VentasNudMontoPago.ValueChanged += (s, e) => VentasMetTotalesPago();
            foreach (var Grid in new[] { _Vista.VentasDgvDocumentos, _Vista.VentasDgvRecibosDeposito })
            {
                Grid.CurrentCellDirtyStateChanged += (s, e) => { if (Grid.IsCurrentCellDirty && Grid.CurrentCell is DataGridViewCheckBoxCell) Grid.CommitEdit(DataGridViewDataErrorContexts.Commit); };
                Grid.DataError += (s, e) => { e.ThrowException = false; V.Informar(_Vista, "Ingrese un importe numerico valido."); };
            }
            _Vista.VentasDgvDocumentos.CellValueChanged += VentasMetAplicacionCambio;
            _Vista.VentasDgvRecibosDeposito.CellValueChanged += VentasMetDepositoCambio;
            _Vista.VentasDgvConsultaRecibos.SelectionChanged += (s, e) => { if (!_Cargando) V.Ejecutar(_Vista, VentasMetAplicacionesConsulta); };
            _Vista.VentasCboEstadoDocumento.Items.Clear(); _Vista.VentasCboEstadoDocumento.Items.Add("Aplicado"); _Vista.VentasCboEstadoDocumento.SelectedIndex = 0; _Vista.VentasCboEstadoDocumento.Enabled = false;
            _Vista.VentasCboEstadoDeposito.Items.Clear(); _Vista.VentasCboEstadoDeposito.Items.Add("Preparacion local"); _Vista.VentasCboEstadoDeposito.SelectedIndex = 0; _Vista.VentasCboEstadoDeposito.Enabled = false;
            _Vista.VentasTxtEstadoEnvio.Text = "Pendiente de enlace con Bancos"; _Vista.VentasTxtModoActualizacion.Text = "Lo define Bancos; sin afectar saldos bancarios";
            _Vista.VentasDtpDesde.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            _Vista.VentasDtpHasta.Value = DateTime.Today;
            _Vista.VentasNudTipoCambioPago.Value = 1;
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasTxtNumeroDocumento, "Numero interno. Si se deja vacio, se genera un correlativo. No emite DTE.");
            _Vista.VentasTtpAcciones.SetToolTip(_Vista.VentasTxtRecibo, "Dejar vacio para generar el numero del recibo al guardar.");
            _Vista.VentasLblDocumentoRecibo.Text = "Para emitir y aplicar un recibo utilice Recepcion de pagos. La pestana Recibo registra documentos y ajustes internos de CxC.";
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(_Vista.VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(_Vista.VentasCboCliente, _Controlador.Catalogo("Clientes"));
                DataTable Monedas = _Controlador.Catalogo("Monedas");
                V.Combo(_Vista.VentasCboMoneda, Monedas); V.Combo(_Vista.VentasCboMonedaDeposito, Monedas.Copy());
                V.Combo(_Vista.VentasCboEstablecimientoDocumento, _Controlador.Catalogo("Establecimientos"));
                _Vista.VentasCboTipoConsulta.SelectedIndex = 0; _Vista.VentasCboEstadoConsulta.SelectedIndex = 0;
                _Inicializado = true;
                _Vista.VentasLblEtapaBancos.Text = _Vista.VentasCboCliente.Items.Count == 0 ? "Registre clientes, monedas y emisor en los catalogos de Ventas para comenzar." : "Seleccione cliente y moneda. Los recibos se generan al guardar Recepcion de pagos.";
            }
            finally { _Cargando = false; VentasMetRefrescarBotones(); }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpConsultaRecibos) VentasMetConsultarRecibos();
        }
        private void VentasMetClienteCambio()
        {
            _DocumentoActual=null; _ReciboActual=null; _EditandoDocumento=_EditandoRecibo=false;
            _Guardados[1]=_Guardados[2]=false;
            _Vista.VentasTxtNumeroDocumento.Clear(); _Vista.VentasTxtRecibo.Clear();
            _Vista.VentasTxtDescripcionDocumento.Clear(); _Vista.VentasTxtObservacionesPago.Clear();
            VentasMetRefrescarBotones();
            DataRow C = V.Fila(_Vista.VentasCboCliente);
            _Vista.VentasTxtCondicion.Text = C == null ? "" : C["diasCredito"] + " dias; limite " + Convert.ToDecimal(C["limiteCredito"]).ToString("N2", V.Cultura);
            if (C != null) _Vista.VentasCboMoneda.SelectedValue = Convert.ToInt32(C["idMonedaCredito"]);
            VentasMetLimpiarResultados();
            if (Cliente > 0 && Moneda > 0) VentasMetConsultarActual();
        }
        private void VentasMetLimpiarResultados()
        {
            _EstadoCuenta = null; _Vista.VentasDgvMovimientos.Rows.Clear(); _Vista.VentasDgvDocumentos.Rows.Clear();
            _Vista.VentasLblValorInicial.Text = _Vista.VentasLblValorCargos.Text = _Vista.VentasLblValorAbonos.Text = _Vista.VentasLblValorFinal.Text = V.Dinero(0, CodigoMoneda);
        }
        private void VentasMetConsultarActual()
        {
            if (_Cargando) return;
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpConsultaRecibos) { VentasMetConsultarRecibos(); return; }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDepositos) { if(!string.IsNullOrWhiteSpace(_Vista.VentasTxtCodigoDeposito.Text)) VentasMetConsultarDeposito(); else VentasMetDisponibles(); return; }
            if (Cliente <= 0 || Moneda <= 0) { VentasMetLimpiarResultados(); return; }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpCuenta)
            { _EstadoCuenta = _Controlador.EstadoCuenta(Cliente, Moneda, _Vista.VentasDtpDesde.Value, _Vista.VentasDtpHasta.Value); VentasMetMostrarEstado(); }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos && _ReciboActual == null) VentasMetPendientes();
        }
        private void VentasMetMostrarEstado()
        {
            _Vista.VentasDgvMovimientos.Rows.Clear(); DataRow R = _EstadoCuenta.Tables[0].Rows[0];
            decimal Inicial = Convert.ToDecimal(R["Inicial"]), Cargos = Convert.ToDecimal(R["Cargos"]), Abonos = Convert.ToDecimal(R["Abonos"]);
            _Vista.VentasLblValorInicial.Text = V.Dinero(Inicial, CodigoMoneda); _Vista.VentasLblValorCargos.Text = V.Dinero(Cargos, CodigoMoneda);
            _Vista.VentasLblValorAbonos.Text = V.Dinero(Abonos, CodigoMoneda); _Vista.VentasLblValorFinal.Text = V.Dinero(Inicial + Cargos - Abonos, CodigoMoneda);
            string[] Tipos = { "", "FACTURA", "PAGO", "NOTA_CREDITO", "NOTA_DEBITO", "AJUSTE_POSITIVO", "AJUSTE_NEGATIVO" };
            string Tipo = Tipos[Math.Max(0, _Vista.VentasCboTipoConsulta.SelectedIndex)];
            foreach (DataRow F in _EstadoCuenta.Tables[1].Rows)
            {
                if (Tipo.Length > 0 && (string)F["Tipo"] != Tipo) continue;
                if (_Vista.VentasCboEstadoConsulta.SelectedIndex > 0 && !string.Equals((string)F["Estado"], _Vista.VentasCboEstadoConsulta.Text, StringComparison.OrdinalIgnoreCase)) continue;
                int I = _Vista.VentasDgvMovimientos.Rows.Add(F["Fecha"], F["Documento"], F["Tipo"], F["Descripcion"], F["Cargo"], F["Abono"], F["Saldo"], F["Estado"]);
                _Vista.VentasDgvMovimientos.Rows[I].Tag = F;
            }
            _Vista.VentasLblRegistros.Text = _Vista.VentasDgvMovimientos.Rows.Count + " movimientos mostrados";
            _Vista.VentasLblEstadoCierre.Text = "Los totales representan todo el periodo; los filtros solo cambian las filas mostradas.";
        }
        private void VentasMetPendientes()
        {
            _Cargando = true;
            try
            {
                _Vista.VentasDgvDocumentos.Rows.Clear();
                foreach (DataRow F in _Controlador.Pendientes(Cliente, Moneda).Rows)
                {
                    int I = _Vista.VentasDgvDocumentos.Rows.Add(false, F["Documento"], F["Fecha"], F["Vencimiento"], F["Moneda"], F["Saldo"], 0m);
                    _Vista.VentasDgvDocumentos.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            VentasMetTotalesPago();
        }
        private void VentasMetAplicacionCambio(object Sender, DataGridViewCellEventArgs E)
        {
            if (_Cargando || E.RowIndex < 0) return;
            V.Ejecutar(_Vista, () =>
            {
                if (E.ColumnIndex == 0 && Convert.ToBoolean(_Vista.VentasDgvDocumentos.Rows[E.RowIndex].Cells[0].Value))
                {
                    var F = _Vista.VentasDgvDocumentos.Rows[E.RowIndex];
                    if (_Controlador.ImporteCelda(F.Cells[6].Value) == 0) F.Cells[6].Value = _Vista.VentasNudMontoPago.Value == 0 ? _Controlador.ImporteCelda(F.Cells[5].Value) : Math.Min(_Controlador.ImporteCelda(F.Cells[5].Value), Math.Max(0, _Vista.VentasNudMontoPago.Value - VentasMetSumar(_Vista.VentasDgvDocumentos, 6)));
                }
                VentasMetTotalesPago();
            });
        }
        private decimal VentasMetSumar(DataGridView Grid, int Columna)
        { return Grid.Rows.Cast<DataGridViewRow>().Where(F => F.Cells[0].Value is bool && (bool)F.Cells[0].Value).Sum(F => _Controlador.ImporteCelda(F.Cells[Columna].Value)); }
        private void VentasMetTotalesPago()
        {
            if (_Cargando) return;
            decimal Aplicado = VentasMetSumar(_Vista.VentasDgvDocumentos, 6);
            _Vista.VentasLblValorRecibido.Text = V.Dinero(_Vista.VentasNudMontoPago.Value, CodigoMoneda);
            _Vista.VentasLblValorAplicado.Text = V.Dinero(Aplicado, CodigoMoneda);
            _Vista.VentasLblValorDisponible.Text = V.Dinero(_Vista.VentasNudMontoPago.Value - Aplicado, CodigoMoneda);
        }
        private DataTable VentasMetSeleccionados(DataGridView Grid, string Id, int Importe)
        {
            _Controlador.ValidarCaptura(!(!Grid.EndEdit()), "Corrija la celda en edicion.");
            var Tabla = new DataTable(); Tabla.Columns.Add(Id, typeof(int)); Tabla.Columns.Add("Importe", typeof(decimal));
            foreach (DataGridViewRow F in Grid.Rows)
                if (F.Cells[0].Value is bool && (bool)F.Cells[0].Value && F.Tag is DataRow)
                    Tabla.Rows.Add(((DataRow)F.Tag)[Id], _Controlador.ImporteCelda(F.Cells[Importe].Value));
            return Tabla;
        }
        private void VentasMetGuardar()
        {
            if (VentasMetGuardarModificacion()) return;
            int Tab = _Vista.VentasTbcMovimientos.SelectedIndex;
            _Controlador.ValidarCaptura(!(_Guardados[Tab]), "El registro ya se guardo. Use Ingresar para comenzar otro.");
            string Numero;
            if (Tab == 1)
            {
                Numero = _Controlador.RegistrarDocumento(Cliente, Moneda, VentasIdUsuario, V.Id(_Vista.VentasCboEstablecimientoDocumento), _Vista.VentasTxtNumeroDocumento.Text, _Vista.VentasCboTipoDocumento.SelectedIndex, _Vista.VentasDtpFechaDocumento.Value, _Vista.VentasDtpVencimientoDocumento.Value, _Vista.VentasNudImporteDocumento.Value, _Vista.VentasTxtFacturaReferencia.Text, _Vista.VentasTxtDescripcionDocumento.Text);
                _Vista.VentasTxtNumeroDocumento.Text = Numero;
                V.Informar(_Vista, "Documento interno de CxC registrado: " + Numero);
            }
            else if (Tab == 2)
            {
                string[] Formas = { "EFECTIVO", "TRANSFERENCIA", "DEPOSITO", "CHEQUE", "TARJETA" };
                _Controlador.ValidarCaptura(!(_Vista.VentasCboFormaPago.SelectedIndex < 0), "Seleccione la forma de pago.");
                Numero = _Controlador.RegistrarRecibo(Cliente, Moneda, VentasIdUsuario, _Vista.VentasTxtRecibo.Text, _Vista.VentasDtpFechaPago.Value, _Vista.VentasNudMontoPago.Value, _Vista.VentasNudTipoCambioPago.Value, Formas[_Vista.VentasCboFormaPago.SelectedIndex], _Vista.VentasTxtReferenciaPago.Text, _Vista.VentasTxtObservacionesPago.Text, VentasMetSeleccionados(_Vista.VentasDgvDocumentos, "idFactura", 6));
                _Vista.VentasTxtRecibo.Text = Numero; VentasMetPendientes();
                V.Informar(_Vista, "Recibo generado y aplicado correctamente: " + Numero);
            }
            else if (Tab == 3)
            {
                Numero = _Controlador.PrepararDeposito(V.Id(_Vista.VentasCboMonedaDeposito), VentasIdUsuario, _Vista.VentasDtpFechaDeposito.Value, _Vista.VentasTxtCodigoDeposito.Text, VentasMetReferencia(_Vista.VentasCboEmpresaDeposito), VentasMetReferencia(_Vista.VentasCboBancoDestino), VentasMetReferencia(_Vista.VentasCboCuentaDestino), VentasMetReferencia(_Vista.VentasCboConceptoBancario), _Vista.VentasTxtReferenciaDeposito.Text, VentasMetSeleccionados(_Vista.VentasDgvRecibosDeposito, "idPago", 7));
                _Vista.VentasTxtCodigoDeposito.Text = Numero; VentasMetDisponibles();
                V.Informar(_Vista, "Deposito preparado localmente: " + Numero + ". El registro en Bancos requiere su enlace.");
            }
            else { _Controlador.ValidarCaptura(false, "Seleccione una pestana de captura."); return; }
            _Guardados[Tab] = true;
            if (Tab == 1)
            {
                string Documento=_Vista.VentasCboTipoDocumento.SelectedIndex == 0 ? "CXC-"+Numero : Numero;
                DataRow F=_Controlador.ListarDocumentos(Cliente,Moneda).AsEnumerable().Single(R => Convert.ToString(R["Documento"]) == Documento);
                VentasMetCargarDocumento(Convert.ToInt32(F["Id"]),Convert.ToString(F["Clase"]));
            }
            else if(Tab == 2) VentasMetCargarRecibo(Convert.ToInt32(_Controlador.ConsultarRecibos(Numero, "").Rows[0]["idPago"]));
            VentasMetRefrescarBotones();
        }
        private void VentasMetNuevo()
        {
            if (_Vista.VentasTbcMovimientos.SelectedIndex == 0) _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpDocumento;
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpConsultaRecibos) _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpRecibos;
            int Tab = _Vista.VentasTbcMovimientos.SelectedIndex; _Guardados[Tab] = false;
            if(Tab == 1) { _DocumentoActual=null; _EditandoDocumento=false; }
            if(Tab == 2) { _ReciboActual=null; _EditandoRecibo=false; }
            if (Tab == 1)
            {
                _Vista.VentasTxtNumeroDocumento.Clear(); _Vista.VentasTxtFacturaReferencia.Clear(); _Vista.VentasTxtDescripcionDocumento.Clear(); _Vista.VentasNudImporteDocumento.Value = 0;
                _Vista.VentasCboTipoDocumento.SelectedIndex = 0; _Vista.VentasDtpFechaDocumento.Value = DateTime.Today;
                DataRow C = V.Fila(_Vista.VentasCboCliente);
                _Vista.VentasDtpVencimientoDocumento.Value = DateTime.Today.AddDays(C == null ? 0 : Convert.ToInt32(C["diasCredito"]));
            }
            if (Tab == 2) { _Vista.VentasTxtRecibo.Clear(); _Vista.VentasTxtReferenciaPago.Clear(); _Vista.VentasTxtObservacionesPago.Clear(); _Vista.VentasNudMontoPago.Value = 0; _Vista.VentasNudTipoCambioPago.Value = 1; _Vista.VentasDtpFechaPago.Value = DateTime.Today; _Vista.VentasCboFormaPago.SelectedIndex = 0; VentasMetPendientes(); }
            if (Tab == 3) { _Vista.VentasTxtCodigoDeposito.Clear(); _Vista.VentasTxtReferenciaDeposito.Clear(); _Vista.VentasDtpFechaDeposito.Value = DateTime.Today; VentasMetDisponibles(); }
            VentasMetRefrescarBotones();
            if (Tab < 3 && Cliente <= 0) _Vista.VentasCboCliente.Focus();
            else if (Tab == 1) _Vista.VentasNudImporteDocumento.Focus();
            else if (Tab == 2) _Vista.VentasNudMontoPago.Focus();
        }
        private void VentasMetRefrescarBotones()
        {
            int Tab = _Vista.VentasTbcMovimientos.SelectedIndex;
            _Vista.VentasBtnGuardar.Enabled = _Inicializado && Tab > 0 && Tab < 4 && !_Guardados[Tab];
            _Vista.VentasBtnIngresar.Enabled = _Inicializado; _Vista.VentasBtnCancelar.Enabled = _Vista.VentasBtnIngresar.Enabled;
            VentasMetActualizarAcciones();
        }
        public void VentasMetConfigurarCatalogosBancos(DataTable Empresas, DataTable Bancos, DataTable Cuentas, DataTable Conceptos, string Modo)
        {
            // Tablas con Id y Nombre suministradas por el modulo dueno de esos catalogos.
            foreach(var Combo in new[] { _Vista.VentasCboEmpresaDeposito, _Vista.VentasCboBancoDestino, _Vista.VentasCboCuentaDestino, _Vista.VentasCboConceptoBancario }) Combo.DropDownStyle=ComboBoxStyle.DropDownList;
            V.Combo(_Vista.VentasCboEmpresaDeposito, Empresas); V.Combo(_Vista.VentasCboBancoDestino, Bancos);
            V.Combo(_Vista.VentasCboCuentaDestino, Cuentas); V.Combo(_Vista.VentasCboConceptoBancario, Conceptos);
            _Vista.VentasTxtModoActualizacion.Text = Modo;
        }
        private static string VentasMetReferencia(ComboBox Combo)
        { return Combo.SelectedValue == null ? Combo.Text.Trim() : Convert.ToString(Combo.SelectedValue); }
        private void VentasMetDisponibles()
        {
            _Cargando = true;
            try
            {
                _Vista.VentasDgvRecibosDeposito.Rows.Clear();
                foreach (DataRow F in _Controlador.RecibosDisponibles(V.Id(_Vista.VentasCboMonedaDeposito)).Rows)
                {
                    int I = _Vista.VentasDgvRecibosDeposito.Rows.Add(false, F["Recibo"], F["Cliente"], F["Moneda"], F["Monto"], F["Depositado"], F["Disponible"], 0m);
                    _Vista.VentasDgvRecibosDeposito.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            _Vista.VentasTxtTotalDeposito.Text = "0.00";
        }
        private void VentasMetDepositoCambio(object Sender, DataGridViewCellEventArgs E)
        {
            if (_Cargando || E.RowIndex < 0) return;
            V.Ejecutar(_Vista, () =>
            {
                var F = _Vista.VentasDgvRecibosDeposito.Rows[E.RowIndex];
                if (E.ColumnIndex == 0 && Convert.ToBoolean(F.Cells[0].Value) && _Controlador.ImporteCelda(F.Cells[7].Value) == 0) F.Cells[7].Value = F.Cells[6].Value;
                _Vista.VentasTxtTotalDeposito.Text = VentasMetSumar(_Vista.VentasDgvRecibosDeposito, 7).ToString("N2", V.Cultura);
            });
        }
        private void VentasMetConsultarRecibos()
        {
            _Cargando = true;
            try
            {
                _Vista.VentasDgvConsultaRecibos.Rows.Clear(); _Vista.VentasDgvConsultaAplicaciones.Rows.Clear(); _Vista.VentasTxtMovimientoBancario.Clear();
                foreach (DataRow F in _Controlador.ConsultarRecibos(_Vista.VentasTxtConsultaRecibo.Text.Trim(), _Vista.VentasTxtConsultaDeposito.Text.Trim()).Rows)
                {
                    int I = _Vista.VentasDgvConsultaRecibos.Rows.Add(F["Recibo"], F["Fecha"], F["Cliente"], F["Moneda"], F["Monto"], F["Estado"], F["Deposito"], F["Depositado"]);
                    _Vista.VentasDgvConsultaRecibos.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            VentasMetAplicacionesConsulta();
        }
        private void VentasMetAplicacionesConsulta()
        {
            DataRow F = _Vista.VentasDgvConsultaRecibos.CurrentRow == null ? null : _Vista.VentasDgvConsultaRecibos.CurrentRow.Tag as DataRow;
            _Vista.VentasDgvConsultaAplicaciones.Rows.Clear(); if (F == null) return;
            _Vista.VentasTxtMovimientoBancario.Text = Convert.ToString(F["MovimientoBancario"]);
            foreach (DataRow A in _Controlador.Aplicaciones(Convert.ToInt32(F["idPago"])).Rows) _Vista.VentasDgvConsultaAplicaciones.Rows.Add(A["Factura"], A["Fecha"], A["Importe"], A["Estado"]);
        }
        private void VentasMetImprimir()
        {
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpCuenta)
            { V.Imprimir(_Vista, "Estado de cuenta", _Vista.VentasCboCliente.Text + " / " + CodigoMoneda, _EstadoCuenta == null ? null : _EstadoCuenta.Tables[1]); return; }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDocumento)
            { V.Imprimir(_Vista, "Documento interno de CxC", CodigoMoneda, _Controlador.Documento(Cliente, Moneda, V.Id(_Vista.VentasCboEstablecimientoDocumento), _Vista.VentasTxtNumeroDocumento.Text.Trim(), _Vista.VentasCboTipoDocumento.SelectedIndex == 0)); return; }
            if (_Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpDepositos)
            {
                DataSet D = _Controlador.Deposito(_Vista.VentasTxtCodigoDeposito.Text.Trim()); DataRow H = D.Tables[0].Rows[0];
                V.Imprimir(_Vista, "Preparacion local " + H["codigoDeposito"], "Estado: " + H["estadoDeposito"] + " / Referencia de cuenta: " + H["cuentaReferencia"] + " / Total: " + H["total"], D.Tables[1]); return;
            }
            bool Recepcion = _Vista.VentasTbcMovimientos.SelectedTab == _Vista.VentasTbpRecibos;
            DataTable Recibos = _Controlador.ConsultarRecibos(Recepcion ? _Vista.VentasTxtRecibo.Text.Trim() : _Vista.VentasTxtConsultaRecibo.Text.Trim(), Recepcion ? "" : _Vista.VentasTxtConsultaDeposito.Text.Trim());
            _Controlador.ValidarCaptura(!(Recibos.Rows.Count != 1), "Consulte un solo recibo para imprimir su comprobante.");
            DataRow R = Recibos.Rows[0];
            V.Imprimir(_Vista, "Recibo " + R["Recibo"], R["Cliente"] + " / " + R["Moneda"] + " / Total: " + Convert.ToDecimal(R["Monto"]).ToString("N2", V.Cultura) + " / " + R["Estado"], _Controlador.Aplicaciones(Convert.ToInt32(R["idPago"])));
        }

        internal void VentasMetMostrarRecepcionPagos() { _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpRecibos; VentasMetActualizarContexto(); }
        internal void VentasMetMostrarConsultaRecibos(string Recibo, string Deposito)
        { _Vista.VentasTxtConsultaRecibo.Text = Recibo ?? ""; _Vista.VentasTxtConsultaDeposito.Text = Deposito ?? ""; _Vista.VentasTbcMovimientos.SelectedTab = _Vista.VentasTbpConsultaRecibos; VentasMetActualizarContexto(); }
        internal void VentasMetCambiarPestana()
        { VentasMetActualizarContexto(); if (!_Cargando && _Inicializado) V.Ejecutar(_Vista, VentasMetConsultarActual); }
        private void VentasMetActualizarContexto() {
            // Un depósito agrupa recibos de distintos clientes; no hereda el filtro individual.
            _Vista.VentasPnlCliente.Enabled = _Vista.VentasTbcMovimientos.SelectedIndex < 3;
            VentasMetRefrescarBotones();
        }
        internal void VentasMetTipoDocumentoCambio() {
            // Presenta el efecto y los campos requeridos del documento.
            int Tipo = _Vista.VentasCboTipoDocumento.SelectedIndex;
            _Vista.VentasCboEstablecimientoDocumento.Enabled = Tipo == 0;
            _Vista.VentasTxtFacturaReferencia.Enabled = Tipo > 0;
            _Vista.VentasTxtNaturalezaDocumento.Text = Tipo < 0 ? string.Empty :
                (Tipo == 1 || Tipo == 4 ? "Abono - disminuye la deuda" : "Cargo - aumenta la deuda");
        }
    }
}
