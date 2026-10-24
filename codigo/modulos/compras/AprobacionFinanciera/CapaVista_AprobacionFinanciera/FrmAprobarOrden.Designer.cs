namespace CapaVista_AprobacionFinanciera
{
    partial class FrmAprobarOrden
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer _Componentes = null;
        private CapaVista_Componentes.Componentes.ClsPanelVerticalCompras ComprasPnlDesplazamiento;
        private System.Windows.Forms.Panel ComprasPnlContenido;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool LiberarRecursos)
        {
            if (LiberarRecursos && (_Componentes != null))
            {
                _Componentes.Dispose();
            }
            base.Dispose(LiberarRecursos);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAprobarOrden));
            this.ComprasPnlDesplazamiento = new CapaVista_Componentes.Componentes.ClsPanelVerticalCompras();
            this.ComprasPnlContenido = new System.Windows.Forms.Panel();
            this.ComprasBtnNoAprobar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnSiAprobar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasUsrComentario = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrEtiquetaComentario = new CapaVista_Componentes.Componentes.UsrLabelCompras();
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
            this.ComprasPnlContenido.Controls.Add(this.ComprasBtnNoAprobar);
            this.ComprasPnlContenido.Controls.Add(this.ComprasBtnSiAprobar);
            this.ComprasPnlContenido.Controls.Add(this.ComprasUsrComentario);
            this.ComprasPnlContenido.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComprasPnlContenido.Location = new System.Drawing.Point(0, 0);
            this.ComprasPnlContenido.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlContenido.Name = "ComprasPnlContenido";
            this.ComprasPnlContenido.Size = new System.Drawing.Size(1279, 723);
            this.ComprasPnlContenido.TabIndex = 0;
            // 
            // ComprasBtnNoAprobar
            // 
            this.ComprasBtnNoAprobar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnNoAprobar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnNoAprobar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnNoAprobar.BackgroundImage")));
            this.ComprasBtnNoAprobar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnNoAprobar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnNoAprobar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnNoAprobar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnNoAprobar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnNoAprobar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnNoAprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnNoAprobar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnNoAprobar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnNoAprobar.Location = new System.Drawing.Point(619, 544);
            this.ComprasBtnNoAprobar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnNoAprobar.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnNoAprobar.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnNoAprobar.Name = "ComprasBtnNoAprobar";
            this.ComprasBtnNoAprobar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnNoAprobar.TabIndex = 10;
            this.ComprasBtnNoAprobar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnSiAprobar
            // 
            this.ComprasBtnSiAprobar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnSiAprobar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnSiAprobar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnSiAprobar.BackgroundImage")));
            this.ComprasBtnSiAprobar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnSiAprobar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnSiAprobar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnSiAprobar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnSiAprobar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnSiAprobar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnSiAprobar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnSiAprobar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnSiAprobar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnSiAprobar.Location = new System.Drawing.Point(412, 544);
            this.ComprasBtnSiAprobar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnSiAprobar.Name = "ComprasBtnSiAprobar";
            this.ComprasBtnSiAprobar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnSiAprobar.TabIndex = 9;
            this.ComprasBtnSiAprobar.UseVisualStyleBackColor = false;
            // 
            // ComprasUsrComentario
            // 
            this.ComprasUsrComentario.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrComentario.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrComentario.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrComentario.Location = new System.Drawing.Point(31, 21);
            this.ComprasUsrComentario.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrComentario.MaxLength = 2147483647;
            this.ComprasUsrComentario.Name = "ComprasUsrComentario";
            this.ComprasUsrComentario.SelectedText = "";
            this.ComprasUsrComentario.SelectionLength = 0;
            this.ComprasUsrComentario.SelectionStart = 0;
            this.ComprasUsrComentario.Size = new System.Drawing.Size(1196, 495);
            this.ComprasUsrComentario.TabIndex = 8;
            // 
            // ComprasUsrEtiquetaComentario
            // 
            this.ComprasUsrEtiquetaComentario.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrEtiquetaComentario.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrEtiquetaComentario.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrEtiquetaComentario.Location = new System.Drawing.Point(13, 14);
            this.ComprasUsrEtiquetaComentario.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ComprasUsrEtiquetaComentario.MaximumSize = new System.Drawing.Size(1167, 21);
            this.ComprasUsrEtiquetaComentario.Name = "ComprasUsrEtiquetaComentario";
            this.ComprasUsrEtiquetaComentario.Size = new System.Drawing.Size(116, 21);
            this.ComprasUsrEtiquetaComentario.TabIndex = 7;
            this.ComprasUsrEtiquetaComentario.TabStop = false;
            this.ComprasUsrEtiquetaComentario.TamanoTipografia = 9F;
            this.ComprasUsrEtiquetaComentario.Text = "Comentario";
            this.ComprasUsrEtiquetaComentario.Texto = "Comentario";
            // 
            // FrmAprobarOrden
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.ComprasPnlDesplazamiento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmAprobarOrden";
            this.Text = "6007 – AprobarOrden";
            this.ComprasPnlDesplazamiento.ResumeLayout(false);
            this.ComprasPnlContenido.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnNoAprobar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnSiAprobar;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrComentario;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrEtiquetaComentario;
    }
}
