namespace CapaVista_MuestreoAuditoria_Logistica
{
    partial class FrmAuditoriaInventario
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAuditoriaInventario));
            this.MuestreoLblTitulo = new System.Windows.Forms.Label();
            this.MuestreoLblDescripcion = new System.Windows.Forms.Label();
            this.MuestreoPnlOpciones = new System.Windows.Forms.Panel();
            this.MuestreoGpbConsulta = new System.Windows.Forms.GroupBox();
            this.MuestreoBtnBuscar = new System.Windows.Forms.Button();
            this.MuestreoTxtBuscar = new System.Windows.Forms.TextBox();
            this.MuestreoCboEstado = new System.Windows.Forms.ComboBox();
            this.MuestreoCboBodega = new System.Windows.Forms.ComboBox();
            this.MuestreoLblBuscar = new System.Windows.Forms.Label();
            this.MuestreoLblEstado = new System.Windows.Forms.Label();
            this.MuestreoLblBodega = new System.Windows.Forms.Label();
            this.MuestreoGpbNuevaAuditoria = new System.Windows.Forms.GroupBox();
            this.MuestreoBtnMultiple = new System.Windows.Forms.Button();
            this.MuestreoBtnIndividual = new System.Windows.Forms.Button();
            this.MuestreoPnlHistorial = new System.Windows.Forms.Panel();
            this.MuestreoLblPagina = new System.Windows.Forms.Label();
            this.MuestreoBtnActualizar = new System.Windows.Forms.Button();
            this.MuestreoBtnEditar = new System.Windows.Forms.Button();
            this.MuestreoBtnConsultar = new System.Windows.Forms.Button();
            this.MuestreoBtnSiguiente = new System.Windows.Forms.Button();
            this.MuestreoBtnAnterior = new System.Windows.Forms.Button();
            this.MuestreoDgvAuditorias = new System.Windows.Forms.DataGridView();
            this.colCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBodega = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colResultado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colObservaciones = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MuestreoLblHistorial = new System.Windows.Forms.Label();
            this.MuestreoPnlOpciones.SuspendLayout();
            this.MuestreoGpbConsulta.SuspendLayout();
            this.MuestreoGpbNuevaAuditoria.SuspendLayout();
            this.MuestreoPnlHistorial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MuestreoDgvAuditorias)).BeginInit();
            this.SuspendLayout();
            // 
            // MuestreoLblTitulo
            // 
            this.MuestreoLblTitulo.AutoSize = true;
            this.MuestreoLblTitulo.BackColor = System.Drawing.Color.Transparent;
            this.MuestreoLblTitulo.Font = new System.Drawing.Font("Lucida Bright", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoLblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoLblTitulo.Location = new System.Drawing.Point(109, 20);
            this.MuestreoLblTitulo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblTitulo.Name = "MuestreoLblTitulo";
            this.MuestreoLblTitulo.Size = new System.Drawing.Size(489, 31);
            this.MuestreoLblTitulo.TabIndex = 0;
            this.MuestreoLblTitulo.Text = "Muestreo y Auditoría de Inventario";
            this.MuestreoLblTitulo.UseWaitCursor = true;
            this.MuestreoLblTitulo.Click += new System.EventHandler(this.label1_Click);
            // 
            // MuestreoLblDescripcion
            // 
            this.MuestreoLblDescripcion.AutoSize = true;
            this.MuestreoLblDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.MuestreoLblDescripcion.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoLblDescripcion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(114)))), ((int)(((byte)(56)))));
            this.MuestreoLblDescripcion.Location = new System.Drawing.Point(111, 58);
            this.MuestreoLblDescripcion.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblDescripcion.Name = "MuestreoLblDescripcion";
            this.MuestreoLblDescripcion.Size = new System.Drawing.Size(327, 18);
            this.MuestreoLblDescripcion.TabIndex = 1;
            this.MuestreoLblDescripcion.Text = "Control y verificación de existencias de productos";
            this.MuestreoLblDescripcion.UseWaitCursor = true;
            // 
            // MuestreoPnlOpciones
            // 
            this.MuestreoPnlOpciones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MuestreoPnlOpciones.BackColor = System.Drawing.Color.White;
            this.MuestreoPnlOpciones.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MuestreoPnlOpciones.Controls.Add(this.MuestreoGpbConsulta);
            this.MuestreoPnlOpciones.Controls.Add(this.MuestreoGpbNuevaAuditoria);
            this.MuestreoPnlOpciones.Location = new System.Drawing.Point(52, 101);
            this.MuestreoPnlOpciones.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoPnlOpciones.Name = "MuestreoPnlOpciones";
            this.MuestreoPnlOpciones.Size = new System.Drawing.Size(867, 159);
            this.MuestreoPnlOpciones.TabIndex = 2;
            this.MuestreoPnlOpciones.UseWaitCursor = true;
            // 
            // MuestreoGpbConsulta
            // 
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoBtnBuscar);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoTxtBuscar);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoCboEstado);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoCboBodega);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoLblBuscar);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoLblEstado);
            this.MuestreoGpbConsulta.Controls.Add(this.MuestreoLblBodega);
            this.MuestreoGpbConsulta.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoGpbConsulta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoGpbConsulta.Location = new System.Drawing.Point(431, 8);
            this.MuestreoGpbConsulta.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoGpbConsulta.Name = "MuestreoGpbConsulta";
            this.MuestreoGpbConsulta.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoGpbConsulta.Size = new System.Drawing.Size(420, 138);
            this.MuestreoGpbConsulta.TabIndex = 4;
            this.MuestreoGpbConsulta.TabStop = false;
            this.MuestreoGpbConsulta.Text = "Consulta rápida";
            this.MuestreoGpbConsulta.UseWaitCursor = true;
            // 
            // MuestreoBtnBuscar
            // 
            this.MuestreoBtnBuscar.Location = new System.Drawing.Point(278, 93);
            this.MuestreoBtnBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnBuscar.Name = "MuestreoBtnBuscar";
            this.MuestreoBtnBuscar.Size = new System.Drawing.Size(120, 31);
            this.MuestreoBtnBuscar.TabIndex = 6;
            this.MuestreoBtnBuscar.Text = "Buscar";
            this.MuestreoBtnBuscar.UseVisualStyleBackColor = true;
            this.MuestreoBtnBuscar.UseWaitCursor = true;
            // 
            // MuestreoTxtBuscar
            // 
            this.MuestreoTxtBuscar.Location = new System.Drawing.Point(11, 98);
            this.MuestreoTxtBuscar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoTxtBuscar.Name = "MuestreoTxtBuscar";
            this.MuestreoTxtBuscar.Size = new System.Drawing.Size(252, 25);
            this.MuestreoTxtBuscar.TabIndex = 5;
            this.MuestreoTxtBuscar.UseWaitCursor = true;
            // 
            // MuestreoCboEstado
            // 
            this.MuestreoCboEstado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MuestreoCboEstado.FormattingEnabled = true;
            this.MuestreoCboEstado.Location = new System.Drawing.Point(150, 46);
            this.MuestreoCboEstado.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoCboEstado.Name = "MuestreoCboEstado";
            this.MuestreoCboEstado.Size = new System.Drawing.Size(114, 25);
            this.MuestreoCboEstado.TabIndex = 4;
            this.MuestreoCboEstado.UseWaitCursor = true;
            // 
            // MuestreoCboBodega
            // 
            this.MuestreoCboBodega.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MuestreoCboBodega.FormattingEnabled = true;
            this.MuestreoCboBodega.Location = new System.Drawing.Point(11, 46);
            this.MuestreoCboBodega.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoCboBodega.Name = "MuestreoCboBodega";
            this.MuestreoCboBodega.Size = new System.Drawing.Size(128, 25);
            this.MuestreoCboBodega.TabIndex = 3;
            this.MuestreoCboBodega.UseWaitCursor = true;
            // 
            // MuestreoLblBuscar
            // 
            this.MuestreoLblBuscar.AutoSize = true;
            this.MuestreoLblBuscar.Location = new System.Drawing.Point(11, 159);
            this.MuestreoLblBuscar.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblBuscar.Name = "MuestreoLblBuscar";
            this.MuestreoLblBuscar.Size = new System.Drawing.Size(141, 18);
            this.MuestreoLblBuscar.TabIndex = 2;
            this.MuestreoLblBuscar.Text = "Buscar por código";
            this.MuestreoLblBuscar.UseWaitCursor = true;
            // 
            // MuestreoLblEstado
            // 
            this.MuestreoLblEstado.AutoSize = true;
            this.MuestreoLblEstado.Location = new System.Drawing.Point(150, 27);
            this.MuestreoLblEstado.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblEstado.Name = "MuestreoLblEstado";
            this.MuestreoLblEstado.Size = new System.Drawing.Size(58, 18);
            this.MuestreoLblEstado.TabIndex = 1;
            this.MuestreoLblEstado.Text = "Estado";
            this.MuestreoLblEstado.UseWaitCursor = true;
            // 
            // MuestreoLblBodega
            // 
            this.MuestreoLblBodega.AutoSize = true;
            this.MuestreoLblBodega.Location = new System.Drawing.Point(11, 27);
            this.MuestreoLblBodega.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblBodega.Name = "MuestreoLblBodega";
            this.MuestreoLblBodega.Size = new System.Drawing.Size(63, 18);
            this.MuestreoLblBodega.TabIndex = 0;
            this.MuestreoLblBodega.Text = "Bodega";
            this.MuestreoLblBodega.UseWaitCursor = true;
            // 
            // MuestreoGpbNuevaAuditoria
            // 
            this.MuestreoGpbNuevaAuditoria.Controls.Add(this.MuestreoBtnMultiple);
            this.MuestreoGpbNuevaAuditoria.Controls.Add(this.MuestreoBtnIndividual);
            this.MuestreoGpbNuevaAuditoria.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoGpbNuevaAuditoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoGpbNuevaAuditoria.Location = new System.Drawing.Point(11, 8);
            this.MuestreoGpbNuevaAuditoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoGpbNuevaAuditoria.Name = "MuestreoGpbNuevaAuditoria";
            this.MuestreoGpbNuevaAuditoria.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoGpbNuevaAuditoria.Size = new System.Drawing.Size(405, 138);
            this.MuestreoGpbNuevaAuditoria.TabIndex = 3;
            this.MuestreoGpbNuevaAuditoria.TabStop = false;
            this.MuestreoGpbNuevaAuditoria.Text = "Nueva auditoría";
            this.MuestreoGpbNuevaAuditoria.UseWaitCursor = true;
            // 
            // MuestreoBtnMultiple
            // 
            this.MuestreoBtnMultiple.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnMultiple.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.MuestreoBtnMultiple.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnMultiple.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoBtnMultiple.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnMultiple.Location = new System.Drawing.Point(206, 37);
            this.MuestreoBtnMultiple.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnMultiple.Name = "MuestreoBtnMultiple";
            this.MuestreoBtnMultiple.Size = new System.Drawing.Size(180, 61);
            this.MuestreoBtnMultiple.TabIndex = 1;
            this.MuestreoBtnMultiple.Text = "Iniciar auditoría múltiple";
            this.MuestreoBtnMultiple.UseVisualStyleBackColor = false;
            this.MuestreoBtnMultiple.UseWaitCursor = true;
            // 
            // MuestreoBtnIndividual
            // 
            this.MuestreoBtnIndividual.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnIndividual.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.MuestreoBtnIndividual.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnIndividual.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoBtnIndividual.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnIndividual.Location = new System.Drawing.Point(15, 37);
            this.MuestreoBtnIndividual.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnIndividual.Name = "MuestreoBtnIndividual";
            this.MuestreoBtnIndividual.Size = new System.Drawing.Size(180, 61);
            this.MuestreoBtnIndividual.TabIndex = 0;
            this.MuestreoBtnIndividual.Text = "Iniciar auditoría individual";
            this.MuestreoBtnIndividual.UseVisualStyleBackColor = false;
            this.MuestreoBtnIndividual.UseWaitCursor = true;
            // 
            // MuestreoPnlHistorial
            // 
            this.MuestreoPnlHistorial.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MuestreoPnlHistorial.BackColor = System.Drawing.Color.White;
            this.MuestreoPnlHistorial.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoLblPagina);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoBtnActualizar);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoBtnEditar);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoBtnConsultar);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoBtnSiguiente);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoBtnAnterior);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoDgvAuditorias);
            this.MuestreoPnlHistorial.Controls.Add(this.MuestreoLblHistorial);
            this.MuestreoPnlHistorial.Location = new System.Drawing.Point(52, 361);
            this.MuestreoPnlHistorial.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoPnlHistorial.Name = "MuestreoPnlHistorial";
            this.MuestreoPnlHistorial.Size = new System.Drawing.Size(867, 268);
            this.MuestreoPnlHistorial.TabIndex = 3;
            this.MuestreoPnlHistorial.UseWaitCursor = true;
            // 
            // MuestreoLblPagina
            // 
            this.MuestreoLblPagina.AutoSize = true;
            this.MuestreoLblPagina.Location = new System.Drawing.Point(109, 231);
            this.MuestreoLblPagina.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblPagina.Name = "MuestreoLblPagina";
            this.MuestreoLblPagina.Size = new System.Drawing.Size(73, 13);
            this.MuestreoLblPagina.TabIndex = 7;
            this.MuestreoLblPagina.Text = "Página 1 de 1";
            this.MuestreoLblPagina.UseWaitCursor = true;
            // 
            // MuestreoBtnActualizar
            // 
            this.MuestreoBtnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnActualizar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnActualizar.Location = new System.Drawing.Point(716, 223);
            this.MuestreoBtnActualizar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnActualizar.Name = "MuestreoBtnActualizar";
            this.MuestreoBtnActualizar.Size = new System.Drawing.Size(109, 28);
            this.MuestreoBtnActualizar.TabIndex = 6;
            this.MuestreoBtnActualizar.Text = "Actualizar";
            this.MuestreoBtnActualizar.UseVisualStyleBackColor = false;
            this.MuestreoBtnActualizar.UseWaitCursor = true;
            // 
            // MuestreoBtnEditar
            // 
            this.MuestreoBtnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnEditar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnEditar.Location = new System.Drawing.Point(600, 223);
            this.MuestreoBtnEditar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnEditar.Name = "MuestreoBtnEditar";
            this.MuestreoBtnEditar.Size = new System.Drawing.Size(109, 28);
            this.MuestreoBtnEditar.TabIndex = 5;
            this.MuestreoBtnEditar.Text = "Editar";
            this.MuestreoBtnEditar.UseVisualStyleBackColor = false;
            this.MuestreoBtnEditar.UseWaitCursor = true;
            // 
            // MuestreoBtnConsultar
            // 
            this.MuestreoBtnConsultar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnConsultar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnConsultar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnConsultar.Location = new System.Drawing.Point(484, 223);
            this.MuestreoBtnConsultar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnConsultar.Name = "MuestreoBtnConsultar";
            this.MuestreoBtnConsultar.Size = new System.Drawing.Size(109, 28);
            this.MuestreoBtnConsultar.TabIndex = 4;
            this.MuestreoBtnConsultar.Text = "Consultar";
            this.MuestreoBtnConsultar.UseVisualStyleBackColor = false;
            this.MuestreoBtnConsultar.UseWaitCursor = true;
            // 
            // MuestreoBtnSiguiente
            // 
            this.MuestreoBtnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnSiguiente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnSiguiente.Location = new System.Drawing.Point(184, 223);
            this.MuestreoBtnSiguiente.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnSiguiente.Name = "MuestreoBtnSiguiente";
            this.MuestreoBtnSiguiente.Size = new System.Drawing.Size(79, 28);
            this.MuestreoBtnSiguiente.TabIndex = 3;
            this.MuestreoBtnSiguiente.Text = "Siguiente";
            this.MuestreoBtnSiguiente.UseVisualStyleBackColor = false;
            this.MuestreoBtnSiguiente.UseWaitCursor = true;
            // 
            // MuestreoBtnAnterior
            // 
            this.MuestreoBtnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(216)))), ((int)(((byte)(197)))));
            this.MuestreoBtnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MuestreoBtnAnterior.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoBtnAnterior.Location = new System.Drawing.Point(15, 223);
            this.MuestreoBtnAnterior.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoBtnAnterior.Name = "MuestreoBtnAnterior";
            this.MuestreoBtnAnterior.Size = new System.Drawing.Size(79, 28);
            this.MuestreoBtnAnterior.TabIndex = 2;
            this.MuestreoBtnAnterior.Text = "Anterior";
            this.MuestreoBtnAnterior.UseVisualStyleBackColor = false;
            this.MuestreoBtnAnterior.UseWaitCursor = true;
            // 
            // MuestreoDgvAuditorias
            // 
            this.MuestreoDgvAuditorias.AllowUserToAddRows = false;
            this.MuestreoDgvAuditorias.AllowUserToDeleteRows = false;
            this.MuestreoDgvAuditorias.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.MuestreoDgvAuditorias.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MuestreoDgvAuditorias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MuestreoDgvAuditorias.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.MuestreoDgvAuditorias.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MuestreoDgvAuditorias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MuestreoDgvAuditorias.ColumnHeadersHeight = 38;
            this.MuestreoDgvAuditorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.MuestreoDgvAuditorias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigo,
            this.colFecha,
            this.colBodega,
            this.colTipo,
            this.colProductos,
            this.colResultado,
            this.colEstado,
            this.colObservaciones});
            this.MuestreoDgvAuditorias.EnableHeadersVisualStyles = false;
            this.MuestreoDgvAuditorias.Location = new System.Drawing.Point(15, 41);
            this.MuestreoDgvAuditorias.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MuestreoDgvAuditorias.MultiSelect = false;
            this.MuestreoDgvAuditorias.Name = "MuestreoDgvAuditorias";
            this.MuestreoDgvAuditorias.ReadOnly = true;
            this.MuestreoDgvAuditorias.RowHeadersVisible = false;
            this.MuestreoDgvAuditorias.RowHeadersWidth = 51;
            this.MuestreoDgvAuditorias.RowTemplate.Height = 30;
            this.MuestreoDgvAuditorias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MuestreoDgvAuditorias.Size = new System.Drawing.Size(832, 167);
            this.MuestreoDgvAuditorias.TabIndex = 1;
            this.MuestreoDgvAuditorias.UseWaitCursor = true;
            // 
            // colCodigo
            // 
            this.colCodigo.HeaderText = "Código";
            this.colCodigo.MinimumWidth = 6;
            this.colCodigo.Name = "colCodigo";
            this.colCodigo.ReadOnly = true;
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.MinimumWidth = 6;
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            // 
            // colBodega
            // 
            this.colBodega.HeaderText = "Bodega";
            this.colBodega.MinimumWidth = 6;
            this.colBodega.Name = "colBodega";
            this.colBodega.ReadOnly = true;
            // 
            // colTipo
            // 
            this.colTipo.HeaderText = "Tipo de auditoría";
            this.colTipo.MinimumWidth = 6;
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            // 
            // colProductos
            // 
            this.colProductos.HeaderText = "Productos";
            this.colProductos.MinimumWidth = 6;
            this.colProductos.Name = "colProductos";
            this.colProductos.ReadOnly = true;
            // 
            // colResultado
            // 
            this.colResultado.HeaderText = "Resultado";
            this.colResultado.MinimumWidth = 6;
            this.colResultado.Name = "colResultado";
            this.colResultado.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 6;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // colObservaciones
            // 
            this.colObservaciones.HeaderText = "Observaciones";
            this.colObservaciones.MinimumWidth = 6;
            this.colObservaciones.Name = "colObservaciones";
            this.colObservaciones.ReadOnly = true;
            // 
            // MuestreoLblHistorial
            // 
            this.MuestreoLblHistorial.AutoSize = true;
            this.MuestreoLblHistorial.BackColor = System.Drawing.Color.Transparent;
            this.MuestreoLblHistorial.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MuestreoLblHistorial.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.MuestreoLblHistorial.Location = new System.Drawing.Point(15, 12);
            this.MuestreoLblHistorial.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MuestreoLblHistorial.Name = "MuestreoLblHistorial";
            this.MuestreoLblHistorial.Size = new System.Drawing.Size(190, 19);
            this.MuestreoLblHistorial.TabIndex = 0;
            this.MuestreoLblHistorial.Text = "Historial de auditorías";
            this.MuestreoLblHistorial.UseWaitCursor = true;
            // 
            // FrmAuditoriaInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CapaVista_MuestreoAuditoria_Logistica.Properties.Resources.FondoLogistica;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(949, 609);
            this.Controls.Add(this.MuestreoPnlHistorial);
            this.Controls.Add(this.MuestreoPnlOpciones);
            this.Controls.Add(this.MuestreoLblDescripcion);
            this.Controls.Add(this.MuestreoLblTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "FrmAuditoriaInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "5011 - Auditoría y muestreo de inventario";
            this.UseWaitCursor = true;
            this.MuestreoPnlOpciones.ResumeLayout(false);
            this.MuestreoGpbConsulta.ResumeLayout(false);
            this.MuestreoGpbConsulta.PerformLayout();
            this.MuestreoGpbNuevaAuditoria.ResumeLayout(false);
            this.MuestreoPnlHistorial.ResumeLayout(false);
            this.MuestreoPnlHistorial.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MuestreoDgvAuditorias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MuestreoLblTitulo;
        private System.Windows.Forms.Label MuestreoLblDescripcion;
        private System.Windows.Forms.Panel MuestreoPnlOpciones;
        private System.Windows.Forms.GroupBox MuestreoGpbNuevaAuditoria;
        private System.Windows.Forms.Button MuestreoBtnMultiple;
        private System.Windows.Forms.Button MuestreoBtnIndividual;
        private System.Windows.Forms.GroupBox MuestreoGpbConsulta;
        private System.Windows.Forms.ComboBox MuestreoCboEstado;
        private System.Windows.Forms.ComboBox MuestreoCboBodega;
        private System.Windows.Forms.Label MuestreoLblBuscar;
        private System.Windows.Forms.Label MuestreoLblEstado;
        private System.Windows.Forms.Label MuestreoLblBodega;
        private System.Windows.Forms.Button MuestreoBtnBuscar;
        private System.Windows.Forms.TextBox MuestreoTxtBuscar;
        private System.Windows.Forms.Panel MuestreoPnlHistorial;
        private System.Windows.Forms.DataGridView MuestreoDgvAuditorias;
        private System.Windows.Forms.Label MuestreoLblHistorial;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBodega;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colResultado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colObservaciones;
        private System.Windows.Forms.Button MuestreoBtnActualizar;
        private System.Windows.Forms.Button MuestreoBtnEditar;
        private System.Windows.Forms.Button MuestreoBtnConsultar;
        private System.Windows.Forms.Button MuestreoBtnSiguiente;
        private System.Windows.Forms.Button MuestreoBtnAnterior;
        private System.Windows.Forms.Label MuestreoLblPagina;
    }
}