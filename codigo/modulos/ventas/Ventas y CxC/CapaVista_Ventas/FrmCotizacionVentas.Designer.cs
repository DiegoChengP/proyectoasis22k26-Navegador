/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Matthew, Jose, Natali
 * Matthew Andreé Juárez Xoy | Carné: 0901-23-4250
 * Jose Javier Torres Martínez | Carné: 0901-23-1091
 * Natali Sofía Montenegro Portillo | Carné: 0901-23-10017
 * Encargado: Matthew Andreé Juárez Xoy
 * Fecha de actualización: 14/10/2026
 * Propósito: Prototipo visual estandarizado de cotizaciones, sin lógica de negocio.
 */
namespace CapaVista_Ventas
{
    partial class FrmCotizacionVentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VentasPnlCotEncabezado = new System.Windows.Forms.Panel();
            this.VentasPbCotMarca = new System.Windows.Forms.PictureBox();
            this.VentasLblCotTitulo = new System.Windows.Forms.Label();
            this.VentasLblCotSubtitulo = new System.Windows.Forms.Label();
            this.VentasPnlCotDocumento = new System.Windows.Forms.Panel();
            this.VentasLblCotNumero = new System.Windows.Forms.Label();
            this.VentasLblCotFecha = new System.Windows.Forms.Label();
            this.VentasLblCotVigencia = new System.Windows.Forms.Label();
            this.VentasLblCotVendedor = new System.Windows.Forms.Label();
            this.VentasLblCotEstado = new System.Windows.Forms.Label();
            this.VentasTxtCotNumero = new System.Windows.Forms.TextBox();
            this.VentasDtpCotFecha = new System.Windows.Forms.DateTimePicker();
            this.VentasDtpCotVigencia = new System.Windows.Forms.DateTimePicker();
            this.VentasCboCotVendedor = new System.Windows.Forms.ComboBox();
            this.VentasCboCotEstado = new System.Windows.Forms.ComboBox();
            this.VentasPnlCotCliente = new System.Windows.Forms.Panel();
            this.VentasLblCotCliente = new System.Windows.Forms.Label();
            this.VentasLblCotNit = new System.Windows.Forms.Label();
            this.VentasLblCotContacto = new System.Windows.Forms.Label();
            this.VentasLblCotMoneda = new System.Windows.Forms.Label();
            this.VentasLblCotClienteObligatorio = new System.Windows.Forms.Label();
            this.VentasCboCotCliente = new System.Windows.Forms.ComboBox();
            this.VentasTxtCotNit = new System.Windows.Forms.TextBox();
            this.VentasTxtCotContacto = new System.Windows.Forms.TextBox();
            this.VentasCboCotMoneda = new System.Windows.Forms.ComboBox();
            this.VentasLblCotDireccion = new System.Windows.Forms.Label();
            this.VentasTxtCotDireccion = new System.Windows.Forms.TextBox();
            this.VentasPnlCotProducto = new System.Windows.Forms.Panel();
            this.VentasLblCotProducto = new System.Windows.Forms.Label();
            this.VentasLblCotCantidad = new System.Windows.Forms.Label();
            this.VentasLblCotPrecio = new System.Windows.Forms.Label();
            this.VentasLblCotDescuento = new System.Windows.Forms.Label();
            this.VentasLblCotProductoObligatorio = new System.Windows.Forms.Label();
            this.VentasCboCotProducto = new System.Windows.Forms.ComboBox();
            this.VentasNudCotCantidad = new System.Windows.Forms.NumericUpDown();
            this.VentasTxtCotPrecio = new System.Windows.Forms.TextBox();
            this.VentasTxtCotDescuento = new System.Windows.Forms.TextBox();
            this.VentasBtnCotAgregar = new System.Windows.Forms.Button();
            this.VentasDgvCotProductos = new System.Windows.Forms.DataGridView();
            this.VentasLblCotRegistros = new System.Windows.Forms.Label();
            this.VentasBtnCotAnterior = new System.Windows.Forms.Button();
            this.VentasBtnCotSiguiente = new System.Windows.Forms.Button();
            this.VentasBtnCotEliminar = new System.Windows.Forms.Button();
            this.VentasBtnCotGuardar = new System.Windows.Forms.Button();
            this.VentasBtnCotLimpiar = new System.Windows.Forms.Button();
            this.VentasBtnCotPedido = new System.Windows.Forms.Button();
            this.VentasBtnCotCerrar = new System.Windows.Forms.Button();
            this.VentasPnlCotTotales = new System.Windows.Forms.Panel();
            this.VentasLblCotTituloSubtotal = new System.Windows.Forms.Label();
            this.VentasLblCotSubtotal = new System.Windows.Forms.Label();
            this.VentasLblCotTituloDescuentos = new System.Windows.Forms.Label();
            this.VentasLblCotDescuentos = new System.Windows.Forms.Label();
            this.VentasLblCotTituloIva = new System.Windows.Forms.Label();
            this.VentasLblCotIva = new System.Windows.Forms.Label();
            this.VentasLblCotTituloTotal = new System.Windows.Forms.Label();
            this.VentasLblCotTotal = new System.Windows.Forms.Label();
            this.VentasPnlCotEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbCotMarca)).BeginInit();
            this.VentasPnlCotDocumento.SuspendLayout();
            this.VentasPnlCotCliente.SuspendLayout();
            this.VentasPnlCotProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasNudCotCantidad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvCotProductos)).BeginInit();
            this.VentasPnlCotTotales.SuspendLayout();
            this.SuspendLayout();
            //
            // VentasPnlCotEncabezado
            //
            this.VentasPnlCotEncabezado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasPnlCotEncabezado.Controls.Add(this.VentasPbCotMarca);
            this.VentasPnlCotEncabezado.Controls.Add(this.VentasLblCotTitulo);
            this.VentasPnlCotEncabezado.Controls.Add(this.VentasLblCotSubtitulo);
            this.VentasPnlCotEncabezado.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlCotEncabezado.Name = "VentasPnlCotEncabezado";
            this.VentasPnlCotEncabezado.Size = new System.Drawing.Size(1100, 78);
            this.VentasPnlCotEncabezado.TabIndex = 0;
            //
            // VentasPbCotMarca
            //
            this.VentasPbCotMarca.Image = global::CapaVista_Ventas.Properties.Resources.IconoVentasImagen;
            this.VentasPbCotMarca.Location = new System.Drawing.Point(20, 13);
            this.VentasPbCotMarca.Name = "VentasPbCotMarca";
            this.VentasPbCotMarca.Size = new System.Drawing.Size(52, 52);
            this.VentasPbCotMarca.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.VentasPbCotMarca.TabIndex = 1;
            this.VentasPbCotMarca.TabStop = false;
            //
            // VentasLblCotTitulo
            //
            this.VentasLblCotTitulo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTitulo.Font = new System.Drawing.Font("Lucida Fax", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.VentasLblCotTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasLblCotTitulo.Location = new System.Drawing.Point(87, 12);
            this.VentasLblCotTitulo.Name = "VentasLblCotTitulo";
            this.VentasLblCotTitulo.Size = new System.Drawing.Size(560, 34);
            this.VentasLblCotTitulo.TabIndex = 2;
            this.VentasLblCotTitulo.Text = "Cotizaciones";
            //
            // VentasLblCotSubtitulo
            //
            this.VentasLblCotSubtitulo.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotSubtitulo.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasLblCotSubtitulo.Location = new System.Drawing.Point(89, 47);
            this.VentasLblCotSubtitulo.Name = "VentasLblCotSubtitulo";
            this.VentasLblCotSubtitulo.Size = new System.Drawing.Size(570, 23);
            this.VentasLblCotSubtitulo.TabIndex = 3;
            this.VentasLblCotSubtitulo.Text = "Propuesta comercial · Ventas y cuentas por cobrar";
            //
            // VentasPnlCotDocumento
            //
            this.VentasPnlCotDocumento.BackColor = System.Drawing.Color.White;
            this.VentasPnlCotDocumento.Controls.Add(this.VentasLblCotNumero);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasLblCotFecha);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasLblCotVigencia);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasLblCotVendedor);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasLblCotEstado);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasTxtCotNumero);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasDtpCotFecha);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasDtpCotVigencia);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasCboCotVendedor);
            this.VentasPnlCotDocumento.Controls.Add(this.VentasCboCotEstado);
            this.VentasPnlCotDocumento.Location = new System.Drawing.Point(20, 92);
            this.VentasPnlCotDocumento.Name = "VentasPnlCotDocumento";
            this.VentasPnlCotDocumento.Size = new System.Drawing.Size(1060, 88);
            this.VentasPnlCotDocumento.TabIndex = 5;
            //
            // VentasLblCotNumero
            //
            this.VentasLblCotNumero.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotNumero.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotNumero.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotNumero.Location = new System.Drawing.Point(14, 12);
            this.VentasLblCotNumero.Name = "VentasLblCotNumero";
            this.VentasLblCotNumero.Size = new System.Drawing.Size(180, 21);
            this.VentasLblCotNumero.TabIndex = 6;
            this.VentasLblCotNumero.Text = "No. de cotización";
            //
            // VentasLblCotFecha
            //
            this.VentasLblCotFecha.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotFecha.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotFecha.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotFecha.Location = new System.Drawing.Point(218, 12);
            this.VentasLblCotFecha.Name = "VentasLblCotFecha";
            this.VentasLblCotFecha.Size = new System.Drawing.Size(146, 21);
            this.VentasLblCotFecha.TabIndex = 7;
            this.VentasLblCotFecha.Text = "Fecha de emisión";
            //
            // VentasLblCotVigencia
            //
            this.VentasLblCotVigencia.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotVigencia.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotVigencia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotVigencia.Location = new System.Drawing.Point(388, 12);
            this.VentasLblCotVigencia.Name = "VentasLblCotVigencia";
            this.VentasLblCotVigencia.Size = new System.Drawing.Size(148, 21);
            this.VentasLblCotVigencia.TabIndex = 8;
            this.VentasLblCotVigencia.Text = "Vigente hasta";
            //
            // VentasLblCotVendedor
            //
            this.VentasLblCotVendedor.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotVendedor.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotVendedor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotVendedor.Location = new System.Drawing.Point(558, 12);
            this.VentasLblCotVendedor.Name = "VentasLblCotVendedor";
            this.VentasLblCotVendedor.Size = new System.Drawing.Size(276, 21);
            this.VentasLblCotVendedor.TabIndex = 9;
            this.VentasLblCotVendedor.Text = "Vendedor";
            //
            // VentasLblCotEstado
            //
            this.VentasLblCotEstado.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotEstado.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotEstado.Location = new System.Drawing.Point(856, 12);
            this.VentasLblCotEstado.Name = "VentasLblCotEstado";
            this.VentasLblCotEstado.Size = new System.Drawing.Size(186, 21);
            this.VentasLblCotEstado.TabIndex = 10;
            this.VentasLblCotEstado.Text = "Estado";
            //
            // VentasTxtCotNumero
            //
            this.VentasTxtCotNumero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasTxtCotNumero.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotNumero.Location = new System.Drawing.Point(14, 37);
            this.VentasTxtCotNumero.Name = "VentasTxtCotNumero";
            this.VentasTxtCotNumero.ReadOnly = true;
            this.VentasTxtCotNumero.Size = new System.Drawing.Size(180, 32);
            this.VentasTxtCotNumero.TabIndex = 11;
            this.VentasTxtCotNumero.Text = "Por asignar";
            //
            // VentasDtpCotFecha
            //
            this.VentasDtpCotFecha.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpCotFecha.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpCotFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpCotFecha.Location = new System.Drawing.Point(218, 37);
            this.VentasDtpCotFecha.Name = "VentasDtpCotFecha";
            this.VentasDtpCotFecha.Size = new System.Drawing.Size(146, 32);
            this.VentasDtpCotFecha.TabIndex = 12;
            this.VentasDtpCotFecha.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            //
            // VentasDtpCotVigencia
            //
            this.VentasDtpCotVigencia.CustomFormat = "dd/MM/yyyy";
            this.VentasDtpCotVigencia.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasDtpCotVigencia.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.VentasDtpCotVigencia.Location = new System.Drawing.Point(388, 37);
            this.VentasDtpCotVigencia.Name = "VentasDtpCotVigencia";
            this.VentasDtpCotVigencia.Size = new System.Drawing.Size(148, 32);
            this.VentasDtpCotVigencia.TabIndex = 13;
            this.VentasDtpCotVigencia.Value = new System.DateTime(2026, 10, 12, 0, 0, 0, 0);
            //
            // VentasCboCotVendedor
            //
            this.VentasCboCotVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCotVendedor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCotVendedor.Items.AddRange(new object[] {
            "Seleccione un vendedor"});
            this.VentasCboCotVendedor.Location = new System.Drawing.Point(558, 37);
            this.VentasCboCotVendedor.Name = "VentasCboCotVendedor";
            this.VentasCboCotVendedor.Size = new System.Drawing.Size(276, 33);
            this.VentasCboCotVendedor.TabIndex = 14;
            //
            // VentasCboCotEstado
            //
            this.VentasCboCotEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCotEstado.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCotEstado.Items.AddRange(new object[] {
            "Borrador",
            "Pendiente",
            "Aprobada",
            "Rechazada"});
            this.VentasCboCotEstado.Location = new System.Drawing.Point(856, 37);
            this.VentasCboCotEstado.Name = "VentasCboCotEstado";
            this.VentasCboCotEstado.Size = new System.Drawing.Size(186, 33);
            this.VentasCboCotEstado.TabIndex = 15;
            //
            // VentasPnlCotCliente
            //
            this.VentasPnlCotCliente.BackColor = System.Drawing.Color.White;
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotCliente);
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotNit);
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotContacto);
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotMoneda);
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotClienteObligatorio);
            this.VentasPnlCotCliente.Controls.Add(this.VentasCboCotCliente);
            this.VentasPnlCotCliente.Controls.Add(this.VentasTxtCotNit);
            this.VentasPnlCotCliente.Controls.Add(this.VentasTxtCotContacto);
            this.VentasPnlCotCliente.Controls.Add(this.VentasCboCotMoneda);
            this.VentasPnlCotCliente.Controls.Add(this.VentasLblCotDireccion);
            this.VentasPnlCotCliente.Controls.Add(this.VentasTxtCotDireccion);
            this.VentasPnlCotCliente.Location = new System.Drawing.Point(20, 194);
            this.VentasPnlCotCliente.Name = "VentasPnlCotCliente";
            this.VentasPnlCotCliente.Size = new System.Drawing.Size(1060, 111);
            this.VentasPnlCotCliente.TabIndex = 16;
            //
            // VentasLblCotCliente
            //
            this.VentasLblCotCliente.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotCliente.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotCliente.Location = new System.Drawing.Point(14, 12);
            this.VentasLblCotCliente.Name = "VentasLblCotCliente";
            this.VentasLblCotCliente.Size = new System.Drawing.Size(364, 20);
            this.VentasLblCotCliente.TabIndex = 17;
            this.VentasLblCotCliente.Text = "Cliente";
            //
            // VentasLblCotNit
            //
            this.VentasLblCotNit.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotNit.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotNit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotNit.Location = new System.Drawing.Point(400, 12);
            this.VentasLblCotNit.Name = "VentasLblCotNit";
            this.VentasLblCotNit.Size = new System.Drawing.Size(174, 20);
            this.VentasLblCotNit.TabIndex = 18;
            this.VentasLblCotNit.Text = "NIT";
            //
            // VentasLblCotContacto
            //
            this.VentasLblCotContacto.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotContacto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotContacto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotContacto.Location = new System.Drawing.Point(596, 12);
            this.VentasLblCotContacto.Name = "VentasLblCotContacto";
            this.VentasLblCotContacto.Size = new System.Drawing.Size(258, 20);
            this.VentasLblCotContacto.TabIndex = 19;
            this.VentasLblCotContacto.Text = "Contacto";
            //
            // VentasLblCotMoneda
            //
            this.VentasLblCotMoneda.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotMoneda.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotMoneda.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotMoneda.Location = new System.Drawing.Point(876, 12);
            this.VentasLblCotMoneda.Name = "VentasLblCotMoneda";
            this.VentasLblCotMoneda.Size = new System.Drawing.Size(166, 20);
            this.VentasLblCotMoneda.TabIndex = 20;
            this.VentasLblCotMoneda.Text = "Moneda";
            //
            // VentasLblCotClienteObligatorio
            //
            this.VentasLblCotClienteObligatorio.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotClienteObligatorio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotClienteObligatorio.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblCotClienteObligatorio.Location = new System.Drawing.Point(65, 10);
            this.VentasLblCotClienteObligatorio.Name = "VentasLblCotClienteObligatorio";
            this.VentasLblCotClienteObligatorio.Size = new System.Drawing.Size(15, 20);
            this.VentasLblCotClienteObligatorio.TabIndex = 21;
            this.VentasLblCotClienteObligatorio.Text = "*";
            //
            // VentasCboCotCliente
            //
            this.VentasCboCotCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCotCliente.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCotCliente.Items.AddRange(new object[] {
            "Seleccione un cliente"});
            this.VentasCboCotCliente.Location = new System.Drawing.Point(14, 37);
            this.VentasCboCotCliente.Name = "VentasCboCotCliente";
            this.VentasCboCotCliente.Size = new System.Drawing.Size(364, 33);
            this.VentasCboCotCliente.TabIndex = 22;
            //
            // VentasTxtCotNit
            //
            this.VentasTxtCotNit.BackColor = System.Drawing.Color.White;
            this.VentasTxtCotNit.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotNit.Location = new System.Drawing.Point(400, 37);
            this.VentasTxtCotNit.Name = "VentasTxtCotNit";
            this.VentasTxtCotNit.Size = new System.Drawing.Size(174, 32);
            this.VentasTxtCotNit.TabIndex = 23;
            //
            // VentasTxtCotContacto
            //
            this.VentasTxtCotContacto.BackColor = System.Drawing.Color.White;
            this.VentasTxtCotContacto.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotContacto.Location = new System.Drawing.Point(596, 37);
            this.VentasTxtCotContacto.Name = "VentasTxtCotContacto";
            this.VentasTxtCotContacto.Size = new System.Drawing.Size(258, 32);
            this.VentasTxtCotContacto.TabIndex = 24;
            //
            // VentasCboCotMoneda
            //
            this.VentasCboCotMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCotMoneda.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCotMoneda.Items.AddRange(new object[] {
            "GTQ – Quetzales"});
            this.VentasCboCotMoneda.Location = new System.Drawing.Point(876, 37);
            this.VentasCboCotMoneda.Name = "VentasCboCotMoneda";
            this.VentasCboCotMoneda.Size = new System.Drawing.Size(166, 33);
            this.VentasCboCotMoneda.TabIndex = 25;
            //
            // VentasLblCotDireccion
            //
            this.VentasLblCotDireccion.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotDireccion.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotDireccion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotDireccion.Location = new System.Drawing.Point(14, 77);
            this.VentasLblCotDireccion.Name = "VentasLblCotDireccion";
            this.VentasLblCotDireccion.Size = new System.Drawing.Size(85, 20);
            this.VentasLblCotDireccion.TabIndex = 26;
            this.VentasLblCotDireccion.Text = "Dirección";
            //
            // VentasTxtCotDireccion
            //
            this.VentasTxtCotDireccion.BackColor = System.Drawing.Color.White;
            this.VentasTxtCotDireccion.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotDireccion.Location = new System.Drawing.Point(101, 73);
            this.VentasTxtCotDireccion.Name = "VentasTxtCotDireccion";
            this.VentasTxtCotDireccion.Size = new System.Drawing.Size(941, 32);
            this.VentasTxtCotDireccion.TabIndex = 27;
            //
            // VentasPnlCotProducto
            //
            this.VentasPnlCotProducto.BackColor = System.Drawing.Color.White;
            this.VentasPnlCotProducto.Controls.Add(this.VentasLblCotProducto);
            this.VentasPnlCotProducto.Controls.Add(this.VentasLblCotCantidad);
            this.VentasPnlCotProducto.Controls.Add(this.VentasLblCotPrecio);
            this.VentasPnlCotProducto.Controls.Add(this.VentasLblCotDescuento);
            this.VentasPnlCotProducto.Controls.Add(this.VentasLblCotProductoObligatorio);
            this.VentasPnlCotProducto.Controls.Add(this.VentasCboCotProducto);
            this.VentasPnlCotProducto.Controls.Add(this.VentasNudCotCantidad);
            this.VentasPnlCotProducto.Controls.Add(this.VentasTxtCotPrecio);
            this.VentasPnlCotProducto.Controls.Add(this.VentasTxtCotDescuento);
            this.VentasPnlCotProducto.Controls.Add(this.VentasBtnCotAgregar);
            this.VentasPnlCotProducto.Location = new System.Drawing.Point(20, 317);
            this.VentasPnlCotProducto.Name = "VentasPnlCotProducto";
            this.VentasPnlCotProducto.Size = new System.Drawing.Size(1060, 85);
            this.VentasPnlCotProducto.TabIndex = 28;
            //
            // VentasLblCotProducto
            //
            this.VentasLblCotProducto.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotProducto.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotProducto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotProducto.Location = new System.Drawing.Point(14, 12);
            this.VentasLblCotProducto.Name = "VentasLblCotProducto";
            this.VentasLblCotProducto.Size = new System.Drawing.Size(366, 21);
            this.VentasLblCotProducto.TabIndex = 29;
            this.VentasLblCotProducto.Text = "Producto";
            //
            // VentasLblCotCantidad
            //
            this.VentasLblCotCantidad.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotCantidad.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotCantidad.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotCantidad.Location = new System.Drawing.Point(400, 12);
            this.VentasLblCotCantidad.Name = "VentasLblCotCantidad";
            this.VentasLblCotCantidad.Size = new System.Drawing.Size(88, 21);
            this.VentasLblCotCantidad.TabIndex = 30;
            this.VentasLblCotCantidad.Text = "Cantidad";
            //
            // VentasLblCotPrecio
            //
            this.VentasLblCotPrecio.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotPrecio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotPrecio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotPrecio.Location = new System.Drawing.Point(510, 12);
            this.VentasLblCotPrecio.Name = "VentasLblCotPrecio";
            this.VentasLblCotPrecio.Size = new System.Drawing.Size(134, 21);
            this.VentasLblCotPrecio.TabIndex = 31;
            this.VentasLblCotPrecio.Text = "Precio unitario (Q)";
            //
            // VentasLblCotDescuento
            //
            this.VentasLblCotDescuento.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotDescuento.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotDescuento.Location = new System.Drawing.Point(666, 12);
            this.VentasLblCotDescuento.Name = "VentasLblCotDescuento";
            this.VentasLblCotDescuento.Size = new System.Drawing.Size(184, 21);
            this.VentasLblCotDescuento.TabIndex = 32;
            this.VentasLblCotDescuento.Text = "Descuento por línea (Q)";
            //
            // VentasLblCotProductoObligatorio
            //
            this.VentasLblCotProductoObligatorio.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotProductoObligatorio.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotProductoObligatorio.ForeColor = System.Drawing.Color.Firebrick;
            this.VentasLblCotProductoObligatorio.Location = new System.Drawing.Point(79, 10);
            this.VentasLblCotProductoObligatorio.Name = "VentasLblCotProductoObligatorio";
            this.VentasLblCotProductoObligatorio.Size = new System.Drawing.Size(14, 20);
            this.VentasLblCotProductoObligatorio.TabIndex = 33;
            this.VentasLblCotProductoObligatorio.Text = "*";
            //
            // VentasCboCotProducto
            //
            this.VentasCboCotProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCotProducto.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboCotProducto.Items.AddRange(new object[] {
            "Seleccione un producto"});
            this.VentasCboCotProducto.Location = new System.Drawing.Point(14, 38);
            this.VentasCboCotProducto.Name = "VentasCboCotProducto";
            this.VentasCboCotProducto.Size = new System.Drawing.Size(366, 33);
            this.VentasCboCotProducto.TabIndex = 34;
            //
            // VentasNudCotCantidad
            //
            this.VentasNudCotCantidad.DecimalPlaces = 2;
            this.VentasNudCotCantidad.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasNudCotCantidad.Location = new System.Drawing.Point(400, 38);
            this.VentasNudCotCantidad.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.VentasNudCotCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.VentasNudCotCantidad.Name = "VentasNudCotCantidad";
            this.VentasNudCotCantidad.Size = new System.Drawing.Size(88, 32);
            this.VentasNudCotCantidad.TabIndex = 35;
            this.VentasNudCotCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VentasNudCotCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // VentasTxtCotPrecio
            //
            this.VentasTxtCotPrecio.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotPrecio.Location = new System.Drawing.Point(510, 38);
            this.VentasTxtCotPrecio.Name = "VentasTxtCotPrecio";
            this.VentasTxtCotPrecio.Size = new System.Drawing.Size(134, 32);
            this.VentasTxtCotPrecio.TabIndex = 36;
            this.VentasTxtCotPrecio.Text = "Q 0.00";
            this.VentasTxtCotPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // VentasTxtCotDescuento
            //
            this.VentasTxtCotDescuento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtCotDescuento.Location = new System.Drawing.Point(666, 38);
            this.VentasTxtCotDescuento.Name = "VentasTxtCotDescuento";
            this.VentasTxtCotDescuento.Size = new System.Drawing.Size(184, 32);
            this.VentasTxtCotDescuento.TabIndex = 37;
            this.VentasTxtCotDescuento.Text = "Q 0.00";
            this.VentasTxtCotDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            //
            // VentasBtnCotAgregar
            //

            this.VentasBtnCotAgregar.Location = new System.Drawing.Point(876, 5);
            this.VentasBtnCotAgregar.Name = "VentasBtnCotAgregar";
            this.VentasBtnCotAgregar.AccessibleDescription = "Agregar producto";
            this.VentasBtnCotAgregar.AccessibleName = "Agregar producto";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotAgregar.Text = string.Empty;
            this.VentasBtnCotAgregar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotAgregar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCotAgregar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalIngresarVentas;
            this.VentasBtnCotAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotAgregar.Image = null;
            this.VentasBtnCotAgregar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotAgregar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotAgregar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotAgregar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotAgregar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotAgregar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotAgregar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotAgregar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotAgregar.UseVisualStyleBackColor = false;
            this.VentasBtnCotAgregar.UseCompatibleTextRendering = false;
            this.VentasBtnCotAgregar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCotAgregar.TabIndex = 38;

            //
            // VentasDgvCotProductos
            //
            this.VentasDgvCotProductos.AllowUserToAddRows = false;
            this.VentasDgvCotProductos.AllowUserToDeleteRows = false;
            this.VentasDgvCotProductos.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvCotProductos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.VentasDgvCotProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvCotProductos.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvCotProductos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvCotProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.VentasDgvCotProductos.ColumnHeadersHeight = 32;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VentasDgvCotProductos.DefaultCellStyle = dataGridViewCellStyle6;
            this.VentasDgvCotProductos.EnableHeadersVisualStyles = false;
            this.VentasDgvCotProductos.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            this.VentasDgvCotProductos.Location = new System.Drawing.Point(20, 415);
            this.VentasDgvCotProductos.MultiSelect = false;
            this.VentasDgvCotProductos.Name = "VentasDgvCotProductos";
            this.VentasDgvCotProductos.ReadOnly = true;
            this.VentasDgvCotProductos.RowHeadersVisible = false;
            this.VentasDgvCotProductos.RowHeadersWidth = 51;
            this.VentasDgvCotProductos.RowTemplate.Height = 27;
            this.VentasDgvCotProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvCotProductos.Size = new System.Drawing.Size(1060, 93);
            this.VentasDgvCotProductos.TabIndex = 39;
            //
            // VentasLblCotRegistros
            //
            this.VentasLblCotRegistros.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotRegistros.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotRegistros.Location = new System.Drawing.Point(20, 555);
            this.VentasLblCotRegistros.Name = "VentasLblCotRegistros";
            this.VentasLblCotRegistros.Size = new System.Drawing.Size(240, 23);
            this.VentasLblCotRegistros.TabIndex = 40;
            this.VentasLblCotRegistros.Text = "0 productos · Página 1 de 1";
            //
            // VentasBtnCotAnterior
            //

            this.VentasBtnCotAnterior.Enabled = false;

            this.VentasBtnCotAnterior.Location = new System.Drawing.Point(274, 514);
            this.VentasBtnCotAnterior.Name = "VentasBtnCotAnterior";
            this.VentasBtnCotAnterior.AccessibleDescription = "Anterior";
            this.VentasBtnCotAnterior.AccessibleName = "Anterior";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotAnterior.Text = string.Empty;
            this.VentasBtnCotAnterior.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotAnterior.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnCotAnterior.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalAnteriorVentas;
            this.VentasBtnCotAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotAnterior.Image = null;
            this.VentasBtnCotAnterior.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotAnterior.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotAnterior.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotAnterior.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotAnterior.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotAnterior.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotAnterior.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotAnterior.UseVisualStyleBackColor = false;
            this.VentasBtnCotAnterior.UseCompatibleTextRendering = false;
            this.VentasBtnCotAnterior.Size = new System.Drawing.Size(61, 64);
            this.VentasBtnCotAnterior.TabIndex = 41;

            //
            // VentasBtnCotSiguiente
            //

            this.VentasBtnCotSiguiente.Enabled = false;

            this.VentasBtnCotSiguiente.Location = new System.Drawing.Point(386, 514);
            this.VentasBtnCotSiguiente.Name = "VentasBtnCotSiguiente";
            this.VentasBtnCotSiguiente.AccessibleDescription = "Siguiente";
            this.VentasBtnCotSiguiente.AccessibleName = "Siguiente";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotSiguiente.Text = string.Empty;
            this.VentasBtnCotSiguiente.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotSiguiente.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnCotSiguiente.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSiguienteVentas;
            this.VentasBtnCotSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotSiguiente.Image = null;
            this.VentasBtnCotSiguiente.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotSiguiente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotSiguiente.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotSiguiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotSiguiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotSiguiente.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotSiguiente.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotSiguiente.UseVisualStyleBackColor = false;
            this.VentasBtnCotSiguiente.UseCompatibleTextRendering = false;
            this.VentasBtnCotSiguiente.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCotSiguiente.TabIndex = 42;

            //
            // VentasBtnCotEliminar
            //

            this.VentasBtnCotEliminar.Enabled = false;

            this.VentasBtnCotEliminar.Location = new System.Drawing.Point(498, 514);
            this.VentasBtnCotEliminar.Name = "VentasBtnCotEliminar";
            this.VentasBtnCotEliminar.AccessibleDescription = "Eliminar producto";
            this.VentasBtnCotEliminar.AccessibleName = "Eliminar producto";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotEliminar.Text = string.Empty;
            this.VentasBtnCotEliminar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotEliminar.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnCotEliminar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalEliminarVentas;
            this.VentasBtnCotEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotEliminar.Image = null;
            this.VentasBtnCotEliminar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotEliminar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotEliminar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotEliminar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotEliminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotEliminar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotEliminar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotEliminar.UseVisualStyleBackColor = false;
            this.VentasBtnCotEliminar.UseCompatibleTextRendering = false;
            this.VentasBtnCotEliminar.Size = new System.Drawing.Size(55, 64);
            this.VentasBtnCotEliminar.TabIndex = 43;

            //
            // VentasBtnCotGuardar
            //

            this.VentasBtnCotGuardar.Location = new System.Drawing.Point(20, 584);
            this.VentasBtnCotGuardar.Name = "VentasBtnCotGuardar";
            this.VentasBtnCotGuardar.AccessibleDescription = "Guardar cotización";
            this.VentasBtnCotGuardar.AccessibleName = "Guardar cotización";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotGuardar.Text = string.Empty;
            this.VentasBtnCotGuardar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotGuardar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCotGuardar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalGuardarVentas;
            this.VentasBtnCotGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotGuardar.Image = null;
            this.VentasBtnCotGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotGuardar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotGuardar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotGuardar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotGuardar.UseVisualStyleBackColor = false;
            this.VentasBtnCotGuardar.UseCompatibleTextRendering = false;
            this.VentasBtnCotGuardar.Size = new System.Drawing.Size(63, 64);
            this.VentasBtnCotGuardar.TabIndex = 45;

            //
            // VentasBtnCotLimpiar
            //

            this.VentasBtnCotLimpiar.Location = new System.Drawing.Point(104, 584);
            this.VentasBtnCotLimpiar.Name = "VentasBtnCotLimpiar";
            this.VentasBtnCotLimpiar.AccessibleDescription = "Limpiar";
            this.VentasBtnCotLimpiar.AccessibleName = "Limpiar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotLimpiar.Text = string.Empty;
            this.VentasBtnCotLimpiar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotLimpiar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCotLimpiar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalRefrescarVentas;
            this.VentasBtnCotLimpiar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotLimpiar.Image = null;
            this.VentasBtnCotLimpiar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotLimpiar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotLimpiar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotLimpiar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotLimpiar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotLimpiar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotLimpiar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotLimpiar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotLimpiar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotLimpiar.UseVisualStyleBackColor = false;
            this.VentasBtnCotLimpiar.UseCompatibleTextRendering = false;
            this.VentasBtnCotLimpiar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCotLimpiar.TabIndex = 46;

            //
            // VentasBtnCotPedido
            //

            this.VentasBtnCotPedido.Enabled = false;

            this.VentasBtnCotPedido.Location = new System.Drawing.Point(188, 584);
            this.VentasBtnCotPedido.Name = "VentasBtnCotPedido";
            this.VentasBtnCotPedido.AccessibleDescription = "Convertir a pedido";
            this.VentasBtnCotPedido.AccessibleName = "Convertir a pedido";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotPedido.Text = string.Empty;
            this.VentasBtnCotPedido.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotPedido.ForeColor = System.Drawing.Color.FromArgb(105, 105, 105);
            this.VentasBtnCotPedido.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalIngresarVentas;
            this.VentasBtnCotPedido.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotPedido.Image = null;
            this.VentasBtnCotPedido.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotPedido.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotPedido.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotPedido.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotPedido.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotPedido.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotPedido.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotPedido.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotPedido.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotPedido.UseVisualStyleBackColor = false;
            this.VentasBtnCotPedido.UseCompatibleTextRendering = false;
            this.VentasBtnCotPedido.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCotPedido.TabIndex = 47;

            //
            // VentasBtnCotCerrar
            //

            this.VentasBtnCotCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            this.VentasBtnCotCerrar.Location = new System.Drawing.Point(272, 584);
            this.VentasBtnCotCerrar.Name = "VentasBtnCotCerrar";
            this.VentasBtnCotCerrar.AccessibleDescription = "Cerrar";
            this.VentasBtnCotCerrar.AccessibleName = "Cerrar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCotCerrar.Text = string.Empty;
            this.VentasBtnCotCerrar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCotCerrar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCotCerrar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalSalirVentas;
            this.VentasBtnCotCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCotCerrar.Image = null;
            this.VentasBtnCotCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotCerrar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCotCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCotCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCotCerrar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCotCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCotCerrar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCotCerrar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCotCerrar.UseVisualStyleBackColor = false;
            this.VentasBtnCotCerrar.UseCompatibleTextRendering = false;
            this.VentasBtnCotCerrar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCotCerrar.TabIndex = 48;

            //
            // VentasPnlCotTotales
            //
            this.VentasPnlCotTotales.BackColor = System.Drawing.Color.White;
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotTituloSubtotal);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotSubtotal);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotTituloDescuentos);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotDescuentos);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotTituloIva);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotIva);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotTituloTotal);
            this.VentasPnlCotTotales.Controls.Add(this.VentasLblCotTotal);
            this.VentasPnlCotTotales.Location = new System.Drawing.Point(788, 556);
            this.VentasPnlCotTotales.Name = "VentasPnlCotTotales";
            this.VentasPnlCotTotales.Size = new System.Drawing.Size(292, 92);
            this.VentasPnlCotTotales.TabIndex = 49;
            //
            // VentasLblCotTituloSubtotal
            //
            this.VentasLblCotTituloSubtotal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTituloSubtotal.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotTituloSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotTituloSubtotal.Location = new System.Drawing.Point(12, 7);
            this.VentasLblCotTituloSubtotal.Name = "VentasLblCotTituloSubtotal";
            this.VentasLblCotTituloSubtotal.Size = new System.Drawing.Size(126, 21);
            this.VentasLblCotTituloSubtotal.TabIndex = 50;
            this.VentasLblCotTituloSubtotal.Text = "Subtotal";
            //
            // VentasLblCotSubtotal
            //
            this.VentasLblCotSubtotal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotSubtotal.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotSubtotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotSubtotal.Location = new System.Drawing.Point(142, 7);
            this.VentasLblCotSubtotal.Name = "VentasLblCotSubtotal";
            this.VentasLblCotSubtotal.Size = new System.Drawing.Size(135, 23);
            this.VentasLblCotSubtotal.TabIndex = 51;
            this.VentasLblCotSubtotal.Text = "Q 0.00";
            this.VentasLblCotSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // VentasLblCotTituloDescuentos
            //
            this.VentasLblCotTituloDescuentos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTituloDescuentos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotTituloDescuentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotTituloDescuentos.Location = new System.Drawing.Point(12, 26);
            this.VentasLblCotTituloDescuentos.Name = "VentasLblCotTituloDescuentos";
            this.VentasLblCotTituloDescuentos.Size = new System.Drawing.Size(126, 21);
            this.VentasLblCotTituloDescuentos.TabIndex = 52;
            this.VentasLblCotTituloDescuentos.Text = "Descuento";
            //
            // VentasLblCotDescuentos
            //
            this.VentasLblCotDescuentos.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotDescuentos.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotDescuentos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotDescuentos.Location = new System.Drawing.Point(142, 26);
            this.VentasLblCotDescuentos.Name = "VentasLblCotDescuentos";
            this.VentasLblCotDescuentos.Size = new System.Drawing.Size(135, 23);
            this.VentasLblCotDescuentos.TabIndex = 53;
            this.VentasLblCotDescuentos.Text = "Q 0.00";
            this.VentasLblCotDescuentos.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // VentasLblCotTituloIva
            //
            this.VentasLblCotTituloIva.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTituloIva.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotTituloIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotTituloIva.Location = new System.Drawing.Point(12, 45);
            this.VentasLblCotTituloIva.Name = "VentasLblCotTituloIva";
            this.VentasLblCotTituloIva.Size = new System.Drawing.Size(126, 21);
            this.VentasLblCotTituloIva.TabIndex = 54;
            this.VentasLblCotTituloIva.Text = "IVA";
            //
            // VentasLblCotIva
            //
            this.VentasLblCotIva.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotIva.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblCotIva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotIva.Location = new System.Drawing.Point(142, 45);
            this.VentasLblCotIva.Name = "VentasLblCotIva";
            this.VentasLblCotIva.Size = new System.Drawing.Size(135, 23);
            this.VentasLblCotIva.TabIndex = 55;
            this.VentasLblCotIva.Text = "Q 0.00";
            this.VentasLblCotIva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // VentasLblCotTituloTotal
            //
            this.VentasLblCotTituloTotal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTituloTotal.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.VentasLblCotTituloTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotTituloTotal.Location = new System.Drawing.Point(12, 65);
            this.VentasLblCotTituloTotal.Name = "VentasLblCotTituloTotal";
            this.VentasLblCotTituloTotal.Size = new System.Drawing.Size(126, 21);
            this.VentasLblCotTituloTotal.TabIndex = 56;
            this.VentasLblCotTituloTotal.Text = "Total";
            //
            // VentasLblCotTotal
            //
            this.VentasLblCotTotal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblCotTotal.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.VentasLblCotTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCotTotal.Location = new System.Drawing.Point(134, 65);
            this.VentasLblCotTotal.Name = "VentasLblCotTotal";
            this.VentasLblCotTotal.Size = new System.Drawing.Size(143, 21);
            this.VentasLblCotTotal.TabIndex = 57;
            this.VentasLblCotTotal.Text = "Q 0.00";
            this.VentasLblCotTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // FrmCotizacionVentas
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.CancelButton = this.VentasBtnCotCerrar;
            this.ClientSize = new System.Drawing.Size(1100, 660);
            this.Controls.Add(this.VentasPnlCotEncabezado);
            this.Controls.Add(this.VentasPnlCotDocumento);
            this.Controls.Add(this.VentasPnlCotCliente);
            this.Controls.Add(this.VentasPnlCotProducto);
            this.Controls.Add(this.VentasDgvCotProductos);
            this.Controls.Add(this.VentasLblCotRegistros);
            this.Controls.Add(this.VentasBtnCotAnterior);
            this.Controls.Add(this.VentasBtnCotSiguiente);
            this.Controls.Add(this.VentasBtnCotEliminar);
            this.Controls.Add(this.VentasBtnCotGuardar);
            this.Controls.Add(this.VentasBtnCotLimpiar);
            this.Controls.Add(this.VentasBtnCotPedido);
            this.Controls.Add(this.VentasBtnCotCerrar);
            this.Controls.Add(this.VentasPnlCotTotales);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CapaVista_Ventas.Properties.Resources.IconoVentas;
            this.MaximizeBox = false;
            this.Name = "FrmCotizacionVentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "7011 – Cotizaciones";
            this.VentasPnlCotEncabezado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbCotMarca)).EndInit();
            this.VentasPnlCotDocumento.ResumeLayout(false);
            this.VentasPnlCotDocumento.PerformLayout();
            this.VentasPnlCotCliente.ResumeLayout(false);
            this.VentasPnlCotCliente.PerformLayout();
            this.VentasPnlCotProducto.ResumeLayout(false);
            this.VentasPnlCotProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasNudCotCantidad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvCotProductos)).EndInit();
            this.VentasPnlCotTotales.ResumeLayout(false);
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotAgregar, "Agregar producto");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotAnterior, "Anterior");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotSiguiente, "Siguiente");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotEliminar, "Eliminar producto");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotGuardar, "Guardar cotización");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotLimpiar, "Limpiar");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotPedido, "Convertir a pedido");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCotCerrar, "Cerrar");
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.Panel VentasPnlCotEncabezado;
        private System.Windows.Forms.PictureBox VentasPbCotMarca;
        private System.Windows.Forms.Label VentasLblCotTitulo;
        private System.Windows.Forms.Label VentasLblCotSubtitulo;
        private System.Windows.Forms.Panel VentasPnlCotDocumento;
        private System.Windows.Forms.Label VentasLblCotNumero;
        private System.Windows.Forms.Label VentasLblCotFecha;
        private System.Windows.Forms.Label VentasLblCotVigencia;
        private System.Windows.Forms.Label VentasLblCotVendedor;
        private System.Windows.Forms.Label VentasLblCotEstado;
        private System.Windows.Forms.TextBox VentasTxtCotNumero;
        private System.Windows.Forms.DateTimePicker VentasDtpCotFecha;
        private System.Windows.Forms.DateTimePicker VentasDtpCotVigencia;
        private System.Windows.Forms.ComboBox VentasCboCotVendedor;
        private System.Windows.Forms.ComboBox VentasCboCotEstado;
        private System.Windows.Forms.Panel VentasPnlCotCliente;
        private System.Windows.Forms.Label VentasLblCotCliente;
        private System.Windows.Forms.Label VentasLblCotNit;
        private System.Windows.Forms.Label VentasLblCotContacto;
        private System.Windows.Forms.Label VentasLblCotMoneda;
        private System.Windows.Forms.Label VentasLblCotClienteObligatorio;
        private System.Windows.Forms.ComboBox VentasCboCotCliente;
        private System.Windows.Forms.TextBox VentasTxtCotNit;
        private System.Windows.Forms.TextBox VentasTxtCotContacto;
        private System.Windows.Forms.ComboBox VentasCboCotMoneda;
        private System.Windows.Forms.Label VentasLblCotDireccion;
        private System.Windows.Forms.TextBox VentasTxtCotDireccion;
        private System.Windows.Forms.Panel VentasPnlCotProducto;
        private System.Windows.Forms.Label VentasLblCotProducto;
        private System.Windows.Forms.Label VentasLblCotCantidad;
        private System.Windows.Forms.Label VentasLblCotPrecio;
        private System.Windows.Forms.Label VentasLblCotDescuento;
        private System.Windows.Forms.Label VentasLblCotProductoObligatorio;
        private System.Windows.Forms.ComboBox VentasCboCotProducto;
        private System.Windows.Forms.NumericUpDown VentasNudCotCantidad;
        private System.Windows.Forms.TextBox VentasTxtCotPrecio;
        private System.Windows.Forms.TextBox VentasTxtCotDescuento;
        private System.Windows.Forms.Button VentasBtnCotAgregar;
        private System.Windows.Forms.DataGridView VentasDgvCotProductos;
        private System.Windows.Forms.Label VentasLblCotRegistros;
        private System.Windows.Forms.Button VentasBtnCotAnterior;
        private System.Windows.Forms.Button VentasBtnCotSiguiente;
        private System.Windows.Forms.Button VentasBtnCotEliminar;
        private System.Windows.Forms.Button VentasBtnCotGuardar;
        private System.Windows.Forms.Button VentasBtnCotLimpiar;
        private System.Windows.Forms.Button VentasBtnCotPedido;
        private System.Windows.Forms.Button VentasBtnCotCerrar;
        private System.Windows.Forms.Panel VentasPnlCotTotales;
        private System.Windows.Forms.Label VentasLblCotTituloSubtotal;
        private System.Windows.Forms.Label VentasLblCotSubtotal;
        private System.Windows.Forms.Label VentasLblCotTituloDescuentos;
        private System.Windows.Forms.Label VentasLblCotDescuentos;
        private System.Windows.Forms.Label VentasLblCotTituloIva;
        private System.Windows.Forms.Label VentasLblCotIva;
        private System.Windows.Forms.Label VentasLblCotTituloTotal;
        private System.Windows.Forms.Label VentasLblCotTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotPrecio;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasColCotImporte;
        private System.Windows.Forms.ToolTip VentasTtpAccionesOriginales;
    }
}

// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 14/10/2026
