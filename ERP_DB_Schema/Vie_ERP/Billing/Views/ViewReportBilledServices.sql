







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