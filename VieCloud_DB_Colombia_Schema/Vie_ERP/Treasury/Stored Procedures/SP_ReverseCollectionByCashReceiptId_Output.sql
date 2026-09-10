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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que gestiona la reversión presupuestal de un recaudo vinculado a un recibo de caja en tesorería. A partir del identificador de una nota de tesorería (cartera), localiza el recibo de caja y el recaudo presupuestal asociado, valida que la vigencia presupuestaria esté activa y que no exista duplicidad de recaudos, y genera una modificación presupuestal de reversión invocando el procedimiento Budget.SP_SaveCollectionModification_Output con los detalles del recaudo original. Adicionalmente, anula los registros de IVA cobrado vinculados al recibo de caja revertido. Se utiliza en flujos de anulación o reversión de pagos recibidos en caja para mantener la consistencia entre tesorería, cartera y el presupuesto de recaudos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la modificación presupuestal de reversión de un recaudo originado en un recibo de caja, validando vigencia activa y unicidad del recaudo, y revierte el IVA recaudado asociado.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir una Treasury.TreasuryNote con Id=@PortfolioNoteId vinculada a un CashReceipts; El usuario @CodeUser y la unidad operativa @OperatingUnitId deben ser válidos para el SP de modificación de recaudo presupuestal', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesa la reversión presupuestal si el recibo de caja tiene un Budget.Collection asociado (AffectBudget=1); La vigencia presupuestal debe estar en Status=2 (activa) para permitir la reversión; Debe existir exactamente un Budget.Collection por CashReceiptId; más de uno aborta la operación; La modificación de recaudo se genera con Status=2 y Nature=1 (débito) usando el InitialValue de cada CollectionDetail; Las observaciones de la modificación siempre incluyen ''Reversión del Recaudo No. '' + código del Collection; EntityName en Budget.Collection siempre se filtra por ''CashReceipts'' y la modificación se vincula como ''TreasuryNote''; Los registros de IVA recaudado asociados al recibo se desactivan (Status=0) tras una reversión exitosa', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversión de recibo de caja; Recaudo presupuestal; Modificación de recaudo (CollectionModification); Vigencia presupuestal activa; Cruce de anticipo; IVA recaudado sobre cuentas por cobrar; Nota de tesorería', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.CollectionModification: Vía EXEC Budget.SP_SaveCollectionModification_Output con XML que contiene encabezado (Status=2, EntityName=''TreasuryNote'') y detalles (Nature=1, Value=InitialValue de CollectionDetail) cuando el recaudo tiene vigencia activa y un único Collection; [UPDATE] Portfolio.AccountReceivableIVACollected: Cuando la modificación presupuestal se genera correctamente, marca Status=0 en todos los registros de IVA recaudado vinculados al recibo de caja a través de CashReceiptDetails y CashReceiptAccountReceivable; [RETURN_RESULT] OUTPUT: Devuelve @CodeResult=0 y @MessageResult vacío en éxito; 999 con mensaje específico ante validación fallida o error', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe recaudo presupuestal asociado al recibo de caja (no se obtuvo Collection vinculado a CashReceipts) → Retorna CodeResult=0 y mensaje vacío sin ejecutar reversión presupuestal; si El recaudo está asociado a una vigencia presupuestal cuyo Status<>2 (no activa) o no existe BudgetaryValidity → Retorna 999 con mensaje ''El recaudo fue realizado en vigencia que actualmente no esta activa.''; si Existe más de un Budget.Collection asociado al mismo CashReceiptId (COUNT>1 agrupado por cr.Id) → Retorna 999 con mensaje ''Existe más de un recaudo asignado al cruce de anticipo.''; si SP_SaveCollectionModification_Output devuelve Code_Output<>0 → Retorna 999 con el mensaje devuelto por el SP o ''No se pudo generar la modificación del recaudo presupuestal'' else Continúa con la reversión del IVA en AccountReceivableIVACollected; si Se produce una excepción dentro del TRY → Retorna 999 con ERROR_MESSAGE() concatenado con el número de línea', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollectionModification_Output', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Treasury.TreasuryNote; Treasury.CashReceipts; Budget.Collection; Budget.BudgetaryValidity; Budget.CollectionDetail; Treasury.CashReceiptDetails; Treasury.CashReceiptAccountReceivable; Portfolio.AccountReceivableIVACollected', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByCashReceiptId_Output';
-- GO
