/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Todos
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Roger Yankhel de Jesús Herrera Alcántara | Carné: 0901-23-2429
 * Natali Sofía Montenegro Portillo | Carné: 0901-23-10017
 * Matthew Andreé Juárez Xoy | Carné: 0901-23-4250
 * Julio Roberto Rosales Mejía | Carné: 0901-23-1426
 * Diego Alejandro Cheng Peña | Carné: 0901-22-8091
 * Encargado: Todos
 * Fecha de actualización: 14/10/2026
 * Propósito: Mostrar únicamente el diseño estandarizado de cierre de movimientos.
 */
namespace CapaVista_Ventas
{
    partial class FrmCierreMovimientosVentas
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.VentasTtpAccionesOriginales = new System.Windows.Forms.ToolTip(this.components);
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VentasPnlEncabezado = new System.Windows.Forms.Panel();
            this.VentasPbMarca = new System.Windows.Forms.PictureBox();
            this.VentasLblTitulo = new System.Windows.Forms.Label();
            this.VentasLblSubtitulo = new System.Windows.Forms.Label();
            this.VentasTbcCierre = new System.Windows.Forms.TabControl();
            this.VentasTbpPreparacion = new System.Windows.Forms.TabPage();
            this.VentasPnlPeriodo = new System.Windows.Forms.Panel();
            this.VentasLblDesde = new System.Windows.Forms.Label();
            this.VentasLblObligatorioDesde = new System.Windows.Forms.Label();
            this.VentasDtpDesde = new System.Windows.Forms.DateTimePicker();
            this.VentasLblHasta = new System.Windows.Forms.Label();
            this.VentasLblObligatorioHasta = new System.Windows.Forms.Label();
            this.VentasDtpHasta = new System.Windows.Forms.DateTimePicker();
            this.VentasLblCorte = new System.Windows.Forms.Label();
            this.VentasLblObligatorioCorte = new System.Windows.Forms.Label();
            this.VentasDtpCorte = new System.Windows.Forms.DateTimePicker();
            this.VentasLblTipo = new System.Windows.Forms.Label();
            this.VentasCboTipo = new System.Windows.Forms.ComboBox();
            this.VentasLblResponsable = new System.Windows.Forms.Label();
            this.VentasTxtResponsable = new System.Windows.Forms.TextBox();
            this.VentasLblEstadoPreparacion = new System.Windows.Forms.Label();
            this.VentasGbxRevision = new System.Windows.Forms.GroupBox();
            this.VentasChkDocumentos = new System.Windows.Forms.CheckBox();
            this.VentasChkPagos = new System.Windows.Forms.CheckBox();
            this.VentasPnlDocumentos = new System.Windows.Forms.Panel();
            this.VentasLblTituloDocumentos = new System.Windows.Forms.Label();
            this.VentasLblDocumentos = new System.Windows.Forms.Label();
            this.VentasPnlCargos = new System.Windows.Forms.Panel();
            this.VentasLblTituloCargos = new System.Windows.Forms.Label();
            this.VentasLblCargos = new System.Windows.Forms.Label();
            this.VentasPnlAbonos = new System.Windows.Forms.Panel();
            this.VentasLblTituloAbonos = new System.Windows.Forms.Label();
            this.VentasLblAbonos = new System.Windows.Forms.Label();
            this.VentasPnlSaldo = new System.Windows.Forms.Panel();
            this.VentasLblTituloSaldo = new System.Windows.Forms.Label();
            this.VentasLblSaldo = new System.Windows.Forms.Label();
            this.VentasDgvRevision = new System.Windows.Forms.DataGridView();
            this.VentasPnlPaginaRevision = new System.Windows.Forms.Panel();
            this.VentasLblPaginaRevision = new System.Windows.Forms.Label();
            this.VentasBtnPaginaRevisionAnterior = new System.Windows.Forms.Button();
            this.VentasBtnPaginaRevisionSiguiente = new System.Windows.Forms.Button();
            this.VentasLblObservaciones = new System.Windows.Forms.Label();
            this.VentasTxtObservaciones = new System.Windows.Forms.TextBox();
            this.VentasTbpHistorial = new System.Windows.Forms.TabPage();
            this.VentasPnlHistorialFiltros = new System.Windows.Forms.Panel();
            this.VentasLblDesdeHistorial = new System.Windows.Forms.Label();
            this.VentasDtpDesdeHistorial = new System.Windows.Forms.DateTimePicker();
            this.VentasLblHastaHistorial = new System.Windows.Forms.Label();
            this.VentasDtpHastaHistorial = new System.Windows.Forms.DateTimePicker();
            this.VentasLblEstadoHistorial = new System.Windows.Forms.Label();
            this.VentasCboEstadoHistorial = new System.Windows.Forms.ComboBox();
            this.VentasLblResponsableHistorial = new System.Windows.Forms.Label();
            this.VentasCboResponsableHistorial = new System.Windows.Forms.ComboBox();
            this.VentasBtnConsultarHistorial = new System.Windows.Forms.Button();
            this.VentasDgvHistorial = new System.Windows.Forms.DataGridView();
            this.VentasPnlPaginaHistorial = new System.Windows.Forms.Panel();
            this.VentasLblPaginaHistorial = new System.Windows.Forms.Label();
            this.VentasBtnPaginaHistorialAnterior = new System.Windows.Forms.Button();
            this.VentasBtnPaginaHistorialSiguiente = new System.Windows.Forms.Button();
            this.VentasBtnCerrar = new System.Windows.Forms.Button();
            this.VentasBtnRevisar = new System.Windows.Forms.Button();
            this.VentasBtnConfirmar = new System.Windows.Forms.Button();
            this.VentasBtnResumen = new System.Windows.Forms.Button();
            this.VentasPnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).BeginInit();
            this.VentasTbcCierre.SuspendLayout();
            this.VentasTbpPreparacion.SuspendLayout();
            this.VentasPnlPeriodo.SuspendLayout();
            this.VentasGbxRevision.SuspendLayout();
            this.VentasPnlDocumentos.SuspendLayout();
            this.VentasPnlCargos.SuspendLayout();
            this.VentasPnlAbonos.SuspendLayout();
            this.VentasPnlSaldo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvRevision)).BeginInit();
            this.VentasPnlPaginaRevision.SuspendLayout();
            this.VentasTbpHistorial.SuspendLayout();
            this.VentasPnlHistorialFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvHistorial)).BeginInit();
            this.VentasPnlPaginaHistorial.SuspendLayout();
            this.SuspendLayout();
            //
            // VentasPnlEncabezado
            //
            this.VentasPnlEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasPnlEncabezado.Controls.Add(this.VentasPbMarca);
            this.VentasPnlEncabezado.Controls.Add(this.VentasLblTitulo);
            this.VentasPnlEncabezado.Controls.Add(this.VentasLblSubtitulo);
            this.VentasPnlEncabezado.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlEncabezado.Name = "VentasPnlEncabezado";
            this.VentasPnlEncabezado.Size = new System.Drawing.Size(1100, 78);
            this.VentasPnlEncabezado.TabIndex = 0;
            //
            // VentasPbMarca
            //
            this.VentasPbMarca.Image = global::CapaVista_Ventas.Properties.Resources.IconoVentasImagen;
            this.VentasPbMarca.Location = new System.Drawing.Point(20, 13);
            this.VentasPbMarca.Name = "VentasPbMarca";
            this.VentasPbMarca.Size = new System.Drawing.Size(52, 52);
            this.VentasPbMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VentasPbMarca.TabIndex = 1;
            this.VentasPbMarca.TabStop = false;
            //
            // VentasLblTitulo
            //
            this.VentasLblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTitulo.Font = new System.Drawing.Font("Lucida Fax", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.VentasLblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasLblTitulo.Location = new System.Drawing.Point(87, 12);
            this.VentasLblTitulo.Name = "VentasLblTitulo";
            this.VentasLblTitulo.Size = new System.Drawing.Size(670, 35);
            this.VentasLblTitulo.TabIndex = 2;
            this.VentasLblTitulo.Text = "Cierre de movimientos";
            //
            // VentasLblSubtitulo
            //
            this.VentasLblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSubtitulo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasLblSubtitulo.Location = new System.Drawing.Point(89, 47);
            this.VentasLblSubtitulo.Name = "VentasLblSubtitulo";
            this.VentasLblSubtitulo.Size = new System.Drawing.Size(660, 23);
            this.VentasLblSubtitulo.TabIndex = 3;
            this.VentasLblSubtitulo.Text = "Revisión del período, corte e historial de cierres";
            //
            // VentasTbcCierre
            //
            this.VentasTbcCierre.Controls.Add(this.VentasTbpPreparacion);
            this.VentasTbcCierre.Controls.Add(this.VentasTbpHistorial);
            this.VentasTbcCierre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTbcCierre.Location = new System.Drawing.Point(20, 92);
            this.VentasTbcCierre.Name = "VentasTbcCierre";
            this.VentasTbcCierre.SelectedIndex = 0;
            this.VentasTbcCierre.Size = new System.Drawing.Size(1060, 500);
            this.VentasTbcCierre.TabIndex = 5;
            //
            // VentasTbpPreparacion
            //
            this.VentasTbpPreparacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlPeriodo);
            this.VentasTbpPreparacion.Controls.Add(this.VentasGbxRevision);
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlDocumentos);
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlCargos);
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlAbonos);
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlSaldo);
            this.VentasTbpPreparacion.Controls.Add(this.VentasDgvRevision);
            this.VentasTbpPreparacion.Controls.Add(this.VentasPnlPaginaRevision);
            this.VentasTbpPreparacion.Controls.Add(this.VentasLblObservaciones);
            this.VentasTbpPreparacion.Controls.Add(this.VentasTxtObservaciones);
            this.VentasTbpPreparacion.Location = new System.Drawing.Point(4, 32);
            this.VentasTbpPreparacion.Name = "VentasTbpPreparacion";
            this.VentasTbpPreparacion.Size = new System.Drawing.Size(1052, 464);
            this.VentasTbpPreparacion.TabIndex = 6;
            this.VentasTbpPreparacion.Text = "Preparar cierre";
            //
            // VentasPnlPeriodo
            //
            this.VentasPnlPeriodo.BackColor = System.Drawing.Color.White;
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblDesde);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblObligatorioDesde);
            this.VentasPnlPeriodo.Controls.Add(this.VentasDtpDesde);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblHasta);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblObligatorioHasta);
            this.VentasPnlPeriodo.Controls.Add(this.VentasDtpHasta);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblCorte);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblObligatorioCorte);
            this.VentasPnlPeriodo.Controls.Add(this.VentasDtpCorte);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblTipo);
            this.VentasPnlPeriodo.Controls.Add(this.VentasCboTipo);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblResponsable);
            this.VentasPnlPeriodo.Controls.Add(this.VentasTxtResponsable);
            this.VentasPnlPeriodo.Controls.Add(this.VentasLblEstadoPreparacion);
            this.VentasPnlPeriodo.Location = new System.Drawing.Point(8, 8);
            this.VentasPnlPeriodo.Name = "VentasPnlPeriodo";
            this.VentasPnlPeriodo.Size = new System.Drawing.Size(1036, 100);
            this.VentasPnlPeriodo.TabIndex = 8;
            //
            // VentasLblDesde
            //
            this.VentasLblDesde.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblDesde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDesde.Location = new System.Drawing.Point(14, 12);
            this.VentasLblDesde.Name = "VentasLblDesde";
            this.VentasLblDesde.Size = new System.Drawing.Size(146, 20);
            this.VentasLblDesde.TabIndex = 9;
            this.VentasLblDesde.Text = "Desde";
            //
            // VentasLblObligatorioDesde
            //
            this.VentasLblObligatorioDesde.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblObligatorioDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblObligatorioDesde.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblObligatorioDesde.Location = new System.Drawing.Point(61, 10);
            this.VentasLblObligatorioDesde.Name = "VentasLblObligatorioDesde";
            this.VentasLblObligatorioDesde.Size = new System.Drawing.Size(14, 21);
            this.VentasLblObligatorioDesde.TabIndex = 10;
            this.VentasLblObligatorioDesde.Text = "*";
            //
            // VentasDtpDesde
            //
            this.VentasDtpDesde.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpDesde.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpDesde.Location = new System.Drawing.Point(14, 37);
            this.VentasDtpDesde.Name = "VentasDtpDesde";
            this.VentasDtpDesde.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpDesde.TabIndex = 11;
            this.VentasDtpDesde.Value = new System.DateTime(2026, 10, 1, 0, 0, 0, 0);
            //
            // VentasLblHasta
            //
            this.VentasLblHasta.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblHasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblHasta.Location = new System.Drawing.Point(178, 12);
            this.VentasLblHasta.Name = "VentasLblHasta";
            this.VentasLblHasta.Size = new System.Drawing.Size(146, 20);
            this.VentasLblHasta.TabIndex = 12;
            this.VentasLblHasta.Text = "Hasta";
            //
            // VentasLblObligatorioHasta
            //
            this.VentasLblObligatorioHasta.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblObligatorioHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblObligatorioHasta.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblObligatorioHasta.Location = new System.Drawing.Point(225, 10);
            this.VentasLblObligatorioHasta.Name = "VentasLblObligatorioHasta";
            this.VentasLblObligatorioHasta.Size = new System.Drawing.Size(14, 21);
            this.VentasLblObligatorioHasta.TabIndex = 13;
            this.VentasLblObligatorioHasta.Text = "*";
            //
            // VentasDtpHasta
            //
            this.VentasDtpHasta.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpHasta.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpHasta.Location = new System.Drawing.Point(178, 37);
            this.VentasDtpHasta.Name = "VentasDtpHasta";
            this.VentasDtpHasta.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpHasta.TabIndex = 14;
            this.VentasDtpHasta.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            //
            // VentasLblCorte
            //
            this.VentasLblCorte.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCorte.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCorte.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCorte.Location = new System.Drawing.Point(342, 12);
            this.VentasLblCorte.Name = "VentasLblCorte";
            this.VentasLblCorte.Size = new System.Drawing.Size(146, 20);
            this.VentasLblCorte.TabIndex = 15;
            this.VentasLblCorte.Text = "Fecha de corte";
            //
            // VentasLblObligatorioCorte
            //
            this.VentasLblObligatorioCorte.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblObligatorioCorte.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblObligatorioCorte.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblObligatorioCorte.Location = new System.Drawing.Point(436, 10);
            this.VentasLblObligatorioCorte.Name = "VentasLblObligatorioCorte";
            this.VentasLblObligatorioCorte.Size = new System.Drawing.Size(14, 21);
            this.VentasLblObligatorioCorte.TabIndex = 16;
            this.VentasLblObligatorioCorte.Text = "*";
            //
            // VentasDtpCorte
            //
            this.VentasDtpCorte.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpCorte.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpCorte.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpCorte.Location = new System.Drawing.Point(342, 37);
            this.VentasDtpCorte.Name = "VentasDtpCorte";
            this.VentasDtpCorte.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpCorte.TabIndex = 17;
            this.VentasDtpCorte.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            //
            // VentasLblTipo
            //
            this.VentasLblTipo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTipo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTipo.Location = new System.Drawing.Point(512, 12);
            this.VentasLblTipo.Name = "VentasLblTipo";
            this.VentasLblTipo.Size = new System.Drawing.Size(224, 20);
            this.VentasLblTipo.TabIndex = 18;
            this.VentasLblTipo.Text = "Tipo de cierre";
            //
            // VentasCboTipo
            //
            this.VentasCboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboTipo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboTipo.Items.AddRange(new object[] {
            "Personalizado",
            "Diario",
            "Mensual"});
            this.VentasCboTipo.Location = new System.Drawing.Point(512, 37);
            this.VentasCboTipo.Name = "VentasCboTipo";
            this.VentasCboTipo.Size = new System.Drawing.Size(224, 33);
            this.VentasCboTipo.TabIndex = 19;
            //
            // VentasLblResponsable
            //
            this.VentasLblResponsable.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblResponsable.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblResponsable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblResponsable.Location = new System.Drawing.Point(760, 12);
            this.VentasLblResponsable.Name = "VentasLblResponsable";
            this.VentasLblResponsable.Size = new System.Drawing.Size(258, 20);
            this.VentasLblResponsable.TabIndex = 20;
            this.VentasLblResponsable.Text = "Responsable";
            //
            // VentasTxtResponsable
            //
            this.VentasTxtResponsable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtResponsable.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtResponsable.Location = new System.Drawing.Point(760, 37);
            this.VentasTxtResponsable.Name = "VentasTxtResponsable";
            this.VentasTxtResponsable.ReadOnly = true;
            this.VentasTxtResponsable.Size = new System.Drawing.Size(258, 32);
            this.VentasTxtResponsable.TabIndex = 21;
            this.VentasTxtResponsable.TabStop = false;
            this.VentasTxtResponsable.Text = "Por asignar";
            //
            // VentasLblEstadoPreparacion
            //
            this.VentasLblEstadoPreparacion.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblEstadoPreparacion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblEstadoPreparacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblEstadoPreparacion.Location = new System.Drawing.Point(14, 76);
            this.VentasLblEstadoPreparacion.Name = "VentasLblEstadoPreparacion";
            this.VentasLblEstadoPreparacion.Size = new System.Drawing.Size(1000, 21);
            this.VentasLblEstadoPreparacion.TabIndex = 22;
            this.VentasLblEstadoPreparacion.Text = "Estado: pendiente de revisión · No. de cierre: por asignar";
            //
            // VentasGbxRevision
            //
            this.VentasGbxRevision.BackColor = System.Drawing.Color.White;
            this.VentasGbxRevision.Controls.Add(this.VentasChkDocumentos);
            this.VentasGbxRevision.Controls.Add(this.VentasChkPagos);
            this.VentasGbxRevision.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasGbxRevision.Location = new System.Drawing.Point(8, 122);
            this.VentasGbxRevision.Name = "VentasGbxRevision";
            this.VentasGbxRevision.Size = new System.Drawing.Size(1036, 68);
            this.VentasGbxRevision.TabIndex = 23;
            this.VentasGbxRevision.TabStop = false;
            this.VentasGbxRevision.Text = "Verificación de documentos y pagos";
            //
            // VentasChkDocumentos
            //
            this.VentasChkDocumentos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasChkDocumentos.Location = new System.Drawing.Point(15, 28);
            this.VentasChkDocumentos.Name = "VentasChkDocumentos";
            this.VentasChkDocumentos.Size = new System.Drawing.Size(250, 23);
            this.VentasChkDocumentos.TabIndex = 24;
            this.VentasChkDocumentos.Text = "Documentos revisados";
            //
            // VentasChkPagos
            //
            this.VentasChkPagos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasChkPagos.Location = new System.Drawing.Point(281, 28);
            this.VentasChkPagos.Name = "VentasChkPagos";
            this.VentasChkPagos.Size = new System.Drawing.Size(250, 23);
            this.VentasChkPagos.TabIndex = 25;
            this.VentasChkPagos.Text = "Pagos conciliados";
            //
            // VentasPnlDocumentos
            //
            this.VentasPnlDocumentos.BackColor = System.Drawing.Color.White;
            this.VentasPnlDocumentos.Controls.Add(this.VentasLblTituloDocumentos);
            this.VentasPnlDocumentos.Controls.Add(this.VentasLblDocumentos);
            this.VentasPnlDocumentos.Location = new System.Drawing.Point(8, 204);
            this.VentasPnlDocumentos.Name = "VentasPnlDocumentos";
            this.VentasPnlDocumentos.Size = new System.Drawing.Size(250, 65);
            this.VentasPnlDocumentos.TabIndex = 28;
            //
            // VentasLblTituloDocumentos
            //
            this.VentasLblTituloDocumentos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloDocumentos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloDocumentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloDocumentos.Location = new System.Drawing.Point(14, 8);
            this.VentasLblTituloDocumentos.Name = "VentasLblTituloDocumentos";
            this.VentasLblTituloDocumentos.Size = new System.Drawing.Size(222, 20);
            this.VentasLblTituloDocumentos.TabIndex = 29;
            this.VentasLblTituloDocumentos.Text = "Documentos del período";
            //
            // VentasLblDocumentos
            //
            this.VentasLblDocumentos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDocumentos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.VentasLblDocumentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDocumentos.Location = new System.Drawing.Point(14, 30);
            this.VentasLblDocumentos.Name = "VentasLblDocumentos";
            this.VentasLblDocumentos.Size = new System.Drawing.Size(222, 29);
            this.VentasLblDocumentos.TabIndex = 30;
            this.VentasLblDocumentos.Text = "0";
            //
            // VentasPnlCargos
            //
            this.VentasPnlCargos.BackColor = System.Drawing.Color.White;
            this.VentasPnlCargos.Controls.Add(this.VentasLblTituloCargos);
            this.VentasPnlCargos.Controls.Add(this.VentasLblCargos);
            this.VentasPnlCargos.Location = new System.Drawing.Point(270, 204);
            this.VentasPnlCargos.Name = "VentasPnlCargos";
            this.VentasPnlCargos.Size = new System.Drawing.Size(250, 65);
            this.VentasPnlCargos.TabIndex = 31;
            //
            // VentasLblTituloCargos
            //
            this.VentasLblTituloCargos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloCargos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloCargos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloCargos.Location = new System.Drawing.Point(14, 8);
            this.VentasLblTituloCargos.Name = "VentasLblTituloCargos";
            this.VentasLblTituloCargos.Size = new System.Drawing.Size(222, 20);
            this.VentasLblTituloCargos.TabIndex = 32;
            this.VentasLblTituloCargos.Text = "Cargos";
            //
            // VentasLblCargos
            //
            this.VentasLblCargos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCargos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.VentasLblCargos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCargos.Location = new System.Drawing.Point(14, 30);
            this.VentasLblCargos.Name = "VentasLblCargos";
            this.VentasLblCargos.Size = new System.Drawing.Size(222, 29);
            this.VentasLblCargos.TabIndex = 33;
            this.VentasLblCargos.Text = "Q 0.00";
            //
            // VentasPnlAbonos
            //
            this.VentasPnlAbonos.BackColor = System.Drawing.Color.White;
            this.VentasPnlAbonos.Controls.Add(this.VentasLblTituloAbonos);
            this.VentasPnlAbonos.Controls.Add(this.VentasLblAbonos);
            this.VentasPnlAbonos.Location = new System.Drawing.Point(532, 204);
            this.VentasPnlAbonos.Name = "VentasPnlAbonos";
            this.VentasPnlAbonos.Size = new System.Drawing.Size(250, 65);
            this.VentasPnlAbonos.TabIndex = 34;
            //
            // VentasLblTituloAbonos
            //
            this.VentasLblTituloAbonos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloAbonos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloAbonos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloAbonos.Location = new System.Drawing.Point(14, 8);
            this.VentasLblTituloAbonos.Name = "VentasLblTituloAbonos";
            this.VentasLblTituloAbonos.Size = new System.Drawing.Size(222, 20);
            this.VentasLblTituloAbonos.TabIndex = 35;
            this.VentasLblTituloAbonos.Text = "Abonos";
            //
            // VentasLblAbonos
            //
            this.VentasLblAbonos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblAbonos.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.VentasLblAbonos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblAbonos.Location = new System.Drawing.Point(14, 30);
            this.VentasLblAbonos.Name = "VentasLblAbonos";
            this.VentasLblAbonos.Size = new System.Drawing.Size(222, 29);
            this.VentasLblAbonos.TabIndex = 36;
            this.VentasLblAbonos.Text = "Q 0.00";
            //
            // VentasPnlSaldo
            //
            this.VentasPnlSaldo.BackColor = System.Drawing.Color.White;
            this.VentasPnlSaldo.Controls.Add(this.VentasLblTituloSaldo);
            this.VentasPnlSaldo.Controls.Add(this.VentasLblSaldo);
            this.VentasPnlSaldo.Location = new System.Drawing.Point(794, 204);
            this.VentasPnlSaldo.Name = "VentasPnlSaldo";
            this.VentasPnlSaldo.Size = new System.Drawing.Size(250, 65);
            this.VentasPnlSaldo.TabIndex = 37;
            //
            // VentasLblTituloSaldo
            //
            this.VentasLblTituloSaldo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloSaldo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloSaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloSaldo.Location = new System.Drawing.Point(14, 8);
            this.VentasLblTituloSaldo.Name = "VentasLblTituloSaldo";
            this.VentasLblTituloSaldo.Size = new System.Drawing.Size(222, 20);
            this.VentasLblTituloSaldo.TabIndex = 38;
            this.VentasLblTituloSaldo.Text = "Saldo al corte";
            //
            // VentasLblSaldo
            //
            this.VentasLblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSaldo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.VentasLblSaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblSaldo.Location = new System.Drawing.Point(14, 30);
            this.VentasLblSaldo.Name = "VentasLblSaldo";
            this.VentasLblSaldo.Size = new System.Drawing.Size(222, 29);
            this.VentasLblSaldo.TabIndex = 39;
            this.VentasLblSaldo.Text = "Q 0.00";
            //
            // VentasDgvRevision
            //
            this.VentasDgvRevision.AllowUserToAddRows = false;
            this.VentasDgvRevision.AllowUserToDeleteRows = false;
            this.VentasDgvRevision.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvRevision.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.VentasDgvRevision.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvRevision.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvRevision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvRevision.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.VentasDgvRevision.ColumnHeadersHeight = 32;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VentasDgvRevision.DefaultCellStyle = dataGridViewCellStyle9;
            this.VentasDgvRevision.EnableHeadersVisualStyles = false;
            this.VentasDgvRevision.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasDgvRevision.Location = new System.Drawing.Point(8, 282);
            this.VentasDgvRevision.MultiSelect = false;
            this.VentasDgvRevision.Name = "VentasDgvRevision";
            this.VentasDgvRevision.ReadOnly = true;
            this.VentasDgvRevision.RowHeadersVisible = false;
            this.VentasDgvRevision.RowHeadersWidth = 51;
            this.VentasDgvRevision.RowTemplate.Height = 27;
            this.VentasDgvRevision.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvRevision.Size = new System.Drawing.Size(1036, 80);
            this.VentasDgvRevision.TabIndex = 40;
            //
            // VentasPnlPaginaRevision
            //
            this.VentasPnlPaginaRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasPnlPaginaRevision.Controls.Add(this.VentasLblPaginaRevision);
            this.VentasPnlPaginaRevision.Controls.Add(this.VentasBtnPaginaRevisionAnterior);
            this.VentasPnlPaginaRevision.Controls.Add(this.VentasBtnPaginaRevisionSiguiente);
            this.VentasPnlPaginaRevision.Location = new System.Drawing.Point(8, 368);
            this.VentasPnlPaginaRevision.Name = "VentasPnlPaginaRevision";
            this.VentasPnlPaginaRevision.Size = new System.Drawing.Size(1036, 70);
            this.VentasPnlPaginaRevision.TabIndex = 41;
            //
            // VentasLblPaginaRevision
            //
            this.VentasLblPaginaRevision.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPaginaRevision.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblPaginaRevision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPaginaRevision.Location = new System.Drawing.Point(12, 10);
            this.VentasLblPaginaRevision.Name = "VentasLblPaginaRevision";
            this.VentasLblPaginaRevision.Size = new System.Drawing.Size(530, 21);
            this.VentasLblPaginaRevision.TabIndex = 42;
            this.VentasLblPaginaRevision.Text = "0 registros · Página 1 de 1";
            //
            // VentasBtnPaginaRevisionAnterior
            //

            this.VentasBtnPaginaRevisionAnterior.Enabled = false;

            this.VentasBtnPaginaRevisionAnterior.Location = new System.Drawing.Point(738, 3);
            this.VentasBtnPaginaRevisionAnterior.Name = "VentasBtnPaginaRevisionAnterior";
            this.VentasBtnPaginaRevisionAnterior.AccessibleDescription = "Anterior";
            this.VentasBtnPaginaRevisionAnterior.AccessibleName = "Anterior";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnPaginaRevisionAnterior.Text = string.Empty;
            this.VentasBtnPaginaRevisionAnterior.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnPaginaRevisionAnterior.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnPaginaRevisionAnterior.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalAnteriorVentas;
            this.VentasBtnPaginaRevisionAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnPaginaRevisionAnterior.Image = null;
            this.VentasBtnPaginaRevisionAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaRevisionAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaRevisionAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnPaginaRevisionAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnPaginaRevisionAnterior.FlatAppearance.BorderSize = 0;
            this.VentasBtnPaginaRevisionAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaRevisionAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaRevisionAnterior.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnPaginaRevisionAnterior.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnPaginaRevisionAnterior.UseVisualStyleBackColor = false;
            this.VentasBtnPaginaRevisionAnterior.UseCompatibleTextRendering = false;
            this.VentasBtnPaginaRevisionAnterior.Size = new System.Drawing.Size(61, 64);
            this.VentasBtnPaginaRevisionAnterior.TabIndex = 43;

            //
            // VentasBtnPaginaRevisionSiguiente
            //

            this.VentasBtnPaginaRevisionSiguiente.Enabled = false;

            this.VentasBtnPaginaRevisionSiguiente.Location = new System.Drawing.Point(888, 3);
            this.VentasBtnPaginaRevisionSiguiente.Name = "VentasBtnPaginaRevisionSiguiente";
            this.VentasBtnPaginaRevisionSiguiente.AccessibleDescription = "Siguiente";
            this.VentasBtnPaginaRevisionSiguiente.AccessibleName = "Siguiente";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnPaginaRevisionSiguiente.Text = string.Empty;
            this.VentasBtnPaginaRevisionSiguiente.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnPaginaRevisionSiguiente.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnPaginaRevisionSiguiente.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSiguienteVentas;
            this.VentasBtnPaginaRevisionSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnPaginaRevisionSiguiente.Image = null;
            this.VentasBtnPaginaRevisionSiguiente.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaRevisionSiguiente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaRevisionSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnPaginaRevisionSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnPaginaRevisionSiguiente.FlatAppearance.BorderSize = 0;
            this.VentasBtnPaginaRevisionSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaRevisionSiguiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaRevisionSiguiente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnPaginaRevisionSiguiente.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnPaginaRevisionSiguiente.UseVisualStyleBackColor = false;
            this.VentasBtnPaginaRevisionSiguiente.UseCompatibleTextRendering = false;
            this.VentasBtnPaginaRevisionSiguiente.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnPaginaRevisionSiguiente.TabIndex = 44;

            //
            // VentasLblObservaciones
            //
            this.VentasLblObservaciones.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblObservaciones.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblObservaciones.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblObservaciones.Location = new System.Drawing.Point(8, 447);
            this.VentasLblObservaciones.Name = "VentasLblObservaciones";
            this.VentasLblObservaciones.Size = new System.Drawing.Size(114, 20);
            this.VentasLblObservaciones.TabIndex = 45;
            this.VentasLblObservaciones.Text = "Observaciones";
            //
            // VentasTxtObservaciones
            //
            this.VentasTxtObservaciones.BackColor = System.Drawing.Color.White;
            this.VentasTxtObservaciones.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtObservaciones.Location = new System.Drawing.Point(128, 443);
            this.VentasTxtObservaciones.Multiline = false;
            this.VentasTxtObservaciones.Name = "VentasTxtObservaciones";
            this.VentasTxtObservaciones.Multiline = false;
            this.VentasTxtObservaciones.Size = new System.Drawing.Size(916, 22);
            this.VentasTxtObservaciones.TabIndex = 46;
            //
            // VentasTbpHistorial
            //
            this.VentasTbpHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTbpHistorial.Controls.Add(this.VentasPnlHistorialFiltros);
            this.VentasTbpHistorial.Controls.Add(this.VentasDgvHistorial);
            this.VentasTbpHistorial.Controls.Add(this.VentasPnlPaginaHistorial);
            this.VentasTbpHistorial.Location = new System.Drawing.Point(4, 32);
            this.VentasTbpHistorial.Name = "VentasTbpHistorial";
            this.VentasTbpHistorial.Size = new System.Drawing.Size(1052, 464);
            this.VentasTbpHistorial.TabIndex = 7;
            this.VentasTbpHistorial.Text = "Historial de cierres";
            //
            // VentasPnlHistorialFiltros
            //
            this.VentasPnlHistorialFiltros.BackColor = System.Drawing.Color.White;
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasLblDesdeHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasDtpDesdeHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasLblHastaHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasDtpHastaHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasLblEstadoHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasCboEstadoHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasLblResponsableHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasCboResponsableHistorial);
            this.VentasPnlHistorialFiltros.Controls.Add(this.VentasBtnConsultarHistorial);
            this.VentasPnlHistorialFiltros.Location = new System.Drawing.Point(8, 8);
            this.VentasPnlHistorialFiltros.Name = "VentasPnlHistorialFiltros";
            this.VentasPnlHistorialFiltros.Size = new System.Drawing.Size(1036, 100);
            this.VentasPnlHistorialFiltros.TabIndex = 47;
            //
            // VentasLblDesdeHistorial
            //
            this.VentasLblDesdeHistorial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDesdeHistorial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblDesdeHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDesdeHistorial.Location = new System.Drawing.Point(14, 12);
            this.VentasLblDesdeHistorial.Name = "VentasLblDesdeHistorial";
            this.VentasLblDesdeHistorial.Size = new System.Drawing.Size(146, 20);
            this.VentasLblDesdeHistorial.TabIndex = 48;
            this.VentasLblDesdeHistorial.Text = "Desde";
            //
            // VentasDtpDesdeHistorial
            //
            this.VentasDtpDesdeHistorial.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpDesdeHistorial.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpDesdeHistorial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpDesdeHistorial.Location = new System.Drawing.Point(14, 37);
            this.VentasDtpDesdeHistorial.Name = "VentasDtpDesdeHistorial";
            this.VentasDtpDesdeHistorial.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpDesdeHistorial.TabIndex = 49;
            this.VentasDtpDesdeHistorial.Value = new System.DateTime(2026, 10, 1, 0, 0, 0, 0);
            //
            // VentasLblHastaHistorial
            //
            this.VentasLblHastaHistorial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblHastaHistorial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblHastaHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblHastaHistorial.Location = new System.Drawing.Point(178, 12);
            this.VentasLblHastaHistorial.Name = "VentasLblHastaHistorial";
            this.VentasLblHastaHistorial.Size = new System.Drawing.Size(146, 20);
            this.VentasLblHastaHistorial.TabIndex = 50;
            this.VentasLblHastaHistorial.Text = "Hasta";
            //
            // VentasDtpHastaHistorial
            //
            this.VentasDtpHastaHistorial.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpHastaHistorial.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpHastaHistorial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpHastaHistorial.Location = new System.Drawing.Point(178, 37);
            this.VentasDtpHastaHistorial.Name = "VentasDtpHastaHistorial";
            this.VentasDtpHastaHistorial.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpHastaHistorial.TabIndex = 51;
            this.VentasDtpHastaHistorial.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            //
            // VentasLblEstadoHistorial
            //
            this.VentasLblEstadoHistorial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblEstadoHistorial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblEstadoHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblEstadoHistorial.Location = new System.Drawing.Point(342, 12);
            this.VentasLblEstadoHistorial.Name = "VentasLblEstadoHistorial";
            this.VentasLblEstadoHistorial.Size = new System.Drawing.Size(224, 20);
            this.VentasLblEstadoHistorial.TabIndex = 52;
            this.VentasLblEstadoHistorial.Text = "Estado";
            //
            // VentasCboEstadoHistorial
            //
            this.VentasCboEstadoHistorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboEstadoHistorial.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboEstadoHistorial.Items.AddRange(new object[] {
            "Todos",
            "Cerrado",
            "Pendiente"});
            this.VentasCboEstadoHistorial.Location = new System.Drawing.Point(342, 37);
            this.VentasCboEstadoHistorial.Name = "VentasCboEstadoHistorial";
            this.VentasCboEstadoHistorial.Size = new System.Drawing.Size(224, 33);
            this.VentasCboEstadoHistorial.TabIndex = 53;
            //
            // VentasLblResponsableHistorial
            //
            this.VentasLblResponsableHistorial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblResponsableHistorial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblResponsableHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblResponsableHistorial.Location = new System.Drawing.Point(590, 12);
            this.VentasLblResponsableHistorial.Name = "VentasLblResponsableHistorial";
            this.VentasLblResponsableHistorial.Size = new System.Drawing.Size(254, 20);
            this.VentasLblResponsableHistorial.TabIndex = 54;
            this.VentasLblResponsableHistorial.Text = "Responsable";
            //
            // VentasCboResponsableHistorial
            //
            this.VentasCboResponsableHistorial.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboResponsableHistorial.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboResponsableHistorial.Items.AddRange(new object[] {
            "Todos los responsables"});
            this.VentasCboResponsableHistorial.Location = new System.Drawing.Point(590, 37);
            this.VentasCboResponsableHistorial.Name = "VentasCboResponsableHistorial";
            this.VentasCboResponsableHistorial.Size = new System.Drawing.Size(254, 33);
            this.VentasCboResponsableHistorial.TabIndex = 55;
            //
            // VentasBtnConsultarHistorial
            //

            this.VentasBtnConsultarHistorial.Location = new System.Drawing.Point(867, 3);
            this.VentasBtnConsultarHistorial.Name = "VentasBtnConsultarHistorial";
            this.VentasBtnConsultarHistorial.AccessibleDescription = "Consultar";
            this.VentasBtnConsultarHistorial.AccessibleName = "Consultar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnConsultarHistorial.Text = string.Empty;
            this.VentasBtnConsultarHistorial.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnConsultarHistorial.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnConsultarHistorial.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalConsultarVentas;
            this.VentasBtnConsultarHistorial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnConsultarHistorial.Image = null;
            this.VentasBtnConsultarHistorial.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnConsultarHistorial.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnConsultarHistorial.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnConsultarHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnConsultarHistorial.FlatAppearance.BorderSize = 0;
            this.VentasBtnConsultarHistorial.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnConsultarHistorial.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnConsultarHistorial.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnConsultarHistorial.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnConsultarHistorial.UseVisualStyleBackColor = false;
            this.VentasBtnConsultarHistorial.UseCompatibleTextRendering = false;
            this.VentasBtnConsultarHistorial.Size = new System.Drawing.Size(58, 64);
            this.VentasBtnConsultarHistorial.TabIndex = 56;

            //
            // VentasDgvHistorial
            //
            this.VentasDgvHistorial.AllowUserToAddRows = false;
            this.VentasDgvHistorial.AllowUserToDeleteRows = false;
            this.VentasDgvHistorial.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvHistorial.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.VentasDgvHistorial.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvHistorial.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvHistorial.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.VentasDgvHistorial.ColumnHeadersHeight = 32;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VentasDgvHistorial.DefaultCellStyle = dataGridViewCellStyle12;
            this.VentasDgvHistorial.EnableHeadersVisualStyles = false;
            this.VentasDgvHistorial.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasDgvHistorial.Location = new System.Drawing.Point(8, 123);
            this.VentasDgvHistorial.MultiSelect = false;
            this.VentasDgvHistorial.Name = "VentasDgvHistorial";
            this.VentasDgvHistorial.ReadOnly = true;
            this.VentasDgvHistorial.RowHeadersVisible = false;
            this.VentasDgvHistorial.RowHeadersWidth = 51;
            this.VentasDgvHistorial.RowTemplate.Height = 27;
            this.VentasDgvHistorial.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvHistorial.Size = new System.Drawing.Size(1036, 252);
            this.VentasDgvHistorial.TabIndex = 57;
            //
            // VentasPnlPaginaHistorial
            //
            this.VentasPnlPaginaHistorial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasPnlPaginaHistorial.Controls.Add(this.VentasLblPaginaHistorial);
            this.VentasPnlPaginaHistorial.Controls.Add(this.VentasBtnPaginaHistorialAnterior);
            this.VentasPnlPaginaHistorial.Controls.Add(this.VentasBtnPaginaHistorialSiguiente);
            this.VentasPnlPaginaHistorial.Location = new System.Drawing.Point(8, 381);
            this.VentasPnlPaginaHistorial.Name = "VentasPnlPaginaHistorial";
            this.VentasPnlPaginaHistorial.Size = new System.Drawing.Size(1036, 70);
            this.VentasPnlPaginaHistorial.TabIndex = 58;
            //
            // VentasLblPaginaHistorial
            //
            this.VentasLblPaginaHistorial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPaginaHistorial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblPaginaHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPaginaHistorial.Location = new System.Drawing.Point(12, 10);
            this.VentasLblPaginaHistorial.Name = "VentasLblPaginaHistorial";
            this.VentasLblPaginaHistorial.Size = new System.Drawing.Size(530, 21);
            this.VentasLblPaginaHistorial.TabIndex = 59;
            this.VentasLblPaginaHistorial.Text = "0 registros · Página 1 de 1";
            //
            // VentasBtnPaginaHistorialAnterior
            //

            this.VentasBtnPaginaHistorialAnterior.Enabled = false;

            this.VentasBtnPaginaHistorialAnterior.Location = new System.Drawing.Point(738, 3);
            this.VentasBtnPaginaHistorialAnterior.Name = "VentasBtnPaginaHistorialAnterior";
            this.VentasBtnPaginaHistorialAnterior.AccessibleDescription = "Anterior";
            this.VentasBtnPaginaHistorialAnterior.AccessibleName = "Anterior";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnPaginaHistorialAnterior.Text = string.Empty;
            this.VentasBtnPaginaHistorialAnterior.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnPaginaHistorialAnterior.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnPaginaHistorialAnterior.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalAnteriorVentas;
            this.VentasBtnPaginaHistorialAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnPaginaHistorialAnterior.Image = null;
            this.VentasBtnPaginaHistorialAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaHistorialAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaHistorialAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnPaginaHistorialAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnPaginaHistorialAnterior.FlatAppearance.BorderSize = 0;
            this.VentasBtnPaginaHistorialAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaHistorialAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaHistorialAnterior.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnPaginaHistorialAnterior.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnPaginaHistorialAnterior.UseVisualStyleBackColor = false;
            this.VentasBtnPaginaHistorialAnterior.UseCompatibleTextRendering = false;
            this.VentasBtnPaginaHistorialAnterior.Size = new System.Drawing.Size(61, 64);
            this.VentasBtnPaginaHistorialAnterior.TabIndex = 60;

            //
            // VentasBtnPaginaHistorialSiguiente
            //

            this.VentasBtnPaginaHistorialSiguiente.Enabled = false;

            this.VentasBtnPaginaHistorialSiguiente.Location = new System.Drawing.Point(888, 3);
            this.VentasBtnPaginaHistorialSiguiente.Name = "VentasBtnPaginaHistorialSiguiente";
            this.VentasBtnPaginaHistorialSiguiente.AccessibleDescription = "Siguiente";
            this.VentasBtnPaginaHistorialSiguiente.AccessibleName = "Siguiente";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnPaginaHistorialSiguiente.Text = string.Empty;
            this.VentasBtnPaginaHistorialSiguiente.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnPaginaHistorialSiguiente.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnPaginaHistorialSiguiente.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSiguienteVentas;
            this.VentasBtnPaginaHistorialSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnPaginaHistorialSiguiente.Image = null;
            this.VentasBtnPaginaHistorialSiguiente.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaHistorialSiguiente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnPaginaHistorialSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnPaginaHistorialSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnPaginaHistorialSiguiente.FlatAppearance.BorderSize = 0;
            this.VentasBtnPaginaHistorialSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaHistorialSiguiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnPaginaHistorialSiguiente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnPaginaHistorialSiguiente.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnPaginaHistorialSiguiente.UseVisualStyleBackColor = false;
            this.VentasBtnPaginaHistorialSiguiente.UseCompatibleTextRendering = false;
            this.VentasBtnPaginaHistorialSiguiente.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnPaginaHistorialSiguiente.TabIndex = 61;

            //
            // VentasBtnCerrar
            //

            this.VentasBtnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.VentasBtnCerrar.Location = new System.Drawing.Point(1016, 593);
            this.VentasBtnCerrar.Name = "VentasBtnCerrar";
            this.VentasBtnCerrar.AccessibleDescription = "Cerrar";
            this.VentasBtnCerrar.AccessibleName = "Cerrar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCerrar.Text = string.Empty;
            this.VentasBtnCerrar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCerrar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCerrar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSalirVentas;
            this.VentasBtnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCerrar.Image = null;
            this.VentasBtnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCerrar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCerrar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCerrar.UseVisualStyleBackColor = false;
            this.VentasBtnCerrar.UseCompatibleTextRendering = false;
            this.VentasBtnCerrar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCerrar.TabIndex = 63;

            //
            // VentasBtnRevisar
            //

            this.VentasBtnRevisar.Location = new System.Drawing.Point(20, 593);
            this.VentasBtnRevisar.Name = "VentasBtnRevisar";
            this.VentasBtnRevisar.AccessibleDescription = "Revisar período";
            this.VentasBtnRevisar.AccessibleName = "Revisar período";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnRevisar.Text = string.Empty;
            this.VentasBtnRevisar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnRevisar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnRevisar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalConsultarVentas;
            this.VentasBtnRevisar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnRevisar.Image = null;
            this.VentasBtnRevisar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnRevisar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnRevisar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnRevisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnRevisar.FlatAppearance.BorderSize = 0;
            this.VentasBtnRevisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnRevisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnRevisar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnRevisar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnRevisar.UseVisualStyleBackColor = false;
            this.VentasBtnRevisar.UseCompatibleTextRendering = false;
            this.VentasBtnRevisar.Size = new System.Drawing.Size(58, 64);
            this.VentasBtnRevisar.TabIndex = 64;

            //
            // VentasBtnConfirmar
            //

            this.VentasBtnConfirmar.Enabled = false;

            this.VentasBtnConfirmar.Location = new System.Drawing.Point(197, 593);
            this.VentasBtnConfirmar.Name = "VentasBtnConfirmar";
            this.VentasBtnConfirmar.AccessibleDescription = "Confirmar cierre";
            this.VentasBtnConfirmar.AccessibleName = "Confirmar cierre";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnConfirmar.Text = string.Empty;
            this.VentasBtnConfirmar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnConfirmar.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnConfirmar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalGuardarVentas;
            this.VentasBtnConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnConfirmar.Image = null;
            this.VentasBtnConfirmar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnConfirmar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnConfirmar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnConfirmar.FlatAppearance.BorderSize = 0;
            this.VentasBtnConfirmar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnConfirmar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnConfirmar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnConfirmar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnConfirmar.UseVisualStyleBackColor = false;
            this.VentasBtnConfirmar.UseCompatibleTextRendering = false;
            this.VentasBtnConfirmar.Size = new System.Drawing.Size(63, 64);
            this.VentasBtnConfirmar.TabIndex = 65;

            //
            // VentasBtnResumen
            //

            this.VentasBtnResumen.Enabled = false;

            this.VentasBtnResumen.Location = new System.Drawing.Point(389, 593);
            this.VentasBtnResumen.Name = "VentasBtnResumen";
            this.VentasBtnResumen.AccessibleDescription = "Ver resumen";
            this.VentasBtnResumen.AccessibleName = "Ver resumen";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnResumen.Text = string.Empty;
            this.VentasBtnResumen.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnResumen.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnResumen.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalConsultarVentas;
            this.VentasBtnResumen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnResumen.Image = null;
            this.VentasBtnResumen.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnResumen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnResumen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnResumen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnResumen.FlatAppearance.BorderSize = 0;
            this.VentasBtnResumen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnResumen.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnResumen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnResumen.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnResumen.UseVisualStyleBackColor = false;
            this.VentasBtnResumen.UseCompatibleTextRendering = false;
            this.VentasBtnResumen.Size = new System.Drawing.Size(58, 64);
            this.VentasBtnResumen.TabIndex = 66;

            //
            // FrmCierreMovimientosVentas
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.CancelButton = this.VentasBtnCerrar;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.VentasPnlEncabezado);
            this.Controls.Add(this.VentasTbcCierre);
            this.Controls.Add(this.VentasBtnCerrar);
            this.Controls.Add(this.VentasBtnRevisar);
            this.Controls.Add(this.VentasBtnConfirmar);
            this.Controls.Add(this.VentasBtnResumen);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CapaVista_Ventas.Properties.Resources.IconoVentas;
            this.MaximizeBox = false;
            this.Name = "FrmCierreMovimientosVentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "7010 – Cierre de movimientos";
            this.VentasPnlEncabezado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).EndInit();
            this.VentasTbcCierre.ResumeLayout(false);
            this.VentasTbpPreparacion.ResumeLayout(false);
            this.VentasTbpPreparacion.PerformLayout();
            this.VentasPnlPeriodo.ResumeLayout(false);
            this.VentasPnlPeriodo.PerformLayout();
            this.VentasGbxRevision.ResumeLayout(false);
            this.VentasPnlDocumentos.ResumeLayout(false);
            this.VentasPnlCargos.ResumeLayout(false);
            this.VentasPnlAbonos.ResumeLayout(false);
            this.VentasPnlSaldo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvRevision)).EndInit();
            this.VentasPnlPaginaRevision.ResumeLayout(false);
            this.VentasTbpHistorial.ResumeLayout(false);
            this.VentasPnlHistorialFiltros.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvHistorial)).EndInit();
            this.VentasPnlPaginaHistorial.ResumeLayout(false);
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnPaginaRevisionAnterior, "Anterior");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnPaginaRevisionSiguiente, "Siguiente");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnConsultarHistorial, "Consultar");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnPaginaHistorialAnterior, "Anterior");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnPaginaHistorialSiguiente, "Siguiente");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCerrar, "Cerrar");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnRevisar, "Revisar período");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnConfirmar, "Confirmar cierre");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnResumen, "Ver resumen");
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel VentasPnlEncabezado;
        private System.Windows.Forms.PictureBox VentasPbMarca;
        private System.Windows.Forms.Label VentasLblTitulo;
        private System.Windows.Forms.Label VentasLblSubtitulo;
        private System.Windows.Forms.TabControl VentasTbcCierre;
        private System.Windows.Forms.TabPage VentasTbpPreparacion;
        private System.Windows.Forms.TabPage VentasTbpHistorial;
        private System.Windows.Forms.Panel VentasPnlPeriodo;
        private System.Windows.Forms.Label VentasLblDesde;
        private System.Windows.Forms.Label VentasLblObligatorioDesde;
        private System.Windows.Forms.DateTimePicker VentasDtpDesde;
        private System.Windows.Forms.Label VentasLblHasta;
        private System.Windows.Forms.Label VentasLblObligatorioHasta;
        private System.Windows.Forms.DateTimePicker VentasDtpHasta;
        private System.Windows.Forms.Label VentasLblCorte;
        private System.Windows.Forms.Label VentasLblObligatorioCorte;
        private System.Windows.Forms.DateTimePicker VentasDtpCorte;
        private System.Windows.Forms.Label VentasLblTipo;
        private System.Windows.Forms.ComboBox VentasCboTipo;
        private System.Windows.Forms.Label VentasLblResponsable;
        private System.Windows.Forms.TextBox VentasTxtResponsable;
        private System.Windows.Forms.Label VentasLblEstadoPreparacion;
        private System.Windows.Forms.GroupBox VentasGbxRevision;
        private System.Windows.Forms.CheckBox VentasChkDocumentos;
        private System.Windows.Forms.CheckBox VentasChkPagos;
        private System.Windows.Forms.Panel VentasPnlDocumentos;
        private System.Windows.Forms.Label VentasLblTituloDocumentos;
        private System.Windows.Forms.Label VentasLblDocumentos;
        private System.Windows.Forms.Panel VentasPnlCargos;
        private System.Windows.Forms.Label VentasLblTituloCargos;
        private System.Windows.Forms.Label VentasLblCargos;
        private System.Windows.Forms.Panel VentasPnlAbonos;
        private System.Windows.Forms.Label VentasLblTituloAbonos;
        private System.Windows.Forms.Label VentasLblAbonos;
        private System.Windows.Forms.Panel VentasPnlSaldo;
        private System.Windows.Forms.Label VentasLblTituloSaldo;
        private System.Windows.Forms.Label VentasLblSaldo;
        private System.Windows.Forms.DataGridView VentasDgvRevision;
        private System.Windows.Forms.Panel VentasPnlPaginaRevision;
        private System.Windows.Forms.Label VentasLblPaginaRevision;
        private System.Windows.Forms.Button VentasBtnPaginaRevisionAnterior;
        private System.Windows.Forms.Button VentasBtnPaginaRevisionSiguiente;
        private System.Windows.Forms.Label VentasLblObservaciones;
        private System.Windows.Forms.TextBox VentasTxtObservaciones;
        private System.Windows.Forms.Panel VentasPnlHistorialFiltros;
        private System.Windows.Forms.Label VentasLblDesdeHistorial;
        private System.Windows.Forms.DateTimePicker VentasDtpDesdeHistorial;
        private System.Windows.Forms.Label VentasLblHastaHistorial;
        private System.Windows.Forms.DateTimePicker VentasDtpHastaHistorial;
        private System.Windows.Forms.Label VentasLblEstadoHistorial;
        private System.Windows.Forms.ComboBox VentasCboEstadoHistorial;
        private System.Windows.Forms.Label VentasLblResponsableHistorial;
        private System.Windows.Forms.ComboBox VentasCboResponsableHistorial;
        private System.Windows.Forms.Button VentasBtnConsultarHistorial;
        private System.Windows.Forms.DataGridView VentasDgvHistorial;
        private System.Windows.Forms.Panel VentasPnlPaginaHistorial;
        private System.Windows.Forms.Label VentasLblPaginaHistorial;
        private System.Windows.Forms.Button VentasBtnPaginaHistorialAnterior;
        private System.Windows.Forms.Button VentasBtnPaginaHistorialSiguiente;
        private System.Windows.Forms.Button VentasBtnCerrar;
        private System.Windows.Forms.Button VentasBtnRevisar;
        private System.Windows.Forms.Button VentasBtnConfirmar;
        private System.Windows.Forms.Button VentasBtnResumen;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColCargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColAbono;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvRevisionColSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColCierre;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColDesde;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColHasta;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColCorte;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColResponsable;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColDocumentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvHistorialColEstado;
        private System.Windows.Forms.ToolTip VentasTtpAccionesOriginales;
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 14/10/2026
