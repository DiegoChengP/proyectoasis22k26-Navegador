namespace CapaVista_TransporteExterno_Logistica.Formas
{
    partial class ListadoTransporteExterno
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoTransporteExterno));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LogisticaDvglListadoTransportes = new System.Windows.Forms.DataGridView();
            this.LogisticaPnlListadoTransportes = new System.Windows.Forms.Panel();
            this.LogisticaLblTituloListadoTransportes = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogisticaDvglListadoTransportes)).BeginInit();
            this.LogisticaPnlListadoTransportes.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LogisticaDvglListadoTransportes);
            this.groupBox2.Controls.Add(this.LogisticaPnlListadoTransportes);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(1081, 613);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            // 
            // LogisticaDvglListadoTransportes
            // 
            this.LogisticaDvglListadoTransportes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LogisticaDvglListadoTransportes.Location = new System.Drawing.Point(7, 51);
            this.LogisticaDvglListadoTransportes.Name = "LogisticaDvglListadoTransportes";
            this.LogisticaDvglListadoTransportes.RowHeadersWidth = 51;
            this.LogisticaDvglListadoTransportes.RowTemplate.Height = 24;
            this.LogisticaDvglListadoTransportes.Size = new System.Drawing.Size(1068, 557);
            this.LogisticaDvglListadoTransportes.TabIndex = 19;
            // 
            // LogisticaPnlListadoTransportes
            // 
            this.LogisticaPnlListadoTransportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(138)))), ((int)(((byte)(132)))));
            this.LogisticaPnlListadoTransportes.Controls.Add(this.LogisticaLblTituloListadoTransportes);
            this.LogisticaPnlListadoTransportes.Location = new System.Drawing.Point(7, 10);
            this.LogisticaPnlListadoTransportes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LogisticaPnlListadoTransportes.Name = "LogisticaPnlListadoTransportes";
            this.LogisticaPnlListadoTransportes.Size = new System.Drawing.Size(1068, 36);
            this.LogisticaPnlListadoTransportes.TabIndex = 18;
            // 
            // LogisticaLblTituloListadoTransportes
            // 
            this.LogisticaLblTituloListadoTransportes.AutoSize = true;
            this.LogisticaLblTituloListadoTransportes.Font = new System.Drawing.Font("Tahoma", 13.2F);
            this.LogisticaLblTituloListadoTransportes.ForeColor = System.Drawing.Color.White;
            this.LogisticaLblTituloListadoTransportes.Location = new System.Drawing.Point(63, 4);
            this.LogisticaLblTituloListadoTransportes.Name = "LogisticaLblTituloListadoTransportes";
            this.LogisticaLblTituloListadoTransportes.Size = new System.Drawing.Size(320, 27);
            this.LogisticaLblTituloListadoTransportes.TabIndex = 9;
            this.LogisticaLblTituloListadoTransportes.Text = "Listado de transportes externos";
            // 
            // ListadoTransporteExterno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1105, 635);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ListadoTransporteExterno";
            this.Text = "5025 ListadoTransporteExterno";
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogisticaDvglListadoTransportes)).EndInit();
            this.LogisticaPnlListadoTransportes.ResumeLayout(false);
            this.LogisticaPnlListadoTransportes.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView LogisticaDvglListadoTransportes;
        private System.Windows.Forms.Panel LogisticaPnlListadoTransportes;
        private System.Windows.Forms.Label LogisticaLblTituloListadoTransportes;
    }
}