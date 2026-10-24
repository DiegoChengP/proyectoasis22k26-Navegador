namespace CapaVista_Inventario_Logistica.Formas
{
    partial class FrmMantenimientoMarca
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantenimientoMarca));
            this.LogisticaPnlMarca = new System.Windows.Forms.Panel();
            this.LogisticaNavMarca = new CapaVista_Navegador.Navegador();
            this.LogisticaLblDescripcionMarca = new System.Windows.Forms.Label();
            this.LogisticaLblTituloMarca = new System.Windows.Forms.Label();
            this.LogisticaPnlMarca.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogisticaPnlMarca
            // 
            this.LogisticaPnlMarca.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.LogisticaPnlMarca.BackgroundImage = global::CapaVista_Inventario_Logistica.Properties.Resources.FondoLogica2;
            this.LogisticaPnlMarca.Controls.Add(this.LogisticaNavMarca);
            this.LogisticaPnlMarca.Controls.Add(this.LogisticaLblDescripcionMarca);
            this.LogisticaPnlMarca.Controls.Add(this.LogisticaLblTituloMarca);
            this.LogisticaPnlMarca.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogisticaPnlMarca.Location = new System.Drawing.Point(0, 0);
            this.LogisticaPnlMarca.Name = "LogisticaPnlMarca";
            this.LogisticaPnlMarca.Size = new System.Drawing.Size(1144, 741);
            this.LogisticaPnlMarca.TabIndex = 0;
            // 
            // LogisticaNavMarca
            // 
            this.LogisticaNavMarca.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaNavMarca.Location = new System.Drawing.Point(28, 125);
            this.LogisticaNavMarca.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LogisticaNavMarca.Name = "LogisticaNavMarca";
            this.LogisticaNavMarca.Size = new System.Drawing.Size(1078, 90);
            this.LogisticaNavMarca.TabIndex = 2;
            // 
            // LogisticaLblDescripcionMarca
            // 
            this.LogisticaLblDescripcionMarca.AutoSize = true;
            this.LogisticaLblDescripcionMarca.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblDescripcionMarca.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.LogisticaLblDescripcionMarca.ForeColor = System.Drawing.Color.Black;
            this.LogisticaLblDescripcionMarca.Location = new System.Drawing.Point(36, 67);
            this.LogisticaLblDescripcionMarca.Name = "LogisticaLblDescripcionMarca";
            this.LogisticaLblDescripcionMarca.Size = new System.Drawing.Size(469, 42);
            this.LogisticaLblDescripcionMarca.TabIndex = 1;
            this.LogisticaLblDescripcionMarca.Text = "Gestiona las marcas comerciales y establece sus porcentajes \r\nde comisión.";
            // 
            // LogisticaLblTituloMarca
            // 
            this.LogisticaLblTituloMarca.AutoSize = true;
            this.LogisticaLblTituloMarca.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblTituloMarca.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.LogisticaLblTituloMarca.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaLblTituloMarca.Location = new System.Drawing.Point(34, 26);
            this.LogisticaLblTituloMarca.Name = "LogisticaLblTituloMarca";
            this.LogisticaLblTituloMarca.Size = new System.Drawing.Size(99, 32);
            this.LogisticaLblTituloMarca.TabIndex = 0;
            this.LogisticaLblTituloMarca.Text = "Marca";
            // 
            // FrmMantenimientoMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1144, 741);
            this.Controls.Add(this.LogisticaPnlMarca);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMantenimientoMarca";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "5005 - Marca";
            this.LogisticaPnlMarca.ResumeLayout(false);
            this.LogisticaPnlMarca.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LogisticaPnlMarca;
        private CapaVista_Navegador.Navegador LogisticaNavMarca;
        private System.Windows.Forms.Label LogisticaLblDescripcionMarca;
        private System.Windows.Forms.Label LogisticaLblTituloMarca;
    }
}