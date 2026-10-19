using System;
using System.Collections.Generic;
using System.Linq;
using CapaModelo_ListadoPrecios;
using CapaControlador_ListadoPrecios.Contratos;

namespace CapaControlador_ListadoPrecios
{
    // Controlador del punto 2.1: validación, cálculo y coordinación con el modelo.
    public class ClsCtrlListadoPrecios : ICtrlListadoPrecios
    {
        private readonly ClsModeloListadoPrecios _modelo;
        public string MensajeEstado { get { return _modelo.MensajeEstado; } }
        public bool UsaBaseDeDatos { get { return _modelo.UsaBaseDeDatos; } }

        public ClsCtrlListadoPrecios() { _modelo = new ClsModeloListadoPrecios(); }

        public List<ClsPrecioCliente> ConsultarListado()
        {
            return _modelo.ObtenerListado().OrderBy(x => x.TipoCliente).ThenBy(x => x.TipoProducto).Select(x => new ClsPrecioCliente { TipoCliente = x.TipoCliente, TipoProducto = x.TipoProducto, PorcentajeDescuento = x.PorcentajeDescuento, PrecioBase = x.PrecioBase, PrecioFinal = x.PrecioFinal }).ToList();
        }

        public decimal CalcularPrecioFinal(decimal precioBase, decimal descuento)
        {
            if (precioBase < 0) throw new ArgumentOutOfRangeException("precioBase", "El precio base no puede ser negativo.");
            if (descuento < 0 || descuento > 100) throw new ArgumentOutOfRangeException("descuento", "El descuento debe estar entre 0 y 100 %.");
            return ClsModeloListadoPrecios.CalcularPrecioFinal(precioBase, descuento);
        }

        public bool Guardar(string tipoCliente, string tipoProducto, decimal porcentaje, decimal precioBase, out string mensaje)
        {
            if (string.IsNullOrWhiteSpace(tipoCliente)) { mensaje = "Seleccione un tipo de cliente."; return false; }
            if (string.IsNullOrWhiteSpace(tipoProducto)) { mensaje = "Seleccione un tipo de producto."; return false; }
            if (precioBase < 0) { mensaje = "El precio base no puede ser negativo."; return false; }
            if (porcentaje < 0 || porcentaje > 100) { mensaje = "El descuento debe estar entre 0 y 100 %."; return false; }
            CapaModelo_ListadoPrecios.Entidades.ClsPrecioCliente registro = new CapaModelo_ListadoPrecios.Entidades.ClsPrecioCliente
            {
                TipoCliente = tipoCliente.Trim(), TipoProducto = tipoProducto.Trim(),
                PorcentajeDescuento = porcentaje, PrecioBase = precioBase,
                PrecioFinal = CalcularPrecioFinal(precioBase, porcentaje)
            };
            return _modelo.Guardar(registro, out mensaje);
        }
    }
}
