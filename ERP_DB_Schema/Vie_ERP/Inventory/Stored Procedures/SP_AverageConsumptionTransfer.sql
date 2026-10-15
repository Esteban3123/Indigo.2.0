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
		  WHEN ANIO = YEAR(@InitialDate) THEN 12 - DATEPART(MONTH , @InitialDate)
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