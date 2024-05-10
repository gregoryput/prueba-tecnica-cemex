

create database MedicaPrueba
go
use MedicaPrueba
go


CREATE TABLE Paciente (
    IDPaciente INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100),
    Edad INT,
    Genero NVARCHAR(10),
    Direccion NVARCHAR(100),
    Telefono NVARCHAR(20)
);
go
CREATE TABLE Doctor (
    IDDoctor INT IDENTITY PRIMARY KEY,
    Nombre NVARCHAR(100),
    Especialidad NVARCHAR(100),
	Hospital NVARCHAR(100)
);
go
CREATE TABLE Asignacion_Paciente_Doctor (
	IDAsignados INT IDENTITY PRIMARY KEY,
    IDPaciente INT,
    IDDoctor INT,
    FOREIGN KEY (IDPaciente) REFERENCES Paciente(IDPaciente),
    FOREIGN KEY (IDDoctor) REFERENCES Doctor(IDDoctor),
   
);
go
-- Inserciones para la tabla Doctor
INSERT INTO Doctor (Nombre, Especialidad, Hospital) 
VALUES
('Dr. García', 'Cardiología', 'Hospital ABC'),
('Dr. López', 'Pediatría', 'Hospital DEF'),
('Dr. Martínez', 'Oncología', 'Hospital GHI'),
('Dra. Rodríguez', 'Ginecología', 'Hospital JKL'),
('Dr. Sánchez', 'Neurología', 'Hospital MNO')

go
-- Inserciones para la tabla Paciente
INSERT INTO Paciente (Nombre, Edad, Genero, Direccion, Telefono) 
VALUES
('Juan Pérez', 35, 'Masculino', 'Calle 123, Ciudad ABC', '+18091234567'),
('María González', 45, 'Femenino', 'Avenida XYZ, Ciudad DEF', '+18099876543'),
('Carlos Martínez', 28, 'Masculino', 'Carrera 456, Ciudad GHI', '+18295678901'),
('Ana López', 50, 'Femenino', 'Calle 789, Ciudad JKL', '+18290123456'),
('Pedro Rodríguez', 60, 'Masculino', 'Avenida LMN, Ciudad OPQ', '+18495432109'),
('Laura Sánchez', 42, 'Femenino', 'Carrera 789, Ciudad RST', '+18096789012'),
('Sergio Torres', 38, 'Masculino', 'Calle 345, Ciudad UVW', '+18299876543'),
('Elena Gómez', 25, 'Femenino', 'Avenida XYZ, Ciudad XYZ', '+18291234567'),
('Héctor Ramírez', 55, 'Masculino', 'Carrera 567, Ciudad ABC', '+18494567890'),
('Isabel Díaz', 33, 'Femenino', 'Calle 901, Ciudad DEF', '+18098901234'),
('Fernando Ruiz', 48, 'Masculino', 'Avenida GHI, Ciudad GHI', '+18293456789'),
('Lucía Torres', 40, 'Femenino', 'Carrera 678, Ciudad JKL', '+18496789012'),
('Diego Sánchez', 30, 'Masculino', 'Calle 123, Ciudad LMN', '+18090123456'),
('Valeria Pérez', 49, 'Femenino', 'Avenida OPQ, Ciudad OPQ', '+18495432109');

go
INSERT INTO Asignacion_Paciente_Doctor (IDPaciente, IDDoctor)
VALUES
(1, 1),   -- Juan Pérez (IDPaciente = 1) está asignado al Dr. García (IDDoctor = 1)
(2, 2)  -- María González (IDPaciente = 2) está asignada al Dr. López (IDDoctor = 2)


