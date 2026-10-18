using System;
using System.Collections.Generic;
using CapaModelo_ComisionVendedores.Entidades;

namespace CapaModelo_ComisionVendedores.Contratos
{
    public interface IRepositorioComision
    {
        List<ClsVentaVendedorBase> ComisionVendedoresMetObtenerVentasPorMetodo(string Metodo, DateTime FechaInicio, DateTime FechaFinExclusiva);
        List<ClsProductoComision> ComisionVendedoresMetConsultarPorcentajes();
        int ComisionVendedoresMetActualizarPorcentaje(List<int> IdsProducto, decimal Porcentaje);
        int ComisionVendedoresMetGuardarComisiones(List<ClsComisionCalculada> Comisiones);
        List<ClsFacturaComision> ComisionVendedoresMetObtenerFacturas();
        List<ClsVendedorComision> ComisionVendedoresMetObtenerVendedores();
        ClsFacturaComision ComisionVendedoresMetObtenerFactura(int IdFactura);
        ClsVendedorComision ComisionVendedoresMetObtenerVendedor(int IdVendedor);
        List<ClsDetalleProductoComision> ComisionVendedoresMetObtenerDetallesFactura(int IdFactura);
    }
}
