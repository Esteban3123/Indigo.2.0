

CREATE VIEW [Authorization].[ViewListRequests]
AS

WITH temp_TraceabilityPaperworkAlert AS (
	SELECT tpa.TraceabilityPaperworkId, 
	       1 AS Alert
	FROM [Authorization].TraceabilityPaperworkAlert tpa 
	WHERE tpa.Status = 1
	GROUP BY tpa.TraceabilityPaperworkId
),
temp_UserInt AS
(
    SELECT
        UserCode,
        MIN(IdPerson) AS IdPerson
    FROM Security.UserInt
    GROUP BY UserCode
    HAVING MIN(IdPerson) = MAX(IdPerson)
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
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce ON ap.Id = apce.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
	WHERE ap.Status = 1 AND (ap.TypePortfolio = 1 OR ap.TypePortfolio IS NULL) 
	
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
	JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
	JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip ON ap.Id = apip.AuthorizationPortfolioId
	JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioInventoryProductId
	WHERE ap.Status = 1 AND (ap.TypePortfolio = 1 OR ap.TypePortfolio IS NULL)
),
temp_TraceabilityPaperworkPostponementReasons AS (
	SELECT MAX(Id) AS Id,
	       TraceabilityPaperworkId
	FROM [Authorization].[TraceabilityPaperworkPostponementReasons]
	GROUP BY TraceabilityPaperworkId
)
-- temp_TraceabilityPaperwork eliminada: h.TraceabilityPaperworkStatus/Id/AssignUserCode/PreviousStatus
-- ya quedan sincronizados en vivo por TR_TraceabilityPaperwork_SyncBotDashboard, no hace falta releer
-- ni recalcular la trazabilidad completa (ROW_NUMBER sobre toda TraceabilityPaperwork) en cada consulta.

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
		h.PatientNameAge,
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
		h.AssignUserCode AS AssignUserCode,
		CASE
			WHEN per.Fullname IS NOT NULL THEN  CONCAT(h.AssignUserCode, ' - ', per.Fullname)
			WHEN NULLIF(h.AssignUser, '') IS NOT NULL THEN  h.AssignUser
			ELSE  h.AssignUserCode
		END AS AssignUser,
		h.RequestTime,
		h.RequestUnitTime,
		h.RequestElapsedTime,
		
		-- MEJORA: Lógica de ColorRequest simplificada con CASE en lugar de IIF anidados
		CASE
			WHEN h.TraceabilityPaperworkStatus <> 16 THEN
				[Authorization].fnGetColor(
					[Authorization].[GetRequestTime](
						CASE WHEN h.TraceabilityPaperworkStatus = 0 THEN 1 ELSE h.TraceabilityPaperworkStatus END,
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
							CASE WHEN h.TraceabilityPaperworkStatus = 0 THEN 1 ELSE h.TraceabilityPaperworkStatus END,
							ISNULL(csae.RequestUnit, csa.RequestUnit),
							ISNULL(csae.RadicatedUnit, csa.RadicatedUnit),
							ISNULL(csae.DeliveryServiceUnit, csa.DeliveryServiceUnit)
						),
						GETDATE()
					)
				)
			ELSE
				CASE 
					WHEN CAST(tpPr.PostponementDate AS DATE) > CAST(GETDATE() AS DATE) THEN 1
					WHEN CAST(tpPr.PostponementDate AS DATE) = CAST(GETDATE() AS DATE) THEN 2
					ELSE 3
				END
		END AS ColorRequest,

		h.TraceabilityPaperworkId AS TraceabilityPaperworkId,
		h.TraceabilityPaperworkStatus AS TraceabilityPaperworkStatus,
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
		h.PreviousStatus
FROM [Authorization].BotDashboardAuthorization h 
JOIN temp_CSA csa ON h.CareCenterCode = csa.CareCenterCode AND h.Type = csa.Type AND h.ServiceId = csa.ItemId 
   AND (h.ContractDescriptionId = csa.ContractDescriptionId  OR (h.ContractDescriptionId IS NULL AND csa.ContractDescriptionId IS NULL))
