-- ================================================
/******* EJEMPLO DE LA ESTRUCTURA QUE DEBEN ENVIAR ***********/
--<KardexCustody>
--  <ThirdPartyId>654</ThirdPartyId>
--  <ProductId>94009</ProductId>
--  <MovementType>1</MovementType>
--  <WarehouseId>4</WarehouseId>
--  <Quantity>10</Quantity>
--  <Value>2500</Value>
--  <AffectAverageCost>1</AffectAverageCost>
--</KardexCustody>
--<KardexCustody>
--  <ThirdPartyId>798</ThirdPartyId>
--  <ProductId>389407</ProductId>
--  <MovementType>1</MovementType>
--  <WarehouseId>4</WarehouseId>
--  <BatchSerialId>2</BatchSerialId>
--  <Quantity>10</Quantity>
--  <Value>2500</Value>
--  <AffectAverageCost>1</AffectAverageCost>
--</KardexCustody>
-- =============================================
-- Author:		Hector Rodriguez
-- Create date: 29/03/2019
-- Description:	Store para afectar el inventario fisico y registrar en el KardexCustody, es decir que este store lo deben consumir todos los procesos que hagan movimientos de productos de custodia
-- =============================================
CREATE PROCEDURE [Inventory].[SP_SavePhysicalInventoryCustodyKardexCustody]
	@KardexCustody xml,
	@AdmissionNumber varchar(10),
	@EntityId int,
	@EntityCode varchar(20),
	@EntityName as varchar(100),
	@User varchar(20),
	@ControlCost as bit = 1
AS
BEGIN
	SET NOCOUNT ON;
	
	declare @TableKardexCustody table(Id int identity(1,1) primary key,ThirdPartyId int, ProductId int, MovementType tinyint, WarehouseId int, BatchSerialId int null, Quantity int, Value numeric(20,4), PreviousCost numeric(20,4), AverageCost numeric(20,4), PreviousAverageCost numeric(20,4), PreviousAmountProduct int, PreviousAmountWarehouse int, PreviousAmountBatch int, AffectAverageCost bit)

    begin try
		insert into @TableKardexCustody
		select 
		t.x.value('ThirdPartyId[1]','int'),
		t.x.value('ProductId[1]','int'),
		t.x.value('MovementType[1]','tinyint'),
		t.x.value('WarehouseId[1]','int'),
		t.x.value('BatchSerialId[1]','int'),
		t.x.value('Quantity[1]','int'),
		t.x.value('Value[1]','numeric(20,4)'),
		0,0,0,0,0,0,
		t.x.value('AffectAverageCost[1]','bit')
		from @KardexCustody.nodes('/Kardex') t(x)

		declare @Cero Tinyint = 0,
			@Uno Tinyint = 1,
			@Dos Tinyint = 2

/******************** VALIDACIONES *************************/
			
		if (select count(*) from @KardexCustody.nodes('/Kardex') t(x)) = 0 begin
			select '999' as CodeMessage, 'Error al actualizar el KardexCustody' as Message, cast(3 as tinyint) as [Status]
			return
		end
		
		if Exists (select 1 from @TableKardexCustody k 
			inner join Inventory.InventoryProduct p with(nolock) on k.ProductId = p.Id 
			where p.ProductSubGroupId is null) begin
			declare @StringProductSubgroup varchar(max) = (select p.Code + ', ' 
				from @TableKardexCustody k 
				inner join Inventory.InventoryProduct p with(nolock) on k.ProductId = p.Id 
				where p.ProductSubGroupId is null for XML PATH(''))
			select '999' as CodeMessage, 'No se logro afectar el Inventario debido a que los siguientes Productos no poseen un un SubGrupo: ' + @StringProductSubgroup as Message, cast(3 as tinyint) as [Status]
			return
		end

		if Exists (select 1 from @TableKardexCustody k 
			inner join Inventory.Warehouse w with(nolock) on k.WarehouseId = w.Id 
			where w.VirtualStore = @Uno) begin
			declare @StringVirtualStore varchar(max) = (select w.Code + ' - ' + w.Name + ', ' 
				from @TableKardexCustody k 
				inner join Inventory.Warehouse w with(nolock) on k.WarehouseId = w.Id 
				where w.VirtualStore = @Uno for XML PATH(''))
			select '999' as CodeMessage, 'No se logro afectar el Inventario debido a que los siguientes almacenes son virtuales: ' + @StringVirtualStore as Message, cast(3 as tinyint) as [Status]
			return
		end

		if Exists (select 1 from @TableKardexCustody k 
			inner join Inventory.Warehouse w with(nolock) on k.WarehouseId = w.Id 
			where w.CustodyStore = @Cero) begin
			declare @StringCustodyStore varchar(max) = (select w.Code + ' - ' + w.Name + ', ' 
				from @TableKardexCustody k 
				inner join Inventory.Warehouse w with(nolock) on k.WarehouseId = w.Id 
				where w.CustodyStore = @Cero for XML PATH(''))
			select '999' as CodeMessage, 'No se logro afectar el Inventario debido a que los siguientes almacenes no son de custodia: ' + @StringCustodyStore as Message, cast(3 as tinyint) as [Status]
			return
		end

