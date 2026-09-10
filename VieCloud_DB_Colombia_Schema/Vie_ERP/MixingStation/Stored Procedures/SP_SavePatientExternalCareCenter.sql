
-- =============================================
-- Author:		Giovanny plazas
-- Create date: 2021-02-08
-- Description:	Procedimiento para guardar los datos del formulario Pacientes para centros de atencion externos en las tablas person y PatientExternalCareCenter
-- =============================================
CREATE PROCEDURE [MixingStation].[SP_SavePatientExternalCareCenter] 	
			@Xml AS XML,
			@UserCode AS VARCHAR(20),
			----------------------------------------
			@CodeResult_Output INT OUTPUT,
			@MessageResult_Output VARCHAR(MAX) OUTPUT,
			@IdExternalPatient INT OUT
AS
BEGIN
	SET NOCOUNT ON
	-- Variables para guardar el paciente
	DECLARE @Id						INT,
			@IdentificationNumber	VARCHAR(25),
			@IdentificationTypeId	INT,
			@Name					VARCHAR(300),
			@LastName				VARCHAR(300),
			@GenderTypeId			INT,
			@Status					BIT,
			@PatientMobileNumber	VARCHAR(10),
			@PatientEmail			VARCHAR(50),
			@ExternalFunctionalUnit	VARCHAR(100),
			@PatientBed				VARCHAR(50),
			------------------------------
			@Message VARCHAR(MAX)

	--Tabla temporal que se devuelve y sirve para poder validar
	DECLARE @TableMessage table(CodeMessage int, MessageReturn varchar(max))

	BEGIN TRY
		SELECT 
			@Id = t.x.value('Id[1]','INT'),
			@IdentificationNumber = t.x.value('IdentificationNumber[1]','varchar(25)'),
			@IdentificationTypeId = t.x.value('IdentificationTypeId[1]','INT'),
			@Name = t.x.value('Name[1]','VARCHAR(300)'),
			@LastName = t.x.value('LastName[1]','VARCHAR(300)'),
			@GenderTypeId = t.x.value('GenderTypeId[1]','INT'),
			@Status = ISNULL(t.x.value('Status[1]','BIT'), 1),
			@PatientMobileNumber = t.x.value('PatientMobileNumber[1]','VARCHAR(10)'),
			@PatientEmail = t.x.value('PatientEmail[1]','VARCHAR(50)'),
			@ExternalFunctionalUnit = t.x.value('ExternalFunctionalUnit[1]','VARCHAR(100)'),
			@PatientBed = t.x.value('PatientBed[1]','VARCHAR(50)')
			FROM @Xml.nodes('/row') t(x)

			PRINT @PatientMobileNumber

			---------------------------- VALIDACIONES ----------------------------
			IF NOT EXISTS(
				SELECT 1
				FROM ADTIPOIDENTIFICA it
				WHERE it.ID = @IdentificationTypeId AND IT.ESTADO = 1
			) BEGIN
				SELECT @CodeResult_Output = 999, 
						@MessageResult_Output = ISNULL(@Message, 'Error en el tipo de identificación seleccionado para el paciente externo.'),
						@IdExternalPatient = 0
				RETURN
			END

			IF NOT EXISTS(
				SELECT 1
				FROM Admissions.GenderTypes gt
				WHERE gt.Id = @GenderTypeId AND gt.Status = 1
			) BEGIN
				SELECT @CodeResult_Output = 999, 
						@MessageResult_Output = ISNULL(@Message, 'Error en el género seleccionado para el paciente externo.'),
						@IdExternalPatient = 0
				RETURN
			END

			---------------------------- INSERCIÓN ----------------------------
			IF @Id = 0
			BEGIN
				INSERT INTO MixingStation.PatientExternalCareCenter (	
					IdentificationNumber,
					IdentificationTypeId,
					[Name],
					LastName,
					GenderTypeId,
					[Status],
					PatientMobileNumber,
					PatientEmail,
					ExternalFunctionalUnit,
					PatientBed,
					CreationUser,
					CreationDate
				) VALUES (
						@IdentificationNumber,
						@IdentificationTypeId,
						@Name,
						@LastName,
						@GenderTypeId,
						@Status,
						@PatientMobileNumber,
						@PatientEmail,
						@ExternalFunctionalUnit,
						@PatientBed,
						@UserCode,
						Common.GETDATE()
				)
				
			SET @Id = SCOPE_IDENTITY()

			SELECT	@Message = 'El paciente externo ' + pecc.IdentificationNumber + ' se ha guardado correctamente'
			FROM MixingStation.PatientExternalCareCenter pecc
			WHERE pecc.Id = @Id

			END
			ELSE BEGIN
				UPDATE [MixingStation].[PatientExternalCareCenter]
				SET IdentificationNumber = @IdentificationNumber,
					IdentificationTypeId = @IdentificationTypeId,
					[Name] = @Name,
					LastName = @LastName,
					GenderTypeId = @GenderTypeId,
					[Status] = @Status,
					PatientMobileNumber = @PatientMobileNumber,
					PatientEmail = @PatientEmail,
					ExternalFunctionalUnit = @ExternalFunctionalUnit,
					PatientBed = @PatientBed,
					ModificationUser = @UserCode,
					ModificationDate = Common.GETDATE()
				WHERE Id = @Id

			SELECT	@Message = 'El paciente externo ' + pecc.IdentificationNumber + ' se ha modificado correctamente'
			FROM MixingStation.PatientExternalCareCenter pecc
			WHERE pecc.Id = @Id

			END

			SELECT @CodeResult_Output = 0,
					@MessageResult_Output = @Message,
					@IdExternalPatient = @Id

	END TRY		
	BEGIN CATCH
		SELECT '999' AS Code, ERROR_MESSAGE() AS Message, ERROR_LINE() AS Line
		SELECT @CodeResult_Output = 999, 
			   @MessageResult_Output = 'Paciente no creado. SP_SavePatientExternalCareCenter: ' + ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(10)),
			   @IdExternalPatient = 0

		RETURN
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra o actualiza los datos de un paciente externo en un centro de atención externo, guardando su información personal (nombre, apellido, documento de identidad, género, celular, correo electrónico, cama y unidad funcional externa) en la tabla PatientExternalCareCenter. Antes de guardar, valida que el tipo de documento exista y esté activo en el catálogo ADTIPOIDENTIFICA y que el género esté vigente en el catálogo GenderTypes de admisiones. Si el paciente no existe (Id = 0) lo crea; si ya existe, lo actualiza registrando el usuario y la fecha de modificación. Retorna un código de resultado, un mensaje de éxito o error, y el identificador del paciente externo creado o modificado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SavePatientExternalCareCenter';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'PROCEDURE', @level1name = N'SP_SavePatientExternalCareCenter';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Persistir (crear o actualizar) la información de un paciente proveniente de un centro de atención externo a partir de un XML, validando catálogos de tipo de identificación y género.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener un nodo /row con los campos del paciente externo (Id, IdentificationNumber, IdentificationTypeId, Name, LastName, GenderTypeId, etc.); @IdentificationTypeId debe corresponder a un registro activo (ESTADO=1) en ADTIPOIDENTIFICA; @GenderTypeId debe corresponder a un registro activo (Status=1) en Admissions.GenderTypes; @Id=0 indica creación; cualquier otro valor indica actualización del registro existente con ese Id', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El tipo de identificación referenciado debe existir y estar activo (ESTADO=1) en ADTIPOIDENTIFICA antes de cualquier escritura; El tipo de género referenciado debe existir y estar activo (Status=1) en Admissions.GenderTypes antes de cualquier escritura; Si el XML no provee Status, se asume activo (Status=1) por defecto vía ISNULL; Las fechas de creación y modificación se registran con Common.GETDATE() (no con GETDATE() del servidor); Ante cualquier excepción se retorna código 999, IdExternalPatient=0 y mensaje con el error y la línea, sin propagar la excepción', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente externo; Centro de atención externo; Tipo de identificación; Tipo de género; Unidad funcional externa; Cama del paciente', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] MixingStation.PatientExternalCareCenter: Cuando @Id=0 y las validaciones de tipo de identificación y género pasan, se inserta un nuevo paciente externo con CreationUser=@UserCode y CreationDate=Common.GETDATE(); [UPDATE] MixingStation.PatientExternalCareCenter: Cuando @Id<>0 y las validaciones pasan, se actualizan los datos del paciente externo identificado por Id=@Id, fijando ModificationUser=@UserCode y ModificationDate=Common.GETDATE(); [RETURN_RESULT] MixingStation.PatientExternalCareCenter: En éxito retorna CodeResult=0, mensaje ''El paciente externo {IdentificationNumber} se ha guardado/modificado correctamente'' y el Id del registro en @IdExternalPatient; [RETURN_RESULT] MixingStation.PatientExternalCareCenter: En CATCH retorna un result set con Code=999, ERROR_MESSAGE() y ERROR_LINE(), y outputs CodeResult_Output=999 con mensaje ''Paciente no creado. SP_SavePatientExternalCareCenter: ...''', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si El tipo de identificación no existe en ADTIPOIDENTIFICA o su ESTADO ≠ 1 → Retorna código 999 con mensaje ''Error en el tipo de identificación seleccionado para el paciente externo.'' e IdExternalPatient=0 else Continúa con la siguiente validación; si El género no existe en Admissions.GenderTypes o su Status ≠ 1 → Retorna código 999 con mensaje ''Error en el género seleccionado para el paciente externo.'' e IdExternalPatient=0 else Continúa con la inserción/actualización; si @Id = 0 (sin Id en el XML) → INSERT en MixingStation.PatientExternalCareCenter con CreationUser/CreationDate y se obtiene el nuevo Id vía SCOPE_IDENTITY() else UPDATE de MixingStation.PatientExternalCareCenter para el Id recibido, asignando ModificationUser/ModificationDate', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADTIPOIDENTIFICA; Admissions.GenderTypes; MixingStation.PatientExternalCareCenter', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'PROCEDURE', @level1name=N'SP_SavePatientExternalCareCenter';
-- GO
