using System.Drawing;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    public partial class ClsGroupBoxCompras : GroupBox
    {

        public ClsGroupBoxCompras()
        {
            Font = new Font("Tahoma",10F,FontStyle.Bold,GraphicsUnit.Point);
            ForeColor = ClsTemaCompras.PrincipalNavegador;
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            Margin = new Padding(3);
        }

    }
}
