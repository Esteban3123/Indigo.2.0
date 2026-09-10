
create FUNCTION [dbo].[DiferenciaDias] (@Fecha as datetime)
RETURNS int
AS
BEGIN

declare @Dias int

SELECT @Dias = DATEDIFF(DAY, @Fecha, GETDATE()) + 1 


RETURN @Dias

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad de días transcurridos desde una fecha dada hasta el día de hoy, sumando 1 para incluir el día inicial en el conteo. Recibe como parámetro una fecha (por ejemplo, fecha de ingreso del paciente, fecha de orden médica o fecha de atención) y devuelve un número entero con la diferencia en días. Se utiliza típicamente para determinar días de estancia hospitalaria, antigüedad de una orden, tiempo de espera en agendamiento u otras métricas de duración en el contexto clínico y administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaDias';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'DiferenciaDias';
GO
