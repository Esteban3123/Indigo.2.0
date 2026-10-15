




CREATE VIEW [Billing].[vFURIPS2Invoice]
AS

WITH IPSService_CTE AS (
		SELECT ips.Id,
			   ips.code,
			   ips.Name,
			   ips.SurgicalGroupId,
			   ips.SubattentionCode
		FROM Contract.IPSService ips
)

SELECT	InvoiceId,
		NumeroFactura,
		Ingreso,
		Tipo,
		CodigoCUM, 
		Descripcion, 
		SUM(Cantidad) AS Cantidad, 
		ValorUnitario AS ValorUnitario, 
		SUM(SubTotal) as SubTotal, 
		SUM(Total) as Total
FROM
(
	SELECT	CAST(concat(i.Id,id.Id) as VARCHAR(500)) idParaOrdenar,
			i.Id AS InvoiceId,
			i.InvoiceNumber AS NumeroFactura,
			i.AdmissionNumber AS Ingreso,
			CASE ce.RIPSConcept 
				WHEN '14' THEN 5 
				WHEN '09' THEN 5 
				ELSE 2 
			END AS Tipo,
			CASE ce.RIPSConcept 
				WHEN '14' THEN '' 
				WHEN '09' THEN '' 
				ELSE ips.Code 
			END AS CodigoCUM,
			LEFT(ips.Name, 100) AS Descripcion,
			IIF(sod.Presentation = 2,0,sod.InvoicedQuantity) AS Cantidad,
			IIF(sod.Presentation = 2,0,sod.SubTotalSalesPrice) AS ValorUnitario,
			IIF(sod.Presentation = 2,0,sod.InvoicedQuantity * sod.SubTotalSalesPrice) AS SubTotal,
			IIF(sod.Presentation = 2,0,sod.InvoicedQuantity * sod.SubTotalSalesPrice) AS Total
	FROM Billing.Invoice i WITH (NOLOCK)
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
	JOIN Contract.CUPSEntity ce WITH (NOLOCK) ON sod.CUPSEntityId = ce.Id
	JOIN IPSService_CTE ips ON sod.IPSServiceId = ips.Id
	WHERE sod.SubTotalSalesPrice > 0
UNION ALL
	SELECT	CAST(concat(i.Id,'-',id.Id) as VARCHAR(500)) idParaOrdenar,
			i.Id as InvoiceId,
			i.InvoiceNumber as NumeroFactura,
			i.AdmissionNumber as Ingreso,
			2 as Tipo,
			ips.Code as CodigoCUM,
			left(CONCAT(ips2.Code,'-',[Contract].[SubattentionName](ips.SubattentionCode),'-',REPLACE(sg.name,'  ','-')),100) as descripcion,
			sod.InvoicedQuantity as Cantidad,
			sods.TotalSalesPrice as ValorUnitario,
			sod.InvoicedQuantity * sods.TotalSalesPrice as SubTotal,
			sod.InvoicedQuantity * sods.TotalSalesPrice as Total
	FROM Billing.Invoice i WITH (NOLOCK)
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
	JOIN Billing.ServiceOrderDetailSurgical sods WITH (NOLOCK) ON sod.Id = sods.ServiceOrderDetailId
	JOIN IPSService_CTE ips ON sods.IPSServiceId = ips.Id
	JOIN IPSService_CTE ips2 on ips2.id = sod.IPSServiceId
	JOIN Contract.SurgicalGroup sg WITH(NOLOCK) on sg.id = ips2.SurgicalGroupId
	WHERE sod.Presentation = 2 AND sods.TotalSalesPrice > 0
UNION ALL
	SELECT	CAST(concat(i.Id,'-',id.Id) as VARCHAR(500)) idParaOrdenar,
			i.Id as InvoiceId,
			i.InvoiceNumber as NumeroFactura,
			i.AdmissionNumber as Ingreso,
			iif(su.MedicalDevice = 1,6,iif(su.OsteosynthesisMaterial = 1,7,case pt.Class when 2 then 1 else 5 end)) as Tipo,
			iif(su.MedicalDevice = 1, ip.HealthRegistration, case pt.Class when 2 then ip.CodeCUM else '' end ) as CodigoCUM,
			iif(su.OsteosynthesisMaterial = 1,
				LEFT(CONCAT(RTRIM(ip.Description),' ',RTRIM(Billing.GetSupplierSod(sod.Id))), 100),
				LEFT(ip.Name, 100)) as Descripcion,
			sod.InvoicedQuantity as Cantidad,
			sod.SubTotalSalesPrice as ValorUnitario,
			sod.InvoicedQuantity * sod.SubTotalSalesPrice as SubTotal,
			sod.InvoicedQuantity * sod.SubTotalSalesPrice as Total
	FROM Billing.Invoice i WITH (NOLOCK)
	JOIN Billing.InvoiceDetail id WITH (NOLOCK) ON i.Id = id.InvoiceId
	JOIN Billing.ServiceOrderDetail sod WITH (NOLOCK) ON id.ServiceOrderDetailId = sod.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON sod.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.InventorySupplie su WITH(NOLOCK) on su.id = ip.SupplieId
	WHERE sod.SubTotalSalesPrice > 0
) as data 
GROUP BY data.idParaOrdenar, InvoiceId, NumeroFactura, Ingreso, Tipo, CodigoCUM, Descripcion, ValorUnitario