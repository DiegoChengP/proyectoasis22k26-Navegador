using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaControlador_DisponibilidadDiaria;

namespace CapaVista_DisponibilidadDiaria.Formularios
{
    public partial class FrmDisponibilidadDiaria : Form
    {
        private readonly ClsControladorDisponibilidad _controlador;

        public FrmDisponibilidadDiaria()
        {
            InitializeComponent();
            _controlador = new ClsControladorDisponibilidad();
            BancosMetCargarCatalogos();
            BancosBtnConsultar_Click(this, EventArgs.Empty);
        }

        // Carga inicial

        private void BancosMetCargarCatalogos()
        {
            BancosCboBanco.Items.Clear();
            foreach (var b in _controlador.BancosMetObtenerBancos())
            {
                BancosCboBanco.Items.Add(b);
            }
            BancosCboBanco.SelectedIndex = 0; // "Todos"

            BancosCboMoneda.Items.Clear();
            BancosCboMoneda.Items.Add("Todas");
            BancosCboMoneda.Items.Add("GTQ");
            BancosCboMoneda.Items.Add("USD");
            BancosCboMoneda.Items.Add("EUR");
            BancosCboMoneda.SelectedIndex = 1; // "GTQ" por defecto según requerimiento

            BancosDtpFechaCorte.Value = new DateTime(2026, 10, 13);
        }

        private void BancosCboBanco_SelectedIndexChanged(object sender, EventArgs e)
        {
            string banco = BancosCboBanco.SelectedItem?.ToString() ?? "Todos";
            BancosCboCuenta.Items.Clear();
            foreach (var c in _controlador.BancosMetObtenerCuentas(banco))
            {
                BancosCboCuenta.Items.Add(c);
            }
            BancosCboCuenta.SelectedIndex = 0;
        }

        // Botón Consultar 

        private void BancosBtnConsultar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fecha = BancosDtpFechaCorte.Value.Date;
                string banco   = BancosCboBanco.SelectedItem?.ToString() ?? "Todos";
                string cuenta  = BancosCboCuenta.SelectedItem?.ToString() ?? "Todas";
                string moneda  = BancosCboMoneda.SelectedItem?.ToString() ?? "GTQ";

                // Detalle por cuenta
                DataTable dt = _controlador.BancosMetConsultarDisponibilidad(fecha, banco, cuenta, moneda);
                BancosDgvDisponibilidad.DataSource = dt;
                BancosMetFormatearGrillaDisponibilidad();

                // Resumen KPI
                string monedaRes = moneda == "Todas" ? "GTQ" : moneda;
                var res = _controlador.BancosMetCalcularResumen(fecha, banco, cuenta, monedaRes);

                string pref = monedaRes == "GTQ" ? "Q" : (monedaRes == "USD" ? "$" : "€");
                BancosLblValorSaldoInicial.Text = $"{pref}{res.SaldoInicial:N2}";
                BancosLblValorIngresos.Text     = $"{pref}{res.Ingresos:N2}";
                BancosLblValorEgresos.Text      = $"{pref}{res.Egresos:N2}";
                BancosLblValorComprometido.Text = $"{pref}{res.Comprometido:N2}";
                BancosLblValorRestringido.Text  = $"{pref}{res.Restringido:N2}";
                BancosLblValorDisponible.Text   = $"{pref}{res.Disponible:N2}";

                BancosLblMonedaResumen.Text = $"Resumen en {monedaRes}";
                BancosLblStatusCuentas.Text = $"{dt.Rows.Count} cuentas";
                BancosLblStatusMoneda.Text  = $"•   Importes en {monedaRes}";

                // Cargar compromisos y resumen para la cuenta seleccionada
                BancosMetActualizarPanelesInferiores();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar disponibilidad:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Cambio de selección en la tabla principal

        private void BancosDgvDisponibilidad_SelectionChanged(object sender, EventArgs e)
        {
            BancosMetActualizarPanelesInferiores();
        }

