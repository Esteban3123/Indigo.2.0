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