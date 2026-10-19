// Funcion Implementada por Oskar Cernmeño
// Utilidad: CRUD de consultas, filtro por modulos ERP, obtencion de cuentas padres.
// Pendiente: Nada por el momento.

using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Runtime.InteropServices;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioCuentasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioCuentasContabilidad
    {
        private readonly string selectAll;
        private readonly string selectAllWithModulo;
        private readonly string insertModuloCuenta;
        private readonly string insert;
        private readonly string update;
        private readonly string delete;
        private readonly string deleteModuloCuenta;
        public readonly string selectRecursivo;

        public ClsRepositorioCuentasContabilidad()
        {
            selectAll = "SELECT id_cuenta, nombre, tipo, id_padre, estado FROM cuenta;";

            selectAllWithModulo = @"SELECT c.id_cuenta, c.nombre, c.tipo, c.id_padre, c.estado 
                                    FROM modulo_cuenta mc 
                                    INNER JOIN modulo m ON mc.id_modulo = m.id_modulo 
                                    INNER JOIN cuenta c ON mc.id_cuenta = c.id_cuenta 
                                    INNER JOIN pais p ON mc.id_pais = p.id_pais 
                                    WHERE m.id_modulo = ? 
                                    ORDER BY m.id_modulo, c.id_cuenta;";

            selectRecursivo = @"
                        WITH RECURSIVE JerarquiaPadres AS (
                                SELECT id_cuenta, nombre, tipo, id_padre, estado
                                FROM cuenta
                                WHERE id_cuenta = ?
                                UNION ALL
                                SELECT c.id_cuenta, c.nombre, c.tipo, c.id_padre, c.estado
                                FROM cuenta c
                                INNER JOIN JerarquiaPadres jp ON c.id_cuenta = jp.id_padre
                            )
                            SELECT id_cuenta, nombre, tipo, id_padre, estado 
                            FROM JerarquiaPadres; ";

            insert = "INSERT INTO cuenta (nombre, tipo, id_padre, estado) VALUES (?, ?, ?, ?);";
            insertModuloCuenta = "INSERT INTO modulo_cuenta (id_cuenta, id_modulo, id_pais) VALUES (?, ?, ?);";
            update = "UPDATE cuenta SET nombre = ?, tipo = ?, id_padre = ?, estado = ? WHERE id_cuenta = ?;";
            delete = "DELETE FROM cuenta WHERE id_cuenta = ?;";
            deleteModuloCuenta = "DELETE FROM modulo_cuenta WHERE id_cuenta = ?;";
        }

        public int ContabilidadFuncAgregar(ClsCuentaContabilidad entidad)
        {
            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("nombre", entidad.Nombre),
                new OdbcParameter("tipo", entidad.Tipo),
                new OdbcParameter("id_padre", entidad.IdPadre == 0 ? (object)DBNull.Value : entidad.IdPadre),
                new OdbcParameter("estado", entidad.Estado)
            };

            return ContabilidadFuncEjecutarNonQuery(insert, _parametros, CommandType.Text);
        }

        // 1 = Guatemala
        public int ContabilidadFuncAgregar(ClsCuentaContabilidad entidad, int idModulo, int idPais =1)
        {

            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("id_cuenta", entidad.IdCuenta),
                new OdbcParameter("id_modulo", idModulo),
                new OdbcParameter("id_pais", idPais)
            };

            return ContabilidadFuncEjecutarNonQuery(insertModuloCuenta, _parametros, CommandType.Text);
        }

        public int ContabilidadFuncEditar(ClsCuentaContabilidad entidad)
        {
            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("nombre", entidad.Nombre),
                new OdbcParameter("tipo", entidad.Tipo),
                new OdbcParameter("id_padre", entidad.IdPadre == 0 ? (object)DBNull.Value : entidad.IdPadre),
                new OdbcParameter("estado", entidad.Estado),
                new OdbcParameter("id_cuenta", entidad.IdCuenta) 
            };

            return ContabilidadFuncEjecutarNonQuery(update, _parametros, CommandType.Text);
        }

        public int ContabilidadFuncEliminarModuloAyuda(ClsCuentaContabilidad entidad)
        {
            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("id_cuenta", entidad.IdCuenta)
            };
            return ContabilidadFuncEjecutarNonQuery(deleteModuloCuenta, _parametros, CommandType.Text);
        }

        public int ContabilidadFuncEliminar(ClsCuentaContabilidad entidad)
        {
            ContabilidadFuncEliminarModuloAyuda(entidad);

            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("id_cuenta", entidad.IdCuenta)
            };
            return ContabilidadFuncEjecutarNonQuery(delete, _parametros, CommandType.Text);
        }

        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int idModulo)
        {
            var lstCuentas = new List<ClsCuentaContabilidad>();
            var _parametros = new List<OdbcParameter>
            {
                new OdbcParameter("id_modulo", idModulo)
            };

            var tblCuentas = ContabilidadFuncEjecutarConsulta(selectAllWithModulo, CommandType.Text, _parametros);

            foreach (DataRow row in tblCuentas.Rows)
            {
                var cuenta = new ClsCuentaContabilidad
                {
                    IdCuenta = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    Tipo = row[2].ToString(),
                    IdPadre = row[3] == DBNull.Value ? 0 : Convert.ToInt32(row[3]),
                    Estado = row[4] == DBNull.Value ? 0 : Convert.ToInt32(row[4])
                };
                lstCuentas.Add(cuenta);
            }

            tblCuentas.Clear();
            return lstCuentas;
        }

        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos()
        {
            var lstCuentas = new List<ClsCuentaContabilidad>();
            var tblCuentas = ContabilidadFuncEjecutarConsulta(selectAll, CommandType.Text);

            foreach (DataRow row in tblCuentas.Rows)
            {
                var cuenta = new ClsCuentaContabilidad
                {
                    IdCuenta = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    Tipo = row[2].ToString(),
                    IdPadre = row[3] == DBNull.Value ? 0 : Convert.ToInt32(row[3]),
                    Estado = row[4] == DBNull.Value ? 0 : Convert.ToInt32(row[4])
                };
                lstCuentas.Add(cuenta);
            }

            tblCuentas.Clear();
            return lstCuentas;
        }

        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerPadres(ClsCuentaContabilidad entidad)
        {
            var lstPadres = new List<ClsCuentaContabilidad>();
            var _parametros = new List<OdbcParameter>();

            _parametros.Add(new OdbcParameter("id_cuenta", entidad.IdCuenta));

            var tblPadres = ContabilidadFuncEjecutarConsulta(selectRecursivo, CommandType.Text, _parametros);

            foreach(DataRow row in tblPadres.Rows)
            {
                var cuenta = new ClsCuentaContabilidad
                {
                    IdCuenta = Convert.ToInt32(row[0]),
                    Nombre = row[1].ToString(),
                    Tipo = row[2].ToString(),
                    IdPadre = row[3] == DBNull.Value ? 0 : Convert.ToInt32(row[3]),
                    Estado = row[4] == DBNull.Value ? 0 : Convert.ToInt32(row[4])
                };
                lstPadres.Add(cuenta);
            }
            tblPadres.Clear();
            return lstPadres;

        }

    }
}