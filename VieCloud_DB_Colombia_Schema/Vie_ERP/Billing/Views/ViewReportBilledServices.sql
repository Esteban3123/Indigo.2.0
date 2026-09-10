

CREATE VIEW [Billing].[ViewReportBilledServices]
AS

SELECT  SOD.Id, I.InvoiceNumber, I.InvoiceDate, sod.ServiceDate, RTRIM(INP.IPNOMCOMP) AS PatientName, I.PatientCode As CODIGONIT, IPS.Code, ips.Name AS [Description], sod.TotalSalesPrice * sod.InvoicedQuantity as TotalSalesPrice, 
I.DocumentType, (ha.Code + ' - ' + ha.Name) AS Entity, c.ContractNumber, ha.Code AS HealthAdministratorCode, I.PatientCode, I.CareGroupId, I.InvoiceCategoryId, I.ThirdPartyId
FROM
[Billing].[Invoice] AS I WITH (NOLOCK) INNER JOIN 
[Billing].[InvoiceDetail] AS ID WITH (NOLOCK) ON I.ID = ID.InvoiceId INNER JOIN
[Billing].[ServiceOrderDetail] AS SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.ID INNER JOIN
[Billing].[ServiceOrder] AS SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id  INNER JOIN 
[Contract].[IPSService] AS IPS WITH (NOLOCK) ON SOD.IPSServiceId = IPS.ID INNER JOIN
[Contract].[CUPSEntity] AS CUPS WITH (NOLOCK) ON SOD.CUPSEntityId = CUPS.Id LEFT OUTER JOIN 
[Contract].[HealthAdministrator] AS HA WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id LEFT OUTER JOIN 
[Contract].[CareGroup] AS CG WITH (NOLOCK) ON I.CareGroupId = CG.ID LEFT OUTER JOIN 
[Contract].[Contract] AS C WITH (NOLOCK) ON CG.ContractId = C.ID LEFT OUTER JOIN 
dbo.INPACIENT AS INP  WITH (NOLOCK) ON I.PatientCode = INP.IPCODPACI INNER JOIN 
dbo.ADINGRESO AS AD WITH (NOLOCK)  ON I.AdmissionNumber = AD.NUMINGRES
WHERE  SO.[Status] = 1 AND SOD.SettlementType != 3 AND SOD.IsDelete = 0 AND SOD.RecordType = 1 and i.[Status] = 1
UNION
SELECT  SOD.Id, I.InvoiceNumber, I.InvoiceDate, sod.ServiceDate, RTRIM(inp.IPNOMCOMP) AS PatientName, i.PatientCode As CODIGONIT, P.Code, P.[Description], sod.TotalSalesPrice * sod.InvoicedQuantity as TotalSalesPrice, 
I.DocumentType, (ha.Code + ' - ' + ha.Name) AS Entity, c.ContractNumber, ha.Code AS HealthAdministratorCode, I.PatientCode, I.CareGroupId, I.InvoiceCategoryId, I.ThirdPartyId
FROM
[Billing].[Invoice] AS I WITH (NOLOCK) INNER JOIN 
[Billing].[InvoiceDetail] AS ID WITH (NOLOCK) ON I.ID = ID.InvoiceId INNER JOIN
[Billing].[ServiceOrderDetail] AS SOD WITH (NOLOCK) ON ID.ServiceOrderDetailId = SOD.ID INNER JOIN
[Billing].[ServiceOrder] AS SO WITH (NOLOCK) ON SOD.ServiceOrderId = SO.Id  INNER JOIN 
[Inventory].[InventoryProduct] AS P WITH (NOLOCK) ON SOD.ProductId = P.ID INNER JOIN 
[Contract].[HealthAdministrator] AS HA WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id LEFT OUTER JOIN 
[Contract].[CareGroup] AS CG WITH (NOLOCK) ON I.CareGroupId = CG.ID LEFT OUTER JOIN 
[Contract].[Contract] AS C WITH (NOLOCK) ON CG.ContractId = C.ID LEFT OUTER JOIN 
dbo.INPACIENT AS INP  WITH (NOLOCK) ON I.PatientCode = INP.IPCODPACI INNER JOIN 
dbo.ADINGRESO AS AD WITH (NOLOCK)  ON I.AdmissionNumber = AD.NUMINGRES
WHERE  SO.[Status] = 1 AND SOD.SettlementType != 3 AND SOD.IsDelete = 0 AND SOD.RecordType = 2 and i.[Status] = 1
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reportería que consolida todos los servicios facturados y activos, integrando facturas, detalles de facturación, órdenes de servicio, datos del paciente, entidad pagadora (EPS/aseguradora) y contrato vigente. Combina dos bloques: procedimientos y servicios clínicos (identificados por código CUPS/IPS) y productos o medicamentos de inventario, unificados mediante UNION para ofrecer una línea por ítem facturado. Permite consultar, por cada servicio o producto cobrado: número y fecha de factura, fecha de prestación del servicio, nombre del paciente, código/cédula del paciente, descripción del servicio o producto, valor total facturado, tipo de documento, entidad pagadora y número de contrato. Es la base para informes de servicios facturados, auditorías de facturación, conciliación con EPS, reportes RIPS y análisis de ingresos por contrato o entidad pagadora.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportBilledServices';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'VIEW', @level1name = N'ViewReportBilledServices';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un solo conjunto los servicios e insumos/medicamentos facturados activos, uniendo datos de la factura, paciente, entidad pagadora, contrato y catálogo (IPSService o InventoryProduct) para reportes de facturación.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La factura debe estar activa (Invoice.Status = 1).; La orden de servicio debe estar activa (ServiceOrder.Status = 1).; El detalle de la orden no debe estar marcado como eliminado (ServiceOrderDetail.IsDelete = 0).; El detalle no debe tener tipo de liquidación 3 (SettlementType != 3).; Debe existir un ingreso (ADINGRESO) coincidente con I.AdmissionNumber (INNER JOIN obligatorio).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan registros cuya factura y orden de servicio estén en estado activo (Status = 1).; Nunca se incluyen detalles eliminados ni con SettlementType = 3.; Solo se consideran detalles cuyo RecordType sea 1 (servicio) o 2 (producto).; Cada fila siempre corresponde a una admisión existente en ADINGRESO (INNER JOIN sobre AdmissionNumber).; La administradora de salud, grupo de cuidado y contrato son opcionales en la rama de servicios (LEFT JOIN), mientras que en la rama de productos la administradora es obligatoria (INNER JOIN).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Factura; Detalle de factura; Orden de servicio; Servicio IPS; CUPS; Administradora de salud; Contrato; Grupo de cuidado; Paciente; Ingreso/Admisión; Producto de inventario; Tipo de liquidación; Tipo de documento', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando SOD.RecordType = 1 se retorna el servicio facturado tomando código y descripción desde Contract.IPSService.; [RETURN_RESULT] (resultset): Cuando SOD.RecordType = 2 se retorna el ítem facturado tomando código y descripción desde Inventory.InventoryProduct.; [RETURN_RESULT] (resultset): El valor TotalSalesPrice retornado se calcula como SOD.TotalSalesPrice * SOD.InvoicedQuantity.; [RETURN_RESULT] (resultset): La columna Entity se construye concatenando HealthAdministrator.Code + '' - '' + HealthAdministrator.Name.; [RETURN_RESULT] (resultset): UNION elimina filas duplicadas exactas entre la rama de servicios (RecordType=1) y la de productos (RecordType=2).', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SOD.RecordType = 1 → Une con Contract.IPSService y Contract.CUPSEntity para tomar código y nombre del servicio de salud. else Si SOD.RecordType = 2, une con Inventory.InventoryProduct para tomar código y descripción del producto/insumo/medicamento.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Billing.Invoice; Billing.InvoiceDetail; Billing.ServiceOrderDetail; Billing.ServiceOrder; Contract.IPSService; Contract.CUPSEntity; Contract.HealthAdministrator; Contract.CareGroup; Contract.Contract; Inventory.InventoryProduct; dbo.INPACIENT; dbo.ADINGRESO', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'VIEW', @level1name=N'ViewReportBilledServices';
GO
