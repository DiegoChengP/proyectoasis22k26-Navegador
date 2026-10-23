using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;

// Oskar Cermeño

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioPolizasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioPolizasContabilidad
    {
        // Estados válidos de poliza_encabezado.estado (ENUM).
        private static readonly string[] _Estados = { "BORRADOR", "APLICADA", "ANULADA" };

        // Orígenes con los que se registran pólizas.
        private static readonly string[] _Origenes = { "MANUAL", "ACTIVOS_FIJOS", "PRESUPUESTO" };

        private readonly string selectAll;
        private readonly string insertEncabezado;
        private readonly string selectUltimoId;
        private readonly string insertDetalle;

        public ClsRepositorioPolizasContabilidad()
        {
            // Encabezado + detalle en una sola consulta; MapearPolizasDesdeTabla agrupa por id_poliza.
            // La tabla cuenta no tiene columna codigo, por eso solo se trae el nombre.
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
                    c.nombre AS nombre_cuenta,
                    IFNULL(pd.debe, 0) AS debe,
                    IFNULL(pd.haber, 0) AS haber
                FROM poliza_encabezado pe
                LEFT JOIN poliza_detalle pd ON pe.id_poliza = pd.id_poliza
                LEFT JOIN cuenta c ON pd.id_cuenta = c.id_cuenta
                ORDER BY pe.fecha DESC, pe.id_poliza DESC, pd.id_poliza_detalle ASC;";

            insertEncabezado = "INSERT INTO poliza_encabezado (codigo_poliza, fecha, estado, periodo_contable, origen, descripcion) VALUES (?, ?, ?, ?, ?, ?);";
            selectUltimoId = "SELECT LAST_INSERT_ID();";
            insertDetalle = "INSERT INTO poliza_detalle (id_poliza, id_cuenta, debe, haber) VALUES (?, ?, ?, ?);";
        }

        // Inserta encabezado y detalle en una sola transacción.
        // Si cualquier línea falla se revierte todo y no queda un encabezado huérfano.
        // Se usan sentencias separadas porque el driver ODBC de MySQL no acepta varias
        // sentencias en un mismo comando a menos que el DSN tenga MULTI_STATEMENTS.
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
                throw new InvalidOperationException($"La póliza está descuadrada. Total Debe ({totalDebe:N2}) no coincide con Total Haber ({totalHaber:N2}).");
            }

            if (totalDebe <= 0m)
            {
                throw new InvalidOperationException("El importe de la póliza debe ser mayor a cero.");
            }

            using (OdbcConnection ConexionActiva = ContabilidadFuncObtenerConexion())
            {
                ConexionActiva.Open();
                using (OdbcTransaction Transaccion = ConexionActiva.BeginTransaction())
                {
                    try
                    {
                        int filasAfectadas;
                        using (OdbcCommand Comando = new OdbcCommand(insertEncabezado, ConexionActiva, Transaccion))
                        {
                            // Parámetros en el orden exacto de los '?'
                            Comando.Parameters.Add("codigo_poliza", OdbcType.VarChar).Value = polizaEncabezado.CodigoPoliza;
                            Comando.Parameters.Add("fecha", OdbcType.Date).Value = polizaEncabezado.Fecha.Date;
                            Comando.Parameters.Add("estado", OdbcType.VarChar).Value = polizaEncabezado.Estado ?? "BORRADOR";
                            Comando.Parameters.Add("periodo_contable", OdbcType.Char).Value = polizaEncabezado.PeriodoContable ?? polizaEncabezado.Fecha.ToString("yyyy-MM");
                            Comando.Parameters.Add("origen", OdbcType.VarChar).Value = polizaEncabezado.Origen ?? "MANUAL";
                            Comando.Parameters.Add("descripcion", OdbcType.VarChar).Value = (object)polizaEncabezado.Descripcion ?? DBNull.Value;
                            filasAfectadas = Comando.ExecuteNonQuery();
                        }

                        int idNuevaPoliza;
                        using (OdbcCommand Comando = new OdbcCommand(selectUltimoId, ConexionActiva, Transaccion))
                        {
                            idNuevaPoliza = Convert.ToInt32(Comando.ExecuteScalar());
                        }

                        foreach (ClsPolizaDetalleContabilidad detalle in polizaEncabezado.Detalle)
                        {
                            using (OdbcCommand Comando = new OdbcCommand(insertDetalle, ConexionActiva, Transaccion))
                            {
                                Comando.Parameters.Add("id_poliza", OdbcType.Int).Value = idNuevaPoliza;
                                Comando.Parameters.Add("id_cuenta", OdbcType.Int).Value = detalle.IdCuenta;
                                Comando.Parameters.Add("debe", OdbcType.Decimal).Value = detalle.Debe;
                                Comando.Parameters.Add("haber", OdbcType.Decimal).Value = detalle.Haber;
                                filasAfectadas += Comando.ExecuteNonQuery();
                            }
                        }

                        Transaccion.Commit();
                        polizaEncabezado.IdPoliza = idNuevaPoliza;
                        return filasAfectadas;
                    }
                    catch
                    {
                        Transaccion.Rollback();
                        throw;
                    }
                }
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

        // Catálogo de cuentas; reutiliza el repositorio de cuentas.
        public List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas()
        {
            return new ClsRepositorioCuentasContabilidad().ContabilidadFuncObtenerTodos().ToList();
        }

        public string[] ContabilidadFuncObtenerEstados()
        {
            return (string[])_Estados.Clone();
        }

        public string[] ContabilidadFuncObtenerOrigenes()
        {
            return (string[])_Origenes.Clone();
        }

        // Todas las pólizas existentes con su detalle.
        public IEnumerable<ClsPolizaContabilidad> ContabilidadFuncObtenerTodos()
        {
            DataTable tabla = ContabilidadFuncEjecutarConsulta(selectAll, CommandType.Text, new List<OdbcParameter>());
            return MapearPolizasDesdeTabla(tabla);
        }

        // Convierte las filas planas del JOIN en pólizas con su lista de detalle.
        private List<ClsPolizaContabilidad> MapearPolizasDesdeTabla(DataTable tabla)
        {
            // Dictionary no garantiza orden; la lista conserva el ORDER BY de la consulta.
            var diccionarioPolizas = new Dictionary<int, ClsPolizaContabilidad>();
            var polizas = new List<ClsPolizaContabilidad>();

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
                    polizas.Add(poliza);
                }

                // Si existe línea de detalle (no es NULL producto del LEFT JOIN)
                if (fila["id_poliza_detalle"] != DBNull.Value)
                {
                    poliza.Detalle.Add(new ClsPolizaDetalleContabilidad
                    {
                        IdPolizaDetalle = Convert.ToInt32(fila["id_poliza_detalle"]),
                        IdPoliza = idPoliza,
                        IdCuenta = Convert.ToInt32(fila["id_cuenta"]),
                        Codigo = string.Empty,
                        Cuenta = fila["nombre_cuenta"] != DBNull.Value ? fila["nombre_cuenta"].ToString() : string.Empty,
                        Debe = Convert.ToDecimal(fila["debe"]),
                        Haber = Convert.ToDecimal(fila["haber"])
                    });
                }
            }

            return polizas;
        }
    }
}
