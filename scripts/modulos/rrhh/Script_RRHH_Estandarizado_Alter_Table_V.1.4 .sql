CREATE DATABASE IF NOT EXISTS `dbsistemaembutidos` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `dbsistemaembutidos`;

CREATE TABLE `tblempleado` (
  `idEmpleado` int NOT NULL AUTO_INCREMENT,
  `codigoEmpleado` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `dpiEmpleado` char(13) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nitEmpleado` char(10) COLLATE utf8mb4_unicode_ci NOT NULL,
  `nombresEmpleado` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `apellidosEmpleado` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `puestoEmpleado` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `generoEmpleado` char(1) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'M = Masculino, F = Femenino',
  `fechaNacimientoEmpleado` date NOT NULL,
  `fechaContratacionEmpleado` date NOT NULL,
  `telefonoEmpleado` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `correoEmpleado` varchar(150) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idEmpleado`),
  UNIQUE KEY `uqCodigoEmpleado` (`codigoEmpleado`),
  UNIQUE KEY `uqDpiEmpleado` (`dpiEmpleado`),
  UNIQUE KEY `uqNitEmpleado` (`nitEmpleado`),
  UNIQUE KEY `uqCorreoEmpleado` (`correoEmpleado`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblrol` (
  `idRol` int NOT NULL AUTO_INCREMENT,
  `nombreRol` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcionRol` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idRol`),
  UNIQUE KEY `uqNombreRol` (`nombreRol`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblmodulo` (
  `idModulo` int NOT NULL AUTO_INCREMENT,
  `nombreModulo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcionModulo` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idModulo`),
  UNIQUE KEY `uqNombreModulo` (`nombreModulo`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblaplicacion` (
  `idAplicacion` int NOT NULL AUTO_INCREMENT,
  `idModulo` int NOT NULL,
  `nombreAplicacion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `descripcionAplicacion` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idAplicacion`),
  KEY `idxAplicacionIdModulo` (`idModulo`),
  CONSTRAINT `Fk_Aplicacion_Modulo` FOREIGN KEY (`idModulo`) REFERENCES `tblmodulo` (`idModulo`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblusuario` (
  `idUsuario` int NOT NULL AUTO_INCREMENT,
  `idEmpleado` int NOT NULL,
  `nombreUsuario` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `contrasenaUsuario` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ultimoAccesoUsuario` datetime NOT NULL,
  `is_active` tinyint(1) NOT NULL DEFAULT '1',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idUsuario`),
  UNIQUE KEY `uqIdEmpleadoUsuario` (`idEmpleado`),
  UNIQUE KEY `uqUsuarioUsuario` (`nombreUsuario`),
  KEY `idxUsuarioIdEmpleado` (`idEmpleado`),
  CONSTRAINT `Fk_Usuario_Empleado` FOREIGN KEY (`idEmpleado`) REFERENCES `tblempleado` (`idEmpleado`) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblrecuperacioncontrasena` (
  `idRecuperacionContrasena` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int NOT NULL,
  `tokenRecuperacionContrasena` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `fechaExpiracionRecuperacionContrasena` datetime NOT NULL,
  `usadoRecuperacionContrasena` tinyint(1) NOT NULL DEFAULT '0',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idRecuperacionContrasena`),
  UNIQUE KEY `uqTokenRecuperacionContrasena` (`tokenRecuperacionContrasena`),
  KEY `idxRecuperacionContrasenaIdUsuario` (`idUsuario`),
  CONSTRAINT `Fk_RecuperacionContrasena_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tblusuario` (`idUsuario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblusuariorol` (
  `idUsuario` int NOT NULL,
  `idRol` int NOT NULL,
  `fechaAsignacionUsuarioRol` date NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idUsuario`,`idRol`),
  KEY `idxUsuarioRolIdRol` (`idRol`),
  CONSTRAINT `Fk_UsuarioRol_Rol` FOREIGN KEY (`idRol`) REFERENCES `tblrol` (`idRol`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Fk_UsuarioRol_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tblusuario` (`idUsuario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblrolmoduloaplicacion` (
  `idRol` int NOT NULL,
  `idModulo` int NOT NULL,
  `idAplicacion` int NOT NULL,
  `derInsertarRolModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derEditarRolModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derEliminarRolModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derImprimirRolModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idRol`,`idModulo`,`idAplicacion`),
  KEY `idxRolModAplIdModulo` (`idModulo`),
  KEY `idxRolModAplIdAplicacion` (`idAplicacion`),
  CONSTRAINT `Fk_RolModuloAplicacion_Aplicacion` FOREIGN KEY (`idAplicacion`) REFERENCES `tblaplicacion` (`idAplicacion`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Fk_RolModuloAplicacion_Modulo` FOREIGN KEY (`idModulo`) REFERENCES `tblmodulo` (`idModulo`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Fk_RolModuloAplicacion_Rol` FOREIGN KEY (`idRol`) REFERENCES `tblrol` (`idRol`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblusuariomoduloaplicacion` (
  `idUsuario` int NOT NULL,
  `idModulo` int NOT NULL,
  `idAplicacion` int NOT NULL,
  `derInsertarUsuarioModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derEditarUsuarioModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derEliminarUsuarioModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `derImprimirUsuarioModuloAplicacion` tinyint(1) NOT NULL DEFAULT '0',
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `updated_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`idUsuario`,`idModulo`,`idAplicacion`),
  KEY `idxUsrModAplIdModulo` (`idModulo`),
  KEY `idxUsrModAplIdAplicacion` (`idAplicacion`),
  CONSTRAINT `Fk_UsuarioModuloAplicacion_Aplicacion` FOREIGN KEY (`idAplicacion`) REFERENCES `tblaplicacion` (`idAplicacion`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Fk_UsuarioModuloAplicacion_Modulo` FOREIGN KEY (`idModulo`) REFERENCES `tblmodulo` (`idModulo`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `Fk_UsuarioModuloAplicacion_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tblusuario` (`idUsuario`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE `tblbitacora` (
  `idBitacora` int NOT NULL AUTO_INCREMENT,
  `idUsuario` int DEFAULT NULL,
  `accionBitacora` varchar(30) COLLATE utf8mb4_unicode_ci NOT NULL,
  `tablaBitacora` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `idRegistroBitacora` int NOT NULL,
  `detallesBitacora` text COLLATE utf8mb4_unicode_ci NOT NULL,
  `ipBitacora` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `fechaHoraBitacora` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `created_at` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`idBitacora`),
  KEY `idxBitacoraIdUsuario` (`idUsuario`),
  KEY `idxBitacoraFechaHora` (`fechaHoraBitacora`),
  KEY `idxBitacoraTabla` (`tablaBitacora`),
  CONSTRAINT `Fk_Bitacora_Usuario` FOREIGN KEY (`idUsuario`) REFERENCES `tblusuario` (`idUsuario`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=249 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

INSERT INTO `tblempleado` VALUES (1,'EMP-001','1234567890123','1234567-K','Isabel','Meléndez','Coordinadora de Implementación','F','2000-05-14','2026-01-10','55512345','isabel@gmail.com',1,'2026-09-17 14:07:07','2026-09-23 23:40:07'),(2,'EMP-002','2345678901234','2345678K','Carlos','Ramírez','Analista de Sistemas','M','1998-03-22','2025-11-05','55523456','carlos.ramirez@terminus.com',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(3,'EMP-003','3456789012345','3456789K','María','López','Administradora de Base de Datos','F','1995-09-30','2025-08-19','55534567','maria.lopez@terminus.com',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(13,'EMP-006','8463778648763','3727836-7','Oscar Emilio','Morales Lemus','Gerente','M','2005-05-28','2026-09-21','32323232','oscaremilio@gmail.com',1,'2026-09-22 04:30:17','2026-09-22 04:30:17'),(14,'EMP-007','5463212315453','5454542-1','Empleado','Examen','Pruebas','F','1990-09-22','2026-09-22','87897654','prueba@gmail.com',1,'2026-09-22 13:11:30','2026-09-22 13:11:30'),(19,'EMP-010','9876543219876','9876543-2','Pruebas','Capacitacion','Probador','M','1990-01-01','2026-01-10','12345678','terminus.seguridad22026@gmail.com',1,'2026-09-23 23:42:57','2026-09-23 23:42:57');

INSERT INTO `tblrol` VALUES (1,'Administrador','Rol con acceso total al sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,'Supervisor','Rol con acceso a reportes y aprobaciones',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(11,'Pruebas','pruebas del sistema',0,'2026-09-18 21:45:38','2026-09-18 21:45:38'),(12,'PerfilExamen','Permisos personalizados para el examen',1,'2026-09-22 13:16:29','2026-09-22 13:16:29'),(14,'Prueba','Esta es una prueba controlada por parte de Seguridad',1,'2026-09-23 23:47:34','2026-09-23 23:47:34');

INSERT INTO `tblmodulo` VALUES (1,'Producción','Módulo de control de producción de embutidos',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,'Inventario','Módulo de control de materia prima y producto terminado',1,'2026-09-17 14:07:07','2026-09-20 05:29:01'),(3,'Ventas','Módulo de gestión de pedidos y clientes',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(4,'Seguridad','Módulo de administración y seguridad del sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(13,'Pruebas','Esto es una prueba controlada por parte de Seguridad.',1,'2026-09-23 23:44:26','2026-09-23 23:44:26');

INSERT INTO `tblaplicacion` VALUES (3,3,'Gestión de Pedidos','Registro y seguimiento de pedidos de clientes',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(4,4,'Empleados','Gestión de empleados del sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(5,4,'Usuarios','Gestión de usuarios del sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(6,4,'Módulos','Administración de módulos del sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(7,4,'Aplicaciones','Administración de aplicaciones del sistema',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(8,4,'Perfiles','Administración de roles/perfiles',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(9,4,'Asignación de Perfiles','Asignación de roles a usuarios',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(10,4,'Asignación Aplicación Perfiles','Asignación de permisos por rol/módulo/aplicación',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(11,4,'Asignación Aplicación Usuario','Asignación de permisos por usuario/módulo/aplicación',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(12,4,'Bitácora','Consulta de bitácora del sistema',1,'2026-09-17 14:07:07','2026-09-20 02:48:38'),(15,4,'Prueba','Esto es una prueba controlada por parte de Seguridad.',1,'2026-09-23 23:46:12','2026-09-23 23:46:12');

INSERT INTO `tblusuario` VALUES (1,1,'imelendez','$2b$10$e9CQKArQRmpf.IUtqpxYmOoO9vyh2./7ped56qEokwfgn7DkcacPC','2026-09-17 08:07:07',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,2,'cramirez','$2b$10$ojN1MyPkUaylOuupBEGmhemdx5tQi4BdbbJrpSF2ZJjD0ZWM/lbe2','2026-09-17 08:07:07',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(3,3,'mlopez','$2b$10$Bo9RDmJRGKKLxm1tSmRO1ODP0CiGmuqy/wcnJ6bs2MkOVe1OQml5i','2026-09-17 08:07:07',1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(12,14,'UsuarioExamen','$2a$11$m6Fi2WikM6YW5QbVZikQBuxMCXRfLjnxPleDRY75FLmVjYncKxEc6','2026-09-22 07:12:30',1,'2026-09-22 13:12:30','2026-09-22 13:12:30'),(13,19,'Pcapacitacion','$2a$11$Vee.0P876yc/Mnwy2ePINeGVsQoOLofGZT30VkeJz/B1y3ND7tSAq','2026-09-23 17:43:51',1,'2026-09-23 23:43:51','2026-09-23 23:43:51');

INSERT INTO `tblrecuperacioncontrasena` VALUES (1,1,'TOKEN-A1B2C3D4','2026-09-18 08:07:07',0,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,2,'TOKEN-E5F6G7H8','2026-09-18 08:07:07',0,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(3,3,'TOKEN-I9J0K1L2','2026-09-18 08:07:07',1,'2026-09-17 14:07:07','2026-09-17 14:07:07');

INSERT INTO `tblusuariorol` VALUES (1,1,'2026-09-17','2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,2,'2026-09-17','2026-09-17 14:07:07','2026-09-17 14:07:07'),(3,2,'2026-09-17','2026-09-17 14:09:00','2026-09-17 14:09:00'),(12,12,'2026-09-22','2026-09-22 13:22:32','2026-09-22 13:22:32'),(13,14,'2026-09-23','2026-09-23 23:47:47','2026-09-23 23:47:47');

INSERT INTO `tblrolmoduloaplicacion` VALUES (1,4,4,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:19:43'),(1,4,5,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,6,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,7,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,8,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,9,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,10,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,11,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(1,4,12,1,1,1,1,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(2,4,4,1,1,0,0,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(12,4,8,1,1,0,0,'2026-09-22 13:20:38','2026-09-22 13:20:38'),(14,4,15,1,1,1,1,'2026-09-23 23:48:40','2026-09-23 23:48:40');

INSERT INTO `tblusuariomoduloaplicacion` VALUES (3,3,3,1,0,0,0,'2026-09-17 14:07:07','2026-09-17 14:07:07'),(13,13,15,0,0,0,0,'2026-09-23 23:49:15','2026-09-23 23:49:15');

INSERT INTO `tblbitacora` VALUES (1,1,'INSERT','tblEmpleado',1,'Alta de empleado de prueba','192.168.1.10','2026-09-17 08:07:07','2026-09-17 14:07:07'),(2,2,'UPDATE','tblAplicacion',2,'Actualización de datos de aplicación','192.168.1.11','2026-09-17 08:07:07','2026-09-17 14:07:07'),(3,3,'DELETE','tblUsuarioRol',3,'Eliminación de asignación de rol','192.168.1.12','2026-09-17 08:07:07','2026-09-17 14:07:07'),(4,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-17 08:07:53','2026-09-17 14:07:53'),(5,1,'INSERT','tblUsuarioRol',3,'Se asignó el perfil 2 al usuario 3','192.168.167.48','2026-09-17 08:09:00','2026-09-17 14:09:00'),(6,1,'INSERT','tblEmpleado',0,'Se agregó el empleado: Oscar Moralesl','192.168.167.48','2026-09-17 08:13:37','2026-09-17 14:13:37'),(7,1,'INSERT','tblUsuario',0,'Se agregó el usuario: SofiGoty','192.168.167.48','2026-09-17 08:14:24','2026-09-17 14:14:24'),(8,1,'INSERT','tblUsuarioRol',4,'Se asignó el perfil 1 al usuario 4','192.168.167.48','2026-09-17 08:14:33','2026-09-17 14:14:33'),(9,1,'LOGIN','tblUsuario',4,'Inicio de sesión exitoso del usuario: SofiGoty','192.168.167.48','2026-09-17 08:14:55','2026-09-17 14:14:55'),(10,1,'UPDATE','tblAsigAppPerf',1,'Se actualizaron permisos de aplicación 4 al rol 1','192.168.167.48','2026-09-17 08:16:33','2026-09-17 14:16:33'),(11,1,'LOGIN','tblUsuario',4,'Inicio de sesión exitoso del usuario: SofiGoty','192.168.167.48','2026-09-17 08:17:20','2026-09-17 14:17:20'),(12,1,'UPDATE','tblAsigAppPerf',1,'Se actualizaron permisos de aplicación 4 al rol 1','192.168.167.48','2026-09-17 08:19:43','2026-09-17 14:19:43'),(13,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-17 08:22:18','2026-09-17 14:22:18'),(14,1,'DELETE','tblEmpleado',4,'Se eliminó el empleado ID: 4','192.168.167.48','2026-09-17 08:23:26','2026-09-17 14:23:26'),(15,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-17 08:27:14','2026-09-17 14:27:14'),(16,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-17 13:53:54','2026-09-17 19:53:54'),(17,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-17 14:04:42','2026-09-17 20:04:42'),(18,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-17 19:21:21','2026-09-18 01:21:21'),(19,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-17 19:51:48','2026-09-18 01:51:48'),(20,1,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.11','2026-09-17 19:53:15','2026-09-18 01:53:16'),(21,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-17 19:53:42','2026-09-18 01:53:42'),(22,1,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.11','2026-09-18 08:24:51','2026-09-18 14:24:51'),(23,NULL,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.11','2026-09-18 08:44:12','2026-09-18 14:44:12'),(24,NULL,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 08:44:48','2026-09-18 14:44:48'),(25,NULL,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.11','2026-09-18 08:58:45','2026-09-18 14:58:45'),(26,NULL,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 08:58:59','2026-09-18 14:58:59'),(27,NULL,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 09:04:20','2026-09-18 15:04:20'),(28,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 09:07:48','2026-09-18 15:07:48'),(29,2,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.11','2026-09-18 09:08:15','2026-09-18 15:08:15'),(30,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 09:08:38','2026-09-18 15:08:38'),(31,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 09:17:02','2026-09-18 15:17:02'),(32,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 11:41:10','2026-09-18 17:41:10'),(33,1,'INSERT','tblEmpleado',0,'Se agregó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-18 11:48:58','2026-09-18 17:48:58'),(34,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-18 11:49:13','2026-09-18 17:49:13'),(35,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-18 11:49:54','2026-09-18 17:49:55'),(36,1,'INSERT','tblEmpleado',0,'Se agregó el empleado: dfdsf dsfdsf','192.168.1.11','2026-09-18 11:58:46','2026-09-18 17:58:46'),(37,1,'UPDATE','tblEmpleado',8,'Se actualizó el empleado: dfdsf dsfdsf','192.168.1.11','2026-09-18 11:59:21','2026-09-18 17:59:21'),(38,1,'DELETE','tblEmpleado',8,'Se eliminó el empleado ID: 8','192.168.1.11','2026-09-18 11:59:25','2026-09-18 17:59:25'),(39,1,'INSERT','tblUsuario',0,'Se agregó el usuario: Omorales','192.168.1.11','2026-09-18 12:02:42','2026-09-18 18:02:42'),(40,1,'INSERT','tblModulo',0,'Se agregó el módulo: Compras','192.168.1.11','2026-09-18 12:10:26','2026-09-18 18:10:26'),(41,1,'UPDATE','tblModulo',5,'Se actualizó el módulo: Compras','192.168.1.11','2026-09-18 12:10:33','2026-09-18 18:10:33'),(42,1,'DELETE','tblModulo',5,'Se eliminó el módulo ID: 5','192.168.1.11','2026-09-18 12:10:38','2026-09-18 18:10:38'),(43,1,'INSERT','tblModulo',5,'Se agregó el módulo: Comprasdfd','192.168.1.11','2026-09-18 12:10:52','2026-09-18 18:10:53'),(44,1,'UPDATE','tblModulo',6,'Se actualizó el módulo: Comprasdfd','192.168.1.11','2026-09-18 12:10:56','2026-09-18 18:10:56'),(45,1,'UPDATE','tblModulo',4,'Se actualizó el módulo: Seguridad','192.168.1.11','2026-09-18 12:11:19','2026-09-18 18:11:19'),(46,1,'DELETE','tblModulo',6,'Se eliminó el módulo ID: 6','192.168.1.11','2026-09-18 12:13:28','2026-09-18 18:13:28'),(47,1,'INSERT','tblAplicacion',0,'Se agregó la aplicación: fdsfsdfdsf','192.168.1.11','2026-09-18 12:24:05','2026-09-18 18:24:05'),(48,1,'UPDATE','tblAplicacion',13,'Se actualizó la aplicación: fdsfsdfds','192.168.1.11','2026-09-18 12:24:20','2026-09-18 18:24:20'),(49,1,'DELETE','tblAplicacion',13,'Se eliminó la aplicación ID: 13','192.168.1.11','2026-09-18 12:24:25','2026-09-18 18:24:25'),(50,1,'INSERT','tblAplicacion',13,'Se agregó la aplicación: Perfiles','192.168.1.11','2026-09-18 12:24:41','2026-09-18 18:24:41'),(51,1,'INSERT','tblModulo',0,'Se agregó el módulo: dsolpkojdfkdsf','192.168.1.11','2026-09-18 12:26:47','2026-09-18 18:26:47'),(52,1,'DELETE','tblModulo',8,'Se eliminó el módulo ID: 8','192.168.1.11','2026-09-18 12:26:51','2026-09-18 18:26:51'),(53,1,'DELETE','tblAplicacion',14,'Se eliminó la aplicación ID: 14','192.168.1.11','2026-09-18 12:27:35','2026-09-18 18:27:35'),(54,1,'INSERT','tblRol',0,'Se agregó el perfil: Probador','192.168.1.11','2026-09-18 12:30:24','2026-09-18 18:30:24'),(55,1,'UPDATE','tblRol',5,'Se actualizó el perfil: Probador','192.168.1.11','2026-09-18 12:33:51','2026-09-18 18:33:51'),(56,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 12:42:44','2026-09-18 18:42:44'),(57,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 12:43:19','2026-09-18 18:43:19'),(58,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 12:43:56','2026-09-18 18:43:56'),(59,1,'DELETE','tblAsigAppPerf',3,'Se eliminó asignación de aplicación 3 al rol 3','192.168.1.11','2026-09-18 12:44:12','2026-09-18 18:44:12'),(60,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 12:45:34','2026-09-18 18:45:34'),(61,1,'UPDATE','tblAsigAppPerf',2,'Se actualizaron permisos de aplicación 2 al rol 2','192.168.1.11','2026-09-18 12:46:57','2026-09-18 18:46:57'),(62,1,'DELETE','tblAsigAppPerf',2,'Se eliminó asignación de aplicación 5 al rol 2','192.168.1.11','2026-09-18 12:47:07','2026-09-18 18:47:07'),(63,1,'INSERT','tblAsigAppUsuario',1,'Se asignó aplicación 1 al módulo 1','192.168.1.11','2026-09-18 12:48:09','2026-09-18 18:48:09'),(64,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 15:40:45','2026-09-18 21:40:45'),(65,1,'UPDATE','tblAsigAppPerf',1,'Se actualizaron permisos de aplicación 1 al rol 1','192.168.1.11','2026-09-18 15:41:54','2026-09-18 21:41:54'),(66,1,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 15:44:36','2026-09-18 21:44:36'),(67,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 15:44:58','2026-09-18 21:44:59'),(68,1,'INSERT','tblRol',0,'Se agregó el perfil: Pruebas','192.168.1.11','2026-09-18 15:45:38','2026-09-18 21:45:38'),(69,1,'DELETE','tblRol',5,'Se eliminó el rol perfil: 5','192.168.1.11','2026-09-18 15:45:45','2026-09-18 21:45:45'),(70,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 15:46:11','2026-09-18 21:46:11'),(71,1,'INSERT','tblUsuarioRol',5,'Se asignó el perfil 3 al usuario 5','192.168.1.11','2026-09-18 15:56:26','2026-09-18 21:56:27'),(72,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 11 al rol 3','192.168.1.11','2026-09-18 15:57:29','2026-09-18 21:57:29'),(73,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 10 al rol 3','192.168.1.11','2026-09-18 15:57:37','2026-09-18 21:57:37'),(74,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 8 al rol 3','192.168.1.11','2026-09-18 15:57:43','2026-09-18 21:57:43'),(75,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 7 al rol 3','192.168.1.11','2026-09-18 15:57:51','2026-09-18 21:57:51'),(76,1,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 15:58:13','2026-09-18 21:58:13'),(77,1,'INSERT','tblAsigAppPerf',1,'Se asignó aplicación 1 al rol 1','192.168.1.11','2026-09-18 15:58:39','2026-09-18 21:58:39'),(78,1,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 16:13:17','2026-09-18 22:13:17'),(79,1,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 17:43:22','2026-09-18 23:43:22'),(80,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 5 al rol 3','192.168.1.11','2026-09-18 17:44:16','2026-09-18 23:44:16'),(81,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 6 al rol 3','192.168.1.11','2026-09-18 17:44:33','2026-09-18 23:44:33'),(82,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 4 al rol 3','192.168.1.11','2026-09-18 17:44:53','2026-09-18 23:44:53'),(83,NULL,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 18:19:05','2026-09-19 00:19:05'),(84,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 18:19:24','2026-09-19 00:19:24'),(85,NULL,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 18:20:11','2026-09-19 00:20:11'),(86,NULL,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-18 18:20:32','2026-09-19 00:20:32'),(87,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 18:20:48','2026-09-19 00:20:48'),(88,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 19:58:29','2026-09-19 01:58:29'),(89,NULL,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 20:25:30','2026-09-19 02:25:30'),(90,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 20:38:01','2026-09-19 02:38:01'),(91,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-18 22:15:35','2026-09-19 04:15:35'),(92,1,'INSERT','tblAsigAppPerf',3,'Se asignó aplicación 12 al rol 3','192.168.1.11','2026-09-18 22:16:49','2026-09-19 04:16:49'),(93,NULL,'LOGIN','tblUsuario',5,'Inicio de sesión exitoso del usuario: Omorales','192.168.1.11','2026-09-18 22:17:09','2026-09-19 04:17:09'),(94,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 09:19:13','2026-09-19 15:19:13'),(95,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 16:24:37','2026-09-19 22:24:37'),(96,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 16:26:02','2026-09-19 22:26:02'),(97,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 16:45:59','2026-09-19 22:45:59'),(98,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-19 16:46:27','2026-09-19 22:46:27'),(99,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-19 16:46:42','2026-09-19 22:46:42'),(100,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-19 16:47:25','2026-09-19 22:47:25'),(101,1,'INSERT','tblEmpleado',5,'Se agregó el empleado: Pepito Muralles','192.168.1.11','2026-09-19 16:49:06','2026-09-19 22:49:06'),(102,1,'UPDATE','tblEmpleado',12,'Se actualizó el empleado: Pepito Muralles','192.168.1.11','2026-09-19 16:49:29','2026-09-19 22:49:29'),(103,1,'UPDATE','tblEmpleado',5,'Se actualizó el empleado: Oscar Emilio Morales Lemus','192.168.1.11','2026-09-19 16:50:24','2026-09-19 22:50:24'),(104,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 20:46:49','2026-09-20 02:46:49'),(105,1,'UPDATE','tblEmpleado',12,'Se actualizó el empleado: Pepito Muralles','192.168.1.11','2026-09-19 20:47:08','2026-09-20 02:47:08'),(106,1,'UPDATE','tblAplicacion',12,'Se actualizó la aplicación: Bitácora','192.168.1.11','2026-09-19 20:48:28','2026-09-20 02:48:28'),(107,1,'UPDATE','tblAplicacion',12,'Se actualizó la aplicación: Bitácora','192.168.1.11','2026-09-19 20:48:38','2026-09-20 02:48:38'),(108,1,'DELETE','tblAplicacion',1,'Se eliminó la aplicación ID: 1','192.168.1.11','2026-09-19 20:48:52','2026-09-20 02:48:52'),(109,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 20:49:17','2026-09-20 02:49:17'),(110,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 22:01:50','2026-09-20 04:01:50'),(111,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 22:33:27','2026-09-20 04:33:27'),(112,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 22:35:23','2026-09-20 04:35:23'),(113,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 22:36:26','2026-09-20 04:36:26'),(114,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 22:55:23','2026-09-20 04:55:23'),(115,1,'DELETE','tblRol',3,'Se eliminó el rol perfil: 3','192.168.1.11','2026-09-19 22:56:55','2026-09-20 04:56:55'),(116,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:25:33','2026-09-20 05:25:33'),(117,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:27:56','2026-09-20 05:27:56'),(118,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:28:33','2026-09-20 05:28:33'),(119,1,'UPDATE','tblModulo',2,'Se actualizó el módulo: Inventario','192.168.1.11','2026-09-19 23:28:56','2026-09-20 05:28:56'),(120,1,'UPDATE','tblModulo',2,'Se actualizó el módulo: Inventario','192.168.1.11','2026-09-19 23:29:01','2026-09-20 05:29:01'),(121,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:30:10','2026-09-20 05:30:10'),(122,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:43:23','2026-09-20 05:43:23'),(123,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:48:49','2026-09-20 05:48:49'),(124,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:50:17','2026-09-20 05:50:17'),(125,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:52:57','2026-09-20 05:52:58'),(126,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-19 23:54:44','2026-09-20 05:54:44'),(127,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:05:44','2026-09-20 06:05:44'),(128,1,'DELETE','tblAplicacion',2,'Se eliminó la aplicación ID: 2','192.168.1.11','2026-09-20 00:06:15','2026-09-20 06:06:15'),(129,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:11:20','2026-09-20 06:11:20'),(130,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:14:04','2026-09-20 06:14:04'),(131,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:17:19','2026-09-20 06:17:19'),(132,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:22:42','2026-09-20 06:22:42'),(133,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.11','2026-09-20 00:26:44','2026-09-20 06:26:44'),(134,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','172.20.10.4','2026-09-20 00:37:21','2026-09-20 06:37:21'),(135,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','172.20.10.4','2026-09-20 00:39:27','2026-09-20 06:39:27'),(136,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:40:49','2026-09-20 14:40:49'),(137,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:43:23','2026-09-20 14:43:23'),(138,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:44:51','2026-09-20 14:44:51'),(139,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:46:20','2026-09-20 14:46:20'),(140,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:47:12','2026-09-20 14:47:12'),(141,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:55:47','2026-09-20 14:55:48'),(142,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:57:12','2026-09-20 14:57:12'),(143,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:58:00','2026-09-20 14:58:00'),(144,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:58:46','2026-09-20 14:58:46'),(145,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 08:59:38','2026-09-20 14:59:38'),(146,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:14:45','2026-09-20 15:14:45'),(147,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:16:11','2026-09-20 15:16:11'),(148,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:26:36','2026-09-20 15:26:36'),(149,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:29:58','2026-09-20 15:29:58'),(150,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:31:54','2026-09-20 15:31:54'),(151,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:34:18','2026-09-20 15:34:18'),(152,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:35:27','2026-09-20 15:35:27'),(153,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:39:39','2026-09-20 15:39:39'),(154,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:42:23','2026-09-20 15:42:23'),(155,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:54:49','2026-09-20 15:54:49'),(156,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:57:32','2026-09-20 15:57:32'),(157,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 09:59:16','2026-09-20 15:59:16'),(158,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 10:04:06','2026-09-20 16:04:06'),(159,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 11:47:28','2026-09-20 17:47:28'),(160,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 12:23:34','2026-09-20 18:23:34'),(161,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 15:35:11','2026-09-20 21:35:11'),(162,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 15:41:55','2026-09-20 21:41:55'),(163,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 18:11:00','2026-09-21 00:11:00'),(164,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 18:14:17','2026-09-21 00:14:17'),(165,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 19:52:31','2026-09-21 01:52:31'),(166,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 20:24:57','2026-09-21 02:24:58'),(167,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-20 21:39:02','2026-09-21 03:39:02'),(168,1,'INSERT','tblUsuario',0,'Se agregó el usuario: Ppito','192.168.1.2','2026-09-20 21:39:53','2026-09-21 03:39:53'),(169,1,'UPDATE','tblUsuario',9,'Se actualizó el usuario: Ppito','192.168.1.2','2026-09-20 21:40:30','2026-09-21 03:40:30'),(170,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 15:21:33','2026-09-21 21:21:33'),(171,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 22:04:15','2026-09-22 04:04:15'),(172,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 22:09:15','2026-09-22 04:09:15'),(173,1,'UPDATE','tblUsuario',9,'Se actualizó el usuario: Ppitote','192.168.1.2','2026-09-21 22:09:53','2026-09-22 04:09:53'),(174,1,'DELETE','tblUsuario',9,'Se eliminó el usuario ID: 9','192.168.1.2','2026-09-21 22:09:59','2026-09-22 04:10:00'),(175,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 22:11:32','2026-09-22 04:11:32'),(176,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 22:24:51','2026-09-22 04:24:51'),(177,1,'DELETE','tblUsuario',5,'Se eliminó el usuario ID: 5','192.168.1.2','2026-09-21 22:25:15','2026-09-22 04:25:15'),(178,1,'DELETE','tblEmpleado',5,'Se eliminó el empleado ID: 5','192.168.1.2','2026-09-21 22:25:25','2026-09-22 04:25:25'),(179,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-21 22:29:12','2026-09-22 04:29:12'),(180,1,'INSERT','tblEmpleado',0,'Se agregó el empleado: Oscar Emilio Morales Lemus','192.168.1.2','2026-09-21 22:30:17','2026-09-22 04:30:17'),(181,1,'INSERT','tblUsuario',0,'Se agregó el usuario: Omorales','192.168.1.2','2026-09-21 22:30:38','2026-09-22 04:30:38'),(182,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-22 06:47:05','2026-09-22 12:47:05'),(183,1,'DELETE','tblUsuario',11,'Se eliminó el usuario ID: 11','192.168.167.48','2026-09-22 06:48:42','2026-09-22 12:48:42'),(184,1,'DELETE','tblEmpleado',12,'Se eliminó el empleado ID: 12','192.168.167.48','2026-09-22 06:50:50','2026-09-22 12:50:51'),(185,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-22 07:08:29','2026-09-22 13:08:29'),(186,1,'INSERT','tblEmpleado',0,'Se agregó el empleado: Empleado Examen','192.168.167.48','2026-09-22 07:11:30','2026-09-22 13:11:31'),(187,1,'INSERT','tblUsuario',0,'Se agregó el usuario: UsuarioExamen','192.168.167.48','2026-09-22 07:12:30','2026-09-22 13:12:30'),(188,1,'INSERT','tblRol',0,'Se agregó el perfil: PerfilExamen','192.168.167.48','2026-09-22 07:16:29','2026-09-22 13:16:29'),(189,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.167.48','2026-09-22 07:17:23','2026-09-22 13:17:23'),(190,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-22 07:19:39','2026-09-22 13:19:39'),(191,1,'INSERT','tblAsigAppPerf',12,'Se asignó aplicación 8 al rol 12','192.168.167.48','2026-09-22 07:20:38','2026-09-22 13:20:39'),(192,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.167.48','2026-09-22 07:21:11','2026-09-22 13:21:11'),(193,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-22 07:21:45','2026-09-22 13:21:45'),(194,1,'INSERT','tblUsuarioRol',12,'Se asignó el perfil 12 al usuario 12','192.168.167.48','2026-09-22 07:22:32','2026-09-22 13:22:32'),(195,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.167.48','2026-09-22 07:22:48','2026-09-22 13:22:48'),(196,12,'INSERT','tblRol',0,'Se agregó el perfil: Prueba','192.168.167.48','2026-09-22 07:25:13','2026-09-22 13:25:13'),(197,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.167.48','2026-09-22 07:26:54','2026-09-22 13:26:54'),(198,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 16:22:37','2026-09-22 22:22:37'),(199,1,'INSERT','tblModulo',0,'Se agregó el módulo: Contabilidad','192.168.1.2','2026-09-22 16:24:31','2026-09-22 22:24:31'),(200,1,'UPDATE','tblModulo',12,'Se actualizó el módulo: Contabilidad','192.168.1.2','2026-09-22 16:26:01','2026-09-22 22:26:01'),(201,1,'DELETE','tblModulo',12,'Se eliminó el módulo ID: 12','192.168.1.2','2026-09-22 16:28:47','2026-09-22 22:28:47'),(202,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 16:57:01','2026-09-22 22:57:01'),(203,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.1.2','2026-09-22 16:57:27','2026-09-22 22:57:27'),(204,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 17:48:41','2026-09-22 23:48:41'),(205,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 17:49:18','2026-09-22 23:49:18'),(206,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 17:52:57','2026-09-22 23:52:57'),(207,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 18:07:57','2026-09-23 00:07:57'),(208,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 18:09:35','2026-09-23 00:09:35'),(209,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.1.2','2026-09-22 18:10:11','2026-09-23 00:10:11'),(210,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 19:16:51','2026-09-23 01:16:51'),(211,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 20:21:32','2026-09-23 02:21:32'),(212,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 20:28:21','2026-09-23 02:28:21'),(213,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 20:39:51','2026-09-23 02:39:51'),(214,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 21:38:32','2026-09-23 03:38:32'),(215,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:32:12','2026-09-23 05:32:12'),(216,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.1.2','2026-09-22 23:33:18','2026-09-23 05:33:18'),(217,2,'LOGIN','tblUsuario',2,'Inicio de sesión exitoso del usuario: cramirez','192.168.1.2','2026-09-22 23:33:42','2026-09-23 05:33:42'),(218,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:34:17','2026-09-23 05:34:17'),(219,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.1.2','2026-09-22 23:38:03','2026-09-23 05:38:03'),(220,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:42:18','2026-09-23 05:42:18'),(221,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:43:12','2026-09-23 05:43:12'),(222,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:44:58','2026-09-23 05:44:58'),(223,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:47:01','2026-09-23 05:47:01'),(224,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:48:54','2026-09-23 05:48:54'),(225,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:51:43','2026-09-23 05:51:43'),(226,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:52:55','2026-09-23 05:52:55'),(227,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:53:56','2026-09-23 05:53:56'),(228,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:56:37','2026-09-23 05:56:37'),(229,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:57:34','2026-09-23 05:57:34'),(230,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-22 23:58:23','2026-09-23 05:58:23'),(231,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 00:00:00','2026-09-23 06:00:00'),(232,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 00:01:45','2026-09-23 06:01:45'),(233,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 00:03:49','2026-09-23 06:03:49'),(234,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 00:15:17','2026-09-23 06:15:17'),(235,12,'LOGIN','tblUsuario',12,'Inicio de sesión exitoso del usuario: UsuarioExamen','192.168.1.2','2026-09-23 00:15:49','2026-09-23 06:15:49'),(236,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 17:32:40','2026-09-23 23:32:40'),(237,1,'UPDATE','tblEmpleado',1,'Se actualizó el empleado: Isabel Meléndez','192.168.1.2','2026-09-23 17:40:07','2026-09-23 23:40:07'),(238,1,'INSERT','tblEmpleado',1,'Se agregó el empleado: Pruebas Capacitacion','192.168.1.2','2026-09-23 17:42:57','2026-09-23 23:42:57'),(239,1,'INSERT','tblUsuario',0,'Se agregó el usuario: Pcapacitacion','192.168.1.2','2026-09-23 17:43:51','2026-09-23 23:43:51'),(240,1,'INSERT','tblModulo',0,'Se agregó el módulo: Pruebas','192.168.1.2','2026-09-23 17:44:26','2026-09-23 23:44:26'),(241,1,'INSERT','tblAplicacion',0,'Se agregó la aplicación: Prueba','192.168.1.2','2026-09-23 17:46:12','2026-09-23 23:46:12'),(242,1,'DELETE','tblRol',13,'Se eliminó el rol perfil: 13','192.168.1.2','2026-09-23 17:46:19','2026-09-23 23:46:19'),(243,1,'UPDATE','tblRol',1,'Se actualizó el perfil: Administrador','192.168.1.2','2026-09-23 17:46:22','2026-09-23 23:46:22'),(244,1,'INSERT','tblRol',1,'Se agregó el perfil: Prueba','192.168.1.2','2026-09-23 17:47:34','2026-09-23 23:47:34'),(245,1,'INSERT','tblUsuarioRol',13,'Se asignó el perfil 14 al usuario 13','192.168.1.2','2026-09-23 17:47:47','2026-09-23 23:47:47'),(246,1,'INSERT','tblAsigAppPerf',14,'Se asignó aplicación 15 al rol 14','192.168.1.2','2026-09-23 17:48:40','2026-09-23 23:48:40'),(247,1,'INSERT','tblAsigAppUsuario',13,'Se asignó aplicación 15 al módulo 13','192.168.1.2','2026-09-23 17:49:15','2026-09-23 23:49:15'),(248,1,'LOGIN','tblUsuario',1,'Inicio de sesión exitoso del usuario: imelendez','192.168.1.2','2026-09-23 19:44:06','2026-09-24 01:44:07');




SELECT a.idModulo, a.idAplicacion, a.nombreAplicacion, r.nombreRol,
       rma.derInsertarRolModuloAplicacion AS Insertar,
       rma.derEditarRolModuloAplicacion AS Editar,
       rma.derEliminarRolModuloAplicacion AS Eliminar,
       rma.derImprimirRolModuloAplicacion AS Imprimir
FROM tblAplicacion a
LEFT JOIN tblRolModuloAplicacion rma ON rma.idAplicacion = a.idAplicacion AND rma.idModulo = a.idModulo
LEFT JOIN tblRol r ON r.idRol = rma.idRol
WHERE a.idModulo = (SELECT idModulo FROM tblModulo WHERE nombreModulo = 'Seguridad')
ORDER BY a.idAplicacion, r.nombreRol;
SELECT idAplicacion, nombreAplicacion
FROM tblAplicacion
WHERE idModulo = (SELECT idModulo FROM tblModulo WHERE nombreModulo = 'Seguridad')
ORDER BY idAplicacion;


-- ----------------------------- ¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡ PARA RECURSOS HUMANOS !!!!!!!!!!!!!!!!!!!!!!!!!-------------

USE dbSistemaEmbutidos;

-- ---------- Departamentos ----------
CREATE TABLE IF NOT EXISTS tblDepartamento (
    idDepartamento INT AUTO_INCREMENT NOT NULL,
    nombreDepartamento VARCHAR(100) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Departamento PRIMARY KEY (idDepartamento),
    CONSTRAINT uqNombreDepartamento UNIQUE (nombreDepartamento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Empresas (sistema multiempresa) ----------
CREATE TABLE tblEmpresa (
    idEmpresa INT AUTO_INCREMENT NOT NULL,
    nitEmpresa CHAR(10) NOT NULL,
    razonSocialEmpresa VARCHAR(100) NOT NULL,
    nombreComercialEmpresa VARCHAR(100) NULL,         -- opcional
    direccionEmpresa VARCHAR(500) NOT NULL,
    representanteLegalEmpresa VARCHAR(100) NOT NULL,  -- aparece en el finiquito
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Empresa PRIMARY KEY (idEmpresa),
    CONSTRAINT uqNitEmpresa UNIQUE (nitEmpresa)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Puestos y empleados ----------
CREATE TABLE tblTipoPuesto (
    idTipoPuesto INT AUTO_INCREMENT NOT NULL,
    nombreTipoPuesto VARCHAR(50) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TipoPuesto PRIMARY KEY (idTipoPuesto),
    CONSTRAINT uqNombreTipoPuesto UNIQUE (nombreTipoPuesto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

CREATE TABLE tblPuesto (
    idPuesto INT AUTO_INCREMENT NOT NULL,
    idTipoPuesto INT NOT NULL,
    nombrePuesto VARCHAR(100) NOT NULL,
    descripcionPuesto VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Puesto PRIMARY KEY (idPuesto),
    CONSTRAINT Fk_Puesto_TipoPuesto FOREIGN KEY (idTipoPuesto)
        REFERENCES tblTipoPuesto (idTipoPuesto) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqNombrePuesto UNIQUE (nombrePuesto)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ---------- Jornadas ----------

CREATE TABLE tblJornada (
    idJornada INT AUTO_INCREMENT NOT NULL,
    nombreJornada VARCHAR(50) NOT NULL,
    horaInicioJornada TIME NOT NULL,
    horaFinJornada TIME NOT NULL,
    minutosToleranciaJornada INT NOT NULL DEFAULT 5,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Jornada PRIMARY KEY (idJornada),
    CONSTRAINT uqNombreJornada UNIQUE (nombreJornada),
    CONSTRAINT ckJornadaHoras CHECK (horaFinJornada <> horaInicioJornada),
    CONSTRAINT ckJornadaTolerancia CHECK (minutosToleranciaJornada >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ---------- Datos de RRHH en el empleado ----------
-- 1) Datos base (padres)
INSERT INTO tblTipoPuesto (nombreTipoPuesto) VALUES ('Administrativo'), ('Operativo');

INSERT INTO tblPuesto (idTipoPuesto, nombrePuesto, descripcionPuesto) VALUES
(1, 'Coordinadora de Implementación', 'Coordina la implementación del sistema'),
(1, 'Analista de Sistemas', 'Análisis de sistemas'),
(1, 'Administradora de Base de Datos', 'Administración de BD'),
(1, 'Gerente', 'Gerencia general'),
(1, 'Pruebas', 'Puesto de pruebas'),
(1, 'Probador', 'Puesto de capacitación y pruebas');

INSERT INTO tblDepartamento (nombreDepartamento) VALUES ('Sistemas'), ('Gerencia');

INSERT INTO tblJornada (nombreJornada, horaInicioJornada, horaFinJornada)
VALUES ('Diurna', '08:00:00', '17:00:00');

-- 2) Historial de puestos: se crea ANTES de eliminar puestoEmpleado
CREATE TABLE tblPuestoEmpleado (
    idPuestoEmpleado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idPuesto INT NOT NULL,
    fechaAsignacionPuestoEmpleado DATE NOT NULL,
    fechaFinPuestoEmpleado DATE NULL,
    vigentePuestoEmpleado INT
        AS (IF(fechaFinPuestoEmpleado IS NULL AND is_active = 1, idEmpleado, NULL)) STORED,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PuestoEmpleado PRIMARY KEY (idPuestoEmpleado),
    CONSTRAINT Fk_PuestoEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT Fk_PuestoEmpleado_Puesto FOREIGN KEY (idPuesto)
        REFERENCES tblPuesto (idPuesto) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqPuestoEmpleadoVigente UNIQUE (vigentePuestoEmpleado),
    CONSTRAINT ckPuestoEmpleadoFechas CHECK
        (fechaFinPuestoEmpleado IS NULL OR fechaFinPuestoEmpleado >= fechaAsignacionPuestoEmpleado),
    KEY idxPuestoEmpleadoEmpleadoFecha (idEmpleado, fechaAsignacionPuestoEmpleado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- 3) Cargar el puesto actual de cada empleado como primer registro del historial
INSERT INTO tblPuestoEmpleado (idEmpleado, idPuesto, fechaAsignacionPuestoEmpleado)
SELECT e.idEmpleado, p.idPuesto, e.fechaContratacionEmpleado
FROM tblEmpleado e
JOIN tblPuesto p ON p.nombrePuesto = e.puestoEmpleado;

-- 4) Columnas nuevas en tblEmpleado (el puesto ya NO vive aquí)
ALTER TABLE tblEmpleado
    ADD COLUMN idDepartamento INT NULL AFTER apellidosEmpleado,
    ADD COLUMN idJornada INT NULL AFTER idDepartamento,
    ADD COLUMN salarioEmpleado DECIMAL(12,4) NULL AFTER idJornada;

UPDATE tblEmpleado e
JOIN tblDepartamento d ON d.nombreDepartamento = IF(e.puestoEmpleado = 'Gerente', 'Gerencia', 'Sistemas')
SET e.idDepartamento = d.idDepartamento
WHERE e.idEmpleado > 0;

UPDATE tblEmpleado
SET idJornada = (SELECT idJornada FROM tblJornada WHERE nombreJornada = 'Diurna'),
    salarioEmpleado = 0
WHERE idEmpleado > 0;

-- 5) NOT NULL, quitar columna vieja y crear FK
ALTER TABLE tblEmpleado
    MODIFY idDepartamento INT NOT NULL,
    MODIFY idJornada INT NOT NULL,
    MODIFY salarioEmpleado DECIMAL(12,4) NOT NULL,
    DROP COLUMN puestoEmpleado,
    ADD CONSTRAINT Fk_Empleado_Departamento FOREIGN KEY (idDepartamento)
        REFERENCES tblDepartamento (idDepartamento) ON DELETE RESTRICT ON UPDATE CASCADE,
    ADD CONSTRAINT Fk_Empleado_Jornada FOREIGN KEY (idJornada)
        REFERENCES tblJornada (idJornada) ON DELETE RESTRICT ON UPDATE CASCADE;


-- ---------- Metodos de pago ----------
CREATE TABLE tblMetodoPago (
    idMetodoPago INT AUTO_INCREMENT NOT NULL,
    nombreMetodoPago VARCHAR(100) NOT NULL,
    descripcionMetodoPago VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_MetodoPago PRIMARY KEY (idMetodoPago),
    CONSTRAINT uqNombreMetodoPago UNIQUE (nombreMetodoPago)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblEmpleadoMetodoPago (
    idEmpleado INT NOT NULL,
    idMetodoPago INT NOT NULL,
    fechaAsignacionEmpleadoMetodoPago DATE NOT NULL,
    montoEmpleadoMetodoPago DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_EmpleadoMetodoPago PRIMARY KEY (idEmpleado, idMetodoPago),
    CONSTRAINT Fk_EmpleadoMetodoPago_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT Fk_EmpleadoMetodoPago_MetodoPago FOREIGN KEY (idMetodoPago)
        REFERENCES tblMetodoPago (idMetodoPago) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Asistencia ----------
CREATE TABLE tblAsistenciaEmpleado (
    idAsistenciaEmpleado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    fechaAsistenciaEmpleado DATE NOT NULL,
    horaEntradaAsistenciaEmpleado TIME NULL,
    horaSalidaAsistenciaEmpleado TIME NULL,
    estadoAsistenciaEmpleado ENUM('PRESENTE', 'TARDANZA', 'AUSENTE', 'PERMISO') NOT NULL DEFAULT 'PRESENTE',
    observacionAsistenciaEmpleado VARCHAR(255) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_AsistenciaEmpleado PRIMARY KEY (idAsistenciaEmpleado),
    CONSTRAINT Fk_AsistenciaEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqAsistenciaEmpleadoFecha UNIQUE (idEmpleado, fechaAsistenciaEmpleado),
    CONSTRAINT ckAsistenciaEmpleadoHoras CHECK (
        horaEntradaAsistenciaEmpleado IS NULL
        OR horaSalidaAsistenciaEmpleado IS NULL
        OR horaSalidaAsistenciaEmpleado > horaEntradaAsistenciaEmpleado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Percepciones y deducciones ----------

CREATE TABLE tblPercepcionDeduccion (
    idPercepcionDeduccion INT AUTO_INCREMENT NOT NULL,
    nombrePercepcionDeduccion VARCHAR(100) NOT NULL,
    periodoPercepcionDeduccion ENUM('MENSUAL', 'ANUAL') NOT NULL,
    efectoPercepcionDeduccion BOOLEAN NOT NULL DEFAULT FALSE,
    tipoPercepcionDeduccion ENUM('Tabla', '%', 'Monto') NOT NULL,
    valorTipoPercepcionDeduccion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    aplicacionPercepcionDeduccion ENUM('Todos', 'Algunos') NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PercepcionDeduccion PRIMARY KEY (idPercepcionDeduccion),
    CONSTRAINT uqNombrePercepcionDeduccion UNIQUE (nombrePercepcionDeduccion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblPercepcionDeduccionEmpleado (
    idPercepcionDeduccionEmpleado INT AUTO_INCREMENT NOT NULL,
    idPercepcionDeduccion INT NOT NULL,
    idEmpleado INT NULL,
    idDepartamento INT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PercepcionDeduccionEmpleado PRIMARY KEY (idPercepcionDeduccionEmpleado),
    CONSTRAINT Fk_PercepcionDeduccionEmpleado_PercepcionDeduccion FOREIGN KEY (idPercepcionDeduccion)
        REFERENCES tblPercepcionDeduccion (idPercepcionDeduccion),
    CONSTRAINT Fk_PercepcionDeduccionEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado),
    CONSTRAINT Fk_PercepcionDeduccionEmpleado_Departamento FOREIGN KEY (idDepartamento)
        REFERENCES tblDepartamento (idDepartamento),
    -- Evita duplicados por empleado y por departamento (cada uno por separado)
    CONSTRAINT uqPercepcionDeduccionEmpleadoEmpleado UNIQUE (idPercepcionDeduccion, idEmpleado),
    CONSTRAINT uqPercepcionDeduccionEmpleadoDepartamento UNIQUE (idPercepcionDeduccion, idDepartamento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblPercepcionDeduccionRango (
    idPercepcionDeduccionRango INT AUTO_INCREMENT NOT NULL,
    idPercepcionDeduccion INT NOT NULL,
    salarioDesdePercepcionDeduccionRango DECIMAL(12,4) NOT NULL,
    salarioHastaPercepcionDeduccionRango DECIMAL(12,4) NULL,
    modoValorPercepcionDeduccionRango ENUM('%', 'Monto') NOT NULL,
    valorPercepcionDeduccionRango DECIMAL(12,4) NOT NULL,
    montoFijoBasePercepcionDeduccionRango DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PercepcionDeduccionRango PRIMARY KEY (idPercepcionDeduccionRango),
    CONSTRAINT Fk_PercepcionDeduccionRango_PercepcionDeduccion FOREIGN KEY (idPercepcionDeduccion)
        REFERENCES tblPercepcionDeduccion (idPercepcionDeduccion),
    CONSTRAINT uqPercepcionDeduccionRangoDesde UNIQUE (idPercepcionDeduccion, salarioDesdePercepcionDeduccionRango)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Nomina ----------

CREATE TABLE tblNominaEncabezado (
    idNominaEncabezado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    fechaInicioNominaEncabezado DATE NOT NULL,
    fechaFinalNominaEncabezado DATE NOT NULL,
    salarioBaseNominaEncabezado DECIMAL(12,4) NOT NULL,
    duracionHoraExtraNominaEncabezado INT NOT NULL DEFAULT 0,
    totalPercepcionNominaEncabezado DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    totalDeduccionNominaEncabezado DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    totalNetoNominaEncabezado DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    estadoNminaEncabezado ENUM('Aprobada', 'Rechazada', 'Pagada') NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_NominaEncabezado PRIMARY KEY (idNominaEncabezado),
    CONSTRAINT Fk_NominaEncabezado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ckNominaEncabezadoFechas CHECK (fechaFinalNominaEncabezado >= fechaInicioNominaEncabezado)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblNominaDetalle (
    idNominaDetalle INT AUTO_INCREMENT NOT NULL,
    idNominaEncabezado INT NOT NULL,
    idPercepcionDeduccion INT NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_NominaDetalle PRIMARY KEY (idNominaDetalle),
    CONSTRAINT Fk_NominaDetalle_NominaEncabezado FOREIGN KEY (idNominaEncabezado)
        REFERENCES tblNominaEncabezado (idNominaEncabezado),
    CONSTRAINT Fk_NominaDetalle_PercepcionDeduccion FOREIGN KEY (idPercepcionDeduccion)
        REFERENCES tblPercepcionDeduccion (idPercepcionDeduccion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 

CREATE TABLE tblHoraExtra (
    idHoraExtra INT AUTO_INCREMENT NOT NULL,
    idPercepcionDeduccion INT NOT NULL,
    idEmpleado INT NOT NULL,
    idAsistenciaEmpleado INT NOT NULL,
    duracionHoraExtra INT NOT NULL,
    valorHoraOrdinariaHoraExtra DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    porcentajeRecargoHoraExtra DECIMAL(3,2) NOT NULL DEFAULT 0.50,
    totalPagoHoraExtra DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    estadoHoraExtra ENUM('PENDIENTE', 'APROBADA', 'RECHAZADA', 'PAGADA') NOT NULL DEFAULT 'PENDIENTE',
    idUsuarioAprueba INT NULL,
    fechaAprobacionHoraExtra DATETIME NULL,
    idNominaEncabezado INT NULL,
    observacionHoraExtra VARCHAR(255) NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_HoraExtra PRIMARY KEY (idHoraExtra),
    CONSTRAINT Fk_HoraExtra_PercepcionDeduccion FOREIGN KEY (idPercepcionDeduccion)
        REFERENCES tblPercepcionDeduccion (idPercepcionDeduccion) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_HoraExtra_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_HoraExtra_AsistenciaEmpleado FOREIGN KEY (idAsistenciaEmpleado)
        REFERENCES tblAsistenciaEmpleado (idAsistenciaEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_HoraExtra_UsuarioAprueba FOREIGN KEY (idUsuarioAprueba)
        REFERENCES tblUsuario (idUsuario) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_HoraExtra_NominaEncabezado FOREIGN KEY (idNominaEncabezado)
        REFERENCES tblNominaEncabezado (idNominaEncabezado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqHoraExtraAsistencia UNIQUE (idAsistenciaEmpleado),
    CONSTRAINT ckHoraExtraDuracion CHECK (duracionHoraExtra > 0 AND duracionHoraExtra <= 360),
    CONSTRAINT ckHoraExtraRecargo CHECK (porcentajeRecargoHoraExtra >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Evaluacion de desempeno ----------

CREATE TABLE tblCicloEvaluacion (
    idCicloEvaluacion INT AUTO_INCREMENT NOT NULL,
    nombreCicloEvaluacion VARCHAR(100) NOT NULL,
    fechaInicioCicloEvaluacion DATE NOT NULL,
    fechaFinCicloEvaluacion DATE NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_CicloEvaluacion PRIMARY KEY (idCicloEvaluacion),
    CONSTRAINT uqNombreCicloEvaluacion UNIQUE (nombreCicloEvaluacion),
    CONSTRAINT ckCicloEvaluacionFechas CHECK (fechaFinCicloEvaluacion >= fechaInicioCicloEvaluacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblCriterioEvaluacion (
    idCriterioEvaluacion INT AUTO_INCREMENT NOT NULL,
    nombreCriterioEvaluacion VARCHAR(100) NOT NULL,
    descripcionCriterioEvaluacion VARCHAR(255) NOT NULL,
    pesoCriterioEvaluacion DECIMAL(3,2) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_CriterioEvaluacion PRIMARY KEY (idCriterioEvaluacion),
    CONSTRAINT uqNombreCriterioEvaluacion UNIQUE (nombreCriterioEvaluacion),
    CONSTRAINT ckCriterioEvaluacionPeso CHECK (pesoCriterioEvaluacion > 0 AND pesoCriterioEvaluacion <= 1)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblNivelDesempeno (
    idNivelDesempeno INT AUTO_INCREMENT NOT NULL,
    nombreNivelDesempeno VARCHAR(100) NOT NULL,
    puntajeMinimoNivelDesempeno DECIMAL(3,2) NOT NULL,
    puntajeMaximoNivelDesempeno DECIMAL(3,2) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_NivelDesempeno PRIMARY KEY (idNivelDesempeno),
    CONSTRAINT uqNombreNivelDesempeno UNIQUE (nombreNivelDesempeno),
    CONSTRAINT ckNivelDesempenoRango CHECK (
        puntajeMinimoNivelDesempeno >= 0
        AND puntajeMaximoNivelDesempeno <= 1
        AND puntajeMaximoNivelDesempeno >= puntajeMinimoNivelDesempeno)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblPlanMejora (
    idPlanMejora INT AUTO_INCREMENT NOT NULL,
    nombrePlanMejora VARCHAR(100) NOT NULL,
    descripcionPlanMejora VARCHAR(255) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PlanMejora PRIMARY KEY (idPlanMejora),
    CONSTRAINT uqNombrePlanMejora UNIQUE (nombrePlanMejora)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblEvaluacionDesempeno (
    idEvaluacionDesempeno INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idEvaluador INT NOT NULL,
    idCicloEvaluacion INT NOT NULL,
    idNivelDesempeno INT NULL,
    idPlanMejora INT NULL,
    fechaEvaluacionDesempeno DATE NOT NULL,
    puntajeEvaluacionDesempeno DECIMAL(4,2) NOT NULL DEFAULT 0.00,      -- sobre 5.00
    porcentajeEvaluacionDesempeno DECIMAL(3,2) NOT NULL DEFAULT 0.00,   -- fraccion: 0.00 a 1.00
    retroalimentacionEvaluacionDesempeno VARCHAR(500) NOT NULL,
    estadoEvaluacionDesempeno ENUM('BORRADOR', 'ENVIADA', 'COMPARTIDA', 'APROBADA', 'ANULADA') NOT NULL DEFAULT 'BORRADOR',
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_EvaluacionDesempeno PRIMARY KEY (idEvaluacionDesempeno),
    CONSTRAINT Fk_EvaluacionDesempeno_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_EvaluacionDesempeno_Evaluador FOREIGN KEY (idEvaluador)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_EvaluacionDesempeno_CicloEvaluacion FOREIGN KEY (idCicloEvaluacion)
        REFERENCES tblCicloEvaluacion (idCicloEvaluacion) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_EvaluacionDesempeno_NivelDesempeno FOREIGN KEY (idNivelDesempeno)
        REFERENCES tblNivelDesempeno (idNivelDesempeno) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT Fk_EvaluacionDesempeno_PlanMejora FOREIGN KEY (idPlanMejora)
        REFERENCES tblPlanMejora (idPlanMejora) ON DELETE SET NULL ON UPDATE CASCADE,
    CONSTRAINT uqEvaluacionDesempenoEmpleadoCiclo UNIQUE (idEmpleado, idCicloEvaluacion),
    CONSTRAINT ckEvaluacionDesempenoPuntaje CHECK (puntajeEvaluacionDesempeno BETWEEN 0 AND 5),
    CONSTRAINT ckEvaluacionDesempenoPorcentaje CHECK (porcentajeEvaluacionDesempeno BETWEEN 0 AND 1)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblDetalleEvaluacionDesempeno (
    idEvaluacionDesempeno INT NOT NULL,
    idCriterioEvaluacion INT NOT NULL,
    pesoDetalleEvaluacionDesempeno DECIMAL(3,2) NOT NULL,     -- copia del peso al momento de evaluar
    notaDetalleEvaluacionDesempeno TINYINT NOT NULL,
    ponderadoDetalleEvaluacionDesempeno DECIMAL(4,2) NOT NULL DEFAULT 0.00,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleEvaluacionDesempeno PRIMARY KEY (idEvaluacionDesempeno, idCriterioEvaluacion),
    CONSTRAINT Fk_DetalleEvaluacionDesempeno_EvaluacionDesempeno FOREIGN KEY (idEvaluacionDesempeno)
        REFERENCES tblEvaluacionDesempeno (idEvaluacionDesempeno) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT Fk_DetalleEvaluacionDesempeno_CriterioEvaluacion FOREIGN KEY (idCriterioEvaluacion)
        REFERENCES tblCriterioEvaluacion (idCriterioEvaluacion) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ckDetalleEvaluacionDesempenoNota CHECK (notaDetalleEvaluacionDesempeno BETWEEN 1 AND 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Permisos y vacaciones ----------
CREATE TABLE tblTipoPermiso (
    idTipoPermiso INT AUTO_INCREMENT NOT NULL,
    nombreTipoPermiso VARCHAR(100) NOT NULL,
    descripcionTipoPermiso VARCHAR(255) NOT NULL,
    conGoceSueldoTipoPermiso BOOLEAN NOT NULL DEFAULT TRUE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_TipoPermiso PRIMARY KEY (idTipoPermiso),
    CONSTRAINT uqNombreTipoPermiso UNIQUE (nombreTipoPermiso)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblPermisoEmpleado (
    idPermisoEmpleado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idTipoPermiso INT NOT NULL,
    fechaInicioPermisoEmpleado DATE NOT NULL,
    fechaFinPermisoEmpleado DATE NOT NULL,
    horaInicioPermisoEmpleado TIME NULL,                      -- solo si se calcula en horas
    horaFinPermisoEmpleado TIME NULL,
    unidadPermisoEmpleado ENUM('DIAS', 'HORAS') NOT NULL DEFAULT 'DIAS',
    totalPermisoEmpleado DECIMAL(6,2) NOT NULL DEFAULT 0.00,  -- dias u horas calculados (ver nota de duracion)
    motivoPermisoEmpleado VARCHAR(255) NOT NULL,
    estadoPermisoEmpleado ENUM('PENDIENTE', 'APROBADO', 'RECHAZADO', 'ANULADO') NOT NULL DEFAULT 'PENDIENTE',
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PermisoEmpleado PRIMARY KEY (idPermisoEmpleado),
    CONSTRAINT Fk_PermisoEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_PermisoEmpleado_TipoPermiso FOREIGN KEY (idTipoPermiso)
        REFERENCES tblTipoPermiso (idTipoPermiso) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ckPermisoEmpleadoFechas CHECK (fechaFinPermisoEmpleado >= fechaInicioPermisoEmpleado),
    CONSTRAINT ckPermisoEmpleadoTotal CHECK (totalPermisoEmpleado >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblPeriodoVacacion (
    idPeriodoVacacion INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    fechaInicioPeriodoVacacion DATE NOT NULL,
    fechaFinPeriodoVacacion DATE NOT NULL,
    diasCorrespondientesPeriodoVacacion DECIMAL(5,2) NOT NULL DEFAULT 15.00,
    diasTomadosPeriodoVacacion DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    diasDisponiblesPeriodoVacacion DECIMAL(5,2)
        AS (diasCorrespondientesPeriodoVacacion - diasTomadosPeriodoVacacion) STORED,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_PeriodoVacacion PRIMARY KEY (idPeriodoVacacion),
    CONSTRAINT Fk_PeriodoVacacion_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqPeriodoVacacionEmpleadoInicio UNIQUE (idEmpleado, fechaInicioPeriodoVacacion),
    CONSTRAINT ckPeriodoVacacionFechas CHECK (fechaFinPeriodoVacacion > fechaInicioPeriodoVacacion),
    CONSTRAINT ckPeriodoVacacionDias CHECK (
        diasTomadosPeriodoVacacion >= 0
        AND diasTomadosPeriodoVacacion <= diasCorrespondientesPeriodoVacacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblVacacionEmpleado (
    idVacacionEmpleado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idPeriodoVacacion INT NOT NULL,
    fechaInicioVacacionEmpleado DATE NOT NULL,
    fechaFinVacacionEmpleado DATE NOT NULL,
    diasTomarVacacionEmpleado DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    observacionVacacionEmpleado VARCHAR(255) NULL,
    estadoVacacionEmpleado ENUM('PENDIENTE', 'APROBADO', 'EN CURSO', 'FINALIZADO', 'ANULADO') NOT NULL DEFAULT 'PENDIENTE',
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_VacacionEmpleado PRIMARY KEY (idVacacionEmpleado),
    CONSTRAINT Fk_VacacionEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_VacacionEmpleado_PeriodoVacacion FOREIGN KEY (idPeriodoVacacion)
        REFERENCES tblPeriodoVacacion (idPeriodoVacacion) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ckVacacionEmpleadoFechas CHECK (fechaFinVacacionEmpleado >= fechaInicioVacacionEmpleado),
    CONSTRAINT ckVacacionEmpleadoDias CHECK (diasTomarVacacionEmpleado > 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
-- ---------- Capacitaciones ----------
CREATE TABLE tblInstructor (
    idInstructor INT AUTO_INCREMENT NOT NULL,
    nombreInstructor VARCHAR(100) NOT NULL,
    especialidadInstructor VARCHAR(100) NOT NULL,
    correoInstructor VARCHAR(150) NOT NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Instructor PRIMARY KEY (idInstructor),
    CONSTRAINT uqCorreoInstructor UNIQUE (correoInstructor)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblCapacitacion (
    idCapacitacion INT AUTO_INCREMENT NOT NULL,
    idDepartamento INT NULL,                                  
    nombreCapacitacion VARCHAR(100) NOT NULL,
    descripcionCapacitacion VARCHAR(255) NULL,
    fechaInicioCapacitacion DATE NOT NULL,
    fechaFinCapacitacion DATE NOT NULL,
    costoCapacitacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    cupoCapacitacion INT NOT NULL,
    estadoCapacitacion BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Capacitacion PRIMARY KEY (idCapacitacion),
    CONSTRAINT Fk_Capacitacion_Departamento FOREIGN KEY (idDepartamento)
        REFERENCES tblDepartamento (idDepartamento)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblAsignacionCapacitacion (
    idAsignacionCapacitacion INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idCapacitacion INT NOT NULL,
    fechaAsignacionCapacitacion DATE NOT NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_AsignacionCapacitacion PRIMARY KEY (idAsignacionCapacitacion),
    CONSTRAINT Fk_AsignacionCapacitacion_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado),
    CONSTRAINT Fk_AsignacionCapacitacion_Capacitacion FOREIGN KEY (idCapacitacion)
        REFERENCES tblCapacitacion (idCapacitacion),
    CONSTRAINT uqAsignacionCapacitacion UNIQUE (idEmpleado, idCapacitacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
CREATE TABLE tblEvaluacionCapacitacion (
    idEvaluacionCapacitacion INT AUTO_INCREMENT NOT NULL,
    idAsignacionCapacitacion INT NOT NULL,
    puntualidadEvaluacionCapacitacion DECIMAL(5,2) NOT NULL,
    calidadEvaluacionCapacitacion DECIMAL(5,2) NOT NULL,
    responsabilidadEvaluacionCapacitacion DECIMAL(5,2) NOT NULL,
    desarrolloEvaluacionCapacitacion DECIMAL(5,2) NOT NULL,
    resultadoFinalEvaluacionCapacitacion DECIMAL(5,2) NOT NULL DEFAULT 0.00,
    observacionEvaluacionCapacitacion VARCHAR(255) NULL,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_EvaluacionCapacitacion PRIMARY KEY (idEvaluacionCapacitacion),
    CONSTRAINT Fk_EvaluacionCapacitacion_AsignacionCapacitacion FOREIGN KEY (idAsignacionCapacitacion)
        REFERENCES tblAsignacionCapacitacion (idAsignacionCapacitacion),
    CONSTRAINT uqEvaluacionCapacitacion UNIQUE (idAsignacionCapacitacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 
 
CREATE TABLE tblMotivoBaja (
    idMotivoBaja INT AUTO_INCREMENT NOT NULL,
    nombreMotivoBaja VARCHAR(100) NOT NULL,
    aplicaIndemnizacionMotivoBaja BOOLEAN NOT NULL DEFAULT FALSE,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_MotivoBaja PRIMARY KEY (idMotivoBaja),
    CONSTRAINT uqNombreMotivoBaja UNIQUE (nombreMotivoBaja)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 

CREATE TABLE tblBajaEmpleado (
    idBajaEmpleado INT AUTO_INCREMENT NOT NULL,
    idEmpleado INT NOT NULL,
    idMotivoBaja INT NOT NULL,
    fechaBajaEmpleado DATE NOT NULL,
    observacionBajaEmpleado VARCHAR(255) NULL,
    idUsuarioRegistra INT NOT NULL,                       
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_BajaEmpleado PRIMARY KEY (idBajaEmpleado),
    CONSTRAINT Fk_BajaEmpleado_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT Fk_BajaEmpleado_Usuario FOREIGN KEY (idUsuarioRegistra)
        REFERENCES tblUsuario (idUsuario) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_BajaEmpleado_MotivoBaja FOREIGN KEY (idMotivoBaja)
        REFERENCES tblMotivoBaja (idMotivoBaja) ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 

CREATE TABLE tblParametroLey (
    idParametroLey INT AUTO_INCREMENT NOT NULL,
    idEmpresa INT NULL,                               
    codigoParametroLey VARCHAR(30) NOT NULL,
    descripcionParametroLey VARCHAR(255) NOT NULL,
    valorParametroLey DECIMAL(12,4) NOT NULL,
    vigenteDesdeParametroLey DATE NOT NULL,
    vigenteHastaParametroLey DATE NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_ParametroLey PRIMARY KEY (idParametroLey),
    CONSTRAINT Fk_ParametroLey_Empresa FOREIGN KEY (idEmpresa)
        REFERENCES tblEmpresa (idEmpresa) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT ckParametroLeyVigencia CHECK
        (vigenteHastaParametroLey IS NULL OR vigenteHastaParametroLey >= vigenteDesdeParametroLey),
    KEY idxParametroLeyBusqueda (codigoParametroLey, idEmpresa, vigenteDesdeParametroLey)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 

CREATE TABLE tblLiquidacion (
    idLiquidacion INT AUTO_INCREMENT NOT NULL,
    idEmpresa INT NOT NULL,
    idEmpleado INT NOT NULL,
    idBajaEmpleado INT NOT NULL,
    fechaIngresoLiquidacion DATE NOT NULL,
    fechaBajaLiquidacion DATE NOT NULL,
    diasLaboradosLiquidacion INT NOT NULL,
    salarioMensualLiquidacion DECIMAL(12,4) NOT NULL,
    salarioPromedioLiquidacion DECIMAL(12,4) NOT NULL,
    baseIndemnizacionLiquidacion DECIMAL(12,4) NOT NULL,
    salarioDiaLiquidacion DECIMAL(12,4) NOT NULL,
    totalPercepcionLiquidacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    totalDeduccionLiquidacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    totalNetoLiquidacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    parametrosUsadosLiquidacion TEXT NULL,            -- JSON con los valores aplicados y su origen (LEY/EMPRESA)
    numeroFiniquitoLiquidacion VARCHAR(30) NULL,
    fechaEmisionFiniquitoLiquidacion DATE NULL,
    montoLetrasFiniquitoLiquidacion VARCHAR(255) NULL,
    estadoLiquidacion ENUM('BORRADOR', 'APROBADA', 'PAGADA', 'ANULADA') NOT NULL DEFAULT 'BORRADOR',
    idUsuarioCrea INT NOT NULL,                        
    idUsuarioAprueba INT NOT NULL,
    fechaAprobacionLiquidacion DATETIME NULL,
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_Liquidacion PRIMARY KEY (idLiquidacion),
    CONSTRAINT Fk_Liquidacion_Empresa FOREIGN KEY (idEmpresa)
        REFERENCES tblEmpresa (idEmpresa) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_Liquidacion_Empleado FOREIGN KEY (idEmpleado)
        REFERENCES tblEmpleado (idEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_Liquidacion_BajaEmpleado FOREIGN KEY (idBajaEmpleado)
        REFERENCES tblBajaEmpleado (idBajaEmpleado) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT Fk_Liquidacion_UsuarioCrea FOREIGN KEY (idUsuarioCrea)
        REFERENCES tblUsuario (idUsuario) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT Fk_Liquidacion_UsuarioAprueba FOREIGN KEY (idUsuarioAprueba)
        REFERENCES tblUsuario (idUsuario) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqNumeroFiniquitoLiquidacion UNIQUE (numeroFiniquitoLiquidacion),
    CONSTRAINT ckLiquidacionFechas CHECK (fechaBajaLiquidacion >= fechaIngresoLiquidacion),
    CONSTRAINT ckLiquidacionDias CHECK (diasLaboradosLiquidacion >= 0),
    KEY idxLiquidacionEmpresaEstado (idEmpresa, estadoLiquidacion)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
 

CREATE TABLE tblDetalleLiquidacion (
    idDetalleLiquidacion INT AUTO_INCREMENT NOT NULL,
    idLiquidacion INT NOT NULL,
    idPercepcionDeduccion INT NOT NULL,
    diasDetalleLiquidacion DECIMAL(8,2) NOT NULL DEFAULT 0.00,
    baseDetalleLiquidacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    montoDetalleLiquidacion DECIMAL(12,4) NOT NULL DEFAULT 0.0000,
    created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    CONSTRAINT Pk_DetalleLiquidacion PRIMARY KEY (idDetalleLiquidacion),
    CONSTRAINT Fk_DetalleLiquidacion_Liquidacion FOREIGN KEY (idLiquidacion)
        REFERENCES tblLiquidacion (idLiquidacion) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT Fk_DetalleLiquidacion_PercepcionDeduccion FOREIGN KEY (idPercepcionDeduccion)
        REFERENCES tblPercepcionDeduccion (idPercepcionDeduccion) ON DELETE RESTRICT ON UPDATE CASCADE,
    CONSTRAINT uqDetalleLiquidacionConcepto UNIQUE (idLiquidacion, idPercepcionDeduccion),
    CONSTRAINT ckDetalleLiquidacionMonto CHECK (montoDetalleLiquidacion >= 0)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;