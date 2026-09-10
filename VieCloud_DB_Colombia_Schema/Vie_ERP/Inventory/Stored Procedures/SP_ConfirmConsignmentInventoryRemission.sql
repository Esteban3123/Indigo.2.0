-- =============================================
-- Author:		Miguel Angel Fonseca
-- Create date: 2017-12-14
-- Description:	Procedimiento almacenado que se ejecuta al confirmar una remision de inventario en consignación
-- =============================================
CREATE PROCEDURE [Inventory].[SP_ConfirmConsignmentInventoryRemission]
@Id As int,
@User as varchar(20),
@ControlCost as bit = 1

AS
BEGIN
	SET NOCOUNT ON;

	--Tabla temporal en donde se almacenan los errores  y poder validar
	declare @TableErrors table(Id int identity primary key, MessageError varchar(max))
	--Variable para retornar el mensaje de error
	declare @MessageError varchar(max)

	declare @countProductNotSubGroup as int
	declare @countProductHandlesBatch as int
	declare @countstock as int
	declare @stockControl as int
	declare @countPursharse as int
	declare @countContract as int

	BEGIN TRY
			-- consultamos los productos que no tengan un subgrupo asociado 
			select @countProductNotSubGroup = COUNT(*) 
			FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
			inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.id = redbs.ConsignmentInventoryRemissionDetailId
			inner join Inventory.ConsignmentInventoryRemission re ON re.id = red.ConsignmentInventoryRemissionId 
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId
			WHERE re.Id = @Id and p.ProductSubGroupId is null

			-- si existen productos sin subgrupo asociados retornamos el mensaje de error
			if @countProductNotSubGroup > 0
			begin
				select @MessageError = stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un subgrupo asociado'
					FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
					inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.id = redbs.ConsignmentInventoryRemissionDetailId
					inner join Inventory.ConsignmentInventoryRemission re ON re.id = red.ConsignmentInventoryRemissionId 
					inner join Inventory.InventoryProduct p on p.Id = red.ProductId
					WHERE re.Id = @Id and p.ProductSubGroupId is null
					order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				select convert(bit, 0) as StatusResult, @MessageError as MessageResult
				return
			end

			-- consultamos los productos que manejen lote y el campo lote del item venga nulo
			select @countProductHandlesBatch = COUNT(*) 
			FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
			inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.id = redbs.ConsignmentInventoryRemissionDetailId
			inner join Inventory.ConsignmentInventoryRemission re ON re.id = red.ConsignmentInventoryRemissionId 
			inner join Inventory.InventoryProduct p on p.Id = red.ProductId 
			inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
			WHERE re.Id = @Id and psg.HandlesBatch = 1 and redbs.BatchSerialId is null

			-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
			if @countProductHandlesBatch  > 0
			begin
				select @MessageError = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
						FROM Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
						inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.id = redbs.ConsignmentInventoryRemissionDetailId
						inner join Inventory.ConsignmentInventoryRemission re ON re.id = red.ConsignmentInventoryRemissionId 
						inner join Inventory.InventoryProduct p on p.Id = red.ProductId 
						inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
						WHERE re.Id = @Id and psg.HandlesBatch = 1 and redbs.BatchSerialId is null
						order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				select convert(bit, 0) as StatusResult, @MessageError as MessageResult
				return
			end
		  	
			-- consultamos la unidad operativa relacionada a la remisión
			declare @OperatingUnitId int = (select OperatingUnitId from Inventory.ConsignmentInventoryRemission where Id = @Id)

			--- Valido que tenga parametros la unidad operativa
			if (select count(*) from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId) = 0 begin
				select convert(bit, 0) as StatusResult, 'No se encontro parametros de inventarios para la unidad operativa ' + (select UnitName from Common.OperatingUnit where Id = @OperatingUnitId) as MessageResult
			end

				IF EXISTS(	SELECT 1	
					FROM Inventory.ConsignmentInventoryRemissionDetail red WITH(nolock)
					JOIN Inventory.ConsignmentInventoryRemission re with(NOLOCK) on red.ConsignmentInventoryRemissionId = re.Id
					JOIN Inventory.PurchaseOrder po WITH(NOLOCK) on red.RemissionSource=2 and po.Code=red.SourceCode
					where re.Id=@Id AND ( re.CurrencyId <> po.CurrencyId)
					
					UNION ALL
					
					SELECT 1	
					FROM Inventory.ConsignmentInventoryRemissionDetail red WITH(nolock)
					JOIN Inventory.ConsignmentInventoryRemission re with(NOLOCK) on red.ConsignmentInventoryRemissionId = re.Id
					JOIN Inventory.InventoryContract ic WITH(NOLOCK) on red.RemissionSource=3 and ic.Code=red.SourceCode
					where re.Id=@Id AND (re.CurrencyId <> ic.CurrencyId)) BEGIN

				select convert(bit, 0) as StatusResult, 'La moneda de los documentos importados es diferente a la de la remisión' as MessageResult

			END

			--- Obtengo el parametro para saber si el IVA va al costo
			declare @IVACost TINYINT = (select top 1 iif(TaxRegistration=3,1,TaxRegistration) from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId)
			declare @OfficialCurrencyId int =(SELECT top 1 OfficialCurrencyId from GeneralLedger.CompanySettings)

			DECLARE 
				@warehouseId as int,
				@productId as int,
				@batchSerialId as int,
				@documentDate as datetime,
				@quantity as int,
				@unitValue as decimal(20,4),
				@value as decimal(20,4),
				@previousCost as decimal(20,4),
				@entityId as int,
				@entityCode as varchar(20),
				@entityName as varchar(250),
				@importedEntityId as int,
				@importedEntityCode as varchar(20),
				@importedEntityName as varchar(250),
				@affectInventory as bit,
				@ControlCostPercentage AS [numeric](5, 2),
				@CurrencyId  INT

			declare detail_cursor cursor for
				SELECT 
					re.WarehouseId, red.ProductId, redbs.BatchSerialId, [Common].[GETDATE](), 
					redbs.Quantity, 
					round(Common.CurrencyConverter(red.UnitValue,re.CurrencyId,@OfficialCurrencyId),2),
					case @IVACost 
					when 1 then ROUND( Common.CurrencyConverter(red.UnitValue + red.UnitValue * red.IvaPercentage / 100,re.CurrencyId,@OfficialCurrencyId ),2)
					else round(Common.CurrencyConverter(red.UnitValue,re.CurrencyId,@OfficialCurrencyId),2) end,
					ISNULL(ROUND(p.ProductCost,2),0),
					re.Id, re.Code, 'ConsignmentInventoryRemission', IIF(po.Id is null,IIF(ic.Id is null,null, ic.id),po.Id), red.SourceCode, IIF(po.Id is null, IIF(ic.Id is null, null, 'InventoryContract'),'PurchaseOrder'),
					1, p.ControlCostPercentage, re.CurrencyId
				from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.id = redbs.ConsignmentInventoryRemissionDetailId
				inner join Inventory.ConsignmentInventoryRemission re ON re.id = red.ConsignmentInventoryRemissionId 
				inner join Inventory.InventoryProduct p ON p.Id = red.ProductId
				left outer join Inventory.PurchaseOrderDetail pod ON pod.Id = red.PurchaseOrderDetailId
				left outer join Inventory.PurchaseOrder po ON po.Id = pod.PurchaseOrderId
 				left outer join Inventory.InventoryContractDetail icd ON icd.Id = red.ContractDetailId
				left outer join Inventory.InventoryContract ic ON ic.Id = icd.InventoryContractId
				where re.Id = @Id

			open detail_cursor
				FETCH NEXT FROM detail_cursor
					INTO @warehouseId, @productId, @batchSerialId, @documentDate, 
						@quantity, @unitValue, @value, @previousCost, 
						@entityId, @entityCode, @entityName, @importedEntityId, @importedEntityCode, @importedEntityName, 
						@affectInventory, @ControlCostPercentage,@CurrencyId

				WHILE @@FETCH_STATUS = 0
				BEGIN

					-- consultamos la cantidad total de items del producto que exiten en el inventario fisico
					declare @previousAmountProduct as int = ISNULL((select sum(phi.Quantity) from Inventory.PhysicalInventory phi where phi.ProductId = @productId),0)

					-- consultamos la cantidad total de items del producto que existen en el inventario fisico por el almacen
					declare @previousAmountWarehouse as int = ISNULL((select sum(phi.Quantity) from Inventory.PhysicalInventory phi where phi.ProductId = @productId and phi.WarehouseId = @warehouseId),0)

					-- consultamos la cantidad total de item del producto que existen en el inventario fisico por lote
					declare @previousAmountBatch as int = IIF(@batchSerialId is null, 
							@previousAmountWarehouse, 
							ISNULL((select sum(Quantity) from Inventory.PhysicalInventory phi where phi.ProductId = @productId and phi.WarehouseId = @warehouseId and phi.batchSerialId = @batchSerialId),0)
						)

					-- calculamos el costo promedio del prodcuto 
					declare @averageCost as decimal(20,4) = ROUND((((@previousCost * @previousAmountProduct) + (@quantity * @value))/(@previousAmountProduct + @quantity)),2)

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
					INSERT INTO [Inventory].[Kardex]
							([MovementType],[WarehouseId],[ProductId],[BatchSerialId],[DocumentDate],[Quantity],[Value],[PreviousCost],
							[AverageCost],[PreviousAverageCost],[PreviousAmountProduct],[PreviousAmountWarehouse],[PreviousAmountBatch],
							[EntityId],[EntityCode],[EntityName],[ImportedEntityId],[ImportedEntityCode],[ImportedEntityName],
							[AffectInventory],[CreationUser],[CreationDate])
						VALUES
							(1,@warehouseId, @productId,@batchSerialId,@documentDate,@quantity,@value,@previousCost,@averageCost,@previousCost,
							@previousAmountProduct,@previousAmountWarehouse,@previousAmountBatch,
							@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,
							@affectInventory,@User,[Common].[GETDATE]())

					-- actualizamos el costo y costo final del producto
					update inventory.InventoryProduct 
					set ProductCost = @averageCost,
						FinalProductCost = @value
					where Id = @productId

					-- afectamos el inventario fisico
					if(select Count(*) from Inventory.PhysicalInventory where WarehouseId = @warehouseId And ProductId = @productId And ISNULL(BatchSerialId,0) = ISNULL(@batchSerialId,0)) > 0
					BEGIN
						-- actualizamos la tabla de inventario fisico de los productos que ya esten registrado en el inventario fisico
						update inventory.PhysicalInventory 
						set Quantity += @quantity
						where ProductId = @productId and 
							WarehouseId = @warehouseId and 
							ISNULL(BatchSerialId, 0) = ISNULL(@batchSerialId,0)
					END
					else
					BEGIN
						-- insertamos en la tabla de inventario fisico los prdocutos que aun no esten registrados en el inventario fisico
						insert into Inventory.PhysicalInventory 
								([WarehouseId],[ProductId],[BatchSerialId],[Quantity]) 
							Values 
								(@warehouseId, @productId, @batchSerialId, @quantity)
					END

					FINDETAILCURSOR:
					FETCH NEXT FROM detail_cursor					
						INTO @warehouseId, @productId, @batchSerialId, @documentDate, 
							@quantity, @unitValue, @value, @previousCost, 
							@entityId, @entityCode, @entityName, @importedEntityId, @importedEntityCode, @importedEntityName, 
							@affectInventory, @ControlCostPercentage,@CurrencyId

				END

			close detail_cursor
			deallocate detail_cursor

			--Si hubo errores se devuelven los mensajes
			if (select COUNT(*) from @TableErrors) > 0
			begin				
				select @MessageError = stuff((select N'' + MessageError + CHAR(13) + CHAR(10) from @TableErrors for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				
				select convert(bit, 0) as StatusResult, @MessageError as MessageResult
				return
			end

			-- actualizamos los registros que se importaron desde orden de compra
			select @countPursharse = count(*) 
				from inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
				where red.ConsignmentInventoryRemissionId = @Id and red.RemissionSource = 2

			if @countPursharse > 0
			BEGIN
				declare @countValidatePursharse as int
				
				-- validamos que la cantidad a importar desde orden de compra no sea mayor a la cantidad disponible en la orden de compra al confirmar
				select @countValidatePursharse = COUNT(*) 
				FROM inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join Inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
				inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId
				where red.ConsignmentInventoryRemissionId = @Id AND pod.OutstandingQuantity < redbs.Quantity

				if @countValidatePursharse > 0
				begin
					select @MessageError = STUFF((select N'; La cantidad pendiente de la orden de compra es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + '. Cantidad Pendiente : ' + CAST( pod.OutstandingQuantity as varchar(30))
							FROM inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
							inner join Inventory.PurchaseOrderDetail pod on pod.Id = red.PurchaseOrderDetailId 
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							where red.ConsignmentInventoryRemissionId = @Id AND pod.OutstandingQuantity < redbs.Quantity
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					select convert(bit, 0) as StatusResult, @MessageError as MessageResult
					return
				end

				UPDATE pod
					SET pod.OutstandingQuantity -= cirdbs.Quantity,
						pod.CancelledQuantity += cirdbs.Quantity
				FROM Inventory.PurchaseOrderDetail pod
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON pod.Id = cird.PurchaseOrderDetailId
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
				WHERE cird.ConsignmentInventoryRemissionId = @Id AND cird.RemissionSource = 2

			END

			-- actualizamos los registros que importaron desde contrato
			select @countContract = count(*) 
				from inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
				where red.ConsignmentInventoryRemissionId = @Id and red.RemissionSource = 3

			if @countContract > 0
			BEGIN
				declare @countValidateContract as int

				-- validamos que la cantidad a importar desde contrato no sea mayor a la cantidad disponible en el contrato al confirmar
				select @countValidateContract = COUNT(*) 
				FROM inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join Inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
				inner join Inventory.InventoryContractDetail cod on cod.Id = red.PurchaseOrderDetailId
				where red.ConsignmentInventoryRemissionId = @Id AND cod.OutstandingQuantity < redbs.Quantity

				if @countValidateContract > 0
				begin
					select @MessageError = STUFF((select N'; La cantidad pendiente del contrato es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' +  CAST(cod.OutstandingQuantity as varchar(30))
							FROM inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join inventory.ConsignmentInventoryRemissionDetail red on red.Id = redbs.ConsignmentInventoryRemissionDetailId 
							inner join Inventory.InventoryContractDetail cod on cod.Id = red.PurchaseOrderDetailId
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							where red.ConsignmentInventoryRemissionId = @Id AND cod.OutstandingQuantity < red.Quantity
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					select convert(bit, 0) as StatusResult, @MessageError as MessageResult
					return
				end

				UPDATE icd
					SET icd.OutstandingQuantity -= cirdbs.Quantity,
						icd.CancelledQuantity += cirdbs.Quantity
				FROM Inventory.InventoryContractDetail icd
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON icd.Id = cird.ContractDetailId
				INNER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON cird.Id = cirdbs.ConsignmentInventoryRemissionDetailId
				WHERE cird.ConsignmentInventoryRemissionId = @Id AND cird.RemissionSource = 3
			END

			-- validacion Stock
			select @stockControl = StockControl 
			FROM Inventory.SettingInventory 
			where OperatingUnitId = @OperatingUnitId

			-- validamos stock por producto			
			if @stockControl = 1
			BEGIN
				SELECT @countstock = COUNT(*) 
				from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId
				where red.ConsignmentInventoryRemissionId = @Id AND (
						p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
						OR
						p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId))
					)

				if @countstock > 0
				BEGIN				
					select @MessageError=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
							from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId)) and red.ConsignmentInventoryRemissionId = @Id
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					select @MessageError+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
							from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							where p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId)) and red.ConsignmentInventoryRemissionId = @Id
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					--select convert(bit, 0) as StatusResult, @errorsStock as MessageResult
					--return
				END
			END

			-- validamos stock por almacen
			if @stockControl = 2
			BEGIN
				SELECT @countstock = COUNT(*) 
				from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
				inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
				inner join Inventory.InventoryProduct p on p.Id = red.ProductId 
				inner join Inventory.ConsignmentInventoryRemission re on re.Id = red.ConsignmentInventoryRemissionId
				where re.Id = @Id AND (
						p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
						OR
						p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId))
					)

				if @countstock > 0
				BEGIN
					select @MessageError=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
							from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							inner join Inventory.ConsignmentInventoryRemission re on re.Id = red.ConsignmentInventoryRemissionId
							inner join Inventory.Warehouse w on w.Id = re.WarehouseId
							where p.MinimumStock > (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId)) and re.Id = @Id
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					select @MessageError+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
							from Inventory.ConsignmentInventoryRemissionDetailBatchSerial redbs 
							inner join Inventory.ConsignmentInventoryRemissionDetail red ON red.Id = redbs.ConsignmentInventoryRemissionDetailId
							inner join Inventory.InventoryProduct p on p.Id = red.ProductId
							inner join Inventory.ConsignmentInventoryRemission re on re.Id = red.ConsignmentInventoryRemissionId
							inner join Inventory.Warehouse w on w.Id = re.WarehouseId
							where p.MaximumStock < (redbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = red.ProductId and phy.WarehouseId = re.WarehouseId)) and re.Id = @Id
							order by p.Code for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

					--select convert(bit, 0) as StatusResult, @errorsStockWarehouse as MessageResult
					--return
				END
			END

			select convert(bit, 1) as StatusResult, @MessageError as MessageResult
	END TRY
	BEGIN CATCH
		select convert(bit, 0) as StatusResult, 'Error ! '+ ERROR_MESSAGE() as MessageResult
	END CATCH
