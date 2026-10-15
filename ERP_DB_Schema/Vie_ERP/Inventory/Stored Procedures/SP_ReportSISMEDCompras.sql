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