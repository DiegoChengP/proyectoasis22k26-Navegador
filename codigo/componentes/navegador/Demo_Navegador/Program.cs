using System;
using System.Windows.Forms;
using CapaVista_Navegador;

namespace Ejecucion_Navegador
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Crud());
        }
    }
}
