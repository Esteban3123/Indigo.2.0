-- =============================================
-- Author:		Giovanny Plazas L
-- Create date: 22/07/2021
-- Description:	Procedimiento para el reporte de SISDIS Circular 002
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportSISDIS002]
	@dateStart DATE,
	@dateEnd DATE,
	@ProductSupplieId VARCHAR(MAX)
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @SupplieId VARCHAR(MAX) =''
	DECLARE @Table_ProductSupplies AS TABLE(Id INT)
	DECLARE		@FilterByProductSupplies BIT = 0
	-- Se verifica si se necesita el filtro--
		IF ISNULL(@SupplieId, '') <> ''
		BEGIN
			SET @FilterByProductSupplies = 1
			INSERT INTO @Table_ProductSupplies
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@SupplieId, ',')
			
		END
	-- Creamos la tabla temporal que devolveremos con los datos
	DECLARE @tableExecution TABLE
	(	TypeRegister TINYINT,
		MonthReport VARCHAR(2),
		Channel VARCHAR(3), 
		IDM VARCHAR(19),--(Identificación del dispositivo médico) IDM
		UnitPriceMinimunSale DECIMAL(14,2), 
		UnitPriceMaximumSale DECIMAL(14,2),
		TotalSales DECIMAL(16,2),
		TotalUnitsSold INT,
		MinInvoice VARCHAR(30),
		TypeEntityMinInvoice VARCHAR(2),
		IdNumberMinInvoice VARCHAR(18),
		TotalUnitsRegisterMin INT,
		MaxInovice VARCHAR(30),
		TypeEntityMaxInvoice VARCHAR(2),
		IdNumberMaxInvoice VARCHAR(18),
		TotalUnitsRegisterMax INT
	)	
	INSERT INTO @TableExecution 
					(	TypeRegister,
						MonthReport,
						Channel,
						IDM, 
						UnitPriceMinimunSale, 
						UnitPriceMaximumSale,
						TotalSales,
						TotalUnitsSold,
						MinInvoice,
						TypeEntityMinInvoice,
						IdNumberMinInvoice,
						TotalUnitsRegisterMin,
						MaxInovice,
						TypeEntityMaxInvoice,
						IdNumberMaxInvoice,
						TotalUnitsRegisterMax
						)
	SELECT	tmp.TypeRegister,
			tmp.MonthReport,
			tmp.Channel,
			tmp.IDM,
			tmp.UnitPriceMinimunSale,
			tmp.UnitPriceMaximumSale,
			tmp.TotalSales,
			tmp.TotalUnitsSold,
			i1.InvoiceNumber,
			tmp.TypeEntityMinInvoice,
			i1.IdNumberMinInvoice,
			i1.TotalUnitsRegisterMin,
			i2.InvoiceNumber,
			tmp.TypeEntityMaxInvoice,
			i2.IdNumberMaxInvoice,
			i2.TotalUnitsRegisterMax
		FROM(
			SELECT	
					2 TypeRegister,
					RIGHT('0'+CAST(MONTH(ip.InvoiceDate) AS VARCHAR(2)),2) MonthReport,
					'INS' Channel,
					ip.CodeAlternative IDM,
					MIN(ip.UnitValue) UnitPriceMinimunSale,
					MAX(ip.UnitValue) UnitPriceMaximumSale,
					SUM(ip.Subtotalvalue) TotalSales,
					SUM(ip.Quantity) TotalUnitsSold,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.InventoryProductId, MIN(ip.UnitValue))  MinInvoice,
					'NI' TypeEntityMinInvoice,
					Inventory.InvoiceSISMED(1, YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate), ip.InventoryProductId, MAX(ip.UnitValue)) MaxInovice,
					'NI' TypeEntityMaxInvoice
			FROM
			(		
					SELECT	
							ip.CodeAlternative,
							ip.Id InventoryProductId,
							i.Id InvoiceId,
							i.InvoiceDate,
							sod.SubTotalSalesPrice UnitValue,
							sod.InvoicedQuantity Quantity,
							sod.GrandTotalSalesPrice Subtotalvalue
					FROM Billing.Invoice i    
					JOIN Billing.InvoiceDetail id ON i.Id = id.InvoiceId
					JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id AND sod.GrandTotalSalesPrice > 0
					JOIN Inventory.InventoryProduct ip ON sod.ProductId = ip.Id
					JOIN Inventory.InventorySupplie ise ON ip.SupplieId = ise.Id
					JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId  AND pt.Class = 3
					LEFT JOIN @Table_ProductSupplies ptt on ise.Id =ptt.Id
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND (@FilterByProductSupplies = 0 OR ptt.Id IS NOT NULL)
				UNION ALL		
					SELECT	ip.CodeAlternative,
							ip.Id InventoryProductId,
							i.Id InvoiceId,
							i.InvoiceDate,
							dipsd.SalePrice UnitValue,
							dipsd.Quantity Quantity,
							dipsd.TotalValue Subtotalvalue
					FROM Billing.Invoice i 
					JOIN Inventory.DocumentInvoiceProductSales dips ON i.Id = dips.InvoiceId
					JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON dips.Id = dipsd.DocumentInvoiceProductSalesId
					JOIN Inventory.InventoryProduct ip ON dipsd.ProductId = ip.Id
					JOIN Inventory.InventorySupplie ise ON ip.SupplieId = ise.Id
					JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId AND pt.Class = 3
					LEFT JOIN @Table_ProductSupplies ptt on ise.Id =ptt.Id
					WHERE i.Status = 1 
						AND i.InvoiceDate BETWEEN @dateStart AND @dateEnd 
						AND (@FilterByProductSupplies = 0 OR ptt.Id IS NOT NULL)
			) ip
			GROUP BY YEAR(ip.InvoiceDate), MONTH(ip.InvoiceDate),ip.InventoryProductId,ip.CodeAlternative
			) tmp
			LEFT JOIN
			(
			SELECT 
			ICA.InvoiceNumber,
			thA.Nit IdNumberMinInvoice,
			ISNULL(SUM(idps.Quantity),SUM(id.InvoicedQuantity)) TotalUnitsRegisterMin
			FROM Billing.Invoice ICA
			JOIN Common.ThirdParty thA ON ICA.ThirdPartyId = thA.Id
			LEFT JOIN Billing.InvoiceDetailProductSales idps ON ICA.Id=idps.InvoiceId
			LEFT JOIN Billing.InvoiceDetail id ON ICA.Id = id.InvoiceId
			GROUP BY InvoiceNumber, thA.Nit
			) i1 ON i1.InvoiceNumber = tmp.MinInvoice
			LEFT JOIN
			(
			SELECT 
			ICB.InvoiceNumber,
			thB.Nit IdNumberMaxInvoice,
			ISNULL(SUM(idps.Quantity),SUM(id.InvoicedQuantity)) TotalUnitsRegisterMax
			FROM Billing.Invoice ICB
			JOIN Common.ThirdParty thB ON ICB.ThirdPartyId = thB.Id
			LEFT JOIN Billing.InvoiceDetailProductSales idps ON ICB.Id=idps.InvoiceId
			LEFT JOIN Billing.InvoiceDetail id ON ICB.Id = id.InvoiceId
			GROUP BY InvoiceNumber, thB.Nit
			) i2 ON i2.InvoiceNumber = tmp.MaxInovice
	SELECT	ROW_NUMBER() OVER(ORDER BY MonthReport,IDM) as Consecutivo, 
			*
	FROM @tableExecution
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte regulatorio SISDIS Circular 002 para la notificación de ventas de dispositivos médicos (insumos de clase 3) ante el ente de control. Para un rango de fechas dado, consolida las ventas facturadas de dispositivos médicos —tanto por facturación clínica como por ventas directas de inventario— agrupadas por mes y por código de identificación del dispositivo médico (IDM/código alternativo), calculando precio unitario mínimo y máximo de venta, total de unidades vendidas y valor total de ventas. Además, identifica la factura con el precio mínimo y la factura con el precio máximo para cada dispositivo, junto con el NIT del tercero pagador y las unidades registradas en cada una. Permite filtrar por insumo específico y devuelve los resultados con un consecutivo para su presentación como archivo de reporte regulatorio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISDIS002';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ReportSISDIS002';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte SISDIS Circular 002 consolidando ventas mensuales de dispositivos médicos (clase de producto = 3) con precio mínimo/máximo, totales y facturas asociadas, sobre fuentes de facturación tradicional y de inventario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El rango @dateStart–@dateEnd debe estar definido para filtrar Billing.Invoice.InvoiceDate.; Los productos a considerar deben tener ProductType.Class = 3 (dispositivos médicos).; Las facturas deben tener Status = 1 (activas/válidas).; Para Billing.ServiceOrderDetail solo se consideran líneas con GrandTotalSalesPrice > 0.; La función dbo.Split debe poder convertir @ProductSupplieId a enteros (aunque el código usa @SupplieId, siempre vacío, por lo que el filtro nunca se activa).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'TypeRegister siempre se reporta como 2 y Channel siempre como ''INS''.; TypeEntityMinInvoice y TypeEntityMaxInvoice se fijan siempre como ''NI'' (NIT).; Solo se incluyen productos cuyo ProductType.Class = 3 (dispositivos médicos).; Solo se consideran facturas con Status = 1 dentro del rango de fechas indicado.; MonthReport se formatea con dos dígitos (RIGHT(''0''+...,2)).; El filtro por insumos nunca se aplica en la práctica porque @SupplieId se declara y queda en cadena vacía, ignorando el parámetro de entrada @ProductSupplieId.; La factura mínima y máxima por IDM/mes se determina mediante la función Inventory.InvoiceSISMED tomando el precio unitario MIN/MAX.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispositivo médico (IDM); Reporte SISDIS Circular 002; Factura de venta; Tercero / NIT; Insumo médico; Precio unitario mínimo y máximo de venta; Canal institucional (INS)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] CLIENT_RESULTSET: Devuelve un resultset con consecutivo (ROW_NUMBER por MonthReport,IDM) y las columnas del reporte SISDIS 002: TypeRegister=2, Channel=''INS'', mes, IDM (CodeAlternative), precios mín/máx, totales vendidos, factura mínima/máxima con NIT del tercero y unidades registradas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@SupplieId,'''') <> '''' (variable local que se inicializa siempre vacía) → Activa @FilterByProductSupplies=1 e inserta en @Table_ProductSupplies los IDs separados por coma vía dbo.Split, restringiendo a esos InventorySupplie.Id. else No filtra por insumos: incluye todos los productos clase 3.; si Origen de la venta → UNION ALL entre ventas de Billing.ServiceOrderDetail (vía InvoiceDetail) y ventas de Inventory.DocumentInvoiceProductSales/Detail, ambas asociadas a Billing.Invoice.; si ISNULL(SUM(idps.Quantity), SUM(id.InvoicedQuantity)) al calcular unidades por factura mín/máx → Si existen detalles en Billing.InvoiceDetailProductSales se usa esa cantidad; en caso contrario se usa la cantidad de Billing.InvoiceDetail.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.InvoiceSISMED; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.ProductType; Inventory.DocumentInvoiceProductSales; Inventory.DocumentInvoiceProductSalesDetail; Common.ThirdParty; Billing.InvoiceDetailProductSales', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ReportSISDIS002';
-- GO
