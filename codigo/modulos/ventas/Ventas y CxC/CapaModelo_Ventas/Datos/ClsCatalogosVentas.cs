// Jose Javier Torres Martinez | 0901-23-1091 | 22/10/2026
using System;
using System.Data;
using System.Data.Odbc;
using System.Globalization;

namespace VentasDatos
{
    internal sealed class ClsCatalogosVentas
    {
        private readonly ClsDatosVentas _Datos;
        internal ClsCatalogosVentas(ClsDatosVentas Datos)
        {
            _Datos = Datos;
        }

        internal DataTable VentasMetCatalogo(string Nombre)
        {
            switch (Nombre)
            {
                case "Clientes":
                    return _Datos.VentasMetTabla("SELECT idCliente Id,CONCAT(codigoCliente,' - ',nombreCliente) Nombre,numeroIdentificacion,direccionCliente,contactoCliente,diasCredito,limiteCredito,idMonedaCredito FROM tblcliente WHERE is_active=1 ORDER BY nombreCliente");
                case "Monedas":
                    return _Datos.VentasMetTabla("SELECT idMoneda Id,CONCAT(codigoMoneda,' - ',nombreMoneda) Nombre,codigoMoneda,simboloMoneda FROM tblmonedaventas WHERE is_active=1 ORDER BY codigoMoneda");
                case "Usuarios":
                    return _Datos.VentasMetTabla("SELECT idUsuario Id,nombreUsuario Nombre FROM tblusuario WHERE is_active=1 ORDER BY nombreUsuario");
                case "Vendedores":
                    return _Datos.VentasMetTabla("SELECT v.idVendedor Id,CONCAT(v.codigoVendedor,' - ',e.nombresEmpleado,' ',e.apellidosEmpleado) Nombre FROM tblvendedor v JOIN tblempleado e ON e.idEmpleado=v.idEmpleado WHERE v.is_active=1 ORDER BY v.codigoVendedor");
                case "Establecimientos":
                    return _Datos.VentasMetTabla("SELECT idEstablecimiento Id,CONCAT(codigoEstablecimiento,' - ',nombreEstablecimiento) Nombre FROM tblestablecimientoventas WHERE is_active=1 ORDER BY nombreEstablecimiento");
                default:
                    throw new ArgumentException("Catalogo no reconocido.");
            }
        }
    }
}
