using System.Drawing;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    public class ClsRadioButtonCompras : RadioButton
    {


        public ClsRadioButtonCompras()
        {
            Font = new Font("Tahoma",9.0F,FontStyle.Regular,GraphicsUnit.Point);

            ForeColor = ClsTemaCompras.PrincipalNavegador;
            BackColor = Color.Transparent;
            AutoSize = true;
            Cursor = Cursors.Hand;
            UseVisualStyleBackColor = true;
            Margin = new Padding(3);
        }

    }
}
