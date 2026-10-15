-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-07
-- Description:	Procedimiento que se encarga de generar el reconocimiento a partir de un radicado
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionByRadicateInvoiceId_Output]
	@OperatingUnitId INT,
	@RadicateInvoiceId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @DocumentDate DATETIME,
			@DependencyId INT,
			@AffectBudget INT,
			@BudgetaryValidityId INT,
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX),
			@RecognitionId INT

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT @DocumentDate = ri.ConfirmDate
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @RadicateInvoiceId

		SELECT @DependencyId = DependencyId
		FROM Portfolio.SettingPortfolio
		WHERE OperatingUnitId = @OperatingUnitId

		--Se actualiza la información de interfaz presupuestal de la factura 
		UPDATE ar
			SET ar.CareGroupId = cg.Id,
				ar.AffectBudget = cg.AffectBudget,
				ar.BudgetId = cg.BillingBudgetId
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		LEFT JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		JOIN Contract.CareGroup cg ON ISNULL(i.CareGroupId, ar.CareGroupId) = cg.Id
		WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0

		SELECT TOP 1 
			@BudgetaryValidityId = bv.Id
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Budget.Budget b ON ar.BudgetId = b.Id
		JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
		JOIN Budget.BudgetaryValidity bv ON bh.BudgetaryValidityId = bv.Id AND bv.Status IN (1, 2)
		WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
		ORDER BY bv.Status DESC
		
		/************************************************ VALIDACIONES ***********************************************/

		IF NOT EXISTS
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
		)
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Debe indicar en los parámetros de cuentas por cobrar la dependecia a usar en el reconocimiento de ingresos.'
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las facturas asociadas al radicado no tienen asignado un rubro presupuestal o el rubro se encuentra asignado a una vigencia no activa.'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1 
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 0
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 0
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas no tienen habilitada la interfaz presupuestal: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
				AND ar.RecognitionId IS NOT NULL
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
							AND ar.RecognitionId IS NOT NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas ya tienen un reconocimiento: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1 
				AND b.Id IS NULL
		)
		BEGIN
			SELECT @MessageResult = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + rid.InvoiceNumber
						FROM Portfolio.RadicateInvoiceD rid
						JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
						LEFT JOIN Budget.Budget b ON ar.BudgetId = b.Id
						WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
							AND b.Id IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas no tienen asignado un rubro presupuestal: ' + CHAR(13) + CHAR(10) + ISNULL(@MessageResult, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.RadicateInvoiceD rid
			JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
			JOIN Budget.Budget b ON ar.BudgetId = b.Id
			JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id AND bh.Type = 1
			WHERE RadicateInvoiceCId = @RadicateInvoiceId AND rid.Devolution = 0 AND ar.AffectBudget = 1
				AND bh.BudgetaryValidityId <> @BudgetaryValidityId
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Los detalles corresponden a más de una vigencia presupuestal.'
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
						ISNULL(ri.ConfirmDate, ri.DocumentDate) DocumentDate,
						ri.RadicatedConsecutive Document,
						'Reconocimiento generado desde la Radicacion de Cuentas No. ' + CAST(ri.RadicatedConsecutive AS VARCHAR(20)) Observations,
						1 RecognitonType,
						c.ThirdPartyId,
						@DependencyId DependencyId,
						0 AutomaticCollection,
						2 Status,
						ri.Id EntityId,
						ri.RadicatedConsecutive EntityCode,
						'RadicateInvoiceC' EntityName
					FROM Portfolio.RadicateInvoiceC ri
					JOIN Common.Customer c ON ri.CustomerId = c.Id
					WHERE ri.Id = @RadicateInvoiceId
				) Recognition
				JOIN
				( 
					SELECT
						0 RecognitionId,
						b.CategoryId, 
						b.RevenueTypeId,
						SUM(ar.Balance) InitialValue
					FROM Portfolio.RadicateInvoiceD rid
					JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
					JOIN Budget.Budget b ON ar.BudgetId = b.Id
					JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
					WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND ar.AffectBudget = 1 AND rid.Devolution = 0
					GROUP BY b.CategoryId, b.RevenueTypeId
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
		
		UPDATE ri
			SET ri.RecognitionId = @RecognitionId
		FROM Portfolio.RadicateInvoiceC ri
		WHERE ri.Id = @RadicateInvoiceId

		UPDATE ar
			SET ar.RecognitionId = @RecognitionId
		FROM Portfolio.RadicateInvoiceD rid
		JOIN Portfolio.AccountReceivable ar ON rid.InvoiceNumber = ar.InvoiceNumber AND ar.AccountReceivableType = 2
		JOIN Budget.Budget b ON ar.BudgetId = b.Id
		JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
		WHERE rid.RadicateInvoiceCId = @RadicateInvoiceId AND ar.AffectBudget = 1 AND rid.Devolution = 0

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END