/*
 * Jose Javier Torres Martínez | 0901-23-1091 | 20/10/2026
 * Carga de formularios de Ventas desde sus DLL, al seleccionar la opción del MDI.
 */
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CapaVista_Ventas
{
    public partial class FrmMdiVentas
    {
        private void VentasMetAbrirModulo(string Biblioteca, string NombreTipo, string Opcion, bool Modal = true)
        {
            string Ruta = Path.Combine(Application.StartupPath, Biblioteca);
            if (!File.Exists(Ruta))
            {
                MessageBox.Show(this, "No se encontró el módulo de " + Opcion + ".\n" +
                    "Ejecute build_ventas.bat para compilar y reunir las DLL junto al ejecutable.",
                    Opcion, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form Formulario = null;
            try
            {
                Assembly BibliotecaModulo = Assembly.LoadFrom(Ruta);
                Type TipoFormulario = BibliotecaModulo.GetType(NombreTipo, true);
                if (!typeof(Form).IsAssignableFrom(TipoFormulario))
                    throw new InvalidOperationException("El módulo no contiene un formulario compatible.");

                Formulario = (Form)Activator.CreateInstance(TipoFormulario);
                if (Modal)
                {
                    using (Formulario) Formulario.ShowDialog(this);
                }
                else
                {
                    Formulario.Show(this);
                }
            }
            catch (Exception Error)
            {
                if (Formulario != null && !Formulario.IsDisposed) Formulario.Dispose();
                Exception Causa = Error is TargetInvocationException && Error.InnerException != null
                    ? Error.InnerException : Error;
                MessageBox.Show(this, "No se pudo abrir " + Opcion + ".\n" + Causa.Message,
                    Opcion, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
