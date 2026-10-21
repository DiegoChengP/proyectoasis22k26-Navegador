-- VENTAS EN QUETZALES - 21/10/2026.
-- Ejecutar el archivo COMPLETO en MySQL Workbench (MySQL 8.0.29 o posterior).
-- Crea dbventas_quetzales. No modifica dbventas ni sus cierres confirmados.
-- GTQ / Quetzal / Q es la unica moneda; todos los tipos de cambio son 1.
-- 10 clientes, vendedores, productos, listas, cotizaciones, pedidos, facturas,
-- recibos, aplicaciones, notas, comisiones, depositos y cierres mensuales.
-- 1 moneda, 1 control de periodo y 3 configuraciones de cuenta contable.
-- 55 detalles de cierre y 55 saldos historicos: cada mes arrastra los saldos previos.
-- Datos ficticios, sin validez fiscal ni envios reales a Bancos o Contabilidad.
-- Incluye solo las dependencias de Seguridad necesarias para probar Ventas.
-- Los usuarios ficticios no tienen contrasenas validas para iniciar sesion.
-- Para usar esta muestra: configurar la Database del DSN Ventasbd como dbventas_quetzales.
-- Esto reemplaza la muestra de varias monedas; no es una conversion de operaciones reales.

CREATE DATABASE IF NOT EXISTS dbventas_quetzales
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE dbventas_quetzales;

