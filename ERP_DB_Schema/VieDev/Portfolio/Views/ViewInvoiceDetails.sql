


CREATE VIEW [Portfolio].[ViewInvoiceDetails]
AS


	with Cte_OfficialCurrency as (select OfficialCurrencyId from GeneralLedger.CompanySettings)

/**************************************************  FACTURA BASICA **************************************************/
	SELECT	CONCAT('InvoiceDetailBasicInvoice', idbi.Id) Id,
			'InvoiceDetailBasicInvoice' EntityName,
			idbi.Id EntityId,
			ar.Id AccountReceivableId,
			bb.DocumentDate as ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			CONCAT(ip.Code, ' - ', ip.Name) CodeName,
			CONCAT(ip.CodeAlternative, ' - ', ip.Description) AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			idbi.Quantity,
			idbi.UnitSalesPrice,
			idbi.TotalSalesPrice,
			idbi.Balance,
			concat( FORMAT( CAST(ROUND((bbd.Value * COALESCE(bbd.PercentageIVA,0))/100,2) AS DECIMAL(20,2)),'N2','es-ES'),
					' (',bbd.PercentageIVA,' %)') TaxValueName,
			bbd.PercentageIVA TaxPercentage,
			Tax.Id TaxId,
			NULL noteId
	FROM Portfolio.AccountReceivable ar
	JOIN Billing.InvoiceDetailBasicInvoice idbi ON ar.InvoiceId = idbi.InvoiceId
	JOIN Billing.BasicBillingDetail bbd ON idbi.BasicBillingDetailId = bbd.Id
	JOIN Inventory.InventoryProduct ip ON bbd.ProductId = ip.Id
	INNER JOIN Inventory.ProductType pt ON pt.Id = ip.ProductTypeId
	JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id	
	JOIN GeneralLedger.MainAccounts ma ON pg.IncomeAccountId = ma.Id
	JOIN Billing.BasicBilling bb ON bbd.BasicBillingId = bb.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON bb.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) = cc.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA Tax WITH(NOLOCK) on Tax.Id =ip.IVAId AND bbd.PercentageIVA >0 
	WHERE ar.AccountReceivableType = 1 --AND ar.Id = @AccountReceivableId
		AND bbd.DetailType = 1 
UNION ALL
	SELECT	CONCAT('InvoiceDetailBasicInvoice', idbi.Id) Id,
			'InvoiceDetailBasicInvoice' EntityName,
			idbi.Id EntityId,
			ar.Id AccountReceivableId,
			bb.DocumentDate as ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			CONCAT(bc.Code, ' - ', bc.Name) CodeName,
			NULL AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			idbi.Quantity,
			idbi.UnitSalesPrice,
			idbi.TotalSalesPrice,
			idbi.Balance,
			concat(FORMAT(CAST(ROUND((bbd.Value * COALESCE(bbd.PercentageIVA,0))/100,2) AS DECIMAL(20,2)),'N2','es-ES') ,
					' (',bbd.PercentageIVA,' %)') TaxValueName,
			bbd.PercentageIVA TaxPercentage,
			bc.IVAId TaxId,
			NULL noteId
	FROM Portfolio.AccountReceivable ar
	JOIN Billing.InvoiceDetailBasicInvoice idbi ON ar.InvoiceId = idbi.InvoiceId
	JOIN Billing.BasicBillingDetail bbd ON idbi.BasicBillingDetailId = bbd.Id
	JOIN Billing.BillingConcept bc ON bbd.BillingConceptId = bc.Id
	JOIN GeneralLedger.MainAccounts ma ON bc.EntityIncomeAccountId = ma.Id
	JOIN Billing.BasicBilling bb ON bbd.BasicBillingId = bb.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON bb.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) = cc.Id
	WHERE ar.AccountReceivableType = 1 --AND ar.Id = @AccountReceivableId
		AND bbd.DetailType = 2
