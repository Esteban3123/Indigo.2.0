-- =============================================
-- Author:		Hector Rodriguez Rubiano
-- Create date: 2019-10-18
-- Description:	Reporte de consumo promedio de orden de traslado
-- =============================================
CREATE PROCEDURE [Inventory].[SP_AverageConsumptionTransfer]
	-- Add the parameters for the stored procedure here
	@Parameters as xml
AS
BEGIN
	SET NOCOUNT ON;
 
    -- Insert statements for procedure here
	DECLARE @InitialDate as date
	DECLARE @EndDate as date
	DECLARE @OrderType as TINYINT
	DECLARE @OfficeTo AS TINYINT
	DECLARE @OriginStore as varchar(max)
	DECLARE @DestinationStore as varchar(max)
	DECLARE @FunctionalUnit as varchar(max)
	DECLARE @Product as varchar(max)
 
DECLARE @TABLE AS TABLE
(
ProductId INT
,TargetId INT
,SourceWarehouseId INT
,OrderType TINYINT
,ANIO  INT
,ENE INT
,FEB INT
,MAR INT
,ABR INT
,MAY INT
,JUN INT
,JUL INT
,AGO INT
,SEP INT
,OCT INT
,NOV INT
,DIC INT
,Average decimal(12,2)
,MeasurementUnitId INT
,ATCId INT
,TargetTypeId TINYINT
,ProductCode VARCHAR(20)
,ProductName VARCHAR(200)
,TargetCode VARCHAR(20)
,TargetName VARCHAR(100)
,SourceCode VARCHAR(20)
,SourceName VARCHAR(100)
,OrderTypeName VARCHAR(10)
,TargetTypeName VARCHAR(20)
,MeasurementUnitCode VARCHAR(20)
,MeasurementUnitName VARCHAR(100)
,ATCConcentration VARCHAR(50)
,Temporal BIT
)
 
select 
@InitialDate = t.x.value('InitialDate[1]','datetime'),
@EndDate = t.x.value('EndDate[1]','datetime'),
@OrderType = t.x.value('OrderType[1]','tinyint'),
@OfficeTo = t.x.value('OfficeTo[1]','tinyint'),
@OriginStore = t.x.value('OriginStore[1]','varchar(max)'),
@DestinationStore = t.x.value('DestinationStore[1]', 'varchar(max)'),
@FunctionalUnit = t.x.value('FunctionalUnit[1]','varchar(max)'),
@Product = t.x.value('Product[1]','varchar(max)')
from @Parameters.nodes('/Parameters') t(x)
 
INSERT INTO @TABLE(ProductId, TargetId, TargetTypeId, SourceWarehouseId, OrderType, ANIO, ENE, FEB, MAR, ABR, MAY, JUN, JUL, AGO, SEP, OCT, NOV, DIC, Temporal)
select 
TD.ProductId
, TargetId = CASE WHEN T.TargetWarehouseId IS NULL THEN T.TargetFunctionalUnitId ELSE T.TargetWarehouseId END
, TargetTypeId = CASE WHEN T.TargetWarehouseId IS NULL THEN 2 ELSE 1 END
, T.SourceWarehouseId
, T.OrderType
, ANIO = DATEPART(YEAR, T.DocumentDate)
,ENE = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 1 THEN tdb.Quantity ELSE 0 END )
,FEB = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 2 THEN tdb.Quantity ELSE 0 END )
,MAR = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 3 THEN tdb.Quantity ELSE 0 END )
,ABR = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 4 THEN tdb.Quantity ELSE 0 END )
,MAY = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 5 THEN tdb.Quantity ELSE 0 END )
,JUN = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 6 THEN tdb.Quantity ELSE 0 END )
,JUL = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 7 THEN tdb.Quantity ELSE 0 END )
,AGO = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 8 THEN tdb.Quantity ELSE 0 END )
,SEP = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 9 THEN tdb.Quantity ELSE 0 END )
,OCT = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 10 THEN tdb.Quantity ELSE 0 END )
,NOV = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 11 THEN tdb.Quantity ELSE 0 END )
,DIC = SUM( CASE DATEPART(MONTH,T.DocumentDate) WHEN 12 THEN tdb.Quantity ELSE 0 END )
, 1
from Inventory.TransferOrder T WITH(NOLOCK)
INNER JOIN Inventory.TransferOrderDetail TD WITH(NOLOCK)
INNER JOIN (SELECT tdb.TransferOrderDetailId, sum(tdb.OutstandingQuantity) Quantity
			FROM Inventory.TransferOrderDetailBatchSerial tdb WITH(NOLOCK)
			group by tdb.TransferOrderDetailId) tdb on TD.Id=tdb.TransferOrderDetailId
