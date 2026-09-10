-- =============================================
-- Author:		Miguel Angel Fonseca Castro
-- Create date: 2020-05-06
-- Description:	Procedimiento para el reporte de listado de disponibilidades del presupuesto de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportListDocumentExpenseAvailability]
	@xmlCriterias AS XML
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE	@DateStart DATE,
			@DateEnd DATE,
			@GroupBy TINYINT,
			@BudgetaryValidityId INT,
			@AvailabilityCode VARCHAR(20)

	BEGIN TRY
		
		/*************************************** CRITERIOS ***************************************/

		SELECT	@DateStart = t.x.value('DateStart[1]','date'),
				@DateEnd = t.x.value('DateEnd[1]','date'),
				@GroupBy = t.x.value('GroupBy[1]','tinyint'),
				@BudgetaryValidityId = t.x.value('BudgetaryValidityId[1]','int'),
				@AvailabilityCode = t.x.value('AvailabilityCode[1]','varchar(20)')
		FROM @xmlCriterias.nodes('/Data') t(x)

		/********************************** OBTENCION DE DATOS **********************************/

		SELECT	CASE @GroupBy
					WHEN 2 THEN cat.Id
					WHEN 3 THEN fs.Id
					WHEN 4 THEN rt.Id
					ELSE 0
				END GroupId,
				CASE @GroupBy
					WHEN 2 THEN cat.Code + ' - ' + cat.Name
					WHEN 3 THEN fs.Code + ' - ' + fs.Name
					WHEN 4 THEN rt.Code + ' - ' + rt.Name
					ELSE ''
				END GroupName,
				a.Code,
				a.DocumentDate,
				a.ExpirationDate,
				a.Observations,
				CASE a.AvailabilityType 
					WHEN 1 THEN 'Ninguno' 
					WHEN 2 THEN 'Disponibilidad' 
					WHEN 3 THEN 'Vigencia Factura'
					ELSE 'N/A'
				END AvailabilityTypeName,
				CASE a.Status 
					WHEN 1 THEN 'Registrado' 
					WHEN 2 THEN 'Confirmado' 
					WHEN 3 THEN 'Anulado' 
					ELSE 'N/A'
				END StatusName,
				cat.Code CategoryCode,
				cat.Name CategoryName,
				fs.Code FinancialSourceCode,
				fs.Name FinancialSourceName,
				rt.Code RevenueTypeCode,
				rt.Name RevenueTypeName,				
				ad.InitialValue,
				ISNULL(am.DebitValue, 0) DebitValue,
				ISNULL(am.CreditValue, 0) CreditValue,
				ad.InitialValue - ISNULL(am.DebitValue, 0) + ISNULL(am.CreditValue, 0) TotalValue,
				ISNULL(cn.ExecutedValue, 0) ExecutedValue,
				ad.InitialValue - ISNULL(am.DebitValue, 0) + ISNULL(am.CreditValue, 0) - ISNULL(cn.ExecutedValue, 0) Balance
		FROM Budget.Availability a WITH (NOLOCK)
		JOIN Budget.AvailabilityDetail ad WITH (NOLOCK) ON a.Id = ad.AvailabilityId
		JOIN Budget.Budget b WITH (NOLOCK) ON ad.BudgetId = b.Id
		JOIN Budget.Category cat WITH (NOLOCK) ON b.CategoryId = cat.Id
		JOIN Budget.FinancialSource fs WITH (NOLOCK) ON cat.FinancialSourceId = fs.Id
		JOIN Budget.RevenueType rt WITH (NOLOCK) ON b.RevenueTypeId = rt.Id
		/************************************  MODIFICACIONES ************************************/
		LEFT JOIN 
		(
			SELECT	amd.AvailabilityDetailId, SUM(IIF(amd.Nature = 1, amd.Value, 0)) DebitValue, SUM(IIF(amd.Nature = 1, 0, amd.Value)) CreditValue
			FROM Budget.AvailabilityModification am WITH (NOLOCK)
			JOIN Budget.AvailabilityModificationDetail amd WITH (NOLOCK) ON am.Id = amd.AvailabilityModificationId
			WHERE am.Status = 2 AND CAST(am.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY amd.AvailabilityDetailId
		) am ON ad.Id = am.AvailabilityDetailId 
		/************************************** COMPROMISOS **************************************/
		LEFT JOIN 
		(
			SELECT	cd.AvailabilityDetailId, SUM(cd.InitialValue + ISNULL(cm.CreditValue, 0) - ISNULL(cm.DebitValue, 0)) ExecutedValue
			FROM Budget.Commitment c WITH (NOLOCK)
			JOIN Budget.CommitmentDetail cd WITH (NOLOCK) ON c.Id = cd.CommitmentId
			LEFT JOIN 
			(
				SELECT cmd.CommitmentDetailId, SUM(IIF(cmd.Nature = 1, cmd.Value, 0)) DebitValue, SUM(IIF(cmd.Nature = 1, 0, cmd.Value)) CreditValue
				FROM Budget.CommitmentModification cm WITH (NOLOCK)
				JOIN Budget.CommitmentModificationDetail cmd WITH (NOLOCK) ON cm.Id = cmd.CommitmentModificationId
				WHERE cm.Status = 2 AND CAST(cm.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY cmd.CommitmentDetailId
			) cm ON cd.Id = cm.CommitmentDetailId
			WHERE c.Status = 2 AND CAST(c.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			GROUP BY cd.AvailabilityDetailId
		) cn ON ad.Id = cn.AvailabilityDetailId 
		WHERE CAST(a.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
			AND (ISNULL(@AvailabilityCode, '') = '' OR a.Code = @AvailabilityCode)
		ORDER BY a.Id
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de listado de certificados de disponibilidad presupuestal de gastos (CDP) para un rango de fechas y vigencia presupuestal seleccionados. Consolida, por cada línea de disponibilidad, el valor inicial, las modificaciones aprobadas (débitos y créditos), el valor total ajustado, el valor ejecutado a través de compromisos y el saldo disponible restante. Permite agrupar los resultados por categoría presupuestal, fuente de financiación o tipo de ingreso/renta, y filtrar opcionalmente por código de disponibilidad. Se usa para el control y seguimiento del presupuesto de gastos, verificando cuánto se ha comprometido y cuánto queda libre en cada CDP dentro del período consultado.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de listado de disponibilidades del presupuesto de gastos con sus saldos, modificaciones y ejecución comprometida en un rango de fechas, agrupable por categoría, fuente o tipo de renta.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de criterios debe contener el nodo /Data con DateStart, DateEnd, GroupBy, BudgetaryValidityId y AvailabilityCode.; DateStart y DateEnd deben definir un rango válido de fechas porque filtran disponibilidades, modificaciones y compromisos.; Las disponibilidades, modificaciones y compromisos considerados deben tener DocumentDate dentro del rango [@DateStart, @DateEnd].', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran modificaciones de disponibilidad con Status = 2 (confirmadas) dentro del rango de fechas.; Solo se consideran compromisos con Status = 2 (confirmados) dentro del rango de fechas, y sus modificaciones también requieren Status = 2.; TotalValue de una disponibilidad siempre se calcula como InitialValue - DebitValue + CreditValue.; Balance siempre se calcula como TotalValue - ExecutedValue.; ExecutedValue de un detalle de disponibilidad agrega los compromisos como InitialValue + CreditValue - DebitValue de sus modificaciones.; Las disponibilidades se filtran exclusivamente por su DocumentDate dentro del rango proporcionado.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad presupuestal (CDP); Presupuesto de gastos; Vigencia presupuestal; Categoría presupuestal; Fuente de financiación; Tipo de renta/ingreso; Modificación de disponibilidad (débito/crédito); Compromiso presupuestal; Modificación de compromiso; Saldo y ejecución presupuestal; Vigencia Factura', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve filas de disponibilidades con InitialValue, DebitValue, CreditValue, TotalValue (= InitialValue - Debit + Credit), ExecutedValue y Balance (= Total - Executed), agrupadas según @GroupBy.; [RETURN_RESULT] (resultset): En caso de error en el TRY, retorna una fila con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y número de línea.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @GroupBy = 2 → Agrupa/etiqueta por Budget.Category (Code + Name).; si @GroupBy = 3 → Agrupa/etiqueta por Budget.FinancialSource (Code + Name).; si @GroupBy = 4 → Agrupa/etiqueta por Budget.RevenueType (Code + Name). else GroupId=0 y GroupName='''' (sin agrupación reconocida).; si a.AvailabilityType IN (1,2,3) → Traduce a ''Ninguno'', ''Disponibilidad'' o ''Vigencia Factura'' respectivamente. else ''N/A''; si a.Status IN (1,2,3) → Traduce a ''Registrado'', ''Confirmado'' o ''Anulado'' respectivamente. else ''N/A''; si ISNULL(@AvailabilityCode,'''') = ''''  → No filtra por código de disponibilidad (incluye todas). else Filtra solo la disponibilidad cuyo Code = @AvailabilityCode.; si amd.Nature = 1 (en modificaciones de disponibilidad) → El valor se acumula como DebitValue. else Se acumula como CreditValue.; si cmd.Nature = 1 (en modificaciones de compromiso) → El valor se acumula como DebitValue del compromiso. else Se acumula como CreditValue del compromiso.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Availability; Budget.AvailabilityDetail; Budget.Budget; Budget.Category; Budget.FinancialSource; Budget.RevenueType; Budget.AvailabilityModification; Budget.AvailabilityModificationDetail; Budget.Commitment; Budget.CommitmentDetail; Budget.CommitmentModification; Budget.CommitmentModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportListDocumentExpenseAvailability';
-- GO
