-- =============================================
-- Author:		<Johan Sebastian Carranza Ramos, Developer Junior>
-- Create date: <22 Octubre de 2019>
-- Description:	Reporte de promedio de entradas a inventario.
-- =============================================
CREATE PROCEDURE [Inventory].[SP_AverageReportEntranceOrder]
	@Parameters as xml
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @InitialDate as date,
			@EndDate as date,
			@Warehouse as varchar(max),
			@Supplier as varchar(max),
			@Product as varchar(max),
			@TipoReporte as varchar(100),
			@Estado as varchar(100)

	Select	@InitialDate = t.x.value('InitialDate[1]','datetime'),
			@EndDate = t.x.value('EndDate[1]','datetime'),
			@Warehouse = t.x.value('Warehouse[1]', 'varchar(max)'),
			@Supplier = t.x.value('Supplier[1]','varchar(max)'),
			@Product = t.x.value('Product[1]','varchar(max)'),
			@TipoReporte = t.x.value('ReportType[1]','varchar(100)'),
			@Estado = t.x.value('State[1]','varchar(100)')
	from @Parameters.nodes('/Parameters') t(x)

	DECLARE @TABLE AS TABLE
	(
		ProductId INT,
		WarehouseId INT, 
		Supplier INT,
		ANIO  INT,
		ENE INT,
		FEB INT,
		MAR INT,
		ABR INT,
		MAY INT,
		JUN INT,
		JUL INT,
		AGO INT,
		SEP INT,
		OCT INT,
		NOV INT,
		DIC INT,
		Average decimal(12,2),
		ProductCode VARCHAR(20),
		ProductName VARCHAR(200),
		WarehouseCode VARCHAR(20),
		WarehouseName VARCHAR(100),
		SupplierCode VARCHAR(20),
		SupplierName VARCHAR(100),
		Temporal BIT
	)

	--Logica inicial para insertar registros agrupados por producto, almacen y proveedor sumando totales pero agrupando por meses.
	INSERT INTO @TABLE(ProductId, WarehouseId, Supplier, ANIO, ENE, FEB, MAR, ABR, MAY, JUN, JUL, AGO, SEP, OCT, NOV, DIC, Temporal)
		Select	ED.ProductId,E.WarehouseId,E.SupplierId
				, ANIO = DATEPART(YEAR, E.DocumentDate)
				,ENE = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 1 THEN ED.Quantity ELSE 0 END )
				,FEB = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 2 THEN ED.Quantity ELSE 0 END )
				,MAR = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 3 THEN ED.Quantity ELSE 0 END )
				,ABR = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 4 THEN ED.Quantity ELSE 0 END )
				,MAY = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 5 THEN ED.Quantity ELSE 0 END )
				,JUN = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 6 THEN ED.Quantity ELSE 0 END )
				,JUL = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 7 THEN ED.Quantity ELSE 0 END )
				,AGO = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 8 THEN ED.Quantity ELSE 0 END )
				,SEP = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 9 THEN ED.Quantity ELSE 0 END )
				,OCT = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 10 THEN ED.Quantity ELSE 0 END )
				,NOV = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 11 THEN ED.Quantity ELSE 0 END )
				,DIC = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 12 THEN ED.Quantity ELSE 0 END )
				, 1
		from Inventory.EntranceVoucher E WITH(NOLOCK) 
		INNER JOIN Inventory.EntranceVoucherDetail ED WITH(NOLOCK) ON ED.EntranceVoucherId = E.Id
		WHERE E.DocumentDate BETWEEN @InitialDate AND @EndDate
			AND (@Estado = 3 OR E.Status = IIF(@Estado = 1, 2, 1)) -- Filtro estado 
			AND (@Warehouse IS NULL OR E.WarehouseId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Warehouse, ','))) -- Filtro almacen
			AND (@Supplier IS NULL OR E.SupplierId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Supplier, ','))) -- Filtro proveedor
			AND (@Product IS NULL OR ED.ProductId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Product, ','))) -- Filtro Producto
		GROUP BY ED.ProductId,E.WarehouseId,E.SupplierId,DATEPART(YEAR, E.DocumentDate),DATEPART(MONTH,E.DocumentDate)

	--Logica para hallar los valores de las devoluciones.
	INSERT INTO @TABLE(ProductId, WarehouseId, Supplier, ANIO, ENE, FEB, MAR, ABR, MAY, JUN, JUL, AGO, SEP, OCT, NOV, DIC, Temporal)
		Select EN.ProductId,E.WarehouseId,C.SupplierId
				,ANIO = DATEPART(YEAR, E.DocumentDate)
				,ENE = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 1 THEN -ED.Quantity ELSE 0 END )
				,FEB = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 2 THEN -ED.Quantity ELSE 0 END )
				,MAR = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 3 THEN -ED.Quantity ELSE 0 END )
				,ABR = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 4 THEN -ED.Quantity ELSE 0 END )
				,MAY = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 5 THEN -ED.Quantity ELSE 0 END )
				,JUN = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 6 THEN -ED.Quantity ELSE 0 END )
				,JUL = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 7 THEN -ED.Quantity ELSE 0 END )
				,AGO = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 8 THEN -ED.Quantity ELSE 0 END )
				,SEP = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 9 THEN -ED.Quantity ELSE 0 END )
				,OCT = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 10 THEN -ED.Quantity ELSE 0 END )
				,NOV = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 11 THEN -ED.Quantity ELSE 0 END )
				,DIC = SUM( CASE DATEPART(MONTH,E.DocumentDate) WHEN 12 THEN -ED.Quantity ELSE 0 END )
				, 1
		from Inventory.EntranceVoucherDevolution E WITH(NOLOCK)
		INNER JOIN Inventory.EntranceVoucherDevolutionDetail ED WITH(NOLOCK) ON ED.EntranceVoucherDevolutionId = E.Id
		INNER JOIN Inventory.EntranceVoucherDetail EN WITH(NOLOCK) ON EN.Id = ED.EntranceVoucherDetailBatchSerialId 
		INNER JOIN Inventory.EntranceVoucher C ON EN.EntranceVoucherId = C.Id
		WHERE E.DocumentDate BETWEEN @InitialDate AND @EndDate
			AND (@Estado IS NULL OR E.Status IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Estado, ','))) -- Filtro estado 
			AND (@Warehouse IS NULL OR E.WarehouseId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Warehouse, ','))) -- Filtro almacen
			AND (@Supplier IS NULL OR C.SupplierId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Supplier, ','))) -- Filtro proveedor
			AND (@Product IS NULL OR EN.ProductId IN (SELECT convert(int, rtrim(Data)) FROM dbo.Split(@Product, ','))) -- Filtro Producto
		GROUP BY EN.ProductId,E.WarehouseId,C.SupplierId,DATEPART(YEAR, E.DocumentDate),DATEPART(MONTH,E.DocumentDate)

	--Logica para insertar los registros agrupados por producto , sumando los totales de cada mes.
	INSERT INTO @TABLE(ProductId, WarehouseId, Supplier, ANIO, ENE, FEB, MAR, ABR, MAY, JUN, JUL, AGO, SEP, OCT, NOV, DIC, Temporal)
		Select ProductId,
			WarehouseId, Supplier, ANIO, ENE = SUM(ENE),FEB = SUM(FEB),MAR = SUM(MAR),ABR = SUM(ABR),MAY = SUM(MAY)
