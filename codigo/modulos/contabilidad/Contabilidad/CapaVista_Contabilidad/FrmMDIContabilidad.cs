/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 09/10/2026
 * ==================================================================
 * Propósito :
 *  MDI de prototipo no funcional del módulo de Contabilidad. Replica
 *  la estructura del MDI de Seguridad (menú lateral con logo y botones,
 *  encabezado con usuario y rol, bienvenida con Don Buti, botón de
 *  ayuda, resumen del sistema con 6 KPIs, información relevante y nota
 *  de integración) con la paleta de Contabilidad.
 * Reglas especificas:
 *  El MDI lee la sesión y los permisos mediante el Controlador; no toca el Modelo.
 *  Las cinco pantallas son ventanas modales con su propio controlador.
 *  Los indicadores son valores de presentación.
 *  Los procesos contables reales y el Reporteador todavía no se implementan.
 * ===================================================================
*/

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Forms;
using CapaControlador_Contabilidad;

namespace CapaVista_Contabilidad
{
    // Ventana principal del módulo.
    public partial class FrmMDIContabilidad : Form
    {
        // Las coordenadas y tamaños son Int32 porque la API de WinForms los exige.
        // Las proporciones se calculan en Double; no hay cálculos monetarios en la Vista.

        // Módulo ERP dueño de las pólizas: filtra las cuentas que se pueden usar (1 = Contabilidad).
        private readonly int _IdModulo;
        private const int _AnchoMenuExpandido = 270;
        private const int _AnchoMenuContraido = 64;
        private const int _MargenMenu = 32;      // 16 px a cada lado del panel del menú
        private const int _MargenDashboard = 44;
        private const int _AltoKpiBase = 189;
        private const int _AnchoKpiBase = 172;
        // Botón de la ventana abierta.
        private Button _BotonActivo;
        // Botón bajo el mouse.
        private Button _BotonSobre;
        // Último estado de sesión y permisos.
        private ClsEstadoSeguridadContabilidad _Estado;
        // Maneja el acceso del menú.
        private readonly ClsPermisosMenuContabilidad _PermisosMenu = new ClsPermisosMenuContabilidad();
        // Botones del submenú Catálogos (códigos 10011 a 10015).
        private Button[] _BotonesCatalogos;
        // Indica si el submenú Catálogos está desplegado.
        private bool _CatalogosAbierto;
        // Botón de cada código de ventana.
        private readonly Dictionary<int, Button> _BotonesMenu;

        // Constructor sin parámetro: lo usa Seguridad al abrir el MDI y el diseñador.
        public FrmMDIContabilidad() : this(1)
        {
        }

        // Prepara el menú, los paneles y los eventos para un módulo ERP.
        public FrmMDIContabilidad(int IdModulo)
        {
            _IdModulo = IdModulo;
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetAplicarTipografia(this);
            _BotonesMenu = new Dictionary<int, Button>
            {
                { 10001, ContabilidadBtnPolizasLocales }, { 10002, ContabilidadBtnPresupuestos },
                { 10003, ContabilidadBtnActivosFijos }, { 10004, ContabilidadBtnCierreContable },
                { 10005, ContabilidadBtnEstadosFinancieros },
                { 10011, ContabilidadBtnPaises },
                { 10012, ContabilidadBtnModulosContables },
                { 10013, ContabilidadBtnVidaUtil },
                { 10014, ContabilidadBtnCuentas },
                { 10015, ContabilidadBtnCuentasModulo }
            };
            _BotonesCatalogos = new[] { ContabilidadBtnPaises, ContabilidadBtnModulosContables, ContabilidadBtnVidaUtil,
                ContabilidadBtnCuentas, ContabilidadBtnCuentasModulo };
            foreach (Button Boton in _BotonesMenu.Values) _PermisosMenu.ContabilidadMetAplicar(Boton, false);
            _PermisosMenu.ContabilidadMetAplicar(ContabilidadBtnCatalogos, false);
            Disposed += (Remitente, Evento) => _PermisosMenu.Dispose();
            ContabilidadPnlPrincipal.Resize += (Remitente, Evento) => ContabilidadMetAcomodarEncabezado();
            this.WindowState = FormWindowState.Maximized;

            ContabilidadMetDobleBuffer(ContabilidadPnlFondo);
            ContabilidadMetDobleBuffer(ContabilidadPnlPrincipal);
            ContabilidadMetDobleBuffer(ContabilidadPnlDashboard);
            ContabilidadMetDobleBuffer(ContabilidadPnlNavegador);
            ContabilidadMetDobleBuffer(ContabilidadPnlInfo);

            this.Load += ContabilidadMetCargarFormulario;
            ContabilidadMetConfigurarEstadosMenu();
            ContabilidadPnlDashboard.Resize += (Remitente, Evento) => ContabilidadMetAcomodarDashboard();
            ContabilidadPnlInfo.Resize += (Remitente, Evento) => ContabilidadMetAcomodarInfo();
        }

