using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    public class ClsPanelCompras : Panel
    {

        public ClsPanelCompras()
        {
            BackColor = ClsTemaCompras.FondoGeneral;
            ForeColor = ClsTemaCompras.PrincipalNavegador;
            DoubleBuffered = true;
            ResizeRedraw = true;
        }

    }
}
