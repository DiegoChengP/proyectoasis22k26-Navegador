/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar

 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Fecha de actualización: 17/10/2026
 * Propósito: Cotizaciones con precios históricos, validaciones y conversión documental.
 */
using System;
using System.Windows.Forms;

namespace CapaVista_Cotizaciones
{
    public partial class FrmCotizacionVentas : Form
    {
        public FrmCotizacionVentas()
        {
            InitializeComponent();
            VentasMetConectarEventos();
            VentasCboCotEstado.SelectedIndex = 0;
            VentasMetMostrarVigencia();
        }

        private void VentasCotVigencia_Changed(object sender, EventArgs e)
        {
            VentasMetMostrarVigencia();
        }

        private void VentasMetMostrarVigencia()
        {
            // La autorización definitiva también se verifica en Modelo al guardar o convertir.
            bool FechaValida = VentasDtpCotVigencia.Value.Date >= DateTime.Today &&
                VentasDtpCotVigencia.Value.Date >= VentasDtpCotFecha.Value.Date;
            bool Vigente = FechaValida && VentasCboCotEstado.Text == "Vigente";
            VentasBtnCotPedido.Enabled = Vigente;
            VentasBtnCotFactura.Enabled = Vigente;
            VentasLblCotVigencia.Text = !FechaValida || VentasCboCotEstado.Text == "Vencida"
                ? "Cotización vencida o fechas no válidas: genere una nueva con los precios actuales."
                : "Una cotización vigente conserva sus precios. Si vence, se genera una nueva con los precios actuales.";
            VentasMetActualizarEdicion();
        }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 17/10/2026
