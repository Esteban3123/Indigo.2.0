
CREATE VIEW [Billing].[VReportInvoicePartialDetail]
AS
	with Settings_cte as (SELECT top 1 ISNULL(bs.LiquidateMasterAccount,0) LiquidateMasterAccount from Billing.SettingsBilling bs WITH(NOLOCK) order by bs.LiquidateMasterAccount DESC )
	
	SELECT	CONCAT(sodd.Id, sods.Id) Id,
			sodd.RevenueControlDetailId, 
			sodd.Id ServiceOrderDetailDistributionId,
			sod.Id ServiceOrderDetailId, 			
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
			 cd.Code as ContractDescriptionCode,
			 cd.Name ContractDescriptionName,
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate, sod.AuthorizationNumber, sod.RecordType, sod.Presentation,
			IIF(sods.Id IS NULL, sodd.Quantity, IIF(sodsf.Id IS NULL, 0, sodd.Quantity)) InvoicedQuantity, 
			sod.TotalSalesPrice, sod.ThirdPartyDiscount, 
			IIF(sods.Id IS NULL, sodd.SubTotalPatientSalesPrice, IIF(sodsf.Id IS NULL, 0, sodd.SubTotalPatientSalesPrice)) SubTotalPatientSalesPrice, 
			IIF(sods.Id IS NULL, sodd.ThirdPartySalesPrice, IIF(sodsf.Id IS NULL, 0, sodd.ThirdPartySalesPrice)) ThirdPartySalesPrice, 
			----------- DETALE QUIRURGICO -----------
			sods.Id SurgicalId, ipss.Code CodeSurgical, SUBSTRING(ipss.Name, 1, 100) NameSurgical, 
			sods.InvoicedQuantity QuantitySurgical, sods.TotalSalesPrice TotalSalesPriceSurgical,
			------------ CUENTA MADRE ---------------
			iif(gli.Id is not null , CONCAT(GLI.Percentage, '%'), '') AS IvaPercentage,
			IIF(bs.LiquidateMasterAccount=0,sod.TaxValue * sod.InvoicedQuantity,sodd.GrandTotalTaxes) AS IvaTotalValue,
			iif(bs.LiquidateMasterAccount=0,sod.GrossValue,ROUND(sodd.SubTotalSalesPrice/sodd.Quantity,2)) AS GrossValue,
			sodd.SubTotalSalesPrice,
			IIF(bs.LiquidateMasterAccount=0,sod.GrossValue*sod.InvoicedQuantity,(sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount)) AS NetWorth,
			ROUND((sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount)/sodd.Quantity,2) AS NetUnitValue,			
			sodd.GrandTotalSalesPrice,
			sodd.GrandTotalDiscount,
			ISNULL(GLI.Percentage,0) TaxPercentage
	FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id
	JOIN Contract.IPSService ips WITH (NOLOCK) ON sod.IPSServiceId = ips.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(ISNULL(sod.ApplyRIAS, 0) = 1, ISNULL(ce.RIASBillingGroupId, ce.BillingGroupId), ce.BillingGroupId)	
	JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
	LEFT JOIN Contract.CUPSEntityContractDescriptions cecd with(nolock) ON sod.CUPSEntityContractDescriptionId = cecd.Id
	LEFT JOIN Contract.ContractDescriptions cd with(nolock) ON cecd.ContractDescriptionId = cd.Id
	LEFT JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON sods.ServiceOrderDetailId = sod.Id AND sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
	LEFT JOIN Contract.IPSService ipss WITH (NOLOCK) ON sods.IPSServiceId = ipss.Id
	LEFT JOIN 
	(
		SELECT sods.ServiceOrderDetailId, MIN (sods.Id) Id
		FROM Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) 
		WHERE sods.TotalSalesPrice > 0 AND sods.OnlyMedicalFees = 0 
		GROUP BY sods.ServiceOrderDetailId
	) sodsf ON sods.Id = sodsf.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI ON GLI.Id = sod.IvaId
	JOIN Settings_cte bs on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0
