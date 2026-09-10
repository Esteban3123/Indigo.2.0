
CREATE PROCEDURE [Inventory].[SP_ConfirmEntranceVoucher]
@Id As int,
@User as varchar(20),
@ContainerNameCrystal as varchar(20),
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
	declare @countRemisionEntrance as int

	BEGIN TRY
			IF EXISTS
			(
				SELECT 1
				FROM Inventory.EntranceVoucherDetail evd
				LEFT JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs ON evd.Id = evdbs.EntranceVoucherDetailId
				WHERE evd.EntranceVoucherId = @Id AND evdbs.Id IS NULL
			)
			BEGIN
				declare @errorlotes varchar(MAX)
				select @errorlotes=stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un lote asociado'
				FROM Inventory.EntranceVoucherDetail evd				
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				LEFT JOIN Inventory.EntranceVoucherDetailBatchSerial evdbs ON evd.Id = evdbs.EntranceVoucherDetailId
				WHERE evd.EntranceVoucherId = @Id AND evdbs.Id IS NULL
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errorlotes as MessageResult
				return
			END

			-- consultamos los productos que no tengan un subgrupo asociado 
			select @countProductNotSubGroup = COUNT(*) FROM Inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd ON evd.id = evdbs.EntranceVoucherDetailId
			inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
			WHERE evd.EntranceVoucherId = @Id and p.ProductSubGroupId is null

			-- si existen productos sin subgrupo asociados retornamos el mensaje de error
			if @countProductNotSubGroup > 0
			begin
				declare @errors varchar(MAX)
				select @errors=stuff((select N'; El producto ' + p.Code	+ ' - ' + p.Name + ' no tiene un subgrupo asociado'
				FROM Inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd ON evd.id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				WHERE evd.EntranceVoucherId = @Id and p.ProductSubGroupId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errors as MessageResult
				return
			end

			-- consultamos los productos que manejen lote y el campo lote del item venga nulo
			select @countProductHandlesBatch = COUNT(*) FROM Inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd ON evd.id = evdbs.EntranceVoucherDetailId
			inner join Inventory.InventoryProduct p on p.Id = evd.ProductId inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
			WHERE evd.EntranceVoucherId = @Id and psg.HandlesBatch = 1 and evdbs.BatchSerialId is null

			-- si existen productos que manejen lote y tengan el campo de lote nulo retornamos el mensaje de error
			if @countProductHandlesBatch  > 0
			begin
				declare @errorsHandles varchar(MAX)
				select @errorsHandles = stuff((select N'; El parametro batchSerialId no puede ser nulo ya que el producto ' + p.Code	+ ' - ' + p.Name + ' maneja lote'
				FROM Inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd ON evd.id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId inner join Inventory.ProductSubGroup psg on psg.Id = p.ProductSubGroupId
				WHERE evd.EntranceVoucherId = @Id and psg.HandlesBatch = 1 and evdbs.BatchSerialId is null
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select convert(bit, 0) as StatusResult, @errorsHandles as MessageResult
				return
			end

			declare @OperatingUnitId int = (select OperatingUnitId from Inventory.EntranceVoucher where Id = @Id)
			--- Valido que en tenga parametros la unidad operativa
			if (select count(*) from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId) = 0 begin
				select convert(bit, 0) as StatusResult, 'No se encontro parametros de inventarios para la unidad operativa ' + (select UnitName from Common.OperatingUnit where Id = @OperatingUnitId) as MessageResult
			end
			--- Obtengo el parametro para saber si el IVA va al costo
			declare @IVACost TINYINT,
					@TRMValue numeric(20,5),
					@TRMValueReverse numeric(20,5),
					@CurrencyId INT,
					@OfficialCurrencyId INT

					--set @IVACost = (select TaxRegistration from Inventory.SettingInventory where OperatingUnitId = @OperatingUnitId)
					SELECT top 1 @OfficialCurrencyId = OfficialCurrencyId from GeneralLedger.CompanySettings WITH(NOLOCK)
					SELECT top 1 @CurrencyId = CurrencyId, @IVACost = TaxRegistration from Inventory.EntranceVoucher WITH(NOLOCK) WHERE Id=@Id

			/****** TASA DE CAMBIO ***************/
			IF @CurrencyId = @OfficialCurrencyId BEGIN
				SET @TRMValue = 1
				SET @TRMValueReverse =1
			END
			ELSE IF NOT EXISTS(SELECT 1 FROM Common.TRM WITH(NOLOCK) WHERE CurrencyId=@CurrencyId AND MeasurementDate = CAST( Common.GETDATE() as date) and OfficialCurrencyId= @OfficialCurrencyId) BEGIN
					SELECT convert(bit, 0) as StatusResult,
							CONCAT('No hay Tasa de cambio para el ','(',(SELECT top 1 Abbreviation from Common.Currency where Id=@CurrencyId) ,')') as MessageResult
			END

				SELECT top 1 @TRMValue =  trm.Value, @TRMValueReverse= trm.ValueOfficialToCurrency
				FROM Common.TRM trm WITH(NOLOCK) 
				where trm.CurrencyId =@CurrencyId and trm.OfficialCurrencyId=@OfficialCurrencyId and trm.MeasurementDate =CAST( Common.GETDATE() as date)

			/**************************************/

			declare @warehouseId as int
			declare @productId as int
			declare @batchSerialId as int
			declare @documentDate as datetime
			declare @invoiceDate as datetime
			declare @quantity as int
			declare @UnitValue as decimal(18,2)
			declare @value as decimal(20,4)
			declare @previousCost as decimal(20,4)
			declare @entityId as int
			declare @entityCode as varchar(20)
			declare @entityName as varchar(250)
			declare @importedEntityId as int
			declare @importedEntityCode as varchar(20)
			declare @importedEntityName as varchar(250)
			declare @affectInventory as bit
			declare @entranceSource as int
			DECLARE @ControlCostPercentage AS [numeric](5, 2)

			
			declare detail_cursor cursor for
			SELECT 
				ev.WarehouseId, --1
				evd.ProductId, --2
				evdbs.BatchSerialId, --3
				ev.DocumentDate, --4
				ev.InvoiceDate,--5
				evdbs.Quantity, --6
				[Portfolio].[fnConvertValueBasedOnExchangeRate](Round(evd.UnitValue,0),@TRMValue,@TRMValueReverse),--7
				CASE @IVACost 
					WHEN 1 THEN [Portfolio].[fnConvertValueBasedOnExchangeRate](ROUND(( ROUND(evd.UnitValue, 4) * ( 1 - evd.DiscountPercentage / 100 ) * ( 1 + evd.IvaPercentage / 100 ) ), 4),@TRMValue,@TRMValueReverse)
					ELSE [Portfolio].[fnConvertValueBasedOnExchangeRate]( ROUND(ROUND(evd.UnitValue, 4) * ( 1 - evd.DiscountPercentage / 100), 2),@TRMValue,@TRMValueReverse)
				END, --8
				Round(ISNULL(p.ProductCost,0),2),--9
				ev.Id, 
				ev.Code, 
				'EntranceVoucher', 
				IIF(re.Id is null, IIF(po.Id is null, IIF(ic.Id is null, IIF(cir.Id IS NULL, NULL, cir.Id), ic.id),po.Id), re.Id), 
				evd.SourceCode, 
				IIF(re.Id is null, IIF(po.Id is null, IIF(ic.Id is null, IIF(cir.Id IS NULL, NULL, 'ConsignmentInventoryRemission'), 'InventoryContract'),'PurchaseOrder'),'RemissionEntrance'),
				1,
				evd.EntranceSource, 
				p.ControlCostPercentage
			FROM Inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join Inventory.EntranceVoucherDetail evd ON evd.id = evdbs.EntranceVoucherDetailId
			inner join Inventory.EntranceVoucher ev ON ev.id = evd.EntranceVoucherId 
			inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
			left outer join Inventory.RemissionEntranceDetailBatchSerial redbs on redbs.Id = evd.RemissionEntranceDetailBatchSerialId
			left outer join Inventory.RemissionEntranceDetail red ON red.Id = redbs.RemissionEntranceDetailId
			left outer join Inventory.RemissionEntrance re ON re.Id = red.RemissionEntranceId
			left outer join Inventory.PurchaseOrderDetail pod ON pod.Id = evd.PurchaseOrderDetailId
			left outer join Inventory.PurchaseOrder po ON po.Id = pod.PurchaseOrderId
 			left outer join Inventory.InventoryContractDetail icd ON icd.Id = evd.ContractDetailId
			left outer join Inventory.InventoryContract ic ON ic.Id = icd.InventoryContractId
			LEFT OUTER JOIN Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdbs ON evd.ConsignmentInventoryRemissionDetailBatchSerialId = cirdbs.Id
			LEFT OUTER JOIN Inventory.ConsignmentInventoryRemissionDetail cird ON cirdbs.ConsignmentInventoryRemissionDetailId = cird.Id
			LEFT OUTER JOIN Inventory.ConsignmentInventoryRemission cir ON cird.ConsignmentInventoryRemissionId = cir.Id
			WHERE ev.Id = @Id

			open detail_cursor
					FETCH NEXT FROM detail_cursor
					INTO @warehouseId, @productId,@batchSerialId,@documentDate,@invoiceDate,@quantity, @UnitValue, @value,@previousCost,@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,@affectInventory,@entranceSource, @ControlCostPercentage

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

					if @entranceSource = 4 OR @entranceSource = 5
					BEGIN 
						set @affectInventory = 0
						set @averageCost = @previousCost
					END

					-- Control Costo Promedio					
					IF @affectInventory = 1 AND @ControlCost = 1 AND ISNULL(@ControlCostPercentage, 0) > 0
					BEGIN
						DECLARE @VariationCost AS [numeric](18, 2) = ROUND((IIF(@previousCost = 0, 1, (@averageCost - @previousCost) / (@previousCost)) * 100), 2) 

						IF ABS(@VariationCost) >= @ControlCostPercentage
						BEGIN							
							INSERT INTO @TableErrors
								SELECT 'El costo promedio del producto "' + p.Name + '" tendrá un porcentaje de variación (' + LTRIM(STR(@VariationCost,18)) + '%) mayor al establecido (' + LTRIM(STR(@ControlCostPercentage,10)) + '%)'
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
											 Values (1,@warehouseId, @productId,@batchSerialId,@documentDate,@quantity,@value,@previousCost,
														@averageCost,@previousCost,@previousAmountProduct,@previousAmountWarehouse,@previousAmountBatch,
														@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,
														@affectInventory,@User,[Common].[GETDATE]())
					
					-- si el item fue importado de un documento diferente a remision de entrada procedemos a actualizar el costo,costo final del producto y afectar el inventario fisico
					if @entranceSource <> 4 AND @entranceSource <> 5 BEGIN
						-- actualizamos el costo y costo final del producto
						update inventory.InventoryProduct 
							set ProductCost = @averageCost,
								FinalProductCost = @value, 
								LastPurchase = @invoiceDate
							where Id = @productId
					
						-- afectamos el inventario fisico
						if(select Count(*) from Inventory.PhysicalInventory where WarehouseId = @warehouseId And ProductId = @productId And ISNULL(BatchSerialId,0) = ISNULL(@batchSerialId,0)) > 0 BEGIN
							-- actualizamos la tabla de inventario fisico de los productos que ya esten registrado en el inventario fisico
							update inventory.PhysicalInventory set Quantity += @quantity
								where ProductId = @productId
									and WarehouseId = @warehouseId
									and ISNULL(BatchSerialId, 0) = ISNULL(@batchSerialId,0)
						END
						ELSE BEGIN
							-- insertamos en la tabla de inventario fisico los prdocutos que aun no esten registrados en el inventario fisico
							insert into Inventory.PhysicalInventory ([WarehouseId],[ProductId],[BatchSerialId],[Quantity]) Values (@warehouseId, @productId, @batchSerialId, @quantity)
						END									 
					END

					FINDETAILCURSOR:
					FETCH NEXT FROM detail_cursor
					INTO @warehouseId, @productId,@batchSerialId,@documentDate,@invoiceDate,@quantity,@UnitValue, @value,@previousCost,@entityId,@entityCode,@entityName,@importedEntityId,@importedEntityCode,@importedEntityName,@affectInventory,@entranceSource, @ControlCostPercentage
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
			select @countPursharse = count(*) from inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId where evd.EntranceVoucherId = @Id and evd.EntranceSource = 2

			if @countPursharse > 0
			BEGIN
				declare @countValidatePursharse as int
				declare @errorsValidateQuantityPod varchar(MAX) = ''

				--Se valida que las ordenes de compra importadas esten confirmadas por el nuevo requerimiento de desconfirmación de orden de compra
				if (select count(*) 
				from Inventory.EntranceVoucherDetail evd
				inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId
				inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId
				where evd.EntranceVoucherId = @Id and po.Status = 1) > 0
				begin
					select @errorsValidateQuantityPod = STUFF((select N'; La orden de compra ' + po.Code	+ ' no está confirmada'
					from Inventory.EntranceVoucherDetail evd
					inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId
					inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId
					where evd.EntranceVoucherId = @Id and po.Status = 1
					order by po.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select convert(bit, 0) as StatusResult, @errorsValidateQuantityPod as MessageResult
					return
				end

				-- validamos que la cantidad a importar desde orden de compra no sea mayor a la cantidad disponible en la orden de compra al confirmar
				select @countValidatePursharse = COUNT(*) FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd 
				on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId
				where evd.EntranceVoucherId = @Id AND pod.OutstandingQuantity < evdbs.Quantity

				if @countValidatePursharse > 0
				begin
					select @errorsValidateQuantityPod = STUFF((select N'; La cantidad pendiente de la orden de compra es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + cast(pod.OutstandingQuantity as varchar(20))
					FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join inventory.EntranceVoucherDetail evd
					on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId 
					inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
					where evd.EntranceVoucherId = @Id AND pod.OutstandingQuantity < evd.Quantity
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select convert(bit, 0) as StatusResult, @errorsValidateQuantityPod as MessageResult
					return
				end

				--update inventory.PurchaseOrderDetail set OutstandingQuantity -= (select evdbs.Quantity from inventory.EntranceVoucherDetailBatchSerial evdbs 
				--inner join inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId where evd.EntranceVoucherId = @Id and evd.EntranceSource = 2
				--and evd.PurchaseOrderDetailId = inventory.PurchaseOrderDetail.Id),
				--CancelledQuantity += (select evdbs.Quantity from inventory.EntranceVoucherDetailBatchSerial evdbs 
				--inner join inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId where evd.EntranceVoucherId = @Id and evd.EntranceSource = 2
				--and evd.PurchaseOrderDetailId = inventory.PurchaseOrderDetail.Id)
				--where Id in (select PurchaseOrderDetailId from inventory.EntranceVoucherDetail where EntranceVoucherId = @Id and EntranceSource = 2)

				update pod2 set pod2.OutstandingQuantity -= temp.TempQuantity, pod2.CancelledQuantity += temp.TempQuantity
				from Inventory.PurchaseOrderDetail pod2
				inner join (
					select evd.PurchaseOrderDetailId, SUM(evdbs.Quantity) TempQuantity
					from Inventory.EntranceVoucherDetailBatchSerial evdbs
					inner join Inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId
					where evd.EntranceVoucherId = @Id and evd.EntranceSource = 2
					group by evd.PurchaseOrderDetailId
				) temp on temp.PurchaseOrderDetailId = pod2.Id

				declare @countHCORDPRODQ as int
				-- consultamos si los datos importados desde orden de compra se generaron desde cristal
				select @countHCORDPRODQ = count(*) From inventory.EntranceVoucherDetail evd inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId
				inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId where evd.EntranceVoucherId = @Id AND po.OsteosynthesisEquipment = 1 
				-- si los datos importados desde orden de compra se generaron desde crystal procedemos a actualizar las tablas de crystal
				if @countHCORDPRODQ > 0
				BEGIN

					declare @idHCORDPRODQ int
					declare @codeProduct as varchar(20)
					declare @countDetailCreate int
					declare @countDetailcommitted int
				
					DECLARE cursorHCORDPRODQ CURSOR FOR 
					SELECT po.OsteosynthesisEquipmentId, p.Code
					FROM Inventory.EntranceVoucherDetail evd inner join Inventory.PurchaseOrderDetail pod on pod.Id = evd.PurchaseOrderDetailId
					inner join Inventory.PurchaseOrder po on po.Id = pod.PurchaseOrderId inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
					where evd.EntranceVoucherId = @Id AND po.OsteosynthesisEquipment = 1 
					--Group by po.OsteosynthesisEquipmentId, p.code

					OPEN cursorHCORDPRODQ

					FETCH NEXT FROM cursorHCORDPRODQ INTO @idHCORDPRODQ, @codeProduct

					WHILE @@FETCH_STATUS = 0
					BEGIN
						declare @sqlCrystal as nvarchar(500) = 'update ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQD] set MATESTADO = ''2'' where AUTOPROCED = '''+ cast(@idHCORDPRODQ as varchar(20)) + ''' AND CODPRODUC = ''' + @codeProduct +'''';
						exec sp_executesql @sqlCrystal

						set @sqlCrystal = 'Select @countDetailCreate = Count(*) From ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQD] where AUTOPROCED = ' + cast(@idHCORDPRODQ as varchar(20));
						exec sp_executesql @sqlCrystal, N'@countDetailCreate int output', @countDetailCreate output

						set @sqlCrystal = 'Select @countDetailcommitted = Count(*) From ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQD] where MATESTADO = 2 AND AUTOPROCED = ' + cast(@idHCORDPRODQ as varchar(20));
						exec sp_executesql @sqlCrystal, N'@countDetailcommitted int output', @countDetailcommitted output

						if @countDetailCreate = @countDetailcommitted
						BEGIN
						set @sqlCrystal  = 'update ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQ] set SOLICITAMATOST = ''2'' where AUTO = '+ cast(@idHCORDPRODQ as varchar(20));
						
						exec sp_executesql @sqlCrystal, N'@countDetailcommitted int output, @countDetailCreate int Output', @countDetailcommitted output ,  @countDetailCreate output
						END
						--set @sqlCrystal  = 'update ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQ] set SOLICITAMATOST = Case when 
						--(Select @countDetailCreate = Count(*) From ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQD] where AUTOPROCED = ''' + cast(@idHCORDPRODQ as varchar(20)) + ''')
						--= (Select @countDetailcommitted = Count(*) From ['+@ContainerNameCrystal+'].[dbo].[HCORDPROQD] where MATESTADO = ''2'' AND AUTOPROCED = ''' + cast(@idHCORDPRODQ as varchar(20)) + ''')
						--then 2 else 1 END where AUTO = '+ cast(@idHCORDPRODQ as varchar(20));
						--exec sp_executesql @sqlCrystal, N'@countDetailcommitted int output, @countDetailCreate int Output', @countDetailcommitted output ,  @countDetailCreate output
						--END
						FETCH NEXT FROM cursorHCORDPRODQ INTO @idHCORDPRODQ, @codeProduct
					END 
					CLOSE cursorHCORDPRODQ;
					DEALLOCATE cursorHCORDPRODQ;
				END
			END

			-- actualizamos los registros que importaron desde contraro
			select @countContract = count(*) from inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId where evd.EntranceVoucherId = @Id and evd.EntranceSource = 3

			if @countContract > 0
			BEGIN
				declare @countValidateContract as int
				-- validamos que la cantidad a importar desde contrato no sea mayor a la cantidad disponible en el contrato al confirmar
				select @countValidateContract = COUNT(*) FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd 
				on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.InventoryContractDetail cod on cod.Id = evd.ContractDetailId
				where evd.EntranceVoucherId = @Id AND cod.OutstandingQuantity < evdbs.Quantity

				if @countValidateContract > 0
				begin
					declare @errorsValidateQuantityCo varchar(MAX)
					select @errorsValidateQuantityCo = STUFF((select N'; La cantidad pendiente del contrato es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + cast(cod.OutstandingQuantity as varchar(10))
					FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join inventory.EntranceVoucherDetail evd
					on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.InventoryContractDetail cod on cod.Id = evd.ContractDetailId 
					inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
					where evd.EntranceVoucherId = @Id AND cod.OutstandingQuantity < evd.Quantity
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select convert(bit, 0) as StatusResult, @errorsValidateQuantityCo as MessageResult
					return
				end

				UPDATE icd 
					SET icd.OutstandingQuantity = icd.OutstandingQuantity - evdbs.Quantity,
						icd.CancelledQuantity = icd.CancelledQuantity + evdbs.Quantity
				FROM Inventory.InventoryContractDetail icd
				JOIN Inventory.EntranceVoucherDetail evd ON icd.Id = evd.ContractDetailId
				JOIN
				(
					SELECT evdbs.EntranceVoucherDetailId, SUM(evdbs.Quantity) Quantity
					FROM Inventory.EntranceVoucherDetailBatchSerial evdbs
					GROUP BY evdbs.EntranceVoucherDetailId
				) evdbs ON evd.Id = evdbs.EntranceVoucherDetailId
				WHERE evd.EntranceVoucherId = @Id AND evd.EntranceSource = 3
			END

			-- actualizamos los registro que importaron desde remision de entrada
			select @countRemisionEntrance = count(*) from inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join inventory.EntranceVoucherDetail evd on evd.Id = evdbs.EntranceVoucherDetailId where evd.EntranceVoucherId = @Id and evd.EntranceSource = 4

			if @countRemisionEntrance > 0
			BEGIN
				declare @countValidateRemission as int
				-- validamos que la cantidad a importar desde remision de entrada no sea mayor a la cantidad disponible en la remision de entrada al confirmar
				select @countValidateRemission = COUNT(*) FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join Inventory.EntranceVoucherDetail evd 
				on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.RemissionEntranceDetailBatchSerial redbs on redbs.Id = evd.RemissionEntranceDetailBatchSerialId
				where evd.EntranceVoucherId = @Id AND redbs.OutstandingQuantity < evdbs.Quantity
				if @countValidateRemission > 0
				begin
					declare @errorsValidateQuantityRed varchar(MAX)
					select @errorsValidateQuantityRed = STUFF((select N'; La cantidad pendiente de la remision de entrada es menor que la cantidad a descontar del producto ' + p.Code	+ ' - ' + p.Name + ' Cantidad Pendiente : ' + cast(redbs.OutstandingQuantity as varchar(20))
					FROM inventory.EntranceVoucherDetailBatchSerial evdbs inner join inventory.EntranceVoucherDetail evd
					on evd.Id = evdbs.EntranceVoucherDetailId inner join Inventory.RemissionEntranceDetailBatchSerial redbs on redbs.Id = evd.RemissionEntranceDetailBatchSerialId 
					inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
					where evd.EntranceVoucherId = @Id AND redbs.OutstandingQuantity < evd.Quantity
					order by p.Code
					for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
					select convert(bit, 0) as StatusResult, @errorsValidateQuantityRed as MessageResult
					return
				end
				
				update redbs set redbs.OutstandingQuantity -= evdbs.Quantity
				from Inventory.RemissionEntranceDetailBatchSerial redbs
				inner join Inventory.EntranceVoucherDetail evd on evd.RemissionEntranceDetailBatchSerialId = redbs.Id
				inner join Inventory.EntranceVoucherDetailBatchSerial evdbs on evdbs.EntranceVoucherDetailId = evd.Id
				where evd.EntranceVoucherId = @Id and evd.EntranceSource = 4

			END

			-- validacion Stock
			select @stockControl = StockControl FROM Inventory.SettingInventory 
			where OperatingUnitId = (select OperatingUnitId from Inventory.EntranceVoucher where Id = @Id)

			-- validamos stock por producto
			declare @errorsStock varchar(MAX)
			if @stockControl = 1
			BEGIN
			SELECT @countstock = COUNT(*) from Inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
			inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
			where evd.EntranceVoucherId = @Id and evd.EntranceSource <> 4 AND evd.EntranceSource <> 5 AND
				(
					p.MinimumStock > (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId))
					or 
					p.MaximumStock < (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId))
				)

				if @countstock > 0
				BEGIN
				
				select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock mínimo'
				from Inventory.EntranceVoucherDetailBatchSerial evdbs 
				inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				where p.MinimumStock > (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId))
				and evd.EntranceVoucherId = @Id and evd.EntranceSource <> 4 and evd.EntranceSource <> 5
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' alcanzo su stock máximo'
				from Inventory.EntranceVoucherDetailBatchSerial evdbs 
				inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				where p.MaximumStock < (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId))
				and evd.EntranceVoucherId = @Id and evd.EntranceSource <> 4 and evd.EntranceSource <> 5
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END
			END
			-- validamos stock por almacen
			if @stockControl = 2
			BEGIN
			SELECT @countstock = COUNT(*) from Inventory.EntranceVoucherDetailBatchSerial evdbs 
			inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
			inner join Inventory.InventoryProduct p on p.Id = evd.ProductId inner join Inventory.EntranceVoucher ev on ev.Id = evd.EntranceVoucherId
			where ev.Id = @Id and evd.EntranceSource <> 4 and evd.EntranceSource <> 5 and
				(
					p.MinimumStock > (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId and phy.WarehouseId = ev.WarehouseId))
					or 
					p.MaximumStock < (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId and phy.WarehouseId = ev.WarehouseId))
				)

				if @countstock > 0
				BEGIN
				select @errorsStock=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock mínimo'
				from Inventory.EntranceVoucherDetailBatchSerial evdbs 
				inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				inner join Inventory.EntranceVoucher ev on ev.Id = evd.EntranceVoucherId
				inner join Inventory.Warehouse w on w.Id = ev.WarehouseId
				where p.MinimumStock > (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId and phy.WarehouseId = ev.WarehouseId))
				and ev.Id = @Id and evd.EntranceSource <> 4 and evd.EntranceSource <> 5
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				select @errorsStock+=stuff((select N'; El Producto ' + p.Code	+ ' - ' + p.Name + ' en el almacen ' + w.Code + ' - ' + w.Name + ' alcanzo su stock máximo'
				from Inventory.EntranceVoucherDetailBatchSerial evdbs 
				inner join Inventory.EntranceVoucherDetail evd ON evd.Id = evdbs.EntranceVoucherDetailId
				inner join Inventory.InventoryProduct p on p.Id = evd.ProductId
				inner join Inventory.EntranceVoucher ev on ev.Id = evd.EntranceVoucherId
				inner join Inventory.Warehouse w on w.Id = ev.WarehouseId
				where p.MaximumStock < (evdbs.Quantity + (select sum(Quantity) from Inventory.PhysicalInventory phy where phy.ProductId = evd.ProductId and phy.WarehouseId = ev.WarehouseId))
				and ev.Id = @Id and evd.EntranceSource <> 4 and evd.EntranceSource <> 5
				order by p.Code
				for xml path(N''), type).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')
				END
			END

			select convert(bit, 1) as StatusResult, @errorsStock as MessageResult
		END TRY
	BEGIN CATCH
		select convert(bit, 0) as StatusResult, 'Linea: ' + cast(ERROR_LINE() as varchar(20)) + ' Error ! '+ ERROR_MESSAGE() as MessageResult
	END CATCH

	END
GO
-- EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Confirma y registra definitivamente un comprobante de entrada de mercancía al inventario (vale de entrada). Valida que todos los productos del comprobante tengan lotes asociados, subgrupos configurados y, cuando aplica, números de lote o serial correctamente asignados; también verifica la tasa de cambio vigente y los parámetros de inventario de la unidad operativa. Una vez superadas las validaciones, actualiza el stock de los productos en bodega aplicando los valores, impuestos (IVA al costo si corresponde) y conversión de moneda, tocando las entidades de comprobante de entrada, detalle de comprobante, lotes/seriales y el catálogo maestro de productos. Existe para garantizar la integridad del ingreso de mercancía antes de afectar el saldo de inventario y generar los movimientos contables y de stock correspondientes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmEntranceVoucher';
-- GO
-- EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'PROCEDURE', @level1name = N'SP_ConfirmEntranceVoucher';
-- GO
-- GO
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Confirma un comprobante de entrada de inventario validando lotes, subgrupos, stock, TRM, cantidades pendientes de origen y registra los movimientos en Kardex actualizando inventario físico, costos y documentos de origen (OC, contrato, remisión, consignación).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo detalle del comprobante (EntranceVoucherDetail) debe tener al menos un registro asociado en EntranceVoucherDetailBatchSerial; Todos los productos del comprobante deben tener ProductSubGroupId asignado; Si el subgrupo del producto tiene HandlesBatch=1, el detalle de lote/serial debe tener BatchSerialId no nulo; Debe existir configuración en Inventory.SettingInventory para la OperatingUnit del comprobante; Si la moneda del comprobante difiere de la oficial (CompanySettings.OfficialCurrencyId), debe existir TRM para esa moneda en la fecha actual; Si EntranceSource=2 (orden de compra), las órdenes de compra asociadas no pueden estar en Status=1 (no confirmadas) y pod.OutstandingQuantity debe ser >= cantidad a ingresar; Si EntranceSource=3 (contrato), icd.OutstandingQuantity debe ser >= cantidad a ingresar; Si EntranceSource=4 (remisión de entrada), redbs.OutstandingQuantity debe ser >= cantidad a ingresar', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Comprobante de entrada; Lote/Serial; Subgrupo de producto; Kardex; Inventario físico; Costo promedio; Tasa de cambio (TRM); IVA al costo; Orden de compra; Contrato de inventario; Remisión de entrada; Inventario en consignación; Stock mínimo y máximo; Equipo de osteosíntesis (Crystal); Unidad operativa', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[INSERT] Inventory.Kardex: Por cada línea de detalle/lote del comprobante se inserta un movimiento de Kardex con MovementType=1, cantidades previas (producto, almacén, lote), costo promedio recalculado y datos de la entidad de origen (RemissionEntrance, PurchaseOrder, InventoryContract o ConsignmentInventoryRemission).; [UPDATE] Inventory.PhysicalInventory: Cuando EntranceSource <> 4 y <> 5 y ya existe registro para (WarehouseId, ProductId, BatchSerialId), incrementa Quantity en la cantidad ingresada.; [INSERT] Inventory.PhysicalInventory: Cuando EntranceSource <> 4 y <> 5 y no existe registro previo para (WarehouseId, ProductId, BatchSerialId), inserta una nueva fila con la cantidad ingresada.; [UPDATE] Inventory.InventoryProduct: Cuando EntranceSource <> 4 y <> 5, actualiza ProductCost con el costo promedio recalculado, FinalProductCost con el valor unitario convertido a TRM y LastPurchase con la fecha de factura del comprobante.; [UPDATE] Inventory.PurchaseOrderDetail: Cuando hay líneas con EntranceSource=2, resta la cantidad ingresada de OutstandingQuantity y la suma a CancelledQuantity en el detalle de la orden de compra correspondiente.; [UPDATE] Inventory.InventoryContractDetail: Cuando hay líneas con EntranceSource=3, resta la cantidad ingresada de OutstandingQuantity y la suma a CancelledQuantity en el detalle del contrato.; [UPDATE] Inventory.RemissionEntranceDetailBatchSerial: Cuando hay líneas con EntranceSource=4, resta la cantidad ingresada de OutstandingQuantity en la línea de lote/serial de la remisión de entrada.; [UPDATE] HCORDPROQD (BD Crystal externa): Si la orden de compra tiene OsteosynthesisEquipment=1, se ejecuta dinámicamente UPDATE poniendo MATESTADO=''2'' en HCORDPROQD para cada (AUTOPROCED, CODPRODUC) ingresado.; [UPDATE] HCORDPROQ (BD Crystal externa): Si todos los detalles del equipo de osteosíntesis quedaron en MATESTADO=2, actualiza SOLICITAMATOST=''2'' en HCORDPROQ para el AUTO correspondiente.; [RETURN_RESULT] ResultSet: Devuelve (StatusResult bit, MessageResult varchar) con StatusResult=0 y mensaje de error cuando alguna validación falla; al finalizar exitosamente retorna StatusResult=1 con los avisos de stock mínimo/máximo en MessageResult.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Calls', @value=N'Portfolio.fnConvertValueBasedOnExchangeRate; Common.GETDATE; sp_executesql', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.EntranceVoucherDetail; Inventory.EntranceVoucherDetailBatchSerial; Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.EntranceVoucher; Inventory.SettingInventory; Common.OperatingUnit; GeneralLedger.CompanySettings; Common.TRM; Common.Currency; Inventory.RemissionEntranceDetailBatchSerial; Inventory.RemissionEntranceDetail; Inventory.RemissionEntrance; Inventory.PurchaseOrderDetail; Inventory.PurchaseOrder; Inventory.InventoryContractDetail; Inventory.InventoryContract; Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemission; Inventory.PhysicalInventory; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'PROCEDURE', @level1name=N'SP_ConfirmEntranceVoucher';
-- GO
