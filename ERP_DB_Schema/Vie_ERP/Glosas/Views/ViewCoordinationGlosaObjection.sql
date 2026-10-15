


CREATE VIEW [Glosas].[ViewCoordinationGlosaObjection]
AS
SELECT	gord.Id,
		gord.GlosaObjectionsReceptionCId,
		gor.CustomerId,
		gord.DocumentType,
		gord.PortfolioGlosaId,
		gord.InvoiceNumber,
		gpg.IngressNumber,
		gpg.PatientName,		
		gord.State,
		gpg.State StatePortfolio,
		IIF(gpg.State IN (3, 6), 1, 0) StateRecord,
		ISNULL(gpg.ValueGlosado, 0) ValueGlosado,
		ISNULL(gpg.ValueReiterated, 0) ValueReiterated,
		ISNULL(gpg.ValueAcceptedFirstInstance, 0) ValueAcceptedFirstInstance,
		ISNULL(gpg.ValueAcceptedSecondInstance, 0) ValueAcceptedSecondInstance,
		gpg.InvoiceValueEntity,
		gpg.ValueAcceptedIPSconciliation,
		gpg.InvoiceValuePacient,
		gpg.ImportunityCauseId,
		CONCAT(ic.Code, ' - ', ic.Name) ImportunityCauseCodeName
FROM Glosas.GlosaObjectionsReceptionC gor WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON gor.Id = gord.GlosaObjectionsReceptionCId
JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON gord.PortfolioGlosaId = gpg.Id
LEFT JOIN Glosas.ImportunityCauses ic WITH (NOLOCK) ON gpg.ImportunityCauseId = ic.Id
WHERE
(
	(gpg.State IN ('2', '3') AND gord.DocumentType = '1') 
	OR
	(gpg.State IN ('5', '6') AND gord.DocumentType = '2')
)