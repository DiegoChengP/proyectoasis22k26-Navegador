-- =============================================================================
--  TERMINUS ERP  |  Contabilidad
--  Script 1 de 2: estructura
-- =============================================================================

CREATE DATABASE IF NOT EXISTS dbContabilidad
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;
USE dbContabilidad;

-- =============================================================================
--  1. CATÁLOGOS
-- =============================================================================

CREATE TABLE tblPais (
    Pk_Pais     INT          NOT NULL AUTO_INCREMENT,
    nombrePais  VARCHAR(100) NOT NULL,
    is_active   BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at  TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at  TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Pais),
    CONSTRAINT uqPaisNombre UNIQUE (nombrePais)
) ENGINE=InnoDB;

CREATE TABLE tblModuloContable (
    Pk_ModuloContable     INT          NOT NULL AUTO_INCREMENT,
    nombreModuloContable  VARCHAR(100) NOT NULL,
    codigoModuloContable  VARCHAR(30)  NOT NULL,
    is_active             BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at            TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at            TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_ModuloContable),
    CONSTRAINT uqModuloNombre UNIQUE (nombreModuloContable),
    CONSTRAINT uqModuloCodigo UNIQUE (codigoModuloContable)
) ENGINE=InnoDB;

-- TABLA CON JERARQUÍA AUTORREFERENCIADA
CREATE TABLE tblCuenta (
    Pk_Cuenta               INT          NOT NULL AUTO_INCREMENT,
    codigoCuenta            VARCHAR(30)  NOT NULL,
    nombreCuenta            VARCHAR(100) NOT NULL,
    tipoCuenta              VARCHAR(30)  NOT NULL,
    Fk_Cuenta_Padre_Cuenta  INT          NULL, -- Columna con prefijo FK estandarizado
    is_active               BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at              TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at              TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_Cuenta),
    CONSTRAINT uqCuentaCodigo UNIQUE (codigoCuenta),
    CONSTRAINT ckCuentaTipo CHECK (tipoCuenta IN ('DEUDORA','ACREEDORA')),
    CONSTRAINT Fk_Cuenta_Padre_Cuenta FOREIGN KEY (Fk_Cuenta_Padre_Cuenta) REFERENCES tblCuenta (Pk_Cuenta),
    INDEX idxCuentaPadre (Fk_Cuenta_Padre_Cuenta),
    INDEX idxCuentaTipo  (tipoCuenta),
    INDEX idxCuentaNombre (nombreCuenta)
) ENGINE=InnoDB;

CREATE TABLE tblModuloCuentaPais (
    Fk_ModuloContable_Pk_ModuloContable INT       NOT NULL,
    Fk_Cuenta_Pk_Cuenta                 INT       NOT NULL,
    Fk_Pais_Pk_Pais                     INT       NOT NULL,
    is_active                           BOOLEAN   NOT NULL DEFAULT TRUE,
    created_at                          TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                          TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Fk_ModuloContable_Pk_ModuloContable, Fk_Cuenta_Pk_Cuenta, Fk_Pais_Pk_Pais),
    CONSTRAINT Fk_ModuloCuentaPais_Modulo FOREIGN KEY (Fk_ModuloContable_Pk_ModuloContable) REFERENCES tblModuloContable (Pk_ModuloContable),
    CONSTRAINT Fk_ModuloCuentaPais_Cuenta FOREIGN KEY (Fk_Cuenta_Pk_Cuenta) REFERENCES tblCuenta (Pk_Cuenta),
    CONSTRAINT Fk_ModuloCuentaPais_Pais   FOREIGN KEY (Fk_Pais_Pk_Pais)   REFERENCES tblPais   (Pk_Pais),
    INDEX idxModuloCuentaPaisCuenta (Fk_Cuenta_Pk_Cuenta),
    INDEX idxModuloCuentaPaisPais   (Fk_Pais_Pk_Pais)
) ENGINE=InnoDB;

-- =============================================================================
--  2. PÓLIZAS CONTABLES
-- =============================================================================

