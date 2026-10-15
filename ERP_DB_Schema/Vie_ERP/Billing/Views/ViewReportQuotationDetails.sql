


CREATE VIEW [Billing].[ViewReportQuotationDetails]
AS
	SELECT	CONCAT('QuotationServiceOrderDetail-',qsod.Id,'-',qsods.Id) Id,
			q.Id QuotationId,
			qsod.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ips.Code,
			ce.Code CUPSCode, 
			ce.RIPSCode,
			NULL CodeAlternative, 
			NULL CodeAlternativeTwo, 
			NULL CodeCUM,
			SUBSTRING(ips.Name, 1,94) Name, 
			SUBSTRING(ce.Description, 1, 94) CUPSName, 
			ce.RIPSDescription RIPSName, 
			cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			qsod.ServiceDate, qsod.AuthorizationNumber, qsod.RecordType, qsod.Presentation,
			IIF(qsods.Id IS NULL, qsod.InvoicedQuantity, IIF(qsodsf.Id IS NULL, 0, qsod.InvoicedQuantity)) InvoicedQuantity, 
			qsod.TotalSalesPrice, qsod.ThirdPartyDiscount, 
			IIF(qsods.Id IS NULL, qsod.GrandTotalSalesPrice, IIF(qsodsf.Id IS NULL, 0, qsod.GrandTotalSalesPrice)) ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			qsods.Id SurgicalId, ipss.Code CodeSurgical, SUBSTRING(ipss.Name, 1, 100) NameSurgical, 
			qsods.InvoicedQuantity QuantitySurgical, qsods.TotalSalesPrice TotalSalesPriceSurgical
	FROM Billing.Quotation q WITH (NOLOCK)
	JOIN Billing.QuotationServiceOrderDetail qsod WITH (NOLOCK) ON qsod.QuotationId = q.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = qsod.CUPSEntityId
	LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON qsod.IPSServiceId = ips.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(qsod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId)
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON qsod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.QuotationServiceOrderDetailSurgical qsods WITH (NOLOCK) ON qsods.QuotationServiceOrderDetailId = qsod.Id
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON qsods.IPSServiceId = ipss.Id
	LEFT JOIN 
	(
		SELECT qsods.QuotationServiceOrderDetailId, MIN(qsods.Id) Id
		FROM Billing.QuotationServiceOrderDetailSurgical qsods WITH (NOLOCK)
		GROUP BY qsods.QuotationServiceOrderDetailId
	) qsodsf ON qsods.Id = qsodsf.Id
UNION ALL
	SELECT	CONCAT('QuotationPharmaceuticalDispensingDetail-',qpdd.Id) Id,
			q.Id QuotationId,
			qpdd.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ip.Code,
			NULL CUPSCode, 
			NULL RIPSCode,
			ip.CodeAlternative, 
			ip.CodeAlternativeTwo, 
			case LTRIM(RTRIM(isnull(ip.CodeCUM,''))) when '' then ip.Code else ip.CodeCUM end CodeCUM, 
			ip.Name, 
			NULL CUPSName, 
			NULL RIPSName, 
			NULL ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			qpdd.ServiceDate, qpdd.AuthorizationNumber, 2 RecordType, NULL Presentation,
			qpdd.Quantity InvoicedQuantity, 
			qpdd.SalePrice TotalSalesPrice, qpdd.DiscountValue ThirdPartyDiscount, 
			qpdd.GrandTotalSalesPrice ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			NULL SurgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical	
	FROM Billing.Quotation q WITH (NOLOCK)
	JOIN Billing.QuotationPharmaceuticalDispensingDetail qpdd WITH (NOLOCK) ON qpdd.QuotationId = q.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = qpdd.ProductId
	LEFT JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ip.BillingGroupId