USE `dbsistemaembutidos`;



CREATE TABLE tlbMetodopago (
    IDmetodo  int NOT NULL AUTO_INCREMENT,
    codigoMetodo     VARCHAR(20) NOT NULL UNIQUE,
    nombreMetodo     VARCHAR(60) NOT NULL
);



CREATE TABLE tblproveedor (
    IDproveedor INT NOT NULL AUTO_INCREMENT,
    codigoProveedor VARCHAR(20) NOT NULL UNIQUE,
    nit VARCHAR(20) NOT NULL UNIQUE,
    nombreComercial VARCHAR(150),
    direccion VARCHAR(200),
    telefono VARCHAR(30),
    email VARCHAR(120),
    contacto VARCHAR(100)
);