CREATE TABLE tblPolizaEncabezado (
    Pk_PolizaEncabezado                  INT          NOT NULL AUTO_INCREMENT,
    codigoPolizaEncabezado               VARCHAR(30)  NOT NULL,
    fechaPolizaEncabezado                DATE         NOT NULL,
    periodoContablePolizaEncabezado      CHAR(7)      NOT NULL,
    estadoPolizaEncabezado               VARCHAR(30)  NOT NULL DEFAULT 'BORRADOR',
    origenPolizaEncabezado               VARCHAR(30)  NOT NULL DEFAULT 'MANUAL',
    documentoOrigenPolizaEncabezado      VARCHAR(30)  NULL,
    descripcionPolizaEncabezado          VARCHAR(255) NULL,
    is_active                            BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at                           TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                           TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PolizaEncabezado),
    CONSTRAINT uqPolizaCodigo UNIQUE (codigoPolizaEncabezado),
    CONSTRAINT uqPolizaDocumentoOrigen UNIQUE (origenPolizaEncabezado, documentoOrigenPolizaEncabezado),
    CONSTRAINT ckPolizaEstado CHECK (estadoPolizaEncabezado IN ('BORRADOR','APLICADA','ANULADA')),
    CONSTRAINT ckPolizaOrigen CHECK (origenPolizaEncabezado IN ('MANUAL','BANCOS','ACTIVOS_FIJOS','PRESUPUESTO','NOMINA','VENTAS','COMPRAS')),
    INDEX idxPolizaPeriodo (periodoContablePolizaEncabezado),
    INDEX idxPolizaFecha   (fechaPolizaEncabezado),
    INDEX idxPolizaEstado  (estadoPolizaEncabezado)
) ENGINE=InnoDB;

CREATE TABLE tblPolizaDetalle (
    Pk_PolizaDetalle                        INT           NOT NULL AUTO_INCREMENT,
    Fk_PolizaEncabezado_Pk_PolizaEncabezado INT           NOT NULL,
    Fk_Cuenta_Pk_Cuenta                     INT           NOT NULL,
    lineaPolizaDetalle                      INT           NOT NULL,
    debePolizaDetalle                       DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    haberPolizaDetalle                      DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    descripcionPolizaDetalle                VARCHAR(255)  NULL,
    is_active                               BOOLEAN       NOT NULL DEFAULT TRUE,
    created_at                              TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                              TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PolizaDetalle),
    CONSTRAINT uqPolizaDetalleLinea UNIQUE (Fk_PolizaEncabezado_Pk_PolizaEncabezado, lineaPolizaDetalle),
    CONSTRAINT ckDetalleMontoPositivo CHECK (debePolizaDetalle >= 0 AND haberPolizaDetalle >= 0),
    CONSTRAINT ckDetallePartidaDoble  CHECK (debePolizaDetalle = 0 OR haberPolizaDetalle = 0),
    CONSTRAINT Fk_PolizaDetalle_PolizaEncabezado FOREIGN KEY (Fk_PolizaEncabezado_Pk_PolizaEncabezado) REFERENCES tblPolizaEncabezado (Pk_PolizaEncabezado),
    CONSTRAINT Fk_PolizaDetalle_Cuenta FOREIGN KEY (Fk_Cuenta_Pk_Cuenta) REFERENCES tblCuenta (Pk_Cuenta),
    INDEX idxPolizaDetalleEncabezado (Fk_PolizaEncabezado_Pk_PolizaEncabezado),
    INDEX idxPolizaDetalleCuenta     (Fk_Cuenta_Pk_Cuenta)
) ENGINE=InnoDB;

-- =============================================================================
--  3. PRESUPUESTO
-- =============================================================================

