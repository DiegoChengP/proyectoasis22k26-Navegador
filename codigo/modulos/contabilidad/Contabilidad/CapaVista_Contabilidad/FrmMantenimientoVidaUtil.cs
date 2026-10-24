/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Dylan Hernández
 * Carné : 0901-23-519
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Mantenimiento del maestro de vida útil por tipo de activo (tblMaestroVidaUtil).
 *  Usa el control Navegador: la grilla, los botones y los permisos los maneja el
 *  componente con los IDs de módulo y aplicación registrados en Seguridad.
 * ===================================================================
*/

using System.Drawing;
using System.Windows.Forms;
using CapaControlador_Contabilidad;
using CapaVista_Contabilidad.Properties;

namespace CapaVista_Contabilidad
{
    public partial class FrmMantenimientoVidaUtil : Form
    {
        public FrmMantenimientoVidaUtil() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        public FrmMantenimientoVidaUtil(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Mantenimiento de vida útil", new Size(1180, 688));
            ContabilidadUsrNavegador.NavegadorMetConfigurar("tblmaestrovidautil", Permisos.IdModulo, Permisos.IdAplicacion);
        }
    }
}
