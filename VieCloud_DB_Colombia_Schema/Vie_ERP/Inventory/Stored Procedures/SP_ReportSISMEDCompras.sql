-- =============================================
-- Author:		Juan Bermudez
-- Create date: 16/04/2016
-- Description:	Procedimiento para el reporte de SISMED de Compras
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportSISMEDCompras]
	@dateStart DATE,
	@dateEnd DATE
AS
BEGIN
	SET NOCOUNT ON

	-- Tabla temporal para almacenar los resultados
	CREATE TABLE #tableExecution
	(
		Mes VARCHAR(2), 
		Tipo INT, 		
		CUM VARCHAR(20),
		IUM VARCHAR(20),
		ValorMinimo DECIMAL(18,2), 
		ValorMaximo DECIMAL(18,2), 
		TotalVentas DECIMAL(18,2), 
		Cantidad DECIMAL(18,2), 
		FacturaVentaMin VARCHAR(100), 
		FacturaVentaMax VARCHAR(100)
	)

	-- Indice para mejor rendimiento en inserciones y consultas
	CREATE INDEX IX_Temp_Mes_CUM ON #tableExecution(Mes, CUM)

	-- ===================================================================
	-- Obtener datos de compras
	-- ===================================================================
	;WITH PurchaseData AS
	(
		-- Agregación de datos de compras
		SELECT 
			YEAR(ev.DocumentDate) AS Year,
			MONTH(ev.DocumentDate) AS Month,
			ip.Id AS ProductId,
			pt.Class,
			ip.CodeCUM,
			ip.IUM,
			MIN(evd.UnitValue) AS MinValue,
			MAX(evd.UnitValue) AS MaxValue,
			SUM(evd.SubTotalValue) AS TotalPurchases,
			SUM(evd.Quantity) AS TotalQuantity
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON evd.ProductId = ip.Id
		JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON pt.Id = ip.ProductTypeId AND pt.Class = 2
		WHERE ev.Status = 2 
			AND ev.DocumentDate BETWEEN @dateStart AND @dateEnd AND ISNULL(atc.ProductNPT, 0) = 0
		GROUP BY YEAR(ev.DocumentDate), MONTH(ev.DocumentDate), ip.Id, pt.Class, ip.CodeCUM, ip.IUM
	)
	INSERT INTO #tableExecution 
		(Mes, Tipo, CUM, IUM, ValorMinimo, ValorMaximo, TotalVentas, Cantidad, FacturaVentaMin, FacturaVentaMax)
	SELECT	
			RIGHT('0' + CAST(pd.Month AS VARCHAR(2)), 2) AS Mes,
			pd.Class AS Tipo,
			ISNULL(pd.CodeCUM, '') AS CUM,
			ISNULL(pd.IUM, '') AS IUM,
			pd.MinValue AS ValorMinimo,
			pd.MaxValue AS ValorMaximo,
			pd.TotalPurchases AS TotalVentas,
			pd.TotalQuantity AS Cantidad,
			-- Obtener factura con valor mínimo
			minInvoice.InvoiceNumber AS FacturaVentaMin,
			-- Obtener factura con valor máximo
			maxInvoice.InvoiceNumber AS FacturaVentaMax
	FROM PurchaseData pd
	-- OUTER APPLY para obtener la factura con el valor mínimo
	OUTER APPLY (
		SELECT TOP 1 ev.InvoiceNumber
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = pd.Year
			AND MONTH(ev.DocumentDate) = pd.Month
			AND evd.ProductId = pd.ProductId
			AND evd.UnitValue = pd.MinValue
	) minInvoice
	-- OUTER APPLY para obtener la factura con el valor máximo
	OUTER APPLY (
		SELECT TOP 1 ev.InvoiceNumber
		FROM Inventory.EntranceVoucher ev WITH (NOLOCK)
		JOIN Inventory.EntranceVoucherDetail evd WITH (NOLOCK) ON ev.Id = evd.EntranceVoucherId
		WHERE ev.Status = 2 
			AND YEAR(ev.DocumentDate) = pd.Year
			AND MONTH(ev.DocumentDate) = pd.Month
			AND evd.ProductId = pd.ProductId
			AND evd.UnitValue = pd.MaxValue
	) maxInvoice

	-- Resultado final
	SELECT	
		ROW_NUMBER() OVER(ORDER BY Mes, CUM) AS Consecutivo, 
		Mes,
		Tipo,
		CUM,
		IUM,
		ValorMinimo,
		ValorMaximo,
		TotalVentas,
		Cantidad,
		FacturaVentaMin,
		FacturaVentaMax
	FROM #tableExecution

	-- Limpiar tabla temporal
	DROP TABLE #tableExecution
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de compras de medicamentos e insumos requerido por el sistema SISMED (Sistema de Información de Precios de Medicamentos). Consolida, por mes y producto, los valores mínimo y máximo de compra, la cantidad total adquirida, el valor total y los números de factura asociados al precio mínimo y máximo, consultando los comprobantes de entrada de inventario (EntranceVoucher y su detalle) en un rango de fechas dado. Filtra únicamente productos de clase 2 (medicamentos con código CUM e IUM) que estén activos y no sean nutrición parenteral total (NPT), tomando solo comprobantes en estado aprobado. El resultado es la información estructurada que las entidades del sector salud deben reportar a las autoridades sanitarias colombianas sobre precios y volúmenes de compra de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDCompras';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISMEDCompras';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte SISMED de compras de medicamentos agregando por mes y producto los valores unitarios mínimo y máximo, total comprado, cantidad y las facturas asociadas a esos extremos en un rango de fechas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango de fechas @dateStart y @dateEnd debe estar definido y ser coherente (inicio <= fin).; Deben existir comprobantes de entrada con Status = 2 dentro del rango para producir resultados.; Los productos involucrados deben tener ProductType con Class = 2 y un ATC asociado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo considera comprobantes de entrada con Status = 2 (estado aprobado/confirmado).; Solo incluye productos cuyo ProductType.Class = 2 (categoría de medicamentos para SISMED).; Excluye productos marcados como NPT (ATC.ProductNPT = 1); solo considera ProductNPT = 0 o NULL.; La agregación se realiza por año, mes y producto, reportando valor unitario mínimo y máximo, total comprado y cantidad total.; El mes se reporta como cadena de 2 dígitos con cero a la izquierda.; CUM e IUM nulos se reportan como cadena vacía en lugar de NULL.; Cuando hay múltiples facturas con el mismo valor mínimo o máximo, se toma solo una arbitrariamente (TOP 1 sin ORDER BY).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reporte SISMED; Compras de medicamentos; Clasificación CUM; Clasificación IUM; Clasificación ATC; Productos NPT (Nutrición Parenteral Total); Comprobante de entrada / factura de compra', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] #tableExecution: Devuelve un resultset con consecutivo, mes, tipo, CUM, IUM, valor mínimo, valor máximo, total de compras, cantidad y facturas asociadas a los valores extremos, filtrando EntranceVoucher.Status=2, fecha en [@dateStart,@dateEnd], ProductType.Class=2 y ATC.ProductNPT=0/NULL.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.InventoryProduct; Inventory.ATC; Inventory.ProductType', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISMEDCompras';
-- GO