,JUN = SUM(JUN),JUL = SUM(JUL),AGO = SUM(AGO),SEP = SUM(SEP),OCT = SUM(OCT),NOV = SUM(NOV),DIC = SUM(DIC), 0
FROM @TABLE
GROUP BY  ProductId, WarehouseId, Supplier, ANIO

--Logica para setear los datos del producto.
UPDATE T SET ProductCode = RTRIM(LTRIM(P.Code)),ProductName = P.Name
FROM @TABLE T INNER JOIN Inventory.InventoryProduct P WITH(NOLOCK) ON P.Id = T.ProductId
WHERE T.Temporal = 0

--Logica para setear los datos del almacen.
UPDATE T SET WarehouseCode = RTRIM(LTRIM(W.Code)) ,WarehouseName = W.Name
FROM @TABLE T INNER JOIN Inventory.Warehouse W WITH(NOLOCK) ON W.Id = T.WarehouseId
WHERE T.Temporal = 0

--Logica para setear los datos del Proveedor.
UPDATE T SET SupplierCode = W.Code ,SupplierName = W.Name
FROM @TABLE T INNER JOIN Common.Supplier W WITH(NOLOCK) ON W.Id = T.Supplier
WHERE T.Temporal = 0

--Logica para asignar el valor promedio de cada mes.
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
WHERE Temporal = 0 AND (ENE+ FEB+ MAR+ ABR+ MAY+ JUN+ JUL+ AGO+ SEP+ OCT+ NOV+ DIC) > 0

--Visualizacion final.
Select * FROM @TABLE 
Where Temporal = 0 AND Average>0
Order By ProductId,ANIO ASC,WarehouseId,Supplier


END