        // Carga la seguridad y acomoda el dashboard.
        private void ContabilidadMetCargarFormulario(object Remitente, EventArgs Evento)
        {
            ContabilidadMetActualizarSeguridad(true);
            ContabilidadMetAcomodarDashboard();
        }

        // Seguridad lo llama al abrir el MDI con el empleado y los roles de la sesión.
        public void ContabilidadMetActualizarInfoUsuario(string NombreEmpleado, string Roles)
        {
            ContabilidadLblUsuario.Text = "Usuario: " + NombreEmpleado;
            ContabilidadLblUsuarioRol.Text = "Rol: " + Roles;
            ContabilidadMetAcomodarEncabezado();
        }

        // Vuelve a consultar la sesión y los permisos de Seguridad.
        public void ContabilidadMetActualizarSeguridad(bool MostrarAviso = false)
        {
            ClsEstadoSeguridadContabilidad Estado = new ClsControladorSeguridadContabilidad().ContabilidadFuncObtenerEstado();
            _Estado = Estado;
            ContabilidadLblUsuario.Text = "Usuario: " + Estado.NombreUsuario;
            ContabilidadLblUsuarioRol.Text = "Rol: " + Estado.Roles;
            foreach (KeyValuePair<int, Button> Boton in _BotonesMenu)
                _PermisosMenu.ContabilidadMetAplicar(Boton.Value, Estado.Permisos[Boton.Key].TieneAcceso);
            bool AlgunCatalogo = false;
            foreach (int Codigo in new[] { 10011, 10012, 10013, 10014, 10015 }) AlgunCatalogo |= Estado.Permisos[Codigo].TieneAcceso;
            _PermisosMenu.ContabilidadMetAplicar(ContabilidadBtnCatalogos, AlgunCatalogo);
            if (!AlgunCatalogo) _CatalogosAbierto = false;
            ContabilidadMetAcomodarCatalogos();
            if (_BotonActivo != null && !_BotonActivo.Enabled) _BotonActivo = null;
            _BotonSobre = null;
            ContabilidadMetAcomodarEncabezado();
            if (MostrarAviso && !string.IsNullOrWhiteSpace(Estado.Mensaje))
                MessageBox.Show(this, Estado.Mensaje, "Seguridad de Contabilidad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Reparte el ancho del encabezado.
        private void ContabilidadMetAcomodarEncabezado()
        {
            int AnchoDisponible = Math.Max(0, ContabilidadPnlPrincipal.ClientSize.Width - ContabilidadLblUsuario.Left - 24);
            int AnchoUsuario = Math.Max(0, (AnchoDisponible - 24) / 2);
            ContabilidadLblUsuario.AutoSize = false;
            ContabilidadLblUsuario.AutoEllipsis = true;
            ContabilidadLblUsuario.Width = AnchoUsuario;
            ContabilidadLblUsuarioRol.AutoSize = false;
            ContabilidadLblUsuarioRol.AutoEllipsis = true;
            ContabilidadLblUsuarioRol.Left = ContabilidadLblUsuario.Right + 24;
            ContabilidadLblUsuarioRol.Width = Math.Max(0, AnchoDisponible - AnchoUsuario - 24);
        }

        // Reacomoda el contenido del dashboard cuando cambia el tamaño de la ventana: Don Buti, el
        // resumen y la fila de KPIs se reparten según el espacio disponible.
        private void ContabilidadMetAcomodarDashboard()
        {
            int Ancho = ContabilidadPnlDashboard.ClientSize.Width;
            int Alto = ContabilidadPnlDashboard.ClientSize.Height;
            if (Ancho <= 0 || Alto <= 0)
            {
                return;
            }

            int AltoInfo = ContabilidadPnlInfo.Height;
            int PosicionSuperiorInfo = Alto - 24 - AltoInfo;
            ContabilidadPnlInfo.SetBounds(_MargenDashboard, PosicionSuperiorInfo, Ancho - 2 * _MargenDashboard, AltoInfo);

            bool Compacto = Alto < 720;
            int AnchoKpi = Math.Max(100, Math.Min(190, (Ancho - 2 * _MargenDashboard - 5 * 12) / 6));
            // Al colapsar el menú en una laptop, el ancho adicional no debe hacer
            // crecer las tarjetas hacia la bienvenida y el botón de Ayuda.
            if (Compacto)
            {
                AnchoKpi = Math.Min(140, AnchoKpi);
            }
            int AltoKpi = (int)Math.Round(AnchoKpi * (double)_AltoKpiBase / _AnchoKpiBase);
            int Separacion = (Ancho - 2 * _MargenDashboard - 6 * AnchoKpi) / 5;
            int PosicionSuperiorKpi = PosicionSuperiorInfo - 24 - AltoKpi;

            Panel[] Kpis = { ContabilidadPnlKPI1, ContabilidadPnlKPI2, ContabilidadPnlKPI3,
                             ContabilidadPnlKPI4, ContabilidadPnlKPI5, ContabilidadPnlKPI6 };
            Label[] Valores = { ContabilidadLblKPIResp1, ContabilidadLblKPIResp2, ContabilidadLblKPIResp3,
                                ContabilidadLblKPIResp4, ContabilidadLblKPIResp5, ContabilidadLblKPIResp6 };
            Label[] Textos = { ContabilidadLblKPI1, ContabilidadLblKPI2, ContabilidadLblKPI3,
                               ContabilidadLblKPI4, ContabilidadLblKPI5, ContabilidadLblKPI6 };

            for (int Indice = 0; Indice < Kpis.Length; Indice++)
            {
                Kpis[Indice].SetBounds(_MargenDashboard + Indice * (AnchoKpi + Separacion), PosicionSuperiorKpi, AnchoKpi, AltoKpi);
                Valores[Indice].Location = new Point(14 * AnchoKpi / _AnchoKpiBase, 95 * AltoKpi / _AltoKpiBase);
                Textos[Indice].Location = new Point(18 * AnchoKpi / _AnchoKpiBase, 138 * AltoKpi / _AltoKpiBase);
            }

            ContabilidadLblResumen.Location = new Point(38, PosicionSuperiorKpi - ContabilidadLblResumen.Height - 12);

            int AltoButti = Math.Max(180, PosicionSuperiorKpi - 50);
            int AnchoButti = (int)Math.Round(AltoButti * 313.0 / 415.0);
            int PosicionIzquierdaButti = Math.Max(500, (int)(Ancho * 0.43));
            ContabilidadPbButti.SetBounds(PosicionIzquierdaButti, 10, AnchoButti, AltoButti);
            ContabilidadLblBienvenido.Top = Compacto ? 16 : 36;
            ContabilidadLblModulo.Top = Compacto ? 92 : 112;
            ContabilidadLblParrafo.Top = Compacto ? 136 : 160;
            int TamanoAyuda = Compacto ? 64 : 84;
            int PosicionAyuda = Math.Min(Compacto ? 220 : 268, ContabilidadLblResumen.Top - TamanoAyuda - 12);
            ContabilidadBtnAyuda.SetBounds(52, PosicionAyuda, TamanoAyuda, TamanoAyuda);
            ContabilidadMetAcomodarInfo();
        }

        // Divide el panel inferior: información relevante a la izquierda, nota de integración a la derecha.
        private void ContabilidadMetAcomodarInfo()
        {
            int Divisor = (int)(ContabilidadPnlInfo.Width * 0.60);
            ContabilidadPnlInfoDivisor.Location = new Point(Divisor, 14);
            ContabilidadPbInfo.Location = new Point(Divisor + 28, (ContabilidadPnlInfo.Height - ContabilidadPbInfo.Height) / 2);
            ContabilidadLblNotaTitulo.Location = new Point(Divisor + 90, 14);
            ContabilidadLblNotaTexto.Location = new Point(Divisor + 92, 40);
            ContabilidadLblInfoTexto.AutoSize = false;
            ContabilidadLblInfoTexto.Size = new Size(Math.Max(1, Divisor - 52), 40);
            ContabilidadLblNotaTexto.AutoSize = false;
            ContabilidadLblNotaTexto.Size = new Size(Math.Max(1, ContabilidadPnlInfo.Width - Divisor - 112), 48);
            ContabilidadPnlInfo.Invalidate();
        }

        // Fondo blanco con esquinas redondeadas y borde suave del panel de información.
        private void ContabilidadMetPintarInformacion(object Remitente, PaintEventArgs Evento)
        {
            Graphics Lienzo = Evento.Graphics;
            Lienzo.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle Area = new Rectangle(1, 1, ContabilidadPnlInfo.Width - 3, ContabilidadPnlInfo.Height - 3);
            using (GraphicsPath Ruta = ContabilidadFuncRedondearEsquinas(Area, 22))
            using (SolidBrush Relleno = new SolidBrush(Color.White))
            using (Pen Borde = new Pen(Color.FromArgb(212, 222, 228), 2))
            {
                Lienzo.FillPath(Relleno, Ruta);
                Lienzo.DrawPath(Borde, Ruta);
            }
        }

        // Ruta con esquinas redondeadas.
        private GraphicsPath ContabilidadFuncRedondearEsquinas(Rectangle Rectangulo, int Radio)
        {
            GraphicsPath RutaGrafica = new GraphicsPath();
            int Diametro = Radio * 2;

            RutaGrafica.AddArc(Rectangulo.X, Rectangulo.Y, Diametro, Diametro, 180, 90);
            RutaGrafica.AddArc(Rectangulo.Right - Diametro, Rectangulo.Y, Diametro, Diametro, 270, 90);
            RutaGrafica.AddArc(Rectangulo.Right - Diametro, Rectangulo.Bottom - Diametro, Diametro, Diametro, 0, 90);
            RutaGrafica.AddArc(Rectangulo.X, Rectangulo.Bottom - Diametro, Diametro, Diametro, 90, 90);
            RutaGrafica.CloseFigure();

            return RutaGrafica;
        }

        // Evita el parpadeo al redimensionar (DoubleBuffered es protegida en Control).
        private static void ContabilidadMetDobleBuffer(Control Objetivo)
        {
            typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(Objetivo, true, null);
        }

        // Contrae/expande el menú lateral; el resto del MDI se reacomoda solo por el Dock/Anchor.
        private void ContabilidadMetAlternarMenu(object Remitente, EventArgs Evento)
        {
            bool Expandido = ContabilidadPnlNavegador.Width == _AnchoMenuExpandido;
            int NuevoAncho = Expandido ? _AnchoMenuContraido : _AnchoMenuExpandido;

            ContabilidadPnlNavegador.Width = NuevoAncho;
            ContabilidadPnlLateral.Width = NuevoAncho + _MargenMenu;
            ContabilidadMetAcomodarCatalogos();
        }

        // Despliega o recoge el submenú de catálogos.
        private void ContabilidadMetAlternarCatalogos(object Remitente, EventArgs Evento)
        {
            _CatalogosAbierto = !_CatalogosAbierto;
            ContabilidadMetAcomodarCatalogos();
        }

        // Muestra u oculta los catálogos; con el menú contraído se alinean a la izquierda para ver su ícono.
        private void ContabilidadMetAcomodarCatalogos()
        {
            int Izquierda = ContabilidadPnlNavegador.Width == _AnchoMenuExpandido ? 21 : 3;
            foreach (Button Boton in _BotonesCatalogos)
            {
                Boton.Visible = _CatalogosAbierto;
                Boton.Left = Izquierda;
            }
            ContabilidadBtnCatalogos.Invalidate();
        }

        // Abre Mantenimiento de países.
        private void ContabilidadMetAbrirPaises(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10011, (Button)Remitente, Permisos => new FrmMantenimientoPais(Permisos));
        }

        // Abre Mantenimiento de módulos contables.
        private void ContabilidadMetAbrirModulosContables(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10012, (Button)Remitente, Permisos => new FrmMantenimientoModuloContable(Permisos));
        }

