create DATABASE ClinicaMedica_DB
GO
use ClinicaMedica_DB
GO

CREATE TABLE Permisos(
    IDPermiso SMALLINT PRIMARY KEY IDENTITY(1,1),
    NombrePermiso varchar(50) not null,
)
GO
CREATE TABLE Roles(
    IdRol SMALLINT PRIMARY KEY IDENTITY(1,1),
    NombreRol VARCHAR(50) not null,
)
GO
Create table Permisos_X_Rol(
    IdPermiso SMALLINT FOREIGN KEY REFERENCES Permisos(IDPermiso),
    IdRol SMALLINT FOREIGN KEY REFERENCES Roles(IdRol),
    PRIMARY KEY(IdPermiso,IdRol),
)
GO
CREATE TABLE Usuarios(
    IdUsuario int PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) UNIQUE NOT NULL,
    Contraseña VARCHAR(50) not NULL,
    IdRol SMALLINT NOT NULL FOREIGN KEY REFERENCES Roles(IdRol)
)
GO
Create table Medicos(
    IdMedico int PRIMARY KEY IDENTITY(1,1),
    Nombres VARCHAR(50) not null,
    Apellidos VARCHAR(50) not null,
    Matricula VARCHAR(50) unique not null,
    Mail varchar(50) not null, 
    Telefono varchar(20) not null,
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

INSERT INTO Permisos (NombrePermiso) VALUES ('Administrar Pacientes');
INSERT INTO Permisos (NombrePermiso) VALUES ('Administrar Médicos');
INSERT INTO Permisos (NombrePermiso) VALUES ('Dar Alta Turnos');
INSERT INTO Permisos (NombrePermiso) VALUES ('Ver Turnos');
INSERT INTO Permisos (NombrePermiso) VALUES ('Modificar Turnos');
INSERT INTO Permisos (NombrePermiso) VALUES ('Administrar Recepcionistas');

-- Administrador: Puede manipular todo
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (1, 1); -- Administrar Pacientes
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (2, 1); -- Administrar Médicos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (3, 1); -- Dar Alta Turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (4, 1); -- Ver Turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (5, 1); -- Modificar Turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (6, 1); -- Administrar Recepcionistas

-- Recepcionista: Puede administrar pacientes y médicos, y dar de alta turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (1, 2); -- Administrar Pacientes
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (2, 2); -- Administrar Médicos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (3, 2); -- Dar Alta Turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (4, 2); -- Ver Turnos

-- Médico: Puede ver y modificar sus turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (4, 3); -- Ver Turnos
INSERT INTO Permisos_X_Rol (IdPermiso, IdRol) VALUES (5, 3); -- Modificar Turnos


-- Administrador
INSERT INTO Usuarios (NombreUsuario, Contraseña, IdRol) VALUES ('admin', 'admin123', 1);

-- Recepcionista
INSERT INTO Usuarios (NombreUsuario, Contraseña, IdRol) VALUES ('recepcionista', 'recep123', 2);

-- Médico
INSERT INTO Usuarios (NombreUsuario, Contraseña, IdRol) VALUES ('medico1', 'medico123', 3);

INSERT INTO Prepagas(NombrePrepaga) VALUES ('Osde')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Galeno')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Swis Medical')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('Medicus')
INSERT INTO Prepagas(NombrePrepaga) VALUES ('No tiene')


INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Juan', 'Pérez', 'M001', 'juan.perez@example.com', '123456789');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('María', 'Gómez', 'M002', 'maria.gomez@example.com', '987654321');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Carlos', 'Fernández', 'M003', 'carlos.fernandez@example.com', '123123123');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Laura', 'Martínez', 'M004', 'laura.martinez@example.com', '321321321');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Ana', 'Sánchez', 'M005', 'ana.sanchez@example.com', '456456456');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Diego', 'Rodríguez', 'M006', 'diego.rodriguez@example.com', '654654654');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Clara', 'Martínez', 'M007', 'clara.martinez@example.com', '789789789');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Santiago', 'López', 'M008', 'santiago.lopez@example.com', '321654987');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Valentina', 'González', 'M009', 'valentina.gonzalez@example.com', '987321654');
INSERT INTO Medicos (Nombres, Apellidos, Matricula, Mail, Telefono) VALUES ('Fernando', 'Hernández', 'M010', 'fernando.hernandez@example.com', '123789456');


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

INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (1, 1, 1, '2024-10-23', '09:00', 'Consulta general', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (2, 2, 2, '2024-10-24', '10:00', 'Chequeo anual', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (3, 3, 3, '2024-10-25', '11:00', 'Revisión de medicamentos', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (4, 4, 1, '2024-10-26', '12:00', 'Control de hipertensión', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (5, 5, 2, '2024-10-27', '13:00', 'Consulta dermatológica', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (1, 6, 3, '2024-10-28', '14:00', 'Chequeo de rutina', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (2, 7, 1, '2024-10-29', '15:00', 'Consulta de seguimiento', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (3, 8, 2, '2024-10-30', '16:00', 'Consulta de pediatría', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (4, 9, 3, '2024-10-31', '17:00', 'Consulta neurológica', 'Nuevo');
INSERT INTO Turnos (IdPaciente, IdMedico, IdEspecialidad, Fecha, Horario, Observaciones, Estado) VALUES (5, 10, 1, '2024-11-01', '18:00', 'Control médico', 'Nuevo');


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

