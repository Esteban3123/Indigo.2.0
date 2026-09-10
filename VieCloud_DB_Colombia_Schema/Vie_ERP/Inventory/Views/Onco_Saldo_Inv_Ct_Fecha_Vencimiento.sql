CREATE VIEW [Inventory].[Onco_Saldo_Inv_Ct_Fecha_Vencimiento]
AS
     SELECT PD.Id, 
            pd.Code Código, 
            pd.Name, 
            al.Code AS Cod_Almac, 
            al.Name AS Nom_Almacen, 
            (CASE
                 WHEN PD.ProductTypeId = '1'
                 THEN 'MEDICAMENTO'
                 WHEN PD.ProductTypeId = '2'
                 THEN 'INSUMOS'
                 WHEN PD.ProductTypeId = '6'
                 THEN 'NUTRICIONES'
                 ELSE 'OTR0'
             END) TIPO, 
            LT.BatchCode AS LOTE, 
            LT.ExpirationDate AS Fecha_Vencimiento, 
            sal.Quantity Cant
     FROM Inventory.InventoryProduct PD
          INNER JOIN inventory.PhysicalInventory AS Sal WITH(NOLOCK) ON SAL.ProductId = pd.Id
          INNER JOIN inventory.Warehouse AS AL WITH(NOLOCK) ON SAL.WarehouseId = al.Id
          LEFT JOIN inventory.BatchSerial AS LT WITH(NOLOCK) ON SAL.BatchSerialId = lt.Id
     WHERE sal.Quantity > 0;

-- K.EntityName in ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution','TransferOrder', 'TransferOrderDevolution' ) and pd.code ='58816-01'   
---group by  pd.ID, PD.CODE, pd.Name,PD.ProductTypeId, LT.BatchCode, LT.ExpirationDate,    
--order by  pd.ID
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldos de inventario de oncología por lote y fecha de vencimiento. Muestra las existencias actuales (cantidad mayor a cero) de medicamentos, insumos y nutriciones almacenadas en cada bodega, identificando el lote y su fecha de vencimiento para facilitar el control de caducidad. Integra el catálogo de productos, el inventario físico por bodega y los lotes o series registrados, clasificando cada artículo según su tipo (medicamento, insumo, nutrición u otro). Sirve para reportería de trazabilidad, gestión de vencimientos y control de stock en el servicio de oncología.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el saldo físico vigente de productos de inventario por almacén y lote, indicando tipo de producto y fecha de vencimiento, para soporte de control de stock oncológico.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existen registros de inventario físico (PhysicalInventory) asociados a productos y almacenes válidos.; El producto puede o no tener lote/serial asociado (LEFT JOIN sobre BatchSerial).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen saldos con cantidad estrictamente positiva (Quantity > 0).; Cada producto se categoriza en uno de cuatro tipos: MEDICAMENTO, INSUMOS, NUTRICIONES u OTR0.; La ausencia de lote no excluye al producto del resultado (LEFT JOIN con BatchSerial).; El saldo se reporta a nivel de producto + almacén + lote.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; Insumo; Nutrición; Almacén; Lote; Fecha de vencimiento; Saldo de inventario; Inventario físico; Oncología', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.InventoryProduct: Devuelve únicamente filas donde la cantidad física en inventario es mayor a cero (sal.Quantity > 0).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica el producto como ''MEDICAMENTO'' else Evalúa otros tipos; si PD.ProductTypeId = ''2'' → Clasifica el producto como ''INSUMOS''; si PD.ProductTypeId = ''6'' → Clasifica el producto como ''NUTRICIONES'' else Cualquier otro ProductTypeId se etiqueta como ''OTR0''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; inventory.PhysicalInventory; inventory.Warehouse; inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Saldo_Inv_Ct_Fecha_Vencimiento';
GO
