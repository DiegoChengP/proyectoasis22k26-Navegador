using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_CierreMovimientos;
using V = VentasPresentacion.ClsVistaVentas;

namespace CapaVista_CierreMovimientos
{
    public partial class FrmCierreMovimientosVentas
    {
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
            get { return V.Id(VentasCboUsuarioRegistro); }
            set { _UsuarioInicial = value; if (VentasCboUsuarioRegistro.DataSource != null) VentasCboUsuarioRegistro.SelectedValue = value; }
        }
        private int Moneda { get { return V.Id(VentasCboMonedaCierre); } }
        private string Tipo { get { return VentasCboTipoCierre.SelectedIndex == 1 ? "ANUAL" : "MENSUAL"; } }
        private void VentasMetConectarEventos()
        {
            Load += (s, e) => V.Ejecutar(this, VentasMetCargar);
            VentasBtnRevisar.Click += (s, e) => V.Ejecutar(this, VentasMetRevisar);
            VentasBtnConfirmar.Click += (s, e) => V.Ejecutar(this, VentasMetConfirmar);
            VentasBtnCancelar.Click += (s, e) => VentasMetLimpiarRevision();
            VentasBtnCerrar.Click += (s, e) => Close();
            VentasBtnTrasladarContabilidad.Click += (s, e) => V.Ejecutar(this, () =>
            {
                if (MessageBox.Show(this, "¿Desea registrar esta partida en Contabilidad?", "Traslado contable", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                string Referencia = _Controlador.Trasladar(_IdCierre, VentasIdUsuario, _EnlaceContable);
                VentasMetMostrarPartida(); V.Informar(this, "Contabilidad confirmo la partida: " + Referencia);
            });
            VentasBtnImprimir.Click += (s, e) => V.Ejecutar(this, () => V.Imprimir(this, "Cierre " + _NumeroCierre, VentasCboMonedaCierre.Text + " / " + VentasDtpDesdeCierre.Value.ToString("dd/MM/yyyy") + " al " + VentasDtpHastaCierre.Value.ToString("dd/MM/yyyy"), _Revision));
            VentasCboMonedaCierre.SelectedIndexChanged += (s, e) => { if (!_Cargando) VentasMetLimpiarRevision(); };
            VentasChkRevisionDocumentos.CheckedChanged += (s, e) => VentasMetActualizarConfirmacion();
            VentasTbcCierre.SelectedIndexChanged += (s, e) => { if (!_Cargando && Moneda > 0) V.Ejecutar(this, VentasMetRevisar); };
            VentasDgvHistorialCierre.SelectionChanged += (s, e) =>
            {
                if (_Cargando || VentasDgvHistorialCierre.CurrentRow == null) return;
                var F = VentasDgvHistorialCierre.CurrentRow.Tag as DataRow;
                if (F != null) { _IdCierre = Convert.ToInt32(F["idCierre"]); _NumeroCierre = Convert.ToString(F["numeroCierre"]); V.Ejecutar(this, VentasMetMostrarPartida); }
            };
            VentasDtpDesdeCierre.Enabled = false; VentasDtpHastaCierre.Enabled = false; VentasDtpCorteCierre.Enabled = false;
            VentasTxtPeriodoHistorial.MaxLength = 7;
            VentasBtnTrasladarContabilidad.Enabled = false;
            VentasLblTrasladoCierre.Text = "La partida se prepara despues del cierre. El registro contable requiere el enlace del modulo de Contabilidad.";
        }
        private void VentasMetCargar()
        {
            _Cargando = true;
            try
            {
                V.Combo(VentasCboUsuarioRegistro, _Controlador.Catalogo("Usuarios"), _UsuarioInicial);
                V.Combo(VentasCboMonedaCierre, _Controlador.Catalogo("Monedas"));
                DataTable Clientes = _Controlador.Catalogo("Clientes"); DataRow Todos = Clientes.NewRow(); Todos["Id"] = 0; Todos["Nombre"] = "Todos los clientes"; Clientes.Rows.InsertAt(Todos, 0);
                V.Combo(VentasCboClienteHistorial, Clientes); VentasCboClienteHistorial.SelectedValue = 0;
            }
            finally { _Cargando = false; }
            VentasMetLimpiarRevision();
        }
        private void VentasMetLimpiarRevision()
        {
            _IdCierre = 0; _Revision = null; _Partida = null; _NumeroCierre = "";
            VentasDgvRevisionCierre.Rows.Clear(); VentasDgvPoliza.Rows.Clear(); VentasChkRevisionDocumentos.Checked = false;
            VentasBtnConfirmar.Enabled = false; VentasTxtEstadoCierre.Text = "Pendiente de revision";
            VentasTxtReferenciaPoliza.Clear(); VentasTxtFechaTraslado.Clear(); VentasTxtEstadoTraslado.Text = "Sin traslado contable";
            VentasLblValorAnteriorCierre.Text = VentasLblValorCargosCierre.Text = VentasLblValorAbonosCierre.Text = VentasLblValorCargosAcumulados.Text = VentasLblValorAbonosAcumulados.Text = VentasLblValorActualCierre.Text = "0.00";
        }
        private void VentasMetRevisar()
        {
            VentasBtnConfirmar.Enabled = false;
            if (Moneda <= 0) throw new InvalidOperationException("Seleccione la moneda. Cada moneda se cierra por separado.");
            if (VentasTbcCierre.SelectedTab == VentasTbpHistorial) { VentasMetHistorial(); return; }
            if (VentasTbcCierre.SelectedTab == VentasTbpContabilidad) { VentasMetMostrarPartida(); return; }
            DataTable Estado = _Controlador.EstadoPeriodo(Moneda, Tipo, VentasDtpDesdeCierre.Value, VentasDtpHastaCierre.Value);
            _IdCierre = Estado.Rows.Count == 0 ? 0 : Convert.ToInt32(Estado.Rows[0]["idCierre"]);
            _NumeroCierre = Estado.Rows.Count == 0 ? "" : Convert.ToString(Estado.Rows[0]["numeroCierre"]);
            if (_IdCierre == 0) _Revision = _Controlador.Revisar(Moneda, Tipo, VentasDtpDesdeCierre.Value, VentasDtpHastaCierre.Value);
            else
            {
                DataTable Historia = _Controlador.Historial(Moneda, 0, VentasDtpDesdeCierre.Value.Year.ToString());
                _Revision = Historia.Clone(); foreach (DataRow F in Historia.Rows) if (Convert.ToInt32(F["idCierre"]) == _IdCierre) _Revision.ImportRow(F);
            }
            VentasDgvRevisionCierre.Rows.Clear();
            foreach (DataRow F in _Revision.Rows) VentasDgvRevisionCierre.Rows.Add(F["Codigo"], F["Nombre"], F["Anterior"], F["Cargos"], F["Abonos"], F["CargosAcumulados"], F["AbonosAcumulados"], F["Actual"]);
            var Etiquetas = new[] { VentasLblValorAnteriorCierre, VentasLblValorCargosCierre, VentasLblValorAbonosCierre, VentasLblValorCargosAcumulados, VentasLblValorAbonosAcumulados, VentasLblValorActualCierre };
            string[] Columnas = { "Anterior", "Cargos", "Abonos", "CargosAcumulados", "AbonosAcumulados", "Actual" };
            for (int I = 0; I < Columnas.Length; I++) Etiquetas[I].Text = _Revision.AsEnumerable().Sum(F => Convert.ToDecimal(F[Columnas[I]])).ToString("N2", V.Cultura);
            VentasTxtEstadoCierre.Text = _IdCierre == 0 ? "Revisado; sin cerrar" : "Cerrado: " + _NumeroCierre;
            VentasMetActualizarConfirmacion();
            VentasMetMostrarPartida();
        }
        private void VentasMetActualizarConfirmacion()
        {
            VentasBtnConfirmar.Enabled = _Revision != null && _IdCierre == 0 && VentasChkRevisionDocumentos.Checked && VentasTbcCierre.SelectedTab == VentasTbpPreparar && VentasDtpHastaCierre.Value.Date < DateTime.Today;
        }
        private void VentasMetConfirmar()
        {
            if (_Revision == null) throw new InvalidOperationException("Revise el periodo antes de confirmarlo.");
            if (MessageBox.Show(this, "El cierre conservara los saldos y bloqueara registros con fechas de este periodo. ¿Desea confirmarlo?", "Confirmar cierre", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            string Numero = _Controlador.Confirmar(Moneda, VentasIdUsuario, Tipo, VentasDtpDesdeCierre.Value, VentasDtpHastaCierre.Value, VentasChkRevisionDocumentos.Checked);
            VentasMetRevisar(); V.Informar(this, "Cierre confirmado: " + Numero + ". Se conservaron los saldos historicos por cliente y factura.");
        }
        private void VentasMetHistorial()
        {
            _Cargando = true;
            try
            {
                VentasDgvHistorialCierre.Rows.Clear();
                foreach (DataRow F in _Controlador.Historial(Moneda, V.Id(VentasCboClienteHistorial), VentasTxtPeriodoHistorial.Text).Rows)
                {
                    int I = VentasDgvHistorialCierre.Rows.Add(F["Periodo"], F["Codigo"], F["Nombre"], F["Anterior"], F["Cargos"], F["Abonos"], F["CargosAcumulados"], F["AbonosAcumulados"], F["Actual"], F["Estado"]);
                    VentasDgvHistorialCierre.Rows[I].Tag = F;
                }
            }
            finally { _Cargando = false; }
            if (VentasDgvHistorialCierre.CurrentRow != null)
            {
                var F = VentasDgvHistorialCierre.CurrentRow.Tag as DataRow;
                if (F != null) { _IdCierre = Convert.ToInt32(F["idCierre"]); _NumeroCierre = Convert.ToString(F["numeroCierre"]); }
            }
            VentasBtnConfirmar.Enabled = false; VentasMetMostrarPartida();
        }
        private void VentasMetMostrarPartida()
        {
            VentasDgvPoliza.Rows.Clear(); VentasBtnTrasladarContabilidad.Enabled = false;
            if (_IdCierre <= 0) return;
            _Partida = _Controlador.Partida(_IdCierre);
            foreach (DataRow F in _Partida.Rows) VentasDgvPoliza.Rows.Add(F["Cuenta"], F["Descripcion"], F["Debe"], F["Haber"]);
            decimal Debe = _Partida.AsEnumerable().Sum(F => (decimal)F["Debe"]), Haber = _Partida.AsEnumerable().Sum(F => (decimal)F["Haber"]);
            VentasLblPolizaTotales.Text = "Debe: " + Debe.ToString("N2", V.Cultura) + " / Haber: " + Haber.ToString("N2", V.Cultura);
            VentasTxtReferenciaPoliza.Text = "CXC-" + _NumeroCierre;
            VentasTxtEstadoTraslado.Text = "Preparada; pendiente de enlace";
            DataTable Estado = _Controlador.EstadoTraslado(_IdCierre);
            bool Confirmado = Estado.Rows.Count > 0 && (string)Estado.Rows[0]["estado"] == "CONFIRMADO";
            if (Confirmado)
            {
                VentasTxtReferenciaPoliza.Text = Convert.ToString(Estado.Rows[0]["referenciaContable"]);
                VentasTxtFechaTraslado.Text = Convert.ToDateTime(Estado.Rows[0]["fechaConfirmacion"]).ToString("dd/MM/yyyy HH:mm");
                VentasTxtEstadoTraslado.Text = "Confirmado por Contabilidad";
            }
            VentasBtnTrasladarContabilidad.Enabled = !Confirmado && _EnlaceContable != null && _Partida.Rows.Count > 0 && !_Partida.AsEnumerable().Any(F => ((string)F["Cuenta"]).StartsWith("PENDIENTE", StringComparison.Ordinal));
        }
    }
}
