
CREATE VIEW [Inventory].[Onco_Rotación_Inventario_Cons]
AS
     SELECT pd.Code Código, 
            PD.Name NombreProducto, 
            pd.CodeCUM CUM, 
            (CASE
                 WHEN PD.ProductTypeId = '1'
                 THEN 'MEDICAMENTO'
                 WHEN PD.ProductTypeId = '2'
                 THEN 'INSUMOS'
                 WHEN PD.ProductTypeId = '6'
                 THEN 'NUTRICIONES'
                 ELSE 'OTR0'
             END) NOMBTIPO, 
            (CASE
                 WHEN PD.ProductTypeId = '1'
                 THEN atc.Code
                 WHEN PD.ProductTypeId = '2'
                 THEN ins.Code
                 ELSE ''
             END) CODMedica, 
            (CASE
                 WHEN PD.ProductTypeId = '1'
                 THEN atc.Name
                 WHEN PD.ProductTypeId = '2'
                 THEN ins.SupplieName
                 ELSE ''
             END) NombMedica, 
            PD.ProductCost AS COSTPROM, 
            PD.FinalProductCost AS UCOMP, 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2020-8'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2020-8', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2020-9'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2020-9', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2020-10'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2020-10', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2020-11'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2020-11', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2020-12'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2020-12', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-1'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-1', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-2'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-2', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-3'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-3', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-4'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-4', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-5'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-5', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-6'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-6', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-7'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-7', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-8'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-8', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-9'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-9', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-10'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-10', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-11'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-11', 
            SUM(CASE
                    WHEN concat(YEAR(K.DocumentDate), '-', MONTH(K.DocumentDate)) = '2021-12'
                    THEN(CASE
                             WHEN MovementType = '1'
                             THEN -k.Quantity
                             ELSE k.Quantity
                         END)
                    ELSE 0
                END) '2021-12', 
            MIN(SAL.AL001) AS AL001, 
            MIN(SAL.AL002) AS AL002, 
            MIN(SAL.AL005) AS AL005, 
            MIN(SAL.AL009) AS AL009, 
            MIN(SAL.AL011) AS AL011, 
            MIN(SAL.AL015) AS AL015, 
            MIN(SAL.AL016) AS AL016, 
            MIN(SAL.AL017) AS AL017, 
            MIN(SAL.AL018) AS AL018, 
            MIN(SAL.AL019) AS AL019, 
            MIN(SAL.AL029) AS AL029, 
            MIN(SAL.AL035) AS AL035, 
            MIN(SAL.AL042) AS AL042, 
            MIN(SAL.AL044) AS AL044, 
            MIN(SAL.AL045) AS AL045, 
            MIN(SAL.AL046) AS AL046, 
            MIN(SAL.AL055) AS AL055, 
            MIN(SAL.AL073) AS AL073, 
            MIN(SAL.AL080) AS AL080, 
            MIN(SAL.AL085) AS AL085, 
            MIN(SAL.AL075) AS AL075, 
            MIN(SAL.AL097) AS AL097

     --PUC.Number,-- PUC.Name NomCuenta, K.EntityCode, K.EntityName, CASE WHEN K.EntityName = 'EntranceVoucher' THEN   
     --P.Code +' '+ p.Name+' '+ CE.InvoiceNumber +' '+ ce.Description WHEN K.EntityName = 'PharmaceuticalDispensing' THEN 'Ingeso Nro ' + dp.AdmissionNumber WHEN K.EntityName = 'PharmaceuticalDispensingDevolution'  
     --THEN 'Ingeso Nro ' + DD.AdmissionNumber  WHEN K.EntityName = 'TransferOrder' THEN AL.Code  WHEN K.EntityName = 'InventoryAdjustment' THEN AJ.Description WHEN K.EntityName = 'TransferOrderDevolution' THEN DTR.Description END AS Descripción,  
     --k.MovementType Tipo,-- k.PreviousAmountWarehouse SaldoAnt,   
     --CASE WHEN k.Value>0 THEN k.Value WHEN k.Value<=0 THEN K.AverageCost END CtProm, k.AffectInventory as Ct   

     FROM inventory.Kardex AS K
          INNER JOIN Inventory.InventoryProduct PD WITH(NOLOCK) ON Pd.Id = k.ProductId
          LEFT JOIN Inventory.ATC ATC WITH(NOLOCK) ON ATC.Id = PD.ATCId
          LEFT JOIN Inventory.InventorySupplie Ins WITH(NOLOCK) ON ins.Id = PD.SupplieId
          LEFT OUTER JOIN inventory.ProductType TIN WITH(NOLOCK) ON TIN.Id = PD.ProductTypeId
          LEFT OUTER JOIN Inventory.Warehouse AL WITH(NOLOCK) ON al.Id = k.WarehouseId
          LEFT JOIN Inventory.TransferOrder TR WITH(NOLOCK) ON TR.Id = k.EntityId
                                                               AND K.EntityName = 'TransferOrder'
                                                               AND TR.TargetWarehouseId = AL.Id
          LEFT JOIN Inventory.Onco_TABLASALDOXBODEGA AS Sal WITH(NOLOCK) ON SAL.Id = pd.Id  
     --left join Inventory.EntranceVoucher CE WITH (nolock) ON CE.Id = k.EntityId AND K.EntityName = 'EntranceVoucher'  
     --left join Common.Supplier P WITH (nolock) ON P.Id = CE.SupplierId  
     --left join Inventory.PharmaceuticalDispensing DP WITH (nolock) ON DP.Id = k.EntityId AND K.EntityName = 'PharmaceuticalDispensing'  
     --left join Inventory.PharmaceuticalDispensingDevolution DD WITH (nolock) ON DD.Id = k.EntityId AND K.EntityName = 'PharmaceuticalDispensingDevolution'  
     --LEFT JOIN Inventory.InventoryAdjustment AJ WITH (nolock) ON AJ.Id = k.EntityId AND K.EntityName = 'InventoryAdjustment'  
     --LEFT JOIN Inventory.TransferOrderDevolution DTR WITH (nolock) ON DTR.Id = k.EntityId AND DTR.TransferOrderId = TR.Id AND K.EntityName = 'TransferOrderDevolution'  
     --left join Inventory.ProductGroup GP WITH (nolock) ON GP.ID = PD.ProductGroupId  
     --LEFT JOIN GeneralLedger.MainAccounts  as puc WITH (nolock) ON PUC.ID = GP.ReferenceInputDebitAccountId  

     WHERE(K.EntityName IN('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution')
          OR (K.EntityName IN('TransferOrder', 'TransferOrderDevolution')
     AND tr.DispatchTo <> '1')) ---and pd.code ='19971195-02'  

     GROUP BY pd.Code, 
              PD.Name, 
              pd.CodeCUM, 
              PD.ProductTypeId, 
              ATC.CODE, 
              ATC.Name, 
              ins.code, 
              ins.SupplieName, 
              PD.FinalProductCost, 
              PD.ProductCost; ---, -----year(K.DocumentDate),month(K.DocumentDate), k.Quantity----,SAL.Quantity  
