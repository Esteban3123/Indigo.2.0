-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-10-19
-- Description:	Variacion mensual del costo de los productos
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ClosedMonthVariationCost]
	@MonthClosed AS INT,
	@YearClosed AS INT
AS
BEGIN
	DECLARE @Date DATE = '01/' + RIGHT( '0' + CAST(@MonthClosed AS VARCHAR(2)), 2) + '/'+ CAST(@YearClosed AS VARCHAR(4))
	DECLARE @DatePrevious DATE = DATEADD(MONTH, -1, @Date)
	DECLARE @MonthClosedPrevious INT = MONTH(@DatePrevious)
	DECLARE @YearClosedPrevious INT = YEAR(@DatePrevious)
	
	SELECT
		i.Code, i.Name,
		i.ProductCost, i.ProductCostPrevious,
		IIF( 
			i.ProductCostPrevious = 0, 
			1,
			((i.ProductCost-i.ProductCostPrevious)/i.ProductCostPrevious)
		) AS Variation
	FROM
	(
		SELECT 
			ip.Code, ip.Name,
			ip.ProductCost,
			ISNULL( cmi.ProductCost, 0 ) ProductCostPrevious
		FROM Inventory.PhysicalInventory [pi]
		INNER JOIN Inventory.Warehouse w
				ON [pi].WarehouseId = w.Id AND w.VirtualStore = 0
		INNER JOIN Inventory.InventoryProduct ip 
			ON [pi].ProductId = ip.Id
		LEFT JOIN Inventory.ClosedMonthInventory cmi
			ON ip.Id = cmi.ProductId
		LEFT JOIN Inventory.ClosedMonth cm
			ON cmi.ClosedMonthId = cm.Id
				AND cm.Month = @MonthClosedPrevious
				AND cm.Year = @YearClosedPrevious
		GROUP BY ip.Code, ip.Name, ip.ProductCost, cmi.ProductCost
	) as i
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la variación porcentual del costo de los productos de inventario entre el mes cerrado solicitado y el mes inmediatamente anterior. Compara el costo actual de cada producto (registrado en el catálogo maestro de productos) con el costo que tenía ese mismo producto al cierre del período previo (obtenido del historial de cierres mensuales). Solo considera productos presentes en el inventario físico de bodegas físicas (excluye bodegas virtuales). Es útil para reportes de control de costos, análisis de variaciones de precios de medicamentos e insumos, y auditoría contable del cierre mensual de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ClosedMonthVariationCost';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ClosedMonthVariationCost';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la variación mensual del costo de cada producto comparando el costo actual del inventario contra el costo registrado en el cierre del mes inmediatamente anterior.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Mes y año recibidos deben formar una fecha válida al concatenarse como ''01/MM/YYYY''; Debe existir un cierre mensual (ClosedMonth) correspondiente al mes anterior para obtener el costo previo; en caso contrario se asume 0; Solo se consideran bodegas no virtuales (VirtualStore = 0)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El periodo de comparación siempre es el mes inmediatamente anterior al recibido (DATEADD MONTH -1); Los productos asociados a bodegas virtuales se excluyen del cálculo; Si no hay registro de cierre previo para un producto, su costo previo se trata como 0; Evita división por cero al sustituir la variación por 1 cuando el costo previo es 0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Bodega; Producto de inventario; Cierre mensual de inventario; Costo de producto; Variación de costo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve por producto: Code, Name, ProductCost actual, ProductCostPrevious del cierre del mes anterior y Variation calculada', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductCostPrevious = 0 → Variation se fija en 1 (100%) else Variation = (ProductCost - ProductCostPrevious) / ProductCostPrevious', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ClosedMonthInventory; Inventory.ClosedMonth', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ClosedMonthVariationCost';
-- GO
