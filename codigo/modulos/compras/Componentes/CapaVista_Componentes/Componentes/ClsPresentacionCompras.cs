using System.Windows.Forms;
using System.Drawing;

namespace CapaVista_Componentes.Componentes
{
    public static class ClsPresentacionCompras
    {
        public static Size TamanoFormulario => new Size(1300, 700);
        public static int AlturaContenido => 723;

        public static void ComprasMetFijarTamanoFormulario(Form Formulario)
        {
            Formulario.AutoSize = false;
            Formulario.FormBorderStyle = FormBorderStyle.FixedSingle;
            Formulario.MaximizeBox = false;
            Formulario.MinimizeBox = true;
            Formulario.StartPosition = FormStartPosition.CenterParent;
            var Recursos = new System.Resources.ResourceManager("CapaVista_Componentes.Componentes.RecursosCompras", typeof(ClsPresentacionCompras).Assembly);
            Formulario.Icon = (Icon)Recursos.GetObject("IconoCompras");
            Formulario.ClientSize = TamanoFormulario;
            Formulario.MinimumSize = Formulario.Size;
            Formulario.MaximumSize = Formulario.Size;
            Formulario.Load += (Remitente, Evento) =>
            {
                Rectangle Area = Screen.FromControl(Formulario.Owner ?? Formulario).WorkingArea;
                ComprasMetAjustarAreaFormulario(Formulario, Area);
            };
            ComprasMetAplicarScrollVertical(Formulario);
        }

        private static void ComprasMetAplicarScrollVertical(Control Contenedor)
        {
            DateTimePicker Fecha = Contenedor as DateTimePicker;
            if (Fecha != null)
            {
                Fecha.Format = DateTimePickerFormat.Custom;
                Fecha.CustomFormat = "dd/MM/yyyy";
            }
            DataGridView Tabla = Contenedor as DataGridView;
            if (Tabla != null) Tabla.ScrollBars = ScrollBars.Vertical;
            foreach (Control Hijo in Contenedor.Controls) ComprasMetAplicarScrollVertical(Hijo);
        }

        public static void ComprasMetAjustarAreaFormulario(Form Formulario, Rectangle Area)
        {
            Size Tamano = new Size(System.Math.Min(Formulario.Width, Area.Width), System.Math.Min(Formulario.Height, Area.Height));
            Formulario.MinimumSize = Size.Empty;
            Formulario.MaximumSize = Size.Empty;
            Formulario.Size = Tamano;
            Formulario.MinimumSize = Tamano;
            Formulario.MaximumSize = Tamano;
        }

        public static void ComprasProcAgregarColumna(UsrDataGridViewCompras Tabla, string Campo,
            string Encabezado, int Peso, bool EsMonto)
        {
            Tabla.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn Columna = new DataGridViewTextBoxColumn
            {
                Name = "ComprasCol" + Campo,
                DataPropertyName = Campo,
                HeaderText = Encabezado,
                FillWeight = Peso
            };

            if (EsMonto)
            {
                Columna.DefaultCellStyle.Format = "N2";
                Columna.DefaultCellStyle.FormatProvider = System.Globalization.CultureInfo.InvariantCulture;
                Columna.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            Tabla.Columns.Add(Columna);
        }

        public static void ComprasProcMostrarMonto(UsrTextBoxCompras Entrada, decimal Monto)
        {
            Entrada.Text = Monto.ToString("N2", System.Globalization.CultureInfo.InvariantCulture);
            Entrada.Entrada.SelectAll();
            Entrada.Entrada.SelectionAlignment = HorizontalAlignment.Right;
        }

    }
}
