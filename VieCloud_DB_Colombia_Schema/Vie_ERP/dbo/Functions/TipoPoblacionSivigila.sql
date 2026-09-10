
CREATE FUNCTION [dbo].[TipoPoblacionSivigila] (@IdFichaSivigila as int,@CodigoTipoPoblacion as int)
RETURNS varchar (1)
AS
BEGIN

declare @TipoPoblacionMarcada varchar (1)

IF (Select COUNT(*) from HCFICHAPOBLACIONAL where IDFICHANOTIFICACION = @IdFichaSivigila AND CODPOBLACION = @CodigoTipoPoblacion) > 0 BEGIN
	set @TipoPoblacionMarcada = 'X'
END 

RETURN @TipoPoblacionMarcada 

END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función escalar que verifica si una ficha de notificación SIVIGILA tiene registrado un tipo de población específico. Recibe el identificador de la ficha de notificación en salud pública y el código del grupo poblacional, y retorna ''X'' si existe esa combinación en la tabla de población de fichas, o nulo si no existe. Se utiliza para marcar casillas de tipo de población en los reportes SIVIGILA, indicando a qué grupo pertenece el paciente notificado (por ejemplo: gestante, menor de edad, indígena, entre otros).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoPoblacionSivigila';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'TipoPoblacionSivigila';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Indica con ''X'' si una ficha de notificación Sivigila tiene asociado un determinado tipo de población; en caso contrario retorna NULL.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la tabla HCFICHAPOBLACIONAL con las columnas IDFICHANOTIFICACION y CODPOBLACION; Los identificadores de ficha y código de población deben corresponder a valores válidos en el dominio Sivigila', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de retorno solo puede ser ''X'' o NULL; No se modifican datos; la función es de solo lectura', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ficha de notificación Sivigila; Población poblacional de la ficha; Tipo de población', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (retorno de función): Cuando COUNT(*) en HCFICHAPOBLACIONAL filtrado por IDFICHANOTIFICACION y CODPOBLACION es > 0, retorna ''X''; en caso contrario retorna NULL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe al menos un registro en HCFICHAPOBLACIONAL para la ficha de notificación y el código de población indicados → Devuelve ''X'' indicando que el tipo de población está marcado else Devuelve NULL (variable sin asignar) indicando que el tipo de población no está marcado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.HCFICHAPOBLACIONAL', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'TipoPoblacionSivigila';
GO
