
CREATE FUNCTION [dbo].[AG_Tipo] (@Tipo as int)
RETURNS varchar (15)
AS
BEGIN

declare @grupo varchar(15)
SET @grupo=
     CASE WHEN @Tipo=0 THEN 'Primera Vez' 
          WHEN @Tipo=1 THEN 'Control' 
		  WHEN @Tipo=2 THEN 'Pos Operatorio'
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convierte el código numérico del tipo de consulta o cita médica en su descripción legible para el usuario. Recibe un número entero y devuelve el nombre correspondiente: 0 = ''Primera Vez'', 1 = ''Control'', 2 = ''Pos Operatorio''. Se usa en módulos de agendamiento y atención ambulatoria para mostrar la clasificación de la cita en reportes, pantallas y documentos clínicos, evitando que el usuario final vea códigos numéricos internos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Tipo';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Tipo';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce el código numérico de tipo de cita/agenda a su descripción textual (Primera Vez, Control o Pos Operatorio).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de tipo recibido debe ser entero; valores fuera del dominio {0,1,2} retornan NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo reconoce los códigos 0, 1 y 2; cualquier otro valor produce NULL; El texto retornado nunca excede 15 caracteres; Mapeo fijo: 0→Primera Vez, 1→Control, 2→Pos Operatorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita/Agenda; Primera Vez; Control; Pos Operatorio', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Tipo = 0 → Devuelve ''Primera Vez''; si Tipo = 1 → Devuelve ''Control''; si Tipo = 2 → Devuelve ''Pos Operatorio''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Tipo';
GO
