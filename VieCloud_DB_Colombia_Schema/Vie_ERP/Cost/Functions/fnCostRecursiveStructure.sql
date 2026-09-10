
-- =============================================
-- Author:		Jhon Jairo Salas
-- Create date: 05/12/2016
-- Description:	Funcio recursiva para obtener todos los Id Hijos
-- =============================================
create FUNCTION [Cost].[fnCostRecursiveStructure]
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
	select Id from [Cost].[CostOrganizationalStructureOfCosts] where ParentId = @StructureId
  
	OPEN structure_cursor  
  
	FETCH NEXT FROM structure_cursor   
	INTO @IdSon 
  
	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		
		insert into @TableReturn
		select Id from [Cost].[fnCostRecursiveStructure](@IdSon)

		FETCH NEXT FROM structure_cursor   
		INTO @IdSon
	END   
	CLOSE structure_cursor;  
	DEALLOCATE structure_cursor;

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que, dado un nodo de la estructura organizacional de costos, devuelve todos sus identificadores descendientes (hijos, nietos y demás niveles inferiores). Recorre de forma recursiva la tabla de estructura organizacional de costos, siguiendo las relaciones padre-hijo definidas por el campo ParentId. Se utiliza para obtener el árbol completo de unidades o centros de costo que dependen de un nodo raíz, lo que permite consolidar y analizar costos a cualquier nivel jerárquico de la organización.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructure';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructure';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve recursivamente todos los identificadores descendientes (incluido el nodo raíz dado) de la jerarquía de estructura organizacional de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de estructura recibido debe existir en la jerarquía de centros de costo para obtener descendientes; si no existe, sólo se retorna a sí mismo.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado siempre contiene al menos un registro: el Id de entrada.; El recorrido es descendente: nunca incluye ancestros del nodo de entrada.; La jerarquía se resuelve mediante la relación Id ↔ ParentId de la estructura organizacional de costos.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Jerarquía de centros de costo; Relación padre-hijo de unidades de costo', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Siempre se inserta el Id raíz recibido como primer registro del resultado.; [INSERT] @TableReturn: Por cada fila en CostOrganizationalStructureOfCosts cuyo ParentId coincide con el nodo actual, se insertan recursivamente todos sus descendientes.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen filas con ParentId igual al nodo actual (cursor con @@FETCH_STATUS = 0) → Se invoca recursivamente la función para cada hijo y se acumulan sus Ids else Se cierra el cursor y se retorna sólo con el nodo raíz acumulado', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.fnCostRecursiveStructure', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostOrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructure';
GO
