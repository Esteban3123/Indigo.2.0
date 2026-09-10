-- =============================================
-- Author:		Cristhian Salazar
-- Create date: 2022-02-08
-- Description:	Procedimiento para el reporte de ejecucion presupuestal de gastos
-- =============================================
CREATE PROCEDURE [Budget].[SP_ReportExpenseSchedule_Co]
	@validityId as integer,
	@month as integer
AS
BEGIN	
	SET NOCOUNT ON
		
	
	DECLARE @TableExecution TABLE
	(
		BudgetId integer,
		Code VARCHAR(40),
		Name VARCHAR(MAX),
		ValidityType tinyint,
		BudgetSection varchar(5),
		Sector varchar(5),
		InitialValue DECIMAL(18,0) DEFAULT (0),
		Balance DECIMAL(18,0) DEFAULT (0)
	)

	BEGIN TRY
		

		/********************************************  OBTENCION DE DATOS ********************************************/

		-- insertamos en la tabla temporal los datos del presupuesto inicial
		INSERT INTO @TableExecution 
		(
			BudgetId, Code, Name, ValidityType, BudgetSection, Sector, InitialValue, Balance
		)
		SELECT
			b.Id,
			ccpet.Code,
			ccpet.Name,
			c.Validity,
			'2.6' as Seccion, -- por definir
			'19' as Sector, -- por definir
			b.InitialValue,
			b.InitialValue
		FROM Budget.Budget b 
		JOIN Budget.Category c ON c.Id = b.CategoryId
		JOIN Budget.CCPET ccpet on ccpet.Id = c.CCPETCodeId
		JOIN Budget.FinancialSource fs ON fs.Id = c.FinancialSourceId 
		JOIN Budget.RevenueType rt ON rt.Id = b.RevenueTypeId 
		JOIN Budget.BudgetHeader bh ON bh.Id = b.BudgetHeaderId 
		WHERE bh.BudgetaryValidityId = @validityId
		AND c.ItemType = 2 
		
		 -- afectamos el saldo con los movimientos con fecha de corte
		 UPDATE te
			SET te.Balance = te.Balance + [data].[Value]
		 FROM @TableExecution te
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
		 FROM @TableExecution te
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
		ValidityType,
		BudgetSection,
		Sector,
		SUM(InitialValue) as InitialValue,
		SUM(Balance) as Balance
		FROM @TableExecution
		GROUP BY Code, Name, ValidityType, BudgetSection, Sector
	END TRY
	BEGIN CATCH	
		SELECT '999' CodeResult, ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)) MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de ejecución presupuestal de gastos para una vigencia y mes de corte determinados. Parte del presupuesto inicial registrado en Budget.Budget (cruzado con categorías, partidas CCPET, fuentes de financiación y tipos de ingreso bajo un encabezado de presupuesto), luego ajusta el saldo disponible aplicando los movimientos aprobados de traslados presupuestales (BudgetTransfer) y modificaciones presupuestales (BudgetModification) ocurridos hasta el mes indicado. Devuelve un consolidado por código y nombre de partida presupuestal (CCPET) con el valor presupuestado inicial y el saldo resultante tras los movimientos, utilizado para el seguimiento y control de la ejecución del presupuesto de egresos o gastos institucionales.', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'PROCEDURE', @level1name = N'SP_ReportExpenseSchedule_Co';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera el reporte de ejecución presupuestal de gastos para una vigencia y mes de corte, calculando el saldo de cada rubro a partir del valor inicial más los traslados y modificaciones aprobados.', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un BudgetHeader con BudgetaryValidityId igual al parámetro de vigencia; Las categorías deben ser de tipo gasto (Category.ItemType = 2) para ser incluidas; Los traslados y modificaciones deben estar en Status = 2 para afectar el saldo; El parámetro @month debe estar en rango válido de mes (1-12) para el filtro MONTH(DocumentDate) <= @month', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran categorías de tipo gasto (Category.ItemType = 2); Solo se acumulan traslados y modificaciones en estado 2 (aprobado/aplicado); Solo se incluyen movimientos con MONTH(DocumentDate) <= @month dentro de la vigencia indicada; Los valores InitialValue y Balance se truncan a DECIMAL(18,0) (sin decimales); BudgetSection se fija en ''2.6'' y Sector en ''19'' como valores constantes (pendientes de definir); El saldo final = valor inicial + (créditos - débitos) de traslados aprobados + (créditos - débitos) de modificaciones aprobadas, hasta el mes de corte', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'ejecución presupuestal de gastos; vigencia presupuestal; rubro presupuestal (CCPET); saldo presupuestal; traslados presupuestales; modificaciones presupuestales; naturaleza débito/crédito; sección presupuestal; sector presupuestal', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] ?: Retorna por rubro (Code, Name, ValidityType, BudgetSection, Sector) la suma de InitialValue y Balance acumulado tras aplicar traslados y modificaciones de la vigencia hasta el mes indicado; [RETURN_RESULT] ?: En caso de error, retorna un resultset con CodeResult=''999'' y MessageResult conteniendo ERROR_MESSAGE() y la línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Budget.BudgetTransferDetail.Nature = 1 (débito) → El valor del traslado se resta al saldo (Value * -1); en caso contrario se suma; si Budget.BudgetModificationDetail.Nature = 1 (débito) → El valor de la modificación se resta al saldo (Value * -1); en caso contrario se suma; si Ocurre una excepción en el bloque TRY → Devuelve un resultset con CodeResult=''999'' y MessageResult con el mensaje y línea del error', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Budget.Budget; Budget.Category; Budget.CCPET; Budget.FinancialSource; Budget.RevenueType; Budget.BudgetHeader; Budget.BudgetTransfer; Budget.BudgetTransferDetail; Budget.BudgetModification; Budget.BudgetModificationDetail', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Budget', @level1type=N'PROCEDURE', @level1name=N'SP_ReportExpenseSchedule_Co';
-- GO
