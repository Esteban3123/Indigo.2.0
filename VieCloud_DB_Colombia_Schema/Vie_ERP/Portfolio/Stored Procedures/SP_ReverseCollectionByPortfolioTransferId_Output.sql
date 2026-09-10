-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-11
-- Description:	Procedimiento que se encarga de generar la modificación del recaudo a partir de una reversión de cruce de anticipos
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_ReverseCollectionByPortfolioTransferId_Output]
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

	DECLARE @NoteCode VARCHAR(20),
			@NoteDate DATETIME,
			@PortfolioNoteYear INT,
			@PortfolioTransferId INT,
			@PortfolioTransferYear INT,
			@AffectBudget INT = 0,
			@BudgetaryValidityId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT TOP 1 
				@NoteCode = pn.Code,
				@NoteDate = pn.NoteDate,
				@PortfolioNoteYear = YEAR(pn.NoteDate),
				@PortfolioTransferId = pn.PortfolioTransferId,
				@PortfolioTransferYear = YEAR(pt.DocumentDate),
				@AffectBudget = IIF(c.Id IS NULL AND r.Id IS NULL, 0, 1)
		FROM Portfolio.PortfolioNote pn
		JOIN Portfolio.PortfolioTransfer pt ON pn.PortfolioTransferId = pt.Id
		LEFT JOIN Budget.Collection c ON pt.Id = c.EntityId AND c.EntityName = 'PortfolioTransfer'
		LEFT JOIN Budget.Recognition r ON pt.Id = r.EntityId AND r.EntityName = 'PortfolioTransfer'
		WHERE pn.Id = @PortfolioNoteId
		
		/************************************************ VALIDACIONES ***********************************************/

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @PortfolioNoteYear <> @PortfolioTransferYear
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'No se puede reversar un cruce de anticipos de otra vigencia.'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioNote pn
			JOIN Portfolio.PortfolioTransfer pt ON pn.PortfolioTransferId = pt.Id
			LEFT JOIN Budget.Collection c ON pt.Id = c.EntityId AND c.EntityName = 'PortfolioTransfer'
			LEFT JOIN Budget.Recognition r ON pt.Id = r.EntityId AND r.EntityName = 'PortfolioTransfer'
			LEFT JOIN Budget.BudgetaryValidity bv ON ISNULL(c.BudgetaryValidityId, r.BudgetaryValidityId) = bv.Id AND bv.Status IN (1, 2)
			WHERE pn.Id = @PortfolioNoteId AND bv.Id IS NULL
		)
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'El recaudo fue realizado en vigencia que actualmente no esta activa.'
			RETURN
		END

		/***********************************  REVERSION DE RECAUDOS PRESUPUESTALES ***********************************/
		
		DECLARE @CollectionRows INT = 1,
				@CollectionId INT = 0

		WHILE @CollectionRows > 0
		BEGIN
			SELECT TOP 1
				@CollectionId = c.Id,
				@BudgetaryValidityId = c.BudgetaryValidityId
			FROM 
			(
				SELECT c.*
				FROM Budget.Collection c 
				WHERE c.EntityId = @PortfolioTransferId AND c.EntityName = 'PortfolioTransfer'
					AND c.Id > @CollectionId
				
				UNION ALL

				SELECT c.*
				FROM Budget.Recognition r
				JOIN Budget.Collection c ON r.Id = c.EntityId AND c.EntityName = 'Recognition'
				WHERE r.EntityId = @PortfolioTransferId AND r.EntityName = 'PortfolioTransfer'
					AND c.Id > @CollectionId
			) c
			ORDER BY c.Id

			SET @CollectionRows = @@ROWCOUNT
			IF @CollectionRows = 0 
			BEGIN
				BREAK
			END

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
							@NoteDate DocumentDate,
							c.Id CollectionId,
							@NoteCode Document,
							'Reversión del Recaudo No. ' + c.Code Observations,
							2 Status,
							@PortfolioNoteId EntityId,
							@NoteCode EntityCode,
							'PortfolioNote' EntityName
						FROM Budget.Collection c
						WHERE c.Id = @CollectionId
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

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/***********************************  REVERSION DE RECONOCIMIENTOS PRESUPUESTALES ***********************************/
		
		DECLARE @RecognitionRows INT = 1,
				@RecognitionId INT = 0

		WHILE @RecognitionRows > 0
		BEGIN
			SELECT TOP 1
				@RecognitionId = r.Id,
				@BudgetaryValidityId = r.BudgetaryValidityId
			FROM Budget.Recognition r
			WHERE r.EntityId = @PortfolioTransferId AND r.EntityName = 'PortfolioTransfer'
				AND r.Id > @RecognitionId
			ORDER BY r.Id

			SET @RecognitionRows = @@ROWCOUNT
			IF @RecognitionRows = 0 
			BEGIN
				BREAK
			END

			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						RecognitionModification.*,
						RecognitionModificationDetail.*
					FROM 
					(
						SELECT 
							0 Id,						
							'' Code,
							@OperatingUnitId OperatingUnitId,
							@BudgetaryValidityId BudgetaryValidityId,
							@NoteDate DocumentDate,
							r.Id RecognitionId,
							@NoteCode Document,
							'Reversión del Reconocimiento No. ' + r.Code Observations,
							2 Status,
							@PortfolioNoteId EntityId,
							@NoteCode EntityCode,
							'PortfolioNote' EntityName
						FROM Budget.Recognition r
						WHERE r.Id = @RecognitionId
					) RecognitionModification
					JOIN
					( 
						SELECT 
							0 RecognitionModificationId,
							rd.Id RecognitionDetailId,
							rd.CategoryId,
							rd.RevenueTypeId,
							1 Nature,
							rd.InitialValue Value
						FROM Budget.RecognitionDetail rd
						WHERE rd.RecognitionId = @RecognitionId
					) RecognitionModificationDetail ON RecognitionModification.Id = RecognitionModificationDetail.RecognitionModificationId
					For XML AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveRecognitionModification_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar la modificación del reconocimiento presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/********************************************* REVERSAR PAGO IVA *********************************************/

		UPDATE aric SET Status = 0
		FROM Portfolio.PortfolioNote pn
		JOIN Portfolio.PortfolioTransferDetail ptd ON pn.PortfolioTransferId = ptd.PortfolioTrasferId
		JOIN Portfolio.AccountReceivableIVACollected aric ON ptd.Id = aric.PortfolioTransferDetailId
		WHERE pn.Id = @PortfolioNoteId

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reversa o anulación presupuestal de un cruce de anticipos de cartera. A partir de una nota de cartera (PortfolioNoteId) asociada a un traslado de cartera (PortfolioTransfer), el procedimiento verifica si ese cruce generó recaudos (Collection) o reconocimientos (Recognition) presupuestales; si los generó, produce las modificaciones de reversión correspondientes llamando a los SPs de modificación de recaudo y reconocimiento presupuestal. Valida que el cruce pertenezca a la misma vigencia presupuestal activa antes de proceder, y retorna un código y mensaje de resultado indicando éxito o la causa del rechazo.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la reversión presupuestal (modificaciones de recaudo y reconocimiento) y revierte el IVA recaudado cuando se reversa un cruce de anticipos de cartera, validando que pertenezca a la misma vigencia activa.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La PortfolioNote indicada debe existir y estar vinculada a un PortfolioTransfer; Debe existir un usuario válido que ejecute la operación (CodeUser); Los SPs Budget.SP_SaveCollectionModification_Output y Budget.SP_SaveRecognitionModification_Output deben estar disponibles', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesa la reversión si existe al menos un Collection o Recognition vinculado al PortfolioTransfer (EntityName=''PortfolioTransfer''); La nota de cartera y la transferencia de cartera deben pertenecer al mismo año calendario para permitir reversión; La vigencia presupuestal del recaudo/reconocimiento debe estar en estado 1 o 2 (activa); Cada modificación de recaudo se genera con Status=2 y Nature=1, tomando InitialValue del detalle original como valor de reversión; Las modificaciones de reversión se asocian a la PortfolioNote como entidad origen (EntityName=''PortfolioNote''); Se procesan tanto Collections directas sobre PortfolioTransfer como Collections asociadas a Recognitions de ese PortfolioTransfer; Cualquier error captura ERROR_MESSAGE y ERROR_LINE devolviendo 999', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reversión de cruce de anticipos; Recaudo presupuestal; Reconocimiento presupuestal; Vigencia presupuestal activa; Nota de cartera; Transferencia de cartera; IVA recaudado en cuentas por cobrar', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.CollectionModification: Por cada Collection asociada al PortfolioTransfer (directa o vía Recognition) invoca SP_SaveCollectionModification_Output con Status=2, Nature=1 y Value = CollectionDetail.InitialValue, generando una modificación de reversión vinculada a la PortfolioNote; [INSERT] Budget.RecognitionModification: Por cada Recognition con EntityName=''PortfolioTransfer'' y EntityId=PortfolioTransferId invoca SP_SaveRecognitionModification_Output con Status=2, Nature=1 y Value = RecognitionDetail.InitialValue, generando la modificación de reversión vinculada a la PortfolioNote; [UPDATE] Portfolio.AccountReceivableIVACollected: Marca Status=0 en todos los registros de IVA recaudado vinculados a los PortfolioTransferDetail de la PortfolioTransfer asociada a la nota, reversando el pago de IVA; [RETURN_RESULT] OUTPUT: Retorna CodeResult=0 con mensaje acumulado en éxito, o 999 con mensaje específico en cada validación fallida o error capturado por CATCH', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si No existe Collection ni Recognition asociado al PortfolioTransfer (AffectBudget = 0) → Retorna CodeResult=0 con mensaje vacío sin ejecutar reversión else Continúa con validaciones y reversión presupuestal; si El año de la nota de cartera difiere del año del documento de transferencia (vigencias distintas) → Retorna 999 ''No se puede reversar un cruce de anticipos de otra vigencia.''; si La vigencia presupuestal asociada al recaudo/reconocimiento no está en Status IN (1,2) → Retorna 999 ''El recaudo fue realizado en vigencia que actualmente no esta activa.''; si SP_SaveCollectionModification_Output retorna Code_Output <> 0 → Retorna 999 con el mensaje del SP o ''No se pudo generar la modificación del recaudo presupuestal''; si SP_SaveRecognitionModification_Output retorna Code_Output <> 0 → Retorna 999 con el mensaje del SP o ''No se pudo generar la modificación del reconocimiento presupuestal''', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollectionModification_Output; Budget.SP_SaveRecognitionModification_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Portfolio.PortfolioTransfer; Budget.Collection; Budget.Recognition; Budget.BudgetaryValidity; Budget.CollectionDetail; Budget.RecognitionDetail; Portfolio.PortfolioTransferDetail; Portfolio.AccountReceivableIVACollected', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_ReverseCollectionByPortfolioTransferId_Output';
-- GO
