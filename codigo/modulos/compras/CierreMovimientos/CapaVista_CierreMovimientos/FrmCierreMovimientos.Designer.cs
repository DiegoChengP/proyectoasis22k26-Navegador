namespace CapaVista_CierreMovimientos
{
    partial class FrmCierreMovimientos
    {
        private System.ComponentModel.IContainer _Componentes = null;
        private CapaVista_Componentes.Componentes.ClsPanelVerticalCompras ComprasPnlDesplazamiento;
        private System.Windows.Forms.Panel ComprasPnlContenido;

        protected override void Dispose(bool LiberarRecursos)
        {
            if (LiberarRecursos && (_Componentes != null))
            {
                _Componentes.Dispose();
            }
            base.Dispose(LiberarRecursos);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCierreMovimientos));
            this.ComprasPnlDesplazamiento = new CapaVista_Componentes.Componentes.ClsPanelVerticalCompras();
            this.ComprasPnlContenido = new System.Windows.Forms.Panel();
            this.ComprasTlpPrincipal = new CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras();
            this.ComprasUsrTitulo = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasGbxPeriodo = new CapaVista_Componentes.Componentes.ClsGroupBoxCompras();
            this.ComprasUsrPeriodo = new CapaVista_Componentes.Componentes.UsrComboBoxCompras();
            this.ComprasUsrEtiquetaPeriodo = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasTlpContenido = new CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras();
            this.ComprasGbxValidaciones = new CapaVista_Componentes.Componentes.ClsGroupBoxCompras();
            this.ComprasUsrValidaciones = new CapaVista_Componentes.Componentes.UsrDataGridViewCompras();
            this.ComprasGbxSaldos = new CapaVista_Componentes.Componentes.ClsGroupBoxCompras();
            this.ComprasUsrSaldos = new CapaVista_Componentes.Componentes.UsrDataGridViewCompras();
            this.ComprasTlpTotales = new CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras();
            this.ComprasUsrTotalCargos = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrEtiquetaTotalAbonos = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrTotalAbonos = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrEtiquetaSaldoFinal = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrSaldoFinal = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrEtiquetaTotalCargos = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasTlpBotones = new CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras();
            this.ComprasBtnEjecutarCierre = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnValidar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnImprimir = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnAyuda = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnSalir = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasPnlDesplazamiento.SuspendLayout();
            this.ComprasPnlContenido.SuspendLayout();
            this.ComprasTlpPrincipal.SuspendLayout();
            this.ComprasGbxPeriodo.SuspendLayout();
            this.ComprasTlpContenido.SuspendLayout();
            this.ComprasGbxValidaciones.SuspendLayout();
            this.ComprasGbxSaldos.SuspendLayout();
            this.ComprasTlpTotales.SuspendLayout();
            this.ComprasTlpBotones.SuspendLayout();
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
            this.ComprasPnlContenido.Controls.Add(this.ComprasTlpPrincipal);
            this.ComprasPnlContenido.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComprasPnlContenido.Location = new System.Drawing.Point(0, 0);
            this.ComprasPnlContenido.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlContenido.Name = "ComprasPnlContenido";
            this.ComprasPnlContenido.Size = new System.Drawing.Size(1279, 723);
            this.ComprasPnlContenido.TabIndex = 0;
            // 
            // ComprasTlpPrincipal
            // 
            this.ComprasTlpPrincipal.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasTlpPrincipal.ColumnCount = 1;
            this.ComprasTlpPrincipal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpPrincipal.Controls.Add(this.ComprasUsrTitulo, 0, 0);
            this.ComprasTlpPrincipal.Controls.Add(this.ComprasGbxPeriodo, 0, 1);
            this.ComprasTlpPrincipal.Controls.Add(this.ComprasTlpContenido, 0, 2);
            this.ComprasTlpPrincipal.Controls.Add(this.ComprasTlpTotales, 0, 3);
            this.ComprasTlpPrincipal.Controls.Add(this.ComprasTlpBotones, 0, 4);
            this.ComprasTlpPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasTlpPrincipal.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasTlpPrincipal.Location = new System.Drawing.Point(0, 0);
            this.ComprasTlpPrincipal.Name = "ComprasTlpPrincipal";
            this.ComprasTlpPrincipal.Padding = new System.Windows.Forms.Padding(16);
            this.ComprasTlpPrincipal.RowCount = 5;
            this.ComprasTlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.ComprasTlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.ComprasTlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.ComprasTlpPrincipal.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.ComprasTlpPrincipal.Size = new System.Drawing.Size(1279, 723);
            this.ComprasTlpPrincipal.TabIndex = 0;
            // 
            // ComprasUsrTitulo
            // 
            this.ComprasUsrTitulo.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrTitulo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrTitulo.Font = new System.Drawing.Font("Tahoma", 16.2F);
            this.ComprasUsrTitulo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrTitulo.Location = new System.Drawing.Point(21, 22);
            this.ComprasUsrTitulo.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.ComprasUsrTitulo.MaximumSize = new System.Drawing.Size(1875, 40);
            this.ComprasUsrTitulo.MostrarAsterisco = false;
            this.ComprasUsrTitulo.Name = "ComprasUsrTitulo";
            this.ComprasUsrTitulo.Size = new System.Drawing.Size(390, 38);
            this.ComprasUsrTitulo.TabIndex = 0;
            this.ComprasUsrTitulo.TabStop = false;
            this.ComprasUsrTitulo.TamanoTipografia = 16.2F;
            this.ComprasUsrTitulo.Text = "Cierre de Movimientos";
            this.ComprasUsrTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrTitulo.Texto = "Cierre de Movimientos";
            // 
            // ComprasGbxPeriodo
            // 
            this.ComprasGbxPeriodo.BackColor = System.Drawing.Color.Transparent;
            this.ComprasGbxPeriodo.Controls.Add(this.ComprasUsrPeriodo);
            this.ComprasGbxPeriodo.Controls.Add(this.ComprasUsrEtiquetaPeriodo);
            this.ComprasGbxPeriodo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasGbxPeriodo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasGbxPeriodo.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ComprasGbxPeriodo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasGbxPeriodo.Location = new System.Drawing.Point(19, 69);
            this.ComprasGbxPeriodo.Name = "ComprasGbxPeriodo";
            this.ComprasGbxPeriodo.Size = new System.Drawing.Size(1241, 74);
            this.ComprasGbxPeriodo.TabIndex = 1;
            this.ComprasGbxPeriodo.TabStop = false;
            this.ComprasGbxPeriodo.Text = "Período a cerrar";
            // 
            // ComprasUsrPeriodo
            // 
            this.ComprasUsrPeriodo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrPeriodo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrPeriodo.Location = new System.Drawing.Point(214, 24);
            this.ComprasUsrPeriodo.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrPeriodo.MaxLength = 0;
            this.ComprasUsrPeriodo.MostrarEtiquetaError = false;
            this.ComprasUsrPeriodo.Name = "ComprasUsrPeriodo";
            this.ComprasUsrPeriodo.Size = new System.Drawing.Size(174, 36);
            this.ComprasUsrPeriodo.TabIndex = 1;
            // 
            // ComprasUsrEtiquetaPeriodo
            // 
            this.ComprasUsrEtiquetaPeriodo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrEtiquetaPeriodo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrEtiquetaPeriodo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrEtiquetaPeriodo.Location = new System.Drawing.Point(3, 27);
            this.ComprasUsrEtiquetaPeriodo.MaximumSize = new System.Drawing.Size(1000, 22);
            this.ComprasUsrEtiquetaPeriodo.Name = "ComprasUsrEtiquetaPeriodo";
            this.ComprasUsrEtiquetaPeriodo.Size = new System.Drawing.Size(208, 22);
            this.ComprasUsrEtiquetaPeriodo.TabIndex = 0;
            this.ComprasUsrEtiquetaPeriodo.TabStop = false;
            this.ComprasUsrEtiquetaPeriodo.TamanoTipografia = 9F;
            this.ComprasUsrEtiquetaPeriodo.Text = "Período";
            this.ComprasUsrEtiquetaPeriodo.Texto = "Período";
            // 
            // ComprasTlpContenido
            // 
            this.ComprasTlpContenido.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasTlpContenido.ColumnCount = 2;
            this.ComprasTlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.ComprasTlpContenido.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.ComprasTlpContenido.Controls.Add(this.ComprasGbxValidaciones, 0, 0);
            this.ComprasTlpContenido.Controls.Add(this.ComprasGbxSaldos, 1, 0);
            this.ComprasTlpContenido.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasTlpContenido.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasTlpContenido.Location = new System.Drawing.Point(19, 149);
            this.ComprasTlpContenido.Name = "ComprasTlpContenido";
            this.ComprasTlpContenido.RowCount = 1;
            this.ComprasTlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpContenido.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 375F));
            this.ComprasTlpContenido.Size = new System.Drawing.Size(1241, 375);
            this.ComprasTlpContenido.TabIndex = 2;
            // 
            // ComprasGbxValidaciones
            // 
            this.ComprasGbxValidaciones.BackColor = System.Drawing.Color.Transparent;
            this.ComprasGbxValidaciones.Controls.Add(this.ComprasUsrValidaciones);
            this.ComprasGbxValidaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasGbxValidaciones.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasGbxValidaciones.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ComprasGbxValidaciones.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasGbxValidaciones.Location = new System.Drawing.Point(3, 3);
            this.ComprasGbxValidaciones.Name = "ComprasGbxValidaciones";
            this.ComprasGbxValidaciones.Size = new System.Drawing.Size(490, 369);
            this.ComprasGbxValidaciones.TabIndex = 0;
            this.ComprasGbxValidaciones.TabStop = false;
            this.ComprasGbxValidaciones.Text = "Validaciones previas";
            // 
            // ComprasUsrValidaciones
            // 
            this.ComprasUsrValidaciones.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrValidaciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrValidaciones.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrValidaciones.Location = new System.Drawing.Point(3, 24);
            this.ComprasUsrValidaciones.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrValidaciones.Name = "ComprasUsrValidaciones";
            this.ComprasUsrValidaciones.Size = new System.Drawing.Size(484, 342);
            this.ComprasUsrValidaciones.TabIndex = 0;
            // 
            // ComprasGbxSaldos
            // 
            this.ComprasGbxSaldos.BackColor = System.Drawing.Color.Transparent;
            this.ComprasGbxSaldos.Controls.Add(this.ComprasUsrSaldos);
            this.ComprasGbxSaldos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasGbxSaldos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasGbxSaldos.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ComprasGbxSaldos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasGbxSaldos.Location = new System.Drawing.Point(499, 3);
            this.ComprasGbxSaldos.Name = "ComprasGbxSaldos";
            this.ComprasGbxSaldos.Size = new System.Drawing.Size(739, 369);
            this.ComprasGbxSaldos.TabIndex = 1;
            this.ComprasGbxSaldos.TabStop = false;
            this.ComprasGbxSaldos.Text = "Resumen por proveedor";
            // 
            // ComprasUsrSaldos
            // 
            this.ComprasUsrSaldos.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrSaldos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrSaldos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrSaldos.Location = new System.Drawing.Point(3, 24);
            this.ComprasUsrSaldos.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrSaldos.Name = "ComprasUsrSaldos";
            this.ComprasUsrSaldos.Size = new System.Drawing.Size(733, 342);
            this.ComprasUsrSaldos.TabIndex = 0;
            // 
            // ComprasTlpTotales
            // 
            this.ComprasTlpTotales.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasTlpTotales.ColumnCount = 3;
            this.ComprasTlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ComprasTlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ComprasTlpTotales.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrTotalCargos, 0, 1);
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrEtiquetaTotalAbonos, 1, 0);
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrTotalAbonos, 1, 1);
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrEtiquetaSaldoFinal, 2, 0);
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrSaldoFinal, 2, 1);
            this.ComprasTlpTotales.Controls.Add(this.ComprasUsrEtiquetaTotalCargos, 0, 0);
            this.ComprasTlpTotales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasTlpTotales.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasTlpTotales.Location = new System.Drawing.Point(19, 530);
            this.ComprasTlpTotales.Name = "ComprasTlpTotales";
            this.ComprasTlpTotales.RowCount = 2;
            this.ComprasTlpTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.ComprasTlpTotales.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpTotales.Size = new System.Drawing.Size(1241, 74);
            this.ComprasTlpTotales.TabIndex = 3;
            // 
            // ComprasUsrTotalCargos
            // 
            this.ComprasUsrTotalCargos.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrTotalCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrTotalCargos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrTotalCargos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrTotalCargos.Location = new System.Drawing.Point(0, 26);
            this.ComprasUsrTotalCargos.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrTotalCargos.MaxLength = 2147483647;
            this.ComprasUsrTotalCargos.MostrarEtiquetaError = false;
            this.ComprasUsrTotalCargos.Name = "ComprasUsrTotalCargos";
            this.ComprasUsrTotalCargos.ReadOnly = true;
            this.ComprasUsrTotalCargos.SelectedText = "";
            this.ComprasUsrTotalCargos.SelectionLength = 0;
            this.ComprasUsrTotalCargos.SelectionStart = 0;
            this.ComprasUsrTotalCargos.Size = new System.Drawing.Size(413, 48);
            this.ComprasUsrTotalCargos.TabIndex = 1;
            // 
            // ComprasUsrEtiquetaTotalAbonos
            // 
            this.ComprasUsrEtiquetaTotalAbonos.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaTotalAbonos.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrEtiquetaTotalAbonos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrEtiquetaTotalAbonos.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrEtiquetaTotalAbonos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrEtiquetaTotalAbonos.Location = new System.Drawing.Point(416, 3);
            this.ComprasUsrEtiquetaTotalAbonos.MaximumSize = new System.Drawing.Size(1000, 22);
            this.ComprasUsrEtiquetaTotalAbonos.MostrarAsterisco = false;
            this.ComprasUsrEtiquetaTotalAbonos.Name = "ComprasUsrEtiquetaTotalAbonos";
            this.ComprasUsrEtiquetaTotalAbonos.Size = new System.Drawing.Size(407, 20);
            this.ComprasUsrEtiquetaTotalAbonos.TabIndex = 2;
            this.ComprasUsrEtiquetaTotalAbonos.TabStop = false;
            this.ComprasUsrEtiquetaTotalAbonos.TamanoTipografia = 9F;
            this.ComprasUsrEtiquetaTotalAbonos.Text = "Total Abonos (Q):";
            this.ComprasUsrEtiquetaTotalAbonos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaTotalAbonos.Texto = "Total Abonos (Q):";
            // 
            // ComprasUsrTotalAbonos
            // 
            this.ComprasUsrTotalAbonos.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrTotalAbonos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrTotalAbonos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrTotalAbonos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrTotalAbonos.Location = new System.Drawing.Point(413, 26);
            this.ComprasUsrTotalAbonos.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrTotalAbonos.MaxLength = 2147483647;
            this.ComprasUsrTotalAbonos.MostrarEtiquetaError = false;
            this.ComprasUsrTotalAbonos.Name = "ComprasUsrTotalAbonos";
            this.ComprasUsrTotalAbonos.ReadOnly = true;
            this.ComprasUsrTotalAbonos.SelectedText = "";
            this.ComprasUsrTotalAbonos.SelectionLength = 0;
            this.ComprasUsrTotalAbonos.SelectionStart = 0;
            this.ComprasUsrTotalAbonos.Size = new System.Drawing.Size(413, 48);
            this.ComprasUsrTotalAbonos.TabIndex = 3;
            // 
            // ComprasUsrEtiquetaSaldoFinal
            // 
            this.ComprasUsrEtiquetaSaldoFinal.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaSaldoFinal.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrEtiquetaSaldoFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrEtiquetaSaldoFinal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrEtiquetaSaldoFinal.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrEtiquetaSaldoFinal.Location = new System.Drawing.Point(829, 3);
            this.ComprasUsrEtiquetaSaldoFinal.MaximumSize = new System.Drawing.Size(1000, 22);
            this.ComprasUsrEtiquetaSaldoFinal.MostrarAsterisco = false;
            this.ComprasUsrEtiquetaSaldoFinal.Name = "ComprasUsrEtiquetaSaldoFinal";
            this.ComprasUsrEtiquetaSaldoFinal.Size = new System.Drawing.Size(409, 20);
            this.ComprasUsrEtiquetaSaldoFinal.TabIndex = 4;
            this.ComprasUsrEtiquetaSaldoFinal.TabStop = false;
            this.ComprasUsrEtiquetaSaldoFinal.TamanoTipografia = 9F;
            this.ComprasUsrEtiquetaSaldoFinal.Text = "Saldo Final CxP (Q):";
            this.ComprasUsrEtiquetaSaldoFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaSaldoFinal.Texto = "Saldo Final CxP (Q):";
            // 
            // ComprasUsrSaldoFinal
            // 
            this.ComprasUsrSaldoFinal.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrSaldoFinal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrSaldoFinal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrSaldoFinal.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrSaldoFinal.Location = new System.Drawing.Point(826, 26);
            this.ComprasUsrSaldoFinal.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrSaldoFinal.MaxLength = 2147483647;
            this.ComprasUsrSaldoFinal.MostrarEtiquetaError = false;
            this.ComprasUsrSaldoFinal.Name = "ComprasUsrSaldoFinal";
            this.ComprasUsrSaldoFinal.ReadOnly = true;
            this.ComprasUsrSaldoFinal.SelectedText = "";
            this.ComprasUsrSaldoFinal.SelectionLength = 0;
            this.ComprasUsrSaldoFinal.SelectionStart = 0;
            this.ComprasUsrSaldoFinal.Size = new System.Drawing.Size(415, 48);
            this.ComprasUsrSaldoFinal.TabIndex = 5;
            // 
            // ComprasUsrEtiquetaTotalCargos
            // 
            this.ComprasUsrEtiquetaTotalCargos.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaTotalCargos.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrEtiquetaTotalCargos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasUsrEtiquetaTotalCargos.Font = new System.Drawing.Font("Tahoma", 9F);
            this.ComprasUsrEtiquetaTotalCargos.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrEtiquetaTotalCargos.Location = new System.Drawing.Point(3, 3);
            this.ComprasUsrEtiquetaTotalCargos.MaximumSize = new System.Drawing.Size(1000, 22);
            this.ComprasUsrEtiquetaTotalCargos.MostrarAsterisco = false;
            this.ComprasUsrEtiquetaTotalCargos.Name = "ComprasUsrEtiquetaTotalCargos";
            this.ComprasUsrEtiquetaTotalCargos.Size = new System.Drawing.Size(407, 20);
            this.ComprasUsrEtiquetaTotalCargos.TabIndex = 0;
            this.ComprasUsrEtiquetaTotalCargos.TabStop = false;
            this.ComprasUsrEtiquetaTotalCargos.TamanoTipografia = 9F;
            this.ComprasUsrEtiquetaTotalCargos.Text = "Total Cargos (Q):";
            this.ComprasUsrEtiquetaTotalCargos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrEtiquetaTotalCargos.Texto = "Total Cargos (Q):";
            // 
            // ComprasTlpBotones
            // 
            this.ComprasTlpBotones.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasTlpBotones.ColumnCount = 5;
            this.ComprasTlpBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ComprasTlpBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ComprasTlpBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ComprasTlpBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ComprasTlpBotones.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.ComprasTlpBotones.Controls.Add(this.ComprasBtnEjecutarCierre, 1, 0);
            this.ComprasTlpBotones.Controls.Add(this.ComprasBtnValidar, 0, 0);
            this.ComprasTlpBotones.Controls.Add(this.ComprasBtnImprimir, 2, 0);
            this.ComprasTlpBotones.Controls.Add(this.ComprasBtnAyuda, 3, 0);
            this.ComprasTlpBotones.Controls.Add(this.ComprasBtnSalir, 4, 0);
            this.ComprasTlpBotones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComprasTlpBotones.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasTlpBotones.Location = new System.Drawing.Point(19, 610);
            this.ComprasTlpBotones.Name = "ComprasTlpBotones";
            this.ComprasTlpBotones.RowCount = 1;
            this.ComprasTlpBotones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ComprasTlpBotones.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.ComprasTlpBotones.Size = new System.Drawing.Size(1241, 94);
            this.ComprasTlpBotones.TabIndex = 4;
            // 
            // ComprasBtnEjecutarCierre
            // 
            this.ComprasBtnEjecutarCierre.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnEjecutarCierre.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnEjecutarCierre.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnEjecutarCierre.BackgroundImage")));
            this.ComprasBtnEjecutarCierre.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnEjecutarCierre.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnEjecutarCierre.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnEjecutarCierre.FlatAppearance.BorderSize = 0;
            this.ComprasBtnEjecutarCierre.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnEjecutarCierre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnEjecutarCierre.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnEjecutarCierre.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnEjecutarCierre.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnEjecutarCierre.Location = new System.Drawing.Point(332, 7);
            this.ComprasBtnEjecutarCierre.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnEjecutarCierre.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnEjecutarCierre.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnEjecutarCierre.Name = "ComprasBtnEjecutarCierre";
            this.ComprasBtnEjecutarCierre.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnEjecutarCierre.TabIndex = 1;
            this.ComprasBtnEjecutarCierre.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnValidar
            // 
            this.ComprasBtnValidar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnValidar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnValidar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnValidar.BackgroundImage")));
            this.ComprasBtnValidar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnValidar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnValidar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnValidar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnValidar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnValidar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnValidar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnValidar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnValidar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnValidar.Location = new System.Drawing.Point(84, 7);
            this.ComprasBtnValidar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnValidar.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnValidar.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnValidar.Name = "ComprasBtnValidar";
            this.ComprasBtnValidar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnValidar.TabIndex = 0;
            this.ComprasBtnValidar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnImprimir
            // 
            this.ComprasBtnImprimir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnImprimir.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnImprimir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnImprimir.BackgroundImage")));
            this.ComprasBtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnImprimir.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnImprimir.FlatAppearance.BorderSize = 0;
            this.ComprasBtnImprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnImprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnImprimir.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnImprimir.Location = new System.Drawing.Point(580, 7);
            this.ComprasBtnImprimir.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnImprimir.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnImprimir.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnImprimir.Name = "ComprasBtnImprimir";
            this.ComprasBtnImprimir.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnImprimir.TabIndex = 2;
            this.ComprasBtnImprimir.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnAyuda
            // 
            this.ComprasBtnAyuda.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnAyuda.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnAyuda.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnAyuda.BackgroundImage")));
            this.ComprasBtnAyuda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnAyuda.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnAyuda.FlatAppearance.BorderSize = 0;
            this.ComprasBtnAyuda.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnAyuda.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnAyuda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnAyuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnAyuda.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnAyuda.Location = new System.Drawing.Point(828, 7);
            this.ComprasBtnAyuda.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnAyuda.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnAyuda.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnAyuda.Name = "ComprasBtnAyuda";
            this.ComprasBtnAyuda.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnAyuda.TabIndex = 3;
            this.ComprasBtnAyuda.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnSalir
            // 
            this.ComprasBtnSalir.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnSalir.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnSalir.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnSalir.BackgroundImage")));
            this.ComprasBtnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnSalir.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnSalir.FlatAppearance.BorderSize = 0;
            this.ComprasBtnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnSalir.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnSalir.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnSalir.Location = new System.Drawing.Point(1076, 7);
            this.ComprasBtnSalir.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnSalir.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnSalir.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnSalir.Name = "ComprasBtnSalir";
            this.ComprasBtnSalir.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnSalir.TabIndex = 4;
            this.ComprasBtnSalir.UseVisualStyleBackColor = false;
            // 
            // FrmCierreMovimientos
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.ComprasPnlDesplazamiento);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximizeBox = false;
            this.Name = "FrmCierreMovimientos";
            this.Text = "6006 – CierreMovimientos";
            this.ComprasPnlDesplazamiento.ResumeLayout(false);
            this.ComprasPnlContenido.ResumeLayout(false);
            this.ComprasTlpPrincipal.ResumeLayout(false);
            this.ComprasGbxPeriodo.ResumeLayout(false);
            this.ComprasTlpContenido.ResumeLayout(false);
            this.ComprasGbxValidaciones.ResumeLayout(false);
            this.ComprasGbxSaldos.ResumeLayout(false);
            this.ComprasTlpTotales.ResumeLayout(false);
            this.ComprasTlpBotones.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras ComprasTlpPrincipal;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrTitulo;
        private CapaVista_Componentes.Componentes.ClsGroupBoxCompras ComprasGbxPeriodo;
        private CapaVista_Componentes.Componentes.UsrComboBoxCompras ComprasUsrPeriodo;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrEtiquetaPeriodo;
        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras ComprasTlpContenido;
        private CapaVista_Componentes.Componentes.ClsGroupBoxCompras ComprasGbxValidaciones;
        private CapaVista_Componentes.Componentes.UsrDataGridViewCompras ComprasUsrValidaciones;
        private CapaVista_Componentes.Componentes.ClsGroupBoxCompras ComprasGbxSaldos;
        private CapaVista_Componentes.Componentes.UsrDataGridViewCompras ComprasUsrSaldos;
        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras ComprasTlpTotales;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrEtiquetaTotalCargos;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrTotalCargos;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrEtiquetaTotalAbonos;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrTotalAbonos;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrEtiquetaSaldoFinal;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrSaldoFinal;
        private CapaVista_Componentes.Componentes.ClsTableLayoutPanelCompras ComprasTlpBotones;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnValidar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnEjecutarCierre;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnImprimir;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnAyuda;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnSalir;
    }
}
