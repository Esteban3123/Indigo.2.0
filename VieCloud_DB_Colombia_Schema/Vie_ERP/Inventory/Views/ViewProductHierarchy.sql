

CREATE VIEW [Inventory].[ViewProductHierarchy]
AS

WITH CTE AS (
  SELECT 
	PH.Id, 
	PH.HierarchyProductFinalId, 
	PH.ParentProductId, 
	PH.ProductId, 
	IP.Name, 
	LOG(PH.ConversionUnit) AS LogConversionUnit
  FROM Inventory.ProductHierarchy PH
  JOIN Inventory.InventoryProduct IP ON IP.Id = PH.ParentProductId
)
SELECT 
	CTE.Id, 
	CTE.HierarchyProductFinalId,
	CTE.ParentProductId, 
	CTE.ProductId, 
	CTE.Name,
   EXP(SUM(CTE.LogConversionUnit) OVER (PARTITION BY CTE.HierarchyProductFinalId ORDER BY CTE.Id)) AS Quantity
FROM CTE
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que expone la jerarquía completa de presentaciones de productos del inventario (medicamentos, insumos, dispositivos médicos), calculando de forma acumulada la cantidad equivalente en la unidad base (unidad mínima de dispensación) para cada nivel de la cadena de empaque, por ejemplo: caja → blíster → tableta. Combina la tabla de relaciones padre-hijo de productos (ProductHierarchy) con el catálogo maestro de artículos (InventoryProduct) y aplica logaritmos acumulados sobre el factor de conversión para obtener la cantidad total de unidades mínimas que representa cada nivel jerárquico. Es útil para reportes de equivalencias de presentaciones, conversión de unidades de compra a unidades de dispensación, y control de stock en diferentes niveles de empaque.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewProductHierarchy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewProductHierarchy';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone la jerarquía de productos del inventario calculando la cantidad acumulada de conversión desde la raíz hasta cada nivel mediante el producto de las unidades de conversión.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada registro de jerarquía debe tener un producto padre existente en el catálogo de productos.; La unidad de conversión debe ser estrictamente positiva (LOG no admite cero ni negativos).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La cantidad acumulada se obtiene como producto multiplicativo de las unidades de conversión a lo largo de la jerarquía (implementado vía EXP(SUM(LOG))).; Solo se incluyen niveles cuyo producto padre exista en el catálogo de productos (JOIN interno).; La acumulación se reinicia por cada HierarchyProductFinalId (PARTITION BY) y respeta el orden ascendente de Id.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'jerarquía de productos; producto de inventario; unidad de conversión; producto padre', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ProductHierarchy: Devuelve un registro por cada fila de jerarquía con su producto padre, producto hijo y la cantidad acumulada (EXP de la suma de logaritmos de la unidad de conversión) particionada por la jerarquía final y ordenada por Id.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ProductHierarchy; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewProductHierarchy';
GO
