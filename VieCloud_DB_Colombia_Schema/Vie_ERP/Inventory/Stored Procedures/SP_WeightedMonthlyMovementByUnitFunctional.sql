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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el movimiento mensual ponderado de inventario por unidad funcional para un mes y año de cierre contable indicados. Consolida los movimientos del kardex de tres fuentes: ventas de productos (facturas de venta), dispensaciones farmacéuticas a pacientes y devoluciones de dispensación farmacéutica, sumando el valor total (cantidad × costo) generado por cada unidad funcional dentro del período. Con ese consolidado, calcula el peso relativo (ponderación) de cada unidad funcional respecto al total de la unidad operativa, retornando además el centro de costo y la cuenta contable asociada, dato clave para la distribución de costos de inventario en el cierre contable mensual.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la distribución porcentual ponderada de los movimientos mensuales de inventario (ventas, dispensaciones y devoluciones farmacéuticas) por unidad funcional dentro de cada unidad operativa, retornando el peso junto con sus cuentas contables de costo y centro de costo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El mes y año recibidos deben permitir construir una fecha válida con formato ''dd/MM/yyyy''.; Debe existir configuración en Inventory.SettingInventory para la unidad operativa.; Debe existir relación en Inventory.SettingInventoryFunctionalUnit entre la configuración de inventario y la unidad funcional.; La unidad funcional debe existir en Payroll.FunctionalUnit.; Debe haber movimientos en Kardex en el rango del mes/año dado para que el denominador (Total por unidad operativa) no sea cero.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El rango de fechas siempre cubre desde el día 1 hasta el último día calendario del mes/año recibidos.; Solo se consideran movimientos de Kardex cuyo EntityName sea uno de: DocumentInvoiceProductSales, PharmaceuticalDispensing o PharmaceuticalDispensingDevolution.; El ponderado de cada unidad funcional es relativo (proporción) dentro del total de su misma unidad operativa, no global.; El valor del movimiento se calcula como Quantity * Value del Kardex.; La suma de los ponderados por unidad operativa equivale a 1 (antes del redondeo).; Solo se retornan unidades funcionales que tengan configuración contable activa en SettingInventoryFunctionalUnit y existan en Payroll.FunctionalUnit.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Movimiento mensual ponderado de inventario; Unidad funcional; Unidad operativa; Kardex; Dispensación farmacéutica; Devolución de dispensación farmacéutica; Factura de venta de productos; Cuenta contable de costo; Centro de costo; Cierre mensual de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Retorna por cada (OperatingUnitId, FunctionalUnitId) el ponderado = Valor de movimientos de la UF / Total de movimientos de la unidad operativa, redondeado a 6 decimales, junto con CostAccountId y CostCenterId.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Kardex.EntityName = ''DocumentInvoiceProductSales'' → Suma Quantity*Value agrupado por OperatingUnitId y FunctionalUnitId tomados del documento de factura de venta.; si Kardex.EntityName = ''PharmaceuticalDispensing'' → Suma Quantity*Value uniendo con dispensación farmacéutica y su detalle por ProductId, agrupando por OperatingUnitId del encabezado y FunctionalUnitId del detalle.; si Kardex.EntityName = ''PharmaceuticalDispensingDevolution'' → Suma Quantity*Value navegando devolución → detalle de devolución → batch/serial → detalle de dispensación, validando ProductId, agrupando por OperatingUnitId de la devolución y FunctionalUnitId del detalle de dispensación original.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.DocumentInvoiceProductSales; Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDevolution; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.SettingInventory; Inventory.SettingInventoryFunctionalUnit; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_WeightedMonthlyMovementByUnitFunctional';
-- GO
