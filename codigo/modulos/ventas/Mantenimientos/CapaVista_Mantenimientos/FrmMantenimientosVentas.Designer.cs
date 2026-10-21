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
 * Propósito: Diseño visual sencillo y editable en el diseñador, sin lógica de negocio.
 */

namespace CapaVista_Mantenimientos
{
    partial class FrmMantenimientosVentas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.VentasTtpAccionesOriginales = new System.Windows.Forms.ToolTip(this.components);
            this.VentasBtnIngresar = new System.Windows.Forms.Button();
            this.VentasBtnConsultar = new System.Windows.Forms.Button();
            this.VentasBtnModificar = new System.Windows.Forms.Button();
            this.VentasBtnEliminar = new System.Windows.Forms.Button();
            this.VentasBtnGuardar = new System.Windows.Forms.Button();
            this.VentasBtnCancelar = new System.Windows.Forms.Button();
            this.VentasBtnSalir = new System.Windows.Forms.Button();
            this.VentasDgvCatalogoColCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasDgvCatalogoColNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasDgvCatalogoColDescripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasDgvCatalogoColEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VentasPnlCabecera = new System.Windows.Forms.Panel();
            this.VentasLblTitulo = new System.Windows.Forms.Label();
            this.VentasLblSubtitulo = new System.Windows.Forms.Label();
            this.VentasLblCodigo = new System.Windows.Forms.Label();
            this.VentasPnlDatos = new System.Windows.Forms.Panel();
            this.VentasCboCatalogoEtiqueta = new System.Windows.Forms.Label();
            this.VentasCboCatalogo = new System.Windows.Forms.ComboBox();
            this.VentasTxtBuscarEtiqueta = new System.Windows.Forms.Label();
            this.VentasTxtBuscar = new System.Windows.Forms.TextBox();
            this.VentasTxtCodigoEtiqueta = new System.Windows.Forms.Label();
            this.VentasTxtCodigo = new System.Windows.Forms.TextBox();
            this.VentasTxtNombreEtiqueta = new System.Windows.Forms.Label();
            this.VentasTxtNombre = new System.Windows.Forms.TextBox();
            this.VentasCboEstadoEtiqueta = new System.Windows.Forms.Label();
            this.VentasCboEstado = new System.Windows.Forms.ComboBox();
            this.VentasTxtDescripcionEtiqueta = new System.Windows.Forms.Label();
            this.VentasTxtDescripcion = new System.Windows.Forms.TextBox();
            this.VentasLblRegistros = new System.Windows.Forms.Label();
            this.VentasDgvCatalogo = new System.Windows.Forms.DataGridView();
            this.navegador1 = new CapaVista_Navegador.Navegador();
            this.VentasPnlCabecera.SuspendLayout();
            this.VentasPnlDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvCatalogo)).BeginInit();
            this.SuspendLayout();
            // 
            // VentasBtnIngresar
            // 
            this.VentasBtnIngresar.AccessibleDescription = "Ingresar";
            this.VentasBtnIngresar.AccessibleName = "Ingresar";
            this.VentasBtnIngresar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnIngresar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalIngresarVentas;
            this.VentasBtnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnIngresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnIngresar.FlatAppearance.BorderSize = 0;
            this.VentasBtnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnIngresar.Location = new System.Drawing.Point(24, 536);
            this.VentasBtnIngresar.Name = "VentasBtnIngresar";
            this.VentasBtnIngresar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnIngresar.TabIndex = 4;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnIngresar, "Ingresar");
            this.VentasBtnIngresar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnConsultar
            // 
            this.VentasBtnConsultar.AccessibleDescription = "Consultar";
            this.VentasBtnConsultar.AccessibleName = "Consultar";
            this.VentasBtnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnConsultar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalConsultarVentas;
            this.VentasBtnConsultar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnConsultar.FlatAppearance.BorderSize = 0;
            this.VentasBtnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnConsultar.Location = new System.Drawing.Point(106, 536);
            this.VentasBtnConsultar.Name = "VentasBtnConsultar";
            this.VentasBtnConsultar.Size = new System.Drawing.Size(58, 64);
            this.VentasBtnConsultar.TabIndex = 5;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnConsultar, "Consultar");
            this.VentasBtnConsultar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnModificar
            // 
            this.VentasBtnModificar.AccessibleDescription = "Modificar";
            this.VentasBtnModificar.AccessibleName = "Modificar";
            this.VentasBtnModificar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnModificar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalModificarVentas;
            this.VentasBtnModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnModificar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnModificar.FlatAppearance.BorderSize = 0;
            this.VentasBtnModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnModificar.Location = new System.Drawing.Point(188, 536);
            this.VentasBtnModificar.Name = "VentasBtnModificar";
            this.VentasBtnModificar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnModificar.TabIndex = 6;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnModificar, "Modificar");
            this.VentasBtnModificar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnEliminar
            // 
            this.VentasBtnEliminar.AccessibleDescription = "Eliminar";
            this.VentasBtnEliminar.AccessibleName = "Eliminar";
            this.VentasBtnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnEliminar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalEliminarVentas;
            this.VentasBtnEliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnEliminar.FlatAppearance.BorderSize = 0;
            this.VentasBtnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnEliminar.Location = new System.Drawing.Point(270, 536);
            this.VentasBtnEliminar.Name = "VentasBtnEliminar";
            this.VentasBtnEliminar.Size = new System.Drawing.Size(55, 64);
            this.VentasBtnEliminar.TabIndex = 7;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnEliminar, "Eliminar");
            this.VentasBtnEliminar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnGuardar
            // 
            this.VentasBtnGuardar.AccessibleDescription = "Guardar";
            this.VentasBtnGuardar.AccessibleName = "Guardar";
            this.VentasBtnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnGuardar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalGuardarVentas;
            this.VentasBtnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnGuardar.FlatAppearance.BorderSize = 0;
            this.VentasBtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnGuardar.Location = new System.Drawing.Point(352, 536);
            this.VentasBtnGuardar.Name = "VentasBtnGuardar";
            this.VentasBtnGuardar.Size = new System.Drawing.Size(63, 64);
            this.VentasBtnGuardar.TabIndex = 8;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnGuardar, "Guardar");
            this.VentasBtnGuardar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnCancelar
            // 
            this.VentasBtnCancelar.AccessibleDescription = "Cancelar";
            this.VentasBtnCancelar.AccessibleName = "Cancelar";
            this.VentasBtnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnCancelar.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalCancelarVentas;
            this.VentasBtnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnCancelar.FlatAppearance.BorderSize = 0;
            this.VentasBtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnCancelar.Location = new System.Drawing.Point(434, 536);
            this.VentasBtnCancelar.Name = "VentasBtnCancelar";
            this.VentasBtnCancelar.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnCancelar.TabIndex = 9;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnCancelar, "Cancelar");
            this.VentasBtnCancelar.UseVisualStyleBackColor = false;
            // 
            // VentasBtnSalir
            // 
            this.VentasBtnSalir.AccessibleDescription = "Salir";
            this.VentasBtnSalir.AccessibleName = "Salir";
            this.VentasBtnSalir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(63)))), ((int)(((byte)(147)))));
            this.VentasBtnSalir.BackgroundImage = global::CapaVista_Mantenimientos.Properties.Resources.BotonOriginalSalirVentas;
            this.VentasBtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VentasBtnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.VentasBtnSalir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.VentasBtnSalir.FlatAppearance.BorderSize = 0;
            this.VentasBtnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.VentasBtnSalir.Location = new System.Drawing.Point(914, 536);
            this.VentasBtnSalir.Name = "VentasBtnSalir";
            this.VentasBtnSalir.Size = new System.Drawing.Size(64, 64);
            this.VentasBtnSalir.TabIndex = 10;
            this.VentasTtpAccionesOriginales.SetToolTip(this.VentasBtnSalir, "Salir");
            this.VentasBtnSalir.UseVisualStyleBackColor = false;
            // 
            // VentasDgvCatalogoColCodigo
            // 
            this.VentasDgvCatalogoColCodigo.HeaderText = "Código";
            this.VentasDgvCatalogoColCodigo.MinimumWidth = 6;
            this.VentasDgvCatalogoColCodigo.Name = "VentasDgvCatalogoColCodigo";
            this.VentasDgvCatalogoColCodigo.ReadOnly = true;
            // 
            // VentasDgvCatalogoColNombre
            // 
            this.VentasDgvCatalogoColNombre.HeaderText = "Nombre";
            this.VentasDgvCatalogoColNombre.MinimumWidth = 6;
            this.VentasDgvCatalogoColNombre.Name = "VentasDgvCatalogoColNombre";
            this.VentasDgvCatalogoColNombre.ReadOnly = true;
            // 
            // VentasDgvCatalogoColDescripcion
            // 
            this.VentasDgvCatalogoColDescripcion.HeaderText = "Descripción";
            this.VentasDgvCatalogoColDescripcion.MinimumWidth = 6;
            this.VentasDgvCatalogoColDescripcion.Name = "VentasDgvCatalogoColDescripcion";
            this.VentasDgvCatalogoColDescripcion.ReadOnly = true;
            // 
            // VentasDgvCatalogoColEstado
            // 
            this.VentasDgvCatalogoColEstado.HeaderText = "Estado";
            this.VentasDgvCatalogoColEstado.MinimumWidth = 6;
            this.VentasDgvCatalogoColEstado.Name = "VentasDgvCatalogoColEstado";
            this.VentasDgvCatalogoColEstado.ReadOnly = true;
            // 
            // VentasPnlCabecera
            // 
            this.VentasPnlCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasPnlCabecera.Controls.Add(this.VentasLblTitulo);
            this.VentasPnlCabecera.Controls.Add(this.VentasLblSubtitulo);
            this.VentasPnlCabecera.Controls.Add(this.VentasLblCodigo);
            this.VentasPnlCabecera.Location = new System.Drawing.Point(0, 0);
            this.VentasPnlCabecera.Name = "VentasPnlCabecera";
            this.VentasPnlCabecera.Size = new System.Drawing.Size(1000, 86);
            this.VentasPnlCabecera.TabIndex = 0;
            // 
            // VentasLblTitulo
            // 
            this.VentasLblTitulo.Font = new System.Drawing.Font("Lucida Fax", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.VentasLblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasLblTitulo.Location = new System.Drawing.Point(24, 16);
            this.VentasLblTitulo.Name = "VentasLblTitulo";
            this.VentasLblTitulo.Size = new System.Drawing.Size(750, 32);
            this.VentasLblTitulo.TabIndex = 0;
            this.VentasLblTitulo.Text = "Mantenimientos";
            // 
            // VentasLblSubtitulo
            // 
            this.VentasLblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.VentasLblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasLblSubtitulo.Location = new System.Drawing.Point(26, 56);
            this.VentasLblSubtitulo.Name = "VentasLblSubtitulo";
            this.VentasLblSubtitulo.Size = new System.Drawing.Size(760, 20);
            this.VentasLblSubtitulo.TabIndex = 1;
            this.VentasLblSubtitulo.Text = "Catálogos de Ventas y cuentas por cobrar";
            // 
            // VentasLblCodigo
            // 
            this.VentasLblCodigo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(162)))), ((int)(((byte)(74)))));
            this.VentasLblCodigo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.VentasLblCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblCodigo.Location = new System.Drawing.Point(834, 27);
            this.VentasLblCodigo.Name = "VentasLblCodigo";
            this.VentasLblCodigo.Size = new System.Drawing.Size(140, 28);
            this.VentasLblCodigo.TabIndex = 2;
            this.VentasLblCodigo.Text = "7009";
            this.VentasLblCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VentasPnlDatos
            // 
            this.VentasPnlDatos.BackColor = System.Drawing.Color.White;
            this.VentasPnlDatos.Controls.Add(this.VentasCboCatalogoEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasCboCatalogo);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtBuscarEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtBuscar);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtCodigoEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtCodigo);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtNombreEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtNombre);
            this.VentasPnlDatos.Controls.Add(this.VentasCboEstadoEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasCboEstado);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtDescripcionEtiqueta);
            this.VentasPnlDatos.Controls.Add(this.VentasTxtDescripcion);
            this.VentasPnlDatos.Location = new System.Drawing.Point(20, 104);
            this.VentasPnlDatos.Name = "VentasPnlDatos";
            this.VentasPnlDatos.Size = new System.Drawing.Size(960, 210);
            this.VentasPnlDatos.TabIndex = 1;
            // 
            // VentasCboCatalogoEtiqueta
            // 
            this.VentasCboCatalogoEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasCboCatalogoEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasCboCatalogoEtiqueta.Location = new System.Drawing.Point(16, 14);
            this.VentasCboCatalogoEtiqueta.Name = "VentasCboCatalogoEtiqueta";
            this.VentasCboCatalogoEtiqueta.Size = new System.Drawing.Size(350, 22);
            this.VentasCboCatalogoEtiqueta.TabIndex = 0;
            this.VentasCboCatalogoEtiqueta.Text = "Catálogo";
            // 
            // VentasCboCatalogo
            // 
            this.VentasCboCatalogo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboCatalogo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasCboCatalogo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasCboCatalogo.Items.AddRange(new object[] {
            "Clientes",
            "Vendedores",
            "Formas de pago",
            "Monedas"});
            this.VentasCboCatalogo.Location = new System.Drawing.Point(16, 38);
            this.VentasCboCatalogo.Name = "VentasCboCatalogo";
            this.VentasCboCatalogo.Size = new System.Drawing.Size(350, 31);
            this.VentasCboCatalogo.TabIndex = 1;
            // 
            // VentasTxtBuscarEtiqueta
            // 
            this.VentasTxtBuscarEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasTxtBuscarEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtBuscarEtiqueta.Location = new System.Drawing.Point(390, 14);
            this.VentasTxtBuscarEtiqueta.Name = "VentasTxtBuscarEtiqueta";
            this.VentasTxtBuscarEtiqueta.Size = new System.Drawing.Size(550, 22);
            this.VentasTxtBuscarEtiqueta.TabIndex = 2;
            this.VentasTxtBuscarEtiqueta.Text = "Buscar por código o nombre";
            // 
            // VentasTxtBuscar
            // 
            this.VentasTxtBuscar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTxtBuscar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtBuscar.Location = new System.Drawing.Point(390, 38);
            this.VentasTxtBuscar.Name = "VentasTxtBuscar";
            this.VentasTxtBuscar.Size = new System.Drawing.Size(550, 30);
            this.VentasTxtBuscar.TabIndex = 3;
            // 
            // VentasTxtCodigoEtiqueta
            // 
            this.VentasTxtCodigoEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasTxtCodigoEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtCodigoEtiqueta.Location = new System.Drawing.Point(16, 84);
            this.VentasTxtCodigoEtiqueta.Name = "VentasTxtCodigoEtiqueta";
            this.VentasTxtCodigoEtiqueta.Size = new System.Drawing.Size(180, 22);
            this.VentasTxtCodigoEtiqueta.TabIndex = 4;
            this.VentasTxtCodigoEtiqueta.Text = "Código";
            // 
            // VentasTxtCodigo
            // 
            this.VentasTxtCodigo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTxtCodigo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtCodigo.Location = new System.Drawing.Point(16, 108);
            this.VentasTxtCodigo.Name = "VentasTxtCodigo";
            this.VentasTxtCodigo.Size = new System.Drawing.Size(180, 30);
            this.VentasTxtCodigo.TabIndex = 5;
            // 
            // VentasTxtNombreEtiqueta
            // 
            this.VentasTxtNombreEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasTxtNombreEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtNombreEtiqueta.Location = new System.Drawing.Point(220, 84);
            this.VentasTxtNombreEtiqueta.Name = "VentasTxtNombreEtiqueta";
            this.VentasTxtNombreEtiqueta.Size = new System.Drawing.Size(460, 22);
            this.VentasTxtNombreEtiqueta.TabIndex = 6;
            this.VentasTxtNombreEtiqueta.Text = "Nombre";
            // 
            // VentasTxtNombre
            // 
            this.VentasTxtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTxtNombre.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtNombre.Location = new System.Drawing.Point(220, 108);
            this.VentasTxtNombre.Name = "VentasTxtNombre";
            this.VentasTxtNombre.Size = new System.Drawing.Size(460, 30);
            this.VentasTxtNombre.TabIndex = 7;
            // 
            // VentasCboEstadoEtiqueta
            // 
            this.VentasCboEstadoEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasCboEstadoEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasCboEstadoEtiqueta.Location = new System.Drawing.Point(704, 84);
            this.VentasCboEstadoEtiqueta.Name = "VentasCboEstadoEtiqueta";
            this.VentasCboEstadoEtiqueta.Size = new System.Drawing.Size(236, 22);
            this.VentasCboEstadoEtiqueta.TabIndex = 8;
            this.VentasCboEstadoEtiqueta.Text = "Estado";
            // 
            // VentasCboEstado
            // 
            this.VentasCboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.VentasCboEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasCboEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasCboEstado.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.VentasCboEstado.Location = new System.Drawing.Point(704, 108);
            this.VentasCboEstado.Name = "VentasCboEstado";
            this.VentasCboEstado.Size = new System.Drawing.Size(236, 31);
            this.VentasCboEstado.TabIndex = 9;
            // 
            // VentasTxtDescripcionEtiqueta
            // 
            this.VentasTxtDescripcionEtiqueta.Font = new System.Drawing.Font("Tahoma", 10F);
            this.VentasTxtDescripcionEtiqueta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtDescripcionEtiqueta.Location = new System.Drawing.Point(16, 148);
            this.VentasTxtDescripcionEtiqueta.Name = "VentasTxtDescripcionEtiqueta";
            this.VentasTxtDescripcionEtiqueta.Size = new System.Drawing.Size(924, 22);
            this.VentasTxtDescripcionEtiqueta.TabIndex = 10;
            this.VentasTxtDescripcionEtiqueta.Text = "Descripción";
            // 
            // VentasTxtDescripcion
            // 
            this.VentasTxtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasTxtDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasTxtDescripcion.Location = new System.Drawing.Point(16, 172);
            this.VentasTxtDescripcion.Name = "VentasTxtDescripcion";
            this.VentasTxtDescripcion.Size = new System.Drawing.Size(924, 30);
            this.VentasTxtDescripcion.TabIndex = 11;
            // 
            // VentasLblRegistros
            // 
            this.VentasLblRegistros.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.VentasLblRegistros.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.VentasLblRegistros.Location = new System.Drawing.Point(24, 330);
            this.VentasLblRegistros.Name = "VentasLblRegistros";
            this.VentasLblRegistros.Size = new System.Drawing.Size(900, 22);
            this.VentasLblRegistros.TabIndex = 2;
            this.VentasLblRegistros.Text = "Registros del catálogo";
            // 
            // VentasDgvCatalogo
            // 
            this.VentasDgvCatalogo.AllowUserToAddRows = false;
            this.VentasDgvCatalogo.AllowUserToDeleteRows = false;
            this.VentasDgvCatalogo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.VentasDgvCatalogo.BackgroundColor = System.Drawing.Color.White;
            this.VentasDgvCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.VentasDgvCatalogo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.VentasDgvCatalogo.ColumnHeadersHeight = 32;
            this.VentasDgvCatalogo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.VentasDgvCatalogo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VentasDgvCatalogoColCodigo,
            this.VentasDgvCatalogoColNombre,
            this.VentasDgvCatalogoColDescripcion,
            this.VentasDgvCatalogoColEstado});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(212)))), ((int)(((byte)(234)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.VentasDgvCatalogo.DefaultCellStyle = dataGridViewCellStyle2;
            this.VentasDgvCatalogo.EnableHeadersVisualStyles = false;
            this.VentasDgvCatalogo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.VentasDgvCatalogo.Location = new System.Drawing.Point(20, 360);
            this.VentasDgvCatalogo.MultiSelect = false;
            this.VentasDgvCatalogo.Name = "VentasDgvCatalogo";
            this.VentasDgvCatalogo.ReadOnly = true;
            this.VentasDgvCatalogo.RowHeadersVisible = false;
            this.VentasDgvCatalogo.RowHeadersWidth = 51;
            this.VentasDgvCatalogo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.VentasDgvCatalogo.Size = new System.Drawing.Size(960, 156);
            this.VentasDgvCatalogo.TabIndex = 3;
            // 
            // navegador1
            // 
            this.navegador1.Location = new System.Drawing.Point(12, 80);
            this.navegador1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.navegador1.Name = "navegador1";
            this.navegador1.Size = new System.Drawing.Size(1438, 111);
            this.navegador1.TabIndex = 11;
            // 
            // FrmMantenimientosVentas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.CancelButton = this.VentasBtnSalir;
            this.ClientSize = new System.Drawing.Size(1362, 620);
            this.Controls.Add(this.navegador1);
            this.Controls.Add(this.VentasPnlCabecera);
            this.Controls.Add(this.VentasPnlDatos);
            this.Controls.Add(this.VentasLblRegistros);
            this.Controls.Add(this.VentasDgvCatalogo);
            this.Controls.Add(this.VentasBtnIngresar);
            this.Controls.Add(this.VentasBtnConsultar);
            this.Controls.Add(this.VentasBtnModificar);
            this.Controls.Add(this.VentasBtnEliminar);
            this.Controls.Add(this.VentasBtnGuardar);
            this.Controls.Add(this.VentasBtnCancelar);
            this.Controls.Add(this.VentasBtnSalir);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(29)))), ((int)(((byte)(74)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = global::CapaVista_Mantenimientos.Properties.Resources.IconoVentas;
            this.MaximizeBox = false;
            this.Name = "FrmMantenimientosVentas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "7009 – Mantenimientos";
            this.VentasPnlCabecera.ResumeLayout(false);
            this.VentasPnlDatos.ResumeLayout(false);
            this.VentasPnlDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VentasDgvCatalogo)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private System.Windows.Forms.ToolTip VentasTtpAccionesOriginales;
        private System.Windows.Forms.Panel VentasPnlCabecera;
        private System.Windows.Forms.Label VentasLblTitulo;
        private System.Windows.Forms.Label VentasLblSubtitulo;
        private System.Windows.Forms.Label VentasLblCodigo;
        private System.Windows.Forms.Panel VentasPnlDatos;
        private System.Windows.Forms.Label VentasCboCatalogoEtiqueta;
        private System.Windows.Forms.ComboBox VentasCboCatalogo;
        private System.Windows.Forms.Label VentasTxtBuscarEtiqueta;
        private System.Windows.Forms.TextBox VentasTxtBuscar;
        private System.Windows.Forms.Label VentasTxtCodigoEtiqueta;
        private System.Windows.Forms.TextBox VentasTxtCodigo;
        private System.Windows.Forms.Label VentasTxtNombreEtiqueta;
        private System.Windows.Forms.TextBox VentasTxtNombre;
        private System.Windows.Forms.Label VentasCboEstadoEtiqueta;
        private System.Windows.Forms.ComboBox VentasCboEstado;
        private System.Windows.Forms.Label VentasTxtDescripcionEtiqueta;
        private System.Windows.Forms.TextBox VentasTxtDescripcion;
        private System.Windows.Forms.Label VentasLblRegistros;
        private System.Windows.Forms.DataGridView VentasDgvCatalogo;
        private System.Windows.Forms.Button VentasBtnIngresar;
        private System.Windows.Forms.Button VentasBtnConsultar;
        private System.Windows.Forms.Button VentasBtnModificar;
        private System.Windows.Forms.Button VentasBtnEliminar;
        private System.Windows.Forms.Button VentasBtnGuardar;
        private System.Windows.Forms.Button VentasBtnCancelar;
        private System.Windows.Forms.Button VentasBtnSalir;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvCatalogoColCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvCatalogoColNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvCatalogoColDescripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn VentasDgvCatalogoColEstado;
        private CapaVista_Navegador.Navegador navegador1;
    }
}
// FIN DEL CÓDIGO — Equipo asignado al formulario | Fecha: 14/10/2026
