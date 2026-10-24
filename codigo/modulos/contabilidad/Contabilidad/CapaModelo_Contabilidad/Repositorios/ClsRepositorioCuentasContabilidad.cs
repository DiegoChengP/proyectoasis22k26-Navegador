/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Consulta el catálogo de cuentas (tblCuenta): todas, las asignadas
 *  a un módulo ERP, las imputables de un módulo y los ancestros de
 *  una cuenta.
 * Reglas especificas:
 *  Repositorio creado por Oskar Cermeño (filtro por módulo y consulta
 *  recursiva de padres). Se reescribió para las tablas de EST-01 y
 *  quedó de solo lectura: altas, cambios y bajas de cuentas se hacen
 *  con el control Navegador. Imputable = activa y sin hijas activas.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioCuentasContabilidad : ClsRepositorioMaestroContabilidad, IRepositorioCuentasContabilidad
    {
        // Columnas que se leen siempre, en este orden.
        private const string _Columnas = "c.Pk_Cuenta, c.codigoCuenta, c.nombreCuenta, c.tipoCuenta, c.Fk_Cuenta_Padre, c.is_active";

        // Todas las cuentas.
        private readonly string _SelectTodas;
        // Cuentas asignadas a un módulo (sin repetir por país).
        private readonly string _SelectPorModulo;
        // Cuentas del módulo que no tienen hijas activas.
        private readonly string _SelectImputables;
        // La cuenta y sus ancestros.
        private readonly string _SelectPadres;

        // Arma las sentencias SQL.
        public ClsRepositorioCuentasContabilidad()
        {
            _SelectTodas = "SELECT " + _Columnas + " FROM tblCuenta c ORDER BY c.codigoCuenta";

            _SelectPorModulo =
                "SELECT DISTINCT " + _Columnas + " FROM tblModuloCuentaPais mc " +
                "INNER JOIN tblModuloContable m ON m.Pk_Modulo = mc.Fk_Modulo " +
                "INNER JOIN tblCuenta c ON c.Pk_Cuenta = mc.Fk_Cuenta " +
                "WHERE mc.Fk_Modulo = ? AND mc.is_active = TRUE AND m.is_active = TRUE AND c.is_active = TRUE " +
                "ORDER BY c.codigoCuenta";

            _SelectImputables =
                "SELECT DISTINCT " + _Columnas + " FROM tblModuloCuentaPais mc " +
                "INNER JOIN tblModuloContable m ON m.Pk_Modulo = mc.Fk_Modulo " +
                "INNER JOIN tblCuenta c ON c.Pk_Cuenta = mc.Fk_Cuenta " +
                "WHERE mc.Fk_Modulo = ? AND mc.is_active = TRUE AND m.is_active = TRUE AND c.is_active = TRUE " +
                "AND NOT EXISTS (SELECT 1 FROM tblCuenta h WHERE h.Fk_Cuenta_Padre = c.Pk_Cuenta AND h.is_active = TRUE) " +
                "ORDER BY c.codigoCuenta";

            _SelectPadres =
                "WITH RECURSIVE jerarquiaPadres AS (" +
                "SELECT Pk_Cuenta, codigoCuenta, nombreCuenta, tipoCuenta, Fk_Cuenta_Padre, is_active FROM tblCuenta WHERE Pk_Cuenta = ? " +
                "UNION ALL " +
                "SELECT c.Pk_Cuenta, c.codigoCuenta, c.nombreCuenta, c.tipoCuenta, c.Fk_Cuenta_Padre, c.is_active " +
                "FROM tblCuenta c INNER JOIN jerarquiaPadres jp ON c.Pk_Cuenta = jp.Fk_Cuenta_Padre) " +
                "SELECT Pk_Cuenta, codigoCuenta, nombreCuenta, tipoCuenta, Fk_Cuenta_Padre, is_active FROM jerarquiaPadres";
        }

        // Todas las cuentas del catálogo.
        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos()
        {
            return ContabilidadFuncMapear(ContabilidadFuncEjecutarConsulta(_SelectTodas, CommandType.Text));
        }

        // Cuentas asignadas a un módulo ERP.
        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerTodos(int IdModulo)
        {
            return ContabilidadFuncMapear(ContabilidadFuncEjecutarConsulta(_SelectPorModulo, CommandType.Text,
                new List<OdbcParameter> { new OdbcParameter("Modulo", IdModulo) }));
        }

        // Cuentas del módulo que se pueden usar en una póliza.
        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerImputables(int IdModulo)
        {
            return ContabilidadFuncMapear(ContabilidadFuncEjecutarConsulta(_SelectImputables, CommandType.Text,
                new List<OdbcParameter> { new OdbcParameter("Modulo", IdModulo) }));
        }

        // La cuenta y todos sus ancestros hasta la raíz.
        public IEnumerable<ClsCuentaContabilidad> ContabilidadFuncObtenerPadres(ClsCuentaContabilidad Entidad)
        {
            return ContabilidadFuncMapear(ContabilidadFuncEjecutarConsulta(_SelectPadres, CommandType.Text,
                new List<OdbcParameter> { new OdbcParameter("Cuenta", Entidad.IdCuenta) }));
        }

        // Convierte las filas en entidades.
        private static List<ClsCuentaContabilidad> ContabilidadFuncMapear(DataTable Tabla)
        {
            List<ClsCuentaContabilidad> Cuentas = new List<ClsCuentaContabilidad>();
            foreach (DataRow Fila in Tabla.Rows)
            {
                Cuentas.Add(new ClsCuentaContabilidad
                {
                    IdCuenta = Convert.ToInt32(Fila[0]),
                    Codigo = Convert.ToString(Fila[1]),
                    Nombre = Convert.ToString(Fila[2]),
                    Tipo = Convert.ToString(Fila[3]),
                    IdPadre = Fila[4] == DBNull.Value ? 0 : Convert.ToInt32(Fila[4]),
                    Activo = Convert.ToInt32(Fila[5]) == 1
                });
            }
            Tabla.Dispose();
            return Cuentas;
        }
    }
}
