using System;

namespace CapaControlador_OrdenesCompra
{
    public sealed class ClsTotalesArticulo
    {
        public decimal Subtotal { get; internal set; }
        public decimal Impuesto { get; internal set; }
        public decimal Total { get; internal set; }
    }

    public static class ClsCalculoOrdenCompra
    {
        public static ClsTotalesArticulo ComprasFuncCalcularArticulo(decimal Cantidad, decimal PrecioUnitario,
            decimal Descuento, decimal PorcentajeImpuesto)
        {
            decimal Subtotal = Math.Round(Cantidad * PrecioUnitario, 2);
            decimal Impuesto = Math.Round((Subtotal - Descuento) * PorcentajeImpuesto, 2);
            return new ClsTotalesArticulo
            {
                Subtotal = Subtotal,
                Impuesto = Impuesto,
                Total = Subtotal - Descuento + Impuesto
            };
        }
    }
}
