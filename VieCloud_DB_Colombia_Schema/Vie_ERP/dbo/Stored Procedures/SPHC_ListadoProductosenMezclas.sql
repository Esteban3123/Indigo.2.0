
CREATE PROCEDURE [dbo].[SPHC_ListadoProductosenMezclas]
AS
BEGIN
	SET NOCOUNT ON;

SELECT RTRIM(D.CODPRODUC) AS Codigo, RTRIM(D.ABRPROMEZ) AS 'Descripcion DCI', RTRIM(D.DESPRODUC) AS Medicamento, COALESCE(NULLIF(D.CODDCIMED, CAST('' AS CHAR)),CAST('' AS CHAR)) AS 'Codigo DCI', phy.Quantity AS Disponibles
from dbo.IHLISTPRO D With(Nolock)
	INNER JOIN Inventory.ATC C  With(Nolock) ON D.CODPRODUC = C.Code
	LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
	LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
WHERE TIPFORMED IN ('1','2','3') AND TIPPRODUC IN ('1','3') AND PROESTADO = 1 
end
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista los medicamentos e insumos habilitados que pueden utilizarse en la preparación de mezclas (por ejemplo, mezclas intravenosas o nutrición parenteral). Para cada producto devuelve el código interno, la descripción DCI (Denominación Común Internacional), el nombre comercial del medicamento y el stock físico disponible en bodega. Combina el catálogo maestro de productos farmacéuticos con la clasificación ATC y el inventario físico, filtrando únicamente formas farmacéuticas líquidas o inyectables (TIPFORMED 1, 2, 3), tipos de producto aptos para mezclas (TIPPRODUC 1 y 3) y productos activos (PROESTADO = 1). Se usa principalmente por el servicio de farmacia para consultar qué medicamentos están disponibles y en qué cantidad al momento de preparar mezclas.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'PROCEDURE', @level1name = N'SPHC_ListadoProductosenMezclas';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los productos farmacéuticos activos aptos para preparación en mezclas, mostrando su código, DCI, descripción y cantidad disponible en inventario físico.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos deben tener PROESTADO = 1 (activos); El tipo de forma medicamentosa (TIPFORMED) debe ser ''1'', ''2'' o ''3''; El tipo de producto (TIPPRODUC) debe ser ''1'' o ''3''; Debe existir correspondencia entre el código del producto en IHLISTPRO y el Code en Inventory.ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran productos activos (PROESTADO = 1); Solo se consideran productos de inventario con Status = 1; El Código DCI nunca se devuelve NULL: si está vacío o nulo se reemplaza por cadena vacía; Los campos de texto se devuelven sin espacios a la derecha (RTRIM); Filtra exclusivamente formas medicamentosas y tipos de producto compatibles con preparación de mezclas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'medicamento; producto farmacéutico; mezclas (preparados magistrales); DCI (Denominación Común Internacional); clasificación ATC; forma medicamentosa; inventario físico; disponibilidad en bodega', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve productos con TIPFORMED IN (''1'',''2'',''3'') AND TIPPRODUC IN (''1'',''3'') AND PROESTADO = 1, incluyendo cantidad física disponible mediante LEFT JOIN a InventoryProduct (Status=1) y PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SPHC_ListadoProductosenMezclas';
-- GO
