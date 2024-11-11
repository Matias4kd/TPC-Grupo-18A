

CREATE PROCEDURE SP_ObtenerTurnosDisponibles
    @IdMedico INT,
    @Fecha DATE
AS
BEGIN
    DECLARE @HoraInicio TIME;
    DECLARE @HoraFin TIME;
    DECLARE @HoraActual TIME;

    -- Consulta el horario de trabajo del médico para el día especificado
    SELECT @HoraInicio = HoraInicio, @HoraFin = HoraFin
    FROM TurnosTrabajo
    WHERE IdMedico = @IdMedico 
      AND DiaTrabajo = DATENAME(WEEKDAY, @Fecha);

    -- Verifica que el médico trabaje el día solicitado
    IF @HoraInicio IS NULL OR @HoraFin IS NULL
    BEGIN
        PRINT 'El médico no trabaja en el día seleccionado';
        RETURN;
    END

    -- Crear una tabla temporal para almacenar los turnos disponibles
    CREATE TABLE #TurnosDisponibles (Horario TIME);

    -- Generar los turnos de 1 hora dentro del rango de trabajo
    SET @HoraActual = @HoraInicio;
    WHILE @HoraActual < @HoraFin
    BEGIN
        -- Verificar si el horario ya está ocupado
        IF NOT EXISTS (
            SELECT 1 
            FROM Turnos 
            WHERE IdMedico = @IdMedico 
              AND Fecha = @Fecha 
              AND Horario = @HoraActual
        )
        BEGIN
            -- Insertar el horario en la tabla temporal
            INSERT INTO #TurnosDisponibles (Horario) VALUES (@HoraActual);
        END

        -- Avanzar la hora en 1 hora
        SET @HoraActual = DATEADD(hour, 1, @HoraActual);
    END

    -- Seleccionar los turnos disponibles
    SELECT Horario FROM #TurnosDisponibles;

    -- Limpiar la tabla temporal
    DROP TABLE #TurnosDisponibles;
END;