







CREATE VIEW [Billing].[VReportInvoiceDetailLoad]
AS
--Se agrego and sod.IsPackage = 0 apara que no salga el paquete---despues lili informa que si debe salir el paquete y se comenta el codigo
SELECT        i.Id, i.InvoiceNumber, sod.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, sods.Id AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         ce.Code AS CUPSCode, SUBSTRING(CE.Description, 1, 94) AS CUPSName, IPS.Code, SUBSTRING(IPS.Name, 1,94) as Name, SOD.ServiceDate, SOD.InvoicedQuantity, 
                         SOD.TotalSalesPrice, SODD.SubTotalPatientSalesPrice, SODD.ThirdPartySalesPrice, IPSS.Code AS CodeSurgical, SUBSTRING(IPSS.Name, 1, 100) 
                         AS NameSurgical, SODS.InvoicedQuantity AS QuantitySurgical, SODS.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, 
                         ce.Code AS CodeAlternative, ce.Code AS CodeAlternativeTwo, ce.Code AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, ce.RIPSCode,ce.RIPSDescription as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 cd.Name ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         BILLING.ServiceOrderDetailDistribution AS SODD WITH (NOLOCK) ON SOD.ID = SODD.ServiceOrderDetailId And SODD.RevenueControlDetailId = I.RevenueControlDetailId left JOIN
                         Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id = SOD.CUPSEntityId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = CE.BillingGroupId INNER JOIN
                         Contract.IPSService AS IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber LEFT OUTER JOIN
						 Billing.ServiceOrderDetailSurgical AS SODS WITH (NOLOCK) ON SODS.ServiceOrderDetailId = SOD.Id AND SODS.TotalSalesPrice >= 0 AND SODS.OnlyMedicalFees = 0 LEFT OUTER JOIN
                         Contract.IPSService AS IPSS WITH (NOLOCK) ON SODS.IPSServiceId = IPSS.Id
						 left outer join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
						 left outer join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
WHERE        sod.GrandTotalSalesPrice >= 0 and sod.IsDelete = 0 AND I.[Status] = 1 --and sod.IsPackage = 0
UNION ALL
SELECT        i.Id, i.InvoiceNumber, sod.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, NULL AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         IP.Code AS CUPSCode, SUBSTRING(IP.Name, 1, 94) AS CUPSName, IP.Code, SUBSTRING(IP.Name,1,94) AS Name, SOD.ServiceDate, SOD.InvoicedQuantity, SOD.TotalSalesPrice, 
                         SODD.SubTotalPatientSalesPrice, SODD.ThirdPartySalesPrice, IP.Code, IP.Name, SOD.InvoicedQuantity AS QuantitySurgical, 
                         SOD.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, Ip.CodeAlternative AS CodeAlternative, 
                         IP.CodeAlternativeTwo AS CodeAlternativeTwo, case LTRIM(RTRIM(isnull(IP.CodeCUM,''))) when '' then IP.Code else IP.CodeCUM end AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, '' as RIPSCode, '' as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 '' ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         BILLING.ServiceOrderDetailDistribution AS SODD WITH (NOLOCK) ON SOD.ID = SODD.ServiceOrderDetailId And SODD.RevenueControlDetailId = I.RevenueControlDetailId INNER JOIN
                         Inventory.InventoryProduct AS Ip WITH (NOLOCK) ON Ip.Id = SOD.ProductId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = IP.BillingGroupId LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber 
WHERE        SODD.GrandTotalSalesPrice >= 0 and sod.IsDelete = 0 AND I.[Status] = 1
UNION ALL
--Permite mostrar los servicios de lo empaquetado
SELECT        i.Id, i.InvoiceNumber, sodp.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, sods.Id AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         ce.Code AS CUPSCode, SUBSTRING(CE.Description, 1, 94) AS CUPSName, IPS.Code, SUBSTRING(IPS.Name, 1,94) as Name, SOD.ServiceDate, SOD.InvoicedQuantity, 
                         SODp.TotalSalesPrice, 0 as SubTotalPatientSalesPrice, 0 as ThirdPartySalesPrice, IPSS.Code AS CodeSurgical, SUBSTRING(IPSS.Name, 1, 100) 
                         AS NameSurgical, SODS.InvoicedQuantity AS QuantitySurgical, SODS.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, 
                         ce.Code AS CodeAlternative, ce.Code AS CodeAlternativeTwo, ce.Code AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, ce.RIPSCode,ce.RIPSDescription as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 cd.Name ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Billing.ServiceorderDetail as Sodp on sodp.PackageServiceOrderDetailId = sod.Id inner JOIN 
                         Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id = SODp.CUPSEntityId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = CE.BillingGroupId INNER JOIN
                         Contract.IPSService AS IPS WITH (NOLOCK) ON SODp.IPSServiceId = IPS.Id LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber LEFT OUTER JOIN 
                         Billing.ServiceOrderDetailSurgical AS SODS WITH (NOLOCK) ON SODS.ServiceOrderDetailId = SOD.Id AND SODS.TotalSalesPrice >= 0 AND SODS.OnlyMedicalFees = 0 LEFT OUTER JOIN
                         Contract.IPSService AS IPSS WITH (NOLOCK) ON SODS.IPSServiceId = IPSS.Id
						 left outer join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
						 left outer join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
