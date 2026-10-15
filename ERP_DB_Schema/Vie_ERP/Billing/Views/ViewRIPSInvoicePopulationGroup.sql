


CREATE VIEW [Billing].[ViewRIPSInvoicePopulationGroup]
AS
SELECT
	iv.Id AS InvoiceId
   ,iv.InvoiceNumber
   ,iv.AdmissionNumber
   ,iv.DocumentType
   ,iv.HealthAdministratorId
   ,CONCAT(ha.Code, ' - ', ha.[Name]) AS HealthAdministratorCodeName
   ,CONCAT(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName
   ,iv.CareGroupId
   ,CONCAT(cg.Code, ' - ', cg.[Name]) AS CareGroupCodeName
   ,iv.TotalInvoice
   ,iv.InvoiceDate
   ,iv.ThirdPartySalesValue
   ,iv.TotalPatientSalesPrice
   ,iv.[Status] AS [State]
   ,IIF(iv.[Status] = 1, 'FACTURADO', 'ANULADO')
	AS StateName
   ,rc.RadicatedConsecutive
   ,rc.Id AS InvoiceRadicateId
   ,iv.CapitationInitialDate
   ,iv.CapitationEndDate
   ,iv.ThirdPartyId
   ,iv.InvoiceCategoryId
   ,ing.CODCENATE
   ,ISNULL(iv.ContractId, cg.ContractId) AS ContractId
   ,ing.ITIPORIES
   ,ing.ICAUSAING
   ,PSP.IDADPOBESPE
FROM [Billing].[Invoice] iv WITH (NOLOCK)
LEFT JOIN [Contract].[HealthAdministrator] ha WITH (NOLOCK)
	ON iv.HealthAdministratorId = ha.Id
LEFT JOIN [Common].[ThirdParty] tp WITH (NOLOCK)
	ON iv.ThirdPartyId = tp.Id
INNER JOIN [Contract].[CareGroup] cg WITH (NOLOCK)
	ON iv.CareGroupId = cg.Id
INNER JOIN .[ADINGRESO] ing WITH (NOLOCK)
	ON iv.AdmissionNumber = ing.NUMINGRES
LEFT OUTER JOIN dbo.INPACIENT AS INP WITH (NOLOCK)
	ON iv.PatientCode = INP.IPCODPACI
LEFT OUTER JOIN dbo.ADCENATEN AS CAI WITH (NOLOCK)
	ON ing.CODCENATE = CAI.CODCENATE
LEFT JOIN [Portfolio].[RadicateInvoiceD] rd
	ON iv.InvoiceNumber = rd.InvoiceNumber
LEFT JOIN [Portfolio].[RadicateInvoiceC] rc
	ON rd.RadicateInvoiceCId = rc.Id
LEFT OUTER JOIN Contract.Contract AS C WITH (NOLOCK)
	ON ISNULL(iv.ContractId, cg.ContractId) = C.Id
LEFT OUTER JOIN Billing.InvoiceCategories AS IC WITH (NOLOCK)
	ON iv.InvoiceCategoryId = IC.Id
LEFT OUTER JOIN dbo.ADPOBESPEPAC AS PSP WITH (NOLOCK)
	ON INP.IPCODPACI = PSP.IPCODPACI AND PSP.ESTADO = 1
WHERE iv.[Status] = 1