UNION ALL
	SELECT	CONCAT('InvoiceDetailBasicInvoice', idbi.Id) Id,
			'InvoiceDetailBasicInvoice' EntityName,
			idbi.Id EntityId,
			ar.Id AccountReceivableId,
			bb.DocumentDate ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			CONCAT(fapa.Plate, ' - ', fai.Description) CodeName,
			NULL AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			idbi.Quantity,
			idbi.UnitSalesPrice,
			idbi.TotalSalesPrice,
			idbi.Balance,
			concat( FORMAT(CAST(ROUND((bbd.Value * COALESCE(bbd.PercentageIVA,0))/100,2) AS DECIMAL(20,2)),'N2','es-ES') ,
					' (',bbd.PercentageIVA,' %)') TaxValueName,
			bbd.PercentageIVA TaxPercentage,
			fai.IVAId TaxId,
			NULL noteId
	FROM Portfolio.AccountReceivable ar
	JOIN Billing.InvoiceDetailBasicInvoice idbi ON ar.InvoiceId = idbi.InvoiceId
	JOIN Billing.BasicBillingDetail bbd ON idbi.BasicBillingDetailId = bbd.Id
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa ON bbd.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemCatalog faic ON fai.ItemCatalogId = faic.Id
	JOIN GeneralLedger.MainAccounts ma ON IIF
		(
			(bbd.Value - bbd.ValueDiscount) > fapa.HistoricalValue, 
			faic.NetIncomeAccountId, 
			faic.LossMainAccountId
		) = ma.Id
	JOIN Billing.BasicBilling bb ON bbd.BasicBillingId = bb.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON bb.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, fu.CostCenterId, NULL) = cc.Id
	WHERE ar.AccountReceivableType = 1 --AND ar.Id = @AccountReceivableId
		AND bbd.DetailType = 3
/************************************************** FACTURA LEY 100 **************************************************/
UNION ALL
	SELECT	CONCAT('InvoiceDetail', id.Id) Id,
			'InvoiceDetail' EntityName,
			id.Id EntityId,
			ar.Id AccountReceivableId,
			sod.ServiceDate,
			ce.Code CodeCups,
			----------------------------------
			CONCAT(bg.Code, ' - ', bg.Name) BillingGroupCodeName,
			CONCAT(ips.Code, ' - ', ips.Name) CodeName,
			CONCAT(ce.Code, ' - ', ce.Description) AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			id.InvoicedQuantity,
			ROUND(((id.InvoicedQuantity * id.TotalSalesPrice) + id.GrandTotalDiscount) / id.InvoicedQuantity, 2) UnitSalesPrice,
			id.ThirdPartySalesPrice TotalSalesPrice,
			id.Balance ,
			CONCAT(FORMAT(id.GrandTotalTaxes,'N2','es-ES'),' ',cu.Abbreviation,' (',COALESCE(giva.Percentage,0),' %)') TaxValueName,
			giva.Percentage TaxPercentage,
			id.TaxId,
			NULL noteId
	FROM Billing.SettingsBilling sb WITH(NOLOCK)
	JOIN Portfolio.AccountReceivable ar WITH(NOLOCK) ON sb.IdOperatingUnit = ar.OperatingUnitId
	JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON ar.InvoiceId = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
	JOIN Contract.IPSService ips  WITH(NOLOCK) ON sod.IPSServiceId = ips.Id
	JOIN Contract.CUPSEntity ce WITH(NOLOCK) ON sod.CUPSEntityId = ce.Id
	JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ce.BillingGroupId = bg.Id
	JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON sod.IncomeMainAccountId = ma.Id	
	JOIN Cte_OfficialCurrency cte on 1=1
	join Common.Currency cu WITH(NOLOCK) on cu.Id = COALESCE(ar.CurrencyId,cte.OfficialCurrencyId)
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) = cc.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA giva on id.TaxId =giva.Id
	WHERE ar.AccountReceivableType = 2 --AND ar.Id = @AccountReceivableId
		AND sod.RecordType = 1
		AND (sb.AccountingForSurgical <> 2 OR sod.Presentation <> 2)
		AND sod.IsDelete = 0
		AND sod.SettlementType <> 3
		AND id.GrandTotalSalesPrice > 0
UNION ALL
	SELECT	CONCAT('InvoiceDetailSurgical', ids.Id) Id,
			'InvoiceDetailSurgical' EntityName,
			ids.Id EntityId,
			ar.Id AccountReceivableId,
			sod.ServiceDate,
			ce.Code CodeCups,
			----------------------------------
			CONCAT(bg.Code, ' - ', bg.Name) BillingGroupCodeName,
			CONCAT(ips.Code, ' - ', ips.Name) CodeName,
			CONCAT(ce.Code, ' - ', ce.Description) AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			ids.InvoicedQuantity,
			ids.TotalSalesPrice UnitSalesPrice,
			ids.TotalSalesPrice TotalSalesPrice,
			ids.Balance,
			'' TaxValueName,
			NULL TaxPercentage,
			NULL TaxId,
			NULL noteId
	FROM Billing.SettingsBilling sb
	JOIN Portfolio.AccountReceivable ar ON sb.IdOperatingUnit = ar.OperatingUnitId
	JOIN Billing.InvoiceDetail id ON ar.InvoiceId = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod ON id.ServiceOrderDetailId = sod.Id
	JOIN Billing.InvoiceDetailSurgical ids ON id.Id = ids.InvoiceDetailId 
	JOIN Contract.IPSService ips ON ids.IPSServiceId = ips.Id
	JOIN Contract.CUPSEntity ce ON sod.CUPSEntityId = ce.Id
	JOIN Billing.BillingGroup bg ON ce.BillingGroupId = bg.Id
	JOIN GeneralLedger.MainAccounts ma ON ids.IncomeMainAccountId = ma.Id				
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, ids.CostCenterId, NULL) = cc.Id
	WHERE ar.AccountReceivableType = 2 --AND ar.Id = @AccountReceivableId
		AND sod.RecordType = 1
		AND (sb.AccountingForSurgical = 2 AND sod.Presentation = 2)
		AND sod.IsDelete = 0
		AND sod.SettlementType <> 3
		AND id.GrandTotalSalesPrice > 0

