using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    [DefaultBindingProperty("Text")]
    [DefaultEvent("TextChanged")]
    public partial class UsrTextBoxCompras : UserControl
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

        public UsrTextBoxCompras()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ComprasUsrError.ComprasProcConservarRojo();
            ComprasTxtTexto.TextChanged += (Sender, Evento) => ComprasProcTextoCambiado(Evento);
            Enter += (Sender, Evento) => ComprasTxtTexto.Focus();
            ComprasTxtTexto.KeyDown += (Sender, Evento) => OnKeyDown(Evento);
            ComprasTxtTexto.KeyPress += (Sender, Evento) => OnKeyPress(Evento);
            ComprasTxtTexto.KeyUp += (Sender, Evento) => OnKeyUp(Evento);
            ComprasTxtTexto.MouseClick += (Sender, Evento) => OnMouseClick(Evento);
            ComprasTxtTexto.Enter += (Sender, Evento) => ComprasProcActualizarBorde();
            ComprasTxtTexto.Leave += (Sender, Evento) => ComprasProcActualizarBorde();
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
                return ComprasTxtTexto == null ? base.Text : ComprasTxtTexto.Text;
            }
            set
            {
                if (ComprasTxtTexto != null)
                {
                    string Nuevo = value ?? string.Empty;
                    bool Cambiado = ComprasTxtTexto.Text != Nuevo;
                    int Version = _VersionTexto;
                    ComprasTxtTexto.Text = Nuevo;
                    if (Cambiado && Version == _VersionTexto)
                        ComprasProcTextoCambiado(EventArgs.Empty);
                }
                else
                {
                    base.Text = value;
                }
            }
        }

        internal RichTextBox ComprasTxtEntrada => ComprasTxtTexto;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBox Entrada => ComprasTxtTexto;

        [DefaultValue(false)]
        public bool ReadOnly { get => ComprasTxtTexto.ReadOnly; set => ComprasTxtTexto.ReadOnly = value; }
        [Category("Compras")]
        [Description("Permite escribir varias líneas cuando es True y una sola línea cuando es False.")]
        [DefaultValue(false)]
        [Browsable(true)]
        [RefreshProperties(RefreshProperties.All)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Multiline { get => ComprasTxtTexto.Multiline; set => ComprasTxtTexto.Multiline = value; }
        [Browsable(false)]
        public int SelectionStart { get => ComprasTxtTexto.SelectionStart; set => ComprasTxtTexto.SelectionStart = value; }
        [Browsable(false)]
        public int SelectionLength { get => ComprasTxtTexto.SelectionLength; set => ComprasTxtTexto.SelectionLength = value; }
        [Browsable(false)]
        public string SelectedText { get => ComprasTxtTexto.SelectedText; set => ComprasTxtTexto.SelectedText = value; }
        [Browsable(false)]
        public int TextLength => ComprasTxtTexto.TextLength;
        public void Clear() => ComprasTxtTexto.Clear();
        public void Select(int Inicio, int Longitud) => ComprasTxtTexto.Select(Inicio, Longitud);
        public void SelectAll() => ComprasTxtTexto.SelectAll();
        public void AppendText(string Texto) => ComprasTxtTexto.AppendText(Texto);
        public void Copy() => ComprasTxtTexto.Copy();
        public void Cut() => ComprasTxtTexto.Cut();
        public void Paste() => ComprasTxtTexto.Paste();
        public void Undo() => ComprasTxtTexto.Undo();

        protected override void OnFontChanged(EventArgs Evento)
        {
            base.OnFontChanged(Evento);
            if (ComprasTxtTexto != null) ComprasTxtTexto.Font = Font;
        }

        protected override void OnForeColorChanged(EventArgs Evento)
        {
            base.OnForeColorChanged(Evento);
            if (ComprasTxtTexto != null) ComprasTxtTexto.ForeColor = ForeColor;
        }

        protected override void OnBackColorChanged(EventArgs Evento)
        {
            base.OnBackColorChanged(Evento);
            if (ComprasTxtTexto != null) ComprasTxtTexto.BackColor = BackColor;
            if (ComprasTlpMain != null) ComprasTlpMain.BackColor = BackColor;
        }

        private void ComprasProcActualizarBorde()
        {
            ComprasPnlBorde.BackColor = _ErrorVisible ? Color.Red :
                ComprasTxtTexto.Focused ? ClsTemaCompras.AcentoNavegador : ClsTemaCompras.PrincipalNavegador;
        }

        protected override void OnCausesValidationChanged(EventArgs Evento)
        {
            base.OnCausesValidationChanged(Evento);
            if (ComprasTxtTexto != null) ComprasTxtTexto.CausesValidation = CausesValidation;
        }

        public int MaxLength
        {
            get
            {
                return ComprasTxtTexto.MaxLength;
            }
            set
            {
                ComprasTxtTexto.MaxLength = value;
            }
        }

        public event KeyEventHandler TextoKeyDown
        {
            add
            {
                ComprasTxtTexto.KeyDown += value;
            }
            remove
            {
                ComprasTxtTexto.KeyDown -= value;
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
