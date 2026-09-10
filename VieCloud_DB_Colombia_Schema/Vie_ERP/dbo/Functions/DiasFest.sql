
CREATE FUNCTION [dbo].[DiasFest] (@FechaIngreso as datetime, @FechaCita as datetime,@NumIngreso as char(10))
RETURNS nvarchar (max)
AS
BEGIN

declare @Cant nvarchar (max)

SELECT @Cant = count (diahabil)
from dbo.Tiempo as a,dbo.adingreso as b 
where b.fecha between CONVERT(VARCHAR(11),@FechaIngreso,106) and CONVERT(VARCHAR(11), @FechaCita,106) 
and diahabil='1' and @Numingreso= numingres

RETURN @Cant

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que calcula la cantidad de días hábiles (no festivos) entre la fecha de ingreso y la fecha de una cita o evento, para un número de ingreso específico. Cruza el calendario laboral de la tabla de Tiempo con los registros de admisión de pacientes (ADINGRESO) para contar únicamente los días marcados como hábiles dentro del rango de fechas indicado. Se usa para determinar cuántos días hábiles transcurrieron durante un episodio de atención, útil en procesos de agendamiento, control de tiempos de espera o auditoría de estancias. Recibe como parámetros la fecha de ingreso del paciente, la fecha de la cita o corte, y el número de ingreso del episodio asistencial.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiasFest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiasFest';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad de días hábiles existentes entre la fecha de ingreso y la fecha de cita para un ingreso específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en adingreso cuyo numingres coincida con el identificador de ingreso recibido.; La tabla Tiempo debe contener registros con la marca de día hábil para el rango consultado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se cuentan días marcados como hábiles (diahabil=''1'').; El conteo se restringe al ingreso indicado mediante coincidencia con numingres.; El rango de fechas se evalúa convirtiendo las fechas a formato VARCHAR(106), lo que puede afectar la precisión del filtro.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ingreso; días hábiles; fecha de ingreso; fecha de cita', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.Tiempo: Cuando diahabil=''1'' y numingres coincide con el ingreso dado y la fecha está entre fecha de ingreso y fecha de cita, se cuenta y se retorna el total de días hábiles.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.Tiempo; dbo.adingreso', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'DiasFest';
GO
