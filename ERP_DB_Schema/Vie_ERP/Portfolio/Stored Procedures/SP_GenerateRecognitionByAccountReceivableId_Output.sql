-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-04
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionByAccountReceivableId_Output]
	@OperatingUnitId INT,
	@AccountReceivableId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @DependencyId INT,
			@AffectBudget INT,			
			@BudgetaryValidityId INT,
			@AccountReceivableType TINYINT,
			@LiquidationType TINYINT,
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@RecognitionId INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		IF NOT EXISTS (SELECT 1 FROM Billing.SettingsBilling WITH (NOLOCK) WHERE BudgetInterface = 1)
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		-------------------------------------------------------------------------------------------------------------------

		SELECT	@AffectBudget = ar.AffectBudget,
				@BudgetaryValidityId = bv.Id,
				@AccountReceivableType = ar.AccountReceivableType,
				@LiquidationType = IIF(ar.AccountReceivableType = 1, 1, cg.LiquidationType)
		FROM Portfolio.AccountReceivable ar		
		LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
		LEFT JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
		LEFT JOIN Budget.BudgetaryValidity bv ON bh.BudgetaryValidityId = bv.Id AND bv.Status IN (1, 2) AND YEAR(ar.AccountReceivableDate) = bv.Year
		LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		LEFT JOIN Contract.CareGroup cg ON i.CareGroupId = cg.Id
		WHERE ar.Id = @AccountReceivableId

		SELECT
			@DependencyId = IIF(@AccountReceivableType = 1, BasicBillingDependencyId, DependencyId)
		FROM Billing.SettingsBilling
		WHERE IdOperatingUnit = @OperatingUnitId
		
		/************************************************ VALIDACIONES ***********************************************/

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Debe indicar en los parámetros de facturación la dependecia a usar en el reconocimiento de ingresos.'
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El grupo de atención no tienen asignado un rubro presupuestal o el rubro se encuentra asignado a una vigencia no activa.'
			RETURN
		END

		/**************************************** RECONOCIMIENTO PRESUPUESTAL ****************************************/
		
		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					Recognition.*,
					RecognitionDetail.*
				FROM 
				(
					SELECT 
						0 Id,
						@OperatingUnitId OperatingUnitId,
						'' Code,
						@BudgetaryValidityId BudgetaryValidityId,
						ar.AccountReceivableDate DocumentDate,
						ar.InvoiceNumber Document,
						CASE ar.AccountReceivableType
							WHEN 4 THEN 'Reconocimiento generado del pagaré asociado ' + IIF(@LiquidationType = 1, 'a la Factura', 'al Control de Servicio') + ' No. ' + ar.InvoiceNumber 
							WHEN 6 THEN 'Reconocimiento generado del valor del paciente asociado ' + IIF(@LiquidationType = 1, 'a la Factura', 'al Control de Servicio') + ' No. ' + ar.InvoiceNumber 
							ELSE 'Reconocimiento generado desde ' + IIF(@LiquidationType = 1, 'la Factura', 'el Control de Servicio') + ' No. ' + ar.InvoiceNumber 
						END Observations,
						1 RecognitonType,
						ar.ThirdPartyId,
						@DependencyId DependencyId,
						IIF(@LiquidationType = 1, 0, IIF(ar.AccountReceivableType = 6, 1, 0)) AutomaticCollection,
						2 Status,
						ar.Id EntityId,
						ar.InvoiceNumber EntityCode,
						'AccountReceivable' EntityName
					FROM Portfolio.AccountReceivable ar
					WHERE ar.Id = @AccountReceivableId
				) Recognition
				JOIN
				( 
					SELECT
						0 RecognitionId,
						b.CategoryId, 
						b.RevenueTypeId,
						(ar.Value - ISNULL(i.ValueTax, 0)) InitialValue
					FROM Portfolio.AccountReceivable ar
					JOIN Budget.Budget b ON ar.BudgetId = b.Id
					LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
					WHERE ar.Id = @AccountReceivableId
				) RecognitionDetail ON Recognition.Id = RecognitionDetail.RecognitionId
				For xml AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Budget].[SP_SaveRecognition_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, @RecognitionId OUT, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
			RETURN
		END

		/**************************************** ACTUALIZACION DE REGISTROS *****************************************/
		
		UPDATE ar 
			SET RecognitionId = @RecognitionId
		FROM Portfolio.AccountReceivable ar
		WHERE ar.Id = @AccountReceivableId

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END