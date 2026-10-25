
namespace CapaVista_Componentes.Componentes
{
    partial class UsrComboBoxCompras
    {
        private System.ComponentModel.IContainer _Componentes = null;

        protected override void Dispose(bool LiberarRecursos)
        {
            if (LiberarRecursos && (_Componentes != null))
            {
                _Componentes.Dispose();
            }
            base.Dispose(LiberarRecursos);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            this.ComprasTlpMain = new CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras();
            this.ComprasPnlBorde = new System.Windows.Forms.Panel();
            this.ComprasCboEntrada = new CapaVista_Componentes.Componentes.ClsSelectorCompras();
            this.ComprasUsrError = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasTlpMain.SuspendLayout();
            this.ComprasPnlBorde.SuspendLayout();
            this.SuspendLayout();
            //
            // ComprasTlpMain
            //
            this.ComprasTlpMain.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasTlpMain.ColumnCount = 1;
            this.ComprasTlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpMain.Controls.Add(this.ComprasPnlBorde, 0, 0);
            this.ComprasTlpMain.Controls.Add(this.ComprasUsrError, 0, 1);
            this.ComprasTlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasTlpMain.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasTlpMain.Location = new System.Drawing.Point(0, 0);
            this.ComprasTlpMain.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasTlpMain.Name = "ComprasTlpMain";
            this.ComprasTlpMain.Padding = new System.Windows.Forms.Padding(3);
            this.ComprasTlpMain.RowCount = 2;
            this.ComprasTlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.ComprasTlpMain.Size = new System.Drawing.Size(174, 66);
            this.ComprasTlpMain.TabIndex = 0;
            //
            // ComprasPnlBorde
            //
            this.ComprasPnlBorde.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasPnlBorde.Controls.Add(this.ComprasCboEntrada);
            this.ComprasPnlBorde.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasPnlBorde.Location = new System.Drawing.Point(3, 3);
            this.ComprasPnlBorde.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlBorde.Name = "ComprasPnlBorde";
            this.ComprasPnlBorde.Padding = new System.Windows.Forms.Padding(1);
            this.ComprasPnlBorde.Size = new System.Drawing.Size(168, 30);
            this.ComprasPnlBorde.TabIndex = 2;
            //
            // ComprasCboEntrada
            //
            this.ComprasCboEntrada.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasCboEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasCboEntrada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComprasCboEntrada.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasCboEntrada.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasCboEntrada.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasCboEntrada.Location = new System.Drawing.Point(1, 1);
            this.ComprasCboEntrada.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasCboEntrada.Name = "ComprasCboEntrada";
            this.ComprasCboEntrada.Size = new System.Drawing.Size(166, 31);
            this.ComprasCboEntrada.TabIndex = 0;
            //
            // ComprasUsrError
            //
            this.ComprasUsrError.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrError.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrError.ColorTexto = System.Drawing.Color.Red;
            this.ComprasUsrError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrError.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrError.ForeColor = System.Drawing.Color.Red;
            this.ComprasUsrError.Location = new System.Drawing.Point(6, 37);
            this.ComprasUsrError.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ComprasUsrError.MaximumSize = new System.Drawing.Size(1000, 22);
            this.ComprasUsrError.MostrarAsterisco = false;
            this.ComprasUsrError.Name = "ComprasUsrError";
            this.ComprasUsrError.Size = new System.Drawing.Size(162, 22);
            this.ComprasUsrError.TabIndex = 0;
            this.ComprasUsrError.TabStop = false;
            this.ComprasUsrError.Text = "";
            this.ComprasUsrError.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrError.Texto = "";
            //
            // UsrComboBoxCompras
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.Controls.Add(this.ComprasTlpMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UsrComboBoxCompras";
            this.Size = new System.Drawing.Size(174, 66);
            this.ComprasTlpMain.ResumeLayout(false);
            this.ComprasPnlBorde.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras ComprasTlpMain;
        private System.Windows.Forms.Panel ComprasPnlBorde;
        private UsrLabelCompras ComprasUsrError;
        private CapaVista_Componentes.Componentes.ClsSelectorCompras ComprasCboEntrada;
    }
}
