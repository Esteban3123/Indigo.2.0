
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