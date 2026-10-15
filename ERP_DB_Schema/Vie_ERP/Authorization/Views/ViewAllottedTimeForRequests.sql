-- Ordenes Asignadas sin gestionar
CREATE VIEW [Authorization].[ViewAllottedTimeForRequests]
AS
SELECT	tp.AssignUserCode,
		SUM
		(
			ISNULL(csae.Assignment, csa.Assignment) * CASE ISNULL(csae.AssignmentUnit, csa.AssignmentUnit)
																	WHEN 1 THEN 1
																	WHEN 2 THEN 60
																	WHEN 3 THEN 1440
																END
		) AssignedMinutes
FROM [Authorization].TraceabilityPaperwork tp
JOIN
(
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 Type, 
			apce.CUPSEntityId ItemId,
			apce.ContractDescriptionId ContractDescriptionId,
			csa.Assignment, csa.AssignmentUnit
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
UNION ALL
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 Type, 
			apip.InventoryProductId ItemId,
			NULL ContractDescriptionId,
			csa.Assignment, csa.AssignmentUnit
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
) csa ON tp.CareCenterCode = csa.CareCenterCode AND tp.Type = csa.Type AND tp.ServiceId = csa.ItemId AND ISNULL(tp.ContractDescriptionId, 0) = ISNULL(csa.ContractDescriptionId, 0)
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND tp.CareGroupId = csae.CareGroupId
WHERE tp.Status = 1
	AND ISNULL(csae.SusceptibleAuthorization, 1) = 1
	AND tp.AssignUserCode IS NOT NULL
GROUP BY tp.AssignUserCode