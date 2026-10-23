using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CapaModelo_Contabilidad.Contratos;
using CapaModelo_Contabilidad.Entidades;
using CapaModelo_Contabilidad.Repositorios;

namespace CapaControlador_Contabilidad.Modelos
{
    public class ClsModeloCuentaContabilidad
    {
        private int _idCuenta;
        private string _nombre;
        private string _tipo;
        private int _idPadre;
        private int _estado;
        private int _idModulo; 

        private IRepositorioCuentasContabilidad _repositorioCuentas;
        private List<ClsModeloCuentaContabilidad> _listaCuentas;

        public EstadoEntidadContabilidad EstadoOperacion { private get; set; }

        public int IdCuenta { get => _idCuenta; set => _idCuenta = value; }

        [Required(ErrorMessage = "El nombre de la cuenta es requerido.")]
        [StringLength(maximumLength: 150, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 150 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9\s\.\-\(\)]+$", ErrorMessage = "El nombre contiene caracteres no válidos.")]
        public string Nombre { get => _nombre; set => _nombre = value; }

        [Required(ErrorMessage = "El tipo de cuenta es requerido.")]
        [StringLength(maximumLength: 20, MinimumLength = 3, ErrorMessage = "El tipo de cuenta no puede exceder los 20 caracteres.")]
        public string Tipo { get => _tipo; set => _tipo = value; }

        public int IdPadre { get => _idPadre; set => _idPadre = value; }

        [Range(0, 1, ErrorMessage = "El estado debe ser 0 (Inactivo) o 1 (Activo).")]
        public int Estado { get => _estado; set => _estado = value; }

        public int IdModulo { get => _idModulo; set => _idModulo = value; }

        public ClsModeloCuentaContabilidad()
        {
            _repositorioCuentas = new ClsRepositorioCuentasContabilidad();
        }

        public string ContabilidadFuncGrabarCambios()
        {
            try
            {
                if (EstadoOperacion == EstadoEntidadContabilidad.Agregar || EstadoOperacion == EstadoEntidadContabilidad.Modificar)
                {
                    string errorValidacion = ClsValidacionDatosContabilidad.ContabilidadFuncPrimerError(this);
                    if (!string.IsNullOrEmpty(errorValidacion))
                    {
                        return errorValidacion;
                    }
                }
                var entidadCuenta = new ClsCuentaContabilidad
                {
                    IdCuenta = _idCuenta,
                    Nombre = _nombre,
                    Tipo = _tipo,
                    IdPadre = _idPadre,
                    Estado = _estado
                };

                switch (EstadoOperacion)
                {
                    case EstadoEntidadContabilidad.Agregar:
                        if (_idModulo > 0)
                            _repositorioCuentas.ContabilidadFuncAgregar(entidadCuenta, _idModulo, 1);
                        else
                            _repositorioCuentas.ContabilidadFuncAgregar(entidadCuenta);
                        return "Grabacion exitosa";

                    case EstadoEntidadContabilidad.Modificar:
                        _repositorioCuentas.ContabilidadFuncEditar(entidadCuenta);
                        return "Actualizacion exitosa";

                    case EstadoEntidadContabilidad.Borrar:
                        _repositorioCuentas.ContabilidadFuncEliminar(entidadCuenta);
                        return "Eliminacion exitosa";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return string.Empty;
        }

        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerTodos()
        {
            var datosCuentas = _repositorioCuentas.ContabilidadFuncObtenerTodos();
            _listaCuentas = new List<ClsModeloCuentaContabilidad>();

            foreach (ClsCuentaContabilidad item in datosCuentas)
            {
                _listaCuentas.Add(new ClsModeloCuentaContabilidad
                {
                    _idCuenta = item.IdCuenta,
                    _nombre = item.Nombre,
                    _tipo = item.Tipo,
                    _idPadre = item.IdPadre,
                    _estado = item.Estado
                });
            }
            return _listaCuentas;
        }

        public List<ClsModeloCuentaContabilidad> ContabilidadFuncObtenerTodos(int idModulo)
        {
            var datosCuentas = _repositorioCuentas.ContabilidadFuncObtenerTodos(idModulo);
            _listaCuentas = new List<ClsModeloCuentaContabilidad>();

            foreach (ClsCuentaContabilidad item in datosCuentas)
            {
                _listaCuentas.Add(new ClsModeloCuentaContabilidad
                {
                    _idCuenta = item.IdCuenta,
                    _nombre = item.Nombre,
                    _tipo = item.Tipo,
                    _idPadre = item.IdPadre,
                    _estado = item.Estado
                });
            }
            return _listaCuentas;
        }

        public IEnumerable<ClsModeloCuentaContabilidad> ContabilidadFuncBuscar(string filtro)
        {
            if (_listaCuentas == null)
            {
                ContabilidadFuncObtenerTodos();
            }

            return _listaCuentas.FindAll(c =>
                c.IdCuenta.ToString().Contains(filtro) ||
                c.Nombre.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0 ||
                c.Tipo.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}