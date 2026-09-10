CREATE VIEW [Inventory].[Onco_TABLASALDOXBODEGA]
AS
     SELECT PD.Id, 
            pd.Code Código, 
            pd.Name, 
            (CASE
                 WHEN PD.ProductTypeId = '1'
                 THEN 'MEDICAMENTO'
                 WHEN PD.ProductTypeId = '2'
                 THEN 'INSUMOS'
                 WHEN PD.ProductTypeId = '6'
                 THEN 'NUTRICIONES'
                 ELSE 'OTR0'
             END) TIPO, 
            SUM(CASE
                    WHEN sal.WarehouseId = '1'
                    THEN sal.Quantity
                    ELSE 0
                END) AL001, 
            SUM(CASE
                    WHEN sal.WarehouseId = '2'
                    THEN sal.Quantity
                    ELSE 0
                END) AL002, 
            SUM(CASE
                    WHEN sal.WarehouseId = '5'
                    THEN sal.Quantity
                    ELSE 0
                END) AL005, 
            SUM(CASE
                    WHEN sal.WarehouseId = '9'
                    THEN sal.Quantity
                    ELSE 0
                END) AL009, 
            SUM(CASE
                    WHEN sal.WarehouseId = '11'
                    THEN sal.Quantity
                    ELSE 0
                END) AL011, 
            SUM(CASE
                    WHEN sal.WarehouseId = '15'
                    THEN sal.Quantity
                    ELSE 0
                END) AL015, 
            SUM(CASE
                    WHEN sal.WarehouseId = '16'
                    THEN sal.Quantity
                    ELSE 0
                END) AL016, 
            SUM(CASE
                    WHEN sal.WarehouseId = '17'
                    THEN sal.Quantity
                    ELSE 0
                END) AL017, 
            SUM(CASE
                    WHEN sal.WarehouseId = '18'
                    THEN sal.Quantity
                    ELSE 0
                END) AL018, 
            SUM(CASE
                    WHEN sal.WarehouseId = '19'
                    THEN sal.Quantity
                    ELSE 0
                END) AL019, 
            SUM(CASE
                    WHEN sal.WarehouseId = '29'
                    THEN sal.Quantity
                    ELSE 0
                END) AL029, 
            SUM(CASE
                    WHEN sal.WarehouseId = '35'
                    THEN sal.Quantity
                    ELSE 0
                END) AL035, 
            SUM(CASE
                    WHEN sal.WarehouseId = '42'
                    THEN sal.Quantity
                    ELSE 0
                END) AL042, 
            SUM(CASE
                    WHEN sal.WarehouseId = '44'
                    THEN sal.Quantity
                    ELSE 0
                END) AL044, 
            SUM(CASE
                    WHEN sal.WarehouseId = '45'
                    THEN sal.Quantity
                    ELSE 0
                END) AL045, 
            SUM(CASE
                    WHEN sal.WarehouseId = '46'
                    THEN sal.Quantity
                    ELSE 0
                END) AL046, 
            SUM(CASE
                    WHEN sal.WarehouseId = '55'
                    THEN sal.Quantity
                    ELSE 0
                END) AL055, 
            SUM(CASE
                    WHEN sal.WarehouseId = '73'
                    THEN sal.Quantity
                    ELSE 0
                END) AL073, 
            SUM(CASE
                    WHEN sal.WarehouseId = '80'
                    THEN sal.Quantity
                    ELSE 0
                END) AL080, 
            SUM(CASE
                    WHEN sal.WarehouseId = '85'
                    THEN sal.Quantity
                    ELSE 0
                END) AL085, 
            SUM(CASE
                    WHEN sal.WarehouseId = '99'
                    THEN sal.Quantity
                    ELSE 0
                END) AL075, 
            SUM(CASE
                    WHEN sal.WarehouseId = '106'
                    THEN sal.Quantity
                    ELSE 0
                END) AL097
     FROM Inventory.InventoryProduct PD
          INNER JOIN inventory.PhysicalInventory AS Sal WITH(NOLOCK) ON SAL.ProductId = pd.Id

     ---WHERE -- K.EntityName in ('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution','TransferOrder', 'TransferOrderDevolution' ) and pd.code ='58816-01'   

     GROUP BY pd.ID, 
              PD.CODE, 
              pd.Name, 
              PD.ProductTypeId;

--order by  pd.ID
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de saldos de inventario físico por bodega para el módulo de Oncología. Muestra cada producto del catálogo (medicamentos, insumos y nutriciones) con su cantidad disponible desagregada por cada bodega o almacén (AL001, AL002, AL005, AL009, AL011, AL015, AL016, AL017, AL018, AL019, AL029, AL035, AL042, AL044, AL045, AL046, AL055, AL073, AL080, AL085, AL075, AL097), pivotando las filas del inventario físico en columnas por bodega. Combina el catálogo maestro de productos (InventoryProduct) con el conteo real de unidades en cada bodega (PhysicalInventory) para obtener el stock disponible por ubicación. Sirve para reportes de disponibilidad de medicamentos, insumos y nutriciones oncológicas en cada almacén, facilitando la gestión de abastecimiento y control de inventario en el servicio de oncología.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_TABLASALDOXBODEGA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_TABLASALDOXBODEGA';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida el saldo de inventario físico de cada producto pivotado por bodega, mostrando además su clasificación (medicamento, insumo, nutrición u otro).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de registros en Inventory.PhysicalInventory asociados a productos vigentes en Inventory.InventoryProduct.; Los identificadores de bodega (WarehouseId) usados como columnas (1, 2, 5, 9, 11, 15, 16, 17, 18, 19, 29, 35, 42, 44, 45, 46, 55, 73, 80, 85, 99, 106) deben existir en el maestro de bodegas para que los saldos sean significativos.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan productos que tengan al menos un registro en el inventario físico (INNER JOIN).; Los saldos se pivotean por bodega: cada columna ALxxx representa exclusivamente la suma de cantidades de la bodega con ese WarehouseId.; Si un producto no tiene movimientos en una bodega específica, su columna correspondiente queda en 0.; La clasificación de tipo de producto se limita a tres categorías conocidas (1, 2, 6); cualquier otro valor se agrupa como ''OTR0''.; La columna AL075 corresponde realmente a WarehouseId = 99 (no a 75), y AL097 a WarehouseId = 106.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Producto (medicamento, insumo, nutrición); Bodega/Almacén; Saldo de existencias', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve por producto el código, nombre, tipo y la suma de cantidades agrupadas por WarehouseId en columnas fijas (AL001..AL097).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductTypeId = 1 → Clasifica el producto como ''MEDICAMENTO''; si ProductTypeId = 2 → Clasifica el producto como ''INSUMOS''; si ProductTypeId = 6 → Clasifica el producto como ''NUTRICIONES'' else Clasifica el producto como ''OTR0''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.InventoryProduct; Inventory.PhysicalInventory', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_TABLASALDOXBODEGA';
GO
