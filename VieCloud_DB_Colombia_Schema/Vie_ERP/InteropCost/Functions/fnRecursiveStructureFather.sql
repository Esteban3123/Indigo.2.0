
create FUNCTION [InteropCost].[fnRecursiveStructureFather]
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
	
	
	insert into @TableReturn values(@StructureId,(select Name from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId), @Level)
	
       if (select ParentId from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId) is not null begin
		insert into @TableReturn
		select Id, Name, NumberLevel from InteropCost.[fnRecursiveStructureFather]((select ParentId from InteropCost.OrganizationalStructureOfCosts where Id = @StructureId), @Level + 1)
		
	end
	

	RETURN 
END
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Función recursiva que recorre hacia arriba la jerarquía de la estructura organizacional de costos, partiendo de un nodo dado e identificando todos sus niveles padres hasta llegar a la raíz. Recibe el identificador de un nodo de la estructura (centro de costo, área o departamento) y devuelve la cadena de ancestros con su nombre y el nivel jerárquico que ocupa cada uno. Utiliza la tabla OrganizationalStructureOfCosts para navegar la relación padre-hijo a través del campo ParentId, llamándose a sí misma de forma recursiva mientras exista un padre. Se usa para reportería financiera de costos cuando se necesita conocer la ruta jerárquica completa de un centro de costo dentro del árbol organizacional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructureFather';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'FUNCTION', @level1name = N'fnRecursiveStructureFather';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Devuelve la cadena ascendente de ancestros (padres) de un nodo en la estructura organizacional de costos, indicando el nivel de profundidad de cada ancestro.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El nodo inicial debe existir en InteropCost.OrganizationalStructureOfCosts para que el Name no resulte NULL.; La jerarquía ParentId no debe contener ciclos, dado que la recursión termina solo al encontrar ParentId IS NULL.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nodo inicial siempre se incluye en el resultado con NumberLevel igual al @Level recibido (por defecto 0).; Cada ancestro aparece con un NumberLevel mayor que su descendiente, reflejando la profundidad ascendente en la jerarquía.; La recursión recorre exclusivamente hacia arriba (de hijo a padre) vía ParentId.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'estructura organizacional de costos; jerarquía padre-hijo de centros de costo; nivel jerárquico', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TableReturn: Inserta el nodo actual con (Id, Name, NumberLevel=@Level) en cada invocación recursiva.; [INSERT] @TableReturn: Si el nodo tiene ParentId no nulo, inserta los resultados de la llamada recursiva sobre el padre incrementando el nivel en 1.', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ParentId del nodo actual IS NOT NULL → Llama recursivamente a fnRecursiveStructureFather sobre el ParentId con @Level+1 y agrega los resultados a la tabla de retorno. else Termina la recursión sin agregar más filas (queda solo el nodo raíz alcanzado).', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'InteropCost.fnRecursiveStructureFather', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'InteropCost.OrganizationalStructureOfCosts', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'InteropCost', @level1type=N'FUNCTION', @level1name=N'fnRecursiveStructureFather';
GO
