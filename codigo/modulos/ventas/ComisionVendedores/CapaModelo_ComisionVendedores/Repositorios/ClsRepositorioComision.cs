using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using CapaModelo_ComisionVendedores.Contratos;
using CapaModelo_ComisionVendedores.Entidades;

namespace CapaModelo_ComisionVendedores.Repositorios
{
    public class ClsRepositorioComision : ClsSentencias, IRepositorioComision
    {
        public List<ClsFacturaComision> VentasFuncObtenerFacturas()
        {
            string Comando = @"SELECT f.id, f.fecha, c.nombre AS cliente, f.total,
                COALESCE(SUM(CASE WHEN UPPER(mc.tipo) IN ('PAGO', 'ABONO') THEN mc.monto ELSE 0 END), 0) AS total_pagado,
                COALESCE((SELECT ma.monto FROM MovimientosCliente ma WHERE ma.id_factura = f.id AND UPPER(ma.tipo) IN ('PAGO', 'ABONO') ORDER BY ma.fecha DESC, ma.id DESC LIMIT 1), 0) AS ultimo_abono
                FROM Facturas f INNER JOIN Clientes c ON c.id = f.id_cliente
                LEFT JOIN MovimientosCliente mc ON mc.id_factura = f.id
                GROUP BY f.id, f.fecha, c.nombre, f.total ORDER BY f.fecha DESC, f.id DESC";
            DataTable Datos = VentasFuncEjecucionConsulta(Comando, new List<OdbcParameter>());
            List<ClsFacturaComision> Resultado = new List<ClsFacturaComision>();
            foreach (DataRow Fila in Datos.Rows)
                Resultado.Add(new ClsFacturaComision { IdFactura = Convert.ToInt32(Fila["id"]), Fecha = Convert.ToDateTime(Fila["fecha"]), Cliente = Convert.ToString(Fila["cliente"]), Total = Convert.ToDecimal(Fila["total"]), TotalPagado = Convert.ToDecimal(Fila["total_pagado"]), UltimoAbono = Convert.ToDecimal(Fila["ultimo_abono"]) });
            return Resultado;
        }

        public List<ClsVendedorComision> VentasFuncObtenerVendedores()
        {
            DataTable Datos = VentasFuncEjecucionConsulta("SELECT id, nombre, porcentaje FROM Vendedores ORDER BY nombre", new List<OdbcParameter>());
            List<ClsVendedorComision> Resultado = new List<ClsVendedorComision>();
            foreach (DataRow Fila in Datos.Rows)
                Resultado.Add(new ClsVendedorComision { IdVendedor = Convert.ToInt32(Fila["id"]), NombreVendedor = Convert.ToString(Fila["nombre"]), Porcentaje = Convert.ToDecimal(Fila["porcentaje"]) });
            return Resultado;
        }

        public ClsFacturaComision VentasFuncObtenerFactura(int IdFactura)
        {
            string Comando = @"SELECT f.id, f.fecha, c.nombre AS cliente, f.total,
                COALESCE(SUM(CASE WHEN UPPER(mc.tipo) IN ('PAGO', 'ABONO') THEN mc.monto ELSE 0 END), 0) AS total_pagado,
                COALESCE((SELECT ma.monto FROM MovimientosCliente ma WHERE ma.id_factura = f.id AND UPPER(ma.tipo) IN ('PAGO', 'ABONO') ORDER BY ma.fecha DESC, ma.id DESC LIMIT 1), 0) AS ultimo_abono
                FROM Facturas f INNER JOIN Clientes c ON c.id = f.id_cliente
                LEFT JOIN MovimientosCliente mc ON mc.id_factura = f.id
                WHERE f.id = ? GROUP BY f.id, f.fecha, c.nombre, f.total";
            DataTable Datos = VentasFuncEjecucionConsulta(Comando,
                new List<OdbcParameter> { new OdbcParameter("IdFactura", OdbcType.Int) { Value = IdFactura } });
            if (Datos.Rows.Count == 0) return null;
            DataRow Fila = Datos.Rows[0];
            return new ClsFacturaComision { IdFactura = Convert.ToInt32(Fila["id"]), Fecha = Convert.ToDateTime(Fila["fecha"]), Cliente = Convert.ToString(Fila["cliente"]), Total = Convert.ToDecimal(Fila["total"]), TotalPagado = Convert.ToDecimal(Fila["total_pagado"]), UltimoAbono = Convert.ToDecimal(Fila["ultimo_abono"]) };
        }

