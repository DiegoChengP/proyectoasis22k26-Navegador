CREATE DATABASE IF NOT EXISTS dbventas
  DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE dbventas;
SET NAMES utf8mb4;
SET @ventas_fk_checks_anterior = @@FOREIGN_KEY_CHECKS;
SET FOREIGN_KEY_CHECKS = 0;

-- tblCorrelativoVenta | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7000
CREATE TABLE `tblCorrelativoVenta` (
  `idaplicacion` INT NOT NULL DEFAULT 7000,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `prefijoCorrelativoVenta` VARCHAR(30) NOT NULL,
  `ultimoNumeroCorrelativoVenta` BIGINT NOT NULL DEFAULT 0,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CorrelativoVenta` PRIMARY KEY (`id`),
  CONSTRAINT `ckCorrelativoVentaNumeroNoNegativo` CHECK (ultimoNumeroCorrelativoVenta >= 0),
  CONSTRAINT `uqCorrelativoVentaClaveNegocio` UNIQUE (`prefijoCorrelativoVenta`),
  CONSTRAINT `ckCorrelativoVentaIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCorrelativoVentaIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

-- tblTipoCliente | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblTipoCliente` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `codigoTipoCliente` VARCHAR(30) NOT NULL,
  `nombreTipoCliente` VARCHAR(100) NOT NULL,
  `porcentajeDescuentoTipoCliente` DECIMAL(5,2) NOT NULL DEFAULT 0.00,
  `descripcionTipoCliente` VARCHAR(255) NULL,
  `activoTipoCliente` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_TipoCliente` PRIMARY KEY (`id`),
  CONSTRAINT `uqTipoClientecodigo` UNIQUE (`codigoTipoCliente`),
  CONSTRAINT `uqTipoClientenombre` UNIQUE (`nombreTipoCliente`),
  CONSTRAINT `ckTipoClienteporcentajeDescuentoRango` CHECK (`porcentajeDescuentoTipoCliente` BETWEEN 0 AND 100),
  CONSTRAINT `ckTipoClienteactivoBooleano` CHECK (`activoTipoCliente` IN (0,1)),
  CONSTRAINT `ckTipoClienteIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckTipoClienteIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

-- tblListaPrecioEncabezado | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblListaPrecioEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_TipoCliente_Codigo` INT NOT NULL,
  `codigoListaPrecioEncabezado` VARCHAR(30) NOT NULL,
  `nombreListaPrecioEncabezado` VARCHAR(100) NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `vigenteDesdeListaPrecioEncabezado` DATE NOT NULL,
  `vigenteHastaListaPrecioEncabezado` DATE NULL,
  `activoListaPrecioEncabezado` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ListaPrecioEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqListaPrecioEncabezadocodigo` UNIQUE (`codigoListaPrecioEncabezado`),
  KEY `idxListaPrecioEncabezadoTipoCliente` (`Fk_TipoCliente_Codigo`),
  KEY `idxListaPrecioEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  CONSTRAINT `ckListaPrecioEncabezadoactivoListaPrecioEncabezadoBooleano` CHECK (activoListaPrecioEncabezado IN (0,1)),
  CONSTRAINT `ckListaPrecioEncabezadoFechasCoherentes` CHECK (vigenteHastaListaPrecioEncabezado >= vigenteDesdeListaPrecioEncabezado),
  CONSTRAINT `ckListaPrecioEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckListaPrecioEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblListaPrecioEncabezado`
  ADD CONSTRAINT `Fk_ListaPrecioEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ListaPrecioEncabezado_TipoCliente` FOREIGN KEY (`Fk_TipoCliente_Codigo`)
    REFERENCES `tblTipoCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblPrecioTipoCliente | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblPrecioTipoCliente` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_TipoCliente_Codigo` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `porcentajeDescuento` DECIMAL(5,2) NOT NULL DEFAULT 0.00,
  `precioBase` DECIMAL(12,4) NOT NULL,
  `precioFinal` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(precioBase * (1 - (porcentajeDescuento / 100)), 4)) STORED,
  `activo` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_PrecioTipoCliente` PRIMARY KEY (`id`),
  CONSTRAINT `uqPrecioTipoCliente_TipoClienteProducto` UNIQUE (`Fk_TipoCliente_Codigo`, `Fk_Producto_Codigo`),
  CONSTRAINT `ckPrecioTipoClienteDescuentoRango` CHECK (`porcentajeDescuento` BETWEEN 0 AND 100),
  CONSTRAINT `ckPrecioTipoClienteIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckPrecioTipoClienteIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblPrecioTipoCliente`
  ADD CONSTRAINT `Fk_PrecioTipoCliente_TipoCliente` FOREIGN KEY (`Fk_TipoCliente_Codigo`) REFERENCES `tblTipoCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_PrecioTipoCliente_Producto` FOREIGN KEY (`Fk_Producto_Codigo`) REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblHistorialModificacionPrecio | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblHistorialModificacionPrecio` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_TipoCliente_Codigo` INT NOT NULL,
  `tipoModificacion` ENUM('DESCUENTO_PORCENTAJE', 'AUMENTO_PORCENTAJE', 'AUMENTO_BASE') NOT NULL,
  `valorModificacion` DECIMAL(12,4) NOT NULL,
  `alcanceModificacion` ENUM('INDIVIDUAL', 'SELECCIONADOS', 'TODOS_FILTROS') NOT NULL,
  `Fk_Marca_Codigo` INT NULL,
  `Fk_Linea_Codigo` INT NULL,
  `Fk_Categoria_Codigo` INT NULL,
  `Fk_Proveedor_Codigo` INT NULL,
  `Fk_Producto_Codigo` INT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_HistorialModificacionPrecio` PRIMARY KEY (`id`),
  CONSTRAINT `ckHistorialModificacionPrecioIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckHistorialModificacionPrecioIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblHistorialModificacionPrecio`
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_TipoCliente` FOREIGN KEY (`Fk_TipoCliente_Codigo`) REFERENCES `tblTipoCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Usuario` FOREIGN KEY (`Fk_Usuario_Registro`) REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Marca` FOREIGN KEY (`Fk_Marca_Codigo`) REFERENCES `tblMarca` (`Pk_Marca`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Linea` FOREIGN KEY (`Fk_Linea_Codigo`) REFERENCES `tblLinea` (`Pk_Linea`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Categoria` FOREIGN KEY (`Fk_Categoria_Codigo`) REFERENCES `tblCategoria` (`Pk_Categoria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Proveedor` FOREIGN KEY (`Fk_Proveedor_Codigo`) REFERENCES `tblProveedor` (`Pk_Proveedor`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialModificacionPrecio_Producto` FOREIGN KEY (`Fk_Producto_Codigo`) REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblListaPrecioDetalle | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblListaPrecioDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_ListaPrecioEncabezado_Codigo` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `precioUnitarioListaPrecioDetalle` DECIMAL(12,4) NOT NULL,
  `activoListaPrecioDetalle` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ListaPrecioDetalle` PRIMARY KEY (`Fk_ListaPrecioEncabezado_Codigo`, `id`),
  CONSTRAINT `uqListaPrecioDetalleListaPrecioEncabezadoCodigoProductoCodigo` UNIQUE (`Fk_ListaPrecioEncabezado_Codigo`, `Fk_Producto_Codigo`),
  KEY `idxListaPrecioDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckListaPrecioDetalleactivoListaPrecioDetalleBooleano` CHECK (activoListaPrecioDetalle IN (0,1)),
  CONSTRAINT `ckListaPrecioDetallePrecioNoNegativo` CHECK (precioUnitarioListaPrecioDetalle >= 0),
  CONSTRAINT `ckListaPrecioDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckListaPrecioDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqListaPrecioDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblListaPrecioDetalle`
  ADD CONSTRAINT `Fk_ListaPrecioDetalle_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ListaPrecioDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblClienteListaPrecio | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblClienteListaPrecio` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_ListaPrecioEncabezado_Codigo` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ClienteListaPrecio` PRIMARY KEY (`id`),
  KEY `idxClienteListaPrecioListaPrecioEncabezadoCodigo` (`Fk_ListaPrecioEncabezado_Codigo`),
  CONSTRAINT `uqClienteListaPrecioClaveNegocio` UNIQUE (`Fk_Cliente_Codigo`),
  CONSTRAINT `ckClienteListaPrecioIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckClienteListaPrecioIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblClienteListaPrecio`
  ADD CONSTRAINT `Fk_ClienteListaPrecio_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ClienteListaPrecio_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblHistorialModificacionPrecioDetalle | Autor: Diego Alejandro Cheng Peña - 0901-22-8091 | Modulo: 7000 | Aplicacion: 7002
CREATE TABLE `tblHistorialModificacionPrecioDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7002,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_HistorialModificacionPrecio_Codigo` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `porcentajeDescuentoAnterior` DECIMAL(5,2) NOT NULL,
  `precioBaseAnterior` DECIMAL(12,4) NOT NULL,
  `precioFinalAnterior` DECIMAL(12,4) NOT NULL,
  `porcentajeDescuentoNuevo` DECIMAL(5,2) NOT NULL,
  `precioBaseNuevo` DECIMAL(12,4) NOT NULL,
  `precioFinalNuevo` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_HistorialModificacionPrecioDetalle` PRIMARY KEY (`Fk_HistorialModificacionPrecio_Codigo`, `id`),
  CONSTRAINT `ckHistorialModificacionPrecioDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckHistorialModificacionPrecioDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqHistorialModificacionPrecioDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblHistorialModificacionPrecioDetalle`
  ADD CONSTRAINT `Fk_HistorialDetalle_HistorialEncabezado` FOREIGN KEY (`Fk_HistorialModificacionPrecio_Codigo`) REFERENCES `tblHistorialModificacionPrecio` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_HistorialDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`) REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblNotaCxC | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7014
CREATE TABLE `tblNotaCxC` (
  `idaplicacion` INT NOT NULL DEFAULT 7014,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_NotaCxC` PRIMARY KEY (`id`),
  CONSTRAINT `uqNotaCxCnumero` UNIQUE (`numeroNotaCxC`),
  KEY `idxNotaCxCFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`),
  KEY `idxNotaCxCUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckNotaCxCMontoPositivo` CHECK (montoNotaCxC > 0),
  CONSTRAINT `ckNotaCxCValoresTipoNota` CHECK (tipoNotaCxC IN ('CREDITO','DEBITO')),
  CONSTRAINT `ckNotaCxCValoresEstadoNota` CHECK (estadoNotaCxC IN ('BORRADOR','APLICADA','ANULADA')),
  CONSTRAINT `ckNotaCxCIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckNotaCxCIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblNotaCxC`
  ADD CONSTRAINT `Fk_NotaCxC_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_NotaCxC_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblMovimientoCxC | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7003
CREATE TABLE `tblMovimientoCxC` (
  `idaplicacion` INT NOT NULL DEFAULT 7003,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_MovimientoCxC` PRIMARY KEY (`id`),
  CONSTRAINT `uqMovimientoCxCAplicacionPagoCodigo` UNIQUE (`Fk_AplicacionPago_Codigo`),
  CONSTRAINT `uqMovimientoCxCNotaCxCCodigo` UNIQUE (`Fk_NotaCxC_Codigo`),
  CONSTRAINT `uqMovimientoCxCRevertido` UNIQUE (`Fk_MovimientoCxC_Revertido`),
  KEY `idxMovimientoCxCFacturaEncabezadoCodigofecha` (`Fk_FacturaEncabezado_Codigo`, `fechaMovimientoCxC`),
  KEY `idxMovimientoCxCfecha` (`fechaMovimientoCxC`),
  KEY `idxMovimientoCxCUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckMovimientoCxCCargoAbonoExcluyentes` CHECK (cargoMovimientoCxC >= 0 AND abonoMovimientoCxC >= 0 AND ((cargoMovimientoCxC > 0 AND abonoMovimientoCxC = 0) OR (abonoMovimientoCxC > 0 AND cargoMovimientoCxC = 0))),
  CONSTRAINT `ckMovimientoCxCReversionIdentificada` CHECK ((tipoMovimientoCxC = 'REVERSO' AND Fk_MovimientoCxC_Revertido IS NOT NULL) OR (tipoMovimientoCxC <> 'REVERSO' AND Fk_MovimientoCxC_Revertido IS NULL)),
  CONSTRAINT `ckMovimientoCxCValoresTipoMovimiento` CHECK (tipoMovimientoCxC IN ('FACTURA','PAGO','NOTA_CREDITO','NOTA_DEBITO','AJUSTE_POSITIVO','AJUSTE_NEGATIVO','REVERSO')),
  CONSTRAINT `ckMovimientoCxCIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckMovimientoCxCIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblMovimientoCxC`
  ADD CONSTRAINT `Fk_MovimientoCxC_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_MovimientoCxC_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_MovimientoCxC_NotaCxC` FOREIGN KEY (`Fk_NotaCxC_Codigo`)
    REFERENCES `tblNotaCxC` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_MovimientoCxC_MovimientoCxC_Revertido` FOREIGN KEY (`Fk_MovimientoCxC_Revertido`)
    REFERENCES `tblMovimientoCxC` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_MovimientoCxC_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblDepositoVentaEncabezado | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7015
CREATE TABLE `tblDepositoVentaEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7015,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_DepositoVentaEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqDepositoVentaEncabezadocodigo` UNIQUE (`codigoDepositoVentaEncabezado`),
  CONSTRAINT `uqDepositoVentaEncabezadoMovimientoBancarioCodigo` UNIQUE (`Fk_MovimientoBancario_Codigo`),
  KEY `idxDepositoVentaEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxDepositoVentaEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxDepositoVentaEncabezadoCuentaBancariaCodigo` (`Fk_CuentaBancaria_Codigo`),
  KEY `idxDepositoVentaEncabezadoTipoTransaccionCodigo` (`Fk_TipoTransaccion_Codigo`),
  CONSTRAINT `ckDepositoVentaEncabezadoTotalPositivo` CHECK (totalDepositoVentaEncabezado > 0),
  CONSTRAINT `ckDepositoVentaEncabezadoValoresEstadoDeposito` CHECK (estadoDepositoVentaEncabezado IN ('PREPARADO','ENVIADO','CONFIRMADO','ANULADO')),
  CONSTRAINT `ckDepositoVentaEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckDepositoVentaEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblDepositoVentaEncabezado`
  ADD CONSTRAINT `Fk_DepositoVentaEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_DepositoVentaEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_DepositoVentaEncabezado_CuentaBancaria` FOREIGN KEY (`Fk_CuentaBancaria_Codigo`)
    REFERENCES `tblCuentaBancaria` (`Pk_CuentaBancaria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_DepositoVentaEncabezado_TipoTransaccion` FOREIGN KEY (`Fk_TipoTransaccion_Codigo`)
    REFERENCES `tblTipoTransaccion` (`Pk_TipoTransaccion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_DepositoVentaEncabezado_MovimientoBancario` FOREIGN KEY (`Fk_MovimientoBancario_Codigo`)
    REFERENCES `tblMovimientoBancario` (`Pk_MovimientoBancario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblReciboPago | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7005
CREATE TABLE `tblReciboPago` (
  `idaplicacion` INT NOT NULL DEFAULT 7005,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_ReciboPago` PRIMARY KEY (`id`),
  CONSTRAINT `uqReciboPagonumero` UNIQUE (`numeroReciboPago`),
  KEY `idxReciboPagoClienteCodigofecha` (`Fk_Cliente_Codigo`, `fechaReciboPago`),
  KEY `idxReciboPagoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxReciboPagoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxReciboPagoFormaPagoVentaCodigo` (`Fk_FormaPagoVenta_Codigo`),
  CONSTRAINT `ckReciboPagoMontoPositivo` CHECK (montoReciboPago > 0),
  CONSTRAINT `ckReciboPagoValoresEstadoPago` CHECK (estadoReciboPago IN ('BORRADOR','CONFIRMADO','ANULADO')),
  CONSTRAINT `ckReciboPagoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckReciboPagoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblReciboPago`
  ADD CONSTRAINT `Fk_ReciboPago_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ReciboPago_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ReciboPago_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ReciboPago_FormaPagoVenta` FOREIGN KEY (`Fk_FormaPagoVenta_Codigo`)
    REFERENCES `tblFormaPagoVenta` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblDepositoVentaDetalle | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7015
CREATE TABLE `tblDepositoVentaDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7015,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_DepositoVentaEncabezado_Codigo` INT NOT NULL,
  `Fk_ReciboPago_Codigo` INT NOT NULL,
  `importeDepositoVentaDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_DepositoVentaDetalle` PRIMARY KEY (`Fk_DepositoVentaEncabezado_Codigo`, `id`),
  KEY `idxDepositoVentaDetalleReciboPagoCodigo` (`Fk_ReciboPago_Codigo`),
  CONSTRAINT `ckDepositoVentaDetalleImportePositivo` CHECK (importeDepositoVentaDetalle > 0),
  CONSTRAINT `uqDepositoVentaDetalleClaveNegocio` UNIQUE (`Fk_DepositoVentaEncabezado_Codigo`, `Fk_ReciboPago_Codigo`),
  CONSTRAINT `ckDepositoVentaDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckDepositoVentaDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqDepositoVentaDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblDepositoVentaDetalle`
  ADD CONSTRAINT `Fk_DepositoVentaDetalle_DepositoVentaEncabezado` FOREIGN KEY (`Fk_DepositoVentaEncabezado_Codigo`)
    REFERENCES `tblDepositoVentaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_DepositoVentaDetalle_ReciboPago` FOREIGN KEY (`Fk_ReciboPago_Codigo`)
    REFERENCES `tblReciboPago` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblAplicacionPago | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7005
CREATE TABLE `tblAplicacionPago` (
  `idaplicacion` INT NOT NULL DEFAULT 7005,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_ReciboPago_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `fechaAplicacionPago` DATE NOT NULL,
  `montoAplicadoAplicacionPago` DECIMAL(12,4) NOT NULL,
  `estadoAplicacionPago` VARCHAR(20) NOT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_AplicacionPago` PRIMARY KEY (`id`),
  KEY `idxAplicacionPagoFacturaEncabezadoCodigofecha` (`Fk_FacturaEncabezado_Codigo`, `fechaAplicacionPago`),
  KEY `idxAplicacionPagoReciboPagoCodigo` (`Fk_ReciboPago_Codigo`),
  KEY `idxAplicacionPagoUsuarioRegistro` (`Fk_Usuario_Registro`),
  CONSTRAINT `ckAplicacionPagoMontoPositivo` CHECK (montoAplicadoAplicacionPago > 0),
  CONSTRAINT `ckAplicacionPagoValoresEstadoAplicacion` CHECK (estadoAplicacionPago IN ('APLICADA','REVERTIDA')),
  CONSTRAINT `ckAplicacionPagoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckAplicacionPagoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblAplicacionPago`
  ADD CONSTRAINT `Fk_AplicacionPago_ReciboPago` FOREIGN KEY (`Fk_ReciboPago_Codigo`)
    REFERENCES `tblReciboPago` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_AplicacionPago_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_AplicacionPago_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCotizacionEncabezado | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7011
CREATE TABLE `tblCotizacionEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7011,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_CotizacionEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqCotizacionEncabezadonumero` UNIQUE (`numeroCotizacionEncabezado`),
  KEY `idxCotizacionEncabezadoClienteCodigo` (`Fk_Cliente_Codigo`),
  KEY `idxCotizacionEncabezadoVendedorCodigo` (`Fk_Vendedor_Codigo`),
  KEY `idxCotizacionEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxCotizacionEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxCotizacionEncabezadoListaPrecioEncabezadoCodigo` (`Fk_ListaPrecioEncabezado_Codigo`),
  CONSTRAINT `ckCotizacionEncabezadoValoresEstadoCotizacion` CHECK (estadoCotizacionEncabezado IN ('BORRADOR','EMITIDA','ACEPTADA','RECHAZADA','VENCIDA','ANULADA')),
  CONSTRAINT `ckCotizacionEncabezadoFechasCoherentes` CHECK (vigenteHastaCotizacionEncabezado >= fechaEmisionCotizacionEncabezado),
  CONSTRAINT `ckCotizacionEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCotizacionEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblCotizacionEncabezado`
  ADD CONSTRAINT `Fk_CotizacionEncabezado_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CotizacionEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CotizacionEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CotizacionEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CotizacionEncabezado_ListaPrecioEncabezado` FOREIGN KEY (`Fk_ListaPrecioEncabezado_Codigo`)
    REFERENCES `tblListaPrecioEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCotizacionDetalle | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7011
CREATE TABLE `tblCotizacionDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7011,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_CotizacionDetalle` PRIMARY KEY (`Fk_CotizacionEncabezado_Codigo`, `id`),
  CONSTRAINT `uqCotizacionDetalleCotizacionEncabezadoCodigonumeroLinea` UNIQUE (`Fk_CotizacionEncabezado_Codigo`, `numeroLineaCotizacionDetalle`),
  KEY `idxCotizacionDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckCotizacionDetalletasaImpuestoCotizacionDetalleRango` CHECK (tasaImpuestoCotizacionDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckCotizacionDetalleCantidadPrecioDescuentoValidos` CHECK (cantidadCotizacionDetalle > 0 AND precioUnitarioCotizacionDetalle >= 0 AND descuentoMontoCotizacionDetalle >= 0 AND descuentoMontoCotizacionDetalle <= cantidadCotizacionDetalle * precioUnitarioCotizacionDetalle),
  CONSTRAINT `ckCotizacionDetalleNumeroLineaPositivo` CHECK (numeroLineaCotizacionDetalle > 0),
  CONSTRAINT `uqCotizacionDetalleId` UNIQUE (`id`),
  CONSTRAINT `ckCotizacionDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCotizacionDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblCotizacionDetalle`
  ADD CONSTRAINT `Fk_CotizacionDetalle_CotizacionEncabezado` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
    REFERENCES `tblCotizacionEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CotizacionDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblPedidoEncabezado | Autor: Natali Sofía Montenegro Portillo - 0901-23-10017 | Modulo: 7000 | Aplicacion: 7004
CREATE TABLE `tblPedidoEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7004,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `referenciaPedidoEncabezado` VARCHAR(30) NOT NULL,
  `Fk_CotizacionEncabezado_Codigo` INT NULL,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `Fk_Vendedor_Codigo` INT NOT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaPedidoEncabezado` DATE NOT NULL,
  `fechaEntregaPedidoEncabezado` DATE NULL,
  `fechaVencimientoPedidoEncabezado` DATE NULL,
  `estadoPedidoEncabezado` VARCHAR(25) NOT NULL DEFAULT 'PENDIENTE',
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_PedidoEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqPedidoEncabezadoReferencia` UNIQUE (`referenciaPedidoEncabezado`),
  CONSTRAINT `uqPedidoEncabezadoCotizacion` UNIQUE (`Fk_CotizacionEncabezado_Codigo`),
  KEY `idxPedidoEncabezadoCliente` (`Fk_Cliente_Codigo`),
  KEY `idxPedidoEncabezadoVendedor` (`Fk_Vendedor_Codigo`),
  KEY `idxPedidoEncabezadoMoneda` (`Fk_Moneda_Codigo`),
  CONSTRAINT `ckPedidoEncabezadoEstado` CHECK (`estadoPedidoEncabezado` IN ('PENDIENTE','CONFIRMADO','COMPLETADO','ANULADO')),
  CONSTRAINT `ckPedidoEncabezadoFechas` CHECK (
    (`fechaEntregaPedidoEncabezado` IS NULL OR `fechaEntregaPedidoEncabezado` >= `fechaPedidoEncabezado`)
    AND (`fechaVencimientoPedidoEncabezado` IS NULL OR `fechaVencimientoPedidoEncabezado` >= `fechaPedidoEncabezado`)
  ),
  CONSTRAINT `ckPedidoEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckPedidoEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblPedidoEncabezado`
  ADD CONSTRAINT `Fk_PedidoEncabezado_Cotizacion` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
    REFERENCES `tblCotizacionEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_PedidoEncabezado_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_PedidoEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_PedidoEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblPedidoDetalle | Autor: Natali Sofía Montenegro Portillo - 0901-23-10017 | Modulo: 7000 | Aplicacion: 7004
CREATE TABLE `tblPedidoDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7004,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_PedidoEncabezado_Codigo` INT NOT NULL,
  `OrdenPedido` INT NOT NULL,
  `Fk_Producto_Codigo` INT NOT NULL,
  `cantidadPedidoDetalle` DECIMAL(12,4) NOT NULL,
  `precioUnitarioPedidoDetalle` DECIMAL(12,4) NOT NULL,
  `descuentoMontoPedidoDetalle` DECIMAL(12,4) NOT NULL DEFAULT 0,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_PedidoDetalle` PRIMARY KEY (`Fk_PedidoEncabezado_Codigo`, `id`),
  KEY `idxPedidoDetalleProducto` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckPedidoDetalleOrdenPositivo` CHECK (`OrdenPedido` > 0),
  CONSTRAINT `ckPedidoDetalleCantidadPrecioDescuento` CHECK (
    `cantidadPedidoDetalle` > 0
    AND `precioUnitarioPedidoDetalle` >= 0
    AND `descuentoMontoPedidoDetalle` >= 0
    AND `descuentoMontoPedidoDetalle` <= `cantidadPedidoDetalle` * `precioUnitarioPedidoDetalle`
  ),
  CONSTRAINT `uqPedidoDetalleClaveNegocio` UNIQUE (`Fk_PedidoEncabezado_Codigo`, `OrdenPedido`, `Fk_Producto_Codigo`),
  CONSTRAINT `ckPedidoDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckPedidoDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqPedidoDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblPedidoDetalle`
  ADD CONSTRAINT `Fk_PedidoDetalle_PedidoEncabezado` FOREIGN KEY (`Fk_PedidoEncabezado_Codigo`)
    REFERENCES `tblPedidoEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_PedidoDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblClienteFacturaVersion | Autor: Matthew Andreé Juárez Xoy - 0901-23-4250 | Modulo: 7000 | Aplicacion: 7007
CREATE TABLE `tblClienteFacturaVersion` (
  `idaplicacion` INT NOT NULL DEFAULT 7007,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_Cliente_Codigo` INT NOT NULL,
  `numeroVersionClienteFacturaVersion` INT NOT NULL,
  `tipoIdentificacionClienteFacturaVersion` ENUM('NIT','CUI','CF') NOT NULL,
  `nitClienteFacturaVersion` CHAR(10) NULL,
  `dpiClienteFacturaVersion` CHAR(13) NULL,
  `nombreClienteFacturaVersion` VARCHAR(100) NOT NULL,
  `direccionClienteFacturaVersion` VARCHAR(500) NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ClienteFacturaVersion` PRIMARY KEY (`id`),
  CONSTRAINT `uqClienteFacturaVersionClienteCodigonumeroVersion` UNIQUE (`Fk_Cliente_Codigo`, `numeroVersionClienteFacturaVersion`),
  CONSTRAINT `ckClienteFacturaVersionIdentificacionCoherente` CHECK ((tipoIdentificacionClienteFacturaVersion = 'CF' AND nitClienteFacturaVersion IS NULL AND dpiClienteFacturaVersion IS NULL) OR (tipoIdentificacionClienteFacturaVersion = 'NIT' AND nitClienteFacturaVersion IS NOT NULL AND dpiClienteFacturaVersion IS NULL) OR (tipoIdentificacionClienteFacturaVersion = 'CUI' AND dpiClienteFacturaVersion IS NOT NULL AND nitClienteFacturaVersion IS NULL)),
  CONSTRAINT `ckClienteFacturaVersionVersionPositiva` CHECK (numeroVersionClienteFacturaVersion > 0),
  CONSTRAINT `ckClienteFacturaVersionIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckClienteFacturaVersionIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblClienteFacturaVersion`
  ADD CONSTRAINT `Fk_ClienteFacturaVersion_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblFacturaEncabezado | Autor: Matthew Andreé Juárez Xoy - 0901-23-4250 | Modulo: 7000 | Aplicacion: 7007
CREATE TABLE `tblFacturaEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7007,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `uuidDteFacturaEncabezado` CHAR(36) NOT NULL,
  `serieFacturaEncabezado` VARCHAR(30) NOT NULL,
  `numeroFacturaEncabezado` VARCHAR(30) NOT NULL,
  `Fk_Sucursal_Codigo` INT NOT NULL,
  `Fk_ClienteFacturaVersion_Codigo` INT NOT NULL,
  `Fk_Vendedor_Codigo` INT NULL,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `Fk_CotizacionEncabezado_Codigo` INT NULL,
  `Fk_PedidoEncabezado_Codigo` INT NULL,
  `fechaEmisionFacturaEncabezado` DATETIME NOT NULL,
  `fechaVencimientoFacturaEncabezado` DATE NOT NULL,
  `tipoDteFacturaEncabezado` VARCHAR(30) NOT NULL DEFAULT 'Factura',
  `condicionPagoFacturaEncabezado` VARCHAR(15) NOT NULL,
  `estadoFacturaEncabezado` VARCHAR(25) NOT NULL DEFAULT 'EMITIDA',
  `fechaCertificacionFacturaEncabezado` DATETIME NULL,
  `subtotalFacturaEncabezado` DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
  `impuestoIvaFacturaEncabezado` DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
  `totalFacturaEncabezado` DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
  `observacionesFacturaEncabezado` TEXT NULL,
  `Fk_Usuario_Registro` INT NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_FacturaEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqFacturaEncabezadoSucursalCodigoserienumero` UNIQUE (`Fk_Sucursal_Codigo`, `serieFacturaEncabezado`, `numeroFacturaEncabezado`),
  CONSTRAINT `uqFacturaEncabezadoCotizacionEncabezadoCodigo` UNIQUE (`Fk_CotizacionEncabezado_Codigo`),
  KEY `idxFacturaEncabezadoPedidoEncabezadoCodigo` (`Fk_PedidoEncabezado_Codigo`),
  KEY `idxFacturaEncabezadoVendedorCodigo` (`Fk_Vendedor_Codigo`),
  KEY `idxFacturaEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxFacturaEncabezadoUsuarioRegistro` (`Fk_Usuario_Registro`),
  KEY `idxFacturaEncabezadoClienteFacturaVersionCodigo` (`Fk_ClienteFacturaVersion_Codigo`),
  CONSTRAINT `ckFacturaEncabezadoCondicionPagoValida` CHECK (condicionPagoFacturaEncabezado IN ('CONTADO','CREDITO')),
  CONSTRAINT `ckFacturaEncabezadoValoresEstadoFactura` CHECK (estadoFacturaEncabezado IN ('BORRADOR','EMITIDA','ANULADA')),
  CONSTRAINT `ckFacturaEncabezadoFechasCoherentes` CHECK (fechaVencimientoFacturaEncabezado >= DATE(fechaEmisionFacturaEncabezado)),
  CONSTRAINT `uqFacturaEncabezadoClaveNegocio` UNIQUE (`uuidDteFacturaEncabezado`),
  CONSTRAINT `ckFacturaEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckFacturaEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblFacturaEncabezado`
  ADD CONSTRAINT `Fk_FacturaEncabezado_Sucursal` FOREIGN KEY (`Fk_Sucursal_Codigo`)
REFERENCES `tblSucursal` (`Pk_Sucursal`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_ClienteFacturaVersion` FOREIGN KEY (`Fk_ClienteFacturaVersion_Codigo`)
REFERENCES `tblClienteFacturaVersion` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
REFERENCES `tblVendedor` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_CotizacionEncabezado` FOREIGN KEY (`Fk_CotizacionEncabezado_Codigo`)
REFERENCES `tblCotizacionEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_PedidoEncabezado` FOREIGN KEY (`Fk_PedidoEncabezado_Codigo`)
REFERENCES `tblPedidoEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaEncabezado_Usuario_Registro` FOREIGN KEY (`Fk_Usuario_Registro`)
REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblFacturaDetalle | Autor: Matthew Andreé Juárez Xoy - 0901-23-4250 | Modulo: 7000 | Aplicacion: 7007
CREATE TABLE `tblFacturaDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7007,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `Fk_FacturaEncabezado_UuidDte` CHAR(36) NOT NULL,
  `numeroLineaFacturaDetalle` INT NOT NULL,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_Producto_Codigo` INT NOT NULL,
  `cantidadFacturaDetalle` DECIMAL(12,4) NOT NULL,
  `precioUnitarioFacturaDetalle` DECIMAL(12,4) NOT NULL,
  `descuentoMontoFacturaDetalle` DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
  `tasaImpuestoFacturaDetalle` DECIMAL(3,2) NOT NULL DEFAULT 0.12,
  `impuestoMontoFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND((cantidadFacturaDetalle * precioUnitarioFacturaDetalle - descuentoMontoFacturaDetalle) * tasaImpuestoFacturaDetalle, 4)) STORED,
  `subtotalLineaFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(cantidadFacturaDetalle * precioUnitarioFacturaDetalle - descuentoMontoFacturaDetalle, 4)) STORED,
  `totalLineaFacturaDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(cantidadFacturaDetalle * precioUnitarioFacturaDetalle - descuentoMontoFacturaDetalle + impuestoMontoFacturaDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_FacturaDetalle` PRIMARY KEY (`Fk_FacturaEncabezado_UuidDte`, `id`),
  KEY `idxFacturaDetalleProductoCodigo` (`Fk_Producto_Codigo`),
  CONSTRAINT `ckFacturaDetalletasaImpuestoFacturaDetalleRango` CHECK (tasaImpuestoFacturaDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckFacturaDetalleCantidadPrecioDescuentoValidos` CHECK (cantidadFacturaDetalle > 0 AND precioUnitarioFacturaDetalle >= 0 AND descuentoMontoFacturaDetalle >= 0 AND descuentoMontoFacturaDetalle <= cantidadFacturaDetalle * precioUnitarioFacturaDetalle),
  CONSTRAINT `ckFacturaDetalleNumeroLineaPositivo` CHECK (numeroLineaFacturaDetalle > 0),
  CONSTRAINT `uqFacturaDetalleClaveNegocio` UNIQUE (`Fk_FacturaEncabezado_UuidDte`, `numeroLineaFacturaDetalle`),
  CONSTRAINT `ckFacturaDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckFacturaDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqFacturaDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblFacturaDetalle`
  ADD CONSTRAINT `Fk_FacturaDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_UuidDte`)
REFERENCES `tblFacturaEncabezado` (`uuidDteFacturaEncabezado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_FacturaDetalle_Producto` FOREIGN KEY (`Fk_Producto_Codigo`)
REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblReglaComision | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblReglaComision` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `codigoReglaComision` VARCHAR(30) NOT NULL,
  `nombreReglaComision` VARCHAR(100) NOT NULL,
  `baseCalculoReglaComision` VARCHAR(30) NOT NULL,
  `tasaComisionReglaComision` DECIMAL(3,2) NOT NULL,
  `vigenteDesdeReglaComision` DATE NOT NULL,
  `vigenteHastaReglaComision` DATE NULL,
  `requiereAutorizacionReglaComision` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ReglaComision` PRIMARY KEY (`id`),
  CONSTRAINT `uqReglaComisioncodigo` UNIQUE (`codigoReglaComision`),
  CONSTRAINT `ckReglaComisiontasaComisionReglaComisionRango` CHECK (tasaComisionReglaComision BETWEEN 0 AND 0.99),
  CONSTRAINT `ckReglaComisionrequiereAutorizacionReglaComisionBooleano` CHECK (requiereAutorizacionReglaComision IN (0,1)),
  CONSTRAINT `ckReglaComisionValoresBaseCalculo` CHECK (baseCalculoReglaComision IN ('VENTA_NETA','COBRO_NETO')),
  CONSTRAINT `ckReglaComisionFechasCoherentes` CHECK (vigenteHastaReglaComision >= vigenteDesdeReglaComision),
  CONSTRAINT `ckReglaComisionIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckReglaComisionIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

-- tblComisionEncabezado | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblComisionEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_ComisionEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqComisionEncabezadonumero` UNIQUE (`numeroComisionEncabezado`),
  KEY `idxComisionEncabezadoVendedorCodigofechaDesdefechaHasta` (`Fk_Vendedor_Codigo`, `fechaDesdeComisionEncabezado`, `fechaHastaComisionEncabezado`),
  KEY `idxComisionEncabezadoReglaComisionCodigo` (`Fk_ReglaComision_Codigo`),
  KEY `idxComisionEncabezadoMonedaCodigo` (`Fk_Moneda_Codigo`),
  KEY `idxComisionEncabezadoUsuarioSolicita` (`Fk_Usuario_Solicita`),
  KEY `idxComisionEncabezadoUsuarioAutoriza` (`Fk_Usuario_Autoriza`),
  CONSTRAINT `ckComisionEncabezadoValoresEstadoComision` CHECK (estadoComisionEncabezado IN ('BORRADOR','SOLICITADA','APROBADA','RECHAZADA','ENVIADA')),
  CONSTRAINT `ckComisionEncabezadoFechasCoherentes` CHECK (fechaHastaComisionEncabezado >= fechaDesdeComisionEncabezado),
  CONSTRAINT `ckComisionEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckComisionEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblComisionEncabezado`
  ADD CONSTRAINT `Fk_ComisionEncabezado_Vendedor` FOREIGN KEY (`Fk_Vendedor_Codigo`)
    REFERENCES `tblVendedor` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionEncabezado_ReglaComision` FOREIGN KEY (`Fk_ReglaComision_Codigo`)
    REFERENCES `tblReglaComision` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionEncabezado_Usuario_Solicita` FOREIGN KEY (`Fk_Usuario_Solicita`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionEncabezado_Usuario_Autoriza` FOREIGN KEY (`Fk_Usuario_Autoriza`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblComisionFacturaEncabezado | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblComisionFacturaEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `metodoComisionFacturaEncabezado` ENUM('LINEA','PRODUCTO','CATEGORIA','MARCA','MIXTA') NOT NULL,
  `estadoComisionFacturaEncabezado` ENUM('CALCULADA','ANULADA') NOT NULL DEFAULT 'CALCULADA',
  `observacionesComisionFacturaEncabezado` TEXT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionFacturaEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqComisionFacturaEncabezadoFacturaEncabezadoCodigo` UNIQUE (`Fk_FacturaEncabezado_Codigo`),
  CONSTRAINT `ckComisionFacturaEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckComisionFacturaEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblComisionFacturaEncabezado`
  ADD CONSTRAINT `Fk_ComisionFacturaEncabezado_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblComisionDetalle | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblComisionDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_ComisionEncabezado_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `Fk_AplicacionPago_Codigo` INT NULL,
  `baseComisionDetalle` DECIMAL(12,4) NOT NULL,
  `tasaAplicadaComisionDetalle` DECIMAL(3,2) NOT NULL,
  `montoComisionDetalle` DECIMAL(12,4) GENERATED ALWAYS AS (ROUND(baseComisionDetalle * tasaAplicadaComisionDetalle, 4)) STORED,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ComisionDetalle` PRIMARY KEY (`Fk_ComisionEncabezado_Codigo`, `id`),
  KEY `idxComisionDetalleComisionEncabezadoCodigo` (`Fk_ComisionEncabezado_Codigo`),
  KEY `idxComisionDetalleFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`),
  KEY `idxComisionDetalleAplicacionPagoCodigo` (`Fk_AplicacionPago_Codigo`),
  CONSTRAINT `ckComisionDetalletasaAplicadaComisionDetalleRango` CHECK (tasaAplicadaComisionDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckComisionDetalleBaseNoNegativa` CHECK (baseComisionDetalle >= 0),
  CONSTRAINT `ckComisionDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckComisionDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqComisionDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblComisionDetalle`
  ADD CONSTRAINT `Fk_ComisionDetalle_ComisionEncabezado` FOREIGN KEY (`Fk_ComisionEncabezado_Codigo`)
    REFERENCES `tblComisionEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionDetalle_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblComisionAbono | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblComisionAbono` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_ComisionAbono` PRIMARY KEY (`id`),
  CONSTRAINT `uqComisionAbonoAplicacionPagoCodigo` UNIQUE (`Fk_AplicacionPago_Codigo`),
  KEY `idxComisionAbonoComisionFacturaEncabezadoCodigoestadoPa19c1f12f` (`Fk_ComisionFacturaEncabezado_Codigo`, `estadoPagoVendedorComisionAbono`),
  CONSTRAINT `ckComisionAbonotasaFacturaPagadaAcumuladaComisionAbonoRango` CHECK (tasaFacturaPagadaAcumuladaComisionAbono BETWEEN 0 AND 1),
  CONSTRAINT `ckComisionAbonoDevengoNoNegativo` CHECK (montoComisionDevengadoComisionAbono >= 0),
  CONSTRAINT `ckComisionAbonoFechaPagoVendedor` CHECK (estadoPagoVendedorComisionAbono <> 'PAGADO' OR fechaPagoVendedorComisionAbono IS NOT NULL),
  CONSTRAINT `ckComisionAbonoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckComisionAbonoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblComisionAbono`
  ADD CONSTRAINT `Fk_ComisionAbono_ComisionFacturaEncabezado` FOREIGN KEY (`Fk_ComisionFacturaEncabezado_Codigo`)
    REFERENCES `tblComisionFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionAbono_AplicacionPago` FOREIGN KEY (`Fk_AplicacionPago_Codigo`)
    REFERENCES `tblAplicacionPago` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblComisionFacturaDetalle | Autor: Roger Yankhel de Jesús Herrera Alcántara - 0901-23-2429 | Modulo: 7000 | Aplicacion: 7013
CREATE TABLE `tblComisionFacturaDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7013,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_ComisionFacturaDetalle` PRIMARY KEY (`Fk_ComisionFacturaEncabezado_Codigo`, `id`),
  CONSTRAINT `uqComisionFacturaDetalleFacturaDetalleCodigo` UNIQUE (`Fk_FacturaDetalle_Codigo`),
  KEY `idxComisionFacturaDetalleComisionFacturaEncabezadoCodigo` (`Fk_ComisionFacturaEncabezado_Codigo`),
  KEY `idxComisionFacturaDetalleLineaCodigo` (`Fk_Linea_Codigo`),
  KEY `idxComisionFacturaDetalleCategoriaCodigo` (`Fk_Categoria_Codigo`),
  KEY `idxComisionFacturaDetalleMarcaCodigo` (`Fk_Marca_Codigo`),
  CONSTRAINT `ckComisionFacturaDetalletasaAplicadaComisionFacturaDetalleRango` CHECK (tasaAplicadaComisionFacturaDetalle BETWEEN 0 AND 0.99),
  CONSTRAINT `ckComisionFacturaDetalleBaseNoNegativa` CHECK (baseComisionFacturaDetalle >= 0),
  CONSTRAINT `ckComisionFacturaDetalleClasificacionTipada` CHECK ((metodoComisionFacturaDetalle = 'PRODUCTO' AND Fk_Linea_Codigo IS NULL AND Fk_Categoria_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'LINEA' AND Fk_Linea_Codigo IS NOT NULL AND Fk_Categoria_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'CATEGORIA' AND Fk_Categoria_Codigo IS NOT NULL AND Fk_Linea_Codigo IS NULL AND Fk_Marca_Codigo IS NULL) OR (metodoComisionFacturaDetalle = 'MARCA' AND Fk_Marca_Codigo IS NOT NULL AND Fk_Linea_Codigo IS NULL AND Fk_Categoria_Codigo IS NULL)),
  CONSTRAINT `ckComisionFacturaDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckComisionFacturaDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqComisionFacturaDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblComisionFacturaDetalle`
  ADD CONSTRAINT `Fk_ComisionFacturaDetalle_ComisionFacturaEncabezado` FOREIGN KEY (`Fk_ComisionFacturaEncabezado_Codigo`)
    REFERENCES `tblComisionFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionFacturaDetalle_FacturaDetalle` FOREIGN KEY (`Fk_FacturaDetalle_Codigo`)
    REFERENCES `tblFacturaDetalle` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionFacturaDetalle_Linea` FOREIGN KEY (`Fk_Linea_Codigo`)
    REFERENCES `tblLinea` (`Pk_Linea`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionFacturaDetalle_Categoria` FOREIGN KEY (`Fk_Categoria_Codigo`)
    REFERENCES `tblCategoria` (`Pk_Categoria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_ComisionFacturaDetalle_Marca` FOREIGN KEY (`Fk_Marca_Codigo`)
    REFERENCES `tblMarca` (`Pk_Marca`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCliente | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7009
CREATE TABLE `tblCliente` (
  `idaplicacion` INT NOT NULL DEFAULT 7009,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `codigoCliente` VARCHAR(30) NOT NULL,
  `tipoIdentificacionCliente` ENUM('NIT','CUI','CF') NOT NULL,
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
  CONSTRAINT `Pk_Cliente` PRIMARY KEY (`id`),
  CONSTRAINT `uqClientecodigo` UNIQUE (`codigoCliente`),
  CONSTRAINT `uqClientenit` UNIQUE (`nitCliente`),
  CONSTRAINT `uqClientedpi` UNIQUE (`dpiCliente`),
  KEY `idxClienteMonedaCredito` (`Fk_Moneda_Credito`),
  CONSTRAINT `ckClienteactivoClienteBooleano` CHECK (activoCliente IN (0,1)),
  CONSTRAINT `ckClienteIdentificacionCoherente` CHECK ((tipoIdentificacionCliente = 'CF' AND nitCliente IS NULL AND dpiCliente IS NULL) OR (tipoIdentificacionCliente = 'NIT' AND nitCliente IS NOT NULL AND dpiCliente IS NULL) OR (tipoIdentificacionCliente = 'CUI' AND dpiCliente IS NOT NULL AND nitCliente IS NULL)),
  CONSTRAINT `ckClienteCreditoNoNegativo` CHECK (diasCreditoCliente >= 0 AND limiteCreditoCliente >= 0),
  CONSTRAINT `ckClienteIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckClienteIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblCliente`
  ADD CONSTRAINT `Fk_Cliente_Moneda_Credito` FOREIGN KEY (`Fk_Moneda_Credito`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblVendedor | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7009
CREATE TABLE `tblVendedor` (
  `idaplicacion` INT NOT NULL DEFAULT 7009,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `codigoVendedor` VARCHAR(30) NOT NULL,
  `Fk_Empleado_Codigo` INT NOT NULL,
  `activoVendedor` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_Vendedor` PRIMARY KEY (`id`),
  CONSTRAINT `uqVendedorcodigo` UNIQUE (`codigoVendedor`),
  CONSTRAINT `uqVendedorEmpleadoCodigo` UNIQUE (`Fk_Empleado_Codigo`),
  CONSTRAINT `ckVendedoractivoVendedorBooleano` CHECK (activoVendedor IN (0,1)),
  CONSTRAINT `ckVendedorIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckVendedorIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblVendedor`
  ADD CONSTRAINT `Fk_Vendedor_Empleado` FOREIGN KEY (`Fk_Empleado_Codigo`)
    REFERENCES `tblEmpleado` (`idEmpleado`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblProductoVenta | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7009
CREATE TABLE `tblProductoVenta` (
  `idaplicacion` INT NOT NULL DEFAULT 7009,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL,
  `tipoItemProductoVenta` CHAR(1) NOT NULL,
  `unidadMedidaProductoVenta` VARCHAR(30) NOT NULL,
  `tasaImpuestoProductoVenta` DECIMAL(3,2) NOT NULL DEFAULT 0,
  `activoProductoVenta` BOOLEAN NOT NULL DEFAULT 1,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ProductoVenta` PRIMARY KEY (`id`),
  CONSTRAINT `ckProductoVentatasaImpuestoProductoVentaRango` CHECK (tasaImpuestoProductoVenta BETWEEN 0 AND 0.99),
  CONSTRAINT `ckProductoVentaactivoProductoVentaBooleano` CHECK (activoProductoVenta IN (0,1)),
  CONSTRAINT `ckProductoVentaTipoItemValido` CHECK (tipoItemProductoVenta IN ('B','S')),
  CONSTRAINT `ckProductoVentaIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckProductoVentaIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblProductoVenta`
  ADD CONSTRAINT `Fk_ProductoVenta_Producto` FOREIGN KEY (`id`)
    REFERENCES `tblProducto` (`Pk_Producto`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblFormaPagoVenta | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7009
CREATE TABLE `tblFormaPagoVenta` (
  `idaplicacion` INT NOT NULL DEFAULT 7009,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `codigoFormaPagoVenta` VARCHAR(30) NOT NULL,
  `nombreFormaPagoVenta` VARCHAR(100) NOT NULL,
  `requiereReferenciaFormaPagoVenta` BOOLEAN NOT NULL DEFAULT FALSE,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_FormaPagoVenta` PRIMARY KEY (`id`),
  CONSTRAINT `uqFormaPagoVentacodigo` UNIQUE (`codigoFormaPagoVenta`),
  CONSTRAINT `ckFormaPagoVentarequiereReferenciaFormaPagoVentaBooleano` CHECK (requiereReferenciaFormaPagoVenta IN (0,1)),
  CONSTRAINT `ckFormaPagoVentaIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckFormaPagoVentaIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

-- tblCierreMovimientoCxCEncabezado | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblCierreMovimientoCxCEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_CierreMovimientoCxCEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqCierreMovimientoCxCEncabezadonumero` UNIQUE (`numeroCierreMovimientoCxCEncabezado`),
  KEY `idxCierreMovimientoCxCEncabezadoMonedaCodigofechaDesdefechaHasta` (`Fk_Moneda_Codigo`, `fechaDesdeCierreMovimientoCxCEncabezado`, `fechaHastaCierreMovimientoCxCEncabezado`),
  KEY `idxCierreMovimientoCxCEncabezadoUsuarioResponsable` (`Fk_Usuario_Responsable`),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadodocumentosRevisadosCierra3aaae2a` CHECK (documentosRevisadosCierreMovimientoCxCEncabezado IN (0,1)),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadopagosConciliadosCierreMob8223bf3` CHECK (pagosConciliadosCierreMovimientoCxCEncabezado IN (0,1)),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoValoresTipoCierre` CHECK (tipoCierreMovimientoCxCEncabezado IN ('MENSUAL','ANUAL')),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoValoresEstadoCierre` CHECK (estadoCierreMovimientoCxCEncabezado IN ('BORRADOR','CONFIRMADO','ANULADO')),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoFechasCoherentes` CHECK (fechaHastaCierreMovimientoCxCEncabezado >= fechaDesdeCierreMovimientoCxCEncabezado),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCierreMovimientoCxCEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblCierreMovimientoCxCEncabezado`
  ADD CONSTRAINT `Fk_CierreMovimientoCxCEncabezado_Usuario_Responsable` FOREIGN KEY (`Fk_Usuario_Responsable`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CierreMovimientoCxCEncabezado_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblControlPeriodoCxC | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblControlPeriodoCxC` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `fechaUltimoCierreControlPeriodoCxC` DATE NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_ControlPeriodoCxC` PRIMARY KEY (`id`),
  CONSTRAINT `uqControlPeriodoCxCClaveNegocio` UNIQUE (`Fk_Moneda_Codigo`),
  CONSTRAINT `ckControlPeriodoCxCIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckControlPeriodoCxCIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblControlPeriodoCxC`
  ADD CONSTRAINT `Fk_ControlPeriodoCxC_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCuentaContableCxC | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7000
CREATE TABLE `tblCuentaContableCxC` (
  `idaplicacion` INT NOT NULL DEFAULT 7000,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_Moneda_Codigo` BIGINT NOT NULL,
  `tipoCuentaContableCxC` VARCHAR(30) NOT NULL,
  `cuentaClientesCuentaContableCxC` VARCHAR(30) NOT NULL,
  `cuentaContrapartidaCuentaContableCxC` VARCHAR(30) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CuentaContableCxC` PRIMARY KEY (`id`),
  CONSTRAINT `uqCuentaContableCxCClaveNegocio` UNIQUE (`Fk_Moneda_Codigo`, `tipoCuentaContableCxC`),
  CONSTRAINT `ckCuentaContableCxCIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCuentaContableCxCIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblCuentaContableCxC`
  ADD CONSTRAINT `Fk_CuentaContableCxC_Moneda` FOREIGN KEY (`Fk_Moneda_Codigo`)
    REFERENCES `tblMoneda` (`Pk_Moneda`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCierreSaldoCliente | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblCierreSaldoCliente` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
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
  CONSTRAINT `Pk_CierreSaldoCliente` PRIMARY KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `id`),
  KEY `idxCierreSaldoClienteClienteCodigo` (`Fk_Cliente_Codigo`),
  CONSTRAINT `uqCierreSaldoClienteClaveNegocio` UNIQUE (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `Fk_Cliente_Codigo`),
  CONSTRAINT `ckCierreSaldoClienteIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCierreSaldoClienteIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqCierreSaldoClienteId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblCierreSaldoCliente`
  ADD CONSTRAINT `Fk_CierreSaldoCliente_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CierreSaldoCliente_Cliente` FOREIGN KEY (`Fk_Cliente_Codigo`)
    REFERENCES `tblCliente` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblTrasladoCxCEncabezado | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblTrasladoCxCEncabezado` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_CierreMovimientoCxCEncabezado_Codigo` INT NOT NULL,
  `claveIntegracionTrasladoCxCEncabezado` VARCHAR(80) NOT NULL,
  `estadoTrasladoCxCEncabezado` VARCHAR(20) NOT NULL DEFAULT 'PENDIENTE',
  `referenciaContableTrasladoCxCEncabezado` VARCHAR(100) NULL,
  `Fk_Usuario_Codigo` INT NOT NULL,
  `fechaConfirmacionTrasladoCxCEncabezado` DATETIME NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_TrasladoCxCEncabezado` PRIMARY KEY (`id`),
  CONSTRAINT `uqTrasladoCxCEncabezadoclaveIntegracion` UNIQUE (`claveIntegracionTrasladoCxCEncabezado`),
  KEY `idxTrasladoCxCEncabezadoUsuarioCodigo` (`Fk_Usuario_Codigo`),
  CONSTRAINT `ckTrasladoCxCEncabezadoValoresEstado` CHECK (estadoTrasladoCxCEncabezado IN ('PENDIENTE','PREPARADO','ENVIADO','CONFIRMADO','RECHAZADO')),
  CONSTRAINT `uqTrasladoCxCEncabezadoClaveNegocio` UNIQUE (`Fk_CierreMovimientoCxCEncabezado_Codigo`),
  CONSTRAINT `ckTrasladoCxCEncabezadoIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckTrasladoCxCEncabezadoIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015)
) ENGINE=InnoDB;

ALTER TABLE `tblTrasladoCxCEncabezado`
  ADD CONSTRAINT `Fk_TrasladoCxCEncabezado_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_TrasladoCxCEncabezado_Usuario` FOREIGN KEY (`Fk_Usuario_Codigo`)
    REFERENCES `tblUsuario` (`idUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblTrasladoCxCDetalle | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblTrasladoCxCDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_TrasladoCxCEncabezado_Codigo` INT NOT NULL,
  `numeroLineaTrasladoCxCDetalle` INT NOT NULL,
  `cuentaTrasladoCxCDetalle` VARCHAR(30) NOT NULL,
  `descripcionTrasladoCxCDetalle` VARCHAR(100) NOT NULL,
  `debeTrasladoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `haberTrasladoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_TrasladoCxCDetalle` PRIMARY KEY (`Fk_TrasladoCxCEncabezado_Codigo`, `id`),
  CONSTRAINT `ckTrasladoCxCDetalleDebeHaberNoNegativos` CHECK (debeTrasladoCxCDetalle >= 0 AND haberTrasladoCxCDetalle >= 0),
  CONSTRAINT `uqTrasladoCxCDetalleClaveNegocio` UNIQUE (`Fk_TrasladoCxCEncabezado_Codigo`, `numeroLineaTrasladoCxCDetalle`),
  CONSTRAINT `ckTrasladoCxCDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckTrasladoCxCDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqTrasladoCxCDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblTrasladoCxCDetalle`
  ADD CONSTRAINT `Fk_TrasladoCxCDetalle_TrasladoCxCEncabezado` FOREIGN KEY (`Fk_TrasladoCxCEncabezado_Codigo`)
    REFERENCES `tblTrasladoCxCEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

-- tblCierreMovimientoCxCDetalle | Autor: Jose Torres - 0901-23-1091 | Modulo: 7000 | Aplicacion: 7010
CREATE TABLE `tblCierreMovimientoCxCDetalle` (
  `idaplicacion` INT NOT NULL DEFAULT 7010,
  `idmodulo` INT NOT NULL DEFAULT 7000,
  `id` INT NOT NULL AUTO_INCREMENT,
  `Fk_CierreMovimientoCxCEncabezado_Codigo` INT NOT NULL,
  `Fk_FacturaEncabezado_Codigo` INT NOT NULL,
  `saldoInicialCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `cargosPeriodoCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `abonosPeriodoCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `saldoFinalCierreMovimientoCxCDetalle` DECIMAL(12,4) NOT NULL,
  `created_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  CONSTRAINT `Pk_CierreMovimientoCxCDetalle` PRIMARY KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `id`),
  CONSTRAINT `uqCierreMovimientoCxCDetalleCierreMovimientoCxCEncabeza4f1c6502` UNIQUE (`Fk_CierreMovimientoCxCEncabezado_Codigo`, `Fk_FacturaEncabezado_Codigo`),
  KEY `idxCierreMovimientoCxCDetalleFacturaEncabezadoCodigo` (`Fk_FacturaEncabezado_Codigo`),
  CONSTRAINT `ckCierreMovimientoCxCDetalleIdModulo` CHECK (`idmodulo` = 7000),
  CONSTRAINT `ckCierreMovimientoCxCDetalleIdAplicacion` CHECK (`idaplicacion` BETWEEN 7000 AND 7015),
  CONSTRAINT `uqCierreMovimientoCxCDetalleId` UNIQUE (`id`)
) ENGINE=InnoDB;

ALTER TABLE `tblCierreMovimientoCxCDetalle`
  ADD CONSTRAINT `Fk_CierreMovimientoCxCDetalle_CierreMovimientoCxCEncabezado` FOREIGN KEY (`Fk_CierreMovimientoCxCEncabezado_Codigo`)
    REFERENCES `tblCierreMovimientoCxCEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `Fk_CierreMovimientoCxCDetalle_FacturaEncabezado` FOREIGN KEY (`Fk_FacturaEncabezado_Codigo`)
    REFERENCES `tblFacturaEncabezado` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

SET FOREIGN_KEY_CHECKS = @ventas_fk_checks_anterior;
