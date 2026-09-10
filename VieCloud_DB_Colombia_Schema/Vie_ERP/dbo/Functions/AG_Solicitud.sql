
CREATE FUNCTION [dbo].[AG_Solicitud] (@Solicitud as int)
RETURNS varchar (12)
AS
BEGIN

declare @grupo varchar(12)
SET @grupo=
     CASE WHEN @Solicitud=0 THEN 'Presencial' 
          WHEN @Solicitud=1 THEN 'Telefónica' 
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte el código numérico del tipo de solicitud de agendamiento en su descripción legible: 0 = ''Presencial'', 1 = ''Telefónica''. Se usa para mostrar en reportes y consultas de agenda la modalidad con la que el paciente solicitó su cita, en lugar del código interno. Toca la entidad de agendamiento o turnos médicos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Solicitud';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_Solicitud';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de tipo de solicitud a su etiqueta descriptiva (''Presencial'' o ''Telefónica'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de solicitud debe ser 0 o 1 para obtener una etiqueta; otros valores devuelven NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo reconoce dos códigos válidos (0 y 1); cualquier otro valor produce NULL; El resultado siempre es una cadena de máximo 12 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud; Modalidad de atención (Presencial/Telefónica)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Valor de entrada = 0 → Retorna ''Presencial''; si Valor de entrada = 1 → Retorna ''Telefónica'' else Retorna NULL (CASE sin ELSE)', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_Solicitud';
GO
