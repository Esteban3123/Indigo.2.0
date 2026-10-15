

CREATE VIEW [Billing].[ViewReportRadicateInvoice]
AS
 
WITH CTE_Officialcurrency AS (
	SELECT TOP 1 cs.OfficialCurrencyId 
	FROM GeneralLedger.CompanySettings cs
),
Cte_TRM AS (
	SELECT * 
	FROM Billing.RevenueControlDetail 
	WHERE IsMasterAccount = 2 AND RevenueControlDetailMasterId IS NOT NULL
)

SELECT 
	CONCAT(I.Id, '-', I.DocumentType) AS Id,  
	I.DocumentType, 
	I.InvoiceDate, 
	I.InvoiceNumber, 
	HA.Id AS HealthAdministratorId, 
	I.PatientCode, 
	pcp.FirstName + ' ' + pcp.SecondName + ' ' + pcp.FirstLastName + ' ' + pcp.SecondLastName as PatientFullName, 
	CG.Id AS CareGroupId, 
	IC.Id AS InvoiceCategoryId, 
	IC.Name AS InvoiceCategory,
	I.TotalInvoice AS TotalInvoice, 
	I.ThirdPartyDiscountValue AS Discount,
	TP.Id AS ThirdPartyId, 
	TP.Nit + '-' + TP.DigitVerification as Nit, 
	TP.Name AS ThirdParty, I.AdmissionNumber, 
	I.InvoicedUser, 
	I.Status,
	PRC.Id AS RadicateInvoiceIdC, 
	PU.Fullname As InvoicedUserName, 
	CENTRO.NOMCENATE,
	dips.BranchOfficeId,
	cu.id CurrencyId,
	cu.Abbreviation CurrencyAbbreviation
FROM Billing.Invoice AS I WITH (NOLOCK) 
INNER JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON I.ThirdPartyId = TP.Id
INNER JOIN Common.Person as cp  WITH (NOLOCK) on cp.Id = tp.PersonId 
INNER JOIN CTE_Officialcurrency cs WITH(NOLOCK) on 1=1
INNER JOIN Common.Currency as cu WITH (NOLOCK) on cu.id = ISNULL(i.CurrencyId,CS.OfficialCurrencyId)
LEFT JOIN Common.Person as pcp  WITH (NOLOCK) on pcp.IdentificationNumber = I.PatientCode 
LEFT JOIN Contract.CareGroup AS CG  WITH (NOLOCK) ON I.CareGroupId = CG.Id 
LEFT JOIN Contract.HealthAdministrator AS HA  WITH (NOLOCK) ON I.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.Contract AS C  WITH (NOLOCK) ON cg.ContractId = C.Id 
LEFT JOIN Billing.InvoiceCategories as IC WITH (NOLOCK)  ON I.InvoiceCategoryId = IC.Id						  
LEFT JOIN dbo.INPACIENT AS INP  WITH (NOLOCK) ON I.PatientCode = INP.IPCODPACI 
LEFT JOIN dbo.ADINGRESO AS AD WITH (NOLOCK)  ON AD.NUMINGRES = I.AdmissionNumber
LEFT JOIN Security.[UserInt] AS U ON U.UserCode = I.InvoicedUser 
LEFT JOIN Security.PersonInt AS PU ON U.IdPerson = PU.Id 
LEFT JOIN DBO.ADNIVELES AS AN WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO 
LEFT JOIN Portfolio.RadicateInvoiceD AS PRD WITH (NOLOCK) ON PRD.InvoiceNumber = I.InvoiceNumber AND PRD.state <> 4 
LEFT JOIN Portfolio.RadicateInvoiceC AS PRC WITH (NOLOCK) ON PRC.Id = PRD.RadicateInvoiceCId
LEFT JOIN Inventory.DocumentInvoiceProductSales dips WITH (NOLOCK) ON I.DocumentType = 7 AND I.Id = dips.InvoiceId
LEFT JOIN dbo.ADCENATEN AS CENTRO WITH (NOLOCK)  ON AD.CODCENATE = CENTRO.CODCENATE
 
UNION ALL
 
SELECT 
	CONCAT(rcd.Id, '-', 8) AS Id,
	8 DocumentType, 
	COALESCE(cte.CreationDate,i.InvoiceDate, rcd.CreationDate)  InvoiceDate,
	NULL InvoiceNumber, 
	HA.Id AS HealthAdministratorId, 
	rc.PatientCode, 
	pcp.FirstName + ' ' + pcp.SecondName + ' ' + pcp.FirstLastName + ' ' + pcp.SecondLastName as PatientFullName, 
	CG.Id AS CareGroupId, 
	NULL InvoiceCategoryId, 
	NULL InvoiceCategory,
	rcd.TotalFolio TotalInvoice, 
	rcd.PatientDiscount AS Discount,
	TP.Id AS ThirdPartyId, 
	TP.Nit + '-' + TP.DigitVerification as Nit, 
	TP.Name AS ThirdParty, 
	rc.AdmissionNumber, 
	i.InvoicedUser InvoicedUser, 
	rcd.Status,
	NULL RadicateInvoiceIdC, 
	pu.Fullname InvoicedUserName, 
	CENTRO.NOMCENATE,
	NULL BranchOfficeId,
	cu.id CurrencyId,
	cu.Abbreviation CurrencyAbbreviation
FROM Billing.RevenueControlDetail rcd
JOIN Billing.RevenueControl rc ON rc.Id = rcd.RevenueControlId
INNER JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON rcd.ThirdPartyId = TP.Id
INNER JOIN Common.Person as cp  WITH (NOLOCK) on cp.Id = tp.PersonId 
INNER JOIN CTE_Officialcurrency cs WITH(NOLOCK) on 1=1
INNER JOIN Common.Currency as cu WITH (NOLOCK) on cu.id = CS.OfficialCurrencyId
LEFT JOIN Common.Person as pcp  WITH (NOLOCK) on pcp.IdentificationNumber = rc.PatientCode
LEFT JOIN Contract.CareGroup AS CG  WITH (NOLOCK) ON rcd.CareGroupId = CG.Id 
LEFT JOIN Contract.HealthAdministrator AS HA  WITH (NOLOCK) ON rcd.HealthAdministratorId = HA.Id 
LEFT JOIN Contract.Contract AS C  WITH (NOLOCK) ON cg.ContractId = C.Id 
LEFT JOIN dbo.INPACIENT AS INP  WITH (NOLOCK) ON rc.PatientCode = INP.IPCODPACI 
LEFT JOIN dbo.ADINGRESO AS AD WITH (NOLOCK)  ON AD.NUMINGRES = rc.AdmissionNumber
LEFT JOIN DBO.ADNIVELES AS AN WITH (NOLOCK) ON INP.NIVCODIGO = AN.NIVCODIGO 
LEFT JOIN dbo.ADCENATEN AS CENTRO WITH (NOLOCK)  ON AD.CODCENATE = CENTRO.CODCENATE
LEFT JOIN Cte_TRM cte ON cte.RevenueControlDetailMasterId = rcd.Id
LEFT JOIN Billing.Invoice i ON i.RevenueControlDetailId = rcd.Id				  
LEFT JOIN Security.[UserInt] AS U ON U.UserCode = i.InvoicedUser
LEFT JOIN Security.PersonInt AS PU ON U.IdPerson = PU.Id 
WHERE (rcd.IsMasterAccount IN (2,4) AND rcd.RevenueControlDetailMasterId IS NOT NULL) OR rcd.IsMasterAccount IN (1,3)