ON TD.TransferOrderId = T.Id
WHERE T.DocumentDate BETWEEN @InitialDate AND @EndDate
AND ( @OrderType = 3 OR T.OrderType = @OrderType )
AND ( @OriginStore IS NULL OR T.SourceWarehouseId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@OriginStore, ',')))
AND (	( T.OrderType = 1 AND ( @DestinationStore IS NULL OR T.TargetWarehouseId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@DestinationStore, ','))) )
		OR 
		( T.OrderType = 2 AND (	( @OfficeTo in (1,3) AND T.TargetWarehouseId IS NOT NULL AND (@DestinationStore IS NULL OR T.TargetWarehouseId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@DestinationStore, ','))))
								OR 
								( @OfficeTo in (2,3) AND T.TargetFunctionalUnitId IS NOT NULL AND (@FunctionalUnit IS NULL OR T.TargetFunctionalUnitId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@FunctionalUnit, ','))))
							)
		)
)
AND ( @Product IS NULL OR TD.ProductId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Product, ',')))
GROUP BY 
TD.ProductId
, CASE WHEN T.TargetWarehouseId IS NULL THEN T.TargetFunctionalUnitId ELSE T.TargetWarehouseId END
, CASE WHEN T.TargetWarehouseId IS NULL THEN 2 ELSE 1 END
, T.SourceWarehouseId
, T.OrderType
, DATEPART(YEAR, T.DocumentDate)
, DATEPART(MONTH,T.DocumentDate)
 
 
INSERT INTO @TABLE(ProductId, TargetId, TargetTypeId, SourceWarehouseId, OrderType, ANIO, ENE, FEB, MAR, ABR, MAY, JUN, JUL, AGO, SEP, OCT, NOV, DIC, Temporal)
select 
ProductId
, TargetId
, TargetTypeId
, SourceWarehouseId
, OrderType
, ANIO
,ENE = SUM(ENE)
,FEB = SUM(FEB)
,MAR = SUM(MAR)
,ABR = SUM(ABR)
,MAY = SUM(MAY)
,JUN = SUM(JUN)
,JUL = SUM(JUL)
,AGO = SUM(AGO)
,SEP = SUM(SEP)
,OCT = SUM(OCT)
,NOV = SUM(NOV)
,DIC = SUM(DIC)
, 0
FROM @TABLE
GROUP BY 
ProductId
, TargetId
, TargetTypeId
, SourceWarehouseId
, OrderType
, ANIO
 
 
UPDATE T
SET 
ProductCode = P.Code
,ProductName = P.Name
,MeasurementUnitId = P.MeasurementUnitId
,ATCId = P.ATCId
FROM @TABLE T
INNER JOIN Inventory.InventoryProduct P WITH(NOLOCK)
ON P.Id = T.ProductId
WHERE T.Temporal = 0
 
UPDATE T
SET MeasurementUnitCode = MU.Code
, MeasurementUnitName = MU.Name
FROM @TABLE T
INNER JOIN Inventory.InventoryMeasurementUnit MU WITH(NOLOCK)
ON MU.Id = T.MeasurementUnitId
WHERE T.Temporal = 0
 
UPDATE T
SET ATCConcentration = A.Concentration
FROM @TABLE T
INNER JOIN Inventory.ATC A WITH(NOLOCK)
ON A.Id = T.ATCId
WHERE T.Temporal = 0
 
UPDATE T
SET SourceCode = W.Code
,SourceName = W.Name
FROM @TABLE T
INNER JOIN Inventory.Warehouse W WITH(NOLOCK)
ON W.Id = T.SourceWarehouseId
WHERE T.Temporal = 0
 
