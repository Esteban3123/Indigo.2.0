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