LEFT JOIN [Authorization].TraceabilityPaperworkEvents tpe  ON h.TraceabilityPaperworkEventsId = tpe.Id
LEFT JOIN temp_TraceabilityPaperworkPostponementReasons tpPrTemp ON tpPrTemp.TraceabilityPaperworkId = h.TraceabilityPaperworkId
LEFT JOIN [Authorization].[TraceabilityPaperworkPostponementReasons] tpPr ON tpPr.Id = tpPrTemp.Id
LEFT JOIN temp_TraceabilityPaperworkAlert TempA ON h.TraceabilityPaperworkId = TempA.TraceabilityPaperworkId
LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae ON csa.Id = csae.ConfigurationServicesAmbulatoryId  AND h.CareGroupId = csae.CareGroupId
LEFT JOIN temp_UserInt u ON u.UserCode = h.AssignUserCode
LEFT JOIN Security.PersonInt per ON per.Id = u.IdPerson
LEFT JOIN [Authorization].PostponementReasons pr ON pr.Id = tpPr.PostponementReasonsId
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida y presenta el listado completo de solicitudes de autorización pendientes o en gestión, integrando datos del paciente (cédula, nombre, dirección, teléfono, edad), del ingreso (número de admisión, folio, unidad funcional, centro de atención), del servicio solicitado (código CUPS o producto de inventario, descripción, diagnóstico CIE-10, cantidad), y del estado de trazabilidad documental (trámite, eventos, alertas, razones de aplazamiento y fechas). Combina los portafolios de autorización activos —tanto para servicios ambulatorios CUPS como para productos de inventario— con sus configuraciones de tiempos máximos (solicitud, radicación, entrega) para calcular un indicador de color de semáforo (ColorRequest) que señala si la solicitud está en tiempo, próxima a vencer o vencida. También incorpora el usuario asignado al trámite, la entidad administradora de salud, el grupo de autorización y las observaciones, sirviendo como fuente principal para los dashboards y bandejas de trabajo del módulo de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequests';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'VIEW', @level1name = N'ViewListRequests';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en una vista el listado de solicitudes de autorización del bot, enriquecido con trazabilidad, eventos, aplazamientos, alertas, configuración de tiempos (con excepciones por grupo de atención) y semáforo de color por SLA.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros activos en Authorization.AuthorizationPortfolio (Status=1) que vinculen el centro de atención y el ítem (CUPS o producto de inventario) presente en BotDashboardAuthorization, ya que el JOIN con temp_CSA es INNER.; El campo Type en BotDashboardAuthorization debe coincidir con 1 (CUPS) o 2 (producto de inventario) para emparejar contra temp_CSA.; Las funciones [Authorization].fnGetColor, [Authorization].GetRequestTime, [Authorization].GetRequestElapsedTime y [Authorization].GetRequestUnitTime deben existir y ser accesibles.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen ítems cuyo portafolio de autorización esté activo (ap.Status=1).; Las alertas (TempA.Alert) solo se consideran cuando la alerta de trazabilidad está activa (Status=1 en TraceabilityPaperworkAlert).; Se toma siempre el último motivo de aplazamiento por trámite (MAX(Id) en TraceabilityPaperworkPostponementReasons).; Por cada (EntityId, EntityName) se toma una sola trazabilidad consolidada vía MAX (Id, Status, PreviousStatus, AssignUserCode).; Los valores de trazabilidad (asignación, estado, evento, aplazamiento, alerta) priorizan los datos vigentes en TraceabilityPaperwork sobre los históricos almacenados en BotDashboardAuthorization vía ISNULL.; El identificador de negocio expuesto como Id corresponde a UniqueCode de BotDashboardAuthorization, mientras que IdIdentity es el Id físico.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios de salud; Portafolio de autorización; Centro de atención; Grupo de atención (CareGroup); Trámite de autorización (paperwork); Trazabilidad de trámite; Aplazamiento/postergación de autorización; Motivo de aplazamiento; Alerta de trámite; CUPS (procedimientos); Producto de inventario (medicamentos/insumos); Administradora de salud (EPS); Paciente; Diagnóstico; Profesional; Tiempos/SLA de solicitud, radicación y entrega; Semáforo (ColorRequest); Bot de autorizaciones', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Authorization.ViewListRequests: Devuelve una fila por solicitud del bot (BotDashboardAuthorization) emparejada con la configuración de servicios ambulatorios (CUPS o producto de inventario) cuyo portafolio esté activo (ap.Status=1).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(tp.Status,0) <> 16 → Calcula ColorRequest invocando fnGetColor con los tiempos configurados (Request/Radicated/DeliveryService) según el estado de la trazabilidad, usando la excepción por CareGroup (csae) si existe, o la configuración base (csa) en caso contrario. else Cuando el estado de trazabilidad es 16 (aplazado), el color se determina comparando PostponementDate con GETDATE(): >hoy=1, =hoy=2, <hoy=3.; si Type = 1 en temp_CSA → El ítem proviene de AuthorizationPortfolioCUPSEntity (servicio CUPS) e incluye ContractDescriptionId. else Type = 2: el ítem proviene de AuthorizationPortfolioInventoryProduct (producto de inventario) y ContractDescriptionId queda NULL.; si tpe.Status = 1 (evento creado) vs tpe.Status = 2 (autorizado) vs otros → La fecha base para el tiempo transcurrido es CreationDate si Status=1, AuthorizationDate si Status=2, y RequestDate en cualquier otro caso (o si no hay evento). else Si existe PostponementDate de tpPr, ésta tiene prioridad sobre la fecha del evento.; si Existe excepción en ConfigurationServicesAmbulatoryExceptions para el CareGroupId → Se usan los tiempos (Request/Radicated/DeliveryService y sus unidades) de la excepción. else Se usan los tiempos de la configuración base ConfigurationServicesAmbulatory.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.fnGetColor; Authorization.GetRequestTime; Authorization.GetRequestElapsedTime; Authorization.GetRequestUnitTime', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.TraceabilityPaperworkAlert; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.ConfigurationServicesAmbulatory; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.TraceabilityPaperworkPostponementReasons; Authorization.TraceabilityPaperwork; Authorization.BotDashboardAuthorization; Authorization.TraceabilityPaperworkEvents; Authorization.ConfigurationServicesAmbulatoryExceptions; Security.UserInt; Security.PersonInt; Authorization.PostponementReasons', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'VIEW', @level1name=N'ViewListRequests';
GO
