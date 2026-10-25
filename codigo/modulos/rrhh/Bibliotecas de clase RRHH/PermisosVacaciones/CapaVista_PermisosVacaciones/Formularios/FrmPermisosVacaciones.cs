using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * ==================================================================
 * Área : Recursos Humanos
 * Autor : Daniella Jurado
 * Carné : 0901-23-3679
 * Fecha : 12/10/2026
 * ==================================================================
 * Propósito :
 *  Prototipo NO funcional de la ventana 9301 - PermisosVacaciones.
 *  Una sola ventana con dos secciones (Permisos y Vacaciones) que se
 *  eligen con botones tipo pestaña. Solo contiene el diseño de la
 *  pantalla (sin datos de ejemplo, eventos, validaciones ni acceso a
 *  datos). La lógica se conectará después a
 *  CapaControlador_PermisosVacaciones.
 * ===================================================================
*/

namespace CapaVista_PermisosVacaciones.Formularios
{
    public partial class FrmPermisosVacaciones : Form
    {
        public FrmPermisosVacaciones()
        {
            InitializeComponent();
        }

        private void RrhhBtnPestañaVacaciones_Click(object sender, EventArgs e)
        {
            RrhhTabPermisosVacaciones.SelectedTab = RrhhTabPagVacaciones;
        }

        private void RrhhBtnPestañaPermisos_Click(object sender, EventArgs e)
        {
            RrhhTabPermisosVacaciones.SelectedTab = RrhhTabPagPermisos;
        }
    }
}
