namespace CapaVista_Inventario_Logistica.Formas
{
    partial class FrmMantenimientoCategoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantenimientoCategoria));
            this.LogisticaPnlCategoria = new System.Windows.Forms.Panel();
            this.LogisticaNavCategoria = new CapaVista_Navegador.Navegador();
            this.LogisticaLblDescripcionCategoria = new System.Windows.Forms.Label();
            this.LogisticaLblTituloCategoria = new System.Windows.Forms.Label();
            this.LogisticaPnlCategoria.SuspendLayout();
            this.SuspendLayout();
            // 
            // LogisticaPnlCategoria
            // 
            this.LogisticaPnlCategoria.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.LogisticaPnlCategoria.BackgroundImage = global::CapaVista_Inventario_Logistica.Properties.Resources.FondoLogica2;
            this.LogisticaPnlCategoria.Controls.Add(this.LogisticaNavCategoria);
            this.LogisticaPnlCategoria.Controls.Add(this.LogisticaLblDescripcionCategoria);
            this.LogisticaPnlCategoria.Controls.Add(this.LogisticaLblTituloCategoria);
            this.LogisticaPnlCategoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogisticaPnlCategoria.Location = new System.Drawing.Point(0, 0);
            this.LogisticaPnlCategoria.Name = "LogisticaPnlCategoria";
            this.LogisticaPnlCategoria.Size = new System.Drawing.Size(1144, 741);
            this.LogisticaPnlCategoria.TabIndex = 0;
            // 
            // LogisticaNavCategoria
            // 
            this.LogisticaNavCategoria.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaNavCategoria.Location = new System.Drawing.Point(35, 108);
            this.LogisticaNavCategoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.LogisticaNavCategoria.Name = "LogisticaNavCategoria";
            this.LogisticaNavCategoria.Size = new System.Drawing.Size(1078, 90);
            this.LogisticaNavCategoria.TabIndex = 2;
            // 
            // LogisticaLblDescripcionCategoria
            // 
            this.LogisticaLblDescripcionCategoria.AutoSize = true;
            this.LogisticaLblDescripcionCategoria.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblDescripcionCategoria.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.LogisticaLblDescripcionCategoria.ForeColor = System.Drawing.Color.Black;
            this.LogisticaLblDescripcionCategoria.Location = new System.Drawing.Point(31, 69);
            this.LogisticaLblDescripcionCategoria.Name = "LogisticaLblDescripcionCategoria";
            this.LogisticaLblDescripcionCategoria.Size = new System.Drawing.Size(590, 21);
            this.LogisticaLblDescripcionCategoria.TabIndex = 1;
            this.LogisticaLblDescripcionCategoria.Text = "Gestiona las categorías de productos y configura sus porcentajes de comisión.";
            // 
            // LogisticaLblTituloCategoria
            // 
            this.LogisticaLblTituloCategoria.AutoSize = true;
            this.LogisticaLblTituloCategoria.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblTituloCategoria.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.LogisticaLblTituloCategoria.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaLblTituloCategoria.Location = new System.Drawing.Point(29, 19);
            this.LogisticaLblTituloCategoria.Name = "LogisticaLblTituloCategoria";
            this.LogisticaLblTituloCategoria.Size = new System.Drawing.Size(167, 32);
            this.LogisticaLblTituloCategoria.TabIndex = 0;
            this.LogisticaLblTituloCategoria.Text = "Categorias";
            // 
            // FrmMantenimientoCategoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 741);
            this.Controls.Add(this.LogisticaPnlCategoria);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMantenimientoCategoria";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "5003 - Categoria de Producto";
            this.LogisticaPnlCategoria.ResumeLayout(false);
            this.LogisticaPnlCategoria.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LogisticaPnlCategoria;
        private System.Windows.Forms.Label LogisticaLblDescripcionCategoria;
        private System.Windows.Forms.Label LogisticaLblTituloCategoria;
        private CapaVista_Navegador.Navegador LogisticaNavCategoria;
    }
}