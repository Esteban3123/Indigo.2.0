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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Genera el reporte de promedio de entradas de productos al inventario por mes y año, agrupando las cantidades recibidas por producto, almacén y proveedor. Combina los ingresos de mercancía (comprobantes de entrada) con sus respectivas devoluciones, descontando las cantidades devueltas para obtener el neto real por cada mes del período consultado. Permite filtrar por rango de fechas, almacén, proveedor, producto y estado del comprobante, calculando el promedio mensual de unidades ingresadas. Se utiliza para analizar el comportamiento histórico de abastecimiento y planificar compras o reposición de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageReportEntranceOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_AverageReportEntranceOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Genera un reporte mensualizado y promedio de entradas netas (ingresos menos devoluciones) al inventario, agrupado por producto, almacén y proveedor, en un rango de fechas con filtros opcionales.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El XML @Parameters debe contener al menos InitialDate y EndDate para delimitar el rango de DocumentDate.; Los filtros Warehouse/Supplier/Product, si vienen, deben ser listas separadas por coma de IDs convertibles a INT (procesados con dbo.Split).; El parámetro State debe poder evaluarse como entero: 1, 2 o 3 controla qué Status de comprobantes se incluye.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Las devoluciones siempre se registran como cantidades negativas para netear las entradas en el mismo periodo.; Solo las filas consolidadas (Temporal=0) reciben datos descriptivos (códigos/nombres de producto, almacén y proveedor) y promedio.; El reporte final excluye periodos con Average ≤ 0 o suma anual no positiva.; La agrupación final siempre se hace por ProductId, WarehouseId, Supplier y año del DocumentDate.; La devolución se asocia al proveedor del comprobante de entrada original (Inventory.EntranceVoucher.SupplierId vía EntranceVoucherDetailBatchSerialId), no al de la devolución.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada de inventario; Devolución a proveedor; Producto / medicamento; Almacén / bodega; Proveedor; Promedio mensual de entradas; Lote/serial de entrada', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RESULTSET: Devuelve solo filas consolidadas (Temporal=0) cuyo total anual de unidades sea > 0, ordenadas por ProductId, ANIO, WarehouseId, Supplier.; [INSERT] @TABLE: Inserta filas temporales (Temporal=1) con sumas mensuales positivas de EntranceVoucherDetail.Quantity para comprobantes Inventory.EntranceVoucher cuyo DocumentDate está en el rango y cumple los filtros.; [INSERT] @TABLE: Inserta filas temporales (Temporal=1) con sumas mensuales NEGATIVAS (-Quantity) provenientes de EntranceVoucherDevolution para descontar devoluciones a proveedor en el mismo rango y filtros.; [INSERT] @TABLE: Inserta filas consolidadas (Temporal=0) sumando entradas y devoluciones por ProductId, WarehouseId, Supplier y ANIO.; [UPDATE] @TABLE: Sobre filas consolidadas (Temporal=0) asigna ProductCode/Name desde Inventory.InventoryProduct, WarehouseCode/Name desde Inventory.Warehouse y SupplierCode/Name desde Common.Supplier.; [UPDATE] @TABLE: Calcula Average = suma de los 12 meses dividida entre el número de meses aplicable según el rango (1 si mismo mes y año, diferencia de meses si mismo año, 12-mes_inicial si ANIO=año inicial, mes_final si ANIO=año final, 12 en otros casos), solo cuando la suma anual es > 0.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si @Estado = 3 (en la primera carga de entradas) → Incluye comprobantes EntranceVoucher con cualquier Status (no aplica filtro de estado). else Filtra E.Status = IIF(@Estado=1, 2, 1): si @Estado=1 toma Status=2; cualquier otro valor toma Status=1.; si @Estado IS NULL (en la carga de devoluciones) → No filtra por Status en EntranceVoucherDevolution. else Filtra Status IN (lista parseada de @Estado por dbo.Split).; si Filtros @Warehouse / @Supplier / @Product IS NULL → No aplica el filtro respectivo. else Restringe a IDs presentes en la lista CSV parseada con dbo.Split.; si Cálculo del divisor de Average según relación entre ANIO y el rango (@InitialDate, @EndDate) → Divisor = 1 si mismo mes/año; meses entre fechas si mismo año; 12-mes_inicial si ANIO=año inicial; mes_final si ANIO=año final; 12 en otros casos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'dbo.Split', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucher; Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDevolution; Inventory.EntranceVoucherDevolutionDetail; Inventory.InventoryProduct; Inventory.Warehouse; Common.Supplier; dbo.Split', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_AverageReportEntranceOrder';
-- GO
