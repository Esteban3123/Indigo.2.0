CREATE PROCEDURE [Inventory].[SP_ConfirmRemissionEntrance]
	@Id As int,
	@User as varchar(20),
	@ControlCost as bit = 1
AS
BEGIN
	SET NOCOUNT ON;

	--Tabla temporal en donde se almacenan los errores  y poder validar
	declare @TableErrors table(Id int identity primary key, MessageError varchar(max))

	declare @countProductNotSubGroup as int
	declare @countProductHandlesBatch as int
	declare @countstock as int
	declare @stockControl as int
	declare @countPursharse as int
	declare @countContract as int
	declare @errors varchar(MAX)

	BEGIN TRY
		-- consultamos los productos que manejen lote y el campo lote del item venga nulo
		select @countProductHandlesBatch = COUNT(*) FROM Inventory.RemissionEntranceDetailBatchSerial redbs inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
		inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId inner join Inventory.InventoryProduct p on p.Id = red.ProductId inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
		WHERE re.Id = @Id and psg.HandlesBatch = 1 and redbs.BatchSerialId is null

		-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
		if @countProductHandlesBatch  > 0
		begin		
			select @errors = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
			FROM Inventory.RemissionEntranceDetailBatchSerial redbs inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
			inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId inner join Inventory.InventoryProduct p on p.Id = red.ProductId 
			inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
			WHERE re.Id = @Id and psg.HandlesBatch = 1 and redbs.BatchSerialId is null
			order by p.Code
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
			select convert(bit, 0) as StatusResult, @errors as MessageResult
			return
		end

		-- validamos que las cantidades del lote sean las misma del detalle
		IF EXISTS
		(
			SELECT red.Id
			FROM Inventory.RemissionEntranceDetail red
			LEFT JOIN Inventory.RemissionEntranceDetailBatchSerial redbs ON red.Id = redbs.RemissionEntranceDetailId
			WHERE red.RemissionEntranceId = @Id
			GROUP BY red.Id, red.Quantity
			HAVING red.Quantity <> ISNULL(SUM(redbs.Quantity), 0)
		)
		BEGIN
			SELECT @errors = stuff
			(
				(
					SELECT N'; La sumatoria de los lotes del producto ' + ip.Code + ' - ' + ip.Name + ' no son iguales al de sus detalles'
					FROM Inventory.RemissionEntranceDetail red
					JOIN Inventory.InventoryProduct ip ON red.ProductId = ip.Id
					LEFT JOIN Inventory.RemissionEntranceDetailBatchSerial redbs ON red.Id = redbs.RemissionEntranceDetailId
					WHERE red.RemissionEntranceId = @Id
					GROUP BY red.Id, red.Quantity, ip.Code, ip.Name
					HAVING red.Quantity <> ISNULL(SUM(redbs.Quantity), 0)
					FOR XML PATH(N''), TYPE
				).value(N'.[1]', N'nvarchar(max)'), 1, 2, N''
			)

			select convert(bit, 0) as StatusResult, @errors as MessageResult
			return
		END
		  	
		declare @OperatingUnitId int = (select OperatingUnitId from Inventory.RemissionEntrance where Id = @Id)
		--- Valido que en tenga parametros la unidad operativa
		if (select count(*) from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId) = 0 begin
			select convert(bit, 0) as StatusResult, 'No se encontro parametros de inventarios para la unidad operativa ' + (select UnitName from Common.OperatingUnit where Id = @OperatingUnitId) as MessageResult
		end

		IF EXISTS(	SELECT 1	
					FROM Inventory.RemissionEntranceDetail red WITH(nolock)
					JOIN Inventory.RemissionEntrance re with(NOLOCK) on red.RemissionEntranceId = re.Id
					JOIN Inventory.PurchaseOrder po WITH(NOLOCK) on red.RemissionSource=2 and po.Code=red.SourceCode
					where re.Id=@Id AND ( re.CurrencyId <> po.CurrencyId)
					
					UNION ALL
					
					SELECT 1	
					FROM Inventory.RemissionEntranceDetail red WITH(nolock)
					JOIN Inventory.RemissionEntrance re with(NOLOCK) on red.RemissionEntranceId = re.Id
					JOIN Inventory.InventoryContract ic WITH(NOLOCK) on red.RemissionSource=3 and ic.Code=red.SourceCode
					where re.Id=@Id AND (re.CurrencyId <> ic.CurrencyId)) BEGIN

				select convert(bit, 0) as StatusResult, 'La moneda de los documentos importados es diferente a la de la remisión' as MessageResult

		END

		--si existen almacenes dentro del kardex enviado que tenga habilitado restricciones
		IF EXISTS(SELECT 1  
				from Inventory.RemissionEntranceDetailBatchSerial redbs 
					inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
					inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
					inner join Inventory.InventoryProduct p ON p.Id = red.ProductId
					JOIN Inventory.Warehouse W ON W.Id = re.WarehouseId
					where W.HandleRestrictedProducts =1 and re.Id = @Id)
		BEGIN
			-------------------------PRODUCTOS RESTRINGIDOS------------------------------
			IF EXISTS 
			(
				SELECT 1 
				FROM Inventory.RemissionEntranceDetailBatchSerial redbs 
					inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
					inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
					inner join Inventory.InventoryProduct ip ON ip.Id = red.ProductId
					join Inventory.Warehouse w on w.Id = re.WarehouseId
					join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
				where re.Id=@Id  and W.HandleRestrictedProducts =1 and wc.RestrictionType=2 and (ip.Id = wc.ProductId or ip.ProductSubGroupId = wc.ProductSubgroupId or ip.ProductTypeId = wc.ProductTypeId 
				or ip.ProductGroupId = wc.ProductGroupId)  
			)
			BEGIN
				SELECT @errors = STUFF((
							SELECT DISTINCT  N'; No se logró afectar el Inventario debido a que los siguientes Productos están restringidos: ' + ip.Code + ' - ' + ip.Name
							FROM Inventory.RemissionEntranceDetailBatchSerial redbs 
								inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
								inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
								inner join Inventory.InventoryProduct ip ON ip.Id = red.ProductId
								join Inventory.Warehouse w on w.Id = re.WarehouseId
								join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
							where re.Id=@Id  and W.HandleRestrictedProducts =1 and wc.RestrictionType=2 and (ip.Id = wc.ProductId or ip.ProductSubGroupId = wc.ProductSubgroupId or ip.ProductTypeId = wc.ProductTypeId 
							or ip.ProductGroupId = wc.ProductGroupId)  
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			END

			------------------------------------PRODUCTOS HABILITADOS---------------------
			
			declare @countKardexProducts as INT= (select count(*) from Inventory.RemissionEntranceDetailBatchSerial redbs 
								inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
								inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId where re.Id= @Id)

			IF
			(
				SELECT count(*)
				FROM Inventory.RemissionEntranceDetailBatchSerial redbs 
					inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
					inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
					inner join Inventory.InventoryProduct ip ON ip.Id = red.ProductId
					join Inventory.Warehouse w on w.Id = re.WarehouseId
					join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
				where re.Id = @Id and  W.HandleRestrictedProducts =1 and wc.RestrictionType=1 and 
					(ip.Id = wc.ProductId 
					OR ip.ProductSubGroupId= wc.ProductSubgroupId 
					OR ip.ProductTypeId = wc.ProductTypeId 
					OR ip.ProductGroupId = wc.ProductGroupId)  
			)<@countKardexProducts
			BEGIN

				SELECT @errors = STUFF((
							SELECT DISTINCT  N'; No se logró afectar el Inventario debido a que los siguientes Productos no están habilitados: '+ ip.Code + ' - ' + ip.Name
							FROM Inventory.RemissionEntranceDetailBatchSerial redbs 
								inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
								inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
								inner join Inventory.InventoryProduct ip ON ip.Id = red.ProductId
								join Inventory.Warehouse w on w.Id = re.WarehouseId
								join Inventory.WarehouseRestrictedConditions wc on wc.WarehouseId = w.Id
							where re.Id=@Id  and W.HandleRestrictedProducts =1 and wc.RestrictionType=1 and (ip.Id = wc.ProductId or ip.ProductSubGroupId = wc.ProductSubgroupId or ip.ProductTypeId = wc.ProductTypeId 
							or ip.ProductGroupId = wc.ProductGroupId)  
							FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			END
		END
		

		--- Obtengo el parametro para saber si el IVA va al costo
		declare @IVACost TINYINT, @OfficialCurrencyId int
		set @IVACost = (select top 1 iif(TaxRegistration=3,1,TaxRegistration) from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId)
		set @OfficialCurrencyId =(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings)
		
		declare @warehouseId as int
		declare @productId as int
		declare @batchSerialId as int
		declare @documentDate as datetime
		declare @quantity as int		
		declare @value as decimal(20,4)
		declare @previousCost as decimal(20,4)
		declare @entityId as int
		declare @entityCode as varchar(20)
		declare @entityName as varchar(250)
		declare @importedEntityId as int
		declare @importedEntityCode as varchar(20)
		declare @importedEntityName as varchar(250)
		declare @affectInventory as bit
		DECLARE @ControlCostPercentage AS [numeric](5, 2)

		declare detail_cursor cursor for
			SELECT 
				re.WarehouseId, red.ProductId, redbs.BatchSerialId, [Common].[GETDATE](), redbs.Quantity, 
				case @IVACost 
					WHEN 1 THEN ROUND( Common.CurrencyConverterWithDate(ROUND((red.UnitValue* (1 + red.IvaPercentage / 100)),2),re.CurrencyId,@OfficialCurrencyId,CAST(re.RemissionDate AS DATE)), 2) 
					ELSE ROUND(Common.CurrencyConverterWithDate(red.UnitValue,re.CurrencyId,@OfficialCurrencyId,CAST(re.RemissionDate AS DATE)), 2) 
					END,
				ISNULL(ROUND(p.ProductCost,2),0),
				re.Id, re.Code, 'RemissionEntrance', IIF(po.Id is null,IIF(ic.Id is null,null, ic.id),po.Id), red.SourceCode, IIF(po.Id is null, IIF(ic.Id is null, null, 'InventoryContract'),'PurchaseOrder'),1, p.ControlCostPercentage
			from Inventory.RemissionEntranceDetailBatchSerial redbs 
			inner join Inventory.RemissionEntranceDetail red ON red.id = redbs.RemissionEntranceDetailId
			inner join Inventory.RemissionEntrance re ON re.id = red.RemissionEntranceId 
			inner join Inventory.InventoryProduct p ON p.Id = red.ProductId
			left outer join Inventory.PurchaseOrderDetail pod ON pod.Id = red.PurchaseOrderDetailId
			left outer join Inventory.PurchaseOrder po ON po.Id = pod.PurchaseOrderId
 			left outer join Inventory.InventoryContractDetail icd ON icd.Id = red.ContractDetailId
			left outer join Inventory.InventoryContract ic ON ic.Id = icd.InventoryContractId
			where re.Id = @Id

		open detail_cursor
				FETCH NEXT FROM detail_cursor
				INTO @warehouseId, @productId,@batchSerialId,@documentDate,@quantity, @value,@previousCost,@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,@affectInventory, @ControlCostPercentage

				WHILE @@FETCH_STATUS = 0
				BEGIN
					-- consultamos la cantidad total de items del producto que exiten en el inventario fisico
					declare @previousAmountProduct as int
					set @previousAmountProduct = ISNULL((select sum(phi.Quantity) from Inventory.PhysicalInventory phi where phi.ProductId = @productId),0)

					-- consultamos la cantidad total de items del producto que existen en el inventario fisico por el almacen
					declare @previousAmountWarehouse as int
					set @previousAmountWarehouse = ISNULL((select sum(phi.Quantity) from Inventory.PhysicalInventory phi where phi.ProductId = @productId and phi.WarehouseId = @warehouseId),0)

					-- consultamos la cantidad total de item del producto que existen en el inventario fisico por lote
					declare @previousAmountBatch as int
					set @previousAmountBatch = IIF(@batchSerialId is null, @previousAmountWarehouse, ISNULL((select sum(Quantity) from Inventory.PhysicalInventory phi 
					where phi.ProductId = @productId and phi.WarehouseId = @warehouseId and phi.batchSerialId = @batchSerialId),0))

					-- calculamos el costo promedio del prodcuto 
					declare @averageCost as decimal(20,4)
					set @averageCost = ROUND((((@previousCost * @previousAmountProduct) + (@quantity * @value))/(@previousAmountProduct + @quantity)),2)

					-- Control Costo Promedio					
					IF @affectInventory = 1 AND @ControlCost = 1 AND ISNULL(@ControlCostPercentage, 0) > 0
					BEGIN						
						DECLARE @VariationCost AS [numeric](20, 4) = ROUND((IIF(@previousCost = 0, 1, (@averageCost - @previousCost) / (@previousCost)) * 100), 2) 

						IF ABS(@VariationCost) >= @ControlCostPercentage
						BEGIN							
							INSERT INTO @TableErrors
								SELECT 'El costo promedio del producto "' + p.Name + '" tendrá un porcentaje de variación (' + CAST(@VariationCost AS VARCHAR(20)) + '%) mayor al establecido (' + CAST(@ControlCostPercentage AS VARCHAR(10)) + '%)'
								FROM Inventory.InventoryProduct p
								WHERE p.Id = @productId

							GOTO FINDETAILCURSOR
						END
					END

					--Insertamos en la tabla de Kardex
					INSERT INTO [Inventory].[Kardex]([MovementType],[WarehouseId],[ProductId],[BatchSerialId],[DocumentDate],[Quantity],[Value],[PreviousCost],
														[AverageCost],[PreviousAverageCost],[PreviousAmountProduct],[PreviousAmountWarehouse],[PreviousAmountBatch],
														[EntityId],[EntityCode],[EntityName],[ImportedEntityId],[ImportedEntityCode],[ImportedEntityName],
														[AffectInventory],[CreationUser],[CreationDate])
														Values
														(1,@warehouseId, @productId,@batchSerialId,@documentDate,@quantity,@value,@previousCost,@averageCost,@previousCost,
														@previousAmountProduct,@previousAmountWarehouse,@previousAmountBatch,@entityId,@entityCode,@entityName,@importedEntityId,
														@importedEntityCode,@importedEntityName,@affectInventory,@User,[Common].[GETDATE]())

					-- actualizamos el costo y costo final del producto
					update inventory.InventoryProduct set ProductCost = @averageCost,
					FinalProductCost = @value
					where Id = @productId

					-- afectamos el inventario fisico
					if(select Count(*) from Inventory.PhysicalInventory where WarehouseId = @warehouseId And ProductId = @productId And ISNULL(BatchSerialId,0) = ISNULL(@batchSerialId,0)) > 0
					BEGIN
						-- actualizamos la tabla de inventario fisico de los productos que ya esten registrado en el inventario fisico
						update inventory.PhysicalInventory set Quantity += @quantity
						where ProductId = @productId
						and WarehouseId = @warehouseId
						and ISNULL(BatchSerialId, 0) = ISNULL(@batchSerialId,0)
					END
					else
					BEGIN
						-- insertamos en la tabla de inventario fisico los prdocutos que aun no esten registrados en el inventario fisico
						insert into Inventory.PhysicalInventory ([WarehouseId],[ProductId],[BatchSerialId],[Quantity]) Values (@warehouseId, @productId, @batchSerialId, @quantity)
					END

					FINDETAILCURSOR:
					FETCH NEXT FROM detail_cursor
						INTO @warehouseId, @productId,@batchSerialId,@documentDate,@quantity, @value,@previousCost,@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,@affectInventory, @ControlCostPercentage
				End

		close detail_cursor
		deallocate detail_cursor

		--Si hubo errores se devuelven los mensajes
		if (select COUNT(*) from @TableErrors) > 0
		begin
			declare @MessageError varchar(max)
			select @MessageError = stuff((select N'' + MessageError + CHAR(13) + CHAR(10) from @TableErrors
			for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1,0, N'')
			select convert(bit, 0) as StatusResult, @MessageError as MessageResult
			return
		end

		-- actualizamos los registros que se importaron desde orden de compra
		select @countPursharse = count(*) from inventory.RemissionEntranceDetailBatchSerial redbs 
		inner join inventory.RemissionEntranceDetail red on red.Id = redbs.RemissionEntranceDetailId where red.RemissionEntranceId = @Id and red.RemissionSource = 2

		if @countPursharse > 0
		BEGIN
			declare @countValidatePursharse as int
		
			--Se valida que las ordenes de compra importadas esten confirmadas por el nuevo requerimiento de desconfirmación de orden de compra
			if (select count(*) 
			from Inventory.RemissionEntranceDetail red
			inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId
			inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId
			where red.RemissionEntranceId = @Id and po.Status = 1) > 0
			begin
				select @errors = STUFF((select N'; La orden de compra ' + po.Code	+ ' no está confirmada'
				from Inventory.RemissionEntranceDetail red
				inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId
				inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId
				where red.RemissionEntranceId = @Id and po.Status = 1
				order by po.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			end

			-- validamos que la cantidad a importar desde orden de compra no sea mayor a la cantidad disponible en la orden de compra al confirmar
			select @countValidatePursharse = COUNT(*) FROM inventory.RemissionEntranceDetailBatchSerial redbs inner join Inventory.RemissionEntranceDetail red 
			on red.Id = redbs.RemissionEntranceDetailId inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId
			where red.RemissionEntranceId = @Id AND pod.OutstandingQuantity < redbs.Quantity

			if @countValidatePursharse > 0
			begin
				select @errors = STUFF((select N'; La cantidad pendiente de la orden de compra es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + CAST( pod.OutstandingQuantity as varchar(30))
				FROM inventory.RemissionEntranceDetailBatchSerial redbs inner join inventory.RemissionEntranceDetail red
				on red.Id = redbs.RemissionEntranceDetailId inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId 
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				where red.RemissionEntranceId = @Id AND pod.OutstandingQuantity < redbs.Quantity
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			end

			UPDATE pod
				SET pod.OutstandingQuantity = pod.OutstandingQuantity - redbs.Quantity,
					pod.CancelledQuantity = pod.CancelledQuantity + redbs.Quantity				
			FROM inventory.PurchaseOrderDetail pod
			JOIN Inventory.RemissionEntranceDetail red ON pod.Id = red.PurchaseOrderDetailId AND red.RemissionSource = 2
			JOIN Inventory.RemissionEntranceDetailBatchSerial redbs ON red.Id = redbs.RemissionEntranceDetailId
			WHERE red.RemissionEntranceId = @Id
		END

		-- actualizamos los registros que importaron desde contraro
		select @countContract = count(*) from inventory.RemissionEntranceDetailBatchSerial redbs 
		inner join inventory.RemissionEntranceDetail red on red.Id = redbs.RemissionEntranceDetailId where red.RemissionEntranceId = @Id and red.RemissionSource = 3

		if @countContract > 0
		BEGIN
			declare @countValidateContract as int
			-- validamos que la cantidad a importar desde contrato no sea mayor a la cantidad disponible en el contrato al confirmar
			select @countValidateContract = COUNT(*) FROM inventory.RemissionEntranceDetailBatchSerial redbs inner join Inventory.RemissionEntranceDetail red 
			on red.Id = redbs.RemissionEntranceDetailId inner join Inventory.InventoryContractDetail cod on cod.Id = red.PurchaseOrderDetailId
			where red.RemissionEntranceId = @Id AND cod.OutstandingQuantity < redbs.Quantity

			if @countValidateContract > 0
			begin
				select @errors = STUFF((select N'; La cantidad pendiente del contrato es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' +  CAST(cod.OutstandingQuantity as varchar(30))
				FROM inventory.RemissionEntranceDetailBatchSerial redbs inner join inventory.RemissionEntranceDetail red
				on red.Id = redbs.RemissionEntranceDetailId inner join Inventory.InventoryContractDetail cod on cod.Id = red.PurchaseOrderDetailId 
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				where red.RemissionEntranceId = @Id AND cod.OutstandingQuantity < red.Quantity
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			end

			update inventory.InventoryContractDetail set OutstandingQuantity -= (select redbs.Quantity from inventory.RemissionEntranceDetailBatchSerial redbs 
			inner join inventory.RemissionEntranceDetail red on red.Id = redbs.RemissionEntranceDetailId where red.RemissionEntranceId = @Id and red.RemissionSource = 3
			and red.ContractDetailId = inventory.InventoryContractDetail.Id),
			CancelledQuantity += (select redbs.Quantity from inventory.RemissionEntranceDetailBatchSerial redbs 
			inner join inventory.RemissionEntranceDetail red on red.Id = redbs.RemissionEntranceDetailId where red.RemissionEntranceId = @Id and red.RemissionSource = 3
			and red.ContractDetailId = inventory.InventoryContractDetail.Id)
			where Id in (select ContractDetailId from inventory.RemissionEntranceDetail where RemissionEntranceId = @Id and RemissionSource = 3)
		END

		-- validacion Stock
		select @stockControl = StockControl FROM Inventory.SettingInventory 
		where OperatingUnitId = (select OperatingUnitId from Inventory.RemissionEntrance where Id = @Id)

		-- validamos stock por producto	
		if @stockControl = 1
		BEGIN
			SELECT @countstock = COUNT(*) from Inventory.RemissionEntranceDetailBatchSerial redbs 
			inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId
			where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
			and red.RemissionEntranceId = @Id
			or p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
			and red.RemissionEntranceId = @Id

			if @countstock > 0
			BEGIN				
				select @errors = stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
				from Inventory.RemissionEntranceDetailBatchSerial redbs 
				inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
				and red.RemissionEntranceId = @Id
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select @errors += stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
				from Inventory.RemissionEntranceDetailBatchSerial redbs 
				inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				where p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
				and red.RemissionEntranceId = @Id
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				--select convert(bit, 0) as StatusResult, @errors as MessageResult
				--return
			END
		END

		-- validamos stock por almacen
		if @stockControl = 2
		BEGIN
			SELECT @countstock = COUNT(*) from Inventory.RemissionEntranceDetailBatchSerial redbs 
			inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId inner join Inventory.RemissionEntrance re on re.Id = red.RemissionEntranceId
			where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
			and re.Id = @Id
			or p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
			and re.Id = @Id

			if @countstock > 0
			BEGIN
				select @errors = stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
				from Inventory.RemissionEntranceDetailBatchSerial redbs 
				inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				inner join Inventory.RemissionEntrance re on re.Id = red.RemissionEntranceId
				inner join Inventory.Warehouse w on w.Id = re.WarehouseId
				where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
				and re.Id = @Id
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				select @errors += stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
				from Inventory.RemissionEntranceDetailBatchSerial redbs 
				inner join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				inner join Inventory.RemissionEntrance re on re.Id = red.RemissionEntranceId
				inner join Inventory.Warehouse w on w.Id = re.WarehouseId
				where p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
				and re.Id = @Id
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				--select convert(bit, 0) as StatusResult, @errors as MessageResult
				--return
			END
		END

		select convert(bit, 1) as StatusResult, @errors as MessageResult
	END TRY
	BEGIN CATCH
		select convert(bit, 0) as StatusResult, 'Error ! '+ ERROR_MESSAGE() as MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma el ingreso de una remisión de entrada al inventario, validando que todos los ítems que manejan lote tengan su número de lote o serial asignado, que las cantidades de los lotes coincidan con las del detalle de la remisión, que la moneda de los documentos origen (órdenes de compra o contratos) sea coherente con la de la remisión, y que los productos no estén restringidos en el almacén de destino. Opera sobre las tablas de remisiones de entrada, sus detalles, lotes/seriales, catálogo de productos y subgrupos para asegurar la integridad del proceso de recepción de mercancía o insumos. Si alguna validación falla, retorna un mensaje descriptivo de error sin confirmar el documento; si todas pasan, concreta el ingreso al stock del almacén. Es el punto de control obligatorio antes de que un documento de entrada afecte el inventario real.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmRemissionEntrance';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmRemissionEntrance';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma una remisión de entrada validando lotes, restricciones, monedas, costos y stock; afecta Kardex e inventario físico, actualiza costos del producto y descuenta saldos pendientes en órdenes de compra o contratos vinculados.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La remisión de entrada debe existir y tener detalles con lotes/series asociados.; Los productos cuyo subgrupo HandlesBatch=1 deben traer BatchSerialId no nulo en su detalle de lote.; La sumatoria de cantidades por lote debe coincidir con la cantidad del detalle de la remisión.; Debe existir registro en SettingInventory para la unidad operativa de la remisión.; La moneda de la remisión debe coincidir con la moneda de la orden de compra (RemissionSource=2) o contrato (RemissionSource=3) importado.; Las órdenes de compra importadas no deben estar en Status=1 (no confirmadas).; La cantidad por lote a importar no puede superar OutstandingQuantity del detalle de orden de compra o contrato.; Si el almacén tiene HandleRestrictedProducts=1, los productos no deben estar bajo RestrictionType=2 y deben estar habilitados (RestrictionType=1) para todas las líneas.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.Kardex: Por cada fila del cursor de detalle (RemissionEntranceDetailBatchSerial) se inserta un movimiento MovementType=1 con cantidades, costo previo, costo promedio recalculado y datos de entidad importada (PurchaseOrder/InventoryContract).; [UPDATE] inventory.InventoryProduct: Tras insertar Kardex, se actualiza ProductCost = costo promedio recalculado y FinalProductCost = valor unitario convertido del movimiento.; [UPDATE] Inventory.PhysicalInventory: Si ya existe registro para (WarehouseId, ProductId, BatchSerialId), se incrementa Quantity en la cantidad recibida del lote.; [INSERT] Inventory.PhysicalInventory: Si no existe registro para (WarehouseId, ProductId, BatchSerialId), se inserta una nueva fila con la cantidad recibida.; [UPDATE] Inventory.PurchaseOrderDetail: Para detalles con RemissionSource=2, se reduce OutstandingQuantity y se incrementa CancelledQuantity en la cantidad del lote ingresado.; [UPDATE] Inventory.InventoryContractDetail: Para detalles con RemissionSource=3, se reduce OutstandingQuantity y se incrementa CancelledQuantity en la cantidad del lote ingresado.; [INSERT] @TableErrors: Si @ControlCost=1 y |variación de costo promedio| >= ControlCostPercentage del producto, se registra un mensaje de error de variación y se omite la afectación de inventario para esa línea (GOTO FINDETAILCURSOR).; [RETURN_RESULT] resultset: Devuelve StatusResult=0 con mensajes concatenados cuando falla alguna validación (lotes nulos, sumas, monedas, restringidos, no habilitados, OC no confirmada, cantidades pendientes insuficientes, variación de costo) y StatusResult=1 al finalizar exitosamente.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmRemissionEntrance';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmRemissionEntrance';
-- GO
