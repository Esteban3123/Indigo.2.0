-- =============================================
-- Author:		Juan Patiño
-- Create date: 13/10/2016
-- Description:	Funcion Disponibilidad de dia Entidad Consulta Prioritaria
-- =============================================
CREATE FUNCTION [dbo].[fnDisponibilidadEntidad] 
(
	@FechaTriage as date, @CODEntidad as char(9)
)
RETURNS bit 
AS
BEGIN
	
	declare @NumeroDia as integer = ( SELECT DATEPART(DW,@FechaTriage) )
	declare @Disponible as bit

	Select  @Disponible =
						case @NumeroDia
										when 1 then (select LUN from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 2 then (select MAR  from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 3 then (select MIER  from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 4 then (select JUE from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 5 then (select VIER from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
										when 6 then (select SAB from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad   )
										when 7 then (select DOM from dbo.ADPARCONPRI where CODENTIDA = @CODEntidad  )
						 END 
	
	 
	return @Disponible
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Determina si una entidad (aseguradora o convenio) tiene disponibilidad de atención en consulta prioritaria o urgencias para un día específico. Recibe como parámetros la fecha del triage y el código de la entidad, identifica el día de la semana de esa fecha y consulta en la tabla de parámetros de contrato (ADPARCONPRI) si ese día (lunes a domingo) está habilitado para atención por parte de dicha entidad. Retorna un valor de tipo bit: verdadero si la entidad atiende ese día, falso si no. Se usa para validar la disponibilidad de la entidad antes de registrar o procesar un triage en consulta prioritaria.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnDisponibilidadEntidad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'fnDisponibilidadEntidad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Determina si una entidad (convenio/EPS) tiene habilitada la atención de consulta prioritaria para el día de la semana correspondiente a una fecha dada.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en ADPARCONPRI para el código de entidad consultado; El resultado del mapeo día-columna asume una configuración específica de DATEFIRST en la sesión', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La disponibilidad depende exclusivamente del día de la semana de la fecha indicada y del registro de parametrización de la entidad; El mapeo día-columna sigue el orden DATEPART(DW) del servidor; con DATEFIRST=7 (default US): 1=LUN, 2=MAR, ..., 7=DOM; Si no existe registro en ADPARCONPRI para la entidad, el resultado es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Consulta prioritaria; Disponibilidad por entidad; Días hábiles de atención; Parámetros de convenio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Devuelve el bit del día correspondiente (LUN/MAR/MIER/JUE/VIER/SAB/DOM) de ADPARCONPRI según DATEPART(DW) de la fecha, filtrando por CODENTIDA', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Día de la semana = 1 (domingo según DATEPART DW) → Retorna el flag LUN de la entidad; si Día de la semana = 2 → Retorna el flag MAR; si Día de la semana = 3 → Retorna el flag MIER; si Día de la semana = 4 → Retorna el flag JUE; si Día de la semana = 5 → Retorna el flag VIER; si Día de la semana = 6 → Retorna el flag SAB; si Día de la semana = 7 → Retorna el flag DOM', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADPARCONPRI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'fnDisponibilidadEntidad';
GO
