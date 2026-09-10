
CREATE FUNCTION [dbo].[AG_CitaExtra] (@CExtra as int)
RETURNS varchar (10)
AS
BEGIN

declare @grupo varchar(10)
SET @grupo=
     CASE WHEN @CExtra=0 THEN 'Normal' 
          WHEN @CExtra=1 THEN 'Cita Extra' 
	 END 
RETURN @grupo
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función que convierte un código numérico en la descripción del tipo de cita de agendamiento: 0 corresponde a ''Normal'' y 1 a ''Cita Extra''. Se usa para mostrar en reportes y consultas de agenda si una cita fue programada de manera regular o si fue una cita adicional fuera del cupo habitual. Sirve como etiqueta legible para el campo indicador de cita extra en el módulo de agendamiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_CitaExtra';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'AG_CitaExtra';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un indicador numérico de tipo de cita en una etiqueta legible para clasificar agendamientos como cita normal o extra.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reconocen dos valores válidos (0 y 1); cualquier otro produce NULL.; El resultado nunca excede 10 caracteres.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cita; Cita Extra; Agendamiento', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Cuando el indicador = 0 retorna ''Normal''; cuando = 1 retorna ''Cita Extra''; cualquier otro valor retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Indicador de cita extra = 0 → Devuelve ''Normal''; si Indicador de cita extra = 1 → Devuelve ''Cita Extra'' else Devuelve NULL para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'AG_CitaExtra';
GO
