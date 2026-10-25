namespace CapaVista_Inventario_Logistica.Formas
{
    partial class FrmMantenimientoBodegas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantenimientoBodegas));
            this.LogisticaPnlBodegas = new System.Windows.Forms.Panel();
            this.LogisticaNavBodega = new CapaVista_Navegador.Navegador();
            this.LogisticaLblDescripcionBodega = new System.Windows.Forms.Label();
            this.LogisticaLblTituloBodega = new System.Windows.Forms.Label();
            this.LogisticaPnlBodegas.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogisticaPnlBodegas
            // 
            this.LogisticaPnlBodegas.BackgroundImage = global::CapaVista_Inventario_Logistica.Properties.Resources.FondoLogica2;
            this.LogisticaPnlBodegas.Controls.Add(this.LogisticaNavBodega);
            this.LogisticaPnlBodegas.Controls.Add(this.LogisticaLblDescripcionBodega);
            this.LogisticaPnlBodegas.Controls.Add(this.LogisticaLblTituloBodega);
            this.LogisticaPnlBodegas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogisticaPnlBodegas.Location = new System.Drawing.Point(0, 0);
            this.LogisticaPnlBodegas.Name = "LogisticaPnlBodegas";
            this.LogisticaPnlBodegas.Size = new System.Drawing.Size(1144, 741);
            this.LogisticaPnlBodegas.TabIndex = 0;
            // 
            // LogisticaNavBodega
            // 
            this.LogisticaNavBodega.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaNavBodega.Location = new System.Drawing.Point(20, 94);
            this.LogisticaNavBodega.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LogisticaNavBodega.Name = "LogisticaNavBodega";
            this.LogisticaNavBodega.Size = new System.Drawing.Size(1078, 90);
            this.LogisticaNavBodega.TabIndex = 2;
            // 
            // LogisticaLblDescripcionBodega
            // 
            this.LogisticaLblDescripcionBodega.AutoSize = true;
            this.LogisticaLblDescripcionBodega.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblDescripcionBodega.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.LogisticaLblDescripcionBodega.ForeColor = System.Drawing.Color.Black;
            this.LogisticaLblDescripcionBodega.Location = new System.Drawing.Point(40, 57);
            this.LogisticaLblDescripcionBodega.Name = "LogisticaLblDescripcionBodega";
            this.LogisticaLblDescripcionBodega.Size = new System.Drawing.Size(530, 21);
            this.LogisticaLblDescripcionBodega.TabIndex = 1;
            this.LogisticaLblDescripcionBodega.Text = "Administra las bodegas y asígnalas a las sucursales correspondientes.";
            // 
            // LogisticaLblTituloBodega
            // 
            this.LogisticaLblTituloBodega.AutoSize = true;
            this.LogisticaLblTituloBodega.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblTituloBodega.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LogisticaLblTituloBodega.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaLblTituloBodega.Location = new System.Drawing.Point(37, 13);
            this.LogisticaLblTituloBodega.Name = "LogisticaLblTituloBodega";
            this.LogisticaLblTituloBodega.Size = new System.Drawing.Size(132, 32);
            this.LogisticaLblTituloBodega.TabIndex = 0;
            this.LogisticaLblTituloBodega.Text = "Bodegas";
            // 
            // FrmMantenimientoBodegas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1144, 741);
            this.Controls.Add(this.LogisticaPnlBodegas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMantenimientoBodegas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "5002 - Bodegas";
            this.LogisticaPnlBodegas.ResumeLayout(false);
            this.LogisticaPnlBodegas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LogisticaPnlBodegas;
        private System.Windows.Forms.Label LogisticaLblDescripcionBodega;
        private System.Windows.Forms.Label LogisticaLblTituloBodega;
        private CapaVista_Navegador.Navegador LogisticaNavBodega;
    }
}