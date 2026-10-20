using System.Collections.Generic;

using CapaControlador_ListadoPrecios;

namespace CapaControlador_ListadoPrecios.Contratos
{
    /// <summary>Contrato de operaciones disponibles para el Listado de Precios.</summary>
    public interface ICtrlListadoPrecios
    {
        List<ClsPrecioCliente> ConsultarListado();
        decimal CalcularPrecioFinal(decimal precioBase, decimal descuento);
        bool Guardar(string tipoCliente, string tipoProducto, decimal porcentaje, decimal precioBase, out string mensaje);
    }
}
