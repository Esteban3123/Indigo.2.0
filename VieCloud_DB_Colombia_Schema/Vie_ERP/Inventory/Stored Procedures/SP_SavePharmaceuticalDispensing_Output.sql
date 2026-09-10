-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2021-03-22
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePharmaceuticalDispensing_Output]
	@XmlPharmaceutical XML,
	@XmlDispensingIntegrationMedilaser XML,
	@user VARCHAR(20),
	------------------------------------------------------
	@CodeMessageResult VARCHAR(20) OUTPUT, 
	@ErrorsValidationResult VARCHAR(MAX) OUTPUT, 
	@DispensingIdResult INT OUTPUT, 
	@DispensingCodeResult VARCHAR(20) OUTPUT, 
	@StatusResult TINYINT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @DispensingIntegration TINYINT,
			------------------------------------------------------
			@CodeResult Int,
			@MessageResult VARCHAR(MAX),
			------------------------------------------------------
			@Id INT,
			@Code VARCHAR(20)

	IF @XmlDispensingIntegrationMedilaser.exist('*') > 0
	BEGIN
		--Si el proceso viene de la integración por paciente medilaser se asignan algunos datos
		SELECT	@DispensingIntegration = t.x.value('DispensingIntegration[1]','TINYINT')
		FROM @XmlDispensingIntegrationMedilaser.nodes('/TableXml') t(x)
	END
	ELSE
	BEGIN
		SELECT	@DispensingIntegration = t.x.value('DispensingIntegration[1]','TINYINT')
		FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x)
	END

	IF @DispensingIntegration = 1
	BEGIN
		EXEC [Inventory].[SP_SavePharmaceuticalDispensingNative_Output]
			@XmlPharmaceutical,
			@user,
			---------------------------------------------------------------------------
			@CodeResult OUT,
			@MessageResult OUT,
			------------------------------------------------------
			@Id OUT,
			@Code OUT
	END
	ELSE
	BEGIN
		EXEC [Inventory].[SP_SavePharmaceuticalDispensingIntegrated_Output]
			@XmlPharmaceutical,
			@XmlDispensingIntegrationMedilaser,
			@user,
			---------------------------------------------------------------------------
			@CodeResult OUT,
			@MessageResult OUT,
			------------------------------------------------------
			@Id OUT,
			@Code OUT
	END

	IF @CodeResult <> 0
	BEGIN
		SET @CodeMessageResult = '999'
		SET @ErrorsValidationResult = ISNULL(@MessageResult, 'No se puede dispensar')
		SET @DispensingIdResult = 0
		SET @DispensingCodeResult = ''
		SET @StatusResult = 3
		RETURN
	END

	SET @CodeMessageResult = '0'
	SET @ErrorsValidationResult = ISNULL(@MessageResult, CONCAT('Se guardo correctamente la dispensacion ', @Code))
	SET @DispensingIdResult = ISNULL(@Id,0)
	SET @DispensingCodeResult = ISNULL(@Code, '')
	SET @StatusResult = 1
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que guarda o confirma una dispensación farmacéutica en el módulo de inventario. Recibe la información de la dispensación en formato XML y, según el tipo de integración, enruta el proceso hacia dos subprocedimientos: uno para dispensaciones nativas (SP_SavePharmaceuticalDispensingNative_Output) y otro para dispensaciones integradas con Medilaser (SP_SavePharmaceuticalDispensingIntegrated_Output). Retorna como resultado el identificador y código de la dispensación generada, el estado del proceso y los mensajes de error en caso de fallo, permitiendo al sistema confirmar si la entrega de medicamentos al paciente quedó registrada correctamente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Orquestador que decide si una dispensación farmacéutica se guarda por el flujo nativo o por el flujo integrado con Medilaser, según el indicador DispensingIntegration leído de los XML de entrada, y normaliza el resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe recibirse al menos un XML válido (farmacéutico o de integración Medilaser) que contenga el nodo DispensingIntegration.; El usuario invocador debe estar identificado (@user) para auditoría en los SPs delegados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El indicador DispensingIntegration determina la rama: =1 ejecuta el flujo nativo; cualquier otro valor ejecuta el flujo integrado.; Cuando hay XML de integración Medilaser presente, su DispensingIntegration prevalece sobre el del XML farmacéutico.; Ante error (@CodeResult<>0) siempre se devuelve StatusResult=3 y CodeMessageResult=''999'' sin propagar Id/Code.; Ante éxito siempre se devuelve StatusResult=1 y CodeMessageResult=''0''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; integración Medilaser; dispensación nativa vs integrada', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] OUTPUT params: Si @CodeResult<>0: CodeMessageResult=''999'', ErrorsValidationResult=ISNULL(@MessageResult,''No se puede dispensar''), DispensingIdResult=0, DispensingCodeResult='''', StatusResult=3 y RETURN inmediato.; [RETURN_RESULT] OUTPUT params: Si @CodeResult=0: CodeMessageResult=''0'', ErrorsValidationResult=ISNULL(@MessageResult, CONCAT(''Se guardo correctamente la dispensacion '',@Code)), DispensingIdResult=ISNULL(@Id,0), DispensingCodeResult=ISNULL(@Code,''''), StatusResult=1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @XmlDispensingIntegrationMedilaser.exist(''*'') > 0 → Toma DispensingIntegration desde el XML de integración Medilaser (nodo /TableXml) else Toma DispensingIntegration desde el XML farmacéutico principal (nodo /PharmaceuticalDispensing); si @DispensingIntegration = 1 → Invoca SP_SavePharmaceuticalDispensingNative_Output (flujo nativo, solo XML farmacéutico) else Invoca SP_SavePharmaceuticalDispensingIntegrated_Output (flujo integrado, pasa también el XML de integración Medilaser); si @CodeResult <> 0 tras la ejecución del SP delegado → Retorna CodeMessageResult=''999'', StatusResult=3, IdResult=0, CodeResult='''' y ErrorsValidationResult con el mensaje recibido o ''No se puede dispensar'' else Retorna CodeMessageResult=''0'', StatusResult=1 con Id/Code de la dispensación creada y mensaje ''Se guardo correctamente la dispensacion <Code>''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePharmaceuticalDispensingNative_Output; Inventory.SP_SavePharmaceuticalDispensingIntegrated_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_SavePharmaceuticalDispensing_Output';
-- GO
