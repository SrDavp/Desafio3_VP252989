/*

UNIVERSIDAD DON BOSCO
Facultad de Ingeniería 
 
Taller 3 – Bufete de abogados Don Bosco
Nombre	Código
Daniel Alejandro Valencia Paniagua	VP252939

Grupo: POO104 G04L

Carrera:  Ingeniería en Ciencias de la Computación

Docente: Ing. Carmen Celia Morales Samayoa

Fecha de entrega:  23/10/2026

*/


CREATE DATABASE DbAbogadosDesafio3;
GO
USE DbAbogadosDesafio3;
GO

CREATE TABLE Departamentos(
	idDepartamento INT PRIMARY KEY IDENTITY(1,1),
	Nombre VARCHAR(500),
);
GO

INSERT INTO Departamentos(Nombre) VALUES ('San Salvador'),('Sonsonate'),('San Miguel'),('Usulutan'),('Santa Ana')

CREATE TABLE Usuarios(
	Carnet VARCHAR(9) NOT NULL PRIMARY KEY,
	idDepartamento INT NOT NULL,
	Correo VARCHAR(100) UNIQUE,
	Nombre VARCHAR(100) NOT NULL,
	Apellidos VARCHAR(100) NOT NULL,
	Contraseña VARCHAR(100) NOT NULL,
	Rol INT NOT NULL DEFAULT 1,
	usuarioActivo BIT DEFAULT 1 NOT NULL,
	CONSTRAINT fk_departamentos_usuarios FOREIGN KEY (idDepartamento) REFERENCES Departamentos(idDepartamento)
); 
GO

CREATE TABLE Salarios(
	idSalario INT PRIMARY KEY IDENTITY(1,1),
	Carnet VARCHAR(9) NOT NULL,
	Salario DECIMAL(10,2) NOT NULL,
	ISSS DECIMAL(10,2) NOT NULL,
    AFP DECIMAL(10,2) NOT NULL,
	Renta DECIMAL(10,2) NOT NULL,
	SalarioTotal DECIMAL(10,2) NOT NULL,
	Activo BIT NOT NULL,
	CONSTRAINT fk_salarios_usuario FOREIGN KEY (Carnet) REFERENCES Usuarios(Carnet)
);
GO

CREATE TABLE Historial(
	idHistorial INT PRIMARY KEY  IDENTITY (1,1), 
	Carnet VARCHAR(9) NOT NULL,
	Fecha DATE NOT NULL,
	Detalles VARCHAR(100),
	CONSTRAINT fk_Historial_usuario FOREIGN KEY (Carnet) REFERENCES Usuarios(Carnet)
);
GO

CREATE TABLE Salarios_Historial(
	idSalario INT,
	idHistorial INT,
	CONSTRAINT fk_Salarios_Historial1 FOREIGN KEY (idSalario) REFERENCES Salarios(idSalario),
	CONSTRAINT fk_Salarios_Historial2 FOREIGN KEY (idHistorial) REFERENCES Historial(idHistorial)
);
GO

INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol) VALUES ('MM-202601',1,'fernanda@gmail.com' ,'Fernanda Melissa', 'Muñoz Chavez', '12345678', 1);
GO
INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol) VALUES ('DV-202602',2, 'daniel@gmail.com','Daniel Alejandro', 'Valencia Paniagua', '12345678', 2);
GO
INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol)VALUES('CM-202603', 1, 'carlos.monge@gmail.com', 'Carlos Mario', 'Monge Miranda', '12345678', 2);
GO
INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol) VALUES ('RP-202604', 2, 'rene.penate@gmail.com', 'René Oswaldo', 'Peñate Landaverde', '12345678', 2);
GO
INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol) VALUES ('GP-202605', 1, 'gloria.paniagua@example.com', 'Gloria Mercedes', 'Paniagua Hernandez', '12345678', 1);
GO
INSERT INTO Usuarios(Carnet, idDepartamento, Correo, Nombre, Apellidos, Contraseña, Rol)VALUES('WV-202606', 1, 'william.valencia@gmail.com','William Alfredo', 'Valencia Bautista', '12345678', 2);
GO

/* ------ Daniel -------*/
INSERT INTO Salarios (Carnet, Salario, ISSS, AFP,Renta, SalarioTotal, Activo) VALUES ('DV-202602', 750.00,22.50, 54.38, 29.93, 643.19, 0);
GO

INSERT INTO Salarios (Carnet, Salario, ISSS, AFP,Renta, SalarioTotal, Activo) VALUES ('DV-202602', 1750.00, 30.00, 126.88, 193.57, 1399.55, 1);
GO

/* Historial Daniel */
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2025-10-01','Salario correspondiente a Enero');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2025-11-01','Salario correspondiente a Enero');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2025-12-01','Salario correspondiente a Enero');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-01-01','Salario correspondiente a Enero');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-02-01','Salario correspondiente a Febrero');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-03-01','Salario correspondiente a Marzo');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-04-01','Salario correspondiente a Abril');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-05-01','Salario correspondiente a Mayo');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-06-01','Salario correspondiente a Junio');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-07-01','Salario correspondiente a Julio');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-08-01','Salario correspondiente a Agosto');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-09-01','Salario correspondiente a Septiembre');
go
INSERT INTO Historial(Carnet, Fecha , Detalles) VALUES ('DV-202602','2026-10-01','Salario correspondiente a Octubre');
go

INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,1);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,2);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,3);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,4);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,5);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,6);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (1,7);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,8);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,9);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,10);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,11);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,12);
go
INSERT INTO Salarios_Historial(idSalario,idHistorial) VALUES (2,13);
go

/* Carlos Monge */
INSERT INTO Salarios(Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo)VALUES('CM-202603', 1600.00, 30.00, 171.75, 163.97, 1282.25, 1);
GO

INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES ('CM-202603', '2026-05-01', 'Salario correspondiente a Mayo');
Go
INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES ('CM-202603', '2026-06-01', 'Salario correspondiente a Junio');
Go
INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES ('CM-202603', '2026-07-01', 'Salario correspondiente a Julio');
GO
INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES ('CM-202603', '2026-08-01', 'Salario correspondiente a Agosto');
Go
INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES ('CM-202603', '2026-09-01', 'Salario correspondiente a Septiembre');
GO
INSERT INTO Historial (Carnet, Fecha, Detalles) VALUES('CM-202603', '2026-10-01', 'Salario correspondiente a Octubre')
GO

/* Historial Monge */
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 14);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 15);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 16);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 17);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 18);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (3, 19);
GO

/* rene peñate */
INSERT INTO Salarios(Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo) VALUES ('RP-202604', 1600.00, 30.00, 116.00, 171.75, 1282.25, 1);
GO

/* Historial Rene */
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-02-01', 'Salario correspondiente a Febrero');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-03-01', 'Salario correspondiente a Marzo');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-04-01', 'Salario correspondiente a Abril');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-05-01', 'Salario correspondiente a Mayo');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-06-01', 'Salario correspondiente a Junio');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-07-01', 'Salario correspondiente a Julio');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-08-01', 'Salario correspondiente a Agosto');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-09-01', 'Salario correspondiente a Septiembre');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('RP-202604', '2026-10-01', 'Salario correspondiente a Octubre');
GO

INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 20);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 21);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 22);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 23);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 24);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 25);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 26);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 27);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (4, 28);
GO

/* Historial Gloria */
INSERT INTO Salarios(Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo)
VALUES ('GP-202605', 1750.00, 30.00, 126.88, 199.58, 1393.54, 1);
GO

INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('GP-202605', '2026-09-01', 'Salario correspondiente a Septiembre');
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('GP-202605', '2026-10-01', 'Salario correspondiente a Octubre');
GO

INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (5, 29);
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (5, 30);
GO

/* WIlliam pero sin historial para el funcionamiento */
INSERT INTO Salarios(Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo)VALUES('WV-202606', 1200.00, 30.00, 87.00, 97.55, 985.45, 1);
GO

/* Info Fernanda */
INSERT INTO Salarios(Carnet, Salario, ISSS, AFP, Renta, SalarioTotal, Activo)VALUES('MM-202601', 1200.00, 30.00, 87.00, 97.55, 985.45, 1);
GO
INSERT INTO Historial(Carnet, Fecha, Detalles) VALUES ('MM-202601', '2026-10-01', 'Salario correspondiente a Febrero');
GO
INSERT INTO Salarios_Historial(idSalario, idHistorial) VALUES (6, 31);
GO

/* -------- Pruebas ----------- */
SELECT Usuarios.Carnet, Salario, ISSS,AFP,Fecha,Rol, Activo,Detalles FROM Usuarios 
INNER JOIN Salarios ON Usuarios.Carnet = Salarios.Carnet
INNER JOIN Salarios_Historial ON Salarios.idSalario = Salarios_Historial.idSalario
INNER JOIN Historial ON Salarios_Historial.idHistorial = Historial.idHistorial WHERE Usuarios.Carnet = 'DV-202602'
AND YEAR(Fecha) = YEAR(GETDATE());

SELECT * FROM Historial
SELECT * FROM Salarios_Historial

SELECT * FROM Usuarios; GO
SELECT * FROM Salarios; GO
SELECT * Usuarios WHERE Correo = 'daniel@gmail.com'

update salarios set activo = 1 WHERE carnet = 'MP-202606'

use prueba
drop database DbAbogadosDesafio3

SELECT * FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo != 0

DELETE FROM usuarios WHERE Carnet = 'CR-202603'

SELECT *  FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo = 1 AND Usuarios.Carnet = 'DV-202602'

SELECT idSalario FROM Salarios WHERE Carnet = 'CM-202602' AND Activo != 0

SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Rol, Departamentos.Nombre, Salario FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Salarios.Activo != 0

UPDATE salarios SET activo = 1 WHERE idSalario = 2

SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Rol, Departamentos.Nombre AS Departamento, Salario FROM Usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet WHERE Usuarios.Carnet != @Carnet




SELECT Usuarios.Carnet, Usuarios.Nombre, Apellidos, Contraseña, Rol, usuarioActivo,Departamentos.Nombre as Departamento, correo FROM usuarios 
INNER JOIN Departamentos ON Departamentos.idDepartamento = Usuarios.idDepartamento 
INNER JOIN Salarios ON Salarios.Carnet = Usuarios.Carnet 