        private void BancosMetActualizarPanelesInferiores()
        {
            string cuentaSel = "001-123456-7";
            if (BancosDgvDisponibilidad.SelectedRows.Count > 0)
            {
                var row = BancosDgvDisponibilidad.SelectedRows[0];
                if (row.Cells["Cuenta"] != null && row.Cells["Cuenta"].Value != null)
                {
                    cuentaSel = row.Cells["Cuenta"].Value.ToString();
                }
            }

            // Cargar compromisos
            DataTable dtComp = _controlador.BancosMetConsultarCompromisos(cuentaSel);
            BancosDgvCompromisos.DataSource = dtComp;
            BancosMetFormatearGrillaCompromisos();

            // Cargar resumen de cuenta
            DataTable dtRes = _controlador.BancosMetConsultarResumenCuenta(cuentaSel);
            BancosDgvResumenCuenta.DataSource = dtRes;
            BancosMetFormatearGrillaResumenCuenta();
        }

        // Botón Exportar 

        private void BancosBtnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "La exportación a formatos externos (Excel / PDF) estará disponible al integrar la base de datos MySQL.\n" +
                "Funcionalidad prototipo.",
                "Exportar – Información",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Formateo de DataGridViews

        private void BancosMetFormatearGrillaDisponibilidad()
        {
            BancosDgvDisponibilidad.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BancosDgvDisponibilidad.RowHeadersVisible   = false;
            BancosDgvDisponibilidad.AllowUserToAddRows  = false;
            BancosDgvDisponibilidad.ReadOnly            = true;

            string[] colsNum = { "Saldo Inicial", "Ingresos", "Egresos",
                                  "Comprometido",  "Restringido", "Disponible" };
            foreach (string col in colsNum)
            {
                if (BancosDgvDisponibilidad.Columns.Contains(col))
                {
                    BancosDgvDisponibilidad.Columns[col].DefaultCellStyle.Format    = "N2";
                    BancosDgvDisponibilidad.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            foreach (DataGridViewRow row in BancosDgvDisponibilidad.Rows)
            {
                row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                    ? Color.White
                    : Color.FromArgb(248, 252, 248);
            }
        }

        private void BancosMetFormatearGrillaCompromisos()
        {
            BancosDgvCompromisos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BancosDgvCompromisos.RowHeadersVisible   = false;
            BancosDgvCompromisos.AllowUserToAddRows  = false;
            BancosDgvCompromisos.ReadOnly            = true;

            if (BancosDgvCompromisos.Columns.Contains("Fecha"))
            {
                BancosDgvCompromisos.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }

            if (BancosDgvCompromisos.Columns.Contains("Monto"))
            {
                BancosDgvCompromisos.Columns["Monto"].DefaultCellStyle.Format    = "Q#,##0.00";
                BancosDgvCompromisos.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            foreach (DataGridViewRow row in BancosDgvCompromisos.Rows)
            {
                row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                    ? Color.White
                    : Color.FromArgb(248, 252, 248);
            }
        }

        private void BancosMetFormatearGrillaResumenCuenta()
        {
            BancosDgvResumenCuenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            BancosDgvResumenCuenta.RowHeadersVisible   = false;
            BancosDgvResumenCuenta.AllowUserToAddRows  = false;
            BancosDgvResumenCuenta.ReadOnly            = true;

            if (BancosDgvResumenCuenta.Columns.Contains("Monto"))
            {
                BancosDgvResumenCuenta.Columns["Monto"].DefaultCellStyle.Format    = "Q#,##0.00";
                BancosDgvResumenCuenta.Columns["Monto"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            foreach (DataGridViewRow row in BancosDgvResumenCuenta.Rows)
            {
                string concepto = row.Cells["Concepto"]?.Value?.ToString() ?? "";
                if (concepto.Equals("Disponible", StringComparison.OrdinalIgnoreCase))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(230, 244, 235);
                    row.DefaultCellStyle.ForeColor = Color.FromArgb(28, 74, 48);
                    row.DefaultCellStyle.Font      = new Font("Segoe UI", 9F, FontStyle.Bold);
                }
                else
                {
                    row.DefaultCellStyle.BackColor = row.Index % 2 == 0
                        ? Color.White
                        : Color.FromArgb(248, 252, 248);
                }
            }
        }
    }
}
