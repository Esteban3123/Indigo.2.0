-- =====================================================================================
-- Author: Miguel Angel Fonseca Castro
-- Create date: 2019-10-07
-- Description:	Procedimiento que se encarga de generar el recaudo a partir de un cruce de anticipos
-- =====================================================================================
CREATE PROCEDURE [Portfolio].[SP_GenerateCollectionByPortfolioTransferId_Output]
	@OperatingUnitId INT,
	@PortfolioTransferId INT,
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
		FROM Portfolio.PortfolioTransferDetail ptd
		JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
		WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
			AND ar.AffectBudget = 1
		-- set @AffectBudget = 0

		PRINT CONCAT('@@AffectBudget: ',@AffectBudget)

		SELECT	@DocumentYear = YEAR(pt.DocumentDate),
				@BudgetaryValidityId = bv.Id
		FROM Portfolio.PortfolioTransfer pt
		LEFT JOIN Budget.BudgetaryValidity bv ON YEAR(pt.DocumentDate) = bv.Year AND bv.Status IN (1, 2)
		WHERE pt.Id = @PortfolioTransferId

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
					@MessageResult = 'No existe una vigencia activa asociada a la fecha del Cruce de Anticipo vs CxC.'
			RETURN
		END

		IF @DependencyId IS NULL
		BEGIN
			SELECT	@CodeResult = 999,
					@MessageResult = '(<<)No se ha parametrizado una dependencia en los parámetros de cartera.'
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioTransferDetail ptd
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) > @DocumentYear
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Portfolio.PortfolioTransferDetail ptd
						JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
						WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
							AND YEAR(ar.AccountReceivableDate) > @DocumentYear
						FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			SELECT	@CodeResult = 999,
					@MessageResult = 'Las siguientes facturas son de vigencias superiores a la del traslado (Vigencia Traslado: ' + CAST(@DocumentYear AS VARCHAR(4)) + '): ' + CHAR(13) + CHAR(10) + ISNULL(@Message, '')
			RETURN
		END

		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioTransferDetail ptd
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) = @DocumentYear
				AND ar.RecognitionId IS NULL
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Portfolio.PortfolioTransferDetail ptd
						JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
						WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
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
			FROM Portfolio.PortfolioTransferDetail ptd
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
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
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
				AND YEAR(ar.AccountReceivableDate) < @DocumentYear
				AND (rtd.Id IS NULL OR cd.Id IS NULL)
		)
		BEGIN
			SELECT @Message = STUFF((
						SELECT DISTINCT CHAR(13) + CHAR(10) + ' - ' + ar.InvoiceNumber + ', Vigencia: ' + CAST(YEAR(ar.AccountReceivableDate) AS VARCHAR(4))
						FROM Portfolio.PortfolioTransferDetail ptd
						JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
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
						WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
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
			AccountReceivableId, PortfolioTransferDetailId, Value, Status
		)
		SELECT ar.Id, ptd.Id, IIF(ptd.Value > (i.ValueTax - ISNULL(aric.Value, 0)), (i.ValueTax - ISNULL(aric.Value, 0)), ptd.Value), 1
		FROM Portfolio.PortfolioTransferDetail ptd
		JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
		JOIN Billing.Invoice i ON ar.InvoiceId = i.Id
		LEFT JOIN
		(
			SELECT	aric.AccountReceivableId,
					SUM(aric.Value) Value
			FROM Portfolio.AccountReceivableIVACollected aric
			WHERE aric.Status = 1
			GROUP BY aric.AccountReceivableId
		) aric ON ar.Id = aric.AccountReceivableId
		WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
			AND i.ValueTax > ISNULL(aric.Value, 0)

		/*******************************************  RECAUDO PRESUPUESTAL *******************************************/
		
		IF EXISTS
		(
			SELECT 1
			FROM Portfolio.PortfolioTransferDetail ptd
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			JOIN Budget.Budget b ON ar.BudgetId = b.Id
			JOIN Budget.BudgetHeader bh ON b.BudgetHeaderId = bh.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
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
							pt.DocumentDate DocumentDate,
							'Recaudo generado desde el Cruce de Anticipo Vs CxC No. ' + pt.Code Observations,
							pt.ThirdPartyId,
							2 Status,
							pt.Id EntityId,
							pt.Code EntityCode,
							'PortfolioTransfer' EntityName
						FROM Portfolio.PortfolioTransfer pt
						WHERE pt.Id = @PortfolioTransferId
					) Collection
					JOIN
					( 
						SELECT
							0 CollectionId,
							rd.Id RecognitionDetailId, 
							1 CollectionType,
							SUM(ptd.Value - ISNULL(aric.Value, 0)) InitialValue
						FROM Portfolio.PortfolioTransferDetail ptd
						JOIN Portfolio.AccountReceivable ar On ptd.AccountReceivableId = ar.Id
						JOIN Budget.Budget b ON ar.BudgetId = b.Id
						JOIN Budget.RecognitionDetail rd ON ar.RecognitionId = rd.RecognitionId AND b.CategoryId = rd.CategoryId AND b.RevenueTypeId = rd.RevenueTypeId
						LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON ptd.Id = aric.PortfolioTransferDetailId
						WHERE ptd.PortfolioTrasferId = @PortfolioTransferId
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
			FROM Portfolio.PortfolioTransferDetail ptd
			JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
			WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
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
				FROM Portfolio.PortfolioTransferDetail ptd
				JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
				WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.AffectBudget = 1
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
								pt.DocumentDate DocumentDate,
								@InvoiceNumber Document,
								CONCAT('Reconocimiento generado por el cruce de anticipos ' + pt.Code + ' para la Factura ', @InvoiceNumber, ' de la vigencia ', @Year) Observations,
								1 RecognitonType,
								@ThirdPartyId ThirdPartyId,
								@DependencyId DependencyId,
								1 AutomaticCollection,
								2 Status,
								pt.Id EntityId,
								pt.Code EntityCode,
								'PortfolioTransfer' EntityName
							FROM Portfolio.PortfolioTransfer pt
							WHERE pt.Id = @PortfolioTransferId
						) Recognition
						JOIN
						( 
							SELECT
								0 RecognitionId,
								cd.Id CategoryId, 
								rtd.Id RevenueTypeId,
								SUM(ptd.Value - ISNULL(aric.Value, 0)) InitialValue
							FROM Portfolio.PortfolioTransferDetail ptd
							JOIN Portfolio.AccountReceivable ar ON ptd.AccountReceivableId = ar.Id
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
							LEFT JOIN Portfolio.AccountReceivableIVACollected aric ON ptd.Id = aric.PortfolioTransferDetailId
							WHERE ptd.PortfolioTrasferId = @PortfolioTransferId AND ar.Id = @AccountReceivableId
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
				@MessageResult = ISNULL(@Message, '')
	END TRY
	BEGIN CATCH	
		SELECT	@CodeResult = 999,
				@MessageResult = ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(50))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el recaudo (cobro) a partir de un cruce de anticipos contra cuentas por cobrar, dado un identificador de transferencia de cartera. Consulta el detalle de la transferencia (PortfolioTransferDetail) y las cuentas por cobrar (AccountReceivable) para determinar si alguna factura afecta el presupuesto; si es así, valida que exista una vigencia presupuestaria activa (BudgetaryValidity) para el año del documento, que la dependencia esté parametrizada en la configuración de cartera (SettingPortfolio), y que las facturas involucradas cumplan reglas de vigencia y reconocimiento antes de proceder. Retorna un código y mensaje de resultado indicando éxito o el motivo de rechazo, siendo utilizado en el proceso de cierre y recuperación de cartera del módulo financiero-presupuestal.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'PROCEDURE', @level1name = N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el recaudo presupuestal (y reconocimientos de vigencias anteriores) derivado de un cruce de anticipos contra cuentas por cobrar, registrando además el IVA recaudado asociado.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El PortfolioTransfer indicado debe existir y tener detalle (PortfolioTransferDetail) asociado a cuentas por cobrar.; Al menos una cuenta por cobrar del traslado debe tener AffectBudget = 1 para que el procedimiento ejecute lógica presupuestal; en caso contrario retorna éxito sin acción.; Debe existir una BudgetaryValidity con Status IN (1,2) para el año (YEAR(DocumentDate)) del traslado.; Debe existir una Dependency parametrizada en Portfolio.SettingPortfolio para la OperatingUnit, equivalente (mismo Code) en la vigencia presupuestal vigente.; Las facturas con AffectBudget=1 no deben ser de vigencia (año) superior a la del traslado.; Las facturas de la vigencia actual deben tener RecognitionId asignado.; Las facturas de vigencias anteriores deben tener parametrizado un rubro de recuperación (RevenueType y Category equivalentes en la vigencia destino, vía CareGroup.AccountReceivablePreviousValidityBudgetId o PortfolioRecoveryBudgetId).', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo procesa cuentas por cobrar con AffectBudget = 1; las demás se ignoran a efectos presupuestales.; El IVA recaudado nunca excede el ValueTax pendiente de la factura (se topa con i.ValueTax - SUM(aric.Value previas con Status=1)).; El recaudo presupuestal y los reconocimientos se generan siempre contra la vigencia presupuestal del año del DocumentDate del traslado.; Para vigencias anteriores se genera un reconocimiento por cada CxC (uno por iteración), con AutomaticCollection=1 y Status=2.; Los registros generados por este SP referencian al PortfolioTransfer mediante EntityId/EntityCode/EntityName=''PortfolioTransfer''.; Cualquier excepción no controlada se convierte en CodeResult=999 con el mensaje de error y la línea.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Portfolio.AccountReceivableIVACollected: Para cada PortfolioTransferDetail cuya factura asociada tenga ValueTax > IVA ya recaudado, inserta el menor valor entre el detalle del traslado (ptd.Value) y el IVA pendiente (i.ValueTax - SUM(aric.Value)), con Status=1.; [EXEC] Budget.Collection: Si existen detalles cuyo Budget pertenece a la vigencia presupuestal actual (BudgetHeader.BudgetaryValidityId = vigencia del traslado), arma un XML y llama a Budget.SP_SaveCollection_Output para generar el recaudo presupuestal por RecognitionDetail con InitialValue = SUM(ptd.Value - IVA recaudado).; [EXEC] Budget.Recognition: Por cada cuenta por cobrar de vigencia anterior (YEAR(ar.AccountReceivableDate) < año del traslado) recorre en bucle e invoca Budget.SP_SaveRecognition_Output con un XML construido a partir del rubro de recuperación (CareGroup.AccountReceivablePreviousValidityBudgetId si la diferencia es 1 año, o PortfolioRecoveryBudgetId en otro caso), AutomaticCollection=1, Status=2.; [RETURN_RESULT] @CodeResult/@MessageResult: Devuelve CodeResult=0 con mensaje vacío o acumulado en éxito; CodeResult=999 con mensaje específico en cada validación fallida o cuando los SP de Budget retornan código distinto de 0; en CATCH, 999 con ERROR_MESSAGE + línea.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @AffectBudget = 0 (ninguna CxC del traslado afecta presupuesto) → Retorna CodeResult=0 y MessageResult='''' sin generar recaudo ni reconocimiento. else Continúa con validaciones presupuestales.; si @BudgetaryValidityId IS NULL → Retorna 999 ''No existe una vigencia activa asociada a la fecha del Cruce de Anticipo vs CxC.''; si @DependencyId IS NULL → Retorna 999 ''No se ha parametrizado una dependencia en los parámetros de cartera.''; si Existen facturas con YEAR(AccountReceivableDate) > @DocumentYear → Retorna 999 listando facturas de vigencia superior al traslado.; si Existen facturas de la vigencia actual con RecognitionId IS NULL → Retorna 999 listando facturas sin reconocimiento asociado.; si Existen facturas de vigencias anteriores donde no se resuelve RevenueType (rtd) o Category (cd) equivalente en la vigencia destino → Retorna 999 listando facturas sin rubro de recuperación parametrizado.; si Existe al menos un detalle cuyo Budget tiene BudgetHeader.BudgetaryValidityId = vigencia actual → Construye XML y ejecuta Budget.SP_SaveCollection_Output; si retorna código <>0, aborta con 999.; si Existen CxC con YEAR(AccountReceivableDate) < @DocumentYear → Itera (WHILE) por cada CxC anterior y ejecuta Budget.SP_SaveRecognition_Output; si retorna código <>0, aborta con 999.; si @DocumentYear - 1 = YEAR(ar.AccountReceivableDate) → Usa cg.AccountReceivablePreviousValidityBudgetId como rubro presupuestal de origen. else Usa cg.PortfolioRecoveryBudgetId.', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Budget.SP_SaveCollection_Output; Budget.SP_SaveRecognition_Output', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Portfolio', @level1type=N'PROCEDURE', @level1name=N'SP_GenerateCollectionByPortfolioTransferId_Output';
-- GO
