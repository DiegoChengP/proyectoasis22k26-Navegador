namespace CapaVista_TransporteInterno_Logistica.Formas
{
    partial class FrmTransporteInterno
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTransporteInterno));
            this.LogisticaLblTituloTransporteInterno = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.LogisticaNavTransporteInterno = new CapaVista_Navegador.Navegador();
            this.lblUltimosViajes = new System.Windows.Forms.Label();
            this.dgvUltimosViajes = new System.Windows.Forms.DataGridView();
            this.colCodigoViaje = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTipoTraslado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrigen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDestino = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConductor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoraSalida = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colHoraLlegada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtpFechaConsulta = new System.Windows.Forms.DateTimePicker();
            this.txtBuscarViaje = new System.Windows.Forms.TextBox();
            this.LogisticaBtnGestionViajes = new System.Windows.Forms.Button();
            this.LogisticaBtnControlTransporte = new System.Windows.Forms.Button();
            this.LogisticaBtnConsultasTransporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimosViajes)).BeginInit();
            this.SuspendLayout();
            // 
            // LogisticaLblTituloTransporteInterno
            // 
            this.LogisticaLblTituloTransporteInterno.AutoSize = true;
            this.LogisticaLblTituloTransporteInterno.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblTituloTransporteInterno.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.LogisticaLblTituloTransporteInterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaLblTituloTransporteInterno.Location = new System.Drawing.Point(9, 113);
            this.LogisticaLblTituloTransporteInterno.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LogisticaLblTituloTransporteInterno.Name = "LogisticaLblTituloTransporteInterno";
            this.LogisticaLblTituloTransporteInterno.Size = new System.Drawing.Size(453, 32);
            this.LogisticaLblTituloTransporteInterno.TabIndex = 14;
            this.LogisticaLblTituloTransporteInterno.Text = "Módulo de Transporte Interno";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.label1.Location = new System.Drawing.Point(10, 145);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(809, 21);
            this.label1.TabIndex = 15;
            this.label1.Text = "Administra la planificación, asignación y seguimiento de los vehículos que realiz" +
    "an traslados de productos y ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.label2.Location = new System.Drawing.Point(10, 167);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(975, 21);
            this.label2.TabIndex = 16;
            this.label2.Text = "materiales entre bodegas, áreas internas y sucursales, garantizando la seguridad," +
    " la cadena de frío y la trazabilidad de cada viaje.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.label3.Location = new System.Drawing.Point(9, 80);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 32);
            this.label3.TabIndex = 18;
            this.label3.Text = "Bienvenido al";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::CapaVista_TransporteInterno_Logistica.Properties.Resources.f844b316_02c8_41af_9c16_226a1ef14d22;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(84, 200);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(723, 180);
            this.pictureBox2.TabIndex = 19;
            this.pictureBox2.TabStop = false;
            // 
            // LogisticaNavTransporteInterno
            // 
            this.LogisticaNavTransporteInterno.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaNavTransporteInterno.Location = new System.Drawing.Point(-10, -11);
            this.LogisticaNavTransporteInterno.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LogisticaNavTransporteInterno.Name = "LogisticaNavTransporteInterno";
            this.LogisticaNavTransporteInterno.Size = new System.Drawing.Size(1078, 90);
            this.LogisticaNavTransporteInterno.TabIndex = 13;
            // 
            // lblUltimosViajes
            // 
            this.lblUltimosViajes.AutoSize = true;
            this.lblUltimosViajes.BackColor = System.Drawing.Color.Transparent;
            this.lblUltimosViajes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUltimosViajes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.lblUltimosViajes.Location = new System.Drawing.Point(295, 390);
            this.lblUltimosViajes.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUltimosViajes.Name = "lblUltimosViajes";
            this.lblUltimosViajes.Size = new System.Drawing.Size(207, 21);
            this.lblUltimosViajes.TabIndex = 25;
            this.lblUltimosViajes.Text = "Últimos viajes registrados";
            // 
            // dgvUltimosViajes
            // 
            this.dgvUltimosViajes.AllowUserToAddRows = false;
            this.dgvUltimosViajes.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvUltimosViajes.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUltimosViajes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUltimosViajes.BackgroundColor = System.Drawing.Color.White;
            this.dgvUltimosViajes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUltimosViajes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUltimosViajes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUltimosViajes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCodigoViaje,
            this.colFecha,
            this.colTipoTraslado,
            this.colOrigen,
            this.colDestino,
            this.colVehiculo,
            this.colConductor,
            this.colEstado,
            this.colHoraSalida,
            this.colHoraLlegada});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUltimosViajes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUltimosViajes.EnableHeadersVisualStyles = false;
            this.dgvUltimosViajes.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(207)))), ((int)(((byte)(192)))));
            this.dgvUltimosViajes.Location = new System.Drawing.Point(38, 422);
            this.dgvUltimosViajes.Margin = new System.Windows.Forms.Padding(2);
            this.dgvUltimosViajes.Name = "dgvUltimosViajes";
            this.dgvUltimosViajes.ReadOnly = true;
            this.dgvUltimosViajes.RowHeadersVisible = false;
            this.dgvUltimosViajes.RowHeadersWidth = 51;
            this.dgvUltimosViajes.RowTemplate.Height = 24;
            this.dgvUltimosViajes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUltimosViajes.Size = new System.Drawing.Size(938, 179);
            this.dgvUltimosViajes.TabIndex = 26;
            // 
            // colCodigoViaje
            // 
            this.colCodigoViaje.HeaderText = "Código";
            this.colCodigoViaje.MinimumWidth = 6;
            this.colCodigoViaje.Name = "colCodigoViaje";
            this.colCodigoViaje.ReadOnly = true;
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.MinimumWidth = 6;
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            // 
            // colTipoTraslado
            // 
            this.colTipoTraslado.HeaderText = "Tipo de traslado";
            this.colTipoTraslado.MinimumWidth = 6;
            this.colTipoTraslado.Name = "colTipoTraslado";
            this.colTipoTraslado.ReadOnly = true;
            // 
            // colOrigen
            // 
            this.colOrigen.HeaderText = "Origen";
            this.colOrigen.MinimumWidth = 6;
            this.colOrigen.Name = "colOrigen";
            this.colOrigen.ReadOnly = true;
            // 
            // colDestino
            // 
            this.colDestino.HeaderText = "Destino";
            this.colDestino.MinimumWidth = 6;
            this.colDestino.Name = "colDestino";
            this.colDestino.ReadOnly = true;
            // 
            // colVehiculo
            // 
            this.colVehiculo.HeaderText = "Vehículo";
            this.colVehiculo.MinimumWidth = 6;
            this.colVehiculo.Name = "colVehiculo";
            this.colVehiculo.ReadOnly = true;
            // 
            // colConductor
            // 
            this.colConductor.HeaderText = "Conductor";
            this.colConductor.MinimumWidth = 6;
            this.colConductor.Name = "colConductor";
            this.colConductor.ReadOnly = true;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.MinimumWidth = 6;
            this.colEstado.Name = "colEstado";
            this.colEstado.ReadOnly = true;
            // 
            // colHoraSalida
            // 
            this.colHoraSalida.HeaderText = "Hora salida";
            this.colHoraSalida.MinimumWidth = 6;
            this.colHoraSalida.Name = "colHoraSalida";
            this.colHoraSalida.ReadOnly = true;
            // 
            // colHoraLlegada
            // 
            this.colHoraLlegada.HeaderText = "Hora llegada";
            this.colHoraLlegada.MinimumWidth = 6;
            this.colHoraLlegada.Name = "colHoraLlegada";
            this.colHoraLlegada.ReadOnly = true;
            // 
            // dtpFechaConsulta
            // 
            this.dtpFechaConsulta.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaConsulta.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaConsulta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaConsulta.Location = new System.Drawing.Point(813, 332);
            this.dtpFechaConsulta.Margin = new System.Windows.Forms.Padding(2);
            this.dtpFechaConsulta.Name = "dtpFechaConsulta";
            this.dtpFechaConsulta.Size = new System.Drawing.Size(151, 26);
            this.dtpFechaConsulta.TabIndex = 27;
            // 
            // txtBuscarViaje
            // 
            this.txtBuscarViaje.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscarViaje.Location = new System.Drawing.Point(788, 384);
            this.txtBuscarViaje.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscarViaje.Name = "txtBuscarViaje";
            this.txtBuscarViaje.Size = new System.Drawing.Size(218, 26);
            this.txtBuscarViaje.TabIndex = 28;
            // 
            // LogisticaBtnGestionViajes
            // 
            this.LogisticaBtnGestionViajes.Location = new System.Drawing.Point(670, 103);
            this.LogisticaBtnGestionViajes.Name = "LogisticaBtnGestionViajes";
            this.LogisticaBtnGestionViajes.Size = new System.Drawing.Size(75, 39);
            this.LogisticaBtnGestionViajes.TabIndex = 29;
            this.LogisticaBtnGestionViajes.Text = "Gestion de Viajes";
            this.LogisticaBtnGestionViajes.UseVisualStyleBackColor = true;
            this.LogisticaBtnGestionViajes.Click += new System.EventHandler(this.LogisticaBtnGestionViajes_Click);
            // 
            // LogisticaBtnControlTransporte
            // 
            this.LogisticaBtnControlTransporte.Location = new System.Drawing.Point(776, 103);
            this.LogisticaBtnControlTransporte.Name = "LogisticaBtnControlTransporte";
            this.LogisticaBtnControlTransporte.Size = new System.Drawing.Size(75, 39);
            this.LogisticaBtnControlTransporte.TabIndex = 30;
            this.LogisticaBtnControlTransporte.Text = "Control de Transporte";
            this.LogisticaBtnControlTransporte.UseVisualStyleBackColor = true;
            this.LogisticaBtnControlTransporte.Click += new System.EventHandler(this.LogisticaBtnControlTransporte_Click);
            // 
            // LogisticaBtnConsultasTransporte
            // 
            this.LogisticaBtnConsultasTransporte.Location = new System.Drawing.Point(879, 103);
            this.LogisticaBtnConsultasTransporte.Name = "LogisticaBtnConsultasTransporte";
            this.LogisticaBtnConsultasTransporte.Size = new System.Drawing.Size(97, 39);
            this.LogisticaBtnConsultasTransporte.TabIndex = 31;
            this.LogisticaBtnConsultasTransporte.Text = "Consultas de Transporte";
            this.LogisticaBtnConsultasTransporte.UseVisualStyleBackColor = true;
            this.LogisticaBtnConsultasTransporte.Click += new System.EventHandler(this.LogisticaBtnConsultasTransporte_Click);
            // 
            // FrmTransporteInterno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CapaVista_TransporteInterno_Logistica.Properties.Resources.FondoLogistica;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1028, 609);
            this.Controls.Add(this.LogisticaBtnConsultasTransporte);
            this.Controls.Add(this.LogisticaBtnControlTransporte);
            this.Controls.Add(this.LogisticaBtnGestionViajes);
            this.Controls.Add(this.txtBuscarViaje);
            this.Controls.Add(this.dtpFechaConsulta);
            this.Controls.Add(this.dgvUltimosViajes);
            this.Controls.Add(this.lblUltimosViajes);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LogisticaLblTituloTransporteInterno);
            this.Controls.Add(this.LogisticaNavTransporteInterno);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmTransporteInterno";
            this.Text = "5013-Gestion Transporte interno ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUltimosViajes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label LogisticaLblTituloTransporteInterno;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private CapaVista_Navegador.Navegador LogisticaNavTransporteInterno;
        private System.Windows.Forms.Label lblUltimosViajes;
        private System.Windows.Forms.DataGridView dgvUltimosViajes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCodigoViaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipoTraslado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrigen;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDestino;
        private System.Windows.Forms.DataGridViewTextBoxColumn colVehiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colConductor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoraSalida;
        private System.Windows.Forms.DataGridViewTextBoxColumn colHoraLlegada;
        private System.Windows.Forms.DateTimePicker dtpFechaConsulta;
        private System.Windows.Forms.TextBox txtBuscarViaje;
        private System.Windows.Forms.Button LogisticaBtnGestionViajes;
        private System.Windows.Forms.Button LogisticaBtnControlTransporte;
        private System.Windows.Forms.Button LogisticaBtnConsultasTransporte;
    }
}