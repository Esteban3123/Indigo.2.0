
CREATE VIEW [Inventory].[Onco_Dispensación]
AS
     SELECT CAST(K.DocumentDate AS DATE) AS Fecha, 
            K.EntityCode AS Documento, 
            al.Code Almac, 
            al.Name Nombre_Almacen,
            CASE
                WHEN MovementType = '1'
                THEN 'Dispensación'
                ELSE 'Devolución'
            END TipoMov,
            CASE
                WHEN pd.ProductTypeId = '1'
                THEN 'Medicamento'
                WHEN pd.ProductTypeId = '2'
                THEN 'Insumo'
                ELSE 'Otro'
            END Tipo_Actividad, 
            pd.Code Código, 
            PD.Name NombreProducto, 
            pd.CodeCUM CUM, 
            PUC.Number, 
            PUC.Name NomCuenta, 
            K.EntityCode, 
            K.EntityName,
            CASE
                WHEN K.EntityName = 'PharmaceuticalDispensing'
                THEN 'Ingeso Nro ' + dp.AdmissionNumber
                WHEN K.EntityName = 'PharmaceuticalDispensingDevolution'
                THEN 'Ingeso Nro ' + DD.AdmissionNumber
            END AS Descripción,
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
          LEFT JOIN Inventory.PharmaceuticalDispensing DP WITH(NOLOCK) ON DP.Id = k.EntityId
                                                                          AND K.EntityName = 'PharmaceuticalDispensing'
          LEFT JOIN Inventory.PharmaceuticalDispensingDevolution DD WITH(NOLOCK) ON DD.Id = k.EntityId
                                                                                    AND K.EntityName = 'PharmaceuticalDispensingDevolution'

          --left join VIE08.Inventory.TransferOrder TR WITH (nolock) ON TR.Id = k.EntityId AND K.EntityName = 'TransferOrder' AND TR.TargetWarehouseId = AL.Id  
          --LEFT JOIN VIE08.Inventory.InventoryAdjustment AJ WITH (nolock) ON AJ.Id = k.EntityId AND K.EntityName = 'InventoryAdjustment'  
          --LEFT JOIN VIE08.Inventory.TransferOrderDevolution DTR WITH (nolock) ON DTR.Id = k.EntityId AND DTR.TransferOrderId = TR.Id AND K.EntityName = 'TransferOrderDevolution'  
          LEFT JOIN Inventory.ProductGroup GP WITH(NOLOCK) ON GP.ID = PD.ProductGroupId
          LEFT JOIN Payments.AccountPayableConcepts CCXP WITH(NOLOCK) ON GP.InventoryAccountPayableConceptId = CCXP.Id
          LEFT JOIN GeneralLedger.MainAccounts AS puc WITH(NOLOCK) ON PUC.ID = CCXP.IdAccount
          LEFT JOIN dbo.ADINGRESO AS ing WITH(NOLOCK) ON ing.NUMINGRES = dp.AdmissionNumber
                                                         OR ing.NUMINGRES = dd.AdmissionNumber
          LEFT JOIN dbo.INPACIENT AS pac WITH(NOLOCK) ON ing.IPCODPACI = pac.ID     
     ---LEFT JOIN Common.OperatingUnit  as up WITH (nolock) ON up.Id = ing.u      

     WHERE K.EntityName IN('PharmaceuticalDispensing', 'PharmaceuticalDispensingDevolution')
          AND K.id = '23787';--- CreationDate  BETWEEN '2020-08-31 22:56:00.880' and  '2020-09-02 22:56:00.880' ---and k.EntityCode='0000001261'---pd.Code='20096371-25' and   
---k.EntityCode='0000001261'  