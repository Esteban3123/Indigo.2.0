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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que genera la reversión o modificación presupuestal asociada a una cuenta por cobrar específica. Cuando una cuenta por cobrar afecta el presupuesto, verifica si existe un reconocimiento presupuestal vigente y, de ser así, revierte primero el recaudo asociado a ese reconocimiento (llamando a SP_SaveCollectionModification_Output) y luego revierte el reconocimiento mismo (llamando a SP_SaveRecognitionModification_Output). Se utiliza principalmente en el proceso de anulación de facturas o cuentas de cobro, garantizando que el impacto presupuestal de la cuenta por cobrar quede correctamente deshecho en el módulo de presupuesto.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera la modificación (reversión) del reconocimiento presupuestal —y, si existe, del recaudo asociado— a partir de la anulación de una cuenta por cobrar que afecta presupuesto.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La cuenta por cobrar debe existir en Portfolio.AccountReceivable (se leen OperatingUnitId, Code, AnnulmentDate, AccountReceivableDate, AffectBudget y RecognitionId).; Solo procede si la cuenta por cobrar afecta presupuesto (AffectBudget <> 0).; Debe existir un Reconocimiento (Budget.Recognition) ligado a una BudgetaryValidity cuyo Year coincida con el año de AccountReceivableDate y cuyo Status esté en (1,2).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las modificaciones generadas se crean con Status=2 (estado fijo de reversión).; Las líneas de detalle se generan con Nature=1 y Value igual al InitialValue del detalle original (reversión total al valor inicial).; Las modificaciones se vinculan a la cuenta por cobrar como entidad origen (EntityName=''AccountReceivable'', EntityId=AccountReceivableId).; La fecha del documento de modificación es siempre la AnnulmentDate de la cuenta por cobrar.; Si la cuenta por cobrar no afecta presupuesto, el SP nunca crea modificaciones presupuestales.; La modificación del recaudo, cuando aplica, se realiza antes que la del reconocimiento.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cuenta por cobrar; Anulación de cuenta por cobrar; Reconocimiento presupuestal; Recaudo presupuestal; Vigencia presupuestal; Modificación presupuestal (reversión); Afectación presupuestal', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.CollectionModification: Si existe un Collection (EntityName=''Recognition'') ligado al RecognitionId, se construye un XML con Status=2, DocumentDate=AnnulmentDate de la cuenta por cobrar, Observations=''Reversión del Recaudo No. ''+c.Code y EntityName=''AccountReceivable'', y se invoca Budget.SP_SaveCollectionModification_Output para insertar la modificación del recaudo.; [INSERT] Budget.CollectionModificationDetail: Por cada Budget.CollectionDetail del recaudo se genera una línea con Nature=1 y Value=InitialValue, persistida vía Budget.SP_SaveCollectionModification_Output.; [INSERT] Budget.RecognitionModification: Cuando la vigencia presupuestal cumple (Year y Status IN (1,2)), se invoca Budget.SP_SaveRecognitionModification_Output con XML Status=2, DocumentDate=AnnulmentDate, Observations=''Reversión del Reconocimiento No. ''+r.Code, EntityName=''AccountReceivable'' para registrar la reversión del reconocimiento.; [INSERT] Budget.RecognitionModificationDetail: Por cada Budget.RecognitionDetail del reconocimiento se incluye un detalle con CategoryId, RevenueTypeId, Nature=1 y Value=InitialValue, vía Budget.SP_SaveRecognitionModification_Output.; [RETURN_RESULT] @MessageResult: Si SP_SaveCollectionModification_Output o SP_SaveRecognitionModification_Output devuelven Code <> 0, retorna CodeResult=999 con el mensaje de error correspondiente; si todo OK retorna CodeResult=0.; [RETURN_RESULT] @MessageResult: En cualquier excepción capturada por CATCH, retorna CodeResult=999 con ERROR_MESSAGE() y la línea del error.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AffectBudget = 0 → Sale inmediatamente con CodeResult=0 y MessageResult vacío, sin generar modificaciones presupuestales. else Continúa evaluando vigencia y reconocimiento.; si Existe BudgetaryValidity con Year = año de AccountReceivableDate y Status IN (1,2) ligada al Recognition → Procede a generar la modificación del reconocimiento (y del recaudo si aplica). else No genera modificaciones y retorna CodeResult=0 con el mensaje acumulado.; si Existe Budget.Collection con EntityId=RecognitionId y EntityName=''Recognition'' → Genera primero la modificación del recaudo antes de la modificación del reconocimiento. else Salta la reversión de recaudo y procede solo con la del reconocimiento.; si @Code_Output <> 0 tras llamar al SP de modificación (recaudo o reconocimiento) → Aborta con CodeResult=999 y mensaje devuelto por el SP llamado o un texto por defecto.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollectionModification_Output; Budget.SP_SaveRecognitionModification_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.AccountReceivable; Budget.BudgetaryValidity; Budget.Recognition; Budget.Collection; Budget.CollectionDetail; Budget.RecognitionDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByAccountReceivableId_Output';
-- GO
