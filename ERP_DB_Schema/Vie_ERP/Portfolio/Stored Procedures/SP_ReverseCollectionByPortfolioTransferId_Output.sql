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