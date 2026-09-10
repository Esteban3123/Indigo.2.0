-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2019-10-30
-- Description:	Procedimiento para el reporte resumido de autorizaciones (solicitudes)
-- =============================================
CREATE PROCEDURE [Authorization].[SP_ReportRequestSummary]
		@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATETIME,
			@DateEnd DATETIME,
			@GroupBy TINYINT,
			@CurrentDate DATETIME -- Precalcular fecha actual una sola vez

	SET @CurrentDate = [Common].[GETDATE]()

	-- Usar tablas temporales con índices
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

		-- Insertar filtros en tablas temporales (solo si existen valores)
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

		-- Determinar si hay filtros activos
		DECLARE @HasCareCenterFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #CareCenters) THEN 1 ELSE 0 END
		DECLARE @HasFunctionalUnitFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #FunctionalUnits) THEN 1 ELSE 0 END
		DECLARE @HasCareGroupFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #CareGroups) THEN 1 ELSE 0 END
		DECLARE @HasHealthAdminFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #HealthAdministrator) THEN 1 ELSE 0 END
		DECLARE @HasThirdPartyFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #ThirdParties) THEN 1 ELSE 0 END
		DECLARE @HasStatusFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #Status) THEN 1 ELSE 0 END
		DECLARE @HasUserFilter BIT = CASE WHEN EXISTS(SELECT 1 FROM #Users) THEN 1 ELSE 0 END

		/********************************** OBTENCION DE DATOS **********************************/

		-- Materializar la configuración de servicios ambulatorios en tabla temporal
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
		JOIN [Authorization].ConfigurationServicesAmbulatory csa ON apip.Id = csa.AuthorizationPortfolioCUPSEntityId  -- Mantener igual que el SP original
		WHERE ap.Status = 1

		-- Materializar datos base filtrados por fecha
		CREATE TABLE #BaseData (
			CareCenterCode VARCHAR(20),
			FunctionalUnitCode VARCHAR(20),
			AdmissionNumber VARCHAR(20),
			Quantity INT,
			RequestDate DATETIME,
			Type TINYINT,
			ItemId INT,
			ContractDescriptionId INT,
			TraceabilityPaperworkId INT,
			INDEX IX_Base NONCLUSTERED (CareCenterCode, Type, ItemId)
		)

		INSERT INTO #BaseData
		SELECT	h.CareCenterCode,
				h.FunctionalUnitCode,
				h.AdmissionNumber,
				h.Quantity,
				h.RequestDate,
				h.Type,
				h.ItemId,
				ISNULL(h.ContractDescriptionId, 0) ContractDescriptionId,
				h.TraceabilityPaperworkId
		FROM [Authorization].ViewListRequest h
		WHERE h.RequestDate BETWEEN @DateStart AND @DateEnd 
		  AND (@HasCareCenterFilter = 0 OR h.CareCenterCode IN (SELECT Code FROM #CareCenters))
		  AND (@HasFunctionalUnitFilter = 0 OR h.FunctionalUnitCode IN (SELECT Code FROM #FunctionalUnits))

		SELECT	h.CareCenterCode + ' - ' + cc.NOMCENATE AS CareCenterCodeName,
				h.FunctionalUnitCode + ' - ' + fu.UFUDESCRI AS FunctionalUnitCodeName,
				cg.Code + ' - ' + cg.Name AS CareGroupCodeName,
				ha.Code + ' - ' + ha.Name AS HealthAdministratorCodeName,		
				bg.Code + ' - ' + bg.Name AS BillingGroupCodeName,
				SUM(h.Quantity) AS Quantity,
				SUM(ISNULL(csae.Request, csa.Request)) AS RequestTime,
				CASE ISNULL(csae.RequestUnit, csa.RequestUnit)
					WHEN 1 THEN 'Minutos' 
					WHEN 2 THEN 'Horas' 
					WHEN 3 THEN 'Dias'
				END AS RequestUnitTime,
				SUM(CASE ISNULL(csae.RequestUnit, csa.RequestUnit)
					WHEN 1 THEN DATEDIFF(MINUTE, h.RequestDate, @CurrentDate)
					WHEN 2 THEN DATEDIFF(HOUR, h.RequestDate, @CurrentDate)
					WHEN 3 THEN DATEDIFF(DAY, h.RequestDate, @CurrentDate)
				END) AS ElapsedTime,
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
				END AS StatusName,
				u.UserCode + ' - ' + per.Fullname AS AssignUser
		FROM #BaseData h
		INNER JOIN dbo.ADCENATEN cc ON h.CareCenterCode = cc.CODCENATE
		INNER JOIN dbo.INUNIFUNC fu ON h.FunctionalUnitCode = fu.UFUCODIGO
		INNER JOIN dbo.ADINGRESO ing ON h.AdmissionNumber = ing.NUMINGRES
		INNER JOIN Contract.CareGroup cg ON ing.GENCAREGROUP = cg.Id
		INNER JOIN Contract.HealthAdministrator ha ON ing.GENCONENTITY = ha.Id
		INNER JOIN #ConfigServicesAmbulatory csa ON h.CareCenterCode = csa.CareCenterCode 
			AND h.Type = csa.Type 
			AND h.ItemId = csa.ItemId 
			AND h.ContractDescriptionId = ISNULL(csa.ContractDescriptionId, 0)
		LEFT JOIN [Authorization].ConfigurationServicesAmbulatoryExceptions csae 
			ON csa.Id = csae.ConfigurationServicesAmbulatoryId AND cg.Id = csae.CareGroupId
		LEFT JOIN Contract.CUPSEntity ce ON h.Type = 1 AND ce.Id = h.ItemId
		LEFT JOIN Inventory.InventoryProduct ip ON h.Type = 2 AND ip.Id = h.ItemId
		LEFT JOIN Billing.BillingGroup bg ON bg.Id = ISNULL(ce.BillingGroupId, ip.BillingGroupId)
		LEFT JOIN [Authorization].TraceabilityPaperwork tp ON h.TraceabilityPaperworkId = tp.Id
		LEFT JOIN Security.[User] u ON u.UserCode = tp.AssignUserCode
		LEFT JOIN Security.Person per ON per.Id = u.IdPerson		
		WHERE ISNULL(csae.SusceptibleAuthorization, 1) = 1
			AND (@HasStatusFilter = 0 OR ISNULL(tp.Status, 1) IN (SELECT Id FROM #Status))
			AND (@HasThirdPartyFilter = 0 OR tp.PatientCode IN (SELECT Nit FROM #ThirdParties))
			AND (@HasHealthAdminFilter = 0 OR ha.Id IN (SELECT Id FROM #HealthAdministrator))
			AND (@HasCareGroupFilter = 0 OR cg.Id IN (SELECT Id FROM #CareGroups))
			AND (@HasUserFilter = 0 OR tp.AssignUserCode IN (SELECT Code FROM #Users))
		GROUP BY h.CareCenterCode + ' - ' + cc.NOMCENATE,
				 h.FunctionalUnitCode + ' - ' + fu.UFUDESCRI,
				 cg.Code + ' - ' + cg.Name,
				 ha.Code + ' - ' + ha.Name,
				 bg.Code + ' - ' + bg.Name,
				 ISNULL(csae.RequestUnit, csa.RequestUnit),
				 ISNULL(tp.Status, 1),
				 u.UserCode + ' - ' + per.Fullname

	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH

	-- Limpiar tablas temporales
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte resumido de solicitudes de autorización de servicios de salud. Recibe criterios de filtro en formato XML (rango de fechas, centros de atención, unidades funcionales, grupos de atención, administradoras de salud, terceros, estados y usuarios) y devuelve un resumen agrupado de las solicitudes de autorización registradas en el sistema. Para cada agrupación muestra el centro de atención y unidad funcional (consultando ADCENATEN e INUNIFUNC), el grupo de atención y administradora de salud del ingreso del paciente (ADINGRESO, CareGroup, HealthAdministrator), la cantidad de solicitudes, el tiempo configurado para la solicitud según el portafolio de autorización vigente (AuthorizationPortfolio, AuthorizationPortfolioCUPSEntity, ConfigurationServicesAmbulatory), el tiempo transcurrido desde la solicitud y el estado actual (Solicitado, Radicado, Autorizado, Cancelado, entre otros). Es el procedimiento principal del informe gerencial y operativo de seguimiento y gestión de autorizaciones ambulatorias.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequestSummary';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_ReportRequestSummary';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte resumido y agrupado de solicitudes de autorización ambulatoria, calculando cantidades, tiempos configurados y tiempo transcurrido por centro, unidad funcional, grupo de atención, administradora y estado.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener al menos DateStart, DateEnd y GroupBy en el nodo /Data.; Los listados multivalor (CareCenters, FunctionalUnits, CareGroups, HealthAdministrators, ThirdParties, Status, Users) deben venir como cadenas separadas por coma o vacías.; Deben existir portafolios de autorización con Status = 1 para que la consulta retorne resultados (el INNER JOIN con #ConfigServicesAmbulatory los exige).; La función Common.GETDATE() y dbo.Split deben estar disponibles.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran portafolios de autorización con Status = 1 al construir #ConfigServicesAmbulatory.; El rango de fechas filtra por RequestDate BETWEEN @DateStart AND @DateEnd de forma obligatoria (no es opcional).; Cuando un servicio no tiene Status en TraceabilityPaperwork se asume estado 1 (Solicitado).; Cuando ContractDescriptionId es NULL en la solicitud o en la configuración se normaliza a 0 para el JOIN.; El tiempo transcurrido (ElapsedTime) se calcula contra Common.GETDATE() y en la unidad definida por la configuración (excepción si existe, sino base).; Los servicios marcados con SusceptibleAuthorization = 0 en la excepción nunca aparecen en el reporte.; Las tablas temporales se liberan al final incluso después de errores capturados por el CATCH.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Autorización de servicios ambulatorios; Portafolio de autorización; Solicitud / radicación / autorización / ejecución / facturación / cancelación (ciclo de vida del trámite); Grupo de atención (CareGroup); Administradora de salud (EPS/pagador); Centro de atención y unidad funcional; CUPS (procedimientos); Productos de inventario (medicamentos/insumos); Grupo de facturación; Tiempos de solicitud y unidades (minutos/horas/días); Excepciones de configuración por grupo de atención; Susceptibilidad de autorización; Ingreso/Admisión del paciente', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve un conjunto agrupado por centro de atención, unidad funcional, grupo de atención, administradora de salud, grupo de facturación, unidad de tiempo, estado y usuario asignado, con SUM(Quantity), SUM(Request) como RequestTime, SUM(DATEDIFF) como ElapsedTime y traducción de Status (1..11) y RequestUnit (1=Minutos,2=Horas,3=Días).; [RETURN_RESULT] Resultset: Si ocurre cualquier error, devuelve una fila con CodeResult=''999'' y MessageResult = ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE().', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si h.Type = 1 (servicio CUPS) → Cruza ItemId contra Contract.CUPSEntity y toma BillingGroupId desde CUPSEntity; usa configuración de portafolio CUPS (AuthorizationPortfolioCUPSEntity). else Si h.Type = 2 (producto inventario), cruza con Inventory.InventoryProduct y usa BillingGroupId del producto y configuración de portafolio de inventario.; si Existe ConfigurationServicesAmbulatoryExceptions para el CareGroup del ingreso (csae no nulo) → Usa Request y RequestUnit de la excepción (csae) sobre la configuración base. else Usa Request y RequestUnit de ConfigurationServicesAmbulatory (csa).; si ISNULL(csae.SusceptibleAuthorization,1) = 1 → Incluye la fila en el reporte. else Excluye filas cuya excepción marque el servicio como no susceptible de autorización.; si ISNULL(tp.Status,1) IN (1..11) → Traduce a etiqueta de estado (Solicitado, Radicado, Autorizado, Ejecutado, Facturado, Cancelado, etc.). else Etiqueta ''N/A''.; si Cada @Has<Filtro>Filter = 1 → Aplica el filtro correspondiente (CareCenter, FunctionalUnit, Status, ThirdParty, HealthAdministrator, CareGroup, User). else Ignora el filtro y considera todos los valores.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatory; Authorization.ConfigurationServicesAmbulatoryExceptions; Authorization.ViewListRequest; Authorization.TraceabilityPaperwork; dbo.ADCENATEN; dbo.INUNIFUNC; dbo.ADINGRESO; Contract.CareGroup; Contract.HealthAdministrator; Contract.CUPSEntity; Inventory.InventoryProduct; Billing.BillingGroup; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_ReportRequestSummary';
-- GO
