create DATABASE ClinicaMedica_DB
GO
use ClinicaMedica_DB
GO

CREATE TABLE Roles(
    IdRol SMALLINT PRIMARY KEY IDENTITY(1,1),
    NombreRol VARCHAR(50) not null,
)
GO
CREATE TABLE Usuarios(
    IdUsuario int PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) UNIQUE NOT NULL,
    Contraseña VARCHAR(50) not NULL,
    Nombres VARCHAR(50) not null,
    Apellidos VARCHAR(50) not null,
    Mail varchar(50) not null, 
    Telefono varchar(20) not null,
    IdRol SMALLINT NOT NULL FOREIGN KEY REFERENCES Roles(IdRol),
    FechaDeBaja DATETIME NULL,
)
GO
Create table Medicos(
    IdMedico int PRIMARY KEY IDENTITY(1,1),
    IdUsuario int FOREIGN KEY REFERENCES Usuarios(IdUsuario),
    Matricula VARCHAR(50) unique not null,
    FechaAlta DATETIME DEFAULT GETDATE(),    
)
GO
Create table TurnosTrabajo(
    IdTurnoTrabajo int PRIMARY KEY IDENTITY(1,1),
    IdMedico int FOREIGN KEY REFERENCES Medicos(IdMedico),
    HoraInicio TIME NOT NULL,
    HoraFin TIME not null,
    DiaTrabajo varchar(50) not null,
)
GO
Create TABLE Prepagas(
    IdPrepaga int PRIMARY KEY IDENTITY(1,1),
    NombrePrepaga VARCHAR(50) not null,
)
GO
Create TABLE Prepagas_x_Medico(
    IdPrepaga int FOREIGN KEY REFERENCES Prepagas(IdPrepaga),
    IdMedico int FOREIGN KEY REFERENCES Medicos(IdMedico),
    PRIMARY KEY (IdPrepaga,IdMedico),
)
GO
Create TABLE Especialidades(
    IdEspecialidad int PRIMARY key IDENTITY(1,1),
    NombreEspecialidad varchar(50) not null, 
	Estado varchar(10) not null Default 'Activo',
)
GO
Create table Especialidades_x_Medico(
    IdEspecialidad int FOREIGN KEY REFERENCES Especialidades(IdEspecialidad),
    IdMedico int FOREIGN KEY REFERENCES Medicos(IdMedico),
    PRIMARY KEY (IdEspecialidad,IdMedico),
)
GO
Create TABLE Pacientes(
    IdPaciente bigint PRIMARY KEY IDENTITY(1,1),
    Nombres varchar(50) not null, 
    Apellidos VARCHAR(50) not null, 
    DNI VARCHAR(20) UNIQUE not NULL,
    Mail varchar(50) not null,
    Telefono varchar(20) not null,
    Direccion varchar(100) not null,
    FechaNacimiento DATE not null,
    FechaAlta DATETIME DEFAULT GETDATE(),
    IdPrepaga int FOREIGN key REFERENCES Prepagas(IdPrepaga),
)
GO
Create table Turnos(
    IdTurno bigint PRIMARY KEY IDENTITY(1,1),
    IdPaciente Bigint not null FOREIGN KEY REFERENCES Pacientes(IdPaciente),
    IdMedico int not null FOREIGN KEY REFERENCES Medicos(IdMedico),
    IdEspecialidad int not null FOREIGN KEY REFERENCES Especialidades(IdEspecialidad),
    Fecha DATE not null, 
    Horario TIME not null,
    Observaciones varchar(500),
    Estado VARCHAR(50) DEFAULT 'Nuevo',
)



INSERT INTO Roles (NombreRol) VALUES ('Administrador');
INSERT INTO Roles (NombreRol) VALUES ('Recepcionista');
INSERT INTO Roles (NombreRol) VALUES ('Medico');


-- Administrador
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('admin', 'admin123', 'Carlos', 'Pérez', 'admin@example.com', '111222333', 1);

-- Recepcionista
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('recepcionista1', 'recepcion123', 'Laura', 'Gómez', 'laura.gomez@example.com', '444555666', 2);

-- Médicos
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico1', 'medico123', 'Juan', 'Alvarez', 'juan.alvarez@example.com', '777888999', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico2', 'medico123', 'Ana', 'Rodriguez', 'ana.rodriguez@example.com', '888999000', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico3', 'medico123', 'Luis', 'Martínez', 'luis.martinez@example.com', '999000111', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico4', 'medico123', 'Marta', 'Lopez', 'marta.lopez@example.com', '123456789', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico5', 'medico123', 'Carlos', 'Fernandez', 'carlos.fernandez@example.com', '234567890', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico6', 'medico123', 'Patricia', 'Gonzalez', 'patricia.gonzalez@example.com', '345678901', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico7', 'medico123', 'Raúl', 'Cruz', 'raul.cruz@example.com', '456789012', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico8', 'medico123', 'Beatriz', 'Vázquez', 'beatriz.vazquez@example.com', '567890123', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico9', 'medico123', 'Sergio', 'Morales', 'sergio.morales@example.com', '678901234', 3);
INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombres, Apellidos, Mail, Telefono, IdRol) 
VALUES ('medico10', 'medico123', 'Lucía', 'Hernandez', 'lucia.hernandez@example.com', '789012345', 3);


