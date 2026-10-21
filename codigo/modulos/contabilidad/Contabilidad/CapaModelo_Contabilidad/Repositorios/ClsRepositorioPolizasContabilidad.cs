using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioPolizasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioPolizasContabilidad
    {
        private readonly string selectAll;

        public ClsRepositorioPolizasContabilidad()
        {
          selectAll = @"
            SELECT 
                pe.id_poliza,
                pe.codigo_poliza,
                pe.fecha,
                pe.estado,
                pe.periodo_contable,
                pe.origen,
                pe.descripcion AS descripcion_poliza,
                pd.id_poliza_detalle,
                pd.id_cuenta,
                c.codigo AS codigo_cuenta,
                c.nombre AS nombre_cuenta,
                IFNULL(pd.debe, 0) AS debe,
                IFNULL(pd.haber, 0) AS haber
            FROM poliza_encabezado pe
            LEFT JOIN poliza_detalle pd ON pe.id_poliza = pd.id_poliza
            LEFT JOIN cuenta c ON pd.id_cuenta = c.id_cuenta
            ORDER BY pe.fecha DESC, pe.id_poliza DESC, pd.id_poliza_detalle ASC;";
        }


        // Caso especial voy a implementarlo de una forma distinta.
        // Dado a la complejidad de inserciones compuestas mejor lo manejo con un string builder 
        // La alternativa es usar +=
        // Sin embargo dada la longitud es más legible un StringBuilder
        public int ContabilidadFuncAgregar(ClsPolizaContabilidad polizaEncabezado)
        {
            if (polizaEncabezado.Detalle == null || polizaEncabezado.Detalle.Count < 2)
            {
                throw new InvalidOperationException("Una póliza contable debe contener al menos dos líneas de detalle.");
            }

            decimal totalDebe = polizaEncabezado.Detalle.Sum(d => d.Debe);
            decimal totalHaber = polizaEncabezado.Detalle.Sum(d => d.Haber);

            if (totalDebe != totalHaber)
            {
                throw new InvalidOperationException($"La póliza está descuadrada. Total Debe ({totalDebe:C2}) no coincide con Total Haber ({totalHaber:C2}).");
            }

            if (totalDebe <= 0m)
            {
                throw new InvalidOperationException("El importe de la póliza debe ser mayor a cero.");
            }

            var sb = new StringBuilder();
            var parametros = new List<OdbcParameter>();

            sb.AppendLine("INSERT INTO poliza_encabezado (codigo_poliza, fecha, estado, periodo_contable, origen, descripcion) " +
                          "VALUES (?, NOW(), ?, DATE_FORMAT(NOW(), '%Y-%m'), ?, ?);");
            sb.AppendLine("SET @id_nueva_poliza = LAST_INSERT_ID();");

            // Parámetros del encabezado (en el orden exacto de los '?')
            parametros.Add(new OdbcParameter("codigo_poliza", polizaEncabezado.CodigoPoliza ?? string.Empty));
            parametros.Add(new OdbcParameter("estado", polizaEncabezado.Estado ?? "BORRADOR"));
            parametros.Add(new OdbcParameter("origen", polizaEncabezado.Origen ?? "MANUAL"));
            parametros.Add(new OdbcParameter("descripcion", (object)polizaEncabezado.Descripcion ?? DBNull.Value));

            sb.Append("INSERT INTO poliza_detalle (id_poliza, id_cuenta, debe, haber) VALUES ");

            for (int i = 0; i < polizaEncabezado.Detalle.Count; i++)
            {
                var detalle = polizaEncabezado.Detalle[i];
                sb.Append("(@id_nueva_poliza, ?, ?, ?)");
                sb.AppendLine(i < polizaEncabezado.Detalle.Count - 1 ? "," : ";");

                parametros.Add(new OdbcParameter($"id_cuenta_{i}", detalle.IdCuenta));
                parametros.Add(new OdbcParameter($"debe_{i}", detalle.Debe));
                parametros.Add(new OdbcParameter($"haber_{i}", detalle.Haber));
            }

            return ContabilidadFuncEjecutarNonQuery(sb.ToString(), parametros, CommandType.Text);
        }
    }

        public int ContabilidadFuncEditar(ClsPolizaContabilidad Entidad)
        {
            throw new NotImplementedException();
        }

        public int ContabilidadFuncEliminar(ClsPolizaContabilidad Entidad)
        {
            throw new NotImplementedException();
        }

        public List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas()
        {
            throw new NotImplementedException();
        }

        public string[] ContabilidadFuncObtenerEstados()
        {
            throw new NotImplementedException();
        }

        public string[] ContabilidadFuncObtenerOrigenes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClsPolizaContabilidad> ContabilidadFuncObtenerTodos()
        {
            DataTable tabla = ContabilidadFuncEjecutarConsulta(selectAll, CommandType.Text, new List<OdbcParameter>());
            return MapearPolizasDesdeTabla(tabla);
        }

        private List<ClsPolizaContabilidad> MapearPolizasDesdeTabla(DataTable tabla)
        {
            var diccionarioPolizas = new Dictionary<int, ClsPolizaContabilidad>();

            foreach (DataRow fila in tabla.Rows)
            {
                int idPoliza = Convert.ToInt32(fila["id_poliza"]);

                if (!diccionarioPolizas.TryGetValue(idPoliza, out ClsPolizaContabilidad poliza))
                {
                    poliza = new ClsPolizaContabilidad
                    {
                        IdPoliza = idPoliza,
                        CodigoPoliza = fila["codigo_poliza"].ToString(),
                        Fecha = Convert.ToDateTime(fila["fecha"]),
                        Estado = fila["estado"].ToString(),
                        PeriodoContable = fila["periodo_contable"].ToString(),
                        Origen = fila["origen"].ToString(),
                        Descripcion = fila["descripcion_poliza"] != DBNull.Value ? fila["descripcion_poliza"].ToString() : null,
                        Detalle = new List<ClsPolizaDetalleContabilidad>()
                    };
                    diccionarioPolizas.Add(idPoliza, poliza);
                }

                // Si existe línea de detalle (no es NULL producto del LEFT JOIN)
                if (fila["id_poliza_detalle"] != DBNull.Value)
                {
                    poliza.Detalle.Add(new ClsPolizaDetalleContabilidad
                    {
                        IdPolizaDetalle = Convert.ToInt32(fila["id_poliza_detalle"]),
                        IdPoliza = idPoliza,
                        IdCuenta = Convert.ToInt32(fila["id_cuenta"]),
                        Codigo = fila["codigo_cuenta"] != DBNull.Value ? fila["codigo_cuenta"].ToString() : string.Empty,
                        Cuenta = fila["nombre_cuenta"] != DBNull.Value ? fila["nombre_cuenta"].ToString() : string.Empty,
                        Debe = Convert.ToDecimal(fila["debe"]),
                        Haber = Convert.ToDecimal(fila["haber"])
                    });
                }
            }

            return diccionarioPolizas.Values.ToList();
        }
    }
}
