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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el recaudo presupuestario a partir de un recibo de caja identificado por su ID, afectando el presupuesto de ingresos cuando las cuentas por cobrar (facturas) asociadas al recibo así lo requieren. Valida que exista una vigencia presupuestaria activa para el año del recibo, que la dependencia esté parametrizada en la configuración de cartera, y que las facturas no pertenezcan a vigencias superiores a la del recibo ni carezcan de reconocimiento contable. Compone información de los detalles del recibo de caja (CashReceiptDetails), la aplicación de pagos a cuentas por cobrar (CashReceiptAccountReceivable y AccountReceivable), la vigencia presupuestaria (BudgetaryValidity) y la dependencia presupuestal (Dependency) configurada en cartera (SettingPortfolio) para determinar si corresponde ejecutar el recaudo o retornar un mensaje de error de negocio.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el recaudo presupuestal y, si aplica, los reconocimientos de vigencias anteriores a partir de un recibo de caja, registrando el IVA recaudado y validando consistencia entre las facturas asociadas y la vigencia presupuestal vigente.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir al menos una cuenta por cobrar asociada al recibo de caja con AffectBudget=1 para que el procedimiento ejecute lógica presupuestal; en caso contrario retorna código 0 sin acción.; Debe existir una vigencia presupuestal (Budget.BudgetaryValidity) con Status IN (1,2) cuyo Year coincida con el año de la fecha del recibo de caja.; Debe existir una dependencia parametrizada en Portfolio.SettingPortfolio para la OperatingUnit y un equivalente en Budget.Dependency para la vigencia detectada.; Las facturas asociadas al recibo no pueden ser de vigencias posteriores al año del documento del recibo.; Las facturas de la vigencia actual con AffectBudget=1 deben tener un RecognitionId asignado.; Las facturas de vigencias anteriores deben tener configurado en Contract.CareGroup el rubro de recuperación (PortfolioRecoveryBudgetId o AccountReceivablePreviousValidityBudgetId) con equivalencia válida de RevenueType y Category en la vigencia destino.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El IVA insertado en AccountReceivableIVACollected nunca excede el ValueTax de la factura menos el IVA ya recaudado (Status=1).; Solo se procesa lógica presupuestal sobre cuentas por cobrar marcadas con AffectBudget=1.; El reconocimiento presupuestal de vigencias anteriores se construye con AutomaticCollection=1 y Status=2, marcando que es generado automáticamente desde el recibo de caja.; El Collection generado siempre se asocia a la entidad EntityName=''CashReceipts'' con EntityId=Id del recibo y Status=2.; Si la diferencia de vigencia es exactamente 1 año se usa AccountReceivablePreviousValidityBudgetId; en otro caso se usa PortfolioRecoveryBudgetId del CareGroup.; Cualquier excepción se captura en CATCH y se devuelve 999 con el mensaje y línea de error, sin propagar la excepción.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.AccountReceivableIVACollected: Para cada CashReceiptAccountReceivable cuya factura tenga ValueTax > IVA ya recaudado (SUM aric.Value con Status=1), inserta un registro con Value = MIN(crar.Value, ValueTax - IVA recaudado previo) y Status=1.; [EXEC] Budget.SP_SaveCollection_Output: Cuando existen cuentas por cobrar afectando presupuesto cuyo BudgetHeader pertenece a la vigencia presupuestal detectada, arma XML con el Collection (Status=2, CollectionType=1) y sus detalles agrupados por RecognitionDetail e invoca SP_SaveCollection_Output.; [EXEC] Budget.SP_SaveRecognition_Output: Para cada AccountReceivable con AffectBudget=1 y AccountReceivableDate de vigencia anterior al año del recibo, itera y genera un reconocimiento presupuestal (RecognitonType=1, AutomaticCollection=1, Status=2) usando el rubro AccountReceivablePreviousValidityBudgetId si la diferencia es 1 año o PortfolioRecoveryBudgetId en otro caso.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve 0 con mensaje vacío en éxito; 999 con mensaje específico ante validación fallida o error en SPs invocados; en CATCH devuelve 999 con ERROR_MESSAGE() y línea.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AffectBudget = 0 (ningún AR del recibo afecta presupuesto) → Retorna CodeResult=0 sin generar recaudo ni reconocimientos.; si @BudgetaryValidityId IS NULL → Retorna 999 ''No existe una vigencia activa asociada a la fecha del Recibo de Caja.''; si @DependencyId IS NULL → Retorna 999 ''No se ha parametrizado una dependencia en los parámetros de cartera.''; si Existe AR con AffectBudget=1 cuyo YEAR(AccountReceivableDate) > @DocumentYear → Retorna 999 listando facturas de vigencias superiores.; si Existe AR de la vigencia actual con AffectBudget=1 y RecognitionId IS NULL → Retorna 999 listando facturas sin reconocimiento.; si Existe AR de vigencia anterior cuya equivalencia de RevenueType o Category en la vigencia destino no exista (rtd.Id IS NULL OR cd.Id IS NULL) → Retorna 999 listando facturas sin rubro de recuperación asociado.; si Existen AR cuyo BudgetHeader pertenece a la vigencia detectada → Construye XML y ejecuta Budget.SP_SaveCollection_Output.; si Existen AR de vigencias anteriores → Itera fila por fila ejecutando Budget.SP_SaveRecognition_Output por cada AR.; si @Code_Output <> 0 tras invocar SP de Collection o Recognition → Retorna 999 propagando @Message_Output o mensaje genérico de fallo. else Acumula @Message_Output y continúa el flujo.', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollection_Output; Budget.SP_SaveRecognition_Output', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Treasury', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByCashReceiptId_Output';
-- GO