CREATE TABLE tblPresupuestoEncabezado (
    Pk_PresupuestoEncabezado                  INT           NOT NULL AUTO_INCREMENT,
    nombrePresupuestoEncabezado               VARCHAR(100)  NOT NULL,
    totalPresupuestoEncabezado                DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    gastadoPresupuestoEncabezado              DECIMAL(12,4) NOT NULL DEFAULT 0.0000, 
    restantePresupuestoEncabezado             DECIMAL(12,4) GENERATED ALWAYS AS (totalPresupuestoEncabezado - gastadoPresupuestoEncabezado) STORED,
    fechaInicioPresupuestoEncabezado          DATE          NOT NULL,
    fechaCierrePresupuestoEncabezado          DATE          NULL,
    is_active                                 BOOLEAN       NOT NULL DEFAULT TRUE,
    created_at                                TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                                TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PresupuestoEncabezado),
    CONSTRAINT ckPresupuestoFechas CHECK (fechaCierrePresupuestoEncabezado IS NULL OR fechaCierrePresupuestoEncabezado >= fechaInicioPresupuestoEncabezado),
    CONSTRAINT ckPresupuestoMontos CHECK (totalPresupuestoEncabezado >= 0 AND gastadoPresupuestoEncabezado >= 0)
) ENGINE=InnoDB;

CREATE TABLE tblPresupuestoDetalle (
    Pk_PresupuestoDetalle                              INT           NOT NULL AUTO_INCREMENT,
    Fk_PresupuestoEncabezado_Pk_PresupuestoEncabezado  INT           NOT NULL,
    Fk_ModuloContable_Pk_ModuloContable                INT           NOT NULL,
    Fk_PolizaDetalle_Pk_PolizaDetalle                  INT           NULL,
    nombrePresupuestoDetalle                           VARCHAR(100)  NOT NULL,
    cantidadPresupuestoDetalle                         DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    is_active                                          BOOLEAN       NOT NULL DEFAULT TRUE,
    created_at                                         TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                                         TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_PresupuestoDetalle),
    CONSTRAINT ckPresupuestoDetalleCantidad CHECK (cantidadPresupuestoDetalle >= 0),
    CONSTRAINT Fk_PresupuestoDetalle_PresupuestoEncabezado FOREIGN KEY (Fk_PresupuestoEncabezado_Pk_PresupuestoEncabezado) REFERENCES tblPresupuestoEncabezado (Pk_PresupuestoEncabezado),
    CONSTRAINT Fk_PresupuestoDetalle_Modulo FOREIGN KEY (Fk_ModuloContable_Pk_ModuloContable) REFERENCES tblModuloContable (Pk_ModuloContable),
    CONSTRAINT Fk_PresupuestoDetalle_PolizaDetalle FOREIGN KEY (Fk_PolizaDetalle_Pk_PolizaDetalle) REFERENCES tblPolizaDetalle (Pk_PolizaDetalle),
    INDEX idxPresupuestoDetalleEncabezado (Fk_PresupuestoEncabezado_Pk_PresupuestoEncabezado),
    INDEX idxPresupuestoDetalleModulo     (Fk_ModuloContable_Pk_ModuloContable),
    INDEX idxPresupuestoDetallePolizaDet  (Fk_PolizaDetalle_Pk_PolizaDetalle)
) ENGINE=InnoDB;

-- =============================================================================
--  4. ACTIVOS FIJOS
-- =============================================================================

CREATE TABLE tblMaestroVidaUtil (
    Pk_MaestroVidaUtil      INT          NOT NULL AUTO_INCREMENT,
    codigoMaestroVidaUtil   VARCHAR(30)  NOT NULL,
    nombreMaestroVidaUtil   VARCHAR(100) NOT NULL,
    cantidadMesesMaestroVidaUtil INT     NOT NULL,
    tasaDepreciacionMaestroVidaUtil DECIMAL(3,2) NOT NULL DEFAULT 0.20,
    is_active               BOOLEAN      NOT NULL DEFAULT TRUE,
    created_at              TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at              TIMESTAMP    NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_MaestroVidaUtil),
    CONSTRAINT uqMaestroVidaUtilCodigo UNIQUE (codigoMaestroVidaUtil),
    CONSTRAINT ckMaestroVidaUtilMeses CHECK (cantidadMesesMaestroVidaUtil > 0)
) ENGINE=InnoDB;

