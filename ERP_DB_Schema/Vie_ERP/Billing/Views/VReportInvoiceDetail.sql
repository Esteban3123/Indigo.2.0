






CREATE VIEW [Billing].[VReportInvoiceDetail]
AS
	SELECT	CONCAT(id.Id, sods.Id) Id,
			id.InvoiceId,  
			sod.Id serviceOrderDetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ips.Code,
			ce.Code CUPSCode, 
			ce.RIPSCode,
			NULL CodeAlternative, 
			NULL CodeAlternativeTwo, 
			NULL CodeCUM,
			ips.Name, 
			ce.Description CUPSName, 
			ce.RIPSDescription RIPSName, 
			cd.Code as ContractDescriptionCode,
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate, sod.AuthorizationNumber,
			sod.RecordType,
			sod.Presentation, id.DistributionType, '94' MeasuryUnit,
			IIF(sods.Id IS NULL, id.InvoicedQuantity, IIF(sodsf.Id IS NULL, 0, id.InvoicedQuantity)) InvoicedQuantity, 
			--------------*CAMPOS FACT(CO)*------------
			Id.TotalSalesPrice,
			IIF(sods.Id IS NULL, id.SubTotalPatientSalesPrice, IIF(sodsf.Id IS NULL, 0, id.SubTotalPatientSalesPrice))SubTotalPatientSalesPrice,
			IIF(sods.Id IS NULL, id.ThirdPartySalesPrice, IIF(sodsf.Id IS NULL, 0, id.ThirdPartySalesPrice))ThirdPartySalesPrice,
			Common.CurrencyConverterByModule(sod.GrossValue, cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),NULL,'Invoice', i.InvoiceDate) AS GrossValue,
			Id.ThirdPartyDiscount,
			-----------CAMPOS CUENTA MADRE-----------
			id.NetWorth,
			ROUND(id.NetWorth/id.InvoicedQuantity,2) NetUnitValue,
			(id.NetWorth + id.GrandTotalDiscount) GrossSubValue,
			ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,2) GrossUnitValue,
			IIF(rcd.IsMasterAccount =0	AND id.GrandTotalTaxes = 0,
			Common.CurrencyConverterByModule((sod.TaxValue * sod.InvoicedQuantity), cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),NULL,'Invoice', i.InvoiceDate),
			id.GrandTotalTaxes) AS IvaTotalValue,
			iif(gli.Id is not null , CONCAT(gli.Percentage, '%'), '') AS IvaPercentage,
			id.GrandTotalSalesPrice,
			id.GrandTotalDiscount,
			----------- DETALE QUIRURGICO -----------
			sods.Id surgicalId, ipss.Code CodeSurgical, ipss.Name NameSurgical, 
			sods.InvoicedQuantity QuantitySurgical, sods.TotalSalesPrice TotalSalesPriceSurgical,
			mp.CodeMipres,
			mp.IdMipres
	FROM Billing.Invoice i WITH (NOLOCK) 
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId 
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = sod.CUPSEntityId 	
	JOIN Contract.IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id 
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(sod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId) 
	LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=i.RevenueControlDetailId
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON sod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON sods.ServiceOrderDetailId = sod.Id AND sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON sods.IPSServiceId = ipss.Id	
	LEFT JOIN GeneralLedger.GeneralLedgerIVA gli WITH (NOLOCK) ON gli.Id = ISNULL(id.TaxId,sod.IvaId) 
	LEFT JOIN 
	(
		SELECT sods.ServiceOrderDetailId, MIN (sods.Id) Id
		FROM Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) 
		WHERE sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
		GROUP BY sods.ServiceOrderDetailId
	) sodsf ON sods.Id = sodsf.Id
	outer APPLY(select string_agg(mp.Code,',') CodeMipres, string_agg(mp.IdMipres,',') IdMipres from Billing.MipresCode mp where mp.ServiceOrderDetailId=sod.Id )  mp
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND (i.ThirdPartySalesValue = 0 OR sod.GrandTotalSalesPrice > 0)
UNION ALL
	SELECT	CONCAT(id.Id,psd.Id) Id,
			id.InvoiceId,  
			iif(psd.Id is null, sod.Id,psd.Id)  serviceOrderDetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			iif(ips.id is not null,ips.Code,ip.Code) Code,
			cups.Code CUPSCode, 
			cups.RIPSCode,
			ip.CodeAlternative, 
			ip.CodeAlternativeTwo, 
			case LTRIM(RTRIM(isnull(ip.CodeCUM,''))) when '' then ip.Code else ip.CodeCUM end CodeCUM, 
			iif(ips.id is not null,ips.Name,ip.Name) Name, 
			cups.Description CUPSName, 
			cups.RIPSDescription RIPSName, 
			cd.Code as ContractDescriptionCode,
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate, sod.AuthorizationNumber,
			IIF(psd.CUPSEntityId IS NULL,sod.RecordType,1) RecordType,
			sod.Presentation,
			id.DistributionType, '94' MeasuryUnit,
			sod.InvoicedQuantity InvoicedQuantity, 
			---------------VARIACION POR TIPO DE MONEDA------------
			IIF(psd.Id IS NULL,	id.TotalSalesPrice,
								Common.CurrencyConverterWithDate((	psd.Price-((psd.Price*psd.DiscountPercentage)/100)),
																	cs.OfficialCurrencyId,isnull(i.CurrencyId,cs.OfficialCurrencyId),i.InvoiceDate)) TotalSalesPrice,
			iif(psd.Id IS NULL,id.SubTotalPatientSalesPrice,(((psd.Price-((psd.Price*psd.DiscountPercentage)/100))*PatientPercentage)/100)*sod.InvoicedQuantity) AS SubTotalPatientSalesPrice,
			iif(psd.Id is null,id.ThirdPartySalesPrice,(sod.InvoicedQuantity*(psd.Price-((psd.Price*psd.DiscountPercentage)/100)))) AS ThirdPartySalesPrice,
			Common.CurrencyConverterWithDate(sod.GrossValue, cs.OfficialCurrencyId, ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.InvoiceDate) AS GrossValue,
			IIF(psd.Id IS NULL,	id.ThirdPartyDiscount,
								Common.CurrencyConverterWithDate(((	psd.DiscountPercentage*psd.Price)/100),
																	cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId),i.InvoiceDate)) AS ThirdPartyDiscount,

			-----------CAMPOS CUENTA MADRE-----------
			id.NetWorth AS NetWorth,
			ROUND(id.NetWorth/id.InvoicedQuantity,2) NetUnitValue,
			(id.NetWorth + id.GrandTotalDiscount) GrossSubValue,
			ROUND((id.NetWorth + id.GrandTotalDiscount)/id.InvoicedQuantity,2) GrossUnitValue,
			IIF(rcd.IsMasterAccount=0 AND id.GrandTotalTaxes = 0,
				Common.CurrencyConverterWithDate((sod.TaxValue * sod.InvoicedQuantity), cs.OfficialCurrencyId,ISNULL(i.CurrencyId,cs.OfficialCurrencyId), i.InvoiceDate),
				id.GrandTotalTaxes) AS IvaTotalValue,
			iif(gli.Id is not null , CONCAT(gli.Percentage, '%'), '') AS IvaPercentage,
			id.GrandTotalSalesPrice,
			id.GrandTotalDiscount,
			----------- DETALLE QUIRURGICO -----------
			NULL surgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical,
			mp.CodeMipres,
			mp.IdMipres
	FROM Billing.Invoice i WITH (NOLOCK) 
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sod.Id = id.ServiceOrderDetailId 
	JOIN Inventory.InventoryProduct Ip WITH (NOLOCK) ON Ip.Id = sod.ProductId 
	LEFT JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) on rcd.Id=i.RevenueControlDetailId
	LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
	LEFT JOIN Inventory.InventoryProduct ips WITH(NOLOCK) ON psd.ProductId=ips.Id
	LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON psd.ContractDescriptionsId = cd.Id
	LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL, Ip.BillingGroupId, cups.BillingGroupId)
	LEFT JOIN GeneralLedger.GeneralLedgerIVA gli WITH (NOLOCK) ON gli.Id = ISNULL(id.TaxId,sod.IvaId) 
	outer APPLY(select string_agg(mp.Code,',') CodeMipres, string_agg(mp.IdMipres,',') IdMipres from Billing.MipresCode mp where mp.ServiceOrderDetailId=sod.Id )  mp
	JOIN GeneralLedger.CompanySettings cs WITH(NOLOCK) on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND (i.ThirdPartySalesValue = 0 OR sod.GrandTotalSalesPrice > 0)