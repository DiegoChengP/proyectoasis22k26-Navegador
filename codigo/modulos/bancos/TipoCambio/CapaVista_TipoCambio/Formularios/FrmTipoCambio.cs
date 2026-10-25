using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaControlador_TipoCambio;

namespace CapaVista_TipoCambio.Formularios
{

    public partial class FrmTipoCambio : Form
    {
        private readonly ClsControladorTipoCambio _controlador;

        public FrmTipoCambio()
        {
            InitializeComponent();
            _controlador = new ClsControladorTipoCambio();
            BancosMetInicializarPestañaActuales();
            BancosMetInicializarPestañaHistorico();
            BancosMetInicializarPestañaConversor();
        }

        // tasas actuales

        private void BancosMetInicializarPestañaActuales()
        {
            DataTable dt = _controlador.BancosMetObtenerTasasActuales();
            BancosDgvTasasActuales.DataSource = dt;
            BancosMetFormatearGrillaTasas(BancosDgvTasasActuales);

            // Seleccionar primera fila y actualizar panel de detalle
            if (BancosDgvTasasActuales.Rows.Count > 0)
            {
                BancosDgvTasasActuales.Rows[0].Selected = true;
                BancosMetActualizarDetalleTasaActual();
            }
        }

        private void BancosDgvTasasActuales_SelectionChanged(object sender, EventArgs e)
        {
            BancosMetActualizarDetalleTasaActual();
        }

        private void BancosMetActualizarDetalleTasaActual()
        {
            if (BancosDgvTasasActuales.SelectedRows.Count > 0)
            {
                var row = BancosDgvTasasActuales.SelectedRows[0];
                string orig = row.Cells["Moneda Origen"]?.Value?.ToString() ?? "USD";
                string dest = row.Cells["Moneda Destino"]?.Value?.ToString() ?? "GTQ";
                string fecha = row.Cells["Fecha"]?.Value?.ToString() ?? "13/10/2026";
                string tasa = "";
                if (row.Cells["Tasa"]?.Value != null)
                {
                    tasa = Convert.ToDecimal(row.Cells["Tasa"].Value).ToString("N4");
                }
                string fuente = row.Cells["Fuente"]?.Value?.ToString() ?? "Demostración";

                BancosLblDetalleParVal.Text   = $"{orig} → {dest}";
                BancosLblDetalleFechaVal.Text = fecha;
                BancosLblDetalleTasaVal.Text  = tasa;
                BancosLblDetalleFuenteVal.Text= fuente;
            }
        }

        private void BancosBtnActualizarBanguat_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_controlador.BancosMetActualizarDesdeBanguat(),
                "Actualización – Banguat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Hisorico

        private void BancosMetInicializarPestañaHistorico()
        {
            BancosDtpDesde.Value = new DateTime(2026, 10, 9);
            BancosDtpHasta.Value = new DateTime(2026, 10, 13);

            BancosCboParHist.Items.Clear();
            BancosCboParHist.Items.Add("USD → GTQ");
            BancosCboParHist.Items.Add("EUR → GTQ");
            BancosCboParHist.Items.Add("EUR → USD");
            BancosCboParHist.SelectedIndex = 0;

            BancosBtnBuscarHistorico_Click(this, EventArgs.Empty);
        }

        private void BancosBtnBuscarHistorico_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime desde = BancosDtpDesde.Value.Date;
                DateTime hasta = BancosDtpHasta.Value.Date;

                string par = BancosCboParHist.SelectedItem?.ToString() ?? "USD → GTQ";
                string orig = "USD";
                string dest = "GTQ";
                if (par.Contains("→"))
                {
                    var partes = par.Split(new[] { "→", " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (partes.Length >= 2)
                    {
                        orig = partes[0].Trim();
                        dest = partes[1].Trim();
                    }
                }

                DataTable dt = _controlador.BancosMetObtenerHistorico(desde, hasta, orig, dest);
                BancosDgvHistorico.DataSource = dt;
                BancosMetFormatearGrillaTasas(BancosDgvHistorico);

                // Métricas
                var metricas = _controlador.BancosMetObtenerResumenHistorico();
                BancosLblMinimaValor.Text    = metricas.Minima.ToString("N4");
                BancosLblMaximaValor.Text    = metricas.Maxima.ToString("N4");
                BancosLblVariacionValor.Text = $"+{metricas.Variacion:N4}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar histórico:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BancosBtnExportarHist_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La exportación de tasas históricas estará habilitada al integrar el repositorio de base de datos.",
                "Exportar – Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        //conversor

        private void BancosMetInicializarPestañaConversor()
        {
            BancosCboMonedaOrigenConv.Items.Clear();
            BancosCboMonedaDestinoConv.Items.Clear();
            foreach (var m in new[] { "USD", "EUR", "GTQ" })
            {
                BancosCboMonedaOrigenConv.Items.Add(m);
                BancosCboMonedaDestinoConv.Items.Add(m);
            }
            BancosCboMonedaOrigenConv.SelectedIndex  = 0; // USD
            BancosCboMonedaDestinoConv.SelectedIndex = 2; // GTQ

            BancosNumImporte.Value    = 1000.00m;
            BancosDtpFechaConv.Value  = new DateTime(2026, 10, 13);

            BancosMetRecalcularConversor();
        }

        private void BancosNumImporte_ValueChanged(object sender, EventArgs e)
        {
            BancosMetRecalcularConversor();
        }

        private void BancosCboMonedaOrigenConv_SelectedIndexChanged(object sender, EventArgs e)
        {
            BancosMetRecalcularConversor();
        }

        private void BancosCboMonedaDestinoConv_SelectedIndexChanged(object sender, EventArgs e)
        {
            BancosMetRecalcularConversor();
        }

        private void BancosBtnCalcular_Click(object sender, EventArgs e)
        {
            BancosMetRecalcularConversor();
        }

        private void BancosMetRecalcularConversor()
        {
            decimal importe = BancosNumImporte.Value;
            string orig = BancosCboMonedaOrigenConv.SelectedItem?.ToString() ?? "USD";
            string dest = BancosCboMonedaDestinoConv.SelectedItem?.ToString() ?? "GTQ";

            var (resultado, tasa, _) = _controlador.BancosMetConvertir(importe, orig, dest);

            string prefDest = dest == "GTQ" ? "Q" : (dest == "USD" ? "$" : "€");
            BancosLblConvResultadoGrande.Text = $"{prefDest}{resultado:N2}";
            BancosLblConvCalculoDetalle.Text   = $"{importe:N2} {orig} × {tasa:N4}";
            BancosLblConvTasaDetalle.Text      = $"Tasa aplicada: 1 {orig} = {tasa:N4} {dest}";
            BancosLblConvFuente.Text           = "Fuente: Demostración";
            BancosLblConvFecha.Text            = $"Fecha: {BancosDtpFechaConv.Value:dd/MM/yyyy}";
        }

        // ayudas

        private void BancosMetFormatearGrillaTasas(DataGridView dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible   = false;
            dgv.AllowUserToAddRows  = false;
            dgv.ReadOnly            = true;

            if (dgv.Columns.Contains("Tasa"))
            {
                dgv.Columns["Tasa"].DefaultCellStyle.Format    = "N4";
                dgv.Columns["Tasa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                    ? Color.White
                    : Color.FromArgb(248, 252, 248);
            }
        }
    }
}
