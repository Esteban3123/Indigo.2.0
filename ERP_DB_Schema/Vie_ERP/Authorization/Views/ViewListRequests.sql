

CREATE VIEW [Authorization].[ViewListRequests]
AS

WITH temp_TraceabilityPaperworkAlert AS (
	SELECT tpa.TraceabilityPaperworkId, 
	       1 AS Alert
	FROM [Authorization].TraceabilityPaperworkAlert tpa 
	WHERE tpa.Status = 1
	GROUP BY tpa.TraceabilityPaperworkId
),
temp_CSA AS (
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			1 AS Type, 
			apce.CUPSEntityId AS ItemId,
			apce.ContractDescriptionId,
			csa.Request, 
			csa.RequestUnit, 
			csa.Radicated, 
			csa.RadicatedUnit,
			csa.DeliveryService, 
			csa.DeliveryServiceUnit,
			apce.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc 
	JOIN [Authorization].AuthorizationPortfolio ap  
	    ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce  
	    ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  
	    ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1
	
	UNION ALL
	
	SELECT	csa.Id,
			apcc.CareCenterCode, 		
			2 AS Type, 
			apip.InventoryProductId AS ItemId,
			NULL AS ContractDescriptionId,
			csa.Request, 
			csa.RequestUnit, 
			csa.Radicated, 
			csa.RadicatedUnit,
			csa.DeliveryService, 
			csa.DeliveryServiceUnit,
			apip.AuthorizationGroupId
	FROM [Authorization].AuthorizationPortfolioCareCenter apcc 
	JOIN [Authorization].AuthorizationPortfolio ap  
	    ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip  
	    ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa  
	    ON apip.Id = csa.AuthorizationPortfolioInventoryProductId
	WHERE ap.Status = 1
),
temp_TraceabilityPaperworkPostponementReasons AS (
	SELECT MAX(Id) AS Id, 
	       TraceabilityPaperworkId
	FROM [Authorization].[TraceabilityPaperworkPostponementReasons] 
	GROUP BY TraceabilityPaperworkId
),

temp_TraceabilityPaperwork AS (
	SELECT EntityId,
	       EntityName,
	       Status,
	       Id,
	       PreviousStatus,
	       AssignUserCode
	FROM (
		SELECT tp.EntityId,
		       tp.EntityName,
		       tp.Status,
		       tp.Id,
		       tp.PreviousStatus,
		       tp.AssignUserCode,
		       ROW_NUMBER() OVER (PARTITION BY tp.EntityId, tp.EntityName 
		                          ORDER BY tp.Id DESC) AS rn
		FROM [Authorization].TraceabilityPaperwork tp 
	) t
	WHERE rn = 1
)

