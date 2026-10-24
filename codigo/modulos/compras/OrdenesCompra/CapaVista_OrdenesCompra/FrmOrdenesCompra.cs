using CapaVista_Componentes.Componentes;
using System;

namespace CapaVista_OrdenesCompra
{
    public partial class FrmOrdenesCompra : System.Windows.Forms.Form
    {
        public FrmOrdenesCompra()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
            ComprasProcConfigurarArticulos();
        }

        private void ComprasProcConfigurarArticulos()
        {
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "IDarticulo", "ID artículo", 8, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "nombre", "Nombre", 22, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "unidad", "Unidad", 7, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "cantidad", "Cantidad", 8, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "precioUnitario", "Precio unitario", 11, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "subtotal", "Subtotal", 11, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "descuento", "Descuento", 10, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "porcentajeImpuesto", "Impuesto %", 8, false);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "impuesto", "Impuesto", 10, true);
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasProcAgregarColumna(ComprasUsrArticulos, "total", "Total", 12, true);
            ComprasUsrArticulos.Columns["ComprasColporcentajeImpuesto"].DefaultCellStyle.Format = "P0";
            ComprasUsrArticulos.Tabla.DefaultCellStyle.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            ComprasUsrArticulos.Tabla.ColumnHeadersDefaultCellStyle.Alignment =
                System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            foreach (System.Windows.Forms.DataGridViewColumn Columna in ComprasUsrArticulos.Columns)
            {
                Columna.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            }
            ComprasUsrArticulos.ReadOnly = true;
            ComprasUsrArticulos.AllowUserToAddRows = false;
            ComprasUsrArticulos.AllowUserToDeleteRows = false;

            ComprasProcAgregarArticuloEjemplo(1, "Carne de cerdo", "kg", 25M, 32.50M, 25M, 0.12M);
            ComprasProcAgregarArticuloEjemplo(2, "Carne de res", "kg", 15M, 45M, 0M, 0.12M);
            ComprasProcAgregarArticuloEjemplo(3, "Sal refinada", "kg", 10M, 4.75M, 0M, 0.12M);
            ComprasProcAgregarArticuloEjemplo(4, "Mezcla de especias", "kg", 3M, 68M, 10M, 0.12M);
            ComprasProcAgregarArticuloEjemplo(5, "Tripas para embutidos", "m", 100M, 2.25M, 5M, 0.12M);
            ComprasProcAgregarArticuloEjemplo(6, "Bolsas de empaque", "unidad", 200M, 0.80M, 0M, 0.12M);
            ComprasUsrArticulos.ClearSelection();
        }

        private void ComprasProcAgregarArticuloEjemplo(int IdArticulo, string Nombre, string Unidad,
            decimal Cantidad, decimal PrecioUnitario, decimal Descuento, decimal PorcentajeImpuesto)
        {
            var Totales = CapaControlador_OrdenesCompra.ClsCalculoOrdenCompra.ComprasFuncCalcularArticulo(
                Cantidad, PrecioUnitario, Descuento, PorcentajeImpuesto);
            ComprasUsrArticulos.Rows.Add(IdArticulo, Nombre, Unidad, Cantidad, PrecioUnitario,
                Totales.Subtotal, Descuento, PorcentajeImpuesto, Totales.Impuesto, Totales.Total);
        }
    }
}
