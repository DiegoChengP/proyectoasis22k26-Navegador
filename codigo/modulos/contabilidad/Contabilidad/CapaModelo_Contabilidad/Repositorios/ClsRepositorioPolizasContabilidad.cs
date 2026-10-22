/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Repositorio de pólizas. Por ahora trabaja en memoria con datos de
 *  ejemplo que tienen la forma de las tablas de póliza y cuenta; no
 *  consulta la base de datos. Cuando pase a MySQL solo cambia esta
 *  clase (heredará de ClsRepositorioMaestroContabilidad) y el resto
 *  de las capas se conserva.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;

namespace CapaModelo_Contabilidad.Repositorios
{
    public class ClsRepositorioPolizasContabilidad : IRepositorioPolizasContabilidad
    {
        // Pólizas en memoria.
        private readonly List<ClsPolizaContabilidad> _Polizas = new List<ClsPolizaContabilidad>();
        // Catálogo de cuentas en memoria.
        private readonly List<ClsCuentaContabilidad> _Cuentas = new List<ClsCuentaContabilidad>();

        // Carga las cuentas y las pólizas de ejemplo.
        public ClsRepositorioPolizasContabilidad()
        {
            string[] Nombres =
            {
                "Caja", "Bancos", "Clientes nacionales", "Inventario de materia prima",
                "Inventario de producto terminado", "Maquinaria y equipo", "Depreciación acumulada",
                "Proveedores", "IVA por pagar", "Préstamos bancarios", "Capital social", "Ventas",
                "Costo de ventas", "Gastos de administración", "Gastos de ventas", "Gastos financieros"
            };
            string[] Codigos =
            {
                "1.01.01.001", "1.01.02.001", "1.01.03.001", "1.01.04.001",
                "1.01.04.002", "1.02.01.001", "1.02.02.001", "2.01.01.001",
                "2.01.02.001", "2.02.01.001", "3.01.01.001", "4.01.01.001",
                "5.01.01.001", "6.01.01.001", "6.02.01.001", "6.03.01.001"
            };
            for (int Indice = 0; Indice < Nombres.Length; Indice++)
                _Cuentas.Add(new ClsCuentaContabilidad { IdCuenta = Indice + 1, Codigo = Codigos[Indice], Nombre = Nombres[Indice] });

            // Pólizas de ejemplo: octubre tiene un borrador y septiembre está limpio.
            ContabilidadMetSembrar(new DateTime(2026, 10, 2), "APLICADA", "Bancos", 120000m, "Ventas", 120000m);
            ContabilidadMetSembrar(new DateTime(2026, 10, 5), "APLICADA", "Costo de ventas", 65000m, "Inventario de producto terminado", 65000m);
            ContabilidadMetSembrar(new DateTime(2026, 10, 8), "APLICADA", "Gastos de administración", 18000m, "Bancos", 18000m);
            ContabilidadMetSembrar(new DateTime(2026, 10, 10), "BORRADOR", "Gastos de ventas", 2500m, "Bancos", 2500m);
            ContabilidadMetSembrar(new DateTime(2026, 10, 11), "ANULADA", "Bancos", 900m, "Ventas", 900m);
            ContabilidadMetSembrar(new DateTime(2026, 9, 25), "APLICADA", "Bancos", 80000m, "Ventas", 80000m);
        }

        // Crea una póliza de ejemplo con una línea al debe y otra al haber.
        private void ContabilidadMetSembrar(DateTime Fecha, string Estado, string CuentaDebe, decimal Debe, string CuentaHaber, decimal Haber)
        {
            ClsPolizaContabilidad Poliza = new ClsPolizaContabilidad
            {
                IdPoliza = _Polizas.Count + 1, Fecha = Fecha, Estado = Estado, Periodo = Fecha.ToString("yyyy-MM"),
                Origen = "MANUAL", Descripcion = "Póliza de ejemplo"
            };
            ClsCuentaContabilidad Deudora = _Cuentas.Single(Cuenta => Cuenta.Nombre == CuentaDebe);
            ClsCuentaContabilidad Acreedora = _Cuentas.Single(Cuenta => Cuenta.Nombre == CuentaHaber);
            Poliza.Detalle.Add(new ClsPolizaDetalleContabilidad { IdCuenta = Deudora.IdCuenta, Codigo = Deudora.Codigo, Cuenta = Deudora.Nombre, Debe = Debe });
            Poliza.Detalle.Add(new ClsPolizaDetalleContabilidad { IdCuenta = Acreedora.IdCuenta, Codigo = Acreedora.Codigo, Cuenta = Acreedora.Nombre, Haber = Haber });
            _Polizas.Add(Poliza);
        }

        // Agrega la póliza con el siguiente número.
        public int ContabilidadFuncAgregar(ClsPolizaContabilidad Entidad)
        {
            Entidad.IdPoliza = _Polizas.Count == 0 ? 1 : _Polizas.Max(Poliza => Poliza.IdPoliza) + 1;
            _Polizas.Add(Entidad);
            return 1;
        }

        // Reemplaza la póliza que tenga el mismo número.
        public int ContabilidadFuncEditar(ClsPolizaContabilidad Entidad)
        {
            int Indice = _Polizas.FindIndex(Poliza => Poliza.IdPoliza == Entidad.IdPoliza);
            if (Indice < 0) return 0;
            _Polizas[Indice] = Entidad;
            return 1;
        }

        // Quita la póliza (con MySQL será baja lógica con is_active).
        public int ContabilidadFuncEliminar(ClsPolizaContabilidad Entidad)
        {
            return _Polizas.RemoveAll(Poliza => Poliza.IdPoliza == Entidad.IdPoliza);
        }

        // Devuelve una copia de la lista.
        public IEnumerable<ClsPolizaContabilidad> ContabilidadFuncObtenerTodos()
        {
            return _Polizas.ToList();
        }

        // Devuelve el catálogo de cuentas.
        public List<ClsCuentaContabilidad> ContabilidadFuncObtenerCuentas()
        {
            return _Cuentas.ToList();
        }

        // Estados de la póliza.
        public string[] ContabilidadFuncObtenerEstados()
        {
            return new[] { "BORRADOR", "APLICADA", "ANULADA" };
        }

        // Orígenes de la póliza.
        public string[] ContabilidadFuncObtenerOrigenes()
        {
            return new[] { "MANUAL", "ACTIVOS_FIJOS", "PRESUPUESTO" };
        }
    }
}
