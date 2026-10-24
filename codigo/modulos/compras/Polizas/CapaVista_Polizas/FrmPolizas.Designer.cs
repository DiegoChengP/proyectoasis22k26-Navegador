namespace CapaVista_Polizas
{
    partial class FrmPolizas
    {
        private System.ComponentModel.IContainer _Componentes = null;
        private CapaVista_Componentes.Componentes.ClsPanelVerticalCompras ComprasPnlDesplazamiento;
        private System.Windows.Forms.Panel ComprasPnlContenido;

        protected override void Dispose(bool LiberarRecursos)
        {
            if (LiberarRecursos && _Componentes != null) _Componentes.Dispose();
            base.Dispose(LiberarRecursos);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPolizas));
            this.ComprasPnlDesplazamiento = new CapaVista_Componentes.Componentes.ClsPanelVerticalCompras();
            this.ComprasPnlContenido = new System.Windows.Forms.Panel();
            this.ComprasGbxDetalle = new CapaVista_Componentes.Componentes.ClsGroupBoxCompras();
            this.ComprasUsrLblDiferencia = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasBtnEnviar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnGuardar = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnNuevo = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasBtnIngresarCuenta = new CapaVista_Componentes.Componentes.ClsButtonCompras();
            this.ComprasUsrCboCuenta = new CapaVista_Componentes.Componentes.UsrComboBoxCompras();
            this.ComprasUsrLblCuenta = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasGbxEncabezado = new CapaVista_Componentes.Componentes.ClsGroupBoxCompras();
            this.ComprasUsrLblDescripcion = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrTxtDescripcion = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrLblFecha = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrCboFecha = new CapaVista_Componentes.Componentes.UsrComboBoxCompras();
            this.ComprasUsrLblPeriodo = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrTxtPeriodo = new CapaVista_Componentes.Componentes.UsrTextBoxCompras();
            this.ComprasUsrLblOrigen = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrLblEstado = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasUsrCboOrigen = new CapaVista_Componentes.Componentes.UsrComboBoxCompras();
            this.ComprasUsrCboEstado = new CapaVista_Componentes.Componentes.UsrComboBoxCompras();
            this.ComprasUsrLblTitulo = new CapaVista_Componentes.Componentes.UsrLabelCompras();
            this.ComprasPnlDesplazamiento.SuspendLayout();
            this.ComprasPnlContenido.SuspendLayout();
            this.ComprasGbxDetalle.SuspendLayout();
            this.ComprasGbxEncabezado.SuspendLayout();
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
            this.ComprasPnlContenido.Controls.Add(this.ComprasGbxDetalle);
            this.ComprasPnlContenido.Controls.Add(this.ComprasGbxEncabezado);
            this.ComprasPnlContenido.Controls.Add(this.ComprasUsrLblTitulo);
            this.ComprasPnlContenido.Dock = System.Windows.Forms.DockStyle.Top;
            this.ComprasPnlContenido.Location = new System.Drawing.Point(0, 0);
            this.ComprasPnlContenido.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasPnlContenido.Name = "ComprasPnlContenido";
            this.ComprasPnlContenido.Size = new System.Drawing.Size(1279, 723);
            this.ComprasPnlContenido.TabIndex = 0;
            // 
            // ComprasGbxDetalle
            // 
            this.ComprasGbxDetalle.BackColor = System.Drawing.Color.Transparent;
            this.ComprasGbxDetalle.Controls.Add(this.ComprasUsrLblDiferencia);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasBtnEnviar);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasBtnGuardar);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasBtnNuevo);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasBtnIngresarCuenta);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasUsrCboCuenta);
            this.ComprasGbxDetalle.Controls.Add(this.ComprasUsrLblCuenta);
            this.ComprasGbxDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasGbxDetalle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ComprasGbxDetalle.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasGbxDetalle.Location = new System.Drawing.Point(21, 392);
            this.ComprasGbxDetalle.Name = "ComprasGbxDetalle";
            this.ComprasGbxDetalle.Size = new System.Drawing.Size(1255, 296);
            this.ComprasGbxDetalle.TabIndex = 9;
            this.ComprasGbxDetalle.TabStop = false;
            // 
            // ComprasUsrLblDiferencia
            // 
            this.ComprasUsrLblDiferencia.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblDiferencia.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblDiferencia.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblDiferencia.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblDiferencia.Location = new System.Drawing.Point(10, 245);
            this.ComprasUsrLblDiferencia.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblDiferencia.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblDiferencia.MostrarAsterisco = false;
            this.ComprasUsrLblDiferencia.Name = "ComprasUsrLblDiferencia";
            this.ComprasUsrLblDiferencia.Size = new System.Drawing.Size(368, 32);
            this.ComprasUsrLblDiferencia.TabIndex = 14;
            this.ComprasUsrLblDiferencia.TabStop = false;
            this.ComprasUsrLblDiferencia.Text = "Descuadra- Diferencia Q 00.00";
            this.ComprasUsrLblDiferencia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblDiferencia.Texto = "Descuadra- Diferencia Q 00.00";
            this.ComprasUsrLblDiferencia.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasBtnEnviar
            // 
            this.ComprasBtnEnviar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnEnviar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnEnviar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnEnviar.BackgroundImage")));
            this.ComprasBtnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnEnviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnEnviar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnEnviar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnEnviar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnEnviar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnEnviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnEnviar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnEnviar.Location = new System.Drawing.Point(1146, 213);
            this.ComprasBtnEnviar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnEnviar.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnEnviar.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnEnviar.Name = "ComprasBtnEnviar";
            this.ComprasBtnEnviar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnEnviar.TabIndex = 13;
            this.ComprasBtnEnviar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnGuardar
            // 
            this.ComprasBtnGuardar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnGuardar.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnGuardar.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnGuardar.BackgroundImage")));
            this.ComprasBtnGuardar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnGuardar.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnGuardar.FlatAppearance.BorderSize = 0;
            this.ComprasBtnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnGuardar.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnGuardar.Location = new System.Drawing.Point(1045, 213);
            this.ComprasBtnGuardar.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnGuardar.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnGuardar.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnGuardar.Name = "ComprasBtnGuardar";
            this.ComprasBtnGuardar.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnGuardar.TabIndex = 12;
            this.ComprasBtnGuardar.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnNuevo
            // 
            this.ComprasBtnNuevo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnNuevo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnNuevo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnNuevo.BackgroundImage")));
            this.ComprasBtnNuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnNuevo.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnNuevo.FlatAppearance.BorderSize = 0;
            this.ComprasBtnNuevo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnNuevo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnNuevo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnNuevo.Location = new System.Drawing.Point(942, 213);
            this.ComprasBtnNuevo.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnNuevo.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnNuevo.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnNuevo.Name = "ComprasBtnNuevo";
            this.ComprasBtnNuevo.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnNuevo.TabIndex = 11;
            this.ComprasBtnNuevo.UseVisualStyleBackColor = false;
            // 
            // ComprasBtnIngresarCuenta
            // 
            this.ComprasBtnIngresarCuenta.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ComprasBtnIngresarCuenta.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoBotones;
            this.ComprasBtnIngresarCuenta.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ComprasBtnIngresarCuenta.BackgroundImage")));
            this.ComprasBtnIngresarCuenta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ComprasBtnIngresarCuenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ComprasBtnIngresarCuenta.FlatAppearance.BorderColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnIngresarCuenta.FlatAppearance.BorderSize = 0;
            this.ComprasBtnIngresarCuenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(173)))), ((int)(((byte)(213)))));
            this.ComprasBtnIngresarCuenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(193)))), ((int)(((byte)(224)))));
            this.ComprasBtnIngresarCuenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasBtnIngresarCuenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComprasBtnIngresarCuenta.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasBtnIngresarCuenta.Location = new System.Drawing.Point(338, 45);
            this.ComprasBtnIngresarCuenta.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasBtnIngresarCuenta.MaximumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnIngresarCuenta.MinimumSize = new System.Drawing.Size(80, 80);
            this.ComprasBtnIngresarCuenta.Name = "ComprasBtnIngresarCuenta";
            this.ComprasBtnIngresarCuenta.Size = new System.Drawing.Size(80, 80);
            this.ComprasBtnIngresarCuenta.TabIndex = 10;
            this.ComprasBtnIngresarCuenta.UseVisualStyleBackColor = false;
            // 
            // ComprasUsrCboCuenta
            // 
            this.ComprasUsrCboCuenta.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrCboCuenta.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrCboCuenta.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrCboCuenta.Location = new System.Drawing.Point(10, 68);
            this.ComprasUsrCboCuenta.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrCboCuenta.MaxLength = 0;
            this.ComprasUsrCboCuenta.MostrarEtiquetaError = false;
            this.ComprasUsrCboCuenta.Name = "ComprasUsrCboCuenta";
            this.ComprasUsrCboCuenta.Size = new System.Drawing.Size(289, 45);
            this.ComprasUsrCboCuenta.TabIndex = 9;
            // 
            // ComprasUsrLblCuenta
            // 
            this.ComprasUsrLblCuenta.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblCuenta.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblCuenta.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblCuenta.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblCuenta.Location = new System.Drawing.Point(7, 30);
            this.ComprasUsrLblCuenta.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblCuenta.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblCuenta.MostrarAsterisco = false;
            this.ComprasUsrLblCuenta.Name = "ComprasUsrLblCuenta";
            this.ComprasUsrLblCuenta.Size = new System.Drawing.Size(236, 32);
            this.ComprasUsrLblCuenta.TabIndex = 5;
            this.ComprasUsrLblCuenta.TabStop = false;
            this.ComprasUsrLblCuenta.Text = "Ingresar Cuenta";
            this.ComprasUsrLblCuenta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblCuenta.Texto = "Ingresar Cuenta";
            this.ComprasUsrLblCuenta.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasGbxEncabezado
            // 
            this.ComprasGbxEncabezado.BackColor = System.Drawing.Color.Transparent;
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrLblDescripcion);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrTxtDescripcion);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrLblFecha);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrCboFecha);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrLblPeriodo);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrTxtPeriodo);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrLblOrigen);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrLblEstado);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrCboOrigen);
            this.ComprasGbxEncabezado.Controls.Add(this.ComprasUsrCboEstado);
            this.ComprasGbxEncabezado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComprasGbxEncabezado.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.ComprasGbxEncabezado.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasGbxEncabezado.Location = new System.Drawing.Point(21, 92);
            this.ComprasGbxEncabezado.Name = "ComprasGbxEncabezado";
            this.ComprasGbxEncabezado.Size = new System.Drawing.Size(1255, 296);
            this.ComprasGbxEncabezado.TabIndex = 1;
            this.ComprasGbxEncabezado.TabStop = false;
            // 
            // ComprasUsrLblDescripcion
            // 
            this.ComprasUsrLblDescripcion.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblDescripcion.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblDescripcion.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblDescripcion.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblDescripcion.Location = new System.Drawing.Point(7, 136);
            this.ComprasUsrLblDescripcion.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblDescripcion.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblDescripcion.MostrarAsterisco = false;
            this.ComprasUsrLblDescripcion.Name = "ComprasUsrLblDescripcion";
            this.ComprasUsrLblDescripcion.Size = new System.Drawing.Size(236, 32);
            this.ComprasUsrLblDescripcion.TabIndex = 7;
            this.ComprasUsrLblDescripcion.TabStop = false;
            this.ComprasUsrLblDescripcion.Text = "Descripcion";
            this.ComprasUsrLblDescripcion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblDescripcion.Texto = "Descripcion";
            this.ComprasUsrLblDescripcion.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasUsrTxtDescripcion
            // 
            this.ComprasUsrTxtDescripcion.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrTxtDescripcion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrTxtDescripcion.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrTxtDescripcion.Location = new System.Drawing.Point(10, 174);
            this.ComprasUsrTxtDescripcion.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrTxtDescripcion.MaxLength = 2147483647;
            this.ComprasUsrTxtDescripcion.MostrarEtiquetaError = false;
            this.ComprasUsrTxtDescripcion.Multiline = true;
            this.ComprasUsrTxtDescripcion.Name = "ComprasUsrTxtDescripcion";
            this.ComprasUsrTxtDescripcion.SelectedText = "";
            this.ComprasUsrTxtDescripcion.SelectionLength = 0;
            this.ComprasUsrTxtDescripcion.SelectionStart = 0;
            this.ComprasUsrTxtDescripcion.Size = new System.Drawing.Size(1216, 85);
            this.ComprasUsrTxtDescripcion.TabIndex = 8;
            // 
            // ComprasUsrLblFecha
            // 
            this.ComprasUsrLblFecha.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblFecha.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblFecha.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblFecha.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblFecha.Location = new System.Drawing.Point(22, 36);
            this.ComprasUsrLblFecha.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblFecha.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblFecha.MostrarAsterisco = false;
            this.ComprasUsrLblFecha.Name = "ComprasUsrLblFecha";
            this.ComprasUsrLblFecha.Size = new System.Drawing.Size(224, 32);
            this.ComprasUsrLblFecha.TabIndex = 5;
            this.ComprasUsrLblFecha.TabStop = false;
            this.ComprasUsrLblFecha.Text = "Fecha";
            this.ComprasUsrLblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblFecha.Texto = "Fecha";
            this.ComprasUsrLblFecha.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasUsrCboFecha
            // 
            this.ComprasUsrCboFecha.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrCboFecha.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrCboFecha.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrCboFecha.Location = new System.Drawing.Point(22, 73);
            this.ComprasUsrCboFecha.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrCboFecha.MaxLength = 0;
            this.ComprasUsrCboFecha.MostrarEtiquetaError = false;
            this.ComprasUsrCboFecha.Name = "ComprasUsrCboFecha";
            this.ComprasUsrCboFecha.Size = new System.Drawing.Size(224, 45);
            this.ComprasUsrCboFecha.TabIndex = 3;
            // 
            // ComprasUsrLblPeriodo
            // 
            this.ComprasUsrLblPeriodo.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblPeriodo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblPeriodo.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblPeriodo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblPeriodo.Location = new System.Drawing.Point(260, 36);
            this.ComprasUsrLblPeriodo.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblPeriodo.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblPeriodo.MostrarAsterisco = false;
            this.ComprasUsrLblPeriodo.Name = "ComprasUsrLblPeriodo";
            this.ComprasUsrLblPeriodo.Size = new System.Drawing.Size(236, 32);
            this.ComprasUsrLblPeriodo.TabIndex = 5;
            this.ComprasUsrLblPeriodo.TabStop = false;
            this.ComprasUsrLblPeriodo.Text = "Periodo";
            this.ComprasUsrLblPeriodo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblPeriodo.Texto = "Periodo";
            this.ComprasUsrLblPeriodo.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasUsrTxtPeriodo
            // 
            this.ComprasUsrTxtPeriodo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrTxtPeriodo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrTxtPeriodo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrTxtPeriodo.Location = new System.Drawing.Point(275, 73);
            this.ComprasUsrTxtPeriodo.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrTxtPeriodo.MaxLength = 2147483647;
            this.ComprasUsrTxtPeriodo.MostrarEtiquetaError = false;
            this.ComprasUsrTxtPeriodo.Multiline = true;
            this.ComprasUsrTxtPeriodo.Name = "ComprasUsrTxtPeriodo";
            this.ComprasUsrTxtPeriodo.SelectedText = "";
            this.ComprasUsrTxtPeriodo.SelectionLength = 0;
            this.ComprasUsrTxtPeriodo.SelectionStart = 0;
            this.ComprasUsrTxtPeriodo.Size = new System.Drawing.Size(236, 36);
            this.ComprasUsrTxtPeriodo.TabIndex = 6;
            // 
            // ComprasUsrLblOrigen
            // 
            this.ComprasUsrLblOrigen.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblOrigen.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblOrigen.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblOrigen.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblOrigen.Location = new System.Drawing.Point(850, 30);
            this.ComprasUsrLblOrigen.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblOrigen.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblOrigen.MostrarAsterisco = false;
            this.ComprasUsrLblOrigen.Name = "ComprasUsrLblOrigen";
            this.ComprasUsrLblOrigen.Size = new System.Drawing.Size(338, 32);
            this.ComprasUsrLblOrigen.TabIndex = 5;
            this.ComprasUsrLblOrigen.TabStop = false;
            this.ComprasUsrLblOrigen.Text = "Origen";
            this.ComprasUsrLblOrigen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblOrigen.Texto = "Origen";
            this.ComprasUsrLblOrigen.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasUsrLblEstado
            // 
            this.ComprasUsrLblEstado.AlineacionTexto = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblEstado.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblEstado.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.ComprasUsrLblEstado.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblEstado.Location = new System.Drawing.Point(541, 30);
            this.ComprasUsrLblEstado.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.ComprasUsrLblEstado.MaximumSize = new System.Drawing.Size(1625, 0);
            this.ComprasUsrLblEstado.MostrarAsterisco = false;
            this.ComprasUsrLblEstado.Name = "ComprasUsrLblEstado";
            this.ComprasUsrLblEstado.Size = new System.Drawing.Size(278, 32);
            this.ComprasUsrLblEstado.TabIndex = 4;
            this.ComprasUsrLblEstado.TabStop = false;
            this.ComprasUsrLblEstado.Text = "Estado";
            this.ComprasUsrLblEstado.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ComprasUsrLblEstado.Texto = "Estado";
            this.ComprasUsrLblEstado.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.Campo;
            // 
            // ComprasUsrCboOrigen
            // 
            this.ComprasUsrCboOrigen.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrCboOrigen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrCboOrigen.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrCboOrigen.Location = new System.Drawing.Point(876, 74);
            this.ComprasUsrCboOrigen.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrCboOrigen.MaxLength = 0;
            this.ComprasUsrCboOrigen.MostrarEtiquetaError = false;
            this.ComprasUsrCboOrigen.Name = "ComprasUsrCboOrigen";
            this.ComprasUsrCboOrigen.Size = new System.Drawing.Size(289, 44);
            this.ComprasUsrCboOrigen.TabIndex = 3;
            // 
            // ComprasUsrCboEstado
            // 
            this.ComprasUsrCboEstado.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrCboEstado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ComprasUsrCboEstado.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrCboEstado.Location = new System.Drawing.Point(541, 73);
            this.ComprasUsrCboEstado.Margin = new System.Windows.Forms.Padding(0);
            this.ComprasUsrCboEstado.MaxLength = 0;
            this.ComprasUsrCboEstado.MostrarEtiquetaError = false;
            this.ComprasUsrCboEstado.Name = "ComprasUsrCboEstado";
            this.ComprasUsrCboEstado.Size = new System.Drawing.Size(289, 45);
            this.ComprasUsrCboEstado.TabIndex = 2;
            // 
            // ComprasUsrLblTitulo
            // 
            this.ComprasUsrLblTitulo.AlineacionTexto = System.Drawing.ContentAlignment.MiddleCenter;
            this.ComprasUsrLblTitulo.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ComprasUsrLblTitulo.Font = new System.Drawing.Font("Lucida Sans", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.ComprasUsrLblTitulo.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.ComprasUsrLblTitulo.Location = new System.Drawing.Point(346, 33);
            this.ComprasUsrLblTitulo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.ComprasUsrLblTitulo.MaximumSize = new System.Drawing.Size(2375, 0);
            this.ComprasUsrLblTitulo.MostrarAsterisco = false;
            this.ComprasUsrLblTitulo.Name = "ComprasUsrLblTitulo";
            this.ComprasUsrLblTitulo.Size = new System.Drawing.Size(494, 36);
            this.ComprasUsrLblTitulo.TabIndex = 0;
            this.ComprasUsrLblTitulo.TabStop = false;
            this.ComprasUsrLblTitulo.Text = "Póliza de compras";
            this.ComprasUsrLblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ComprasUsrLblTitulo.Texto = "Póliza de compras";
            this.ComprasUsrLblTitulo.Tipografia = CapaVista_Componentes.Componentes.TipografiaCompras.TituloFormulario;
            // 
            // FrmPolizas
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral;
            this.ClientSize = new System.Drawing.Size(1300, 700);
            this.Controls.Add(this.ComprasPnlDesplazamiento);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ForeColor = CapaVista_Componentes.Componentes.ClsTemaCompras.PrincipalNavegador;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmPolizas";
            this.Text = "6002 – Polizas";
            this.ComprasPnlDesplazamiento.ResumeLayout(false);
            this.ComprasPnlContenido.ResumeLayout(false);
            this.ComprasGbxDetalle.ResumeLayout(false);
            this.ComprasGbxEncabezado.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private CapaVista_Componentes.Componentes.ClsGroupBoxCompras ComprasGbxEncabezado;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblTitulo;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrTxtPeriodo;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblOrigen;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblEstado;
        private CapaVista_Componentes.Componentes.UsrComboBoxCompras ComprasUsrCboOrigen;
        private CapaVista_Componentes.Componentes.UsrComboBoxCompras ComprasUsrCboEstado;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblDescripcion;
        private CapaVista_Componentes.Componentes.UsrTextBoxCompras ComprasUsrTxtDescripcion;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblFecha;
        private CapaVista_Componentes.Componentes.UsrComboBoxCompras ComprasUsrCboFecha;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblPeriodo;
        private CapaVista_Componentes.Componentes.ClsGroupBoxCompras ComprasGbxDetalle;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblDiferencia;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnEnviar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnGuardar;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnNuevo;
        private CapaVista_Componentes.Componentes.ClsButtonCompras ComprasBtnIngresarCuenta;
        private CapaVista_Componentes.Componentes.UsrComboBoxCompras ComprasUsrCboCuenta;
        private CapaVista_Componentes.Componentes.UsrLabelCompras ComprasUsrLblCuenta;
    }
}
