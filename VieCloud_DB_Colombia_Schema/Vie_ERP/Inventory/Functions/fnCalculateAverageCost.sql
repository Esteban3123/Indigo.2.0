
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
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula el costo promedio ponderado de un producto en inventario al momento de registrar un nuevo movimiento (entrada de mercancía). Recibe el producto, la bodega, el lote o serie (si aplica), el valor unitario y la cantidad del movimiento entrante; luego consulta el stock actual en el inventario físico y el costo vigente del producto para combinarlos con los valores del nuevo movimiento mediante la fórmula de promedio ponderado. Si el producto maneja lotes (HandlesBatch), el cálculo se hace a nivel de lote específico; de lo contrario, se toma el total de la bodega. Si el producto aún no existe en el inventario físico, el costo promedio resultante es simplemente el valor del movimiento entrante. El resultado se usa para actualizar el costo promedio del producto en bodega, garantizando una valoración correcta del inventario de medicamentos, insumos y dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'fnCalculateAverageCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'FUNCTION', @level1name = N'fnCalculateAverageCost';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula el costo promedio ponderado de un producto en bodega combinando el inventario físico vigente con un nuevo movimiento de entrada, diferenciando si el producto maneja lotes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe existir en Inventory.InventoryProduct y tener un ProductSubGroup asociado con la bandera HandlesBatch definida.; Si el producto maneja lotes, se requiere BatchSerialId para localizar el registro de PhysicalInventory específico.; ProductCost del producto y Quantity del inventario físico deben estar definidos para que la fórmula ponderada arroje un valor válido.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El costo promedio retornado nunca es 0 (mínimo 1).; Para productos con HandlesBatch=1 la valoración se segrega por lote; para los demás se consolida por bodega.; Si no hay inventario previo, el costo promedio equivale al costo del movimiento entrante.; El resultado se trunca/convierte a numeric(18,0), perdiendo precisión decimal.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'costo promedio ponderado; inventario físico; bodega; lote (BatchSerial); manejo de lotes; subgrupo de productos; costo de producto; valoración de inventario', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Cuando no existe registro en PhysicalInventory (PhysycalInventoryId IS NULL), retorna el valor del movimiento entrante (@Value) como costo promedio.; [RETURN_RESULT] : Cuando existe inventario físico, retorna (ProductCost * QuantityPhysical + @Value * @Quantity) / (QuantityPhysical + @Quantity) como costo promedio ponderado.; [RETURN_RESULT] : Cuando el costo promedio calculado resulta en 0, se fuerza el retorno a 1 para evitar costos nulos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductSubGroup.HandlesBatch = 1 (el producto maneja lotes) → Busca el registro de PhysicalInventory filtrando por WarehouseId, ProductId y BatchSerialId (a nivel de lote específico). else Busca el registro de PhysicalInventory solo por WarehouseId y ProductId (a nivel de bodega, ignorando lote).; si No se encontró registro en PhysicalInventory para los criterios dados → El costo promedio se establece igual al valor unitario del movimiento entrante. else Se aplica la fórmula de promedio ponderado entre el stock físico existente y el nuevo movimiento.; si El costo promedio calculado es 0 → Se reemplaza por 1 como valor mínimo de costo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'FUNCTION', @level1name=N'fnCalculateAverageCost';
GO
