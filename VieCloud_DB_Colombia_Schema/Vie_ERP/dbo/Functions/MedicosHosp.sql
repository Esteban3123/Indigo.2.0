

CREATE FUNCTION [dbo].[MedicosHosp] (@Paciente as varchar(25), @Ingreso as varchar(10))
RETURNS nvarchar (20)
AS
BEGIN

declare @Medico nvarchar(20)

SELECT TOP 1 @Medico=CODPROSAL FROM CHMEDITRA WHERE NUMINGRES=@Ingreso AND IPCODPACI=@Paciente

RETURN @Medico

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que devuelve el código del médico o profesional de salud asignado a un paciente durante un ingreso hospitalario específico. Recibe como parámetros la cédula del paciente y el número de ingreso, y consulta el historial de traslados de camas (CHMEDITRA) para obtener el primer profesional de salud registrado en ese ingreso. Se usa para identificar rápidamente el médico tratante o responsable de un paciente hospitalizado dado su ingreso.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicosHosp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicosHosp';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene el código del profesional de salud asociado al tratamiento de un paciente durante un ingreso hospitalario específico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en CHMEDITRA que coincida con el ingreso y paciente indicados; de lo contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve un único código de médico (TOP 1) aunque existan múltiples registros para la combinación paciente/ingreso.; No modifica datos: es función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; ingreso hospitalario; médico tratante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.CHMEDITRA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicosHosp';
GO
