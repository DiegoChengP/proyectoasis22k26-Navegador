use `dbsistemaembutidos`; 
CREATE TABLE IF NOT EXISTS tblProveedor(
	idProveedor INT AUTO_INCREMENT,
    nombreProveedor VARCHAR(100) NOT NULL,
    nitProveedor char(10) not null,
    correoProveedor VARCHAR(150) NOT NULL,
    telefonoProveedor varchar(30) NOT NULL,
    direccionProveedor VARCHAR(500) NOT NULL,
    estadoProveedor BOOLEAN NOT NULL DEFAULT 1,
	created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Proveedor PRIMARY KEY(idProveedor),
   CONSTRAINT uqProveedorNit UNIQUE (nitProveedor)

);
CREATE TABLE tblCuentaPorPagar (
    idCuentaPorPagar               INT            NOT NULL AUTO_INCREMENT,
    idProveedor                    INT            NOT NULL,
    numeroFacturaCuentaPorPagar     VARCHAR(30)    NOT NULL,
    fechaEmisionCuentaPorPagar      DATE           NOT NULL,
    fechaVencimientoCuentaPorPagar  DATE           NOT NULL,
    montoTotalCuentaPorPagar        DECIMAL(12,4)  NOT NULL,
    saldoCuentaPorPagar             DECIMAL(12,4)  NOT NULL,
    estadoCuentaPorPagar            VARCHAR(30)    NOT NULL DEFAULT 'Pendiente',
    created_at                      TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                      TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

   CONSTRAINT Pk_CuentaPorPagar PRIMARY KEY (idCuentaPorPagar),
	CONSTRAINT Fk_CuentaPorPagar_Proveedor FOREIGN KEY (idProveedor) REFERENCES tblProveedor (idProveedor),
    INDEX idxCuentaPorPagarProveedor (idProveedor),
    CONSTRAINT uqFacturaProveedorCuentaPorPagar UNIQUE (idProveedor, numeroFacturaCuentaPorPagar),
    CONSTRAINT ckMontoTotalCuentaPorPagar CHECK (montoTotalCuentaPorPagar > 0),
    CONSTRAINT ckSaldoCuentaPorPagar CHECK (saldoCuentaPorPagar >= 0 AND saldoCuentaPorPagar <= montoTotalCuentaPorPagar),
    CONSTRAINT ckEstadoCuentaPorPagar CHECK (estadoCuentaPorPagar IN ('Pendiente','Parcial','Pagada','Anulada'))
) ENGINE=InnoDB;

CREATE TABLE tblPagoProveedor (
    idPagoProveedor             INT            NOT NULL AUTO_INCREMENT,
    idCuentaPorPagar            INT            NOT NULL,
    idProveedor                 INT            NOT NULL,
    fechaPagoProveedor          DATE           NOT NULL,
    montoPagoProveedor          DECIMAL(12,4)  NOT NULL,
    metodoPagoProveedor         VARCHAR(30)    NOT NULL,
    referenciaPagoProveedor     VARCHAR(30)    NULL,
    estadoPagoProveedor         VARCHAR(30)    NOT NULL DEFAULT 'Programado',
    created_at                  TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                  TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    CONSTRAINT Pk_PagoProveedor PRIMARY KEY (idPagoProveedor),
	CONSTRAINT Fk_PagoProveedor_CuentaPorPagar FOREIGN KEY (idCuentaPorPagar) REFERENCES tblCuentaPorPagar (idCuentaPorPagar),
    CONSTRAINT Fk_PagoProveedor_Proveedor FOREIGN KEY (idProveedor) REFERENCES tblProveedor (idProveedor),
    INDEX idxPagoProveedorCuenta (idCuentaPorPagar),
    INDEX idxPagoProveedorProveedor (idProveedor),
    INDEX idxPagoProveedorFecha (fechaPagoProveedor),
    CONSTRAINT ckMontoPagoProveedor CHECK (montoPagoProveedor > 0),
    CONSTRAINT ckEstadoPagoProveedor CHECK (estadoPagoProveedor IN ('Pagado','Programado','Rechazado','Anulado')),
    CONSTRAINT ckMetodoPagoProveedor CHECK (metodoPagoProveedor IN ('Transferencia','Cheque','Efectivo','Deposito'))
) ENGINE=InnoDB;

CREATE TABLE tblSolicitudPago (
    idSolicitudPago                INT            NOT NULL AUTO_INCREMENT,
    moduloOrigenSolicitudPago       VARCHAR(30)    NOT NULL,
    idOrigenSolicitudPago           INT            NOT NULL,
    beneficiarioSolicitudPago       VARCHAR(100)   NOT NULL,
    montoSolicitudPago              DECIMAL(12,4)  NOT NULL,
    metodoSolicitudPago             VARCHAR(30)    NOT NULL,
    estadoSolicitudPago             VARCHAR(30)    NOT NULL DEFAULT 'Pendiente',
    created_at                      TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                      TIMESTAMP      NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

	CONSTRAINT Pk_SolicitudPago PRIMARY KEY (idSolicitudPago),
    INDEX idxSolicitudPagoOrigen (moduloOrigenSolicitudPago, idOrigenSolicitudPago),
    CONSTRAINT ckMontoSolicitudPago CHECK (montoSolicitudPago > 0),
    CONSTRAINT ckEstadoSolicitudPago CHECK (estadoSolicitudPago IN ('Pendiente','Procesada','Rechazada'))
) ENGINE=InnoDB;
