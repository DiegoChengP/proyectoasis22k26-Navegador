CREATE DATABASE IF NOT EXISTS dbTesoreriaBancos
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_0900_ai_ci;

USE dbTesoreriaBancos;


CREATE TABLE tblMoneda (
    Pk_Moneda BIGINT NOT NULL AUTO_INCREMENT,
    codigoMoneda CHAR(3) NOT NULL,
    nombreMoneda VARCHAR(255) NOT NULL,
    simboloMoneda VARCHAR(8) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Moneda),
    CONSTRAINT uqMoneda1 UNIQUE (codigoMoneda),
    CONSTRAINT ckMoneda1 CHECK (is_active IN (0,1))
) ENGINE=InnoDB;

CREATE TABLE tblTipoCambio (
    Pk_TipoCambio BIGINT NOT NULL AUTO_INCREMENT,
    fechaTipoCambio DATE NOT NULL,
    valorTipoCambio DECIMAL(12,4) NOT NULL,
    fuenteTipoCambio VARCHAR(100) NOT NULL,
    versionTipoCambio INT NOT NULL DEFAULT 1,
    cotizacionOriginalTipoCambio TEXT NULL,
    metodoTipoCambio VARCHAR(30) NOT NULL DEFAULT 'DIRECTA',
    motivoRevisionTipoCambio VARCHAR(255) NULL,
    Fk_Moneda_Origen_Codigo BIGINT NOT NULL,
    Fk_Moneda_Destino_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_TipoCambio),
    CONSTRAINT uqTipoCambio1 UNIQUE (Fk_Moneda_Origen_Codigo,Fk_Moneda_Destino_Codigo,fechaTipoCambio,fuenteTipoCambio,versionTipoCambio),
    CONSTRAINT ckTipoCambio1 CHECK (valorTipoCambio > 0),
    CONSTRAINT ckTipoCambio2 CHECK (versionTipoCambio > 0),
    CONSTRAINT ckTipoCambio3 CHECK (metodoTipoCambio IN ('DIRECTA','INVERSA','CRUZADA')),
    CONSTRAINT ckTipoCambio4 CHECK (Fk_Moneda_Origen_Codigo <> Fk_Moneda_Destino_Codigo),
    INDEX idxTipoCambioMonedaDestino (Fk_Moneda_Destino_Codigo),
    CONSTRAINT Fk_TipoCambio_Moneda_Origen FOREIGN KEY (Fk_Moneda_Origen_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_TipoCambio_Moneda_Destino FOREIGN KEY (Fk_Moneda_Destino_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblBanco (
    Pk_Banco BIGINT NOT NULL AUTO_INCREMENT,
    codigoBanco VARCHAR(30) NOT NULL,
    nombreBanco VARCHAR(100) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Banco),
    CONSTRAINT uqBanco1 UNIQUE (codigoBanco),
    CONSTRAINT ckBanco1 CHECK (is_active IN (0,1))
) ENGINE=InnoDB;

CREATE TABLE tblTipoCuentaBancaria (
    Pk_TipoCuentaBancaria BIGINT NOT NULL AUTO_INCREMENT,
    codigoTipoCuentaBancaria VARCHAR(30) NOT NULL,
    nombreTipoCuentaBancaria VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_TipoCuentaBancaria),
    CONSTRAINT uqTipoCuentaBancaria1 UNIQUE (codigoTipoCuentaBancaria),
    CONSTRAINT ckTipoCuentaBancaria1 CHECK (is_active IN (0,1))
) ENGINE=InnoDB;

CREATE TABLE tblTipoTransaccion (
    Pk_TipoTransaccion BIGINT NOT NULL AUTO_INCREMENT,
    codigoTipoTransaccion VARCHAR(30) NOT NULL,
    nombreTipoTransaccion VARCHAR(255) NOT NULL,
    naturalezaTipoTransaccion VARCHAR(30) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_TipoTransaccion),
    CONSTRAINT uqTipoTransaccion1 UNIQUE (codigoTipoTransaccion),
    CONSTRAINT ckTipoTransaccion1 CHECK (naturalezaTipoTransaccion IN ('INGRESO','EGRESO','AMBOS')),
    CONSTRAINT ckTipoTransaccion2 CHECK (is_active IN (0,1))
) ENGINE=InnoDB;