END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma una remisión de inventario en consignación, actualizando el stock físico en bodega y registrando los movimientos contables y de costos correspondientes. Antes de procesar, valida que cada producto tenga subgrupo asociado y que los que manejan lote tengan su lote informado; también verifica que la unidad operativa tenga parámetros de inventario configurados y que la moneda de los documentos de origen (órdenes de compra o contratos) coincida con la de la remisión. Compone el proceso integrando el encabezado y detalle de la remisión en consignación, el inventario físico por bodega/producto/lote, y la configuración del módulo de inventario para determinar si el IVA va al costo y qué tipo de comprobante contable aplicar. Se utiliza cuando el usuario confirma el ingreso formal de mercancía (medicamentos, dispositivos médicos, insumos) recibida de un proveedor en modalidad de consignación, dejando el inventario y la contabilidad actualizados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'La moneda de la remisión debe coincidir con la moneda del documento origen (orden de compra o contrato) para poder confirmar.; Todo producto remitido debe tener ProductSubGroupId asignado.; Si el subgrupo maneja lote (HandlesBatch=1) entonces BatchSerialId del detalle es obligatorio.; Los valores monetarios se almacenan en moneda oficial (OfficialCurrencyId de CompanySettings) tras convertir desde la moneda de la remisión.; El costo promedio se recalcula como ((PreviousCost*PreviousAmountProduct)+(Quantity*Value))/(PreviousAmountProduct+Quantity) y se persiste en InventoryProduct.ProductCost.; InventoryProduct.FinalProductCost se actualiza con el último @value (con o sin IVA según parámetro).; Si la variación porcentual del costo promedio supera ControlCostPercentage del producto y @ControlCost=1, no se afecta inventario para esa línea.; El movimiento generado en Kardex es de tipo 1 (entrada) con AffectInventory=1.; Las cantidades recibidas se descuentan de OutstandingQuantity y se suman a CancelledQuantity del documento origen (OC o contrato).; Las validaciones de stock mínimo/máximo solo construyen mensajes informativos; no impiden la confirmación (los return están comentados).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'remisión de inventario en consignación; lote/serial; subgrupo de producto; unidad operativa; moneda y conversión a moneda oficial; IVA al costo; kardex; inventario físico por bodega/producto/lote; costo promedio ponderado; control de variación de costo; orden de compra; contrato de inventario; cantidad pendiente y cancelada; control de stock mínimo/máximo', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Existen productos en la remisión cuyo InventoryProduct.ProductSubGroupId es NULL → Retorna StatusResult=0 con mensaje listando productos sin subgrupo y termina else Continúa validando lotes; si ProductSubGroup.HandlesBatch=1 y BatchSerialId IS NULL en algún detalle → Retorna StatusResult=0 indicando que batchSerialId no puede ser nulo y termina else Continúa con validaciones de moneda; si No existen registros en SettingInventory para la OperatingUnitId de la remisión → Emite resultado con StatusResult=0 informando ausencia de parámetros de inventario para la unidad operativa; si La moneda de la remisión (re.CurrencyId) difiere de la moneda de la PurchaseOrder (RemissionSource=2) o del InventoryContract (RemissionSource=3) origen → Devuelve resultado con mensaje ''La moneda de los documentos importados es diferente a la de la remisión''; si SettingInventory.TaxRegistration=3 (variable @IVACost=1) → El valor de costo unitario incluye el IVA: UnitValue + UnitValue*IvaPercentage/100 convertido a moneda oficial else El valor de costo unitario es solo UnitValue convertido a moneda oficial; si @affectInventory=1 AND @ControlCost=1 AND ControlCostPercentage>0 AND ABS(variación%) >= ControlCostPercentage → Inserta error de variación de costo promedio en @TableErrors y salta (GOTO) la inserción de Kardex/PhysicalInventory para esa línea else Procede a insertar Kardex y actualizar inventario; si Existe registro en PhysicalInventory para el mismo Warehouse+Product+BatchSerial → Suma la cantidad al registro existente (Quantity += @quantity) else Inserta nuevo registro en PhysicalInventory con la cantidad recibida; si RemissionSource=2 y existe al menos una línea con PurchaseOrderDetail.OutstandingQuantity < cantidad recibida → Retorna StatusResult=0 con detalle de productos cuya cantidad pendiente en OC es insuficiente else Descuenta OutstandingQuantity y aumenta CancelledQuantity en PurchaseOrderDetail; si RemissionSource=3 y existe al menos una línea con InventoryContractDetail.OutstandingQuantity < cantidad recibida → Retorna StatusResult=0 con detalle de productos cuya cantidad pendiente en contrato es insuficiente else Descuenta OutstandingQuantity y aumenta CancelledQuantity en InventoryContractDetail; si SettingInventory.StockControl=1 → Valida MinimumStock/MaximumStock contra suma de PhysicalInventory por producto (global) y compone mensajes de stock mínimo/máximo alcanzado; si SettingInventory.StockControl=2 → Valida MinimumStock/MaximumStock contra suma de PhysicalInventory por producto y bodega, generando mensajes que incluyen el almacén; si Hay errores acumulados en @TableErrors tras el cursor → Concatena los mensajes y retorna StatusResult=0 finalizando antes de actualizar OC/contrato else Continúa con actualizaciones de origen y validaciones de stock', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Common.GETDATE; Common.CurrencyConverter', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemission; Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.SettingInventory; Common.OperatingUnit; Inventory.PurchaseOrder; Inventory.PurchaseOrderDetail; Inventory.InventoryContract; Inventory.InventoryContractDetail; Inventory.PhysicalInventory; Inventory.Warehouse; GeneralLedger.CompanySettings', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmConsignmentInventoryRemission';
-- GO
