using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CapaVista_Componentes.Componentes;

namespace CapaVista_PagosProveedores
{
    /*Inicio de código de Diana Mishel Loeiza Ramirez 9959-23-3457 el 11/10/2026*/

    public partial class FrmPagosProveedores : System.Windows.Forms.Form
    {
        private readonly object[,] _Pagos =
        {
            // No. pago, Proveedor, Factura, Fecha, Monto, Estado
            { "PG-0041", "Distribuidora Maya",    "F-1032", new DateTime(2026, 10, 8),  12500.00m, "Pagado" },
            { "PG-0040", "Papelera del Sur",      "F-0987", new DateTime(2026, 10, 10),  3240.50m, "Programado" },
            { "PG-0039", "Insumos Industriales",  "F-2210", new DateTime(2026, 10, 5),   8900.00m, "Rechazado" },
            { "PG-0038", "Servicios Tecnicos GT", "F-0455", new DateTime(2026, 10, 2),   1750.00m, "Anulado" }
        };

        private readonly DataTable _TablaPagos = new DataTable();

        public FrmPagosProveedores()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);

            this.BackColor = ClsTemaCompras.FondoGeneral;

            ComprasProcCargarCombos();
            ComprasProcInicializarTablaPagos();
            ComprasProcCargarPagos();
            ComprasProcConfigurarGrid();
        }

        private void ComprasProcCargarCombos()
        {

            ComprasUsrCboProveedor.Items.AddRange(new object[] { "Todos", "Distribuidora Maya", "Papelera del Sur", "Insumos Industriales", "Servicios Tecnicos GT" });
            ComprasUsrCboProveedor.SelectedIndex = 0;

            ComprasUsrCboEstado.Items.AddRange(new object[] { "Todos", "Pagado", "Programado", "Rechazado", "Anulado" });
            ComprasUsrCboEstado.SelectedIndex = 0;

            ComprasUsrTxtDesde.Text = "01/10/2026";
            ComprasUsrTxtHasta.Text = "31/10/2026";


            ComprasUsrCboProveedorPago.Items.AddRange(new object[] { "Distribuidora Mava", "Distribuidora Maya", "Papelera del Sur" });
            ComprasUsrCboProveedorPago.SelectedIndex = 0;

            ComprasUsrCboFactura.Items.Add("F-1045 · Q 5,600.00");
            ComprasUsrCboFactura.SelectedIndex = 0;

            ComprasUsrTxtFechaPago.Text = "10/10/2026";
            ComprasUsrTxtMonto.Text = "5600.00";

            ComprasUsrCboMetodo.Items.AddRange(new object[] { "Transferencia", "Cheque", "Efectivo" });
            ComprasUsrCboMetodo.SelectedIndex = 0;
        }

        private void ComprasProcInicializarTablaPagos()
        {
            _TablaPagos.Columns.Add("No. pago", typeof(string));
            _TablaPagos.Columns.Add("Proveedor", typeof(string));
            _TablaPagos.Columns.Add("Factura", typeof(string));
            _TablaPagos.Columns.Add("Fecha", typeof(DateTime));
            _TablaPagos.Columns.Add("Monto", typeof(decimal));
            _TablaPagos.Columns.Add("Estado", typeof(string));
        }

        private void ComprasProcCargarPagos()
        {
            for (int Indice = 0; Indice < _Pagos.GetLength(0); Indice++)
            {
                _TablaPagos.Rows.Add(
                    _Pagos[Indice, 0], _Pagos[Indice, 1], _Pagos[Indice, 2],
                    _Pagos[Indice, 3], _Pagos[Indice, 4], _Pagos[Indice, 5]
                );
            }

            ComprasUsrPagos.AutoGenerateColumns = true;
            ComprasUsrPagos.DataSource = _TablaPagos;
        }

        private void ComprasProcConfigurarGrid()
        {
            DataGridView Tabla = ComprasUsrPagos.Tabla;

            Tabla.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            Tabla.Columns["Monto"].DefaultCellStyle.Format = "'Q' #,##0.00";

            ComprasProcAgregarColumnaAccion("Ver", "👁");
            ComprasProcAgregarColumnaAccion("Editar", "✎");
            ComprasProcAgregarColumnaAccion("Eliminar", "🗑");


            Tabla.CellFormatting += ComprasMetDgvPagosCellFormatting;
            Tabla.Layout += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.Scroll += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.ColumnWidthChanged += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.ColumnDisplayIndexChanged += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.RowHeightChanged += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.DataBindingComplete += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.SelectionChanged += (Remitente, Evento) => ComprasProcActualizarBotones();
            Tabla.CellValueChanged += (Remitente, Evento) => ComprasProcActualizarBotones();
            Disposed += (Remitente, Evento) =>
            {
                foreach (Bitmap Imagen in _Imagenes) Imagen?.Dispose();
            };
            ComprasProcActualizarBotones();
        }

        private readonly string[] _NombresImagenes = { "Ver_Tabla", "Modificar_Tabla", "Eliminar_Tabla" };
        private readonly string[] _Acciones = { "Ver", "Editar", "Eliminar" };
        private readonly Bitmap[] _Imagenes = new Bitmap[3];
        private readonly System.Collections.Generic.List<ClsButtonCompras> _Botones = new System.Collections.Generic.List<ClsButtonCompras>();
        private bool _Actualizando;

        private void ComprasProcAgregarColumnaAccion(string Nombre, string Texto)
        {
            int Indice = Array.IndexOf(_Acciones, Nombre);
            var Recursos = new System.ComponentModel.ComponentResourceManager(typeof(FrmPagosProveedores));
            var Imagen = (Bitmap)Recursos.GetObject(_NombresImagenes[Indice]);
            if (Imagen != null) _Imagenes[Indice] = new Bitmap(Imagen);
            ComprasUsrPagos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = Nombre, HeaderText = "", ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable, FillWeight = 25, MinimumWidth = 70
            });
        }

        private void ComprasProcActualizarBotones()
        {
            if (_Actualizando) return;
            _Actualizando = true;
            try
            {
                DataGridView Tabla = ComprasUsrPagos.Tabla;
                int Cantidad = 0;
                foreach (DataGridViewRow Fila in Tabla.Rows)
                {
                    if (Fila.IsNewRow || !Fila.Displayed) continue;
                    for (int Indice = 0; Indice < _Acciones.Length; Indice++)
                    {
                        var Columna = Tabla.Columns[_Acciones[Indice]];
                        if (Columna == null || !Columna.Visible) continue;
                        Rectangle Area = Tabla.GetCellDisplayRectangle(Columna.Index, Fila.Index, false);
                        Rectangle Visible = Tabla.GetCellDisplayRectangle(Columna.Index, Fila.Index, true);
                        if (Area.Width < 5 || Area.Height < 5 || Area.Size != Visible.Size) continue;
                        if (Cantidad == _Botones.Count)
                        {
                            var Nuevo = new ClsButtonCompras { TipoBoton = ComprasTipoBoton.Tabla };
                            Nuevo.Click += (Remitente, Evento) =>
                            {
                                var Celda = (DataGridViewCell)((ClsButtonCompras)Remitente).Tag;
                                Tabla.CurrentCell = Celda;
                                ComprasMetDgvPagosCellClick(Tabla, new DataGridViewCellEventArgs(Celda.ColumnIndex, Celda.RowIndex));
                            };
                            _Botones.Add(Nuevo);
                            Tabla.Controls.Add(Nuevo);
                        }
                        var Boton = _Botones[Cantidad++];
                        Boton.Tag = Fila.Cells[Columna.Index];
                        Boton.AccessibleName = _Acciones[Indice];
                        Boton.BackgroundImage = _Imagenes[Indice];
                        Boton.Text = Boton.BackgroundImage == null ? _Acciones[Indice] : "";
                        int Lado = Math.Min(Area.Width, Area.Height) - 4;
                        Boton.Bounds = new Rectangle(Area.X + (Area.Width - Lado) / 2, Area.Y + (Area.Height - Lado) / 2, Lado, Lado);
                        Boton.EsActivo = Fila.Selected;
                        Boton.Visible = Indice == 0 || Convert.ToString(Fila.Cells["Estado"].Value) != "Anulado";
                    }
                }
                for (int Indice = Cantidad; Indice < _Botones.Count; Indice++) _Botones[Indice].Visible = false;
            }
            finally { _Actualizando = false; }
        }

        // Color del texto según el estado
        private void ComprasMetDgvPagosCellFormatting(object Remitente, DataGridViewCellFormattingEventArgs Evento)
        {
            if (Evento.RowIndex < 0 || Evento.ColumnIndex < 0 || Evento.Value == null) return;
            if (ComprasUsrPagos.Columns[Evento.ColumnIndex].Name != "Estado") return;

            switch (Evento.Value.ToString())
            {
                case "Pagado": Evento.CellStyle.ForeColor = Color.LimeGreen; break;
                case "Programado": Evento.CellStyle.ForeColor = Color.Orange; break;
                case "Rechazado": Evento.CellStyle.ForeColor = Color.IndianRed; break;
                default: Evento.CellStyle.ForeColor = Color.Gray; break;
            }
        }

        private void ComprasMetDgvPagosCellClick(object Remitente, DataGridViewCellEventArgs Evento)
        {

        }
    }
}
/*Fin de código de Diana Mishel Loeiza Ramirez 9959-23-3457 el 11/10/2026*/
