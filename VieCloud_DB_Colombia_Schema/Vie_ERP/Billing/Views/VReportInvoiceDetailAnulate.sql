

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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el detalle de líneas de factura para el reporte de facturas anuladas, combinando dos fuentes: servicios y procedimientos facturados con su código CUPS, grupo de facturación, cantidades, valores totales, valor a cargo del paciente y del tercero pagador (EPS o aseguradora), y cuando aplica, el detalle quirúrgico asociado; y medicamentos o insumos de inventario facturados con sus códigos alternativos y código CUM. Integra los encabezados de factura (número de factura), el detalle de cada línea (InvoiceDetail), la orden de servicio (ServiceOrderDetail), el catálogo CUPS, los servicios propios de la IPS, los grupos de facturación y el detalle quirúrgico (InvoiceDetailSurgical). Sirve para reportería de auditoría y conciliación de facturas anuladas, permitiendo identificar por factura cada servicio, procedimiento o medicamento cobrado, su valor, número de autorización y código RIPS.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetailAnulate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'VReportInvoiceDetailAnulate';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica en una sola vista las líneas facturadas tanto de servicios CUPS (con su detalle quirúrgico y código RIPS) como de productos de inventario (medicamentos/insumos con código CUM y alternativos), filtrando líneas válidas para reportería de auditoría y conciliación de facturas.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las líneas de InvoiceDetail deben estar asociadas a una ServiceOrderDetail válida; Para el bloque CUPS, SOD.CUPSEntityId debe existir en Contract.CUPSEntity y su BillingGroupId en Billing.BillingGroup; Para el bloque de inventario, SOD.ProductId debe existir en Inventory.InventoryProduct con BillingGroupId válido; SOD.IPSServiceId debe existir en Contract.IPSService (bloque CUPS)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen líneas con SOD.SettlementType != 3 (excluye un tipo de liquidación específico); Solo se exponen líneas con SOD.GrandTotalSalesPrice > 0 (con valor facturado positivo); Solo se exponen líneas no eliminadas lógicamente (sod.IsDelete = 0); El detalle quirúrgico solo se vincula cuando tiene precio de venta positivo y no corresponde únicamente a honorarios médicos (OnlyMedicalFees = 0); Las descripciones largas se truncan: CUPSName/Name a 94 caracteres y NameSurgical a 100 caracteres; El campo BillingGroup se presenta concatenando ''Code - Name'' del grupo de facturación; Para productos de inventario, RIPSCode y RIPSName siempre son cadena vacía (no aplica RIPS); El UNION elimina duplicados exactos entre el bloque de servicios CUPS y el de productos de inventario', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; CUPS; Código RIPS; Grupo de facturación; Servicio IPS; Detalle quirúrgico; Honorarios médicos; Medicamento/insumo de inventario; Código CUM; Número de autorización; Tipo de liquidación', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Billing.VReportInvoiceDetailAnulate: Devuelve un conjunto unificado (UNION) de líneas de factura que cumplen SettlementType != 3, GrandTotalSalesPrice > 0 e IsDelete = 0, separando servicios CUPS y productos de inventario', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Línea de detalle corresponde a un servicio CUPS (SOD.CUPSEntityId existente) → Toma datos del catálogo CUPSEntity y agrega LEFT JOIN con InvoiceDetailSurgical/IPSService para incluir detalle quirúrgico; expone RIPSCode y RIPSDescription del CUPS; si Línea de detalle corresponde a un producto de inventario (SOD.ProductId) → Toma datos de InventoryProduct (medicamento/insumo) usando CodeAlternative, CodeAlternativeTwo y CodeCUM; RIPSCode y RIPSName se devuelven vacíos; si LTRIM(RTRIM(ISNULL(IP.CodeCUM,''''))) = '''' (producto sin código CUM) → CodeCUM se reemplaza por IP.Code else Se usa IP.CodeCUM; si En el bloque CUPS: SODS.TotalSalesPrice > 0 AND SODS.OnlyMedicalFees = 0 → Se incluye el detalle quirúrgico vía LEFT JOIN con InvoiceDetailSurgical else Las columnas quirúrgicas quedan en NULL', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.InvoiceDetail; Billing.Invoice; Billing.ServiceOrderDetail; Contract.CUPSEntity; Billing.BillingGroup; Contract.IPSService; Billing.InvoiceDetailSurgical; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'VReportInvoiceDetailAnulate';
GO
