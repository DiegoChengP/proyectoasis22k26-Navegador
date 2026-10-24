namespace CapaVista_TransporteInterno_Logistica.Formas
{
    partial class FrmControlTransporte
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmControlTransporte));
            this.LogisticaNavTransporteInterno = new CapaVista_Navegador.Navegador();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblDescripcionControl = new System.Windows.Forms.Label();
            this.LogisticaLblTituloTransporteInterno = new System.Windows.Forms.Label();
            this.tabControlTransporte = new System.Windows.Forms.TabControl();
            this.tabVerificacionSanitaria = new System.Windows.Forms.TabPage();
            this.tabMonitoreoViaje = new System.Windows.Forms.TabPage();
            this.tabIncidencias = new System.Windows.Forms.TabPage();
            this.grpInspeccionVehiculo = new System.Windows.Forms.GroupBox();
            this.cboVehiculoInspeccion = new System.Windows.Forms.ComboBox();
            this.txtPlacaInspeccion = new System.Windows.Forms.TextBox();
            this.txtTipoVehiculoInspeccion = new System.Windows.Forms.TextBox();
            this.txtCapacidadInspeccion = new System.Windows.Forms.TextBox();
            this.txtUltimaLimpieza = new System.Windows.Forms.TextBox();
            this.grpChecklistSanitario = new System.Windows.Forms.GroupBox();
            this.chkLimpiezaExterior = new System.Windows.Forms.CheckBox();
            this.chkLimpiezaInterior = new System.Windows.Forms.CheckBox();
            this.chkEquipoFrio = new System.Windows.Forms.CheckBox();
            this.chkCompartimentoCarga = new System.Windows.Forms.CheckBox();
            this.chkSinContaminacion = new System.Windows.Forms.CheckBox();
            this.chkTemperaturaAdecuada = new System.Windows.Forms.CheckBox();
            this.grpAprobacionSanitaria = new System.Windows.Forms.GroupBox();
            this.rdbAprobado = new System.Windows.Forms.RadioButton();
            this.rdbNoAprobado = new System.Windows.Forms.RadioButton();
            this.dtpFechaInspeccion = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraInspeccion = new System.Windows.Forms.DateTimePicker();
            this.cboResponsableInspeccion = new System.Windows.Forms.ComboBox();
            this.txtObservacionesInspeccion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControlTransporte.SuspendLayout();
            this.tabVerificacionSanitaria.SuspendLayout();
            this.grpInspeccionVehiculo.SuspendLayout();
            this.grpChecklistSanitario.SuspendLayout();
            this.grpAprobacionSanitaria.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogisticaNavTransporteInterno
            // 
            this.LogisticaNavTransporteInterno.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaNavTransporteInterno.Location = new System.Drawing.Point(-12, -12);
            this.LogisticaNavTransporteInterno.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogisticaNavTransporteInterno.Name = "LogisticaNavTransporteInterno";
            this.LogisticaNavTransporteInterno.Size = new System.Drawing.Size(1437, 111);
            this.LogisticaNavTransporteInterno.TabIndex = 10;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::CapaVista_TransporteInterno_Logistica.Properties.Resources._7;
            this.pictureBox1.Location = new System.Drawing.Point(1091, 85);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(283, 322);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // lblDescripcionControl
            // 
            this.lblDescripcionControl.AutoSize = true;
            this.lblDescripcionControl.BackColor = System.Drawing.Color.Transparent;
            this.lblDescripcionControl.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.lblDescripcionControl.Location = new System.Drawing.Point(7, 141);
            this.lblDescripcionControl.Name = "lblDescripcionControl";
            this.lblDescripcionControl.Size = new System.Drawing.Size(942, 27);
            this.lblDescripcionControl.TabIndex = 8;
            this.lblDescripcionControl.Text = "Verifica la condición del vehiculo, registra la salida, monitorea el viaje y docu" +
    "menta incidencias.";
            // 
            // LogisticaLblTituloTransporteInterno
            // 
            this.LogisticaLblTituloTransporteInterno.AutoSize = true;
            this.LogisticaLblTituloTransporteInterno.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblTituloTransporteInterno.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.LogisticaLblTituloTransporteInterno.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaLblTituloTransporteInterno.Location = new System.Drawing.Point(9, 101);
            this.LogisticaLblTituloTransporteInterno.Name = "LogisticaLblTituloTransporteInterno";
            this.LogisticaLblTituloTransporteInterno.Size = new System.Drawing.Size(681, 40);
            this.LogisticaLblTituloTransporteInterno.TabIndex = 7;
            this.LogisticaLblTituloTransporteInterno.Text = "Control y Seguimiento de Transporte";
            // 
            // tabControlTransporte
            // 
            this.tabControlTransporte.Controls.Add(this.tabVerificacionSanitaria);
            this.tabControlTransporte.Controls.Add(this.tabMonitoreoViaje);
            this.tabControlTransporte.Controls.Add(this.tabIncidencias);
            this.tabControlTransporte.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControlTransporte.ItemSize = new System.Drawing.Size(175, 35);
            this.tabControlTransporte.Location = new System.Drawing.Point(12, 171);
            this.tabControlTransporte.Name = "tabControlTransporte";
            this.tabControlTransporte.SelectedIndex = 0;
            this.tabControlTransporte.Size = new System.Drawing.Size(900, 578);
            this.tabControlTransporte.TabIndex = 11;
            // 
            // tabVerificacionSanitaria
            // 
            this.tabVerificacionSanitaria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabVerificacionSanitaria.Controls.Add(this.grpAprobacionSanitaria);
            this.tabVerificacionSanitaria.Controls.Add(this.grpChecklistSanitario);
            this.tabVerificacionSanitaria.Controls.Add(this.grpInspeccionVehiculo);
            this.tabVerificacionSanitaria.Location = new System.Drawing.Point(4, 39);
            this.tabVerificacionSanitaria.Name = "tabVerificacionSanitaria";
            this.tabVerificacionSanitaria.Padding = new System.Windows.Forms.Padding(3);
            this.tabVerificacionSanitaria.Size = new System.Drawing.Size(892, 535);
            this.tabVerificacionSanitaria.TabIndex = 0;
            this.tabVerificacionSanitaria.Text = "Verificación sanitaria";
            // 
            // tabMonitoreoViaje
            // 
            this.tabMonitoreoViaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabMonitoreoViaje.Location = new System.Drawing.Point(4, 39);
            this.tabMonitoreoViaje.Name = "tabMonitoreoViaje";
            this.tabMonitoreoViaje.Padding = new System.Windows.Forms.Padding(3);
            this.tabMonitoreoViaje.Size = new System.Drawing.Size(892, 477);
            this.tabMonitoreoViaje.TabIndex = 1;
            this.tabMonitoreoViaje.Text = "Monitoreo de viaje";
            // 
            // tabIncidencias
            // 
            this.tabIncidencias.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.tabIncidencias.Location = new System.Drawing.Point(4, 39);
            this.tabIncidencias.Name = "tabIncidencias";
            this.tabIncidencias.Padding = new System.Windows.Forms.Padding(3);
            this.tabIncidencias.Size = new System.Drawing.Size(892, 477);
            this.tabIncidencias.TabIndex = 2;
            this.tabIncidencias.Text = "Incidencias";
            // 
            // grpInspeccionVehiculo
            // 
            this.grpInspeccionVehiculo.Controls.Add(this.txtUltimaLimpieza);
            this.grpInspeccionVehiculo.Controls.Add(this.txtCapacidadInspeccion);
            this.grpInspeccionVehiculo.Controls.Add(this.txtTipoVehiculoInspeccion);
            this.grpInspeccionVehiculo.Controls.Add(this.txtPlacaInspeccion);
            this.grpInspeccionVehiculo.Controls.Add(this.cboVehiculoInspeccion);
            this.grpInspeccionVehiculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.grpInspeccionVehiculo.Location = new System.Drawing.Point(15, 15);
            this.grpInspeccionVehiculo.Name = "grpInspeccionVehiculo";
            this.grpInspeccionVehiculo.Size = new System.Drawing.Size(410, 245);
            this.grpInspeccionVehiculo.TabIndex = 0;
            this.grpInspeccionVehiculo.TabStop = false;
            this.grpInspeccionVehiculo.Text = "1. Inspección del vehículo";
            // 
            // cboVehiculoInspeccion
            // 
            this.cboVehiculoInspeccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVehiculoInspeccion.FormattingEnabled = true;
            this.cboVehiculoInspeccion.Location = new System.Drawing.Point(6, 29);
            this.cboVehiculoInspeccion.Name = "cboVehiculoInspeccion";
            this.cboVehiculoInspeccion.Size = new System.Drawing.Size(372, 31);
            this.cboVehiculoInspeccion.TabIndex = 0;
            // 
            // txtPlacaInspeccion
            // 
            this.txtPlacaInspeccion.Location = new System.Drawing.Point(6, 77);
            this.txtPlacaInspeccion.Name = "txtPlacaInspeccion";
            this.txtPlacaInspeccion.Size = new System.Drawing.Size(372, 30);
            this.txtPlacaInspeccion.TabIndex = 1;
            // 
            // txtTipoVehiculoInspeccion
            // 
            this.txtTipoVehiculoInspeccion.Location = new System.Drawing.Point(6, 113);
            this.txtTipoVehiculoInspeccion.Name = "txtTipoVehiculoInspeccion";
            this.txtTipoVehiculoInspeccion.Size = new System.Drawing.Size(372, 30);
            this.txtTipoVehiculoInspeccion.TabIndex = 2;
            // 
            // txtCapacidadInspeccion
            // 
            this.txtCapacidadInspeccion.Location = new System.Drawing.Point(6, 152);
            this.txtCapacidadInspeccion.Name = "txtCapacidadInspeccion";
            this.txtCapacidadInspeccion.Size = new System.Drawing.Size(372, 30);
            this.txtCapacidadInspeccion.TabIndex = 3;
            // 
            // txtUltimaLimpieza
            // 
            this.txtUltimaLimpieza.Location = new System.Drawing.Point(6, 199);
            this.txtUltimaLimpieza.Name = "txtUltimaLimpieza";
            this.txtUltimaLimpieza.Size = new System.Drawing.Size(372, 30);
            this.txtUltimaLimpieza.TabIndex = 4;
            // 
            // grpChecklistSanitario
            // 
            this.grpChecklistSanitario.Controls.Add(this.chkSinContaminacion);
            this.grpChecklistSanitario.Controls.Add(this.chkTemperaturaAdecuada);
            this.grpChecklistSanitario.Controls.Add(this.chkEquipoFrio);
            this.grpChecklistSanitario.Controls.Add(this.chkCompartimentoCarga);
            this.grpChecklistSanitario.Controls.Add(this.chkLimpiezaInterior);
            this.grpChecklistSanitario.Controls.Add(this.chkLimpiezaExterior);
            this.grpChecklistSanitario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.grpChecklistSanitario.Location = new System.Drawing.Point(15, 266);
            this.grpChecklistSanitario.Name = "grpChecklistSanitario";
            this.grpChecklistSanitario.Size = new System.Drawing.Size(410, 239);
            this.grpChecklistSanitario.TabIndex = 1;
            this.grpChecklistSanitario.TabStop = false;
            this.grpChecklistSanitario.Text = "2. Checklist sanitario";
            // 
            // chkLimpiezaExterior
            // 
            this.chkLimpiezaExterior.AutoSize = true;
            this.chkLimpiezaExterior.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLimpiezaExterior.Location = new System.Drawing.Point(22, 43);
            this.chkLimpiezaExterior.Name = "chkLimpiezaExterior";
            this.chkLimpiezaExterior.Size = new System.Drawing.Size(146, 24);
            this.chkLimpiezaExterior.TabIndex = 0;
            this.chkLimpiezaExterior.Text = "Limpieza exterior";
            this.chkLimpiezaExterior.UseVisualStyleBackColor = true;
            // 
            // chkLimpiezaInterior
            // 
            this.chkLimpiezaInterior.AutoSize = true;
            this.chkLimpiezaInterior.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLimpiezaInterior.Location = new System.Drawing.Point(22, 76);
            this.chkLimpiezaInterior.Name = "chkLimpiezaInterior";
            this.chkLimpiezaInterior.Size = new System.Drawing.Size(143, 24);
            this.chkLimpiezaInterior.TabIndex = 1;
            this.chkLimpiezaInterior.Text = "Limpieza interior";
            this.chkLimpiezaInterior.UseVisualStyleBackColor = true;
            // 
            // chkEquipoFrio
            // 
            this.chkEquipoFrio.AutoSize = true;
            this.chkEquipoFrio.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEquipoFrio.Location = new System.Drawing.Point(22, 142);
            this.chkEquipoFrio.Name = "chkEquipoFrio";
            this.chkEquipoFrio.Size = new System.Drawing.Size(261, 24);
            this.chkEquipoFrio.TabIndex = 3;
            this.chkEquipoFrio.Text = "Funcionamiento del equipo de frío";
            this.chkEquipoFrio.UseVisualStyleBackColor = true;
            // 
            // chkCompartimentoCarga
            // 
            this.chkCompartimentoCarga.AutoSize = true;
            this.chkCompartimentoCarga.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCompartimentoCarga.Location = new System.Drawing.Point(22, 109);
            this.chkCompartimentoCarga.Name = "chkCompartimentoCarga";
            this.chkCompartimentoCarga.Size = new System.Drawing.Size(304, 24);
            this.chkCompartimentoCarga.TabIndex = 2;
            this.chkCompartimentoCarga.Text = "Compartimento de carga en buen estado";
            this.chkCompartimentoCarga.UseVisualStyleBackColor = true;
            // 
            // chkSinContaminacion
            // 
            this.chkSinContaminacion.AutoSize = true;
            this.chkSinContaminacion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSinContaminacion.Location = new System.Drawing.Point(22, 208);
            this.chkSinContaminacion.Name = "chkSinContaminacion";
            this.chkSinContaminacion.Size = new System.Drawing.Size(153, 24);
            this.chkSinContaminacion.TabIndex = 5;
            this.chkSinContaminacion.Text = "Sin contaminación";
            this.chkSinContaminacion.UseVisualStyleBackColor = true;
            // 
            // chkTemperaturaAdecuada
            // 
            this.chkTemperaturaAdecuada.AutoSize = true;
            this.chkTemperaturaAdecuada.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTemperaturaAdecuada.Location = new System.Drawing.Point(22, 175);
            this.chkTemperaturaAdecuada.Name = "chkTemperaturaAdecuada";
            this.chkTemperaturaAdecuada.Size = new System.Drawing.Size(184, 24);
            this.chkTemperaturaAdecuada.TabIndex = 4;
            this.chkTemperaturaAdecuada.Text = "Temperatura adecuada";
            this.chkTemperaturaAdecuada.UseVisualStyleBackColor = true;
            // 
            // grpAprobacionSanitaria
            // 
            this.grpAprobacionSanitaria.Controls.Add(this.txtObservacionesInspeccion);
            this.grpAprobacionSanitaria.Controls.Add(this.cboResponsableInspeccion);
            this.grpAprobacionSanitaria.Controls.Add(this.dtpHoraInspeccion);
            this.grpAprobacionSanitaria.Controls.Add(this.dtpFechaInspeccion);
            this.grpAprobacionSanitaria.Controls.Add(this.rdbNoAprobado);
            this.grpAprobacionSanitaria.Controls.Add(this.rdbAprobado);
            this.grpAprobacionSanitaria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(44)))), ((int)(((byte)(18)))));
            this.grpAprobacionSanitaria.Location = new System.Drawing.Point(440, 15);
            this.grpAprobacionSanitaria.Name = "grpAprobacionSanitaria";
            this.grpAprobacionSanitaria.Size = new System.Drawing.Size(410, 490);
            this.grpAprobacionSanitaria.TabIndex = 2;
            this.grpAprobacionSanitaria.TabStop = false;
            this.grpAprobacionSanitaria.Text = "3. Registro de aprobación";
            // 
            // rdbAprobado
            // 
            this.rdbAprobado.AutoSize = true;
            this.rdbAprobado.Location = new System.Drawing.Point(28, 47);
            this.rdbAprobado.Name = "rdbAprobado";
            this.rdbAprobado.Size = new System.Drawing.Size(112, 27);
            this.rdbAprobado.TabIndex = 0;
            this.rdbAprobado.TabStop = true;
            this.rdbAprobado.Text = "Aprobado";
            this.rdbAprobado.UseVisualStyleBackColor = true;
            // 
            // rdbNoAprobado
            // 
            this.rdbNoAprobado.AutoSize = true;
            this.rdbNoAprobado.Location = new System.Drawing.Point(27, 80);
            this.rdbNoAprobado.Name = "rdbNoAprobado";
            this.rdbNoAprobado.Size = new System.Drawing.Size(137, 27);
            this.rdbNoAprobado.TabIndex = 1;
            this.rdbNoAprobado.TabStop = true;
            this.rdbNoAprobado.Text = "No aprobado";
            this.rdbNoAprobado.UseVisualStyleBackColor = true;
            // 
            // dtpFechaInspeccion
            // 
            this.dtpFechaInspeccion.CustomFormat = "dd/MM/yyyy";
            this.dtpFechaInspeccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaInspeccion.Location = new System.Drawing.Point(16, 139);
            this.dtpFechaInspeccion.Name = "dtpFechaInspeccion";
            this.dtpFechaInspeccion.Size = new System.Drawing.Size(305, 30);
            this.dtpFechaInspeccion.TabIndex = 2;
            // 
            // dtpHoraInspeccion
            // 
            this.dtpHoraInspeccion.CustomFormat = "HH:mm";
            this.dtpHoraInspeccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraInspeccion.Location = new System.Drawing.Point(16, 192);
            this.dtpHoraInspeccion.Name = "dtpHoraInspeccion";
            this.dtpHoraInspeccion.ShowUpDown = true;
            this.dtpHoraInspeccion.Size = new System.Drawing.Size(305, 30);
            this.dtpHoraInspeccion.TabIndex = 3;
            // 
            // cboResponsableInspeccion
            // 
            this.cboResponsableInspeccion.FormattingEnabled = true;
            this.cboResponsableInspeccion.Location = new System.Drawing.Point(16, 284);
            this.cboResponsableInspeccion.Name = "cboResponsableInspeccion";
            this.cboResponsableInspeccion.Size = new System.Drawing.Size(305, 31);
            this.cboResponsableInspeccion.TabIndex = 4;
            // 
            // txtObservacionesInspeccion
            // 
            this.txtObservacionesInspeccion.Location = new System.Drawing.Point(16, 372);
            this.txtObservacionesInspeccion.Multiline = true;
            this.txtObservacionesInspeccion.Name = "txtObservacionesInspeccion";
            this.txtObservacionesInspeccion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacionesInspeccion.Size = new System.Drawing.Size(305, 90);
            this.txtObservacionesInspeccion.TabIndex = 5;
            this.txtObservacionesInspeccion.TextChanged += new System.EventHandler(this.txtObservacionesInspeccion_TextChanged);
            // 
            // FrmControlTransporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::CapaVista_TransporteInterno_Logistica.Properties.Resources.FondoLogistica;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1371, 750);
            this.Controls.Add(this.tabControlTransporte);
            this.Controls.Add(this.LogisticaNavTransporteInterno);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblDescripcionControl);
            this.Controls.Add(this.LogisticaLblTituloTransporteInterno);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FrmControlTransporte";
            this.Text = "5015-Control de Transporte ";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControlTransporte.ResumeLayout(false);
            this.tabVerificacionSanitaria.ResumeLayout(false);
            this.grpInspeccionVehiculo.ResumeLayout(false);
            this.grpInspeccionVehiculo.PerformLayout();
            this.grpChecklistSanitario.ResumeLayout(false);
            this.grpChecklistSanitario.PerformLayout();
            this.grpAprobacionSanitaria.ResumeLayout(false);
            this.grpAprobacionSanitaria.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CapaVista_Navegador.Navegador LogisticaNavTransporteInterno;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDescripcionControl;
        private System.Windows.Forms.Label LogisticaLblTituloTransporteInterno;
        private System.Windows.Forms.TabControl tabControlTransporte;
        private System.Windows.Forms.TabPage tabVerificacionSanitaria;
        private System.Windows.Forms.TabPage tabMonitoreoViaje;
        private System.Windows.Forms.TabPage tabIncidencias;
        private System.Windows.Forms.GroupBox grpInspeccionVehiculo;
        private System.Windows.Forms.TextBox txtUltimaLimpieza;
        private System.Windows.Forms.TextBox txtCapacidadInspeccion;
        private System.Windows.Forms.TextBox txtTipoVehiculoInspeccion;
        private System.Windows.Forms.TextBox txtPlacaInspeccion;
        private System.Windows.Forms.ComboBox cboVehiculoInspeccion;
        private System.Windows.Forms.GroupBox grpChecklistSanitario;
        private System.Windows.Forms.CheckBox chkSinContaminacion;
        private System.Windows.Forms.CheckBox chkTemperaturaAdecuada;
        private System.Windows.Forms.CheckBox chkEquipoFrio;
        private System.Windows.Forms.CheckBox chkCompartimentoCarga;
        private System.Windows.Forms.CheckBox chkLimpiezaInterior;
        private System.Windows.Forms.CheckBox chkLimpiezaExterior;
        private System.Windows.Forms.GroupBox grpAprobacionSanitaria;
        private System.Windows.Forms.TextBox txtObservacionesInspeccion;
        private System.Windows.Forms.ComboBox cboResponsableInspeccion;
        private System.Windows.Forms.DateTimePicker dtpHoraInspeccion;
        private System.Windows.Forms.DateTimePicker dtpFechaInspeccion;
        private System.Windows.Forms.RadioButton rdbNoAprobado;
        private System.Windows.Forms.RadioButton rdbAprobado;
    }
}