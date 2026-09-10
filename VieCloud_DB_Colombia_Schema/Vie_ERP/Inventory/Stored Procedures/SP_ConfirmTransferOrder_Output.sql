-- =============================================
-- Author:		Juan Carlos Bermudez Gutierrez
-- Create date: 28-10-2015
-- Description:	sp que se ejecuta al confirmar una orden de traslado
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConfirmTransferOrder_Output]
	@Id As int,
	@User AS varchar(20),
	@StatusResult Bit Output,
	@MessageResult Varchar(max) output
AS
BEGIN

	SET NOCOUNT ON;

	declare @errors varchar(MAX)
	declare @countProductNotSubGroup as int
	declare @countProductHandlesBatch as int
	declare @countstock as int
	declare @stockControl as int
	declare @countRequest as int
	declare @orderType as int
	declare @countValidateQuantityPhy as int

	BEGIN TRY
			-- se valida que todos los detalles corresponden con sus lotes
			IF EXISTS (
				SELECT 1 
				FROM Inventory.TransferOrderDetail trod 
				LEFT JOIN 
				(
					SELECT trodbs.TransferOrderDetailId, SUM(trodbs.Quantity) Quantity
					FROM Inventory.TransferOrderDetailBatchSerial trodbs 
					GROUP BY trodbs.TransferOrderDetailId
				) trodbs ON trod.Id = trodbs.TransferOrderDetailId
				WHERE trod.TransferOrderId = @Id AND trod.Quantity <> ISNULL(trodbs.Quantity, 0)
			)
			begin				
				select @errors=stuff((
									SELECT DISTINCT N'; La cantidad del detalle del producto ' + ip.Code	+ ' - ' + ip.Name + ' no coincide con el de sus lotes'
									FROM Inventory.TransferOrderDetail trod 
									JOIN Inventory.InventoryProduct ip ON trod.ProductId = ip.Id 
									LEFT JOIN 
									(
										SELECT trodbs.TransferOrderDetailId, SUM(trodbs.Quantity) Quantity
										FROM Inventory.TransferOrderDetailBatchSerial trodbs 
										GROUP BY trodbs.TransferOrderDetailId
									) trodbs ON trod.Id = trodbs.TransferOrderDetailId
									WHERE trod.TransferOrderId = @Id AND trod.Quantity <> ISNULL(trodbs.Quantity, 0)
									for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SET @StatusResult = 0
				SET @MessageResult = @errors
				return
			end

			---- se valida que el costo promedio sea el mismo del detalle
			--IF EXISTS (SELECT 1 FROM Inventory.TransferOrderDetail trod JOIN Inventory.InventoryProduct ip ON trod.ProductId = ip.Id WHERE trod.TransferOrderId = @Id AND trod.Value <> CAST(ip.ProductCost AS DECIMAL(18,2)))
			--begin				
			--	select @errors=stuff((
			--						SELECT DISTINCT N'; El Costo promedio del producto ' + ip.Code	+ ' - ' + ip.Name + ' ha cambiado'
			--						FROM Inventory.TransferOrderDetail trod 
			--						JOIN Inventory.InventoryProduct ip ON trod.ProductId = ip.Id 
			--						WHERE trod.TransferOrderId = @Id AND trod.Value <> CAST(ip.ProductCost AS DECIMAL(18,2))
			--						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

			--	SET @StatusResult = 0
			--	SET @MessageResult = @errors
			--	return
			--end

			-- consultamos los productos que no tengan un subgrupo asociado 
			select @countProductNotSubGroup = COUNT(*) FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
			WHERE tro.Id = @Id and p.ProductSubGroupId is null
			-- si existen productos sin subgrupo asociados retornamos el mensaje de error
			if @countProductNotSubGroup > 0
			begin
				select @errors=stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un subgrupo asociado'
				FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
				inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
				WHERE tro.Id = @Id and p.ProductSubGroupId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SET @StatusResult = 0
				SET @MessageResult = @errors
				return
			end
			
			-- consultamos los productos que manejen lote y el campo lote del item venga nulo
			select @countProductHandlesBatch = COUNT(*) FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId 
			inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
			WHERE tro.Id = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null

			-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
			if @countProductHandlesBatch  > 0
			begin
				select @errors = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
				FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
				inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId 
				inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
				WHERE tro.Id = @Id and psg.HandlesBatch = 1 and phy.BatchSerialId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SET @StatusResult = 0
				SET @MessageResult = @errors
				return
			end
			
			declare @CodeTransferOrder varchar(20) = (select Code from Inventory.TransferOrder where Id = @Id)
			--- Ahora disminuyo el inventario fisico y afecto el Kardex
			declare @KardexXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 2 as MovementType, tro.SourceWarehouseId as WarehouseId, todbs.Quantity as Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
			FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
			inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
			inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
			inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
			WHERE tro.Id = @Id 
			for xml path('Kardex'), elements)

			declare @TableResultKardex table(CodeMessage varchar(20), [Message] varchar(MAX), [Status] tinyint)
			insert into @TableResultKardex
			exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexXml, @Id, @CodeTransferOrder, 'TransferOrder', @User
			
			if(select count(*) from @TableResultKardex where Status = 3) > 0 begin
				--select cast(0 as bit) as StatusResult, [Message] as MessageResult from @TableResultKardex
				SET @StatusResult = 0
				SET @MessageResult = (select TOP 1 [Message] from @TableResultKardex)
				return
			end
			
			-- actualizamos los registros que se importaron desde solicitudes
			select @countRequest = count(*) from inventory.TransferOrderDetailBatchSerial todbs 
			inner join inventory.TransferOrderDetail tod on tod.Id = todbs.TransferOrderDetailId where tod.TransferOrderId = @Id and tod.InventoryRequestDetailId is not null
			
			if @countRequest > 0
			BEGIN
				
				declare @countValidateRequest as int
				-- validamos que la cantidad a importar desde una solicitud no sea mayor a la cantidad disponible en la solicitud al confirmar
				select @countValidateRequest = COUNT(*) FROM inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod 
				on tod.Id = todbs.TransferOrderDetailId inner join Inventory.InventoryRequestDetail red on red.Id = tod.InventoryRequestDetailId
				where tod.TransferOrderId = @Id AND red.OutstandingQuantity < todbs.Quantity

				--Si el permiso 'Quitar Validación Cantidades Importadas' esta en 'Si' no se realiza la validación
				declare @PermissionValidateQuantity int = (select count(*)
														   from Security.PermissionUser pu
														   inner join Security.[User] u on u.Id = pu.IdUser
														   where pu.IdForm = '1519' and pu.Action = '91' and u.UserCode = @User)

				if @countValidateRequest > 0 and @PermissionValidateQuantity = 0
				begin
					select @errors = STUFF((select N'; La cantidad pendiente de la solicitud es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + cast(red.OutstandingQuantity as varchar(20))
					FROM inventory.TransferOrderDetailBatchSerial todbs inner join inventory.TransferOrderDetail tod
					on tod.Id = todbs.TransferOrderDetailId inner join Inventory.InventoryRequestDetail red on red.Id = tod.InventoryRequestDetailId 
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where tod.TransferOrderId = @Id AND red.OutstandingQuantity < todbs.Quantity
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					SET @StatusResult = 0
					SET @MessageResult = @errors
					return
				end

				update inventory.InventoryRequestDetail 
					set OutstandingQuantity -= (
						select SUM(todbs.Quantity) 
						from inventory.TransferOrderDetailBatchSerial todbs 
						inner join inventory.TransferOrderDetail tod on tod.Id = todbs.TransferOrderDetailId 
						where tod.TransferOrderId = @Id 
							and tod.InventoryRequestDetailId is not null
							and tod.InventoryRequestDetailId = inventory.InventoryRequestDetail.Id
						)
				where Id in (select InventoryRequestDetailId from inventory.TransferOrderDetail where TransferOrderId = @Id and InventoryRequestDetailId is not null)
			END
			
			-- validacion Stock
			select @stockControl = StockControl FROM Inventory.SettingInventory 
			where OperatingUnitId = (select OperatingUnitId from Inventory.TransferOrder where Id = @Id)
			
			-- validamos stock por producto
			if @stockControl = 1
			BEGIN				
				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity )
				and tod.TransferOrderId = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
				and tod.TransferOrderId = @Id

				if @countstock > 0
				BEGIN
					select @errors=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
						from Inventory.TransferOrderDetailBatchSerial todbs 
						inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
						inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
						where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
						and tod.TransferOrderId = @Id
						order by p.Code
						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')

					select @errors+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
						from Inventory.TransferOrderDetailBatchSerial todbs 
						inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
						inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
						where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId) - todbs.Quantity)
						and tod.TransferOrderId = @Id
						order by p.Code
						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
				END
			END

			-- validamos stock por almacen
			if @stockControl = 2
			BEGIN				
				SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
				inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
				inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
				where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
				and tro.Id = @Id
				or p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
				and tro.Id = @Id
				
				if @countstock > 0
				BEGIN
					select @errors=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
						from Inventory.TransferOrderDetailBatchSerial todbs 
						inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
						inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
						inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
						inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
						where p.MinimumStock > ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.SourceWarehouseId) - todbs.Quantity)
						and tro.Id = @Id
						order by p.Code
						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
					
					select @errors+= isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
						from Inventory.TransferOrderDetailBatchSerial todbs 
						inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
						inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
						inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
						inner join Inventory.Warehouse w on w.Id = tro.SourceWarehouseId
						where p.MaximumStock < ((select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId) - todbs.Quantity )
						and tro.Id = @Id
						order by p.Code
						for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
				END
			END
			
			-- consulatamos el tipo de la orden de traslado
			select @orderType = OrderType from TransferOrder where Id = @Id
			
			-- si el tipo de la orden de traslado es = 1 realizamos el registro al inventario fisico y la validación de stock para el almacen de destino
			if @orderType = 1
			BEGIN
				--- Ahora aumento el inventario fisico y afecto el Kardex
				declare @KardexTargetXml xml = (select null as ThirdPartyId, ip.Id as ProductId, phy.BatchSerialId, 1 as MovementType, tro.TargetWarehouseId as WarehouseId, todbs.Quantity, ip.ProductCost as Value, 0 as AffectAverageCost
				FROM Inventory.TransferOrderDetailBatchSerial todbs inner join Inventory.TransferOrderDetail tod ON tod.id = todbs.TransferOrderDetailId
				inner join Inventory.TransferOrder tro ON tro.id = tod.TransferOrderId inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
				inner join Inventory.PhysicalInventory phy on phy.Id = todbs.PhysicalInventoryId
				inner join Inventory.InventoryProduct ip on ip.Id = phy.ProductId
				WHERE tro.Id = @Id
				for xml path('Kardex'), elements)
				declare @TableResultTargetKardex table(CodeMessage varchar(20), [Message] varchar(MAX), [Status] tinyint)
				insert into @TableResultTargetKardex
				exec [Inventory].[SP_SavePhysicalInventoryKardex] @KardexTargetXml, @Id, @CodeTransferOrder, 'TransferOrder', @User
				
				if(select count(*) from @TableResultTargetKardex where Status = 3) > 0 begin
					--select cast(0 as bit) as StatusResult, [Message] as MessageResult from @TableResultTargetKardex
					SET @StatusResult = 0
					SET @MessageResult = (SELECT TOP 1 [Message] from @TableResultTargetKardex)
					return
				end
				
				-- validacion Stock
				select @stockControl = StockControl FROM Inventory.SettingInventory 
				where OperatingUnitId = (select OperatingUnitId from Inventory.TransferOrder where Id = @Id)

				-- validamos stock por producto
				if @stockControl = 1
				BEGIN
					SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
					inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
					where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
					and tod.TransferOrderId = @Id
					or p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
					and tod.TransferOrderId = @Id

					if @countstock > 0
					BEGIN				
						select @errors=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
							from Inventory.TransferOrderDetailBatchSerial todbs 
							inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
							inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
							where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
							and tod.TransferOrderId = @Id
							order by p.Code
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')

						select @errors+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
							from Inventory.TransferOrderDetailBatchSerial todbs 
							inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
							inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
							where p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId))
							and tod.TransferOrderId = @Id
							order by p.Code
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
					END
				END

				-- validamos stock por almacen
				if @stockControl = 2
				BEGIN
					SELECT @countstock = COUNT(*) from Inventory.TransferOrderDetailBatchSerial todbs 
					inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
					inner join Inventory.InventoryProduct p on p.Id = tod.ProductId inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
					where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
					and tro.Id = @Id
					or p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
					and tro.Id = @Id

					if @countstock > 0
					BEGIN
						select @errors=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
							from Inventory.TransferOrderDetailBatchSerial todbs 
							inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
							inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
							inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
							inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
							where p.MinimumStock > (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
							and tro.Id = @Id
							order by p.Code
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')

						select @errors+=isnull(stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
							from Inventory.TransferOrderDetailBatchSerial todbs 
							inner join Inventory.TransferOrderDetail tod ON tod.Id = todbs.TransferOrderDetailId
							inner join Inventory.InventoryProduct p on p.Id = tod.ProductId
							inner join Inventory.TransferOrder tro on tro.Id = tod.TransferOrderId
							inner join Inventory.Warehouse w on w.Id = tro.TargetWarehouseId
							where p.MaximumStock < (todbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = tod.ProductId and phy.WarehouseId = tro.TargetWarehouseId))
							and tro.Id = @Id
							order by p.Code
							for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''),'')
					END
				END
			END

			SET @StatusResult = 1
			SET @MessageResult = @errors
	END TRY
	BEGIN CATCH
		SET @StatusResult = 0
		SET @MessageResult = (SELECT 'Error ! '+ ERROR_MESSAGE() + ' - Linea: ' + CAST(ERROR_LINE() AS VARCHAR(20)))
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Procedimiento que confirma la salida de una orden de traslado de inventario entre bodegas o unidades funcionales. Antes de procesar el movimiento, realiza una serie de validaciones de negocio: verifica que las cantidades declaradas en cada línea de detalle coincidan con las cantidades registradas por lote o serial, que todos los productos tengan un subgrupo asociado, y que los productos que manejan lote tengan el número de lote informado. Una vez superadas las validaciones, descuenta el stock físico (inventario físico) de la bodega origen y genera el movimiento en el Kardex para cada producto, lote y serial incluidos en la orden. Devuelve un indicador de éxito o error junto con un mensaje descriptivo que identifica el producto específico que causó el problema, facilitando la trazabilidad del movimiento de mercancías, medicamentos e insumos médicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmTransferOrder_Output';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una orden de traslado validando coherencia de lotes, subgrupos, stock y solicitudes asociadas; descuenta inventario del almacén origen vía Kardex y, si la orden es interna (OrderType=1), también ingresa al almacén destino.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'@Id debe corresponder a una TransferOrder existente con su detalle y lotes/seriales registrados; Debe existir un registro en SettingInventory para la OperatingUnitId de la orden, con StockControl definido; Las filas de PhysicalInventory referenciadas por TransferOrderDetailBatchSerial deben existir; @User debe corresponder a un Security.User existente para evaluar el permiso de validación de cantidades importadas; Los productos involucrados deben tener ProductSubGroupId asignado y, si manejan lote, su PhysicalInventory debe tener BatchSerialId', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Antes de afectar el Kardex, todas las cantidades de detalle deben coincidir exactamente con la suma de sus lotes/seriales; Solo se procesa el traslado si todos los productos tienen subgrupo asignado; Si el subgrupo del producto maneja lote (HandlesBatch=1), el registro de PhysicalInventory referenciado debe tener BatchSerialId no nulo; El movimiento Kardex de salida usa MovementType=2 contra SourceWarehouseId con AffectAverageCost=0 (no recalcula costo promedio); El movimiento Kardex de entrada (solo OrderType=1) usa MovementType=1 contra TargetWarehouseId con AffectAverageCost=0; OutstandingQuantity de la solicitud nunca queda por debajo de cero salvo que exista el permiso IdForm=1519/Action=91, ya que la validación lo impide; Las advertencias de stock mínimo/máximo no abortan la confirmación; se devuelven en @MessageResult con @StatusResult=1; Cualquier error capturado en CATCH retorna @StatusResult=0 con el mensaje de error y la línea', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Orden de traslado; Inventario físico; Kardex; Lote/Serial; Subgrupo de producto; Solicitud de inventario; Stock mínimo y máximo; Control de stock por producto/almacén; Almacén origen y destino; Costo promedio del producto; Permiso de seguridad por formulario y acción', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existe algún detalle cuya cantidad (TransferOrderDetail.Quantity) no coincide con la suma de cantidades de sus lotes/seriales en TransferOrderDetailBatchSerial → Construye mensaje de error por producto, fija @StatusResult=0 y retorna sin afectar inventario; si Algún producto incluido no tiene ProductSubGroupId asignado → Devuelve error indicando que el producto no tiene subgrupo asociado y retorna; si Producto pertenece a un subgrupo con HandlesBatch=1 y PhysicalInventory.BatchSerialId es NULL → Devuelve error ''batchSerialId no puede ser nulo'' y retorna; si SP_SavePhysicalInventoryKardex retorna alguna fila con Status=3 (al disminuir bodega origen) → Fija @StatusResult=0 con el primer mensaje recibido y retorna; si Existen detalles importados desde una solicitud (InventoryRequestDetailId no nulo) y red.OutstandingQuantity < cantidad a descontar y el usuario NO tiene el permiso IdForm=1519, Action=91 (''Quitar Validación Cantidades Importadas'') → Devuelve error ''cantidad pendiente de la solicitud es menor que la cantidad a descontar'' y retorna else Si hay solicitudes asociadas, descuenta la cantidad trasladada de InventoryRequestDetail.OutstandingQuantity; si SettingInventory.StockControl = 1 (control por producto) → Valida si el saldo total del producto tras el egreso queda por debajo de MinimumStock o por encima de MaximumStock y acumula advertencias en @errors; si SettingInventory.StockControl = 2 (control por almacén) → Valida MinimumStock/MaximumStock contra el saldo en el almacén origen y acumula advertencias por almacén; si TransferOrder.OrderType = 1 → Genera segundo movimiento Kardex de tipo 1 (entrada) hacia TargetWarehouseId y repite la validación de stock (por producto o almacén) sobre el almacén destino else No se realiza ingreso al almacén destino; si Segunda llamada a SP_SavePhysicalInventoryKardex (destino) retorna fila con Status=3 → Fija @StatusResult=0 con el primer mensaje y retorna', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Inventory.SP_SavePhysicalInventoryKardex', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrderDetail; Inventory.TransferOrderDetailBatchSerial; Inventory.InventoryProduct; Inventory.TransferOrder; Inventory.ProductSubGroup; Inventory.PhysicalInventory; Inventory.InventoryRequestDetail; Inventory.SettingInventory; Inventory.Warehouse; Security.PermissionUser; Security.User', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmTransferOrder_Output';
-- GO