CREATE TABLE tblActivoFijo (
    Pk_ActivoFijo                                 INT           NOT NULL AUTO_INCREMENT,
    Fk_ModuloContable_Pk_ModuloContable           INT           NOT NULL,
    Fk_MaestroVidaUtil_Pk_MaestroVidaUtil         INT           NOT NULL,
    codigoActivoFijo                              VARCHAR(30)   NOT NULL,
    descripcionActivoFijo                         VARCHAR(255)  NOT NULL,
    codigoCentroCostoActivoFijo                   VARCHAR(30)   NOT NULL,
    valorCompraActivoFijo                         DECIMAL(12,4) NOT NULL,
    valorResidualActivoFijo                       DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    valorDepreciableActivoFijo                    DECIMAL(12,4) GENERATED ALWAYS AS (valorCompraActivoFijo - valorResidualActivoFijo) STORED,
    valorEnLibrosActivoFijo                       DECIMAL(12,4) NOT NULL,
    mesesVidaUtilActivoFijo                       INT           NOT NULL,
    fechaAdquisicionActivoFijo                    DATE          NOT NULL,
    fechaInicioUsoActivoFijo                      DATE          NOT NULL,
    is_active                                     BOOLEAN       NOT NULL DEFAULT TRUE,
    created_at                                    TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at                                    TIMESTAMP     NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    PRIMARY KEY (Pk_ActivoFijo),
    CONSTRAINT uqActivoFijoCodigo UNIQUE (codigoActivoFijo),
    CONSTRAINT ckActivoFijoResidual CHECK (valorResidualActivoFijo >= 0 AND valorResidualActivoFijo <= valorCompraActivoFijo),
    CONSTRAINT ckActivoFijoFechas   CHECK (fechaInicioUsoActivoFijo >= fechaAdquisicionActivoFijo),
    CONSTRAINT ckActivoFijoMeses    CHECK (mesesVidaUtilActivoFijo > 0),
    CONSTRAINT Fk_ActivoFijo_Modulo FOREIGN KEY (Fk_ModuloContable_Pk_ModuloContable) REFERENCES tblModuloContable (Pk_ModuloContable),
    CONSTRAINT Fk_ActivoFijo_MaestroVidaUtil FOREIGN KEY (Fk_MaestroVidaUtil_Pk_MaestroVidaUtil) REFERENCES tblMaestroVidaUtil (Pk_MaestroVidaUtil),
    INDEX idxActivoFijoModulo      (Fk_ModuloContable_Pk_ModuloContable),
    INDEX idxActivoFijoMaestroVida (Fk_MaestroVidaUtil_Pk_MaestroVidaUtil)
) ENGINE=InnoDB;

-- =============================================================================
--  5. VISTAS DE VISUALIZACIÓN
-- =============================================================================

CREATE OR REPLACE VIEW vwLibroDiario AS
SELECT  pe.codigoPolizaEncabezado          AS codigoPoliza,
        pe.fechaPolizaEncabezado           AS fechaPoliza,
        pe.periodoContablePolizaEncabezado AS periodoContablePoliza,
        pe.estadoPolizaEncabezado          AS estadoPoliza,
        pe.origenPolizaEncabezado          AS origenPoliza,
        pd.lineaPolizaDetalle              AS lineaDetalle,
        c.codigoCuenta                     AS codigoCuenta,
        c.nombreCuenta                     AS nombreCuenta,
        c.tipoCuenta                       AS tipoCuenta,
        pd.debePolizaDetalle               AS debeDetalle,
        pd.haberPolizaDetalle              AS haberDetalle,
        pd.descripcionPolizaDetalle        AS descripcionDetalle
FROM    tblPolizaDetalle    pd
JOIN    tblPolizaEncabezado pe ON pe.Pk_PolizaEncabezado = pd.Fk_PolizaEncabezado_Pk_PolizaEncabezado
JOIN    tblCuenta           c  ON c.Pk_Cuenta            = pd.Fk_Cuenta_Pk_Cuenta
WHERE   pd.is_active = TRUE AND pe.is_active = TRUE;

