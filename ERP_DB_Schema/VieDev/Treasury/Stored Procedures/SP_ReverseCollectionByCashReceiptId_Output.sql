-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-17
-- Description:	Procedimiento que se encarga de generar la modificación del recaudo a partir de una reversión de recibo de caja
-- =====================================================================================
CREATE PROCEDURE [Treasury].[SP_ReverseCollectionByCashReceiptId_Output]
	@OperatingUnitId INT,
	@PortfolioNoteId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @CashReceiptId INT,
			@CollectionId INT,
			@AffectBudget INT = 0,
			@BudgetaryValidityId INT,
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT 
			@CashReceiptId = tn.CashReceiptId,
			@CollectionId = c.Id,
			@AffectBudget = 1,
			@BudgetaryValidityId = bv.Id
		FROM Treasury.TreasuryNote tn WITH(NOLOCK)
		JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON tn.CashReceiptId = cr.Id
		JOIN Budget.Collection c WITH(NOLOCK) ON cr.Id = c.EntityId AND c.EntityName = 'CashReceipts'
		LEFT JOIN Budget.BudgetaryValidity bv WITH(NOLOCK) ON c.BudgetaryValidityId = bv.Id AND bv.Status = 2
		WHERE tn.Id = @PortfolioNoteId
		
		/************************************************ VALIDACIONES ***********************************************/

		--SELECT 
		--	 tn.CashReceiptId,
		--	c.Id,
		--	1,
		--	bv.Id
		--	FROM Treasury.TreasuryNote tn WITH(NOLOCK)
		--JOIN Treasury.CashReceipts cr WITH(NOLOCK) ON tn.CashReceiptId = cr.Id
		--LEFT JOIN Budget.Collection c WITH(NOLOCK) ON cr.Id = c.EntityId AND c.EntityName = 'CashReceipts'
		--LEFT JOIN Budget.BudgetaryValidity bv WITH(NOLOCK) ON c.BudgetaryValidityId = bv.Id AND bv.Status = 2
		--WHERE tn.Id = @PortfolioNoteId

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El recaudo fue realizado en vigencia que actualmente no esta activa.'
			RETURN
		END

		IF EXISTS
		(
			SELECT	1
			FROM Treasury.CashReceipts cr
			JOIN Budget.Collection c ON cr.Id = c.EntityId AND c.EntityName = 'CashReceipts'
			WHERE cr.Id = @CashReceiptId
			GROUP BY cr.Id
			HAVING COUNT(*) > 1
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'Existe más de un recaudo asignado al cruce de anticipo.'
			RETURN
		END

		/**************************************** RECONOCIMIENTO PRESUPUESTAL ****************************************/
		
		SELECT @SubXml = CONVERT
		(
			XML, 
			(
				SELECT 
					CollectionModification.*,
					CollectionModificationDetail.*
				FROM 
				(
					SELECT 
						0 Id,						
						'' Code,
						@OperatingUnitId OperatingUnitId,
						@BudgetaryValidityId BudgetaryValidityId,
						tn.NoteDate DocumentDate,
						c.Id CollectionId,
						tn.Code Document,
						'Reversión del Recaudo No. ' + c.Code Observations,
						2 Status,
						tn.Id EntityId,
						tn.Code EntityCode,
						'TreasuryNote' EntityName
					FROM Treasury.TreasuryNote tn
					JOIN Treasury.CashReceipts cr ON tn.CashReceiptId = cr.Id
					JOIN Budget.Collection c ON cr.Id = c.EntityId AND c.EntityName = 'CashReceipts'
					WHERE tn.Id = @PortfolioNoteId
				) CollectionModification
				JOIN
				( 
					SELECT 
						0 CollectionModificationId,
						cd.Id CollectionDetailId,
						1 Nature,
						cd.InitialValue Value
					FROM Budget.CollectionDetail cd
					WHERE cd.CollectionId = @CollectionId
				) CollectionModificationDetail ON CollectionModification.Id = CollectionModificationDetail.CollectionModificationId
				For XML AUTO,TYPE, ELEMENTS
			)
		)

		EXEC [Budget].[SP_SaveCollectionModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

		IF @Code_Output <> 0
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación del recaudo presupuestal')
			RETURN
		END

		/********************************************* REVERSAR PAGO IVA *********************************************/

		UPDATE aric SET Status = 0
		FROM Treasury.TreasuryNote tn
		JOIN Treasury.CashReceiptDetails crd ON tn.CashReceiptId = crd.IdCashReceipt
		JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
		JOIN Portfolio.AccountReceivableIVACollected aric ON crar.Id = aric.CashReceiptAccountReceivableId
		WHERE tn.Id = @PortfolioNoteId

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END