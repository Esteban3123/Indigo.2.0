-- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 15/04/2016
-- Description:	Procedimiento para guardar o confirmar las dispensaciones farmaceuticas
-- =============================================
CREATE PROCEDURE [Inventory].[SP_GeneratePharmaceuticalDispensing]
	@XmlPharmaceutical XML,
	@XmlAnnulateDashboard XML,
	@user VARCHAR(20),
	@XmlDispensingIntegrationMedilaser XML
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @CodeMessageResult VARCHAR(20), 
			@ErrorsValidationResult VARCHAR(MAX), 
			@DispensingIdResult INT, 
			@DispensingCodeResult VARCHAR(20), 
			@StatusResult TINYINT

	--IF @user = '999'
	--BEGIN
	--	IF EXISTS (SELECT 1 FROM @XmlPharmaceutical.nodes('/PharmaceuticalDispensing') t(x))
	--	BEGIN
	--		--- Valido si vienen dispensaciones
	--		EXEC [Inventory].[SP_SavePharmaceuticalDispensing_Output]
	--			@XmlPharmaceutical,
	--			@XmlDispensingIntegrationMedilaser,
	--			@user,
	--			-------------------------------------------
	--			@CodeMessageResult OUT,
	--			@ErrorsValidationResult OUT,
	--			@DispensingIdResult OUT,
	--			@DispensingCodeResult OUT,
	--			@StatusResult OUT
	--	END
	--	ELSE IF EXISTS (SELECT 1 FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashboardPharmacyDetail') t(x)) OR 
	--		EXISTS (SELECT 1 FROM @XmlAnnulateDashboard.nodes('/Main/ViewDashBoardPharmacy_SurgicalPackageDeatils') t(x))
	--	BEGIN
	--		--- Si hay cosas items para anular desde el dashboard
	--		EXEC [Inventory].[SP_AnnulateDashboardPharmacy_Output]
	--			@XmlAnnulateDashboard,
	--			@user,
	--			-------------------------------------------
	--			@CodeMessageResult OUT,
	--			@ErrorsValidationResult OUT,
	--			@DispensingIdResult OUT,
	--			@DispensingCodeResult OUT,
	--			@StatusResult OUT
	--	END
	--END
	--ELSE
	BEGIN
		EXEC Inventory.SP_GeneratePharmaceuticalDispensing_Output 
			@XmlPharmaceutical, 
			@XmlAnnulateDashboard, 
			@user, 
			@XmlDispensingIntegrationMedilaser,
			--------------------------------------------
			@CodeMessageResult OUTPUT, 
			@ErrorsValidationResult OUTPUT, 
			@DispensingIdResult OUTPUT, 
			@DispensingCodeResult OUTPUT, 
			@StatusResult OUTPUT

		SET @ErrorsValidationResult  =  @ErrorsValidationResult + @DispensingCodeResult
	END

	SELECT	@CodeMessageResult AS CodeMessage, 
			@ErrorsValidationResult AS Message, 
			ISNULL(@DispensingIdResult,0) AS DispensingId, 
			ISNULL(@DispensingCodeResult, '') AS DispensingCode, 
			@StatusResult AS [Status]
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento principal para guardar o confirmar dispensaciones farmacéuticas en el módulo de inventario. Recibe la información de la dispensación en formato XML (medicamentos a dispensar, ítems a anular desde el dashboard y datos de integración con Medilaser) junto con el usuario que ejecuta la acción. Orquesta el proceso delegando la lógica de negocio al procedimiento interno SP_GeneratePharmaceuticalDispensing_Output, y devuelve como resultado el código de mensaje, descripción del resultado, identificador y código de la dispensación generada, y el estado de la operación. Se usa desde la interfaz de farmacia para registrar la entrega de medicamentos a pacientes y para anular dispensaciones previamente creadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que delega el guardado/confirmación de dispensaciones farmacéuticas al procedimiento de salida y devuelve un resultado unificado con código de mensaje, errores, identificador y estado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Se debe proveer el XML de dispensación farmacéutica y/o el XML de anulación desde dashboard junto con el XML de integración Medilaser y el usuario que ejecuta la operación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'DispensingId nunca se devuelve NULL (se sustituye por 0).; DispensingCode nunca se devuelve NULL (se sustituye por cadena vacía).; El flujo activo siempre invoca SP_GeneratePharmaceuticalDispensing_Output, sin importar qué XML venga poblado (la lógica condicional por usuario y por tipo de XML está comentada).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; anulación desde dashboard de farmacia; integración Medilaser', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ResultSet: Siempre retorna un único conjunto con CodeMessage, Message (errores concatenados con el código de dispensación), DispensingId (0 si NULL), DispensingCode ('''' si NULL) y Status.; [RETURN_RESULT] ErrorsValidationResult: Tras la ejecución, el mensaje de errores se concatena con el código de dispensación generado: SET @ErrorsValidationResult = @ErrorsValidationResult + @DispensingCodeResult.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_GeneratePharmaceuticalDispensing_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_GeneratePharmaceuticalDispensing';
-- GO
