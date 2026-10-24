using System;
using System.Windows.Forms;
using CapaControlador_Compras;
using CapaControlador_Seguridad.Objetos_de_valor;
using CapaVista_Navegador;

namespace CapaVista_Compras
{
    internal static class ClsMantenimientoCompras
    {
        internal static void ComprasProcConfigurar(Form Formulario, Navegador Navegador, string Tabla, int CodigoAplicacion)
        {
            Navegador.Enabled = false;
            Formulario.Load += (Remitente, Evento) =>
            {
                string Mensaje;
                if (!ClsSesionSeguridad.HaySesionActiva)
                    Mensaje = "Este formulario no está habilitado para el prototipo no funcional.";
                else
                {
                    try
                    {
                        int CodigoModulo = ClsAplicacionesCompras.ComprasFuncObtenerIdModulo();
                        Navegador.NavegadorMetConfigurar(Tabla, CodigoModulo, CodigoAplicacion);
                        Navegador.Enabled = true;
                        return;
                    }
                    catch (InvalidOperationException)
                    {
                        Mensaje = "Este formulario no está habilitado para el prototipo no funcional.";
                    }
                }
                Formulario.Controls.Add(new Label
                {
                    Name = "ComprasLblEstadoSeguridad", Text = Mensaje, Dock = DockStyle.Top,
                    Height = 40, ForeColor = System.Drawing.Color.Red,
                    BackColor = CapaVista_Componentes.Componentes.ClsTemaCompras.FondoGeneral
                });
                Formulario.Controls[Formulario.Controls.Count - 1].BringToFront();
            };
        }
    }
}
