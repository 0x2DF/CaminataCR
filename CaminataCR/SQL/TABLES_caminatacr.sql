
CREATE DATABASE caminataCR

CREATE TABLE rolUsuario
(
idRolUsuario INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
nombreRol VARCHAR(20) NOT NULL
)

INSERT INTO rolUsuario (nombreRol)
VALUES('Administrador'),
	  ('ICT'),
	  ('Regular')

CREATE TABLE Usuario
(
idUsuario INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
cuenta VARCHAR(20) NOT NULL UNIQUE,
contrasenaHash BINARY(64) NOT NULL,
contrasenaSalt UNIQUEIDENTIFIER NOT NULL,
idRolUsuario INT NOT NULL FOREIGN KEY REFERENCES rolUsuario(idRolUsuario)
)

CREATE TABLE Regular
(
idUsuario INT NOT NULL PRIMARY KEY FOREIGN KEY REFERENCES Usuario (idUsuario),
primerNombre VARCHAR(20) NOT NULL,
segundoNombre VARCHAR(20),
primerApellido VARCHAR(20) NOT NULL,
segundoApellido VARCHAR(20) NOT NULL,
correo VARCHAR(50) NOT NULL UNIQUE,
telefono VARCHAR(14) NOT NULL,
fechaNacimiento DATE NOT NULL,
sexo CHARACTER(1) NOT NULL CHECK (sexo='m' OR sexo ='f'),
nacionalidad VARCHAR(20) NOT NULL,
fotografia VARBINARY(MAX),
cuentaBancaria NUMERIC(20) NOT NULL,
activo BIT NOT NULL
)

CREATE TABLE Amigos
(
idUsuario1 INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario),
idUsuario2 INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario),
CONSTRAINT PK_Amigos PRIMARY KEY (idUsuario1, idUsuario2)
)

CREATE TABLE TipoDeCaminata
(
idTipoDeCaminata INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
tipoDeCaminata VARCHAR(30) NOT NULL,
activo BIT NOT NULL,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario)
)

CREATE TABLE NivelDeDificultad
(
idNivelDeDificultad INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
nivelDeDificultad VARCHAR(30) NOT NULL,
activo BIT NOT NULL,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario)
)

CREATE TABLE NivelDePrecio
(
idNivelDePrecio INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
nivelDePrecio VARCHAR(30) NOT NULL,
activo BIT NOT NULL,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario)
)

CREATE TABLE NivelDeCalidad
(
idNivelDeCalidad INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
nivelDeCalidad VARCHAR(30) NOT NULL,
activo BIT NOT NULL,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario)
)

-- START

CREATE TABLE Provincia
(
idProvincia INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
provincia VARCHAR(30) NOT NULL
)

CREATE TABLE Canton
(
idCanton INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
canton VARCHAR(30) NOT NULL
)

CREATE TABLE Distrito
(
idDistrito INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
distrito VARCHAR(30) NOT NULL
)

CREATE TABLE CantonPorProvincia
(
idProvincia INT NOT NULL FOREIGN KEY REFERENCES Provincia(idProvincia),
idCanton INT NOT NULL FOREIGN KEY REFERENCES Canton(idCanton),
CONSTRAINT PK_CantonPorProvincia PRIMARY KEY (idProvincia, idCanton)
)

CREATE TABLE DistritoPorCanton
(
idCanton INT NOT NULL FOREIGN KEY REFERENCES Canton(idCanton),
idDistrito INT NOT NULL FOREIGN KEY REFERENCES Distrito(idDistrito),
CONSTRAINT PK_DistritoPorCanton PRIMARY KEY (idCanton, idDistrito)
)

-- END

CREATE TABLE Caminata
(
idCaminata INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
nombreDelLugar VARCHAR(20) NOT NULL,
idProvincia INT NOT NULL FOREIGN KEY REFERENCES Provincia(idProvincia),
idCanton INT NOT NULL FOREIGN KEY REFERENCES Canton(idCanton),
idDistrito INT NOT NULL FOREIGN KEY REFERENCES Distrito(idDistrito),
detalle VARCHAR(500) NOT NULL,
longitud FLOAT NOT NULL,
latitud FLOAT NOT NULL
)
CREATE TABLE UsuarioPorCaminata
(
idUsuarioPorCaminata INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular(idUsuario),
idCaminata INT NOT NULL FOREIGN KEY REFERENCES Caminata(idCaminata),
fechaHora DATETIME NOT NULL,
idtipoDeCaminata INT NOT NULL FOREIGN KEY REFERENCES TipoDeCaminata(idTipoDeCaminata),
idNivelDeDificultad INT NOT NULL FOREIGN KEY REFERENCES NivelDeDificultad(idNivelDeDificultad),
idNivelDePrecio INT NOT NULL FOREIGN KEY REFERENCES NivelDePrecio(idNivelDePrecio),
idNivelDeCalidad INT NOT NULL FOREIGN KEY REFERENCES NivelDeCalidad(idNivelDeCalidad),
fotografia VARBINARY(MAX),
comentario VARCHAR(500)
)
CREATE TABLE Ruta
(
idRuta INT IDENTITY(1,1) NOT NULL PRIMARY KEY
)

CREATE TABLE RutaPorUPC
(
idRutaPorUPC INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
idRuta INT NOT NULL FOREIGN KEY REFERENCES Ruta(idRuta),
idUsuarioPorCaminata INT NOT NULL FOREIGN KEY REFERENCES UsuarioPorCaminata(idUsuarioPorCaminata)
)

CREATE TABLE PuntosImportantes
(
idPuntosImportantes INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
longitud FLOAT NOT NULL,
latitud FLOAT NOT NULL
)

CREATE TABLE PuntosPorRPUPC
(
idRutaPorUPC INT NOT NULL FOREIGN KEY REFERENCES RutaPorUPC(idRutaPorUPC),
idPuntosImportantes INT NOT NULL FOREIGN KEY REFERENCES PuntosImportantes(idPuntosImportantes),
posicion INT NOT NULL,
comentario VARCHAR(500),
fotografia VARBINARY(MAX),
CONSTRAINT PK_PuntosPorRuta PRIMARY KEY (idRutaPorUPC, idPuntosImportantes)
)

CREATE TABLE Likes
(
idUsuarioPorCaminata INT NOT NULL FOREIGN KEY REFERENCES UsuarioPorCaminata(idUsuarioPorCaminata),
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular (idUsuario),
fechaHora DATETIME NOT NULL,
CONSTRAINT PK_Likes PRIMARY KEY (idUsuario, idUsuarioPorCaminata)
)

CREATE TABLE Bitacora
(
idBitacora INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
fechaHora DATETIME NOT NULL,
descripcion VARCHAR(500) NOT NULL,
tipoCambio VARCHAR(500) NOT NULL,
objeto VARCHAR(500) NOT NULL,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Usuario (idUsuario)
)

Create Table Donacion
(
idDonacion INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
monto FLOAT NOT NULL,
fechaHora DATETIME NOT NULL
)

Create Table CierreDiario
(
idCierreDiario INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
idUsuario INT NOT NULL FOREIGN KEY REFERENCES Regular (idUsuario),
monto FLOAT NOT NULL,
fechaHora DATETIME NOT NULL
)
