-- ===============================================================================================================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-07-10
-- Description:	Procedimiento que se encarga de asignar automáticamente las solicitudes
-- ===============================================================================================================================
CREATE PROCEDURE [Authorization].[SP_AutomaticAllocationTraceabilityPaperwork]
	@TraceabilityPaperworkXml AS XML,
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON
	
	--Se declaran las variables
	DECLARE @DaysToAssign AS INT,
			@PatientCode VARCHAR(20),
			@TimeForRequests INT,
			@AssignUserCode VARCHAR(20),
			--------------------------------------------------------------------------------------------------------------------
			@SubXml XML,
			@Code_Output INT = 0,
			@Message_Output VARCHAR(MAX)

	--Tabla temporal de los detalles
	DECLARE @TraceabilityPaperworks TABLE
	(
		[RowHeaderId] [int] NOT NULL,
		[Id] [int] NOT NULL,
		[AdmissionNumber] [varchar](20) NULL,
		[Folio] [varchar](20) NULL,
		[ServiceCode] [varchar](20) NOT NULL,
		[Type] [tinyint] NOT NULL,
		[PatientCode] [varchar](20) NOT NULL,
		[CareCenterCode] [varchar](20) NOT NULL,
		[RequestDate] [datetime] NOT NULL,
		[RequestQuantity] [int] NOT NULL,
		[FunctionalUnitCode] [varchar](20) NOT NULL,
		[EntityId] [int] NULL,
		[EntityName] [varchar](50) NULL,
		[Status] [tinyint] NOT NULL,
		[CancellationReasonsId] [int] NULL,
		[CancellationReasonsObservations] [varchar](max) NULL,
		[CancellationUserCode] [varchar](20) NULL,
		[AssignUserCode] [varchar](20) NULL,
		[AuthorizationSourceId] [int] NULL,		
		[IsManual] [bit] NOT NULL,
		[CareGroupId] [int] NOT NULL,
		[HealthAdministratorId] [int] NULL,
		[ProfessionalCode] [varchar](20) NOT NULL,
		[CareCenterTargetCode] [varchar](20) NULL,
		[FunctionalUnitTargetId] [int] NULL,
		[ServiceId] [int] NOT NULL, 
		[ContractDescriptionId] [int] NULL,
		[Order] [int] NOT NULL
	)

	BEGIN TRY

		SELECT @DaysToAssign = DaysToAssign
		FROM [Authorization].SettingsAuthorization

		/********************************** *************************************** **********************************/

		--Se obtiene los detalles que vienen en el xml
		INSERT INTO @TraceabilityPaperworks
			SELECT	t.x.value('RowHeaderId[1]','int') as RowHeaderId,
					t.x.value('Id[1]','int') as Id,
					IIF(t.x.value('AdmissionNumber[1]','varchar(20)') = '', null, t.x.value('AdmissionNumber[1]','varchar(20)')) as AdmissionNumber,
					IIF(t.x.value('Folio[1]','varchar(20)') = '', null, t.x.value('Folio[1]','varchar(20)')) as Folio,
					t.x.value('ServiceCode[1]','varchar(20)') as ServiceCode,
					t.x.value('Type[1]','tinyint') as Type,
					t.x.value('PatientCode[1]','varchar(20)') as PatientCode,
					t.x.value('CareCenterCode[1]','varchar(20)') as CareCenterCode,
					t.x.value('RequestDate[1]','datetime') as RequestDate,
					t.x.value('RequestQuantity[1]','int') as RequestQuantity,			
					t.x.value('FunctionalUnitCode[1]','varchar(20)') as FunctionalUnitCode,
					IIF(t.x.value('EntityId[1]','varchar(20)') = '', null, t.x.value('EntityId[1]','varchar(20)')) as EntityId,
					IIF(t.x.value('EntityName[1]','varchar(50)') = '', null, t.x.value('EntityName[1]','varchar(50)')) as EntityName,
					t.x.value('Status[1]','tinyint') as Status,
					IIF(t.x.value('CancellationReasonsId[1]','varchar(20)') = '', null, t.x.value('CancellationReasonsId[1]','varchar(20)')) as CancellationReasonsId,
					IIF(t.x.value('CancellationReasonsObservations[1]','varchar(max)') = '', null, t.x.value('CancellationReasonsObservations[1]','varchar(max)')) as CancellationReasonsObservations,
					IIF(t.x.value('CancellationUserCode[1]','varchar(20)') = '', null, t.x.value('CancellationUserCode[1]','varchar(20)')) as CancellationUserCode,
					IIF(t.x.value('AssignUserCode[1]','varchar(20)') = '', null, t.x.value('AssignUserCode[1]','varchar(20)')) as AssignUserCode,
					IIF(t.x.value('AuthorizationSourceId[1]','varchar(20)') = '', null, t.x.value('AuthorizationSourceId[1]','varchar(20)')) as AuthorizationSourceId,
					t.x.value('IsManual[1]','bit') as IsManual,			
					t.x.value('CareGroupId[1]','int') as CareGroupId,
					IIF(t.x.value('HealthAdministratorId[1]','varchar(20)') = '', null, t.x.value('HealthAdministratorId[1]','varchar(20)')) as HealthAdministratorId,
					t.x.value('ProfessionalCode[1]','varchar(20)') as ProfessionalCode,
					IIF(t.x.value('CareCenterTargetCode[1]','varchar(20)') = '', null, t.x.value('CareCenterTargetCode[1]','varchar(20)')) as CareCenterTargetCode,
					IIF(t.x.value('FunctionalUnitTargetId[1]','varchar(20)') = '', null, t.x.value('FunctionalUnitTargetId[1]','varchar(20)')) as FunctionalUnitTargetId,
					t.x.value('ServiceId[1]','int') as ServiceId,
					t.x.value('ContractDescriptionId[1]','int') as ContractDescriptionId,
					t.x.value('Order[1]','int') as [Order]
			FROM @TraceabilityPaperworkXml.nodes('/TraceabilityPaperwork') t(x)

		/********************************** *************************************** **********************************/

		-- Tiempo a asignar
		SELECT	@PatientCode = RTRIM(PatientCode),
				@TimeForRequests = SUM
				(
					ISNULL(csae.Assignment, csa.Assignment) * CASE ISNULL(csae.AssignmentUnit, csa.AssignmentUnit)
																			WHEN 1 THEN 1
																			WHEN 2 THEN 60
																			WHEN 3 THEN 1440
																		END
				)
		FROM @TraceabilityPaperworks tp
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
		WHERE ISNULL(csae.SusceptibleAuthorization, 1) = 1
		GROUP BY PatientCode

		/********************************** *************************************** **********************************/

		-- Obtenemos el primer registro ordenado por fecha que tenga el tiempo disponible para realizar la asignación
		SELECT TOP 1
			@AssignUserCode = UserCode
		FROM [Authorization].[GetAuthorizationAvailabilityTime]([Common].[GETDATE](), @DaysToAssign)
		WHERE WorkMinutes > AssignedMinutes
			AND AvailabilityMinutes >= @TimeForRequests	
		ORDER BY WorkingDate, UserCode

		-- Se valida si se obtuvo un usuario válido, de lo contrario no realizamos la asignación
		IF ISNULL(@AssignUserCode, '') = ''
		BEGIN
			SELECT @CodeResult = 1, -- No es un error, sólo no se encontró un usuario con el tiempo necesario para asignar las solicitudes del paciente
					@MessageResult = CONCAT('No se encontró un usuario con el tiempo necesario para asignar las solicitudes del paciente: ', @PatientCode)
			RETURN
		END

		--Se actualiza el usuario asignado
		UPDATE @TraceabilityPaperworks
			SET AssignUserCode = @AssignUserCode

		/********************************** *************************************** **********************************/

		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT	TraceabilityPaperwork.*
				FROM @TraceabilityPaperworks TraceabilityPaperwork
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Authorization].[SP_SaveTraceabilityPaperwork_Output] @SubXml, @Code_Output OUT, @Message_Output OUT, NULL, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999, 
					@MessageResult = CONCAT('Error al asignar las solicitudes del paciente: ', @PatientCode, ' - ', @Message_Output)
			RETURN
		END

		/********************************** *************************************** **********************************/

		SELECT @CodeResult = 0, 
			   @MessageResult =  CONCAT('Las solicitudes del paciente ', @PatientCode, ' fueron asignadas correctamente')
	END TRY
	BEGIN CATCH
		SELECT @CodeResult = 999, 
			   @MessageResult = CONCAT('Error al asignar las solicitudes del paciente: ', @PatientCode, ' - ', ERROR_MESSAGE(), ' - Linea: ', CAST(ERROR_LINE() AS VARCHAR(10)))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que asigna automáticamente solicitudes de trazabilidad de papelería (trámites de autorización ambulatoria) recibidas en formato XML. Toma cada solicitud y determina el tiempo máximo permitido para su asignación consultando la configuración general de autorizaciones (SettingsAuthorization) y los plazos definidos por servicio en la configuración de servicios ambulatorios (ConfigurationServicesAmbulatory), cruzando el portafolio de autorización vigente (AuthorizationPortfolio) con el centro de atención correspondiente (AuthorizationPortfolioCareCenter), el código CUPS o servicio de salud (AuthorizationPortfolioCUPSEntity) y, cuando aplica, los productos de inventario como medicamentos e insumos (AuthorizationPortfolioInventoryProduct). Su propósito principal es automatizar la asignación de autorizaciones de servicios ambulatorios —procedimientos, medicamentos y tecnologías en salud— garantizando que cada solicitud quede asignada dentro de los tiempos y condiciones contractuales pactadas por entidad y centro de atención, retornando un código y mensaje de resultado sobre el éxito o falla del proceso.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'PROCEDURE', @level1name = N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Asigna automáticamente las solicitudes de autorización de un paciente a un usuario disponible cuyo tiempo libre alcance para procesarlas, según la configuración de tiempos del portafolio.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un registro en Authorization.SettingsAuthorization que provea DaysToAssign.; El XML de entrada debe contener nodos /TraceabilityPaperwork con los campos requeridos (ServiceCode, Type, PatientCode, CareCenterCode, ServiceId, CareGroupId, etc.).; Debe existir al menos un AuthorizationPortfolio con Status = 1 que coincida con CareCenter, Type, ServiceId y ContractDescriptionId del trámite para calcular el tiempo requerido.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran portafolios activos (AuthorizationPortfolio.Status = 1) para el cálculo del tiempo requerido.; Solo se incluyen ítems susceptibles de autorización: ISNULL(csae.SusceptibleAuthorization, 1) = 1.; La búsqueda de usuario disponible se limita a la ventana de @DaysToAssign días desde Common.GETDATE() y selecciona el primer usuario ordenado por WorkingDate, UserCode.; La asignación es atómica por paciente: o todos los detalles del XML quedan con el mismo AssignUserCode o ninguno se asigna.; El emparejamiento de portafolio considera ContractDescriptionId tratando NULL como 0 (ISNULL(...,0) = ISNULL(...,0)).', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'paciente; solicitud de autorización; portafolio de autorización; centro de atención; grupo de atención (CareGroup); CUPS; producto de inventario; servicios ambulatorios; asignación automática de trámites; disponibilidad de tiempo del usuario auditor', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[UPDATE] @TraceabilityPaperworks: Cuando se encuentra un usuario con tiempo disponible, se actualiza AssignUserCode de todos los detalles con el código del usuario seleccionado antes de delegar el guardado.; [RETURN_RESULT] Authorization.SP_SaveTraceabilityPaperwork_Output: Se invoca el SP de guardado pasando los detalles ya asignados como XML; si retorna Code_Output<>0 se propaga error con CodeResult=999.; [RETURN_RESULT] OUTPUT: Si no se encuentra un usuario con el tiempo necesario, retorna CodeResult=1 y mensaje informativo (no error) indicando el paciente.; [RETURN_RESULT] OUTPUT: Si la asignación termina exitosamente retorna CodeResult=0 con mensaje de confirmación; ante excepción capturada retorna CodeResult=999 con ERROR_MESSAGE y línea.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@AssignUserCode,'''') = '''' (ningún usuario en GetAuthorizationAvailabilityTime cumple WorkMinutes>AssignedMinutes y AvailabilityMinutes>=@TimeForRequests) → Se aborta sin asignar y se devuelve CodeResult=1 con mensaje ''No se encontró un usuario con el tiempo necesario...'' else Se actualiza AssignUserCode en los detalles y se invoca SP_SaveTraceabilityPaperwork_Output.; si @Code_Output <> 0 tras ejecutar SP_SaveTraceabilityPaperwork_Output → Se devuelve CodeResult=999 con mensaje compuesto que incluye el paciente y el mensaje recibido del SP hijo. else Se devuelve CodeResult=0 con mensaje de éxito.; si AssignmentUnit ISNULL(csae.AssignmentUnit, csa.AssignmentUnit) = 1/2/3 → El tiempo se calcula multiplicando Assignment por 1 (minutos), 60 (horas) o 1440 (días) respectivamente.; si Existe excepción en ConfigurationServicesAmbulatoryExceptions para el CareGroupId del trámite → Se usan los valores Assignment/AssignmentUnit/SusceptibleAuthorization de la excepción en lugar de los de ConfigurationServicesAmbulatory.', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Authorization.SP_SaveTraceabilityPaperwork_Output; Authorization.GetAuthorizationAvailabilityTime; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Authorization.SettingsAuthorization; Authorization.AuthorizationPortfolioCareCenter; Authorization.AuthorizationPortfolio; Authorization.AuthorizationPortfolioCUPSEntity; Authorization.AuthorizationPortfolioInventoryProduct; Authorization.ConfigurationServicesAmbulatory; Authorization.ConfigurationServicesAmbulatoryExceptions; Authorization.GetAuthorizationAvailabilityTime', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Authorization', @level1type=N'PROCEDURE', @level1name=N'SP_AutomaticAllocationTraceabilityPaperwork';
-- GO
