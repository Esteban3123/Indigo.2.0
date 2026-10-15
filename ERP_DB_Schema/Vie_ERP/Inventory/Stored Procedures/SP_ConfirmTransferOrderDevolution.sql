









-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 30-10-2015
-- Description:	sp que se ejecuta al confirmar una devolución de orden de traslado
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConfirmTransferOrderDevolution]
@Id As int,
@User as varchar(20)

AS
BEGIN
	SET NOCOUNT ON;

	declare @countProductNotSubGroup as int
	declare @countProductHandlesBatch as int
	declare @countstock as int
	declare @stockControl as int
	declare @orderType as int
	declare @countValidateQuantityPhy as int
	declare @countTransferOrder as int

	BEGIN TRY		

			-- consultamos los productos que no tengan un subgrupo asociado 
			select @countProductNotSubGroup = COUNT(*) FROM inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
			on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId
			inner join Inventory.InventoryProduct p on p.Id = trod.ProductId
			WHERE todd.TransferOrderDevolutionId = @Id and p.ProductSubGroupId is null

			-- si existen productos sin subgrupo asociados retornamos el mensaje de error
			if @countProductNotSubGroup > 0
			begin
				declare @errors varchar(MAX)
				select @errors=stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un subgrupo asociado'
				FROM Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
				on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = trod.ProductId
				WHERE todd.TransferOrderDevolutionId = @Id and p.ProductSubGroupId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			end

			-- consultamos los productos que manejen lote y el campo lote del item venga nulo
			select @countProductHandlesBatch = COUNT(*) FROM Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
			on todbs.Id =todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId
			inner join Inventory.InventoryProduct p on p.Id = trod.ProductId inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId 
			inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId 
			WHERE todd.TransferOrderDevolutionId = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null

			-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
			if @countProductHandlesBatch  > 0
			begin
				declare @errorsHandles varchar(MAX)
				select @errorsHandles = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
				FROM Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs on todbs.Id = todd.TransferOrderDetailBatchSerialId 
				inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId inner join Inventory.InventoryProduct p on p.Id = trod.ProductId 
				inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
				WHERE todd.TransferOrderDevolutionId = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errorsHandles as MessageResult
				return
			end

			
			declare @CodeDevolution varchar(20) = (select Code from Inventory.TransferOrderDevolution where Id = @Id)
			--- Ahora aumento el inventario fisico y afecto el Kardex
			declare @KardexXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 1 as MovementType, tro.SourceWarehouseId as WarehouseId, todd.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
			FROM inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs on todbs.Id = todd.TransferOrderDetailBatchSerialId
			inner join Inventory.TransferOrderDevolution tod on tod.Id = todd.TransferOrderDevolutionId inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId
			inner join Inventory.TransferOrder tro ON tro.id = trod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = trod.ProductId
			inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
			inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
			WHERE tod.Id = @Id
			for xml path('Kardex'), elements)
			declare @TableResultKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
			insert into @TableResultKardex
			exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @Id, @CodeDevolution, 'TransferOrderDevolution', @User

			if(select count(*) from @TableResultKardex where Status = 3) > 0 begin
				select [Status] as StatusResult, [Message] as MessageResult from @TableResultKardex
				return
			end
			
			-- validamos que la cantidad a devolver no sea mayor a la cantidad de la orden de traslado
			select @countTransferOrder = COUNT(*) FROM inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
			on todbs.Id = todd.TransferOrderDetailBatchSerialId 
			where todd.TransferOrderDevolutionId = @Id AND todbs.OutstandingQuantity < todd.Quantity

			if @countTransferOrder > 0
			begin
				declare @errorsValidateQuantityTra varchar(MAX)
				select @errorsValidateQuantityTra = STUFF((select N'; La cantidad a devolver del producto ' + p.Code	+ ' - ' + p.Name + ' es mayor a la cantidad de la orden de traslado'
				FROM inventory.TransferOrderDevolutionDetail todd inner join inventory.TransferOrderDetailBatchSerial todbs
				on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail trod on trod.Id = todbs.TransferOrderDetailId 
				inner join Inventory.InventoryProduct p on p.Id = trod.ProductId
				where todd.TransferOrderDevolutionId = @Id AND todbs.OutstandingQuantity < todd.Quantity
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errorsValidateQuantityTra as MessageResult
				return
			end

			-- actualizamos los registros de orden de traslado
			update inventory.TransferOrderDetailBatchSerial set OutstandingQuantity -= (select todd.Quantity from inventory.TransferOrderDevolutionDetail todd 
			where todd.TransferOrderDetailBatchSerialId = inventory.TransferOrderDetailBatchSerial.Id and todd.TransferOrderDevolutionId = @Id)
			where Id in (select TransferOrderDetailBatchSerialId from inventory.TransferOrderDevolutionDetail where TransferOrderDevolutionId = @Id)
			

			-- validacion Stock
			select @stockControl = StockControl FROM Inventory.SettingInventory 
			where OperatingUnitId = (select OperatingUnitId from inventory.TransferOrderDevolution where Id = @Id)

			-- validamos stock por producto
			declare @errorsStock varchar(MAX)
			if @stockControl = 1
				BEGIN

				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
				on todbs.Id = todd.TransferOrderDetailBatchSerialId	inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) + todd.Quantity )
				and todd.TransferOrderDevolutionId = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) + todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id

				if @countstock > 0
				BEGIN
				
					select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
					from inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) + todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id =todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) + todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END
			END
			-- validamos stock por almacen
			if @stockControl = 2
			BEGIN
				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
				on todd.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) + todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) + todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id

					if @countstock > 0
					BEGIN
					select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id = todd .TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
					inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
					where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) + todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
					inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
					where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) + todd.Quantity )
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					END
			END

			-- consulatamos el tipo de la orden de traslado
			select @orderType = tro.OrderType from inventory.TransferOrderDevolution tod inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId where tod.Id = @Id

			-- si el tipo de la orden de traslado es = 1 realizamos el registro al inventario fisico y la validación de stock para el almacen de destino
			if @orderType = 1
			BEGIN

				--- Ahora aumento el inventario fisico y afecto el Kardex
				declare @KardexTargetXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 2 as MovementType, tro.TargetWarehouseId as WarehouseId, todd.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
				FROM inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs on todbs.Id = todd.TransferOrderDetailBatchSerialId
				inner join Inventory.TransferOrderDevolution tod on tod.Id = todd.TransferOrderDevolutionId inner join Inventory.TransferOrderDetail trod ON trod.id = todbs.TransferOrderDetailId
				inner join Inventory.TransferOrder tro ON tro.id = trod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = trod.ProductId
				inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
				inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
				WHERE tod.Id = @Id
				for xml path('Kardex'), elements)
				declare @TableResultTargetKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
				insert into @TableResultTargetKardex
				exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexTargetXml, @Id, @CodeDevolution, 'TransferOrderDevolution', @User


				-- validamos stock por producto
				if @stockControl = 1
				BEGIN
				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
				on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id

					if @countstock > 0
					BEGIN
				
					select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs
					on todbs.Id = todd.TransferOrderDevolutionId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					--select convert(bit, 0) as StatusResult, @errorsStock as MessageResult
					--return
					END
				END
				-- validamos stock por almacen
				if @stockControl = 2
				BEGIN
				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
				on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todd.Quantity)
				and todd.TransferOrderDevolutionId = @Id

					if @countstock > 0
					BEGIN
					select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs
					on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
					inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
					where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
					from Inventory.TransferOrderDevolutionDetail todd inner join Inventory.TransferOrderDetailBatchSerial todbs 
					on todbs.Id = todd.TransferOrderDetailBatchSerialId inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
					inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
					where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todd.Quantity)
					and todd.TransferOrderDevolutionId = @Id
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					--select convert(bit, 0) as StatusResult, @errorsStockWarehouse as MessageResult
					--return
					END
				END
			END

			select convert(bit, 1) as StatusResult, @errorsStock as MessageResult
	END TRY
	BEGIN CATCH
		select convert(bit, 0) as StatusResult, 'Error ! '+ ERROR_MESSAGE() as MessageResult
	END CATCH

	END