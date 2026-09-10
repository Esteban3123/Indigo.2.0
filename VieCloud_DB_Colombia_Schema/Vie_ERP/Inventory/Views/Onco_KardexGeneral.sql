
CREATE VIEW [Inventory].[Onco_KardexGeneral]
AS
     SELECT al.Code, 
            al.Name, 
            k.Id, 
            pd.Code Código, 
            PD.Name NombreProducto, 
            pd.CodeCUM CUM, 
            PUC.Number, 
            PUC.Name NomCuenta, 
            K.EntityCode, 
            K.EntityName,

            --CASE WHEN K.EntityName = 'EntranceVoucher' THEN   
            --P.Code +' '+ p.Name+' '+ CE.InvoiceNumber +' '+ ce.Description WHEN K.EntityName = 'PharmaceuticalDispensing' THEN 'Ingeso Nro ' + dp.AdmissionNumber WHEN K.EntityName = 'PharmaceuticalDispensingDevolution'  
            --THEN 'Ingeso Nro ' + DD.AdmissionNumber  WHEN K.EntityName = 'TransferOrder' THEN AL.Code  WHEN K.EntityName = 'InventoryAdjustment'   
            --THEN AJ.Description WHEN K.EntityName = 'TransferOrderDevolution' THEN DTR.Description END AS Descripción,  

            K.DocumentDate AS FechaR, 
            K.CreationDate AS FechaC, 
            k.MovementType Tipo, 
            k.PreviousAmountWarehouse SaldoAnt,
            CASE
                WHEN MovementType = '1'
                THEN k.Quantity
            END CantEnt,
            CASE
                WHEN MovementType = '2'
                THEN k.Quantity
            END CantSal,
            CASE
                WHEN k.Value > 0
                THEN k.Value
                WHEN k.Value <= 0
                THEN K.AverageCost
            END CtProm, 
            k.AffectInventory AS Ct
     FROM inventory.Kardex AS K
          INNER JOIN Inventory.InventoryProduct PD WITH(NOLOCK) ON Pd.Id = k.ProductId
          LEFT JOIN Inventory.Warehouse AL WITH(NOLOCK) ON al.Id = k.WarehouseId  
          --left join VIE08.Inventory.EntranceVoucher CE WITH (nolock) ON CE.Id = k.EntityId AND K.EntityName = 'EntranceVoucher'  
          --left join VIE08.Common.Supplier P WITH (nolock) ON P.Id = CE.SupplierId  
          --left join VIE08.Inventory.PharmaceuticalDispensing DP WITH (nolock) ON DP.Id = k.EntityId AND K.EntityName = 'PharmaceuticalDispensing'  
          --left join VIE08.Inventory.PharmaceuticalDispensingDevolution DD WITH (nolock) ON DD.Id = k.EntityId AND K.EntityName = 'PharmaceuticalDispensingDevolution'  
          --left join VIE08.Inventory.TransferOrder TR WITH (nolock) ON TR.Id = k.EntityId AND K.EntityName = 'TransferOrder' AND TR.TargetWarehouseId = AL.Id  
          --LEFT JOIN VIE08.Inventory.InventoryAdjustment AJ WITH (nolock) ON AJ.Id = k.EntityId AND K.EntityName = 'InventoryAdjustment'  
          --LEFT JOIN VIE08.Inventory.TransferOrderDevolution DTR WITH (nolock) ON DTR.Id = k.EntityId AND DTR.TransferOrderId = TR.Id AND K.EntityName = 'TransferOrderDevolution'  
          LEFT JOIN Inventory.ProductGroup GP WITH(NOLOCK) ON GP.ID = PD.ProductGroupId
          LEFT JOIN Payments.AccountPayableConcepts CCXP WITH(NOLOCK) ON GP.InventoryAccountPayableConceptId = CCXP.Id
          LEFT JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON PUC.ID = CCXP.IdAccount;

---WHERE  K.CreationDate  BETWEEN '2020-08-31 22:56:00.880' and  '2020-09-02 22:56:00.880' ---and k.EntityCode='0000001261'---pd.Code='20096371-25' and   
---k.EntityCode='0000001261'
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kardex general de inventario oncológico: consolida todos los movimientos de entrada y salida de productos (medicamentos, insumos y dispositivos médicos) registrados en el kardex, enriquecidos con información del producto (código, nombre y código CUM), la bodega o almacén donde ocurrió el movimiento, el grupo de producto al que pertenece y la cuenta contable PUC asociada a través del concepto de cuentas por pagar. Para cada movimiento expone el saldo anterior, la cantidad de entrada o salida según el tipo de movimiento, y el costo promedio vigente (usando el valor del movimiento si es positivo, o el costo promedio del producto en caso contrario). Sirve como base de reportería contable y de trazabilidad de inventario para el área de oncología, permitiendo auditar las existencias, valorizar el stock y conciliar las cuentas de inventario por producto, lote, bodega y cuenta contable en un período determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_KardexGeneral';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'Onco_KardexGeneral';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de kardex consolidado de inventario para oncología que expone movimientos de productos por bodega con saldos, cantidades de entrada/salida, costo promedio y la cuenta contable asociada al grupo del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada movimiento del kardex debe referenciar un producto existente en el catálogo de inventario (INNER JOIN sobre InventoryProduct).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen movimientos cuyo producto exista en Inventory.InventoryProduct.; El costo promedio reportado nunca queda en cero o negativo: si Value no es positivo se sustituye por AverageCost.; Las entradas y salidas se separan en columnas distintas según el tipo de movimiento (1=entrada, 2=salida).; La cuenta contable expuesta proviene del concepto de cuentas por pagar configurado en el grupo del producto.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Kardex; Inventario; Producto; CUM (Código Único de Medicamento); Bodega/Almacén; Movimiento de inventario (entrada/salida); Costo promedio; Saldo anterior; Grupo de producto; Concepto de cuentas por pagar; Cuenta contable (PUC)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.Kardex: Devuelve cada movimiento del kardex enriquecido con datos de producto, bodega, grupo de producto, concepto de cuentas por pagar y cuenta contable del PUC.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MovementType = ''1'' → El movimiento se clasifica como cantidad de entrada (CantEnt = Quantity) else Se deja nulo en CantEnt; si MovementType = ''2'' → El movimiento se clasifica como cantidad de salida (CantSal = Quantity) else Se deja nulo en CantSal; si k.Value > 0 → Se toma k.Value como costo promedio mostrado (CtProm) else Cuando k.Value <= 0 se toma K.AverageCost como costo promedio mostrado', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Kardex; Inventory.InventoryProduct; Inventory.Warehouse; Inventory.ProductGroup; Payments.AccountPayableConcepts; GeneralLedger.MainAccounts', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'Onco_KardexGeneral';
GO
