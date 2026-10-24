/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito : arranca el módulo directamente en el MDI.
 * ===================================================================
*/

using System;
using System.Windows.Forms;

namespace Ejecucion_Contabilidad
{
    // Punto de entrada del ejecutable.
    internal static class ClsPrograma
    {
        /// <summary>
        /// Inicia Contabilidad directamente en el MDI.
        /// </summary>
        [STAThread]
        // Main conserva su nombre porque es el punto de entrada reconocido por C#.
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CapaVista_Contabilidad.FrmMDIContabilidad());
        }
    }
}
