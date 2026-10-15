
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