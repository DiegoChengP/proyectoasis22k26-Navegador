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

        public List<ClsCatalogoPedido> VentasMetClientes()
        {
            return VentasMetConvertirCatalogo(_Repositorio.VentasMetClientes());
        }

        public List<ClsCatalogoPedido> VentasMetVendedores()
        {
            return VentasMetConvertirCatalogo(_Repositorio.VentasMetVendedores());
        }

        private static List<ClsCatalogoPedido> VentasMetConvertirCatalogo(List<Modelo.ClsCatalogoPedido> Datos)
        {
            return Datos.Select(Dato => new ClsCatalogoPedido { Id = Dato.Id, Nombre = Dato.Nombre }).ToList();
        }

        public List<ClsCotizacionPedido> VentasMetCotizaciones()
        {
            // Integrar aquí la DLL del compañero: devolver cotizaciones vigentes
            // con Número, cliente, vendedor, moneda, cambio, vencimiento y líneas.
            return new List<ClsCotizacionPedido>();
        }

        public List<ClsLineaPedido> VentasMetProductos(DateTime Fecha)
        {
            return _Repositorio.VentasMetProductosPublicos(Fecha)
                .Select(VentasMetConvertirLinea).ToList();
        }

        private static ClsLineaPedido VentasMetConvertirLinea(Modelo.ClsLineaPedido Dato)
        {
            return new ClsLineaPedido {
                ProductoId = Dato.ProductoId, CotizacionDetalleId = Dato.CotizacionDetalleId,
                Codigo = Dato.Codigo, Descripcion = Dato.Descripcion, Tipo = Dato.Tipo,
                Cantidad = Dato.Cantidad, Precio = Dato.Precio, Descuento = Dato.Descuento,
                PorcentajeImpuesto = Dato.PorcentajeImpuesto
            };
        }

        public int VentasMetMonedaPublica() { return _Repositorio.VentasMetMonedaPublica(); }

        public static void VentasMetCalcular(ClsLineaPedido Linea)
        {
            decimal Base = Math.Round(Linea.Cantidad * Linea.Precio, 2, MidpointRounding.AwayFromZero);
            Linea.Impuesto = Math.Round((Base - Linea.Descuento) * Linea.PorcentajeImpuesto / 100m, 2, MidpointRounding.AwayFromZero);
            Linea.Total = Base - Linea.Descuento + Linea.Impuesto;
        }

        public int VentasMetGuardar(ClsPedido Pedido)
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
            return _Repositorio.VentasMetGuardar(Registro);
        }

    }
}
