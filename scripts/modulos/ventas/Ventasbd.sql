CREATE DATABASE IF NOT EXISTS dbventas
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE dbventas;

CREATE TABLE `tblCliente` (
  `Pk_Cliente` INT NOT NULL AUTO_INCREMENT,
  `codigoCliente` VARCHAR(30) NOT NULL,
  `tipoIdentificacionCliente` ENUM('NIT','CUI','EXTRANJERO','CF') NOT NULL,
  `nombreCliente` VARCHAR(100) NOT NULL,
  `direccionCliente` VARCHAR(500) NULL,
  `nombreContactoCliente` VARCHAR(100) NULL,
  `telefonoCliente` VARCHAR(30) NULL,
  `correoCliente` VARCHAR(150) NULL,
  `diasCreditoCliente` INT NOT NULL DEFAULT 0,
  `limiteCreditoCliente` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `Fk_Moneda_Credito` BIGINT NOT NULL,
  `activoCliente` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `nitCliente` CHAR(10) NULL,
  `dpiCliente` CHAR(13) NULL,
  `identificacionExtranjeraCliente` VARCHAR(30) NULL,
  CONSTRAINT `Pk_Cliente` PRIMARY KEY (`Pk_Cliente`),
  CONSTRAINT `uqClientecodigo` UNIQUE (`codigoCliente`),
  CONSTRAINT `uqClientenit` UNIQUE (`nitCliente`),
  CONSTRAINT `uqClientedpi` UNIQUE (`dpiCliente`),
  CONSTRAINT `uqClienteidentificacionExtranjera` UNIQUE (`identificacionExtranjeraCliente`),
  CONSTRAINT `Fk_Cliente_Moneda_Credito` FOREIGN KEY (`Fk_Moneda_Credito`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxClienteMonedaCredito` (`Fk_Moneda_Credito`),
  CONSTRAINT `ckClienteactivoClienteBooleano` CHECK (activoCliente IN (0,1)),
  CONSTRAINT `ckClienteIdentificacionCoherente` CHECK ((tipoIdentificacionCliente = 'CF' AND nitCliente IS NULL AND dpiCliente IS NULL AND identificacionExtranjeraCliente IS NULL) OR (tipoIdentificacionCliente = 'NIT' AND nitCliente IS NOT NULL AND dpiCliente IS NULL AND identificacionExtranjeraCliente IS NULL) OR (tipoIdentificacionCliente = 'CUI' AND dpiCliente IS NOT NULL AND nitCliente IS NULL AND identificacionExtranjeraCliente IS NULL) OR (tipoIdentificacionCliente = 'EXTRANJERO' AND identificacionExtranjeraCliente IS NOT NULL AND nitCliente IS NULL AND dpiCliente IS NULL)),
  CONSTRAINT `ckClienteCreditoNoNegativo` CHECK (diasCreditoCliente >= 0 AND limiteCreditoCliente >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblVendedor` (
  `Pk_Vendedor` INT NOT NULL AUTO_INCREMENT,
  `codigoVendedor` VARCHAR(30) NOT NULL,
  `Fk_Empleado_Codigo` INT NOT NULL,
  `activoVendedor` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_Vendedor` PRIMARY KEY (`Pk_Vendedor`),
  CONSTRAINT `uqVendedorcodigo` UNIQUE (`codigoVendedor`),
  CONSTRAINT `uqVendedorEmpleadoCodigo` UNIQUE (`Fk_Empleado_Codigo`),
  CONSTRAINT `Fk_Vendedor_Empleado` FOREIGN KEY (`Fk_Empleado_Codigo`)
    REFERENCES `tblEmpleado` (`idEmpleado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ckVendedoractivoVendedorBooleano` CHECK (activoVendedor IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblProductoVenta` (
  `Pk_ProductoVenta` INT NOT NULL,
  `tipoItemProductoVenta` CHAR(1) NOT NULL,
  `unidadMedidaProductoVenta` VARCHAR(30) NOT NULL,
  `tasaImpuestoProductoVenta` DECIMAL(3,2) NOT NULL DEFAULT 0,
  `activoProductoVenta` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ProductoVenta` PRIMARY KEY (`Pk_ProductoVenta`),
  CONSTRAINT `Fk_ProductoVenta_Producto` FOREIGN KEY (`Pk_ProductoVenta`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ckProductoVentatasaImpuestoProductoVentaRango` CHECK (tasaImpuestoProductoVenta BETWEEN 0 AND 0.99),
  CONSTRAINT `ckProductoVentaactivoProductoVentaBooleano` CHECK (activoProductoVenta IN (0,1)),
  CONSTRAINT `ckProductoVentaTipoItemValido` CHECK (tipoItemProductoVenta IN ('B','S'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblListaPrecioEncabezado` (
  `Pk_ListaPrecioEncabezado` INT NOT NULL AUTO_INCREMENT,
  `codigoListaPrecioEncabezado` VARCHAR(30) NOT NULL,
  `nombreListaPrecioEncabezado` VARCHAR(100) NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `vigenteDesdeListaPrecioEncabezado` DATE NOT NULL,
  `vigenteHastaListaPrecioEncabezado` DATE NULL,
  `activoListaPrecioEncabezado` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ListaPrecioEncabezado` PRIMARY KEY (`Pk_ListaPrecioEncabezado`),
  CONSTRAINT `uqListaPrecioEncabezadocodigo` UNIQUE (`codigoListaPrecioEncabezado`),
  CONSTRAINT `Fk_ListaPrecioEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxListaPrecioEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  CONSTRAINT `ckListaPrecioEncabezadoactivoListaPrecioEncabezadoBooleano` CHECK (activoListaPrecioEncabezado IN (0,1)),
  CONSTRAINT `ckListaPrecioEncabezadoFechasCoherentes` CHECK (vigenteHastaListaPrecioEncabezado >= vigenteDesdeListaPrecioEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCierreMovimientoCxCEncabezado` (
  `Pk_CierreMovimientoCxCEncabezado` INT NOT NULL AUTO_INCREMENT,
  `numeroCierreMovimientoCxCEncabezado` VARCHAR(30) NOT NULL,
  `fechaDesdeCierreMovimientoCxCEncabezado` DATE NOT NULL,
  `fechaHastaCierreMovimientoCxCEncabezado` DATE NOT NULL,
  `fechaCorteCierreMovimientoCxCEncabezado` DATE NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `tipoCierreMovimientoCxCEncabezado` VARCHAR(20) NOT NULL,
  `estadoCierreMovimientoCxCEncabezado` VARCHAR(20) NOT NULL,
  `documentosRevisadosCierreMovimientoCxCEncabezado` BOOLEAN NOT NULL DEFAULT 0,
  `pagosConciliadosCierreMovimientoCxCEncabezado` BOOLEAN NOT NULL DEFAULT 0,
  `observacionesCierreMovimientoCxCEncabezado` TEXT NULL,
  `Fk_Usuario_Responsable` INT NOT NULL,
  `fechaConfirmacionCierreMovimientoCxCEncabezado` DATETIME NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CierreMovimientoCxCEncabezado` PRIMARY KEY (`Pk_CierreMovimientoCxCEncabezado`),
  CONSTRAINT `uqCierreMovimientoCxCEncabezadonumero` UNIQUE (`numeroCierreMovimientoCxCEncabezado`),
  CONSTRAINT `Fk_CierreMovimientoCxCEncabezado_Usuario_Responsable` FOREIGN KEY (`Fk_Usuario_Responsable`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CierreMovimientoCxCEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxCierreMovimientoCxCEncabezadoMonedaCodigofechaDesdefechaHasta` (`Fk_Moneda_Codigo`, `fechaDesdeCierreMovimientoCxCEncabezado`, `fechaHastaCierreMovimientoCxCEncabezado`),
  KEY `idxCierreMovimientoCxCEncabezadoUsuarioResponsable` (`Fk_Usuario_Responsable`),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadodocumentosRevisadosCierra3aaae2a` CHECK (documentosRevisadosCierreMovimientoCxCEncabezado IN (0,1)),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadopagosConciliadosCierreMob8223bf3` CHECK (pagosConciliadosCierreMovimientoCxCEncabezado IN (0,1)),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoValoresTipoCierre` CHECK (tipoCierreMovimientoCxCEncabezado IN ('MENSUAL','ANUAL')),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoValoresEstadoCierre` CHECK (estadoCierreMovimientoCxCEncabezado IN ('BORRADOR','CONFIRMADO','ANULADO')),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoFechasCoherentes` CHECK (fechaHastaCierreMovimientoCxCEncabezado >= fechaDesdeCierreMovimientoCxCEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblReglaComision` (
  `Pk_ReglaComision` INT NOT NULL AUTO_INCREMENT,
  `codigoReglaComision` VARCHAR(30) NOT NULL,
  `nombreReglaComision` VARCHAR(100) NOT NULL,
  `baseCalculoReglaComision` VARCHAR(30) NOT NULL,
  `tasaComisionReglaComision` DECIMAL(3,2) NOT NULL,
  `vigenteDesdeReglaComision` DATE NOT NULL,
  `vigenteHastaReglaComision` DATE NULL,
  `requiereAutorizacionReglaComision` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ReglaComision` PRIMARY KEY (`Pk_ReglaComision`),
  CONSTRAINT `uqReglaComisioncodigo` UNIQUE (`codigoReglaComision`),
  CONSTRAINT `ckReglaComisiontasaComisionReglaComisionRango` CHECK (tasaComisionReglaComision BETWEEN 0 AND 0.99),
  CONSTRAINT `ckReglaComisionrequiereAutorizacionReglaComisionBooleano` CHECK (requiereAutorizacionReglaComision IN (0,1)),
  CONSTRAINT `ckReglaComisionValoresBaseCalculo` CHECK (baseCalculoReglaComision IN ('VENTA_NETA','COBRO_NETO')),
  CONSTRAINT `ckReglaComisionFechasCoherentes` CHECK (vigenteHastaReglaComision >= vigenteDesdeReglaComision)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblControlPeriodoCxC` (
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaUltimoCierreControlPeriodoCxC` DATE NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ControlPeriodoCxC` PRIMARY KEY (`Fk_Moneda_Codigo`),
  CONSTRAINT `Fk_ControlPeriodoCxC_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCorrelativoVenta` (
  `prefijoCorrelativoVenta` VARCHAR(30) NOT NULL,
  `ultimoNumeroCorrelativoVenta` BIGINT NOT NULL DEFAULT 0,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CorrelativoVenta` PRIMARY KEY (`prefijoCorrelativoVenta`),
  CONSTRAINT `ckCorrelativoVentaNumeroNoNegativo` CHECK (ultimoNumeroCorrelativoVenta >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblDepositoVentaEncabezado` (
  `Pk_DepositoVentaEncabezado` INT NOT NULL AUTO_INCREMENT,
  `codigoDepositoVentaEncabezado` VARCHAR(30) NOT NULL,
  `fechaDepositoVentaEncabezado` DATE NOT NULL,
  `referenciaDepositoVentaEncabezado` VARCHAR(100) NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `totalDepositoVentaEncabezado` DECIMAL(12,4) NOT NULL,
  `estadoDepositoVentaEncabezado` VARCHAR(25) NOT NULL DEFAULT 'PREPARADO',
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Fk_CuentaBancaria_Codigo` BIGINT NOT NULL,
  `Fk_TipoTransaccion_Codigo` BIGINT NOT NULL,
  `Fk_MovimientoBancario_Codigo` BIGINT NULL,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_DepositoVentaEncabezado` PRIMARY KEY (`Pk_DepositoVentaEncabezado`),
  CONSTRAINT `uqDepositoVentaEncabezadocodigo` UNIQUE (`codigoDepositoVentaEncabezado`),
  CONSTRAINT `uqDepositoVentaEncabezadoMovimientoBancarioCodigo` UNIQUE (`Fk_MovimientoBancario_Codigo`),
  CONSTRAINT `Fk_DepositoVentaEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_DepositoVentaEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_DepositoVentaEncabezado_CuentaBancaria` FOREIGN KEY (`Fk_CuentaBancaria_Codigo`)
    REFERENCES `tblCuentaBancaria` (`Pk_CuentaBancaria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_DepositoVentaEncabezado_TipoTransaccion` FOREIGN KEY (`Fk_TipoTransaccion_Codigo`)
    REFERENCES `tblTipoTransaccion` (`Pk_TipoTransaccion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_DepositoVentaEncabezado_MovimientoBancario` FOREIGN KEY (`Fk_MovimientoBancario_Codigo`)
    REFERENCES `tblMovimientoBancario` (`Pk_MovimientoBancario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxDepositoVentaEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxDepositoVentaEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxDepositoVentaEncabezadoCuentaBancariaCodigo` (`Fk_CuentaBancaria_Codigo`),
  KEY `idxDepositoVentaEncabezadoTipoTransaccionCodigo` (`Fk_TipoTransaccion_Codigo`),
  CONSTRAINT `ckDepositoVentaEncabezadoTotalPositivo` CHECK (totalDepositoVentaEncabezado > 0),
  CONSTRAINT `ckDepositoVentaEncabezadoValoresEstadoDeposito` CHECK (estadoDepositoVentaEncabezado IN ('PREPARADO','ENVIADO','CONFIRMADO','ANULADO'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCuentaContableCxC` (
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `tipoCuentaContableCxC` VARCHAR(30) NOT NULL,
  `cuentaClientesCuentaContableCxC` VARCHAR(30) NOT NULL,
  `cuentaContrapartidaCuentaContableCxC` VARCHAR(30) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CuentaContableCxC` PRIMARY KEY (`Fk_Moneda_Codigo`, `tipoCuentaContableCxC`),
  CONSTRAINT `Fk_CuentaContableCxC_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblFormaPagoVenta` (
  `Pk_FormaPagoVenta` INT NOT NULL AUTO_INCREMENT,
  `codigoFormaPagoVenta` VARCHAR(30) NOT NULL,
  `nombreFormaPagoVenta` VARCHAR(100) NOT NULL,
  `requiereReferenciaFormaPagoVenta` BOOLEAN NOT NULL DEFAULT FALSE,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_FormaPagoVenta` PRIMARY KEY (`Pk_FormaPagoVenta`),
  CONSTRAINT `uqFormaPagoVentacodigo` UNIQUE (`codigoFormaPagoVenta`),
  CONSTRAINT `ckFormaPagoVentarequiereReferenciaFormaPagoVentaBooleano` CHECK (requiereReferenciaFormaPagoVenta IN (0,1))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblListaPrecioDetalle` (
  `Pk_ListaPrecioDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_ListaPrecioEncabezado_Codigo` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `precioUnitarioListaPrecioDetalle` DECIMAL(12,4) NOT NULL,
  `activoListaPrecioDetalle` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ListaPrecioDetalle` PRIMARY KEY (`Pk_ListaPrecioDetalle`),
  CONSTRAINT `uqListaPrecioDetalleListaPrecioEncabezadoCodigoProductoCodigo` UNIQUE (`Fk_ListaPrecioEncabezado_Codigo`, `Fk_Producto_Codigo`),
  CONSTRAINT `Fk_ListaPrecioDetalle_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`Pk_ListaPrecioEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ListaPrecioDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxListaPrecioDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckListaPrecioDetalleactivoListaPrecioDetalleBooleano` CHECK (activoListaPrecioDetalle IN (0,1)),
  CONSTRAINT `ckListaPrecioDetallePrecioNoNegativo` CHECK (precioUnitarioListaPrecioDetalle >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCotizacionEncabezado` (
  `Pk_CotizacionEncabezado` INT NOT NULL AUTO_INCREMENT,
  `numeroCotizacionEncabezado` VARCHAR(30) NOT NULL,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_Vendedor_Codigo` INT NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaEmisionCotizacionEncabezado` DATE NOT NULL,
  `vigenteHastaCotizacionEncabezado` DATE NOT NULL,
  `estadoCotizacionEncabezado` VARCHAR(25) NOT NULL,
  `observacionesCotizacionEncabezado` TEXT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Fk_ListaPrecioEncabezado_Codigo` INT NOT NULL,
  CONSTRAINT `Pk_CotizacionEncabezado` PRIMARY KEY (`Pk_CotizacionEncabezado`),
  CONSTRAINT `uqCotizacionEncabezadonumero` UNIQUE (`numeroCotizacionEncabezado`),
  CONSTRAINT `Fk_CotizacionEncabezado_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CotizacionEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`Pk_Vendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CotizacionEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CotizacionEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CotizacionEncabezado_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`Pk_ListaPrecioEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxCotizacionEncabezadoClienteCodigo` (`Fk_Cliente_Codigo`),
  KEY `idxCotizacionEncabezadoVendedorCodigo` (`Fk_Vendedor_Codigo`),
  KEY `idxCotizacionEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxCotizacionEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxCotizacionEncabezadoListaPrecioEncabezadoCodigo` (`Fk_ListaPrecioEncabezado_Codigo`),
  CONSTRAINT `ckCotizacionEncabezadoValoresEstadoCotizacion` CHECK (estadoCotizacionEncabezado IN ('BORRADOR','EMITIDA','ACEPTADA','RECHAZADA','VENCIDA','ANULADA')),
  CONSTRAINT `ckCotizacionEncabezadoFechasCoherentes` CHECK (vigenteHastaCotizacionEncabezado >= fechaEmisionCotizacionEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblReciboPago` (
  `Pk_ReciboPago` INT NOT NULL AUTO_INCREMENT,
  `numeroReciboPago` VARCHAR(30) NOT NULL,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaReciboPago` DATE NOT NULL,
  `montoReciboPago` DECIMAL(12,4) NOT NULL,
  `referenciaReciboPago` VARCHAR(255) NULL,
  `estadoReciboPago` VARCHAR(20) NOT NULL,
  `observacionesReciboPago` TEXT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Fk_FormaPagoVenta_Codigo` INT NOT NULL,
  CONSTRAINT `Pk_ReciboPago` PRIMARY KEY (`Pk_ReciboPago`),
  CONSTRAINT `uqReciboPagonumero` UNIQUE (`numeroReciboPago`),
  CONSTRAINT `Fk_ReciboPago_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ReciboPago_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ReciboPago_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ReciboPago_FormaPagoVenta` FOREIGN KEY (`Fk_FormaPagoVenta_Codigo`)
    REFERENCES `tblFormaPagoVenta` (`Pk_FormaPagoVenta`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxReciboPagoClienteCodigofecha` (`Fk_Cliente_Codigo`, `fechaReciboPago`),
  KEY `idxReciboPagoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxReciboPagoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxReciboPagoFormaPagoVentaCodigo` (`Fk_FormaPagoVenta_Codigo`),
  CONSTRAINT `ckReciboPagoMontoPositivo` CHECK (montoReciboPago > 0),
  CONSTRAINT `ckReciboPagoValoresEstadoPago` CHECK (estadoReciboPago IN ('BORRADOR','CONFIRMADO','ANULADO'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblComisionEncabezado` (
  `Pk_ComisionEncabezado` INT NOT NULL AUTO_INCREMENT,
  `numeroComisionEncabezado` VARCHAR(30) NOT NULL,
  `Fk_Vendedor_Codigo` INT NOT NULL,
  `Fk_ReglaComision_Codigo` INT NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaDesdeComisionEncabezado` DATE NOT NULL,
  `fechaHastaComisionEncabezado` DATE NOT NULL,
  `estadoComisionEncabezado` VARCHAR(25) NOT NULL,
  `Fk_Usuario_Solicita` INT NULL,
  `fechaSolicitudComisionEncabezado` DATETIME NULL,
  `Fk_Usuario_Autoriza` INT NULL,
  `fechaAutorizacionComisionEncabezado` DATETIME NULL,
  `comentarioAutorizacionComisionEncabezado` TEXT NULL,
  `referenciaContableComisionEncabezado` VARCHAR(100) NULL,
  `fechaEnvioComisionEncabezado` DATETIME NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionEncabezado` PRIMARY KEY (`Pk_ComisionEncabezado`),
  CONSTRAINT `uqComisionEncabezadonumero` UNIQUE (`numeroComisionEncabezado`),
  CONSTRAINT `Fk_ComisionEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`Pk_Vendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionEncabezado_ReglaComision` FOREIGN KEY (`Fk_ReglaComision_Codigo`)
    REFERENCES `tblReglaComision` (`Pk_ReglaComision`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionEncabezado_Usuario_Solicita` FOREIGN KEY (`Fk_Usuario_Solicita`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionEncabezado_Usuario_Autoriza` FOREIGN KEY (`Fk_Usuario_Autoriza`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxComisionEncabezadoVendedorCodigofechaDesdefechaHasta` (`Fk_Vendedor_Codigo`, `fechaDesdeComisionEncabezado`, `fechaHastaComisionEncabezado`),
  KEY `idxComisionEncabezadoReglaComisionCodigo` (`Fk_ReglaComision_Codigo`),
  KEY `idxComisionEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxComisionEncabezadoUsuarioSolicita` (`Fk_Usuario_Solicita`),
  KEY `idxComisionEncabezadoUsuarioAutoriza` (`Fk_Usuario_Autoriza`),
  CONSTRAINT `ckComisionEncabezadoValoresEstadoComision` CHECK (estadoComisionEncabezado IN ('BORRADOR','SOLICITADA','APROBADA','RECHAZADA','ENVIADA')),
  CONSTRAINT `ckComisionEncabezadoFechasCoherentes` CHECK (fechaHastaComisionEncabezado >= fechaDesdeComisionEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblClienteListaPrecio` (
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_ListaPrecioEncabezado_Codigo` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ClienteListaPrecio` PRIMARY KEY (`Fk_Cliente_Codigo`),
  CONSTRAINT `Fk_ClienteListaPrecio_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ClienteListaPrecio_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`Pk_ListaPrecioEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxClienteListaPrecioListaPrecioEncabezadoCodigo` (`Fk_ListaPrecioEncabezado_Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCierreSaldoCliente` (
  `Fk_CierreMovimientoCxCEncabezado_Codigo` INT NOT NULL,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `codigoClienteCierreSaldoCliente` VARCHAR(30) NOT NULL,
  `nombreClienteCierreSaldoCliente` VARCHAR(100) NOT NULL,
  `saldoAnteriorCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `cargosPeriodoCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `abonosPeriodoCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `cargosAcumuladosCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `abonosAcumuladosCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `saldoActualCierreSaldoCliente` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CierreSaldoCliente` PRIMARY KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `Fk_Cliente_Codigo`),
  CONSTRAINT `Fk_CierreSaldoCliente_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`Pk_CierreMovimientoCxCEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CierreSaldoCliente_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxCierreSaldoClienteClienteCodigo` (`Fk_Cliente_Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblTrasladoCxCEncabezado` (
  `Fk_CierreMovimientoCxCEncabezado_Codigo` INT NOT NULL,
  `claveIntegracionTrasladoCxCEncabezado` VARCHAR(80) NOT NULL,
  `estadoTrasladoCxCEncabezado` VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',
  `referenciaContableTrasladoCxCEncabezado` VARCHAR(100) NULL,
  `Fk_Usuario_Codigo` INT NOT NULL,
  `fechaConfirmacionTrasladoCxCEncabezado` DATETIME NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_TrasladoCxCEncabezado` PRIMARY KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`),
  CONSTRAINT `uqTrasladoCxCEncabezadoclaveIntegracion` UNIQUE (`claveIntegracionTrasladoCxCEncabezado`),
  CONSTRAINT `Fk_TrasladoCxCEncabezado_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`Pk_CierreMovimientoCxCEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_TrasladoCxCEncabezado_Usuario` FOREIGN KEY (`Fk_Usuario_Codigo`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxTrasladoCxCEncabezadoUsuarioCodigo` (`Fk_Usuario_Codigo`),
  CONSTRAINT `ckTrasladoCxCEncabezadoValoresEstado` CHECK (estadoTrasladoCxCEncabezado IN ('PENDIENTE','PREPARADO','ENVIADO','CONFIRMADO','RECHAZADO'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblClienteFacturaVersion` (
  `Pk_ClienteFacturaVersion` INT NOT NULL AUTO_INCREMENT,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `numeroVersionClienteFacturaVersion` INT NOT NULL,
  `tipoIdentificacionClienteFacturaVersion` ENUM('NIT','CUI','EXTRANJERO','CF') NOT NULL,
  `nitClienteFacturaVersion` CHAR(10) NULL,
  `dpiClienteFacturaVersion` CHAR(13) NULL,
  `identificacionExtranjeraClienteFacturaVersion` VARCHAR(30) NULL,
  `nombreClienteFacturaVersion` VARCHAR(100) NOT NULL,
  `direccionClienteFacturaVersion` VARCHAR(500) NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ClienteFacturaVersion` PRIMARY KEY (`Pk_ClienteFacturaVersion`),
  CONSTRAINT `uqClienteFacturaVersionClienteCodigonumeroVersion` UNIQUE (`Fk_Cliente_Codigo`, `numeroVersionClienteFacturaVersion`),
  CONSTRAINT `Fk_ClienteFacturaVersion_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ckClienteFacturaVersionIdentificacionCoherente` CHECK ((tipoIdentificacionClienteFacturaVersion = 'CF' AND nitClienteFacturaVersion IS NULL AND dpiClienteFacturaVersion IS NULL AND identificacionExtranjeraClienteFacturaVersion IS NULL) OR (tipoIdentificacionClienteFacturaVersion = 'NIT' AND nitClienteFacturaVersion IS NOT NULL AND dpiClienteFacturaVersion IS NULL AND identificacionExtranjeraClienteFacturaVersion IS NULL) OR (tipoIdentificacionClienteFacturaVersion = 'CUI' AND dpiClienteFacturaVersion IS NOT NULL AND nitClienteFacturaVersion IS NULL AND identificacionExtranjeraClienteFacturaVersion IS NULL) OR (tipoIdentificacionClienteFacturaVersion = 'EXTRANJERO' AND identificacionExtranjeraClienteFacturaVersion IS NOT NULL AND nitClienteFacturaVersion IS NULL AND dpiClienteFacturaVersion IS NULL)),
  CONSTRAINT `ckClienteFacturaVersionVersionPositiva` CHECK (numeroVersionClienteFacturaVersion > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCotizacionDetalle` (
  `Pk_CotizacionDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_CotizacionEncabezado_Codigo` INT NOT NULL,
  `numeroLineaCotizacionDetalle` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `descripcionItemCotizacionDetalle` VARCHAR(255) NOT NULL,
  `cantidadCotizacionDetalle` DECIMAL(12,4) NOT NULL,
  `precioUnitarioCotizacionDetalle` DECIMAL(12,4) NOT NULL,
  `descuentoMontoCotizacionDetalle` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `tasaImpuestoCotizacionDetalle` DECIMAL(3,2) NOT NULL DEFAULT 0,
  `impuestoMontoCotizacionDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND((cantidadCotizacionDetalle * precioUnitarioCotizacionDetalle - descuentoMontoCotizacionDetalle) * tasaImpuestoCotizacionDetalle, 4)) STORED,
  `totalLineaCotizacionDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(cantidadCotizacionDetalle * precioUnitarioCotizacionDetalle - descuentoMontoCotizacionDetalle + impuestoMontoCotizacionDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CotizacionDetalle` PRIMARY KEY (`Pk_CotizacionDetalle`),
  CONSTRAINT `uqCotizacionDetalleCotizacionEncabezadoCodigonumeroLinea` UNIQUE (`Fk_CotizacionEncabezado_Codigo`, `numeroLineaCotizacionDetalle`),
  CONSTRAINT `Fk_CotizacionDetalle_CotizacionEncabezado` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
    REFERENCES `tblCotizacionEncabezado` (`Pk_CotizacionEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CotizacionDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxCotizacionDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckCotizacionDetalletasaImpuestoCotizacionDetalleRango` CHECK (tasaImpuestoCotizacionDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckCotizacionDetalleCantidadPrecioDescuentoValidos` CHECK (cantidadCotizacionDetalle > 0 AND precioUnitarioCotizacionDetalle >= 0 AND descuentoMontoCotizacionDetalle >= 0 AND descuentoMontoCotizacionDetalle <= cantidadCotizacionDetalle * precioUnitarioCotizacionDetalle),
  CONSTRAINT `ckCotizacionDetalleNumeroLineaPositivo` CHECK (numeroLineaCotizacionDetalle > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblPedidoEncabezado` (
  `Pk_PedidoEncabezado` INT NOT NULL AUTO_INCREMENT,
  `numeroPedidoEncabezado` VARCHAR(30) NOT NULL,
  `Fk_CotizacionEncabezado_Codigo` INT NULL,
  `referenciaIndependientePedidoEncabezado` VARCHAR(100) NULL,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_Vendedor_Codigo` INT NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaPedidoEncabezado` DATE NOT NULL,
  `fechaEntregaPedidoEncabezado` DATE NULL,
  `fechaVencimientoPedidoEncabezado` DATE NULL,
  `estadoPedidoEncabezado` VARCHAR(25) NOT NULL DEFAULT 'PENDIENTE',
  `observacionesPedidoEncabezado` TEXT NULL,
  `Fk_Usuario_Registro` INT NULL DEFAULT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_PedidoEncabezado` PRIMARY KEY (`Pk_PedidoEncabezado`),
  CONSTRAINT `uqPedidoEncabezadonumero` UNIQUE (`numeroPedidoEncabezado`),
  CONSTRAINT `uqPedidoEncabezadoCotizacionEncabezadoCodigo` UNIQUE (`Fk_CotizacionEncabezado_Codigo`),
  CONSTRAINT `Fk_PedidoEncabezado_CotizacionEncabezado` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
    REFERENCES `tblCotizacionEncabezado` (`Pk_CotizacionEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoEncabezado_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`Pk_Cliente`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`Pk_Vendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxPedidoEncabezadoClienteCodigo` (`Fk_Cliente_Codigo`),
  KEY `idxPedidoEncabezadoVendedorCodigo` (`Fk_Vendedor_Codigo`),
  KEY `idxPedidoEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxPedidoEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckPedidoEncabezadoValoresEstadoPedido` CHECK (estadoPedidoEncabezado IN ('PENDIENTE','CONFIRMADO','PARCIAL','COMPLETADO','ANULADO')),
  CONSTRAINT `ckPedidoEncabezadoFechasCoherentes` CHECK (fechaVencimientoPedidoEncabezado >= fechaPedidoEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblDepositoVentaDetalle` (
  `Fk_DepositoVentaEncabezado_Codigo` INT NOT NULL,
  `Fk_ReciboPago_Codigo` INT NOT NULL,
  `importeDepositoVentaDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_DepositoVentaDetalle` PRIMARY KEY (`Fk_DepositoVentaEncabezado_Codigo`, `Fk_ReciboPago_Codigo`),
  CONSTRAINT `Fk_DepositoVentaDetalle_DepositoVentaEncabezado` FOREIGN KEY (`Fk_DepositoVentaEncabezado_Codigo`)
    REFERENCES `tblDepositoVentaEncabezado` (`Pk_DepositoVentaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_DepositoVentaDetalle_ReciboPago` FOREIGN KEY (`Fk_ReciboPago_Codigo`)
    REFERENCES `tblReciboPago` (`Pk_ReciboPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxDepositoVentaDetalleReciboPagoCodigo` (`Fk_ReciboPago_Codigo`),
  CONSTRAINT `ckDepositoVentaDetalleImportePositivo` CHECK (importeDepositoVentaDetalle > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblTrasladoCxCDetalle` (
  `Fk_TrasladoCxCEncabezado_Codigo` INT NOT NULL,
  `numeroLineaTrasladoCxCDetalle` INT NOT NULL,
  `cuentaTrasladoCxCDetalle` VARCHAR(30) NOT NULL,
  `descripcionTrasladoCxCDetalle` VARCHAR(100) NOT NULL,
  `debeTrasladoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `haberTrasladoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_TrasladoCxCDetalle` PRIMARY KEY (`Fk_TrasladoCxCEncabezado_Codigo`, `numeroLineaTrasladoCxCDetalle`),
  CONSTRAINT `Fk_TrasladoCxCDetalle_TrasladoCxCEncabezado` FOREIGN KEY (`Fk_TrasladoCxCEncabezado_Codigo`)
    REFERENCES `tblTrasladoCxCEncabezado` (`Fk_CierreMovimientoCxCEncabezado_Codigo`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ckTrasladoCxCDetalleDebeHaberNoNegativos` CHECK (debeTrasladoCxCDetalle >= 0 AND haberTrasladoCxCDetalle >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblPedidoDetalle` (
  `Pk_PedidoDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_PedidoEncabezado_Codigo` INT NOT NULL,
  `Fk_CotizacionDetalle_Codigo` INT NULL,
  `numeroLineaPedidoDetalle` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `descripcionItemPedidoDetalle` VARCHAR(255) NOT NULL,
  `cantidadPedidoDetalle` DECIMAL(12,4) NOT NULL,
  `precioUnitarioPedidoDetalle` DECIMAL(12,4) NOT NULL,
  `descuentoMontoPedidoDetalle` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `tasaImpuestoPedidoDetalle` DECIMAL(3,2) NOT NULL DEFAULT 0,
  `impuestoMontoPedidoDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND((cantidadPedidoDetalle * precioUnitarioPedidoDetalle - descuentoMontoPedidoDetalle) * tasaImpuestoPedidoDetalle, 4)) STORED,
  `totalLineaPedidoDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(cantidadPedidoDetalle * precioUnitarioPedidoDetalle - descuentoMontoPedidoDetalle + impuestoMontoPedidoDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_PedidoDetalle` PRIMARY KEY (`Pk_PedidoDetalle`),
  CONSTRAINT `uqPedidoDetallePedidoEncabezadoCodigonumeroLinea` UNIQUE (`Fk_PedidoEncabezado_Codigo`, `numeroLineaPedidoDetalle`),
  CONSTRAINT `Fk_PedidoDetalle_PedidoEncabezado` FOREIGN KEY (`Fk_PedidoEncabezado_Codigo`)
    REFERENCES `tblPedidoEncabezado` (`Pk_PedidoEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoDetalle_CotizacionDetalle` FOREIGN KEY (`Fk_CotizacionDetalle_Codigo`)
    REFERENCES `tblCotizacionDetalle` (`Pk_CotizacionDetalle`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_PedidoDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxPedidoDetalleCotizacionDetalleCodigo` (`Fk_CotizacionDetalle_Codigo`),
  KEY `idxPedidoDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckPedidoDetalletasaImpuestoPedidoDetalleRango` CHECK (tasaImpuestoPedidoDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckPedidoDetalleCantidadPrecioDescuentoValidos` CHECK (cantidadPedidoDetalle > 0 AND precioUnitarioPedidoDetalle >= 0 AND descuentoMontoPedidoDetalle >= 0 AND descuentoMontoPedidoDetalle <= cantidadPedidoDetalle * precioUnitarioPedidoDetalle),
  CONSTRAINT `ckPedidoDetalleNumeroLineaPositivo` CHECK (numeroLineaPedidoDetalle > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblFacturaEncabezado` (
  `Pk_FacturaEncabezado` INT NOT NULL AUTO_INCREMENT,
  `serieFacturaEncabezado` VARCHAR(30) NOT NULL,
  `numeroFacturaEncabezado` VARCHAR(30) NOT NULL,
  `Fk_Sucursal_Codigo` INT NOT NULL,
  `Fk_PedidoEncabezado_Codigo` INT NULL,
  `Fk_Vendedor_Codigo` INT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaEmisionFacturaEncabezado` DATE NOT NULL,
  `fechaVencimientoFacturaEncabezado` DATE NOT NULL,
  `tipoDteFacturaEncabezado` VARCHAR(30) NOT NULL,
  `condicionPagoFacturaEncabezado` VARCHAR(15) NOT NULL,
  `estadoFacturaEncabezado` VARCHAR(25) NOT NULL,
  `uuidDteFacturaEncabezado` CHAR(36) NULL,
  `fechaCertificacionFacturaEncabezado` DATETIME NULL,
  `observacionesFacturaEncabezado` TEXT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `Fk_ClienteFacturaVersion_Codigo` INT NOT NULL,
  `Fk_CotizacionEncabezado_Codigo` INT NULL,
  CONSTRAINT `Pk_FacturaEncabezado` PRIMARY KEY (`Pk_FacturaEncabezado`),
  CONSTRAINT `uqFacturaEncabezadouuidDte` UNIQUE (`uuidDteFacturaEncabezado`),
  CONSTRAINT `uqFacturaEncabezadoSucursalCodigoserienumero` UNIQUE (`Fk_Sucursal_Codigo`, `serieFacturaEncabezado`, `numeroFacturaEncabezado`),
  CONSTRAINT `uqFacturaEncabezadoCotizacionEncabezadoCodigo` UNIQUE (`Fk_CotizacionEncabezado_Codigo`),
  CONSTRAINT `Fk_FacturaEncabezado_Sucursal` FOREIGN KEY (`Fk_Sucursal_Codigo`)
    REFERENCES `tblSucursal` (`Pk_Sucursal`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_PedidoEncabezado` FOREIGN KEY (`Fk_PedidoEncabezado_Codigo`)
    REFERENCES `tblPedidoEncabezado` (`Pk_PedidoEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`Pk_Vendedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_ClienteFacturaVersion` FOREIGN KEY (`Fk_ClienteFacturaVersion_Codigo`)
    REFERENCES `tblClienteFacturaVersion` (`Pk_ClienteFacturaVersion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaEncabezado_CotizacionEncabezado` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
    REFERENCES `tblCotizacionEncabezado` (`Pk_CotizacionEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxFacturaEncabezadoPedidoEncabezadoCodigo` (`Fk_PedidoEncabezado_Codigo`),
  KEY `idxFacturaEncabezadoVendedorCodigo` (`Fk_Vendedor_Codigo`),
  KEY `idxFacturaEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxFacturaEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxFacturaEncabezadoClienteFacturaVersionCodigo` (`Fk_ClienteFacturaVersion_Codigo`),
  CONSTRAINT `ckFacturaEncabezadoCondicionPagoValida` CHECK (condicionPagoFacturaEncabezado IN ('CONTADO','CREDITO')),
  CONSTRAINT `ckFacturaEncabezadoValoresEstadoFactura` CHECK (estadoFacturaEncabezado IN ('BORRADOR','EMITIDA','ANULADA')),
  CONSTRAINT `ckFacturaEncabezadoFechasCoherentes` CHECK (fechaVencimientoFacturaEncabezado >= fechaEmisionFacturaEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblFacturaDetalle` (
  `Pk_FacturaDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_PedidoDetalle_Codigo` INT NULL,
  `numeroLineaFacturaDetalle` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `descripcionItemFacturaDetalle` VARCHAR(255) NOT NULL,
  `cantidadFacturaDetalle` DECIMAL(12,4) NOT NULL,
  `precioUnitarioFacturaDetalle` DECIMAL(12,4) NOT NULL,
  `descuentoMontoFacturaDetalle` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `tasaImpuestoFacturaDetalle` DECIMAL(3,2) NOT NULL DEFAULT 0,
  `impuestoMontoFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND((cantidadFacturaDetalle * precioUnitarioFacturaDetalle - descuentoMontoFacturaDetalle) * tasaImpuestoFacturaDetalle, 4)) STORED,
  `totalLineaFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(cantidadFacturaDetalle * precioUnitarioFacturaDetalle - descuentoMontoFacturaDetalle + impuestoMontoFacturaDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_FacturaDetalle` PRIMARY KEY (`Pk_FacturaDetalle`),
  CONSTRAINT `uqFacturaDetalleFacturaEncabezadoCodigonumeroLinea` UNIQUE (`Fk_FacturaEncabezado_Codigo`, `numeroLineaFacturaDetalle`),
  CONSTRAINT `Fk_FacturaDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaDetalle_PedidoDetalle` FOREIGN KEY (`Fk_PedidoDetalle_Codigo`)
    REFERENCES `tblPedidoDetalle` (`Pk_PedidoDetalle`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_FacturaDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxFacturaDetallePedidoDetalleCodigo` (`Fk_PedidoDetalle_Codigo`),
  KEY `idxFacturaDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckFacturaDetalletasaImpuestoFacturaDetalleRango` CHECK (tasaImpuestoFacturaDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckFacturaDetalleCantidadPrecioDescuentoValidos` CHECK (cantidadFacturaDetalle > 0 AND precioUnitarioFacturaDetalle >= 0 AND descuentoMontoFacturaDetalle >= 0 AND descuentoMontoFacturaDetalle <= cantidadFacturaDetalle * precioUnitarioFacturaDetalle),
  CONSTRAINT `ckFacturaDetalleNumeroLineaPositivo` CHECK (numeroLineaFacturaDetalle > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblNotaCxC` (
  `Pk_NotaCxC` INT NOT NULL AUTO_INCREMENT,
  `numeroNotaCxC` VARCHAR(30) NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `tipoNotaCxC` VARCHAR(10) NOT NULL,
  `fechaNotaCxC` DATE NOT NULL,
  `montoNotaCxC` DECIMAL(12,4) NOT NULL,
  `motivoNotaCxC` TEXT NOT NULL,
  `estadoNotaCxC` VARCHAR(20) NOT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_NotaCxC` PRIMARY KEY (`Pk_NotaCxC`),
  CONSTRAINT `uqNotaCxCnumero` UNIQUE (`numeroNotaCxC`),
  CONSTRAINT `Fk_NotaCxC_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_NotaCxC_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxNotaCxCFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`),
  KEY `idxNotaCxCUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckNotaCxCMontoPositivo` CHECK (montoNotaCxC > 0),
  CONSTRAINT `ckNotaCxCValoresTipoNota` CHECK (tipoNotaCxC IN ('CREDITO','DEBITO')),
  CONSTRAINT `ckNotaCxCValoresEstadoNota` CHECK (estadoNotaCxC IN ('BORRADOR','APLICADA','ANULADA'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblAplicacionPago` (
  `Pk_AplicacionPago` INT NOT NULL AUTO_INCREMENT,
  `Fk_ReciboPago_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `fechaAplicacionPago` DATE NOT NULL,
  `montoAplicadoAplicacionPago` DECIMAL(12,4) NOT NULL,
  `estadoAplicacionPago` VARCHAR(20) NOT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_AplicacionPago` PRIMARY KEY (`Pk_AplicacionPago`),
  CONSTRAINT `Fk_AplicacionPago_ReciboPago` FOREIGN KEY (`Fk_ReciboPago_Codigo`)
    REFERENCES `tblReciboPago` (`Pk_ReciboPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_AplicacionPago_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_AplicacionPago_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxAplicacionPagoFacturaEncabezadoCodigofecha` (`Fk_FacturaEncabezado_Codigo`, `fechaAplicacionPago`),
  KEY `idxAplicacionPagoReciboPagoCodigo` (`Fk_ReciboPago_Codigo`),
  KEY `idxAplicacionPagoUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckAplicacionPagoMontoPositivo` CHECK (montoAplicadoAplicacionPago > 0),
  CONSTRAINT `ckAplicacionPagoValoresEstadoAplicacion` CHECK (estadoAplicacionPago IN ('APLICADA','REVERTIDA'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblCierreMovimientoCxCDetalle` (
  `Pk_CierreMovimientoCxCDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_CierreMovimientoCxCEncabezado_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `saldoInicialCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `cargosPeriodoCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `abonosPeriodoCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `saldoFinalCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CierreMovimientoCxCDetalle` PRIMARY KEY (`Pk_CierreMovimientoCxCDetalle`),
  CONSTRAINT `uqCierreMovimientoCxCDetalleCierreMovimientoCxCEncabeza4f1c6502` UNIQUE (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `Fk_FacturaEncabezado_Codigo`),
  CONSTRAINT `Fk_CierreMovimientoCxCDetalle_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`Pk_CierreMovimientoCxCEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_CierreMovimientoCxCDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxCierreMovimientoCxCDetalleFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblComisionFacturaEncabezado` (
  `Pk_ComisionFacturaEncabezado` INT NOT NULL AUTO_INCREMENT,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `metodoComisionFacturaEncabezado` ENUM('LINEA','PRODUCTO','CATEGORIA','MARCA','MIXTA') NOT NULL,
  `estadoComisionFacturaEncabezado` ENUM('CALCULADA','ANULADA') NOT NULL DEFAULT 'CALCULADA',
  `observacionesComisionFacturaEncabezado` TEXT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionFacturaEncabezado` PRIMARY KEY (`Pk_ComisionFacturaEncabezado`),
  CONSTRAINT `uqComisionFacturaEncabezadoFacturaEncabezadoCodigo` UNIQUE (`Fk_FacturaEncabezado_Codigo`),
  CONSTRAINT `Fk_ComisionFacturaEncabezado_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblMovimientoCxC` (
  `Pk_MovimientoCxC` INT NOT NULL AUTO_INCREMENT,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_AplicacionPago_Codigo` INT NULL,
  `Fk_NotaCxC_Codigo` INT NULL,
  `Fk_MovimientoCxC_Revertido` INT NULL,
  `fechaMovimientoCxC` DATE NOT NULL,
  `tipoMovimientoCxC` VARCHAR(30) NOT NULL,
  `descripcionMovimientoCxC` VARCHAR(255) NOT NULL,
  `cargoMovimientoCxC` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `abonoMovimientoCxC` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_MovimientoCxC` PRIMARY KEY (`Pk_MovimientoCxC`),
  CONSTRAINT `uqMovimientoCxCAplicacionPagoCodigo` UNIQUE (`Fk_AplicacionPago_Codigo`),
  CONSTRAINT `uqMovimientoCxCNotaCxCCodigo` UNIQUE (`Fk_NotaCxC_Codigo`),
  CONSTRAINT `uqMovimientoCxCRevertido` UNIQUE (`Fk_MovimientoCxC_Revertido`),
  CONSTRAINT `Fk_MovimientoCxC_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_MovimientoCxC_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`Pk_AplicacionPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_MovimientoCxC_NotaCxC` FOREIGN KEY (`Fk_NotaCxC_Codigo`)
    REFERENCES `tblNotaCxC` (`Pk_NotaCxC`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_MovimientoCxC_MovimientoCxC_Revertido` FOREIGN KEY (`Fk_MovimientoCxC_Revertido`)
    REFERENCES `tblMovimientoCxC` (`Pk_MovimientoCxC`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_MovimientoCxC_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxMovimientoCxCFacturaEncabezadoCodigofecha` (`Fk_FacturaEncabezado_Codigo`, `fechaMovimientoCxC`),
  KEY `idxMovimientoCxCfecha` (`fechaMovimientoCxC`),
  KEY `idxMovimientoCxCUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckMovimientoCxCCargoAbonoExcluyentes` CHECK (cargoMovimientoCxC >= 0 AND abonoMovimientoCxC >= 0 AND ((cargoMovimientoCxC > 0 AND abonoMovimientoCxC = 0) OR (abonoMovimientoCxC > 0 AND cargoMovimientoCxC = 0))),
  CONSTRAINT `ckMovimientoCxCReversionIdentificada` CHECK ((tipoMovimientoCxC = 'REVERSO' AND Fk_MovimientoCxC_Revertido IS NOT NULL) OR (tipoMovimientoCxC <> 'REVERSO' AND Fk_MovimientoCxC_Revertido IS NULL)),
  CONSTRAINT `ckMovimientoCxCValoresTipoMovimiento` CHECK (tipoMovimientoCxC IN ('FACTURA','PAGO','NOTA_CREDITO','NOTA_DEBITO','AJUSTE_POSITIVO','AJUSTE_NEGATIVO','REVERSO'))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblComisionDetalle` (
  `Pk_ComisionDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_ComisionEncabezado_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_AplicacionPago_Codigo` INT NULL,
  `baseComisionDetalle` DECIMAL(12,4) NOT NULL,
  `tasaAplicadaComisionDetalle` DECIMAL(3,2) NOT NULL,
  `montoComisionDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(baseComisionDetalle * tasaAplicadaComisionDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionDetalle` PRIMARY KEY (`Pk_ComisionDetalle`),
  CONSTRAINT `Fk_ComisionDetalle_ComisionEncabezado` FOREIGN KEY (`Fk_ComisionEncabezado_Codigo`)
    REFERENCES `tblComisionEncabezado` (`Pk_ComisionEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`Pk_FacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionDetalle_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`Pk_AplicacionPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxComisionDetalleComisionEncabezadoCodigo` (`Fk_ComisionEncabezado_Codigo`),
  KEY `idxComisionDetalleFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`),
  KEY `idxComisionDetalleAplicacionPagoCodigo` (`Fk_AplicacionPago_Codigo`),
  CONSTRAINT `ckComisionDetalletasaAplicadaComisionDetalleRango` CHECK (tasaAplicadaComisionDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckComisionDetalleBaseNoNegativa` CHECK (baseComisionDetalle >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblComisionAbono` (
  `Pk_ComisionAbono` INT NOT NULL AUTO_INCREMENT,
  `Fk_ComisionFacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_AplicacionPago_Codigo` INT NOT NULL,
  `tasaFacturaPagadaAcumuladaComisionAbono` DECIMAL(5,4) NOT NULL,
  `montoComisionDevengadoComisionAbono` DECIMAL(12,4) NOT NULL,
  `estadoPagoVendedorComisionAbono` ENUM('PENDIENTE','PAGADO','REVERSADO') NOT NULL DEFAULT 'PENDIENTE',
  `fechaPagoVendedorComisionAbono` DATE NULL,
  `referenciaNominaComisionAbono` VARCHAR(100) NULL,
  `observacionesComisionAbono` TEXT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionAbono` PRIMARY KEY (`Pk_ComisionAbono`),
  CONSTRAINT `uqComisionAbonoAplicacionPagoCodigo` UNIQUE (`Fk_AplicacionPago_Codigo`),
  CONSTRAINT `Fk_ComisionAbono_ComisionFacturaEncabezado` FOREIGN KEY (`Fk_ComisionFacturaEncabezado_Codigo`)
    REFERENCES `tblComisionFacturaEncabezado` (`Pk_ComisionFacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionAbono_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`Pk_AplicacionPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxComisionAbonoComisionFacturaEncabezadoCodigoestadoPa19c1f12f` (`Fk_ComisionFacturaEncabezado_Codigo`, `estadoPagoVendedorComisionAbono`),
  CONSTRAINT `ckComisionAbonotasaFacturaPagadaAcumuladaComisionAbonoRango` CHECK (tasaFacturaPagadaAcumuladaComisionAbono BETWEEN 0 AND 1),
  CONSTRAINT `ckComisionAbonoDevengoNoNegativo` CHECK (montoComisionDevengadoComisionAbono >= 0),
  CONSTRAINT `ckComisionAbonoFechaPagoVendedor` CHECK (estadoPagoVendedorComisionAbono <> 'PAGADO' OR fechaPagoVendedorComisionAbono IS NOT NULL)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblComisionFacturaDetalle` (
  `Pk_ComisionFacturaDetalle` INT NOT NULL AUTO_INCREMENT,
  `Fk_ComisionFacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_FacturaDetalle_Codigo` INT NOT NULL,
  `metodoComisionFacturaDetalle` ENUM('LINEA','PRODUCTO','CATEGORIA','MARCA') NOT NULL,
  `nombreClasificacionHistoricaComisionFacturaDetalle` VARCHAR(100) NOT NULL,
  `tasaAplicadaComisionFacturaDetalle` DECIMAL(3,2) NOT NULL,
  `baseComisionFacturaDetalle` DECIMAL(12,4) NOT NULL,
  `montoComisionLineaComisionFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(baseComisionFacturaDetalle * tasaAplicadaComisionFacturaDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `Fk_Linea_Codigo` INT NULL,
  `Fk_Categoria_Codigo` INT NULL,
  `Fk_Marca_Codigo` INT NULL,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionFacturaDetalle` PRIMARY KEY (`Pk_ComisionFacturaDetalle`),
  CONSTRAINT `uqComisionFacturaDetalleFacturaDetalleCodigo` UNIQUE (`Fk_FacturaDetalle_Codigo`),
  CONSTRAINT `Fk_ComisionFacturaDetalle_ComisionFacturaEncabezado` FOREIGN KEY (`Fk_ComisionFacturaEncabezado_Codigo`)
    REFERENCES `tblComisionFacturaEncabezado` (`Pk_ComisionFacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionFacturaDetalle_FacturaDetalle` FOREIGN KEY (`Fk_FacturaDetalle_Codigo`)
    REFERENCES `tblFacturaDetalle` (`Pk_FacturaDetalle`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionFacturaDetalle_Linea` FOREIGN KEY (`Fk_Linea_Codigo`)
    REFERENCES `tblLinea` (`Pk_Linea`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionFacturaDetalle_Categoria` FOREIGN KEY (`Fk_Categoria_Codigo`)
    REFERENCES `tblCategoria` (`Pk_Categoria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `Fk_ComisionFacturaDetalle_Marca` FOREIGN KEY (`Fk_Marca_Codigo`)
    REFERENCES `tblMarca` (`Pk_Marca`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  KEY `idxComisionFacturaDetalleComisionFacturaEncabezadoCodigo` (`Fk_ComisionFacturaEncabezado_Codigo`),
  KEY `idxComisionFacturaDetalleLineaCodigo` (`Fk_Linea_Codigo`),
  KEY `idxComisionFacturaDetalleCategoriaCodigo` (`Fk_Categoria_Codigo`),
  KEY `idxComisionFacturaDetalleMarcaCodigo` (`Fk_Marca_Codigo`),
  CONSTRAINT `ckComisionFacturaDetalletasaAplicadaComisionFacturaDetalleRango` CHECK (tasaAplicadaComisionFacturaDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckComisionFacturaDetalleBaseNoNegativa` CHECK (baseComisionFacturaDetalle >= 0),
  CONSTRAINT `ckComisionFacturaDetalleClasificacionTipada` CHECK ((metodoComisionFacturaDetalle = 'PRODUCTO' AND Fk_Linea_Codigo IS NULL AND Fk_Categoria_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'LINEA' AND Fk_Linea_Codigo IS NOT NULL AND Fk_Categoria_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'CATEGORIA' AND Fk_Categoria_Codigo IS NOT NULL AND Fk_Linea_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'MARCA' AND Fk_Marca_Codigo IS NOT NULL AND Fk_Linea_Codigo IS NULL AND Fk_Categoria_Codigo IS NULL))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

DELIMITER $$

CREATE TRIGGER `trClienteBeforeInsert` BEFORE INSERT ON `tblCliente` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Credito` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trClienteBeforeUpdate` BEFORE UPDATE ON `tblCliente` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Credito` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trListaPrecioEncabezadoBeforeInsert` BEFORE INSERT ON `tblListaPrecioEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trListaPrecioEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblListaPrecioEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCotizacionEncabezadoBeforeInsert` BEFORE INSERT ON `tblCotizacionEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCotizacionEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblCotizacionEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trPedidoEncabezadoBeforeInsert` BEFORE INSERT ON `tblPedidoEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trPedidoEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblPedidoEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trFacturaEncabezadoBeforeInsert` BEFORE INSERT ON `tblFacturaEncabezado` FOR EACH ROW
BEGIN
  DECLARE fechaUltimoCierre DATE DEFAULT NULL;
  DECLARE fechaCierreAnterior DATE DEFAULT NULL;
  INSERT INTO `tblControlPeriodoCxC` (`Fk_Moneda_Codigo`)
    VALUES (NEW.`Fk_Moneda_Codigo`) ON DUPLICATE KEY UPDATE `Fk_Moneda_Codigo`=NEW.`Fk_Moneda_Codigo`;
  SELECT `fechaUltimoCierreControlPeriodoCxC` INTO fechaUltimoCierre
    FROM `tblControlPeriodoCxC` WHERE `Fk_Moneda_Codigo`=NEW.`Fk_Moneda_Codigo` FOR UPDATE;
  IF fechaUltimoCierre IS NOT NULL AND NEW.`fechaEmisionFacturaEncabezado`<=fechaUltimoCierre THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La fecha pertenece a un periodo de CxC cerrado';
  END IF;
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trFacturaEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblFacturaEncabezado` FOR EACH ROW
BEGIN
  DECLARE fechaUltimoCierre DATE DEFAULT NULL;
  DECLARE fechaCierreAnterior DATE DEFAULT NULL;
  SELECT `fechaUltimoCierreControlPeriodoCxC` INTO fechaCierreAnterior FROM `tblControlPeriodoCxC`
    WHERE `Fk_Moneda_Codigo`=OLD.`Fk_Moneda_Codigo` FOR UPDATE;
  IF fechaCierreAnterior IS NOT NULL AND OLD.`fechaEmisionFacturaEncabezado`<=fechaCierreAnterior THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se modifica una factura de un periodo cerrado';
  END IF;
  INSERT INTO `tblControlPeriodoCxC` (`Fk_Moneda_Codigo`)
    VALUES (NEW.`Fk_Moneda_Codigo`) ON DUPLICATE KEY UPDATE `Fk_Moneda_Codigo`=NEW.`Fk_Moneda_Codigo`;
  SELECT `fechaUltimoCierreControlPeriodoCxC` INTO fechaUltimoCierre
    FROM `tblControlPeriodoCxC` WHERE `Fk_Moneda_Codigo`=NEW.`Fk_Moneda_Codigo` FOR UPDATE;
  IF fechaUltimoCierre IS NOT NULL AND NEW.`fechaEmisionFacturaEncabezado`<=fechaUltimoCierre THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La fecha pertenece a un periodo de CxC cerrado';
  END IF;
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trReciboPagoBeforeInsert` BEFORE INSERT ON `tblReciboPago` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trReciboPagoBeforeUpdate` BEFORE UPDATE ON `tblReciboPago` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trAplicacionPagoBeforeInsert` BEFORE INSERT ON `tblAplicacionPago` FOR EACH ROW
BEGIN
  DECLARE monedaRecibo BIGINT;
  DECLARE clienteRecibo INT;
  SELECT `Fk_Moneda_Codigo`,`Fk_Cliente_Codigo` INTO monedaRecibo,clienteRecibo
    FROM `tblReciboPago` WHERE `Pk_ReciboPago`=NEW.`Fk_ReciboPago_Codigo`;
  IF NOT EXISTS(SELECT 1 FROM `tblFacturaEncabezado` f JOIN `tblClienteFacturaVersion` v
      ON v.`Pk_ClienteFacturaVersion`=f.`Fk_ClienteFacturaVersion_Codigo`
    WHERE f.`Pk_FacturaEncabezado`=NEW.`Fk_FacturaEncabezado_Codigo` AND f.`Fk_Moneda_Codigo`=monedaRecibo
      AND v.`Fk_Cliente_Codigo`=clienteRecibo) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Recibo y factura deben corresponder al mismo cliente y moneda';
  END IF;
END$$

CREATE TRIGGER `trAplicacionPagoBeforeUpdate` BEFORE UPDATE ON `tblAplicacionPago` FOR EACH ROW
BEGIN
  DECLARE monedaRecibo BIGINT;
  DECLARE clienteRecibo INT;
  SELECT `Fk_Moneda_Codigo`,`Fk_Cliente_Codigo` INTO monedaRecibo,clienteRecibo
    FROM `tblReciboPago` WHERE `Pk_ReciboPago`=NEW.`Fk_ReciboPago_Codigo`;
  IF NOT EXISTS(SELECT 1 FROM `tblFacturaEncabezado` f JOIN `tblClienteFacturaVersion` v
      ON v.`Pk_ClienteFacturaVersion`=f.`Fk_ClienteFacturaVersion_Codigo`
    WHERE f.`Pk_FacturaEncabezado`=NEW.`Fk_FacturaEncabezado_Codigo` AND f.`Fk_Moneda_Codigo`=monedaRecibo
      AND v.`Fk_Cliente_Codigo`=clienteRecibo) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Recibo y factura deben corresponder al mismo cliente y moneda';
  END IF;
END$$

CREATE TRIGGER `trMovimientoCxCBeforeInsert` BEFORE INSERT ON `tblMovimientoCxC` FOR EACH ROW
BEGIN
  DECLARE monedaFactura BIGINT;
  DECLARE fechaUltimoCierre DATE DEFAULT NULL;
  DECLARE cantidadOriginal INT DEFAULT 0;
  SELECT `Fk_Moneda_Codigo` INTO monedaFactura FROM `tblFacturaEncabezado`
    WHERE `Pk_FacturaEncabezado`=NEW.`Fk_FacturaEncabezado_Codigo`;
  INSERT INTO `tblControlPeriodoCxC` (`Fk_Moneda_Codigo`) VALUES (monedaFactura)
    ON DUPLICATE KEY UPDATE `Fk_Moneda_Codigo`=monedaFactura;
  SELECT `fechaUltimoCierreControlPeriodoCxC` INTO fechaUltimoCierre FROM `tblControlPeriodoCxC`
    WHERE `Fk_Moneda_Codigo`=monedaFactura FOR UPDATE;
  IF fechaUltimoCierre IS NOT NULL AND NEW.`fechaMovimientoCxC`<=fechaUltimoCierre THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se registran movimientos en un periodo cerrado';
  END IF;
  IF NEW.`Fk_MovimientoCxC_Revertido` IS NOT NULL THEN
    SELECT COUNT(*) INTO cantidadOriginal FROM `tblMovimientoCxC`
    WHERE `Pk_MovimientoCxC`=NEW.`Fk_MovimientoCxC_Revertido`
      AND `Fk_FacturaEncabezado_Codigo`=NEW.`Fk_FacturaEncabezado_Codigo`
      AND `tipoMovimientoCxC`<>'REVERSO'
      AND `cargoMovimientoCxC`=NEW.`abonoMovimientoCxC` AND `abonoMovimientoCxC`=NEW.`cargoMovimientoCxC`;
    IF cantidadOriginal<>1 THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La reversion debe invertir el cargo o abono original de la misma factura'; END IF;
  END IF;
END$$

CREATE TRIGGER `trMovimientoCxCBeforeUpdate` BEFORE UPDATE ON `tblMovimientoCxC` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El libro CxC es inmutable; registre una reversion en periodo abierto';
END$$

CREATE TRIGGER `trCierreMovimientoCxCEncabezadoBeforeInsert` BEFORE INSERT ON `tblCierreMovimientoCxCEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCierreMovimientoCxCEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblCierreMovimientoCxCEncabezado` FOR EACH ROW
BEGIN
  IF OLD.`estadoCierreMovimientoCxCEncabezado`='CONFIRMADO' THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El cierre confirmado no puede modificarse'; END IF;
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCierreMovimientoCxCDetalleBeforeInsert` BEFORE INSERT ON `tblCierreMovimientoCxCDetalle` FOR EACH ROW
BEGIN
  IF (SELECT `estadoCierreMovimientoCxCEncabezado` FROM `tblCierreMovimientoCxCEncabezado`
      WHERE `Pk_CierreMovimientoCxCEncabezado`=NEW.`Fk_CierreMovimientoCxCEncabezado_Codigo`)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan detalles a un cierre confirmado';
  END IF;
END$$

CREATE TRIGGER `trCierreMovimientoCxCDetalleBeforeUpdate` BEFORE UPDATE ON `tblCierreMovimientoCxCDetalle` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle historico del cierre es inmutable';
END$$

CREATE TRIGGER `trComisionEncabezadoBeforeInsert` BEFORE INSERT ON `tblComisionEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trComisionEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblComisionEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trControlPeriodoCxCBeforeInsert` BEFORE INSERT ON `tblControlPeriodoCxC` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trControlPeriodoCxCBeforeUpdate` BEFORE UPDATE ON `tblControlPeriodoCxC` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCierreSaldoClienteBeforeInsert` BEFORE INSERT ON `tblCierreSaldoCliente` FOR EACH ROW
BEGIN
  IF (SELECT `estadoCierreMovimientoCxCEncabezado` FROM `tblCierreMovimientoCxCEncabezado`
      WHERE `Pk_CierreMovimientoCxCEncabezado`=NEW.`Fk_CierreMovimientoCxCEncabezado_Codigo`)='CONFIRMADO' THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se agregan detalles a un cierre confirmado';
  END IF;
END$$

CREATE TRIGGER `trCierreSaldoClienteBeforeUpdate` BEFORE UPDATE ON `tblCierreSaldoCliente` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle historico del cierre es inmutable';
END$$

CREATE TRIGGER `trDepositoVentaEncabezadoBeforeInsert` BEFORE INSERT ON `tblDepositoVentaEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
  IF NOT EXISTS(SELECT 1 FROM tblCuentaBancaria WHERE Pk_CuentaBancaria=NEW.`Fk_CuentaBancaria_Codigo`
      AND Fk_Moneda_Codigo=NEW.`Fk_Moneda_Codigo`) THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La cuenta bancaria debe usar la moneda del deposito'; END IF;
  IF NOT EXISTS(SELECT 1 FROM tblTipoTransaccion WHERE Pk_TipoTransaccion=NEW.`Fk_TipoTransaccion_Codigo`
      AND naturalezaTipoTransaccion IN ('INGRESO','AMBOS')) THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El deposito requiere un concepto bancario de ingreso'; END IF;
  IF NEW.`Fk_MovimientoBancario_Codigo` IS NOT NULL AND NOT EXISTS(SELECT 1 FROM tblMovimientoBancario
      WHERE Pk_MovimientoBancario=NEW.`Fk_MovimientoBancario_Codigo` AND Fk_CuentaBancaria_Codigo=NEW.`Fk_CuentaBancaria_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El movimiento bancario no pertenece a la cuenta del deposito';
  END IF;
END$$

CREATE TRIGGER `trDepositoVentaEncabezadoBeforeUpdate` BEFORE UPDATE ON `tblDepositoVentaEncabezado` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
  IF NOT EXISTS(SELECT 1 FROM tblCuentaBancaria WHERE Pk_CuentaBancaria=NEW.`Fk_CuentaBancaria_Codigo`
      AND Fk_Moneda_Codigo=NEW.`Fk_Moneda_Codigo`) THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La cuenta bancaria debe usar la moneda del deposito'; END IF;
  IF NOT EXISTS(SELECT 1 FROM tblTipoTransaccion WHERE Pk_TipoTransaccion=NEW.`Fk_TipoTransaccion_Codigo`
      AND naturalezaTipoTransaccion IN ('INGRESO','AMBOS')) THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El deposito requiere un concepto bancario de ingreso'; END IF;
  IF NEW.`Fk_MovimientoBancario_Codigo` IS NOT NULL AND NOT EXISTS(SELECT 1 FROM tblMovimientoBancario
      WHERE Pk_MovimientoBancario=NEW.`Fk_MovimientoBancario_Codigo` AND Fk_CuentaBancaria_Codigo=NEW.`Fk_CuentaBancaria_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El movimiento bancario no pertenece a la cuenta del deposito';
  END IF;
END$$

CREATE TRIGGER `trDepositoVentaDetalleBeforeInsert` BEFORE INSERT ON `tblDepositoVentaDetalle` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblDepositoVentaEncabezado` d JOIN `tblReciboPago` p
      ON p.`Fk_Moneda_Codigo`=d.`Fk_Moneda_Codigo`
    WHERE d.`Pk_DepositoVentaEncabezado`=NEW.`Fk_DepositoVentaEncabezado_Codigo` AND p.`Pk_ReciboPago`=NEW.`Fk_ReciboPago_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El recibo y el deposito deben usar la misma moneda';
  END IF;
END$$

CREATE TRIGGER `trDepositoVentaDetalleBeforeUpdate` BEFORE UPDATE ON `tblDepositoVentaDetalle` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblDepositoVentaEncabezado` d JOIN `tblReciboPago` p
      ON p.`Fk_Moneda_Codigo`=d.`Fk_Moneda_Codigo`
    WHERE d.`Pk_DepositoVentaEncabezado`=NEW.`Fk_DepositoVentaEncabezado_Codigo` AND p.`Pk_ReciboPago`=NEW.`Fk_ReciboPago_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El recibo y el deposito deben usar la misma moneda';
  END IF;
END$$

CREATE TRIGGER `trCuentaContableCxCBeforeInsert` BEFORE INSERT ON `tblCuentaContableCxC` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trCuentaContableCxCBeforeUpdate` BEFORE UPDATE ON `tblCuentaContableCxC` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM tblMoneda WHERE Pk_Moneda=NEW.`Fk_Moneda_Codigo` AND codigoMoneda='GTQ') THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Ventas opera en quetzales; seleccione GTQ del catalogo de Bancos';
  END IF;
END$$

CREATE TRIGGER `trComisionAbonoBeforeInsert` BEFORE INSERT ON `tblComisionAbono` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblComisionFacturaEncabezado` ce JOIN `tblAplicacionPago` a
      ON a.`Fk_FacturaEncabezado_Codigo`=ce.`Fk_FacturaEncabezado_Codigo`
    WHERE ce.`Pk_ComisionFacturaEncabezado`=NEW.`Fk_ComisionFacturaEncabezado_Codigo`
      AND a.`Pk_AplicacionPago`=NEW.`Fk_AplicacionPago_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El abono de comision debe corresponder a una aplicacion de la misma factura';
  END IF;
END$$

CREATE TRIGGER `trComisionAbonoBeforeUpdate` BEFORE UPDATE ON `tblComisionAbono` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblComisionFacturaEncabezado` ce JOIN `tblAplicacionPago` a
      ON a.`Fk_FacturaEncabezado_Codigo`=ce.`Fk_FacturaEncabezado_Codigo`
    WHERE ce.`Pk_ComisionFacturaEncabezado`=NEW.`Fk_ComisionFacturaEncabezado_Codigo`
      AND a.`Pk_AplicacionPago`=NEW.`Fk_AplicacionPago_Codigo`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El abono de comision debe corresponder a una aplicacion de la misma factura';
  END IF;
END$$

CREATE TRIGGER `trComisionFacturaDetalleBeforeInsert` BEFORE INSERT ON `tblComisionFacturaDetalle` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblComisionFacturaEncabezado` ce JOIN `tblFacturaDetalle` fd
      ON fd.`Fk_FacturaEncabezado_Codigo`=ce.`Fk_FacturaEncabezado_Codigo`
    JOIN tblProducto pr ON pr.Pk_Producto=fd.`Fk_Producto_Codigo`
    WHERE ce.`Pk_ComisionFacturaEncabezado`=NEW.`Fk_ComisionFacturaEncabezado_Codigo`
      AND fd.`Pk_FacturaDetalle`=NEW.`Fk_FacturaDetalle_Codigo`
      AND (NEW.`Fk_Linea_Codigo` IS NULL OR NEW.`Fk_Linea_Codigo`=pr.Fk_Linea_Codigo)
      AND (NEW.`Fk_Categoria_Codigo` IS NULL OR NEW.`Fk_Categoria_Codigo`=pr.Fk_Categoria_Codigo)
      AND (NEW.`Fk_Marca_Codigo` IS NULL OR NEW.`Fk_Marca_Codigo`=pr.Fk_Marca_Codigo)) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle y la clasificacion deben pertenecer a la factura y producto comisionados';
  END IF;
END$$

CREATE TRIGGER `trComisionFacturaDetalleBeforeUpdate` BEFORE UPDATE ON `tblComisionFacturaDetalle` FOR EACH ROW
BEGIN
  IF NOT EXISTS(SELECT 1 FROM `tblComisionFacturaEncabezado` ce JOIN `tblFacturaDetalle` fd
      ON fd.`Fk_FacturaEncabezado_Codigo`=ce.`Fk_FacturaEncabezado_Codigo`
    JOIN tblProducto pr ON pr.Pk_Producto=fd.`Fk_Producto_Codigo`
    WHERE ce.`Pk_ComisionFacturaEncabezado`=NEW.`Fk_ComisionFacturaEncabezado_Codigo`
      AND fd.`Pk_FacturaDetalle`=NEW.`Fk_FacturaDetalle_Codigo`
      AND (NEW.`Fk_Linea_Codigo` IS NULL OR NEW.`Fk_Linea_Codigo`=pr.Fk_Linea_Codigo)
      AND (NEW.`Fk_Categoria_Codigo` IS NULL OR NEW.`Fk_Categoria_Codigo`=pr.Fk_Categoria_Codigo)
      AND (NEW.`Fk_Marca_Codigo` IS NULL OR NEW.`Fk_Marca_Codigo`=pr.Fk_Marca_Codigo)) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El detalle y la clasificacion deben pertenecer a la factura y producto comisionados';
  END IF;
END$$

CREATE TRIGGER `trClienteFacturaVersionBeforeUpdate` BEFORE UPDATE ON `tblClienteFacturaVersion` FOR EACH ROW
BEGIN
  IF EXISTS(SELECT 1 FROM `tblFacturaEncabezado` WHERE `Fk_ClienteFacturaVersion_Codigo`=OLD.`Pk_ClienteFacturaVersion`) THEN
    SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='La identidad ya facturada es historica; registre una nueva version';
  END IF;
END$$

CREATE TRIGGER `trMovimientoCxCBeforeDelete` BEFORE DELETE ON `tblMovimientoCxC` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico contable no puede eliminarse';
END$$

CREATE TRIGGER `trCierreSaldoClienteBeforeDelete` BEFORE DELETE ON `tblCierreSaldoCliente` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico contable no puede eliminarse';
END$$

CREATE TRIGGER `trCierreMovimientoCxCDetalleBeforeDelete` BEFORE DELETE ON `tblCierreMovimientoCxCDetalle` FOR EACH ROW
BEGIN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El historico contable no puede eliminarse';
END$$

CREATE TRIGGER `trCierreMovimientoCxCEncabezadoBeforeDelete` BEFORE DELETE ON `tblCierreMovimientoCxCEncabezado` FOR EACH ROW
BEGIN
  IF OLD.`estadoCierreMovimientoCxCEncabezado`='CONFIRMADO' THEN SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='No se elimina un cierre confirmado'; END IF;
END$$

DELIMITER ;
