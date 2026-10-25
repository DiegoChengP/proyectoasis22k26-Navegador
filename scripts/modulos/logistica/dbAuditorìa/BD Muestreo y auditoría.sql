CREATE TABLE tblAuditoriaInventario (
    idAuditoriaInventario INT AUTO_INCREMENT PRIMARY KEY,
    fkMovimientoInventarioIdMovimientoInventario INT NOT NULL,
    fechaAuditoriaInventario DATETIME NOT NULL,
    resultadoAuditoriaInventario VARCHAR(50) NOT NULL,
    observacionesCalidadAuditoriaInventario TEXT NOT NULL,
    detallesAuditoriaInventario TEXT NOT NULL,
    isActive BOOLEAN DEFAULT TRUE,
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Fk_Auditoria_Movimiento FOREIGN KEY (fkMovimientoInventarioIdMovimientoInventario) REFERENCES tblMovimientoInventario(idMovimientoInventario)
);
