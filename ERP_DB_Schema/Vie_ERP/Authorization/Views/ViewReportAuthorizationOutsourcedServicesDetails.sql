

CREATE VIEW [Authorization].[ViewReportAuthorizationOutsourcedServicesDetails]
AS
	SELECT	CONCAT('AuthorizationOutsourcedServicesServiceOrderDetail-',aossod.Id,'-',aossods.Id) Id,
			aos.Id AuthorizationOutsourcedServicesId,
			aossod.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
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
			aossod.ServiceDate, aossod.AuthorizationNumber, aossod.RecordType, aossod.Presentation,
			IIF(aossods.Id IS NULL, aossod.InvoicedQuantity, IIF(qsodsf.Id IS NULL, 0, aossod.InvoicedQuantity)) InvoicedQuantity, 
			aossod.TotalSalesPrice, aossod.ThirdPartyDiscount, 
			IIF(aossods.Id IS NULL, aossod.GrandTotalSalesPrice, IIF(qsodsf.Id IS NULL, 0, aossod.GrandTotalSalesPrice)) ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			aossods.Id SurgicalId, ipss.Code CodeSurgical, SUBSTRING(ipss.Name, 1, 100) NameSurgical, 
			aossods.InvoicedQuantity QuantitySurgical, aossods.TotalSalesPrice TotalSalesPriceSurgical
	FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK)
	JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail aossod WITH (NOLOCK) ON aossod.AuthorizationOutsourcedServicesId = aos.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON ce.Id = aossod.CUPSEntityId
	LEFT JOIN Contract.IPSService ips WITH (NOLOCK) ON aossod.IPSServiceId = ips.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(aossod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId)
	LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON aossod.PerformsFunctionalUnitId = fu.Id
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd WITH (NOLOCK) ON aossod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH (NOLOCK) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN [Authorization].AuthorizationOutsourcedServicesServiceOrderDetailSurgical aossods WITH (NOLOCK) ON aossods.AuthorizationOutsourcedServicesServiceOrderDetailId = aossod.Id
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON aossods.IPSServiceId = ipss.Id	
	LEFT JOIN 
	(
		SELECT aossods.QuotationServiceOrderDetailId, MIN(aossods.Id) Id
		FROM Billing.QuotationServiceOrderDetailSurgical aossods WITH (NOLOCK)
		GROUP BY aossods.QuotationServiceOrderDetailId
	) qsodsf ON aossods.Id = qsodsf.Id
UNION ALL
	SELECT	CONCAT('AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail-',aospdd.Id) Id,
			aos.Id AuthorizationOutsourcedServicesId,
			aospdd.Id DetailId, 
			------------  INFORMACION DEL SERVICIO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
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
			aospdd.ServiceDate, aospdd.AuthorizationNumber, 2 RecordType, NULL Presentation,
			aospdd.Quantity InvoicedQuantity, 
			aospdd.SalePrice TotalSalesPrice, aospdd.DiscountValue ThirdPartyDiscount, 
			aospdd.GrandTotalSalesPrice ThirdPartySalesPrice, 
			----------------- DETALE QUIRURGICO ---------------
			NULL SurgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical	
	FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK)
	JOIN [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail aospdd WITH (NOLOCK) ON aospdd.AuthorizationOutsourcedServicesId = aos.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = aospdd.ProductId
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = ip.BillingGroupId
	LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON aospdd.FunctionalUnitId = fu.Id