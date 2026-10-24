namespace CapaVista_Inventario_Logistica.Formas
{
    partial class FrmMantenimientoTipoMovimiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMantenimientoTipoMovimiento));
            this.panel1 = new System.Windows.Forms.Panel();
            this.navegador1 = new CapaVista_Navegador.Navegador();
            this.LogisticaLblDescripcionTipoMovimiento = new System.Windows.Forms.Label();
            this.LogisiticaLblTituloTipoMovimiento = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::CapaVista_Inventario_Logistica.Properties.Resources.FondoLogica2;
            this.panel1.Controls.Add(this.navegador1);
            this.panel1.Controls.Add(this.LogisticaLblDescripcionTipoMovimiento);
            this.panel1.Controls.Add(this.LogisiticaLblTituloTipoMovimiento);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1144, 741);
            this.panel1.TabIndex = 0;
            // 
            // navegador1
            // 
            this.navegador1.BackColor = System.Drawing.Color.Transparent;
            this.navegador1.Location = new System.Drawing.Point(30, 136);
            this.navegador1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.navegador1.Name = "navegador1";
            this.navegador1.Size = new System.Drawing.Size(1078, 90);
            this.navegador1.TabIndex = 2;
            // 
            // LogisticaLblDescripcionTipoMovimiento
            // 
            this.LogisticaLblDescripcionTipoMovimiento.AutoSize = true;
            this.LogisticaLblDescripcionTipoMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.LogisticaLblDescripcionTipoMovimiento.Font = new System.Drawing.Font("Tahoma", 12.75F);
            this.LogisticaLblDescripcionTipoMovimiento.ForeColor = System.Drawing.Color.Black;
            this.LogisticaLblDescripcionTipoMovimiento.Location = new System.Drawing.Point(43, 92);
            this.LogisticaLblDescripcionTipoMovimiento.Name = "LogisticaLblDescripcionTipoMovimiento";
            this.LogisticaLblDescripcionTipoMovimiento.Size = new System.Drawing.Size(430, 42);
            this.LogisticaLblDescripcionTipoMovimiento.TabIndex = 1;
            this.LogisticaLblDescripcionTipoMovimiento.Text = "Configura los tipos de movimiento de inventario y define \r\nsu efecto de entrada o" +
    " salida.";
            // 
            // LogisiticaLblTituloTipoMovimiento
            // 
            this.LogisiticaLblTituloTipoMovimiento.AutoSize = true;
            this.LogisiticaLblTituloTipoMovimiento.BackColor = System.Drawing.Color.Transparent;
            this.LogisiticaLblTituloTipoMovimiento.Font = new System.Drawing.Font("Lucida Fax", 20.25F, System.Drawing.FontStyle.Bold);
            this.LogisiticaLblTituloTipoMovimiento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisiticaLblTituloTipoMovimiento.Location = new System.Drawing.Point(41, 42);
            this.LogisiticaLblTituloTipoMovimiento.Name = "LogisiticaLblTituloTipoMovimiento";
            this.LogisiticaLblTituloTipoMovimiento.Size = new System.Drawing.Size(306, 32);
            this.LogisiticaLblTituloTipoMovimiento.TabIndex = 0;
            this.LogisiticaLblTituloTipoMovimiento.Text = "Tipo de Movimiento";
            // 
            // FrmMantenimientoTipoMovimiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1144, 741);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmMantenimientoTipoMovimiento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "5008 - Tipo de Movimiento";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CapaVista_Navegador.Navegador navegador1;
        private System.Windows.Forms.Label LogisticaLblDescripcionTipoMovimiento;
        private System.Windows.Forms.Label LogisiticaLblTituloTipoMovimiento;
    }
}