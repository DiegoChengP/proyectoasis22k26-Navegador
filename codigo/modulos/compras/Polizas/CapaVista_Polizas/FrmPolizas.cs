using System.Windows.Forms;

namespace CapaVista_Polizas
{
    public partial class FrmPolizas : Form
    {
        public FrmPolizas()
        {
            InitializeComponent();
            CapaVista_Componentes.Componentes.ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
            ComprasBtnIngresarCuenta.Enabled = false;
            ComprasUsrLblCuenta.MostrarAsterisco = true;
            ComprasUsrCboCuenta.SelectedIndexChanged += (Remitente, Evento) =>
            {
                ComprasBtnIngresarCuenta.Enabled = CapaControlador_Polizas.ClsCuentaContable.ComprasFuncFormatoValido(
                    ComprasUsrCboCuenta.Text);
            };
            ComprasUsrCboCuenta.Entrada.Validating += (Remitente, Evento) =>
            {
                if (string.IsNullOrEmpty(ComprasUsrCboCuenta.Text)) return;
                bool EsValido = CapaControlador_Polizas.ClsCuentaContable.ComprasFuncFormatoValido(ComprasUsrCboCuenta.Text);
                Evento.Cancel = !EsValido;
                if (EsValido) ComprasUsrCboCuenta.ComprasMetLimpiarError();
                else ComprasUsrCboCuenta.ComprasMetMostrarError("Use el formato C.GG.CC.SSS; por ejemplo, 1.01.02.001.");
            };
        }
    }
}
