/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Diseño de la ventana FrmMantenimientoPais: solo contiene el control Navegador.
 * ===================================================================
*/

namespace CapaVista_Contabilidad
{
    partial class FrmMantenimientoPais
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool Disposing)
        {
            if (Disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(Disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.ContabilidadUsrNavegador = new CapaVista_Navegador.Navegador();
            this.SuspendLayout();
            // 
            // ContabilidadUsrNavegador
            // 
            this.ContabilidadUsrNavegador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContabilidadUsrNavegador.Location = new System.Drawing.Point(11, 11);
            this.ContabilidadUsrNavegador.Margin = new System.Windows.Forms.Padding(2);
            this.ContabilidadUsrNavegador.Name = "ContabilidadUsrNavegador";
            this.ContabilidadUsrNavegador.Size = new System.Drawing.Size(1158, 90);
            this.ContabilidadUsrNavegador.TabIndex = 0;
            // 
            // FrmMantenimientoPais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 688);
            this.Controls.Add(this.ContabilidadUsrNavegador);
            this.Name = "FrmMantenimientoPais";
            this.Text = "10011 - MantenimientoPais";
            this.ResumeLayout(false);

        }

        #endregion

        // Control Navegador del componente Navegador.
        private CapaVista_Navegador.Navegador ContabilidadUsrNavegador;
    }
}
