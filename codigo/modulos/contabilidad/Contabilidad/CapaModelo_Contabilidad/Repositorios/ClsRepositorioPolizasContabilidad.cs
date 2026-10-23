/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Repositorio de pólizas sobre tblPolizaEncabezado y tblPolizaDetalle:
 *  lista las pólizas con sus líneas, agrega una póliza completa en una
 *  transacción, cambia su estado o descripción y la anula.
 * Reglas especificas:
 *  Insert y listado los creó Oskar Cermeño; se reescribieron para las
 *  tablas de EST-01. El código POL-AAAA-MM-#### lo genera la base con
 *  fnGenerarCodigoPoliza. La póliza se inserta en BORRADOR (los
 *  triggers solo aceptan líneas en ese estado) y después se le da su
 *  estado final. Eliminar es baja lógica: nunca se usa DELETE.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioPolizasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioPolizasContabilidad
    {
        // Pólizas activas con sus líneas y la cuenta de cada línea.
        private const string _SelectTodos =
            "SELECT pe.Pk_PolizaEncabezado, pe.codigoPoliza, pe.fechaPoliza, pe.periodoContablePoliza, pe.estadoPoliza, " +
            "pe.origenPoliza, pe.documentoOrigenPoliza, pe.descripcionPoliza, " +
            "pd.Pk_PolizaDetalle, pd.Fk_Cuenta, pd.lineaDetalle, pd.debeDetalle, pd.haberDetalle, pd.descripcionDetalle, " +
            "c.codigoCuenta, c.nombreCuenta " +
            "FROM tblPolizaEncabezado pe " +
            "LEFT JOIN tblPolizaDetalle pd ON pd.Fk_PolizaEncabezado = pe.Pk_PolizaEncabezado AND pd.is_active = TRUE " +
            "LEFT JOIN tblCuenta c ON c.Pk_Cuenta = pd.Fk_Cuenta " +
            "WHERE pe.is_active = TRUE " +
            "ORDER BY pe.fechaPoliza DESC, pe.Pk_PolizaEncabezado DESC, pd.lineaDetalle";

        // Encabezado nuevo (siempre BORRADOR).
        private const string _InsertEncabezado =
            "INSERT INTO tblPolizaEncabezado (codigoPoliza, fechaPoliza, periodoContablePoliza, estadoPoliza, origenPoliza, documentoOrigenPoliza, descripcionPoliza) " +
            "VALUES (?, ?, ?, 'BORRADOR', ?, ?, ?)";

        // Línea nueva.
        private const string _InsertDetalle =
            "INSERT INTO tblPolizaDetalle (Fk_PolizaEncabezado, Fk_Cuenta, lineaDetalle, debeDetalle, haberDetalle, descripcionDetalle) " +
            "VALUES (?, ?, ?, ?, ?, ?)";

        // Cambio de estado y descripción.
        private const string _UpdateEncabezado =
            "UPDATE tblPolizaEncabezado SET estadoPoliza = ?, descripcionPoliza = ? WHERE Pk_PolizaEncabezado = ?";

        // Baja lógica: anula y desactiva.
        private const string _Anular =
            "UPDATE tblPolizaEncabezado SET estadoPoliza = 'ANULADA', is_active = FALSE WHERE Pk_PolizaEncabezado = ?";

        // Agrega la póliza con sus líneas; devuelve 1 si se grabó.
        public int ContabilidadFuncAgregar(ClsPolizaContabilidad Entidad)
        {
            if (Entidad.Detalle == null || Entidad.Detalle.Count < 2)
                throw new InvalidOperationException("Una póliza contable debe contener al menos dos líneas de detalle.");
            decimal TotalDebe = Entidad.Detalle.Sum(Linea => Linea.Debe);
            if (TotalDebe <= 0m || TotalDebe != Entidad.Detalle.Sum(Linea => Linea.Haber))
                throw new InvalidOperationException("La póliza no cuadra: el total del debe debe ser igual al del haber y mayor que cero.");

            string Periodo = Entidad.Fecha.ToString("yyyy-MM");
            using (OdbcConnection Conexion = ContabilidadFuncObtenerConexion())
            {
                Conexion.Open();
                using (OdbcTransaction Transaccion = Conexion.BeginTransaction())
                {
                    try
                    {
                        // El correlativo lo genera la base.
                        string Codigo;
                        using (OdbcCommand Comando = new OdbcCommand("SELECT fnGenerarCodigoPoliza(?)", Conexion, Transaccion))
                        {
                            Comando.Parameters.AddWithValue("Periodo", Periodo);
                            Codigo = Convert.ToString(Comando.ExecuteScalar());
                        }

                        using (OdbcCommand Comando = new OdbcCommand(_InsertEncabezado, Conexion, Transaccion))
                        {
                            Comando.Parameters.AddWithValue("Codigo", Codigo);
                            Comando.Parameters.Add(new OdbcParameter("Fecha", OdbcType.Date) { Value = Entidad.Fecha.Date });
                            Comando.Parameters.AddWithValue("Periodo", Periodo);
                            Comando.Parameters.AddWithValue("Origen", string.IsNullOrWhiteSpace(Entidad.Origen) ? "MANUAL" : Entidad.Origen);
                            Comando.Parameters.AddWithValue("Documento", string.IsNullOrWhiteSpace(Entidad.DocumentoOrigen) ? (object)DBNull.Value : Entidad.DocumentoOrigen);
                            Comando.Parameters.AddWithValue("Descripcion", string.IsNullOrWhiteSpace(Entidad.Descripcion) ? (object)DBNull.Value : Entidad.Descripcion);
                            Comando.ExecuteNonQuery();
                        }

                        int IdPoliza;
                        using (OdbcCommand Comando = new OdbcCommand("SELECT LAST_INSERT_ID()", Conexion, Transaccion))
                            IdPoliza = Convert.ToInt32(Comando.ExecuteScalar());

                        int Linea = 1;
                        foreach (ClsPolizaDetalleContabilidad Detalle in Entidad.Detalle)
                        {
                            using (OdbcCommand Comando = new OdbcCommand(_InsertDetalle, Conexion, Transaccion))
                            {
                                Comando.Parameters.AddWithValue("Poliza", IdPoliza);
                                Comando.Parameters.AddWithValue("Cuenta", Detalle.IdCuenta);
                                Comando.Parameters.AddWithValue("Linea", Linea++);
                                Comando.Parameters.Add(new OdbcParameter("Debe", OdbcType.Decimal) { Precision = 12, Scale = 4, Value = Detalle.Debe });
                                Comando.Parameters.Add(new OdbcParameter("Haber", OdbcType.Decimal) { Precision = 12, Scale = 4, Value = Detalle.Haber });
                                Comando.Parameters.AddWithValue("DescripcionDetalle", string.IsNullOrWhiteSpace(Detalle.DescripcionDetalle) ? (object)DBNull.Value : Detalle.DescripcionDetalle);
                                Comando.ExecuteNonQuery();
                            }
                        }

                        // Con las líneas ya grabadas se le da el estado final.
                        if (!string.IsNullOrWhiteSpace(Entidad.Estado) && Entidad.Estado != "BORRADOR")
                        {
                            using (OdbcCommand Comando = new OdbcCommand(_UpdateEncabezado, Conexion, Transaccion))
                            {
                                Comando.Parameters.AddWithValue("Estado", Entidad.Estado);
                                Comando.Parameters.AddWithValue("Descripcion", string.IsNullOrWhiteSpace(Entidad.Descripcion) ? (object)DBNull.Value : Entidad.Descripcion);
                                Comando.Parameters.AddWithValue("Poliza", IdPoliza);
                                Comando.ExecuteNonQuery();
                            }
                        }

                        Transaccion.Commit();
                        Entidad.IdPoliza = IdPoliza;
                        Entidad.CodigoPoliza = Codigo;
                        Entidad.PeriodoContable = Periodo;
                        return 1;
                    }
                    catch
                    {
                        Transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        // Cambia el estado y la descripción (los triggers validan la transición).
        public int ContabilidadFuncEditar(ClsPolizaContabilidad Entidad)
        {
            return ContabilidadFuncEjecutarNonQuery(_UpdateEncabezado, new List<OdbcParameter>
            {
                new OdbcParameter("Estado", Entidad.Estado),
                new OdbcParameter("Descripcion", string.IsNullOrWhiteSpace(Entidad.Descripcion) ? (object)DBNull.Value : Entidad.Descripcion),
                new OdbcParameter("Poliza", Entidad.IdPoliza)
            }, CommandType.Text);
        }

        // Anula la póliza (baja lógica).
        public int ContabilidadFuncEliminar(ClsPolizaContabilidad Entidad)
        {
            return ContabilidadFuncEjecutarNonQuery(_Anular, new List<OdbcParameter> { new OdbcParameter("Poliza", Entidad.IdPoliza) }, CommandType.Text);
        }

        // Todas las pólizas activas con sus líneas.
        public IEnumerable<ClsPolizaContabilidad> ContabilidadFuncObtenerTodos()
        {
            using (DataTable Tabla = ContabilidadFuncEjecutarConsulta(_SelectTodos, CommandType.Text))
            {
                Dictionary<int, ClsPolizaContabilidad> Polizas = new Dictionary<int, ClsPolizaContabilidad>();
                List<ClsPolizaContabilidad> Orden = new List<ClsPolizaContabilidad>();
                foreach (DataRow Fila in Tabla.Rows)
                {
                    int IdPoliza = Convert.ToInt32(Fila["Pk_PolizaEncabezado"]);
                    ClsPolizaContabilidad Poliza;
                    if (!Polizas.TryGetValue(IdPoliza, out Poliza))
                    {
                        Poliza = new ClsPolizaContabilidad
                        {
                            IdPoliza = IdPoliza,
                            CodigoPoliza = Convert.ToString(Fila["codigoPoliza"]),
                            Fecha = Convert.ToDateTime(Fila["fechaPoliza"]),
                            PeriodoContable = Convert.ToString(Fila["periodoContablePoliza"]),
                            Estado = Convert.ToString(Fila["estadoPoliza"]),
                            Origen = Convert.ToString(Fila["origenPoliza"]),
                            DocumentoOrigen = Fila["documentoOrigenPoliza"] == DBNull.Value ? null : Convert.ToString(Fila["documentoOrigenPoliza"]),
                            Descripcion = Fila["descripcionPoliza"] == DBNull.Value ? null : Convert.ToString(Fila["descripcionPoliza"])
                        };
                        Polizas.Add(IdPoliza, Poliza);
                        Orden.Add(Poliza);
                    }
                    if (Fila["Pk_PolizaDetalle"] == DBNull.Value) continue;
                    Poliza.Detalle.Add(new ClsPolizaDetalleContabilidad
                    {
                        IdPolizaDetalle = Convert.ToInt32(Fila["Pk_PolizaDetalle"]),
                        IdPoliza = IdPoliza,
                        IdCuenta = Convert.ToInt32(Fila["Fk_Cuenta"]),
                        Linea = Convert.ToInt32(Fila["lineaDetalle"]),
                        Debe = Convert.ToDecimal(Fila["debeDetalle"]),
                        Haber = Convert.ToDecimal(Fila["haberDetalle"]),
                        DescripcionDetalle = Fila["descripcionDetalle"] == DBNull.Value ? null : Convert.ToString(Fila["descripcionDetalle"]),
                        Codigo = Convert.ToString(Fila["codigoCuenta"]),
                        Cuenta = Convert.ToString(Fila["nombreCuenta"])
                    });
                }
                return Orden;
            }
        }

        // Catálogo de cuentas activas.
        public List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas()
        {
            return new ClsRepositorioCuentasContabilidad().ContabilidadFuncObtenerTodos().Where(Cuenta => Cuenta.Activo).ToList();
        }

        // Estados que acepta la base (restricción ckPolizaEstado).
        public string[] ContabilidadFuncObtenerEstados()
        {
            return new[] { "BORRADOR", "APLICADA", "ANULADA" };
        }

        // Orígenes que acepta la base (restricción ckPolizaOrigen).
        public string[] ContabilidadFuncObtenerOrigenes()
        {
            return new[] { "MANUAL", "BANCOS", "ACTIVOS_FIJOS", "PRESUPUESTO", "NOMINA", "VENTAS", "COMPRAS" };
        }

        // Nombre del módulo ERP (solo módulos activos).
        public string ContabilidadFuncObtenerNombreModulo(int IdModulo)
        {
            using (DataTable Tabla = ContabilidadFuncEjecutarConsulta("SELECT nombreModulo FROM tblModuloContable WHERE Pk_Modulo = ? AND is_active = TRUE",
                CommandType.Text, new List<OdbcParameter> { new OdbcParameter("Modulo", IdModulo) }))
                return Tabla.Rows.Count == 0 ? string.Empty : Convert.ToString(Tabla.Rows[0][0]);
        }
    }
}
