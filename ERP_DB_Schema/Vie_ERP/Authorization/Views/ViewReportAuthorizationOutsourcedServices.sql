

CREATE VIEW [Authorization].[ViewReportAuthorizationOutsourcedServices]
AS
SELECT
	aos.Id, 
	aos.Code, 
	aos.DocumentDate, 
	IIF(aos.Type = 1, 'Intrahospitalario', 'Ambulatoria') AS TypeName, 
	aos.AdmissionNumber, 
	ISNULL(cg.Code + ' - ' + cg.Name, '') AS CareGroupCodeName, 
	ISNULL(fu.Code + ' - ' + fu.Name, '') AS FunctionalUnitCodeName, 
	tp.Nit ThirdPartyNit, 
	tp.Name AS ThirdPartyName, 
	ISNULL(tp.Nit, i.IPCODPACI) PatientCode, 
	ISNULL(tp.Name, p.IPNOMCOMP) PatientName, 
	aos.Description, 
	aos.OperatingUnitId, 
	CASE aos.Status 
		WHEN 1 THEN 'Registrado' 
		WHEN 2 THEN 'Confirmado' 
		ELSE 'Anulado' 
	END AS StatusName, 
	aos.StatusInAuthorization, 
	aos.StatusInBilling, 
	aos.CreationUser 
FROM [Authorization].AuthorizationOutsourcedServices aos WITH (NOLOCK) 
LEFT JOIN
(
	SELECT AuthorizationOutsourcedServicesId, MIN(CareGroupId) CareGroupId, MIN(FunctionalUnitId) FunctionalUnitId
	FROM
	(
		SELECT AuthorizationOutsourcedServicesId, CareGroupId, FunctionalUnitId
		FROM [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
		UNION ALL
		SELECT AuthorizationOutsourcedServicesId, CareGroupId, PerformsFunctionalUnitId
		FROM [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail
	) qd
	GROUP BY AuthorizationOutsourcedServicesId
) aosd ON aos.Id = aosd.AuthorizationOutsourcedServicesId
LEFT JOIN .ADINGRESO i WITH (NOLOCK) ON i.NUMINGRES = aos.AdmissionNumber
LEFT JOIN .INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = aosd.CareGroupId
LEFT JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fu.Id = aosd.FunctionalUnitId
LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = aos.ThirdPartyId