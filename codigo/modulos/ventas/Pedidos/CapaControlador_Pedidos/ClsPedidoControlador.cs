using System;
using System.Collections.Generic;
using System.Linq;
using Modelo = CapaModelo_Pedidos;

namespace CapaControlador_Pedidos
{
    public sealed class ClsPedidoControlador
    {
        private readonly Modelo.ClsPedidoRepositorio _Repositorio;

        public ClsPedidoControlador()
        {
            _Repositorio = new Modelo.ClsPedidoRepositorio();
        }

        public List<ClsCatalogoPedido> VentasFuncClientes()
        {
            return VentasFuncConvertirCatalogo(_Repositorio.VentasFuncClientes());
        }

        public List<ClsCatalogoPedido> VentasFuncVendedores()
        {
            return VentasFuncConvertirCatalogo(_Repositorio.VentasFuncVendedores());
        }

        private static List<ClsCatalogoPedido> VentasFuncConvertirCatalogo(List<Modelo.ClsCatalogoPedido> Datos)
        {
            return Datos.Select(Dato => new ClsCatalogoPedido { Id = Dato.Id, Nombre = Dato.Nombre }).ToList();
        }

        public List<ClsCotizacionPedido> VentasFuncCotizaciones()
        {
            // Integrar aquí la DLL del compañero: devolver cotizaciones vigentes
            // con Número, cliente, vendedor, moneda, cambio, vencimiento y líneas.
            return new List<ClsCotizacionPedido>();
        }

        public List<ClsLineaPedido> VentasFuncProductos(DateTime Fecha)
        {
            return _Repositorio.VentasFuncProductosPublicos(Fecha)
                .Select(VentasFuncConvertirLinea).ToList();
        }

        private static ClsLineaPedido VentasFuncConvertirLinea(Modelo.ClsLineaPedido Dato)
        {
            return new ClsLineaPedido {
                ProductoId = Dato.ProductoId, CotizacionDetalleId = Dato.CotizacionDetalleId,
                Codigo = Dato.Codigo, Descripcion = Dato.Descripcion, Tipo = Dato.Tipo,
                Cantidad = Dato.Cantidad, Precio = Dato.Precio, Descuento = Dato.Descuento,
                PorcentajeImpuesto = Dato.PorcentajeImpuesto
            };
        }

        public int VentasFuncMonedaPublica() { return _Repositorio.VentasFuncMonedaPublica(); }

        public static void VentasMetCalcular(ClsLineaPedido Linea)
        {
            decimal Base = Math.Round(Linea.Cantidad * Linea.Precio, 2, MidpointRounding.AwayFromZero);
            Linea.Impuesto = Math.Round((Base - Linea.Descuento) * Linea.PorcentajeImpuesto / 100m, 2, MidpointRounding.AwayFromZero);
            Linea.Total = Base - Linea.Descuento + Linea.Impuesto;
        }

        public int VentasFuncGuardar(ClsPedido Pedido)
        {
            var Registro = new Modelo.ClsPedido {
                Numero = Pedido.Numero.Trim(), CotizacionId = Pedido.CotizacionId,
                Referencia = Pedido.Referencia, ClienteId = Pedido.ClienteId,
                VendedorId = Pedido.VendedorId, MonedaId = Pedido.MonedaId,
                Fecha = Pedido.Fecha.Date,
                Entrega = Pedido.Entrega.Date, Vencimiento = Pedido.Vencimiento.Date,
                TipoCambio = Pedido.TipoCambio
            };
            foreach (ClsLineaPedido Linea in Pedido.Lineas)
            {
                VentasMetCalcular(Linea);
                Registro.Lineas.Add(new Modelo.ClsLineaPedido {
                    ProductoId = Linea.ProductoId, CotizacionDetalleId = Linea.CotizacionDetalleId,
                    Descripcion = Linea.Descripcion, Tipo = Linea.Tipo,
                    Cantidad = Linea.Cantidad, Precio = Linea.Precio, Descuento = Linea.Descuento,
                    PorcentajeImpuesto = Linea.PorcentajeImpuesto, Impuesto = Linea.Impuesto, Total = Linea.Total
                });
            }
            return _Repositorio.VentasFuncGuardar(Registro);
        }

    }
}
