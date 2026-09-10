
-- =============================================
-- Author:		Nicolas Pulido
-- Create date: 10/04/2017
-- Description:	Funcion recursiva para obtener la estructura Organizacional
-- =============================================
create FUNCTION [Cost].[fnCostRecursiveStructureLevel]
(
	@StructureId int,
	@Level int = 0
)
RETURNS 
@TableReturn TABLE 
(
	Id int,
	Code varchar(20),
	[Name] varchar(100),
	ParentId int,
	NumberLevel int
)
AS
BEGIN
	
	insert into @TableReturn
	select @StructureId, Code, [Name], ParentId, @Level from Cost.CostOrganizationalStructureOfCosts where Id = @StructureId

	declare @IdSon int
	declare @CodeSon varchar(20)
	declare @NameSon varchar(100)
	declare @ParentIdSon int

	DECLARE structure_cursor CURSOR FOR   
	select Id,Code,[Name],ParentId from Cost.CostOrganizationalStructureOfCosts where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn		
		select Id,Code,[Name],ParentId,NumberLevel + 1 from Cost.fnCostRecursiveStructureLevel(@IdSon,@Level)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que recorre la estructura organizacional de costos en todos sus niveles jerárquicos, partiendo de un nodo raíz identificado por su ID y descendiendo hacia todos sus hijos y sub-niveles. Consulta la tabla de estructura organizacional de costos (CostOrganizationalStructureOfCosts) para obtener el código, nombre, padre y nivel de profundidad de cada nodo. Devuelve una tabla plana con toda la jerarquía desplegada, indicando en qué nivel de profundidad se encuentra cada elemento (centros de costo, agrupaciones, unidades). Se usa para reportería y análisis de costos cuando se necesita visualizar o filtrar la estructura organizacional completa de centros de costo a partir de cualquier punto de la jerarquía.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructureLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructureLevel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Obtiene de forma recursiva el subárbol de la estructura organizacional de costos a partir de un nodo dado, devolviendo cada nodo con su nivel jerárquico relativo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en la estructura organizacional de costos con el identificador recibido para que la raíz quede representada con datos.; La relación padre-hijo (ParentId) debe ser acíclica para evitar recursión infinita.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nodo raíz solicitado se devuelve con el nivel inicial recibido como parámetro.; Cada descendiente se incluye con un nivel incrementado en 1 respecto al nivel asignado a su padre lógico en la recursión.; Solo se recorren nodos accesibles vía la relación ParentId desde el nodo de inicio (subárbol descendente).; Cada nodo del subárbol aparece una sola vez por rama de descendencia.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Centros de costo; Jerarquía organizacional', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] @TableReturn: Inserta el nodo inicial con el nivel recibido tomando los datos desde CostOrganizationalStructureOfCosts donde Id = @StructureId.; [RETURN_RESULT] @TableReturn: Para cada hijo (registros con ParentId = @StructureId) invoca recursivamente la función e inserta sus filas con NumberLevel incrementado en 1.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.fnCostRecursiveStructureLevel', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostOrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureLevel';
GO
