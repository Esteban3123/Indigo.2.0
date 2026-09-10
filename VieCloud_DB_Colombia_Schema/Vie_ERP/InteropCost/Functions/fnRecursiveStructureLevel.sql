
-- =============================================
-- Author:		Carlos Jhefersson Muñoz Ramirez
-- Create date: 10/04/2017
-- Description:	Funcio recursiva para obtener la structure Organizacional
-- =============================================
create FUNCTION [InteropCost].[fnRecursiveStructureLevel]
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
	select @StructureId, Code, [Name], ParentId, @Level from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId

	declare @IdSon int
	declare @CodeSon varchar(20)
	declare @NameSon varchar(100)
	declare @ParentIdSon int

	DECLARE structure_cursor CURSOR FOR   
	select Id,Code,[Name],ParentId from InteropCost.OrganizationalStructureOfCosts where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn
		select Id,Code,[Name],ParentId,NumberLevel + 1 from InteropCost.fnRecursiveStructureLevel(@IdSon,@Level)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon,@CodeSon,@NameSon,@ParentIdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que recorre la jerarquía completa de la estructura organizacional de costos a partir de un nodo raíz dado, devolviendo todos los niveles descendientes (hijos, nietos, etc.) junto con su profundidad dentro del árbol. Utiliza la tabla OrganizationalStructureOfCosts para navegar las relaciones padre-hijo entre centros de costo, áreas y departamentos de la institución. Se invoca a sí misma recursivamente para expandir cada rama del árbol, retornando el identificador, código, nombre, nodo padre y número de nivel de cada unidad. Es útil para reportería financiera, distribución de gastos y visualización del árbol de centros de costo en cualquier nivel de la jerarquía organizacional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructureLevel';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructureLevel';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recorre recursivamente la jerarquía de la estructura organizacional de costos a partir de un nodo raíz, devolviendo cada nodo descendiente con su nivel de profundidad relativo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de estructura recibido debe existir en InteropCost.OrganizationalStructureOfCosts para que el nodo raíz aparezca en el resultado.; La relación ParentId debe representar una jerarquía acíclica para evitar recursión infinita.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nodo raíz invocado siempre se incluye en el resultado con NumberLevel igual al @Level recibido.; Cada descendiente se devuelve con un NumberLevel mayor al de su padre, reflejando la profundidad jerárquica.; El cursor se cierra y libera (CLOSE/DEALLOCATE) tras recorrer todos los hijos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Centros de costo; Jerarquía padre-hijo; Niveles organizacionales', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Inserta el nodo cuyo Id coincide con el identificador recibido, asignándole el nivel actual (@Level).; [INSERT] @TableReturn: Por cada hijo (ParentId = @StructureId) inserta los nodos retornados por la llamada recursiva, incrementando NumberLevel en 1 respecto al nivel devuelto por el hijo.; [RETURN_RESULT] @TableReturn: Retorna la tabla con columnas Id, Code, Name, ParentId y NumberLevel representando la jerarquía descendiente desde el nodo raíz.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas en OrganizationalStructureOfCosts con ParentId = @StructureId (cursor encuentra hijos) → Para cada hijo se invoca recursivamente fnRecursiveStructureLevel y sus resultados se agregan con NumberLevel + 1. else El nodo se considera hoja y solo se inserta a sí mismo en el resultado.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructureLevel', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.OrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureLevel';
GO
