

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de servicios facturados para la generación del archivo RIPS (Registro Individual de Prestación de Servicios) asociado a cada factura. Integra tres tipos de líneas de facturación: servicios y procedimientos estándar con código CUPS, servicios quirúrgicos con sus honorarios y grupo quirúrgico, y productos de inventario como medicamentos, dispositivos médicos y material de osteosíntesis. Para cada línea agrupa cantidades, valor unitario y totales facturados, clasificándolos según el concepto RIPS (medicamentos, procedimientos, dispositivos médicos, material de osteosíntesis, etc.) e incluyendo el código CUM cuando aplica. Es el insumo principal para reportar ante el ente regulador los servicios prestados y facturados por número de factura y número de ingreso del paciente.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'vFURIPS2Invoice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'vFURIPS2Invoice';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Construye el detalle de ítems facturados en formato FURIPS (procedimientos CUPS, paquetes quirúrgicos, medicamentos, insumos y dispositivos) consolidando líneas de factura con su tipo, código, descripción y valores para reportes a pagadores.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las facturas deben tener detalle (Billing.InvoiceDetail) ligado a un ServiceOrderDetail válido; Cada ServiceOrderDetail debe tener tarifa positiva (SubTotalSalesPrice > 0) o, en el caso quirúrgico, TotalSalesPrice > 0 en ServiceOrderDetailSurgical; Los productos referenciados deben existir en Inventory.InventoryProduct con su ProductType; el SupplieId puede ser nulo (LEFT JOIN); Los servicios CUPS deben estar registrados en Contract.CUPSEntity y Contract.IPSService', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen líneas con valor de venta estrictamente positivo (SubTotalSalesPrice > 0 o TotalSalesPrice > 0); Los conceptos RIPS ''14'' y ''09'' nunca exponen código CUM/CUPS (se reportan en blanco); La descripción se trunca a máximo 100 caracteres en todas las ramas; Los ítems con Presentation=2 en la rama CUPS aportan cantidad y valor cero, evitando doble facturación con la rama quirúrgica; El tipo FURIPS se asigna excluyentemente: 1=medicamento, 2=procedimiento/CUPS, 5=otros/consultas, 6=dispositivo médico, 7=material de osteosíntesis; Para osteosíntesis la descripción siempre incluye el proveedor del SOD', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'FURIPS; Factura; Detalle de factura; Orden de servicio; CUPS; Concepto RIPS; Grupo quirúrgico; Subatención; Medicamento (CUM); Dispositivo médico; Material de osteosíntesis; Registro sanitario; Proveedor de insumo', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve agregado por idParaOrdenar/InvoiceId/Tipo/CodigoCUM/Descripcion/ValorUnitario sumando Cantidad, SubTotal y Total provenientes de tres fuentes unidas (CUPS, paquete quirúrgico, productos de inventario).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ce.RIPSConcept = ''14'' o ''09'' (rama CUPS) → Tipo=5 y CodigoCUM se reporta vacío else Tipo=2 y CodigoCUM = ips.Code; si sod.Presentation = 2 (rama CUPS) → Cantidad, ValorUnitario, SubTotal y Total se fuerzan a 0 (la facturación real se toma en la rama quirúrgica) else Se usan los valores de ServiceOrderDetail; si sod.Presentation = 2 (rama quirúrgica) → Se construye una línea Tipo=2 con CodigoCUM=ips.Code y descripción concatenando código del servicio principal, nombre de la subatención (Contract.SubattentionName) y nombre del grupo quirúrgico, valorada con sods.TotalSalesPrice; si su.MedicalDevice = 1 (rama productos) → Tipo=6 y CodigoCUM = ip.HealthRegistration (registro sanitario del dispositivo médico) else Si su.OsteosynthesisMaterial = 1 → Tipo=7 y descripción concatena descripción del producto + proveedor (Billing.GetSupplierSod); en otro caso, si pt.Class=2 → Tipo=1 y CodigoCUM=ip.CodeCUM (medicamento), si no → Tipo=5 y CodigoCUM vacío', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Contract.SubattentionName; Billing.GetSupplierSod', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Contract.IPSService; Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Contract.CUPSEntity; Billing.ServiceOrderDetailSurgical; Contract.SurgicalGroup; Inventory.InventoryProduct; Inventory.ProductType; Inventory.InventorySupplie', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'vFURIPS2Invoice';
GO