--order by  pd.Code  
---K.DocumentDate>'01-10-2019' ---and K.DocumentDate< '10-05-2020' ---and k.EntityCode='0000001261'---pd.Code='20096371-25' and   
---k.EntityCode='0000001261'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Rotación de inventario oncológico consolidada por producto y mes. Muestra, para cada medicamento, insumo o nutrición del catálogo (clasificados por tipo y con su código CUM y código ATC o de insumo), las cantidades netas despachadas o recibidas mes a mes durante el período agosto 2020 – agosto 2021, calculadas desde el kardex de movimientos (entradas suman, salidas restan). Incluye además el costo promedio y el último costo de compra de cada producto, y cruza con la tabla de saldos por bodega para el análisis de inventario disponible. Sirve para reportes de rotación, consumo histórico, gestión de stock y análisis farmacoeconómico en el área oncológica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Rotación_Inventario_Cons';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_Rotación_Inventario_Cons';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de rotación mensual de inventario para el área de oncología, que pivota cantidades de movimientos del kardex por mes (ago/2020 a dic/2021) y las cruza con saldos por bodega y datos del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los productos deben existir en InventoryProduct vinculados al Kardex por ProductId.; Los movimientos del Kardex deben tener DocumentDate y MovementType definidos.; Para movimientos tipo TransferOrder se requiere que la TransferOrder exista y su TargetWarehouseId coincida con la bodega del kardex.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se consideran movimientos de dispensación farmacéutica (y su devolución) y traslados cuyo destino de despacho no sea ''1''.; Los traslados se vinculan al kardex únicamente cuando la bodega destino de la orden coincide con la bodega del movimiento.; Las salidas (MovementType=''1'') siempre se registran con signo negativo y las demás con signo positivo en los acumulados mensuales.; El rango temporal de columnas pivote está fijo entre agosto de 2020 y diciembre de 2021.; El costo promedio y último costo reportados corresponden a los valores actuales del producto, no a los del periodo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Rotación de inventario; Kardex; Dispensación farmacéutica; Devolución de dispensación; Orden de traslado entre bodegas; Clasificación ATC de medicamentos; Insumos médicos; Nutriciones; Costo promedio y costo final del producto; Saldo por bodega; Oncología', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve un resultado tabular agrupado por producto con columnas mensuales de movimiento neto y mínimos de saldo por bodega.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PD.ProductTypeId = ''1'' → Clasifica como ''MEDICAMENTO'' y toma código/nombre desde ATC. else Si ProductTypeId=''2'' clasifica ''INSUMOS'' y toma datos de InventorySupplie; si ''6'' clasifica ''NUTRICIONES''; en otro caso ''OTR0'' sin código/nombre de medicamento.; si MovementType = ''1'' → La cantidad se resta (-k.Quantity), interpretándose como salida. else La cantidad se suma (+k.Quantity), interpretándose como entrada.; si concat(YEAR,''-'',MONTH) coincide con un mes específico entre 2020-8 y 2021-12 → El movimiento se acumula en la columna correspondiente a ese mes. else El movimiento aporta 0 a esa columna.; si K.EntityName IN (''PharmaceuticalDispensing'',''PharmaceuticalDispensingDevolution'') → El movimiento del kardex es incluido en la vista. else Solo se incluye si EntityName es ''TransferOrder''/''TransferOrderDevolution'' y la orden de traslado tiene DispatchTo distinto de ''1''.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.ATC; Inventory.InventorySupplie; Inventory.ProductType; Inventory.Warehouse; Inventory.TransferOrder; Inventory.Onco_TABLASALDOXBODEGA', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_Rotación_Inventario_Cons';
GO
