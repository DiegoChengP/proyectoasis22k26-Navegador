/*
 * INICIO DEL CÓDIGO — Jose Torres
 * Carné: 0901-23-1091 | Fecha: 15/10/2026
 * Área: Ventas y cuentas por cobrar
 * Propósito: Presentar el dashboard, sus indicadores y la navegación del módulo de Ventas.
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CapaVista_Ventas
{
    public partial class FrmMdiVentas : Form
    {
        private readonly int _IdUsuario;

        // WM_SETREDRAW agrupa los cambios de tamaño para evitar imágenes superpuestas al contraer el menú.
        private const int _WmSetRedraw = 0x000B;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr Ventana, int Mensaje, IntPtr Valor, IntPtr Datos);

        public FrmMdiVentas()
        {
            InitializeComponent();
            // El doble búfer reduce el parpadeo al redimensionar la ventana.
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
        }

        public FrmMdiVentas(string NombreUsuario, string RolUsuario) : this(NombreUsuario, RolUsuario, 0) { }

        public FrmMdiVentas(string NombreUsuario, string RolUsuario, int IdUsuario) : this()
        {
            _IdUsuario = IdUsuario;
            VentasMetActualizarSesion(NombreUsuario, RolUsuario);
        }

        // Presenta el usuario y el rol cuando otro módulo proporciona esa información.
        public void VentasMetActualizarSesion(string NombreUsuario, string RolUsuario)
        {
            VentasLblUsuario.Text = "Usuario: " + (string.IsNullOrWhiteSpace(NombreUsuario) ? "Sin sesión" : NombreUsuario);
            VentasLblRol.Text = "Rol: " + (string.IsNullOrWhiteSpace(RolUsuario) ? "Sin asignar" : RolUsuario);
        }

        // Actualiza directamente los seis labels del diseñador con datos proporcionados por el controlador.
        public void VentasMetActualizarIndicadores(decimal VentasDia, decimal SaldoCxC, int ClientesActivos,
            int FacturasPendientes, decimal PagosDia, int PedidosPendientes)
        {
            var Cultura = System.Globalization.CultureInfo.GetCultureInfo("es-GT");
            VentasLblValorKpi1.Text = "Q " + VentasDia.ToString("N2", Cultura);
            VentasLblValorKpi2.Text = "Q " + SaldoCxC.ToString("N2", Cultura);
            VentasLblValorKpi3.Text = ClientesActivos.ToString("N0", Cultura);
            VentasLblValorKpi4.Text = FacturasPendientes.ToString("N0", Cultura);
            VentasLblValorKpi5.Text = "Q " + PagosDia.ToString("N2", Cultura);
            VentasLblValorKpi6.Text = PedidosPendientes.ToString("N0", Cultura);
        }

        private void VentasMetBtnBurgerClick(object Origen, EventArgs Evento)
        {
            // Como en Seguridad: el panel pasa de 270 a 64 px y el contenido cambia de posición.
            // Los botones conservan su ancho, Image y Text; el panel recorta la parte que queda fuera.
            bool Contraer = VentasPnlNavegador.Width == 270;
            SuspendLayout();
            VentasPnlPrincipal.SuspendLayout();
            SendMessage(Handle, _WmSetRedraw, IntPtr.Zero, IntPtr.Zero);
            try
            {
                VentasPnlNavegador.Width = Contraer ? 64 : 270;
                VentasTlpContenido.Left = Contraer ? 200 : 300;
                VentasTlpContenido.Width = VentasPnlPrincipal.ClientSize.Width - VentasTlpContenido.Left - 14;
                VentasBtnBurger.AccessibleName = Contraer ? "Expandir menú" : "Contraer menú";
            }
            finally
            {
                VentasPnlPrincipal.ResumeLayout(true);
                ResumeLayout(true);
                SendMessage(Handle, _WmSetRedraw, new IntPtr(1), IntPtr.Zero);
                // Repinta también los controles hijos una sola vez al terminar el cambio.
                Invalidate(true);
                Update();
            }
        }

        private void VentasMetAplicarEstadoBoton(Button Boton, bool Sobre)
        {
            bool Activo = Boton == VentasBtnInicio;
            // Efecto de difuminado (hover): al pasar el cursor se sustituye el fondo por un tono
            // morado más suave. No altera el icono Image ni el texto Text del botón.
            Boton.BackgroundImage = Activo ? CapaVista_Ventas.Properties.Resources.BotonMenuActivoVentas :
                (Sobre ? CapaVista_Ventas.Properties.Resources.BotonMenuHoverVentas : CapaVista_Ventas.Properties.Resources.BotonMenuNormalVentas);
            Boton.ForeColor = Activo ? Color.FromArgb(59, 29, 74) : Color.FromArgb(248, 245, 249);
        }

        private void VentasMetBtnOpcionMouseEnter(object Origen, EventArgs Evento)
        { VentasMetAplicarEstadoBoton((Button)Origen, true); }

        private void VentasMetBtnOpcionMouseLeave(object Origen, EventArgs Evento)
        { VentasMetAplicarEstadoBoton((Button)Origen, false); }

        // Inicio conserva la pantalla principal del módulo.
        private void VentasMetBtnInicioClick(object Origen, EventArgs Evento)
        {
            // Aquí se puede agregar la acción específica del botón Inicio.
        }

        // Abre el listado de precios de escritorio sobre el MDI.
        private void VentasMetBtnListadoPreciosClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_ListadoPrecios.FrmListadoPrecios())
                Formulario.ShowDialog(this);
        }

        // Abre la opción de movimientos de clientes.
        private void VentasMetBtnMovimientosClientesClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_MovimientosClientes.FrmMovimientosClientesVentas() { VentasIdUsuario = _IdUsuario })
                Formulario.ShowDialog(this);
        }

        // Abre la opción de recepción de pagos.
        private void VentasMetBtnRecepcionPagosClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_MovimientosClientes.FrmMovimientosClientesVentas() { VentasIdUsuario = _IdUsuario })
            {
                Formulario.VentasMetMostrarRecepcionPagos();
                Formulario.ShowDialog(this);
            }
        }

        // Abre la opción de reportes de Ventas.
        private void VentasMetBtnReportesClick(object Origen, EventArgs Evento)
        {
            CapaVista_Reportes.ClsDocumentoAntiguedad.VentasMetAbrir(this);
        }

        // Abre el formulario independiente de cotizaciones.
        private void VentasMetBtnCotizacionesClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_Cotizaciones.FrmCotizacionVentas() { VentasIdUsuario = _IdUsuario })
                Formulario.ShowDialog(this);
        }

        // Abre el formulario independiente de pedidos.
        private void VentasMetBtnPedidosClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_Pedidos.FrmPedidoVentas())
                Formulario.ShowDialog(this);
        }

        // Abre el formulario independiente de facturas.
        private void VentasMetBtnFacturasClick(object Origen, EventArgs Evento)
        {
            new CapaVista_Facturas.FrmFacturas().Show(this);
        }

        // Abre la opción de comisiones de vendedores.
        private void VentasMetBtnComisionesVendedoresClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_ComisionVendedores.FrmComisionesVendedoresVentas())
                Formulario.ShowDialog(this);
        }

        // Abre la opción de mantenimientos del módulo.
        private void VentasMetBtnMantenimientosClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_Mantenimientos.FrmMantenimientosVentas())
                Formulario.ShowDialog(this);
        }

        // Abre la opción de cierre de movimientos.
        private void VentasMetBtnCierreMovimientosClick(object Origen, EventArgs Evento)
        {
            using (var Formulario = new CapaVista_CierreMovimientos.FrmCierreMovimientosVentas() { VentasIdUsuario = _IdUsuario })
                Formulario.ShowDialog(this);
        }

        // Confirma la salida del módulo y cierra su ventana principal.
        private void VentasMetBtnSalirClick(object Origen, EventArgs Evento)
        {
            if (MessageBox.Show(this, "¿Desea salir del módulo de Ventas?", "Salir de Ventas",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void VentasMetBtnAyudaClick(object Origen, EventArgs Evento)
        {
            MessageBox.Show(this, "Seleccione una opción del menú lateral. El botón de menú contrae o expande la navegación.\n\n" +
                "Los indicadores se mostrarán cuando el controlador proporcione información. Para salir utilice Salir.",
                "Ayuda de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

// FIN DEL CÓDIGO — Jose Torres | Carné: 0901-23-1091 | Fecha: 15/10/2026
