/*
 * Área: Contabilidad
 * Autor: Gabriel André Guillén Pocón
 * Carné: 0901-23-1998
 * Fecha: 14/10/2026
 * Propósito: prototipo no funcional de pólizas locales de Embutidos de Calidad,
 * basado en tblCuenta, tblPolizaEncabezado y tblPolizaDetalle de dbContabilidad.
 */
namespace CapaVista_Contabilidad
{
    partial class FrmPolizasLocales
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool Disposing)
        {
            if (Disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(Disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ContabilidadPnlEncabezado = new System.Windows.Forms.Panel();
            this.ContabilidadLblTitulo = new System.Windows.Forms.Label();
            this.ContabilidadLblFecha = new System.Windows.Forms.Label();
            this.ContabilidadLblPeriodo = new System.Windows.Forms.Label();
            this.ContabilidadLblEstado = new System.Windows.Forms.Label();
            this.ContabilidadLblOrigen = new System.Windows.Forms.Label();
            this.ContabilidadDtpFecha = new System.Windows.Forms.DateTimePicker();
            this.ContabilidadTxtPeriodo = new System.Windows.Forms.TextBox();
            this.ContabilidadCboEstado = new System.Windows.Forms.ComboBox();
            this.ContabilidadCboOrigen = new System.Windows.Forms.ComboBox();
            this.ContabilidadLblDescripcion = new System.Windows.Forms.Label();
            this.ContabilidadTxtDescripcion = new System.Windows.Forms.TextBox();
            this.ContabilidadPnlDetalle = new System.Windows.Forms.Panel();
            this.ContabilidadLblAgregarCuenta = new System.Windows.Forms.Label();
            this.ContabilidadCboCuenta = new System.Windows.Forms.ComboBox();
            this.ContabilidadBtnAgregar = new System.Windows.Forms.Button();
            this.ContabilidadDgvDetalle = new System.Windows.Forms.DataGridView();
            this.ContabilidadColCuenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContabilidadColDebe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContabilidadColHaber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContabilidadColQuitar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ContabilidadLblCuadre = new System.Windows.Forms.Label();
            this.ContabilidadLblTotalDebe = new System.Windows.Forms.Label();
            this.ContabilidadLblTotalHaber = new System.Windows.Forms.Label();
            this.ContabilidadLblEmpresa = new System.Windows.Forms.Label();
            this.ContabilidadBtnNueva = new System.Windows.Forms.Button();
            this.ContabilidadBtnLimpiarDetalle = new System.Windows.Forms.Button();
            this.ContabilidadBtnGuardar = new System.Windows.Forms.Button();
            this.ContabilidadBtnAplicar = new System.Windows.Forms.Button();
            this.ContabilidadLblObligatorioOrigen = new System.Windows.Forms.Label();
            this.ContabilidadLblObligatorioEstado = new System.Windows.Forms.Label();
            this.ContabilidadLblObligatorioFecha = new System.Windows.Forms.Label();
            this.ContabilidadLblObligatorioDescripcion = new System.Windows.Forms.Label();
            this.ContabilidadLblObligatorioCuenta = new System.Windows.Forms.Label();
            System.Windows.Forms.DataGridViewCellStyle ContabilidadEstiloDebe = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle ContabilidadEstiloHaber = new System.Windows.Forms.DataGridViewCellStyle();
            ContabilidadEstiloDebe.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            ContabilidadEstiloDebe.Format = "N2";
            ContabilidadEstiloDebe.NullValue = "0.00";
            ContabilidadEstiloHaber.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            ContabilidadEstiloHaber.Format = "N2";
            ContabilidadEstiloHaber.NullValue = "0.00";
            this.ContabilidadPnlEncabezado.SuspendLayout();
            this.ContabilidadPnlDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContabilidadDgvDetalle)).BeginInit();
            this.SuspendLayout();
            // ContabilidadPnlEncabezado
            this.ContabilidadPnlEncabezado.Name = "ContabilidadPnlEncabezado";
            this.ContabilidadPnlEncabezado.Location = new System.Drawing.Point(24, 24);
            this.ContabilidadPnlEncabezado.Size = new System.Drawing.Size(1132, 640);
            this.ContabilidadPnlEncabezado.TabIndex = 0;
            this.ContabilidadPnlEncabezado.BackColor = System.Drawing.Color.White;
            // ContabilidadLblTitulo
            this.ContabilidadLblTitulo.Name = "ContabilidadLblTitulo";
            this.ContabilidadLblTitulo.Location = new System.Drawing.Point(24, 18);
            this.ContabilidadLblTitulo.Size = new System.Drawing.Size(1084, 36);
            this.ContabilidadLblTitulo.TabIndex = 0;
            this.ContabilidadLblTitulo.Text = "Póliza Encabezado";
            this.ContabilidadLblTitulo.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblTitulo.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblTitulo.Font = new System.Drawing.Font("Lucida Fax", 18F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic);
            this.ContabilidadLblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblTitulo);
            // ContabilidadLblFecha
            this.ContabilidadLblFecha.Name = "ContabilidadLblFecha";
            this.ContabilidadLblFecha.Location = new System.Drawing.Point(24, 72);
            this.ContabilidadLblFecha.Size = new System.Drawing.Size(220, 22);
            this.ContabilidadLblFecha.TabIndex = 0;
            this.ContabilidadLblFecha.Text = "Fecha";
            this.ContabilidadLblFecha.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblFecha.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblFecha.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblFecha);
            // ContabilidadLblPeriodo
            this.ContabilidadLblPeriodo.Name = "ContabilidadLblPeriodo";
            this.ContabilidadLblPeriodo.Location = new System.Drawing.Point(264, 72);
            this.ContabilidadLblPeriodo.Size = new System.Drawing.Size(220, 22);
            this.ContabilidadLblPeriodo.TabIndex = 0;
            this.ContabilidadLblPeriodo.Text = "Período contable";
            this.ContabilidadLblPeriodo.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblPeriodo.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblPeriodo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblPeriodo);
            // ContabilidadLblEstado
            this.ContabilidadLblEstado.Name = "ContabilidadLblEstado";
            this.ContabilidadLblEstado.Location = new System.Drawing.Point(504, 72);
            this.ContabilidadLblEstado.Size = new System.Drawing.Size(270, 22);
            this.ContabilidadLblEstado.TabIndex = 0;
            this.ContabilidadLblEstado.Text = "Estado";
            this.ContabilidadLblEstado.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblEstado.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblEstado);
            // ContabilidadLblOrigen
            this.ContabilidadLblOrigen.Name = "ContabilidadLblOrigen";
            this.ContabilidadLblOrigen.Location = new System.Drawing.Point(794, 72);
            this.ContabilidadLblOrigen.Size = new System.Drawing.Size(314, 22);
            this.ContabilidadLblOrigen.TabIndex = 0;
            this.ContabilidadLblOrigen.Text = "Origen";
            this.ContabilidadLblOrigen.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblOrigen.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblOrigen.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblOrigen);
            // ContabilidadDtpFecha
            this.ContabilidadDtpFecha.Name = "ContabilidadDtpFecha";
            this.ContabilidadDtpFecha.Location = new System.Drawing.Point(24, 100);
            this.ContabilidadDtpFecha.Size = new System.Drawing.Size(220, 28);
            this.ContabilidadDtpFecha.TabIndex = 0;
            this.ContabilidadDtpFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadDtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ContabilidadDtpFecha.CustomFormat = "dd/MM/yyyy";
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadDtpFecha);
            // ContabilidadTxtPeriodo
            this.ContabilidadTxtPeriodo.Name = "ContabilidadTxtPeriodo";
            this.ContabilidadTxtPeriodo.Location = new System.Drawing.Point(264, 100);
            this.ContabilidadTxtPeriodo.Size = new System.Drawing.Size(220, 28);
            this.ContabilidadTxtPeriodo.TabIndex = 1;
            this.ContabilidadTxtPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadTxtPeriodo.ReadOnly = true;
            this.ContabilidadTxtPeriodo.Text = "2026-10";
            this.ContabilidadTxtPeriodo.BackColor = System.Drawing.Color.FromArgb(246, 247, 248);
            this.ContabilidadTxtPeriodo.TabStop = false;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadTxtPeriodo);
            // ContabilidadCboEstado
            this.ContabilidadCboEstado.Name = "ContabilidadCboEstado";
            this.ContabilidadCboEstado.Location = new System.Drawing.Point(504, 100);
            this.ContabilidadCboEstado.Size = new System.Drawing.Size(270, 28);
            this.ContabilidadCboEstado.TabIndex = 2;
            this.ContabilidadCboEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadCboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContabilidadCboEstado.FormattingEnabled = true;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadCboEstado);
            // ContabilidadCboOrigen
            this.ContabilidadCboOrigen.Name = "ContabilidadCboOrigen";
            this.ContabilidadCboOrigen.Location = new System.Drawing.Point(794, 100);
            this.ContabilidadCboOrigen.Size = new System.Drawing.Size(314, 28);
            this.ContabilidadCboOrigen.TabIndex = 3;
            this.ContabilidadCboOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadCboOrigen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ContabilidadCboOrigen.FormattingEnabled = true;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadCboOrigen);
            // ContabilidadLblDescripcion
            this.ContabilidadLblDescripcion.Name = "ContabilidadLblDescripcion";
            this.ContabilidadLblDescripcion.Location = new System.Drawing.Point(24, 144);
            this.ContabilidadLblDescripcion.Size = new System.Drawing.Size(1084, 22);
            this.ContabilidadLblDescripcion.TabIndex = 0;
            this.ContabilidadLblDescripcion.Text = "Descripción";
            this.ContabilidadLblDescripcion.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblDescripcion.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblDescripcion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblDescripcion);
            // ContabilidadTxtDescripcion
            this.ContabilidadTxtDescripcion.Name = "ContabilidadTxtDescripcion";
            this.ContabilidadTxtDescripcion.Location = new System.Drawing.Point(24, 172);
            this.ContabilidadTxtDescripcion.Size = new System.Drawing.Size(1084, 28);
            this.ContabilidadTxtDescripcion.TabIndex = 4;
            this.ContabilidadTxtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadTxtDescripcion.MaxLength = 255;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadTxtDescripcion);
            // ContabilidadPnlDetalle
            this.ContabilidadPnlDetalle.Name = "ContabilidadPnlDetalle";
            this.ContabilidadPnlDetalle.Location = new System.Drawing.Point(24, 220);
            this.ContabilidadPnlDetalle.Size = new System.Drawing.Size(1084, 336);
            this.ContabilidadPnlDetalle.TabIndex = 0;
            this.ContabilidadPnlDetalle.BackColor = System.Drawing.Color.White;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadPnlDetalle);
            // ContabilidadLblAgregarCuenta
            this.ContabilidadLblAgregarCuenta.Name = "ContabilidadLblAgregarCuenta";
            this.ContabilidadLblAgregarCuenta.Location = new System.Drawing.Point(20, 16);
            this.ContabilidadLblAgregarCuenta.Size = new System.Drawing.Size(500, 22);
            this.ContabilidadLblAgregarCuenta.TabIndex = 0;
            this.ContabilidadLblAgregarCuenta.Text = "Agregar cuenta...";
            this.ContabilidadLblAgregarCuenta.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblAgregarCuenta.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblAgregarCuenta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadLblAgregarCuenta);
            // ContabilidadCboCuenta
            this.ContabilidadCboCuenta.Name = "ContabilidadCboCuenta";
            this.ContabilidadCboCuenta.Location = new System.Drawing.Point(20, 44);
            this.ContabilidadCboCuenta.Size = new System.Drawing.Size(500, 28);
            this.ContabilidadCboCuenta.TabIndex = 0;
            this.ContabilidadCboCuenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadCboCuenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.ContabilidadCboCuenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ContabilidadCboCuenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ContabilidadCboCuenta.FormattingEnabled = true;
            this.ContabilidadCboCuenta.DropDownWidth = 500;
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadCboCuenta);
            // ContabilidadBtnAgregar
            this.ContabilidadBtnAgregar.Name = "ContabilidadBtnAgregar";
            this.ContabilidadBtnAgregar.Location = new System.Drawing.Point(536, 38);
            this.ContabilidadBtnAgregar.Size = new System.Drawing.Size(136, 42);
            this.ContabilidadBtnAgregar.TabIndex = 1;
            this.ContabilidadBtnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadBtnAgregar.Text = "Agregar";
            this.ContabilidadBtnAgregar.BackColor = System.Drawing.Color.FromArgb(197, 143, 62);
            this.ContabilidadBtnAgregar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.ContabilidadBtnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContabilidadBtnAgregar.FlatAppearance.BorderSize = 0;
            this.ContabilidadBtnAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(214, 160, 80);
            this.ContabilidadBtnAgregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(169, 120, 50);
            this.ContabilidadBtnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContabilidadBtnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadBtnAgregar.UseVisualStyleBackColor = false;
            this.ContabilidadBtnAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContabilidadBtnAgregar.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_mas;
            this.ContabilidadBtnAgregar.UseVisualStyleBackColor = false;
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadBtnAgregar);
            // ContabilidadDgvDetalle
            this.ContabilidadDgvDetalle.Name = "ContabilidadDgvDetalle";
            this.ContabilidadDgvDetalle.Location = new System.Drawing.Point(20, 94);
            this.ContabilidadDgvDetalle.Size = new System.Drawing.Size(1044, 182);
            System.Windows.Forms.DataGridViewCellStyle EstiloEncabezadoContabilidadDgvDetalle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle EstiloCeldasContabilidadDgvDetalle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle EstiloAlternaContabilidadDgvDetalle = new System.Windows.Forms.DataGridViewCellStyle();
            this.ContabilidadDgvDetalle.BackgroundColor = System.Drawing.Color.White;
            this.ContabilidadDgvDetalle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadDgvDetalle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ContabilidadDgvDetalle.GridColor = System.Drawing.Color.FromArgb(212, 222, 228);
            this.ContabilidadDgvDetalle.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ContabilidadDgvDetalle.RowHeadersVisible = false;
            this.ContabilidadDgvDetalle.AllowUserToAddRows = false;
            this.ContabilidadDgvDetalle.AllowUserToDeleteRows = false;
            this.ContabilidadDgvDetalle.AllowUserToResizeRows = false;
            this.ContabilidadDgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ContabilidadDgvDetalle.MultiSelect = false;
            this.ContabilidadDgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ContabilidadDgvDetalle.EnableHeadersVisualStyles = false;
            this.ContabilidadDgvDetalle.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.ContabilidadDgvDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ContabilidadDgvDetalle.ColumnHeadersHeight = 38;
            EstiloEncabezadoContabilidadDgvDetalle.BackColor = System.Drawing.Color.FromArgb(35, 50, 61);
            EstiloEncabezadoContabilidadDgvDetalle.ForeColor = System.Drawing.Color.White;
            EstiloEncabezadoContabilidadDgvDetalle.SelectionBackColor = System.Drawing.Color.FromArgb(35, 50, 61);
            EstiloEncabezadoContabilidadDgvDetalle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            EstiloEncabezadoContabilidadDgvDetalle.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ContabilidadDgvDetalle.ColumnHeadersDefaultCellStyle = EstiloEncabezadoContabilidadDgvDetalle;
            this.ContabilidadDgvDetalle.RowTemplate.Height = 34;
            EstiloCeldasContabilidadDgvDetalle.Font = new System.Drawing.Font("Segoe UI", 10F);
            EstiloCeldasContabilidadDgvDetalle.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            EstiloCeldasContabilidadDgvDetalle.BackColor = System.Drawing.Color.White;
            EstiloCeldasContabilidadDgvDetalle.SelectionBackColor = System.Drawing.Color.FromArgb(212, 222, 228);
            EstiloCeldasContabilidadDgvDetalle.SelectionForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            EstiloCeldasContabilidadDgvDetalle.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.ContabilidadDgvDetalle.DefaultCellStyle = EstiloCeldasContabilidadDgvDetalle;
            EstiloAlternaContabilidadDgvDetalle.BackColor = System.Drawing.Color.FromArgb(246, 247, 248);
            this.ContabilidadDgvDetalle.AlternatingRowsDefaultCellStyle = EstiloAlternaContabilidadDgvDetalle;
            this.ContabilidadDgvDetalle.TabIndex = 2;
            this.ContabilidadDgvDetalle.AutoGenerateColumns = false;
            this.ContabilidadDgvDetalle.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ContabilidadDgvDetalle.AllowUserToOrderColumns = false;
            this.ContabilidadDgvDetalle.AllowUserToResizeColumns = false;
            this.ContabilidadDgvDetalle.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystrokeOrF2;
            this.ContabilidadDgvDetalle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadDgvDetalle);
            // ContabilidadColCuenta
            this.ContabilidadColCuenta.Name = "ContabilidadColCuenta";
            this.ContabilidadColCuenta.HeaderText = "Cuenta";
            this.ContabilidadColCuenta.DataPropertyName = "Cuenta";
            this.ContabilidadColCuenta.ReadOnly = true;
            this.ContabilidadColCuenta.FillWeight = 520;
            this.ContabilidadColCuenta.MinimumWidth = 300;
            this.ContabilidadColCuenta.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // ContabilidadColDebe
            this.ContabilidadColDebe.Name = "ContabilidadColDebe";
            this.ContabilidadColDebe.HeaderText = "Debe";
            this.ContabilidadColDebe.DataPropertyName = "Debe";
            this.ContabilidadColDebe.ReadOnly = false;
            this.ContabilidadColDebe.FillWeight = 240;
            this.ContabilidadColDebe.MinimumWidth = 200;
            this.ContabilidadColDebe.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ContabilidadColDebe.ValueType = typeof(decimal);
            this.ContabilidadColDebe.DefaultCellStyle = ContabilidadEstiloDebe;
            // ContabilidadColHaber
            this.ContabilidadColHaber.Name = "ContabilidadColHaber";
            this.ContabilidadColHaber.HeaderText = "Haber";
            this.ContabilidadColHaber.DataPropertyName = "Haber";
            this.ContabilidadColHaber.ReadOnly = false;
            this.ContabilidadColHaber.FillWeight = 240;
            this.ContabilidadColHaber.MinimumWidth = 200;
            this.ContabilidadColHaber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ContabilidadColHaber.ValueType = typeof(decimal);
            this.ContabilidadColHaber.DefaultCellStyle = ContabilidadEstiloHaber;
            // ContabilidadColQuitar
            this.ContabilidadColQuitar.Name = "ContabilidadColQuitar";
            this.ContabilidadColQuitar.HeaderText = "";
            this.ContabilidadColQuitar.ToolTipText = "Quitar cuenta";
            this.ContabilidadColQuitar.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_quitar_oscuro;
            this.ContabilidadColQuitar.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.ContabilidadColQuitar.ReadOnly = true;
            this.ContabilidadColQuitar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ContabilidadColQuitar.Width = 44;
            this.ContabilidadColQuitar.MinimumWidth = 44;
            this.ContabilidadColQuitar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ContabilidadDgvDetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { this.ContabilidadColCuenta, this.ContabilidadColDebe, this.ContabilidadColHaber, this.ContabilidadColQuitar });
            // ContabilidadLblCuadre
            this.ContabilidadLblCuadre.Name = "ContabilidadLblCuadre";
            this.ContabilidadLblCuadre.Location = new System.Drawing.Point(20, 290);
            this.ContabilidadLblCuadre.Size = new System.Drawing.Size(500, 28);
            this.ContabilidadLblCuadre.TabIndex = 0;
            this.ContabilidadLblCuadre.Text = "Agregue las cuentas de la póliza";
            this.ContabilidadLblCuadre.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblCuadre.ForeColor = System.Drawing.Color.FromArgb(70, 104, 125);
            this.ContabilidadLblCuadre.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadLblCuadre);
            // ContabilidadLblTotalDebe
            this.ContabilidadLblTotalDebe.Name = "ContabilidadLblTotalDebe";
            this.ContabilidadLblTotalDebe.Location = new System.Drawing.Point(540, 290);
            this.ContabilidadLblTotalDebe.Size = new System.Drawing.Size(240, 28);
            this.ContabilidadLblTotalDebe.TabIndex = 0;
            this.ContabilidadLblTotalDebe.Text = "Total debe: Q 0.00";
            this.ContabilidadLblTotalDebe.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblTotalDebe.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblTotalDebe.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadLblTotalDebe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadLblTotalDebe);
            // ContabilidadLblTotalHaber
            this.ContabilidadLblTotalHaber.Name = "ContabilidadLblTotalHaber";
            this.ContabilidadLblTotalHaber.Location = new System.Drawing.Point(780, 290);
            this.ContabilidadLblTotalHaber.Size = new System.Drawing.Size(240, 28);
            this.ContabilidadLblTotalHaber.TabIndex = 0;
            this.ContabilidadLblTotalHaber.Text = "Total haber: Q 0.00";
            this.ContabilidadLblTotalHaber.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblTotalHaber.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadLblTotalHaber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadLblTotalHaber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadLblTotalHaber);
            // ContabilidadLblEmpresa
            this.ContabilidadLblEmpresa.Name = "ContabilidadLblEmpresa";
            this.ContabilidadLblEmpresa.Location = new System.Drawing.Point(24, 576);
            this.ContabilidadLblEmpresa.Size = new System.Drawing.Size(310, 46);
            this.ContabilidadLblEmpresa.TabIndex = 0;
            this.ContabilidadLblEmpresa.Text = "Embutidos de Calidad";
            this.ContabilidadLblEmpresa.BackColor = System.Drawing.Color.White;
            this.ContabilidadLblEmpresa.ForeColor = System.Drawing.Color.FromArgb(70, 104, 125);
            this.ContabilidadLblEmpresa.Font = new System.Drawing.Font("Tahoma", 10F);
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblEmpresa);
            // ContabilidadBtnNueva
            this.ContabilidadBtnNueva.Name = "ContabilidadBtnNueva";
            this.ContabilidadBtnNueva.Location = new System.Drawing.Point(466, 576);
            this.ContabilidadBtnNueva.Size = new System.Drawing.Size(120, 42);
            this.ContabilidadBtnNueva.TabIndex = 5;
            this.ContabilidadBtnNueva.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadBtnNueva.Text = "Nueva";
            this.ContabilidadBtnNueva.BackColor = System.Drawing.Color.FromArgb(212, 222, 228);
            this.ContabilidadBtnNueva.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadBtnNueva.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContabilidadBtnNueva.FlatAppearance.BorderSize = 0;
            this.ContabilidadBtnNueva.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(195, 209, 218);
            this.ContabilidadBtnNueva.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(176, 193, 205);
            this.ContabilidadBtnNueva.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContabilidadBtnNueva.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadBtnNueva.UseVisualStyleBackColor = false;
            this.ContabilidadBtnNueva.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnNueva.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnNueva.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContabilidadBtnNueva.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_nuevo_oscuro;
            this.ContabilidadBtnNueva.UseVisualStyleBackColor = false;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadBtnNueva);
            // ContabilidadBtnLimpiarDetalle
            this.ContabilidadBtnLimpiarDetalle.Name = "ContabilidadBtnLimpiarDetalle";
            this.ContabilidadBtnLimpiarDetalle.Location = new System.Drawing.Point(598, 576);
            this.ContabilidadBtnLimpiarDetalle.Size = new System.Drawing.Size(170, 42);
            this.ContabilidadBtnLimpiarDetalle.TabIndex = 6;
            this.ContabilidadBtnLimpiarDetalle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadBtnLimpiarDetalle.Text = "Limpiar detalle";
            this.ContabilidadBtnLimpiarDetalle.BackColor = System.Drawing.Color.FromArgb(212, 222, 228);
            this.ContabilidadBtnLimpiarDetalle.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadBtnLimpiarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContabilidadBtnLimpiarDetalle.FlatAppearance.BorderSize = 0;
            this.ContabilidadBtnLimpiarDetalle.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(195, 209, 218);
            this.ContabilidadBtnLimpiarDetalle.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(176, 193, 205);
            this.ContabilidadBtnLimpiarDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContabilidadBtnLimpiarDetalle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadBtnLimpiarDetalle.UseVisualStyleBackColor = false;
            this.ContabilidadBtnLimpiarDetalle.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnLimpiarDetalle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnLimpiarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContabilidadBtnLimpiarDetalle.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_limpiar_oscuro;
            this.ContabilidadBtnLimpiarDetalle.UseVisualStyleBackColor = false;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadBtnLimpiarDetalle);
            // ContabilidadBtnGuardar
            this.ContabilidadBtnGuardar.Name = "ContabilidadBtnGuardar";
            this.ContabilidadBtnGuardar.Location = new System.Drawing.Point(780, 576);
            this.ContabilidadBtnGuardar.Size = new System.Drawing.Size(190, 42);
            this.ContabilidadBtnGuardar.TabIndex = 7;
            this.ContabilidadBtnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadBtnGuardar.Text = "Guardar borrador";
            this.ContabilidadBtnGuardar.BackColor = System.Drawing.Color.FromArgb(70, 104, 125);
            this.ContabilidadBtnGuardar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.ContabilidadBtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContabilidadBtnGuardar.FlatAppearance.BorderSize = 0;
            this.ContabilidadBtnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(88, 123, 145);
            this.ContabilidadBtnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(52, 77, 93);
            this.ContabilidadBtnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContabilidadBtnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadBtnGuardar.UseVisualStyleBackColor = false;
            this.ContabilidadBtnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContabilidadBtnGuardar.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_guardar;
            this.ContabilidadBtnGuardar.UseVisualStyleBackColor = false;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadBtnGuardar);
            // ContabilidadBtnAplicar
            this.ContabilidadBtnAplicar.Name = "ContabilidadBtnAplicar";
            this.ContabilidadBtnAplicar.Location = new System.Drawing.Point(982, 576);
            this.ContabilidadBtnAplicar.Size = new System.Drawing.Size(126, 42);
            this.ContabilidadBtnAplicar.TabIndex = 8;
            this.ContabilidadBtnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ContabilidadBtnAplicar.Text = "Aplicar";
            this.ContabilidadBtnAplicar.BackColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.ContabilidadBtnAplicar.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
            this.ContabilidadBtnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ContabilidadBtnAplicar.FlatAppearance.BorderSize = 0;
            this.ContabilidadBtnAplicar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(52, 77, 93);
            this.ContabilidadBtnAplicar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(28, 40, 49);
            this.ContabilidadBtnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ContabilidadBtnAplicar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadBtnAplicar.UseVisualStyleBackColor = false;
            this.ContabilidadBtnAplicar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnAplicar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ContabilidadBtnAplicar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ContabilidadBtnAplicar.Image = global::CapaVista_Contabilidad.Properties.Resources.ico_aplicar;
            this.ContabilidadBtnAplicar.UseVisualStyleBackColor = false;
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadBtnAplicar);
            // FrmPolizasLocales
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(246, 247, 248);
            this.ClientSize = new System.Drawing.Size(1180, 688);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(35, 50, 61);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.ContabilidadLblObligatorioOrigen.Name = "ContabilidadLblObligatorioOrigen";
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblObligatorioOrigen);
            this.ContabilidadLblObligatorioOrigen.AutoSize = true;
            this.ContabilidadLblObligatorioOrigen.Text = "*";
            this.ContabilidadLblObligatorioOrigen.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadLblObligatorioOrigen.ForeColor = System.Drawing.Color.Firebrick;
            this.ContabilidadLblObligatorioOrigen.BackColor = System.Drawing.Color.Transparent;
            this.ContabilidadLblObligatorioOrigen.Location = new System.Drawing.Point(854, 72);
            this.ContabilidadLblObligatorioEstado.Name = "ContabilidadLblObligatorioEstado";
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblObligatorioEstado);
            this.ContabilidadLblObligatorioEstado.AutoSize = true;
            this.ContabilidadLblObligatorioEstado.Text = "*";
            this.ContabilidadLblObligatorioEstado.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadLblObligatorioEstado.ForeColor = System.Drawing.Color.Firebrick;
            this.ContabilidadLblObligatorioEstado.BackColor = System.Drawing.Color.Transparent;
            this.ContabilidadLblObligatorioEstado.Location = new System.Drawing.Point(566, 72);
            this.ContabilidadLblObligatorioFecha.Name = "ContabilidadLblObligatorioFecha";
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblObligatorioFecha);
            this.ContabilidadLblObligatorioFecha.AutoSize = true;
            this.ContabilidadLblObligatorioFecha.Text = "*";
            this.ContabilidadLblObligatorioFecha.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadLblObligatorioFecha.ForeColor = System.Drawing.Color.Firebrick;
            this.ContabilidadLblObligatorioFecha.BackColor = System.Drawing.Color.Transparent;
            this.ContabilidadLblObligatorioFecha.Location = new System.Drawing.Point(80, 72);
            this.ContabilidadLblObligatorioDescripcion.Name = "ContabilidadLblObligatorioDescripcion";
            this.ContabilidadPnlEncabezado.Controls.Add(this.ContabilidadLblObligatorioDescripcion);
            this.ContabilidadLblObligatorioDescripcion.AutoSize = true;
            this.ContabilidadLblObligatorioDescripcion.Text = "*";
            this.ContabilidadLblObligatorioDescripcion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadLblObligatorioDescripcion.ForeColor = System.Drawing.Color.Firebrick;
            this.ContabilidadLblObligatorioDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.ContabilidadLblObligatorioDescripcion.Location = new System.Drawing.Point(114, 144);
            this.ContabilidadLblObligatorioCuenta.Name = "ContabilidadLblObligatorioCuenta";
            this.ContabilidadPnlDetalle.Controls.Add(this.ContabilidadLblObligatorioCuenta);
            this.ContabilidadLblObligatorioCuenta.AutoSize = true;
            this.ContabilidadLblObligatorioCuenta.Text = "*";
            this.ContabilidadLblObligatorioCuenta.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ContabilidadLblObligatorioCuenta.ForeColor = System.Drawing.Color.Firebrick;
            this.ContabilidadLblObligatorioCuenta.BackColor = System.Drawing.Color.Transparent;
            this.ContabilidadLblObligatorioCuenta.Location = new System.Drawing.Point(146, 16);
            this.Name = "FrmPolizasLocales";
            this.Text = "10001 - PolizasLocales";
            this.Icon = global::CapaVista_Contabilidad.Properties.Resources.Icono_Contabilidad;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Controls.Add(this.ContabilidadPnlEncabezado);
            this.ContabilidadPnlEncabezado.ResumeLayout(false);
            this.ContabilidadPnlEncabezado.PerformLayout();
            this.ContabilidadPnlDetalle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ContabilidadDgvDetalle)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel ContabilidadPnlEncabezado;
        private System.Windows.Forms.Label ContabilidadLblTitulo;
        private System.Windows.Forms.Label ContabilidadLblFecha;
        private System.Windows.Forms.Label ContabilidadLblPeriodo;
        private System.Windows.Forms.Label ContabilidadLblEstado;
        private System.Windows.Forms.Label ContabilidadLblOrigen;
        private System.Windows.Forms.DateTimePicker ContabilidadDtpFecha;
        private System.Windows.Forms.TextBox ContabilidadTxtPeriodo;
        private System.Windows.Forms.ComboBox ContabilidadCboEstado;
        private System.Windows.Forms.ComboBox ContabilidadCboOrigen;
        private System.Windows.Forms.Label ContabilidadLblDescripcion;
        private System.Windows.Forms.TextBox ContabilidadTxtDescripcion;
        private System.Windows.Forms.Panel ContabilidadPnlDetalle;
        private System.Windows.Forms.Label ContabilidadLblAgregarCuenta;
        private System.Windows.Forms.ComboBox ContabilidadCboCuenta;
        private System.Windows.Forms.Button ContabilidadBtnAgregar;
        private System.Windows.Forms.DataGridView ContabilidadDgvDetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContabilidadColCuenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContabilidadColDebe;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContabilidadColHaber;
        private System.Windows.Forms.DataGridViewImageColumn ContabilidadColQuitar;
        private System.Windows.Forms.Label ContabilidadLblCuadre;
        private System.Windows.Forms.Label ContabilidadLblTotalDebe;
        private System.Windows.Forms.Label ContabilidadLblTotalHaber;
        private System.Windows.Forms.Label ContabilidadLblEmpresa;
        private System.Windows.Forms.Button ContabilidadBtnNueva;
        private System.Windows.Forms.Button ContabilidadBtnLimpiarDetalle;
        private System.Windows.Forms.Button ContabilidadBtnGuardar;
        private System.Windows.Forms.Button ContabilidadBtnAplicar;
        private System.Windows.Forms.Label ContabilidadLblObligatorioOrigen;
        private System.Windows.Forms.Label ContabilidadLblObligatorioEstado;
        private System.Windows.Forms.Label ContabilidadLblObligatorioFecha;
        private System.Windows.Forms.Label ContabilidadLblObligatorioDescripcion;
        private System.Windows.Forms.Label ContabilidadLblObligatorioCuenta;
    }
}
