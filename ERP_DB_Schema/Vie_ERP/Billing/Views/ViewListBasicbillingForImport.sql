

CREATE VIEW [Billing].[ViewListBasicbillingForImport]

AS
		SELECT	ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) Id
		,k.*
		FROM
		(
			--Cabecera
			SELECT 
					 bb.Code DocumentCode
					,bb.DocumentDate
					,cu.Id CustomerId
					,CONCAT(cu.Nit,' - ',cu.Name) CustomerCodeName
					,i.InvoiceNumber
					,bb.SaleModality
					, CASE bb.SaleModality
						WHEN 1 THEN 'Contado'
						WHEN 2 THEN 'Credito'
					END SaleModalityName
					,c.Id CurrencyId
					,c.Abbreviation CurrencyAbbreviation
					,bb.Id HeaderId
					,CAST(0 as bit) Selected
					,NULL DetailId
					,NULL DetailType
					,NULL DetailTypeName
					,NULL FunctionalUnitCodeName
					,NULL ItemCodeName
					,NULL Quantity
					,NULL UnitValue
					,NULL ValueIVA
					,NULL TotalValue
					,NULL BasicBillingId
			FROM Billing.BasicBilling bb
			JOIN Common.Customer cu ON cu.Id = bb.CustomerId
			JOIN Billing.Invoice i ON i.Id = bb.InvoiceId
			JOIN Common.Currency c ON c.Id = bb.CurrencyId
			WHERE bb.Status = 2

			UNION ALL

			--Detalles
			SELECT	
					 bb.code DocumentCode
					,NULL DocumentDate
					,NULL CustomerId
					,NULL CustomerCodeName
					,NULL InvoiceNumber
					,NULL SaleModality
					,NULL SaleModalityName
					,NULL CurrencyId
					,NULL CurrencyAbbreviation
					,NULL HeaderId
					,CAST(0 as bit) Selected
					,bbd.Id DetailId
					,bbd.DetailType
					,CASE bbd.DetailType
						WHEN 1 THEN 'Producto'
						WHEN 2 THEN 'Servicio'
						WHEN 3 THEN 'Activo Fijo'
					 END DetailTypeName
					,CONCAT(fu.Code, ' - ' ,fu.Name) FunctionalUnitCodeName
					,CASE bbd.DetailType
						WHEN 1 THEN CONCAT(ip.Code, ' - ', ip.Name)
						WHEN 2 THEN CONCAT(bc.Code, ' - ', bc.Name)
						WHEN 3 THEN CONCAT(fai.Code, ' - ', fai.Description)
					END ItemCodeName
					,bbd.Quantity
					,bbd.Price UnitValue
					,(ROUND((bbd.Value - bbd.ValueDiscount) * bbd.PercentageIVA / 100, 2)) ValueIVA
					,bbd.Value TotalValue
					,bbd.BasicBillingId
			FROM Billing.BasicBillingDetail bbd
			JOIN Billing.BasicBilling bb ON bb.Id = bbd.BasicBillingId
			JOIN Payroll.FunctionalUnit fu ON fu.Id = bbd.FunctionalUnitId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = bbd.ProductId
			LEFT JOIN Billing.BillingConcept bc ON bc.Id = bbd.BillingConceptId
			LEFT JOIN FixedAsset.FixedAssetPhysicalAsset fh ON fh.Id = bbd.PhysicalAssetId
			LEFT JOIN FixedAsset.FixedAssetItem fai ON fai.Id = fh.ItemId
			WHERE bb.Status = 2

			UNION ALL

			--Obsequios
			SELECT	
					 bb.code DocumentCode
					,NULL DocumentDate
					,NULL CustomerId
					,NULL CustomerCodeName
					,NULL InvoiceNumber
					,NULL SaleModality
					,NULL SaleModalityName
					,NULL CurrencyId
					,NULL CurrencyAbbreviation
					,NULL HeaderId
					,CAST(0 as bit) Selected
					,bbg.Id DetailId
					,NULL DetailType
					,'Obsequio' DetailTypeName
					,NULL FunctionalUnitCodeName
					,CONCAT(ip.Code, ' - ', ip.Name) ItemCodeName
					,bbg.Quantity
					,NULL UnitValue
					,NULL ValueIVA
					,NULL TotalValue
					,bbg.BasicBillingId
			FROM Billing.BasicBillingGifts bbg
			JOIN Billing.BasicBilling bb ON bb.Id = bbg.BasicBillingId
			LEFT JOIN Inventory.InventoryProduct ip ON ip.Id = bbg.ProductId
			WHERE bb.Status = 2
		) K