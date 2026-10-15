-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2021-03-12
-- Description:	Procedimiento que se encarga de generar la modificacion de reconocimiento a partir de una cuenta por cobrar
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionModificationByAccountReceivableId_Output]
	@AccountReceivableId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @OperatingUnitId INT,
			@AccountReceivableCode VARCHAR(20),			
			@AccountReceivableAnnulmentDate DATETIME,
			@AccountReceivableYear INT,
			------------------------------
			@AffectBudget INT,
			@RecognitionId INT,
			@CollectionId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT	@OperatingUnitId = ar.OperatingUnitId,
				@AccountReceivableCode = ar.Code,
				@AccountReceivableAnnulmentDate = ar.AnnulmentDate,
				@AccountReceivableYear = YEAR(ar.AccountReceivableDate),
				------------------------------
				@AffectBudget = ar.AffectBudget,
				@RecognitionId = ar.RecognitionId
		FROM Portfolio.AccountReceivable ar
		WHERE ar.Id = @AccountReceivableId
		
		/************************************************ VALIDACIONES ***********************************************/

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		/**************************************** MODIFICACION PRESUPUESTALES ****************************************/

		IF EXISTS 
		(
			SELECT	1
			FROM Budget.BudgetaryValidity bv
			JOIN Budget.Recognition r ON bv.Id = r.BudgetaryValidityId
			WHERE @AccountReceivableYear = bv.Year AND bv.Status IN (1, 2) AND r.Id = @RecognitionId
		) 
		BEGIN
			/**************************************** MODIFICACION RECAUDO ****************************************/

			IF EXISTS 
			(
				SELECT	1
				FROM Budget.Recognition r
				JOIN Budget.Collection c ON r.Id = c.EntityId AND 'Recognition' = c.EntityName
				WHERE r.Id = @RecognitionId
			) 
			BEGIN
				SELECT	@CollectionId = c.Id
				FROM Budget.Recognition r
				JOIN Budget.Collection c ON r.Id = c.EntityId AND 'Recognition' = c.EntityName
				WHERE r.Id = @RecognitionId

				-------------------------------------------------------------------------------------------------------

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
								c.BudgetaryValidityId,
								@AccountReceivableAnnulmentDate DocumentDate,
								c.Id CollectionId,
								@AccountReceivableCode Document,
								'Reversión del Recaudo No. ' + c.Code Observations,
								2 Status,
								@AccountReceivableId EntityId,
								@AccountReceivableCode EntityCode,
								'AccountReceivable' EntityName
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
						FOR XML AUTO,TYPE, ELEMENTS
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

			/**************************************** MODIFICACION RECONOCIMIENTO ****************************************/		
		
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
							r.BudgetaryValidityId,
							@AccountReceivableAnnulmentDate DocumentDate,
							r.Id RecognitionId,
							@AccountReceivableCode Document,
							'Reversión del Reconocimiento No. ' + r.Code Observations,
							2 Status,
							@AccountReceivableId EntityId,
							@AccountReceivableCode EntityCode,
							'AccountReceivable' EntityName
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
					FOR XML AUTO,TYPE, ELEMENTS
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

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END