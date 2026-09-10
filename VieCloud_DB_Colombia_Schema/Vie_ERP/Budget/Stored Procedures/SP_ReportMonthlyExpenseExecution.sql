-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-03-01
-- Description:	Procedimiento para el reporte mensual de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportMonthlyExpenseExecution]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON
	SET DATEFORMAT DMY

	DECLARE	@BudgetaryValidityId INT,
			@Year INT,
			@Month INT,
			@CodeToUse TINYINT,
			@IncludeZero BIT,
			@Categories VARCHAR(MAX),
			@FinancialSources VARCHAR(MAX),
			@RevenueTypes VARCHAR(MAX),
			-------------
			@FilterByCategories BIT = 0,
			@FilterByFinancialSources BIT = 0,
			@FilterByRevenueTypes BIT = 0

	DECLARE @Table_Categories AS TABLE(Id INT)
	DECLARE @Table_FinancialSources AS TABLE(Id INT)
	DECLARE @Table_RevenueTypes AS TABLE(Id INT)

	DECLARE @TableExecution TABLE
	(
		BudgetId INT,
		CategoryId INT,
		CategoryCode VARCHAR(40),
		CategoryName VARCHAR(MAX),
		AlternativeCode VARCHAR(40),
		FinancialSourceCode VARCHAR(20),
		FinancialSourceName VARCHAR(MAX),
		RevenueTypeId INT,
		RevenueTypeCode VARCHAR(20),
		RevenueTypeName VARCHAR(MAX),
		BudgetInitial DECIMAL(18,0) DEFAULT (0),
		BudgetTransferCredit DECIMAL(18,0) DEFAULT (0),
		BudgetTransferDebit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationCredit DECIMAL(18,0) DEFAULT (0),
		BudgetModificationDebit DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		AvailabilityBalanceMonth DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		CommitmentBalanceMonth DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		ObligationBalanceMonth DECIMAL(18,0) DEFAULT (0),
		PaymentOrderBalanceMonthPrevious DECIMAL(18,0) DEFAULT (0),
		PaymentOrderBalanceMonth DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		
		/********************************** CRITERIOS Y FILTROS **********************************/

		SELECT	@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@Year = t.x.value('Year[1]','int'),
				@Month = t.x.value('Month[1]','int'),
				@CodeToUse = t.x.value('CodeToUse[1]','tinyint'),
				@IncludeZero = t.x.value('IncludeZero[1]','bit'),
				@Categories = t.x.value('Categories[1]','varchar(max)'),
				@FinancialSources = t.x.value('FinancialSources[1]','varchar(max)'),
				@RevenueTypes = t.x.value('RevenueTypes[1]','varchar(max)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		DECLARE @InitialDate AS DATE = DATEFROMPARTS(@Year,1,1)
		DECLARE @EndDate AS DATE = DATEADD(DAY,-1,DATEFROMPARTS(@Year,@Month,1))
		DECLARE @InitialDateMonthSelect AS DATE = DATEFROMPARTS(@Year,@Month,1)
		DECLARE @EndDateMonthSelect AS DATE = DATEADD(DAY,-1,DATEADD(MONTH,1,@InitialDateMonthSelect))

		IF ISNULL(@Categories, '') <> ''
		BEGIN
			SET @FilterByCategories = 1

			INSERT INTO @Table_Categories
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@Categories, ',')
		END

		IF ISNULL(@FinancialSources, '') <> ''
		BEGIN
			SET @FilterByFinancialSources = 1

			INSERT INTO @Table_FinancialSources
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@FinancialSources, ',')
		END

		IF ISNULL(@RevenueTypes, '') <> ''
		BEGIN
			SET @FilterByRevenueTypes = 1

			INSERT INTO @Table_RevenueTypes
				SELECT CAST(Data AS INT) Data 
				FROM dbo.Split(@RevenueTypes, ',')
		END

		/********************************** OBTENCION DE DATOS **********************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableExecution 
		(
			BudgetId, CategoryId, CategoryCode, CategoryName, AlternativeCode, FinancialSourceCode, FinancialSourceName, RevenueTypeId, RevenueTypeCode, RevenueTypeName, BudgetInitial
		)
		SELECT 
			b.Id, c.Id, c.Code, c.Name, AlternativeCode, fs.Code, fs.Name, rt.Id, rt.Code, rt.Name, b.InitialValue 
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		LEFT JOIN @Table_Categories tc ON c.Id = tc.Id
		LEFT JOIN @Table_FinancialSources tfs ON fs.Id = tfs.Id
		LEFT JOIN @Table_RevenueTypes trt ON rt.Id = trt.Id
		WHERE bh.BudgetaryValidityId = @BudgetaryValidityId 
			AND c.ItemType = 2
			AND (@FilterByCategories = 0 OR tc.Id IS NOT NULL)
			AND (@FilterByFinancialSources = 0 OR tfs.Id IS NOT NULL)
			AND (@FilterByRevenueTypes = 0 OR trt.Id IS NOT NULL)

		-- insertamos en la tabla temporal los datos del traslado de presupuesto 
		UPDATE te
			SET te.BudgetTransferDebit = data.DebitValue,
				te.BudgetTransferCredit = data.CreditValue
		FROM @TableExecution te
		JOIN
		(
			SELECT 
				btd.BudgetId,			
				SUM(IIF(btd.Nature = 1, btd.Value, 0)) DebitValue,
				SUM(IIF(btd.Nature = 1, 0, btd.Value)) CreditValue
			FROM Budget.BudgetTransfer bt 
			JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
			WHERE bt.Status = 2 AND CAST(bt.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateMonthSelect
			GROUP BY btd.BudgetId
		) AS data ON te.BudgetId = data.BudgetId

		-- insertamos en la tabla temporal los datos de la modificacion del presupuesto
		UPDATE te
			SET te.BudgetModificationDebit = data.DebitValue,
				te.BudgetModificationCredit = data.CreditValue
		FROM @TableExecution te
		JOIN
		(
			SELECT 
				bmd.BudgetId,
				SUM(IIF(bmd.Nature = 1, bmd.Value, 0)) DebitValue,
				SUM(IIF(bmd.Nature = 1, 0, bmd.Value)) CreditValue 
			FROM Budget.BudgetModification bm
			JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
			WHERE bm.Status = 2 AND CAST(bm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDateMonthSelect
			GROUP BY bmd.BudgetId
		) AS data ON data.BudgetId = te.BudgetId 

		-- insertamos en la tabla temporal los datos de los movimientos de disponibilidades en los meses anteriores al seleccionado
		UPDATE te 
			SET te.AvailabilityBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				ad.BudgetId,
				SUM(ad.InitialValue) Value
			FROM Budget.Availability a
			JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
			WHERE a.Status = 2 AND CAST(a.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY ad.BudgetId
		) AS data ON te.BudgetId = data.BudgetId
		LEFT JOIN
		(
			SELECT 
				ad.BudgetId,
				SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue,
				SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am 
			JOIN Budget.AvailabilityModificationDetail amd ON am.Id = amd.AvailabilityModificationId
			JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY ad.BudgetId
		) AS modification ON te.BudgetId = modification.BudgetId

		--insertamos en la tabla temporal los datos de los movimientos de disponibilidades del mes seleccionado
		UPDATE te 
			SET te.AvailabilityBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				ad.BudgetId,
				SUM(ad.InitialValue) Value
			FROM Budget.Availability a
			JOIN Budget.AvailabilityDetail ad ON a.Id = ad.AvailabilityId
			WHERE a.Status = 2 AND CAST(a.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY ad.BudgetId
		) AS data ON te.BudgetId = data.BudgetId
		LEFT JOIN
		(
			SELECT 
				ad.BudgetId,
				SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue,
				SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am 
			JOIN Budget.AvailabilityModificationDetail amd ON am.Id = amd.AvailabilityModificationId
			JOIN Budget.AvailabilityDetail ad ON amd.AvailabilityDetailId = ad.Id
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY ad.BudgetId
		) AS modification ON te.BudgetId = modification.BudgetId

		-- insertamos en la tabla temporal los datos de los movimientos de compromisos en los meses anteriores al seleccionado
		UPDATE te 
			SET te.CommitmentBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			FROM Budget.Commitment c 
			JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm 
			JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
			JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		--insertamos en la tabla temporal los datos de los movimientos de compromisos del mes seleccionado
		UPDATE te 
			SET te.CommitmentBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(cd.InitialValue) Value
			FROM Budget.Commitment c 
			JOIN Budget.CommitmentDetail cd ON c.Id = cd.CommitmentId
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				cd.CategoryId,
				cd.RevenueTypeId,
				SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue,
				SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
			FROM Budget.CommitmentModification cm 
			JOIN Budget.CommitmentModificationDetail cmd ON cm.Id = cmd.CommitmentModificationId
			JOIN Budget.CommitmentDetail cd ON cmd.CommitmentDetailId = cd.Id
			WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY cd.CategoryId, cd.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- insertamos en la tabla temporal los datos de los movimientos de obligaciones en los meses anteriores al seleccionado
		UPDATE te 
			SET te.ObligationBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(od.InitialValue) Value
			FROM Budget.Obligation o 
			JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue,
				SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om 
			JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
			JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		--insertamos en la tabla temporal los datos de los movimientos de obligaciones del mes seleccionado
		UPDATE te 
			SET te.ObligationBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(od.InitialValue) Value
			FROM Budget.Obligation o 
			JOIN Budget.ObligationDetail od ON o.Id = od.ObligationId
			WHERE o.Status = 2 AND CAST(o.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(IIF(omd.Nature = 1, omd.Value, 0)) DebitValue,
				SUM(IIF(omd.Nature = 1, 0, omd.Value)) CreditValue
			FROM Budget.ObligationModification om 
			JOIN Budget.ObligationModificationDetail omd ON om.Id = omd.ObligationModificationId
			JOIN Budget.ObligationDetail od ON omd.ObligationDetailId = od.Id
			WHERE om.Status = 2 AND CAST(om.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- insertamos en la tabla temporal los datos de los movimientos de ordenes de pago en los meses anteriores al seleccionado
		UPDATE te 
			SET te.PaymentOrderBalanceMonthPrevious = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(pod.InitialValue) Value
			FROM Budget.PaymentOrder po 
			JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDate AND @EndDate
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		--insertamos en la tabla temporal los datos de los movimientos de ordenes de pago del mes seleccionado
		UPDATE te 
			SET te.PaymentOrderBalanceMonth = ISNULL(data.Value, 0) - ISNULL(modification.DebitValue, 0) + ISNULL(modification.CreditValue, 0)
		FROM @TableExecution te
		LEFT JOIN 
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(pod.InitialValue) Value
			FROM Budget.PaymentOrder po 
			JOIN Budget.PaymentOrderDetail pod ON po.Id = pod.PaymentOrderId
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE po.Status = 2 AND CAST(po.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS data ON te.CategoryId = data.CategoryId AND te.revenueTypeId = data.RevenueTypeId
		LEFT JOIN
		(
			SELECT 
				od.CategoryId,
				od.RevenueTypeId,
				SUM(rrd.Value) DebitValue,
				0 CreditValue
			FROM Budget.ReimbursementResource rr
			JOIN Budget.ReimbursementResourceDetaill rrd ON rr.Id = rrd.ReimbursementResourceId
			JOIN Budget.PaymentOrderDetail pod ON rrd.PaymentOrderDetailId = pod.Id
			JOIN Budget.ObligationDetail od ON pod.ObligationDetailId = od.Id
			WHERE rr.Status = 2 AND CAST(rr.DocumentDate AS DATE) BETWEEN @InitialDateMonthSelect AND @EndDateMonthSelect
			GROUP BY od.CategoryId, od.RevenueTypeId
		) AS modification ON te.CategoryId = modification.CategoryId AND te.revenueTypeId = modification.RevenueTypeId

		-- Retornamos el resultado
		SELECT * 
		FROM @TableExecution 
		WHERE @IncludeZero = 1 OR
		(
			BudgetInitial <> 0 OR BudgetTransferCredit <> 0 OR BudgetTransferDebit <> 0 OR BudgetModificationCredit <> 0 OR BudgetModificationDebit <> 0 OR 
			AvailabilityBalanceMonthPrevious <> 0 OR AvailabilityBalanceMonth <> 0 OR 
			CommitmentBalanceMonthPrevious <> 0 OR CommitmentBalanceMonth <> 0 OR
			ObligationBalanceMonthPrevious <> 0 OR ObligationBalanceMonth <> 0 OR
			PaymentOrderBalanceMonthPrevious <> 0 OR PaymentOrderBalanceMonth <> 0
		)
		ORDER BY IIF(@CodeToUse = 1, CategoryCode, AlternativeCode)
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte mensual de ejecución presupuestal de gastos para una vigencia presupuestal específica. Consolida en un único resultado los valores de presupuesto inicial, traslados de crédito y débito, modificaciones, y los saldos acumulados de disponibilidades, compromisos, obligaciones y órdenes de pago, tanto del mes seleccionado como de los meses anteriores dentro del año. Recibe criterios de filtro en formato XML (vigencia, año, mes, tipo de código a usar, inclusión de ceros, y listas opcionales de categorías, fuentes de financiación y tipos de ingreso/renta), apoyándose en la función Split para descomponer esas listas. Sirve para la generación de informes contables y presupuestales de gasto institucional, permitiendo hacer seguimiento a la ejecución del presupuesto de egresos mes a mes.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportMonthlyExpenseExecution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte mensual de ejecución presupuestal de gastos para una vigencia, comparando saldos acumulados de meses anteriores y del mes seleccionado en presupuesto inicial, traslados, modificaciones, disponibilidades, compromisos, obligaciones y órdenes de pago.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@xmlCriterias debe contener el nodo /Data con BudgetaryValidityId, Year, Month, CodeToUse, IncludeZero y opcionalmente Categories, FinancialSources, RevenueTypes (listas separadas por coma); @Year y @Month deben formar una fecha válida ya que se usan en DATEFROMPARTS para construir los rangos; Debe existir un BudgetHeader activo para @BudgetaryValidityId con líneas en Budget.Budget cuya categoría sea de tipo gasto (ItemType=2); La función dbo.Split debe estar disponible para parsear las listas CSV de filtros', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran rubros presupuestales con Category.ItemType = 2 (gastos); Solo se incluyen movimientos cuyos documentos estén en Status = 2 (estado aprobado/vigente) en BudgetTransfer, BudgetModification, Availability, AvailabilityModification, Commitment, CommitmentModification, Obligation, ObligationModification, PaymentOrder y ReimbursementResource; El presupuesto base se restringe a BudgetHeader.BudgetaryValidityId = @BudgetaryValidityId; El rango ''meses anteriores'' va de 01-Ene-@Year hasta el último día del mes anterior a @Month; el rango ''mes seleccionado'' cubre exactamente @Month de @Year; Para traslados y modificaciones presupuestales el rango considerado es el acumulado desde 01-Ene-@Year hasta el fin del mes seleccionado (no se separa mes vs anteriores); Naturaleza=1 se interpreta como débito; cualquier otro valor se acumula como crédito; El saldo de cada bloque (disponibilidad, compromiso, obligación, orden de pago) se calcula como Valor inicial - Débitos de modificación + Créditos de modificación; para órdenes de pago, el ''débito'' proviene de los reembolsos (ReimbursementResource) y no hay crédito (CreditValue = 0); Disponibilidades se enlazan al detalle por BudgetId; compromisos, obligaciones y órdenes de pago se enlazan por (CategoryId, RevenueTypeId); Los filtros opcionales por categorías, fuentes y tipos de ingreso se aplican mediante el patrón (@FilterByX = 0 OR tx.Id IS NOT NULL), por lo que cadenas vacías o nulas implican no filtrar; Los valores monetarios se truncan a DECIMAL(18,0) (sin decimales)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Ejecución presupuestal de gastos; Vigencia presupuestal; Presupuesto inicial; Traslados presupuestales (débito/crédito); Modificaciones presupuestales; Disponibilidad presupuestal (CDP); Compromisos presupuestales; Obligaciones presupuestales; Órdenes de pago; Reembolsos / reintegros de recursos; Fuente de financiación; Tipo de ingreso/renta; Categoría presupuestal (rubro); Naturaleza del movimiento (débito=1, crédito≠1)', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Resultset: Devuelve la tabla de ejecución presupuestal con saldos por categoría/fuente/tipo de ingreso, filtrando filas en cero salvo que @IncludeZero=1, ordenado por CategoryCode o AlternativeCode según @CodeToUse; [RETURN_RESULT] Resultset: En caso de error capturado, devuelve un resultset con CodeResult=''999'' y MessageResult conteniendo ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ISNULL(@Categories,'''') <> '''' → Activa filtro por categorías y carga IDs en tabla temporal vía dbo.Split else No filtra por categorías; si ISNULL(@FinancialSources,'''') <> '''' → Activa filtro por fuentes de financiación y carga IDs en tabla temporal else No filtra por fuentes de financiación; si ISNULL(@RevenueTypes,'''') <> '''' → Activa filtro por tipos de ingreso y carga IDs en tabla temporal else No filtra por tipos de ingreso; si @IncludeZero = 1 → Devuelve todas las filas de la ejecución, incluso con todos los saldos en cero else Solo devuelve filas con al menos un valor distinto de cero (presupuesto inicial, traslados, modificaciones, disponibilidades, compromisos, obligaciones u órdenes de pago); si @CodeToUse = 1 → Ordena el resultado por CategoryCode else Ordena el resultado por AlternativeCode; si Excepción capturada en BEGIN CATCH → Retorna un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y número de línea, en lugar del reporte', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail; Budget.Availability; Budget.AvailabilityDetail; Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.Commitment; Budget.CommitmentDetail; Budget.CommitmentModification; Budget.CommitmentModificationDetail; Budget.Obligation; Budget.ObligationDetail; Budget.ObligationModification; Budget.ObligationModificationDetail; Budget.PaymentOrder; Budget.PaymentOrderDetail; Budget.ReimbursementResource; Budget.ReimbursementResourceDetaill', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportMonthlyExpenseExecution';
-- GO
