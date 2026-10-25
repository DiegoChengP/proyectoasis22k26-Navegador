namespace CapaVista_AprobacionFinanciera
{
    partial class FrmEvaluarOrden
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer _Componentes = null;
        private CapaVista_Componentes.Componentes.ClsPanelVerticalCompras ComprasPnlDesplazamiento;
        private System.Windows.Forms.Panel ComprasPnlContenido;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool LiberarRecursos)
        {
            if (LiberarRecursos && (_Componentes != null))
            {
                _Componentes.Dispose();
            }
            base.Dispose(LiberarRecursos);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEvaluarOrden));
            this.ComprasPnlDesplazamiento = new CapaVista_Componentes.Componentes.ClsPanelVerticalCompras();
            this.ComprasPnlContenido = new System.Windows.Forms.Panel();
            this.ComprasBtnRegresar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnRechazar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnAprobar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasLblEvaluacion = new CapaVista_Componentes.Componentes.ClsEtiquetaCompras();
            this.ComprasUsrDetalleOrden = new CapaVista_Componentes.Componentes.UsrDataGridViewCompras();
            this.ComprasPnlDesplazamiento.SuspendLayout();
            this.ComprasPnlContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComprasPnlDesplazamiento
            // 
            this.ComprasPnlDesplazamiento.AutoScroll = true;
            this.ComprasPnlDesplazamiento.Controls.Add(this.ComprasPnlContenido);
            this.ComprasPnlDesplazamiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasPnlDesplazamiento.Location = new System.Drawing.Point(0, 0);
            this.ComprasPnlDesplazamiento.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlDesplazamiento.Name = "ComprasPnlDesplazamiento";
            this.ComprasPnlDesplazamiento.Size = new System.Drawing.Size(1300, 700);
            this.ComprasPnlDesplazamiento.TabIndex = 0;
            // 
            // ComprasPnlContenido
            // 
            this.ComprasPnlContenido.Controls.Add(this.ComprasBtnRegresar);
            this.ComprasPnlContenido.Controls.Add(this.ComprasBtnRechazar);
            this.ComprasPnlContenido.Controls.Add(this.ComprasBtnAprobar);
            this.ComprasPnlContenido.Controls.Add(this.ComprasLblEvaluacion);
            this.ComprasPnlContenido.Controls.Add(this.ComprasUsrDetalleOrden);
            this.ComprasPnlContenido.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComprasPnlContenido.Location = new System.Drawing.Point(0, 0);
            this.ComprasPnlContenido.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlContenido.Name = "ComprasPnlContenido";
            this.ComprasPnlContenido.Size = new System.Drawing.Size(1279, 723);
            this.ComprasPnlContenido.TabIndex = 0;
            // 
            // ComprasBtnRegresar
            // 
            this.ComprasBtnRegresar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnRegresar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnRegresar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnRegresar.BackgroundImage")));
            this.ComprasBtnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnRegresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnRegresar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnRegresar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnRegresar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnRegresar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnRegresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnRegresar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnRegresar.Location = new System.Drawing.Point(731, 540);
            this.ComprasBtnRegresar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnRegresar.Name = "ComprasBtnRegresar";
            this.ComprasBtnRegresar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnRegresar.TabIndex = 9;
            this.ComprasBtnRegresar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnRechazar
            // 
            this.ComprasBtnRechazar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnRechazar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnRechazar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnRechazar.BackgroundImage")));
            this.ComprasBtnRechazar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnRechazar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnRechazar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnRechazar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnRechazar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnRechazar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnRechazar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnRechazar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnRechazar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnRechazar.Location = new System.Drawing.Point(595, 540);
            this.ComprasBtnRechazar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnRechazar.Name = "ComprasBtnRechazar";
            this.ComprasBtnRechazar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnRechazar.TabIndex = 8;
            this.ComprasBtnRechazar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnAprobar
            // 
            this.ComprasBtnAprobar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnAprobar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnAprobar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnAprobar.BackgroundImage")));
            this.ComprasBtnAprobar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnAprobar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnAprobar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnAprobar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnAprobar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnAprobar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnAprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnAprobar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnAprobar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnAprobar.Location = new System.Drawing.Point(449, 540);
            this.ComprasBtnAprobar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnAprobar.Name = "ComprasBtnAprobar";
            this.ComprasBtnAprobar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnAprobar.TabIndex = 7;
            this.ComprasBtnAprobar.UseVisualStyleBackColor = false;
            // 
            // ComprasLblEvaluacion
            // 
            this.ComprasLblEvaluacion.AutoSize = true;
            this.ComprasLblEvaluacion.Font = new System.Drawing.Font("Lucida Fax", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasLblEvaluacion.Location = new System.Drawing.Point(36, 20);
            this.ComprasLblEvaluacion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ComprasLblEvaluacion.Name = "ComprasLblEvaluacion";
            this.ComprasLblEvaluacion.Size = new System.Drawing.Size(355, 34);
            this.ComprasLblEvaluacion.TabIndex = 6;
            this.ComprasLblEvaluacion.Text = "6009 – Evaluar orden";
            // 
            // ComprasUsrDetalleOrden
            // 
            this.ComprasUsrDetalleOrden.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrDetalleOrden.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrDetalleOrden.Location = new System.Drawing.Point(32, 79);
            this.ComprasUsrDetalleOrden.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrDetalleOrden.Name = "ComprasUsrDetalleOrden";
            this.ComprasUsrDetalleOrden.Size = new System.Drawing.Size(1234, 413);
            this.ComprasUsrDetalleOrden.TabIndex = 5;
            // 
            // FrmEvaluarOrden
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.ComprasPnlDesplazamiento);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmEvaluarOrden";
            this.Text = "6009 – EvaluarOrden";
            this.ComprasPnlDesplazamiento.ResumeLayout(false);
            this.ComprasPnlContenido.ResumeLayout(false);
            this.ComprasPnlContenido.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnRegresar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnRechazar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnAprobar;
        private CapaVista_Componentes.Componentes.ClsEtiquetaCompras ComprasLblEvaluacion;
        private CapaVista_Componentes.Componentes.UsrDataGridViewCompras ComprasUsrDetalleOrden;
    }
}
