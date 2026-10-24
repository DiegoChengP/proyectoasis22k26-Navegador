

CREATE DATABASE IF NOT EXISTS dbLogistica CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE dbLogistica;

-- ==============================================================
-- BASE DE DATOS UNIFICADA: INVENTARIO Y LOGISTICA
-- MySQL 8.0.16+ / InnoDB
-- Los atributos de negocio conservan sus nombres; auditoria usa EST-01.
-- Ejecutar en un esquema previamente seleccionado (USE nombre_bd;).
-- ==============================================================
SET NAMES utf8mb4;

CREATE TABLE tblSucursal (
    idSucursal INT AUTO_INCREMENT PRIMARY KEY,
    nombreSucursal VARCHAR(100) NOT NULL,
    descripcionSucursal VARCHAR(255),
    ubicacionSucursal VARCHAR(500) NOT NULL,
    paisSucursal VARCHAR(100) NOT NULL,
    estadoSucursal BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblBodega (
    idBodega INT AUTO_INCREMENT PRIMARY KEY,
    nombreBodega VARCHAR(100) NOT NULL,
    fkSucursalIdSucursal INT NOT NULL,
    paisBodega VARCHAR(100) NOT NULL,
    estadoBodega BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    UNIQUE KEY uqBodegaSucursal (idBodega, fkSucursalIdSucursal),
    CONSTRAINT Fk_Bodega_Sucursal FOREIGN KEY (fkSucursalIdSucursal) REFERENCES tblSucursal(idSucursal)
) ENGINE=InnoDB;

CREATE TABLE tblCategoria (
    idCategoria INT AUTO_INCREMENT PRIMARY KEY,
    nombreCategoria VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionCategoria DECIMAL(3,2) NOT NULL,
    estadoCategoria BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblLinea (
    idLinea INT AUTO_INCREMENT PRIMARY KEY,
    nombreLinea VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionLinea DECIMAL(3,2) NOT NULL,
    estadoLinea BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblMarca (
    idMarca INT AUTO_INCREMENT PRIMARY KEY,
    nombreMarca VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionMarca DECIMAL(3,2) NOT NULL,
    estadoMarca BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblProducto (
    idProducto INT AUTO_INCREMENT PRIMARY KEY,
    skuProducto VARCHAR(30) NOT NULL UNIQUE,
    clasificacionProducto VARCHAR(5) NOT NULL DEFAULT '?????',
    nombreProducto VARCHAR(100) NOT NULL,
    fkMarcaIdMarca INT NOT NULL,
    fkCategoriaIdCategoria INT NOT NULL,
    fkLineaIdLinea INT NOT NULL,
    descripcionProducto VARCHAR(255),
    imagenProducto LONGBLOB NULL,
    paisProducto VARCHAR(100) NOT NULL,
    precioCompraProducto DECIMAL(12,4) NOT NULL,
    precioVentaProducto DECIMAL(12,4) NOT NULL,
    porcentajeComisionProducto DECIMAL(3,2) NOT NULL,
    stockMinimoProducto INT NOT NULL,
    stockMaximoProducto INT NOT NULL,
    pesoProducto DECIMAL(4,2) NOT NULL,
    estadoProducto BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_Producto_Marca FOREIGN KEY (fkMarcaIdMarca) REFERENCES tblMarca(idMarca),
    CONSTRAINT Fk_Producto_Categoria FOREIGN KEY (fkCategoriaIdCategoria) REFERENCES tblCategoria(idCategoria),
    CONSTRAINT Fk_Producto_Linea FOREIGN KEY (fkLineaIdLinea) REFERENCES tblLinea(idLinea)
) ENGINE=InnoDB;

CREATE TABLE tblStock (
    fkSucursalIdSucursal INT NOT NULL,
    fkBodegaIdBodega INT NOT NULL,
    fkProductoIdProducto INT NOT NULL,
    stockFisicoStock INT NOT NULL DEFAULT 0,
    stockVirtualStock INT NOT NULL DEFAULT 0,
    estadoVentaStock BOOLEAN DEFAULT TRUE,
    estadoCompraStock BOOLEAN DEFAULT TRUE,
    estadoStock BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (fkSucursalIdSucursal, fkBodegaIdBodega, fkProductoIdProducto),
    CONSTRAINT Fk_Stock_SucursalBodega FOREIGN KEY (fkBodegaIdBodega, fkSucursalIdSucursal)
        REFERENCES tblBodega (idBodega, fkSucursalIdSucursal),
    CONSTRAINT Fk_Stock_Producto FOREIGN KEY (fkProductoIdProducto) REFERENCES tblProducto(idProducto)
) ENGINE=InnoDB;

CREATE TABLE tblTipoMovimiento (
    idTipoMovimiento INT AUTO_INCREMENT PRIMARY KEY,
    nombreTipoMovimiento VARCHAR(50) NOT NULL UNIQUE,
    descripcionTipoMovimiento VARCHAR(255),
    signoTipoMovimiento CHAR(1) NOT NULL,
    estadoTipoMovimiento BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblEstado (
    idEstado INT AUTO_INCREMENT NOT NULL,
    tipoEstado VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Estado PRIMARY KEY (idEstado),
    CONSTRAINT uqTipoEstado UNIQUE (tipoEstado)
) ENGINE=InnoDB;

CREATE TABLE tblTipoTransporte (
    idTipoTransporte INT AUTO_INCREMENT NOT NULL,
    nombreTipoTransporte VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TipoTransporte PRIMARY KEY (idTipoTransporte),
    CONSTRAINT uqNombreTipoTransporte UNIQUE (nombreTipoTransporte)
) ENGINE=InnoDB;

CREATE TABLE tblTransporte (
    idTransporte INT AUTO_INCREMENT PRIMARY KEY,
    placaVehiculoTransporte VARCHAR(30) NOT NULL,
    conductorTransporte VARCHAR(100) NOT NULL,
    empresaTransporteTransporte VARCHAR(100) NOT NULL,
    estadoTransporte BOOLEAN DEFAULT TRUE,
    nroPlacaTransporte VARCHAR(30) NOT NULL,
    marcaTransporte VARCHAR(100) NULL,
    modeloTransporte VARCHAR(100) NULL,
    anioTransporte SMALLINT NULL,
    esRefrigeradoTransporte BOOLEAN NOT NULL DEFAULT FALSE,
    idEstado INT NOT NULL,
    idTipoTransporte INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT uqNroPlacaTransporte UNIQUE (nroPlacaTransporte),
    INDEX idxIdEstadoTransporte (idEstado),
    INDEX idxIdTipoTransporteTransporte (idTipoTransporte),
    CONSTRAINT Fk_Transporte_Estado FOREIGN KEY (idEstado) REFERENCES tblEstado(idEstado),
    CONSTRAINT Fk_Transporte_TipoTransporte FOREIGN KEY (idTipoTransporte) REFERENCES tblTipoTransporte(idTipoTransporte)
) ENGINE=InnoDB;

CREATE TABLE tblMovimientoInventario (
    idMovimientoInventario INT AUTO_INCREMENT PRIMARY KEY,
    fkTipoMovimientoIdTipoMovimiento INT NOT NULL,
    fkBodegaOrigenIdBodega INT NULL,
    fkBodegaDestinoIdBodega INT NULL,
    fkTransporteIdTransporte INT NULL,
    fechaMovimientoInventario DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observacionesMovimientoInventario VARCHAR(500) NOT NULL,
    estadoMovimientoInventario BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_Movimiento_Tipo FOREIGN KEY (fkTipoMovimientoIdTipoMovimiento) REFERENCES tblTipoMovimiento(idTipoMovimiento),
    CONSTRAINT Fk_Movimiento_BodegaOrigen FOREIGN KEY (fkBodegaOrigenIdBodega) REFERENCES tblBodega(idBodega),
    CONSTRAINT Fk_Movimiento_BodegaDestino FOREIGN KEY (fkBodegaDestinoIdBodega) REFERENCES tblBodega(idBodega),
    CONSTRAINT Fk_Movimiento_Transporte FOREIGN KEY (fkTransporteIdTransporte) REFERENCES tblTransporte(idTransporte)
) ENGINE=InnoDB;

CREATE TABLE tblDetalleMovimiento (
    idDetalleMovimiento INT AUTO_INCREMENT PRIMARY KEY,
    fkMovimientoInventarioIdMovimientoInventario INT NOT NULL,
    loteMovimientoInventario VARCHAR(30),
    fkProductoIdProducto INT NOT NULL,
    cantidadDetalleMovimiento INT NOT NULL,
    precioCompraDetalleMovimiento DECIMAL(12,4) NOT NULL,
    estadoDetalleMovimiento BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_DetalleMovimiento_Movimiento FOREIGN KEY (fkMovimientoInventarioIdMovimientoInventario)
        REFERENCES tblMovimientoInventario(idMovimientoInventario),
    CONSTRAINT Fk_DetalleMovimiento_Producto FOREIGN KEY (fkProductoIdProducto)
        REFERENCES tblProducto(idProducto)
) ENGINE=InnoDB;

CREATE TABLE tblAuditoriaInventario (
    idAuditoriaInventario INT AUTO_INCREMENT PRIMARY KEY,
    fkMovimientoInventarioIdMovimientoInventario INT NOT NULL,
    fechaAuditoriaInventario DATETIME NOT NULL,
    resultadoAuditoriaInventario VARCHAR(50) NOT NULL,
    observacionesCalidadAuditoriaInventario TEXT NOT NULL,
    detallesAuditoriaInventario TEXT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_Auditoria_Movimiento FOREIGN KEY (fkMovimientoInventarioIdMovimientoInventario)
        REFERENCES tblMovimientoInventario(idMovimientoInventario)
) ENGINE=InnoDB;

CREATE TABLE tblCierreMovimiento (
    idCierreMovimiento INT AUTO_INCREMENT PRIMARY KEY,
    nombreCierreMovimiento VARCHAR(100) NOT NULL,
    fechaInicioCierreMovimiento DATE NOT NULL,
    fechaFinCierreMovimiento DATE NOT NULL,
    estadoCierreMovimiento VARCHAR(20) NOT NULL DEFAULT 'ABIERTO',
    esCierreAnualCierreMovimiento BOOLEAN NOT NULL DEFAULT FALSE,
    toleranciaCierreMovimiento INT NOT NULL DEFAULT 0,
    diferenciaUnidadesCierreMovimiento INT NOT NULL DEFAULT 0,
    diferenciaMontoCierreMovimiento DECIMAL(12,4) NOT NULL DEFAULT 0,
    motivoAceptacionCierreMovimiento VARCHAR(500) NULL,
    motivoAnulacionCierreMovimiento VARCHAR(500) NULL,
    observacionesCierreMovimiento VARCHAR(500) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tblDetalleCierre (
    idDetalleCierre INT AUTO_INCREMENT PRIMARY KEY,
    fkIdCierreMovimiento INT NOT NULL,
    fkSucursalIdSucursal INT NOT NULL,
    fkBodegaIdBodega INT NOT NULL,
    fkProductoIdProducto INT NOT NULL,
    CONSTRAINT Fk_DetalleCierre_Cierre FOREIGN KEY (fkIdCierreMovimiento)
        REFERENCES tblCierreMovimiento(idCierreMovimiento),
    CONSTRAINT Fk_DetalleCierre_SucursalBodega FOREIGN KEY (fkBodegaIdBodega, fkSucursalIdSucursal)
        REFERENCES tblBodega(idBodega, fkSucursalIdSucursal),
    CONSTRAINT Fk_DetalleCierre_Producto FOREIGN KEY (fkProductoIdProducto)
        REFERENCES tblProducto(idProducto),
    CONSTRAINT Fk_DetalleCierre_Stock FOREIGN KEY (fkSucursalIdSucursal, fkBodegaIdBodega, fkProductoIdProducto)
        REFERENCES tblStock(fkSucursalIdSucursal, fkBodegaIdBodega, fkProductoIdProducto),
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE tbltraslado (
    idTraslado INT NOT NULL AUTO_INCREMENT,
    codigoTraslado VARCHAR(30) NOT NULL,
    fkBodegaOrigenIdBodega INT NOT NULL,
    fkBodegaDestinoIdBodega INT NOT NULL,
    fkTransporteIdTransporte INT NULL,
    fechaSolicitudTraslado DATETIME NOT NULL,
    fechaSalidaTraslado DATETIME NULL,
    fechaLlegadaTraslado DATETIME NULL,
    estadoTraslado ENUM('SOLICITADO','EN_TRANSITO','RECIBIDO','CANCELADO') NOT NULL DEFAULT 'SOLICITADO',
    observacionesTraslado VARCHAR(500) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (idTraslado),
    UNIQUE KEY uqCodigoTraslado (codigoTraslado),
    CONSTRAINT Fk_TrasladoBodegaOrigen FOREIGN KEY (fkBodegaOrigenIdBodega) REFERENCES tblBodega(idBodega),
    CONSTRAINT Fk_TrasladoBodegaDestino FOREIGN KEY (fkBodegaDestinoIdBodega) REFERENCES tblBodega(idBodega),
    CONSTRAINT Fk_TrasladoTransporte FOREIGN KEY (fkTransporteIdTransporte) REFERENCES tblTransporte(idTransporte),
    CONSTRAINT ckTrasladoBodegasDistintas CHECK (fkBodegaOrigenIdBodega <> fkBodegaDestinoIdBodega)
) ENGINE=InnoDB;

CREATE TABLE tbldetalletraslado (
    fkTrasladoIdTraslado INT NOT NULL,
    fkProductoIdProducto INT NOT NULL,
    cantidadDetalleTraslado INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (fkTrasladoIdTraslado, fkProductoIdProducto),
    CONSTRAINT Fk_DetalleTrasladoTraslado FOREIGN KEY (fkTrasladoIdTraslado) REFERENCES tbltraslado(idTraslado),
    CONSTRAINT Fk_DetalleTrasladoProducto FOREIGN KEY (fkProductoIdProducto) REFERENCES tblProducto(idProducto),
    CONSTRAINT ckDetalleTrasladoCantidad CHECK (cantidadDetalleTraslado > 0)
) ENGINE=InnoDB;

CREATE TABLE tblCamaraRefrigeracion (
    idCamaraRefrigeracion INT AUTO_INCREMENT NOT NULL,
    idTransporte INT NOT NULL,
    marcaEquipoCamaraRefrigeracion VARCHAR(100) NOT NULL,
    tempMinimaCelsiusCamaraRefrigeracion DECIMAL(5,2) NOT NULL,
    tempMaximaCelsiusCamaraRefrigeracion DECIMAL(5,2) NOT NULL,
    estadoOperativoCamaraRefrigeracion VARCHAR(30) NOT NULL DEFAULT 'OPERATIVO',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_CamaraRefrigeracion PRIMARY KEY (idCamaraRefrigeracion),
    CONSTRAINT uqIdTransporteCamaraRefrigeracion UNIQUE (idTransporte),
    CONSTRAINT Fk_CamaraRefrigeracion_Transporte FOREIGN KEY (idTransporte) REFERENCES tblTransporte(idTransporte)
) ENGINE=InnoDB;

CREATE TABLE tblConductor (
    idConductor INT AUTO_INCREMENT NOT NULL,
    codigoConductor VARCHAR(30) NOT NULL,
    nombreCompletoConductor VARCHAR(100) NOT NULL,
    numeroLicenciaConductor VARCHAR(30) NOT NULL,
    fechaVencimientoDocConductor DATE NOT NULL,
    idEstado INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Conductor PRIMARY KEY (idConductor),
    CONSTRAINT uqCodigoConductor UNIQUE (codigoConductor),
    CONSTRAINT uqNumeroLicenciaConductor UNIQUE (numeroLicenciaConductor),
    INDEX idxIdEstadoConductor (idEstado),
    CONSTRAINT Fk_Conductor_Estado FOREIGN KEY (idEstado) REFERENCES tblEstado(idEstado)
) ENGINE=InnoDB;

CREATE TABLE tblInspeccionSanitaria (
    idInspeccionSanitaria BIGINT AUTO_INCREMENT NOT NULL,
    idTransporte INT NOT NULL,
    fechaUtcInspeccionSanitaria DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    sanitizadoCorrectoInspeccionSanitaria BOOLEAN NOT NULL,
    libreOloresContamInspeccionSanitaria BOOLEAN NOT NULL,
    aprobadoParaCargaInspeccionSanitaria BOOLEAN NOT NULL,
    observacionesInspeccionSanitaria VARCHAR(500) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_InspeccionSanitaria PRIMARY KEY (idInspeccionSanitaria),
    INDEX idxIdTransporteInspeccionSanitaria (idTransporte),
    CONSTRAINT Fk_InspeccionSanitaria_Transporte FOREIGN KEY (idTransporte) REFERENCES tblTransporte(idTransporte)
) ENGINE=InnoDB;

CREATE TABLE tblLoteProducto (
    idLoteProducto BIGINT AUTO_INCREMENT NOT NULL,
    codigoLoteProducto VARCHAR(30) NOT NULL,
    descripcionLoteProducto VARCHAR(255) NOT NULL,
    requiereFrioLoteProducto BOOLEAN NOT NULL DEFAULT TRUE,
    fechaFabricacionLoteProducto DATE NOT NULL,
    fechaVencimientoLoteProducto DATE NOT NULL,
    tempRequeridaMinLoteProducto DECIMAL(5,2) NULL,
    tempRequeridaMaxLoteProducto DECIMAL(5,2) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_LoteProducto PRIMARY KEY (idLoteProducto),
    CONSTRAINT uqCodigoLoteProducto UNIQUE (codigoLoteProducto)
) ENGINE=InnoDB;

CREATE TABLE tblEncabezadoViaje (
    correlativoEncabezadoViaje BIGINT AUTO_INCREMENT NOT NULL,
    fechaDocumentoEncabezadoViaje DATETIME NOT NULL,
    horaSalidaUtcEncabezadoViaje DATETIME NULL,
    horaLlegadaUtcEncabezadoViaje DATETIME NULL,
    requiereCadenaFrioEncabezadoViaje BOOLEAN NOT NULL DEFAULT TRUE,
    estadoViajeEncabezadoViaje VARCHAR(30) NOT NULL DEFAULT 'EN_CURSO',
    pesoTotalEncabezadoViaje DECIMAL(18,3) NULL,
    idTransporte INT NOT NULL,
    idConductor INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_EncabezadoViaje PRIMARY KEY (correlativoEncabezadoViaje),
    INDEX idxIdTransporteEncabezadoViaje (idTransporte),
    INDEX idxIdConductorEncabezadoViaje (idConductor),
    INDEX idxEstadoViajeEncabezadoViaje (estadoViajeEncabezadoViaje),
    CONSTRAINT Fk_EncabezadoViaje_Transporte FOREIGN KEY (idTransporte) REFERENCES tblTransporte(idTransporte),
    CONSTRAINT Fk_EncabezadoViaje_Conductor FOREIGN KEY (idConductor) REFERENCES tblConductor(idConductor)
) ENGINE=InnoDB;

CREATE TABLE tblDetalleViaje (
    idDetalleViaje BIGINT AUTO_INCREMENT NOT NULL,
    correlativoEncabezadoViaje BIGINT NOT NULL,
    ordenDetalleViaje INT NOT NULL,
    descripcionDetalleViaje VARCHAR(255) NOT NULL,
    cantidadDetalleViaje DECIMAL(18,3) NOT NULL,
    pesoDetalleViaje DECIMAL(18,3) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleViaje PRIMARY KEY (idDetalleViaje),
    CONSTRAINT uqOrdenDetalleViaje UNIQUE (correlativoEncabezadoViaje, ordenDetalleViaje),
    CONSTRAINT Fk_DetalleViaje_EncabezadoViaje FOREIGN KEY (correlativoEncabezadoViaje) REFERENCES tblEncabezadoViaje(correlativoEncabezadoViaje)
) ENGINE=InnoDB;

CREATE TABLE tblDetalleViajeLoteProducto (
    idDetalleViaje BIGINT NOT NULL,
    idLoteProducto BIGINT NOT NULL,
    cantidadCajasDetalleViajeLoteProducto INT NOT NULL,
    pesoKgDetalleViajeLoteProducto DECIMAL(18,3) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleViajeLoteProducto PRIMARY KEY (idDetalleViaje, idLoteProducto),
    INDEX idxIdLoteProductoDetalleViajeLoteProducto (idLoteProducto),
    CONSTRAINT Fk_DetalleViajeLoteProducto_DetalleViaje FOREIGN KEY (idDetalleViaje) REFERENCES tblDetalleViaje(idDetalleViaje),
    CONSTRAINT Fk_DetalleViajeLoteProducto_LoteProducto FOREIGN KEY (idLoteProducto) REFERENCES tblLoteProducto(idLoteProducto)
) ENGINE=InnoDB;

CREATE TABLE tblTelemetriaTemperatura (
    idTelemetriaTemperatura BIGINT AUTO_INCREMENT NOT NULL,
    correlativoEncabezadoViaje BIGINT NOT NULL,
    fechaHoraUtcTelemetriaTemperatura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    temperaturaCelsiusTelemetriaTemperatura DECIMAL(5,2) NOT NULL,
    /* Porcentaje en formato decimal: 0.6500 = 65 % (DECIMAL(5,4) permite llegar a 100 %) */
    humedadRelativaTelemetriaTemperatura DECIMAL(5,4) NULL,
    puertaAbiertaTelemetriaTemperatura BOOLEAN NOT NULL DEFAULT FALSE,
    ubicacionGpsTelemetriaTemperatura VARCHAR(100) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TelemetriaTemperatura PRIMARY KEY (idTelemetriaTemperatura),
    INDEX idxCorrelativoFechaTelemetriaTemperatura (correlativoEncabezadoViaje, fechaHoraUtcTelemetriaTemperatura),
    CONSTRAINT Fk_TelemetriaTemperatura_EncabezadoViaje FOREIGN KEY (correlativoEncabezadoViaje) REFERENCES tblEncabezadoViaje(correlativoEncabezadoViaje)
) ENGINE=InnoDB;

CREATE TABLE tblAlertaCadenaFrio (
    idAlertaCadenaFrio BIGINT AUTO_INCREMENT NOT NULL,
    correlativoEncabezadoViaje BIGINT NOT NULL,
    idTelemetriaTemperatura BIGINT NOT NULL,
    tipoAlertaCadenaFrio VARCHAR(30) NOT NULL,
    temperaturaRegistradaAlertaCadenaFrio DECIMAL(5,2) NOT NULL,
    atendidaAlertaCadenaFrio BOOLEAN NOT NULL DEFAULT FALSE,
    observacionCalidadAlertaCadenaFrio VARCHAR(500) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_AlertaCadenaFrio PRIMARY KEY (idAlertaCadenaFrio),
    INDEX idxCorrelativoEncabezadoViajeAlertaCadenaFrio (correlativoEncabezadoViaje),
    INDEX idxIdTelemetriaTemperaturaAlertaCadenaFrio (idTelemetriaTemperatura),
    INDEX idxAtendidaAlertaCadenaFrio (atendidaAlertaCadenaFrio),
    CONSTRAINT Fk_AlertaCadenaFrio_EncabezadoViaje FOREIGN KEY (correlativoEncabezadoViaje) REFERENCES tblEncabezadoViaje(correlativoEncabezadoViaje),
    CONSTRAINT Fk_AlertaCadenaFrio_TelemetriaTemperatura FOREIGN KEY (idTelemetriaTemperatura) REFERENCES tblTelemetriaTemperatura(idTelemetriaTemperatura)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblEmpresaTransportista (
 Pk_EmpresaTransportista INT AUTO_INCREMENT PRIMARY KEY,
 nombreEmpresaTransportista VARCHAR(100) NOT NULL,
 contactoEmpresaTransportista VARCHAR(100),
 telefonoEmpresaTransportista VARCHAR(30),
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqEmpresaTransportistaNombre UNIQUE (nombreEmpresaTransportista)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblTipoServicio (
 Pk_TipoServicio INT AUTO_INCREMENT PRIMARY KEY,
 nombreTipoServicio VARCHAR(100) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqTipoServicioNombre UNIQUE (nombreTipoServicio)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblTarifaTransporte (
 Pk_TarifaTransporte INT AUTO_INCREMENT PRIMARY KEY,
 nombreTarifaTransporte VARCHAR(100) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqTarifaTransporteNombre UNIQUE (nombreTarifaTransporte)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblServicioTransporteExterno (
 Pk_ServicioTransporteExterno INT AUTO_INCREMENT PRIMARY KEY,
 Fk_EmpresaTransportista_Codigo INT NOT NULL,
 Fk_TipoServicio_Codigo INT NOT NULL,
 Fk_Traslado_IdTraslado INT NULL,
 numeroGuiaServicioTransporteExterno VARCHAR(30) NULL,
 estadoServicioTransporteExterno VARCHAR(30) NOT NULL DEFAULT 'Programado',
 origenServicioTransporteExterno VARCHAR(100) NOT NULL,
 direccionOrigenServicioTransporteExterno VARCHAR(500),
 destinoServicioTransporteExterno VARCHAR(100) NOT NULL,
 direccionDestinoServicioTransporteExterno VARCHAR(500),
 fechaContratacionServicioTransporteExterno DATE NOT NULL,
 fechaEstimadaEntregaServicioTransporteExterno DATE NOT NULL,
 fechaEntregaRealServicioTransporteExterno DATE NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqServicioNumeroGuia UNIQUE (numeroGuiaServicioTransporteExterno),
 CONSTRAINT ckServicioEstado CHECK (estadoServicioTransporteExterno IN ('Programado','En tránsito','Entregado','Cancelado')),
 CONSTRAINT ckServicioFechas CHECK (fechaEstimadaEntregaServicioTransporteExterno >= fechaContratacionServicioTransporteExterno),
 CONSTRAINT Fk_ServicioEmpresa FOREIGN KEY (Fk_EmpresaTransportista_Codigo) REFERENCES tblEmpresaTransportista(Pk_EmpresaTransportista),
 CONSTRAINT Fk_ServicioTipo FOREIGN KEY (Fk_TipoServicio_Codigo) REFERENCES tblTipoServicio(Pk_TipoServicio),
 CONSTRAINT Fk_ServicioTraslado FOREIGN KEY (Fk_Traslado_IdTraslado) REFERENCES tbltraslado(idTraslado)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblCostoTransporte (
 Pk_CostoTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 Fk_TarifaTransporte_Codigo INT NOT NULL,
 cantidadCostoTransporte DECIMAL(12,4) NOT NULL,
 costoUnitarioTransporte DECIMAL(12,4) NOT NULL,
 costoTotalTransporte DECIMAL(12,4) NOT NULL,
 monedaCostoTransporte CHAR(3) NOT NULL,
 tipoPagoCostoTransporte VARCHAR(30) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqCostoServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT ckCostoCantidad CHECK (cantidadCostoTransporte > 0),
 CONSTRAINT ckCostoUnitario CHECK (costoUnitarioTransporte >= 0),
 CONSTRAINT ckCostoMoneda CHECK (monedaCostoTransporte IN ('GTQ','USD')),
 CONSTRAINT ckCostoTipoPago CHECK (tipoPagoCostoTransporte IN ('Transferencia','Efectivo')),
 CONSTRAINT Fk_CostoServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno),
 CONSTRAINT Fk_CostoTarifa FOREIGN KEY (Fk_TarifaTransporte_Codigo) REFERENCES tblTarifaTransporte(Pk_TarifaTransporte)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblVehiculoTransporte (
 Pk_VehiculoTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 placaVehiculoTransporte VARCHAR(30),
 tipoVehiculoTransporte VARCHAR(100),
 capacidadVehiculoTransporte VARCHAR(100),
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqVehiculoServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT Fk_VehiculoServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblConductorTransporte (
 Pk_ConductorTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 nombreConductorTransporte VARCHAR(100),
 telefonoConductorTransporte VARCHAR(30),
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqConductorServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT Fk_ConductorServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblProductoTransporte (
 Pk_ProductoTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 Fk_Producto_IdProducto INT NOT NULL,
 cantidadProductoTransporte DECIMAL(12,4) NOT NULL,
 unidadMedidaProductoTransporte VARCHAR(30),
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT ckProductoCantidad CHECK (cantidadProductoTransporte > 0),
 CONSTRAINT Fk_ProductoServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno),
 CONSTRAINT Fk_ProductoCatalogo FOREIGN KEY (Fk_Producto_IdProducto) REFERENCES tblproducto(idProducto)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblUbicacionTransporte (
 Pk_UbicacionTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 ubicacionActualTransporte VARCHAR(500) NOT NULL,
 fechaActualizacionUbicacionTransporte DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT Fk_UbicacionServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno),
 INDEX idxUbicacionServicioFecha (Fk_ServicioTransporteExterno_Codigo, fechaActualizacionUbicacionTransporte)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblIncidenciaTransporte (
 Pk_IncidenciaTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 codigoIncidenciaTransporte VARCHAR(30) NOT NULL,
 fechaIncidenciaTransporte DATETIME NOT NULL,
 tipoIncidenciaTransporte VARCHAR(100) NOT NULL,
 descripcionIncidenciaTransporte TEXT NOT NULL,
 responsableIncidenciaTransporte VARCHAR(100),
 estadoIncidenciaTransporte VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqIncidenciaCodigo UNIQUE (codigoIncidenciaTransporte),
 CONSTRAINT Fk_IncidenciaServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblHistorialTransporte (
 Pk_HistorialTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 fechaMovimientoHistorial DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
 tipoMovimientoHistorial VARCHAR(100) NOT NULL,
 descripcionMovimientoHistorial VARCHAR(255) NOT NULL,
 estadoTransporteHistorial VARCHAR(30),
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT Fk_HistorialServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno)
) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS tblSolicitudPagoTransporte (
 Pk_SolicitudPagoTransporte INT AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 codigoSolicitudPagoTransporte VARCHAR(30) NULL,
 estadoSolicitudPagoTransporte VARCHAR(30) NOT NULL DEFAULT 'Solicitado',
 referenciaBancariaSolicitudPagoTransporte VARCHAR(100) NULL,
 fechaPagoSolicitudPagoTransporte DATE NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqSolicitudServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT uqSolicitudCodigo UNIQUE (codigoSolicitudPagoTransporte),
 CONSTRAINT Fk_SolicitudServicio FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo)
  REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno)
) ENGINE=InnoDB;

-- =============================================================
-- VISTA Y PROCEDIMIENTOS DE TRANSPORTE EXTERNO
-- =============================================================
CREATE OR REPLACE VIEW vwSolicitudesPagoTransporte AS
SELECT
 s.codigoSolicitudPagoTransporte AS solicitud,
 t.numeroGuiaServicioTransporteExterno AS numeroGuia,
 e.nombreEmpresaTransportista AS beneficiario,
 CASE ts.nombreTipoServicio
  WHEN 'Refrigerado' THEN 'Servicio de transporte externo refrigerado'
  WHEN 'Congelado' THEN 'Servicio de transporte externo congelado'
  WHEN 'Carga seca' THEN 'Servicio de transporte externo de carga seca'
  WHEN 'Carga especial' THEN 'Servicio de transporte externo de carga especial'
  ELSE CONCAT('Servicio de transporte externo ', LOWER(ts.nombreTipoServicio))
 END AS concepto,
 c.costoTotalTransporte AS montoTotal,
 s.fechaPagoSolicitudPagoTransporte AS fechaPago,
 s.estadoSolicitudPagoTransporte AS estadoSolicitud,
 s.referenciaBancariaSolicitudPagoTransporte AS referenciaBancaria
FROM tblServicioTransporteExterno t
INNER JOIN tblEmpresaTransportista e
 ON e.Pk_EmpresaTransportista = t.Fk_EmpresaTransportista_Codigo
INNER JOIN tblTipoServicio ts
 ON ts.Pk_TipoServicio = t.Fk_TipoServicio_Codigo
INNER JOIN tblCostoTransporte c
 ON c.Fk_ServicioTransporteExterno_Codigo = t.Pk_ServicioTransporteExterno
INNER JOIN tblSolicitudPagoTransporte s
 ON s.Fk_ServicioTransporteExterno_Codigo = t.Pk_ServicioTransporteExterno;

DROP PROCEDURE IF EXISTS spGenerarSolicitudPagoTransporte;
DELIMITER $$
-- ============================================================
-- PROCEDIMIENTO: GENERAR SOLICITUD DE PAGO
-- ============================================================
CREATE PROCEDURE spGenerarSolicitudPagoTransporte(IN pIdServicio INT)
BEGIN
 DECLARE vId INT;
 DECLARE EXIT HANDLER FOR SQLEXCEPTION
 BEGIN
  ROLLBACK;
  RESIGNAL;
 END;
 START TRANSACTION;
 IF NOT EXISTS (
  SELECT 1 FROM tblCostoTransporte
  WHERE Fk_ServicioTransporteExterno_Codigo = pIdServicio
 ) THEN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='El servicio no existe o no tiene costo registrado';
 END IF;
 INSERT INTO tblSolicitudPagoTransporte (Fk_ServicioTransporteExterno_Codigo)
 VALUES (pIdServicio);
 SET vId = LAST_INSERT_ID();
 UPDATE tblSolicitudPagoTransporte
 SET codigoSolicitudPagoTransporte = CONCAT('SP-', LPAD(vId, 6, '0'))
 WHERE Pk_SolicitudPagoTransporte = vId;
 COMMIT;
 SELECT vId AS idSolicitud, CONCAT('SP-', LPAD(vId, 6, '0')) AS codigoSolicitudPago;
END$$
DELIMITER ;

DROP PROCEDURE IF EXISTS spRegistrarServicioTransporteExterno;
DELIMITER $$
-- ============================================================
-- PROCEDIMIENTO: REGISTRAR SERVICIO DE TRANSPORTE EXTERNO
-- ============================================================
CREATE PROCEDURE spRegistrarServicioTransporteExterno(
 IN pEmpresa INT, IN pTipoServicio INT,
 IN pOrigen VARCHAR(100), IN pDireccionOrigen VARCHAR(500),
 IN pDestino VARCHAR(100), IN pDireccionDestino VARCHAR(500),
 IN pFechaContratacion DATE, IN pFechaEstimada DATE, IN pFechaReal DATE,
 IN pTarifa INT, IN pCantidad DECIMAL(12,4), IN pCosto DECIMAL(12,4),
 IN pMoneda CHAR(3), IN pTipoPago VARCHAR(30)
)
BEGIN
 DECLARE vId INT;
 DECLARE vEmpresa VARCHAR(100);
 DECLARE vPrefijo VARCHAR(10);
 DECLARE vGuia VARCHAR(30);
 DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;
 START TRANSACTION;
 SELECT nombreEmpresaTransportista INTO vEmpresa
 FROM tblEmpresaTransportista WHERE Pk_EmpresaTransportista=pEmpresa;
 IF vEmpresa IS NULL THEN
  SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Empresa transportista no válida';
 END IF;
 SET vPrefijo=CASE UPPER(TRIM(vEmpresa))
  WHEN 'FORZA' THEN 'FZ'
  WHEN 'CARGO EXPRESO' THEN 'CE'
  WHEN 'GUATEX' THEN 'GX'
  WHEN 'DHL EXPRESS' THEN 'DHL'
  WHEN 'FEDEX' THEN 'FDX'
  WHEN 'UPS' THEN 'UPS'
  ELSE 'TR' END;
 INSERT INTO tblServicioTransporteExterno (
  Fk_EmpresaTransportista_Codigo,Fk_TipoServicio_Codigo,
  origenServicioTransporteExterno,direccionOrigenServicioTransporteExterno,
  destinoServicioTransporteExterno,direccionDestinoServicioTransporteExterno,
  fechaContratacionServicioTransporteExterno,fechaEstimadaEntregaServicioTransporteExterno,
  fechaEntregaRealServicioTransporteExterno
 ) VALUES (pEmpresa,pTipoServicio,pOrigen,pDireccionOrigen,
           pDestino,pDireccionDestino,pFechaContratacion,pFechaEstimada,pFechaReal);
 SET vId=LAST_INSERT_ID();
 SET vGuia=CONCAT(vPrefijo,'-',LPAD(vId,6,'0'));
 UPDATE tblServicioTransporteExterno SET numeroGuiaServicioTransporteExterno=vGuia
 WHERE Pk_ServicioTransporteExterno=vId;
 INSERT INTO tblCostoTransporte (
  Fk_ServicioTransporteExterno_Codigo,Fk_TarifaTransporte_Codigo,
  cantidadCostoTransporte,costoUnitarioTransporte,costoTotalTransporte,
  monedaCostoTransporte,tipoPagoCostoTransporte
 ) VALUES (vId,pTarifa,pCantidad,pCosto,ROUND(pCantidad*pCosto,4),pMoneda,pTipoPago);
 COMMIT;
 SELECT vId AS idServicio, vGuia AS numeroGuia;
END$$

DELIMITER ;

