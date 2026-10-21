/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Fecha de actualización: 17/10/2026
 * Propósito: Operar CxC, registrar documentos y recibos y preparar depósitos con persistencia transaccional.
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
            VentasMetConectarEventos();
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
            if (!_Cargando && _Inicializado) VentasPresentacion.ClsVistaVentas.Ejecutar(this, VentasMetConsultarActual);
        }

        private void VentasMetActualizarContexto()
        {
            // Un depósito agrupa recibos de distintos clientes; no hereda el filtro individual.
            VentasPnlCliente.Enabled = VentasTbcMovimientos.SelectedIndex < 3;
            VentasMetRefrescarBotones();
        }

        private void VentasCboTipoDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Presenta el efecto y los campos requeridos del documento.
            int Tipo = VentasCboTipoDocumento.SelectedIndex;
            VentasCboEstablecimientoDocumento.Enabled = Tipo == 0;
            VentasTxtFacturaReferencia.Enabled = Tipo > 0;
            VentasTxtNaturalezaDocumento.Text = Tipo < 0 ? string.Empty :
                (Tipo == 1 || Tipo == 4 ? "Abono - disminuye la deuda" : "Cargo - aumenta la deuda");
        }

        // Entrada de consulta de recibos y depósitos para la integración con Bancos.
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