UPDATE T
SET TargetCode = W.Code
, TargetName = W.Name
FROM @TABLE T
INNER JOIN Inventory.Warehouse W WITH(NOLOCK)
ON W.Id = T.TargetId
AND T.TargetTypeId = 1
WHERE T.Temporal = 0
 
UPDATE T
SET TargetCode = FU.Code
, TargetName = FU.Name
FROM @TABLE T
INNER JOIN Payroll.FunctionalUnit FU WITH(NOLOCK)
ON FU.Id = T.TargetId
AND T.TargetTypeId = 2
WHERE T.Temporal = 0
 
UPDATE @TABLE SET OrderTypeName = CASE WHEN OrderType = 1 then 'Traslado' else 'Consumo' END
, TargetTypeName = CASE WHEN TargetTypeId = 1 then 'Almacén' else 'Unidad funcional' END
FROM @TABLE
WHERE Temporal = 0
 
UPDATE @TABLE SET Average = 
	convert(decimal(12,2),(ENE+ FEB+ MAR+ ABR+ MAY+ JUN+ JUL+ AGO+ SEP+ OCT+ NOV+ DIC))/
	--mismo año y mismo mes entonces 1
	--mismo año entonces diferencia de meses
    --añodoc igual añoinicial entonces 12 - mesinicial
	--añodoc igual añofin entonces mesfin
	--demas casos 12
	(CASE WHEN YEAR(@InitialDate) = year(@EndDate) AND  DATEPART(MONTH , @EndDate) = DATEPART(MONTH , @InitialDate) THEN 1
	      WHEN YEAR(@InitialDate) = year(@EndDate) THEN DATEPART(MONTH , @EndDate) - DATEPART(MONTH , @InitialDate)
		  WHEN ANIO = YEAR(@InitialDate) THEN 12 - DATEPART(MONTH , @InitialDate) + 1
		  WHEN ANIO = YEAR(@EndDate) THEN DATEPART(MONTH , @EndDate)
		  ELSE 12
		  END
	)
FROM @TABLE
WHERE Temporal = 0
 
