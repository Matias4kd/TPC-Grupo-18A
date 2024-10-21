


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


