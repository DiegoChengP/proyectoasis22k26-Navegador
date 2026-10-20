/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Fecha de actualización: 17/10/2026
 * Propósito: Unificar el prototipo visual de CxC, recepción de pagos y preparación de depósitos.
 */
using System;
using System.Windows.Forms;

namespace CapaVista_MovimientosClientes
{
    public partial class FrmMovimientosClientesVentas : Form
    {
        public FrmMovimientosClientesVentas()
        {
            InitializeComponent();
        }

        // Los dos accesos del MDI comparten los mismos controles y el mismo diseño.
        public void VentasMetMostrarRecepcionPagos()
        {
            VentasTbcMovimientos.SelectedTab = VentasTbpRecibos;
            VentasMetActualizarContexto();
        }

        private void VentasTbcMovimientos_SelectedIndexChanged(object sender, EventArgs e)
        {
            VentasMetActualizarContexto();
        }

        private void VentasMetActualizarContexto()
        {
            // Un depósito agrupa recibos de distintos clientes; no hereda el filtro individual.
            bool EsConsulta = VentasTbcMovimientos.SelectedTab == VentasTbpConsultaRecibos;
            VentasPnlCliente.Enabled = VentasTbcMovimientos.SelectedIndex < 3;
            VentasBtnIngresar.Enabled = !EsConsulta;
            VentasBtnGuardar.Enabled = !EsConsulta;
            VentasBtnCancelar.Enabled = !EsConsulta;
        }

        private void VentasCboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Presenta el efecto del tipo elegido; no registra ni calcula movimientos.
            int Tipo = VentasCboTipoDocumento.SelectedIndex;
            VentasTxtNaturalezaDocumento.Text = Tipo < 0 ? string.Empty :
                (Tipo == 1 || Tipo == 4 ? "Abono - disminuye la deuda" : "Cargo - aumenta la deuda");
        }

        // Punto de entrada visual para la consulta integrada solicitada por Bancos.
        // Recibe referencias para la pantalla; no consulta ni confirma operaciones bancarias.
        public void VentasMetMostrarConsultaRecibos(string ReferenciaRecibo, string CodigoDeposito)
        {
            VentasTxtConsultaRecibo.Text = ReferenciaRecibo ?? string.Empty;
            VentasTxtConsultaDeposito.Text = CodigoDeposito ?? string.Empty;
            VentasTbcMovimientos.SelectedTab = VentasTbpConsultaRecibos;
            VentasMetActualizarContexto();
        }
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 17/10/2026