/******************** VALIDACIONES POR TIPO *************************/		
		DECLARE @errors VARCHAR(MAX)
		-- Reversión de Factura de Compra
		IF (@EntityName = 'DocumentInvoiceProductSales' AND ISNULL((SELECT COUNT(*) FROM @TableKardexCustody k WHERE k.MovementType = @Uno), 0) = ISNULL((SELECT COUNT(*) FROM @TableKardexCustody k), 0)) BEGIN
			IF (ISNULL((SELECT TOP 1 COUNT(*)
						FROM @TableKardexCustody t
						LEFT JOIN Inventory.KardexCustody k with(nolock) ON 
							k.WarehouseId = t.WarehouseId AND
							k.ProductId = t.ProductId AND
							ISNULL(k.BatchSerialId, 0) = ISNULL(t.BatchSerialId, 0) AND
							k.EntityId = @EntityId AND
							k.EntityCode = @EntityCode AND
							k.EntityName = @EntityName
						GROUP BY t.ProductId
						HAVING SUM(ISNULL(t.Quantity, 0)) <> SUM(ISNULL(k.Quantity, 0) * IIF(ISNULL(k.MovementType, 1) = 1, -1, 1))), 0) > 0) BEGIN				
				SELECT @errors = STUFF((
					SELECT N'; La cantidad del producto ' + ip.Code + ' - ' + ip.Name + ' es diferente de la facturada '
					FROM @TableKardexCustody t
					JOIN Inventory.InventoryProduct ip with(nolock) ON ip.Id = t.ProductId
					LEFT JOIN Inventory.KardexCustody k with(nolock) ON 
						k.WarehouseId = t.WarehouseId AND
						k.ProductId = t.ProductId AND
						ISNULL(k.BatchSerialId, 0) = ISNULL(t.BatchSerialId, 0) AND
						k.EntityId = @EntityId AND
						k.EntityCode = @EntityCode AND
						k.EntityName = @EntityName
					GROUP BY t.ProductId, ip.Code, ip.Name
					HAVING SUM(ISNULL(t.Quantity, 0)) <> SUM(ISNULL(k.Quantity, 0) * IIF(ISNULL(k.MovementType, 1) = 1, -1, 1))
				FOR XML PATH(N''), TYPE).value(N'.[1]', N'nvarchar(max)'), 1, 2, N'')

				SELECT '999' as CodeMessage, @errors as Message, cast(3 as tinyint) as [Status]
				RETURN
			END
		END

/******************** FIN VALIDACIONES *************************/

		update k 
			set PreviousCost = round(p.FinalProductCost,2), 
				PreviousAverageCost = round(p.ProductCost,2), 
				AverageCost = round(p.ProductCost,2), 
				PreviousAmountProduct = isnull((select sum(Quantity) from Inventory.PhysicalInventoryCustody with(nolock) where AdmissionNumber = @AdmissionNumber and ProductId = k.ProductId),0), 
				PreviousAmountWarehouse = isnull((select sum(Quantity) from Inventory.PhysicalInventoryCustody with(nolock) where AdmissionNumber = @AdmissionNumber and ProductId = k.ProductId and WarehouseId = k.WarehouseId),0), 
				PreviousAmountBatch = isnull((select sum(Quantity) from Inventory.PhysicalInventoryCustody with(nolock) where AdmissionNumber = @AdmissionNumber and ProductId = k.ProductId and WarehouseId = k.WarehouseId and BatchSerialId = k.BatchSerialId),0)
		from @TableKardexCustody k inner join Inventory.InventoryProduct p with(nolock) on k.ProductId = p.Id


