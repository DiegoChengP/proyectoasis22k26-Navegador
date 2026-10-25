using CapaControlador_Seguridad.Objetos_de_valor;
using CapaVista_Navegador;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CapaVista_Compras
{
    public partial class FrmMDICompras : Form
    {
        private ClsTransicionNavegador _ClsTransicionNavegador;
        private Form _FormularioSubproceso;
        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras _OpcionSubproceso;
        private bool _ContenidoOculto;
        private bool _NavegadorContraido;
        private bool _PantallaCompleta;
        private FormBorderStyle _BordeAnterior;
        private FormWindowState _EstadoAnterior;
        private System.Drawing.Rectangle _LimitesAnteriores;
        private readonly Dictionary<TableLayoutPanel, ColumnStyle> _ColumnasNavegador =
            new Dictionary<TableLayoutPanel, ColumnStyle>();

        public FrmMDICompras()
        {
            InitializeComponent();
            DoubleBuffered = true;
            foreach (Control ControlActual in ComprasTlpNavegador.Controls)
            {
                CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras Panel = ControlActual as CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras;
                if (Panel != null) ComprasMetConfigurarEstadosTab(Panel, Panel);
            }
            ComprasTlpPresentacion.SizeChanged += (Origen, Argumentos) => ComprasMetAjustarPresentacion();
            ComprasMetAjustarPresentacion();
            ComprasMetActualizarInfoUsuario();
            TableLayoutPanel[] PanelesIndicadores = { ComprasTlpKpi1, ComprasTlpKpi2, ComprasTlpKpi3, ComprasTlpKpi4, ComprasTlpKpi5, ComprasTlpKpi6 };
            foreach (TableLayoutPanel Panel in PanelesIndicadores)
            {
                Panel.RowStyles[0].Height = 56F;
                Panel.RowStyles[1].Height = 28F;
                foreach (Control Elemento in Panel.Controls)
                    if (Panel.GetRow(Elemento) == 2) Elemento.Margin = new Padding(2);
            }
        }

        protected override bool ProcessCmdKey(ref Message Mensaje, Keys Teclas)
        {
            if (Teclas != (Keys.Alt | Keys.Enter)) return base.ProcessCmdKey(ref Mensaje, Teclas);
            if ((Mensaje.LParam.ToInt64() & (1L << 30)) != 0) return true;
            ComprasMetAlternarPantallaCompleta();
            return true;
        }

        private void ComprasMetAlternarPantallaCompleta()
        {
            SuspendLayout();
            try
            {
                if (!_PantallaCompleta)
                {
                    _BordeAnterior = FormBorderStyle;
                    _EstadoAnterior = WindowState;
                    _LimitesAnteriores = WindowState == FormWindowState.Normal ? Bounds : RestoreBounds;
                    System.Drawing.Rectangle Pantalla = Screen.FromControl(this).WorkingArea;
                    WindowState = FormWindowState.Normal;
                    FormBorderStyle = FormBorderStyle.None;
                    Bounds = Pantalla;
                    _PantallaCompleta = true;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                    FormBorderStyle = _BordeAnterior;
                    Bounds = _LimitesAnteriores;
                    WindowState = _EstadoAnterior;
                    _PantallaCompleta = false;
                }
            }
            finally
            {
                ResumeLayout(true);
                ComprasMetAjustarDashboard();
            }
        }

        protected override void OnLoad(EventArgs Evento)
        {
            base.OnLoad(Evento);
            if (DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
            ComprasMetAjustarDashboard();
            ComprasTlpPrincipal.PerformLayout();
            ComprasPnlMain.PerformLayout();
            ComprasMetActualizarInfoUsuario();
        }

        private void ComprasMetConfigurarEstadosTab(Control Elemento, CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras Panel)
        {
            Elemento.MouseEnter += (Origen, Argumentos) => Panel.ComprasProcActualizarHover(true);
            Elemento.MouseLeave += (Origen, Argumentos) =>
            {
                if (!IsHandleCreated || IsDisposed || Disposing) return;
                BeginInvoke(new Action(() =>
                {
                    if (Panel.IsDisposed || IsDisposed || Disposing) return;
                    Panel.ComprasProcActualizarHover(Panel.ClientRectangle.Contains(Panel.PointToClient(Cursor.Position)));
                }));
            };
            foreach (Control Hijo in Elemento.Controls) ComprasMetConfigurarEstadosTab(Hijo, Panel);
        }

        private void ComprasMetActualizarInfoUsuario()
        {
            string Usuario = ClsSesionSeguridad.HaySesionActiva
                ? ClsSesionSeguridad.NombreUsuario : "Sin sesión";
            string Rol = ClsSesionSeguridad.HaySesionActiva
                ? ClsSesionSeguridad.SeguridadMetRolesComoTexto() : string.Empty;
            ComprasLblUsuario.Texto = "Usuario: " + Usuario;
            ComprasLblRol.Texto = "Rol: " + Rol;
            ComprasLblUsuario.Width = TextRenderer.MeasureText(ComprasLblUsuario.Texto, ComprasLblUsuario.Font).Width + 16;
            ComprasLblRol.Width = TextRenderer.MeasureText(ComprasLblRol.Texto, ComprasLblRol.Font).Width + 16;
        }

        private void ComprasMetAbrirSubproceso<T>(CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras Opcion) where T : Form, new()
        {
            if (IsDisposed || Disposing) return;
            if (_FormularioSubproceso != null && !_FormularioSubproceso.IsDisposed)
            {
                if (_FormularioSubproceso is T)
                {
                    if (_FormularioSubproceso.WindowState == FormWindowState.Minimized)
                        _FormularioSubproceso.WindowState = FormWindowState.Normal;
                    _FormularioSubproceso.Activate();
                    return;
                }
                _FormularioSubproceso.Close();
                if (_FormularioSubproceso != null && !_FormularioSubproceso.IsDisposed) return;
            }

            T Formulario = new T();
            try
            {
                Formulario.StartPosition = FormStartPosition.CenterParent;
                Formulario.ShowInTaskbar = false;
                Formulario.MaximizeBox = false;
                Formulario.MinimizeBox = true;
                Formulario.FormClosed += ComprasMetSubprocesoCerrado;
                _FormularioSubproceso = Formulario;
                _OpcionSubproceso = Opcion;
                ComprasMetActualizarSeleccionSubproceso();
                Formulario.ShowDialog(this);
            }
            finally
            {
                Formulario.FormClosed -= ComprasMetSubprocesoCerrado;
                Formulario.Dispose();
                _FormularioSubproceso = null;
                _OpcionSubproceso = null;
                ComprasMetActualizarSeleccionSubproceso();
            }
        }

        private void ComprasMetActualizarSeleccionSubproceso()
        {
            if (IsDisposed || Disposing) return;
            foreach (Control ControlActual in ComprasTlpNavegador.Controls)
            {
                var Opcion = ControlActual as CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras;
                if (Opcion == null) continue;
                Opcion.Seleccionado = ReferenceEquals(Opcion, _OpcionSubproceso);
                if (!Opcion.Seleccionado) Opcion.ComprasProcActualizarHover(false);
            }
        }

        private void ComprasMetSubprocesoCerrado(object Remitente, FormClosedEventArgs Evento)
        {
            Form Formulario = (Form)Remitente;
            Formulario.FormClosed -= ComprasMetSubprocesoCerrado;
            if (!ReferenceEquals(_FormularioSubproceso, Formulario)) return;
            _FormularioSubproceso = null;
            _OpcionSubproceso = null;
            ComprasMetActualizarSeleccionSubproceso();
        }

        private void ComprasMetAbrirMovimientosProveedores(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_MovimientosProveedores.FrmMovimientosProveedores>(ComprasTlpMovimientosProveedores);
        }

        private void ComprasMetAbrirPolizas(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_Polizas.FrmPolizas>(ComprasTlpPolizas);
        }

        private void ComprasMetAbrirOrdenesCompra(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_OrdenesCompra.FrmOrdenesCompra>(ComprasTlpOrdenesCompra);
        }

        private void ComprasMetAbrirPagosProveedores(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_PagosProveedores.FrmPagosProveedores>(ComprasTlpPagosProveedores);
        }

        private void ComprasMetAbrirAprobacionFinanciera(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_AprobacionFinanciera.FrmAprobacionFinanciera>(ComprasTlpAprobacionFinanciera);
        }

        private void ComprasMetAbrirCierreMovimientos(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<CapaVista_CierreMovimientos.FrmCierreMovimientos>(ComprasTlpCierreMovimientos);
        }

        private void ComprasMetAbrirProveedores(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<FrmMantenimientoProveedor>(ComprasTlpProveedores);
        }

        private void ComprasMetAbrirMetodosPago(object Remitente, EventArgs Evento)
        {
            ComprasMetAbrirSubproceso<FrmMantenimientoMetodoPago>(ComprasTlpMetodosPago);
        }

        private void ComprasMetOpcionPendienteClick(object Remitente, EventArgs Evento)
        {
            // Punto de integracion existente para mantenimientos y bitacora.
        }

        private void ComprasBtnSalir_Click(object Remitente, EventArgs Evento)
        {
            Close();
        }

        private async void ComprasBtnBurger_Click(object Remitente, EventArgs Evento)
        {
            if (_ContenidoOculto || _ClsTransicionNavegador != null) return;
            _ContenidoOculto = true;
            bool EncabezadoVisible = ComprasFlpEncabezado.Visible;
            bool ContenidoVisible = ComprasPnlMain.Visible;
            var Tiempo = System.Diagnostics.Stopwatch.StartNew();
            ComprasTlpPrincipal.SuspendLayout();
            try
            {
                ComprasFlpEncabezado.Visible = false;
                ComprasPnlMain.Visible = false;
            }
            finally
            {
                ComprasTlpPrincipal.ResumeLayout(true);
            }
            try
            {
                ComprasTlpPrincipal.Refresh();
                ComprasMetCambiarNavegador();
                int Espera = Math.Max(1, 500 - (int)Tiempo.ElapsedMilliseconds);
                await System.Threading.Tasks.Task.Delay(Espera);
            }
            finally
            {
                if (!IsDisposed && !Disposing)
                {
                    using (var Cubierta = new Panel())
                    {
                        Cubierta.BackColor = ComprasTlpPrincipal.BackColor;
                        int Izquierda = ComprasTlpPrincipal.Left + ComprasTlpPrincipal.Padding.Left
                            + (int)ComprasTlpPrincipal.ColumnStyles[0].Width;
                        Cubierta.Bounds = new System.Drawing.Rectangle(Izquierda, ComprasTlpPrincipal.Top,
                            Math.Max(0, ComprasTlpPrincipal.Right - Izquierda), ComprasTlpPrincipal.Height);
                        Controls.Add(Cubierta);
                        Cubierta.BringToFront();
                        Cubierta.Refresh();
                        ComprasTlpPrincipal.SuspendLayout();
                        try
                        {
                            ComprasFlpEncabezado.Visible = EncabezadoVisible;
                            ComprasPnlMain.Visible = ContenidoVisible;
                        }
                        finally
                        {
                            ComprasTlpPrincipal.ResumeLayout(true);
                        }
                        ComprasPnlMain.PerformLayout();
                        ComprasMetAjustarDashboard();
                        ComprasFlpEncabezado.Refresh();
                        ComprasPnlMain.Refresh();
                        Controls.Remove(Cubierta);
                        ComprasTlpPrincipal.Refresh();
                    }
                }
                _ContenidoOculto = false;
            }
        }

        private void ComprasMetCambiarNavegador()
        {
            bool Contraer = !_NavegadorContraido;
            int AnchoAnterior = ComprasTlpNavegador.Width;
            int AnchoFinal = Contraer ? 64 + ComprasTlpNavegador.Padding.Horizontal : 270;
            System.Drawing.Bitmap ImagenMenu = null;
            if (Contraer)
            {
                ImagenMenu = new System.Drawing.Bitmap(ComprasTlpNavegador.Width, ComprasTlpNavegador.Height);
                ComprasTlpNavegador.DrawToBitmap(ImagenMenu, ComprasTlpNavegador.ClientRectangle);
            }
            _NavegadorContraido = !_NavegadorContraido;
            ComprasTlpPrincipal.SuspendLayout();
            ComprasTlpNavegador.SuspendLayout();
            try
            {
                ComprasMetAlternarSegundaColumna(ComprasTlpNavegador);
                foreach (Control ControlActual in ComprasTlpNavegador.Controls)
                {
                    TableLayoutPanel Panel = ControlActual as TableLayoutPanel;
                    if (Panel != null) ComprasMetAlternarSegundaColumna(Panel);
                }
                ComprasTlpNavegador.Width = _NavegadorContraido
                    ? 64 + ComprasTlpNavegador.Padding.Horizontal : 270;
                ComprasTlpPrincipal.ColumnStyles[0].Width = _NavegadorContraido
                    ? ComprasTlpNavegador.Width + 16 : 286F;
                ComprasBtnAlternarNavegador.AccessibleName = _NavegadorContraido
                    ? "Expandir menú" : "Contraer menú";
            }
            finally
            {
                ComprasTlpNavegador.ResumeLayout(true);
                ComprasTlpPrincipal.ResumeLayout(true);
                if (ImagenMenu == null)
                {
                    ImagenMenu = new System.Drawing.Bitmap(ComprasTlpNavegador.Width, ComprasTlpNavegador.Height);
                    ComprasTlpNavegador.DrawToBitmap(ImagenMenu, ComprasTlpNavegador.ClientRectangle);
                }
                var Cubierta = new ClsTransicionNavegador(ImagenMenu, AnchoAnterior, AnchoFinal);
                _ClsTransicionNavegador = Cubierta;
                Cubierta.Disposed += (Origen, Argumentos) => _ClsTransicionNavegador = null;
                Cubierta.Location = PointToClient(ComprasTlpNavegador.PointToScreen(System.Drawing.Point.Empty));
                Cubierta.Size = new System.Drawing.Size(Math.Max(AnchoAnterior, AnchoFinal), ComprasTlpNavegador.Height);
                Cubierta.BackColor = ComprasTlpPrincipal.BackColor;
                Controls.Add(Cubierta);
                Cubierta.BringToFront();
                Cubierta.Iniciar();
            }
        }

        private sealed class ClsTransicionNavegador : Control
        {
            private readonly System.Drawing.Bitmap _Menu;
            private readonly Timer _Reloj = new Timer { Interval = 15 };
            private readonly System.Diagnostics.Stopwatch _Tiempo = new System.Diagnostics.Stopwatch();
            private readonly int _Desde, _Hasta;
            private int _AnchoVisible;

            internal ClsTransicionNavegador(System.Drawing.Bitmap Menu, int Desde, int Hasta)
            {
                _Menu = Menu;
                _Desde = Desde;
                _Hasta = Hasta;
                _AnchoVisible = Desde;
                SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                    | ControlStyles.OptimizedDoubleBuffer | ControlStyles.Opaque, true);
                _Reloj.Tick += (Origen, Argumentos) =>
                {
                    double Paso = Math.Min(1D, _Tiempo.Elapsed.TotalMilliseconds / 260D);
                    if (Paso >= 1D)
                    {
                        Dispose();
                        return;
                    }
                    Paso = Paso * Paso * Paso * (Paso * (Paso * 6D - 15D) + 10D);
                    _AnchoVisible = (int)Math.Round(_Desde + (_Hasta - _Desde) * Paso);
                    Invalidate();
                };
            }

            internal void Iniciar()
            {
                Refresh();
                _Tiempo.Start();
                _Reloj.Start();
            }

            protected override void OnPaintBackground(PaintEventArgs Evento) { }

            protected override void OnPaint(PaintEventArgs Evento)
            {
                Evento.Graphics.Clear(BackColor);
                var Estado = Evento.Graphics.Save();
                Evento.Graphics.SetClip(new System.Drawing.Rectangle(0, 0, _AnchoVisible, Height));
                Evento.Graphics.DrawImageUnscaled(_Menu, 0, 0);
                Evento.Graphics.Restore(Estado);
            }

            protected override void Dispose(bool Disposing)
            {
                if (Disposing)
                {
                    _Reloj.Dispose();
                    _Menu.Dispose();
                }
                base.Dispose(Disposing);
            }
        }

        private void ComprasMetAjustarDashboard()
        {
            ComprasPnlDashboard.PerformLayout();
            ComprasTlpDashboard.PerformLayout();
            ComprasTlpHero.PerformLayout();
            ComprasMetAjustarPresentacion();
        }

        private void ComprasMetAjustarPresentacion()
        {
            bool Compacto = ComprasTlpPresentacion.Height < 320;
            RowStyle Fila = ComprasTlpPresentacion.RowStyles[1];
            SizeType Tipo = Compacto ? SizeType.Absolute : SizeType.Percent;
            float Altura = Compacto ? 64F : 22F;
            if (Fila.SizeType == Tipo && Fila.Height == Altura) return;
            ComprasTlpPresentacion.SuspendLayout();
            try
            {
                Fila.SizeType = Tipo;
                Fila.Height = Altura;
            }
            finally
            {
                ComprasTlpPresentacion.ResumeLayout(true);
            }
        }

        private void ComprasMetAlternarSegundaColumna(TableLayoutPanel Panel)
        {
            if (Panel.ColumnCount < 2 || Panel.ColumnStyles.Count < 2) return;
            Panel.SuspendLayout();
            try
            {
                ColumnStyle Columna = Panel.ColumnStyles[1];
                if (_NavegadorContraido)
                {
                    _ColumnasNavegador[Panel] = new ColumnStyle(Columna.SizeType, Columna.Width);
                    foreach (Control ControlActual in Panel.Controls)
                        if (Panel.GetColumn(ControlActual) == 1) ControlActual.Visible = false;
                    Columna.SizeType = SizeType.Absolute;
                    Columna.Width = 0;
                }
                else
                {
                    ColumnStyle Original;
                    if (!_ColumnasNavegador.TryGetValue(Panel, out Original)) return;
                    Columna.SizeType = Original.SizeType;
                    Columna.Width = Original.Width;
                    foreach (Control ControlActual in Panel.Controls)
                        if (Panel.GetColumn(ControlActual) == 1) ControlActual.Visible = true;
                    _ColumnasNavegador.Remove(Panel);
                }
            }
            finally
            {
                Panel.ResumeLayout(true);
            }
        }
    }
}
