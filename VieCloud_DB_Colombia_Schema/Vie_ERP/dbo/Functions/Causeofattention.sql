CREATE FUNCTION [dbo].[Causeofattention] (@Code as int)
RETURNS varchar (300)
AS
BEGIN

declare @Causeofattention varchar(300)
	
 SET @Causeofattention  = (SELECT Name FROM Causesofattention WHERE Code = @Code)
/*
Función que me va a listar la causa de atención.
*/

RETURN @Causeofattention

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que recibe un código numérico de causa de atención y devuelve el nombre descriptivo correspondiente, consultando el catálogo de causas de atención médica. Permite traducir el código de motivo de consulta o ingreso (urgencia, consulta externa, hospitalización, entre otros) a su descripción legible para el usuario. Se utiliza en reportes, pantallas y procesos que necesitan mostrar el nombre de la causa de atención a partir de su código, incluyendo la generación de reportes RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Causeofattention';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'Causeofattention';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Resolver el nombre descriptivo de una causa de atención a partir de su código.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Causesofattention cuyo Code coincida con el valor recibido para obtener un nombre; en caso contrario el resultado será NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Devuelve un único nombre de causa de atención asociado al código recibido; si no existe coincidencia, retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Causa de atención', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] dbo.Causesofattention: Retorna el campo Name de Causesofattention donde Code coincide con el código recibido.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.Causesofattention', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Causeofattention';
GO
