/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Jose, Diego
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Diego Alejandro Cheng Peña | Carné: 0901-22-8091
 * Encargado: Jose Javier Torres Martínez
 * Fecha de actualización: 14/10/2026
 * Propósito: Definir los controles estándar y editables del prototipo de movimientos de clientes.
 */
namespace CapaVista_Ventas
{
    partial class FrmMovimientosClientesVentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VentasTtpAccionesOriginales = new System.Windows.Forms.ToolTip(this.components);
            this.VentasBtnConsultar = new System.Windows.Forms.Button();
            this.VentasBtnLimpiar = new System.Windows.Forms.Button();
            this.VentasBtnAnterior = new System.Windows.Forms.Button();
            this.VentasBtnSiguiente = new System.Windows.Forms.Button();
            this.VentasBtnCerrar = new System.Windows.Forms.Button();
            this.VentasPnlEncabezado = new System.Windows.Forms.Panel();
            this.VentasPbMarca = new System.Windows.Forms.PictureBox();
            this.VentasLblTitulo = new System.Windows.Forms.Label();
            this.VentasLblSubtitulo = new System.Windows.Forms.Label();
            this.VentasPnlFiltros = new System.Windows.Forms.Panel();
            this.VentasLblCliente = new System.Windows.Forms.Label();
            this.VentasLblObligatorio = new System.Windows.Forms.Label();
            this.VentasLblDesde = new System.Windows.Forms.Label();
            this.VentasLblHasta = new System.Windows.Forms.Label();
            this.VentasLblTipo = new System.Windows.Forms.Label();
            this.VentasLblEstado = new System.Windows.Forms.Label();
            this.VentasPnlCliente = new System.Windows.Forms.Panel();
            this.VentasCboCliente = new System.Windows.Forms.ComboBox();
            this.VentasPnlDesde = new System.Windows.Forms.Panel();
            this.VentasDtpDesde = new System.Windows.Forms.DateTimePicker();
            this.VentasPnlHasta = new System.Windows.Forms.Panel();
            this.VentasDtpHasta = new System.Windows.Forms.DateTimePicker();
            this.VentasCboTipo = new System.Windows.Forms.ComboBox();
            this.VentasCboEstado = new System.Windows.Forms.ComboBox();
            this.VentasLblValidacion = new System.Windows.Forms.Label();
            this.VentasPnlResumenInicial = new System.Windows.Forms.Panel();
            this.VentasPnlAcentoInicial = new System.Windows.Forms.Panel();
            this.VentasLblTituloInicial = new System.Windows.Forms.Label();
            this.VentasLblInicial = new System.Windows.Forms.Label();
            this.VentasPnlResumenCargos = new System.Windows.Forms.Panel();
            this.VentasPnlAcentoCargos = new System.Windows.Forms.Panel();
            this.VentasLblTituloCargos = new System.Windows.Forms.Label();
            this.VentasLblCargos = new System.Windows.Forms.Label();
            this.VentasPnlResumenAbonos = new System.Windows.Forms.Panel();
            this.VentasPnlAcentoAbonos = new System.Windows.Forms.Panel();
            this.VentasLblTituloAbonos = new System.Windows.Forms.Label();
            this.VentasLblAbonos = new System.Windows.Forms.Label();
            this.VentasPnlResumenSaldo = new System.Windows.Forms.Panel();
            this.VentasPnlAcentoSaldo = new System.Windows.Forms.Panel();
            this.VentasLblTituloSaldo = new System.Windows.Forms.Label();
            this.VentasLblSaldo = new System.Windows.Forms.Label();
            this.VentasTbcMovimientos = new System.Windows.Forms.TabControl();
            this.VentasTbpCuenta = new System.Windows.Forms.TabPage();
            this.VentasPnlPaginacion = new System.Windows.Forms.Panel();
            this.VentasLblPaginacion = new System.Windows.Forms.Label();
            this.VentasDgvMovimientos = new System.Windows.Forms.DataGridView();
            this.VentasTbpDetalle = new System.Windows.Forms.TabPage();
            this.VentasLblDocumentoDetalle = new System.Windows.Forms.Label();
            this.VentasLblClienteDetalle = new System.Windows.Forms.Label();
            this.VentasTxtClienteDetalle = new System.Windows.Forms.TextBox();
            this.VentasLblFechaDetalle = new System.Windows.Forms.Label();
            this.VentasTxtFechaDetalle = new System.Windows.Forms.TextBox();
            this.VentasLblTipoDetalle = new System.Windows.Forms.Label();
            this.VentasTxtTipoDetalle = new System.Windows.Forms.TextBox();
            this.VentasLblEstadoDetalle = new System.Windows.Forms.Label();
            this.VentasTxtEstadoDetalle = new System.Windows.Forms.TextBox();
            this.VentasLblDescripcionDetalle = new System.Windows.Forms.Label();
            this.VentasTxtDescripcionDetalle = new System.Windows.Forms.TextBox();
            this.VentasLblCargoDetalle = new System.Windows.Forms.Label();
            this.VentasLblAbonoDetalle = new System.Windows.Forms.Label();
            this.VentasLblSaldoDetalle = new System.Windows.Forms.Label();
            this.VentasErrValidacion = new System.Windows.Forms.ErrorProvider(this.components);
            this.VentasPnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).BeginInit();
            this.VentasPnlFiltros.SuspendLayout();
            this.VentasPnlCliente.SuspendLayout();
            this.VentasPnlDesde.SuspendLayout();
            this.VentasPnlHasta.SuspendLayout();
            this.VentasPnlResumenInicial.SuspendLayout();
            this.VentasPnlResumenCargos.SuspendLayout();
            this.VentasPnlResumenAbonos.SuspendLayout();
            this.VentasPnlResumenSaldo.SuspendLayout();
            this.VentasTbcMovimientos.SuspendLayout();
            this.VentasTbpCuenta.SuspendLayout();
            this.VentasPnlPaginacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvMovimientos)).BeginInit();
            this.VentasTbpDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasErrValidacion)).BeginInit();
            this.SuspendLayout();
            // 
            // VentasBtnConsultar
            // 
            this.VentasBtnConsultar.AccessibleDescription = "Consultar";
            this.VentasBtnConsultar.AccessibleName = "Consultar";
            this.VentasBtnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnConsultar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalConsultarVentas;
            this.VentasBtnConsultar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnConsultar.FlatAppearance.BorderSize = 0;
            this.VentasBtnConsultar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnConsultar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnConsultar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnConsultar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasBtnConsultar.Location = new System.Drawing.Point(14, 74);
            this.VentasBtnConsultar.Name = "VentasBtnConsultar";
            this.VentasBtnConsultar.Size = new System.Drawing.Size(58, 64);
            this.VentasBtnConsultar.TabIndex = 20;
            this.VentasBtnConsultar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnConsultar, "Consultar");
            this.VentasBtnConsultar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnLimpiar
            // 
            this.VentasBtnLimpiar.AccessibleDescription = "Limpiar";
            this.VentasBtnLimpiar.AccessibleName = "Limpiar";
            this.VentasBtnLimpiar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnLimpiar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalRefrescarVentas;
            this.VentasBtnLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnLimpiar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnLimpiar.FlatAppearance.BorderSize = 0;
            this.VentasBtnLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnLimpiar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasBtnLimpiar.Location = new System.Drawing.Point(98, 74);
            this.VentasBtnLimpiar.Name = "VentasBtnLimpiar";
            this.VentasBtnLimpiar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnLimpiar.TabIndex = 21;
            this.VentasBtnLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnLimpiar, "Limpiar");
            this.VentasBtnLimpiar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnAnterior
            // 
            this.VentasBtnAnterior.AccessibleDescription = "Anterior";
            this.VentasBtnAnterior.AccessibleName = "Anterior";
            this.VentasBtnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnAnterior.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalAnteriorVentas;
            this.VentasBtnAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnAnterior.Enabled = false;
            this.VentasBtnAnterior.FlatAppearance.BorderSize = 0;
            this.VentasBtnAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnAnterior.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnAnterior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.VentasBtnAnterior.Location = new System.Drawing.Point(720, 3);
            this.VentasBtnAnterior.Name = "VentasBtnAnterior";
            this.VentasBtnAnterior.Size = new System.Drawing.Size(61, 64);
            this.VentasBtnAnterior.TabIndex = 46;
            this.VentasBtnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnAnterior, "Anterior");
            this.VentasBtnAnterior.UseVisualStyleBackColor = false;
            // 
            // VentasBtnSiguiente
            // 
            this.VentasBtnSiguiente.AccessibleDescription = "Siguiente";
            this.VentasBtnSiguiente.AccessibleName = "Siguiente";
            this.VentasBtnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnSiguiente.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSiguienteVentas;
            this.VentasBtnSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnSiguiente.Enabled = false;
            this.VentasBtnSiguiente.FlatAppearance.BorderSize = 0;
            this.VentasBtnSiguiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnSiguiente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnSiguiente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(105)))), ((int)(((byte)(105)))));
            this.VentasBtnSiguiente.Location = new System.Drawing.Point(873, 3);
            this.VentasBtnSiguiente.Name = "VentasBtnSiguiente";
            this.VentasBtnSiguiente.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnSiguiente.TabIndex = 47;
            this.VentasBtnSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnSiguiente, "Siguiente");
            this.VentasBtnSiguiente.UseVisualStyleBackColor = false;
            // 
            // VentasBtnCerrar
            // 
            this.VentasBtnCerrar.AccessibleDescription = "Cerrar";
            this.VentasBtnCerrar.AccessibleName = "Cerrar";
            this.VentasBtnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnCerrar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSalirVentas;
            this.VentasBtnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.VentasBtnCerrar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasBtnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCerrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCerrar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasBtnCerrar.Location = new System.Drawing.Point(1016, 592);
            this.VentasBtnCerrar.Name = "VentasBtnCerrar";
            this.VentasBtnCerrar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCerrar.TabIndex = 63;
            this.VentasBtnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCerrar, "Cerrar");
            this.VentasBtnCerrar.UseVisualStyleBackColor = false;
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
            this.VentasLblTitulo.Location = new System.Drawing.Point(87, 13);
            this.VentasLblTitulo.Name = "VentasLblTitulo";
            this.VentasLblTitulo.Size = new System.Drawing.Size(570, 34);
            this.VentasLblTitulo.TabIndex = 2;
            this.VentasLblTitulo.Text = "Movimientos de clientes";
            // 
            // VentasLblSubtitulo
            // 
            this.VentasLblSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSubtitulo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasLblSubtitulo.Location = new System.Drawing.Point(89, 48);
            this.VentasLblSubtitulo.Name = "VentasLblSubtitulo";
            this.VentasLblSubtitulo.Size = new System.Drawing.Size(575, 22);
            this.VentasLblSubtitulo.TabIndex = 3;
            this.VentasLblSubtitulo.Text = "Consulta del estado de cuenta y detalle de documentos";
            // 
            // VentasPnlFiltros
            // 
            this.VentasPnlFiltros.BackColor = System.Drawing.Color.White;
            this.VentasPnlFiltros.Controls.Add(this.VentasLblCliente);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblObligatorio);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblDesde);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblHasta);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblTipo);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblEstado);
            this.VentasPnlFiltros.Controls.Add(this.VentasPnlCliente);
            this.VentasPnlFiltros.Controls.Add(this.VentasPnlDesde);
            this.VentasPnlFiltros.Controls.Add(this.VentasPnlHasta);
            this.VentasPnlFiltros.Controls.Add(this.VentasCboTipo);
            this.VentasPnlFiltros.Controls.Add(this.VentasCboEstado);
            this.VentasPnlFiltros.Controls.Add(this.VentasBtnConsultar);
            this.VentasPnlFiltros.Controls.Add(this.VentasBtnLimpiar);
            this.VentasPnlFiltros.Controls.Add(this.VentasLblValidacion);
            this.VentasPnlFiltros.Location = new System.Drawing.Point(20, 92);
            this.VentasPnlFiltros.Name = "VentasPnlFiltros";
            this.VentasPnlFiltros.Size = new System.Drawing.Size(1060, 156);
            this.VentasPnlFiltros.TabIndex = 5;
            // 
            // VentasLblCliente
            // 
            this.VentasLblCliente.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCliente.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCliente.Location = new System.Drawing.Point(14, 13);
            this.VentasLblCliente.Name = "VentasLblCliente";
            this.VentasLblCliente.Size = new System.Drawing.Size(88, 21);
            this.VentasLblCliente.TabIndex = 6;
            this.VentasLblCliente.Text = "Cliente";
            // 
            // VentasLblObligatorio
            // 
            this.VentasLblObligatorio.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblObligatorio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblObligatorio.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblObligatorio.Location = new System.Drawing.Point(66, 11);
            this.VentasLblObligatorio.Name = "VentasLblObligatorio";
            this.VentasLblObligatorio.Size = new System.Drawing.Size(14, 20);
            this.VentasLblObligatorio.TabIndex = 7;
            this.VentasLblObligatorio.Text = "*";
            // 
            // VentasLblDesde
            // 
            this.VentasLblDesde.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDesde.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblDesde.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDesde.Location = new System.Drawing.Point(397, 13);
            this.VentasLblDesde.Name = "VentasLblDesde";
            this.VentasLblDesde.Size = new System.Drawing.Size(130, 21);
            this.VentasLblDesde.TabIndex = 8;
            this.VentasLblDesde.Text = "Desde";
            // 
            // VentasLblHasta
            // 
            this.VentasLblHasta.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblHasta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblHasta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblHasta.Location = new System.Drawing.Point(550, 13);
            this.VentasLblHasta.Name = "VentasLblHasta";
            this.VentasLblHasta.Size = new System.Drawing.Size(130, 21);
            this.VentasLblHasta.TabIndex = 9;
            this.VentasLblHasta.Text = "Hasta";
            // 
            // VentasLblTipo
            // 
            this.VentasLblTipo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTipo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTipo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTipo.Location = new System.Drawing.Point(702, 13);
            this.VentasLblTipo.Name = "VentasLblTipo";
            this.VentasLblTipo.Size = new System.Drawing.Size(170, 21);
            this.VentasLblTipo.TabIndex = 10;
            this.VentasLblTipo.Text = "Tipo de movimiento";
            // 
            // VentasLblEstado
            // 
            this.VentasLblEstado.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblEstado.Location = new System.Drawing.Point(890, 13);
            this.VentasLblEstado.Name = "VentasLblEstado";
            this.VentasLblEstado.Size = new System.Drawing.Size(150, 21);
            this.VentasLblEstado.TabIndex = 11;
            this.VentasLblEstado.Text = "Estado";
            // 
            // VentasPnlCliente
            // 
            this.VentasPnlCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasPnlCliente.Controls.Add(this.VentasCboCliente);
            this.VentasPnlCliente.Location = new System.Drawing.Point(14, 37);
            this.VentasPnlCliente.Name = "VentasPnlCliente";
            this.VentasPnlCliente.Size = new System.Drawing.Size(364, 32);
            this.VentasPnlCliente.TabIndex = 12;
            // 
            // VentasCboCliente
            // 
            this.VentasCboCliente.AccessibleName = "Cliente obligatorio";
            this.VentasCboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCliente.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCliente.Items.AddRange(new object[] {
            "Seleccione un cliente",
            "CLI-001 · Cliente de ejemplo 001",
            "CLI-002 · Cliente de ejemplo 002"});
            this.VentasCboCliente.Location = new System.Drawing.Point(3, 3);
            this.VentasCboCliente.Name = "VentasCboCliente";
            this.VentasCboCliente.Size = new System.Drawing.Size(358, 33);
            this.VentasCboCliente.TabIndex = 13;
            // 
            // VentasPnlDesde
            // 
            this.VentasPnlDesde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasPnlDesde.Controls.Add(this.VentasDtpDesde);
            this.VentasPnlDesde.Location = new System.Drawing.Point(397, 37);
            this.VentasPnlDesde.Name = "VentasPnlDesde";
            this.VentasPnlDesde.Size = new System.Drawing.Size(136, 32);
            this.VentasPnlDesde.TabIndex = 14;
            // 
            // VentasDtpDesde
            // 
            this.VentasDtpDesde.AccessibleName = "Fecha desde";
            this.VentasDtpDesde.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpDesde.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpDesde.Location = new System.Drawing.Point(3, 3);
            this.VentasDtpDesde.Name = "VentasDtpDesde";
            this.VentasDtpDesde.Size = new System.Drawing.Size(130, 32);
            this.VentasDtpDesde.TabIndex = 15;
            this.VentasDtpDesde.Value = new System.DateTime(2026, 10, 1, 0, 0, 0, 0);
            // 
            // VentasPnlHasta
            // 
            this.VentasPnlHasta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasPnlHasta.Controls.Add(this.VentasDtpHasta);
            this.VentasPnlHasta.Location = new System.Drawing.Point(550, 37);
            this.VentasPnlHasta.Name = "VentasPnlHasta";
            this.VentasPnlHasta.Size = new System.Drawing.Size(136, 32);
            this.VentasPnlHasta.TabIndex = 16;
            // 
            // VentasDtpHasta
            // 
            this.VentasDtpHasta.AccessibleName = "Fecha hasta";
            this.VentasDtpHasta.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpHasta.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpHasta.Location = new System.Drawing.Point(3, 3);
            this.VentasDtpHasta.Name = "VentasDtpHasta";
            this.VentasDtpHasta.Size = new System.Drawing.Size(130, 32);
            this.VentasDtpHasta.TabIndex = 17;
            this.VentasDtpHasta.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            // 
            // VentasCboTipo
            // 
            this.VentasCboTipo.AccessibleName = "Tipo del movimiento";
            this.VentasCboTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboTipo.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboTipo.Items.AddRange(new object[] {
            "Todos",
            "Factura",
            "Pago",
            "Nota de crédito",
            "Nota de débito"});
            this.VentasCboTipo.Location = new System.Drawing.Point(702, 40);
            this.VentasCboTipo.Name = "VentasCboTipo";
            this.VentasCboTipo.Size = new System.Drawing.Size(174, 33);
            this.VentasCboTipo.TabIndex = 18;
            // 
            // VentasCboEstado
            // 
            this.VentasCboEstado.AccessibleName = "Estado del movimiento";
            this.VentasCboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboEstado.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboEstado.Items.AddRange(new object[] {
            "Todos",
            "Pendiente",
            "Aplicado"});
            this.VentasCboEstado.Location = new System.Drawing.Point(890, 40);
            this.VentasCboEstado.Name = "VentasCboEstado";
            this.VentasCboEstado.Size = new System.Drawing.Size(154, 33);
            this.VentasCboEstado.TabIndex = 19;
            // 
            // VentasLblValidacion
            // 
            this.VentasLblValidacion.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblValidacion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblValidacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasLblValidacion.Location = new System.Drawing.Point(330, 85);
            this.VentasLblValidacion.Name = "VentasLblValidacion";
            this.VentasLblValidacion.Size = new System.Drawing.Size(710, 37);
            this.VentasLblValidacion.TabIndex = 22;
            this.VentasLblValidacion.Text = "Selecciona un cliente y consulta sus movimientos.";
            this.VentasLblValidacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // VentasPnlResumenInicial
            // 
            this.VentasPnlResumenInicial.BackColor = System.Drawing.Color.White;
            this.VentasPnlResumenInicial.Controls.Add(this.VentasPnlAcentoInicial);
            this.VentasPnlResumenInicial.Controls.Add(this.VentasLblTituloInicial);
            this.VentasPnlResumenInicial.Controls.Add(this.VentasLblInicial);
            this.VentasPnlResumenInicial.Location = new System.Drawing.Point(20, 260);
            this.VentasPnlResumenInicial.Name = "VentasPnlResumenInicial";
            this.VentasPnlResumenInicial.Size = new System.Drawing.Size(256, 76);
            this.VentasPnlResumenInicial.TabIndex = 23;
            // 
            // VentasPnlAcentoInicial
            // 
            this.VentasPnlAcentoInicial.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasPnlAcentoInicial.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlAcentoInicial.Name = "VentasPnlAcentoInicial";
            this.VentasPnlAcentoInicial.Size = new System.Drawing.Size(4, 76);
            this.VentasPnlAcentoInicial.TabIndex = 24;
            // 
            // VentasLblTituloInicial
            // 
            this.VentasLblTituloInicial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloInicial.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloInicial.Location = new System.Drawing.Point(16, 10);
            this.VentasLblTituloInicial.Name = "VentasLblTituloInicial";
            this.VentasLblTituloInicial.Size = new System.Drawing.Size(230, 20);
            this.VentasLblTituloInicial.TabIndex = 25;
            this.VentasLblTituloInicial.Text = "Saldo inicial";
            // 
            // VentasLblInicial
            // 
            this.VentasLblInicial.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblInicial.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.VentasLblInicial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblInicial.Location = new System.Drawing.Point(16, 33);
            this.VentasLblInicial.Name = "VentasLblInicial";
            this.VentasLblInicial.Size = new System.Drawing.Size(230, 32);
            this.VentasLblInicial.TabIndex = 26;
            this.VentasLblInicial.Text = "Q 0.00";
            // 
            // VentasPnlResumenCargos
            // 
            this.VentasPnlResumenCargos.BackColor = System.Drawing.Color.White;
            this.VentasPnlResumenCargos.Controls.Add(this.VentasPnlAcentoCargos);
            this.VentasPnlResumenCargos.Controls.Add(this.VentasLblTituloCargos);
            this.VentasPnlResumenCargos.Controls.Add(this.VentasLblCargos);
            this.VentasPnlResumenCargos.Location = new System.Drawing.Point(288, 260);
            this.VentasPnlResumenCargos.Name = "VentasPnlResumenCargos";
            this.VentasPnlResumenCargos.Size = new System.Drawing.Size(256, 76);
            this.VentasPnlResumenCargos.TabIndex = 27;
            // 
            // VentasPnlAcentoCargos
            // 
            this.VentasPnlAcentoCargos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasPnlAcentoCargos.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlAcentoCargos.Name = "VentasPnlAcentoCargos";
            this.VentasPnlAcentoCargos.Size = new System.Drawing.Size(4, 76);
            this.VentasPnlAcentoCargos.TabIndex = 28;
            // 
            // VentasLblTituloCargos
            // 
            this.VentasLblTituloCargos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloCargos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloCargos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloCargos.Location = new System.Drawing.Point(16, 10);
            this.VentasLblTituloCargos.Name = "VentasLblTituloCargos";
            this.VentasLblTituloCargos.Size = new System.Drawing.Size(230, 20);
            this.VentasLblTituloCargos.TabIndex = 29;
            this.VentasLblTituloCargos.Text = "Cargos del período";
            // 
            // VentasLblCargos
            // 
            this.VentasLblCargos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCargos.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.VentasLblCargos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCargos.Location = new System.Drawing.Point(16, 33);
            this.VentasLblCargos.Name = "VentasLblCargos";
            this.VentasLblCargos.Size = new System.Drawing.Size(230, 32);
            this.VentasLblCargos.TabIndex = 30;
            this.VentasLblCargos.Text = "Q 0.00";
            // 
            // VentasPnlResumenAbonos
            // 
            this.VentasPnlResumenAbonos.BackColor = System.Drawing.Color.White;
            this.VentasPnlResumenAbonos.Controls.Add(this.VentasPnlAcentoAbonos);
            this.VentasPnlResumenAbonos.Controls.Add(this.VentasLblTituloAbonos);
            this.VentasPnlResumenAbonos.Controls.Add(this.VentasLblAbonos);
            this.VentasPnlResumenAbonos.Location = new System.Drawing.Point(556, 260);
            this.VentasPnlResumenAbonos.Name = "VentasPnlResumenAbonos";
            this.VentasPnlResumenAbonos.Size = new System.Drawing.Size(256, 76);
            this.VentasPnlResumenAbonos.TabIndex = 31;
            // 
            // VentasPnlAcentoAbonos
            // 
            this.VentasPnlAcentoAbonos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasPnlAcentoAbonos.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlAcentoAbonos.Name = "VentasPnlAcentoAbonos";
            this.VentasPnlAcentoAbonos.Size = new System.Drawing.Size(4, 76);
            this.VentasPnlAcentoAbonos.TabIndex = 32;
            // 
            // VentasLblTituloAbonos
            // 
            this.VentasLblTituloAbonos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloAbonos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloAbonos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloAbonos.Location = new System.Drawing.Point(16, 10);
            this.VentasLblTituloAbonos.Name = "VentasLblTituloAbonos";
            this.VentasLblTituloAbonos.Size = new System.Drawing.Size(230, 20);
            this.VentasLblTituloAbonos.TabIndex = 33;
            this.VentasLblTituloAbonos.Text = "Abonos del período";
            // 
            // VentasLblAbonos
            // 
            this.VentasLblAbonos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblAbonos.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.VentasLblAbonos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblAbonos.Location = new System.Drawing.Point(16, 33);
            this.VentasLblAbonos.Name = "VentasLblAbonos";
            this.VentasLblAbonos.Size = new System.Drawing.Size(230, 32);
            this.VentasLblAbonos.TabIndex = 34;
            this.VentasLblAbonos.Text = "Q 0.00";
            // 
            // VentasPnlResumenSaldo
            // 
            this.VentasPnlResumenSaldo.BackColor = System.Drawing.Color.White;
            this.VentasPnlResumenSaldo.Controls.Add(this.VentasPnlAcentoSaldo);
            this.VentasPnlResumenSaldo.Controls.Add(this.VentasLblTituloSaldo);
            this.VentasPnlResumenSaldo.Controls.Add(this.VentasLblSaldo);
            this.VentasPnlResumenSaldo.Location = new System.Drawing.Point(824, 260);
            this.VentasPnlResumenSaldo.Name = "VentasPnlResumenSaldo";
            this.VentasPnlResumenSaldo.Size = new System.Drawing.Size(256, 76);
            this.VentasPnlResumenSaldo.TabIndex = 35;
            // 
            // VentasPnlAcentoSaldo
            // 
            this.VentasPnlAcentoSaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(74)))));
            this.VentasPnlAcentoSaldo.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlAcentoSaldo.Name = "VentasPnlAcentoSaldo";
            this.VentasPnlAcentoSaldo.Size = new System.Drawing.Size(4, 76);
            this.VentasPnlAcentoSaldo.TabIndex = 36;
            // 
            // VentasLblTituloSaldo
            // 
            this.VentasLblTituloSaldo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTituloSaldo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTituloSaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTituloSaldo.Location = new System.Drawing.Point(16, 10);
            this.VentasLblTituloSaldo.Name = "VentasLblTituloSaldo";
            this.VentasLblTituloSaldo.Size = new System.Drawing.Size(230, 20);
            this.VentasLblTituloSaldo.TabIndex = 37;
            this.VentasLblTituloSaldo.Text = "Saldo final";
            // 
            // VentasLblSaldo
            // 
            this.VentasLblSaldo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSaldo.Font = new System.Drawing.Font("Segoe UI", 17F, System.Drawing.FontStyle.Bold);
            this.VentasLblSaldo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblSaldo.Location = new System.Drawing.Point(16, 33);
            this.VentasLblSaldo.Name = "VentasLblSaldo";
            this.VentasLblSaldo.Size = new System.Drawing.Size(230, 32);
            this.VentasLblSaldo.TabIndex = 38;
            this.VentasLblSaldo.Text = "Q 0.00";
            // 
            // VentasTbcMovimientos
            // 
            this.VentasTbcMovimientos.Controls.Add(this.VentasTbpCuenta);
            this.VentasTbcMovimientos.Controls.Add(this.VentasTbpDetalle);
            this.VentasTbcMovimientos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTbcMovimientos.Location = new System.Drawing.Point(20, 348);
            this.VentasTbcMovimientos.Name = "VentasTbcMovimientos";
            this.VentasTbcMovimientos.SelectedIndex = 0;
            this.VentasTbcMovimientos.Size = new System.Drawing.Size(1060, 232);
            this.VentasTbcMovimientos.TabIndex = 39;
            // 
            // VentasTbpCuenta
            // 
            this.VentasTbpCuenta.BackColor = System.Drawing.Color.White;
            this.VentasTbpCuenta.Controls.Add(this.VentasPnlPaginacion);
            this.VentasTbpCuenta.Controls.Add(this.VentasDgvMovimientos);
            this.VentasTbpCuenta.Location = new System.Drawing.Point(4, 32);
            this.VentasTbpCuenta.Name = "VentasTbpCuenta";
            this.VentasTbpCuenta.Padding = new System.Windows.Forms.Padding(8);
            this.VentasTbpCuenta.Size = new System.Drawing.Size(1052, 196);
            this.VentasTbpCuenta.TabIndex = 40;
            this.VentasTbpCuenta.Text = "Estado de cuenta";
            // 
            // VentasPnlPaginacion
            // 
            this.VentasPnlPaginacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasPnlPaginacion.Controls.Add(this.VentasLblPaginacion);
            this.VentasPnlPaginacion.Controls.Add(this.VentasBtnAnterior);
            this.VentasPnlPaginacion.Controls.Add(this.VentasBtnSiguiente);
            this.VentasPnlPaginacion.Location = new System.Drawing.Point(8, 124);
            this.VentasPnlPaginacion.Name = "VentasPnlPaginacion";
            this.VentasPnlPaginacion.Size = new System.Drawing.Size(1036, 70);
            this.VentasPnlPaginacion.TabIndex = 43;
            // 
            // VentasLblPaginacion
            // 
            this.VentasLblPaginacion.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPaginacion.Font = new System.Drawing.Font("Tahoma", 9F);
            this.VentasLblPaginacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPaginacion.Location = new System.Drawing.Point(12, 12);
            this.VentasLblPaginacion.Name = "VentasLblPaginacion";
            this.VentasLblPaginacion.Size = new System.Drawing.Size(548, 22);
            this.VentasLblPaginacion.TabIndex = 44;
            this.VentasLblPaginacion.Text = "0 registros · Página 1 de 1";
            // 
            // VentasDgvMovimientos
            // 
            this.VentasDgvMovimientos.AllowUserToAddRows = false;
            this.VentasDgvMovimientos.AllowUserToDeleteRows = false;
            this.VentasDgvMovimientos.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvMovimientos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.VentasDgvMovimientos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvMovimientos.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvMovimientos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvMovimientos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.VentasDgvMovimientos.ColumnHeadersHeight = 32;
            this.VentasDgvMovimientos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VentasDgvMovimientos.DefaultCellStyle = dataGridViewCellStyle6;
            this.VentasDgvMovimientos.EnableHeadersVisualStyles = false;
            this.VentasDgvMovimientos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasDgvMovimientos.Location = new System.Drawing.Point(8, 8);
            this.VentasDgvMovimientos.MultiSelect = false;
            this.VentasDgvMovimientos.Name = "VentasDgvMovimientos";
            this.VentasDgvMovimientos.ReadOnly = true;
            this.VentasDgvMovimientos.RowHeadersVisible = false;
            this.VentasDgvMovimientos.RowHeadersWidth = 51;
            this.VentasDgvMovimientos.RowTemplate.Height = 27;
            this.VentasDgvMovimientos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvMovimientos.Size = new System.Drawing.Size(1036, 110);
            this.VentasDgvMovimientos.TabIndex = 42;
            // 
            // VentasTbpDetalle
            // 
            this.VentasTbpDetalle.BackColor = System.Drawing.Color.White;
            this.VentasTbpDetalle.Controls.Add(this.VentasLblDocumentoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblClienteDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasTxtClienteDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblFechaDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasTxtFechaDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblTipoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasTxtTipoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblEstadoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasTxtEstadoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblDescripcionDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasTxtDescripcionDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblCargoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblAbonoDetalle);
            this.VentasTbpDetalle.Controls.Add(this.VentasLblSaldoDetalle);
            this.VentasTbpDetalle.Location = new System.Drawing.Point(4, 32);
            this.VentasTbpDetalle.Name = "VentasTbpDetalle";
            this.VentasTbpDetalle.Padding = new System.Windows.Forms.Padding(8);
            this.VentasTbpDetalle.Size = new System.Drawing.Size(1052, 196);
            this.VentasTbpDetalle.TabIndex = 41;
            this.VentasTbpDetalle.Text = "Detalle del movimiento";
            // 
            // VentasLblDocumentoDetalle
            // 
            this.VentasLblDocumentoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDocumentoDetalle.Font = new System.Drawing.Font("Lucida Fax", 15F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.VentasLblDocumentoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDocumentoDetalle.Location = new System.Drawing.Point(20, 14);
            this.VentasLblDocumentoDetalle.Name = "VentasLblDocumentoDetalle";
            this.VentasLblDocumentoDetalle.Size = new System.Drawing.Size(990, 32);
            this.VentasLblDocumentoDetalle.TabIndex = 48;
            this.VentasLblDocumentoDetalle.Text = "Detalle del documento";
            // 
            // VentasLblClienteDetalle
            // 
            this.VentasLblClienteDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblClienteDetalle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblClienteDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblClienteDetalle.Location = new System.Drawing.Point(20, 59);
            this.VentasLblClienteDetalle.Name = "VentasLblClienteDetalle";
            this.VentasLblClienteDetalle.Size = new System.Drawing.Size(365, 20);
            this.VentasLblClienteDetalle.TabIndex = 49;
            this.VentasLblClienteDetalle.Text = "Cliente";
            // 
            // VentasTxtClienteDetalle
            // 
            this.VentasTxtClienteDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtClienteDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VentasTxtClienteDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtClienteDetalle.Location = new System.Drawing.Point(20, 82);
            this.VentasTxtClienteDetalle.Name = "VentasTxtClienteDetalle";
            this.VentasTxtClienteDetalle.ReadOnly = true;
            this.VentasTxtClienteDetalle.Size = new System.Drawing.Size(365, 32);
            this.VentasTxtClienteDetalle.TabIndex = 50;
            this.VentasTxtClienteDetalle.TabStop = false;
            // 
            // VentasLblFechaDetalle
            // 
            this.VentasLblFechaDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblFechaDetalle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblFechaDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblFechaDetalle.Location = new System.Drawing.Point(408, 59);
            this.VentasLblFechaDetalle.Name = "VentasLblFechaDetalle";
            this.VentasLblFechaDetalle.Size = new System.Drawing.Size(170, 20);
            this.VentasLblFechaDetalle.TabIndex = 51;
            this.VentasLblFechaDetalle.Text = "Fecha";
            // 
            // VentasTxtFechaDetalle
            // 
            this.VentasTxtFechaDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtFechaDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VentasTxtFechaDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtFechaDetalle.Location = new System.Drawing.Point(408, 82);
            this.VentasTxtFechaDetalle.Name = "VentasTxtFechaDetalle";
            this.VentasTxtFechaDetalle.ReadOnly = true;
            this.VentasTxtFechaDetalle.Size = new System.Drawing.Size(170, 32);
            this.VentasTxtFechaDetalle.TabIndex = 52;
            this.VentasTxtFechaDetalle.TabStop = false;
            // 
            // VentasLblTipoDetalle
            // 
            this.VentasLblTipoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTipoDetalle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTipoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTipoDetalle.Location = new System.Drawing.Point(602, 59);
            this.VentasLblTipoDetalle.Name = "VentasLblTipoDetalle";
            this.VentasLblTipoDetalle.Size = new System.Drawing.Size(218, 20);
            this.VentasLblTipoDetalle.TabIndex = 53;
            this.VentasLblTipoDetalle.Text = "Tipo";
            // 
            // VentasTxtTipoDetalle
            // 
            this.VentasTxtTipoDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtTipoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VentasTxtTipoDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtTipoDetalle.Location = new System.Drawing.Point(602, 82);
            this.VentasTxtTipoDetalle.Name = "VentasTxtTipoDetalle";
            this.VentasTxtTipoDetalle.ReadOnly = true;
            this.VentasTxtTipoDetalle.Size = new System.Drawing.Size(218, 32);
            this.VentasTxtTipoDetalle.TabIndex = 54;
            this.VentasTxtTipoDetalle.TabStop = false;
            // 
            // VentasLblEstadoDetalle
            // 
            this.VentasLblEstadoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblEstadoDetalle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblEstadoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblEstadoDetalle.Location = new System.Drawing.Point(842, 59);
            this.VentasLblEstadoDetalle.Name = "VentasLblEstadoDetalle";
            this.VentasLblEstadoDetalle.Size = new System.Drawing.Size(182, 20);
            this.VentasLblEstadoDetalle.TabIndex = 55;
            this.VentasLblEstadoDetalle.Text = "Estado";
            // 
            // VentasTxtEstadoDetalle
            // 
            this.VentasTxtEstadoDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtEstadoDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VentasTxtEstadoDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtEstadoDetalle.Location = new System.Drawing.Point(842, 82);
            this.VentasTxtEstadoDetalle.Name = "VentasTxtEstadoDetalle";
            this.VentasTxtEstadoDetalle.ReadOnly = true;
            this.VentasTxtEstadoDetalle.Size = new System.Drawing.Size(182, 32);
            this.VentasTxtEstadoDetalle.TabIndex = 56;
            this.VentasTxtEstadoDetalle.TabStop = false;
            // 
            // VentasLblDescripcionDetalle
            // 
            this.VentasLblDescripcionDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblDescripcionDetalle.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblDescripcionDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblDescripcionDetalle.Location = new System.Drawing.Point(20, 120);
            this.VentasLblDescripcionDetalle.Name = "VentasLblDescripcionDetalle";
            this.VentasLblDescripcionDetalle.Size = new System.Drawing.Size(1000, 20);
            this.VentasLblDescripcionDetalle.TabIndex = 57;
            this.VentasLblDescripcionDetalle.Text = "Descripción";
            // 
            // VentasTxtDescripcionDetalle
            // 
            this.VentasTxtDescripcionDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtDescripcionDetalle.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtDescripcionDetalle.Location = new System.Drawing.Point(20, 143);
            this.VentasTxtDescripcionDetalle.Name = "VentasTxtDescripcionDetalle";
            this.VentasTxtDescripcionDetalle.ReadOnly = true;
            this.VentasTxtDescripcionDetalle.Size = new System.Drawing.Size(1004, 32);
            this.VentasTxtDescripcionDetalle.TabIndex = 58;
            this.VentasTxtDescripcionDetalle.TabStop = false;
            // 
            // VentasLblCargoDetalle
            // 
            this.VentasLblCargoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCargoDetalle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.VentasLblCargoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCargoDetalle.Location = new System.Drawing.Point(20, 176);
            this.VentasLblCargoDetalle.Name = "VentasLblCargoDetalle";
            this.VentasLblCargoDetalle.Size = new System.Drawing.Size(300, 20);
            this.VentasLblCargoDetalle.TabIndex = 59;
            this.VentasLblCargoDetalle.Text = "Cargo: —";
            // 
            // VentasLblAbonoDetalle
            // 
            this.VentasLblAbonoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblAbonoDetalle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.VentasLblAbonoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblAbonoDetalle.Location = new System.Drawing.Point(370, 176);
            this.VentasLblAbonoDetalle.Name = "VentasLblAbonoDetalle";
            this.VentasLblAbonoDetalle.Size = new System.Drawing.Size(300, 20);
            this.VentasLblAbonoDetalle.TabIndex = 60;
            this.VentasLblAbonoDetalle.Text = "Abono: —";
            // 
            // VentasLblSaldoDetalle
            // 
            this.VentasLblSaldoDetalle.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSaldoDetalle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.VentasLblSaldoDetalle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblSaldoDetalle.Location = new System.Drawing.Point(720, 176);
            this.VentasLblSaldoDetalle.Name = "VentasLblSaldoDetalle";
            this.VentasLblSaldoDetalle.Size = new System.Drawing.Size(300, 20);
            this.VentasLblSaldoDetalle.TabIndex = 61;
            this.VentasLblSaldoDetalle.Text = "Saldo: —";
            // 
            // VentasErrValidacion
            // 
            this.VentasErrValidacion.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.VentasErrValidacion.ContainerControl = this;
            // 
            // FrmMovimientosClientesVentas
            // 
            this.AcceptButton = this.VentasBtnConsultar;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.CancelButton = this.VentasBtnCerrar;
            this.ClientSize = new System.Drawing.Size(1098, 660);
            this.Controls.Add(this.VentasPnlEncabezado);
            this.Controls.Add(this.VentasPnlFiltros);
            this.Controls.Add(this.VentasPnlResumenInicial);
            this.Controls.Add(this.VentasPnlResumenCargos);
            this.Controls.Add(this.VentasPnlResumenAbonos);
            this.Controls.Add(this.VentasPnlResumenSaldo);
            this.Controls.Add(this.VentasTbcMovimientos);
            this.Controls.Add(this.VentasBtnCerrar);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CapaVista_Ventas.Properties.Resources.IconoVentas;
            this.MaximizeBox = false;
            this.Name = "FrmMovimientosClientesVentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "7003 – Movimientos de clientes";
            this.VentasPnlEncabezado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).EndInit();
            this.VentasPnlFiltros.ResumeLayout(false);
            this.VentasPnlCliente.ResumeLayout(false);
            this.VentasPnlDesde.ResumeLayout(false);
            this.VentasPnlHasta.ResumeLayout(false);
            this.VentasPnlResumenInicial.ResumeLayout(false);
            this.VentasPnlResumenCargos.ResumeLayout(false);
            this.VentasPnlResumenAbonos.ResumeLayout(false);
            this.VentasPnlResumenSaldo.ResumeLayout(false);
            this.VentasTbcMovimientos.ResumeLayout(false);
            this.VentasTbpCuenta.ResumeLayout(false);
            this.VentasPnlPaginacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvMovimientos)).EndInit();
            this.VentasTbpDetalle.ResumeLayout(false);
            this.VentasTbpDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasErrValidacion)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel VentasPnlEncabezado;
        private System.Windows.Forms.PictureBox VentasPbMarca;
        private System.Windows.Forms.Label VentasLblTitulo;
        private System.Windows.Forms.Label VentasLblSubtitulo;
        private System.Windows.Forms.Panel VentasPnlFiltros;
        private System.Windows.Forms.Label VentasLblCliente;
        private System.Windows.Forms.Label VentasLblObligatorio;
        private System.Windows.Forms.Label VentasLblDesde;
        private System.Windows.Forms.Label VentasLblHasta;
        private System.Windows.Forms.Label VentasLblTipo;
        private System.Windows.Forms.Label VentasLblEstado;
        private System.Windows.Forms.Panel VentasPnlCliente;
        private System.Windows.Forms.ComboBox VentasCboCliente;
        private System.Windows.Forms.Panel VentasPnlDesde;
        private System.Windows.Forms.DateTimePicker VentasDtpDesde;
        private System.Windows.Forms.Panel VentasPnlHasta;
        private System.Windows.Forms.DateTimePicker VentasDtpHasta;
        private System.Windows.Forms.ComboBox VentasCboTipo;
        private System.Windows.Forms.ComboBox VentasCboEstado;
        private System.Windows.Forms.Button VentasBtnConsultar;
        private System.Windows.Forms.Button VentasBtnLimpiar;
        private System.Windows.Forms.Label VentasLblValidacion;
        private System.Windows.Forms.Panel VentasPnlResumenInicial;
        private System.Windows.Forms.Panel VentasPnlAcentoInicial;
        private System.Windows.Forms.Label VentasLblTituloInicial;
        private System.Windows.Forms.Label VentasLblInicial;
        private System.Windows.Forms.Panel VentasPnlResumenCargos;
        private System.Windows.Forms.Panel VentasPnlAcentoCargos;
        private System.Windows.Forms.Label VentasLblTituloCargos;
        private System.Windows.Forms.Label VentasLblCargos;
        private System.Windows.Forms.Panel VentasPnlResumenAbonos;
        private System.Windows.Forms.Panel VentasPnlAcentoAbonos;
        private System.Windows.Forms.Label VentasLblTituloAbonos;
        private System.Windows.Forms.Label VentasLblAbonos;
        private System.Windows.Forms.Panel VentasPnlResumenSaldo;
        private System.Windows.Forms.Panel VentasPnlAcentoSaldo;
        private System.Windows.Forms.Label VentasLblTituloSaldo;
        private System.Windows.Forms.Label VentasLblSaldo;
        private System.Windows.Forms.TabControl VentasTbcMovimientos;
        private System.Windows.Forms.TabPage VentasTbpCuenta;
        private System.Windows.Forms.TabPage VentasTbpDetalle;
        private System.Windows.Forms.DataGridView VentasDgvMovimientos;
        private System.Windows.Forms.Panel VentasPnlPaginacion;
        private System.Windows.Forms.Label VentasLblPaginacion;
        private System.Windows.Forms.Button VentasBtnAnterior;
        private System.Windows.Forms.Button VentasBtnSiguiente;
        private System.Windows.Forms.Label VentasLblDocumentoDetalle;
        private System.Windows.Forms.Label VentasLblClienteDetalle;
        private System.Windows.Forms.TextBox VentasTxtClienteDetalle;
        private System.Windows.Forms.Label VentasLblFechaDetalle;
        private System.Windows.Forms.TextBox VentasTxtFechaDetalle;
        private System.Windows.Forms.Label VentasLblTipoDetalle;
        private System.Windows.Forms.TextBox VentasTxtTipoDetalle;
        private System.Windows.Forms.Label VentasLblEstadoDetalle;
        private System.Windows.Forms.TextBox VentasTxtEstadoDetalle;
        private System.Windows.Forms.Label VentasLblDescripcionDetalle;
        private System.Windows.Forms.TextBox VentasTxtDescripcionDetalle;
        private System.Windows.Forms.Label VentasLblCargoDetalle;
        private System.Windows.Forms.Label VentasLblAbonoDetalle;
        private System.Windows.Forms.Label VentasLblSaldoDetalle;
        private System.Windows.Forms.Button VentasBtnCerrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColDocumento;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCargo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColAbono;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColSaldo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColEstado;
        private System.Windows.Forms.ErrorProvider VentasErrValidacion;
        private System.Windows.Forms.ToolTip VentasTtpAccionesOriginales;
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 14/10/2026
