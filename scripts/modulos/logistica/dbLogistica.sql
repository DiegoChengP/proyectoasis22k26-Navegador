-- REVISION V1.1
-- TERMINUS ERP | Logistica | Estandarizacion BD V1.1 | 24/10/2026
-- Script de CREACION para base vacia
CREATE DATABASE IF NOT EXISTS dbLogistica;
USE dbLogistica;


/* RESPONSABLE BRIAN DE LA CRUZ */ 
-- tblEmpresaTransportista
CREATE TABLE IF NOT EXISTS tblEmpresaTransportista (
Pk_EmpresaTransportista INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 nombreEmpresaTransportista VARCHAR(100) NOT NULL,
 contactoEmpresaTransportista VARCHAR(100) NOT NULL,
 telefonoEmpresaTransportista VARCHAR(30) COLLATE utf8mb4_unicode_ci NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqEmpresaTransportistaNombre UNIQUE (nombreEmpresaTransportista)
) ENGINE=InnoDB;

-- tblTipoServicio
CREATE TABLE IF NOT EXISTS tblTipoServicio (
Pk_TipoServicio INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 nombreTipoServicio VARCHAR(100) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqTipoServicioNombre UNIQUE (nombreTipoServicio)
) ENGINE=InnoDB;

-- tblTarifaTransporte
CREATE TABLE IF NOT EXISTS tblTarifaTransporte (
Pk_TarifaTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 nombreTarifaTransporte VARCHAR(100) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqTarifaTransporteNombre UNIQUE (nombreTarifaTransporte)
) ENGINE=InnoDB;

-- tblEstado
CREATE TABLE IF NOT EXISTS tblEstado (
Pk_Estado INT AUTO_INCREMENT NOT NULL,
    tipoEstado VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Estado PRIMARY KEY (Pk_Estado),
    CONSTRAINT uqTipoEstado UNIQUE (tipoEstado)
) ENGINE=InnoDB;

-- tblTipoTransporte
CREATE TABLE IF NOT EXISTS tblTipoTransporte (
Pk_TipoTransporte INT AUTO_INCREMENT NOT NULL,
    nombreTipoTransporte VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TipoTransporte PRIMARY KEY (Pk_TipoTransporte),
    CONSTRAINT uqNombreTipoTransporte UNIQUE (nombreTipoTransporte)
) ENGINE=InnoDB;

