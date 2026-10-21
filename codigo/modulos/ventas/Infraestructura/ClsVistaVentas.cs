// Ayudas de presentacion enlazadas como fuente en cada Vista; no son una DLL.
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows.Forms;

namespace VentasPresentacion
{
    internal static class ClsVistaVentas
    {
        internal static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-GT");
        internal static void Combo(ComboBox Control, DataTable Datos, int IdSeleccionado = 0)
        {
            Control.DataSource = null; Control.Items.Clear();
            if (!Datos.Columns.Contains("Id")) return;
            Control.DisplayMember = "Nombre"; Control.ValueMember = "Id"; Control.DataSource = Datos;
            if (IdSeleccionado > 0) Control.SelectedValue = IdSeleccionado; else Control.SelectedIndex = -1;
        }
        internal static int Id(ComboBox Control)
        { return Control.SelectedValue == null || Control.SelectedValue is DataRowView ? 0 : Convert.ToInt32(Control.SelectedValue); }
        internal static DataRow Fila(ComboBox Control) { var F = Control.SelectedItem as DataRowView; return F == null ? null : F.Row; }
        internal static void Fechas(DataGridView Grid)
        {
            Grid.CellFormatting += (s, e) =>
            {
                if (e.Value is DateTime) { e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy"); e.FormattingApplied = true; }
            };
        }
        internal static string Dinero(decimal Importe, string Moneda) { return Moneda + " " + Importe.ToString("N2", Cultura); }
        internal static decimal Decimal(object Valor)
        {
            if (Valor == null || Valor == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(Valor))) return 0;
            decimal Resultado;
            if (!decimal.TryParse(Convert.ToString(Valor), NumberStyles.Number, CultureInfo.CurrentCulture, out Resultado))
                throw new InvalidOperationException("Ingrese un importe numerico valido.");
            return Resultado;
        }
        internal static void Ejecutar(Form Ventana, Action Accion)
        {
            Ventana.UseWaitCursor = true;
            try { Accion(); }
            catch (Exception Error)
            {
                MessageBox.Show(Ventana, Error.Message, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { Ventana.UseWaitCursor = false; }
        }
        internal static void Informar(Form Ventana, string Mensaje)
        { MessageBox.Show(Ventana, Mensaje, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        internal static void Imprimir(Form Ventana, string Titulo, string Encabezado, DataTable Datos)
        {
            if (Datos == null || Datos.Rows.Count == 0) throw new InvalidOperationException("Consulte o guarde datos antes de imprimir.");
            var Lineas = new System.Collections.Generic.List<string> { Titulo, Encabezado, "" };
            foreach (DataRow Fila in Datos.Rows)
            {
                foreach (DataColumn Columna in Datos.Columns)
                {
                    object Valor = Fila[Columna];
                    string Texto = Valor is DateTime ? ((DateTime)Valor).ToString("dd/MM/yyyy") : Valor is decimal ? ((decimal)Valor).ToString("N2", Cultura) : Convert.ToString(Valor);
                    Lineas.Add(Columna.ColumnName + ": " + Texto);
                }
                Lineas.Add("");
            }
            int Posicion = 0;
            using (var Documento = new PrintDocument())
            using (var Fuente = new Font("Tahoma", 10))
            using (var Vista = new PrintPreviewDialog { Document = Documento, Width = 1000, Height = 750 })
            {
                Documento.DocumentName = Titulo;
                Documento.BeginPrint += (s, e) => Posicion = 0;
                Documento.PrintPage += (s, e) =>
                {
                    float Y = e.MarginBounds.Top;
                    while (Posicion < Lineas.Count)
                    {
                        string Texto = Lineas[Posicion];
                        SizeF Medida = e.Graphics.MeasureString(Texto.Length == 0 ? " " : Texto, Fuente, e.MarginBounds.Width);
                        if (Y + Medida.Height > e.MarginBounds.Bottom) break;
                        e.Graphics.DrawString(Texto, Fuente, Brushes.Black, new RectangleF(e.MarginBounds.Left, Y, e.MarginBounds.Width, Medida.Height));
                        Y += Medida.Height + 3; Posicion++;
                    }
                    e.HasMorePages = Posicion < Lineas.Count;
                };
                Vista.ShowDialog(Ventana);
            }
        }
    }
}