UNION ALL

	SELECT	CONCAT('ServiceOrderDetailSurgical', sods.Id) Id,
			'ServiceOrderDetailSurgical' EntityName,
			pnard.EntityId,
			pnara.AccountReceivableId,
			sod.ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			CONCAT(ips.Code, ' - ', ips.Name) CodeName,
			NULL AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			 1 InvoicedQuantity,
			sods.TotalSalesPrice UnitSalesPrice,
			sods.TotalSalesPrice TotalSalesPrice,
			ROUND(sods.TotalSalesPrice - pnard.Value,2) Balance,
			'' TaxValueName,
			pnard.TaxPercentage,
			pnard.TaxId,
			pn.Id noteId
	FROM Portfolio.PortfolioNoteAccountReceivableDetail pnard with(NOLOCK) 
	JOIN Portfolio.PortfolioNoteAccountReceivableAdvance pnara WITH(NOLOCK) on pnard.PortfolioNoteAccountReceivableId=pnara.Id
	JOIN Portfolio.PortfolioNote pn WITH(NOLOCK) on pnara.PortfolioNoteId =pn.Id
	JOIN Billing.ServiceOrderDetailSurgical sods WITH(NOLOCK) on pnard.EntityId =sods.Id and pnard.EntityName ='ServiceOrderDetailSurgical'
	JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) on sods.ServiceOrderDetailId =sod.Id
	JOIN Contract.IPSService ips WITH(NOLOCK) on sods.IPSServiceId = ips.Id
	JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) on ma.Id=pnard.MainAccountId
	LEFT join Payroll.CostCenter cc WITH(NOLOCK) ON cc.Id=pnard.CostCenterId
	where pn.EntityName ='Glosas' and pn.Status =2

UNION ALL
	SELECT	CONCAT('InvoiceDetail', id.Id) Id,
			'InvoiceDetail' EntityName,
			id.Id EntityId,
			ar.Id AccountReceivableId,
			sod.ServiceDate,
			NULL CodeCups,
			----------------------------------
			CONCAT(bg.Code, ' - ', bg.Name) BillingGroupCodeName,
			CONCAT(ip.Code, ' - ', ip.Name) CodeName,
			CONCAT(ip.CodeAlternative, ' - ', ip.Description) AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			id.InvoicedQuantity,
			ROUND(((id.InvoicedQuantity * id.TotalSalesPrice) + id.GrandTotalDiscount) / id.InvoicedQuantity, 2) UnitSalesPrice,
			id.ThirdPartySalesPrice TotalSalesPrice,
			id.Balance ,
			CONCAT(FORMAT(id.GrandTotalTaxes,'N2','es-ES'),' ',cu.Abbreviation,' (',COALESCE(giva.Percentage,0),' %)') TaxValueName,
			giva.Percentage TaxPercentage,
			id.TaxId,
			NULL noteId
	FROM Portfolio.AccountReceivable ar WITH(NOLOCK)
	JOIN Billing.InvoiceDetail id WITH(NOLOCK) ON ar.InvoiceId = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH(NOLOCK) ON id.ServiceOrderDetailId = sod.Id
	JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON sod.ProductId = ip.Id
	JOIN Cte_OfficialCurrency cte on 1=1
	join Common.Currency cu WITH(NOLOCK) on cu.Id = COALESCE(ar.CurrencyId,cte.OfficialCurrencyId)
	LEFT JOIN Billing.BillingGroup bg WITH(NOLOCK) ON ip.BillingGroupId = bg.Id
	JOIN GeneralLedger.MainAccounts ma WITH(NOLOCK) ON sod.IncomeMainAccountId = ma.Id				
	LEFT JOIN Payroll.CostCenter cc WITH(NOLOCK) ON IIF(ma.HandlesCostCenter = 1, sod.CostCenterId, NULL) = cc.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA giva WITH(NOLOCK) on giva.Id =id.TaxId
	WHERE ar.AccountReceivableType = 2 --AND ar.Id = @AccountReceivableId
		AND sod.RecordType = 2
		AND sod.IsDelete = 0
		AND sod.SettlementType <> 3
		AND id.GrandTotalSalesPrice > 0