UNION ALL
	SELECT	CONCAT(sodd.Id,psd.Id) Id,
			sodd.RevenueControlDetailId, 
			sodd.Id ServiceOrderDetailDistributionId,
			iif(psd.Id is null, sod.Id,psd.Id)  serviceOrderDetailId, 
			------------  INFORMACION DEL PRODUCTO ------------
			bg.Code + ' - ' + bg.Name BillingGroup, 
			iif(ips.id is not null,ips.Code,ip.Code) Code, 
			cups.Code CUPSCode, 
			cups.RIPSCode, 			
			ip.CodeAlternative, 
			ip.CodeAlternativeTwo, 
			case LTRIM(RTRIM(isnull(ip.CodeCUM,''))) when '' then ip.Code else ip.CodeCUM end CodeCUM, 
			iif(ips.Name is not null,ips.Name,ip.Name) Name, 
			cups.Description CUPSName, 
			cups.RIPSDescription RIPSName, 
			cd.Code as ContractDescriptionCode,
			cd.Name ContractDescriptionName, 
			------------- INFORMACION DEL DETALLE -------------
			sod.ServiceDate,
			sod.AuthorizationNumber,
			IIF(psd.CUPSEntityId IS NULL,sod.RecordType,1) RecordType,
			sod.Presentation,
			IIF(bs.LiquidateMasterAccount=0,sod.InvoicedQuantity,sodd.Quantity) AS InvoicedQuantity,
			iif(psd.Id is null,sod.TotalSalesPrice,(psd.Price-((psd.Price*psd.DiscountPercentage)/100))) TotalSalesPrice,
			iif(psd.Id IS NULL,sod.ThirdPartyDiscount,((psd.DiscountPercentage*psd.Price)/100)) ThirdPartyDiscount, 
			iif(psd.Id IS NULL,sodd.SubTotalPatientSalesPrice,(((psd.Price-((psd.Price*psd.DiscountPercentage)/100))*sodd.PatientPercentage)/100)*sod.InvoicedQuantity) SubTotalPatientSalesPrice, 
			iif(psd.Id is null,sodd.ThirdPartySalesPrice,(sod.InvoicedQuantity*(psd.Price-((psd.Price*psd.DiscountPercentage)/100)))) ThirdPartySalesPrice, 
			----------- DETALE QUIRURGICO -----------
			NULL SurgicalId, NULL CodeSurgical, NULL NameSurgical, 
			NULL QuantitySurgical, NULL TotalSalesPriceSurgical,
			----------- CUENTA MADRE -----------------
			iif(gli.Id is not null , CONCAT(GLI.Percentage, '%'), '') AS IvaPercentage,
			iif(bs.LiquidateMasterAccount = 0,(sod.TaxValue * sod.InvoicedQuantity),sodd.GrandTotalTaxes) AS IvaTotalValue,
			iif(bs.LiquidateMasterAccount=0,sod.GrossValue,ROUND(sodd.SubTotalSalesPrice/sodd.Quantity,2)) AS GrossValue,
			sodd.SubTotalSalesPrice,
			(sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount) AS NetWorth,
			ROUND((sodd.SubTotalSalesPrice - sodd.GrandTotalDiscount)/sodd.Quantity,2) AS NetUnitValue,
			sodd.GrandTotalSalesPrice,
			sodd.GrandTotalDiscount,
			ISNULL(GLI.Percentage,0) TaxPercentage
	FROM Billing.ServiceOrderDetailDistribution sodd WITH (NOLOCK)
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON sodd.ServiceOrderDetailId = sod.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON ip.Id = sod.ProductId
	JOIN Billing.RevenueControlDetail rcd WITH(NOLOCK) ON rcd.Id=sodd.RevenueControlDetailId
	LEFT JOIN Billing.ProductServiceDetail psd WITH(NOLOCK) ON sod.Id = psd.ServiceOrderDetailId
	LEFT JOIN Inventory.InventoryProduct ips WITH(NOLOCK) ON psd.ProductId=ips.Id
	LEFT JOIN Contract.CUPSEntity cups WITH(NOLOCK) ON psd.CUPSEntityId=cups.Id
	LEFT JOIN Contract.ContractDescriptions cd WITH(NOLOCK) ON psd.ContractDescriptionsId = cd.Id
	JOIN Billing.BillingGroup bg WITH (NOLOCK) ON bg.Id = IIF(psd.CUPSEntityId IS NULL,Ip.BillingGroupId,cups.BillingGroupId)
	LEFT JOIN GeneralLedger.GeneralLedgerIVA GLI ON GLI.Id = sod.IvaId
	JOIN Settings_cte bs on 1=1
	WHERE sod.IsDelete = 0 AND sod.SettlementType != 3 AND sodd.GrandTotalSalesPrice > 0