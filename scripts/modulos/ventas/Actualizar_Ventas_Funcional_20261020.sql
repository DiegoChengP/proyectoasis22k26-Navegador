-- Jose Javier Torres Martinez | 0901-23-1091 | 20/10/2026
-- Ejecutar en la base seleccionada. Requiere Seguridad. No cambia la base seleccionada.
-- Tablas existentes conservadas; no contiene datos de demostracion.
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
-- Regla para implementar/validar: fechaDesde <= fechaHasta <= fechaCorte. Las casillas son constancias del responsable, no validaciones automáticas. Definir con el ingeniero si un cierre bloquea registros y cómo se reabre antes de programarlo.
CREATE TABLE IF NOT EXISTS `tblcierremovimientos` (
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
-- Este archivo se ejecuta una vez. El instalador comprueba los triggers existentes.
DELIMITER $$
CREATE TRIGGER trg_ventas_movimiento_insert BEFORE INSERT ON tblmovimientocxc FOR EACH ROW
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
CREATE TRIGGER trg_ventas_movimiento_update BEFORE UPDATE ON tblmovimientocxc FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El libro CxC es inmutable; registre un reverso en un periodo abierto';
END$$
CREATE TRIGGER trg_ventas_movimiento_delete BEFORE DELETE ON tblmovimientocxc FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se pueden eliminar movimientos del libro CxC';
END$$
CREATE TRIGGER trg_ventas_snapshot_update BEFORE UPDATE ON tblcierresaldocliente FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico de saldos de cierre es inmutable';
END$$
CREATE TRIGGER trg_ventas_snapshot_delete BEFORE DELETE ON tblcierresaldocliente FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico de saldos de cierre es inmutable';
END$$
DELIMITER ;

-- Las cabeceras financieras y fotografias tambien quedan protegidas tras el cierre.
DELIMITER $$
CREATE TRIGGER trg_ventas_factura_insert BEFORE INSERT ON tblfactura FOR EACH ROW
BEGIN
  DECLARE ultimo DATE;
  INSERT INTO tblcontrolperiodocxc(idMoneda) VALUES(NEW.idMoneda) ON DUPLICATE KEY UPDATE idMoneda=VALUES(idMoneda);
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=NEW.idMoneda FOR UPDATE;
  IF ultimo IS NOT NULL AND NEW.fechaEmision<=ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se registra una factura en un periodo cerrado';
  END IF;
END$$
CREATE TRIGGER trg_ventas_factura_update BEFORE UPDATE ON tblfactura FOR EACH ROW
BEGIN
  DECLARE ultimo DATE;
  SELECT fechaUltimoCierre INTO ultimo FROM tblcontrolperiodocxc WHERE idMoneda=OLD.idMoneda FOR UPDATE;
  IF ultimo IS NOT NULL AND OLD.fechaEmision<=ultimo THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La factura pertenece a un periodo cerrado';
  END IF;
END$$
CREATE TRIGGER trg_ventas_cierre_update BEFORE UPDATE ON tblcierremovimientos FOR EACH ROW
BEGIN
  IF OLD.estadoCierre='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El cierre confirmado no puede modificarse';
  END IF;
END$$
CREATE TRIGGER trg_ventas_cierre_delete BEFORE DELETE ON tblcierremovimientos FOR EACH ROW
BEGIN
  IF OLD.estadoCierre='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El cierre confirmado no puede eliminarse';
  END IF;
END$$
CREATE TRIGGER trg_ventas_snapshot_insert BEFORE INSERT ON tblcierresaldocliente FOR EACH ROW
BEGIN
  IF (SELECT estadoCierre FROM tblcierremovimientos WHERE idCierre=NEW.idCierre)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan saldos a un cierre confirmado';
  END IF;
END$$
CREATE TRIGGER trg_ventas_detallecierre_insert BEFORE INSERT ON tblcierremovimientosdetalle FOR EACH ROW
BEGIN
  IF (SELECT estadoCierre FROM tblcierremovimientos WHERE idCierre=NEW.idCierre)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan facturas a un cierre confirmado';
  END IF;
END$$
CREATE TRIGGER trg_ventas_detallecierre_update BEFORE UPDATE ON tblcierremovimientosdetalle FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle historico del cierre es inmutable';
END$$
CREATE TRIGGER trg_ventas_detallecierre_delete BEFORE DELETE ON tblcierremovimientosdetalle FOR EACH ROW
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
CREATE TRIGGER trg_ventas_factura_periodonuevo_update BEFORE UPDATE ON tblfactura FOR EACH ROW
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
