

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
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma una devolución de orden de traslado de inventario identificada por su ID y el usuario responsable. Antes de procesar, valida que todos los productos devueltos tengan subgrupo asociado, que los productos que manejan lote tengan el lote informado, y que las cantidades a devolver no superen las cantidades pendientes registradas en el detalle del traslado original. Si las validaciones pasan, incrementa el inventario físico en la bodega de origen del traslado y registra el movimiento en el Kardex llamando a SP_SavePhysicalInventoryKardex, afectando tablas de devoluciones (TransferOrderDevolution, TransferOrderDevolutionDetail), lotes/seriales (TransferOrderDetailBatchSerial), inventario físico (PhysicalInventory) y productos (InventoryProduct). Finalmente marca la devolución como confirmada y actualiza las cantidades pendientes del traslado. Se utiliza para cerrar el ciclo logístico de retorno de medicamentos e insumos entre bodegas o unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrderDevolution';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una devolución de orden de traslado: valida integridad de productos/lotes y cantidades, registra el movimiento en kardex e inventario físico (origen y, si aplica, destino), descuenta la cantidad pendiente del traslado y reporta alertas de stock.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir un TransferOrderDevolution con el Id recibido y su TransferOrder asociado; Los detalles de devolución deben referenciar TransferOrderDetailBatchSerial y PhysicalInventory válidos; Debe existir un registro en SettingInventory para la OperatingUnitId de la devolución (de allí se lee StockControl); Los productos de las líneas deben tener un ProductSubGroupId asignado; Si el subgrupo del producto maneja lote (HandlesBatch=1), el PhysicalInventory asociado debe tener BatchSerialId', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se procesa la devolución si todos los productos tienen subgrupo asociado; Para productos cuyo subgrupo maneja lote, el PhysicalInventory referenciado debe tener BatchSerialId no nulo; La cantidad a devolver de cada línea no puede exceder la OutstandingQuantity vigente del TransferOrderDetailBatchSerial; Siempre se registra un movimiento Kardex de tipo 1 (entrada) sobre la bodega origen del traslado, con costo tomado de InventoryProduct.ProductCost y AffectAverageCost=0; Si OrderType=1 además se registra un movimiento Kardex de tipo 2 sobre la bodega destino; OutstandingQuantity de TransferOrderDetailBatchSerial se decrementa exactamente en la cantidad devuelta correspondiente a la devolución procesada; Las validaciones de stock mínimo/máximo se reportan como mensaje pero no detienen la confirmación; Cualquier excepción captura ERROR_MESSAGE() y retorna StatusResult=0', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de orden de traslado; Inventario físico; Kardex; Lote / serial; Subgrupo de producto; Manejo de lote (HandlesBatch); Stock mínimo y máximo; Control de stock por producto / por almacén; Bodega origen y bodega destino; Tipo de orden de traslado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.PhysicalInventory: Vía EXEC Inventory.SP_SavePhysicalInventoryKardex con un XML que arma un movimiento MovementType=1 (entrada) sobre tro.SourceWarehouseId con la cantidad y costo del producto para incrementar el inventario físico de la bodega origen; [INSERT] Inventory.PhysicalInventory: Cuando TransferOrder.OrderType=1, vía EXEC Inventory.SP_SavePhysicalInventoryKardex con un XML adicional con MovementType=2 sobre tro.TargetWarehouseId para afectar también la bodega destino; [UPDATE] Inventory.TransferOrderDetailBatchSerial: Para cada Id presente en TransferOrderDevolutionDetail con TransferOrderDevolutionId=@Id, decrementa OutstandingQuantity en el valor de Quantity de la línea de devolución correspondiente; [RETURN_RESULT] RESULT: Retorna un único conjunto (StatusResult bit, MessageResult varchar): 0 con mensaje específico cuando falla una validación (sin subgrupo, lote nulo, cantidad excedida, error del SP de kardex con Status=3, o excepción capturada) y 1 con los mensajes acumulados de stock mín/máx en el caso exitoso', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen productos en el detalle de devolución cuyo InventoryProduct.ProductSubGroupId es NULL → Retorna StatusResult=0 con un mensaje listando los productos sin subgrupo y aborta el proceso; si Existen productos cuyo subgrupo tiene HandlesBatch=1 y PhysicalInventory.BatchSerialId es NULL → Retorna StatusResult=0 indicando que batchSerialId no puede ser nulo para productos que manejan lote y aborta; si El SP_SavePhysicalInventoryKardex devuelve algún registro con Status=3 → Retorna ese Status/Message como resultado y aborta el proceso; si Algún detalle tiene OutstandingQuantity < Quantity (cantidad a devolver mayor a la pendiente de la orden de traslado) → Retorna StatusResult=0 con mensaje indicando que la cantidad a devolver es mayor a la cantidad de la orden de traslado y aborta; si SettingInventory.StockControl = 1 (control por producto) → Valida MinimumStock/MaximumStock contra la suma de PhysicalInventory.Quantity por producto y arma mensajes de stock mínimo/máximo (informativos, no aborta); si SettingInventory.StockControl = 2 (control por almacén) → Valida MinimumStock/MaximumStock contra la suma de PhysicalInventory.Quantity filtrada por WarehouseId del almacén origen y arma mensajes (informativos); si TransferOrder.OrderType = 1 → Genera un segundo Kardex con MovementType=2 sobre el TargetWarehouseId y vuelve a aplicar la validación de stock (por producto o por almacén destino) según StockControl', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePhysicalInventoryKardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrderDevolutionDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.TransferOrderDetail; Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.PhysicalInventory; Inventory.TransferOrderDevolution; Inventory.TransferOrder; Inventory.SettingInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrderDevolution';
-- GO