-- tblTransporte
CREATE TABLE IF NOT EXISTS tblTransporte (
Pk_Transporte INT AUTO_INCREMENT NOT NULL,
    nroPlacaTransporte VARCHAR(30) NOT NULL,
    marcaTransporte VARCHAR(100) NOT NULL,
    modeloTransporte VARCHAR(100) NOT NULL,
    anioTransporte SMALLINT NOT NULL,
    esRefrigeradoTransporte BOOLEAN NOT NULL DEFAULT FALSE,
    Fk_Estado_Codigo INT NOT NULL,
    Fk_TipoTransporte_Codigo INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Transporte PRIMARY KEY (Pk_Transporte),
    CONSTRAINT uqNroPlacaTransporte UNIQUE (nroPlacaTransporte),
    INDEX idxIdEstadoTransporte (Fk_Estado_Codigo),
    INDEX idxIdTipoTransporteTransporte (Fk_TipoTransporte_Codigo),
    CONSTRAINT Fk_Transporte_Estado FOREIGN KEY (Fk_Estado_Codigo) REFERENCES tblEstado(Pk_Estado) ON DELETE RESTRICT,
    CONSTRAINT Fk_Transporte_TipoTransporte FOREIGN KEY (Fk_TipoTransporte_Codigo) REFERENCES tblTipoTransporte(Pk_TipoTransporte) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblCamaraRefrigeracion
CREATE TABLE IF NOT EXISTS tblCamaraRefrigeracion (
Pk_CamaraRefrigeracion INT AUTO_INCREMENT NOT NULL,
    Fk_Transporte_Codigo INT NOT NULL,
    marcaEquipoCamaraRefrigeracion VARCHAR(100) NOT NULL,
    tempMinimaCelsiusCamaraRefrigeracion DECIMAL(5,2) NOT NULL,
    tempMaximaCelsiusCamaraRefrigeracion DECIMAL(5,2) NOT NULL,
    estadoOperativoCamaraRefrigeracion VARCHAR(30) NOT NULL DEFAULT 'OPERATIVO',
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_CamaraRefrigeracion PRIMARY KEY (Pk_CamaraRefrigeracion),
    CONSTRAINT uqIdTransporteCamaraRefrigeracion UNIQUE (Fk_Transporte_Codigo),
    CONSTRAINT Fk_CamaraRefrigeracion_Transporte FOREIGN KEY (Fk_Transporte_Codigo) REFERENCES tblTransporte(Pk_Transporte) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblConductor
CREATE TABLE IF NOT EXISTS tblConductor (
Pk_Conductor INT AUTO_INCREMENT NOT NULL,
    codigoConductor VARCHAR(30) NOT NULL,
    nombreCompletoConductor VARCHAR(100) NOT NULL,
    numeroLicenciaConductor VARCHAR(30) NOT NULL,
    fechaVencimientoDocConductor DATE NOT NULL,
    Fk_Estado_Codigo INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Conductor PRIMARY KEY (Pk_Conductor),
    CONSTRAINT uqCodigoConductor UNIQUE (codigoConductor),
    CONSTRAINT uqNumeroLicenciaConductor UNIQUE (numeroLicenciaConductor),
    INDEX idxIdEstadoConductor (Fk_Estado_Codigo),
    CONSTRAINT Fk_Conductor_Estado FOREIGN KEY (Fk_Estado_Codigo) REFERENCES tblEstado(Pk_Estado) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblInspeccionSanitaria
CREATE TABLE IF NOT EXISTS tblInspeccionSanitaria (
Pk_InspeccionSanitaria BIGINT AUTO_INCREMENT NOT NULL,
    Fk_Transporte_Codigo INT NOT NULL,
    fechaInspeccionSanitaria DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    sanitizadoCorrectoInspeccionSanitaria BOOLEAN NOT NULL,
    libreOloresContamInspeccionSanitaria BOOLEAN NOT NULL,
    aprobadoParaCargaInspeccionSanitaria BOOLEAN NOT NULL,
    observacionesInspeccionSanitaria VARCHAR(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_InspeccionSanitaria PRIMARY KEY (Pk_InspeccionSanitaria),
    INDEX idxIdTransporteInspeccionSanitaria (Fk_Transporte_Codigo),
    CONSTRAINT Fk_InspeccionSanitaria_Transporte FOREIGN KEY (Fk_Transporte_Codigo) REFERENCES tblTransporte(Pk_Transporte) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblLoteProducto
CREATE TABLE IF NOT EXISTS tblLoteProducto (
Pk_LoteProducto BIGINT AUTO_INCREMENT NOT NULL,
    codigoLoteProducto VARCHAR(30) NOT NULL,
    descripcionLoteProducto VARCHAR(255) NOT NULL,
    requiereFrioLoteProducto BOOLEAN NOT NULL DEFAULT TRUE,
    fechaFabricacionLoteProducto DATE NOT NULL,
    fechaVencimientoLoteProducto DATE NOT NULL,
    tempRequeridaMinLoteProducto DECIMAL(5,2) NOT NULL,
    tempRequeridaMaxLoteProducto DECIMAL(5,2) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_LoteProducto PRIMARY KEY (Pk_LoteProducto),
    CONSTRAINT uqCodigoLoteProducto UNIQUE (codigoLoteProducto)
) ENGINE=InnoDB;

-- tblEncabezadoViaje
CREATE TABLE IF NOT EXISTS tblEncabezadoViaje (
Pk_EncabezadoViaje BIGINT AUTO_INCREMENT NOT NULL,
    fechaDocumentoEncabezadoViaje DATETIME NOT NULL,
    horaSalidaEncabezadoViaje DATETIME NOT NULL,
    horaLlegadaEncabezadoViaje DATETIME NULL,
    requiereCadenaFrioEncabezadoViaje BOOLEAN NOT NULL DEFAULT TRUE,
    estadoViajeEncabezadoViaje VARCHAR(30) NOT NULL DEFAULT 'EN_CURSO',
    pesoTotalEncabezadoViaje DECIMAL(18,3) NOT NULL,
    Fk_Transporte_Codigo INT NOT NULL,
    Fk_Conductor_Codigo INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_EncabezadoViaje PRIMARY KEY (Pk_EncabezadoViaje),
    INDEX idxIdTransporteEncabezadoViaje (Fk_Transporte_Codigo),
    INDEX idxIdConductorEncabezadoViaje (Fk_Conductor_Codigo),
    INDEX idxEstadoViajeEncabezadoViaje (estadoViajeEncabezadoViaje),
    CONSTRAINT Fk_EncabezadoViaje_Transporte FOREIGN KEY (Fk_Transporte_Codigo) REFERENCES tblTransporte(Pk_Transporte) ON DELETE RESTRICT,
    CONSTRAINT Fk_EncabezadoViaje_Conductor FOREIGN KEY (Fk_Conductor_Codigo) REFERENCES tblConductor(Pk_Conductor) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblDetalleViaje
CREATE TABLE IF NOT EXISTS tblDetalleViaje (
Pk_DetalleViaje BIGINT AUTO_INCREMENT NOT NULL,
    Fk_EncabezadoViaje_Codigo BIGINT NOT NULL,
    ordenDetalleViaje INT NOT NULL,
    descripcionDetalleViaje VARCHAR(255) NOT NULL,
    cantidadDetalleViaje DECIMAL(18,3) NOT NULL,
    pesoDetalleViaje DECIMAL(18,3) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleViaje PRIMARY KEY (Pk_DetalleViaje),
    CONSTRAINT uqOrdenDetalleViaje UNIQUE (Fk_EncabezadoViaje_Codigo, ordenDetalleViaje),
    CONSTRAINT Fk_DetalleViaje_EncabezadoViaje FOREIGN KEY (Fk_EncabezadoViaje_Codigo) REFERENCES tblEncabezadoViaje(Pk_EncabezadoViaje) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblDetalleViajeLoteProducto
CREATE TABLE IF NOT EXISTS tblDetalleViajeLoteProducto (
Fk_DetalleViaje_Codigo BIGINT NOT NULL,
    Fk_LoteProducto_Codigo BIGINT NOT NULL,
    cantidadCajasDetalleViajeLoteProducto INT NOT NULL,
    pesoKgDetalleViajeLoteProducto DECIMAL(18,3) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleViajeLoteProducto PRIMARY KEY (Fk_DetalleViaje_Codigo, Fk_LoteProducto_Codigo),
    INDEX idxIdLoteProductoDetalleViajeLoteProducto (Fk_LoteProducto_Codigo),
    CONSTRAINT Fk_DetalleViajeLoteProducto_DetalleViaje FOREIGN KEY (Fk_DetalleViaje_Codigo) REFERENCES tblDetalleViaje(Pk_DetalleViaje) ON DELETE CASCADE,
    CONSTRAINT Fk_DetalleViajeLoteProducto_LoteProducto FOREIGN KEY (Fk_LoteProducto_Codigo) REFERENCES tblLoteProducto(Pk_LoteProducto) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblTelemetriaTemperatura
CREATE TABLE IF NOT EXISTS tblTelemetriaTemperatura (
Pk_TelemetriaTemperatura BIGINT AUTO_INCREMENT NOT NULL,
    Fk_EncabezadoViaje_Codigo BIGINT NOT NULL,
    fechaHoraTelemetriaTemperatura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    temperaturaCelsiusTelemetriaTemperatura DECIMAL(5,2) NOT NULL,
    /* Porcentaje en formato decimal: 0.6500 = 65 % (DECIMAL(5,4) permite llegar a 100 %) */
    humedadRelativaTelemetriaTemperatura DECIMAL(5,4) NOT NULL,
    puertaAbiertaTelemetriaTemperatura BOOLEAN NOT NULL DEFAULT FALSE,
    ubicacionGpsTelemetriaTemperatura VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TelemetriaTemperatura PRIMARY KEY (Pk_TelemetriaTemperatura),
    INDEX idxCorrelativoFechaTelemetriaTemperatura (Fk_EncabezadoViaje_Codigo, fechaHoraTelemetriaTemperatura),
    CONSTRAINT Fk_TelemetriaTemperatura_EncabezadoViaje FOREIGN KEY (Fk_EncabezadoViaje_Codigo) REFERENCES tblEncabezadoViaje(Pk_EncabezadoViaje) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblAlertaCadenaFrio
CREATE TABLE IF NOT EXISTS tblAlertaCadenaFrio (
Pk_AlertaCadenaFrio BIGINT AUTO_INCREMENT NOT NULL,
    Fk_EncabezadoViaje_Codigo BIGINT NOT NULL,
    Fk_TelemetriaTemperatura_Codigo BIGINT NOT NULL,
    tipoAlertaCadenaFrio VARCHAR(30) NOT NULL,
    temperaturaRegistradaAlertaCadenaFrio DECIMAL(5,2) NOT NULL,
    atendidaAlertaCadenaFrio BOOLEAN NOT NULL DEFAULT FALSE,
    observacionCalidadAlertaCadenaFrio VARCHAR(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_AlertaCadenaFrio PRIMARY KEY (Pk_AlertaCadenaFrio),
    INDEX idxCorrelativoEncabezadoViajeAlertaCadenaFrio (Fk_EncabezadoViaje_Codigo),
    INDEX idxIdTelemetriaTemperaturaAlertaCadenaFrio (Fk_TelemetriaTemperatura_Codigo),
    INDEX idxAtendidaAlertaCadenaFrio (atendidaAlertaCadenaFrio),
    CONSTRAINT Fk_AlertaCadenaFrio_EncabezadoViaje FOREIGN KEY (Fk_EncabezadoViaje_Codigo) REFERENCES tblEncabezadoViaje(Pk_EncabezadoViaje) ON DELETE CASCADE,
    CONSTRAINT Fk_AlertaCadenaFrio_TelemetriaTemperatura FOREIGN KEY (Fk_TelemetriaTemperatura_Codigo) REFERENCES tblTelemetriaTemperatura(Pk_TelemetriaTemperatura) ON DELETE CASCADE
) ENGINE=InnoDB;


/* RESPONSABLE VELVETH SARAI CHAVEZ MEJIA*/ 
-- tblSucursal
CREATE TABLE IF NOT EXISTS tblSucursal (
Pk_Sucursal INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreSucursal VARCHAR(100) NOT NULL,
    descripcionSucursal VARCHAR(255) NOT NULL,
    ubicacionSucursal VARCHAR(500) NOT NULL,
    paisSucursal VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- tblBodega
CREATE TABLE IF NOT EXISTS tblBodega (
Pk_Bodega INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreBodega VARCHAR(100) NOT NULL,
    Fk_Sucursal_Codigo INT NOT NULL,
    paisBodega VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
	
    UNIQUE KEY uqBodegaSucursal (Pk_Bodega,Fk_Sucursal_Codigo),
    CONSTRAINT Fk_Bodega_Sucursal FOREIGN KEY (Fk_Sucursal_Codigo) REFERENCES tblSucursal(Pk_Sucursal) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblCategoria
CREATE TABLE IF NOT EXISTS tblCategoria (
Pk_Categoria INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreCategoria VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionCategoria DECIMAL(3,2) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT ckporcentajeComisionCategoriaRango CHECK (porcentajeComisionCategoria >= 0.00 AND porcentajeComisionCategoria <= 0.99)
) ENGINE=InnoDB;

-- tblLinea
CREATE TABLE IF NOT EXISTS tblLinea (
Pk_Linea INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreLinea VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionLinea DECIMAL(3,2) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT ckporcentajeComisionLineaRango CHECK (porcentajeComisionLinea >= 0.00 AND porcentajeComisionLinea <= 0.99)
) ENGINE=InnoDB;

-- tblMarca
CREATE TABLE IF NOT EXISTS tblMarca (
Pk_Marca INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreMarca VARCHAR(100) NOT NULL UNIQUE,
    porcentajeComisionMarca DECIMAL(3,2) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT ckporcentajeComisionMarcaRango CHECK (porcentajeComisionMarca >= 0.00 AND porcentajeComisionMarca <= 0.99)
) ENGINE=InnoDB;

-- tblProducto
CREATE TABLE IF NOT EXISTS tblProducto (
Pk_Producto INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    skuProducto VARCHAR(30) NOT NULL UNIQUE,
    clasificacionProducto CHAR(5) NOT NULL DEFAULT '?????',
    nombreProducto VARCHAR(100) NOT NULL,
    Fk_Marca_Codigo INT NOT NULL,
    Fk_Categoria_Codigo INT NOT NULL,
    Fk_Linea_Codigo INT NOT NULL,
    descripcionProducto VARCHAR(255) NOT NULL,
    imagenProducto LONGBLOB NOT NULL, -- Campo BLOB para almacenar la imagen (Método de conversión de Img a binario en aplicación)
    paisProducto VARCHAR(100) NOT NULL,
    precioCompraProducto DECIMAL(12,4) NOT NULL,
    precioVentaProducto DECIMAL(12,4) NOT NULL,
    
    porcentajeComisionProducto DECIMAL(3,2) NOT NULL,
    stockMinimoProducto INT NOT NULL ,
    stockMaximoProducto INT NOT NULL ,
    pesoProducto DECIMAL(4,2) NOT NULL,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT Fk_Producto_Marca FOREIGN KEY (Fk_Marca_Codigo) REFERENCES tblMarca(Pk_Marca) ON DELETE RESTRICT,
    CONSTRAINT Fk_Producto_Categoria FOREIGN KEY (Fk_Categoria_Codigo) REFERENCES tblCategoria(Pk_Categoria) ON DELETE RESTRICT,
    CONSTRAINT Fk_Producto_Linea FOREIGN KEY (Fk_Linea_Codigo) REFERENCES tblLinea(Pk_Linea) ON DELETE RESTRICT,
    CONSTRAINT ckporcentajeComisionProductoRango CHECK (porcentajeComisionProducto >= 0.00 AND porcentajeComisionProducto <= 0.99)
) ENGINE=InnoDB;

-- tblStock
CREATE TABLE IF NOT EXISTS tblStock (
Pk_Stock INT AUTO_INCREMENT NOT NULL,
Fk_Sucursal_Codigo INT NOT NULL,
    Fk_Bodega_Codigo INT NOT NULL,
    Fk_Producto_Codigo INT NOT NULL,
    
    stockFisicoStock INT NOT NULL DEFAULT 0,
    stockVirtualStock INT NOT NULL DEFAULT 0,

    estadoVentaStock BOOLEAN NOT NULL,
    estadoCompraStock BOOLEAN NOT NULL,

    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT Pk_Stock PRIMARY KEY (Pk_Stock),
    CONSTRAINT uqStockSucursalBodegaProducto UNIQUE (Fk_Sucursal_Codigo, Fk_Bodega_Codigo, Fk_Producto_Codigo),

    CONSTRAINT Fk_Stock_Bodega FOREIGN KEY (Fk_Bodega_Codigo, Fk_Sucursal_Codigo)
    REFERENCES tblBodega(Pk_Bodega, Fk_Sucursal_Codigo) ON DELETE RESTRICT,

    CONSTRAINT Fk_Stock_Producto FOREIGN KEY (Fk_Producto_Codigo)
    REFERENCES tblProducto(Pk_Producto) ON DELETE RESTRICT,
    CONSTRAINT ckEstadoVentaStock CHECK (estadoVentaStock IN (0, 1)),
    CONSTRAINT ckEstadoCompraStock CHECK (estadoCompraStock IN (0, 1))
) ENGINE=InnoDB;

-- tblTipoMovimiento
CREATE TABLE IF NOT EXISTS tblTipoMovimiento (
Pk_TipoMovimiento INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreTipoMovimiento VARCHAR(100) NOT NULL UNIQUE,
    descripcionTipoMovimiento VARCHAR(255) NOT NULL,
	signoTipoMovimiento CHAR(1) NOT NULL,

	created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT ckSignoTipoMovimiento CHECK (signoTipoMovimiento IN ('+', '-'))
) ENGINE=InnoDB;

-- tblMovimientoInventario
CREATE TABLE IF NOT EXISTS tblMovimientoInventario (
Pk_MovimientoInventario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Fk_TipoMovimiento_Codigo INT NOT NULL,

    Fk_Bodega_Origen_Codigo INT NULL,
    Fk_Bodega_Destino_Codigo INT NULL,
    Fk_Transporte_Codigo INT NULL,
    fechaMovimientoInventario DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    observacionesMovimientoInventario VARCHAR(500) NOT NULL,
    
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT Fk_MovimientoInventario_TipoMovimiento FOREIGN KEY (Fk_TipoMovimiento_Codigo)
	REFERENCES tblTipoMovimiento(Pk_TipoMovimiento) ON DELETE RESTRICT,

    CONSTRAINT Fk_MovimientoInventario_Bodega_Origen FOREIGN KEY (Fk_Bodega_Origen_Codigo)
    REFERENCES tblBodega(Pk_Bodega) ON DELETE RESTRICT,

    CONSTRAINT Fk_MovimientoInventario_Bodega_Destino FOREIGN KEY (Fk_Bodega_Destino_Codigo)
    REFERENCES tblBodega(Pk_Bodega) ON DELETE RESTRICT,

    CONSTRAINT Fk_MovimientoInventario_Transporte FOREIGN KEY (Fk_Transporte_Codigo)
    REFERENCES tblTransporte(Pk_Transporte) ON DELETE RESTRICT
) ENGINE=InnoDB;

-- tblDetalleMovimiento
CREATE TABLE IF NOT EXISTS tblDetalleMovimiento (
Pk_DetalleMovimiento INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Fk_MovimientoInventario_Codigo INT NOT NULL,
	loteMovimientoInventario VARCHAR(30) NOT NULL,
    Fk_Producto_Codigo INT NOT NULL,
    cantidadDetalleMovimiento INT NOT NULL,
    precioCompraDetalleMovimiento DECIMAL(12,4) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
        ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT Fk_DetalleMovimiento_MovimientoInventario FOREIGN KEY (Fk_MovimientoInventario_Codigo)
    REFERENCES tblMovimientoInventario(Pk_MovimientoInventario) ON DELETE CASCADE,
    CONSTRAINT Fk_DetalleMovimiento_Producto FOREIGN KEY (Fk_Producto_Codigo) REFERENCES tblProducto(Pk_Producto) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* RESPONSABLE JOSE XAVIER BOLAÑOS TENAS */ 
-- tblTraslado
CREATE TABLE IF NOT EXISTS tblTraslado (
Pk_Traslado INT NOT NULL AUTO_INCREMENT,
    codigoTraslado VARCHAR(30) NOT NULL,
    Fk_Bodega_Origen_Codigo INT NOT NULL,
    Fk_Bodega_Destino_Codigo INT NOT NULL,
    Fk_Transporte_Codigo INT NULL,
    fechaSolicitudTraslado DATETIME NOT NULL,
    fechaSalidaTraslado DATETIME NULL,
    fechaLlegadaTraslado DATETIME NULL,
    estadoTraslado ENUM('SOLICITADO','EN_TRANSITO','RECIBIDO','CANCELADO') NOT NULL DEFAULT 'SOLICITADO',
    observacionesTraslado VARCHAR(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (Pk_Traslado),
    UNIQUE KEY uqCodigoTraslado (codigoTraslado),
    CONSTRAINT Fk_Traslado_Bodega_Origen FOREIGN KEY (Fk_Bodega_Origen_Codigo) REFERENCES tblBodega(Pk_Bodega) ON DELETE RESTRICT,
    CONSTRAINT Fk_Traslado_Bodega_Destino FOREIGN KEY (Fk_Bodega_Destino_Codigo) REFERENCES tblBodega(Pk_Bodega) ON DELETE RESTRICT,
    CONSTRAINT Fk_Traslado_Transporte FOREIGN KEY (Fk_Transporte_Codigo) REFERENCES tblTransporte(Pk_Transporte) ON DELETE RESTRICT,
    CONSTRAINT ckTrasladoBodegasDistintas CHECK (Fk_Bodega_Origen_Codigo <> Fk_Bodega_Destino_Codigo) -- Origen y destino no pueden ser la misma bodega
) ENGINE=InnoDB;

-- tblDetalleTraslado
CREATE TABLE IF NOT EXISTS tblDetalleTraslado (
Fk_Traslado_Codigo INT NOT NULL,
    Fk_Producto_Codigo INT NOT NULL,
    cantidadDetalleTraslado INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    PRIMARY KEY (Fk_Traslado_Codigo, Fk_Producto_Codigo),
    CONSTRAINT Fk_DetalleTraslado_Traslado FOREIGN KEY (Fk_Traslado_Codigo) REFERENCES tblTraslado(Pk_Traslado) ON DELETE CASCADE,
    CONSTRAINT Fk_DetalleTraslado_Producto FOREIGN KEY (Fk_Producto_Codigo) REFERENCES tblProducto(Pk_Producto) ON DELETE RESTRICT,
    CONSTRAINT ckDetalleTrasladoCantidad CHECK (cantidadDetalleTraslado > 0)
) ENGINE=InnoDB;

/* RESPONSABLE CARLOS EDUARDO CHIN CAAL */ 
-- tblAuditoriaInventario
CREATE TABLE IF NOT EXISTS tblAuditoriaInventario (
Pk_AuditoriaInventario INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Fk_MovimientoInventario_Codigo INT NOT NULL,
    fechaAuditoriaInventario DATETIME NOT NULL,
    resultadoAuditoriaInventario VARCHAR(100) NOT NULL,
    observacionesCalidadAuditoriaInventario TEXT NOT NULL,
    detallesAuditoriaInventario TEXT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_AuditoriaInventario_MovimientoInventario FOREIGN KEY (Fk_MovimientoInventario_Codigo) REFERENCES tblMovimientoInventario(Pk_MovimientoInventario) ON DELETE RESTRICT
) ENGINE=InnoDB;

/* RESPONSABLE NILLS BERDUCIDO GOMEZ */ 

-- tblCierreMovimiento
CREATE TABLE IF NOT EXISTS tblCierreMovimiento (
Pk_CierreMovimiento INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    nombreCierreMovimiento VARCHAR(100) NOT NULL,
    fechaInicioCierreMovimiento DATE NOT NULL,
    fechaFinCierreMovimiento DATE NOT NULL,
    estadoCierreMovimiento VARCHAR(30) NOT NULL DEFAULT 'ABIERTO',
    esCierreAnualCierreMovimiento BOOLEAN NOT NULL DEFAULT FALSE,
    toleranciaCierreMovimiento INT NOT NULL DEFAULT 0,
    diferenciaUnidadesCierreMovimiento INT NOT NULL DEFAULT 0,
    diferenciaMontoCierreMovimiento DECIMAL(12,4) NOT NULL DEFAULT 0,
    motivoAceptacionCierreMovimiento VARCHAR(500) NOT NULL,
    motivoAnulacionCierreMovimiento VARCHAR(500) NOT NULL,
    observacionesCierreMovimiento VARCHAR(500) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB;

-- tblDetalleCierre
CREATE TABLE IF NOT EXISTS tblDetalleCierre (
Pk_DetalleCierre INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Fk_CierreMovimiento_Codigo INT NOT NULL,
    Fk_Stock_Codigo INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_DetalleCierre_CierreMovimiento FOREIGN KEY (Fk_CierreMovimiento_Codigo) REFERENCES tblCierreMovimiento(Pk_CierreMovimiento) ON DELETE CASCADE,
    CONSTRAINT Fk_DetalleCierre_Stock FOREIGN KEY (Fk_Stock_Codigo) REFERENCES tblStock(Pk_Stock) ON DELETE RESTRICT
) ENGINE=InnoDB;


/* RESPONSABLE GABRIELA PINTO GARCIA  */ 
-- tblServicioTransporteExterno
CREATE TABLE IF NOT EXISTS tblServicioTransporteExterno (
Pk_ServicioTransporteExterno INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_EmpresaTransportista_Codigo INT NOT NULL,
 Fk_TipoServicio_Codigo INT NOT NULL,
 Fk_Traslado_Codigo INT NOT NULL,
 numeroGuiaServicioTransporteExterno VARCHAR(30) NOT NULL,
 estadoServicioTransporteExterno VARCHAR(30) NOT NULL DEFAULT 'Programado',
 origenServicioTransporteExterno VARCHAR(100) NOT NULL,
 direccionOrigenServicioTransporteExterno VARCHAR(500) NOT NULL,
 destinoServicioTransporteExterno VARCHAR(100) NOT NULL,
 direccionDestinoServicioTransporteExterno VARCHAR(500) NOT NULL,
 fechaContratacionServicioTransporteExterno DATE NOT NULL,
 fechaEstimadaEntregaServicioTransporteExterno DATE NOT NULL,
 fechaEntregaRealServicioTransporteExterno DATE NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqServicioNumeroGuia UNIQUE (numeroGuiaServicioTransporteExterno),
 CONSTRAINT Fk_ServicioTransporteExterno_EmpresaTransportista FOREIGN KEY (Fk_EmpresaTransportista_Codigo) REFERENCES tblEmpresaTransportista(Pk_EmpresaTransportista) ON DELETE RESTRICT,
 CONSTRAINT Fk_ServicioTransporteExterno_TipoServicio FOREIGN KEY (Fk_TipoServicio_Codigo) REFERENCES tblTipoServicio(Pk_TipoServicio) ON DELETE RESTRICT,
 CONSTRAINT Fk_ServicioTransporteExterno_Traslado FOREIGN KEY (Fk_Traslado_Codigo) REFERENCES tblTraslado(Pk_Traslado) ON DELETE RESTRICT,
    CONSTRAINT ckServicioEstado CHECK (estadoServicioTransporteExterno IN ('Programado','En tránsito','Entregado','Cancelado')),
    CONSTRAINT ckServicioFechas CHECK (fechaEstimadaEntregaServicioTransporteExterno >= fechaContratacionServicioTransporteExterno)
) ENGINE=InnoDB;

-- tblCostoTransporte
CREATE TABLE IF NOT EXISTS tblCostoTransporte (
Pk_CostoTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
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
 CONSTRAINT Fk_CostoTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE,
 CONSTRAINT Fk_CostoTransporte_TarifaTransporte FOREIGN KEY (Fk_TarifaTransporte_Codigo) REFERENCES tblTarifaTransporte(Pk_TarifaTransporte) ON DELETE RESTRICT,
    CONSTRAINT ckCostoCantidad CHECK (cantidadCostoTransporte > 0),
    CONSTRAINT ckCostoUnitario CHECK (costoUnitarioTransporte >= 0),
    CONSTRAINT ckCostoMoneda CHECK (monedaCostoTransporte IN ('GTQ','USD')),
    CONSTRAINT ckCostoTipoPago CHECK (tipoPagoCostoTransporte IN ('Transferencia','Efectivo'))
) ENGINE=InnoDB;

-- tblVehiculoTransporte
CREATE TABLE IF NOT EXISTS tblVehiculoTransporte (
Pk_VehiculoTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 placaVehiculoTransporte VARCHAR(30) NOT NULL,
 tipoVehiculoTransporte VARCHAR(100) NOT NULL,
 capacidadVehiculoTransporte VARCHAR(100) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqVehiculoServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT Fk_VehiculoTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblConductorTransporte
CREATE TABLE IF NOT EXISTS tblConductorTransporte (
Pk_ConductorTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 nombreConductorTransporte VARCHAR(100) NOT NULL,
 telefonoConductorTransporte VARCHAR(30) COLLATE utf8mb4_unicode_ci NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqConductorServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT Fk_ConductorTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblProductoTransporte
CREATE TABLE IF NOT EXISTS tblProductoTransporte (
Pk_ProductoTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 Fk_Producto_Codigo INT NOT NULL,
 cantidadProductoTransporte DECIMAL(12,4) NOT NULL,
 unidadMedidaProductoTransporte VARCHAR(30) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT Fk_ProductoTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE,
 CONSTRAINT Fk_ProductoTransporte_Producto FOREIGN KEY (Fk_Producto_Codigo) REFERENCES tblProducto(Pk_Producto) ON DELETE RESTRICT,
    CONSTRAINT ckProductoCantidad CHECK (cantidadProductoTransporte > 0)
) ENGINE=InnoDB;

-- tblUbicacionTransporte
CREATE TABLE IF NOT EXISTS tblUbicacionTransporte (
Pk_UbicacionTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 ubicacionActualTransporte VARCHAR(500) NOT NULL,
 fechaActualizacionUbicacionTransporte DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT Fk_UbicacionTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE,
 INDEX idxUbicacionServicioFecha (Fk_ServicioTransporteExterno_Codigo, fechaActualizacionUbicacionTransporte)
) ENGINE=InnoDB;

-- tblIncidenciaTransporte
CREATE TABLE IF NOT EXISTS tblIncidenciaTransporte (
Pk_IncidenciaTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 codigoIncidenciaTransporte VARCHAR(30) NOT NULL,
 fechaIncidenciaTransporte DATETIME NOT NULL,
 tipoIncidenciaTransporte VARCHAR(100) NOT NULL,
 descripcionIncidenciaTransporte TEXT NOT NULL,
 responsableIncidenciaTransporte VARCHAR(100) NOT NULL,
 estadoIncidenciaTransporte VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqIncidenciaCodigo UNIQUE (codigoIncidenciaTransporte),
 CONSTRAINT Fk_IncidenciaTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblHistorialTransporte
CREATE TABLE IF NOT EXISTS tblHistorialTransporte (
Pk_HistorialTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 fechaMovimientoHistorial DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
 tipoMovimientoHistorial VARCHAR(100) NOT NULL,
 descripcionMovimientoHistorial VARCHAR(255) NOT NULL,
 estadoTransporteHistorial VARCHAR(30) NOT NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT Fk_HistorialTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo) REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE
) ENGINE=InnoDB;

-- tblSolicitudPagoTransporte
CREATE TABLE IF NOT EXISTS tblSolicitudPagoTransporte (
Pk_SolicitudPagoTransporte INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
 Fk_ServicioTransporteExterno_Codigo INT NOT NULL,
 codigoSolicitudPagoTransporte VARCHAR(30) NOT NULL,
 estadoSolicitudPagoTransporte VARCHAR(30) NOT NULL DEFAULT 'Solicitado',
 referenciaBancariaSolicitudPagoTransporte VARCHAR(100) NULL,
 fechaPagoSolicitudPagoTransporte DATE NULL,
 created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
 updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
 CONSTRAINT uqSolicitudServicio UNIQUE (Fk_ServicioTransporteExterno_Codigo),
 CONSTRAINT uqSolicitudCodigo UNIQUE (codigoSolicitudPagoTransporte),
 CONSTRAINT Fk_SolicitudPagoTransporte_ServicioTransporteExterno FOREIGN KEY (Fk_ServicioTransporteExterno_Codigo)
  REFERENCES tblServicioTransporteExterno(Pk_ServicioTransporteExterno) ON DELETE CASCADE
) ENGINE=InnoDB;

-- VISTA DE SOLICITUDES DE PAGO
CREATE OR REPLACE VIEW vwSolicitudesPagoTransporte AS
SELECT s.codigoSolicitudPagoTransporte AS solicitud,
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
JOIN tblEmpresaTransportista e ON e.Pk_EmpresaTransportista = t.Fk_EmpresaTransportista_Codigo
JOIN tblTipoServicio ts ON ts.Pk_TipoServicio = t.Fk_TipoServicio_Codigo
JOIN tblCostoTransporte c ON c.Fk_ServicioTransporteExterno_Codigo = t.Pk_ServicioTransporteExterno
JOIN tblSolicitudPagoTransporte s ON s.Fk_ServicioTransporteExterno_Codigo = t.Pk_ServicioTransporteExterno;

DROP PROCEDURE IF EXISTS spGenerarSolicitudPagoTransporte;
DELIMITER $$
-- La referencia bancaria y la fecha de pago pueden quedar NULL mientras el pago esté pendiente.
CREATE PROCEDURE spGenerarSolicitudPagoTransporte(
    IN pIdServicio INT,
    IN pReferenciaBancaria VARCHAR(100),
    IN pFechaPago DATE
)
BEGIN
    DECLARE vId INT;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION BEGIN ROLLBACK; RESIGNAL; END;
    START TRANSACTION;
    IF NOT EXISTS (SELECT 1 FROM tblCostoTransporte
                   WHERE Fk_ServicioTransporteExterno_Codigo = pIdServicio) THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El servicio no existe o no tiene costo registrado';
    END IF;
    INSERT INTO tblSolicitudPagoTransporte (
        Fk_ServicioTransporteExterno_Codigo,
        codigoSolicitudPagoTransporte,
        referenciaBancariaSolicitudPagoTransporte,
        fechaPagoSolicitudPagoTransporte
    ) VALUES (
        pIdServicio,
        CONCAT('TMP-', LEFT(REPLACE(UUID(), '-', ''), 26)),
        pReferenciaBancaria,
        pFechaPago
    );
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
CREATE PROCEDURE spRegistrarServicioTransporteExterno(
    IN pEmpresa INT, IN pTipoServicio INT, IN pTraslado INT,
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
    IF pTraslado IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'El traslado es obligatorio';
    END IF;
    SELECT nombreEmpresaTransportista INTO vEmpresa
    FROM tblEmpresaTransportista WHERE Pk_EmpresaTransportista = pEmpresa;
    IF vEmpresa IS NULL THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Empresa transportista no válida';
    END IF;
    SET vPrefijo = CASE UPPER(TRIM(vEmpresa))
        WHEN 'FORZA' THEN 'FZ' WHEN 'CARGO EXPRESO' THEN 'CE'
        WHEN 'GUATEX' THEN 'GX' WHEN 'DHL EXPRESS' THEN 'DHL'
        WHEN 'FEDEX' THEN 'FDX' WHEN 'UPS' THEN 'UPS' ELSE 'TR' END;
    INSERT INTO tblServicioTransporteExterno (
       Fk_EmpresaTransportista_Codigo, Fk_TipoServicio_Codigo, Fk_Traslado_Codigo,
       origenServicioTransporteExterno, direccionOrigenServicioTransporteExterno,
       destinoServicioTransporteExterno, direccionDestinoServicioTransporteExterno,
       fechaContratacionServicioTransporteExterno, fechaEstimadaEntregaServicioTransporteExterno,
       fechaEntregaRealServicioTransporteExterno,
       numeroGuiaServicioTransporteExterno
    ) VALUES (pEmpresa, pTipoServicio, pTraslado, pOrigen, pDireccionOrigen,
              pDestino, pDireccionDestino, pFechaContratacion, pFechaEstimada, pFechaReal,
              CONCAT('TMP-', LEFT(REPLACE(UUID(), '-', ''), 26)));
    SET vId = LAST_INSERT_ID();
    SET vGuia = CONCAT(vPrefijo, '-', LPAD(vId, 6, '0'));
    UPDATE tblServicioTransporteExterno
    SET numeroGuiaServicioTransporteExterno = vGuia
    WHERE Pk_ServicioTransporteExterno = vId;
    INSERT INTO tblCostoTransporte (
       Fk_ServicioTransporteExterno_Codigo, Fk_TarifaTransporte_Codigo,
       cantidadCostoTransporte, costoUnitarioTransporte, costoTotalTransporte,
       monedaCostoTransporte, tipoPagoCostoTransporte
    ) VALUES (vId, pTarifa, pCantidad, pCosto, ROUND(pCantidad * pCosto, 4), pMoneda, pTipoPago);
    COMMIT;
    SELECT vId AS idServicio, vGuia AS numeroGuia;
END$$
DELIMITER ;
