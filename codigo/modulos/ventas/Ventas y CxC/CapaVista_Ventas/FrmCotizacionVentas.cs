/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar

 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Fecha de actualización: 17/10/2026
 * Propósito: Prototipo visual estandarizado de cotizaciones, sin lógica de negocio.
 */
using System;
using System.Windows.Forms;

namespace CapaVista_Ventas
{
    public partial class FrmCotizacionVentas : Form
    {
        public FrmCotizacionVentas()
        {
            InitializeComponent();
            VentasCboCotEstado.SelectedIndex = 0;
            VentasMetMostrarVigencia();
        }

        private void VentasCotVigencia_Changed(object sender, EventArgs e)
        {
            VentasMetMostrarVigencia();
        }

        private void VentasMetMostrarVigencia()
        {
            // Estado de la vista: no convierte documentos ni modifica precios o inventarios.
            bool FechaValida = VentasDtpCotVigencia.Value.Date >= DateTime.Today &&
                VentasDtpCotVigencia.Value.Date >= VentasDtpCotFecha.Value.Date;
            bool Vigente = FechaValida && VentasCboCotEstado.Text == "Vigente";
            VentasBtnCotPedido.Enabled = Vigente;
            VentasBtnCotFactura.Enabled = Vigente;
            VentasLblCotVigencia.Text = !FechaValida || VentasCboCotEstado.Text == "Vencida"
                ? "Cotización vencida o fechas no válidas: genere una nueva con los precios actuales."
                : "Una cotización vigente conserva sus precios. Si vence, se genera una nueva con los precios actuales.";
        }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 17/10/2026
