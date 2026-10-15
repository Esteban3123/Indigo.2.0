-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-12-04
-- Description:	Validaciones al momento de realizar el cierre de un ingreso
-- =============================================
CREATE PROCEDURE [Billing].[SP_CloseAdmissionValidations_Output]
	@AdmissionNumber VARCHAR(20),
	@StatusResult BIT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @AdmissionType INT,
			@UFUEgresoHospitalario CHAR(10),
			@UFUEgresoMedico CHAR(10),
			@AdmissionBedCode CHAR(10),
			@Message VARCHAR(MAX),
			@Message_Output VARCHAR(MAX),
			@LiquidateMasterAccount as BIT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SELECT	@AdmissionType = TIPOINGRE,
				@UFUEgresoHospitalario = COALESCE(UFUEGRHOS,''),
				@UFUEgresoMedico = COALESCE(UFUEGRMED,''),
				@AdmissionBedCode = COALESCE(CONVERT(CHAR(10),CODCAMACT ), '')
		FROM [dbo].[ADINGRESO] WITH (NOLOCK) 
		WHERE NUMINGRES = @AdmissionNumber

		SET @LiquidateMasterAccount = (SELECT top 1 ISNULL(LiquidateMasterAccount,0)
										from Billing.SettingsBilling
										where LiquidateMasterAccount =1)

		/********************************************* DOCUMENTOS ORIGEN *********************************************/

		IF EXISTS  (
			SELECT 1 
			FROM MedicalHistory.PharmaDose pd WITH(NOLOCK)
			INNER JOIN HCFARMEPC hc WITH(NOLOCK) ON pd.IDHCFARMEPC = hc.CODCONCEC
			WHERE HC.NUMINGRES = @AdmissionNumber AND pd.DeliveryStatus <> 2--IN (0, 1)
		) BEGIN
			SET @Message_Output = 'Existen Productos/Servicios de central de mezclas realizados sin generación de órden de servicios'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Solicitudes IntraHospitalarias)
		IF EXISTS  (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacy] WITH (NOLOCK) 
			WHERE Ingreso = @AdmissionNumber AND TIPOSOLICITUD IN (0, 1, 3)
		) BEGIN
			SET @Message_Output = 'Existen Solicitudes IntraHospitalarias por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Devoluciones)
		IF EXISTS  (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacyDevolution] WITH (NOLOCK) 
			WHERE Ingreso = @AdmissionNumber
		) BEGIN
			SET @Message_Output = 'Existen Devoluciones por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Paquetes quirurgicos)
		IF EXISTS (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacy_SurgicalPackage] WITH (NOLOCK) 
			WHERE Ingreso = @AdmissionNumber
		) BEGIN
			SET @Message_Output = 'Existen Solicitudes de Paquetes quirurgicos por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Quimioterapia)
		IF EXISTS (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacyChemotherapy] WITH (NOLOCK) 
			WHERE Ingreso = @AdmissionNumber
		) BEGIN
			SET @Message_Output = 'Existen Solicitudes de Quimioterapia por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--se buscan las dispensaciones farmaceuticas sin confirmar para este ingreso
		IF EXISTS (
			SELECT 1
			FROM Inventory.PharmaceuticalDispensing WITH (NOLOCK) 
			WHERE AdmissionNumber = @AdmissionNumber AND [Status] = 1
		) BEGIN
			SELECT @Message_Output = STUFF((
					SELECT CHAR(13) + CHAR(10) + Code
					FROM Inventory.PharmaceuticalDispensing WITH (NOLOCK)
					WHERE [Status] = 1 AND AdmissionNumber = @AdmissionNumber
					ORDER BY Code
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message_Output = 'Las Dispensaciones Farmacéuticas no han sido confirmadas: ' + CHAR(13) + CHAR(10) + @Message_Output
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--se buscan las devoluciones de dispensaciones farmaceuticas sin confirmar para este ingreso
		IF EXISTS (
			SELECT 1
			FROM Inventory.PharmaceuticalDispensingDevolution WITH (NOLOCK)
			WHERE AdmissionNumber = @AdmissionNumber AND [Status] = 1
		)
		BEGIN
			SELECT @Message_Output = STUFF((
					SELECT CHAR(13) + CHAR(10) + Code
					FROM Inventory.PharmaceuticalDispensingDevolution WITH (NOLOCK)
					WHERE [Status] = 1 AND AdmissionNumber = @AdmissionNumber
					ORDER BY Code
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message_Output = 'Las Devoluciones de Dispensaciones Farmacéuticas no han sido confirmadas: ' + CHAR(13) + CHAR(10) + @Message_Output
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/************************************************** INGRESO **************************************************/

		IF @AdmissionType = 1 
		BEGIN
			--Ambulatorio
			IF EXISTS (
				SELECT 1 FROM [dbo].[HCHISPACA] WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber
			) AND NOT EXISTS (
				SELECT 1 FROM [dbo].[HCHISPACA] WITH (NOLOCK) WHERE NUMINGRES = @AdmissionNumber AND INDICAPAC IN (9,10,11,12,15,16) 
			) and ((select count(*) from CHREGESTA  WHERE NUMINGRES = @AdmissionNumber and REGESTADO = 1) > 0)
			BEGIN
				SET @Message_Output = CONCAT('No se puede Cerrar el Ingreso (', @AdmissionNumber, '):  No se le ha dado alta médica al paciente')
				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END
		ELSE
		BEGIN
			IF @AdmissionBedCode <> '' BEGIN
				--Hospitalario
				 IF @UFUEgresoHospitalario = '' BEGIN
			 		SET @Message_Output = CONCAT('No se puede Cerrar el Ingreso (', @AdmissionNumber, '):  El paciente no tiene Egreso de Cama')
			 		SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
				 END

				IF @UFUEgresoMedico = '' and @LiquidateMasterAccount =0 BEGIN
					SET @Message_Output = CONCAT('No se puede Cerrar el Ingreso (', @AdmissionNumber, '):  El paciente no tiene Alta médica')
					SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
				END
			END
		END

		/************************************************* RESULTADO *************************************************/

		IF ISNULL(@Message, '') <> ''
		BEGIN
			SELECT	@StatusResult = CONVERT(BIT, 0), 
					@MessageResult = 'Se presentaron los siguientes errores: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END

		SELECT	@StatusResult = CONVERT(BIT, 1), 
				@MessageResult = ''
	END TRY
	BEGIN CATCH
		SELECT	@StatusResult = CONVERT(BIT, 0), 
				@MessageResult = 'Error validando cierre ingreso: '+ ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
		RETURN
	END CATCH
END