CREATE OR REPLACE VIEW vwPolizaCuadre AS
SELECT  pe.codigoPolizaEncabezado            AS codigoPoliza,
        pe.fechaPolizaEncabezado             AS fechaPoliza,
        pe.estadoPolizaEncabezado            AS estadoPoliza,
        pe.descripcionPolizaEncabezado       AS descripcionPoliza,
        COUNT(pd.Pk_PolizaDetalle)           AS totalLineasPoliza,
        SUM(pd.debePolizaDetalle)            AS totalDebePoliza,
        SUM(pd.haberPolizaDetalle)           AS totalHaberPoliza,
        (SUM(pd.debePolizaDetalle) - SUM(pd.haberPolizaDetalle)) AS diferenciaPoliza,
        CASE WHEN SUM(pd.debePolizaDetalle) = SUM(pd.haberPolizaDetalle)
             THEN 'CUADRADA' ELSE 'DESCUADRADA' END AS resultadoCuadre
FROM    tblPolizaEncabezado pe
JOIN    tblPolizaDetalle    pd ON pd.Fk_PolizaEncabezado_Pk_PolizaEncabezado = pe.Pk_PolizaEncabezado
WHERE   pe.is_active = TRUE AND pd.is_active = TRUE
GROUP BY pe.Pk_PolizaEncabezado, pe.codigoPolizaEncabezado, pe.fechaPolizaEncabezado,
         pe.estadoPolizaEncabezado, pe.descripcionPolizaEncabezado;

CREATE OR REPLACE VIEW vwBalanceCuenta AS
SELECT  c.codigoCuenta          AS codigoCuenta,
        c.nombreCuenta          AS nombreCuenta,
        c.tipoCuenta            AS tipoCuenta,
        SUM(pd.debePolizaDetalle)     AS totalDebeCuenta,
        SUM(pd.haberPolizaDetalle)    AS totalHaberCuenta,
        (SUM(pd.debePolizaDetalle) - SUM(pd.haberPolizaDetalle)) AS saldoCuenta
FROM    tblPolizaDetalle    pd
JOIN    tblPolizaEncabezado pe ON pe.Pk_PolizaEncabezado = pd.Fk_PolizaEncabezado_Pk_PolizaEncabezado
JOIN    tblCuenta           c  ON c.Pk_Cuenta            = pd.Fk_Cuenta_Pk_Cuenta
WHERE   pe.estadoPolizaEncabezado = 'APLICADA'
  AND   pe.is_active = TRUE AND pd.is_active = TRUE
GROUP BY c.Pk_Cuenta, c.codigoCuenta, c.nombreCuenta, c.tipoCuenta;

-- =============================================================================
--  6. FUNCIONES
-- =============================================================================
DELIMITER $$

DROP FUNCTION IF EXISTS fnGenerarCodigoPoliza$$
CREATE FUNCTION fnGenerarCodigoPoliza (p_periodoContable CHAR(7))
RETURNS VARCHAR(30)
READS SQL DATA
BEGIN
    DECLARE v_correlativo INT;
    SELECT IFNULL(MAX(CAST(SUBSTRING(codigoPolizaEncabezado, -4) AS UNSIGNED)), 0) + 1
      INTO v_correlativo
      FROM tblPolizaEncabezado
     WHERE periodoContablePolizaEncabezado = p_periodoContable;
    RETURN CONCAT('POL-', p_periodoContable, '-', LPAD(v_correlativo, 4, '0'));
END$$

DROP FUNCTION IF EXISTS fnValidarCuadrePoliza$$
CREATE FUNCTION fnValidarCuadrePoliza (p_Pk_PolizaEncabezado INT)
RETURNS BOOLEAN
READS SQL DATA
BEGIN
    DECLARE v_debe  DECIMAL(12,4) DEFAULT 0;
    DECLARE v_haber DECIMAL(12,4) DEFAULT 0;
    DECLARE v_lineas INT DEFAULT 0;
    SELECT IFNULL(SUM(debePolizaDetalle),0), IFNULL(SUM(haberPolizaDetalle),0), COUNT(*)
      INTO v_debe, v_haber, v_lineas
      FROM tblPolizaDetalle
     WHERE Fk_PolizaEncabezado_Pk_PolizaEncabezado = p_Pk_PolizaEncabezado
       AND is_active = TRUE;
    RETURN (v_lineas >= 2 AND v_debe = v_haber AND v_debe > 0);
END$$

