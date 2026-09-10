

-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 28-10-2015
-- Description:	sp que se ejecuta al confirmar una orden de traslado
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConfirmTransferOrder]
@Id As int,
@User as varchar(20)

AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @StatusResult bit,
		@MessageResult varchar(max)

	EXEC [Inventory].[SP_ConfirmTransferOrder_Output]
		@Id,
		@User,
		@StatusResult Output,
		@MessageResult output

	SELECT @StatusResult AS StatusResult, @MessageResult AS MessageResult

	--declare @countProductNotSubGroup as int
	--declare @countProductHandlesBatch as int
	--declare @countstock as int
	--declare @stockControl as int
	--declare @countRequest as int
	--declare @orderType as int
	--declare @countValidateQuantityPhy as int

	--BEGIN TRY		
	--		-- consultamos los productos que no tengan un subgrupo asociado 
	--		select @countProductNotSubGroup = COUNT(*) FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--		inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--		WHERE tro.Id = @Id and p.ProductSubGroupId is null
	--		-- si existen productos sin subgrupo asociados retornamos el mensaje de error
	--		if @countProductNotSubGroup > 0
	--		begin
	--			declare @errors varchar(MAX)
	--			select @errors=stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un subgrupo asociado'
	--			FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--			WHERE tro.Id = @Id and p.ProductSubGroupId is null
	--			order by p.Code
	--			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	--			select convert(bit, 0) as StatusResult, @errors as MessageResult
	--			return
	--		end
			
	--		-- consultamos los productos que manejen lote y el campo lote del item venga nulo
	--		select @countProductHandlesBatch = COUNT(*) FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--		inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId 
	--		inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
	--		WHERE tro.Id = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null

	--		-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
	--		if @countProductHandlesBatch  > 0
	--		begin
	--			declare @errorsHandles varchar(MAX)
	--			select @errorsHandles = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
	--			FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId 
	--			inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
	--			WHERE tro.Id = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null
	--			order by p.Code
	--			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	--			select convert(bit, 0) as StatusResult, @errorsHandles as MessageResult
	--			return
	--		end
			
	--		declare @CodeTransferOrder varchar(20) = (select Code from Inventory.TransferOrder where Id = @Id)
	--		--- Ahora disminuyo el inventario fisico y afecto el Kardex
	--		declare @KardexXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 2 as MovementType, tro.SourceWarehouseId as WarehouseId, todbs.Quantity as Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
	--		FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--		inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--		inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
	--		inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
	--		WHERE tro.Id = @Id 
	--		for xml path('Kardex'), elements)
	--		declare @TableResultKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
	--		insert into @TableResultKardex
	--		exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @Id, @CodeTransferOrder, 'TransferOrder', @User
			
	--		if(select count(*) from @TableResultKardex where Status = 3) > 0 begin
	--			select cast(0 as bit) as StatusResult, [Message] as MessageResult from @TableResultKardex
	--			return
	--		end
			
	--		-- actualizamos los registros que se importaron desde solicitudes
	--		select @countRequest = count(*) from inventory.TransferOrderDetailBatchSerial todbs 
	--		inner join inventory.TransferOrderDetail tod on tod.Id = todbs.TransferOrderDetailId where tod.TransferOrderId = @Id and tod.InventoryRequestDetailId is not null
			
	--		if @countRequest > 0
	--		BEGIN
				
	--			declare @countValidateRequest as int
	--			-- validamos que la cantidad a importar desde una solicitud no sea mayor a la cantidad disponible en la solicitud al confirmar
	--			select @countValidateRequest = COUNT(*) FROM inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod 
	--			on tod.Id = todbs.TransferOrderDetailId inner join Inventory.InventoryRequestDetail red on red.Id = tod.InventoryRequestDetailId
	--			where tod.TransferOrderId = @Id AND red.OutstandingQuantity < todbs.Quantity

	--			if @countValidateRequest > 0
	--			begin
	--				declare @errorsValidateQuantityRed varchar(MAX)
	--				select @errorsValidateQuantityRed = STUFF((select N'; La cantidad pendiente de la solicitud es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + cast(red.OutstandingQuantity as varchar(20))
	--				FROM inventory.TransferOrderDetailBatchSerial todbs inner join inventory.TransferOrderDetail tod
	--				on tod.Id = todbs.TransferOrderDetailId inner join Inventory.InventoryRequestDetail red on red.Id = tod.InventoryRequestDetailId 
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				where tod.TransferOrderId = @Id AND red.OutstandingQuantity < todbs.Quantity
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
	--				select convert(bit, 0) as StatusResult, @errorsValidateQuantityRed as MessageResult
	--				return
	--			end

	--			update inventory.InventoryRequestDetail set OutstandingQuantity -= (select todbs.Quantity from inventory.TransferOrderDetailBatchSerial todbs 
	--			inner join inventory.TransferOrderDetail tod on tod.Id = todbs.TransferOrderDetailId where tod.TransferOrderId = @Id and tod.InventoryRequestDetailId is not null
	--			and tod.InventoryRequestDetailId = inventory.InventoryRequestDetail.Id)
	--			where Id in (select InventoryRequestDetailId from inventory.TransferOrderDetail where TransferOrderId = @Id and InventoryRequestDetailId is not null)
	--		END
			
	--		-- validacion Stock
	--		select @stockControl = StockControl FROM Inventory.SettingInventory 
	--		where OperatingUnitId = (select OperatingUnitId from Inventory.TransferOrder where Id = @Id)
			
	--		-- validamos stock por producto
	--		declare @errorsStock varchar(MAX)
			
	--		if @stockControl = 1
	--			BEGIN
				
	--			SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
	--			inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--			inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--			where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity )
	--			and tod.TransferOrderId = @Id
	--			or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
	--			and tod.TransferOrderId = @Id

	--			if @countstock > 0
	--			BEGIN
	--				select @errorsStock=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
	--				and tod.TransferOrderId = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				select @errorsStock+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
	--				and tod.TransferOrderId = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--			END
	--		END
	--		-- validamos stock por almacen
	--		if @stockControl = 2
	--		BEGIN
				
	--			SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
	--			inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--			inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--			where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
	--			and tro.Id = @Id
	--			or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
	--			and tro.Id = @Id
				
	--				if @countstock > 0
	--				BEGIN
	--				print ':P'
	--				select @errorsStock=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--				inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
	--				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
	--				and tro.Id = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
					
	--				select @errorsStock+= isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--				inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
	--				where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todbs.Quantity )
	--				and tro.Id = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				END
	--		END
			
	--		-- consulatamos el tipo de la orden de traslado
	--		select @orderType = OrderType from TransferOrder where Id = @Id
			
	--		-- si el tipo de la orden de traslado es = 1 realizamos el registro al inventario fisico y la validación de stock para el almacen de destino
	--		if @orderType = 1
	--		BEGIN

	--			--- Ahora aumento el inventario fisico y afecto el Kardex
	--			declare @KardexTargetXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 1 as MovementType, tro.TargetWarehouseId as WarehouseId, todbs.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
	--			FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
	--			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--			inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
	--			inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
	--			WHERE tro.Id = @Id
	--			for xml path('Kardex'), elements)
	--			declare @TableResultTargetKardex table(CodeMessage varchar(20), [Message] varchar(1000), [Status] tinyint)
	--			insert into @TableResultTargetKardex
	--			exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexTargetXml, @Id, @CodeTransferOrder, 'TransferOrder', @User
				
	--			if(select count(*) from @TableResultTargetKardex where Status = 3) > 0 begin
	--				select cast(0 as bit) as StatusResult, [Message] as MessageResult from @TableResultTargetKardex
	--				return
	--			end
				
	--			-- validacion Stock
	--			select @stockControl = StockControl FROM Inventory.SettingInventory 
	--			where OperatingUnitId = (select OperatingUnitId from Inventory.TransferOrder where Id = @Id)

	--			-- validamos stock por producto
	--			if @stockControl = 1
	--			BEGIN
	--			SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
	--			inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--			inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--			where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
	--			and tod.TransferOrderId = @Id
	--			or p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
	--			and tod.TransferOrderId = @Id

	--				if @countstock > 0
	--				BEGIN
				
	--				select @errorsStock=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
	--				and tod.TransferOrderId = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				select @errorsStock+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				where p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
	--				and tod.TransferOrderId = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				--select convert(bit, 0) as StatusResult, @errorsStock as MessageResult
	--				--return
	--				END
	--			END
	--			-- validamos stock por almacen
	--			if @stockControl = 2
	--			BEGIN
	--			SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
	--			inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--			inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--			where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
	--			and tro.Id = @Id
	--			or p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
	--			and tro.Id = @Id

	--				if @countstock > 0
	--				BEGIN
	--				select @errorsStock=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--				inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
	--				where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
	--				and tro.Id = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				select @errorsStock+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
	--				from Inventory.TransferOrderDetailBatchSerial todbs 
	--				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
	--				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
	--				inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
	--				inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
	--				where p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
	--				and tro.Id = @Id
	--				order by p.Code
	--				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
	--				--select convert(bit, 0) as StatusResult, @errorsStockWarehouse as MessageResult
	--				--return
	--				END
	--			END
	--		END
	--		print '999'
	--		select convert(bit, 1) as StatusResult, @errorsStock as MessageResult
	--END TRY
	--BEGIN CATCH
	--	select convert(bit, 0) as StatusResult, 'Error ! '+ ERROR_MESSAGE() as MessageResult
	--END CATCH

