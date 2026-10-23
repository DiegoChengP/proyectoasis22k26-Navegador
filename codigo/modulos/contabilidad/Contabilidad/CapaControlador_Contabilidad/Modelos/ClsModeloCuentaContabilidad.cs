/*
 * ==================================================================
 * Área : Contabilidad
 * Autor : Gabriel André Guillén Pocón
 * Carné : 0901-23-1998
 * Fecha : 21/10/2026
 * ==================================================================
 * Propósito :
 *  Modelo de una cuenta contable para la Vista: sus datos y las
 *  consultas por módulo ERP, de cuentas imputables, de ancestros y
 *  la búsqueda por texto.
 * Reglas especificas:
 *  Clase creada por Oskar Cermeño. Quedó de solo lectura porque el
 *  mantenimiento de cuentas lo hace el control Navegador; el filtro de
 *  cuentas por módulo se conserva para las pólizas.
 * ===================================================================
*/

using System;
using System.Collections.Generic;
using System.Linq;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using CapaModelo_Contabilidad.Repositorios;

namespace CapaControlador_Contabilidad
{
    public class ClsModeloCuentaContabilidad
    {
        // Repositorio de cuentas (consulta a la base).
        private readonly IRepositorioCuentasContabilidad _Repositorio;
        // Última lista consultada, para la búsqueda.
        private List<ClsModeloCuentaContabilidad> _ListaCuentas;

        // Llave de la cuenta.
        public int IdCuenta { get; set; }
        // Código de la cuenta.
        public string Codigo { get; set; }
        // Nombre de la cuenta.
        public string Nombre { get; set; }
        // Naturaleza: DEUDORA o ACREEDORA.
        public string Tipo { get; set; }
        // Cuenta padre; 0 si es raíz.
        public int IdPadre { get; set; }
        // Indica si está activa.
        public bool Activo { get; set; }
        // Código y nombre juntos, para mostrar en los combos.
        public string Descripcion { get { return Codigo + " - " + Nombre; } }

        // Usa el repositorio de la base de datos.
        public ClsModeloCuentaContabilidad()
        {
            _Repositorio = new ClsRepositorioCuentasContabilidad();
        }

        // Todas las cuentas del catálogo.
        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerTodos()
        {
            _ListaCuentas = ContabilidadFuncConvertir(_Repositorio.ContabilidadFuncObtenerTodos());
            return _ListaCuentas;
        }

        // Cuentas asignadas a un módulo ERP.
        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerTodos(int IdModulo)
        {
            _ListaCuentas = ContabilidadFuncConvertir(_Repositorio.ContabilidadFuncObtenerTodos(IdModulo));
            return _ListaCuentas;
        }

        // Cuentas del módulo que se pueden usar en una póliza.
        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerImputables(int IdModulo)
        {
            _ListaCuentas = ContabilidadFuncConvertir(_Repositorio.ContabilidadFuncObtenerImputables(IdModulo));
            return _ListaCuentas;
        }

        // La cuenta y sus ancestros hasta la raíz.
        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerPadres()
        {
            return ContabilidadFuncConvertir(_Repositorio.ContabilidadFuncObtenerPadres(new ClsCuentaContabilidad { IdCuenta = IdCuenta }));
        }

        // Busca por código, nombre o naturaleza dentro de la última lista consultada.
        public IEnumerable<ClsModeloCuentaContabilidad> ContabilidadFuncBuscar(string Filtro)
        {
            if (_ListaCuentas == null) ContabilidadFuncObtenerTodos();
            return _ListaCuentas.FindAll(Cuenta =>
                Cuenta.Codigo.IndexOf(Filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                Cuenta.Nombre.IndexOf(Filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                Cuenta.Tipo.IndexOf(Filtro, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        // Pasa de entidades del Modelo a modelos del Controlador.
        private static List<ClsModeloCuentaContabilidad> ContabilidadFuncConvertir(IEnumerable<ClsCuentaContabilidad> Entidades)
        {
            return Entidades.Select(Entidad => new ClsModeloCuentaContabilidad
            {
                IdCuenta = Entidad.IdCuenta, Codigo = Entidad.Codigo, Nombre = Entidad.Nombre,
                Tipo = Entidad.Tipo, IdPadre = Entidad.IdPadre, Activo = Entidad.Activo
            }).ToList();
        }
    }
}