SELECT	h.Id AS IdIdentity,
		h.UniqueCode AS Id, 
		h.EntityName, 
		h.EntityId,
		h.CareCenterCode,
		h.CareCenterCodeName,
		h.FunctionalUnitCode,
		h.FunctionalUnitName, 
		h.FunctionalUnitCodeName,
		h.AdmissionNumber,
		h.Folio,
		h.TypeClinicalHistory,
		h.CareGroupId,
		h.CareGroupCode,
		h.CareGroupName,
		h.CareGroupCodeName,
		h.HealthAdministratorId,
		h.HealthAdministratorCode,
		h.HealthAdministratorName, 
		h.HealthAdministratorCodeName,
		h.PatientCode, 
		h.PatientName,
		h.PatientAddress,
		h.PatientPhone,
		ISNULL(h.PatientAge, '') AS PatientAge,	
		h.RequestDate,
		h.ProfessionalCode,
		h.Quantity,
		h.Type,
		h.ServiceId, 
		h.ServiceCode,
		h.ItemCodeOriginal,
		h.ServiceDescription, 
		h.ContractDescriptionId,
		h.ContractDescriptionCodeName,
		h.IsCovered,
		h.Contracted,
		h.Quoted,
		ISNULL(tp.AssignUserCode, h.AssignUserCode) AS AssignUserCode,
		ISNULL(CONCAT(tp.AssignUserCode, ' - ', per.Fullname), h.AssignUser) AS AssignUser,
		h.RequestTime,
		h.RequestUnitTime,
		h.RequestElapsedTime,
		
		-- MEJORA: Lógica de ColorRequest simplificada con CASE en lugar de IIF anidados
		CASE 
			WHEN ISNULL(tp.Status, 0) <> 16 THEN
				[Authorization].fnGetColor(
					[Authorization].[GetRequestTime](
						ISNULL(tp.Status, 1), 
						ISNULL(csae.Request, csa.Request), 
						ISNULL(csae.Radicated, csa.Radicated), 
						ISNULL(csae.DeliveryService, csa.DeliveryService)
					),
					[Authorization].[GetRequestElapsedTime](
						ISNULL(tpPr.PostponementDate, 
						       CASE 
						           WHEN tpe.Id IS NULL THEN h.RequestDate
						           WHEN tpe.Status = 1 THEN tpe.CreationDate
						           WHEN tpe.Status = 2 THEN tpe.AuthorizationDate
						           ELSE h.RequestDate
						       END
						), 
						[Authorization].[GetRequestUnitTime](
							ISNULL(tp.Status, 1), 
							ISNULL(csae.RequestUnit, csa.RequestUnit), 
							ISNULL(csae.RadicatedUnit, csa.RadicatedUnit), 
							ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit)
						)
					)
				)
			ELSE
				CASE 
					WHEN CAST(tpPr.PostponementDate AS DATE) > CAST(GETDATE() AS DATE) THEN 1
					WHEN CAST(tpPr.PostponementDate AS DATE) = CAST(GETDATE() AS DATE) THEN 2
					ELSE 3
				END
		END AS ColorRequest,

		tp.Id AS TraceabilityPaperworkId,
		ISNULL(tp.Status, h.TraceabilityPaperworkStatus) AS TraceabilityPaperworkStatus,
		tpe.Id AS TraceabilityPaperworkEventsId,
		ISNULL(tpe.Status, h.TraceabilityPaperworkEventsStatus) AS TraceabilityPaperworkEventsStatus,
		h.AuthorizationSourceId,
		h.IsManual,
		h.Observations,
		ISNULL(TempA.Alert, h.Alert) AS Alert,
		h.PatientThirdPartyId,
		h.AuthorizationGroupId,
		h.AuthorizationGroupCodeName,
		h.ProfessionalCodeName,
		h.CareCenterTargetCodeName,
		h.FunctionalUnitTargetCodeName,
		h.DiagnosticCode,
		h.DiagnosticDescription,
		-- CORRECCIÓN: Cambio de tppr a tpPrTemp (alias correcto)
		ISNULL(tpPrTemp.Id, h.TraceabilityPaperworkPostponementReasonsId) AS TraceabilityPaperworkPostponementReasonsId,
		ISNULL(tpPr.PostponementReasonsId, h.PostponementReasonsId) AS PostponementReasonsId,
		ISNULL(tpPr.PostponementDate, h.PostponementDate) AS PostponementDate,
		ISNULL(tpPr.CreationDate, h.PostponementCreationDate) AS PostponementCreationDate,
		ISNULL(tpPr.CreationUser, h.PostponementCreationUser) AS PostponementCreationUser,
		ISNULL(CONCAT(pr.Code, ' - ', pr.Name), h.PostponementCodeName) AS PostponementCodeName,
		tp.PreviousStatus
FROM [Authorization].BotDashboardAuthorization h 
JOIN temp_CSA csa  ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type  AND h.ServiceId = csa.ItemId AND ISNULL(h.ContractDescriptionId, 0) = ISNULL(csa.ContractDescriptionId, 0)
LEFT JOIN temp_TraceabilityPaperwork tp  ON h.EntityId = tp.EntityId AND h.EntityName = tp.EntityName
LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe  ON h.TraceabilityPaperworkEventsId = tpe.Id
LEFT JOIN temp_TraceabilityPaperworkPostponementReasons tpPrTemp ON tpPrTemp.TraceabilityPaperworkId = tp.Id
LEFT JOIN [Authorization].[TraceabilityPaperworkPostponementReasons] tpPr ON tpPr.Id = tpPrTemp.Id
LEFT JOIN temp_TraceabilityPaperworkAlert TempA ON tp.Id = TempA.TraceabilityPaperworkId
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae ON csa.Id = csae.ConfigurationServicesAmbulatoryId  AND h.CareGroupId = csae.CareGroupId
LEFT JOIN Security.[UserInt] u ON u.UserCode = tp.AssignUserCode
LEFT JOIN Security.PersonInt per ON per.Id = u.IdPerson
LEFT JOIN [Authorization].PostponementReasons pr ON pr.Id = tpPr.PostponementReasonsId