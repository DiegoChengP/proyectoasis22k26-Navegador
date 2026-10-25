using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    [DefaultBindingProperty("SelectedValue")]
    [DefaultEvent("SelectedIndexChanged")]
    public partial class UsrComboBoxCompras : UserControl
    {

        private bool _ErrorVisible;
        private bool _MostrarEtiquetaError = true;
        private int _AlturaEtiquetaError = 22;
        private int _AlturaErrorAmpliada;
        private int _AlturaFilaErrorAplicada = 30;
        private int _VersionTexto;

        [Category("Compras")]
        [Description("Muestra la etiqueta de error y reserva su espacio. Al desactivarla, solo se conserva el borde de error.")]
        [DefaultValue(true)]
        [RefreshProperties(RefreshProperties.All)]
        public bool MostrarEtiquetaError
        {
            get => _MostrarEtiquetaError;
            set
            {
                if (_MostrarEtiquetaError == value) return;
                _MostrarEtiquetaError = value;
                ComprasProcActualizarEspacioError();
            }
        }

        [Category("Compras")]
        [Description("Altura en píxeles de la etiqueta de error, sin incluir sus márgenes.")]
        [DefaultValue(22)]
        [RefreshProperties(RefreshProperties.All)]
        public int AlturaEtiquetaError
        {
            get => _AlturaEtiquetaError;
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException(nameof(value));
                if (_AlturaEtiquetaError == value) return;
                _AlturaEtiquetaError = value;
                ComprasProcActualizarEspacioError();
            }
        }

        private void ComprasProcActualizarEspacioError()
        {
            if (ComprasUsrError == null || ComprasTlpMain == null) return;
            using (new ClsActualizacionDisenoCompras(ComprasTlpMain))
            {
                int Altura = Math.Max(_AlturaEtiquetaError, _AlturaErrorAmpliada);
                int AlturaFila = _MostrarEtiquetaError ? Altura + ComprasUsrError.Margin.Vertical : 0;
                int Diferencia = AlturaFila - _AlturaFilaErrorAplicada;
                _AlturaFilaErrorAplicada = AlturaFila;
                ComprasUsrError.MinimumSize = Size.Empty;
                ComprasUsrError.MaximumSize = new Size(0, Altura);
                ComprasUsrError.Visible = _MostrarEtiquetaError;
                ClsActualizacionDisenoCompras.ComprasProcAltura(ComprasTlpMain.RowStyles[1], AlturaFila);
                if (Diferencia != 0) Height = Math.Max(1, Height + Diferencia);
            }
        }

        public UsrComboBoxCompras()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ComprasUsrError.ComprasProcConservarRojo();
            ComprasCboEntrada.TextChanged += (Sender, Evento) => ComprasProcTextoCambiado(Evento);
            Enter += (Sender, Evento) => ComprasCboEntrada.Focus();
            ComprasCboEntrada.KeyDown += (Sender, Evento) => OnKeyDown(Evento);
            ComprasCboEntrada.KeyPress += (Sender, Evento) => OnKeyPress(Evento);
            ComprasCboEntrada.KeyUp += (Sender, Evento) => OnKeyUp(Evento);
            ComprasCboEntrada.MouseClick += (Sender, Evento) => OnMouseClick(Evento);
            ComprasCboEntrada.Enter += (Sender, Evento) => ComprasProcActualizarBorde();
            ComprasCboEntrada.Leave += (Sender, Evento) => ComprasProcActualizarBorde();
            ComprasCboEntrada.SelectedIndexChanged += (Sender, Evento) =>
            {
                ComprasMetLimpiarError();
                SelectedIndexChanged?.Invoke(this, Evento);
            };
            ComprasCboEntrada.SelectedValueChanged += (Sender, Evento) => SelectedValueChanged?.Invoke(this, Evento);
            ComprasCboEntrada.SelectionChangeCommitted += (Sender, Evento) => SelectionChangeCommitted?.Invoke(this, Evento);
            ComprasCboEntrada.DropDown += (Sender, Evento) => DropDown?.Invoke(this, Evento);
            ComprasCboEntrada.DropDownClosed += (Sender, Evento) => DropDownClosed?.Invoke(this, Evento);
            ComprasCboEntrada.DataSourceChanged += (Sender, Evento) => DataSourceChanged?.Invoke(this, Evento);
            ComprasProcAjustarAlturaEntrada();
            ComprasMetLimpiarError();
        }

        private void ComprasProcTextoCambiado(EventArgs Evento)
        {
            _VersionTexto++;
            ComprasMetLimpiarError();
            OnTextChanged(Evento);
        }

        [Browsable(true)]
        [Bindable(true)]
        [DefaultValue("")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                return ComprasCboEntrada == null ? base.Text : ComprasCboEntrada.Text;
            }
            set
            {
                if (ComprasCboEntrada != null)
                {
                    string Nuevo = value ?? string.Empty;
                    string Anterior = ComprasCboEntrada.Text;
                    int Version = _VersionTexto;
                    ComprasCboEntrada.Text = Nuevo;
                    if (Anterior != ComprasCboEntrada.Text && Version == _VersionTexto)
                        ComprasProcTextoCambiado(EventArgs.Empty);
                }
                else
                {
                    base.Text = value;
                }
            }
        }

        internal ClsSelectorCompras ComprasCboInterno => ComprasCboEntrada;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ClsSelectorCompras Entrada => ComprasCboEntrada;

        [Category("Datos")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ComboBox.ObjectCollection Items => ComprasCboEntrada.Items;

        [Category("Datos")]
        [DefaultValue(null)]
        [AttributeProvider(typeof(IListSource))]
        public object DataSource { get => ComprasCboEntrada.DataSource; set => ComprasCboEntrada.DataSource = value; }

        [Category("Datos")]
        [DefaultValue("")]
        public string DisplayMember { get => ComprasCboEntrada.DisplayMember; set => ComprasCboEntrada.DisplayMember = value; }

        [Category("Datos")]
        [DefaultValue("")]
        public string ValueMember { get => ComprasCboEntrada.ValueMember; set => ComprasCboEntrada.ValueMember = value; }

        [Browsable(false)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedValue { get => ComprasCboEntrada.SelectedValue; set => ComprasCboEntrada.SelectedValue = value; }

        [Browsable(false)]
        [Bindable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object SelectedItem { get => ComprasCboEntrada.SelectedItem; set => ComprasCboEntrada.SelectedItem = value; }

        [Category("Datos")]
        [DefaultValue(-1)]
        [Bindable(true)]
        public int SelectedIndex { get => ComprasCboEntrada.SelectedIndex; set => ComprasCboEntrada.SelectedIndex = value; }

        [Category("Comportamiento")]
        [DefaultValue(ComboBoxStyle.DropDownList)]
        [RefreshProperties(RefreshProperties.All)]
        public ComboBoxStyle DropDownStyle
        {
            get => ComprasCboEntrada.DropDownStyle;
            set { ComprasCboEntrada.DropDownStyle = value; ComprasProcAjustarAlturaEntrada(); }
        }

        [Category("Comportamiento")]
        [DefaultValue(false)]
        public bool Sorted { get => ComprasCboEntrada.Sorted; set => ComprasCboEntrada.Sorted = value; }

        [Category("Comportamiento")]
        [DefaultValue(8)]
        public int MaxDropDownItems { get => ComprasCboEntrada.MaxDropDownItems; set => ComprasCboEntrada.MaxDropDownItems = value; }

        [Category("Comportamiento")]
        [DefaultValue(true)]
        public bool IntegralHeight { get => ComprasCboEntrada.IntegralHeight; set => ComprasCboEntrada.IntegralHeight = value; }

        [Category("Comportamiento")]
        [DefaultValue(AutoCompleteMode.None)]
        public AutoCompleteMode AutoCompleteMode { get => ComprasCboEntrada.AutoCompleteMode; set => ComprasCboEntrada.AutoCompleteMode = value; }

        [Category("Comportamiento")]
        [DefaultValue(AutoCompleteSource.None)]
        public AutoCompleteSource AutoCompleteSource { get => ComprasCboEntrada.AutoCompleteSource; set => ComprasCboEntrada.AutoCompleteSource = value; }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public AutoCompleteStringCollection AutoCompleteCustomSource => ComprasCboEntrada.AutoCompleteCustomSource;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool DroppedDown { get => ComprasCboEntrada.DroppedDown; set => ComprasCboEntrada.DroppedDown = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart { get => ComprasCboEntrada.SelectionStart; set => ComprasCboEntrada.SelectionStart = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength { get => ComprasCboEntrada.SelectionLength; set => ComprasCboEntrada.SelectionLength = value; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText { get => ComprasCboEntrada.SelectedText; set => ComprasCboEntrada.SelectedText = value; }

        public event EventHandler SelectedIndexChanged;
        public event EventHandler SelectedValueChanged;
        public event EventHandler SelectionChangeCommitted;
        public event EventHandler DropDown;
        public event EventHandler DropDownClosed;
        public event EventHandler DataSourceChanged;

        public void BeginUpdate() => ComprasCboEntrada.BeginUpdate();
        public void EndUpdate() => ComprasCboEntrada.EndUpdate();
        public int FindString(string Texto) => ComprasCboEntrada.FindString(Texto);
        public int FindStringExact(string Texto) => ComprasCboEntrada.FindStringExact(Texto);
        public string GetItemText(object Elemento) => ComprasCboEntrada.GetItemText(Elemento);
        public void Select(int Inicio, int Longitud) => ComprasCboEntrada.Select(Inicio, Longitud);
        public void SelectAll() => ComprasCboEntrada.SelectAll();

        private void ComprasProcAjustarAlturaEntrada()
        {
            if (ComprasCboEntrada == null || ComprasPnlBorde == null) return;
            ComprasPnlBorde.MaximumSize = DropDownStyle == ComboBoxStyle.Simple ? Size.Empty :
                new Size(0, ComprasCboEntrada.PreferredHeight + ComprasPnlBorde.Padding.Vertical);
        }

        protected override void OnFontChanged(EventArgs Evento)
        {
            base.OnFontChanged(Evento);
            if (ComprasCboEntrada != null) ComprasCboEntrada.Font = Font;
            ComprasProcAjustarAlturaEntrada();
        }

        protected override void OnForeColorChanged(EventArgs Evento)
        {
            base.OnForeColorChanged(Evento);
            if (ComprasCboEntrada != null) ComprasCboEntrada.ForeColor = ForeColor;
        }

        protected override void OnBackColorChanged(EventArgs Evento)
        {
            base.OnBackColorChanged(Evento);
            if (ComprasCboEntrada != null) ComprasCboEntrada.BackColor = BackColor;
            if (ComprasTlpMain != null) ComprasTlpMain.BackColor = BackColor;
        }

        private void ComprasProcActualizarBorde()
        {
            ComprasPnlBorde.BackColor = _ErrorVisible ? Color.Red :
                ComprasCboEntrada.Focused ? ClsTemaCompras.AcentoNavegador : ClsTemaCompras.PrincipalNavegador;
        }

        protected override void OnCausesValidationChanged(EventArgs Evento)
        {
            base.OnCausesValidationChanged(Evento);
            if (ComprasCboEntrada != null) ComprasCboEntrada.CausesValidation = CausesValidation;
        }

        public int MaxLength
        {
            get
            {
                return ComprasCboEntrada.MaxLength;
            }
            set
            {
                ComprasCboEntrada.MaxLength = value;
            }
        }

        public event KeyEventHandler TextoKeyDown
        {
            add
            {
                ComprasCboEntrada.KeyDown += value;
            }
            remove
            {
                ComprasCboEntrada.KeyDown -= value;
            }
        }

        public void ComprasMetMostrarError(string Mensaje)
        {
            if (string.IsNullOrWhiteSpace(Mensaje))
            {
                ComprasMetLimpiarError();
                return;
            }

            using (new ClsActualizacionDisenoCompras(ComprasTlpMain))
            {
                if (ComprasUsrError.Texto != Mensaje) ComprasUsrError.Texto = Mensaje;
                if (ComprasUsrError.ColorTexto != Color.Red) ComprasUsrError.ColorTexto = Color.Red;
                _ErrorVisible = true;
                ComprasProcActualizarEspacioError();
                if (ComprasPnlBorde.BackColor != Color.Red) ComprasPnlBorde.BackColor = Color.Red;
            }
        }

        internal int ComprasFuncAlturaError(string Mensaje)
        {
            int Ancho = System.Math.Max(1, Width - 12);
            int Altura = System.Math.Max(22, TextRenderer.MeasureText(Mensaje, ComprasUsrError.Font, new Size(Ancho, int.MaxValue), TextFormatFlags.WordBreak).Height);
            int AlturaEntrada = Math.Max(25, ComprasPnlBorde.Height) + ComprasTlpMain.Padding.Vertical;
            return AlturaEntrada + (_MostrarEtiquetaError ? Math.Max(_AlturaEtiquetaError, Altura) + ComprasUsrError.Margin.Vertical : 0);
        }

        internal void ComprasProcAmpliarError(int AlturaFila)
        {
            _AlturaErrorAmpliada = Math.Max(0, AlturaFila - Math.Max(25, ComprasPnlBorde.Height)
                - ComprasTlpMain.Padding.Vertical - ComprasUsrError.Margin.Vertical);
            ComprasProcActualizarEspacioError();
        }

        public void ComprasMetLimpiarError()
        {
            using (new ClsActualizacionDisenoCompras(ComprasTlpMain))
            {
                if (ComprasUsrError.Texto != string.Empty) ComprasUsrError.Texto = string.Empty;
                _ErrorVisible = false;
                _AlturaErrorAmpliada = 0;
                ComprasProcActualizarEspacioError();
                ComprasProcActualizarBorde();
            }
        }

    }
}
