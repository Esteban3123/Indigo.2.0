-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-17
-- Description:	Procedimiento que se encarga de generar el recaudo a partir de un recibo de caja
-- =====================================================================================
CREATE PROCEDURE [Treasury].[SP_GenerateCollectionByCashReceiptId_Output]
	@OperatingUnitId INT,
	@CashReceiptId INT,
	@CodeUser VARCHAR(20),
	------------------------------------------------------
	@CodeResult INT OUTPUT,
	@MessageResult VARCHAR(MAX) OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	/*************************************************** VARIABLES ***************************************************/

	DECLARE @AffectBudget INT = 0,
			@DocumentYear INT,
			@BudgetaryValidityId INT,
			@BudgetaryValidityStatus TINYINT,
			@DependencyId INT,
			------------------------------
			@Message VARCHAR(MAX),
			------------------------------
			@SubXml XML,
			@Code_Output INT,
			@Message_Output VARCHAR(MAX)

	-------------------------------------------------------------------------------------------------------------------

	BEGIN TRY

		SELECT	@AffectBudget = 1
		FROM Treasury.CashReceiptDetails crd
		JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
		JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
		WHERE crd.IdCashReceipt = @CashReceiptId
			AND ar.AffectBudget = 1

		SELECT	@DocumentYear = YEAR(cr.DocumentDate),
				@BudgetaryValidityId = bv.Id
		FROM Treasury.CashReceipts cr
		LEFT JOIN Budget.BudgetaryValidity bv ON YEAR(cr.DocumentDate) = bv.Year AND bv.Status IN (1, 2)
		WHERE cr.Id = @CashReceiptId

		SELECT	@DependencyId = dd.Id
		FROM Portfolio.SettingPortfolio sp
		JOIN Budget.Dependency d ON sp.DependencyId = d.Id
		JOIN Budget.Dependency dd ON d.Code = dd.Code AND dd.BudgetaryValidityId = @BudgetaryValidityId
		WHERE sp.OperatingUnitId = @OperatingUnitId
		
		/************************************************ VALIDACIONES ***********************************************/

		IF @AffectBudget = 0
		BEGIN
			SELECT	@CodeResult = 0,
					@MessageResult = ''
			RETURN
		END

		IF @BudgetaryValidityId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'No existe una vigencia activa asociada a la fecha del Recibo de Caja.'
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = 'No se ha parametrizado una dependencia en los parámetros de cartera.'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Treasury.CashReceiptDetails crd
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
			WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) > @DocumentYear
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Treasury.CashReceiptDetails crd
						JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
						JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
						WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
							AND YEAR(ar.AccountReceivableDate) > @DocumentYear
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas son de vigencias superiores a la del Recibo de Caja (' + CAST(@DocumentYear AS VARCHAR(4)) + '): ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Treasury.CashReceiptDetails crd
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
			WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) = @DocumentYear
				AND ar.RecognitionId IS NULL
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Treasury.CashReceiptDetails crd
						JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
						JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
						WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
							AND YEAR(ar.AccountReceivableDate) > @DocumentYear
							AND ar.RecognitionId IS NULL
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas de vigencias actual no tienen un reconocimiento asociado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Treasury.CashReceiptDetails crd
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
			LEFT JOIN Contract.CareGroup cg ON ar.CareGroupId = cg.Id
			LEFT JOIN Budget.Budget b ON IIF(@DocumentYear - 1 = YEAR(ar.AccountReceivableDate), cg.AccountReceivablePreviousValidityBudgetId, cg.PortfolioRecoveryBudgetId) = b.Id
			------------------------------------------------------------------
			LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
			LEFT JOIN Budget.Category c ON b.CategoryId = c.Id
			LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
			---------------------------------------------------------------
			LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @BudgetaryValidityId
			LEFT JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @BudgetaryValidityId
			LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @BudgetaryValidityId
			-------------------------------------------------------------
			WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) < @DocumentYear
				AND (rtd.Id IS NULL OR cd.Id IS NULL)
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Treasury.CashReceiptDetails crd
						JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
						JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
						LEFT JOIN Contract.CareGroup cg ON ar.CareGroupId = cg.Id
						LEFT JOIN Budget.Budget b ON IIF(@DocumentYear - 1 = YEAR(ar.AccountReceivableDate), cg.AccountReceivablePreviousValidityBudgetId, cg.PortfolioRecoveryBudgetId) = b.Id
						------------------------------------------------------------------
						LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
						LEFT JOIN Budget.Category c ON b.CategoryId = c.Id
						LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
						---------------------------------------------------------------
						LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @BudgetaryValidityId
						LEFT JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @BudgetaryValidityId
						LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @BudgetaryValidityId
						-------------------------------------------------------------
						WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
							AND ar.AccountReceivableType = 2
							AND YEAR(ar.AccountReceivableDate) < @DocumentYear
							AND (rtd.Id IS NULL OR cd.Id IS NULL)
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas de vigencias anteriores no tienen un rubro de recuperación asociado: ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END		

		/*********************************************  GENERAR PAGO IVA *********************************************/

		INSERT INTO Portfolio.AccountReceivableIVACollected
		(
			AccountReceivableId, CashReceiptAccountReceivableId, Value, Status
		)
		SELECT ar.Id, crar.Id, IIF(crar.Value > (i.ValueTax - ISNULL(aric.Value, 0)), (i.ValueTax - ISNULL(aric.Value, 0)), crar.Value), 1
		FROM Treasury.CashReceiptDetails crd
		JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
		JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
		JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		LEFT JOIN
		(
			SELECT	aric.AccountReceivableId,
					SUM(aric.Value) Value
			FROM Portfolio.AccountReceivableIVACollected aric
			WHERE aric.Status = 1
			GROUP BY aric.AccountReceivableId
		) aric ON ar.Id = aric.AccountReceivableId
		WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
			AND i.ValueTax > ISNULL(aric.Value, 0)

		/*******************************************  RECAUDO PRESUPUESTAL *******************************************/
		
		IF EXISTS
		(
			SELECT 1
			FROM Treasury.CashReceiptDetails crd
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
			JOIN Budget.Budget b ON ar.BudgetId = b.Id
			JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
			WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
				AND bh.BudgetaryValidityId = @BudgetaryValidityId
		)
		BEGIN
			SELECT @SubXml = CONVERT
			(
				XML, 
				(
					SELECT 
						Collection.*,
						CollectionDetail.*
					FROM 
					(
						SELECT 
							0 Id,						
							'' Code,
							@OperatingUnitId OperatingUnitId,
							@BudgetaryValidityId BudgetaryValidityId,
							cr.DocumentDate DocumentDate,
							'Recaudo generado desde el Recibo de Caja No. ' + cr.Code Observations,
							cr.IdThirdParty ThirdPartyId,
							2 Status,
							cr.Id EntityId,
							cr.Code EntityCode,
							'CashReceipts' EntityName
						FROM Treasury.CashReceipts cr
						WHERE cr.Id = @CashReceiptId
					) Collection
					JOIN
					( 
						SELECT
							0 CollectionId,
							rd.Id RecognitionDetailId, 
							1 CollectionType,
							ROUND(SUM(crar.Value - ISNULL(aric.Value, 0)), 0) InitialValue
						FROM Treasury.CashReceiptDetails crd
						JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
						JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
						JOIN Budget.Budget b ON ar.BudgetId = b.Id
						JOIN Budget.RecognitionDetail rd ON ar.RecognitionId = rd.RecognitionId AND b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
						LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON crar.Id = aric.CashReceiptAccountReceivableId
						WHERE crd.IdCashReceipt = @CashReceiptId
						GROUP BY rd.Id
					) CollectionDetail ON Collection.Id = CollectionDetail.CollectionId
					For xml AUTO,TYPE, ELEMENTS
				)
			)

			EXEC [Budget].[SP_SaveCollection_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

			IF @Code_Output <> 0
			BEGIN
				SELECT	@CodeResult = 999,
						@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el recaudo presupuestal')
				RETURN
			END

			SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
		END

		/*****************************  RECONOCIMIENTO PRESUPUESTAL VIGENCIAS ANTERIORES *****************************/
		
		IF EXISTS
		(
			SELECT 1
			FROM Treasury.CashReceiptDetails crd
			JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
			JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
			WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) < @DocumentYear
		)
		BEGIN
			DECLARE @AccountReceivableRows INT = 1,
					@AccountReceivableId INT = 0,
					@Year INT,
					@ThirdPartyId INT,
					@InvoiceNumber VARCHAR(20)

			WHILE @AccountReceivableRows > 0
			BEGIN
				SELECT TOP 1
					@AccountReceivableId = ar.Id,
					@Year = YEAR(ar.AccountReceivableDate),
					@ThirdPartyId = ar.ThirdPartyId,
					@InvoiceNumber = ar.InvoiceNumber
				FROM Treasury.CashReceiptDetails crd
				JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
				JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
				WHERE crd.IdCashReceipt = @CashReceiptId AND ar.AffectBudget = 1
					AND YEAR(ar.AccountReceivableDate) < @DocumentYear
					AND ar.Id > @AccountReceivableId
				ORDER BY ar.Id

				SET @AccountReceivableRows = @@ROWCOUNT
				IF @AccountReceivableRows = 0 
				BEGIN
					BREAK
				END

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
								cr.DocumentDate DocumentDate,
								@InvoiceNumber Document,
								CONCAT('Reconocimiento generado por el Recibo de Caja ' + cr.Code + ' para la Factura ', @InvoiceNumber, ' de la vigencia ', @Year) Observations,
								1 RecognitonType,
								@ThirdPartyId ThirdPartyId,
								@DependencyId DependencyId,
								1 AutomaticCollection,
								2 Status,
								cr.Id EntityId,
								cr.Code EntityCode,
								'CashReceipts' EntityName
							FROM Treasury.CashReceipts cr
							WHERE cr.Id = @CashReceiptId
						) Recognition
						JOIN
						( 
							SELECT
								0 RecognitionId,
								cd.Id CategoryId, 
								rtd.Id RevenueTypeId,
								SUM(crar.Value - ISNULL(aric.Value, 0)) InitialValue
							FROM Treasury.CashReceiptDetails crd
							JOIN Treasury.CashReceiptAccountReceivable crar ON crd.Id = crar.CashReceiptDetailId
							JOIN Portfolio.AccountReceivable ar ON crar.AccountReceivableId = ar.Id
							LEFT JOIN Contract.CareGroup cg ON ar.CareGroupId = cg.Id
							LEFT JOIN Budget.Budget b ON IIF(@DocumentYear - 1 = @Year, cg.AccountReceivablePreviousValidityBudgetId, cg.PortfolioRecoveryBudgetId) = b.Id
							------------------------------------------------------------------
							LEFT JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
							LEFT JOIN Budget.Category c ON b.CategoryId = c.Id
							LEFT JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
							---------------------------------------------------------------
							LEFT JOIN Budget.RevenueType rtd ON rt.Type = rtd.Type AND rt.Code = rtd.Code AND rtd.BudgetaryValidityId = @BudgetaryValidityId
							LEFT JOIN Budget.FinancialSource fsd ON fs.Code = fsd.Code AND fsd.BudgetaryValidityId = @BudgetaryValidityId
							LEFT JOIN Budget.Category cd ON fsd.Id = cd.FinancialSourceId AND c.Code = cd.Code AND cd.BudgetaryValidityId = @BudgetaryValidityId
							-------------------------------------------------------------
							LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON crar.Id = aric.CashReceiptAccountReceivableId
							WHERE crd.IdCashReceipt = @CashReceiptId AND ar.Id = @AccountReceivableId
							GROUP BY cd.Id, rtd.Id
						) RecognitionDetail ON Recognition.Id = RecognitionDetail.RecognitionId
						For xml AUTO,TYPE, ELEMENTS
					)
				)

				EXEC [Budget].[SP_SaveRecognition_Output] @SubXml, '', @CodeUser, @Code_Output OUT, @Message_Output OUT, NULL, NULL

				IF @Code_Output <> 0
				BEGIN
					SELECT	@CodeResult = 999,
							@MessageResult = ISNULL(@Message_Output, 'No se pudo generar el reconocimiento presupuestal')
					RETURN
				END

				SET @Message = ISNULL(@Message, '') + IIF(@Message_Output = '', '', IIF(ISNULL(@Message, '') = '', '', CHAR(13) + CHAR(10)) + @Message_Output)
			END
		END

		/************************************************* RESULTADO *************************************************/

		SELECT	@CodeResult = 0,
				@MessageResult = ISNULL(@Message_Output, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END