        public ClsVendedorComision VentasFuncObtenerVendedor(int IdVendedor)
        {
            DataTable Datos = VentasFuncEjecucionConsulta("SELECT id, nombre, porcentaje FROM Vendedores WHERE id = ?",
                new List<OdbcParameter> { new OdbcParameter("IdVendedor", OdbcType.Int) { Value = IdVendedor } });
            if (Datos.Rows.Count == 0) return null;
            DataRow Fila = Datos.Rows[0];
            return new ClsVendedorComision { IdVendedor = Convert.ToInt32(Fila["id"]), NombreVendedor = Convert.ToString(Fila["nombre"]), Porcentaje = Convert.ToDecimal(Fila["porcentaje"]) };
        }

        public List<ClsDetalleProductoComision> VentasFuncObtenerDetallesFactura(int IdFactura)
        {
            string Comando = @"SELECT p.id AS id_producto, p.nombre, p.marca, p.linea, p.categoria,
                df.cantidad, df.precio_unitario, p.porcentaje
                FROM DetalleFactura df INNER JOIN Productos p ON p.id = df.id_producto
                WHERE df.id_factura = ? ORDER BY p.nombre";
            DataTable Datos = VentasFuncEjecucionConsulta(Comando,
                new List<OdbcParameter> { new OdbcParameter("IdFactura", OdbcType.Int) { Value = IdFactura } });
            List<ClsDetalleProductoComision> Resultado = new List<ClsDetalleProductoComision>();
            foreach (DataRow Fila in Datos.Rows)
                Resultado.Add(new ClsDetalleProductoComision { IdProducto = Convert.ToInt32(Fila["id_producto"]), NombreProducto = Convert.ToString(Fila["nombre"]), Marca = Convert.ToString(Fila["marca"]), Linea = Convert.ToString(Fila["linea"]), Categoria = Convert.ToString(Fila["categoria"]), Cantidad = Convert.ToDecimal(Fila["cantidad"]), PrecioUnitario = Convert.ToDecimal(Fila["precio_unitario"]), Porcentaje = Convert.ToDecimal(Fila["porcentaje"]) });
            return Resultado;
        }

