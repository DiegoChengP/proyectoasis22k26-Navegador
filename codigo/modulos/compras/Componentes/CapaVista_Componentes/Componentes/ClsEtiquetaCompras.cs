using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaVista_Componentes.Componentes
{
    public sealed class ClsEtiquetaCompras : Label
    {
        internal bool ComprasConservarRojo { get; set; }

        protected override void OnPaint(PaintEventArgs Evento)
        {
            if (!ComprasConservarRojo)
            {
                base.OnPaint(Evento);
                return;
            }
            TextFormatFlags Formato = TextFormatFlags.WordBreak | TextFormatFlags.NoPrefix | TextFormatFlags.TextBoxControl;
            if (TextAlign == ContentAlignment.MiddleLeft) Formato |= TextFormatFlags.VerticalCenter;
            Rectangle Area = new Rectangle(Padding.Left, Padding.Top, Math.Max(0, ClientSize.Width - Padding.Horizontal), Math.Max(0, ClientSize.Height - Padding.Vertical));
            TextRenderer.DrawText(Evento.Graphics, Text, Font, Area, Color.Red, Formato);
        }
    }
}