END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma una orden de traslado de inventario entre bodegas o almacenes. Recibe el identificador de la orden y el usuario que ejecuta la acción, y delega toda la lógica de validación y procesamiento al procedimiento interno SP_ConfirmTransferOrder_Output, retornando un indicador de éxito o fallo junto con el mensaje de resultado. Es el punto de entrada que activa el movimiento físico de productos, afecta el inventario y el Kardex, y cierra el ciclo de la orden de traslado en el módulo de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrder';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrder';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Wrapper que confirma una orden de traslado de inventario delegando la lógica en SP_ConfirmTransferOrder_Output y expone su resultado (estado y mensaje) como result set.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el procedimiento Inventory.SP_ConfirmTransferOrder_Output, ya que toda la lógica se delega a él.; Se debe proporcionar el identificador de la orden de traslado y el usuario que realiza la confirmación.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Toda la lógica de validación y afectación de inventario se delega al procedimiento Inventory.SP_ConfirmTransferOrder_Output; este wrapper no aplica reglas de negocio propias.; Siempre devuelve un único result set con dos columnas: StatusResult (bit) y MessageResult (varchar(max)) provenientes de los parámetros OUTPUT del SP invocado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de traslado de inventario; Confirmación de traslado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (result set): Tras invocar SP_ConfirmTransferOrder_Output con los parámetros OUTPUT @StatusResult y @MessageResult, se ejecuta SELECT @StatusResult AS StatusResult, @MessageResult AS MessageResult retornando el resultado al consumidor.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_ConfirmTransferOrder_Output', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder';
-- GO
