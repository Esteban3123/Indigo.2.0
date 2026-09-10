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
			@AdmissionStatus CHAR(1),
			@NormalizedAdmissionNumber CHAR(10) = @AdmissionNumber,
			@UFUEgresoHospitalario CHAR(10),
			@UFUEgresoMedico CHAR(10),
			@AdmissionBedCode CHAR(10),
			@Message VARCHAR(MAX),
			@Message_Output VARCHAR(MAX),
			@LiquidateMasterAccount as BIT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY
		SELECT	@AdmissionType = TIPOINGRE,
				@AdmissionStatus = IESTADOIN,
				@UFUEgresoHospitalario = COALESCE(UFUEGRHOS,''),
				@UFUEgresoMedico = COALESCE(UFUEGRMED,''),
				@AdmissionBedCode = COALESCE(CONVERT(CHAR(10),CODCAMACT ), '')
		FROM [dbo].[ADINGRESO]
		WHERE NUMINGRES = @AdmissionNumber

		SET @LiquidateMasterAccount = (SELECT top 1 ISNULL(LiquidateMasterAccount,0)
										from Billing.SettingsBilling
										where LiquidateMasterAccount =1)

		/********************************************* DOCUMENTOS ORIGEN *********************************************/

		IF EXISTS  (
			SELECT 1
			FROM MedicalHistory.PharmaDose ph
			JOIN HCFARMEPD h ON h.CodeSusceptibleMixingStation = ph.CodeSusceptibleMixingStation
			JOIN HCFARMEPC hc ON hc.CODCONCEC = ph.IDHCFARMEPC
			WHERE h.NUMINGRES = @AdmissionNumber AND ph.DeliveryStatus <> 2 AND hc.ORDESTADO <> '3' AND 
			(h.SENDTO = 2 AND h.PROESTADO <> '3' AND NOT EXISTS (	SELECT 1 
																	FROM MixingStation.RequestPackageDetailStatus rpds
                                                                    WHERE rpds.GroupingCodeDose = ph.GroupingCodeDose))
		) BEGIN
			SET @Message_Output = 'Existen Productos/Servicios de central de mezclas realizados sin generación de órden de servicios'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Solicitudes IntraHospitalarias)
		IF EXISTS  (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacy]
			WHERE Ingreso = @AdmissionNumber AND TIPOSOLICITUD IN (0, 1, 3)
		) BEGIN
			SET @Message_Output = 'Existen Solicitudes IntraHospitalarias por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Devoluciones)
		IF EXISTS  (
			SELECT 1
			FROM [dbo].[ViewDashBoardPharmacyDevolution]
			WHERE Ingreso = @AdmissionNumber
		) BEGIN
			SET @Message_Output = 'Existen Devoluciones por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Paquetes quirurgicos)
		--Se consulta HCFARMEPC directamente en lugar de ViewDashBoardPharmacy_SurgicalPackage:
		--esa vista une con dbo.AGEPROGQX, tabla migrada al ecosistema de agendamiento (goBookings).
		--El filtro de la vista era ORDESTADO = '1' AND IDAGEPROGQX IS NOT NULL; se conserva igual.
		IF EXISTS (
			SELECT 1
			FROM [dbo].[HCFARMEPC] AS PharmacyRequest
			WHERE PharmacyRequest.NUMINGRES = @NormalizedAdmissionNumber
				AND PharmacyRequest.ORDESTADO = '1'
				AND PharmacyRequest.IDAGEPROGQX IS NOT NULL
		) BEGIN
			SET @Message_Output = 'Existen Solicitudes de Paquetes quirurgicos por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--validaciones dashboard farmacia (Quimioterapia)
		--Se consulta HCFARMEPC directamente en lugar de ViewDashBoardPharmacyChemotherapy:
		--esa vista une con dbo.AGASICITA, tabla migrada a agendamiento (goBookings), pero por LEFT JOIN
		--y solo para enriquecer fecha/estado de cita y ciclo; nunca participaba del filtro.
		--El filtro de la vista era ORDESTADO = '1' AND ORDENQUIMIO = 1 AND ADINGRESO.IESTADOIN IN (' ','P').
		--El estado del ingreso es constante respecto a HCFARMEPC, por eso se evalua antes del EXISTS.
		IF @AdmissionStatus IN (' ', 'P')
			AND EXISTS (
				SELECT 1
				FROM [dbo].[HCFARMEPC] AS PharmacyRequest
				WHERE PharmacyRequest.NUMINGRES = @NormalizedAdmissionNumber
					AND PharmacyRequest.ORDESTADO = '1'
					AND PharmacyRequest.ORDENQUIMIO = 1
			) BEGIN
			SET @Message_Output = 'Existen Solicitudes de Quimioterapia por procesar en el Dashboard Farmacia'
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--se buscan las dispensaciones farmaceuticas sin confirmar para este ingreso
		IF EXISTS (
			SELECT 1
			FROM Inventory.PharmaceuticalDispensing
			WHERE AdmissionNumber = @AdmissionNumber AND [Status] = 1
		) BEGIN
			SELECT @Message_Output = STUFF((
					SELECT CHAR(13) + CHAR(10) + Code
					FROM Inventory.PharmaceuticalDispensing
					WHERE [Status] = 1 AND AdmissionNumber = @AdmissionNumber
					ORDER BY Code
					FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			
			SET @Message_Output = 'Las Dispensaciones Farmacéuticas no han sido confirmadas: ' + CHAR(13) + CHAR(10) + @Message_Output
			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		--se buscan las devoluciones de dispensaciones farmaceuticas sin confirmar para este ingreso
		IF EXISTS (
			SELECT 1
			FROM Inventory.PharmaceuticalDispensingDevolution
			WHERE AdmissionNumber = @AdmissionNumber AND [Status] = 1
		)
		BEGIN
			SELECT @Message_Output = STUFF((
					SELECT CHAR(13) + CHAR(10) + Code
					FROM Inventory.PharmaceuticalDispensingDevolution
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
				SELECT 1 FROM [dbo].[HCHISPACA] WHERE NUMINGRES = @AdmissionNumber
			) AND NOT EXISTS (
				SELECT 1 FROM [dbo].[HCHISPACA] WHERE NUMINGRES = @AdmissionNumber AND INDICAPAC IN (9,10,11,12,15,16)
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento de validación que se ejecuta antes de cerrar o egresar un ingreso hospitalario (ambulatorio, urgencias u hospitalización). Verifica que no existan pendientes en farmacia: órdenes de central de mezclas sin procesar, solicitudes intrahospitalarias, devoluciones, paquetes quirúrgicos, quimioterapia, dispensaciones farmacéuticas sin confirmar y devoluciones de dispensaciones sin confirmar, consultando los dashboards de farmacia, las dispensaciones de inventario y las dosis de historia clínica. Retorna un indicador de éxito o fallo junto con un mensaje acumulado que lista todos los bloqueos encontrados, permitiendo al área de facturación y egreso conocer exactamente qué trámites deben resolverse antes de que el cierre del ingreso sea posible.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmissionValidations_Output';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'PROCEDURE', @level1name = N'SP_CloseAdmissionValidations_Output';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Valida si un ingreso puede cerrarse verificando que no existan pendientes farmacéuticos (central de mezclas, dashboards, dispensaciones/devoluciones) y que, según el tipo de ingreso, cuente con alta médica y/o egreso de cama.', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@AdmissionNumber debe corresponder a un ingreso existente en dbo.ADINGRESO para obtener tipo de ingreso, UFU de egreso y cama actual; Debe existir configuración en Billing.SettingsBilling para evaluar la bandera LiquidateMasterAccount (si no, se asume 0 por ISNULL)', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El cierre solo se permite si no existen documentos farmacéuticos pendientes (dosis de central de mezclas con DeliveryStatus<>2, solicitudes intrahospitalarias TIPOSOLICITUD IN (0,1,3), devoluciones, paquetes quirúrgicos o quimioterapia en los dashboards de farmacia); Las dispensaciones farmacéuticas y sus devoluciones deben estar confirmadas (Status<>1) para permitir el cierre; Para ingresos hospitalarios (con cama asignada) se exige siempre egreso de cama (UFUEGRHOS); La exigencia de alta médica hospitalaria (UFUEGRMED) se relaja cuando SettingsBilling.LiquidateMasterAccount=1; Los errores se acumulan separados por CRLF y se reportan todos juntos; el procedimiento nunca lanza RAISERROR sino que retorna por OUTPUT; Si no se cumplen todas las validaciones, @StatusResult=0; en éxito @StatusResult=1 con mensaje vacío', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cierre de ingreso; Ingreso ambulatorio; Ingreso hospitalario; Alta médica; Egreso de cama; Central de mezclas; Dispensación farmacéutica; Devolución farmacéutica; Solicitudes intrahospitalarias; Paquetes quirúrgicos; Quimioterapia; Liquidación de cuenta maestra', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si TIPOINGRE = 1 (Ambulatorio): existe historia clínica HCHISPACA del ingreso, no existe ninguna con INDICAPAC IN (9,10,11,12,15,16) y CHREGESTA tiene REGESTADO=1 → Acumula error: ''No se le ha dado alta médica al paciente''; si TIPOINGRE <> 1 y CODCAMACT (cama actual) no vacío (Hospitalario) y UFUEGRHOS vacío → Acumula error: ''El paciente no tiene Egreso de Cama''; si TIPOINGRE <> 1 y CODCAMACT no vacío y UFUEGRMED vacío y LiquidateMasterAccount = 0 → Acumula error: ''El paciente no tiene Alta médica'' else Si LiquidateMasterAccount=1 se omite la validación de alta médica para hospitalarios; si Al final del flujo, si @Message no está vacío → Devuelve @StatusResult=0 con mensaje consolidado de errores else Devuelve @StatusResult=1 y mensaje vacío; si Excepción capturada en BEGIN CATCH → Devuelve @StatusResult=0 con texto del ERROR_MESSAGE y ERROR_LINE', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'dbo.ADINGRESO; Billing.SettingsBilling; MedicalHistory.PharmaDose; dbo.HCFARMEPC; dbo.ViewDashBoardPharmacy; dbo.ViewDashBoardPharmacyDevolution; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDevolution; dbo.HCHISPACA; dbo.CHREGESTA', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Billing', @level1type=N'PROCEDURE', @level1name=N'SP_CloseAdmissionValidations_Output';
GO
