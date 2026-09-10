
CREATE FUNCTION [dbo].[MedicationAvailability] 
(
	@CODPRODUC AS char(20),
	@CentroAtencion AS char(20)
)
RETURNS INTEGER
AS
Begin
		DECLARE @Almacenes as table (Id int)
		DECLARE @DISPONIBLES AS INTEGER

		if @CentroAtencion is not null and len(rtrim(@CentroAtencion))> 0
		begin
			INSERT INTO @Almacenes
			select Id from Inventory.Warehouse where CodeCenterAttention = @CentroAtencion
		end

		SELECT @DISPONIBLES = sum(isnull(phy.Quantity,0)) 
		FROM dbo.IHLISTPRO D With(Nolock)
		INNER JOIN Inventory.ATC C  With(Nolock) ON RTRIM(D.CODPRODUC) = C.Code and C.DiluentProduct = 0
		LEFT OUTER JOIN Inventory.InventoryProduct B  With(Nolock) on B.Status = 1 AND B.ATCId = C.Id
		LEFT OUTER JOIN Inventory.PhysicalInventory phy  With(Nolock) on phy.ProductId = B.Id
		AND phy.WarehouseId IN (SELECT Id from @Almacenes) 
		WHERE D.CODPRODUC = @CODPRODUC

		RETURN ISNULL(@DISPONIBLES, 0)
end
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Calcula la cantidad disponible en inventario físico de un medicamento o insumo específico, dado su código de producto y un centro de atención. Consulta las bodegas asociadas al centro de atención indicado, luego suma las unidades registradas en el inventario físico para ese producto en dichas bodegas, cruzando el catálogo de productos farmacéuticos (IHLISTPRO) con la clasificación ATC y el inventario físico por bodega. Retorna un entero con el total de unidades disponibles (cero si no hay existencias), y se usa para verificar disponibilidad de medicamentos antes de dispensar, formular o gestionar pedidos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicationAvailability';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'FUNCTION', @level1name = N'MedicationAvailability';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Calcula la cantidad disponible total de un medicamento en las bodegas asociadas a un centro de atención, excluyendo productos diluyentes.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El producto debe existir en el catálogo maestro y tener correspondencia en la clasificación ATC; Si se especifica centro de atención, deben existir bodegas asociadas a ese centro para obtener cantidades', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca retorna NULL: cuando no hay existencias o no hay bodegas, retorna 0; Excluye del cálculo los productos marcados como diluyentes (DiluentProduct=0); Solo considera productos de inventario activos (Status=1); La disponibilidad se restringe a las bodegas del centro de atención solicitado; Usa lecturas con NOLOCK en todas las tablas consultadas', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Disponibilidad de medicamentos; Centro de atención; Bodega/almacén; Producto diluyente; Inventario físico; Clasificación ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] RETURN: Retorna la suma de cantidades físicas (PhysicalInventory.Quantity) del producto en bodegas del centro indicado; si la suma es NULL retorna 0', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Centro de atención no nulo y con longitud mayor a 0 → Carga en tabla temporal las bodegas (Warehouse) cuyo CodeCenterAttention coincide con el centro recibido else No se cargan bodegas, por lo que el filtro de WarehouseId quedará vacío y la suma resultará en 0; si ATC.DiluentProduct = 0 → Solo se consideran productos no clasificados como diluyentes para el cálculo de disponibilidad; si InventoryProduct.Status = 1 → Solo se toman en cuenta productos de inventario activos', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; dbo.IHLISTPRO; Inventory.ATC; Inventory.InventoryProduct; Inventory.PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'FUNCTION', @level1name=N'MedicationAvailability';
GO
