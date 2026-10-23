// Jose Javier Torres Martinez | 0901-23-1091 | 23/10/2026
using System.Data;
using System;
using VentasDatos;

namespace CapaModelo_CierreMovimientos
{
    public sealed class ClsConsultasCierre
    {
        private readonly ClsDatosVentas _Datos;

        internal ClsConsultasCierre(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        // Saldos: consultas parametrizadas.
        public DataTable VentasFuncSaldosConsultarMovimientocxc(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCliente,c.codigoCliente Codigo,c.nombreCliente Nombre,SUM(CASE WHEN m.fechaMovimiento<? THEN m.cargo-m.abono ELSE 0 END) Anterior,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) Cargos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) Abonos,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.cargo ELSE 0 END) CargosAcumulados,SUM(CASE WHEN m.fechaMovimiento>=? THEN m.abono ELSE 0 END) AbonosAcumulados,SUM(m.cargo-m.abono) Actual
    FROM tblmovimientocxc m
    JOIN tblfactura f ON f.idFactura=m.idFactura
    JOIN tblcliente c ON c.idCliente=f.idCliente
    WHERE f.idMoneda=?
      AND m.fechaMovimiento<=?
    GROUP BY c.idCliente,c.codigoCliente,c.nombreCliente
    ORDER BY c.codigoCliente";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Listar: consultas parametrizadas.
        public DataTable VentasFuncListarConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT c.idCierre Id,c.idMoneda,c.numeroCierre Numero,m.codigoMoneda Moneda,c.tipoCierre Tipo,c.fechaDesde Desde,c.fechaHasta Hasta,c.estadoCierre Estado,COALESCE(c.observaciones,'') Observaciones
    FROM tblcierremovimientos c
    JOIN tblmonedaventas m ON m.idMoneda=c.idMoneda
    WHERE (?=0 OR c.idMoneda=?)
    ORDER BY c.fechaDesde DESC,c.idCierre DESC";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // Historial: consultas parametrizadas.
        public DataTable VentasFuncHistorialConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT h.idCierre,h.numeroCierre,CONCAT(h.tipoCierre,' ',DATE_FORMAT(h.fechaDesde,'%Y-%m')) Periodo,c.codigoCliente Codigo,c.nombreCliente Nombre,c.saldoAnterior Anterior,c.cargosPeriodo Cargos,c.abonosPeriodo Abonos,c.cargosAcumulados CargosAcumulados,c.abonosAcumulados AbonosAcumulados,c.saldoActual Actual,h.estadoCierre Estado
    FROM tblcierremovimientos h
    JOIN tblcierresaldocliente c ON c.idCierre=h.idCierre
    WHERE h.idMoneda=?
      AND (?=0 OR c.idCliente=?)
      AND DATE_FORMAT(h.fechaDesde,'%Y-%m') LIKE CONCAT(?,'%')
    ORDER BY h.fechaDesde DESC,h.idCierre,c.codigoCliente";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }

        // EstadoPeriodo: consultas parametrizadas.
        public DataTable VentasFuncEstadoPeriodoConsultarCierremovimientos(params object[] Parametros)
        {
            const string Consulta = @"SELECT *
    FROM tblcierremovimientos
    WHERE idMoneda=?
      AND tipoCierre=?
      AND fechaDesde=?
      AND fechaHasta=?
      AND estadoCierre='CONFIRMADO'";
            return _Datos.VentasFuncTabla(Consulta, Parametros);
        }
    }
}
