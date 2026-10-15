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