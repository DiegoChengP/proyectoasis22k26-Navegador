using CapaVista_Componentes.Componentes;
using CapaVista_Navegador;
using System;
using System.Windows.Forms;

namespace CapaVista_Compras
{
    public partial class FrmMantenimientoMetodoPago : Form
    {
        public FrmMantenimientoMetodoPago()
        {
            InitializeComponent();
            ClsPresentacionCompras.ComprasMetFijarTamanoFormulario(this);
            ClsMantenimientoCompras.ComprasProcConfigurar(this, ComprasUsrMantenimiento, "tlbMetodopago",
                CapaControlador_Compras.ClsAplicacionesCompras.MantenimientoMetodoPago);
        }


    }
}
