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