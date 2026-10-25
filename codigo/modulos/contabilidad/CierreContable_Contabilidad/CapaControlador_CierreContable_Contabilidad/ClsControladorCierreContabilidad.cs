/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Donald Osorio
 * Carné : 0901-23-17982
 * Fecha : 17/10/2026
 * ==================================================================
 * Propósito :
 *  Reglas del cierre contable: pólizas de un período, resumen y
 *  diagnóstico que permite o bloquea el cierre. Responsable de la
 *  pantalla: Donald.
 * Reglas especificas:
 *  No cierra períodos en la base: solo revisa y valida.
 * ===================================================================
*/

using System;
using System.Data;
using System.Linq;
using CapaControlador_Contabilidad;
using CapaControlador_PolizasCuentas_Contabilidad;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Repositorios;
using CapaModelo_PolizasCuentas_Contabilidad.Contratos;
using CapaModelo_PolizasCuentas_Contabilidad.Entidades;
using CapaModelo_PolizasCuentas_Contabilidad.Repositorios;

namespace CapaControlador_CierreContable_Contabilidad
{
    public class ClsControladorCierreContabilidad
    {
        private readonly IRepositorioPolizasContabilidad _Repositorio = new ClsRepositorioPolizasContabilidad();

        public DataTable ContabilidadFuncPolizas(DateTime Periodo)
        {
            DataTable Resultado = new DataTable();
            Resultado.Columns.Add("Poliza", typeof(int));
            Resultado.Columns.Add("Fecha", typeof(DateTime));
            Resultado.Columns.Add("Estado", typeof(string));
            Resultado.Columns.Add("Debe", typeof(decimal));
            Resultado.Columns.Add("Haber", typeof(decimal));
            Resultado.Columns.Add("Diferencia", typeof(decimal));
            string Mes = Periodo.ToString("yyyy-MM");
            foreach (ClsPolizaContabilidad Poliza in _Repositorio.ContabilidadFuncObtenerTodos().Where(Elemento => Elemento.PeriodoContable == Mes))
            {
                decimal Debe = Poliza.Detalle.Sum(Linea => Linea.Debe);
                decimal Haber = Poliza.Detalle.Sum(Linea => Linea.Haber);
                Resultado.Rows.Add(Poliza.IdPoliza, Poliza.Fecha, Poliza.Estado, Debe, Haber, Debe - Haber);
            }
            return Resultado;
        }

        public string ContabilidadFuncValidarCierre(DataTable Polizas)
        {
            if (Polizas.Rows.Count == 0) return "El período no tiene pólizas para revisar.";
            foreach (DataRow Fila in Polizas.Rows)
            {
                if ((string)Fila["Estado"] == "BORRADOR") return "Hay pólizas en borrador. Revise y valide o anule antes del cierre.";
                if (ContabilidadFuncEsFirme(Fila) && (decimal)Fila["Diferencia"] != 0m) return "Hay pólizas validadas descuadradas.";
            }
            if (!Polizas.Rows.Cast<DataRow>().Any(ContabilidadFuncEsFirme)) return "El período no tiene pólizas validadas.";
            return string.Empty;
        }

        public ClsResumenCierreContabilidad ContabilidadFuncResumenCierre(DataTable Polizas)
        {
            var Filas = Polizas.Rows.Cast<DataRow>();
            var Aplicadas = Filas.Where(ContabilidadFuncEsFirme).ToList();
            return new ClsResumenCierreContabilidad
            {
                Aplicadas = Aplicadas.Count, Borradores = Filas.Count(Fila => (string)Fila["Estado"] == "BORRADOR"),
                Debe = Aplicadas.Sum(Fila => (decimal)Fila["Debe"]), Haber = Aplicadas.Sum(Fila => (decimal)Fila["Haber"])
            };
        }

        private static bool ContabilidadFuncEsFirme(DataRow Fila)
        {
            string Estado = (string)Fila["Estado"];
            return Estado == "VALIDADA" || Estado == "APLICADA";
        }
    }
}
