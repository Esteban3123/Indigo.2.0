
-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte detallado de autorizaciones (solicitudes)
-- =============================================
CREATE PROCEDURE [Authorization].[SP_ReportRequestDetailed]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATETIME,
			@DateEnd DATETIME,
			@GroupBy TINYINT,
			@CurrentDate DATETIME

	SET @CurrentDate = [Common].[GETDATE]()

	CREATE TABLE #CareCenters (Code VARCHAR(20) PRIMARY KEY)
	CREATE TABLE #FunctionalUnits (Code VARCHAR(20) PRIMARY KEY)
	CREATE TABLE #CareGroups (Id INT PRIMARY KEY)
	CREATE TABLE #HealthAdministrator (Id INT PRIMARY KEY)
	CREATE TABLE #ThirdParties (Nit VARCHAR(20) PRIMARY KEY)
	CREATE TABLE #Status (Id INT PRIMARY KEY)
	CREATE TABLE #Users (Code VARCHAR(20) PRIMARY KEY)

	BEGIN TRY
		
		/*************************************** CRITERIOS ***************************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','datetime'),
				@DateEnd = t.x.value('DateEnd[1]','datetime'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint')
		FROM @xmlCriterias.nodes('/Data') t(x)

		INSERT INTO #CareCenters (Code)
		SELECT CAST(Data AS VARCHAR(20)) 
		FROM dbo.Split(
			(SELECT t.x.value('CareCenters[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('CareCenters[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #FunctionalUnits (Code)
		SELECT CAST(Data AS VARCHAR(20)) 
		FROM dbo.Split(
			(SELECT t.x.value('FunctionalUnits[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('FunctionalUnits[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #CareGroups (Id)
		SELECT CAST(Data AS INT) 
		FROM dbo.Split(
			(SELECT t.x.value('CareGroups[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('CareGroups[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #HealthAdministrator (Id)
		SELECT CAST(Data AS INT) 
		FROM dbo.Split(
			(SELECT t.x.value('HealthAdministrators[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('HealthAdministrators[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #ThirdParties (Nit)
		SELECT CAST(Data AS VARCHAR(20)) 
		FROM dbo.Split(
			(SELECT t.x.value('ThirdParties[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('ThirdParties[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #Status (Id)
		SELECT CAST(Data AS INT) 
		FROM dbo.Split(
			(SELECT t.x.value('Status[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('Status[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		INSERT INTO #Users (Code)
		SELECT CAST(Data AS VARCHAR(20)) 
		FROM dbo.Split(
			(SELECT t.x.value('Users[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)), 
			','
		)
		WHERE (SELECT t.x.value('Users[1]','varchar(max)') FROM @xmlCriterias.nodes('/Data') t(x)) <> ''

		DECLARE @HasCareCenterFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #CareCenters) THEN 1 ELSE 0 END
		DECLARE @HasFunctionalUnitFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #FunctionalUnits) THEN 1 ELSE 0 END
		DECLARE @HasCareGroupFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #CareGroups) THEN 1 ELSE 0 END
		DECLARE @HasHealthAdminFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #HealthAdministrator) THEN 1 ELSE 0 END
		DECLARE @HasThirdPartyFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #ThirdParties) THEN 1 ELSE 0 END
		DECLARE @HasStatusFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #Status) THEN 1 ELSE 0 END
		DECLARE @HasUserFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #Users) THEN 1 ELSE 0 END

		/********************************** OBTENCION DE DATOS **********************************/

		CREATE TABLE #ConfigServicesAmbulatory (
			Id INT,
			CareCenterCode VARCHAR(20),
			Type INT,
			ItemId INT,
			ContractDescriptionId INT,
			Request INT,
			RequestUnit TINYINT,
			INDEX IX_CSA NONCLUSTERED (CareCenterCode, Type, ItemId, ContractDescriptionId)
		)

		INSERT INTO #ConfigServicesAmbulatory
		SELECT	csa.Id,
				apcc.CareCenterCode, 		
				1 Type, 
				apce.CUPSEntityId ItemId,
				apce.ContractDescriptionId,
				csa.Request, 
				csa.RequestUnit
		FROM [Authorization].AuthorizationPortfolioCareCenter apcc
		JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
		JOIN [Authorization].AuthorizationPortfolioCUPSEntity apce ON ap.Id = apce.AuthorizationPortfolioId
		JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apce.Id = csa.AuthorizationPortfolioCUPSEntityId
		WHERE ap.Status = 1

		INSERT INTO #ConfigServicesAmbulatory
		SELECT	csa.Id,
				apcc.CareCenterCode, 		
				2 Type, 
				apip.InventoryProductId ItemId,
				NULL ContractDescriptionId,
				csa.Request, 
				csa.RequestUnit
		FROM [Authorization].AuthorizationPortfolioCareCenter apcc
		JOIN [Authorization].AuthorizationPortfolio ap ON apcc.AuthorizationPortfolioId = ap.Id
		JOIN [Authorization].AuthorizationPortfolioInventoryProduct apip ON ap.Id = apip.AuthorizationPortfolioId
		JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId
		WHERE ap.Status = 1

		CREATE TABLE #BaseData (
			CareCenterCode VARCHAR(20),
			FunctionalUnitCode VARCHAR(20),
			AdmissionNumber VARCHAR(20),
			PatientCode VARCHAR(20),
			Quantity INT,
			RequestDate DATETIME,
			Type TINYINT,
			ItemId INT,
			ItemCode VARCHAR(20),
			ItemName VARCHAR(200),
			ContractDescriptionId INT,
			CUPSEntityContractDescriptionId INT,
			DescriptionCodeName VARCHAR(500),
			TraceabilityPaperworkId INT,
			ProfessionalCode VARCHAR(20),
			Observations VARCHAR(MAX),
			INDEX IX_Base NONCLUSTERED (CareCenterCode, Type, ItemId)
		)

		INSERT INTO #BaseData
		SELECT	h.CareCenterCode,
				h.FunctionalUnitCode,
				h.AdmissionNumber,
				h.PatientCode,
				h.Quantity,
				h.RequestDate,
				h.Type,
				h.ItemId,
				h.ItemCode,
				h.ItemName,
				ISNULL(h.ContractDescriptionId, 0) ContractDescriptionId,
				ISNULL(h.CUPSEntityContractDescriptionId, 0) CUPSEntityContractDescriptionId,
				h.DescriptionCodeName,
				h.TraceabilityPaperworkId,
				h.ProfessionalCode,
				h.Observations
		FROM [Authorization].ViewListRequest h
		WHERE h.RequestDate BETWEEN @DateStart AND @DateEnd
		  AND (@HasCareCenterFilter = 0 OR h.CareCenterCode IN (SELECT Code FROM #CareCenters))
		  AND (@HasFunctionalUnitFilter = 0 OR h.FunctionalUnitCode IN (SELECT Code FROM #FunctionalUnits))

		SELECT	CASE @GroupBy
					WHEN 1 THEN h.CareCenterCode + ' - ' + cc.NOMCENATE
					WHEN 2 THEN h.FunctionalUnitCode + ' - ' + fu.UFUDESCRI
					WHEN 3 THEN cg.Code + ' - ' + cg.Name
					WHEN 4 THEN ha.Code + ' - ' + ha.Name
					WHEN 5 THEN CASE ISNULL(tp.Status, 1) 
						WHEN 1 THEN 'Solicitado' 
						WHEN 2 THEN 'Radicado' 
						WHEN 3 THEN 'Radicado Pendiente de Autorizacion' 
						WHEN 4 THEN 'Radicado No Autorizado' 
						WHEN 5 THEN 'Autorizado' 
						WHEN 6 THEN 'Autorizado en Entrega' 
						WHEN 7 THEN 'Autorizado Entregado' 
						WHEN 8 THEN 'Agendado' 
						WHEN 9 THEN 'Ejecutado' 
						WHEN 10 THEN 'Facturado' 
						WHEN 11 THEN 'Cancelado' 
						ELSE 'N/A'
					END
					WHEN 6 THEN u.UserCode + ' - ' + per.Fullname
				END GroupName,
				h.CareCenterCode + ' - ' + cc.NOMCENATE AS CareCenterCodeName,
				h.FunctionalUnitCode + ' - ' + fu.UFUDESCRI AS FunctionalUnitCodeName,
				cg.Code + ' - ' + cg.Name AS CareGroupCodeName,
				ha.Code + ' - ' + ha.Name AS HealthAdministratorCodeName,				
				h.AdmissionNumber,
				d.MainDiagnostic,
				h.PatientCode, RTRIM(LTRIM(p.IPNOMCOMP)) PatientName, 
				dbo.Edad(p.IPFECNACI, @CurrentDate) PatientAge, 
				h.RequestDate, 
				h.ProfessionalCode,
				bg.Code + ' - ' + bg.Name AS BillingGroupCodeName, 
				h.ItemCode AS ServiceCode, 
				h.ItemCode + ' - ' + h.ItemName AS ServiceDescription, 
				h.DescriptionCodeName AS ContractDescriptionCodeName,
				h.Quantity,
				IIF(ISNULL(ptc.Id, 0) > 0 OR ISNULL(prd.Id, 0) > 0, 1, 0) IsCovered,
				IIF(ISNULL(ptc.Contracted, 0) = 1 OR ISNULL(prd.Contracted, 0) = 1, 1, 0) Contracted,
				IIF(ISNULL(ptc.Quoted, 0) = 1 OR ISNULL(prd.Quoted, 0) = 1, 1, 0) Quoted,
				ISNULL(csae.Request, csa.Request) RequestTime,
				CASE ISNULL(csae.RequestUnit, csa.RequestUnit)
					WHEN 1 THEN 'Minutos' 
					WHEN 2 THEN 'Horas' 
					WHEN 3 THEN 'Dias' 
				END RequestUnitTime,
				CASE ISNULL(csae.RequestUnit, csa.RequestUnit)
					WHEN 1 THEN DATEDIFF(MINUTE, h.RequestDate, @CurrentDate)
					WHEN 2 THEN DATEDIFF(HOUR, h.RequestDate, @CurrentDate)
					WHEN 3 THEN DATEDIFF(DAY, h.RequestDate, @CurrentDate)
				END ElapsedTime,
				CASE ISNULL(tp.Status, 1) 
					WHEN 1 THEN 'Solicitado' 
					WHEN 2 THEN 'Radicado' 
					WHEN 3 THEN 'Radicado Pendiente de Autorizacion' 
					WHEN 4 THEN 'Radicado No Autorizado' 
					WHEN 5 THEN 'Autorizado' 
					WHEN 6 THEN 'Autorizado en Entrega' 
					WHEN 7 THEN 'Autorizado Entregado' 
					WHEN 8 THEN 'Agendado' 
					WHEN 9 THEN 'Ejecutado' 
					WHEN 10 THEN 'Facturado' 
					WHEN 11 THEN 'Cancelado' 
					ELSE 'N/A'
				END StatusName,
				h.Observations,
				tp.CancellationReasonsObservations,
				u.UserCode + ' - ' + per.Fullname AS AssignUser
		FROM #BaseData h
		INNER JOIN dbo.ADCENATEN cc ON h.CareCenterCode = cc.CODCENATE
		INNER JOIN dbo.INUNIFUNC fu ON h.FunctionalUnitCode = fu.UFUCODIGO
		INNER JOIN dbo.ADINGRESO ing ON h.AdmissionNumber = ing.NUMINGRES
		INNER JOIN Contract.CareGroup cg ON ing.GENCAREGROUP = cg.Id
		INNER JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
		INNER JOIN dbo.INPACIENT p ON h.PatientCode = p.IPCODPACI
		INNER JOIN #ConfigServicesAmbulatory csa ON h.CareCenterCode = csa.CareCenterCode 
			AND h.Type = csa.Type 
			AND h.ItemId = csa.ItemId 
			AND h.ContractDescriptionId = ISNULL(csa.ContractDescriptionId, 0)
		LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae 
			ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
		LEFT JOIN Contract.ProcedureCups ptc ON h.Type = 1 
			AND cg.ProcedureTemplateId = ptc.ProceduresTemplateId 
			AND h.ItemId = ptc.CupsId 
			AND h.CUPSEntityContractDescriptionId = ISNULL(ptc.CUPSEntityContractDescriptionId, 0)
		LEFT JOIN Inventory.ProductRateDetail prd ON h.Type = 2 
			AND cg.ProductRateId = prd.ProductRateId 
			AND h.ItemId = prd.ProductId 
			AND CAST(h.RequestDate AS DATE) BETWEEN prd.InitialDate AND prd.EndDate
		LEFT JOIN Contract.CUPSEntity ce ON h.Type = 1 AND ce.Id = h.ItemId
		LEFT JOIN Inventory.InventoryProduct ip ON h.Type = 2 AND ip.Id = h.ItemId
		LEFT JOIN Billing.BillingGroup bg ON bg.Id = ISNULL(ce.BillingGroupId, ip.BillingGroupId)
		LEFT JOIN [Authorization].TraceabilityPaperwork tp ON h.TraceabilityPaperworkId = tp.Id
		LEFT JOIN Security.[User] u ON u.UserCode = tp.AssignUserCode
		LEFT JOIN Security.Person per ON per.Id = u.IdPerson
		LEFT JOIN
		(
			SELECT d.NUMINGRES, MIN(d.CODDIAGNO) MainDiagnostic
			FROM dbo.INDIAGNOP d
			WHERE d.CODDIAPRI = 1
			GROUP BY d.NUMINGRES
		) d ON h.AdmissionNumber = d.NUMINGRES
		WHERE ISNULL(csae.SusceptibleAuthorization, 1) = 1
			AND (@HasStatusFilter = 0 OR ISNULL(tp.Status, 1) IN (SELECT Id FROM #Status))
			AND (@HasThirdPartyFilter = 0 OR tp.PatientCode IN (SELECT Nit FROM #ThirdParties))
			AND (@HasHealthAdminFilter = 0 OR ha.Id IN (SELECT Id FROM #HealthAdministrator))
			AND (@HasCareGroupFilter = 0 OR cg.Id IN (SELECT Id FROM #CareGroups))
			AND (@HasUserFilter = 0 OR tp.AssignUserCode IN (SELECT Code FROM #Users))
		ORDER BY 1

	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH

	DROP TABLE IF EXISTS #CareCenters
	DROP TABLE IF EXISTS #FunctionalUnits
	DROP TABLE IF EXISTS #CareGroups
	DROP TABLE IF EXISTS #HealthAdministrator
	DROP TABLE IF EXISTS #ThirdParties
	DROP TABLE IF EXISTS #Status
	DROP TABLE IF EXISTS #Users
	DROP TABLE IF EXISTS #ConfigServicesAmbulatory
	DROP TABLE IF EXISTS #BaseData
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte detallado de solicitudes de autorización de servicios de salud, filtrando por criterios enviados en formato XML como rango de fechas, centros de atención, unidades funcionales, grupos de atención, administradoras de salud, terceros, estados y usuarios. Cruza información de admisiones (ADINGRESO), pacientes (INPACIENT), centros de atención (ADCENATEN), unidades funcionales (INUNIFUNC), grupos de cuidado (CareGroup) y administradoras (HealthAdministrator) con la vista de solicitudes (ViewListRequest) para obtener un listado enriquecido de cada solicitud. Determina además si el servicio solicitado está cubierto, contratado o cotizado según el portafolio de autorización (AuthorizationPortfolio, AuthorizationPortfolioCUPSEntity), calcula tiempos transcurridos desde la solicitud comparándolos con los plazos configurados en los servicios ambulatorios (ConfigurationServicesAmbulatory), y permite agrupar los resultados por centro de atención, unidad funcional, grupo de atención, administradora, estado o usuario autorizador. Es el insumo principal para el módulo de reportería de autorizaciones y control de tiempos de respuesta en la gestión de autorizaciones de servicios.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequestDetailed';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequestDetailed';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte detallado de solicitudes de autorización ambulatoria, agrupado y filtrado dinámicamente, enriqueciendo cada solicitud con datos de paciente, contrato, cobertura, tiempos transcurridos y estado de trazabilidad.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener al menos los nodos DateStart, DateEnd y GroupBy en /Data.; Las listas multivalor (CareCenters, FunctionalUnits, CareGroups, HealthAdministrators, ThirdParties, Status, Users) llegan como cadenas separadas por coma; si vienen vacías no se aplica el filtro respectivo.; Solo se consideran portafolios de autorización con Status = 1 (activos) al construir la configuración de servicios ambulatorios.; La función dbo.Split debe estar disponible para parsear las listas del XML.; Debe existir la vista Authorization.ViewListRequest como fuente principal de solicitudes.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La fecha actual se obtiene siempre vía Common.GETDATE() y no con GETDATE() nativo, garantizando coherencia de zona horaria del sistema.; Solo se incluyen ítems cuya configuración (csae.SusceptibleAuthorization) sea 1 o NULL, es decir, susceptibles de autorización.; Las solicitudes se restringen siempre al rango [@DateStart, @DateEnd] sobre RequestDate.; Solo se consideran portafolios activos (ap.Status = 1) para armar la configuración de servicios ambulatorios.; ContractDescriptionId y CUPSEntityContractDescriptionId nulos se normalizan a 0 para hacer joins determinísticos con la cobertura.; El diagnóstico principal por ingreso es el mínimo CODDIAGNO con CODDIAPRI = 1 agrupado por NUMINGRES.; La cobertura de productos de inventario solo aplica si la fecha de solicitud cae dentro de la vigencia [InitialDate, EndDate] de la tarifa.; Todas las tablas temporales se liberan al final, incluso tras un error capturado en CATCH.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve un conjunto de resultados con el detalle de solicitudes de autorización en el rango [@DateStart, @DateEnd], agrupado según @GroupBy (1=CentroAtención, 2=UnidadFuncional, 3=GrupoAtención, 4=AdministradoraSalud, 5=Estado, 6=UsuarioAsignado) y ordenado por GroupName.; [RETURN_RESULT] RESULTSET: En caso de error en TRY/CATCH retorna un único resultset con CodeResult=''999'' y MessageResult = ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Para cada filtro multivalor (#CareCenters, #FunctionalUnits, #CareGroups, #HealthAdministrator, #ThirdParties, #Status, #Users): EXISTS registros → Se aplica el filtro IN sobre la columna correspondiente (CareCenterCode, FunctionalUnitCode, cg.Id, ha.Id, tp.PatientCode, ISNULL(tp.Status,1), tp.AssignUserCode) else No se aplica filtro y se incluyen todas las filas; si @GroupBy = 1..6 → Define la columna GroupName con la concatenación correspondiente (centro, unidad funcional, grupo de atención, administradora, estado traducido o usuario+nombre) else GroupName queda NULL; si h.Type = 1 (servicio CUPS) → Se carga configuración desde AuthorizationPortfolioCUPSEntity y se valida cobertura contra Contract.ProcedureCups por ProcedureTemplateId/CupsId/CUPSEntityContractDescriptionId else Si h.Type = 2 (producto de inventario) se carga desde AuthorizationPortfolioInventoryProduct y se valida cobertura contra Inventory.ProductRateDetail por ProductRateId/ProductId con vigencia BETWEEN InitialDate y EndDate; si ISNULL(ptc.Id,0) > 0 OR ISNULL(prd.Id,0) > 0 → IsCovered = 1 (el ítem está cubierto por la tarifa/plantilla del grupo de atención) else IsCovered = 0; si ISNULL(tp.Status,1) IN (1..11) → Traduce el código de estado a etiqueta: 1 Solicitado, 2 Radicado, 3 Radicado Pendiente de Autorización, 4 Radicado No Autorizado, 5 Autorizado, 6 Autorizado en Entrega, 7 Autorizado Entregado, 8 Agendado, 9 Ejecutado, 10 Facturado, 11 Cancelado else Etiqueta ''N/A''; si ISNULL(csae.RequestUnit, csa.RequestUnit) = 1/2/3 → ElapsedTime se calcula con DATEDIFF en MINUTE/HOUR/DAY entre RequestDate y la fecha actual; RequestUnitTime se traduce a ''Minutos''/''Horas''/''Días'' else ElapsedTime y RequestUnitTime quedan NULL; si Existe excepción en ConfigurationServicesAmbulatoryExceptions para el (csa, CareGroup) → Se usan los tiempos (Request, RequestUnit) de la excepción y se aplica el filtro ISNULL(csae.SusceptibleAuthorization,1)=1 else Se usan los tiempos definidos en ConfigurationServicesAmbulatory base', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split; Common.GETDATE; dbo.Edad', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestDetailed';
-- GO
