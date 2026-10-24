/*
 * INICIO DEL CÓDIGO — Ventas y cuentas por cobrar
 * Asignación: Matthew, Diego
 * Matthew Andreé Juárez Xoy | Carné: 0901-23-4250
 * Diego Alejandro Cheng Peña | Carné: 0901-22-8091
 * Encargado: Diego Alejandro Cheng Peña
 * Fecha de actualización: 14/10/2026
 * Propósito: Conservar el diseño de Listado de precios con botones estándar de Ventas.
 */
namespace CapaVista_Ventas
{
    partial class FrmListadoPrecios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.VentasTtpAccionesOriginales = new System.Windows.Forms.ToolTip(this.components);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListadoPrecios));
            this.VentasPnlEncabezado = new System.Windows.Forms.Panel();
            this.VentasPbMarca = new System.Windows.Forms.PictureBox();
            this.VentasLblTitulo = new System.Windows.Forms.Label();
            this.VentasLblSubtitulo = new System.Windows.Forms.Label();
            this.VentasCboTipoCliente = new System.Windows.Forms.ComboBox();
            this.VentasLblTipoCliente = new System.Windows.Forms.Label();
            this.VentasLblPorcentajeDescuento = new System.Windows.Forms.Label();
            this.VentasNudPorcentajeDescuento = new System.Windows.Forms.NumericUpDown();
            this.VentasLblSimboloPorcentaje = new System.Windows.Forms.Label();
            this.VentasLblPrecioBase = new System.Windows.Forms.Label();
            this.VentasTxtPrecioBase = new System.Windows.Forms.TextBox();
            this.VentasLblPrecioFinal = new System.Windows.Forms.Label();
            this.VentasTxtPrecioFinal = new System.Windows.Forms.TextBox();
            this.VentasLblListadoClientes = new System.Windows.Forms.Label();
            this.VentasDgvListadoClientes = new System.Windows.Forms.DataGridView();
            this.VentasClmTipoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasClmPorcentaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasClmPrecioBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasClmPrecioFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasClmAcciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasBtnGuardar = new System.Windows.Forms.Button();
            this.VentasBtnCancelar = new System.Windows.Forms.Button();
            this.VentasLblMonedaPrecioFinal = new System.Windows.Forms.Label();
            this.VentasLblMonedaPrecioBase = new System.Windows.Forms.Label();
            this.VentasPnlEncabezado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasNudPorcentajeDescuento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvListadoClientes)).BeginInit();
            System.Windows.Forms.DataGridViewCellStyle VentasEstiloCabecera = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle VentasEstiloCeldas = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.VentasPnlEncabezado.Size = new System.Drawing.Size(948, 78);
            this.VentasPnlEncabezado.TabIndex = 1;
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
            this.VentasLblTitulo.Size = new System.Drawing.Size(570, 35);
            this.VentasLblTitulo.TabIndex = 2;
            this.VentasLblTitulo.Text = "Listado de precios";
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
            this.VentasLblSubtitulo.Text = "Tipos de cliente, descuentos y precios";
            //
            // VentasCboTipoCliente
            //
            this.VentasCboTipoCliente.FormattingEnabled = true;
            this.VentasCboTipoCliente.Items.AddRange(new object[] {
            "Mayorista",
            "Minorista",
            "Público",
            "Canal 1",
            "Canal 2",
            "Canal 3",
            "Otros"});
            this.VentasCboTipoCliente.Location = new System.Drawing.Point(37, 154);
            this.VentasCboTipoCliente.Name = "VentasCboTipoCliente";
            this.VentasCboTipoCliente.Size = new System.Drawing.Size(244, 28);
            this.VentasCboTipoCliente.TabIndex = 2;
            //
            // VentasLblTipoCliente
            //
            this.VentasLblTipoCliente.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblTipoCliente.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblTipoCliente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblTipoCliente.Location = new System.Drawing.Point(33, 121);
            this.VentasLblTipoCliente.Name = "VentasLblTipoCliente";
            this.VentasLblTipoCliente.Size = new System.Drawing.Size(180, 30);
            this.VentasLblTipoCliente.TabIndex = 7;
            this.VentasLblTipoCliente.Text = "Tipo de cliente";

            //
            // VentasLblPorcentajeDescuento
            //
            this.VentasLblPorcentajeDescuento.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPorcentajeDescuento.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblPorcentajeDescuento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPorcentajeDescuento.Location = new System.Drawing.Point(323, 121);
            this.VentasLblPorcentajeDescuento.Name = "VentasLblPorcentajeDescuento";
            this.VentasLblPorcentajeDescuento.Size = new System.Drawing.Size(237, 30);
            this.VentasLblPorcentajeDescuento.TabIndex = 8;
            this.VentasLblPorcentajeDescuento.Text = "Porcentaje de descuento";
            //
            // VentasNudPorcentajeDescuento
            //
            this.VentasNudPorcentajeDescuento.Location = new System.Drawing.Point(342, 154);
            this.VentasNudPorcentajeDescuento.Name = "VentasNudPorcentajeDescuento";
            this.VentasNudPorcentajeDescuento.Size = new System.Drawing.Size(126, 26);
            this.VentasNudPorcentajeDescuento.TabIndex = 9;
            //
            // VentasLblSimboloPorcentaje
            //
            this.VentasLblSimboloPorcentaje.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblSimboloPorcentaje.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblSimboloPorcentaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblSimboloPorcentaje.Location = new System.Drawing.Point(474, 152);
            this.VentasLblSimboloPorcentaje.Name = "VentasLblSimboloPorcentaje";
            this.VentasLblSimboloPorcentaje.Size = new System.Drawing.Size(34, 30);
            this.VentasLblSimboloPorcentaje.TabIndex = 10;
            this.VentasLblSimboloPorcentaje.Text = "%";

            //
            // VentasLblPrecioBase
            //
            this.VentasLblPrecioBase.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPrecioBase.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblPrecioBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPrecioBase.Location = new System.Drawing.Point(588, 120);
            this.VentasLblPrecioBase.Name = "VentasLblPrecioBase";
            this.VentasLblPrecioBase.Size = new System.Drawing.Size(114, 30);
            this.VentasLblPrecioBase.TabIndex = 11;
            this.VentasLblPrecioBase.Text = "Precio base";
            //
            // VentasTxtPrecioBase
            //
            this.VentasTxtPrecioBase.Location = new System.Drawing.Point(617, 153);
            this.VentasTxtPrecioBase.Name = "VentasTxtPrecioBase";
            this.VentasTxtPrecioBase.Size = new System.Drawing.Size(110, 26);
            this.VentasTxtPrecioBase.TabIndex = 12;
            //
            // VentasLblPrecioFinal
            //
            this.VentasLblPrecioFinal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblPrecioFinal.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblPrecioFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblPrecioFinal.Location = new System.Drawing.Point(775, 120);
            this.VentasLblPrecioFinal.Name = "VentasLblPrecioFinal";
            this.VentasLblPrecioFinal.Size = new System.Drawing.Size(114, 30);
            this.VentasLblPrecioFinal.TabIndex = 13;
            this.VentasLblPrecioFinal.Text = "Precio final";
            //
            // VentasTxtPrecioFinal
            //
            this.VentasTxtPrecioFinal.Location = new System.Drawing.Point(806, 154);
            this.VentasTxtPrecioFinal.Name = "VentasTxtPrecioFinal";
            this.VentasTxtPrecioFinal.Size = new System.Drawing.Size(110, 26);
            this.VentasTxtPrecioFinal.TabIndex = 14;
            //
            // VentasLblListadoClientes
            //
            this.VentasLblListadoClientes.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblListadoClientes.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblListadoClientes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblListadoClientes.Location = new System.Drawing.Point(305, 255);
            this.VentasLblListadoClientes.Name = "VentasLblListadoClientes";
            this.VentasLblListadoClientes.Size = new System.Drawing.Size(361, 30);
            this.VentasLblListadoClientes.TabIndex = 15;
            this.VentasLblListadoClientes.Text = "Listado de precios por tipo de cliente";
            //
            // VentasDgvListadoClientes
            //
            this.VentasDgvListadoClientes.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvListadoClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.VentasDgvListadoClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VentasClmTipoCliente,
            this.VentasClmPorcentaje,
            this.VentasClmPrecioBase,
            this.VentasClmPrecioFinal,
            this.VentasClmAcciones});
            this.VentasDgvListadoClientes.GridColor = System.Drawing.Color.FromArgb(228, 212, 234);
            this.VentasDgvListadoClientes.Location = new System.Drawing.Point(160, 291);
            this.VentasDgvListadoClientes.Name = "VentasDgvListadoClientes";
            this.VentasDgvListadoClientes.RowHeadersVisible = false;
            this.VentasDgvListadoClientes.RowTemplate.Height = 28;
            this.VentasDgvListadoClientes.Size = new System.Drawing.Size(776, 134);
            this.VentasDgvListadoClientes.TabIndex = 16;

            //
            // VentasClmTipoCliente
            //
            this.VentasClmTipoCliente.HeaderText = "Tipo de cliente";
            this.VentasClmTipoCliente.MinimumWidth = 8;
            this.VentasClmTipoCliente.Name = "VentasClmTipoCliente";
            this.VentasClmTipoCliente.Width = 150;
            //
            // VentasClmPorcentaje
            //
            this.VentasClmPorcentaje.HeaderText = "Descuento (%)";
            this.VentasClmPorcentaje.MinimumWidth = 8;
            this.VentasClmPorcentaje.Name = "VentasClmPorcentaje";
            this.VentasClmPorcentaje.Width = 150;
            //
            // VentasClmPrecioBase
            //
            this.VentasClmPrecioBase.HeaderText = "Precio base (Q)";
            this.VentasClmPrecioBase.MinimumWidth = 8;
            this.VentasClmPrecioBase.Name = "VentasClmPrecioBase";
            this.VentasClmPrecioBase.Width = 150;
            //
            // VentasClmPrecioFinal
            //
            this.VentasClmPrecioFinal.HeaderText = "Precio final (Q)";
            this.VentasClmPrecioFinal.MinimumWidth = 8;
            this.VentasClmPrecioFinal.Name = "VentasClmPrecioFinal";
            this.VentasClmPrecioFinal.Width = 150;
            //
            // VentasClmAcciones
            //
            this.VentasClmAcciones.HeaderText = "Acciones";
            this.VentasClmAcciones.MinimumWidth = 8;
            this.VentasClmAcciones.Name = "VentasClmAcciones";
            this.VentasClmAcciones.Width = 150;
            //
            // VentasBtnGuardar
            //
            this.VentasBtnGuardar.AccessibleName = "Guardar";
            this.VentasBtnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            this.VentasBtnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;

            this.VentasBtnGuardar.Location = new System.Drawing.Point(36, 265);
            this.VentasBtnGuardar.Name = "VentasBtnGuardar";
            this.VentasBtnGuardar.AccessibleDescription = "Guardar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnGuardar.Text = string.Empty;
            this.VentasBtnGuardar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnGuardar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnGuardar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalGuardarVentas;
            this.VentasBtnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnGuardar.Image = null;
            this.VentasBtnGuardar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnGuardar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnGuardar.FlatAppearance.BorderSize = 0;
            this.VentasBtnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnGuardar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnGuardar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnGuardar.UseVisualStyleBackColor = false;
            this.VentasBtnGuardar.UseCompatibleTextRendering = false;
            this.VentasBtnGuardar.Size = new System.Drawing.Size(63, 64);
            this.VentasBtnGuardar.TabIndex = 17;

            //
            // VentasBtnCancelar
            //
            this.VentasBtnCancelar.AccessibleName = "Cancelar";
            this.VentasBtnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));

            this.VentasBtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;

            this.VentasBtnCancelar.Location = new System.Drawing.Point(36, 341);
            this.VentasBtnCancelar.Name = "VentasBtnCancelar";
            this.VentasBtnCancelar.AccessibleDescription = "Cancelar";
            // Imagen original completa: solo el color vino fue cambiado a morado.
            this.VentasBtnCancelar.Text = string.Empty;
            this.VentasBtnCancelar.BackColor = System.Drawing.Color.FromArgb(123, 63, 147);
            this.VentasBtnCancelar.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.VentasBtnCancelar.BackgroundImage = global::CapaVista_Ventas.Properties.Resources.BotonOriginalCancelarVentas;
            this.VentasBtnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCancelar.Image = null;
            this.VentasBtnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasBtnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.VentasBtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCancelar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasBtnCancelar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasBtnCancelar.Padding = new System.Windows.Forms.Padding(0);
            this.VentasBtnCancelar.UseVisualStyleBackColor = false;
            this.VentasBtnCancelar.UseCompatibleTextRendering = false;
            this.VentasBtnCancelar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCancelar.TabIndex = 6;

            //
            // VentasLblMonedaPrecioFinal
            //
            this.VentasLblMonedaPrecioFinal.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblMonedaPrecioFinal.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblMonedaPrecioFinal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblMonedaPrecioFinal.Location = new System.Drawing.Point(775, 150);
            this.VentasLblMonedaPrecioFinal.Name = "VentasLblMonedaPrecioFinal";
            this.VentasLblMonedaPrecioFinal.Size = new System.Drawing.Size(34, 30);
            this.VentasLblMonedaPrecioFinal.TabIndex = 18;
            this.VentasLblMonedaPrecioFinal.Text = "Q";
            //
            // VentasLblMonedaPrecioBase
            //
            this.VentasLblMonedaPrecioBase.BackColor = System.Drawing.Color.Transparent;
            this.VentasLblMonedaPrecioBase.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasLblMonedaPrecioBase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblMonedaPrecioBase.Location = new System.Drawing.Point(577, 150);
            this.VentasLblMonedaPrecioBase.Name = "VentasLblMonedaPrecioBase";
            this.VentasLblMonedaPrecioBase.Size = new System.Drawing.Size(34, 30);
            this.VentasLblMonedaPrecioBase.TabIndex = 19;
            this.VentasLblMonedaPrecioBase.Text = "Q";
            //
            // FrmListadoPrecios
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(948, 453);
            this.Controls.Add(this.VentasLblMonedaPrecioBase);
            this.Controls.Add(this.VentasLblMonedaPrecioFinal);
            this.Controls.Add(this.VentasBtnCancelar);
            this.Controls.Add(this.VentasBtnGuardar);
            this.Controls.Add(this.VentasDgvListadoClientes);
            this.Controls.Add(this.VentasLblListadoClientes);
            this.Controls.Add(this.VentasTxtPrecioFinal);
            this.Controls.Add(this.VentasLblPrecioFinal);
            this.Controls.Add(this.VentasTxtPrecioBase);
            this.Controls.Add(this.VentasLblPrecioBase);
            this.Controls.Add(this.VentasLblSimboloPorcentaje);
            this.Controls.Add(this.VentasNudPorcentajeDescuento);
            this.Controls.Add(this.VentasLblPorcentajeDescuento);
            this.Controls.Add(this.VentasLblTipoCliente);
            this.Controls.Add(this.VentasCboTipoCliente);
            this.Controls.Add(this.VentasPnlEncabezado);
            this.VentasCboTipoCliente.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasCboTipoCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasNudPorcentajeDescuento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasNudPorcentajeDescuento.DecimalPlaces = 2;
            this.VentasNudPorcentajeDescuento.Maximum = 100M;
            this.VentasNudPorcentajeDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VentasTxtPrecioBase.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtPrecioBase.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VentasTxtPrecioFinal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.VentasTxtPrecioFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.VentasLblListadoClientes.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.VentasLblListadoClientes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.VentasDgvListadoClientes.AllowUserToAddRows = false;
            this.VentasDgvListadoClientes.AllowUserToDeleteRows = false;
            this.VentasDgvListadoClientes.ReadOnly = true;
            this.VentasDgvListadoClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvListadoClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.VentasDgvListadoClientes.EnableHeadersVisualStyles = false;
            this.VentasDgvListadoClientes.ColumnHeadersHeight = 34;
            this.VentasDgvListadoClientes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasDgvListadoClientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvListadoClientes.MultiSelect = false;
            VentasEstiloCabecera.BackColor = System.Drawing.Color.FromArgb(59, 29, 74);
            VentasEstiloCabecera.ForeColor = System.Drawing.Color.FromArgb(248, 245, 249);
            VentasEstiloCabecera.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.VentasDgvListadoClientes.ColumnHeadersDefaultCellStyle = VentasEstiloCabecera;
            VentasEstiloCeldas.ForeColor = System.Drawing.Color.FromArgb(59, 29, 74);
            VentasEstiloCeldas.SelectionBackColor = System.Drawing.Color.FromArgb(228, 212, 234);
            VentasEstiloCeldas.SelectionForeColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.VentasDgvListadoClientes.DefaultCellStyle = VentasEstiloCeldas;
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.BackColor = System.Drawing.Color.FromArgb(248, 245, 249);
            this.ForeColor = System.Drawing.Color.FromArgb(59, 29, 74);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CapaVista_Ventas.Properties.Resources.IconoVentas;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "FrmListadoPrecios";
            this.Text = "7002 – Listado de precios";
            this.VentasPnlEncabezado.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.VentasPbMarca)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasNudPorcentajeDescuento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvListadoClientes)).EndInit();
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnGuardar, "Guardar");
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCancelar, "Cancelar");
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel VentasPnlEncabezado;
        private System.Windows.Forms.PictureBox VentasPbMarca;
        private System.Windows.Forms.Label VentasLblTitulo;
        private System.Windows.Forms.Label VentasLblSubtitulo;
        private System.Windows.Forms.ComboBox VentasCboTipoCliente;
        private System.Windows.Forms.Label VentasLblTipoCliente;
        private System.Windows.Forms.Label VentasLblPorcentajeDescuento;
        private System.Windows.Forms.NumericUpDown VentasNudPorcentajeDescuento;
        private System.Windows.Forms.Label VentasLblSimboloPorcentaje;
        private System.Windows.Forms.Label VentasLblPrecioBase;
        private System.Windows.Forms.TextBox VentasTxtPrecioBase;
        private System.Windows.Forms.Label VentasLblPrecioFinal;
        private System.Windows.Forms.TextBox VentasTxtPrecioFinal;
        private System.Windows.Forms.Label VentasLblListadoClientes;
        private System.Windows.Forms.DataGridView VentasDgvListadoClientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasClmTipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasClmPorcentaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasClmPrecioBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasClmPrecioFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasClmAcciones;
        private System.Windows.Forms.Button VentasBtnGuardar;
        private System.Windows.Forms.Button VentasBtnCancelar;
        private System.Windows.Forms.Label VentasLblMonedaPrecioFinal;
        private System.Windows.Forms.Label VentasLblMonedaPrecioBase;
        private System.Windows.Forms.ToolTip VentasTtpAccionesOriginales;
    }
}
// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 14/10/2026
