
CREATE VIEW [Authorization].[ViewAuthorizationOutsourcedServicesAdmissionInformation]
AS
SELECT	aos.Id, 
		aos.Id AuthorizationOutsourcedServicesId,
		aos.AdmissionNumber,
		CONCAT(cg.Code, ' - ', cg.Name) AS CareGroupCodeName, 
		CONCAT(ISNULL(tp.Nit, i.IPCODPACI), ' - ', ISNULL(tp.Name, p.IPNOMCOMP)) PatientCodeName
FROM [Authorization].AuthorizationOutsourcedServices aos
LEFT JOIN
(
	SELECT AuthorizationOutsourcedServicesId, MIN(CareGroupId) CareGroupId
	FROM
	(
		SELECT AuthorizationOutsourcedServicesId, CareGroupId
		FROM [Authorization].AuthorizationOutsourcedServicesPharmaceuticalDispensingDetail
		UNION ALL
		SELECT AuthorizationOutsourcedServicesId, CareGroupId
		FROM [Authorization].AuthorizationOutsourcedServicesServiceOrderDetail
	) qd
	GROUP BY AuthorizationOutsourcedServicesId
) aosd ON aos.Id = aosd.AuthorizationOutsourcedServicesId
LEFT JOIN .ADINGRESO i WITH (NOLOCK) ON i.NUMINGRES = aos.AdmissionNumber
LEFT JOIN .INPACIENT p WITH (NOLOCK) ON p.IPCODPACI = i.IPCODPACI
LEFT JOIN Contract.CareGroup cg WITH (NOLOCK) ON cg.Id = aosd.CareGroupId
LEFT JOIN Common.ThirdParty AS tp WITH (NOLOCK) ON tp.Id = aos.ThirdPartyId