
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
			NULL IUM,
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
			COALESCE(NULLIF(LTRIM(RTRIM(ip.IUM)), ''), NULLIF(LTRIM(RTRIM(ip.CodeCUM)), ''), ip.Code) CodeCUM,
			ip.IUM,
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle parcial de facturación para reportes de facturas, combinando servicios de salud (procedimientos y medicamentos/insumos) con su distribución financiera entre el tercero pagador (EPS/aseguradora) y el paciente. Integra la información de órdenes de servicio, distribución de valores, grupos de facturación, códigos CUPS, servicios propios de la IPS, descripciones de contrato y detalle quirúrgico, aplicando la configuración de cuenta madre del módulo de facturación para calcular correctamente cantidades facturadas, precios brutos, descuentos, IVA y valores netos unitarios y totales. Existe para alimentar reportes detallados de facturación parcial, conciliación de ingresos y auditoría de cobros por folio de control de ingresos.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoicePartialDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoicePartialDetail';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el detalle de ítems facturados (servicios y productos) para reportes de factura parcial, unificando información de procedimientos CUPS, productos de inventario, detalle quirúrgico, IVA y valores con/sin liquidación por cuenta madre.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos un registro en Billing.SettingsBilling para resolver el flag LiquidateMasterAccount (CTE toma TOP 1 ordenado DESC, asume 0 si NULL).; Los ítems considerados deben tener IsDelete=0 y SettlementType distinto de 3 (excluye anulados/no liquidables).; Para la rama de servicios: GrandTotalSalesPrice del detalle > 0.; Para la rama de productos: GrandTotalSalesPrice de la distribución > 0.; Cada ServiceOrderDetail debe estar asociado a un CUPSEntity e IPSService (rama servicios) o a un InventoryProduct (rama productos).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Excluye siempre ítems con IsDelete=1 o SettlementType=3.; El comportamiento de cálculo de IVA, valor bruto, neto y cantidad facturada está gobernado por un único flag global LiquidateMasterAccount (TOP 1 de SettingsBilling).; Para un mismo ServiceOrderDetail con múltiples registros quirúrgicos válidos, solo el de menor Id conserva las cantidades/valores facturados al paciente y al tercero, evitando duplicación.; El Id de la fila resultante es la concatenación de IDs (sodd.Id+sods.Id en servicios, sodd.Id+psd.Id en productos), por lo que no es único de una sola tabla.; NetUnitValue siempre se calcula como (SubTotalSalesPrice - GrandTotalDiscount)/Quantity de la distribución, redondeado a 2 decimales, sin importar el flag de cuenta madre.; Solo considera detalles quirúrgicos que NO sean exclusivos de honorarios médicos (OnlyMedicalFees=0) y con TotalSalesPrice>0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportInvoicePartialDetail: Devuelve dos conjuntos unidos por UNION ALL: (1) ítems de servicios CUPS/IPS con eventual detalle quirúrgico y (2) ítems de productos de inventario con eventual ProductServiceDetail.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Settings_cte.LiquidateMasterAccount = 0 → Los valores IvaTotalValue, GrossValue, NetWorth e InvoicedQuantity se calculan a partir de ServiceOrderDetail (sod.TaxValue*InvoicedQuantity, sod.GrossValue, sod.GrossValue*InvoicedQuantity). else Se calculan desde ServiceOrderDetailDistribution (sodd.GrandTotalTaxes, SubTotalSalesPrice/Quantity, SubTotalSalesPrice-GrandTotalDiscount, sodd.Quantity), reflejando liquidación por cuenta madre.; si ISNULL(sod.ApplyRIAS,0) = 1 (rama servicios) → El BillingGroup se toma de ce.RIASBillingGroupId (con fallback a ce.BillingGroupId). else Se toma de ce.BillingGroupId.; si Existe ServiceOrderDetailSurgical con TotalSalesPrice>0 y OnlyMedicalFees=0 (rama servicios) → Se expone el detalle quirúrgico, pero InvoicedQuantity/SubTotalPatientSalesPrice/ThirdPartySalesPrice solo conservan el valor cuando el surgical es el de menor Id (sodsf); para los demás surgical relacionados al mismo detalle se devuelven en 0 para no duplicar valores. else Se reportan los valores originales del detalle y los campos quirúrgicos quedan en NULL.; si psd.Id IS NOT NULL (existe ProductServiceDetail para el ítem de producto) → Los precios y descuentos se recalculan desde psd: TotalSalesPrice = psd.Price - (psd.Price*psd.DiscountPercentage/100); ThirdPartyDiscount = psd.DiscountPercentage*psd.Price/100; el RecordType se fuerza a 1 cuando psd.CUPSEntityId no es nulo, y se toma el código/nombre del producto ips referenciado por psd. else Se usan los valores tal cual de ServiceOrderDetail/ServiceOrderDetailDistribution.; si psd.CUPSEntityId IS NULL (rama productos) → El BillingGroup se obtiene de InventoryProduct.BillingGroupId. else Se obtiene de CUPSEntity.BillingGroupId asociado al psd.; si LTRIM(RTRIM(ip.CodeCUM)) = '''' o NULL → CodeCUM expuesto = ip.Code. else CodeCUM expuesto = ip.CodeCUM.; si sod.IvaId tiene match en GeneralLedgerIVA → IvaPercentage se muestra como ''X%'' y TaxPercentage = GLI.Percentage. else IvaPercentage = '''' y TaxPercentage = 0.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.SettingsBilling; Billing.ServiceOrderDetailDistribution; Billing.ServiceOrderDetail; Contract.CUPSEntity; Contract.IPSService; Billing.BillingGroup; Billing.RevenueControlDetail; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Billing.ServiceOrderDetailSurgical; GeneralLedger.GeneralLedgerIVA; Inventory.InventoryProduct; Billing.ProductServiceDetail', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoicePartialDetail';
GO
