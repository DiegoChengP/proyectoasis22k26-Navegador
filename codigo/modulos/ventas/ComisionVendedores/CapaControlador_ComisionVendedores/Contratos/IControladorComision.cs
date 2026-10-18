using System;
using System.Collections.Generic;
using CapaControlador_ComisionVendedores.DTO;
using CapaControlador_ComisionVendedores.Enumeraciones;

namespace CapaControlador_ComisionVendedores.Contratos
{
    public interface IControladorComision
    {
        List<ClsComisionVendedorResultado> ComisionVendedoresMetCalcular(
            EnumMetodoComision Metodo,
            EnumBaseComision BaseCalculo,
            DateTime FechaInicio,
            DateTime FechaFin);

        int ComisionVendedoresMetGuardar(
            List<ClsComisionVendedorResultado> Resultados,
            DateTime FechaInicio,
            DateTime FechaFin,
            EnumMetodoComision Metodo,
            EnumBaseComision BaseCalculo);

        List<ClsProductoPorcentaje> ComisionVendedoresMetConsultarPorcentajes(EnumMetodoComision Metodo);
        int ComisionVendedoresMetActualizarPorcentaje(EnumMetodoComision Metodo, List<int> IdsProducto, decimal Porcentaje);
        List<ClsFacturaComisionVista> ComisionVendedoresMetObtenerFacturas();
        List<ClsVendedorComisionVista> ComisionVendedoresMetObtenerVendedores();
        List<ClsComisionFacturaDetalle> ComisionVendedoresMetCalcularFactura(int IdFactura, int IdVendedor, EnumMetodoComision Metodo);
    }
}
