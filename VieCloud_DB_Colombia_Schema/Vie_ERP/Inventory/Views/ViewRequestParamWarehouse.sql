

CREATE view [Inventory].[ViewRequestParamWarehouse]
as

SELECT 
  wa.Id, 
  wa.Id AS WarehouseId, 
  wa.Code AS WarehouseCode, 
  wa.Name AS WarehouseName, 
  rw.Id AS RequestParamWarehouseId, 
  rp.Id AS RequestParamId,
  rp.RequiredAuthorization,
  wa.status as StatusWarehouse,
  CAST(
    CASE WHEN rw.Id IS NULL THEN 0 ELSE 1 END AS bit
  ) AS IsSelected 
FROM 
  Inventory.Warehouse AS wa 
  LEFT OUTER JOIN Inventory.RequestParamWarehouse AS rw ON rw.WarehouseId = wa.Id 
  LEFT OUTER JOIN Inventory.RequestParam AS rp ON rw.RequestParamId = rp.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra todas las bodegas o almacenes del módulo de inventario junto con su configuración de parámetros de solicitud. Para cada bodega indica si está asociada a un parámetro de pedido (IsSelected = true/false), el identificador de ese parámetro, si las solicitudes requieren autorización y el estado actual de la bodega. Sirve para visualizar y gestionar qué bodegas están habilitadas dentro de cada configuración de solicitud de materiales o medicamentos, combinando las tablas de bodegas, relación bodega-parámetro y parámetros de solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamWarehouse';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewRequestParamWarehouse';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el catálogo de bodegas indicando cuáles están asociadas a un parámetro de solicitud de inventario, junto con su estado y si requieren autorización.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Siempre se listan todas las bodegas existentes, estén o no vinculadas a un parámetro de solicitud (LEFT JOIN sobre Warehouse).; IsSelected es bit derivado: 1 cuando existe vínculo en RequestParamWarehouse, 0 en caso contrario.; Cuando la bodega no está vinculada, los campos provenientes de RequestParamWarehouse y RequestParam quedan en NULL.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Bodega/Almacén de inventario; Parámetros de solicitud de inventario; Autorización requerida; Estado de bodega', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] result-set: Para cada bodega de Inventory.Warehouse retorna una fila; si existe coincidencia en RequestParamWarehouse vía WarehouseId, IsSelected=1 y se exponen RequestParamWarehouseId, RequestParamId y RequiredAuthorization; de lo contrario IsSelected=0 y dichos campos son NULL.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si rw.Id IS NULL → IsSelected = 0 (bodega no asociada al parámetro de solicitud) else IsSelected = 1 (bodega asociada al parámetro de solicitud)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; Inventory.RequestParamWarehouse; Inventory.RequestParam', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewRequestParamWarehouse';
GO
