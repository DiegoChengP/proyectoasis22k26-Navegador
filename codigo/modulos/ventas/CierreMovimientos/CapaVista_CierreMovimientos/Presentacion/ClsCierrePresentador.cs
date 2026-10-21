using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_CierreMovimientos;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_CierreMovimientos
{
    internal sealed partial class ClsCierrePresentador
    {
        private readonly FrmCierreMovimientosVentas _Vista;
        internal ClsCierrePresentador(FrmCierreMovimientosVentas Vista)
        {
            _Vista = Vista;
            VentasMetConectarEventos();
            _Vista.VentasNudAnioCierre.Value = DateTime.Today.Year;
            _Vista.VentasCboMesCierre.SelectedIndex = DateTime.Today.Month - 1;
            _Vista.VentasCboTipoCierre.SelectedIndex = 0;
            VentasMetPeriodoCambio();
        }

        private readonly ClsCierreControlador _Controlador = new ClsCierreControlador();
        private bool _Cargando = true;
        private int _UsuarioInicial;
        private int _IdCierre;
        private DataTable _Revision;
        private DataTable _Partida;
        private string _NumeroCierre = "";
        private Func<string, int, DateTime, DataTable, string> _EnlaceContable;
        public void VentasMetConfigurarEnlaceContable(Func<string, int, DateTime, DataTable, string> Registrar)
        { _EnlaceContable = Registrar; if (_IdCierre > 0) VentasMetMostrarPartida(); }
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
        private int Moneda { get { return V.Id(_Vista.VentasCboMonedaCierre); } }
        private string Tipo { get { return _Vista.VentasCboTipoCierre.SelectedIndex == 1 ? "ANUAL" : "MENSUAL"; } }
        private void VentasMetConectarEventos()
        {
            _Vista.Load += (s, e) => V.Ejecutar(_Vista, VentasMetCargar);
            _Vista.VentasBtnIngresar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetNuevo);
            _Vista.VentasBtnRevisar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetConsultarCierre);
            _Vista.VentasBtnGuardarBorrador.Click += (s,e) => V.Ejecutar(_Vista,VentasMetGuardarBorrador);
            _Vista.VentasBtnModificar.Click += (s,e) => V.Ejecutar(_Vista,VentasMetModificarBorrador);
            _Vista.VentasBtnEliminar.Click += (s,e) => V.Ejecutar(_Vista,VentasMetAnularBorrador);
            _Vista.VentasBtnRefrescar.Click += (s,e) => V.Ejecutar(_Vista,VentasMetRevisar);
            _Vista.VentasCboClienteHistorial.SelectedIndexChanged += (s,e) => { if(!_Cargando && Moneda>0 && _Vista.VentasTbcCierre.SelectedTab==_Vista.VentasTbpHistorial) V.Ejecutar(_Vista,VentasMetHistorial); };
            _Vista.VentasTxtPeriodoHistorial.KeyDown += (s,e) => { if(e.KeyCode==Keys.Enter) { e.SuppressKeyPress=true; V.Ejecutar(_Vista,VentasMetHistorial); } };
            _Vista.VentasBtnConfirmar.Click += (s, e) => V.Ejecutar(_Vista, VentasMetConfirmar);
            _Vista.VentasBtnCancelar.Click += (s, e) => VentasMetLimpiarRevision();
            _Vista.VentasBtnCerrar.Click += (s, e) => _Vista.Close();
            _Vista.VentasBtnTrasladarContabilidad.Click += (s, e) => V.Ejecutar(_Vista, () =>
            {
                if (MessageBox.Show(_Vista, "¿Desea registrar esta partida en Contabilidad?", "Traslado contable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                string Referencia = _Controlador.Trasladar(_IdCierre, VentasIdUsuario, _EnlaceContable);
                VentasMetMostrarPartida(); V.Informar(_Vista, "Contabilidad confirmo la partida: " + Referencia);
            });
            _Vista.VentasBtnImprimir.Click += (s,e) => V.Ejecutar(_Vista,VentasMetImprimirCierre);
            _Vista.VentasCboMonedaCierre.SelectedIndexChanged += (s, e) => { if (!_Cargando) { _IdBorrador=0; _EditandoBorrador=true; VentasMetLimpiarRevision(); VentasMetActualizarAccionesCierre(); } };
            _Vista.VentasChkRevisionDocumentos.CheckedChanged += (s, e) => VentasMetActualizarConfirmacion();
            _Vista.VentasTbcCierre.SelectedIndexChanged += (s, e) => { if (!_Cargando && Moneda > 0) V.Ejecutar(_Vista, VentasMetRevisar); };
            _Vista.VentasDgvHistorialCierre.SelectionChanged += (s, e) =>
            {
                if (_Cargando || _Vista.VentasDgvHistorialCierre.CurrentRow == null) return;
                var F = _Vista.VentasDgvHistorialCierre.CurrentRow.Tag as DataRow;
                if (F != null) { _IdCierre = Convert.ToInt32(F["idCierre"]); _NumeroCierre = Convert.ToString(F["numeroCierre"]); V.Ejecutar(_Vista, VentasMetMostrarPartida); }
            };
            _Vista.VentasDtpDesdeCierre.Enabled = false; _Vista.VentasDtpHastaCierre.Enabled = false; _Vista.VentasDtpCorteCierre.Enabled = false;
            _Vista.VentasTxtPeriodoHistorial.MaxLength = 7;
            _Vista.VentasBtnTrasladarContabilidad.Enabled = false;
            _Vista.VentasLblTrasladoCierre.Text = "La partida se prepara despues del cierre. El registro contable requiere el enlace del modulo de Contabilidad.";
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(_Vista.VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(_Vista.VentasCboMonedaCierre, _Controlador.Catalogo("Monedas"));
                DataTable Clientes = _Controlador.Catalogo("Clientes"); DataRow Todos = Clientes.NewRow(); Todos["Id"] = 0; Todos["Nombre"] = "Todos los clientes"; Clientes.Rows.InsertAt(Todos, 0);
                V.Combo(_Vista.VentasCboClienteHistorial, Clientes); _Vista.VentasCboClienteHistorial.SelectedValue = 0;
            }
            finally { _Cargando = false; }
            VentasMetLimpiarRevision();
        }
        private void VentasMetNuevo()
        {
            _IdBorrador=0; _EditandoBorrador=true; _Vista.VentasTxtObservacionesCierre.Clear();
            _Cargando = true;
            try
            {
                _Vista.VentasTbcCierre.SelectedTab = _Vista.VentasTbpPreparar;
                _Vista.VentasNudAnioCierre.Value = DateTime.Today.Year;
                _Vista.VentasCboMesCierre.SelectedIndex = DateTime.Today.Month - 1;
                _Vista.VentasCboTipoCierre.SelectedIndex = 0;
            }
            finally { _Cargando = false; }
            VentasMetPeriodoCambio();
            _Vista.VentasCboMonedaCierre.Focus(); VentasMetActualizarAccionesCierre();
        }
        private void VentasMetLimpiarRevision()
        {
            _IdCierre = 0; _Revision = null; _Partida = null; _NumeroCierre = "";
            _Vista.VentasDgvRevisionCierre.Rows.Clear(); _Vista.VentasDgvPoliza.Rows.Clear(); _Vista.VentasChkRevisionDocumentos.Checked = false;
            _Vista.VentasBtnConfirmar.Enabled = false; _Vista.VentasTxtEstadoCierre.Text = "Pendiente de revision";
            _Vista.VentasTxtReferenciaPoliza.Clear(); _Vista.VentasTxtFechaTraslado.Clear(); _Vista.VentasTxtEstadoTraslado.Text = "Sin traslado contable";
            _Vista.VentasLblValorAnteriorCierre.Text = _Vista.VentasLblValorCargosCierre.Text = _Vista.VentasLblValorAbonosCierre.Text = _Vista.VentasLblValorCargosAcumulados.Text = _Vista.VentasLblValorAbonosAcumulados.Text = _Vista.VentasLblValorActualCierre.Text = "0.00";
        }
        private void VentasMetRevisar()
        {
            _Vista.VentasBtnConfirmar.Enabled = false;
            _Controlador.ValidarCaptura(!(Moneda <= 0), "Seleccione la moneda. Cada moneda se cierra por separado.");
            if (_Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpHistorial) { VentasMetHistorial(); return; }
            if (_Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpContabilidad) { VentasMetMostrarPartida(); return; }
            DataTable Estado = _Controlador.EstadoPeriodo(Moneda, Tipo, _Vista.VentasDtpDesdeCierre.Value, _Vista.VentasDtpHastaCierre.Value);
            _IdCierre = Estado.Rows.Count == 0 ? 0 : Convert.ToInt32(Estado.Rows[0]["idCierre"]);
            _NumeroCierre = Estado.Rows.Count == 0 ? "" : Convert.ToString(Estado.Rows[0]["numeroCierre"]);
            if (_IdCierre == 0) _Revision = _Controlador.Revisar(Moneda, Tipo, _Vista.VentasDtpDesdeCierre.Value, _Vista.VentasDtpHastaCierre.Value);
            else
            {
                DataTable Historia = _Controlador.Historial(Moneda, 0, _Vista.VentasDtpDesdeCierre.Value.Year.ToString());
                _Revision = Historia.Clone(); foreach (DataRow F in Historia.Rows) if (Convert.ToInt32(F["idCierre"]) == _IdCierre) _Revision.ImportRow(F);
            }
            _Vista.VentasDgvRevisionCierre.Rows.Clear();
            foreach (DataRow F in _Revision.Rows) _Vista.VentasDgvRevisionCierre.Rows.Add(F["Codigo"], F["Nombre"], F["Anterior"], F["Cargos"], F["Abonos"], F["CargosAcumulados"], F["AbonosAcumulados"], F["Actual"]);
            var Etiquetas = new[] { _Vista.VentasLblValorAnteriorCierre, _Vista.VentasLblValorCargosCierre, _Vista.VentasLblValorAbonosCierre, _Vista.VentasLblValorCargosAcumulados, _Vista.VentasLblValorAbonosAcumulados, _Vista.VentasLblValorActualCierre };
            string[] Columnas = { "Anterior", "Cargos", "Abonos", "CargosAcumulados", "AbonosAcumulados", "Actual" };
            for (int I = 0; I < Columnas.Length; I++) Etiquetas[I].Text = _Revision.AsEnumerable().Sum(F => Convert.ToDecimal(F[Columnas[I]])).ToString("N2", V.Cultura);
            if (_IdCierre == 0 && !_EditandoBorrador)
            {
                DataRow B=_Controlador.Listar(Moneda).AsEnumerable().FirstOrDefault(F=>Convert.ToString(F["Estado"])=="BORRADOR" && Convert.ToString(F["Tipo"])==Tipo && Convert.ToDateTime(F["Desde"]).Date==_Vista.VentasDtpDesdeCierre.Value.Date && Convert.ToDateTime(F["Hasta"]).Date==_Vista.VentasDtpHastaCierre.Value.Date);
                if(B!=null) { _IdBorrador=Convert.ToInt32(B["Id"]); _Vista.VentasTxtObservacionesCierre.Text=Convert.ToString(B["Observaciones"]); }
            }
            _Vista.VentasTxtEstadoCierre.Text = _IdCierre == 0 ? (_IdBorrador > 0 ? "Borrador revisado" : "Revisado; sin cerrar") : "Cerrado: " + _NumeroCierre;
            VentasMetActualizarConfirmacion();
            VentasMetMostrarPartida();
        }
        private void VentasMetActualizarConfirmacion()
        {
            VentasMetActualizarAccionesCierre();
            _Vista.VentasBtnConfirmar.Enabled = _Revision != null && _IdCierre == 0 && _Vista.VentasChkRevisionDocumentos.Checked && _Vista.VentasTbcCierre.SelectedTab == _Vista.VentasTbpPreparar && _Vista.VentasDtpHastaCierre.Value.Date < DateTime.Today;
        }
        private void VentasMetConfirmar()
        {
            _Controlador.ValidarCaptura(!(_Revision == null), "Revise el periodo antes de confirmarlo.");
            if (MessageBox.Show(_Vista, "El cierre conservara los saldos y bloqueara registros con fechas de este periodo. ¿Desea confirmarlo?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (_IdBorrador == 0 || _EditandoBorrador) VentasMetPersistirBorrador();
            string Numero = _Controlador.Confirmar(Moneda, VentasIdUsuario, Tipo, _Vista.VentasDtpDesdeCierre.Value, _Vista.VentasDtpHastaCierre.Value, _Vista.VentasChkRevisionDocumentos.Checked);
            VentasMetRevisar(); V.Informar(_Vista, "Cierre confirmado: " + Numero + ". Se conservaron los saldos historicos por cliente y factura.");
        }
        private void VentasMetHistorial()
        {
            _Cargando = true;
            try
            {
                _IdCierre=0; _NumeroCierre="";
                _Vista.VentasDgvHistorialCierre.Rows.Clear();
                foreach (DataRow F in _Controlador.Historial(Moneda, V.Id(_Vista.VentasCboClienteHistorial), _Vista.VentasTxtPeriodoHistorial.Text).Rows)
                {
                    int I = _Vista.VentasDgvHistorialCierre.Rows.Add(F["Periodo"], F["Codigo"], F["Nombre"], F["Anterior"], F["Cargos"], F["Abonos"], F["CargosAcumulados"], F["AbonosAcumulados"], F["Actual"], F["Estado"]);
                    _Vista.VentasDgvHistorialCierre.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            if (_Vista.VentasDgvHistorialCierre.CurrentRow != null)
            {
                var F = _Vista.VentasDgvHistorialCierre.CurrentRow.Tag as DataRow;
                if (F != null) { _IdCierre = Convert.ToInt32(F["idCierre"]); _NumeroCierre = Convert.ToString(F["numeroCierre"]); }
            }
            _Vista.VentasBtnConfirmar.Enabled = false; VentasMetMostrarPartida();
        }
        private void VentasMetMostrarPartida()
        {
            _Vista.VentasDgvPoliza.Rows.Clear(); _Vista.VentasBtnTrasladarContabilidad.Enabled = false;
            if (_IdCierre <= 0) return;
            _Partida = _Controlador.Partida(_IdCierre);
            foreach (DataRow F in _Partida.Rows) _Vista.VentasDgvPoliza.Rows.Add(F["Cuenta"], F["Descripcion"], F["Debe"], F["Haber"]);
            decimal Debe = _Partida.AsEnumerable().Sum(F => (decimal)F["Debe"]), Haber = _Partida.AsEnumerable().Sum(F => (decimal)F["Haber"]);
            _Vista.VentasLblPolizaTotales.Text = "Debe: " + Debe.ToString("N2", V.Cultura) + " / Haber: " + Haber.ToString("N2", V.Cultura);
            _Vista.VentasTxtReferenciaPoliza.Text = "CXC-" + _NumeroCierre;
            _Vista.VentasTxtEstadoTraslado.Text = "Preparada; pendiente de enlace";
            DataTable Estado = _Controlador.EstadoTraslado(_IdCierre);
            bool Confirmado = Estado.Rows.Count > 0 && (string)Estado.Rows[0]["estado"] == "CONFIRMADO";
            if (Confirmado)
            {
                _Vista.VentasTxtReferenciaPoliza.Text = Convert.ToString(Estado.Rows[0]["referenciaContable"]);
                _Vista.VentasTxtFechaTraslado.Text = Convert.ToDateTime(Estado.Rows[0]["fechaConfirmacion"]).ToString("dd/MM/yyyy HH:mm");
                _Vista.VentasTxtEstadoTraslado.Text = "Confirmado por Contabilidad";
            }
            _Vista.VentasBtnTrasladarContabilidad.Enabled = !Confirmado && _EnlaceContable != null && _Partida.Rows.Count > 0 && !_Partida.AsEnumerable().Any(F => ((string)F["Cuenta"]).StartsWith("PENDIENTE", StringComparison.Ordinal));
        }
        internal void VentasMetPeriodoCambio()
        {
            // Mantiene el período completo; la revisión anterior queda invalidada al cambiarlo.
            bool Anual = _Vista.VentasCboTipoCierre.Text == "Anual";
            int Anio = (int)_Vista.VentasNudAnioCierre.Value;
            int Mes = _Vista.VentasCboMesCierre.SelectedIndex < 0 ? DateTime.Today.Month : _Vista.VentasCboMesCierre.SelectedIndex + 1;
            DateTime[] Periodo=_Controlador.Periodo(Anio,Mes,Anual);
            DateTime Desde=Periodo[0], Hasta=Periodo[1];
            _Vista.VentasCboMesCierre.Enabled = !Anual;
            _Vista.VentasDtpDesdeCierre.Value = Desde;
            _Vista.VentasDtpHastaCierre.Value = Hasta;
            _Vista.VentasDtpCorteCierre.Value = Hasta;
            string Unidad = Anual ? "año" : "mes";
            _Vista.VentasClmRevisionCierreCargos.HeaderText = "Cargos del " + Unidad;
            _Vista.VentasClmRevisionCierreAbonos.HeaderText = "Abonos del " + Unidad;
            _Vista.VentasLblTituloCargosCierre.Text = "Cargos del " + Unidad;
            _Vista.VentasLblTituloAbonosCierre.Text = "Abonos del " + Unidad;
            if (!_Cargando) { if(!_EditandoBorrador) { _IdBorrador=0; _EditandoBorrador=true; _Vista.VentasTxtObservacionesCierre.Clear(); } VentasMetLimpiarRevision(); VentasMetActualizarAccionesCierre(); }
                }
    }
}
