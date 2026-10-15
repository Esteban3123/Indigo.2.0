
--- =============================================
-- Author:		Cristhian Mauricio Salazar Narvaez
-- Create date: 09-12-2015
-- Description:	<Description, ,>
-- =============================================
create FUNCTION [Inventory].[fnCalculateAverageCost]
(
	@ProductId int,
	@WarehouseId int,
	@BatchSerialId int,
	@Value numeric(20,4),
	@Quantity int
)
RETURNS numeric(18,0)
AS
BEGIN
	
	declare @AverageCost numeric(18,0)
	declare @PhysycalInventoryId int, @QuantityPhysical int
	if (select s.HandlesBatch from Inventory.InventoryProduct p inner join Inventory.ProductSubGroup s on p.ProductSubGroupId = s.Id where p.Id = @ProductId) = 1 begin
		select @PhysycalInventoryId = Id, @QuantityPhysical = Quantity from Inventory.PhysicalInventory where WarehouseId = @WarehouseId and ProductId = @ProductId and BatchSerialId = @BatchSerialId
	end
	else begin
		select @PhysycalInventoryId = Id, @QuantityPhysical = Quantity from Inventory.PhysicalInventory where WarehouseId = @WarehouseId and ProductId = @ProductId
	end
	if @PhysycalInventoryId is null begin --Si no estaba creado en el inventario fisico
		set @AverageCost = @Value
	end
	else begin
		declare @ValuePhysicalInventory numeric(18,0) = (select ProductCost from Inventory.InventoryProduct where Id = @ProductId) * @QuantityPhysical
		declare @ValueMovement numeric(18,0) = @Value * @Quantity
		set @AverageCost = (@ValuePhysicalInventory + @ValueMovement) / (@QuantityPhysical + @Quantity)
	end
	if @AverageCost = 0 begin
		set @AverageCost = 1
	end
	return @AverageCost
END