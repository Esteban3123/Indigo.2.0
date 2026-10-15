



CREATE VIEW [Billing].[VReportInvoiceDetailAnulate]
AS
SELECT        i.Id, i.InvoiceNumber, ID.ServiceOrderDetailId AS serviceOrderDetailId, id.Id AS invoiceDetailId, sods.Id AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, ce.Code AS CUPSCode, SUBSTRING(CE.Description, 1, 94) 
                         AS CUPSName, IPS.Code, SUBSTRING(IPS.Name, 1, 94) AS Name, ID.ServiceDate, ID.InvoicedQuantity, ID.TotalSalesPrice, ID.SubTotalPatientSalesPrice, ID.ThirdPartySalesPrice, 
                         IPSS.Code AS CodeSurgical, SUBSTRING(IPSS.Name, 1, 100) AS NameSurgical, SODS.InvoicedQuantity AS QuantitySurgical, SODS.TotalSalesPrice AS TotalSalesPriceSurgical, ID.ThirdPartyDiscount, 
                         ID.Presentation, ce.Code AS CodeAlternative, ce.Code AS CodeAlternativeTwo, ce.Code AS CodeCUM, ID.RecordType,
						 SOD.AuthorizationNumber, ce.RIPSCode,ce.RIPSDescription as RIPSName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Contract.CUPSEntity AS CE WITH (NOLOCK) ON CE.Id = SOD.CUPSEntityId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = CE.BillingGroupId INNER JOIN
                         Contract.IPSService AS IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.Id LEFT OUTER JOIN
                         Billing.InvoiceDetailSurgical AS SODS WITH (NOLOCK) ON SODS.InvoiceDetailId = ID.Id AND SODS.TotalSalesPrice > 0 AND SODS.OnlyMedicalFees = 0 LEFT OUTER JOIN
                         Contract.IPSService AS IPSS WITH (NOLOCK) ON SODS.IPSServiceId = IPSS.Id
WHERE        sod.SettlementType != 3 AND sod.GrandTotalSalesPrice > 0 AND sod.IsDelete = 0
UNION
SELECT        i.Id, i.InvoiceNumber, sod.Id AS serviceOrderDetailId, id.Id AS invoiceDetailId, NULL AS surgicalId, BG.Code + ' - ' + BG.Name AS BillingGroup, IP.Code AS CUPSCode, SUBSTRING(IP.Name, 1, 94) AS CUPSName, 
                         IP.Code, SUBSTRING(IP.Name, 1, 94) AS Name, SOD.ServiceDate, SOD.InvoicedQuantity, SOD.TotalSalesPrice, ID.SubTotalPatientSalesPrice, ID.ThirdPartySalesPrice, IP.Code, IP.Name, 
                         SOD.InvoicedQuantity AS QuantitySurgical, SOD.TotalSalesPrice AS TotalSalesPriceSurgical, sod.ThirdPartyDiscount, sod.Presentation, Ip.CodeAlternative AS CodeAlternative, 
                         IP.CodeAlternativeTwo AS CodeAlternativeTwo, CASE LTRIM(RTRIM(isnull(IP.CodeCUM, ''))) WHEN '' THEN IP.Code ELSE IP.CodeCUM END AS CodeCUM, SOD.RecordType,
						 SOD.AuthorizationNumber, '' as RIPSCode, '' as RIPSName
FROM            Billing.InvoiceDetail AS ID WITH (NOLOCK) INNER JOIN
                         Billing.Invoice AS I WITH (NOLOCK) ON ID.InvoiceId = I.Id INNER JOIN
                         Billing.ServiceOrderDetail AS SOD WITH (NOLOCK) ON SOD.Id = ID.ServiceOrderDetailId INNER JOIN
                         Inventory.InventoryProduct AS Ip WITH (NOLOCK) ON Ip.Id = SOD.ProductId INNER JOIN
                         Billing.BillingGroup AS BG WITH (NOLOCK) ON BG.Id = IP.BillingGroupId
WHERE        SOD.SettlementType != 3 AND SOD.GrandTotalSalesPrice > 0 AND sod.IsDelete = 0