        public List<ClsVentaVendedorBase> VentasFuncObtenerVentasPorMetodo(
            string Metodo, DateTime FechaInicio, DateTime FechaFinExclusiva)
        {
            string Campo = "";
            string Porcentaje = "v.porcentaje";
            string MontoBase = "SUM(f.total)";
            string EnlacesProducto = "";
            string Agrupacion = "v.id, v.nombre, v.porcentaje";
            switch (Metodo)
            {
                case "Vendedor": Campo = "'Todos'"; break;
                case "Marca": Campo = "p.marca"; Porcentaje = "p.porcentaje"; EnlacesProducto = " INNER JOIN DetalleFactura df ON df.id_factura = f.id INNER JOIN Productos p ON p.id = df.id_producto"; MontoBase = "SUM(df.cantidad * df.precio_unitario)"; Agrupacion += ", p.marca, p.porcentaje"; break;
                case "Linea": Campo = "p.linea"; Porcentaje = "p.porcentaje"; EnlacesProducto = " INNER JOIN DetalleFactura df ON df.id_factura = f.id INNER JOIN Productos p ON p.id = df.id_producto"; MontoBase = "SUM(df.cantidad * df.precio_unitario)"; Agrupacion += ", p.linea, p.porcentaje"; break;
                case "Producto": Campo = "p.nombre"; Porcentaje = "p.porcentaje"; EnlacesProducto = " INNER JOIN DetalleFactura df ON df.id_factura = f.id INNER JOIN Productos p ON p.id = df.id_producto"; MontoBase = "SUM(df.cantidad * df.precio_unitario)"; Agrupacion += ", p.nombre, p.porcentaje"; break;
                case "Categoria": Campo = "p.categoria"; Porcentaje = "p.porcentaje"; EnlacesProducto = " INNER JOIN DetalleFactura df ON df.id_factura = f.id INNER JOIN Productos p ON p.id = df.id_producto"; MontoBase = "SUM(df.cantidad * df.precio_unitario)"; Agrupacion += ", p.categoria, p.porcentaje"; break;
                default: throw new ArgumentException("Método de comisión no válido.");
            }
            string ComandoTexto = @"
                SELECT v.id AS id_vendedor,
                       v.nombre AS nombre_vendedor,
                       " + Campo + @" AS elemento_comision,
                       " + Porcentaje + @" AS porcentaje_vendedor,
                       COALESCE(" + MontoBase + @", 0) AS base_ventas
                FROM Vendedores v
                INNER JOIN ClienteVendedor cv ON cv.id_vendedor = v.id
                INNER JOIN Clientes c ON c.id = cv.id_cliente
                INNER JOIN Facturas f ON f.id_cliente = c.id" + EnlacesProducto + @"
                WHERE f.fecha >= ? AND f.fecha < ?
                GROUP BY " + Agrupacion + @"
                ORDER BY v.nombre";

            List<OdbcParameter> Parametros = new List<OdbcParameter>
            {
                new OdbcParameter("FechaInicio", OdbcType.DateTime) { Value = FechaInicio },
                new OdbcParameter("FechaFin", OdbcType.DateTime) { Value = FechaFinExclusiva }
            };

            DataTable TablaDatos = VentasFuncEjecucionConsulta(ComandoTexto, Parametros);
            List<ClsVentaVendedorBase> Resultados = new List<ClsVentaVendedorBase>();

            foreach (DataRow Fila in TablaDatos.Rows)
            {
                Resultados.Add(new ClsVentaVendedorBase
                {
                    IdVendedor = Convert.ToInt32(Fila["id_vendedor"]),
                    NombreVendedor = Convert.ToString(Fila["nombre_vendedor"]),
                    ElementoComision = Convert.ToString(Fila["elemento_comision"]),
                    PorcentajeVendedor = Convert.ToDecimal(Fila["porcentaje_vendedor"]),
                    BaseVentas = Convert.ToDecimal(Fila["base_ventas"])
                });
            }

            return Resultados;
        }

        public List<ClsProductoComision> VentasFuncConsultarPorcentajes()
        {
            string Comando = "SELECT id, nombre, marca, linea, categoria, porcentaje FROM Productos ORDER BY nombre";
            DataTable Datos = VentasFuncEjecucionConsulta(Comando, new List<OdbcParameter>());
            List<ClsProductoComision> Lista = new List<ClsProductoComision>();
            foreach (DataRow Fila in Datos.Rows)
                Lista.Add(new ClsProductoComision { IdProducto = Convert.ToInt32(Fila["id"]), NombreProducto = Convert.ToString(Fila["nombre"]), Marca = Convert.ToString(Fila["marca"]), Linea = Convert.ToString(Fila["linea"]), Categoria = Convert.ToString(Fila["categoria"]), Porcentaje = Convert.ToDecimal(Fila["porcentaje"]) });
            return Lista;
        }