CREATE TABLE tblCuentaBancaria (
    Pk_CuentaBancaria BIGINT NOT NULL AUTO_INCREMENT,
    numeroCuentaBancaria VARCHAR(100) NOT NULL,
    nombreCuentaBancaria VARCHAR(255) NOT NULL,
    codigoCuentaContableCuentaBancaria VARCHAR(30) NULL,
    limiteSobregiroCuentaBancaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    Fk_Banco_Codigo BIGINT NOT NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    Fk_TipoCuentaBancaria_Codigo BIGINT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_CuentaBancaria),
    CONSTRAINT uqCuentaBancaria1 UNIQUE (Fk_Banco_Codigo,numeroCuentaBancaria),
    CONSTRAINT ckCuentaBancaria1 CHECK (limiteSobregiroCuentaBancaria >= 0),
    CONSTRAINT ckCuentaBancaria2 CHECK (is_active IN (0,1)),
    INDEX idxCuentaBancariaMoneda (Fk_Moneda_Codigo),
    INDEX idxCuentaBancariaTipo (Fk_TipoCuentaBancaria_Codigo),
    CONSTRAINT Fk_CuentaBancaria_Banco FOREIGN KEY (Fk_Banco_Codigo) REFERENCES tblBanco(Pk_Banco) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_CuentaBancaria_Moneda FOREIGN KEY (Fk_Moneda_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_CuentaBancaria_Tipo FOREIGN KEY (Fk_TipoCuentaBancaria_Codigo) REFERENCES tblTipoCuentaBancaria(Pk_TipoCuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblDisponibilidadDiaria (
    Pk_DisponibilidadDiaria BIGINT NOT NULL AUTO_INCREMENT,
    fechaDisponibilidadDiaria DATE NOT NULL,
    saldoInicialDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    ingresosDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    egresosDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    comprometidoDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    restringidoDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    saldoDisponibleDisponibilidadDiaria DECIMAL(12,4) NOT NULL,
    saldoDisponibleGTQDisponibilidadDiaria DECIMAL(12,4) NOT NULL,
    tipoCambioAplicadoDisponibilidadDiaria DECIMAL(12,4) NOT NULL DEFAULT 1.0000,
    fuenteDisponibilidadDiaria VARCHAR(100) NOT NULL,
    fechaGeneracionDisponibilidadDiaria DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_TipoCambio_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DisponibilidadDiaria),
    CONSTRAINT uqDisponibilidadDiaria1 UNIQUE (Fk_CuentaBancaria_Codigo,fechaDisponibilidadDiaria),
    CONSTRAINT ckDisponibilidadDiaria1 CHECK (saldoInicialDisponibilidadDiaria >= 0),
    CONSTRAINT ckDisponibilidadDiaria2 CHECK (ingresosDisponibilidadDiaria >= 0 AND egresosDisponibilidadDiaria >= 0 AND comprometidoDisponibilidadDiaria >= 0),
    CONSTRAINT ckDisponibilidadDiaria4 CHECK (restringidoDisponibilidadDiaria >= 0),
    CONSTRAINT ckDisponibilidadDiaria3 CHECK (tipoCambioAplicadoDisponibilidadDiaria > 0),
    INDEX idxDisponibilidadDiariaTipoCambio (Fk_TipoCambio_Codigo),
    CONSTRAINT Fk_DisponibilidadDiaria_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_DisponibilidadDiaria_TipoCambio FOREIGN KEY (Fk_TipoCambio_Codigo) REFERENCES tblTipoCambio(Pk_TipoCambio) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblPoliticaAprobacion (
    Pk_PoliticaAprobacion BIGINT NOT NULL AUTO_INCREMENT,
    nombrePoliticaAprobacion VARCHAR(255) NOT NULL,
    tipoOperacionPoliticaAprobacion VARCHAR(30) NOT NULL,
    montoMinimoPoliticaAprobacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    montoMaximoPoliticaAprobacion DECIMAL(12,4) NOT NULL,
    cantidadAprobacionesPoliticaAprobacion INT NOT NULL DEFAULT 1,
    fechaInicioPoliticaAprobacion DATE NOT NULL,
    fechaFinPoliticaAprobacion DATE NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PoliticaAprobacion),
    CONSTRAINT ckPoliticaAprobacion1 CHECK (tipoOperacionPoliticaAprobacion IN ('ORDEN_COMPRA','ORDEN_PAGO')),
    CONSTRAINT ckPoliticaAprobacion2 CHECK (montoMinimoPoliticaAprobacion >= 0 AND montoMaximoPoliticaAprobacion >= montoMinimoPoliticaAprobacion),
    CONSTRAINT ckPoliticaAprobacion3 CHECK (cantidadAprobacionesPoliticaAprobacion > 0),
    CONSTRAINT ckPoliticaAprobacion4 CHECK (fechaFinPoliticaAprobacion IS NULL OR fechaFinPoliticaAprobacion >= fechaInicioPoliticaAprobacion),
    CONSTRAINT ckPoliticaAprobacion5 CHECK (is_active IN (0,1)),
    INDEX idxPoliticaAprobacionMoneda (Fk_Moneda_Codigo),
    CONSTRAINT Fk_PoliticaAprobacion_Moneda FOREIGN KEY (Fk_Moneda_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblOrdenCompraRecibida (
    Pk_OrdenCompraRecibida BIGINT NOT NULL AUTO_INCREMENT,
    referenciaExternaOrdenCompraRecibida VARCHAR(100) NOT NULL,
    proveedorOrdenCompraRecibida VARCHAR(100) NOT NULL,
    fechaOrdenCompraRecibida DATE NOT NULL,
    fechaVencimientoOrdenCompraRecibida DATE NOT NULL,
    importeOrdenCompraRecibida DECIMAL(12,4) NOT NULL,
    estadoOrdenCompraRecibida VARCHAR(30) NOT NULL DEFAULT 'PENDIENTE',
    referenciaDocumentoOrdenCompraRecibida VARCHAR(500) NULL,
    identificadorSolicitanteOrdenCompraRecibida VARCHAR(100) NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_OrdenCompraRecibida),
    CONSTRAINT uqOrdenCompraRecibida1 UNIQUE (referenciaExternaOrdenCompraRecibida),
    CONSTRAINT ckOrdenCompraRecibida1 CHECK (importeOrdenCompraRecibida > 0),
    CONSTRAINT ckOrdenCompraRecibida2 CHECK (fechaVencimientoOrdenCompraRecibida >= fechaOrdenCompraRecibida),
    CONSTRAINT ckOrdenCompraRecibida3 CHECK (estadoOrdenCompraRecibida IN ('PENDIENTE','APROBADA','RECHAZADA','PAGADA')),
    INDEX idxOrdenCompraRecibidaMoneda (Fk_Moneda_Codigo),
    CONSTRAINT Fk_OrdenCompraRecibida_Moneda FOREIGN KEY (Fk_Moneda_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblPlanillaRecibida (
    Pk_PlanillaRecibida BIGINT NOT NULL AUTO_INCREMENT,
    referenciaExternaPlanillaRecibida VARCHAR(100) NOT NULL,
    inicioPeriodoPlanillaRecibida DATE NOT NULL,
    finPeriodoPlanillaRecibida DATE NOT NULL,
    estadoPlanillaRecibida VARCHAR(30) NOT NULL DEFAULT 'RECIBIDA',
    referenciaDocumentoPlanillaRecibida VARCHAR(500) NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PlanillaRecibida),
    CONSTRAINT uqPlanillaRecibida1 UNIQUE (referenciaExternaPlanillaRecibida),
    CONSTRAINT ckPlanillaRecibida1 CHECK (finPeriodoPlanillaRecibida >= inicioPeriodoPlanillaRecibida),
    CONSTRAINT ckPlanillaRecibida2 CHECK (estadoPlanillaRecibida IN ('RECIBIDA','PROCESADA','ANULADA')),
    INDEX idxPlanillaRecibidaMoneda (Fk_Moneda_Codigo),
    CONSTRAINT Fk_PlanillaRecibida_Moneda FOREIGN KEY (Fk_Moneda_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblDetallePlanillaRecibida (
    Pk_DetallePlanillaRecibida BIGINT NOT NULL AUTO_INCREMENT,
    referenciaEmpleadoDetallePlanillaRecibida VARCHAR(100) NOT NULL,
    beneficiarioDetallePlanillaRecibida VARCHAR(100) NOT NULL,
    importeNetoDetallePlanillaRecibida DECIMAL(12,4) NOT NULL,
    Fk_PlanillaRecibida_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DetallePlanillaRecibida),
    CONSTRAINT uqDetallePlanillaRecibida1 UNIQUE (Fk_PlanillaRecibida_Codigo,referenciaEmpleadoDetallePlanillaRecibida),
    CONSTRAINT ckDetallePlanillaRecibida1 CHECK (importeNetoDetallePlanillaRecibida > 0),
    CONSTRAINT Fk_DetallePlanillaRecibida_Planilla FOREIGN KEY (Fk_PlanillaRecibida_Codigo) REFERENCES tblPlanillaRecibida(Pk_PlanillaRecibida) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblOrdenPago (
    Pk_OrdenPago BIGINT NOT NULL AUTO_INCREMENT,
    referenciaOrdenPago VARCHAR(30) NOT NULL,
    beneficiarioOrdenPago VARCHAR(100) NOT NULL,
    importeOrdenPago DECIMAL(12,4) NOT NULL,
    medioOrdenPago VARCHAR(30) NOT NULL,
    estadoOrdenPago VARCHAR(30) NOT NULL DEFAULT 'PENDIENTE',
    fechaProgramadaOrdenPago DATE NOT NULL,
    identificadorSolicitanteOrdenPago VARCHAR(100) NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    Fk_OrdenCompraRecibida_Codigo BIGINT NULL,
    Fk_DetallePlanillaRecibida_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_OrdenPago),
    CONSTRAINT uqOrdenPago1 UNIQUE (referenciaOrdenPago),
    CONSTRAINT ckOrdenPago1 CHECK (importeOrdenPago > 0),
    CONSTRAINT ckOrdenPago2 CHECK (medioOrdenPago IN ('CHEQUE','TRANSFERENCIA')),
    CONSTRAINT ckOrdenPago3 CHECK (estadoOrdenPago IN ('PENDIENTE','AUTORIZADA','RECHAZADA','EMITIDA','PAGADA','ANULADA')),
    CONSTRAINT ckOrdenPago4 CHECK ((Fk_OrdenCompraRecibida_Codigo IS NOT NULL) + (Fk_DetallePlanillaRecibida_Codigo IS NOT NULL) = 1),
    CONSTRAINT ckOrdenPago5 CHECK (Fk_DetallePlanillaRecibida_Codigo IS NULL OR medioOrdenPago = 'CHEQUE'),
    INDEX idxOrdenPagoCuenta (Fk_CuentaBancaria_Codigo),
    INDEX idxOrdenPagoMoneda (Fk_Moneda_Codigo),
    INDEX idxOrdenPagoCompra (Fk_OrdenCompraRecibida_Codigo),
    INDEX idxOrdenPagoPlanilla (Fk_DetallePlanillaRecibida_Codigo),
    CONSTRAINT Fk_OrdenPago_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_OrdenPago_Moneda FOREIGN KEY (Fk_Moneda_Codigo) REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_OrdenPago_Compra FOREIGN KEY (Fk_OrdenCompraRecibida_Codigo) REFERENCES tblOrdenCompraRecibida(Pk_OrdenCompraRecibida) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_OrdenPago_Planilla FOREIGN KEY (Fk_DetallePlanillaRecibida_Codigo) REFERENCES tblDetallePlanillaRecibida(Pk_DetallePlanillaRecibida) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblAprobacion (
    Pk_Aprobacion BIGINT NOT NULL AUTO_INCREMENT,
    decisionAprobacion VARCHAR(30) NOT NULL,
    comentarioAprobacion VARCHAR(255) NULL,
    identificadorAprobadorAprobacion VARCHAR(100) NOT NULL,
    referenciaEvidencia2FAAprobacion VARCHAR(255) NOT NULL,
    fechaValidacion2FAAprobacion DATETIME NOT NULL,
    Fk_OrdenCompraRecibida_Codigo BIGINT NULL,
    Fk_OrdenPago_Codigo BIGINT NULL,
    Fk_PoliticaAprobacion_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Aprobacion),
    CONSTRAINT ckAprobacion1 CHECK (decisionAprobacion IN ('APROBADA','RECHAZADA')),
    CONSTRAINT ckAprobacion2 CHECK ((Fk_OrdenCompraRecibida_Codigo IS NOT NULL) + (Fk_OrdenPago_Codigo IS NOT NULL) = 1),
    INDEX idxAprobacionOrdenCompra (Fk_OrdenCompraRecibida_Codigo),
    INDEX idxAprobacionOrdenPago (Fk_OrdenPago_Codigo),
    INDEX idxAprobacionPolitica (Fk_PoliticaAprobacion_Codigo),
    CONSTRAINT Fk_Aprobacion_OrdenCompra FOREIGN KEY (Fk_OrdenCompraRecibida_Codigo) REFERENCES tblOrdenCompraRecibida(Pk_OrdenCompraRecibida) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_Aprobacion_OrdenPago FOREIGN KEY (Fk_OrdenPago_Codigo) REFERENCES tblOrdenPago(Pk_OrdenPago) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_Aprobacion_Politica FOREIGN KEY (Fk_PoliticaAprobacion_Codigo) REFERENCES tblPoliticaAprobacion(Pk_PoliticaAprobacion) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblChequera (
    Pk_Chequera BIGINT NOT NULL AUTO_INCREMENT,
    serieChequera VARCHAR(30) NOT NULL,
    numeroInicialChequera BIGINT NOT NULL,
    numeroFinalChequera BIGINT NOT NULL,
    siguienteNumeroChequera BIGINT NOT NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Chequera),
    CONSTRAINT uqChequera1 UNIQUE (Fk_CuentaBancaria_Codigo,serieChequera),
    CONSTRAINT ckChequera1 CHECK (numeroInicialChequera > 0),
    CONSTRAINT ckChequera2 CHECK (numeroFinalChequera >= numeroInicialChequera),
    CONSTRAINT ckChequera3 CHECK (siguienteNumeroChequera BETWEEN numeroInicialChequera AND numeroFinalChequera + 1),
    CONSTRAINT ckChequera4 CHECK (is_active IN (0,1)),
    CONSTRAINT Fk_Chequera_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblCheque (
    Pk_Cheque BIGINT NOT NULL AUTO_INCREMENT,
    numeroCheque BIGINT NOT NULL,
    beneficiarioCheque VARCHAR(100) NOT NULL,
    estadoCheque VARCHAR(30) NOT NULL DEFAULT 'GENERADO',
    referenciaEvidencia2FACheque VARCHAR(255) NOT NULL,
    fechaValidacion2FACheque DATETIME NOT NULL,
    identificadorGeneradorCheque VARCHAR(100) NOT NULL,
    Fk_Chequera_Codigo BIGINT NOT NULL,
    Fk_OrdenPago_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Cheque),
    CONSTRAINT uqCheque1 UNIQUE (Fk_Chequera_Codigo,numeroCheque),
    CONSTRAINT uqCheque2 UNIQUE (Fk_OrdenPago_Codigo),
    CONSTRAINT ckCheque1 CHECK (estadoCheque IN ('GENERADO','ENTREGADO','COBRADO','ANULADO')),
    CONSTRAINT Fk_Cheque_Chequera FOREIGN KEY (Fk_Chequera_Codigo) REFERENCES tblChequera(Pk_Chequera) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_Cheque_OrdenPago FOREIGN KEY (Fk_OrdenPago_Codigo) REFERENCES tblOrdenPago(Pk_OrdenPago) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblMovimientoBancario (
    Pk_MovimientoBancario BIGINT NOT NULL AUTO_INCREMENT,
    referenciaMovimientoBancario VARCHAR(100) NULL,
    conceptoMovimientoBancario VARCHAR(255) NOT NULL,
    importeMovimientoBancario DECIMAL(12,4) NOT NULL,
    importeGTQMovimientoBancario DECIMAL(12,4) NOT NULL,
    tipoCambioAplicadoMovimientoBancario DECIMAL(12,4) NOT NULL DEFAULT 1.0000,
    sentidoMovimientoBancario INT NOT NULL,
    fechaOperacionMovimientoBancario DATE NOT NULL,
    fechaValorMovimientoBancario DATE NOT NULL,
    origenMovimientoBancario VARCHAR(30) NOT NULL DEFAULT 'MANUAL',
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_TipoTransaccion_Codigo BIGINT NOT NULL,
    Fk_TipoCambio_Codigo BIGINT NULL,
    Fk_OrdenPago_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_MovimientoBancario),
    CONSTRAINT uqMovimientoBancario1 UNIQUE (Fk_OrdenPago_Codigo),
    CONSTRAINT ckMovimientoBancario1 CHECK (importeMovimientoBancario > 0 AND importeGTQMovimientoBancario > 0),
    CONSTRAINT ckMovimientoBancario2 CHECK (tipoCambioAplicadoMovimientoBancario > 0),
    CONSTRAINT ckMovimientoBancario3 CHECK (sentidoMovimientoBancario IN (-1,1)),
    CONSTRAINT ckMovimientoBancario4 CHECK (origenMovimientoBancario IN ('MANUAL','ESTADO_CUENTA','ORDEN_PAGO')),
    INDEX idxMovimientoBancarioCuentaFecha (Fk_CuentaBancaria_Codigo,fechaOperacionMovimientoBancario),
    INDEX idxMovimientoBancarioTipo (Fk_TipoTransaccion_Codigo),
    INDEX idxMovimientoBancarioTipoCambio (Fk_TipoCambio_Codigo),
    CONSTRAINT Fk_MovimientoBancario_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_MovimientoBancario_Tipo FOREIGN KEY (Fk_TipoTransaccion_Codigo) REFERENCES tblTipoTransaccion(Pk_TipoTransaccion) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_MovimientoBancario_TipoCambio FOREIGN KEY (Fk_TipoCambio_Codigo) REFERENCES tblTipoCambio(Pk_TipoCambio) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_MovimientoBancario_OrdenPago FOREIGN KEY (Fk_OrdenPago_Codigo) REFERENCES tblOrdenPago(Pk_OrdenPago) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblExtractoBancario (
    Pk_ExtractoBancario BIGINT NOT NULL AUTO_INCREMENT,
    referenciaExtractoBancario VARCHAR(100) NOT NULL,
    fechaInicioExtractoBancario DATE NOT NULL,
    fechaFinExtractoBancario DATE NOT NULL,
    saldoInicialExtractoBancario DECIMAL(12,4) NOT NULL,
    saldoFinalExtractoBancario DECIMAL(12,4) NOT NULL,
    huellaExtractoBancario CHAR(64) NULL,
    fechaImportacionExtractoBancario DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_ExtractoBancario),
    CONSTRAINT uqExtractoBancario1 UNIQUE (Fk_CuentaBancaria_Codigo,referenciaExtractoBancario),
    CONSTRAINT uqExtractoBancario2 UNIQUE (Fk_CuentaBancaria_Codigo,huellaExtractoBancario),
    CONSTRAINT ckExtractoBancario1 CHECK (fechaFinExtractoBancario >= fechaInicioExtractoBancario),
    CONSTRAINT Fk_ExtractoBancario_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblDetalleExtracto (
    Pk_DetalleExtracto BIGINT NOT NULL AUTO_INCREMENT,
    identificadorDetalleExtracto VARCHAR(100) NOT NULL,
    referenciaDetalleExtracto VARCHAR(100) NULL,
    conceptoDetalleExtracto VARCHAR(255) NOT NULL,
    fechaOperacionDetalleExtracto DATE NOT NULL,
    fechaValorDetalleExtracto DATE NOT NULL,
    importeDetalleExtracto DECIMAL(12,4) NOT NULL,
    sentidoDetalleExtracto INT NOT NULL,
    Fk_ExtractoBancario_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DetalleExtracto),
    CONSTRAINT uqDetalleExtracto1 UNIQUE (Fk_ExtractoBancario_Codigo,identificadorDetalleExtracto),
    CONSTRAINT ckDetalleExtracto1 CHECK (importeDetalleExtracto > 0),
    CONSTRAINT ckDetalleExtracto2 CHECK (sentidoDetalleExtracto IN (-1,1)),
    CONSTRAINT Fk_DetalleExtracto_Extracto FOREIGN KEY (Fk_ExtractoBancario_Codigo) REFERENCES tblExtractoBancario(Pk_ExtractoBancario) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblConciliacion (
    Pk_Conciliacion BIGINT NOT NULL AUTO_INCREMENT,
    fechaCorteConciliacion DATE NOT NULL,
    estadoConciliacion VARCHAR(30) NOT NULL DEFAULT 'ABIERTA',
    metodoConciliacion VARCHAR(30) NOT NULL DEFAULT 'AUTOMATICA',
    saldoLibroConciliacion DECIMAL(12,4) NULL,
    saldoBancoConciliacion DECIMAL(12,4) NULL,
    diferenciaConciliacion DECIMAL(12,4) NULL,
    identificadorResponsableConciliacion VARCHAR(100) NULL,
    fechaCierreConciliacion DATETIME NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_ExtractoBancario_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Conciliacion),
    CONSTRAINT uqConciliacion1 UNIQUE (Fk_CuentaBancaria_Codigo,fechaCorteConciliacion),
    CONSTRAINT ckConciliacion1 CHECK (estadoConciliacion IN ('ABIERTA','PROCESANDO','CONCILIADA','CON_DIFERENCIAS','CERRADA')),
    CONSTRAINT ckConciliacion2 CHECK (metodoConciliacion IN ('AUTOMATICA','MANUAL','MIXTA')),
    INDEX idxConciliacionExtracto (Fk_ExtractoBancario_Codigo),
    CONSTRAINT Fk_Conciliacion_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_Conciliacion_Extracto FOREIGN KEY (Fk_ExtractoBancario_Codigo) REFERENCES tblExtractoBancario(Pk_ExtractoBancario) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblDetalleConciliacion (
    Pk_DetalleConciliacion BIGINT NOT NULL AUTO_INCREMENT,
    resultadoDetalleConciliacion VARCHAR(30) NOT NULL,
    metodoDetalleConciliacion VARCHAR(30) NOT NULL,
    diferenciaDetalleConciliacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    motivoDetalleConciliacion VARCHAR(255) NULL,
    Fk_Conciliacion_Codigo BIGINT NOT NULL,
    Fk_MovimientoBancario_Codigo BIGINT NULL,
    Fk_DetalleExtracto_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DetalleConciliacion),
    CONSTRAINT uqDetalleConciliacion1 UNIQUE (Fk_Conciliacion_Codigo,Fk_MovimientoBancario_Codigo),
    CONSTRAINT uqDetalleConciliacion2 UNIQUE (Fk_Conciliacion_Codigo,Fk_DetalleExtracto_Codigo),
    CONSTRAINT ckDetalleConciliacion1 CHECK (resultadoDetalleConciliacion IN ('COINCIDE','PENDIENTE','DIFERENCIA')),
    CONSTRAINT ckDetalleConciliacion2 CHECK (metodoDetalleConciliacion IN ('AUTOMATICA','MANUAL')),
    CONSTRAINT ckDetalleConciliacion3 CHECK (Fk_MovimientoBancario_Codigo IS NOT NULL OR Fk_DetalleExtracto_Codigo IS NOT NULL),
    INDEX idxDetalleConciliacionMovimiento (Fk_MovimientoBancario_Codigo),
    INDEX idxDetalleConciliacionExtracto (Fk_DetalleExtracto_Codigo),
    CONSTRAINT Fk_DetalleConciliacion_Conciliacion FOREIGN KEY (Fk_Conciliacion_Codigo) REFERENCES tblConciliacion(Pk_Conciliacion) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_DetalleConciliacion_Movimiento FOREIGN KEY (Fk_MovimientoBancario_Codigo) REFERENCES tblMovimientoBancario(Pk_MovimientoBancario) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_DetalleConciliacion_Extracto FOREIGN KEY (Fk_DetalleExtracto_Codigo) REFERENCES tblDetalleExtracto(Pk_DetalleExtracto) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblConfiguracionPoliza (
    Pk_ConfiguracionPoliza BIGINT NOT NULL AUTO_INCREMENT,
    codigoCuentaContableBancoConfiguracionPoliza VARCHAR(30) NOT NULL,
    codigoCuentaContableContrapartidaConfiguracionPoliza VARCHAR(30) NOT NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_TipoTransaccion_Codigo BIGINT NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_ConfiguracionPoliza),
    CONSTRAINT uqConfiguracionPoliza1 UNIQUE (Fk_CuentaBancaria_Codigo,Fk_TipoTransaccion_Codigo),
    CONSTRAINT ckConfiguracionPoliza1 CHECK (is_active IN (0,1)),
    INDEX idxConfiguracionPolizaTipo (Fk_TipoTransaccion_Codigo),
    CONSTRAINT Fk_ConfiguracionPoliza_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo) REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_ConfiguracionPoliza_Tipo FOREIGN KEY (Fk_TipoTransaccion_Codigo) REFERENCES tblTipoTransaccion(Pk_TipoTransaccion) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblPolizaBancaria (
    Pk_PolizaBancaria BIGINT NOT NULL AUTO_INCREMENT,
    fechaPolizaBancaria DATE NOT NULL,
    estadoPolizaBancaria VARCHAR(30) NOT NULL DEFAULT 'BORRADOR',
    referenciaContabilidadPolizaBancaria VARCHAR(100) NULL,
    fechaEnvioContabilidadPolizaBancaria DATETIME NULL,
    Fk_MovimientoBancario_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PolizaBancaria),
    CONSTRAINT uqPolizaBancaria1 UNIQUE (Fk_MovimientoBancario_Codigo),
    CONSTRAINT ckPolizaBancaria1 CHECK (estadoPolizaBancaria IN ('BORRADOR','VALIDADA','PENDIENTE_ENVIO','ENVIADA','ACEPTADA','RECHAZADA','ANULADA')),
    CONSTRAINT Fk_PolizaBancaria_Movimiento FOREIGN KEY (Fk_MovimientoBancario_Codigo) REFERENCES tblMovimientoBancario(Pk_MovimientoBancario) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblDetallePolizaBancaria (
    Pk_DetallePolizaBancaria BIGINT NOT NULL AUTO_INCREMENT,
    codigoCuentaContableDetallePolizaBancaria VARCHAR(30) NOT NULL,
    debeDetallePolizaBancaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    haberDetallePolizaBancaria DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    Fk_PolizaBancaria_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DetallePolizaBancaria),
    CONSTRAINT ckDetallePolizaBancaria1 CHECK ((debeDetallePolizaBancaria > 0 AND haberDetallePolizaBancaria = 0) OR (haberDetallePolizaBancaria > 0 AND debeDetallePolizaBancaria = 0)),
    INDEX idxDetallePolizaBancariaPoliza (Fk_PolizaBancaria_Codigo),
    CONSTRAINT Fk_DetallePolizaBancaria_Poliza FOREIGN KEY (Fk_PolizaBancaria_Codigo) REFERENCES tblPolizaBancaria(Pk_PolizaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblAutOrdenCompra (
    Pk_AutOrdenCompra BIGINT NOT NULL AUTO_INCREMENT,
    Fk_OrdenCompraRecibida_Codigo BIGINT NOT NULL,
    identificadorEmpresaAutOrdenCompra INT NOT NULL,
    estadoAutOrdenCompra VARCHAR(30) NOT NULL DEFAULT 'PENDIENTE',
    identificadorUsuarioAutorizaAutOrdenCompra INT NULL,
    fechaDecisionAutOrdenCompra DATETIME NULL,
    comentarioAutOrdenCompra VARCHAR(255) NULL,
    motivoRechazoAutOrdenCompra VARCHAR(255) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_AutOrdenCompra),
    CONSTRAINT uqAutOrdenCompra1 UNIQUE (Fk_OrdenCompraRecibida_Codigo),
    CONSTRAINT ckAutOrdenCompra1 CHECK (identificadorEmpresaAutOrdenCompra > 0),
    CONSTRAINT ckAutOrdenCompra2 CHECK (estadoAutOrdenCompra IN ('PENDIENTE','AUTORIZADA','RECHAZADA')),
    CONSTRAINT ckAutOrdenCompra3 CHECK (
        (estadoAutOrdenCompra = 'PENDIENTE' AND identificadorUsuarioAutorizaAutOrdenCompra IS NULL AND fechaDecisionAutOrdenCompra IS NULL)
        OR
        (estadoAutOrdenCompra IN ('AUTORIZADA','RECHAZADA') AND identificadorUsuarioAutorizaAutOrdenCompra IS NOT NULL AND fechaDecisionAutOrdenCompra IS NOT NULL)
    ),
    CONSTRAINT ckAutOrdenCompra4 CHECK (identificadorUsuarioAutorizaAutOrdenCompra IS NULL OR identificadorUsuarioAutorizaAutOrdenCompra > 0),
    CONSTRAINT ckAutOrdenCompra5 CHECK (estadoAutOrdenCompra <> 'RECHAZADA' OR (motivoRechazoAutOrdenCompra IS NOT NULL AND CHAR_LENGTH(TRIM(motivoRechazoAutOrdenCompra)) > 0)),
    CONSTRAINT Fk_AutOrdenCompra_Compra FOREIGN KEY (Fk_OrdenCompraRecibida_Codigo)
        REFERENCES tblOrdenCompraRecibida(Pk_OrdenCompraRecibida) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblAutDisponibilidadFondos (
    Pk_AutDisponibilidadFondos BIGINT NOT NULL AUTO_INCREMENT,
    Fk_AutOrdenCompra_Codigo BIGINT NOT NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    fechaCorteAutDisponibilidadFondos DATE NOT NULL,
    fechaEvaluacionAutDisponibilidadFondos DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    saldoDisponibleAutDisponibilidadFondos DECIMAL(12,4) NOT NULL,
    Fk_Moneda_Origen_Codigo BIGINT NOT NULL,
    Fk_Moneda_Destino_Codigo BIGINT NOT NULL,
    tipoCambioAutDisponibilidadFondos DECIMAL(12,4) NOT NULL,
    Fk_TipoCambio_Codigo BIGINT NULL,
    fuenteAutDisponibilidadFondos VARCHAR(100) NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_AutDisponibilidadFondos),
    CONSTRAINT uqAutDisponibilidadFondos1 UNIQUE
        (Pk_AutDisponibilidadFondos,Fk_AutOrdenCompra_Codigo,Fk_CuentaBancaria_Codigo),
    CONSTRAINT ckAutDisponibilidadFondos1 CHECK (tipoCambioAutDisponibilidadFondos > 0),
    CONSTRAINT ckAutDisponibilidadFondos2 CHECK (
        (Fk_Moneda_Origen_Codigo = Fk_Moneda_Destino_Codigo AND tipoCambioAutDisponibilidadFondos = 1.0000 AND Fk_TipoCambio_Codigo IS NULL)
        OR (Fk_Moneda_Origen_Codigo <> Fk_Moneda_Destino_Codigo AND Fk_TipoCambio_Codigo IS NOT NULL)
    ),
    INDEX idxAutDisponibilidadFondosEvaluacion
        (Fk_AutOrdenCompra_Codigo,Fk_CuentaBancaria_Codigo,fechaEvaluacionAutDisponibilidadFondos),
    INDEX idxAutDisponibilidadFondosCuenta (Fk_CuentaBancaria_Codigo),
    INDEX idxAutDisponibilidadFondosMonedaOrigen (Fk_Moneda_Origen_Codigo),
    INDEX idxAutDisponibilidadFondosMonedaDestino (Fk_Moneda_Destino_Codigo),
    INDEX idxAutDisponibilidadFondosTipoCambio (Fk_TipoCambio_Codigo),
    CONSTRAINT Fk_AutDisponibilidadFondos_Autorizacion FOREIGN KEY (Fk_AutOrdenCompra_Codigo)
        REFERENCES tblAutOrdenCompra(Pk_AutOrdenCompra) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutDisponibilidadFondos_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo)
        REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutDisponibilidadFondos_MonedaOrigen FOREIGN KEY (Fk_Moneda_Origen_Codigo)
        REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutDisponibilidadFondos_MonedaDestino FOREIGN KEY (Fk_Moneda_Destino_Codigo)
        REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutDisponibilidadFondos_TipoCambio FOREIGN KEY (Fk_TipoCambio_Codigo)
        REFERENCES tblTipoCambio(Pk_TipoCambio) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblAutPagoOrden (
    Pk_AutPagoOrden BIGINT NOT NULL AUTO_INCREMENT,
    Fk_AutOrdenCompra_Codigo BIGINT NOT NULL,
    Fk_CuentaBancaria_Codigo BIGINT NOT NULL,
    Fk_AutDisponibilidadFondos_Codigo BIGINT NOT NULL,
    montoAsignadoAutPagoOrden DECIMAL(12,4) NOT NULL,
    estadoAutPagoOrden VARCHAR(30) NOT NULL DEFAULT 'PROPUESTA',
    fechaReservaAutPagoOrden DATETIME NULL,
    Fk_OrdenPago_Codigo BIGINT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_AutPagoOrden),
    CONSTRAINT uqAutPagoOrden1 UNIQUE (Fk_OrdenPago_Codigo),
    CONSTRAINT ckAutPagoOrden1 CHECK (montoAsignadoAutPagoOrden > 0),
    CONSTRAINT ckAutPagoOrden2 CHECK (estadoAutPagoOrden IN ('PROPUESTA','RESERVADA','LIBERADA','APLICADA')),
    CONSTRAINT ckAutPagoOrden3 CHECK (estadoAutPagoOrden NOT IN ('RESERVADA','APLICADA') OR fechaReservaAutPagoOrden IS NOT NULL),
    CONSTRAINT ckAutPagoOrden4 CHECK (estadoAutPagoOrden <> 'APLICADA' OR Fk_OrdenPago_Codigo IS NOT NULL),
    INDEX idxAutPagoOrdenAutorizacion (Fk_AutOrdenCompra_Codigo),
    INDEX idxAutPagoOrdenCuenta (Fk_CuentaBancaria_Codigo),
    INDEX idxAutPagoOrdenEvidencia (Fk_AutDisponibilidadFondos_Codigo,Fk_AutOrdenCompra_Codigo,Fk_CuentaBancaria_Codigo),
    CONSTRAINT Fk_AutPagoOrden_Autorizacion FOREIGN KEY (Fk_AutOrdenCompra_Codigo)
        REFERENCES tblAutOrdenCompra(Pk_AutOrdenCompra) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutPagoOrden_Cuenta FOREIGN KEY (Fk_CuentaBancaria_Codigo)
        REFERENCES tblCuentaBancaria(Pk_CuentaBancaria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutPagoOrden_Evidencia FOREIGN KEY
        (Fk_AutDisponibilidadFondos_Codigo,Fk_AutOrdenCompra_Codigo,Fk_CuentaBancaria_Codigo)
        REFERENCES tblAutDisponibilidadFondos
        (Pk_AutDisponibilidadFondos,Fk_AutOrdenCompra_Codigo,Fk_CuentaBancaria_Codigo)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_AutPagoOrden_OrdenPago FOREIGN KEY (Fk_OrdenPago_Codigo)
        REFERENCES tblOrdenPago(Pk_OrdenPago) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE tblPartidaConsolidada (
    Pk_PartidaConsolidada BIGINT NOT NULL AUTO_INCREMENT,
    identificadorEmpresaPartidaConsolidada INT NOT NULL,
    fechaInicioPartidaConsolidada DATE NOT NULL,
    fechaFinPartidaConsolidada DATE NOT NULL,
    conceptoPartidaConsolidada VARCHAR(255) NOT NULL,
    Fk_Moneda_Codigo BIGINT NOT NULL,
    totalDebePartidaConsolidada DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    totalHaberPartidaConsolidada DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    estadoPartidaConsolidada VARCHAR(30) NOT NULL DEFAULT 'GENERADA',
    identificadorUsuarioGeneraPartidaConsolidada VARCHAR(100) NOT NULL,
    fechaGeneracionPartidaConsolidada DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    identificadorUsuarioTrasladaPartidaConsolidada VARCHAR(100) NULL,
    fechaTrasladoPartidaConsolidada DATETIME NULL,
    claveTrasladoPartidaConsolidada VARCHAR(100) NOT NULL,
    referenciaContabilidadPartidaConsolidada VARCHAR(100) NULL,
    mensajeResultadoTrasladoPartidaConsolidada VARCHAR(500) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PartidaConsolidada),
    CONSTRAINT uqPartidaConsolidadaClave UNIQUE (claveTrasladoPartidaConsolidada),
    CONSTRAINT uqPartidaConsolidadaPeriodo UNIQUE
        (identificadorEmpresaPartidaConsolidada,fechaInicioPartidaConsolidada,fechaFinPartidaConsolidada,Fk_Moneda_Codigo),
    CONSTRAINT ckPartidaConsolidadaEmpresa CHECK (identificadorEmpresaPartidaConsolidada > 0),
    CONSTRAINT ckPartidaConsolidadaPeriodo CHECK (
        DAYOFMONTH(fechaInicioPartidaConsolidada) = 1
        AND fechaFinPartidaConsolidada = LAST_DAY(fechaInicioPartidaConsolidada)
    ),
    CONSTRAINT ckPartidaConsolidadaDatos CHECK (
        CHAR_LENGTH(TRIM(conceptoPartidaConsolidada)) > 0
        AND CHAR_LENGTH(TRIM(claveTrasladoPartidaConsolidada)) > 0
        AND CHAR_LENGTH(TRIM(identificadorUsuarioGeneraPartidaConsolidada)) > 0
    ),
    CONSTRAINT ckPartidaConsolidadaTotales CHECK (
        totalDebePartidaConsolidada >= 0 AND totalHaberPartidaConsolidada >= 0
    ),
    CONSTRAINT ckPartidaConsolidadaEstado CHECK (
        estadoPartidaConsolidada IN ('GENERADA','VALIDADA','EN_PROCESO','TRASLADADA','ERROR')
    ),
    CONSTRAINT ckPartidaConsolidadaCuadre CHECK (
        estadoPartidaConsolidada NOT IN ('VALIDADA','EN_PROCESO','TRASLADADA')
        OR (totalDebePartidaConsolidada = totalHaberPartidaConsolidada AND totalDebePartidaConsolidada > 0)
    ),
    CONSTRAINT ckPartidaConsolidadaUsuarioTraslado CHECK (
        estadoPartidaConsolidada NOT IN ('EN_PROCESO','TRASLADADA')
        OR (
            identificadorUsuarioTrasladaPartidaConsolidada IS NOT NULL
            AND CHAR_LENGTH(TRIM(identificadorUsuarioTrasladaPartidaConsolidada)) > 0
        )
    ),
    CONSTRAINT ckPartidaConsolidadaFechaTraslado CHECK (
        estadoPartidaConsolidada <> 'TRASLADADA' OR fechaTrasladoPartidaConsolidada IS NOT NULL
    ),
    CONSTRAINT ckPartidaConsolidadaReferenciaTraslado CHECK (
        estadoPartidaConsolidada <> 'TRASLADADA'
        OR (
            referenciaContabilidadPartidaConsolidada IS NOT NULL
            AND CHAR_LENGTH(TRIM(referenciaContabilidadPartidaConsolidada)) > 0
        )
    ),
    CONSTRAINT ckPartidaConsolidadaOrdenFechas CHECK (
        fechaTrasladoPartidaConsolidada IS NULL
        OR fechaTrasladoPartidaConsolidada >= fechaGeneracionPartidaConsolidada
    ),
    INDEX idxPartidaConsolidadaEmpresaEstado (identificadorEmpresaPartidaConsolidada,estadoPartidaConsolidada),
    INDEX idxPartidaConsolidadaMoneda (Fk_Moneda_Codigo),
    CONSTRAINT Fk_PartidaConsolidada_Moneda FOREIGN KEY (Fk_Moneda_Codigo)
        REFERENCES tblMoneda(Pk_Moneda) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblDetallePartidaConsolidada (
    Pk_DetallePartidaConsolidada BIGINT NOT NULL AUTO_INCREMENT,
    Fk_PartidaConsolidada_Codigo BIGINT NOT NULL,
    codigoCuentaContableDetallePartidaConsolidada VARCHAR(30) NOT NULL,
    nombreCuentaContableDetallePartidaConsolidada VARCHAR(255) NOT NULL,
    debeDetallePartidaConsolidada DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    haberDetallePartidaConsolidada DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_DetallePartidaConsolidada),
    CONSTRAINT uqDetallePartidaCuenta UNIQUE
        (Fk_PartidaConsolidada_Codigo,codigoCuentaContableDetallePartidaConsolidada),
    CONSTRAINT ckDetallePartidaCuenta CHECK (
        CHAR_LENGTH(TRIM(codigoCuentaContableDetallePartidaConsolidada)) > 0
        AND CHAR_LENGTH(TRIM(nombreCuentaContableDetallePartidaConsolidada)) > 0
    ),
    CONSTRAINT ckDetallePartidaImportes CHECK (
        debeDetallePartidaConsolidada >= 0
        AND haberDetallePartidaConsolidada >= 0
        AND (debeDetallePartidaConsolidada > 0 OR haberDetallePartidaConsolidada > 0)
    ),
    CONSTRAINT Fk_DetallePartidaConsolidada_Partida FOREIGN KEY (Fk_PartidaConsolidada_Codigo)
        REFERENCES tblPartidaConsolidada(Pk_PartidaConsolidada)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;

CREATE TABLE tblPartidaConsolidadaPolizaBancaria (
    Fk_PartidaConsolidada_Codigo BIGINT NOT NULL,
    Fk_PolizaBancaria_Codigo BIGINT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Fk_PartidaConsolidada_Codigo,Fk_PolizaBancaria_Codigo),
    CONSTRAINT uqPartidaConsolidadaPolizaBancariaPoliza UNIQUE (Fk_PolizaBancaria_Codigo),
    CONSTRAINT Fk_PartidaConsolidadaPolizaBancaria_Partida FOREIGN KEY (Fk_PartidaConsolidada_Codigo)
        REFERENCES tblPartidaConsolidada(Pk_PartidaConsolidada)
        ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_PartidaConsolidadaPolizaBancaria_Poliza FOREIGN KEY (Fk_PolizaBancaria_Codigo)
        REFERENCES tblPolizaBancaria(Pk_PolizaBancaria)
        ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;
