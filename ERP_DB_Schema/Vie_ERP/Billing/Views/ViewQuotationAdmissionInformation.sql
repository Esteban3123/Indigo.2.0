CREATE VIEW [Billing].[ViewQuotationAdmissionInformation]
AS
SELECT	q.Id,
		q.Id QuotationId, 
		CONCAT(cg.Code, ' - ', cg.Name) AS CareGroupCodeName, 
		ISNULL(tp.Nit, i.IPCODPACI) + ' - ' + ISNULL(tp.Name, p.IPNOMCOMP) PatientCodeName
FROM Billing.Quotation q
LEFT JOIN
(
	SELECT QuotationId, MIN(CareGroupId) CareGroupId
	FROM
	(
		SELECT QuotationId, CareGroupId
		FROM Billing.QuotationPharmaceuticalDispensingDetail
		UNION ALL
		SELECT QuotationId, CareGroupId
		FROM Billing.QuotationServiceOrderDetail
	) qd
	GROUP BY QuotationId
) qd ON q.Id = qd.QuotationId
LEFT JOIN .ADINGRESO i ON q.AdmissionNumber = i.NUMINGRES
LEFT JOIN .INPACIENT p ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg ON cg.Id = ISNULL(i.GENCAREGROUP, qd.CareGroupId)
LEFT JOIN Common.ThirdParty tp ON q.ThirdPartyId = tp.Id