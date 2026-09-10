CREATE FUNCTION [dbo].[PrioridadLaboratorio]
(
@CodigoPrioridad As char 
)
RETURNS nvarchar (15)
AS
BEGIN

    DECLARE @Prioridad as nvarchar(15)

    SELECT @Prioridad = CASE @CodigoPrioridad WHEN '1' THEN 'Urgente' WHEN '2' THEN 'Rutinario' END 

    RETURN @Prioridad
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que traduce un código de prioridad de laboratorio a su descripción textual: el código ''1'' retorna ''Urgente'' y el código ''2'' retorna ''Rutinario''. Cualquier valor distinto devuelve NULL. Se utiliza para mostrar la prioridad legible de órdenes o solicitudes de laboratorio en reportes o consultas.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código de prioridad de laboratorio a su descripción textual (''Urgente'' o ''Rutinario'').', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código de prioridad debe ser de tipo char para ser evaluado por el CASE', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reconocen dos códigos válidos: ''1'' (Urgente) y ''2'' (Rutinario); cualquier otro valor produce NULL; La descripción retornada nunca excede 15 caracteres', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Prioridad de laboratorio; Urgente; Rutinario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código de prioridad = ''1'' → Devuelve ''Urgente''; si Código de prioridad = ''2'' → Devuelve ''Rutinario'' else Devuelve NULL para cualquier otro valor', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'PrioridadLaboratorio';
GO