DROP FUNCTION IF EXISTS fnEsCuentaImputable$$
CREATE FUNCTION fnEsCuentaImputable (p_Pk_Cuenta INT)
RETURNS BOOLEAN
READS SQL DATA
BEGIN
    DECLARE v_activa BOOLEAN DEFAULT FALSE;
    DECLARE v_hijos  INT     DEFAULT 0;
    SELECT IFNULL(MAX(is_active), FALSE) INTO v_activa
      FROM tblCuenta WHERE Pk_Cuenta = p_Pk_Cuenta;
    SELECT COUNT(*) INTO v_hijos
      FROM tblCuenta WHERE Fk_Cuenta_Padre_Cuenta = p_Pk_Cuenta AND is_active = TRUE;
    RETURN (v_activa = TRUE AND v_hijos = 0);
END$$

-- =============================================================================
--  7. PROCEDIMIENTOS ALMACENADOS
-- =============================================================================

DROP PROCEDURE IF EXISTS spInsertarPolizaBancos$$
CREATE PROCEDURE spInsertarPolizaBancos (
    IN  p_documentoOrigen     VARCHAR(30),
    IN  p_fechaMovimiento     DATE,
    IN  p_descripcion         VARCHAR(255),
    IN  p_Fk_CuentaBanco      INT,
    IN  p_Fk_CuentaContraparte INT,
    IN  p_monto               DECIMAL(12,4),
    IN  p_tipoMovimiento      VARCHAR(10),
    OUT p_codigoPoliza        VARCHAR(30),
    OUT p_resultado           VARCHAR(255)
)
sp: BEGIN
    DECLARE v_Pk_Poliza   INT;
    DECLARE v_periodo     CHAR(7);
    DECLARE v_existente   VARCHAR(30) DEFAULT NULL;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        SET p_codigoPoliza = NULL;
        SET p_resultado = 'ERROR: el traslado fue revertido, no se registro ninguna poliza.';
    END;

    IF p_documentoOrigen IS NULL OR TRIM(p_documentoOrigen) = '' THEN
        SET p_resultado = 'ERROR: el documento de origen es obligatorio para el control de duplicados.';
        LEAVE sp;
    END IF;

    IF p_tipoMovimiento NOT IN ('INGRESO','EGRESO') THEN
        SET p_resultado = 'ERROR: el tipo de movimiento debe ser INGRESO o EGRESO.';
        LEAVE sp;
    END IF;

    IF p_monto IS NULL OR p_monto <= 0 THEN
        SET p_resultado = 'ERROR: el monto debe ser mayor a cero.';
        LEAVE sp;
    END IF;

    IF p_Fk_CuentaBanco = p_Fk_CuentaContraparte THEN
        SET p_resultado = 'ERROR: la cuenta de banco y la contrapartida no pueden ser la misma.';
        LEAVE sp;
    END IF;

    IF NOT fnEsCuentaImputable(p_Fk_CuentaBanco) OR NOT fnEsCuentaImputable(p_Fk_CuentaContraparte) THEN
        SET p_resultado = 'ERROR: solo se permiten cuentas activas de ultimo nivel (hoja).';
        LEAVE sp;
    END IF;

    SELECT codigoPolizaEncabezado INTO v_existente
      FROM tblPolizaEncabezado
     WHERE origenPolizaEncabezado = 'BANCOS'
       AND documentoOrigenPolizaEncabezado = p_documentoOrigen
     LIMIT 1;

    IF v_existente IS NOT NULL THEN
        SET p_codigoPoliza = v_existente;
        SET p_resultado = CONCAT('DUPLICADO: el documento ', p_documentoOrigen,
                                 ' ya fue trasladado en la poliza ', v_existente,
                                 '. No se creo un nuevo registro.');
        LEAVE sp;
    END IF;

    SET v_periodo = DATE_FORMAT(p_fechaMovimiento, '%Y-%m');

    START TRANSACTION;

        SET p_codigoPoliza = fnGenerarCodigoPoliza(v_periodo);

        INSERT INTO tblPolizaEncabezado
            (codigoPolizaEncabezado, fechaPolizaEncabezado, periodoContablePolizaEncabezado, estadoPolizaEncabezado,
             origenPolizaEncabezado, documentoOrigenPolizaEncabezado, descripcionPolizaEncabezado)
        VALUES
            (p_codigoPoliza, p_fechaMovimiento, v_periodo, 'BORRADOR',
             'BANCOS', p_documentoOrigen, p_descripcion);

        SET v_Pk_Poliza = LAST_INSERT_ID();

        IF p_tipoMovimiento = 'INGRESO' THEN
            INSERT INTO tblPolizaDetalle
                (Fk_PolizaEncabezado_Pk_PolizaEncabezado, Fk_Cuenta_Pk_Cuenta, lineaPolizaDetalle, debePolizaDetalle, haberPolizaDetalle, descripcionPolizaDetalle)
            VALUES
                (v_Pk_Poliza, p_Fk_CuentaBanco,       1, p_monto, 0, CONCAT('Ingreso bancario doc. ', p_documentoOrigen)),
                (v_Pk_Poliza, p_Fk_CuentaContraparte, 2, 0, p_monto, CONCAT('Contrapartida doc. ', p_documentoOrigen));
        ELSE
            INSERT INTO tblPolizaDetalle
                (Fk_PolizaEncabezado_Pk_PolizaEncabezado, Fk_Cuenta_Pk_Cuenta, lineaPolizaDetalle, debePolizaDetalle, haberPolizaDetalle, descripcionPolizaDetalle)
            VALUES
                (v_Pk_Poliza, p_Fk_CuentaContraparte, 1, p_monto, 0, CONCAT('Contrapartida doc. ', p_documentoOrigen)),
                (v_Pk_Poliza, p_Fk_CuentaBanco,       2, 0, p_monto, CONCAT('Egreso bancario doc. ', p_documentoOrigen));
        END IF;

        IF NOT fnValidarCuadrePoliza(v_Pk_Poliza) THEN
            ROLLBACK;
            SET p_codigoPoliza = NULL;
            SET p_resultado = 'ERROR: la poliza no cuadra (debe <> haber). Traslado revertido.';
            LEAVE sp;
        END IF;

        UPDATE tblPolizaEncabezado
           SET estadoPolizaEncabezado = 'APLICADA'
         WHERE Pk_PolizaEncabezado = v_Pk_Poliza;

    COMMIT;

    SET p_resultado = CONCAT('OK: poliza ', p_codigoPoliza, ' registrada y aplicada por Q',
                             FORMAT(p_monto, 4), '.');
