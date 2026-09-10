-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-25
-- Description:	Procedimiento para el reporte mensual de ejecucion presupuestal de gastos por rubro y tercero
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportBudgetExecutionByCategoryThird]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE	@BudgetaryValidityId INT,
			@CutoffDate DATETIME,
			@CodeToUse TINYINT,
			@Level TINYINT,
			@IncludeZero BIT,
			@Budgets VARCHAR(MAX),
			@ThirdParties VARCHAR(MAX),
			-------------
			@FilterByBudgets BIT = 0,
			@FilterByThirdParties BIT = 0

	DECLARE @Table_Budgets AS TABLE(Id INT)
	DECLARE @Table_ThirdParties AS TABLE(Id INT)

	DECLARE @ExecutionBudget AS TABLE
	(	
		BudgetId INT,
		Level INT,
		-----------------------------------
		AvailabilityCode VARCHAR(20),
		AvailabilityDate DATE,
		AvailabilityModificationCode VARCHAR(20),
		AvailabilityModificationDate DATE,
		-----------------------------------
		ThirdPartyId INT,
		CommitmentCode VARCHAR(20),
		CommitmentDate DATE,		
		CommitmentModificationCode VARCHAR(20),
		CommitmentModificationDate DATE,
		-----------------------------------
		ObligationCode VARCHAR(20),
		ObligationDate DATE,
		ObligationModificationCode VARCHAR(20),
		ObligationModificationDate DATE,
		-----------------------------------
		PaymentOrderCode VARCHAR(20),
		PaymentOrderDate DATE,
		ReimbursementResourceCode VARCHAR(20),
		ReimbursementResourceDate DATE,
		-----------------------------------
		AvailabilityValue NUMERIC DEFAULT(0),
		AvailabilityDebitValue NUMERIC DEFAULT(0),
		AvailabilityCreditValue NUMERIC DEFAULT(0),
		CommitmentValue NUMERIC DEFAULT(0),
		CommitmentDebitValue NUMERIC DEFAULT(0),
		CommitmentCreditValue NUMERIC DEFAULT(0),
		ObligationValue NUMERIC DEFAULT(0),
		ObligationDebitValue NUMERIC DEFAULT(0),
		ObligationCreditValue NUMERIC DEFAULT(0),
		PaymentOrderValue NUMERIC DEFAULT(0),
		PaymentOrderDebitValue NUMERIC DEFAULT(0)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@CutoffDate = t.x.value('CutoffDate[1]','datetime'),
				@CodeToUse = t.x.value('CodeToUse[1]','tinyint'),
				@Level = t.x.value('Level[1]','tinyint'),
				@IncludeZero = t.x.value('IncludeZero[1]','bit'),
				@Budgets = t.x.value('Budgets[1]','varchar(max)'),
				@ThirdParties = t.x.value('ThirdParties[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		IF ISNULL(@Budgets, '') <> ''
		BEGIN
			SET @FilterByBudgets = 1

			INSERT INTO @Table_Budgets
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Budgets, ',')
		END

		IF ISNULL(@ThirdParties, '') <> ''
		BEGIN
			SET @FilterByThirdParties = 1

			INSERT INTO @Table_ThirdParties
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@ThirdParties, ',')
		END

		/**********************************  OBTENCION DE DATOS **********************************/

		------------------------------------------  DISPONIBILIDADES ----------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, AvailabilityValue
		)
		SELECT	ad.BudgetId, 1, a.Code, a.DocumentDate, ad.InitialValue
		FROM Budget.Availability a
		JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
		JOIN
		(
			SELECT	cd.AvailabilityDetailId
			FROM Budget.Commitment c
			JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
			JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
			LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
			LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
			WHERE c.BudgetaryValidityId = @BudgetaryValidityId AND c.Status = 2 AND c.DocumentDate <= @CutoffDate
				AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
				AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
			GROUP BY cd.AvailabilityDetailId
		) cd ON ad.Id = cd.AvailabilityDetailId

		-------------------------------------- DISPONIBILIDAD MODIFICACIONES ----------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, AvailabilityModificationCode, AvailabilityModificationDate, AvailabilityDebitValue, AvailabilityCreditValue
		)
		SELECT	ad.BudgetId, 2, a.Code, a.DocumentDate, am.Code, am.DocumentDate, IIF(amd.Nature = 1, amd.Value, 0), IIF(amd.Nature = 1, 0, amd.Value)
		FROM Budget.AvailabilityModification am
		JOIN Budget.AvailabilityModificationDetail amd ON am.Id =amd.AvailabilityModificationId
		JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		JOIN
		(
			SELECT	cd.AvailabilityDetailId
			FROM Budget.Commitment c
			JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
			JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
			LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
			LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
			WHERE c.BudgetaryValidityId = @BudgetaryValidityId AND c.Status = 2 AND c.DocumentDate <= @CutoffDate
				AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
				AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)
			GROUP BY cd.AvailabilityDetailId
		) cd ON ad.Id = cd.AvailabilityDetailId
		WHERE am.Status = 2 AND @Level > 1

		--------------------------------------------- COMPROMISOS -------------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, CommitmentValue
		)
		SELECT	ad.BudgetId, 3, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, cd.InitialValue
		FROM Budget.Commitment c
		JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE c.BudgetaryValidityId = @BudgetaryValidityId AND c.Status = 2 AND @Level > 2 
			AND c.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		---------------------------------------- COMPROMISO MODIFICACIONES ------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, CommitmentModificationCode, CommitmentModificationDate, CommitmentDebitValue, CommitmentCreditValue
		)
		SELECT	ad.BudgetId, 4, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, cm.Code, cm.DocumentDate, IIF(cmd.Nature = 1, cmd.Value, 0), IIF(cmd.Nature = 1, 0, cmd.Value)
		FROM Budget.CommitmentModification cm
		JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
		JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE cm.BudgetaryValidityId = @BudgetaryValidityId AND cm.Status = 2 AND @Level > 3
			AND cm.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		----------------------------------------------  OBLIGACIONES ------------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, ObligationCode, ObligationDate, ObligationValue
		)
		SELECT	ad.BudgetId, 5, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, o.Code, o.DocumentDate, od.InitialValue
		FROM Budget.Obligation o
		JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
		JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE o.BudgetaryValidityId = @BudgetaryValidityId AND o.Status = 2 AND @Level > 4
			AND o.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		---------------------------------------- OBLIGACION MODIFICACIONES ------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, ObligationCode, ObligationDate, ObligationModificationCode, ObligationModificationDate, ObligationDebitValue, ObligationCreditValue
		)
		SELECT	ad.BudgetId, 6, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, o.Code, o.DocumentDate, om.Code, om.DocumentDate, IIF(omd.Nature = 1, omd.Value, 0), IIF(omd.Nature = 1, 0, omd.Value)
		FROM Budget.ObligationModification om
		JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
		JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
		JOIN Budget.Obligation o ON od.ObligationId = o.Id
		JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE om.BudgetaryValidityId = @BudgetaryValidityId AND om.Status = 2 AND @Level > 5
			AND om.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		--------------------------------------------- ORDENES DE PAGO -----------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, ObligationCode, ObligationDate, PaymentOrderCode, PaymentOrderDate, PaymentOrderValue
		)
		SELECT	ad.BudgetId, 7, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, o.Code, o.DocumentDate, po.Code, po.DocumentDate, pod.InitialValue
		FROM Budget.PaymentOrder po
		JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
		JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
		JOIN Budget.Obligation o ON od.ObligationId = o.Id
		JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE po.BudgetaryValidityId = @BudgetaryValidityId AND po.Status IN (2, 4) AND @Level > 6
			AND po.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		-----------------------------------------------  REINTEGROS -------------------------------------------

		INSERT INTO @ExecutionBudget
		(
			BudgetId, Level, AvailabilityCode, AvailabilityDate, ThirdPartyId, CommitmentCode, CommitmentDate, ObligationCode, ObligationDate, PaymentOrderCode, PaymentOrderDate, ReimbursementResourceCode, ReimbursementResourceDate, PaymentOrderDebitValue
		)
		SELECT	ad.BudgetId, 8, a.Code, a.DocumentDate, c.ThirdPartyId, c.Code, c.DocumentDate, o.Code, o.DocumentDate, po.Code, po.DocumentDate, rr.Code, rr.DocumentDate, rrd.Value
		FROM Budget.ReimbursementResource rr
		JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
		JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
		JOIN Budget.PaymentOrder po ON pod.PaymentOrderId = po.Id
		JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
		JOIN Budget.Obligation o ON od.ObligationId = o.Id
		JOIN Budget.CommitmentDetail cd ON od.CommitmentDetailId = cd.Id
		JOIN Budget.Commitment c ON cd.CommitmentId = c.Id
		JOIN Budget.AvailabilityDetail ad ON cd.AvailabilityDetailId = ad.Id
		JOIN Budget.Availability a ON ad.AvailabilityId = a.Id
		LEFT JOIN @Table_Budgets tb ON ad.BudgetId = tb.Id
		LEFT JOIN @Table_ThirdParties ttp ON c.ThirdPartyId = ttp.Id
		WHERE rr.BudgetaryValidityId = @BudgetaryValidityId AND rr.Status = 2 AND @Level > 7
			AND rr.DocumentDate <= @CutoffDate			
			AND (@FilterByBudgets = 0 OR tb.Id IS NOT NULL)
			AND (@FilterByThirdParties = 0 OR ttp.Id IS NOT NULL)

		/*************************************** RESULTADO ***************************************/

		-- Retornamos el resultado
		SELECT	CONCAT(IIF(@CodeToUse = 1, c.Code, c.AlternativeCode), ' - ', c.Name) CategoryCodeName,
				CONCAT(fs.Code, ' - ', fs.Name) FinancialSourceCodeName,
				CONCAT(rt.Code, ' - ', rt.Name) RevenueTypeCode,
				CONCAT(tp.Nit, ' - ', tp.Name) ThirdPartyNitName,
				CASE eb.Level
					WHEN 1 THEN 'Disponibilidad'
					WHEN 2 THEN 'Disponibilidad Modificación'
					WHEN 3 THEN 'Compromiso'
					WHEN 4 THEN 'Compromiso Modificación'
					WHEN 5 THEN 'Obligación'
					WHEN 6 THEN 'Obligación Modificación'
					WHEN 7 THEN 'Orden de Pago'
					WHEN 8 THEN 'Reintegro'
				END MovementType,
				eb.*
		FROM @ExecutionBudget eb
		JOIN Budget.Budget b ON eb.BudgetId = b.Id
		JOIN Budget.Category c ON b.CategoryId = c.Id
		JOIN Budget.FinancialSource fs ON c.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt ON b.RevenueTypeId = rt.Id
		LEFT JOIN Common.ThirdParty tp ON eb.ThirdPartyId = tp.Id
		ORDER BY eb.BudgetId, eb.AvailabilityCode, eb.ThirdPartyId,
			CASE eb.Level
				WHEN 1 THEN eb.AvailabilityDate
				WHEN 2 THEN eb.AvailabilityModificationDate
				WHEN 3 THEN eb.CommitmentDate
				WHEN 4 THEN eb.CommitmentModificationDate
				WHEN 5 THEN eb.ObligationDate
				WHEN 6 THEN eb.ObligationModificationDate
				WHEN 7 THEN eb.PaymentOrderDate
				WHEN 8 THEN eb.ReimbursementResourceDate
			END, Level
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento para el reporte mensual de ejecución presupuestal de gastos por rubro (categoría presupuestal) y tercero. Recibe criterios en formato XML que incluyen la vigencia presupuestal, fecha de corte, nivel de detalle, rubros y terceros a filtrar. Consolida en una tabla temporal todos los movimientos de la cadena presupuestal: disponibilidades (CDP) con sus modificaciones, compromisos con sus modificaciones, obligaciones con sus modificaciones, órdenes de pago y recursos de reembolso; cruzando las tablas Budget.Availability, Budget.AvailabilityDetail, Budget.Commitment y Budget.CommitmentDetail. El resultado entrega, por rubro y tercero, los valores iniciales, débitos, créditos y saldos de cada etapa del gasto, permitiendo hacer seguimiento y control de la ejecución presupuestal de egresos en un período de vigencia determinado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte detallado de ejecución presupuestal de gastos por rubro y tercero, mostrando por cada etapa (disponibilidad, compromiso, obligación, orden de pago, reintegro) sus modificaciones, valores y saldos hasta una fecha de corte.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @xmlCriterias debe contener el nodo /Data con BudgetaryValidityId, CutoffDate, CodeToUse, Level, IncludeZero, Budgets y ThirdParties.; Las listas Budgets y ThirdParties, si se proveen, deben ser cadenas separadas por coma con identificadores enteros válidos (se castean a INT).; Debe existir una vigencia presupuestal (BudgetaryValidityId) con documentos en estado válido para producir resultados.; Solo se consideran documentos cuya fecha (DocumentDate) sea menor o igual a @CutoffDate.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Result set (reporte): Devuelve un conjunto con CategoryCodeName, FinancialSourceCodeName, RevenueTypeCode, ThirdPartyNitName y MovementType (etiqueta textual según Level 1..8) ordenado por BudgetId, AvailabilityCode, ThirdPartyId, fecha del movimiento y Level.; [RETURN_RESULT] Result set (error): Si ocurre una excepción, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() concatenado con la línea del error.; [INSERT] @ExecutionBudget: Inserta filas Level=1 (Disponibilidad) solo para detalles de disponibilidad referenciados por compromisos en estado 2 con DocumentDate<=@CutoffDate dentro de la vigencia.; [INSERT] @ExecutionBudget: Inserta filas Level=2 (Disponibilidad Modificación) cuando @Level>1, AvailabilityModification.Status=2; el valor se asigna a AvailabilityDebitValue si Nature=1, en caso contrario a AvailabilityCreditValue.; [INSERT] @ExecutionBudget: Inserta filas Level=3 (Compromiso) cuando @Level>2, Commitment.Status=2 y DocumentDate<=@CutoffDate.; [INSERT] @ExecutionBudget: Inserta filas Level=4 (Compromiso Modificación) cuando @Level>3, CommitmentModification.Status=2; débito/crédito según Nature=1.; [INSERT] @ExecutionBudget: Inserta filas Level=5 (Obligación) cuando @Level>4, Obligation.Status=2 y DocumentDate<=@CutoffDate.; [INSERT] @ExecutionBudget: Inserta filas Level=6 (Obligación Modificación) cuando @Level>5, ObligationModification.Status=2; débito/crédito según Nature=1.; [INSERT] @ExecutionBudget: Inserta filas Level=7 (Orden de Pago) cuando @Level>6 y PaymentOrder.Status IN (2,4) con DocumentDate<=@CutoffDate.; [INSERT] @ExecutionBudget: Inserta filas Level=8 (Reintegro) cuando @Level>7 y ReimbursementResource.Status=2 con DocumentDate<=@CutoffDate; el valor se carga en PaymentOrderDebitValue.; [INSERT] @Table_Budgets: Si @Budgets no es vacío, se activa @FilterByBudgets=1 y se cargan los IDs parseados con dbo.Split por coma.; [INSERT] @Table_ThirdParties: Si @ThirdParties no es vacío, se activa @FilterByThirdParties=1 y se cargan los IDs parseados con dbo.Split por coma.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Budgets,'''') <> '''' → Activa filtro por rubros y carga @Table_Budgets con los IDs. else No filtra por rubros (incluye todos).; si ISNULL(@ThirdParties,'''') <> '''' → Activa filtro por terceros y carga @Table_ThirdParties con los IDs. else No filtra por terceros (incluye todos).; si @Level controla la profundidad del reporte (>1..>7) → Cada nivel solo se incluye si @Level supera el umbral correspondiente: 1 Disponibilidad, 2 ModDisp, 3 Compromiso, 4 ModCompromiso, 5 Obligación, 6 ModObligación, 7 OrdenPago, 8 Reintegro.; si Nature = 1 en detalles de modificación (disp/compromiso/obligación) → El valor se registra como Débito (DebitValue). else El valor se registra como Crédito (CreditValue).; si @CodeToUse = 1 → El reporte muestra CategoryCodeName con Category.Code. else Muestra Category.AlternativeCode.; si po.Status IN (2, 4) → Las órdenes de pago consideran tanto estado 2 como 4 (a diferencia del resto que solo usa Status=2).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportBudgetExecutionByCategoryThird';
-- GO
