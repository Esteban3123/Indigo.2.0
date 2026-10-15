
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