/************************************************  FACTURA MONTO FIJO ************************************************/
UNION ALL
	SELECT	CONCAT('AccountReceivable', ar.Id) Id,
			'AccountReceivable' EntityName,
			ar.Id EntityId,
			ar.Id AccountReceivableId,
			i.InvoiceDate ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			'Factura Monto Fijo' CodeName,
			NULL AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			IIF(i.CapitationPatientValue = 0, 1, i.CapitationlPatientsAmount) InvoicedQuantity,
			IIF(i.CapitationPatientValue = 0, i.InvoiceValue, i.CapitationPatientValue) UnitSalesPrice,
			ROUND
			(
				IIF(i.CapitationPatientValue = 0, 1, i.CapitationlPatientsAmount) * IIF(i.CapitationPatientValue = 0, i.InvoiceValue, i.CapitationPatientValue),
				2
			) TotalSalesPrice,
			ar.Balance,
			'' TaxValueName,
			null TaxPercentage,
			NULL TaxId,
			NULL noteId
	FROM Billing.SettingsBilling sb
	JOIN Portfolio.AccountReceivable ar ON sb.IdOperatingUnit = ar.OperatingUnitId
	JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
	JOIN Billing.InvoiceEntityCapitated iec ON i.Id = iec.InvoiceId
	JOIN Contract.CareGroup cg ON iec.CareGroupId = cg.Id
	JOIN GeneralLedger.MainAccounts ma ON sb.CapitationRevenueMainAccountId = ma.Id
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, cg.CostCenterId, NULL) = cc.Id
	WHERE ar.AccountReceivableType = 2 --AND ar.Id = @AccountReceivableId
/*************************************************  FACTURA PRODUCTO *************************************************/
UNION ALL
	SELECT	CONCAT('InvoiceDetailProductSales', idps.Id) Id,
			'InvoiceDetailProductSales' EntityName,
			idps.Id EntityId,
			ar.Id AccountReceivableId,
			dips.DocumentDate as ServiceDate,
			NULL CodeCups,
			----------------------------------
			NULL BillingGroupCodeName,
			CONCAT(ip.Code, ' - ', ip.Name) CodeName,
			CONCAT(ip.CodeAlternative, ' - ', ip.Description) AlternativeCodeName,
			----------------------------------
			ma.Id MainAccountId, 
			CONCAT(ma.Number, ' - ', ma.Name) MainAccountNumberName,
			cc.Id CostCenterId, 
			CONCAT(cc.Code, ' - ', cc.Name) CostCenterCodeName,
			----------------------------------
			idps.Quantity,
			idps.UnitSalesPrice,
			idps.TotalSalesPrice,
			idps.Balance,
			'' TaxValueName,
			NULL TaxPercentage,
			NULL TaxId,
			NULL noteId
	FROM Billing.SettingsBilling sb
	JOIN Portfolio.AccountReceivable ar ON sb.IdOperatingUnit = ar.OperatingUnitId
	JOIN Billing.InvoiceDetailProductSales idps ON ar.InvoiceId = idps.InvoiceId
	JOIN Inventory.DocumentInvoiceProductSalesDetail dipsd ON idps.DocumentInvoiceProductSalesDetailId = dipsd.Id				
	JOIN Inventory.InventoryProduct ip ON dipsd.ProductId = ip.Id
	JOIN Inventory.ProductGroup pg ON ip.ProductGroupId = pg.Id
	JOIN GeneralLedger.MainAccounts ma ON pg.IncomeAccountId = ma.Id
	LEFT JOIN Inventory.DocumentInvoiceProductSales dips ON dipsd.DocumentInvoiceProductSalesId = dips.Id
	LEFT JOIN Payroll.FunctionalUnit fu ON dips.FunctionalUnitId = fu.Id
	LEFT JOIN Payroll.CostCenter cc ON IIF(ma.HandlesCostCenter = 1, IIF(sb.AssociateCostCenter = 1, fu.CostCenterId, pg.CostCenterId), NULL) = cc.Id
	WHERE ar.AccountReceivableType = 7 --AND ar.Id = @AccountReceivableId