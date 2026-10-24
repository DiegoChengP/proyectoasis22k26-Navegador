using System;
using System.Data;
using System.Windows.Forms;
using CapaVista_Componentes.Componentes;

namespace CapaVista_MovimientosProveedores
{
    public partial class FrmMovimientosProveedores : System.Windows.Forms.Form
    {
        private readonly object[,] _Movimientos =
        {
            // Fecha, Tipo de movimiento, Documento, Descripción, Debe, Haber, Saldo, Estado

            {
                new DateTime(2026, 10, 1),
                "Saldo inicial",
                "SAL-001",
                "Saldo inicial del proveedor",
                0.00m, 0.00m, 0.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 2),
                "Factura",
                "FAC-00125",
                "Compra de suministros de oficina",
                2500.00m, 0.00m, 2500.00m,
                "Pendiente"
            },
            {
                new DateTime(2026, 10, 3),
                "Factura",
                "FAC-00128",
                "Compra de equipo informático",
                4800.00m, 0.00m, 7300.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 5),
                "Pago",
                "PAG-00045",
                "Pago parcial mediante transferencia",
                0.00m, 3000.00m, 4300.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 6),
                "Nota de crédito",
                "NC-00018",
                "Descuento por productos defectuosos",
                0.00m, 500.00m, 3800.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 7),
                "Factura",
                "FAC-00134",
                "Compra de materiales de limpieza",
                1200.00m, 0.00m, 5000.00m,
                "Pendiente"
            },
            {
                new DateTime(2026, 10, 8),
                "Pago",
                "PAG-00049",
                "Pago de factura FAC-00128",
                0.00m, 2000.00m, 3000.00m,
                "Aplicado"
            },
            {
                new DateTime(2026, 10, 10),
                "Factura",
                "FAC-00140",
                "Compra de repuestos para mantenimiento",
                1750.00m, 0.00m, 4750.00m,
                "Pendiente"
            }
        };

        private readonly DataTable _TablaMovimientos = new DataTable();

        public FrmMovimientosProveedores()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);

            this.BackColor = ClsTemaCompras.FondoGeneral;

            ComprasCboProveedores.DropDownStyle = ComboBoxStyle.DropDown;
            ComprasCboProveedores.Text = "Buscar Proveedor...";
            ComprasProcInicializarTablaMovimientos();
            ComprasProcCargarMovimientos();
            ComprasUsrMovimientos.DataBindingComplete += (Remitente, Evento) => ComprasProcFormatearMovimientos();
            ComprasProcFormatearMovimientos();
        }
        private void ComprasProcInicializarTablaMovimientos()
        {
            _TablaMovimientos.Columns.Add("ComprasColFecha", typeof(DateTime));
            _TablaMovimientos.Columns.Add("Tipo de movimiento", typeof(string));
            _TablaMovimientos.Columns.Add("ComprasColDocumento", typeof(string));
            _TablaMovimientos.Columns.Add("Descripción", typeof(string));
            _TablaMovimientos.Columns.Add("ComprasColDebe", typeof(decimal));
            _TablaMovimientos.Columns.Add("ComprasColHaber", typeof(decimal));
            _TablaMovimientos.Columns.Add("ComprasColSaldo", typeof(decimal));
            _TablaMovimientos.Columns.Add("ComprasColEstado", typeof(string));
        }

        private void ComprasProcFormatearMovimientos()
        {
            string[] Campos = { "ComprasColFecha", "ComprasColDocumento", "ComprasColDebe", "ComprasColHaber", "ComprasColSaldo", "ComprasColEstado" };
            string[] Titulos = { "Fecha", "Documento", "Debe", "Haber", "Saldo", "Estado" };
            for (int Indice = 0; Indice < Campos.Length; Indice++)
                if (ComprasUsrMovimientos.Columns.Contains(Campos[Indice]))
                    ComprasUsrMovimientos.Columns[Campos[Indice]].HeaderText = Titulos[Indice];
            if (!ComprasUsrMovimientos.Columns.Contains("ComprasColFecha")) return;
            ComprasUsrMovimientos.Columns["ComprasColFecha"].DefaultCellStyle.Format = "dd/MM/yyyy";
            foreach (string Campo in new[] { "ComprasColDebe", "ComprasColHaber", "ComprasColSaldo" })
            {
                var Estilo = ComprasUsrMovimientos.Columns[Campo].DefaultCellStyle;
                Estilo.Format = "N2";
                Estilo.FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
                Estilo.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void ComprasProcCargarMovimientos()
        {
            for (int Indice = 0; Indice < _Movimientos.GetLength(0); Indice++)
            {
                _TablaMovimientos.Rows.Add(
                    _Movimientos[Indice, 0],
                    _Movimientos[Indice, 1],
                    _Movimientos[Indice, 2],
                    _Movimientos[Indice, 3],
                    _Movimientos[Indice, 4],
                    _Movimientos[Indice, 5],
                    _Movimientos[Indice, 6],
                    _Movimientos[Indice, 7]
                );
            }

            ComprasUsrMovimientos.AutoGenerateColumns = true;
            ComprasUsrMovimientos.DataSource = _TablaMovimientos;
        }
    }
}
