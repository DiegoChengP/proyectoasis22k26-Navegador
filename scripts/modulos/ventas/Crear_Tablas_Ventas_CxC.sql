-- INICIO — Jose Torres | Carné: 0901-23-1091 | Fecha: 13/10/2026
-- Ventas y cuentas por cobrar: 22 tablas de la propuesta revisable.
-- Archivo preparado localmente. NO se ha ejecutado contra ninguna base de datos.
-- No contiene DROP, DELETE, TRUNCATE, datos de ejemplo ni credenciales.
--
-- REQUISITOS ANTES DE EJECUTAR EN MYSQL WORKBENCH:
-- 1. Tener en dbsistemaembutidos las tablas EXISTENTES de Seguridad:
--    tblempleado(idEmpleado INT) y tblusuario(idUsuario INT), ambas InnoDB.
--    Sus claves deben ser INT firmado, conforme al script local de Seguridad.
--    Este archivo NO crea ni modifica dichas tablas; solamente referencia sus PK.
-- 2. Verificar catálogos compartidos con Inventarios/empresa antes de ejecutar.
--    tblproductoventas, tblmonedaventas y tblestablecimientoventas son propuestos.
-- 3. Si estas tablas ya existen, revisar su definición; el archivo no las reemplaza.
--    CREATE TABLE se detiene ante tablas existentes para no ocultar diferencias.
-- 4. Seleccionar y ejecutar el archivo completo en el orden presentado.
--    El DDL puede quedar aplicado parcialmente si una sentencia falla.
--
-- Mantiene la nomenclatura del script de Seguridad: tbl..., atributos camelCase.
-- Monedas, estados y tasas fiscales deben acordarse con cada equipo.
-- Importes DECIMAL: guardar números, no cadenas con el símbolo Q.

CREATE DATABASE IF NOT EXISTS `dbsistemaembutidos`
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `dbsistemaembutidos`;

-- 01. tblmonedaventas: Catálogo de monedas; reutilizar el catálogo general si existe.
CREATE TABLE `tblmonedaventas` (
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
CREATE TABLE `tblestablecimientoventas` (
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
CREATE TABLE `tblcliente` (
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
CREATE TABLE `tblvendedor` (
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
CREATE TABLE `tblproductoventas` (
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
CREATE TABLE `tbllistaprecios` (
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
CREATE TABLE `tbllistapreciosdetalle` (
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
CREATE TABLE `tblcotizacion` (
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
CREATE TABLE `tblcotizaciondetalle` (
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
CREATE TABLE `tblpedido` (
  `idPedido` INT NOT NULL AUTO_INCREMENT,
  `numeroPedido` VARCHAR(40) NOT NULL,
  `idCotizacion` INT NULL COMMENT 'NULL si independiente',
  `referenciaIndependiente` VARCHAR(100) NULL,
  `idCliente` INT NOT NULL,
  `idVendedor` INT NOT NULL,
  `idMoneda` INT NOT NULL,
  `fechaPedido` DATE NOT NULL,
  `fechaEntrega` DATE NULL,
  `tipoCambio` DECIMAL(18,6) NOT NULL DEFAULT 1,
  `estadoPedido` VARCHAR(25) NOT NULL COMMENT 'BORRADOR, CONFIRMADO, PARCIAL, COMPLETADO, ANULADO',
  `subtotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `descuentoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `impuestoTotal` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `total` DECIMAL(18,2) NOT NULL DEFAULT 0,
  `observaciones` VARCHAR(500) NULL,
  `idUsuarioRegistro` INT NOT NULL,
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
CREATE TABLE `tblpedidodetalle` (
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
CREATE TABLE `tblfactura` (
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
CREATE TABLE `tblfacturadetalle` (
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
CREATE TABLE `tblnotacxc` (
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
CREATE TABLE `tblpago` (
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
CREATE TABLE `tblaplicacionpago` (
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
CREATE TABLE `tblmovimientocxc` (
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
-- Regla para implementar/validar: fechaDesde <= fechaHasta <= fechaCorte. Las casillas son constancias del responsable, no validaciones automáticas. Definir con el ingeniero si un cierre bloquea registros y cómo se reabre antes de programarlo.
CREATE TABLE `tblcierremovimientos` (
  `idCierre` INT NOT NULL AUTO_INCREMENT,
  `numeroCierre` VARCHAR(40) NOT NULL,
  `fechaDesde` DATE NOT NULL,
  `fechaHasta` DATE NOT NULL,
  `fechaCorte` DATE NOT NULL,
  `idMoneda` INT NOT NULL,
  `tipoCierre` VARCHAR(20) NOT NULL COMMENT 'DIARIO, MENSUAL, PERSONALIZADO',
  `estadoCierre` VARCHAR(20) NOT NULL COMMENT 'BORRADOR, CONFIRMADO, REABIERTO',
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
CREATE TABLE `tblcierremovimientosdetalle` (
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
CREATE TABLE `tblreglacomision` (
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
CREATE TABLE `tblcomision` (
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
CREATE TABLE `tblcomisiondetalle` (
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

-- REGLAS QUE LAS FK NO RESUELVEN POR SÍ SOLAS
-- Implementar en CapaModelo/Controlador, dentro de transacciones:
-- * Cantidad > 0; precios, descuentos, impuestos y cargos/abonos no negativos.
-- * Cada movimiento debe tener cargo > 0 XOR abono > 0, con el otro importe en 0.
-- * Solo un cargo original de tipo FACTURA por factura emitida; validar con bloqueo.
-- * Pago, aplicación y factura deben pertenecer al mismo cliente y moneda.
-- * Aplicaciones vigentes no exceden el pago ni el saldo disponible de la factura.
-- * Estados VARCHAR se validan contra su catálogo y transiciones acordadas.
-- * Emitir factura + detalles + cargo CxC: una transacción.
-- * Confirmar pago + aplicaciones + abonos: una transacción.
-- * Revertir produce otro movimiento opuesto con su fecha efectiva; no borrar el original.
-- * Revertir un reverso se puede modelar apuntando al reverso anterior.
-- * Facturas emitidas y sus datos históricos no se recalculan con cambios de catálogos.
-- * Cierres confirmados conservan su fotografía. Reaperturas requieren versionado
--   o una tabla de eventos que se definirá antes de implementar esa funcionalidad.
-- * No sumar distintas monedas sin una política explícita de conversión.
-- * Anticipos y pagos en otra moneda quedan fuera de esta primera propuesta.
--
-- REPORTE DE ANTIGÜEDAD: NO EXISTEN TABLAS PARA 30/60/90 DÍAS.
-- Saldo al corte por factura = SUM(cargo - abono) con fechaMovimiento <= fechaCorte.
-- Vigente si fechaVencimiento > fechaCorte.
-- De lo contrario: 0-30, 31-60, 61-90 o más de 90 días de diferencia.
-- Considerar reversos según su fecha, nunca excluir todo el historial por estado actual.
-- Saldo final del período = saldo inicial + cargos del período - abonos del período.
-- Estos reportes se construirán con consultas; el script solo crea estructuras.
--
-- FIN — Jose Torres | Carné: 0901-23-1091 | Fecha: 13/10/2026
