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