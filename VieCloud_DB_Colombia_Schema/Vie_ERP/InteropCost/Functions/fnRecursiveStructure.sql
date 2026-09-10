-- =============================================
-- Author:		Jhon Jairo Salas
-- Create date: 05/12/2016
-- Description:	Funcion recursiva para obtener todos los Id Hijos
-- =============================================
CREATE FUNCTION [InteropCost].[fnRecursiveStructure]
(
	@StructureId int
)
RETURNS 
@TableReturn TABLE 
(
	Id int
)
AS
BEGIN
	
	insert into @TableReturn values(@StructureId)

	declare @IdSon int
	

	DECLARE structure_cursor CURSOR FOR   
	select Id from InteropCost.OrganizationalStructureOfCosts where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon 
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn
		select Id from InteropCost.fnRecursiveStructure(@IdSon)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que, dado un nodo de la estructura organizacional de costos (centro de costo, área o departamento), devuelve el identificador de ese nodo y todos los identificadores de sus nodos hijos en cualquier nivel de profundidad de la jerarquía. Recorre de forma recursiva la tabla de estructura organizacional de costos consultando los registros cuyo padre coincide con el nodo recibido, acumulando todos los IDs descendientes. Se utiliza para obtener el árbol completo de centros de costo subordinados a un nodo dado, lo que permite filtrar costos, gastos o reportes financieros por toda una rama jerárquica sin importar cuántos niveles tenga.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructure';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve recursivamente todos los identificadores descendientes (incluido el nodo raíz) dentro de la jerarquía de estructura organizacional de costos.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador recibido debe existir o ser referenciable como ParentId en InteropCost.OrganizationalStructureOfCosts para que devuelva descendientes; en caso contrario solo retorna el propio Id.; La jerarquía padre-hijo en OrganizationalStructureOfCosts no debe contener ciclos, pues la recursión entraría en bucle infinito.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nodo raíz (@StructureId) siempre forma parte del resultado.; El resultado contiene al nodo y a todos sus descendientes transitivos según la relación ParentId.; La función recorre la jerarquía vía cursor + autollamada recursiva, no mediante CTE recursivo.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Jerarquía padre-hijo de centros de costo', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Siempre inserta el @StructureId recibido como primer registro del resultado.; [INSERT] @TableReturn: Por cada fila en OrganizationalStructureOfCosts cuyo ParentId = @StructureId, inserta los Ids retornados por la llamada recursiva fnRecursiveStructure(@IdSon).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas en InteropCost.OrganizationalStructureOfCosts con ParentId = @StructureId (cursor con @@FETCH_STATUS = 0) → Itera por cada hijo y agrega recursivamente sus descendientes al resultado else Retorna únicamente el @StructureId inicial sin descendientes', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.OrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructure';
GO