/******************** inserto en el inventario fisico los productos que nunca han tenido movimiento *************************/
		INSERT INTO Inventory.PhysicalInventoryCustody(AdmissionNumber, WarehouseId,ProductId,BatchSerialId,Quantity)
			select @AdmissionNumber, k.WarehouseId, k.ProductId, k.BatchSerialId, 0 
			from @TableKardexCustody k 
			left join Inventory.PhysicalInventoryCustody ph with(nolock) on ph.AdmissionNumber = @AdmissionNumber and ph.ProductId = k.ProductId and ph.WarehouseId = k.WarehouseId and ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0) 
			where ph.Id is null
		
/******************** Si hay Movimientos de entrada *************************/
		if Exists (select 1 from @TableKardexCustody where MovementType = @Uno) begin		
			--- Aumento la cantidad en el inventario fisico
			update Inventory.PhysicalInventoryCustody set Quantity += k.Quantity
			from (
				select ProductId, WarehouseId, BatchSerialId, MovementType, sum(Quantity) as Quantity 
				from @TableKardexCustody group by ProductId, WarehouseId, BatchSerialId, MovementType
			) k 
			inner join Inventory.PhysicalInventoryCustody ph with(nolock) on ph.AdmissionNumber = @AdmissionNumber and ph.ProductId = k.ProductId and ph.WarehouseId = k.WarehouseId and ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0) 
			where k.MovementType = @Uno
		end

		
