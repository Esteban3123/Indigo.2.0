
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE  FUNCTION [dbo].[GetLetter]
(	
	-- Add the parameters for the function here
	@ScheduleDetailId int
)
RETURNS Char(1) 

Begin

declare @Letter char(1)

	-- Add the SELECT statement with parameter references here
	 (select @letter = letter from Payroll.ScheduleDetail where id = @ScheduleDetailId )

	 return @Letter

end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recupera la letra o código de turno asignado a un detalle específico del horario de un empleado. Dado el identificador único de un registro de detalle de turno (ScheduleDetailId), consulta la tabla de detalle de jornadas de nómina y devuelve el carácter que representa el tipo de turno o jornada programada (por ejemplo, ''M'' para mañana, ''T'' para tarde, ''N'' para noche). Se utiliza para obtener de forma puntual el identificador de letra del turno dentro de los procesos de programación de horarios y gestión de nómina.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetLetter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'GetLetter';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene la letra identificadora asociada a un detalle de jornada/turno programado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Payroll.ScheduleDetail con el id consultado para retornar un valor; de lo contrario se retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre es un único CHAR(1) correspondiente al detalle de turno indicado.; No modifica datos; es función de solo lectura.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Detalle de turno/jornada programada; Letra identificadora de turno', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Payroll.ScheduleDetail: Cuando id = parámetro recibido, retorna el valor de la columna letter como CHAR(1).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Payroll.ScheduleDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'GetLetter';
GO
