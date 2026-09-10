-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 2022-02-08
-- Description:	Procedimiento para el reporte de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportIncomeSchedule_Co]
	@validityId as integer,
	@month as integer
AS
BEGIN	
	SET NOCOUNT ON
		
	
	DECLARE @TableSchedule TABLE
	(
		BudgetId integer,
		Code VARCHAR(40),
		Name VARCHAR(MAX),
		InitialValue DECIMAL(18,0) DEFAULT (0),
		Balance DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		

		/********************************************  OBTENCION DE DATOS ********************************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableSchedule 
		(
			BudgetId, Code, Name, InitialValue, Balance
		)
		SELECT
			b.Id,
			ccpet.Code,
			ccpet.Name,
			b.InitialValue,
			b.InitialValue
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.CCPET ccpet on ccpet.Id = c.CCPETCodeId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		WHERE bh.BudgetaryValidityId = @validityId
		AND c.ItemType = 1 
		
		 -- afectamos el saldo con los movimientos con fecha de corte
		 UPDATE te
			SET te.Balance = te.Balance + [data].[Value]
		 FROM @TableSchedule te
		 JOIN
		 (
			SELECT 
				btd.BudgetId,			
				SUM(isnull(IIF(btd.Nature = 1, btd.Value * -1, btd.Value),0)) as [Value]
			FROM Budget.BudgetTransfer bt 
			JOIN Budget.BudgetTransferDetail btd ON bt.Id = btd.TransferId
			WHERE bt.BudgetaryValidityId = @validityId and MONTH(bt.DocumentDate) <= @month AND bt.Status = 2
			GROUP BY btd.BudgetId
		) AS [data] ON te.BudgetId = [data].BudgetId

		UPDATE te
			SET te.Balance = te.Balance + [data].[Value]
		 FROM @TableSchedule te
		 JOIN
		 (
			SELECT 
				bmd.BudgetId,			
				SUM(isnull(IIF(bmd.Nature = 1, bmd.Value * -1, bmd.Value),0)) as [Value]
			FROM Budget.BudgetModification bm 
			JOIN Budget.BudgetModificationDetail bmd ON bm.Id = bmd.ModificationId
			WHERE bm.BudgetaryValidityId = @validityId and MONTH(bm.DocumentDate) <= @month AND bm.Status = 2
			GROUP BY bmd.BudgetId
		) AS [data] ON te.BudgetId = [data].BudgetId

		/************************************************* RESULTADO *************************************************/

		SELECT	
		Code,
		Name,
		SUM(InitialValue) as InitialValue,
		SUM(Balance) as Balance
		FROM @TableSchedule
		GROUP BY Code, Name
		having SUM(InitialValue) > 0 OR SUM(Balance) > 0
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reporte de ejecución presupuestal de ingresos por vigencia y mes de corte. Para una vigencia presupuestal dada, consolida el presupuesto inicial de ingresos (tomado de Budget, CCPET, Category, FinancialSource y RevenueType) y luego ajusta el saldo disponible aplicando los traslados presupuestales (BudgetTransfer/BudgetTransferDetail) y las modificaciones presupuestales aprobadas (BudgetModification/BudgetModificationDetail) cuya fecha de documento corresponda al mes de corte indicado. El resultado final agrupa por código y nombre de la partida presupuestal de ingreso, mostrando el valor inicial y el saldo ajustado, excluyendo rubros sin movimiento. Sirve para el seguimiento y control de la ejecución presupuestal de rentas e ingresos institucionales en la vigencia seleccionada.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportIncomeSchedule_Co';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de ejecución presupuestal de ingresos por rubro CCPET para una vigencia y mes de corte, calculando el valor inicial y el saldo afectado por traslados y modificaciones aprobados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un BudgetHeader con BudgetaryValidityId igual al parámetro de vigencia.; Las categorías a reportar deben tener ItemType = 1 (ítems de ingreso).; Los traslados y modificaciones se consideran solo si su Status = 2 (estado aplicado/aprobado).; El parámetro de mes debe ser un número de mes válido para filtrar MONTH(DocumentDate).', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran categorías cuyo ItemType = 1 (rubros de ingreso).; Solo se incluyen traslados y modificaciones con Status = 2 (aprobados/aplicados).; El filtro temporal es por mes calendario (MONTH(DocumentDate) <= @month) sin considerar el año del documento, asumiendo que la vigencia ya restringe el año.; Los movimientos con Nature = 1 disminuyen el saldo; cualquier otra naturaleza lo aumenta.; El resultado final omite filas con InitialValue y Balance ambos en cero o negativos (HAVING SUM(InitialValue) > 0 OR SUM(Balance) > 0).; Los valores se truncan a DECIMAL(18,0) (sin decimales) en la tabla temporal.; El reporte agrupa por Code y Name del CCPET, consolidando múltiples líneas presupuestales del mismo rubro.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'presupuesto; vigencia presupuestal; rubro/partida presupuestal (CCPET); categoría presupuestal; fuente de financiación; tipo de ingreso; traslado presupuestal; modificación presupuestal; naturaleza débito/crédito; saldo presupuestal; ejecución presupuestal de ingresos', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Devuelve resultset con Code, Name, InitialValue y Balance agrupados por rubro CCPET, filtrando filas donde SUM(InitialValue) > 0 OR SUM(Balance) > 0.; [RETURN_RESULT] (resultset de error): En caso de excepción, retorna un resultset con CodeResult=''999'' y MessageResult con el mensaje y línea del error.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si btd.Nature = 1 (naturaleza débito en el detalle de traslado) → El valor se resta al saldo (Value * -1) else El valor se suma al saldo tal cual; si bmd.Nature = 1 (naturaleza débito en el detalle de modificación) → El valor se resta al saldo (Value * -1) else El valor se suma al saldo tal cual; si Error en el bloque TRY → Se devuelve un resultset con CodeResult=''999'' y MessageResult con ERROR_MESSAGE() y línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.CCPET; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportIncomeSchedule_Co';
-- GO
