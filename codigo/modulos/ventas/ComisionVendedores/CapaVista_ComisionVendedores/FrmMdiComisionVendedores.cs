using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using CapaControlador_ComisionVendedores.Contratos;
using CapaControlador_ComisionVendedores.Controladores;
using CapaControlador_ComisionVendedores.DTO;
using CapaControlador_ComisionVendedores.Enumeraciones;

namespace ComisionVendedoresVista
{
    public partial class FrmMdiComisionVendedores : Form
    {
        private readonly IControladorComision _ControladorComision;
        private List<ClsComisionVendedorResultado> _Resultados;
        private List<ClsComisionFacturaDetalle> _DetallesFactura;
        private List<ClsProductoPorcentaje> _ProductosPorcentaje;
        private EnumMetodoComision _MetodoCalculado = EnumMetodoComision.Vendedor;
        private const int _FilasPorPagina = 9;
        private int _PaginaResultados;
        private int _PaginaFactura;
        private int _PaginaPorcentajes;

        public FrmMdiComisionVendedores()
            : this(new ClsControladorComision())
        {
        }

        public FrmMdiComisionVendedores(IControladorComision ControladorComision)
        {
            InitializeComponent();
            _ControladorComision = ControladorComision ?? throw new ArgumentNullException("ControladorComision");
            _Resultados = new List<ClsComisionVendedorResultado>();
            _DetallesFactura = new List<ClsComisionFacturaDetalle>();
            _ProductosPorcentaje = new List<ClsProductoPorcentaje>();
        }

        private void ComisionVendedoresMetCalcularClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblEstado,
                    ComisionVendedoresDtpFechaInicio, ComisionVendedoresDtpFechaFin, ComisionVendedoresCboMetodo);
                _MetodoCalculado = ComisionVendedoresMetObtenerMetodo(ComisionVendedoresCboMetodo.SelectedItem.ToString());
                if (ComisionVendedoresDtpFechaInicio.Value.Date > ComisionVendedoresDtpFechaFin.Value.Date)
                    throw new ArgumentException("La fecha inicial debe ser anterior o igual a la fecha final.");
                _Resultados = _ControladorComision.ComisionVendedoresMetCalcular(_MetodoCalculado, EnumBaseComision.Ventas,
                    ComisionVendedoresDtpFechaInicio.Value.Date, ComisionVendedoresDtpFechaFin.Value.Date);
                _PaginaResultados = 0;
                ComisionVendedoresMetMostrarPaginaResultados();
                ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnGuardar, _Resultados.Count > 0,
                    System.Drawing.Color.FromArgb(201, 162, 74));
                ComisionVendedoresLblEstado.Text = _Resultados.Count == 0 ? "No se encontraron facturas para el periodo." : "Cálculo completado. Base: VENTAS.";
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnGuardar, false,
                    System.Drawing.Color.FromArgb(201, 162, 74));
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblEstado, Excepcion.Message,
                    ComisionVendedoresDtpFechaInicio, ComisionVendedoresDtpFechaFin, ComisionVendedoresCboMetodo);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComisionVendedoresMetGuardarClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblEstado);
                int Guardados = _ControladorComision.ComisionVendedoresMetGuardar(_Resultados,
                    ComisionVendedoresDtpFechaInicio.Value.Date, ComisionVendedoresDtpFechaFin.Value.Date,
                    _MetodoCalculado, EnumBaseComision.Ventas);
                ComisionVendedoresLblEstado.Text = "Resultados guardados en ComisionCalculada.";
                ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnGuardar, false,
                    System.Drawing.Color.FromArgb(201, 162, 74));
                MessageBox.Show("Se guardaron " + Guardados + " resultados.", "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblEstado, Excepcion.Message);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComisionVendedoresMetBuscarClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblMantenimientoEstado, ComisionVendedoresCboAlcance);
                List<ClsProductoPorcentaje> Productos = ComisionVendedoresMetCargarPorcentajes();
                ComisionVendedoresLblMantenimientoEstado.Text = Productos.Count + " producto(s) encontrados.";
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblMantenimientoEstado, Excepcion.Message, ComisionVendedoresCboAlcance);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComisionVendedoresMetCargarCatalogosClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblEstadoFactura,
                    ComisionVendedoresCboFactura, ComisionVendedoresCboVendedorFactura, ComisionVendedoresCboMetodoFactura);
                ComisionVendedoresCboFactura.DataSource = null;
                ComisionVendedoresCboVendedorFactura.DataSource = null;
                ComisionVendedoresCboFactura.DataSource = _ControladorComision.ComisionVendedoresMetObtenerFacturas();
                ComisionVendedoresCboVendedorFactura.DataSource = _ControladorComision.ComisionVendedoresMetObtenerVendedores();
                ComisionVendedoresLblEstadoFactura.Text = "Seleccione una factura y el vendedor responsable.";
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblEstadoFactura, Excepcion.Message,
                    ComisionVendedoresCboFactura, ComisionVendedoresCboVendedorFactura);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComisionVendedoresMetCalcularFacturaClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblEstadoFactura,
                    ComisionVendedoresCboFactura, ComisionVendedoresCboVendedorFactura, ComisionVendedoresCboMetodoFactura);
                if (ComisionVendedoresCboFactura.SelectedValue == null || ComisionVendedoresCboVendedorFactura.SelectedValue == null)
                    throw new ArgumentException("Cargue las facturas y vendedores, y seleccione ambos.");

                int IdFactura = Convert.ToInt32(ComisionVendedoresCboFactura.SelectedValue, CultureInfo.InvariantCulture);
                int IdVendedor = Convert.ToInt32(ComisionVendedoresCboVendedorFactura.SelectedValue, CultureInfo.InvariantCulture);
                EnumMetodoComision Metodo = ComisionVendedoresMetObtenerMetodo(ComisionVendedoresCboMetodoFactura.SelectedItem.ToString());
                List<ClsComisionFacturaDetalle> DetalleFacturaCalculado = _ControladorComision.ComisionVendedoresMetCalcularFactura(IdFactura, IdVendedor, Metodo);
                ComisionVendedoresDgvDetalleFactura.DataSource = null;
                _DetallesFactura = DetalleFacturaCalculado;
                _PaginaFactura = 0;
                ComisionVendedoresMetMostrarPaginaFactura();

                decimal TotalComision = 0m, Disponible = 0m, Pendiente = 0m, ComisionUltimoAbono = 0m;
                foreach (ClsComisionFacturaDetalle Detalle in DetalleFacturaCalculado)
                {
                    TotalComision += Detalle.ComisionTotal;
                    Disponible += Detalle.ComisionDisponible;
                    Pendiente += Detalle.ComisionPendiente;
                    ComisionUltimoAbono += Detalle.ComisionUltimoAbono;
                }
                ClsFacturaComisionVista Factura = ComisionVendedoresCboFactura.SelectedItem as ClsFacturaComisionVista;
                ClsVendedorComisionVista Vendedor = ComisionVendedoresCboVendedorFactura.SelectedItem as ClsVendedorComisionVista;
                decimal PagadoFactura = Factura == null ? 0m : Factura.TotalPagado;
                ComisionVendedoresLblEstadoFactura.Text = "Factura " + IdFactura + " · " + (Vendedor == null ? "" : Vendedor.NombreVendedor) +
                    " · Cobrado Q " + PagadoFactura.ToString("N2", CultureInfo.GetCultureInfo("es-GT")) +
                    " · Comisión total Q " + TotalComision.ToString("N2", CultureInfo.GetCultureInfo("es-GT")) +
                    " · Disponible Q " + Disponible.ToString("N2", CultureInfo.GetCultureInfo("es-GT")) +
                    " · Comisión último abono Q " + ComisionUltimoAbono.ToString("N2", CultureInfo.GetCultureInfo("es-GT")) +
                    " · Pendiente Q " + Pendiente.ToString("N2", CultureInfo.GetCultureInfo("es-GT")) +
                    " · El registro persistente se conectará a la tabla que proporcionarás.";
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblEstadoFactura, Excepcion.Message,
                    ComisionVendedoresCboFactura, ComisionVendedoresCboVendedorFactura, ComisionVendedoresCboMetodoFactura);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ComisionVendedoresMetActualizarClick(object Sender, EventArgs Argumentos)
        {
            try
            {
                ComisionVendedoresMetRestablecerEstado(ComisionVendedoresLblMantenimientoEstado, ComisionVendedoresTxtPorcentaje);
                decimal Porcentaje;
                if (!decimal.TryParse(ComisionVendedoresTxtPorcentaje.Text, NumberStyles.Number,
                    CultureInfo.GetCultureInfo("es-GT"), out Porcentaje))
                {
                    ComisionVendedoresMetMostrarError(ComisionVendedoresLblMantenimientoEstado,
                        "Ingrese un porcentaje válido entre 0 y 100.", ComisionVendedoresTxtPorcentaje);
                    throw new ArgumentException("Ingrese un porcentaje válido entre 0 y 100.");
                }
                if (ComisionVendedoresDgvPorcentajes.SelectedRows.Count == 0)
                    throw new ArgumentException("Busque y seleccione al menos un elemento para actualizar.");

                EnumMetodoComision Metodo = ComisionVendedoresMetObtenerMetodo(ComisionVendedoresCboAlcance.SelectedItem.ToString());
                HashSet<int> IdsProducto = new HashSet<int>();
                foreach (DataGridViewRow Fila in ComisionVendedoresDgvPorcentajes.SelectedRows)
                {
                    ClsProductoPorcentaje Producto = Fila.DataBoundItem as ClsProductoPorcentaje;
                    if (Producto == null) continue;
                    IdsProducto.Add(Producto.IdProducto);
                }

                if (IdsProducto.Count == 0) throw new ArgumentException("Seleccione una fila válida para actualizar.");
                int Actualizados = _ControladorComision.ComisionVendedoresMetActualizarPorcentaje(Metodo, IdsProducto.ToList(), Porcentaje);

                _ProductosPorcentaje = _ControladorComision.ComisionVendedoresMetConsultarPorcentajes(Metodo);
                _PaginaPorcentajes = Math.Min(_PaginaPorcentajes, ComisionVendedoresMetObtenerUltimaPagina(_ProductosPorcentaje.Count));
                ComisionVendedoresMetMostrarPaginaPorcentajes();
                ComisionVendedoresLblMantenimientoEstado.Text = Actualizados + " producto(s) actualizado(s).";
            }
            catch (Exception Excepcion)
            {
                ComisionVendedoresMetMostrarError(ComisionVendedoresLblMantenimientoEstado, Excepcion.Message);
                MessageBox.Show(Excepcion.Message, "7008 – ComisionVendedores", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<ClsProductoPorcentaje> ComisionVendedoresMetCargarPorcentajes()
        {
            _ProductosPorcentaje = _ControladorComision.ComisionVendedoresMetConsultarPorcentajes(
                ComisionVendedoresMetObtenerMetodo(ComisionVendedoresCboAlcance.SelectedItem.ToString()));
            _PaginaPorcentajes = 0;
            ComisionVendedoresMetMostrarPaginaPorcentajes();
            return _ProductosPorcentaje;
        }

        private void ComisionVendedoresMetMostrarPaginaResultados()
        {
            ComisionVendedoresDgvResultados.DataSource = _Resultados.Skip(_PaginaResultados * _FilasPorPagina).Take(_FilasPorPagina).ToList();
            ComisionVendedoresLblPaginaResultados.Text = ComisionVendedoresMetObtenerTextoPagina(_PaginaResultados, _Resultados.Count);
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnAnteriorResultados, _PaginaResultados > 0,
                System.Drawing.Color.FromArgb(228, 212, 234));
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnSiguienteResultados,
                _PaginaResultados < ComisionVendedoresMetObtenerUltimaPagina(_Resultados.Count), System.Drawing.Color.FromArgb(228, 212, 234));
        }

        private void ComisionVendedoresMetMostrarPaginaFactura()
        {
            ComisionVendedoresDgvDetalleFactura.DataSource = _DetallesFactura.Skip(_PaginaFactura * _FilasPorPagina).Take(_FilasPorPagina).ToList();
            ComisionVendedoresLblPaginaFactura.Text = ComisionVendedoresMetObtenerTextoPagina(_PaginaFactura, _DetallesFactura.Count);
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnAnteriorFactura, _PaginaFactura > 0,
                System.Drawing.Color.FromArgb(228, 212, 234));
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnSiguienteFactura,
                _PaginaFactura < ComisionVendedoresMetObtenerUltimaPagina(_DetallesFactura.Count), System.Drawing.Color.FromArgb(228, 212, 234));
        }

        private void ComisionVendedoresMetMostrarPaginaPorcentajes()
        {
            ComisionVendedoresDgvPorcentajes.DataSource = _ProductosPorcentaje.Skip(_PaginaPorcentajes * _FilasPorPagina).Take(_FilasPorPagina).ToList();
            ComisionVendedoresLblPaginaPorcentajes.Text = ComisionVendedoresMetObtenerTextoPagina(_PaginaPorcentajes, _ProductosPorcentaje.Count);
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnAnteriorPorcentajes, _PaginaPorcentajes > 0,
                System.Drawing.Color.FromArgb(228, 212, 234));
            ComisionVendedoresMetActualizarBoton(ComisionVendedoresBtnSiguientePorcentajes,
                _PaginaPorcentajes < ComisionVendedoresMetObtenerUltimaPagina(_ProductosPorcentaje.Count), System.Drawing.Color.FromArgb(228, 212, 234));
        }

        private int ComisionVendedoresMetObtenerUltimaPagina(int TotalRegistros)
        {
            return Math.Max(0, (int)Math.Ceiling(TotalRegistros / (double)_FilasPorPagina) - 1);
        }

        private void ComisionVendedoresMetMostrarError(Label EtiquetaEstado, string Mensaje, params Control[] Controles)
        {
            EtiquetaEstado.ForeColor = System.Drawing.Color.Firebrick;
            EtiquetaEstado.Text = Mensaje;
            foreach (Control ControlValidado in Controles)
                ControlValidado.BackColor = System.Drawing.Color.MistyRose;
        }

        private void ComisionVendedoresMetActualizarBoton(Button Boton, bool Habilitado, System.Drawing.Color ColorActivo)
        {
            Boton.Enabled = Habilitado;
            Boton.BackColor = Habilitado ? ColorActivo : System.Drawing.Color.LightGray;
            Boton.ForeColor = Habilitado ? System.Drawing.Color.FromArgb(59, 29, 74) : System.Drawing.SystemColors.GrayText;
        }

        private void ComisionVendedoresMetRestablecerEstado(Label EtiquetaEstado, params Control[] Controles)
        {
            EtiquetaEstado.ForeColor = System.Drawing.Color.FromArgb(59, 29, 74);
            foreach (Control ControlValidado in Controles)
                ControlValidado.BackColor = System.Drawing.SystemColors.Window;
        }

        private string ComisionVendedoresMetObtenerTextoPagina(int Pagina, int TotalRegistros)
        {
            int TotalPaginas = TotalRegistros == 0 ? 0 : ComisionVendedoresMetObtenerUltimaPagina(TotalRegistros) + 1;
            return "Página " + (TotalPaginas == 0 ? 0 : Pagina + 1) + " de " + TotalPaginas + " · " + TotalRegistros + " registros";
        }

        private void ComisionVendedoresMetCambiarPaginaResultados(object Sender, EventArgs Argumentos)
        {
            _PaginaResultados += Sender == ComisionVendedoresBtnAnteriorResultados ? -1 : 1;
            ComisionVendedoresMetMostrarPaginaResultados();
        }

        private void ComisionVendedoresMetCambiarPaginaFactura(object Sender, EventArgs Argumentos)
        {
            _PaginaFactura += Sender == ComisionVendedoresBtnAnteriorFactura ? -1 : 1;
            ComisionVendedoresMetMostrarPaginaFactura();
        }

        private void ComisionVendedoresMetCambiarPaginaPorcentajes(object Sender, EventArgs Argumentos)
        {
            _PaginaPorcentajes += Sender == ComisionVendedoresBtnAnteriorPorcentajes ? -1 : 1;
            ComisionVendedoresMetMostrarPaginaPorcentajes();
        }

        private EnumMetodoComision ComisionVendedoresMetObtenerMetodo(string Texto)
        {
            switch (Texto)
            {
                case "Vendedor": return EnumMetodoComision.Vendedor;
                case "Marca": return EnumMetodoComision.Marca;
                case "Línea": return EnumMetodoComision.Linea;
                case "Producto": return EnumMetodoComision.Producto;
                case "Categoría": return EnumMetodoComision.Categoria;
                default: throw new ArgumentException("Seleccione un método de comisión.");
            }
        }
    }
}
