/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Mario Taracena
 * Carné : 0901-23-9335
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Mantenimiento del asignación módulo - cuenta - país (tblModuloCuentaPais).
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
    public partial class FrmAsignacionCuentaModulo : Form
    {
        public FrmAsignacionCuentaModulo() : this(ClsPermisosPantallaContabilidad.Total)
        {
        }

        public FrmAsignacionCuentaModulo(ClsPermisosPantallaContabilidad Permisos)
        {
            InitializeComponent();
            ClsPresentacionContabilidad.ContabilidadMetPrepararVentana(this, "Asignación de cuentas por módulo", new Size(1180, 688));
            ContabilidadUsrNavegador.NavegadorMetConfigurar("tblmodulocuentapais", Permisos.IdModulo, Permisos.IdAplicacion);
        }
    }
}