select ProductId
,TargetId
,SourceWarehouseId
,OrderType
,ANIO
,ENE
,FEB
,MAR
,ABR
,MAY
,JUN
,JUL
,AGO
,SEP
,OCT
,NOV
,DIC
,Average
,MeasurementUnitId = ISNULL(MeasurementUnitId,0)
,ATCId = ISNULL(ATCId, 0)
,TargetTypeId
,ProductCode
,ProductName
,TargetCode
,TargetName
,SourceCode
,SourceName
,OrderTypeName
,TargetTypeName
,MeasurementUnitCode = ISNULL(MeasurementUnitCode,'')
,MeasurementUnitName = ISNULL(MeasurementUnitName,'')
,ATCConcentration = ISNULL( ATCConcentration, '')
from @TABLE WHERE Temporal = 0
order by ProductId, TargetId, SourceWarehouseId, OrderType, ANIO
 
 
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de consumo promedio mensual por producto en órdenes de traslado de inventario entre bodegas y unidades funcionales, para un rango de fechas y filtros seleccionados (bodega origen, bodega destino, unidad funcional, tipo de orden y producto). Consolida las cantidades despachadas mes a mes (enero a diciembre) desde las órdenes de traslado y sus detalles de lotes/seriales, calculando el promedio de consumo anual por producto y destino. Enriquece los resultados con información del producto (código, nombre, unidad de medida, concentración ATC) para facilitar el análisis de rotación y planificación de reabastecimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageConsumptionTransfer';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageConsumptionTransfer';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte de consumo promedio mensual de productos a partir de las órdenes de traslado en un rango de fechas, agrupado por producto, origen y destino (bodega o unidad funcional), incluyendo desglose por mes y promedio.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML de entrada debe contener InitialDate y EndDate válidos para filtrar T.DocumentDate BETWEEN @InitialDate AND @EndDate.; Las listas multivaluadas (OriginStore, DestinationStore, FunctionalUnit, Product) deben venir como cadenas separadas por comas parseables por dbo.Split a enteros.; @OrderType debe ser 1 (Traslado), 2 (Consumo) o 3 (ambos); @OfficeTo debe ser 1 (Almacén), 2 (Unidad funcional) o 3 (ambos) cuando OrderType=2.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El resultado final excluye las filas intermedias mensuales (Temporal=1); sólo se devuelven filas consolidadas (Temporal=0).; La cantidad consumida se mide siempre como SUM(OutstandingQuantity) de TransferOrderDetailBatchSerial, no como la cantidad solicitada en el detalle.; El destino se modela dualmente: TargetTypeId=1 referencia Inventory.Warehouse y TargetTypeId=2 referencia Payroll.FunctionalUnit, nunca ambos en la misma fila.; OrderType solo se etiqueta como ''Traslado'' (1) o ''Consumo'' (cualquier otro), reflejando dos tipos de movimiento de inventario.; El promedio nunca se divide por 0: cuando el rango cubre un único mes el divisor se fuerza a 1.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] @TABLE: Inserta filas mensuales (Temporal=1) con cantidades sumadas desde Inventory.TransferOrderDetailBatchSerial.OutstandingQuantity, agrupadas por producto, destino, origen, tipo de orden, año y mes, filtrando por rango de fechas y demás parámetros.; [INSERT] @TABLE: Inserta filas consolidadas (Temporal=0) sumando los 12 meses por ProductId, TargetId, TargetTypeId, SourceWarehouseId, OrderType y ANIO sobre las filas temporales previamente insertadas.; [UPDATE] @TABLE: Para filas Temporal=0 enriquece ProductCode, ProductName, MeasurementUnitId y ATCId desde Inventory.InventoryProduct.; [UPDATE] @TABLE: Para filas Temporal=0 enriquece MeasurementUnitCode/Name desde Inventory.InventoryMeasurementUnit y ATCConcentration desde Inventory.ATC.; [UPDATE] @TABLE: Para filas Temporal=0 enriquece SourceCode/SourceName desde Inventory.Warehouse usando SourceWarehouseId.; [UPDATE] @TABLE: Cuando TargetTypeId=1 toma TargetCode/Name desde Inventory.Warehouse; cuando TargetTypeId=2 los toma desde Payroll.FunctionalUnit.; [UPDATE] @TABLE: Asigna OrderTypeName=''Traslado'' si OrderType=1 y ''Consumo'' en otro caso; TargetTypeName=''Almacén'' si TargetTypeId=1 y ''Unidad funcional'' si TargetTypeId=2.; [UPDATE] @TABLE: Calcula Average = suma de los 12 meses dividida por número de meses según el rango: 1 si mismo año y mismo mes; diferencia de meses si mismo año; 12 - mes inicial si ANIO=año inicial; mes final si ANIO=año final; 12 en otro caso.; [RETURN_RESULT] @TABLE: Devuelve únicamente las filas con Temporal=0, ordenadas por ProductId, TargetId, SourceWarehouseId, OrderType, ANIO, sustituyendo nulos por 0 o '''' en MeasurementUnitId, ATCId, MeasurementUnitCode, MeasurementUnitName y ATCConcentration.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @OrderType = 3 → No filtra por tipo de orden (incluye Traslado y Consumo). else Filtra T.OrderType = @OrderType.; si T.OrderType = 1 (Traslado) → Solo aplica filtro opcional por TargetWarehouseId usando @DestinationStore.; si T.OrderType = 2 (Consumo) y @OfficeTo IN (1,3) → Incluye registros con TargetWarehouseId NOT NULL filtrando por @DestinationStore.; si T.OrderType = 2 (Consumo) y @OfficeTo IN (2,3) → Incluye registros con TargetFunctionalUnitId NOT NULL filtrando por @FunctionalUnit.; si T.TargetWarehouseId IS NULL → TargetId = TargetFunctionalUnitId y TargetTypeId = 2 (unidad funcional). else TargetId = TargetWarehouseId y TargetTypeId = 1 (almacén).; si YEAR(@InitialDate) = YEAR(@EndDate) AND mismo mes → Divisor del promedio = 1. else Aplica reglas de divisor según año del registro vs rango (ver side effect del cálculo de Average).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrder; Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.InventoryProduct; Inventory.InventoryMeasurementUnit; Inventory.ATC; Inventory.Warehouse; Payroll.FunctionalUnit', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageConsumptionTransfer';
-- GO