END$$

DROP PROCEDURE IF EXISTS spAnularPoliza$$
CREATE PROCEDURE spAnularPoliza (
    IN  p_codigoPoliza VARCHAR(30),
    IN  p_motivo       VARCHAR(255),
    OUT p_resultado    VARCHAR(255)
)
sp: BEGIN
    DECLARE v_estado VARCHAR(30) DEFAULT NULL;

    SELECT estadoPolizaEncabezado INTO v_estado
      FROM tblPolizaEncabezado WHERE codigoPolizaEncabezado = p_codigoPoliza LIMIT 1;

    IF v_estado IS NULL THEN
        SET p_resultado = CONCAT('ERROR: no existe la poliza ', p_codigoPoliza, '.');
        LEAVE sp;
    END IF;

    IF v_estado = 'ANULADA' THEN
        SET p_resultado = CONCAT('AVISO: la poliza ', p_codigoPoliza, ' ya estaba anulada.');
        LEAVE sp;
    END IF;

    UPDATE tblPolizaEncabezado
       SET estadoPolizaEncabezado      = 'ANULADA',
           is_active                   = FALSE,
           descripcionPolizaEncabezado = CONCAT(IFNULL(descripcionPolizaEncabezado,''), ' | ANULADA: ', IFNULL(p_motivo,'sin motivo'))
     WHERE codigoPolizaEncabezado = p_codigoPoliza;

    SET p_resultado = CONCAT('OK: poliza ', p_codigoPoliza, ' anulada.');
END$$

-- =============================================================================
--  8. TRIGGERS
-- =============================================================================