        // Abre Mantenimiento de vida útil.
        private void ContabilidadMetAbrirVidaUtil(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10013, (Button)Remitente, Permisos => new FrmMantenimientoVidaUtil(Permisos));
        }

        // Abre Mantenimiento de cuentas.
        private void ContabilidadMetAbrirCuentas(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10014, (Button)Remitente, Permisos => new FrmMantenimientoCuenta(Permisos));
        }

        // Abre Asignación de cuentas por módulo.
        private void ContabilidadMetAbrirCuentasModulo(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10015, (Button)Remitente, Permisos => new FrmAsignacionCuentaModulo(Permisos));
        }

        // Abre Pólizas locales.
        private void ContabilidadMetAbrirPolizasLocales(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10001, (Button)Remitente, Permisos => new FrmPolizasLocales(Permisos, _IdModulo));
        }

        // Abre Presupuestos.
        private void ContabilidadMetAbrirPresupuestos(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10002, (Button)Remitente, Permisos => new FrmPresupuestos(Permisos));
        }

        // Abre Activos fijos.
        private void ContabilidadMetAbrirActivosFijos(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10003, (Button)Remitente, Permisos => new FrmActivosFijos(Permisos));
        }

        // Abre Cierre contable.
        private void ContabilidadMetAbrirCierreContable(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10004, (Button)Remitente, Permisos => new FrmCierreContable(Permisos));
        }

        // Abre Estados financieros.
        private void ContabilidadMetAbrirEstadosFinancieros(object Remitente, EventArgs Evento)
        {
            ContabilidadMetAbrirPantalla(10005, (Button)Remitente, Permisos => new FrmEstadosFinancieros(Permisos));
        }


        // Estados visuales de presentación (EST-10, Parte I §11.3).
        private void ContabilidadMetConfigurarEstadosMenu()
        {
            Button[] Botones = { ContabilidadBtnPolizasLocales, ContabilidadBtnPresupuestos,
                ContabilidadBtnActivosFijos, ContabilidadBtnCierreContable, ContabilidadBtnEstadosFinancieros,
                ContabilidadBtnCatalogos,
                ContabilidadBtnPaises,
                ContabilidadBtnModulosContables,
                ContabilidadBtnVidaUtil,
                ContabilidadBtnCuentas,
                ContabilidadBtnCuentasModulo };
            string[] Titulos = { "Pólizas locales", "Presupuestos", "Activos fijos",
                "Cierre contable", "Estados financieros", "Catálogos", "Países", "Módulos contables", "Vida útil", "Cuentas contables", "Cuentas por módulo" };
            for (int Indice = 0; Indice < Botones.Length; Indice++)
            {
                Button Boton = Botones[Indice];
                Boton.AccessibleName = Titulos[Indice];
                Boton.MouseEnter += (Remitente, Evento) =>
                {
                    _BotonSobre = (Button)Remitente;
                    _BotonSobre.Invalidate();
                };
                Boton.MouseLeave += (Remitente, Evento) =>
                {
                    _BotonSobre = null;
                    ((Button)Remitente).Invalidate();
                };
                Boton.Paint += ContabilidadMetPintarEstadoMenu;
            }
        }

        // Dibuja el hover, el estado activo y la etiqueta.
        private void ContabilidadMetPintarEstadoMenu(object Remitente, PaintEventArgs Evento)
        {
            Button Boton = (Button)Remitente;
            // Catálogos queda marcado mientras esté abierto alguno de sus mantenimientos.
            bool Activo = Boton == _BotonActivo || (Boton == ContabilidadBtnCatalogos && _BotonActivo != null && Array.IndexOf(_BotonesCatalogos, _BotonActivo) >= 0);
            if (Boton.Enabled && (Boton == _BotonSobre || Activo))
            {
                using (SolidBrush Fondo = new SolidBrush(Color.FromArgb(65, 70, 104, 125)))
                    Evento.Graphics.FillRectangle(Fondo, Boton.ClientRectangle);
            }
            if (Boton.Enabled && Activo)
            {
                using (SolidBrush Acento = new SolidBrush(Color.FromArgb(197, 143, 62)))
                    Evento.Graphics.FillRectangle(Acento, 0, 0, 4, Boton.Height);
            }
            // La etiqueta se presenta en Tahoma y con solo la primera letra mayúscula
            // (EST-10 §2.3 y §9), conservando el dibujo y el archivo PNG originales.
            Rectangle Etiqueta = new Rectangle(60, 4, Boton.Width - 64, Boton.Height - 8);
            using (SolidBrush Fondo = new SolidBrush(Boton.Enabled ? Color.FromArgb(70, 104, 125) : Color.FromArgb(96, 96, 96)))
                Evento.Graphics.FillRectangle(Fondo, Etiqueta);
            Color Texto = !Boton.Enabled ? Color.FromArgb(210, 210, 210) :
                Activo ? Color.FromArgb(197, 143, 62) : Color.FromArgb(212, 222, 228);
            TextRenderer.DrawText(Evento.Graphics, Boton.AccessibleName, Boton.Font,
                Etiqueta, Texto, TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
            if (Boton == ContabilidadBtnCatalogos)
            {
                // Flecha de desplegado (hacia abajo) o recogido (hacia la derecha).
                int X = Boton.Width - 26, Y = Boton.Height / 2;
                Point[] Flecha = _CatalogosAbierto
                    ? new[] { new Point(X, Y - 3), new Point(X + 10, Y - 3), new Point(X + 5, Y + 4) }
                    : new[] { new Point(X + 2, Y - 6), new Point(X + 2, Y + 6), new Point(X + 9, Y) };
                using (SolidBrush Pincel = new SolidBrush(Texto)) Evento.Graphics.FillPolygon(Pincel, Flecha);
            }
        }

        // Consulta los permisos y abre la pantalla si el usuario tiene acceso.
        private void ContabilidadMetAbrirPantalla(int Codigo, Button Boton, Func<ClsPermisosPantallaContabilidad, Form> Crear)
        {
            // Volver a consultar evita usar un acceso anterior si cambió la sesión.
            ContabilidadMetActualizarSeguridad(true);
            if (!_Estado.Permisos[Codigo].TieneAcceso) return;
            Button Anterior = _BotonActivo;
            _BotonActivo = Boton;
            Anterior?.Invalidate();
            Boton.Invalidate();
            using (Form Formulario = Crear(_Estado.Permisos[Codigo]))
            {
                Formulario.ShowDialog(this);
            }
        }

        // Avisa que la ayuda aún no está disponible.
        private void ContabilidadMetMostrarAyuda(object Remitente, EventArgs Evento)
        {
            MessageBox.Show("La ayuda del módulo de Contabilidad aún no está disponible.",
                "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}