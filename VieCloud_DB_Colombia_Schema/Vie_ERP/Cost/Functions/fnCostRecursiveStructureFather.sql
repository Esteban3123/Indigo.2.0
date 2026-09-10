
create FUNCTION [Cost].[fnCostRecursiveStructureFather]
(
	@StructureId int,
	@Level int = 0
)
RETURNS 
@TableReturn TABLE 
(
	Id int,
	Name varchar(100),
	NumberLevel int
)
AS
BEGIN
	
	
	insert into @TableReturn values(@StructureId,(select Name from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId), @Level)
	
       if (select ParentId from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId) is not null begin
		insert into @TableReturn
		select Id, Name, NumberLevel from [Cost].[fnCostRecursiveStructureFather]((select ParentId from [Cost].[CostOrganizationalStructureOfCosts] where Id = @StructureId), @Level + 1)
		
	end
	

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que recorre hacia arriba la jerarquía de la estructura organizacional de costos, partiendo de un nodo dado e identificando todos sus nodos padre hasta llegar a la raíz. Recibe el identificador de un nodo de la estructura de costos y devuelve la cadena de niveles superiores (Id, nombre y número de nivel) consultando la tabla CostOrganizationalStructureOfCosts. Se utiliza para conocer el árbol de ascendencia de un centro de costo o unidad organizacional dentro del modelo de costeo, permitiendo agrupar y reportar costos por niveles jerárquicos superiores.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructureFather';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'FUNCTION', @level1name = N'fnCostRecursiveStructureFather';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devolver la cadena ascendente de ancestros (padres) de un nodo de la estructura organizacional de costos, indicando el nivel jerárquico relativo de cada ancestro respecto al nodo inicial.', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El identificador de estructura debe existir en Cost.CostOrganizationalStructureOfCosts para obtener Name y ParentId válidos; La jerarquía de ParentId no debe contener ciclos para evitar recursión infinita; Profundidad de la cadena padre dentro del límite de anidamiento de SQL Server (32 niveles)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nodo de entrada siempre se incluye en el resultado con el nivel inicial recibido; Cada ancestro queda registrado con un nivel incrementado en 1 respecto a su descendiente; La recursión termina cuando se alcanza un nodo sin padre (raíz de la jerarquía)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Estructura organizacional de costos; Jerarquía de centros de costo; Nivel jerárquico', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Siempre se inserta el nodo recibido con su Name y el nivel actual; [INSERT] @TableReturn: Cuando ParentId del nodo actual no es NULL, se insertan los registros devueltos por la llamada recursiva sobre el padre con nivel+1', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El nodo actual tiene ParentId no nulo → Se invoca recursivamente la función con el ParentId y se incrementa el nivel en 1, agregando los ancestros al resultado else Se detiene la recursión y se devuelve solo lo acumulado hasta ese nodo (raíz alcanzada)', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Cost.fnCostRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Cost.CostOrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Cost', @level1type=N'FUNCTION', @level1name=N'fnCostRecursiveStructureFather';
GO
