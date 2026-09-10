

CREATE FUNCTION [dbo].[Productos2012] (@Registro as nvarchar(100))
RETURNS nvarchar (60)
AS
BEGIN

declare @Producto nvarchar(60)

SELECT top 1 @Producto = IPRCODIGO
FROM        IHRINDDGH
WHERE CODPRODUC = @REGISTRO

RETURN @Producto

END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Función escalar que, dado un código de registro como entrada, consulta la tabla `IHRINDDGH` y retorna el código de producto (`IPRCODIGO`) asociado, tomando el primer registro coincidente. Actúa como un lookup de código de producto a partir de un identificador de registro en dicha tabla.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve el código de producto interno asociado a un código de registro de producto consultando la tabla maestra de productos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla IHRINDDGH con las columnas IPRCODIGO y CODPRODUC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre retorna a lo sumo un valor (TOP 1) sin criterio de ordenamiento explícito; Si no existe coincidencia en IHRINDDGH, el resultado es NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'producto; código de producto; registro de producto', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] IHRINDDGH: Cuando CODPRODUC coincide con el registro recibido, retorna el primer IPRCODIGO encontrado; si no hay coincidencia retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHRINDDGH', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'Productos2012';
GO
