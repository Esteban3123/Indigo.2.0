-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 11/02/2021
-- Description:	Procedimiento que se encarga de guardar las solicitudes dosis unitarias centros de atención externos
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveRequestUnitDoseExternalCareCenter]
	@Xml xml,
	@UserCode varchar(20)
AS
BEGIN
	SET NOCOUNT ON
	
	--Variables para guardar la cabecera
	declare @Id int, 
			@Code varchar(20), 
			@CMConfigurationId int, 
			@ExternalCareCenterId int, 
			@RequestType tinyint, 
			@DocumentDate datetime, 
			@Status tinyint, 
			@OperatingUnitId int, 
			@ContractExternalClientsId int

	--Variables de proceso
	DECLARE @IsManual BIT,
			@MessageReturn VARCHAR(MAX),
			@ExternalPatientXml XML,			
			@CodeResult_Output INT,
			@MessageResult_Output VARCHAR(MAX)

	--Tabla de detalles de pacientes
	declare @RequestUnitDoseExternalCareCenterPatient table (
		RequestUnitDoseExternalCareCenterPatientRowId int, 
		Id int, 
		RequestUnitDoseExternalCareCenterId int, 
		PatientExternalCareCenterId int,
		UnitDoseTypeId int,
		NptId int,
		IsDelete bit
	)

	-- Tabla de pacientes externos
	declare @PatientExternalCareCenter table(
		RequestUnitDoseExternalCareCenterPatientRowId int, 
		Id int, 
		IdentificationNumber	VARCHAR(25),
		IdentificationTypeId	INT,
		[Name]					VARCHAR(300),
		LastName				VARCHAR(300),
		GenderTypeId			INT,
		PatientMobileNumber		VARCHAR(10),
		PatientEmail			VARCHAR(50),
		ExternalFunctionalUnit	VARCHAR(100),
		PatientBed				VARCHAR(50)
	)

	--Tabla de preparaciones
	declare @ExternalPatientPreparations table (
		RequestUnitDoseExternalCareCenterPatientRowId int,
		ExternalPatientPreparationRowId int,
		Id int, 
		RequestUnitDoseExternalCareCenterPatientId int, 
		PreparationsRequested int,
		PreparationTypeId int,
		AdministrationRouteId int,
		AssociatedPackageId int,
		VolumeTotalOrder decimal(18,2), 
		TotalPreparedUnitMeasurementId int,
		Concentration varchar(50),
		Description varchar(max),
		IsDelete bit
	)

	--Tabla detalle de preparaciones
	declare @ExternalPatientPreparationDetails table (
		ExternalPatientPreparationRowId int,
		Id int, 
		ExternalPatientPreparationId int, 
		ItemType int, 
		AtcId int,
		SupplieId int,
		ProductId int,
		ComponentType int,
		Quantity decimal(18,2), 
		MeasurementUnitId int,
		Volume decimal(18,2), 
		VolumeMeasureUnitId int,
		IsDelete bit
	)
	
	--Tabla en detalles de maquila
	declare @RequestUnitDoseExternalCareCenterMaquila table (
		Id int, 
		RequestUnitDoseExternalCareCenterId int, 
		Type tinyint, 
		ATCId int, 
		PackageId int, 
		UnitDoseTypeId int, 
		Quantity int, 
		IsDelete bit
	)

	BEGIN TRY		
		--Se obtienen los datos para la cabecera
		select 
			@Id = t.x.value('Id[1]','int'),
			@Code = t.x.value('Code[1]','varchar(20)'),
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int'),
			@ExternalCareCenterId = t.x.value('ExternalCareCenterId[1]','int'),
			@RequestType = t.x.value('RequestType[1]','tinyint'),
			@DocumentDate = convert(date, t.x.value('DocumentDate[1]','varchar(20)'), 103),
			@Status = t.x.value('Status[1]','tinyint'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@ContractExternalClientsId = t.x.value('ContractExternalClientsId[1]','int')
		from @Xml.nodes('/RequestUnitDoseExternalCareCenter') t(x)
		
		--Se obtienen los detalles del xml
		insert into @RequestUnitDoseExternalCareCenterPatient
			select 
				t.x.value('RequestUnitDoseExternalCareCenterPatientRowId[1]','int') as RequestUnitDoseExternalCareCenterPatientRowId,
				t.x.value('Id[1]','int') as Id,
				t.x.value('RequestUnitDoseExternalCareCenterId[1]','int') as RequestUnitDoseExternalCareCenterId,
				t.x.value('PatientExternalCareCenterId[1]','int') as PatientExternalCareCenterId,
				t.x.value('UnitDoseTypeId[1]','int') as UnitDoseTypeId,
				t.x.value('NptId[1]','int') as NptId,
				t.x.value('IsDelete[1]','bit') as IsDelete
			from @Xml.nodes('/RequestUnitDoseExternalCareCenter/RequestUnitDoseExternalCareCenterPatient') t(x)		

		--Se obtienen los pacientes externos del xml
		insert into @PatientExternalCareCenter
			select 
				t.x.value('RequestUnitDoseExternalCareCenterPatientRowId[1]','int') as RequestUnitDoseExternalCareCenterPatientRowId,
				t.x.value('Id[1]','int') as Id,
				t.x.value('IdentificationNumber[1]','varchar(25)') AS IdentificationNumber,
				t.x.value('IdentificationTypeId[1]','INT') AS IdentificationTypeId,
				t.x.value('Name[1]','VARCHAR(300)') AS [Name],
				t.x.value('LastName[1]','VARCHAR(300)') AS LastName,
				t.x.value('GenderTypeId[1]','INT') AS GenderTypeId,
				t.x.value('PatientMobileNumber[1]','VARCHAR(10)') AS  PatientMobileNumber,
				t.x.value('PatientEmail[1]','VARCHAR(50)') AS  PatientEmail,
				t.x.value('ExternalFunctionalUnit[1]','VARCHAR(100)') AS  ExternalFunctionalUnit,
				t.x.value('PatientBed[1]','VARCHAR(50)') AS  PatientBed
			from @Xml.nodes('/RequestUnitDoseExternalCareCenter/RequestUnitDoseExternalCareCenterPatient/PatientExternalCareCenter') t(x)

		--Se obtienen las preparaciones personalizadas del xml
		insert into @ExternalPatientPreparations
		select 
			t.x.value('RequestUnitDoseExternalCareCenterPatientRowId[1]','int') as RequestUnitDoseExternalCareCenterPatientRowId,
			t.x.value('ExternalPatientPreparationRowId[1]','int') as ExternalPatientPreparationRowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('RequestUnitDoseExternalCareCenterPatientId[1]','int') as RequestUnitDoseExternalCareCenterPatientId,
			t.x.value('PreparationsRequested[1]','int') as PreparationsRequested,
			t.x.value('PreparationTypeId[1]','int') as PreparationTypeId,
			t.x.value('AdministrationRouteId[1]','int') as AdministrationRouteId,
			t.x.value('AssociatedPackageId[1]','int') as AssociatedPackageId,
			t.x.value('VolumeTotalOrder[1]','decimal(18,2)') as VolumeTotalOrder,
			t.x.value('TotalPreparedUnitMeasurementId[1]','int') as TotalPreparedUnitMeasurementId,
			t.x.value('Concentration[1]','varchar(50)') as Concentration,
			t.x.value('Description[1]','varchar(max)') as Description,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/RequestUnitDoseExternalCareCenter/RequestUnitDoseExternalCareCenterPatient/ExternalPatientPreparation') t(x)

		--Se obtienen los detalles de las preparaciones personalizadas del xml
		insert into @ExternalPatientPreparationDetails
		select 
			t.x.value('ExternalPatientPreparationRowId[1]','int') as ExternalPatientPreparationRowId,
			t.x.value('Id[1]','int') as Id,
			t.x.value('ExternalPatientPreparationId[1]','int') as ExternalPatientPreparationId,
			t.x.value('itemType[1]','int') as itemType,
			t.x.value('AtcId[1]','int') as AtcId,
			t.x.value('SupplieId[1]','int') as SupplieId,
			t.x.value('ProductId[1]','int') as ProductId,
			t.x.value('ComponentType[1]','int') as ComponentType,
			t.x.value('Quantity[1]','decimal(18,2)') as Quantity,
			t.x.value('MeasurementUnitId[1]','int') as MeasurementUnitId,
			t.x.value('Volume[1]','decimal(18,2)') as Volume,
			t.x.value('VolumeMeasureUnitId[1]','int') as VolumeMeasureUnitId,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/RequestUnitDoseExternalCareCenter/RequestUnitDoseExternalCareCenterPatient/ExternalPatientPreparation/ExternalPatientPreparationDetail') t(x)

		--Se obtienen los detalles
		insert into @RequestUnitDoseExternalCareCenterMaquila
		select 
			t.x.value('Id[1]','int') as Id,
			t.x.value('RequestUnitDoseExternalCareCenterId[1]','int') as RequestUnitDoseExternalCareCenterId,
			t.x.value('Type[1]','tinyint') as Type,
			IIF(t.x.value('ATCId[1]','varchar(20)') = '', null, t.x.value('ATCId[1]','varchar(20)')) as ATCId,
			IIF(t.x.value('PackageId[1]','varchar(20)') = '', null, t.x.value('PackageId[1]','varchar(20)')) as PackageId,
			t.x.value('UnitDoseTypeId[1]','int') as UnitDoseTypeId,
			t.x.value('Quantity[1]','int') as Quantity,
			t.x.value('IsDelete[1]','bit') as IsDelete
		from @Xml.nodes('/RequestUnitDoseExternalCareCenter/RequestUnitDoseExternalCareCenterMaquila') t(x)
	
		/*************************************VALIDACIONES************************************/

		IF EXISTS (SELECT 1 FROM MixingStation.RequestUnitDoseExternalCareCenter r WHERE r.Id = @Id AND r.Status <> 1)
		BEGIN
			SELECT 999 CodeMessage, 
				   'La solicitud de centro de atención externa se encuentra en estado: ' + IIF(r.Status = 2, 'Confirmado', 'Anulado') Message, 
				   0 Id, 
				   '' Code
			FROM MixingStation.RequestUnitDoseExternalCareCenter r 
			WHERE r.Id = @Id
			RETURN
		END

		---------------------------------------------------------------------------------------------------------------

		IF ISNULL(@Id, 0) = 0 --Se guarda la cabecera
		BEGIN
			--Si se esta insertando por primera vez se consulta la secuencia numerica
			EXEC Common.SP_GetSequence 520, '2220', @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @CodeResult_Output OUT, @MessageResult_Output OUT

			IF @CodeResult_Output <> 0
			BEGIN
				SELECT	999 CodeMessage, 
						REPLACE(@MessageResult_Output, '{0}', 'solicitud de centro de atención externa') Message, 
						0 Id, 
						'' Code
				RETURN
			END

			--Se inserta la cabecera
			INSERT INTO [MixingStation].[RequestUnitDoseExternalCareCenter](
				[Code],[CMConfigurationId],[ExternalCareCenterId],
				[RequestType], [DocumentDate],[Status], [ContractExternalClientsId],
				[CreationUser],[CreationDate],[ConfirmationUser],[ConfirmationDate])
			VALUES(@Code, @CMConfigurationId, @ExternalCareCenterId, @RequestType, @DocumentDate, @Status, @ContractExternalClientsId,
				@UserCode, Common.GETDATE(), IIF(@Status = 2, @UserCode, NULL), IIF(@Status = 2, Common.GETDATE(), NULL))

			SET @Id = SCOPE_IDENTITY()
		END
		ELSE BEGIN --Se actualiza la cabecera
			UPDATE [MixingStation].[RequestUnitDoseExternalCareCenter] 
			SET [Code] = @Code, 
				[CMConfigurationId] = @CMConfigurationId, 
				[ExternalCareCenterId] = @ExternalCareCenterId,
				[RequestType] = @RequestType, 
				[DocumentDate] = @DocumentDate, 
				[Status] = @Status,  
				[ContractExternalClientsId] = @ContractExternalClientsId, 
				[ModificationUser] = @UserCode, 
				[ModificationDate] = Common.GETDATE(), 
				[ConfirmationUser] = IIF(@Status = 2, @UserCode, NULL), 
				[ConfirmationDate] = IIF(@Status = 2, Common.GETDATE(), NULL), 
				[AnnulmentUser] = IIF(@Status = 3, @UserCode, NULL), 
				[AnnulmentDate] = IIF(@Status = 3, Common.GETDATE(), NULL)
			WHERE Id = @Id
		END

		--Se valida si se esta anulando
		IF @Status = 3
		BEGIN 
			SELECT 0 AS CodeMessage, 'Se anuló correctamente la solicitud' AS Message, @Id Id, @Code Code
			RETURN
		END

		---------------------------------------------------------------------------------------------------------------

		--Se eliminan los detalles
		DELETE eppd
		FROM MixingStation.ExternalPatientPreparationDetail eppd
		JOIN @ExternalPatientPreparationDetails eppdt ON eppd.Id = eppdt.Id
		WHERE eppdt.IsDelete = 1

		DELETE FROM @ExternalPatientPreparationDetails WHERE IsDelete = 1

		DELETE eppd
		FROM MixingStation.ExternalPatientPreparationDetail eppd
		JOIN @ExternalPatientPreparations eppt ON eppd.ExternalPatientPreparationId = eppt.Id
		WHERE eppt.Id > 0 AND eppt.IsDelete = 1

		DELETE epp
		FROM MixingStation.ExternalPatientPreparation epp
		JOIN @ExternalPatientPreparations eppt ON epp.Id = eppt.Id
		WHERE eppt.Id > 0 AND eppt.IsDelete = 1

		DELETE FROM @ExternalPatientPreparations WHERE IsDelete = 1

		DELETE eppd
		FROM MixingStation.ExternalPatientPreparationDetail eppd
		JOIN MixingStation.ExternalPatientPreparation epp ON eppd.ExternalPatientPreparationId = epp.Id
		JOIN MixingStation.RequestUnitDoseExternalCareCenterPatient rudeccp ON epp.RequestUnitDoseExternalCareCenterPatientId = rudeccp.Id
		JOIN @RequestUnitDoseExternalCareCenterPatient rudeccpt ON rudeccp.Id = rudeccpt.Id
		WHERE rudeccpt.Id > 0 AND rudeccpt.IsDelete = 1

		DELETE epp
		FROM MixingStation.ExternalPatientPreparation epp
		JOIN MixingStation.RequestUnitDoseExternalCareCenterPatient rudeccp ON epp.RequestUnitDoseExternalCareCenterPatientId = rudeccp.Id
		JOIN @RequestUnitDoseExternalCareCenterPatient rudeccpt ON rudeccp.Id = rudeccpt.Id
		WHERE rudeccpt.Id > 0 AND rudeccpt.IsDelete = 1

		DELETE rudeccp
		FROM MixingStation.RequestUnitDoseExternalCareCenterPatient rudeccp
		JOIN @RequestUnitDoseExternalCareCenterPatient rudeccpt ON rudeccp.Id = rudeccpt.Id
		WHERE rudeccpt.Id > 0 AND rudeccpt.IsDelete = 1

		DELETE pecc
		FROM MixingStation.PatientExternalCareCenter pecc
		JOIN @RequestUnitDoseExternalCareCenterPatient rudeccpt ON pecc.Id = rudeccpt.PatientExternalCareCenterId
		WHERE rudeccpt.Id > 0 AND rudeccpt.IsDelete = 1

		DELETE FROM @RequestUnitDoseExternalCareCenterPatient WHERE IsDelete = 1

		DELETE rudeccm
		FROM MixingStation.RequestUnitDoseExternalCareCenterMaquila rudeccm
		JOIN @RequestUnitDoseExternalCareCenterMaquila rudeccmt ON rudeccm.Id = rudeccmt.Id
		WHERE rudeccmt.Id > 0 AND rudeccmt.IsDelete = 1

		DELETE FROM @RequestUnitDoseExternalCareCenterMaquila WHERE IsDelete = 1
		
		---------------------------------------------------------------------------------------------------------------
		
		IF @RequestType = 1
		BEGIN
		--======================================== SOLICITUDES PACIENTE ========================================
			--Variables para recorrer el detalle 
			DECLARE @RequestUnitDoseExternalCareCenterPatientRows INT = 1, 
					@RequestUnitDoseExternalCareCenterPatientRowId INT = 0, 
					@RequestUnitDoseExternalCareCenterPatientId INT,
					@RequestUnitDoseExternalCareCenterId INT, 
					@PatientExternalCareCenterId INT, 
					@UnitDoseTypeId INT,
					@NptId INT,
					@ExternalPatientPreparationRows INT = 1, 
					@ExternalPatientPreparationRowId INT = 0, 
					@ExternalPatientPreparationId INT

			WHILE @RequestUnitDoseExternalCareCenterPatientRows > 0
			BEGIN
				SELECT TOP 1 
					@RequestUnitDoseExternalCareCenterPatientRowId = RequestUnitDoseExternalCareCenterPatientRowId, 
					@RequestUnitDoseExternalCareCenterPatientId = Id,
					@RequestUnitDoseExternalCareCenterId = RequestUnitDoseExternalCareCenterId, 
					@PatientExternalCareCenterId = PatientExternalCareCenterId,
					@UnitDoseTypeId = UnitDoseTypeId,
					@NptId = NptId,
					----------------------------------------------
					@ExternalPatientPreparationRows = 1,
					@ExternalPatientPreparationRowId = 0
				FROM @RequestUnitDoseExternalCareCenterPatient
				WHERE RequestUnitDoseExternalCareCenterPatientRowId > @RequestUnitDoseExternalCareCenterPatientRowId 
				ORDER BY RequestUnitDoseExternalCareCenterPatientRowId

				SET @RequestUnitDoseExternalCareCenterPatientRows = @@RowCount
				IF @RequestUnitDoseExternalCareCenterPatientRows = 0 
					BREAK

				-- Crea o actualiza el paciente externo
				SET @ExternalPatientXml = 
				(
					SELECT 
						@PatientExternalCareCenterId Id,
						IdentificationNumber,
						IdentificationTypeId,
						[Name],
						LastName,
						GenderTypeId,
						PatientMobileNumber,
						PatientEmail,
						ExternalFunctionalUnit,
						PatientBed
					FROM @PatientExternalCareCenter
					WHERE RequestUnitDoseExternalCareCenterPatientRowId =  @RequestUnitDoseExternalCareCenterPatientRowId
					FOR XML PATH, TYPE, ELEMENTS
				)

				EXEC [MixingStation].[SP_SavePatientExternalCareCenter] @ExternalPatientXml, @UserCode, @CodeResult_Output OUT, @MessageResult_Output OUT, @PatientExternalCareCenterId OUT
				IF @CodeResult_Output <> 0
				BEGIN
					SELECT 999 AS CodeMessage, @MessageResult_Output, 0 Id, '' Code
					RETURN
				END

				--Se inserta el detalle de la solicitud
				IF @RequestUnitDoseExternalCareCenterPatientId = 0 
				BEGIN
					INSERT INTO [MixingStation].[RequestUnitDoseExternalCareCenterPatient](
								[RequestUnitDoseExternalCareCenterId], [PatientExternalCareCenterId], [UnitDoseTypeId], [NptId]) 
					VALUES	(@Id, @PatientExternalCareCenterId, @UnitDoseTypeId, @NptId)

					SET @RequestUnitDoseExternalCareCenterPatientId = SCOPE_IDENTITY()
				END
				ELSE BEGIN --Se actualiza el detalle
					UPDATE [MixingStation].[RequestUnitDoseExternalCareCenterPatient] 
					SET [PatientExternalCareCenterId] = @PatientExternalCareCenterId, [UnitDoseTypeId] = @UnitDoseTypeId, [NptId] = @NptId
					WHERE Id = @RequestUnitDoseExternalCareCenterPatientId
				END

				/*************** Proceso con detalle de preparaciones ***************/
				WHILE @ExternalPatientPreparationRows > 0
				BEGIN
					SELECT TOP 1 
						@ExternalPatientPreparationRowId = ExternalPatientPreparationRowId, 
						@ExternalPatientPreparationId = Id
					FROM @ExternalPatientPreparations
					WHERE ExternalPatientPreparationRowId > @ExternalPatientPreparationRowId
						AND RequestUnitDoseExternalCareCenterPatientRowId = @RequestUnitDoseExternalCareCenterPatientRowId
					ORDER BY ExternalPatientPreparationRowId

					SET @ExternalPatientPreparationRows = @@RowCount
					IF @ExternalPatientPreparationRows = 0 
						BREAK

					--Se inserta la preparacion personalizada
					IF @ExternalPatientPreparationId = 0 
					BEGIN
						INSERT INTO [MixingStation].[ExternalPatientPreparation](
							RequestUnitDoseExternalCareCenterPatientId,
							PreparationsRequested, PreparationTypeId,
							AdministrationRouteId, AssociatedPackageId,
							VolumeTotalOrder, TotalPreparedUnitMeasurementId,
							Concentration, Description
						) 
						SELECT	@RequestUnitDoseExternalCareCenterPatientId,
								PreparationsRequested,
								PreparationTypeId,
								AdministrationRouteId,
								AssociatedPackageId,
								VolumeTotalOrder,
								TotalPreparedUnitMeasurementId,
								Concentration,
								Description
						FROM @ExternalPatientPreparations
						WHERE ExternalPatientPreparationRowId = @ExternalPatientPreparationRowId

						SET @ExternalPatientPreparationId = SCOPE_IDENTITY()
					END
					ELSE BEGIN --Se actualiza la preparacion personalizada
						UPDATE epp 
							SET PreparationsRequested = eppt.PreparationsRequested,
								PreparationTypeId = eppt.PreparationTypeId,
								AdministrationRouteId = eppt.AdministrationRouteId,
								AssociatedPackageId = eppt.AssociatedPackageId,
								VolumeTotalOrder = eppt.VolumeTotalOrder,
								TotalPreparedUnitMeasurementId = eppt.TotalPreparedUnitMeasurementId,
								Concentration = eppt.Concentration,
								Description = eppt.Description
						FROM [MixingStation].[ExternalPatientPreparation] epp
						JOIN @ExternalPatientPreparations eppt ON epp.Id = eppt.Id
						WHERE ExternalPatientPreparationRowId = @ExternalPatientPreparationRowId
					END

					/*************** detalles de la preparacion personalizada ***************/
					INSERT INTO [MixingStation].[ExternalPatientPreparationDetail](
						ExternalPatientPreparationId,
						itemType, AtcId, SupplieId, ProductId,
						ComponentType, Quantity, MeasurementUnitId,
						Volume, VolumeMeasureUnitId
					) 
					SELECT	@ExternalPatientPreparationId,
							ItemType,
							AtcId,
							SupplieId,
							ProductId,
							ComponentType,
							Quantity,
							MeasurementUnitId,
							Volume,
							VolumeMeasureUnitId
					FROM @ExternalPatientPreparationDetails
					WHERE ExternalPatientPreparationRowId = @ExternalPatientPreparationRowId
						AND Id = 0
			
					--Se actualizan los detalles de la preparacion personalizada
					UPDATE eppd 
						SET itemType = eppdt.ItemType,
							AtcId = eppdt.AtcId,
							SupplieId = eppdt.SupplieId,
							ProductId = eppdt.ProductId,
							ComponentType = eppdt.ComponentType,
							Quantity = eppdt.Quantity,
							MeasurementUnitId = eppdt.MeasurementUnitId,
							Volume = eppdt.Volume,
							VolumeMeasureUnitId = eppdt.VolumeMeasureUnitId
					FROM [MixingStation].[ExternalPatientPreparationDetail] eppd
					JOIN @ExternalPatientPreparationDetails eppdt ON eppd.Id = eppdt.Id
					WHERE ExternalPatientPreparationRowId = @ExternalPatientPreparationRowId
						AND eppdt.Id > 0 AND eppd.ExternalPatientPreparationId = @ExternalPatientPreparationId
				END
			END
		END
		ELSE IF	@RequestType = 2
		BEGIN
			--======================================== SOLICITUDES MAQUILA ======================================== 
			--Se insertan los registros nuevos de maquila
			INSERT INTO [MixingStation].[RequestUnitDoseExternalCareCenterMaquila](
						[RequestUnitDoseExternalCareCenterId],[Type],[ATCId],[PackageId],[UnitDoseTypeId],[Quantity])
			SELECT @Id, Type, ATCId, PackageId, UnitDoseTypeId, Quantity
			FROM @RequestUnitDoseExternalCareCenterMaquila
			WHERE Id = 0

			--Se actualizan los registros de maquila
			UPDATE m 
			SET m.RequestUnitDoseExternalCareCenterId = temp.RequestUnitDoseExternalCareCenterId,
				m.Type = temp.Type, 
				m.ATCId = temp.ATCId, 
				m.PackageId = temp.PackageId,
				m.UnitDoseTypeId = temp.UnitDoseTypeId, 
				m.Quantity = temp.Quantity		
			FROM @RequestUnitDoseExternalCareCenterMaquila temp
			JOIN MixingStation.RequestUnitDoseExternalCareCenterMaquila m ON m.Id = temp.Id
			WHERE temp.Id > 0

			----------------- CREACIÓN DE LA SOLICITUD DE CENTRAL DE MEZCLAS -----------------
			--Si se esta confirmando y la solicitud es tipo maquila
			IF @Status = 2 
			BEGIN
				--Variables de la solicitud
				DECLARE @RequestCode VARCHAR(20), @RequestId INT

				--Se consulta la secuencia numerica para la solicitud de central de mezclas
				EXEC Common.SP_GetSequence 520, '2224', @OperatingUnitId, NULL, NULL, @IsManual OUT, @RequestCode OUT, @CodeResult_Output OUT, @MessageResult_Output OUT

				IF @CodeResult_Output <> 0
				BEGIN
					SELECT	999 CodeMessage, 
							REPLACE(@MessageResult_Output, '{0}', 'Solicitudes Central Mezclas') Message, 
							0 Id, 
							'' Code
					RETURN
				END

				--Se realiza la cabecera de la solicitud
				INSERT INTO [MixingStation].[RequestMixingStation](
							[Code],[Status],[RequestUser],[RequestDate], [CMConfigurationId])
				VALUES(@RequestCode, 2, @UserCode, Common.GETDATE(), @CMConfigurationId)

				--Se obtiene el id generado de la cabecera de la solicitud
				SET @RequestId = SCOPE_IDENTITY()

				--Código del centro de atención externo
				DECLARE @ExternalCareCenterCode VARCHAR(20) = (SELECT Code FROM MixingStation.ExternalCareCenter WHERE Id = @ExternalCareCenterId)

				--Se crean los detalles de la solicitud
				INSERT INTO [MixingStation].[RequestMixingStationDetail](
							[RequestMixingStationId],[ATCId],[PackageId],[UnitDoseTypeId],[Quantity],[Status],[EntityId],[EntityName], [CareCenterCode], [Source])
				SELECT	@RequestId, d.ATCId, d.PackageId, d.UnitDoseTypeId, d.Quantity, 1, d.Id, 'RequestUnitDoseExternalCareCenterMaquila', @ExternalCareCenterCode, 3
				FROM MixingStation.RequestUnitDoseExternalCareCenterMaquila d
				WHERE d.RequestUnitDoseExternalCareCenterId = @Id
			END
		END
		ELSE BEGIN
			SELECT 999 AS CodeMessage, 'Tipo de solicitud no válida' AS Message, 0 Id, '' Code
			RETURN
		END

		SELECT @MessageReturn = 'Se guardó correctamente la solicitud dosis unitaria centro atención externo con código ' 
				+ @Code + IIF(@Status = 2 AND @RequestType = 2, CHAR(13) + CHAR(10) + 'Se generó solicitud central de mezclas con código ' + @RequestCode, '')

		--Se retorna el ok
		SELECT 0 AS CodeMessage, @MessageReturn AS Message, @Id Id, @Code Code
		RETURN

	END TRY
	BEGIN CATCH
		--Se retorna el error
		SELECT	999 AS CodeMessage, 
				CONCAT('Error: ', ERROR_MESSAGE(), ' - Linea: ', ERROR_LINE()) AS Message, 
				0 Id, 
				'' Code
		RETURN
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda o actualiza una solicitud de dosis unitaria para centros de atención externos (por ejemplo, clínicas o IPS externas que solicitan preparaciones farmacéuticas a la estación de mezclas). Recibe un XML completo con la cabecera de la solicitud, los pacientes externos asociados (con sus datos de identificación, cama y unidad funcional), las preparaciones personalizadas requeridas (tipo de preparación, vía de administración, volumen, concentración) y el detalle de componentes de cada preparación (medicamentos, insumos, cantidades). Según el estado del registro, puede crear una nueva solicitud (obteniendo un código secuencial), confirmarla, modificarla o anularla, registrando en cada caso el usuario y la fecha de la operación. Es el punto central de registro de solicitudes de elaboración de mezclas para pacientes de centros externos, integrando el contrato con el cliente externo y la configuración de la estación de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Una solicitud con Status distinto de 1 (Confirmado=2 o Anulado=3) no puede modificarse: el procedimiento aborta antes de cualquier cambio; Status=2 fija ConfirmationUser/ConfirmationDate; Status=3 fija AnnulmentUser/AnnulmentDate; en otros estados ambos quedan NULL; El Code de la cabecera se genera vía Common.SP_GetSequence (tipo 520, subtipo ''2220'') solo cuando es inserción nueva (Id=0); Para RequestType=2 confirmado, siempre se crea una solicitud en RequestMixingStation con Status=2 y secuencia tipo 520 subtipo ''2224'', enlazando los detalles de maquila con EntityName=''RequestUnitDoseExternalCareCenterMaquila'' y Source=3; Cuando se elimina un RequestUnitDoseExternalCareCenterPatient se eliminan también sus ExternalPatientPreparation, ExternalPatientPreparationDetail y el PatientExternalCareCenter asociado; Solo se procesan detalles de pacientes/preparaciones cuando RequestType=1; solo se procesan registros de maquila cuando RequestType=2; Si el RequestType no es 1 ni 2, no se persiste detalle alguno y se retorna error 999; ATCId y PackageId del XML de maquila se normalizan a NULL cuando vienen como cadena vacía; Toda la operación se envuelve en TRY/CATCH; ante excepción retorna CodeMessage=999 con mensaje y línea de error', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de dosis unitaria; centro de atención externo; maquila farmacéutica; preparación personalizada (central de mezclas); paciente externo; ATC / presentación / unidad de dosis; ruta de administración; concentración y volumen de preparación; secuencia documental; confirmación y anulación de solicitud; solicitud a central de mezclas (RequestMixingStation)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe la solicitud (Id) y su Status <> 1 (no está en estado activo/pendiente) → Retorna CodeMessage 999 informando que la solicitud está ''Confirmado'' (Status=2) o ''Anulado'' (otro), sin aplicar cambios else Continúa con guardado/actualización; si ISNULL(@Id,0) = 0 → Solicita secuencia ''2224''/''2220'' vía Common.SP_GetSequence (tipo 520) e INSERTA cabecera en RequestUnitDoseExternalCareCenter con CreationUser/CreationDate else UPDATE de la cabecera registrando ModificationUser/ModificationDate; si @Status = 2 (confirmando) → Setea ConfirmationUser y ConfirmationDate con el usuario y fecha actual else Deja ConfirmationUser/Date en NULL; si @Status = 3 (anulando) → Setea AnnulmentUser/AnnulmentDate y retorna CodeMessage 0 con mensaje ''Se anuló correctamente la solicitud'' sin procesar detalles else Continúa con procesamiento de detalles; si @RequestType = 1 (solicitud por paciente) → Itera pacientes: por cada uno invoca SP_SavePatientExternalCareCenter, inserta/actualiza RequestUnitDoseExternalCareCenterPatient, y dentro itera ExternalPatientPreparation con sus ExternalPatientPreparationDetail else Evalúa otras ramas de RequestType; si @RequestType = 2 (solicitud maquila) → Inserta nuevos registros (Id=0) y actualiza existentes en RequestUnitDoseExternalCareCenterMaquila; si además @Status=2 genera solicitud en RequestMixingStation y sus detalles else Si RequestType no es 1 ni 2 retorna 999 ''Tipo de solicitud no válida''; si @RequestType = 2 AND @Status = 2 → Obtiene secuencia ''2224'', inserta cabecera en RequestMixingStation (Status=2) e inserta detalles en RequestMixingStationDetail desde RequestUnitDoseExternalCareCenterMaquila con EntityName=''RequestUnitDoseExternalCareCenterMaquila'', Source=3 y CareCenterCode del ExternalCareCenter; si @CodeResult_Output <> 0 tras llamar a Common.SP_GetSequence o SP_SavePatientExternalCareCenter → Retorna CodeMessage 999 con el mensaje devuelto y aborta el proceso; si IsDelete = 1 en cualquier nodo del XML (paciente/preparación/detalle/maquila) → Elimina en cascada de ExternalPatientPreparationDetail, ExternalPatientPreparation, RequestUnitDoseExternalCareCenterPatient, PatientExternalCareCenter y RequestUnitDoseExternalCareCenterMaquila según corresponda', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; MixingStation.SP_SavePatientExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestUnitDoseExternalCareCenter; MixingStation.ExternalPatientPreparationDetail; MixingStation.ExternalPatientPreparation; MixingStation.RequestUnitDoseExternalCareCenterPatient; MixingStation.PatientExternalCareCenter; MixingStation.RequestUnitDoseExternalCareCenterMaquila; MixingStation.ExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseExternalCareCenter';
-- GO
