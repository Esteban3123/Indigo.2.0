-- ===============================================================================================================================
-- Author:		Carlos Mario Arias Rubiano
-- Create date: 09/02/2021
-- Description:	Procedimiento que se encarga de guardar las solicitudes dosis unitarias inventario
-- ===============================================================================================================================
CREATE PROCEDURE [MixingStation].[SP_SaveRequestUnitDoseInventory]
	@Xml XML,
	@UserCode VARCHAR(20)
AS
BEGIN
	--============== CREACIÓN DE VARIABLES ==============--
	DECLARE 
        @Id                INT = 0,
        @Code              VARCHAR(20) = '',
        @CMConfigurationId INT,
        @WarehouseId       INT,
        @DocumentDate      DATETIME,
        @Status            TINYINT,
        @OperatingUnitId   INT,
        @CareCenterCode    VARCHAR(20),
		-- Secuencua númerica
		@IsManual BIT, 
		@IdForm VARCHAR(5),
		-- Variables para la secuencia númerica de solicitud
		@RequestCode VARCHAR(20), 
		@RequestId INT,
		-- Variables de salida de otros SP
		@Code_Output INT,
		@Message_Output VARCHAR(MAX),	
		-------------------------------
		@CodeMessage INT = 0,
		@Message NVARCHAR(MAX) = ''

    DECLARE @RequestUnitDoseInventoryDetail TABLE (
        Id                       INT,
        RequestUnitDoseInventoryId INT,
        UnitDoseTypeId          INT,
        ATCId                   INT,
        PackageId               INT,
        Quantity                INT,
        AdministrationRouteId  INT,
        IsDelete               BIT
    );


	BEGIN TRY
		--============== INSERCIÓN DE DATOS ==============--
			
		--Se obtienen los datos para la cabecera
		SELECT 
			@Id = ISNULL(t.x.value('Id[1]','int'), 0),
			@Code = ISNULL(t.x.value('Code[1]','varchar(20)'), ''),
			@CMConfigurationId = t.x.value('CMConfigurationId[1]','int'),
			@WarehouseId = t.x.value('WarehouseId[1]','int'),
			@DocumentDate = CONVERT(DATE, t.x.value('DocumentDate[1]','varchar(20)'), 103),
			@Status = t.x.value('Status[1]','tinyint'),
			@OperatingUnitId = t.x.value('OperatingUnitId[1]','int'),
			@CareCenterCode = t.x.value('CareCenterCode[1]','varchar(20)')
		FROM @Xml.nodes('/RequestUnitDoseInventory') t(x)
					   
		--Se obtienen los detalles del xml
		INSERT INTO @RequestUnitDoseInventoryDetail
		SELECT 
			t.x.value('Id[1]','int') AS Id,
			t.x.value('RequestUnitDoseInventoryId[1]','int') AS RequestUnitDoseInventoryId,
			t.x.value('UnitDoseTypeId[1]','int') AS UnitDoseTypeId,
			NULLIF(t.x.value('ATCId[1]','varchar(20)'), '') AS ATCId,
			NULLIF(t.x.value('PackageId[1]','varchar(20)'), '') AS PackageId,
			t.x.value('Quantity[1]','int') AS Quantity,
			NULLIF(t.x.value('AdministrationRouteId[1]','int'), 0) AS AdministrationRouteId,
			t.x.value('IsDelete[1]','bit') AS IsDelete
		FROM @Xml.nodes('/RequestUnitDoseInventory/RequestUnitDoseInventoryDetail') t(x)

		/*************************************VALIDACIONES************************************/

		IF EXISTS (SELECT 1 FROM MixingStation.RequestUnitDoseInventory WHERE Id = @Id AND Status <> 1)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = 'La solicitud de dosis unitarias de inventario se encuentra en estado: ' + IIF(Status = 2, 'Confirmado', 'Anulado')
			FROM MixingStation.RequestUnitDoseInventory
			WHERE Id = @Id
								
			GOTO FinSP;
		END

		IF @Status <> 3 AND NOT EXISTS (SELECT 1 FROM @RequestUnitDoseInventoryDetail)
		BEGIN
			SELECT	@CodeMessage = 999, 
					@Message = 'La solicitud de dosis unitarias de inventario no tiene detalles.'
			
			GOTO FinSP;
		END

		/*************************************************************************************/

		--Se guarda la cabecera
		IF @Id = 0
		BEGIN
			--Si se esta insertando por primera vez se consulta la secuencia numerica
			SET @IdForm = '2217'
			EXEC Common.SP_GetSequence 520, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @Code OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = REPLACE(@Message_Output, '{0}', 'la solicitud de dosis unitarias de inventario')
								
				GOTO FinSP;
			END

			--Se insertan datos de cabecera
			INSERT INTO MixingStation.RequestUnitDoseInventory (
				Code, CMConfigurationId, WarehouseId, DocumentDate, Status,
				CreationUser, CreationDate,
				ConfirmationUser, ConfirmationDate,
				CareCenterCode
			)
			VALUES (
				@Code, @CMConfigurationId, @WarehouseId, @DocumentDate, @Status,
				@UserCode, Common.GETDATE(),
				IIF(@Status = 2, @UserCode, NULL),
				IIF(@Status = 2, Common.GETDATE(), NULL),
				@CareCenterCode
			);
			SET @Id = SCOPE_IDENTITY();
		END
		ELSE
		BEGIN
			UPDATE MixingStation.RequestUnitDoseInventory
			SET
				Code = @Code,
				CMConfigurationId = @CMConfigurationId,
				WarehouseId = @WarehouseId,
				DocumentDate = @DocumentDate,
				Status = @Status,
				ModificationUser = @UserCode,
				ModificationDate = Common.GETDATE(),
				ConfirmationUser = IIF(@Status = 2, @UserCode, NULL),
				ConfirmationDate = IIF(@Status = 2, Common.GETDATE(), NULL),
				AnnulmentUser = IIF(@Status = 3, @UserCode, NULL),
				AnnulmentDate = IIF(@Status = 3, Common.GETDATE(), NULL),
				CareCenterCode = @CareCenterCode
			WHERE Id = @Id;
		END

		--Se anula el registro
		IF @Status = 3
		BEGIN
			SELECT	@CodeMessage = 0,
					@Message = 'Se anuló correctamente la solicitud'

			GOTO FinSP;
		END

		--Se eliminan los detalles
		DELETE rudi 
		FROM MixingStation.RequestUnitDoseInventoryDetail rudi
		JOIN @RequestUnitDoseInventoryDetail d 
			ON rudi.RequestUnitDoseInventoryId = @Id
				AND rudi.Id = d.Id
		WHERE d.IsDelete = 1

		DELETE FROM @RequestUnitDoseInventoryDetail 
		WHERE IsDelete = 1

		--Se insertan los registros nuevos
		INSERT INTO MixingStation.RequestUnitDoseInventoryDetail (
			RequestUnitDoseInventoryId, UnitDoseTypeId, ATCId, PackageId, Quantity, AdministrationRouteId)
		SELECT
			@Id, UnitDoseTypeId, ATCId, PackageId, Quantity, AdministrationRouteId
		FROM @RequestUnitDoseInventoryDetail
		WHERE Id = 0

		-- Actualizar los registros existentes en RequestUnitDoseInventoryDetail
		UPDATE rd
		SET rd.UnitDoseTypeId = temp.UnitDoseTypeId,
			rd.ATCId = temp.ATCId,
			rd.PackageId = temp.PackageId,
			rd.Quantity = temp.Quantity,
			rd.AdministrationRouteId = temp.AdministrationRouteId
		FROM MixingStation.RequestUnitDoseInventoryDetail rd
		JOIN @RequestUnitDoseInventoryDetail temp 
			ON rd.RequestUnitDoseInventoryId = @Id
				AND rd.Id = temp.Id

		/*************************************************************************************/

		-- Si se está confirmando (Status = 2)
		IF @Status = 2
		BEGIN
			SET @IdForm = '2224'
			EXEC Common.SP_GetSequence 520, @IdForm, @OperatingUnitId, NULL, NULL, @IsManual OUT, @RequestCode OUT, @Code_Output OUT, @Message_Output OUT

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeMessage = 999, 
						@Message = REPLACE(@Message_Output, '{0}', 'la solicitud de central de mezclas')
								
				GOTO FinSP;
			END

			-- Se realiza la cabecera de la solicitud en la tabla RequestMixingStation
			INSERT INTO [MixingStation].[RequestMixingStation](
				[Code], [Status], [RequestUser], [RequestDate], [CMConfigurationId])
			VALUES 
				(@RequestCode, 2, @UserCode, Common.GETDATE(), @CMConfigurationId);

			-- Obtenemos el ID generado de la cabecera de la solicitud
			SET @RequestId = SCOPE_IDENTITY();

			-- Se crean los detalles de la solicitud en la tabla RequestMixingStationDetail
			INSERT INTO MixingStation.RequestMixingStationDetail (
				RequestMixingStationId, ATCId, PackageId, UnitDoseTypeId, Quantity,
				Status, EntityId, EntityName, CareCenterCode, Source
			)
			SELECT
				@RequestId, ATCId, PackageId, UnitDoseTypeId, Quantity,
				1, Id, 'RequestUnitDoseInventoryDetail', @CareCenterCode, 4
			FROM MixingStation.RequestUnitDoseInventoryDetail
			WHERE RequestUnitDoseInventoryId = @Id
		END

		/*************************************************************************************/

		SET @Message = 'Se guardó correctamente la solicitud dosis unitaria inventario con código ' + @Code + IIF(@Status = 2,  CHAR(13) + CHAR(10) + 'Se generó solicitud central de mezclas con código ' + @RequestCode, '')

		FinSP:
	END TRY
	BEGIN CATCH
		BEGIN 
			SELECT	@CodeMessage = 999, 
					@Message = CONCAT(ERROR_MESSAGE(), ' - Line: ', ERROR_LINE()), 
					@Id = 0, 
					@Code = ''
		END
	END CATCH

	SELECT @CodeMessage AS CodeMessage, @Message AS Message, @Id AS Id, @Code AS Code;
	RETURN
END