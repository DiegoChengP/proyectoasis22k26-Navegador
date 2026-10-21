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
        internal static DataRow Seleccionar(Form Padre, string Titulo, DataTable Datos)
        {
            using (var Ventana = new Form { Text = Titulo, Width = 1000, Height = 480, StartPosition = FormStartPosition.CenterParent, MinimizeBox = false, MaximizeBox = false, ShowInTaskbar = false, BackColor = Color.FromArgb(248,245,249) })
            using (var Buscar = new TextBox { Name = "VentasTxtBuscar", Dock = DockStyle.Top, Font = new Font("Tahoma",11) })
            using (var Grid = new DataGridView { Name = "VentasDgvSeleccion", Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = true, AllowUserToAddRows = false, AllowUserToDeleteRows = false, MultiSelect = false, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, RowHeadersVisible = false, BackgroundColor = Color.White, EnableHeadersVisualStyles = false })
            using (var Pie = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 44, FlowDirection = FlowDirection.RightToLeft })
            using (var Aceptar = new Button { Name = "VentasBtnSeleccionar", Text = "Seleccionar", Width = 110, Height = 32, BackColor = Color.FromArgb(123,63,147), ForeColor = Color.White, FlatStyle = FlatStyle.Flat })
            using (var Cancelar = new Button { Text = "Cancelar", Width = 100, Height = 32, DialogResult = DialogResult.Cancel })
            {
                Grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(59,29,74); Grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                Grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(228,212,234); Grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(59,29,74);
                Grid.DataSource = Datos; foreach (DataGridViewColumn C in Grid.Columns) if (C.Name == "Id" || C.Name.StartsWith("id")) C.Visible = false;
                DataRow Elegida = null;
                Action Elegir = () => { var F=Grid.CurrentRow == null ? null : Grid.CurrentRow.DataBoundItem as DataRowView; if (F != null) { Elegida=F.Row; Ventana.DialogResult=DialogResult.OK; } };
                Aceptar.Click += (a,b) => Elegir(); Grid.CellDoubleClick += (a,b) => { if(b.RowIndex>=0) Elegir(); };
                Buscar.TextChanged += (a,b) =>
                {
                    var Filtro=Datos.Clone(); string Texto=Buscar.Text.Trim();
                    foreach(DataRow F in Datos.Rows) if (Texto.Length == 0 || Array.Exists(F.ItemArray, Valor => Convert.ToString(Valor).IndexOf(Texto,StringComparison.OrdinalIgnoreCase)>=0)) Filtro.ImportRow(F);
                    Grid.DataSource=Filtro;
                };
                Pie.Controls.Add(Aceptar); Pie.Controls.Add(Cancelar); Ventana.Controls.Add(Grid); Ventana.Controls.Add(Buscar); Ventana.Controls.Add(Pie);
                Ventana.AcceptButton=Aceptar; Ventana.CancelButton=Cancelar; Ventana.ShowDialog(Padre); return Elegida;
            }
        }
        internal static string Motivo(Form Padre, string Titulo)
        {
            using(var Ventana=new Form { Text=Titulo,Width=520,Height=205,StartPosition=FormStartPosition.CenterParent,FormBorderStyle=FormBorderStyle.FixedDialog,MinimizeBox=false,MaximizeBox=false,ShowInTaskbar=false,BackColor=Color.FromArgb(248,245,249) })
            using(var Etiqueta=new Label { Text="Motivo de anulacion",Left=16,Top=16,Width=460 })
            using(var Texto=new TextBox { Name="VentasTxtMotivo",Left=16,Top=44,Width=470,Height=55,Multiline=true,MaxLength=180 })
            using(var Aceptar=new Button { Name="VentasBtnAceptarMotivo",Text="Anular",Left=260,Top=112,Width=110,DialogResult=DialogResult.OK,BackColor=Color.FromArgb(123,63,147),ForeColor=Color.White })
            using(var Cancelar=new Button { Text="Cancelar",Left=380,Top=112,Width=105,DialogResult=DialogResult.Cancel })
            {
                Ventana.Controls.AddRange(new Control[]{Etiqueta,Texto,Aceptar,Cancelar}); Ventana.AcceptButton=Aceptar; Ventana.CancelButton=Cancelar;
                return Ventana.ShowDialog(Padre)==DialogResult.OK ? Texto.Text.Trim() : null;
            }
        }
    }
}
