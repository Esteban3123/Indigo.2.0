



CREATE VIEW [Billing].[ViewReportQuotation]
AS
SELECT        
	q.Id, 
	q.Code, 
	q.DocumentDate, 
	q.QuotationType, 

		CASE q.QuotationType 
		WHEN 1 THEN 'Intrahospitalario' 
		ELSE 'Ambulatoria' 
		END AS QuotationTypeName,

	q.AdmissionNumber, 
	q.Description, 
	q.OperatingUnitId, 
	q.Status, 
                         
		CASE q.Status WHEN 1 THEN 'Registrado' 
		WHEN 2 THEN 'Confirmado' 
		ELSE 'Anulado' 
		END AS StatusName, 
	
	q.StatusInAuthorization, 
	q.StatusInBilling, 
	q.ThirdPartyId, 
	tp.Nit, 
	tp.Nit + ' - ' + tp.Name AS ThirdPartyDescription,
	ISNULL(tp.Nit, i.IPCODPACI) PatientCode,
	ISNULL(tp.Name, p.IPNOMCOMP) PatientName,
	ISNULL(tp.Nit, i.IPCODPACI) + ' - ' + ISNULL(tp.Name, p.IPNOMCOMP) PatientDescription,
	ISNULL(cg.Code + ' - ' + cg.Name, '') CareGroupCodeName,
	q.CreationUser
FROM    Billing.Quotation AS q WITH (nolock) 
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