CREATE TABLE IF NOT EXISTS `tblempleado` (
  `idEmpleado` int NOT NULL AUTO_INCREMENT,
  `codigoEmpleado` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `dpiEmpleado` char(13) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nitEmpleado` char(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nombresEmpleado` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `apellidosEmpleado` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `puestoEmpleado` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `generoEmpleado` char(1) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'M = Masculino, F = Femenino',
  `fechaNacimientoEmpleado` date NOT NULL,
  `fechaContratacionEmpleado` date NOT NULL,
  `telefonoEmpleado` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `correoEmpleado` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idEmpleado`),
  UNIQUE KEY `uqCodigoEmpleado` (`codigoEmpleado`),
  UNIQUE KEY `uqDpiEmpleado` (`dpiEmpleado`),
  UNIQUE KEY `uqNitEmpleado` (`nitEmpleado`),
  UNIQUE KEY `uqCorreoEmpleado` (`correoEmpleado`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE IF NOT EXISTS `tblusuario` (
  `idUsuario` int NOT NULL AUTO_INCREMENT,
  `idEmpleado` int NOT NULL,
  `nombreUsuario` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `contrasenaUsuario` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ultimoAccesoUsuario` datetime NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `uqIdEmpleadoUsuario` (`idEmpleado`),
  UNIQUE KEY `uqUsuarioUsuario` (`nombreUsuario`),
  KEY `idxUsuarioIdEmpleado` (`idEmpleado`),
  CONSTRAINT `Fk_Usuario_Empleado` FOREIGN KEY (`idEmpleado`) REFERENCES `tblempleado` (`idEmpleado`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DELIMITER $$
DROP PROCEDURE IF EXISTS sp_ventas_validar_seguridad_20261020$$
CREATE PROCEDURE sp_ventas_validar_seguridad_20261020()
BEGIN
  IF (SELECT COUNT(*) FROM information_schema.TABLES
      WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME IN ('tblusuario','tblempleado')
        AND ENGINE='InnoDB') <> 2 THEN
    SIGNAL SQLSTATE '45000'
      SET MESSAGE_TEXT='Importe primero Seguridad: se requieren tblusuario y tblempleado en InnoDB';
  END IF;
  IF (SELECT COUNT(*) FROM information_schema.COLUMNS
      WHERE TABLE_SCHEMA=DATABASE()
        AND ((TABLE_NAME='tblusuario' AND COLUMN_NAME='idUsuario')
          OR (TABLE_NAME='tblempleado' AND COLUMN_NAME='idEmpleado'))
        AND DATA_TYPE='int' AND COLUMN_TYPE NOT LIKE '%unsigned%') <> 2 THEN
    SIGNAL SQLSTATE '45000'
      SET MESSAGE_TEXT='Las PK de Seguridad deben ser INT con signo para coincidir con Ventas';
  END IF;
END$$
CALL sp_ventas_validar_seguridad_20261020()$$
DROP PROCEDURE sp_ventas_validar_seguridad_20261020$$
DELIMITER ;

CREATE TABLE IF NOT EXISTS `tblmonedaventas` (
  `idMoneda` INT NOT NULL AUTO_INCREMENT,
  `codigoMoneda` CHAR(3) NOT NULL COMMENT 'GTQ, USD',
  `nombreMoneda` VARCHAR(60) NOT NULL,
  `simboloMoneda` VARCHAR(10) NOT NULL,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idMoneda`),
  UNIQUE KEY `uq_tblmonedaventas_1` (`codigoMoneda`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 02. tblestablecimientoventas: Establecimiento emisor; reutilizar el catálogo de empresa si existe.
CREATE TABLE IF NOT EXISTS `tblestablecimientoventas` (
  `idEstablecimiento` INT NOT NULL AUTO_INCREMENT,
  `codigoEstablecimiento` VARCHAR(20) NOT NULL,
  `nombreEstablecimiento` VARCHAR(150) NOT NULL,
  `nitEmisor` VARCHAR(25) NOT NULL,
  `direccionEstablecimiento` VARCHAR(255) NOT NULL,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idEstablecimiento`),
  UNIQUE KEY `uq_tblestablecimientoventas_1` (`codigoEstablecimiento`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 03. tblcliente: Clientes y condiciones de crédito.
-- Regla para implementar/validar: diasCredito y limiteCredito >= 0. No almacenar saldoActual como fuente del estado de cuenta.
CREATE TABLE IF NOT EXISTS `tblcliente` (
  `idCliente` INT NOT NULL AUTO_INCREMENT,
  `codigoCliente` VARCHAR(30) NOT NULL,
  `tipoIdentificacion` VARCHAR(20) NOT NULL COMMENT 'NIT, CUI, EXTRANJERO, CF',
  `numeroIdentificacion` VARCHAR(30) NULL COMMENT 'CF no es una identificación única',
  `nombreCliente` VARCHAR(200) NOT NULL,
  `direccionCliente` VARCHAR(255) NULL,
  `contactoCliente` VARCHAR(150) NULL,
  `telefonoCliente` VARCHAR(30) NULL,
  `correoCliente` VARCHAR(150) NULL,
  `diasCredito` INT NOT NULL DEFAULT 0,
  `limiteCredito` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `idMonedaCredito` INT NOT NULL,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idCliente`),
  UNIQUE KEY `uq_tblcliente_1` (`codigoCliente`),
  KEY `idx_tblcliente_1` (`idMonedaCredito`),
  CONSTRAINT `fk_tblcliente_1` FOREIGN KEY (`idMonedaCredito`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 04. tblvendedor: Asigna a un empleado existente la función de vendedor.
CREATE TABLE IF NOT EXISTS `tblvendedor` (
  `idVendedor` INT NOT NULL AUTO_INCREMENT,
  `codigoVendedor` VARCHAR(30) NOT NULL,
  `idEmpleado` INT NOT NULL,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idVendedor`),
  UNIQUE KEY `uq_tblvendedor_1` (`codigoVendedor`),
  UNIQUE KEY `uq_tblvendedor_2` (`idEmpleado`),
  CONSTRAINT `fk_tblvendedor_1` FOREIGN KEY (`idEmpleado`)
    REFERENCES `tblempleado` (`idEmpleado`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 05. tblproductoventas: Referencia de productos y servicios para documentos y precios.
-- Regla para implementar/validar: Coordinar con Inventarios: si ya existe tblproducto, usar su PK y no duplicar el catálogo. La tasa es configurable; este documento no define reglas fiscales.
CREATE TABLE IF NOT EXISTS `tblproductoventas` (
  `idProducto` INT NOT NULL AUTO_INCREMENT,
  `codigoProducto` VARCHAR(40) NOT NULL,
  `nombreProducto` VARCHAR(180) NOT NULL,
  `descripcionProducto` VARCHAR(255) NULL,
  `tipoItem` CHAR(1) NOT NULL COMMENT 'B = bien, S = servicio',
  `unidadMedida` VARCHAR(30) NOT NULL,
  `porcentajeImpuesto` DECIMAL(7,4) NOT NULL DEFAULT 0,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idProducto`),
  UNIQUE KEY `uq_tblproductoventas_1` (`codigoProducto`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 06. tbllistaprecios: Encabezado de listas de precios consumidas también por Blazor.
-- Regla para implementar/validar: vigenteHasta >= vigenteDesde cuando tenga valor.
CREATE TABLE IF NOT EXISTS `tbllistaprecios` (
  `idListaPrecio` INT NOT NULL AUTO_INCREMENT,
  `codigoListaPrecio` VARCHAR(30) NOT NULL,
  `nombreListaPrecio` VARCHAR(100) NOT NULL,
  `idMoneda` INT NOT NULL,
  `vigenteDesde` DATE NOT NULL,
  `vigenteHasta` DATE NULL COMMENT 'sin fecha final',
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idListaPrecio`),
  UNIQUE KEY `uq_tbllistaprecios_1` (`codigoListaPrecio`),
  KEY `idx_tbllistaprecios_1` (`idMoneda`),
  CONSTRAINT `fk_tbllistaprecios_1` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 07. tbllistapreciosdetalle: Precio de cada producto dentro de una lista.
-- Regla para implementar/validar: UQ(idListaPrecio, idProducto). Precio >= 0. La web y WinForms consultan la misma lista; Blazor no necesita duplicar tablas.
CREATE TABLE IF NOT EXISTS `tbllistapreciosdetalle` (
  `idListaPrecioDetalle` INT NOT NULL AUTO_INCREMENT,
  `idListaPrecio` INT NOT NULL,
  `idProducto` INT NOT NULL,
  `precioUnitario` DECIMAL(18,4) NOT NULL,
  `is_active` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idListaPrecioDetalle`),
  UNIQUE KEY `uq_tbllistapreciosdetalle_1` (`idListaPrecio`, `idProducto`),
  KEY `idx_tbllistapreciosdetalle_1` (`idProducto`),
  CONSTRAINT `fk_tbllistapreciosdetalle_1` FOREIGN KEY (`idListaPrecio`)
    REFERENCES `tbllistaprecios` (`idListaPrecio`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tbllistapreciosdetalle_2` FOREIGN KEY (`idProducto`)
    REFERENCES `tblproductoventas` (`idProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 08. tblcotizacion: Propuesta comercial; aún no genera deuda del cliente.
CREATE TABLE IF NOT EXISTS `tblcotizacion` (
  `idCotizacion` INT NOT NULL AUTO_INCREMENT,
  `numeroCotizacion` VARCHAR(40) NOT NULL,
  `idCliente` INT NOT NULL,
  `idVendedor` INT NOT NULL,
  `idMoneda` INT NOT NULL,
  `fechaEmision` DATE NOT NULL,
  `vigenteHasta` DATE NOT NULL,
  `tipoCambio` DECIMAL(18,6) NOT NULL DEFAULT 1,
  `estadoCotizacion` VARCHAR(25) NOT NULL COMMENT 'BORRADOR, EMITIDA, ACEPTADA, RECHAZADA, VENCIDA, ANULADA',
  `subtotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `descuentoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `impuestoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `total` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idCotizacion`),
  UNIQUE KEY `uq_tblcotizacion_1` (`numeroCotizacion`),
  KEY `idx_tblcotizacion_1` (`idCliente`),
  KEY `idx_tblcotizacion_2` (`idVendedor`),
  KEY `idx_tblcotizacion_3` (`idMoneda`),
  KEY `idx_tblcotizacion_4` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblcotizacion_1` FOREIGN KEY (`idCliente`)
    REFERENCES `tblcliente` (`idCliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcotizacion_2` FOREIGN KEY (`idVendedor`)
    REFERENCES `tblvendedor` (`idVendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcotizacion_3` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcotizacion_4` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 09. tblcotizaciondetalle: Productos cotizados y condiciones congeladas de cada línea.
-- Regla para implementar/validar: UQ(idCotizacion, numeroLinea). Cantidad > 0; precio y descuentos >= 0.
CREATE TABLE IF NOT EXISTS `tblcotizaciondetalle` (
  `idCotizacionDetalle` INT NOT NULL AUTO_INCREMENT,
  `idCotizacion` INT NOT NULL,
  `numeroLinea` INT NOT NULL,
  `idProducto` INT NOT NULL,
  `descripcionItem` VARCHAR(255) NOT NULL,
  `tipoItem` CHAR(1) NOT NULL COMMENT 'B, S',
  `cantidad` DECIMAL(18,4) NOT NULL,
  `precioUnitario` DECIMAL(18,4) NOT NULL,
  `descuentoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `porcentajeImpuesto` DECIMAL(7,4) NOT NULL DEFAULT 0,
  `impuestoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `totalLinea` DECIMAL(18,2) NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idCotizacionDetalle`),
  UNIQUE KEY `uq_tblcotizaciondetalle_1` (`idCotizacion`, `numeroLinea`),
  KEY `idx_tblcotizaciondetalle_1` (`idProducto`),
  CONSTRAINT `fk_tblcotizaciondetalle_1` FOREIGN KEY (`idCotizacion`)
    REFERENCES `tblcotizacion` (`idCotizacion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcotizaciondetalle_2` FOREIGN KEY (`idProducto`)
    REFERENCES `tblproductoventas` (`idProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 10. tblpedido: Pedido independiente o basado en una cotización.
-- Regla para implementar/validar: El origen se deduce de idCotizacion. Cliente y moneda deben coincidir con la cotización de origen.
CREATE TABLE IF NOT EXISTS `tblpedido` (
  `idPedido` INT NOT NULL AUTO_INCREMENT,
  `numeroPedido` VARCHAR(40) NOT NULL,
  `idCotizacion` INT NULL COMMENT 'NULL si independiente',
  `referenciaIndependiente` VARCHAR(100) NULL,
  `idCliente` INT NOT NULL,
  `idVendedor` INT NOT NULL,
  `idMoneda` INT NOT NULL,
  `fechaPedido` DATE NOT NULL,
  `fechaEntrega` DATE NULL,
  `fechaVencimiento` DATE NULL,
  `tipoCambio` DECIMAL(18,6) NOT NULL DEFAULT 1,
  `estadoPedido` VARCHAR(25) NOT NULL DEFAULT 'PENDIENTE' COMMENT 'PENDIENTE, CONFIRMADO, PARCIAL, COMPLETADO, ANULADO',
  `subtotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `descuentoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `impuestoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `total` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioRegistro` INT NULL DEFAULT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idPedido`),
  UNIQUE KEY `uq_tblpedido_1` (`numeroPedido`),
  KEY `idx_tblpedido_1` (`idCotizacion`),
  KEY `idx_tblpedido_2` (`idCliente`),
  KEY `idx_tblpedido_3` (`idVendedor`),
  KEY `idx_tblpedido_4` (`idMoneda`),
  KEY `idx_tblpedido_5` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblpedido_1` FOREIGN KEY (`idCotizacion`)
    REFERENCES `tblcotizacion` (`idCotizacion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedido_2` FOREIGN KEY (`idCliente`)
    REFERENCES `tblcliente` (`idCliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedido_3` FOREIGN KEY (`idVendedor`)
    REFERENCES `tblvendedor` (`idVendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedido_4` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedido_5` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 11. tblpedidodetalle: Productos solicitados y trazabilidad hacia la cotización.
-- Regla para implementar/validar: UQ(idPedido, numeroLinea). La línea de cotización debe pertenecer al encabezado de origen.
CREATE TABLE IF NOT EXISTS `tblpedidodetalle` (
  `idPedidoDetalle` INT NOT NULL AUTO_INCREMENT,
  `idPedido` INT NOT NULL,
  `idCotizacionDetalle` INT NULL,
  `numeroLinea` INT NOT NULL,
  `idProducto` INT NOT NULL,
  `descripcionItem` VARCHAR(255) NOT NULL,
  `tipoItem` CHAR(1) NOT NULL,
  `cantidad` DECIMAL(18,4) NOT NULL,
  `precioUnitario` DECIMAL(18,4) NOT NULL,
  `descuentoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `porcentajeImpuesto` DECIMAL(7,4) NOT NULL DEFAULT 0,
  `impuestoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `totalLinea` DECIMAL(18,2) NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idPedidoDetalle`),
  UNIQUE KEY `uq_tblpedidodetalle_1` (`idPedido`, `numeroLinea`),
  KEY `idx_tblpedidodetalle_1` (`idCotizacionDetalle`),
  KEY `idx_tblpedidodetalle_2` (`idProducto`),
  CONSTRAINT `fk_tblpedidodetalle_1` FOREIGN KEY (`idPedido`)
    REFERENCES `tblpedido` (`idPedido`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedidodetalle_2` FOREIGN KEY (`idCotizacionDetalle`)
    REFERENCES `tblcotizaciondetalle` (`idCotizacionDetalle`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpedidodetalle_3` FOREIGN KEY (`idProducto`)
    REFERENCES `tblproductoventas` (`idProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 12. tblfactura: Factura emitida, vencimiento y datos históricos del receptor.
-- Regla para implementar/validar: UQ(idEstablecimiento, serieFactura, numeroFactura). Guardar los datos del receptor al emitir, aunque después se edite el cliente. Una factura pagada conserva su estado de emisión; su saldo se deriva del libro de CxC.
CREATE TABLE IF NOT EXISTS `tblfactura` (
  `idFactura` INT NOT NULL AUTO_INCREMENT,
  `serieFactura` VARCHAR(30) NOT NULL,
  `numeroFactura` VARCHAR(40) NOT NULL,
  `idEstablecimiento` INT NOT NULL,
  `idPedido` INT NULL,
  `idCliente` INT NOT NULL,
  `idVendedor` INT NULL,
  `idMoneda` INT NOT NULL,
  `fechaEmision` DATE NOT NULL,
  `fechaVencimiento` DATE NOT NULL COMMENT 'base del reporte de mora',
  `tipoDte` VARCHAR(30) NOT NULL COMMENT 'catálogo por definir con el equipo de Facturas',
  `condicionPago` VARCHAR(15) NOT NULL COMMENT 'CONTADO, CREDITO',
  `diasCreditoAplicados` INT NOT NULL DEFAULT 0,
  `tipoCambio` DECIMAL(18,6) NOT NULL DEFAULT 1,
  `tipoIdentificacionReceptor` VARCHAR(20) NOT NULL,
  `identificacionReceptor` VARCHAR(30) NOT NULL,
  `nombreReceptor` VARCHAR(200) NOT NULL,
  `direccionReceptor` VARCHAR(255) NULL,
  `subtotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `descuentoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `impuestoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `total` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `estadoFactura` VARCHAR(25) NOT NULL COMMENT 'BORRADOR, EMITIDA, ANULADA',
  `uuidDte` CHAR(36) NULL COMMENT 'solo cuando exista certificación',
  `fechaCertificacion` DATETIME NULL,
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idFactura`),
  UNIQUE KEY `uq_tblfactura_1` (`uuidDte`),
  UNIQUE KEY `uq_tblfactura_2` (`idEstablecimiento`, `serieFactura`, `numeroFactura`),
  KEY `idx_tblfactura_1` (`idCliente`, `idMoneda`, `fechaVencimiento`),
  KEY `idx_tblfactura_2` (`idPedido`),
  KEY `idx_tblfactura_3` (`idVendedor`),
  KEY `idx_tblfactura_4` (`idMoneda`),
  KEY `idx_tblfactura_5` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblfactura_1` FOREIGN KEY (`idEstablecimiento`)
    REFERENCES `tblestablecimientoventas` (`idEstablecimiento`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfactura_2` FOREIGN KEY (`idPedido`)
    REFERENCES `tblpedido` (`idPedido`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfactura_3` FOREIGN KEY (`idCliente`)
    REFERENCES `tblcliente` (`idCliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfactura_4` FOREIGN KEY (`idVendedor`)
    REFERENCES `tblvendedor` (`idVendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfactura_5` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfactura_6` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 13. tblfacturadetalle: Líneas facturadas, precios e impuestos históricos.
-- Regla para implementar/validar: UQ(idFactura, numeroLinea). Coordinar con el equipo si se permitirá una factura con varios pedidos: esta propuesta usa un pedido por encabezado, con facturación parcial por líneas.
CREATE TABLE IF NOT EXISTS `tblfacturadetalle` (
  `idFacturaDetalle` INT NOT NULL AUTO_INCREMENT,
  `idFactura` INT NOT NULL,
  `idPedidoDetalle` INT NULL,
  `numeroLinea` INT NOT NULL,
  `idProducto` INT NOT NULL,
  `descripcionItem` VARCHAR(255) NOT NULL,
  `tipoItem` CHAR(1) NOT NULL,
  `cantidad` DECIMAL(18,4) NOT NULL,
  `precioUnitario` DECIMAL(18,4) NOT NULL,
  `descuentoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `porcentajeImpuesto` DECIMAL(7,4) NOT NULL DEFAULT 0,
  `impuestoMonto` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `totalLinea` DECIMAL(18,2) NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idFacturaDetalle`),
  UNIQUE KEY `uq_tblfacturadetalle_1` (`idFactura`, `numeroLinea`),
  KEY `idx_tblfacturadetalle_1` (`idPedidoDetalle`),
  KEY `idx_tblfacturadetalle_2` (`idProducto`),
  CONSTRAINT `fk_tblfacturadetalle_1` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfacturadetalle_2` FOREIGN KEY (`idPedidoDetalle`)
    REFERENCES `tblpedidodetalle` (`idPedidoDetalle`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblfacturadetalle_3` FOREIGN KEY (`idProducto`)
    REFERENCES `tblproductoventas` (`idProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 14. tblnotacxc: Ajustes de crédito o débito sobre una factura; futuro mantenimiento, sin formulario nuevo.
-- Regla para implementar/validar: Monto > 0, en la moneda de la factura. Es una propuesta de ajustes de CxC, no un modelo completo de notas fiscales certificadas.
CREATE TABLE IF NOT EXISTS `tblnotacxc` (
  `idNota` INT NOT NULL AUTO_INCREMENT,
  `numeroNota` VARCHAR(40) NOT NULL,
  `idFactura` INT NOT NULL,
  `tipoNota` VARCHAR(10) NOT NULL COMMENT 'CREDITO, DEBITO',
  `fechaNota` DATE NOT NULL,
  `monto` DECIMAL(18,2) NOT NULL,
  `motivo` VARCHAR(500) NOT NULL,
  `estadoNota` VARCHAR(20) NOT NULL COMMENT 'BORRADOR, APLICADA, ANULADA',
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idNota`),
  UNIQUE KEY `uq_tblnotacxc_1` (`numeroNota`),
  KEY `idx_tblnotacxc_1` (`idFactura`),
  KEY `idx_tblnotacxc_2` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblnotacxc_1` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblnotacxc_2` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 15. tblpago: Recibo de dinero; se reparte entre una o varias facturas.
-- Regla para implementar/validar: Monto > 0. No guardar números completos de tarjetas ni credenciales bancarias. Primera etapa: pagos totalmente aplicados; anticipos sin factura requieren extender el modelo.
CREATE TABLE IF NOT EXISTS `tblpago` (
  `idPago` INT NOT NULL AUTO_INCREMENT,
  `numeroRecibo` VARCHAR(40) NOT NULL,
  `idCliente` INT NOT NULL,
  `idMoneda` INT NOT NULL,
  `fechaPago` DATE NOT NULL,
  `montoPago` DECIMAL(18,2) NOT NULL,
  `tipoCambio` DECIMAL(18,6) NOT NULL DEFAULT 1,
  `formaPago` VARCHAR(30) NOT NULL COMMENT 'EFECTIVO, TRANSFERENCIA, CHEQUE, TARJETA',
  `referenciaPago` VARCHAR(100) NULL,
  `estadoPago` VARCHAR(20) NOT NULL COMMENT 'BORRADOR, CONFIRMADO, ANULADO',
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idPago`),
  UNIQUE KEY `uq_tblpago_1` (`numeroRecibo`),
  KEY `idx_tblpago_1` (`idCliente`, `fechaPago`),
  KEY `idx_tblpago_2` (`idMoneda`),
  KEY `idx_tblpago_3` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblpago_1` FOREIGN KEY (`idCliente`)
    REFERENCES `tblcliente` (`idCliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpago_2` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblpago_3` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 16. tblaplicacionpago: Parte de un pago que abona una factura; permite pagos parciales.
-- Regla para implementar/validar: Monto > 0. Misma moneda y cliente en pago y factura. Aplicaciones vigentes no superan el monto del pago ni el saldo disponible de la factura; validar en una transacción.
CREATE TABLE IF NOT EXISTS `tblaplicacionpago` (
  `idAplicacionPago` INT NOT NULL AUTO_INCREMENT,
  `idPago` INT NOT NULL,
  `idFactura` INT NOT NULL,
  `fechaAplicacion` DATE NOT NULL,
  `montoAplicado` DECIMAL(18,2) NOT NULL,
  `estadoAplicacion` VARCHAR(20) NOT NULL COMMENT 'APLICADA, REVERTIDA',
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idAplicacionPago`),
  KEY `idx_tblaplicacionpago_1` (`idFactura`, `fechaAplicacion`),
  KEY `idx_tblaplicacionpago_2` (`idPago`),
  KEY `idx_tblaplicacionpago_3` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblaplicacionpago_1` FOREIGN KEY (`idPago`)
    REFERENCES `tblpago` (`idPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblaplicacionpago_2` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblaplicacionpago_3` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 17. tblmovimientocxc: Libro histórico de cargos y abonos; fuente única del estado de cuenta y antigüedad.
-- Regla para implementar/validar: Exactamente uno de cargo o abono > 0. Cliente, moneda y vencimiento se obtienen de la factura. Emitir factura crea un cargo; aplicar pago crea un abono; notas generan el signo correspondiente. Revertir crea una fila opuesta, nunca borra la original. Evitar generar dos movimientos para el mismo evento.
CREATE TABLE IF NOT EXISTS `tblmovimientocxc` (
  `idMovimiento` INT NOT NULL AUTO_INCREMENT,
  `idFactura` INT NOT NULL,
  `idAplicacionPago` INT NULL,
  `idNota` INT NULL,
  `idMovimientoOrigen` INT NULL COMMENT 'movimiento que se revierte',
  `fechaMovimiento` DATE NOT NULL COMMENT 'fecha efectiva contable',
  `tipoMovimiento` VARCHAR(30) NOT NULL COMMENT 'FACTURA, PAGO, NOTA_CREDITO, NOTA_DEBITO, REVERSO',
  `descripcionMovimiento` VARCHAR(255) NOT NULL,
  `cargo` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `abono` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `idUsuarioRegistro` INT NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idMovimiento`),
  UNIQUE KEY `uq_tblmovimientocxc_1` (`idAplicacionPago`),
  UNIQUE KEY `uq_tblmovimientocxc_2` (`idNota`),
  UNIQUE KEY `uq_tblmovimientocxc_3` (`idMovimientoOrigen`),
  KEY `idx_tblmovimientocxc_1` (`idFactura`, `fechaMovimiento`),
  KEY `idx_tblmovimientocxc_2` (`fechaMovimiento`),
  KEY `idx_tblmovimientocxc_3` (`idUsuarioRegistro`),
  CONSTRAINT `fk_tblmovimientocxc_1` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblmovimientocxc_2` FOREIGN KEY (`idAplicacionPago`)
    REFERENCES `tblaplicacionpago` (`idAplicacionPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblmovimientocxc_3` FOREIGN KEY (`idNota`)
    REFERENCES `tblnotacxc` (`idNota`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblmovimientocxc_4` FOREIGN KEY (`idMovimientoOrigen`)
    REFERENCES `tblmovimientocxc` (`idMovimiento`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblmovimientocxc_5` FOREIGN KEY (`idUsuarioRegistro`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 18. tblcierremovimientos: Encabezado de cierre y responsable.
-- Periodos mensuales o anuales completos; el controlador confirma y bloquea el periodo. Un cierre confirmado es inmutable.
CREATE TABLE IF NOT EXISTS `tblcierremovimientos` (
  `idCierre` INT NOT NULL AUTO_INCREMENT,
  `numeroCierre` VARCHAR(40) NOT NULL,
  `fechaDesde` DATE NOT NULL,
  `fechaHasta` DATE NOT NULL,
  `fechaCorte` DATE NOT NULL,
  `idMoneda` INT NOT NULL,
  `tipoCierre` VARCHAR(20) NOT NULL COMMENT 'MENSUAL, ANUAL',
  `estadoCierre` VARCHAR(20) NOT NULL COMMENT 'BORRADOR, CONFIRMADO, ANULADO',
  `documentosRevisados` TINYINT(1) NOT NULL DEFAULT 0,
  `pagosConciliados` TINYINT(1) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioResponsable` INT NOT NULL,
  `fechaConfirmacion` DATETIME NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idCierre`),
  UNIQUE KEY `uq_tblcierremovimientos_1` (`numeroCierre`),
  KEY `idx_tblcierremovimientos_1` (`idMoneda`, `fechaDesde`, `fechaHasta`),
  KEY `idx_tblcierremovimientos_2` (`idUsuarioResponsable`),
  CONSTRAINT `fk_tblcierremovimientos_1` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcierremovimientos_2` FOREIGN KEY (`idUsuarioResponsable`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 19. tblcierremovimientosdetalle: Fotografía de saldos por factura al confirmar un cierre.
-- Regla para implementar/validar: UQ(idCierre, idFactura). saldoFinal = saldoInicial + cargosPeriodo - abonosPeriodo. Copia histórica inmutable; no es el saldo corriente del cliente.
CREATE TABLE IF NOT EXISTS `tblcierremovimientosdetalle` (
  `idCierreDetalle` INT NOT NULL AUTO_INCREMENT,
  `idCierre` INT NOT NULL,
  `idFactura` INT NOT NULL,
  `saldoInicial` DECIMAL(18,2) NOT NULL,
  `cargosPeriodo` DECIMAL(18,2) NOT NULL,
  `abonosPeriodo` DECIMAL(18,2) NOT NULL,
  `saldoFinal` DECIMAL(18,2) NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idCierreDetalle`),
  UNIQUE KEY `uq_tblcierremovimientosdetalle_1` (`idCierre`, `idFactura`),
  KEY `idx_tblcierremovimientosdetalle_1` (`idFactura`),
  CONSTRAINT `fk_tblcierremovimientosdetalle_1` FOREIGN KEY (`idCierre`)
    REFERENCES `tblcierremovimientos` (`idCierre`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcierremovimientosdetalle_2` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 20. tblreglacomision: Reglas acordadas para calcular comisiones.
-- Regla para implementar/validar: Porcentaje entre 0 y 100. Definir con el equipo si se comisiona venta o cobro, qué impuestos/descuentos se excluyen y cómo afectan devoluciones.
CREATE TABLE IF NOT EXISTS `tblreglacomision` (
  `idReglaComision` INT NOT NULL AUTO_INCREMENT,
  `codigoRegla` VARCHAR(30) NOT NULL,
  `nombreRegla` VARCHAR(100) NOT NULL,
  `baseCalculo` VARCHAR(30) NOT NULL COMMENT 'VENTA_NETA, COBRO_NETO',
  `porcentajeComision` DECIMAL(7,4) NOT NULL,
  `vigenteDesde` DATE NOT NULL,
  `vigenteHasta` DATE NULL,
  `requiereAutorizacion` TINYINT(1) NOT NULL DEFAULT 1,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idReglaComision`),
  UNIQUE KEY `uq_tblreglacomision_1` (`codigoRegla`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 21. tblcomision: Liquidación de comisión por vendedor, período y moneda.
-- Regla para implementar/validar: Totales se obtienen sumando el detalle. El enlace contable se trabaja en clases, sin formulario ni tablas internas de Contabilidad en esta propuesta.
CREATE TABLE IF NOT EXISTS `tblcomision` (
  `idComision` INT NOT NULL AUTO_INCREMENT,
  `numeroComision` VARCHAR(40) NOT NULL,
  `idVendedor` INT NOT NULL,
  `idReglaComision` INT NOT NULL,
  `idMoneda` INT NOT NULL,
  `fechaDesde` DATE NOT NULL,
  `fechaHasta` DATE NOT NULL,
  `estadoComision` VARCHAR(25) NOT NULL COMMENT 'BORRADOR, SOLICITADA, APROBADA, RECHAZADA, ENVIADA',
  `idUsuarioSolicita` INT NULL,
  `fechaSolicitud` DATETIME NULL,
  `idUsuarioAutoriza` INT NULL,
  `fechaAutorizacion` DATETIME NULL,
  `comentarioAutorizacion` VARCHAR(500) NULL,
  `referenciaContable` VARCHAR(100) NULL COMMENT 'referencia entregada por el módulo externo',
  `fechaEnvio` DATETIME NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idComision`),
  UNIQUE KEY `uq_tblcomision_1` (`numeroComision`),
  KEY `idx_tblcomision_1` (`idVendedor`, `fechaDesde`, `fechaHasta`),
  KEY `idx_tblcomision_2` (`idReglaComision`),
  KEY `idx_tblcomision_3` (`idMoneda`),
  KEY `idx_tblcomision_4` (`idUsuarioSolicita`),
  KEY `idx_tblcomision_5` (`idUsuarioAutoriza`),
  CONSTRAINT `fk_tblcomision_1` FOREIGN KEY (`idVendedor`)
    REFERENCES `tblvendedor` (`idVendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomision_2` FOREIGN KEY (`idReglaComision`)
    REFERENCES `tblreglacomision` (`idReglaComision`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomision_3` FOREIGN KEY (`idMoneda`)
    REFERENCES `tblmonedaventas` (`idMoneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomision_4` FOREIGN KEY (`idUsuarioSolicita`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomision_5` FOREIGN KEY (`idUsuarioAutoriza`)
    REFERENCES `tblusuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 22. tblcomisiondetalle: Base y tasa efectivamente aplicadas a cada venta o cobro.
-- Regla para implementar/validar: La tasa se congela para conservar el cálculo histórico. No comisionar otra vez la misma factura/abono en liquidaciones aprobadas; revisar esta regla también entre períodos.
CREATE TABLE IF NOT EXISTS `tblcomisiondetalle` (
  `idComisionDetalle` INT NOT NULL AUTO_INCREMENT,
  `idComision` INT NOT NULL,
  `idFactura` INT NOT NULL,
  `idAplicacionPago` INT NULL COMMENT 'requerido para COBRO_NETO',
  `baseComision` DECIMAL(18,2) NOT NULL,
  `porcentajeAplicado` DECIMAL(7,4) NOT NULL,
  `montoComision` DECIMAL(18,2) NOT NULL,
  `created_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idComisionDetalle`),
  KEY `idx_tblcomisiondetalle_1` (`idComision`),
  KEY `idx_tblcomisiondetalle_2` (`idFactura`),
  KEY `idx_tblcomisiondetalle_3` (`idAplicacionPago`),
  CONSTRAINT `fk_tblcomisiondetalle_1` FOREIGN KEY (`idComision`)
    REFERENCES `tblcomision` (`idComision`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomisiondetalle_2` FOREIGN KEY (`idFactura`)
    REFERENCES `tblfactura` (`idFactura`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `fk_tblcomisiondetalle_3` FOREIGN KEY (`idAplicacionPago`)
    REFERENCES `tblaplicacionpago` (`idAplicacionPago`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Extensiones funcionales sin cambiar las tablas utilizadas por otros equipos.
CREATE TABLE IF NOT EXISTS tblclientelistaprecios (
  idCliente INT NOT NULL, idMoneda INT NOT NULL, idListaPrecio INT NOT NULL,
  PRIMARY KEY(idCliente,idMoneda),
  FOREIGN KEY(idCliente) REFERENCES tblcliente(idCliente),
  FOREIGN KEY(idMoneda) REFERENCES tblmonedaventas(idMoneda),
  FOREIGN KEY(idListaPrecio) REFERENCES tbllistaprecios(idListaPrecio)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tblcotizacioncontrol (
  idCotizacion INT NOT NULL, idListaPrecio INT NOT NULL, idPedido INT NULL, idFactura INT NULL,
  PRIMARY KEY(idCotizacion), UNIQUE(idPedido), UNIQUE(idFactura),
  FOREIGN KEY(idCotizacion) REFERENCES tblcotizacion(idCotizacion),
  FOREIGN KEY(idListaPrecio) REFERENCES tbllistaprecios(idListaPrecio),
  FOREIGN KEY(idPedido) REFERENCES tblpedido(idPedido),
  FOREIGN KEY(idFactura) REFERENCES tblfactura(idFactura)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tblcontrolperiodocxc (
  idMoneda INT NOT NULL, fechaUltimoCierre DATE NULL,
  PRIMARY KEY(idMoneda), FOREIGN KEY(idMoneda) REFERENCES tblmonedaventas(idMoneda)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tblcorrelativoventas (
  prefijo VARCHAR(12) NOT NULL, ultimoNumero BIGINT NOT NULL DEFAULT 0, PRIMARY KEY(prefijo)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tblcierresaldocliente (
  idCierre INT NOT NULL, idCliente INT NOT NULL,
  codigoCliente VARCHAR(30) NOT NULL, nombreCliente VARCHAR(200) NOT NULL,
  saldoAnterior DECIMAL(18,2) NOT NULL, cargosPeriodo DECIMAL(18,2) NOT NULL,
  abonosPeriodo DECIMAL(18,2) NOT NULL, cargosAcumulados DECIMAL(18,2) NOT NULL,
  abonosAcumulados DECIMAL(18,2) NOT NULL, saldoActual DECIMAL(18,2) NOT NULL,
  PRIMARY KEY(idCierre,idCliente), FOREIGN KEY(idCierre) REFERENCES tblcierremovimientos(idCierre),
  FOREIGN KEY(idCliente) REFERENCES tblcliente(idCliente)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
-- Cuenta/empresa/concepto son identificadores EXTERNOS suministrados por Bancos.
-- La preparacion local nunca acredita saldos bancarios ni afirma que hubo un envio.
CREATE TABLE IF NOT EXISTS tbldepositoventas (
  idDeposito INT NOT NULL AUTO_INCREMENT, codigoDeposito VARCHAR(40) NOT NULL,
  fechaDeposito DATE NOT NULL, empresaReferencia VARCHAR(100) NOT NULL,
  bancoReferencia VARCHAR(100) NOT NULL, cuentaReferencia VARCHAR(100) NOT NULL,
  conceptoReferencia VARCHAR(100) NOT NULL, referenciaDeposito VARCHAR(100) NULL,
  idMoneda INT NOT NULL, total DECIMAL(18,2) NOT NULL, estadoDeposito VARCHAR(25) NOT NULL DEFAULT 'PREPARADO',
  movimientoBancario VARCHAR(100) NULL, idUsuarioRegistro INT NOT NULL,
  created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY(idDeposito), UNIQUE(codigoDeposito),
  FOREIGN KEY(idMoneda) REFERENCES tblmonedaventas(idMoneda),
  FOREIGN KEY(idUsuarioRegistro) REFERENCES tblusuario(idUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tbldepositoventasdetalle (
  idDeposito INT NOT NULL, idPago INT NOT NULL, importe DECIMAL(18,2) NOT NULL,
  PRIMARY KEY(idDeposito,idPago), FOREIGN KEY(idDeposito) REFERENCES tbldepositoventas(idDeposito),
  FOREIGN KEY(idPago) REFERENCES tblpago(idPago)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tblcuentacontablecxc (
  idMoneda INT NOT NULL, tipoMovimiento VARCHAR(30) NOT NULL,
  cuentaClientes VARCHAR(40) NOT NULL, cuentaContrapartida VARCHAR(40) NOT NULL,
  PRIMARY KEY(idMoneda,tipoMovimiento), FOREIGN KEY(idMoneda) REFERENCES tblmonedaventas(idMoneda)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
-- IF NOT EXISTS conserva los triggers ya instalados (MySQL 8.0.29 o posterior).
DELIMITER $$
CREATE TRIGGER IF NOT EXISTS trg_ventas_movimiento_insert BEFORE INSERT ON tblmovimientocxc FOR EACH ROW
BEGIN
  DECLARE moneda INT;
  DECLARE ultimo DATE;
  IF NEW.cargo < 0 OR NEW.abono < 0 OR ((NEW.cargo > 0) = (NEW.abono > 0)) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Un movimiento debe tener cargo o abono positivo, exclusivamente';
  END IF;
  SELECT idMoneda INTO moneda FROM tblfactura WHERE idFactura=NEW.idFactura;
  INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES(moneda) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda);
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=moneda FOR UPDATE;
  IF ultimo IS NOT NULL AND NEW.fechaMovimiento <= ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El periodo de CxC esta cerrado';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_movimiento_update BEFORE UPDATE ON tblmovimientocxc FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El libro CxC es inmutable; registre un reverso en un periodo abierto';
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_movimiento_delete BEFORE DELETE ON tblmovimientocxc FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se pueden eliminar movimientos del libro CxC';
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_snapshot_update BEFORE UPDATE ON tblcierresaldocliente FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico de saldos de cierre es inmutable';
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_snapshot_delete BEFORE DELETE ON tblcierresaldocliente FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico de saldos de cierre es inmutable';
END$$
DELIMITER ;

-- Las cabeceras financieras y fotografias tambien quedan protegidas tras el cierre.
DELIMITER $$
CREATE TRIGGER IF NOT EXISTS trg_ventas_factura_insert BEFORE INSERT ON tblfactura FOR EACH ROW
BEGIN
  DECLARE ultimo DATE;
  INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES(NEW.idMoneda) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda);
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=NEW.idMoneda FOR UPDATE;
  IF ultimo IS NOT NULL AND NEW.fechaEmision<=ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se registra una factura en un periodo cerrado';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_factura_update BEFORE UPDATE ON tblfactura FOR EACH ROW
BEGIN
  DECLARE ultimo DATE;
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=OLD.idMoneda FOR UPDATE;
  IF ultimo IS NOT NULL AND OLD.fechaEmision<=ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La factura pertenece a un periodo cerrado';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_cierre_update BEFORE UPDATE ON tblcierremovimientos FOR EACH ROW
BEGIN
  IF OLD.estadoCierre='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El cierre confirmado no puede modificarse';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_cierre_delete BEFORE DELETE ON tblcierremovimientos FOR EACH ROW
BEGIN
  IF OLD.estadoCierre='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El cierre confirmado no puede eliminarse';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_snapshot_insert BEFORE INSERT ON tblcierresaldocliente FOR EACH ROW
BEGIN
  IF (SELECT estadoCierre FROM tblcierremovimientos WHERE idCierre=NEW.idCierre)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan saldos a un cierre confirmado';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_detallecierre_insert BEFORE INSERT ON tblcierremovimientosdetalle FOR EACH ROW
BEGIN
  IF (SELECT estadoCierre FROM tblcierremovimientos WHERE idCierre=NEW.idCierre)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan facturas a un cierre confirmado';
  END IF;
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_detallecierre_update BEFORE UPDATE ON tblcierremovimientosdetalle FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle historico del cierre es inmutable';
END$$
CREATE TRIGGER IF NOT EXISTS trg_ventas_detallecierre_delete BEFORE DELETE ON tblcierremovimientosdetalle FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle historico del cierre es inmutable';
END$$
DELIMITER ;
-- Bandeja local e idempotente para el enlace contable. No escribe tablas de Contabilidad.
CREATE TABLE IF NOT EXISTS tbltrasladocxc (
  idCierre INT NOT NULL, claveIntegracion VARCHAR(80) NOT NULL,
  estado VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE', referenciaContable VARCHAR(100) NULL,
  idUsuario INT NOT NULL, fechaConfirmacion DATETIME NULL,
  PRIMARY KEY(idCierre), UNIQUE(claveIntegracion),
  FOREIGN KEY(idCierre) REFERENCES tblcierremovimientos(idCierre),
  FOREIGN KEY(idUsuario) REFERENCES tblusuario(idUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
CREATE TABLE IF NOT EXISTS tbltrasladocxcdetalle (
  idCierre INT NOT NULL, numeroLinea INT NOT NULL, cuenta VARCHAR(40) NOT NULL,
  descripcion VARCHAR(100) NOT NULL, debe DECIMAL(18,2) NOT NULL, haber DECIMAL(18,2) NOT NULL,
  PRIMARY KEY(idCierre,numeroLinea), FOREIGN KEY(idCierre) REFERENCES tbltrasladocxc(idCierre)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
DELIMITER $$
CREATE TRIGGER IF NOT EXISTS trg_ventas_factura_periodonuevo_update BEFORE UPDATE ON tblfactura FOR EACH ROW
BEGIN
  DECLARE ultimo DATE;
  INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES(NEW.idMoneda) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda);
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=NEW.idMoneda FOR UPDATE;
  IF ultimo IS NOT NULL AND NEW.fechaEmision<=ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se cambia la fecha o moneda de una factura hacia un periodo cerrado';
  END IF;
END$$
DELIMITER ;

-- Bitacora del CRUD. Conserva los cambios sin borrar el libro CxC ni los cierres.
CREATE TABLE IF NOT EXISTS tblbitacoracrudventas (
  idBitacora BIGINT NOT NULL AUTO_INCREMENT,
  entidad VARCHAR(40) NOT NULL,
  idRegistro INT NOT NULL,
  accion VARCHAR(40) NOT NULL,
  detalle TEXT NOT NULL,
  idUsuario INT NOT NULL,
  fecha DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (idBitacora),
  KEY idx_crudventas_registro (entidad,idRegistro),
  FOREIGN KEY (idUsuario) REFERENCES tblusuario(idUsuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;


SET @ventas_cargar_demo = 1;

DELIMITER $$
DROP PROCEDURE IF EXISTS sp_ventas_cargar_demo_20261020$$
CREATE PROCEDURE sp_ventas_cargar_demo_20261020()
demo: BEGIN
  DECLARE v_cierre INT;
  DECLARE v_deposito INT;
  DECLARE v_codigo CHAR(3);
  DECLARE v_usuario INT;
  DECLARE v_empleado INT;
  DECLARE v_vendedor INT;
  DECLARE v_moneda INT;
  DECLARE v_establecimiento INT;
  DECLARE v_lista INT;
  DECLARE v_cliente INT;
  DECLARE v_producto INT;
  DECLARE v_cotizacion INT;
  DECLARE v_pedido INT;
  DECLARE v_factura INT;
  DECLARE v_pago INT;
  DECLARE v_aplicacion INT;
  DECLARE v_nota INT;
  DECLARE v_regla INT;
  DECLARE v_comision INT;
  DECLARE v_i INT DEFAULT 1;
  DECLARE v_sufijo CHAR(3);
  DECLARE v_fecha DATE;
  DECLARE v_desde DATE;
  DECLARE v_hasta DATE;
  DECLARE v_ultimo DATE;
  DECLARE v_lock INT DEFAULT 0;
  DECLARE v_lock_nombre VARCHAR(64);
  DECLARE EXIT HANDLER FOR SQLEXCEPTION
  BEGIN
    ROLLBACK;
    IF v_lock=1 THEN
      DO RELEASE_LOCK(v_lock_nombre);
    END IF;
    RESIGNAL;
  END;

  IF COALESCE(@ventas_cargar_demo,0) <> 1 THEN
    SELECT 'Estructura lista. Datos ficticios omitidos.' AS resultado;
    LEAVE demo;
  END IF;
  SET v_lock_nombre=CONCAT('ventas_demo_',LEFT(SHA2(DATABASE(),256),40));
  SELECT GET_LOCK(v_lock_nombre,5) INTO v_lock;
  IF COALESCE(v_lock,0) <> 1 THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ya hay otra carga DEMO en curso. Intente de nuevo';
  END IF;
  START TRANSACTION;
  IF EXISTS(SELECT 1 FROM tblcorrelativoventas WHERE prefijo='Q10-001') THEN
    COMMIT;
    DO RELEASE_LOCK(v_lock_nombre);
    SET v_lock=0;
    SELECT 'Los ejemplos DEMO ya fueron cargados. Se conservan sus cambios.' AS resultado;
    LEAVE demo;
  END IF;

  -- Esta base es una muestra aislada. Los registros de Seguridad son ficticios.
  IF EXISTS(SELECT 1 FROM tblcliente) OR EXISTS(SELECT 1 FROM tblmonedaventas)
     OR EXISTS(SELECT 1 FROM tblempleado) OR EXISTS(SELECT 1 FROM tblusuario) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La muestra requiere tablas vacias o una carga DEMO10 ya completada';
  END IF;
  -- GTQ es la unica moneda. Todos los importes de esta muestra son quetzales.
  INSERT INTO tblmonedaventas(codigoMoneda,nombreMoneda,simboloMoneda)
    VALUES('GTQ','Quetzal','Q');
  SET v_moneda=LAST_INSERT_ID();
  INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES(v_moneda);
  INSERT INTO tblcuentacontablecxc(idMoneda,tipoMovimiento,cuentaClientes,cuentaContrapartida) VALUES
    (v_moneda,'FACTURA','DEMO-CLIENTES','DEMO-VENTAS'),
    (v_moneda,'PAGO','DEMO-CLIENTES','DEMO-CAJA'),
    (v_moneda,'NOTA_CREDITO','DEMO-CLIENTES','DEMO-AJUSTES');

  WHILE v_i<=10 DO
    SET v_sufijo=LPAD(v_i,3,'0');
    -- Diez meses consecutivos: no se crean diez cierres del mismo periodo en GTQ.
    SET v_fecha=DATE_SUB(CURRENT_DATE(),INTERVAL DAYOFMONTH(CURRENT_DATE())-1 DAY);
    SET v_desde=DATE_SUB(v_fecha,INTERVAL (11-v_i) MONTH);
    SET v_hasta=LAST_DAY(v_desde);
    SET v_fecha=v_desde;
    SET v_codigo='GTQ';
    INSERT INTO tblempleado(codigoEmpleado,dpiEmpleado,nitEmpleado,nombresEmpleado,apellidosEmpleado,
      puestoEmpleado,generoEmpleado,fechaNacimientoEmpleado,fechaContratacionEmpleado,telefonoEmpleado,correoEmpleado)
      VALUES(CONCAT('DEMO10-E-',v_sufijo),LPAD(v_i,13,'0'),LPAD(v_i,10,'0'),CONCAT('Empleado ficticio ',v_sufijo),
        'Pruebas Ventas','Vendedor de prueba','M','1995-01-01',v_desde,'00000000',CONCAT('empleado',v_sufijo,'@example.invalid'));
    SET v_empleado=LAST_INSERT_ID();
    INSERT INTO tblusuario(idEmpleado,nombreUsuario,contrasenaUsuario,ultimoAccesoUsuario)
      VALUES(v_empleado,CONCAT('demo10_',v_sufijo),'SIN_CREDENCIAL_VALIDA_SOLO_DATOS_DE_PRUEBA',CURRENT_TIMESTAMP);
    SET v_usuario=LAST_INSERT_ID();
    INSERT INTO tblvendedor(codigoVendedor,idEmpleado) VALUES(CONCAT('DEMO10-V-',v_sufijo),v_empleado);
    SET v_vendedor=LAST_INSERT_ID();
    INSERT INTO tblestablecimientoventas(codigoEstablecimiento,nombreEstablecimiento,nitEmisor,direccionEstablecimiento)
      VALUES(CONCAT('DEMO10-EST-',v_sufijo),CONCAT('Sucursal ficticia ',v_sufijo),'DEMO-SIN-VALIDEZ','Direccion de prueba');
    SET v_establecimiento=LAST_INSERT_ID();
    INSERT INTO tbllistaprecios(codigoListaPrecio,nombreListaPrecio,idMoneda,vigenteDesde,vigenteHasta)
      VALUES(CONCAT('DEMO10-LP-',v_sufijo),CONCAT('Lista de prueba ',v_codigo),v_moneda,v_desde,NULL);
    SET v_lista=LAST_INSERT_ID();

    INSERT INTO tblcliente(codigoCliente,tipoIdentificacion,numeroIdentificacion,nombreCliente,direccionCliente,
      contactoCliente,telefonoCliente,correoCliente,diasCredito,limiteCredito,idMonedaCredito)
      VALUES(CONCAT('DEMO-CLI-',v_sufijo),'CF','CF',CONCAT('Cliente ficticio ',v_sufijo),
        'Direccion ficticia de pruebas','Contacto de prueba','00000000',CONCAT('cliente',v_sufijo,'@example.invalid'),30,10000,v_moneda);
    SET v_cliente=LAST_INSERT_ID();
    INSERT INTO tblclientelistaprecios(idCliente,idMoneda,idListaPrecio) VALUES(v_cliente,v_moneda,v_lista);
    INSERT INTO tblproductoventas(codigoProducto,nombreProducto,descripcionProducto,tipoItem,unidadMedida,porcentajeImpuesto)
      VALUES(CONCAT('DEMO-PRO-',v_sufijo),CONCAT('Producto ficticio ',v_sufijo),'Solo para pruebas','B','UNIDAD',12);
    SET v_producto=LAST_INSERT_ID();
    INSERT INTO tbllistapreciosdetalle(idListaPrecio,idProducto,precioUnitario) VALUES(v_lista,v_producto,100);

    INSERT INTO tblcotizacion(numeroCotizacion,idCliente,idVendedor,idMoneda,fechaEmision,vigenteHasta,
      tipoCambio,estadoCotizacion,subtotal,descuentoTotal,impuestoTotal,total,observaciones,idUsuarioRegistro)
      VALUES(CONCAT('DEMO-COT-',v_sufijo),v_cliente,v_vendedor,v_moneda,CURRENT_DATE(),DATE_ADD(CURRENT_DATE(),INTERVAL 30 DAY),
        1,'BORRADOR',100,0,12,112,'Cotizacion ficticia editable',v_usuario);
    SET v_cotizacion=LAST_INSERT_ID();
    INSERT INTO tblcotizacioncontrol(idCotizacion,idListaPrecio) VALUES(v_cotizacion,v_lista);
    INSERT INTO tblcotizaciondetalle(idCotizacion,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,
      precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea)
      VALUES(v_cotizacion,1,v_producto,CONCAT('Producto ficticio ',v_sufijo),'B',1,100,0,12,12,112);

    -- Pedido independiente: no convierte ni bloquea la cotizacion de ejemplo.
    INSERT INTO tblpedido(numeroPedido,referenciaIndependiente,idCliente,idVendedor,idMoneda,fechaPedido,
      fechaEntrega,tipoCambio,estadoPedido,subtotal,descuentoTotal,impuestoTotal,total,observaciones,idUsuarioRegistro)
      VALUES(CONCAT('DEMO-PED-',v_sufijo),'Pedido ficticio independiente',v_cliente,v_vendedor,v_moneda,v_fecha,
        DATE_ADD(v_fecha,INTERVAL 3 DAY),1,'PENDIENTE',100,0,12,112,'Solo para pruebas',v_usuario);
    SET v_pedido=LAST_INSERT_ID();
    INSERT INTO tblpedidodetalle(idPedido,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,
      precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea)
      VALUES(v_pedido,1,v_producto,CONCAT('Producto ficticio ',v_sufijo),'B',1,100,0,12,12,112);

    INSERT INTO tblfactura(serieFactura,numeroFactura,idEstablecimiento,idCliente,idVendedor,idMoneda,
      fechaEmision,fechaVencimiento,tipoDte,condicionPago,diasCreditoAplicados,tipoCambio,
      tipoIdentificacionReceptor,identificacionReceptor,nombreReceptor,direccionReceptor,
      subtotal,descuentoTotal,impuestoTotal,total,estadoFactura,observaciones,idUsuarioRegistro)
      VALUES('CXC',CONCAT('DEMO-FAC-',v_sufijo),v_establecimiento,v_cliente,v_vendedor,v_moneda,
        v_fecha,DATE_ADD(v_fecha,INTERVAL 30 DAY),'INTERNO_CXC','CREDITO',30,1,
        'CF','CF',CONCAT('Cliente ficticio ',v_sufijo),'Direccion ficticia de pruebas',
        100,0,12,112,'EMITIDA','Documento ficticio de CxC; sin certificacion fiscal',v_usuario);
    SET v_factura=LAST_INSERT_ID();
    INSERT INTO tblfacturadetalle(idFactura,numeroLinea,idProducto,descripcionItem,tipoItem,cantidad,
      precioUnitario,descuentoMonto,porcentajeImpuesto,impuestoMonto,totalLinea)
      VALUES(v_factura,1,v_producto,CONCAT('Producto ficticio ',v_sufijo),'B',1,100,0,12,12,112);
    INSERT INTO tblmovimientocxc(idFactura,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro)
      VALUES(v_factura,v_fecha,'FACTURA',CONCAT('DEMO cargo factura ',v_sufijo),112,0,v_usuario);

    INSERT INTO tblpago(numeroRecibo,idCliente,idMoneda,fechaPago,montoPago,tipoCambio,formaPago,
      referenciaPago,estadoPago,observaciones,idUsuarioRegistro)
      VALUES(CONCAT('DEMO-REC-',v_sufijo),v_cliente,v_moneda,v_fecha,50,1,'EFECTIVO',
        CONCAT('DEMO-REF-',v_sufijo),'CONFIRMADO','Recibo ficticio aplicado a una factura',v_usuario);
    SET v_pago=LAST_INSERT_ID();
    INSERT INTO tblaplicacionpago(idPago,idFactura,fechaAplicacion,montoAplicado,estadoAplicacion,idUsuarioRegistro)
      VALUES(v_pago,v_factura,v_fecha,50,'APLICADA',v_usuario);
    SET v_aplicacion=LAST_INSERT_ID();
    INSERT INTO tblmovimientocxc(idFactura,idAplicacionPago,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro)
      VALUES(v_factura,v_aplicacion,v_fecha,'PAGO',CONCAT('DEMO abono recibo ',v_sufijo),0,50,v_usuario);

    INSERT INTO tblnotacxc(numeroNota,idFactura,tipoNota,fechaNota,monto,motivo,estadoNota,idUsuarioRegistro)
      VALUES(CONCAT('DEMO-NC-',v_sufijo),v_factura,'CREDITO',v_fecha,5,'Ajuste ficticio de prueba','APLICADA',v_usuario);
    SET v_nota=LAST_INSERT_ID();
    INSERT INTO tblmovimientocxc(idFactura,idNota,fechaMovimiento,tipoMovimiento,descripcionMovimiento,cargo,abono,idUsuarioRegistro)
      VALUES(v_factura,v_nota,v_fecha,'NOTA_CREDITO',CONCAT('DEMO nota de credito ',v_sufijo),0,5,v_usuario);

    INSERT INTO tblreglacomision(codigoRegla,nombreRegla,baseCalculo,porcentajeComision,vigenteDesde,vigenteHasta)
      VALUES(CONCAT('DEMO-REG-',v_sufijo),CONCAT('Regla ficticia ',v_i,' por ciento'),'VENTA_NETA',v_i,v_desde,NULL);
    SET v_regla=LAST_INSERT_ID();
    INSERT INTO tblcomision(numeroComision,idVendedor,idReglaComision,idMoneda,fechaDesde,fechaHasta,estadoComision,idUsuarioSolicita)
      VALUES(CONCAT('DEMO-LIQ-',v_sufijo),v_vendedor,v_regla,v_moneda,v_desde,v_hasta,'BORRADOR',v_usuario);
    SET v_comision=LAST_INSERT_ID();
    INSERT INTO tblcomisiondetalle(idComision,idFactura,baseComision,porcentajeAplicado,montoComision)
      VALUES(v_comision,v_factura,100,v_i,v_i);

    -- Deposito local PREPARADO: sin movimiento ni confirmacion bancaria real.
    INSERT INTO tbldepositoventas(codigoDeposito,fechaDeposito,empresaReferencia,bancoReferencia,
      cuentaReferencia,conceptoReferencia,referenciaDeposito,idMoneda,total,estadoDeposito,idUsuarioRegistro)
      VALUES(CONCAT('DEMO10-DEP-',v_sufijo),v_fecha,'EMPRESA_FICTICIA','BANCO_FICTICIO',
        CONCAT('CUENTA_PRUEBA_',v_sufijo),'COBRO_FICTICIO',CONCAT('DEMO10-REF-',v_sufijo),v_moneda,50,'PREPARADO',v_usuario);
    SET v_deposito=LAST_INSERT_ID();
    INSERT INTO tbldepositoventasdetalle(idDeposito,idPago,importe) VALUES(v_deposito,v_pago,50);
    -- Cierre mensual de GTQ. Incluye todos los saldos anteriores hasta esa fecha.
    INSERT INTO tblcierremovimientos(numeroCierre,fechaDesde,fechaHasta,fechaCorte,idMoneda,
      tipoCierre,estadoCierre,documentosRevisados,pagosConciliados,observaciones,idUsuarioResponsable)
      VALUES(CONCAT('DEMO10-CIE-',v_sufijo),v_desde,v_hasta,v_hasta,v_moneda,'MENSUAL','BORRADOR',1,1,
        'Historico ficticio de prueba. No corresponde a operaciones reales.',v_usuario);
    SET v_cierre=LAST_INSERT_ID();
    INSERT INTO tblcierremovimientosdetalle(idCierre,idFactura,saldoInicial,cargosPeriodo,abonosPeriodo,saldoFinal)
      SELECT v_cierre,f.idFactura,
        SUM(CASE WHEN m.fechaMovimiento<v_desde THEN m.cargo-m.abono ELSE 0 END),
        SUM(CASE WHEN m.fechaMovimiento BETWEEN v_desde AND v_hasta THEN m.cargo ELSE 0 END),
        SUM(CASE WHEN m.fechaMovimiento BETWEEN v_desde AND v_hasta THEN m.abono ELSE 0 END),
        SUM(m.cargo-m.abono)
      FROM tblfactura f JOIN tblmovimientocxc m ON m.idFactura=f.idFactura
      WHERE f.idMoneda=v_moneda AND m.fechaMovimiento<=v_hasta GROUP BY f.idFactura;
    INSERT INTO tblcierresaldocliente(idCierre,idCliente,codigoCliente,nombreCliente,saldoAnterior,
      cargosPeriodo,abonosPeriodo,cargosAcumulados,abonosAcumulados,saldoActual)
      SELECT v_cierre,c.idCliente,c.codigoCliente,c.nombreCliente,
        SUM(CASE WHEN m.fechaMovimiento<v_desde THEN m.cargo-m.abono ELSE 0 END),
        SUM(CASE WHEN m.fechaMovimiento BETWEEN v_desde AND v_hasta THEN m.cargo ELSE 0 END),
        SUM(CASE WHEN m.fechaMovimiento BETWEEN v_desde AND v_hasta THEN m.abono ELSE 0 END),
        SUM(m.cargo),SUM(m.abono),SUM(m.cargo-m.abono)
      FROM tblcliente c JOIN tblfactura f ON f.idCliente=c.idCliente
      JOIN tblmovimientocxc m ON m.idFactura=f.idFactura
      WHERE f.idMoneda=v_moneda AND m.fechaMovimiento<=v_hasta
      GROUP BY c.idCliente,c.codigoCliente,c.nombreCliente;
    UPDATE tblcierremovimientos SET estadoCierre='CONFIRMADO',fechaConfirmacion=CURRENT_TIMESTAMP WHERE idCierre=v_cierre;
    UPDATE tblcontrolperiodocxc SET fechaUltimoCierre=v_hasta WHERE idMoneda=v_moneda;
    INSERT INTO tbltrasladocxc(idCierre,claveIntegracion,estado,idUsuario)
      VALUES(v_cierre,CONCAT('DEMO10-TRASLADO-',v_sufijo),'PENDIENTE',v_usuario);
    INSERT INTO tbltrasladocxcdetalle(idCierre,numeroLinea,cuenta,descripcion,debe,haber) VALUES
      (v_cierre,1,'DEMO-CLIENTES','Saldo ficticio CxC',57,0),
      (v_cierre,2,'DEMO-CONTRAPARTIDA','Contrapartida ficticia',0,57);
    INSERT INTO tblbitacoracrudventas(entidad,idRegistro,accion,detalle,idUsuario)
      VALUES('DATOS_DEMO10',v_factura,'CARGAR','Datos ficticios: factura 112 - pago 50 - nota 5 = saldo 57',v_usuario);
    INSERT INTO tblcorrelativoventas(prefijo,ultimoNumero) VALUES(CONCAT('Q10-',v_sufijo),1);
    SET v_i=v_i+1;
  END WHILE;

  COMMIT;
  DO RELEASE_LOCK(v_lock_nombre);
  SET v_lock=0;
  SELECT 'Carga GTQ completada: 10 clientes, facturas, cotizaciones y recibos en quetzales.' AS resultado,
    'Por factura: Q 112.00 - pago Q 50.00 - nota Q 5.00 = saldo Q 57.00. Total Q 570.00' AS comprobacion;
END$$
CALL sp_ventas_cargar_demo_20261020()$$
DROP PROCEDURE sp_ventas_cargar_demo_20261020$$
DELIMITER ;

-- Verificacion de cantidades por tabla.
SELECT 'tblempleado' AS tabla, COUNT(*) AS registros FROM tblempleado
UNION ALL
SELECT 'tblusuario' AS tabla, COUNT(*) AS registros FROM tblusuario
UNION ALL
SELECT 'tblmonedaventas' AS tabla, COUNT(*) AS registros FROM tblmonedaventas
UNION ALL
SELECT 'tblestablecimientoventas' AS tabla, COUNT(*) AS registros FROM tblestablecimientoventas
UNION ALL
SELECT 'tblcliente' AS tabla, COUNT(*) AS registros FROM tblcliente
UNION ALL
SELECT 'tblvendedor' AS tabla, COUNT(*) AS registros FROM tblvendedor
UNION ALL
SELECT 'tblproductoventas' AS tabla, COUNT(*) AS registros FROM tblproductoventas
UNION ALL
SELECT 'tbllistaprecios' AS tabla, COUNT(*) AS registros FROM tbllistaprecios
UNION ALL
SELECT 'tbllistapreciosdetalle' AS tabla, COUNT(*) AS registros FROM tbllistapreciosdetalle
UNION ALL
SELECT 'tblcotizacion' AS tabla, COUNT(*) AS registros FROM tblcotizacion
UNION ALL
SELECT 'tblcotizaciondetalle' AS tabla, COUNT(*) AS registros FROM tblcotizaciondetalle
UNION ALL
SELECT 'tblpedido' AS tabla, COUNT(*) AS registros FROM tblpedido
UNION ALL
SELECT 'tblpedidodetalle' AS tabla, COUNT(*) AS registros FROM tblpedidodetalle
UNION ALL
SELECT 'tblfactura' AS tabla, COUNT(*) AS registros FROM tblfactura
UNION ALL
SELECT 'tblfacturadetalle' AS tabla, COUNT(*) AS registros FROM tblfacturadetalle
UNION ALL
SELECT 'tblnotacxc' AS tabla, COUNT(*) AS registros FROM tblnotacxc
UNION ALL
SELECT 'tblpago' AS tabla, COUNT(*) AS registros FROM tblpago
UNION ALL
SELECT 'tblaplicacionpago' AS tabla, COUNT(*) AS registros FROM tblaplicacionpago
UNION ALL
SELECT 'tblmovimientocxc' AS tabla, COUNT(*) AS registros FROM tblmovimientocxc
UNION ALL
SELECT 'tblcierremovimientos' AS tabla, COUNT(*) AS registros FROM tblcierremovimientos
UNION ALL
SELECT 'tblcierremovimientosdetalle' AS tabla, COUNT(*) AS registros FROM tblcierremovimientosdetalle
UNION ALL
SELECT 'tblreglacomision' AS tabla, COUNT(*) AS registros FROM tblreglacomision
UNION ALL
SELECT 'tblcomision' AS tabla, COUNT(*) AS registros FROM tblcomision
UNION ALL
SELECT 'tblcomisiondetalle' AS tabla, COUNT(*) AS registros FROM tblcomisiondetalle
UNION ALL
SELECT 'tblclientelistaprecios' AS tabla, COUNT(*) AS registros FROM tblclientelistaprecios
UNION ALL
SELECT 'tblcotizacioncontrol' AS tabla, COUNT(*) AS registros FROM tblcotizacioncontrol
UNION ALL
SELECT 'tblcontrolperiodocxc' AS tabla, COUNT(*) AS registros FROM tblcontrolperiodocxc
UNION ALL
SELECT 'tblcorrelativoventas' AS tabla, COUNT(*) AS registros FROM tblcorrelativoventas
UNION ALL
SELECT 'tblcierresaldocliente' AS tabla, COUNT(*) AS registros FROM tblcierresaldocliente
UNION ALL
SELECT 'tbldepositoventas' AS tabla, COUNT(*) AS registros FROM tbldepositoventas
UNION ALL
SELECT 'tbldepositoventasdetalle' AS tabla, COUNT(*) AS registros FROM tbldepositoventasdetalle
UNION ALL
SELECT 'tblcuentacontablecxc' AS tabla, COUNT(*) AS registros FROM tblcuentacontablecxc
UNION ALL
SELECT 'tbltrasladocxc' AS tabla, COUNT(*) AS registros FROM tbltrasladocxc
UNION ALL
SELECT 'tbltrasladocxcdetalle' AS tabla, COUNT(*) AS registros FROM tbltrasladocxcdetalle
UNION ALL
SELECT 'tblbitacoracrudventas' AS tabla, COUNT(*) AS registros FROM tblbitacoracrudventas;
