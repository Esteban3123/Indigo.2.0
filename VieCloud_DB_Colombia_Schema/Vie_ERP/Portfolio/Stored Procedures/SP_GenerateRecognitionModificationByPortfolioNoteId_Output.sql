-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-16
-- Description:	Procedimiento que se encarga de generar el recaudo a partir de un cruce de anticipos
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateRecognitionModificationByPortfolioNoteId_Output]
	@PortfolioNoteId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @OperatingUnitId INT,
			@PortfolioNoteCode VARCHAR(20),
			@PortfolioNoteDate DATETIME,
			@Nature TINYINT,
			------------------------------
			@BudgetaryValidityId INT,
			@RecognitionRows INT,
			@RecognitionId INT,
			@AdjusmentValue DECIMAL(18,2),
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT	@OperatingUnitId = pn.OperatingUnitId,
				@PortfolioNoteCode = pn.Code,
				@PortfolioNoteDate = pn.NoteDate,
				@Nature = IIF(pn.Nature = 1, 2, 1)
		FROM Portfolio.PortfolioNote pn
		WHERE pn.Id = @PortfolioNoteId
		
		/*********************************  MODIFICACION RECONOCIMIENTO PRESUPUESTAL *********************************/
		
		SET @RecognitionRows = 1
		SET @RecognitionId = 0

		WHILE @RecognitionRows > 0
		BEGIN
			SELECT TOP 1
				@RecognitionId = r.Id,
				@BudgetaryValidityId = r.BudgetaryValidityId,
				@AdjusmentValue =  SUM(pnara.AdjusmentValue - ISNULL(i.ValueTax, 0))
			FROM Portfolio.PortfolioNoteAccountReceivableAdvance pnara
			JOIN Portfolio.AccountReceivable ar ON pnara.AccountReceivableId = ar.Id
			JOIN Budget.Recognition r ON ar.RecognitionId = r.Id
			JOIN Budget.BudgetaryValidity bv ON r.BudgetaryValidityId = bv.Id AND YEAR(ar.AccountReceivableDate) = bv.Year AND bv.Status IN (1, 2)
			LEFT JOIN Billing.Invoice i WITH (NOLOCK) ON i.InvoiceNumber = ar.InvoiceNumber
			WHERE pnara.PortfolioNoteId = @PortfolioNoteId
				AND r.Id > @RecognitionId
			GROUP BY r.Id, r.BudgetaryValidityId
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
							@PortfolioNoteDate DocumentDate,
							@RecognitionId RecognitionId,
							@PortfolioNoteCode Document,
							'Modificación generada desde la Nota de Cartera No. ' + @PortfolioNoteCode Observations,
							2 Status,
							@PortfolioNoteId EntityId,
							@PortfolioNoteCode EntityCode,
							'PortfolioNote' EntityName
					) RecognitionModification
					JOIN
					( 
						SELECT
							0 RecognitionModificationId,
							rd.Id RecognitionDetailId,
							rd.CategoryId,
							rd.RevenueTypeId,
							@Nature Nature,
							@AdjusmentValue Value
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
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera modificaciones de reconocimiento presupuestal a partir del cruce de anticipos registrados en una nota de cartera. Para cada reconocimiento presupuestal vinculado a las cuentas por cobrar de la nota, calcula el valor neto de ajuste (descontando impuestos de factura) y construye un XML estructurado con el encabezado y detalle de la modificación, el cual envía al procedimiento de presupuesto para su persistencia. Se usa en el proceso de recaudo y conciliación de cartera cuando se aplican anticipos contra obligaciones de cobro, garantizando que el reconocimiento presupuestal quede actualizado con los ajustes correspondientes a cada vigencia presupuestal activa.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera modificaciones de reconocimiento presupuestal a partir de los anticipos cruzados en una nota de cartera, invirtiendo la naturaleza del movimiento por cada reconocimiento afectado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir la nota de cartera referenciada en Portfolio.PortfolioNote; Los anticipos en Portfolio.PortfolioNoteAccountReceivableAdvance deben estar enlazados a cuentas por cobrar con reconocimiento presupuestal; La vigencia presupuestal asociada al reconocimiento debe coincidir con el año de la cuenta por cobrar y estar en estado 1 o 2 (activa/abierta)', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El valor de ajuste por reconocimiento se calcula como SUM(pnara.AdjusmentValue - ISNULL(i.ValueTax,0)), descontando el IVA de la factura asociada cuando exista; La naturaleza de la modificación es siempre la opuesta a la de la nota de cartera (1↔2); Solo se procesan reconocimientos cuya vigencia presupuestal está en estado 1 o 2 y cuyo año coincide con el año de la cuenta por cobrar; Las modificaciones se procesan en orden ascendente de Recognition.Id, una por iteración; El estado de la modificación creada siempre es 2; La trazabilidad queda registrada con EntityName=''PortfolioNote'', EntityId y EntityCode apuntando a la nota de cartera origen', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Nota de cartera; Anticipo; Cuenta por cobrar; Reconocimiento presupuestal; Modificación de reconocimiento; Vigencia presupuestal; Naturaleza del movimiento; Factura; IVA (ValueTax); Cruce de anticipos', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Budget.RecognitionModification: Por cada reconocimiento (r.Id) afectado por los anticipos cruzados en la nota, se construye un XML con cabecera y detalle y se delega la inserción a Budget.SP_SaveRecognitionModification_Output con Status=2 y observación ''Modificación generada desde la Nota de Cartera No. '' + código de la nota; [RAISERROR] Budget.RecognitionModification: Si SP_SaveRecognitionModification_Output retorna Code <> 0, se aborta retornando @CodeResult=999 con el mensaje del SP hijo o ''No se pudo generar la modificación del reconocimiento presupuestal''; [RETURN_RESULT] @MessageResult: En CATCH retorna @CodeResult=999 con ERROR_MESSAGE() + '' - Linea: '' + ERROR_LINE(); en caso exitoso retorna @CodeResult=0 con la concatenación de mensajes de las modificaciones generadas', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pn.Nature = 1 → @Nature se asigna a 2 (naturaleza invertida para la modificación) else @Nature se asigna a 1; si Iteración WHILE: existe siguiente Recognition con Id > @RecognitionId asociado a la nota → Construye XML y ejecuta SP_SaveRecognitionModification_Output para ese reconocimiento else Sale del bucle (BREAK cuando @@ROWCOUNT = 0); si @Code_Output <> 0 tras invocar SP_SaveRecognitionModification_Output → Retorna inmediatamente con código 999 y propaga el mensaje de error else Acumula el mensaje en @Message y continúa con el siguiente reconocimiento', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveRecognitionModification_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Portfolio.PortfolioNote; Portfolio.PortfolioNoteAccountReceivableAdvance; Portfolio.AccountReceivable; Budget.Recognition; Budget.BudgetaryValidity; Billing.Invoice; Budget.RecognitionDetail', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateRecognitionModificationByPortfolioNoteId_Output';
-- GO
