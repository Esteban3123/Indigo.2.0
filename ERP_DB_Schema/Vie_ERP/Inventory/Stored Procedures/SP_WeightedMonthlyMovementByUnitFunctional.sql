-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-11-15
-- Description:	sp para calcular el movimiento mensual ponderado por Unidad Funcional de inventario
-- =============================================
CREATE PROCEDURE [Inventory].[SP_WeightedMonthlyMovementByUnitFunctional]
	@MonthClosed AS INT,
	@YearClosed AS INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @DateStart DATE = '01/' + RIGHT( '0' + CAST(@MonthClosed AS VARCHAR(2)), 2) + '/'+ CAST(@YearClosed AS VARCHAR(4))
	DECLARE @DateEnd DATE = DATEADD(DAY, -1, DATEADD(MONTH, 1, @DateStart))

	--Tabla temporal en donde se almacena el ponderado de los movimientos realizados por unidad funcional
	DECLARE 
		@TableWeighted TABLE (
			OperatingUnitId INT,
			FunctionalUnitId INT,
			Value DECIMAL(32,4)
		)

	--DATOS PARA REALIZAR PONDERACIÓN
		--Unidad Operativa
		--Unidad Funcional
		--Movimiento total
	
	INSERT INTO @TableWeighted (OperatingUnitId, FunctionalUnitId, Value)
		SELECT 
			p.OperatingUnitId, p.FunctionalUnitId, SUM(Value) AS Value
		FROM
		(			
				SELECT 
					dips.OperatingUnitId, dips.FunctionalUnitId, 
					SUM(k.Quantity * k.Value) AS Value
				FROM Inventory.Kardex k
				INNER JOIN Inventory.DocumentInvoiceProductSales dips
					ON k.EntityName = 'DocumentInvoiceProductSales'
						AND k.EntityId = dips.Id
				WHERE CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY dips.OperatingUnitId, dips.FunctionalUnitId

			UNION ALL

				SELECT 
					pd.OperatingUnitId, pdd.FunctionalUnitId, 
					SUM(k.Quantity * k.Value) AS Value
				FROM Inventory.Kardex k
				INNER JOIN Inventory.PharmaceuticalDispensing pd
					ON k.EntityName = 'PharmaceuticalDispensing'
						AND k.EntityId = pd.Id
				INNER JOIN Inventory.PharmaceuticalDispensingDetail pdd
					ON pd.Id = pdd.PharmaceuticalDispensingId
						AND k.ProductId = pdd.ProductId
				WHERE CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY pd.OperatingUnitId, pdd.FunctionalUnitId

			UNION ALL
			
				SELECT
					pdd.OperatingUnitId, pddt.FunctionalUnitId, 
					SUM(k.Quantity * k.Value) AS Value
				FROM Inventory.Kardex k
				INNER JOIN Inventory.PharmaceuticalDispensingDevolution pdd
					ON k.EntityName = 'PharmaceuticalDispensingDevolution'
						AND k.EntityId = pdd.Id
				INNER JOIN Inventory.PharmaceuticalDispensingDevolutionDetail pddd
					ON pdd.Id = pddd.PharmaceuticalDispensingDevolutionId
				INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs
					ON pddd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
				INNER JOIN Inventory.PharmaceuticalDispensingDetail pddt
					ON pddbs.PharmaceuticalDispensingDetailId = pddt.Id
						AND k.ProductId = pddt.ProductId
				WHERE CAST(k.DocumentDate AS DATE) BETWEEN @DateStart AND @DateEnd
				GROUP BY pdd.OperatingUnitId, pddt.FunctionalUnitId
		) p
		GROUP BY p.OperatingUnitId, p.FunctionalUnitId


/** ------------------------------- RESULTADOS ------------------------------- **/

		SELECT DISTINCT 
			tw.OperatingUnitId, tw.FunctionalUnitId, ROUND((tw.Value / wg.Total), 6) AS Weighted, sifu.CostAccountId, fu.CostCenterId
		FROM @TableWeighted tw
		INNER JOIN
		(
			SELECT tw.OperatingUnitId, SUM(tw.Value) AS Total
			FROM @TableWeighted tw
			GROUP BY tw.OperatingUnitId
		) wg ON tw.OperatingUnitId = wg.OperatingUnitId
		INNER JOIN Inventory.SettingInventory si ON tw.OperatingUnitId = si.OperatingUnitId
		INNER JOIN Inventory.SettingInventoryFunctionalUnit sifu ON si.Id = sifu.SettingInventoryId AND tw.FunctionalUnitId = sifu.FunctionalUnitId
		INNER JOIN Payroll.FunctionalUnit fu ON sifu.FunctionalUnitId = fu.Id

END