INSERT INTO Prepagas(NombrePrepaga) VALUES ('Osde')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Galeno')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Swis Medical')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Medicus')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('No tiene')


-- Relacionamos los médicos con los usuarios correspondientes
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (3, 'MED001');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (4, 'MED002');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (5, 'MED003');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (6, 'MED004');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (7, 'MED005');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (8, 'MED006');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (9, 'MED007');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (10, 'MED008');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (11, 'MED009');
INSERT INTO Medicos (IdUsuario, Matricula) VALUES (12, 'MED010');

INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (1, 1); -- Osde
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (2, 2); -- Galeno
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (3, 3); -- Swiss Medical
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (4, 4); -- Medicus
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (1, 5); -- Osde
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (2, 6); -- Galeno
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (3, 7); -- Swiss Medical
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (4, 8); -- Medicus
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (1, 9); -- Osde
INSERT INTO Prepagas_x_Medico (IdPrepaga, IdMedico) VALUES (2, 10); -- Galeno

INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Medicina General');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Pediatría');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Dermatología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Cardiología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Neurología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Endocrinología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Ginecología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Oftalmología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Traumatología');
INSERT INTO Especialidades (NombreEspecialidad) VALUES ('Psiquiatría');

INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (1, 1); -- Medicina General
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (2, 2); -- Pediatría
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (3, 3); -- Dermatología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (4, 4); -- Cardiología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (5, 5); -- Neurología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (6, 6); -- Endocrinología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (7, 7); -- Ginecología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (8, 8); -- Oftalmología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (9, 9); -- Traumatología
INSERT INTO Especialidades_x_Medico (IdEspecialidad, IdMedico) VALUES (10, 10); -- Psiquiatría


INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Pedro', 'López', '12345678', 'pedro.lopez@example.com', '555555555', 'Calle 1, Ciudad', '1990-01-01', 1);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Sofía', 'Ramírez', '87654321', 'sofia.ramirez@example.com', '666666666', 'Calle 2, Ciudad', '1985-02-02', 2);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Luis', 'Martínez', '13579246', 'luis.martinez@example.com', '777777777', 'Calle 3, Ciudad', '1995-03-03', 3);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Clara', 'González', '24681357', 'clara.gonzalez@example.com', '888888888', 'Calle 4, Ciudad', '1988-04-04', 4);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Javier', 'Hernández', '35792468', 'javier.hernandez@example.com', '999999999', 'Calle 5, Ciudad', '1992-05-05', 1);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('María', 'Lopez', '11122233', 'maria.lopez@example.com', '111222333', 'Calle 6, Ciudad', '1980-06-06', 2);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Fernando', 'Martínez', '22233344', 'fernando.martinez@example.com', '222333444', 'Calle 7, Ciudad', '1991-07-07', 3);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Esteban', 'Cruz', '33344455', 'esteban.cruz@example.com', '333444555', 'Calle 8, Ciudad', '1987-08-08', 4);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Sofía', 'Rojas', '44455566', 'sofia.rojas@example.com', '444555666', 'Calle 9, Ciudad', '1993-09-09', 1);
INSERT INTO Pacientes (Nombres, Apellidos, DNI, Mail, Telefono, Direccion, FechaNacimiento, IdPrepaga) VALUES ('Lucía', 'Mendoza', '55566677', 'lucia.mendoza@example.com', '555666777', 'Calle 10, Ciudad', '1982-10-10', 2);

-- Turnos para el médico 1
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '17:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '17:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '17:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '17:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '17:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (1, '09:00', '12:00', 'Sábado');

-- Turnos para el médico 2
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '18:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '18:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '18:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '18:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '18:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (2, '10:00', '13:00', 'Sábado');

-- Turnos para el médico 3
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '16:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '16:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '16:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '16:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '16:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (3, '08:00', '12:00', 'Sábado');

-- Turnos para el médico 4
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '19:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '19:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '19:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '19:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '19:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (4, '11:00', '14:00', 'Sábado');

-- Turnos para el médico 5
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '17:30', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '17:30', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '17:30', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '17:30', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '17:30', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (5, '09:30', '12:30', 'Sábado');

-- Turnos para el médico 6
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '18:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '18:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '18:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '18:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '18:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (6, '10:00', '13:00', 'Sábado');

-- Turnos para el médico 7
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '17:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '17:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '17:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '17:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '17:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (7, '09:00', '12:00', 'Sábado');

-- Turnos para el médico 8
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '18:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '18:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '18:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '18:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '18:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (8, '10:00', '13:00', 'Sábado');

-- Turnos para el médico 9
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '16:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '16:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '16:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '16:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '16:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (9, '08:00', '12:00', 'Sábado');

-- Turnos para el médico 10
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '19:00', 'Lunes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '19:00', 'Martes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '19:00', 'Miércoles');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '19:00', 'Jueves');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '19:00', 'Viernes');
INSERT INTO TurnosTrabajo (IdMedico, HoraInicio, HoraFin, DiaTrabajo) VALUES (10, '11:00', '14:00', 'Sábado');

