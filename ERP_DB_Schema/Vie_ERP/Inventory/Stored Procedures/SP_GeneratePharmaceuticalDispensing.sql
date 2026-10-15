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