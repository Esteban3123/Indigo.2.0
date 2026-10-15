-- =============================================
-- Author:		Nicolas Pulido
-- Create date: 31/Marzo/2017
-- Description:	Genera el informe de inventario valorizado al corte NIIF
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ReportNIIFRealizableCost]
	@Year varchar(4),
	@Month varchar(2),
	@InitialWarehouse varchar(50),
	@FinalWarehouse varchar(50),
	@InitialGroup varchar(50),
	@FinalGroup varchar(50),
	@ReportType int
AS
BEGIN
	SET NOCOUNT ON
	
	--Fechas
	declare @InitialDate as varchar(12) = @Year + '-' + @Month, @FinalDate as varchar(12) = @Year + '-' + @Month
	--Codigo del producto
	declare @ProductCode varchar(20)
	--Nombre del producto
	declare @ProductName as varchar(200)
	--Unidad del producto
	declare @ProductUnit as varchar(100)
	--Concentracion del producto
	declare @ProductConcentration as varchar(50)
	--Costo unitario producto
	declare @ProductUnitValue as decimal = 0
	--Costo promedio producto
	declare @ProductAverangeValue as decimal = 0
	--Ultimo costo producto
	declare @ProductLastValue as decimal = 0
	--Costo venta del producto
	declare @ProductSellingValue as decimal = 0
	--Cantidad producto
	declare @ProductQuantity as decimal = 0
	--Valor total promedio producto
	declare @ProductTotalAverangeValue as decimal = 0
	--Valor total costo producto
	declare @ProductTotalLastValue as decimal = 0
	--Codigo del grupo
	declare @GroupNumber as varchar(20)
	--Nombre del grupo
	declare @GroupName as varchar(100)
	--Codigo del almacen(Warehouse)
	declare @WarehouseNumber as varchar(20)
	--Nombre del almacen
	declare @WarehouseName as varchar(100) 

	--Temporal de clase
	declare @tmpProductClass as int
	--Temporal de tipo de formulación
	declare @tmpProductFormulationType as int
	declare @tmpWeightName as varchar(100)
	declare @tmpVolumeName as varchar(100)

	--Tabla en la que se guardan los datos del reporte
	declare @RealizableCostHeader table(Id int identity(1,1), InitialDate varchar(12), FinalDate varchar(12), Code varchar(20), [Description] varchar(200), Unit varchar(100), Concentration varchar(50), UnitValue int, AverageValue int, SellingValue int, LastCost int, Quantity int, TotalValueAverangeValue int, TotalValueLastValue int, MainAccountNumber varchar(20), MainAccountName varchar(100), WarehouseNumber varchar(20), WarehouseName varchar(100))
	--Tabla temporal con los id del inventario fisico
	declare @tmpPhysicalInventoryIds table(Id int identity(1,1), PhysicalInventoryId int)
	--Id inventario fisico
	declare @PhysicalInventoryId as int

	insert into @tmpPhysicalInventoryIds
	select [pi].Id from Inventory.PhysicalInventory [pi]
	
	
	-- se declara el cursor item_cursor que va a contener los ids de los items
	DECLARE PhysicalInventory_cursor CURSOR FOR   
	select PhysicalInventoryId from @tmpPhysicalInventoryIds

	OPEN PhysicalInventory_cursor  
	FETCH NEXT FROM PhysicalInventory_cursor   
	INTO @PhysicalInventoryId

	WHILE @@FETCH_STATUS = 0
	BEGIN   

		--Consulta los datos a mostrar en el reporte
		select 
		@ProductCode = ip.Code, 
		@ProductName = ip.[Name], 
		@ProductUnitValue = ip.ProductCost, 
		@ProductAverangeValue = ip.ProductCost, 
		@ProductLastValue = ip.FinalProductCost, 
		@ProductSellingValue = ip.SellingPrice, 
		@ProductQuantity = [pi].Quantity, 
		@ProductConcentration =atc.Concentration,
		@GroupNumber = pg.Code,
		@GroupName = pg.[Name],
		@WarehouseNumber = case when @ReportType = 2 then w.Code else '' end,
		@WarehouseName = case when @ReportType = 2 then w.[Name] else '' end,
		@tmpProductFormulationType = atc.FormulationType, 
		@tmpProductClass = pt.Class
		from Inventory.PhysicalInventory [pi]
		inner join Inventory.InventoryProduct ip on ip.Id = [pi].ProductId
		inner join Inventory.Warehouse w on w.Id = [pi].WarehouseId and w.VirtualStore = 0
		inner join Inventory.ProductType pt on pt.Id = ip.ProductTypeId
		left join Inventory.ATC atc on atc.Id = ip.ATCId
		left join Inventory.ProductGroup pg on pg.Id = ip.ProductGroupId
		where [pi].Id = @PhysicalInventoryId and w.Code >= @InitialWarehouse and w.Code <= @FinalWarehouse and pg.Code >= @InitialGroup and pg.Code <= @FinalGroup

		set @ProductTotalAverangeValue = @ProductQuantity * @ProductAverangeValue
		set @ProductTotalLastValue = @ProductQuantity * @ProductLastValue

		--Varia dependiendo de la clase 
		--1 - Grupo
		--2 - Item Medicamento
		--3 - Item Insumo
		--4 - Item Otro
		if @tmpProductClass = 3 or @tmpProductClass = 4 begin
			select @ProductUnit = imu.[Name] from Inventory.InventoryMeasurementUnit imu
			inner join Inventory.InventoryProduct ip on ip.MeasurementUnitId = imu.Id
			inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
			where [pi].Id = @PhysicalInventoryId
		end
		if @tmpProductClass = 2 begin
			--Varia dependiendo de la formulacion del ATC
			--1 - Peso
			--2 - Volumen
			--3 - Peso - Volumen
			--4 - Unidad de administración
			if @tmpProductFormulationType = 1 begin
				select @ProductUnit = imu.[Name] from Inventory.InventoryMeasurementUnit imu
				inner join Inventory.ATC atc on atc.WeightMeasureUnit = imu.Id
				inner join Inventory.InventoryProduct ip on ip.ATCId = atc.Id
				inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
				where [pi].Id = @PhysicalInventoryId
			end
			else if @tmpProductFormulationType = 2 begin
				select @ProductUnit = imu.[Name] from Inventory.InventoryMeasurementUnit imu
				inner join Inventory.ATC atc on atc.VolumeMeasureUnit = imu.Id
				inner join Inventory.InventoryProduct ip on ip.ATCId = atc.Id
				inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
				where [pi].Id = @PhysicalInventoryId
			end
			else if @tmpProductFormulationType = 3 begin
				select @tmpWeightName = imu.[Name] from Inventory.InventoryMeasurementUnit imu
				inner join Inventory.ATC atc on atc.WeightMeasureUnit = imu.Id
				inner join Inventory.InventoryProduct ip on ip.ATCId = atc.Id
				inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
				where [pi].Id = @PhysicalInventoryId

				select @tmpVolumeName = imu.[Name] from Inventory.InventoryMeasurementUnit imu
				inner join Inventory.ATC atc on atc.VolumeMeasureUnit = imu.Id
				inner join Inventory.InventoryProduct ip on ip.ATCId = atc.Id
				inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
				where [pi].Id = @PhysicalInventoryId

				set @ProductUnit = @tmpWeightName + ' - ' + @tmpVolumeName
			end
			else begin
				select @ProductUnit = imu.[Name] from Inventory.InventoryMeasurementUnit imu
				inner join Inventory.ATC atc on atc.AdministrationUnitId = imu.Id
				inner join Inventory.InventoryProduct ip on ip.ATCId = atc.Id
				inner join Inventory.PhysicalInventory [pi] on [pi].ProductId = ip.Id
				where [pi].Id = @PhysicalInventoryId
			end
		end
		insert into @RealizableCostHeader(InitialDate,FinalDate,Code,[Description],UnitValue,AverageValue,LastCost,Quantity,Concentration,TotalValueAverangeValue,TotalValueLastValue,MainAccountNumber,MainAccountName,WarehouseNumber,WarehouseName,Unit,SellingValue) values(@InitialDate,@FinalDate,@ProductCode,@ProductName,@ProductUnitValue,@ProductAverangeValue,@ProductLastValue,@ProductQuantity,@ProductConcentration,@ProductTotalAverangeValue,@ProductTotalLastValue,@GroupNumber,@GroupName,@WarehouseNumber,@WarehouseName,@ProductUnit,@ProductSellingValue)
	FETCH NEXT FROM PhysicalInventory_cursor   
	INTO @PhysicalInventoryId
	END
	CLOSE PhysicalInventory_cursor;  
	DEALLOCATE PhysicalInventory_cursor;

	--Cambia dependiendo del tipo de reporte:
	--1 - Cierre Mensual sin Filtros, omite las cantidades por almacen y grupo sumandolas 
	--2 - Reporte de Cierre Mensual con Filtros
	if @ReportType = 1 begin
		select InitialDate,FinalDate,Code,[Description],Unit,Concentration,UnitValue,AverageValue,SellingValue,LastCost, sum(Quantity) as Quantity, sum(TotalValueAverangeValue) as TotalValueAverangeValue, sum(TotalValueLastValue) as TotalValueLastValue,MainAccountNumber,MainAccountName,WarehouseNumber,WarehouseName from @RealizableCostHeader where Quantity > 0 group by InitialDate,FinalDate,Code,[Description],Unit,Concentration,UnitValue,AverageValue,SellingValue,LastCost,MainAccountNumber,MainAccountName,WarehouseNumber,WarehouseName order by [Description] asc
	end
	else begin
		select InitialDate,FinalDate,Code,[Description],SellingValue,AverageValue,LastCost,UnitValue, sum(Quantity) as Quantity,Unit,Concentration, sum(TotalValueAverangeValue) as TotalValueAverangeValue,sum(TotalValueLastValue) as TotalValueLastValue,MainAccountNumber,MainAccountName,WarehouseNumber,WarehouseName from @RealizableCostHeader where Quantity > 0 group by InitialDate,FinalDate,Code,[Description],Unit,Concentration,UnitValue,AverageValue,SellingValue,LastCost,Quantity,TotalValueAverangeValue,TotalValueLastValue,MainAccountNumber,MainAccountName,WarehouseNumber,WarehouseName order by [Description] asc
	end 
END