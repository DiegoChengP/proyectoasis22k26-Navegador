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