WHERE        sod.GrandTotalSalesPrice >= 0 and sod.IsDelete = 0 AND I.[Status] = 1 
UNION ALL
--Permite mostrar los productos de lo empaquetado
SELECT        i.Id, i.InvoiceNumber, sodp.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, NULL AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         IP.Code AS CUPSCode, SUBSTRING(IP.Name, 1, 94) AS CUPSName, IP.Code, SUBSTRING(IP.Name,1,94) AS Name, SOD.ServiceDate, SOD.InvoicedQuantity, SODp.TotalSalesPrice, 
                         0 as SubTotalPatientSalesPrice,0 as ThirdPartySalesPrice, IP.Code, IP.Name, SOD.InvoicedQuantity AS QuantitySurgical, 
                         SOD.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, Ip.CodeAlternative AS CodeAlternative, 
                         IP.CodeAlternativeTwo AS CodeAlternativeTwo, case LTRIM(RTRIM(isnull(IP.CodeCUM,''))) when '' then IP.Code else IP.CodeCUM end AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, '' as RIPSCode, '' as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 '' ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Billing.ServiceorderDetail as Sodp on sodp.PackageServiceOrderDetailId = sod.Id inner JOIN  
                         Inventory.InventoryProduct AS Ip WITH (NOLOCK) ON Ip.Id = SODp.ProductId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = IP.BillingGroupId	LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber
WHERE        SODp.GrandTotalSalesPrice >= 0 and sod.IsDelete = 0 AND I.[Status] = 1
UNION ALL
SELECT        i.Id, i.InvoiceNumber, sod.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, sods.Id AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         ce.Code AS CUPSCode, SUBSTRING(CE.Description, 1, 94) AS CUPSName, IPS.Code, SUBSTRING(IPS.Name, 1,94) as Name, SOD.ServiceDate, SOD.InvoicedQuantity, 
                         SOD.TotalSalesPrice, ID.SubTotalPatientSalesPrice, ID.ThirdPartySalesPrice, IPSS.Code AS CodeSurgical, SUBSTRING(IPSS.Name, 1, 100) 
                         AS NameSurgical, SODS.InvoicedQuantity AS QuantitySurgical, SODS.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, 
                         ce.Code AS CodeAlternative, ce.Code AS CodeAlternativeTwo, ce.Code AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, ce.RIPSCode,ce.RIPSDescription as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 cd.Name ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id = SOD.CUPSEntityId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = CE.BillingGroupId INNER JOIN
                         Contract.IPSService AS IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id LEFT OUTER JOIN
                         Billing.ServiceOrderDetailSurgical AS SODS WITH (NOLOCK) ON SODS.ServiceOrderDetailId = SOD.Id AND SODS.TotalSalesPrice >= 0 AND SODS.OnlyMedicalFees = 0 LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber LEFT OUTER JOIN
                         Contract.IPSService AS IPSS WITH (NOLOCK) ON SODS.IPSServiceId = IPSS.Id
						 left outer join Contract.CUPSEntityContractDescriptions cecd with(nolock) on cecd.Id = SOD.CUPSEntityContractDescriptionId
						 left outer join Contract.ContractDescriptions cd with(nolock) on cd.Id = cecd.ContractDescriptionId
WHERE        sod.GrandTotalSalesPrice >= 0 and sod.IsDelete = 0 AND I.[Status] = 2
UNION ALL
SELECT        i.Id, i.InvoiceNumber, sod.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, NULL AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, 
                         IP.Code AS CUPSCode, SUBSTRING(IP.Name, 1, 94) AS CUPSName, IP.Code, SUBSTRING(IP.Name,1,94) AS Name, SOD.ServiceDate, SOD.InvoicedQuantity, SOD.TotalSalesPrice, 
                         ID.SubTotalPatientSalesPrice, ID.ThirdPartySalesPrice, IP.Code, IP.Name, SOD.InvoicedQuantity AS QuantitySurgical, 
                         SOD.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, Ip.CodeAlternative AS CodeAlternative, 
                         IP.CodeAlternativeTwo AS CodeAlternativeTwo, case LTRIM(RTRIM(isnull(IP.CodeCUM,''))) when '' then IP.Code else IP.CodeCUM end AS CodeCUM, SOD.RecordType,I.AdmissionNumber,
						 SOD.AuthorizationNumber, '' as RIPSCode, '' as RIPSName,
						 INGRES.FECHA as ProcessingDate, INGRES.LINEA as ProcessLine, INGRES.ENTIDAD as PacientEntity, INGRES.REGIMEN as PacientRegimen, INGRES.TIPOAFILIADO as AffiliateType,
						 INGRES.ESTADO as AffiliateStatus,INGRES.CONFIRMADOERP as ERPConfirm, INGRES.REPORTAIPS as IPSReport, INGRES.IAUTORIZA as AutorizacionIngreso,
						 '' ContractDescriptionName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Inventory.InventoryProduct AS Ip WITH (NOLOCK) ON Ip.Id = SOD.ProductId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = IP.BillingGroupId LEFT OUTER JOIN
						 dbo.ADINGRESO AS INGRES WITH (NOLOCK) ON INGRES.NUMINGRES = I.AdmissionNumber
WHERE        sod.IsDelete = 0 AND I.[Status] = 2