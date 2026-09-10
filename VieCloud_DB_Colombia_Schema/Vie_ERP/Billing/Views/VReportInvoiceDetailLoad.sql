

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de detalle de facturación para carga y reporte de facturas emitidas. Consolida en un único resultado las líneas de factura (servicios, procedimientos, medicamentos e insumos, así como servicios empaquetados) cruzando el encabezado de factura, el detalle de la orden de servicio, la distribución financiera entre el tercero pagador (EPS, aseguradora) y el paciente, el catálogo CUPS, el catálogo de servicios IPS, el inventario de productos y el detalle quirúrgico. Incorpora además datos del ingreso o admisión del paciente (número de ingreso, entidad, régimen, tipo de afiliado, estado) y la descripción del contrato asociado. Se utiliza para generación de reportes de facturación, conciliación de valores cobrados, reportería RIPS y análisis de cartera, permitiendo ver por cada ítem facturado el grupo de facturación, código CUPS, código CUM, precio de venta, subtotales por paciente y tercero, cantidades, fecha del servicio, número de autorización y confirmación en el ERP.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetailLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetailLoad';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en seis ramas UNION los renglones que alimentan el reporte de detalle de facturación, mostrando servicios CUPS, productos de inventario, ítems empaquetados y datos del ingreso del paciente para facturas en estado 1 o 2.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de la factura (Billing.Invoice) con Status en {1,2} y AdmissionNumber consistente con dbo.ADINGRESO.NUMINGRES para enlazar datos del ingreso; Cada InvoiceDetail debe tener un ServiceOrderDetail asociado y, para Status=1, una ServiceOrderDetailDistribution cuyo RevenueControlDetailId coincida con el de la factura; Los ítems deben estar vigentes (sod.IsDelete = 0); Para ítems de servicio se requiere CUPSEntity y BillingGroup; para ítems de inventario se requiere InventoryProduct con su BillingGroup; Para mostrar detalle quirúrgico debe existir ServiceOrderDetailSurgical con TotalSalesPrice >= 0 y OnlyMedicalFees = 0', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen ítems no eliminados (sod.IsDelete = 0); Para Status=1 se exige GrandTotalSalesPrice >= 0 (sea de SOD o SODD según rama); para Status=2 sobre productos no se aplica esa restricción; Solo se incluyen detalles quirúrgicos con TotalSalesPrice >= 0 y OnlyMedicalFees = 0 (excluye filas de solo honorarios médicos); Las filas de Status=1 usan la distribución SODD enlazada al RevenueControlDetailId de la factura, garantizando coherencia entre factura y distribución de ingresos; Los nombres CUPS/IPS se truncan: CUPSName e IPS.Name a 94 caracteres; NameSurgical a 100; Para los ítems empaquetados los valores de paciente y tercero se reportan en 0 (los valores reales quedan en el ítem padre/paquete); BillingGroup se construye como ''Code - Name''; El reporte solo considera facturas en Status 1 o 2; otros estados quedan excluidos; Datos del ingreso (ADINGRESO) son opcionales: si no hay coincidencia por NUMINGRES = AdmissionNumber, las columnas relacionadas quedan en NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; CUPS; IPS / Servicio IPS; Grupo de facturación; Paquete de servicios; Producto de inventario / medicamento / insumo; Cirugía / honorarios médicos; Ingreso/admisión de paciente; Régimen y tipo de afiliado; Autorización; RIPS; Copago/Cuota paciente vs tercero pagador; Contrato / Descripción de contrato', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si I.Status = 1 (factura activa) y el ítem es un servicio CUPS (SOD.CUPSEntityId) → Genera fila usando CUPSEntity, IPSService y datos de distribución (SODD), con valores de paciente/tercero desde ServiceOrderDetailDistribution; si I.Status = 1 y el ítem es un producto de inventario (SOD.ProductId) → Genera fila usando InventoryProduct, con CodeAlternative, CodeAlternativeTwo y CodeCUM (si CodeCUM está vacío usa Code); si I.Status = 1 y existe un detalle empaquetado (sodp.PackageServiceOrderDetailId = sod.Id) con CUPSEntity → Expone los servicios contenidos en el paquete con SubTotalPatientSalesPrice=0 y ThirdPartySalesPrice=0; si I.Status = 1 y existe un detalle empaquetado con producto de inventario → Expone los productos contenidos en el paquete con valores de paciente/tercero en 0; si I.Status = 2 (factura en estado distinto, posiblemente anulada/borrador) y el ítem es servicio CUPS → Genera fila tomando los valores SubTotalPatientSalesPrice y ThirdPartySalesPrice desde InvoiceDetail (ID) en lugar de SODD; si I.Status = 2 y el ítem es producto de inventario → Genera fila desde InventoryProduct usando los valores monetarios del InvoiceDetail; no se filtra por GrandTotalSalesPrice >= 0; si IP.CodeCUM es NULL o vacío tras LTRIM/RTRIM → Se usa IP.Code como CodeCUM else Se usa IP.CodeCUM', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceDetail; Billing.Invoice; Billing.ServiceOrderDetail; Billing.ServiceOrderDetailDistribution; Contract.CUPSEntity; Billing.BillingGroup; Contract.IPSService; dbo.ADINGRESO; Billing.ServiceOrderDetailSurgical; Contract.CUPSEntityContractDescriptions; Contract.ContractDescriptions; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailLoad';
GO
