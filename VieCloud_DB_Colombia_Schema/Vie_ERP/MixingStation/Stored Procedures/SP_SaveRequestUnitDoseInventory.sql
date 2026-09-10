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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que crea, modifica, confirma o anula solicitudes de inventario de dosis unitarias en la estación de mezclas (MixingStation). Recibe los datos de cabecera y detalle en formato XML junto con el código del usuario que ejecuta la acción, parsea el XML para extraer la configuración del módulo de medicamentos (CMConfigurationId), el almacén (WarehouseId), el centro de atención (CareCenterCode) y el estado de la solicitud (borrador, confirmado o anulado). Para solicitudes nuevas obtiene el consecutivo o código único mediante el procedimiento Common.SP_GetSequence (formulario 2217) y registra el encabezado en MixingStation.RequestUnitDoseInventory; para solicitudes existentes actualiza los campos de modificación, confirmación o anulación según el estado indicado. Gestiona también el detalle de la solicitud en MixingStation.RequestUnitDoseInventoryDetail, eliminando las líneas marcadas para borrar e insertando o actualizando las líneas de medicamentos (tipo de dosis unitaria, código ATC, presentación/empaque, cantidad, vía de administración).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SaveRequestUnitDoseInventory';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persiste (crea, actualiza o anula) solicitudes de dosis unitarias de inventario y, al confirmarse, genera automáticamente la solicitud equivalente en la central de mezclas con su detalle.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener el nodo /RequestUnitDoseInventory con los datos de cabecera; las fechas vienen en formato 103 (dd/mm/yyyy).; Si el estado enviado no es 3 (anulación) debe existir al menos un detalle en /RequestUnitDoseInventoryDetail.; La solicitud sólo es editable si su estado actual es 1 (activa); estados 2 (Confirmado) o 3 (Anulado) bloquean el guardado.; Common.SP_GetSequence debe poder generar la secuencia para los formularios 2217 (cabecera) y 2224 (solicitud central de mezclas) bajo el módulo 520 y la unidad operativa indicada.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Sólo se permiten modificaciones sobre solicitudes en estado 1 (activa).; Code de la cabecera siempre se obtiene desde Common.SP_GetSequence (no se acepta el del XML para alta).; ConfirmationUser/Date se llenan exclusivamente cuando Status = 2; AnnulmentUser/Date exclusivamente cuando Status = 3.; La generación de la solicitud en central de mezclas (RequestMixingStation) ocurre únicamente al confirmar (Status = 2) y enlaza cada detalle con EntityName=''RequestUnitDoseInventoryDetail'' y Source=4.; Una anulación (Status=3) no altera los detalles de la solicitud.; Cualquier excepción captura ERROR_MESSAGE/ERROR_LINE, retorna CodeMessage=999 y deja Id=0 y Code='''' en la salida.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'solicitud de dosis unitaria de inventario; central de mezclas; código ATC; vía de administración; tipo de dosis unitaria; empaque/presentación; centro de atención; unidad operativa; secuencia numérica de formulario; confirmación y anulación de solicitudes', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MixingStation.RequestUnitDoseInventory: Cuando @Id = 0 se inserta la cabecera con el Code obtenido por Common.SP_GetSequence (form 2217); si @Status = 2 también se llenan ConfirmationUser y ConfirmationDate con el usuario y la fecha actual.; [UPDATE] MixingStation.RequestUnitDoseInventory: Cuando @Id <> 0 se actualiza la cabecera; ConfirmationUser/Date se setean sólo si @Status = 2 y AnnulmentUser/Date sólo si @Status = 3, en caso contrario se ponen NULL.; [DELETE] MixingStation.RequestUnitDoseInventoryDetail: Se eliminan los detalles cuyo Id coincide con líneas marcadas con IsDelete = 1 en el XML para la solicitud @Id.; [INSERT] MixingStation.RequestUnitDoseInventoryDetail: Las líneas del XML con Id = 0 (no marcadas para borrar) se insertan asociadas a la solicitud @Id.; [UPDATE] MixingStation.RequestUnitDoseInventoryDetail: Las líneas con Id existente se actualizan con UnitDoseTypeId, ATCId, PackageId, Quantity y AdministrationRouteId provenientes del XML.; [INSERT] MixingStation.RequestMixingStation: Si @Status = 2 (confirmación), se crea una cabecera de solicitud de central de mezclas con Status = 2, Code obtenido por Common.SP_GetSequence (form 2224) y el usuario/fecha actuales.; [INSERT] MixingStation.RequestMixingStationDetail: Al confirmar, por cada detalle de la solicitud de inventario se inserta una línea en RequestMixingStationDetail con Status = 1, EntityName = ''RequestUnitDoseInventoryDetail'', EntityId = Id del detalle origen y Source = 4.; [RETURN_RESULT] RESULT: Siempre retorna un único recordset con CodeMessage (0 éxito, 999 error/regla), Message descriptivo, Id de la solicitud y Code generado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe MixingStation.RequestUnitDoseInventory con Id = @Id y Status <> 1 → Se aborta con CodeMessage 999 indicando que la solicitud está Confirmada (Status=2) o Anulada (Status=3).; si @Status <> 3 y la tabla de detalles del XML está vacía → Se aborta con CodeMessage 999 ''La solicitud de dosis unitarias de inventario no tiene detalles.''; si @Id = 0 (alta nueva) → Se solicita secuencia (form 2217) y se inserta cabecera nueva. else Se actualiza la cabecera existente.; si @Status = 3 (anulación) → Tras grabar la cabecera se finaliza con mensaje ''Se anuló correctamente la solicitud'' sin tocar los detalles.; si @Status = 2 (confirmación) → Se solicita secuencia (form 2224) y se genera cabecera+detalle en RequestMixingStation/RequestMixingStationDetail enlazados a los detalles de la solicitud de inventario.; si Common.SP_GetSequence retorna @Code_Output <> 0 → Se aborta con CodeMessage 999 y mensaje del SP (con marcador {0} reemplazado por el contexto: ''la solicitud de dosis unitarias de inventario'' o ''la solicitud de central de mezclas'').', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.SP_GetSequence; Common.GETDATE', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.RequestUnitDoseInventory; MixingStation.RequestUnitDoseInventoryDetail', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SaveRequestUnitDoseInventory';
-- GO
