
CREATE VIEW [Billing].[ViewRIPSInvoice]
AS
SELECT  distinct iv.Id AS InvoiceId, 
		iv.InvoiceNumber,
		iv.AdmissionNumber, 
		iv.DocumentType,
		iv.HealthAdministratorId,
		concat(ha.Code, ' - ', ha.[Name]) AS HealthAdministratorCodeName,
		concat(tp.Nit, ' - ', tp.[Name]) AS ThirdPartyNitName,
		iv.CareGroupId,
		concat(cg.Code, ' - ', cg.[Name]) AS CareGroupCodeName,
		iv.TotalInvoice, CAST(iv.InvoiceDate AS DATE) InvoiceDate,
		iv.ThirdPartySalesValue,
		iv.TotalPatientSalesPrice,
		iv.[Status] AS [State], 
		iif(iv.[Status] = 1, 'FACTURADO', 'ANULADO') AS StateName, 
		rc.RadicatedConsecutive,
		rc.Id AS InvoiceRadicateId, 
		iv.CapitationInitialDate,
		iv.CapitationEndDate,
		iv.ThirdPartyId,
		iv.InvoiceCategoryId,
		ing.CODCENATE, 
		ISNULL(iv.ContractId, cg.ContractId) AS ContractId ,
		ing.ITIPORIES,
		ing.ICAUSAING,
		COALESCE(Diag.CODDIAGNO,ing.CODDIAEGR,iv.OutputDiagnosis,'Z000') AS MainDiagnosticCode 
FROM	[Billing].[Invoice] iv WITH (nolock) LEFT JOIN
        [Contract].[HealthAdministrator] ha WITH (nolock) ON iv.HealthAdministratorId = ha.Id LEFT JOIN
        [Common].[ThirdParty] tp WITH (nolock) ON iv.ThirdPartyId = tp.Id INNER JOIN
        [Contract].[CareGroup] cg WITH (nolock) ON iv.CareGroupId = cg.Id INNER JOIN
        .[ADINGRESO] ing WITH (nolock) ON iv.AdmissionNumber = ing.NUMINGRES LEFT OUTER JOIN
		dbo.INPACIENT AS INP WITH (NOLOCK) ON iv.PatientCode = INP.IPCODPACI LEFT OUTER JOIN
        dbo.ADCENATEN AS CAI WITH (NOLOCK) ON ing.CODCENATE = CAI.CODCENATE LEFT JOIN
        [Portfolio].[RadicateInvoiceD] rd ON iv.InvoiceNumber = rd.InvoiceNumber LEFT JOIN
        [Portfolio].[RadicateInvoiceC] rc ON rd.RadicateInvoiceCId = rc.Id LEFT OUTER JOIN
        Contract.Contract AS C WITH (NOLOCK) ON ISNULL(iv.ContractId, cg.ContractId) = C.Id LEFT OUTER JOIN
        Billing.InvoiceCategories AS IC WITH (NOLOCK) ON iv.InvoiceCategoryId = IC.Id LEFT OUTER JOIN
		dbo.INDIAGNOP diag WITH (NOLOCK) ON ing.NUMINGRES =diag.NUMINGRES AND ing.CODDIAEGR = diag.CODDIAGNO AND diag.CODDIAPRI = 1
		WHERE  iv.[Status] = 1