DROP TRIGGER IF EXISTS trPolizaEncabezadoBeforeUpdate$$
CREATE TRIGGER trPolizaEncabezadoBeforeUpdate
BEFORE UPDATE ON tblPolizaEncabezado
FOR EACH ROW
BEGIN
    IF OLD.estadoPolizaEncabezado = 'ANULADA' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se puede modificar una poliza ANULADA.';
    END IF;

    IF OLD.estadoPolizaEncabezado = 'APLICADA' AND NEW.estadoPolizaEncabezado <> 'ANULADA' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Una poliza APLICADA solo puede cambiar al estado ANULADA.';
    END IF;

    IF NEW.codigoPolizaEncabezado <> OLD.codigoPolizaEncabezado
       OR NOT (NEW.documentoOrigenPolizaEncabezado <=> OLD.documentoOrigenPolizaEncabezado) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'El correlativo y el documento de origen no son modificables.';
    END IF;
END$$

DROP TRIGGER IF EXISTS trPolizaDetalleBeforeInsert$$
CREATE TRIGGER trPolizaDetalleBeforeInsert
BEFORE INSERT ON tblPolizaDetalle
FOR EACH ROW
BEGIN
    DECLARE v_estado VARCHAR(30);
    SELECT estadoPolizaEncabezado INTO v_estado
      FROM tblPolizaEncabezado WHERE Pk_PolizaEncabezado = NEW.Fk_PolizaEncabezado_Pk_PolizaEncabezado;

    IF v_estado <> 'BORRADOR' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Solo se pueden agregar lineas a polizas en estado BORRADOR.';
    END IF;

    IF NOT fnEsCuentaImputable(NEW.Fk_Cuenta_Pk_Cuenta) THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'Solo se permiten cuentas activas de ultimo nivel (hoja).';
    END IF;
END$$

DROP TRIGGER IF EXISTS trPolizaDetalleBeforeUpdate$$
CREATE TRIGGER trPolizaDetalleBeforeUpdate
BEFORE UPDATE ON tblPolizaDetalle
FOR EACH ROW
BEGIN
    DECLARE v_estado VARCHAR(30);
    SELECT estadoPolizaEncabezado INTO v_estado
      FROM tblPolizaEncabezado WHERE Pk_PolizaEncabezado = OLD.Fk_PolizaEncabezado_Pk_PolizaEncabezado;

    IF v_estado <> 'BORRADOR' THEN
        SIGNAL SQLSTATE '45000'
            SET MESSAGE_TEXT = 'No se pueden modificar lineas de una poliza aplicada o anulada.';
    END IF;
END$$

DROP TRIGGER IF EXISTS trPolizaDetalleBeforeDelete$$
CREATE TRIGGER trPolizaDetalleBeforeDelete
BEFORE DELETE ON tblPolizaDetalle
FOR EACH ROW
BEGIN
    SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Por política del módulo de Contabilidad, se prefiere la anulación lógica (is_active = FALSE) sobre el DELETE físico.';
END$$

DELIMITER ;

-- =============================================================================
--  9. VISTA DE CONTROL DE TRASLADOS
-- =============================================================================
CREATE OR REPLACE VIEW vwPolizaTraslado AS
SELECT  pe.origenPolizaEncabezado            AS moduloOrigen,
        pe.documentoOrigenPolizaEncabezado   AS documentoOrigen,
        pe.codigoPolizaEncabezado            AS codigoPoliza,
        pe.fechaPolizaEncabezado             AS fechaPoliza,
        pe.estadoPolizaEncabezado            AS estadoPoliza,
        SUM(pd.debePolizaDetalle)            AS montoTrasladado,
        pe.created_at                        AS fechaTraslado
FROM    tblPolizaEncabezado pe
JOIN    tblPolizaDetalle    pd ON pd.Fk_PolizaEncabezado_Pk_PolizaEncabezado = pe.Pk_PolizaEncabezado
WHERE   pe.documentoOrigenPolizaEncabezado IS NOT NULL
GROUP BY pe.Pk_PolizaEncabezado, pe.origenPolizaEncabezado, pe.documentoOrigenPolizaEncabezado,
         pe.codigoPolizaEncabezado, pe.fechaPolizaEncabezado, pe.estadoPolizaEncabezado, pe.created_at;