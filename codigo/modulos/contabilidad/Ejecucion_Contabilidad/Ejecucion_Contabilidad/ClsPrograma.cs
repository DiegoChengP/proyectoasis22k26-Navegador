/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito : arranca con el login de Seguridad, que abre el MDI de Contabilidad al elegirlo.
 * ===================================================================
*/

using System;
using System.Windows.Forms;

namespace Ejecucion_Contabilidad
{
    internal static class ClsPrograma
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CapaVista_Seguridad.FrmLogin());
        }
    }
}
