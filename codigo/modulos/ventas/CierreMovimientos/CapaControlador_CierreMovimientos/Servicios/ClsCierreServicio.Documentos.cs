using System;
using System.Data;
using CapaModelo_CierreMovimientos;
using CapaControlador_CierreMovimientos.Validaciones;
using VentasValidaciones;

namespace CapaControlador_CierreMovimientos.Servicios
{
    internal sealed partial class ClsCierreServicio
    {
        public DataTable Listar(int Moneda)
        {
            using (var Db = new ClsCierreRepositorio()) return Db.Tabla(ConsultaCierre.ListarConsultarCierremovimientos, Moneda, Moneda);
        }

        public string GuardarBorrador(int Id, int Moneda, int Usuario, string Tipo, DateTime Desde, DateTime Hasta, string Observaciones)
        {
            Tipo = (Tipo ?? "").Trim().ToUpperInvariant(); ClsReglasCierre.ValidarPeriodo(Tipo, Desde.Date, Hasta.Date);
            Observaciones = ClsValidacionesVentas.Texto(Observaciones, 500, "observaciones", false);
            using (var Db = new ClsCierreRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario); Db.PeriodoAbierto(Moneda, Desde);
                if (Id > 0)
                {
                    DataTable Actual = Db.Tabla(ConsultaCierre.GuardarBorradorConsultarCierremovimientos, Id);
                    ClsValidacionesVentas.Exigir(Actual.Rows.Count == 1 && (string)Actual.Rows[0]["estadoCierre"] == "BORRADOR" && Convert.ToInt32(Actual.Rows[0]["idMoneda"]) == Moneda,
                        "Solo se modifica un borrador de la moneda seleccionada. Los cierres confirmados conservan su historico.");
                }
                ClsValidacionesVentas.Exigir(Convert.ToInt32(Db.Escalar(ConsultaCierre.GuardarBorradorConsultarCierremovimientos2, Moneda, Tipo, Desde.Date, Hasta.Date, Id)) == 0,
                    "Ya existe un borrador o cierre confirmado para ese periodo y moneda.");
                string Numero;
                if (Id == 0)
                {
                    Numero = Db.Numero("CIE");
                    Db.Ejecutar(ConsultaCierre.GuardarBorradorInsertarCierremovimientos, Numero, Desde.Date, Hasta.Date, Hasta.Date, Moneda, Tipo, Observaciones, Usuario);
                    Id = Db.Id();
                }
                else
                {
                    Numero = Convert.ToString(Db.Escalar(ConsultaCierre.GuardarBorradorConsultarCierremovimientos3, Id));
                    Db.Ejecutar(ConsultaCierre.GuardarBorradorActualizarCierremovimientos, Tipo, Desde.Date, Hasta.Date, Hasta.Date, Observaciones, Id);
                }
                Db.Auditar("CIERRE", Id, "GUARDAR_BORRADOR", Observaciones, Usuario); Db.Confirmar(); return Numero;
            }
        }

        public void EliminarBorrador(int Id, int Usuario, string Motivo)
        {
            Motivo = ClsValidacionesVentas.Texto(Motivo, 180, "motivo de eliminacion");
            using (var Db = new ClsCierreRepositorio())
            {
                Db.Iniciar(); Db.Usuario(Usuario);
                DataTable Origen = Db.Tabla(ConsultaCierre.EliminarBorradorConsultarCierremovimientos, Id);
                ClsValidacionesVentas.Exigir(Origen.Rows.Count == 1, "No existe el cierre."); Db.BloquearMoneda(Convert.ToInt32(Origen.Rows[0][0]));
                DataTable Actual = Db.Tabla(ConsultaCierre.EliminarBorradorConsultarCierremovimientos2, Id);
                ClsValidacionesVentas.Exigir((string)Actual.Rows[0][0] == "BORRADOR", "Solo se elimina un borrador. No se eliminan cierres confirmados.");
                Db.Ejecutar(ConsultaCierre.EliminarBorradorActualizarCierremovimientos, Motivo, Id);
                Db.Auditar("CIERRE", Id, "ELIMINAR_BORRADOR", Motivo, Usuario); Db.Confirmar();
            }
        }
    }
}
