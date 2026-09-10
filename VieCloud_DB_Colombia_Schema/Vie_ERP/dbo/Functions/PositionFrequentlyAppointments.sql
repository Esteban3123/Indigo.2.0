

CREATE FUNCTION [dbo].[PositionFrequentlyAppointments] (@Codigo varchar(10), @IDAgrupacion varchar(100))
RETURNS varchar (10)
AS
BEGIN

DECLARE @Resultado VARCHAR(10)

    IF @IDAgrupacion IS NULL
        RETURN NULL

    ;WITH Citas AS (
        SELECT 
            CODAUTONU,
            FECHORAIN,
            Posicion = ROW_NUMBER() OVER (PARTITION BY IdFrequentlyAppointment ORDER BY FECHORAIN),
            Total = COUNT(*) OVER (PARTITION BY IdFrequentlyAppointment)
        FROM AGASICITA
        WHERE IdFrequentlyAppointment = @IDAgrupacion
    )
    SELECT @Resultado = CAST(Posicion AS VARCHAR) + '/' + CAST(Total AS VARCHAR)
    FROM Citas
    WHERE CODAUTONU = @Codigo

    RETURN @Resultado

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula la posición de una cita dentro de un grupo de citas recurrentes o frecuentes, devolviendo un resultado en formato ''posición/total'' (por ejemplo, ''2/5''). Recibe el código de una cita específica y el identificador del grupo de agendamiento frecuente, y consulta la tabla de citas (AGASICITA) para determinar el orden cronológico de esa cita dentro del conjunto agrupado. Es útil para saber en qué lugar de la secuencia se encuentra una cita recurrente, por ejemplo ''esta es la 3ra cita de 5 programadas''. Se aplica en el módulo de agendamiento cuando se gestionan ciclos o series de citas médicas para un mismo paciente o propósito asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PositionFrequentlyAppointments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'PositionFrequentlyAppointments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la posición ordinal de una cita dentro de un grupo de citas frecuentes (formato "posición/total"), ordenada por fecha-hora de inicio.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de agrupación de citas frecuentes no debe ser nulo; en caso contrario retorna NULL sin consultar datos.; Debe existir al menos una cita en AGASICITA con el identificador de agrupación dado y el código indicado para obtener resultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La posición se calcula ordenando las citas del grupo por fecha-hora de inicio ascendente.; El total y la posición se calculan únicamente sobre citas que comparten el mismo identificador de agrupación frecuente.; El resultado tiene formato ''numero/numero'' (posición sobre total) cuando existe la cita.; Si la cita no pertenece al grupo, el resultado queda NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita médica; Citas frecuentes (agrupación de citas recurrentes)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El identificador de agrupación de citas frecuentes es NULL → Retorna NULL inmediatamente sin ejecutar la consulta else Calcula posición y total de la cita dentro del grupo y retorna ''Posicion/Total''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.AGASICITA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PositionFrequentlyAppointments';
GO
