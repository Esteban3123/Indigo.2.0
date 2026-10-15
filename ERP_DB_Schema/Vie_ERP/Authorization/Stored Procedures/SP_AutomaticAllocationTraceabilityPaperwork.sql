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