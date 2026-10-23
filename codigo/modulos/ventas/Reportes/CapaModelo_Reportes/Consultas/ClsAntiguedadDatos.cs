// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System;
using System.Data;
using VentasDatos;

namespace CapaModelo_Reportes
{
    public sealed class ClsAntiguedadDatos
    {
        public DataTable VentasFuncClientes()
        {
            using (var Datos = new ClsDatosVentas())
                return Datos.VentasFuncTabla("SELECT idCliente, CONCAT(codigoCliente,' - ',nombreCliente) Cliente FROM tblcliente ORDER BY nombreCliente");
        }

        public DataTable VentasFuncConsultar(DateTime Corte, int Cliente)
        {
            // El libro conserva el saldo historico aunque hoy la factura ya este pagada o anulada.
            const string Consulta = @"SELECT c.nombreCliente Cliente,
                CONCAT(f.serieFactura,'-',f.numeroFactura) Documento,
                f.fechaVencimiento Vencimiento, SUM(m.cargo-m.abono) Saldo
                FROM tblmovimientocxc m
                JOIN tblfactura f ON f.idFactura=m.idFactura
                JOIN tblcliente c ON c.idCliente=f.idCliente
                JOIN tblmonedaventas mo ON mo.idMoneda=f.idMoneda
                WHERE m.fechaMovimiento<=? AND f.fechaEmision<=?
                  AND (?=0 OR f.idCliente=?) AND UPPER(mo.codigoMoneda)='GTQ'
                GROUP BY f.idFactura,c.nombreCliente,f.serieFactura,f.numeroFactura,f.fechaVencimiento
                HAVING SUM(m.cargo-m.abono)>0
                ORDER BY c.nombreCliente,f.fechaVencimiento,f.idFactura";
            using (var Datos = new ClsDatosVentas())
                return Datos.VentasFuncTabla(Consulta, Corte.Date, Corte.Date, Cliente, Cliente);
        }
    }
}
