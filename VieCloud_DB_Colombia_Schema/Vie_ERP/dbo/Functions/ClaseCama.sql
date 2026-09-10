
CREATE FUNCTION [dbo].[ClaseCama] (@CodigoClaseCama as int)
RETURNS nvarchar (30)
AS
BEGIN

declare @NomClaseCama nvarchar(30)

SELECT @NomClaseCama = CASE @CodigoClaseCama WHEN '1' THEN 'Observacion Urgencias' WHEN '2' THEN 'Recuperacion Post-Quirurgico' WHEN '3' THEN 'Hospitalaria' WHEN '4' THEN 'Cuna de Observación' END

RETURN @NomClaseCama

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que convierte un código numérico de clase de cama en su nombre descriptivo. Dado un número entero, retorna el tipo de cama correspondiente: 1 = Observación Urgencias, 2 = Recuperación Post-Quirúrgico, 3 = Hospitalaria, 4 = Cuna de Observación. Se utiliza para mostrar en reportes y consultas el nombre legible de la clase o tipo de cama asignada a un paciente durante su estadía, en lugar del código interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ClaseCama';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'ClaseCama';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Traduce un código numérico de clase de cama a su descripción textual estandarizada para uso en reportes y consultas hospitalarias.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El código recibido debe corresponder a uno de los valores definidos (1-4); cualquier otro valor produce NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El catálogo de clases de cama está fijo en el código (hardcoded) con 4 valores posibles.; La descripción retornada nunca excede 30 caracteres.; Códigos fuera del rango 1-4 siempre retornan NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Clase de cama; Observación de Urgencias; Recuperación Post-Quirúrgica; Hospitalización; Cuna de Observación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Código = 1 → Devuelve ''Observacion Urgencias''; si Código = 2 → Devuelve ''Recuperacion Post-Quirurgico''; si Código = 3 → Devuelve ''Hospitalaria''; si Código = 4 → Devuelve ''Cuna de Observación'' else Devuelve NULL para cualquier otro código', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'ClaseCama';
GO