        public int VentasFuncActualizarPorcentaje(List<int> IdsProducto, decimal Porcentaje)
        {
            if (IdsProducto == null || IdsProducto.Count == 0)
                return 0;

            string Marcadores = string.Join(", ", IdsProducto.ConvertAll(IdProducto => "?"));
            string ComandoTexto = "UPDATE Productos SET porcentaje = ? WHERE id IN (" + Marcadores + ")";
            using (OdbcConnection Conexion = VentasFuncObtenerConexion())
            {
                Conexion.Open();
                using (OdbcCommand Comando = new OdbcCommand(ComandoTexto, Conexion))
                {
                    Comando.Parameters.Add(new OdbcParameter("Porcentaje", OdbcType.Decimal) { Precision = 7, Scale = 4, Value = Porcentaje });
                    foreach (int IdProducto in IdsProducto)
                        Comando.Parameters.Add(new OdbcParameter("IdProducto", OdbcType.Int) { Value = IdProducto });
                    return Comando.ExecuteNonQuery();
                }
            }
        }

        public int VentasFuncGuardarComisiones(List<ClsComisionCalculada> Comisiones)
        {
            if (Comisiones == null || Comisiones.Count == 0)
                return 0;

            string ComandoActualizar = @"
                UPDATE ComisionCalculada
                SET monto = ?
                WHERE id_vendedor = ? AND fecha_ini = ? AND fecha_fin = ?
                  AND metodo = ? AND `base` = ?";
            string ComandoInsertar = @"
                INSERT INTO ComisionCalculada
                    (id_vendedor, fecha_ini, fecha_fin, metodo, `base`, monto)
                VALUES (?, ?, ?, ?, ?, ?)";

            using (OdbcConnection ConexionActiva = VentasFuncObtenerConexion())
            {
                ConexionActiva.Open();
                using (OdbcTransaction Transaccion = ConexionActiva.BeginTransaction())
                {
                    try
                    {
                        foreach (ClsComisionCalculada Comision in Comisiones)
                        {
                            int RegistrosActualizados = VentasFuncEjecucionNonQuery(
                                ComandoActualizar,
                                VentasFuncParametrosActualizar(Comision),
                                ConexionActiva,
                                Transaccion);

                            if (RegistrosActualizados == 0)
                            {
                                VentasFuncEjecucionNonQuery(
                                    ComandoInsertar,
                                    VentasFuncParametrosInsertar(Comision),
                                    ConexionActiva,
                                    Transaccion);
                            }
                        }

                        Transaccion.Commit();
                        return Comisiones.Count;
                    }
                    catch
                    {
                        Transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        private List<OdbcParameter> VentasFuncParametrosActualizar(ClsComisionCalculada Comision)
        {
            return new List<OdbcParameter>
            {
                VentasFuncParametroMonto(Comision.Monto),
                new OdbcParameter("IdVendedor", OdbcType.Int) { Value = Comision.IdVendedor },
                VentasFuncParametroFecha("FechaInicio", Comision.FechaInicio),
                VentasFuncParametroFecha("FechaFin", Comision.FechaFin),
                new OdbcParameter("Metodo", OdbcType.VarChar, 30) { Value = Comision.Metodo },
                new OdbcParameter("BaseCalculo", OdbcType.VarChar, 20) { Value = Comision.BaseCalculo }
            };
        }

        private List<OdbcParameter> VentasFuncParametrosInsertar(ClsComisionCalculada Comision)
        {
            return new List<OdbcParameter>
            {
                new OdbcParameter("IdVendedor", OdbcType.Int) { Value = Comision.IdVendedor },
                VentasFuncParametroFecha("FechaInicio", Comision.FechaInicio),
                VentasFuncParametroFecha("FechaFin", Comision.FechaFin),
                new OdbcParameter("Metodo", OdbcType.VarChar, 30) { Value = Comision.Metodo },
                new OdbcParameter("BaseCalculo", OdbcType.VarChar, 20) { Value = Comision.BaseCalculo },
                VentasFuncParametroMonto(Comision.Monto)
            };
        }

        private OdbcParameter VentasFuncParametroFecha(string Nombre, DateTime Fecha)
        {
            return new OdbcParameter(Nombre, OdbcType.Date) { Value = Fecha.Date };
        }

        private OdbcParameter VentasFuncParametroMonto(decimal Monto)
        {
            return new OdbcParameter("Monto", OdbcType.Decimal) { Precision = 18, Scale = 2, Value = Monto };
        }
    }
}