/******************** Si hay Movimientos de salida *************************/
		if Exists (select 1 from @TableKardexCustody where MovementType = @Dos) begin			
			--- Valido que haya cantidades en el I. Fisico
			declare @Count as integer = 0
			set @Count = (SELECT count(*) FROM (select k.ProductId from @TableKardexCustody k inner join Inventory.PhysicalInventoryCustody ph on ph.AdmissionNumber = @AdmissionNumber and ph.ProductId = k.ProductId and ph.WarehouseId = k.WarehouseId and ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0) where k.MovementType = 2 group by ph.AdmissionNumber, k.ProductId, k.WarehouseId, k.BatchSerialId, ph.Quantity having ph.Quantity < SUM(k.Quantity)) AS TMP)
			if @Count > 0 begin
				declare @StringProductPhysical varchar(max) = (select distinct p.Code + ' - ' + p.Name + ', ' 
					from @TableKardexCustody k 
					inner join Inventory.InventoryProduct p with(nolock) on p.Id = k.ProductId 
					inner join Inventory.PhysicalInventoryCustody ph with(nolock) on 
						ph.AdmissionNumber = @AdmissionNumber and ph.ProductId = k.ProductId 
						And ph.WarehouseId = k.WarehouseId and ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0) 
					where k.MovementType = @Dos 
					group by ph.AdmissionNumber, k.ProductId, k.WarehouseId, k.BatchSerialId, p.Code, p.Name, ph.Quantity Having ph.Quantity < SUM(k.Quantity) for XML PATH(''))
				select '999' as CodeMessage, 'Los siguientes productos no tienen cantidades suficientes para realizar el movimiento de salida: ' + @StringProductPhysical as Message, cast(3 as tinyint) as [Status]
				return
			end
			
			update Inventory.PhysicalInventoryCustody set Quantity -= k.Quantity
			from (
				select ProductId, WarehouseId,BatchSerialId,MovementType,sum(Quantity) as Quantity  
				from @TableKardexCustody 
				group by ProductId, WarehouseId,BatchSerialId,MovementType
			) k 
			inner join Inventory.PhysicalInventoryCustody ph with(nolock) on ph.AdmissionNumber = @AdmissionNumber and ph.ProductId = k.ProductId and ph.WarehouseId = k.WarehouseId and ISNULL(ph.BatchSerialId,0) = ISNULL(k.BatchSerialId,0) 
			where k.MovementType = @Dos			
		
		end

		--- Recorro los Items que esten repetidos para descontarles los valores de sus colegas repetidos
		declare @PRC_MovementType tinyint
		declare @PRC_Id int ,@PRC_WarehouseId int, @PRC_ProductId int, @PRC_BatchSerialId int, @PRC_Quantity int
		declare @Last_MovementType tinyint
		declare @Last_WarehouseId int = 0, @Last_ProductId int = 0, @Last_BatchSerialId int = 0, @Sum_Quantity int = 0, @IndexCursor int = 0


		Declare @Rows Int, @RowId Int
		Set @Rows = 1
		Set @RowId = 1

		While @Rows > 0
		Begin
			
			SELECT Top 1 @RowId = Id, @PRC_Id = Id, @PRC_MovementType= MovementType,
				@PRC_WarehouseId = WarehouseId, @PRC_ProductId = ProductId, 
				@PRC_BatchSerialId = BatchSerialId, @PRC_Quantity = Quantity 
			from @TableKardexCustody where Id >= @RowId
				And cast(MovementType as varchar(20)) + cast(WarehouseId as varchar(20)) + cast(ProductId as varchar(20)) + cast(isnull(BatchSerialId,0) as varchar(20)) in (select cast(MovementType as varchar(20)) + cast(WarehouseId as varchar(20)) + cast(ProductId as varchar(20)) + cast(isnull(BatchSerialId,0) as varchar(20)) from @TableKardexCustody group by MovementType,WarehouseId, ProductId, BatchSerialId having count(*) > 1) 
			Order By Id

			Set @Rows = @@ROWCOUNT
			If @Rows = 0 
				Break
			
			set @IndexCursor += 1
			if @Last_MovementType <> @PRC_MovementType or @Last_WarehouseId <> @PRC_WarehouseId or @Last_ProductId <> @PRC_ProductId or isnull(@Last_BatchSerialId,0) <> isnull(@PRC_BatchSerialId,0) begin
				set @IndexCursor = 1
				set @Sum_Quantity = 0
			end
			if @IndexCursor > 1 begin
				if @PRC_MovementType = 1 begin
					update @TableKardexCustody set PreviousAmountProduct += @Sum_Quantity, PreviousAmountWarehouse += @Sum_Quantity, PreviousAmountBatch = iif(PreviousAmountBatch = 0,0, PreviousAmountBatch + @Sum_Quantity) where Id = @PRC_Id
				end
				else if @PRC_MovementType = 2 begin
					update @TableKardexCustody set PreviousAmountProduct -= @Sum_Quantity, PreviousAmountWarehouse -= @Sum_Quantity, PreviousAmountBatch = iif(PreviousAmountBatch = 0,0, PreviousAmountBatch - @Sum_Quantity) where Id = @PRC_Id
				end
			end
			set @Sum_Quantity += @PRC_Quantity
			set @Last_MovementType = @PRC_MovementType
			set @Last_WarehouseId = @PRC_WarehouseId 
			set @Last_ProductId = @PRC_ProductId 
			set @Last_BatchSerialId = isnull(@PRC_BatchSerialId,0)

			Set @RowId += 1
		End
		
		INSERT INTO [Inventory].[KardexCustody]
           ([AdmissionNumber]
		   ,[MovementType]
           ,[ThirdPartyId]
           ,[WarehouseId]
           ,[ProductId]
           ,[BatchSerialId]
           ,[DocumentDate]
           ,[Quantity]
           ,[Value]
           ,[PreviousCost]
           ,[AverageCost]
           ,[PreviousAverageCost]
           ,[PreviousAmountProduct]
           ,[PreviousAmountWarehouse]
           ,[PreviousAmountBatch]
           ,[EntityId]
           ,[EntityCode]
           ,[EntityName]
           ,[CreationUser]
           ,[CreationDate])
		   select @AdmissionNumber, MovementType, ThirdPartyId, WarehouseId, ProductId, BatchSerialId, [Common].[GETDATE](), Quantity, Value, PreviousCost, AverageCost, PreviousAverageCost, PreviousAmountProduct, PreviousAmountWarehouse, PreviousAmountBatch, @EntityId, @EntityCode, @EntityName, @User, getdate() from @TableKardexCustody order by MovementType, ProductId, BatchSerialId, Id
		select '0' as CodeMessage, 'se Afecto correctamente el Kardex y el Inventario Fisico de Custodia' as Message, cast(1 as tinyint) as [Status]
	end try
	begin catch
		select '999' as CodeMessage, 'Ocurrio un error al afectar el KardexCustody: ' + ERROR_MESSAGE() + ' Linea: ' + cast(ERROR_LINE() as varchar(20)) as Message, cast(3 as tinyint) as [Status]
	end catch
END