


CREATE VIEW [Glosas].[ViewCoordinationGlosaExport] 
AS
WITH RE_Radicate AS
(
	SELECT rid.InvoiceNumber, MAX(ri.Id) Id, MAX(rid.RadicatedNumber) RadicatedNumber, MAX(rid.RadicatedDate) RadicatedDate
	FROM Portfolio.RadicateInvoiceC ri WITH (NOLOCK)
	JOIN Portfolio.RadicateInvoiceD rid WITH (NOLOCK) ON ri.Id = rid.RadicateInvoiceCId
	WHERE ri.State = 2 AND rid.State = 2 
	GROUP BY rid.InvoiceNumber
)

SELECT 
	gorc.Id AS GlosaObjectionsReceptionCId, gpg.InvoiceNumber, gmg.Id AS MovementId, gpg.InvoiceDate, ri.RadicatedNumber, 
	ri.RadicatedDate, gorc.RadicatedConsecutive AS ObjectionRadicatedConsecutive, gorc.RadicatedDate AS ObjectionRadicatedDate, 
	gpg.PatientName, gpg.IngressNumber, gpg.IngressDate, gid.ServiceCode,
	gid.ServiceName, gid.ServiceDate, gidQX.ServiceCode AS ServiceCodeQX, gidQX.ServiceName AS ServiceNameQX,
	gid.CostCenterCode, gid.CostCenterName, gmg.CodeGlosa, cg.NameSpecific, gmg.RationaleGlosa, 
	gmg.ValueGlosado, gmg.ValuePayments, r.Name AS ResponsableName
FROM Glosas.GlosaObjectionsReceptionC gorc WITH (NOLOCK)
JOIN Glosas.GlosaObjectionsReceptionD gord WITH (NOLOCK) ON gorc.Id = gord.GlosaObjectionsReceptionCId
JOIN Glosas.GlosaPortfolioGlosada gpg WITH (NOLOCK) ON gpg.Id = gord.PortfolioGlosaId
JOIN Glosas.GlosaMovementGlosa gmg WITH (NOLOCK) ON gpg.InvoiceNumber = gmg.InvoiceNumber 
JOIN Glosas.GlosaInvoiceDetail gid WITH (NOLOCK) ON gid.Id = gmg.InvoiceDetailId
JOIN RE_Radicate ri ON ri.InvoiceNumber = gpg.InvoiceNumber
JOIN Common.ConceptGlosas cg WITH (NOLOCK) ON cg.Id = gmg.CodeGlosaId
JOIN Glosas.Responsible r WITH (NOLOCK) ON r.Id = gmg.ResponsibleId
LEFT JOIN Glosas.GlosaInvoiceDetailQX gidQX  WITH (NOLOCK) ON gid.Id = gidQX.InvoiceDetailId AND gmg.InvoiceDetailIdQX = gidQX.Id
WHERE
(
	(gpg.State IN ('2', '3') AND gord.DocumentType = '1') 
	OR
	(gpg.State IN ('5', '6') AND gord.DocumentType = '2')
)