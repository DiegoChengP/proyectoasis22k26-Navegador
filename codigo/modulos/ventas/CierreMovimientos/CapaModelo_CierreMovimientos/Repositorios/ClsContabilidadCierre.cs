// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_CierreMovimientos
{
    public sealed class ClsContabilidadCierre
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsContabilidadCierre(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        // PrepararTraslado: consultas parametrizadas.
        public DataTable VentasFuncPrepararTrasladoConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcierremovimientos
    WHERE idCierre=?
      AND estadoCierre='CONFIRMADO'
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public object VentasFuncPrepararTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT COUNT(*)
    FROM tbltrasladocxc
    WHERE idCierre=?";
            return _Datos.VentasFuncEscalar(Consulta, Parametros);
        }

        public int VentasFuncPrepararTrasladoInsertarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbltrasladocxc(idCierre,claveIntegracion,idUsuario)
    VALUES (?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public int VentasFuncPrepararTrasladoInsertarTrasladocxcdetalle(params object[] Parametros)
        {
            const string Consulta = @"INSERT INTO tbltrasladocxcdetalle(idCierre,numeroLinea,cuenta,descripcion,debe,haber)
    VALUES (?,?,?,?,?,?)";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }

        public DataTable VentasFuncPrepararTrasladoConsultarTrasladocxc2(params object[] Parametros)
        {
            const string Consulta = @"SELECT t.*,c.idMoneda,c.fechaHasta
    FROM tbltrasladocxc t
    JOIN tblcierremovimientos c ON c.idCierre=t.idCierre
    WHERE t.idCierre=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public DataTable VentasFuncPrepararTrasladoConsultarTrasladocxcdetalle(params object[] Parametros)
        {
            const string Consulta = @"SELECT cuenta Cuenta,descripcion Descripcion,debe Debe,haber Haber
    FROM tbltrasladocxcdetalle
    WHERE idCierre=?
    ORDER BY numeroLinea";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Partida: consultas parametrizadas.
        public DataTable VentasFuncPartidaConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT m.tipoMovimiento,SUM(m.cargo) Cargos,SUM(m.abono) Abonos,p.cuentaClientes,p.cuentaContrapartida
    FROM tblcierremovimientos c
    JOIN tblfactura f ON f.idMoneda=c.idMoneda
    JOIN tblmovimientocxc m ON m.idFactura=f.idFactura
    LEFT JOIN tblcuentacontablecxc p ON p.idMoneda=c.idMoneda
      AND p.tipoMovimiento=m.tipoMovimiento
    WHERE c.idCierre=?
      AND c.estadoCierre='CONFIRMADO'
      AND m.fechaMovimiento BETWEEN c.fechaDesde
      AND c.fechaHasta
    GROUP BY m.tipoMovimiento,p.cuentaClientes,p.cuentaContrapartida
    ORDER BY m.tipoMovimiento";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // EstadoTraslado: consultas parametrizadas.
        public DataTable VentasFuncEstadoTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbltrasladocxc
    WHERE idCierre=?";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // ConfirmarTraslado: consultas parametrizadas.
        public DataTable VentasFuncConfirmarTrasladoConsultarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tbltrasladocxc
    WHERE idCierre=?
    FOR UPDATE";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        public int VentasFuncConfirmarTrasladoActualizarTrasladocxc(params object[] Parametros)
        {
            const string Consulta = @"UPDATE tbltrasladocxc
    SET estado='CONFIRMADO',referenciaContable=?,fechaConfirmacion=CURRENT_TIMESTAMP
    WHERE idCierre=?";
            return _Datos.VentasFuncEjecutar(Consulta, Parametros);
        }
    }
}
