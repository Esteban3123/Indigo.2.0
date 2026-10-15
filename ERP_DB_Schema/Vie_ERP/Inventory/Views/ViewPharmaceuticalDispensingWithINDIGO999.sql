CREATE VIEW [Inventory].[ViewPharmaceuticalDispensingWithINDIGO999]
AS
     SELECT ipdd.Id AS 'id', 
            ipd.Id AS 'IdPharmaceuticalDispensing', 
            ipd.Code AS 'Codigo', 
            cou.UnitName AS 'UnidadOperativa', 
            ipd.AdmissionNumber AS 'Ingreso', 
            p.IPNOMCOMP AS 'NombreCompletoPaciente', 
            ipd.DocumentDate AS 'FechaDispensacion',
            CASE
                WHEN ipd.AffectInventory = 1
                THEN 'Si'
                WHEN ipd.AffectInventory = 0
                THEN 'No'
            END AS 'AfectaInventario',
            CASE
                WHEN ipd.STATUS = 1
                THEN 'registrado'
                WHEN ipd.STATUS = 2
                THEN 'confirmado'
                ELSE 'anulado'
            END AS 'Estado', 
            ipd.CreationUser AS 'UsuarioCreado', 
            ipd.CreationDate AS 'FechaCreacion', 
            iip.Code AS 'IdProducto', 
            iip.Name AS 'Producto', 
            iw.Code AS 'IdAlmacen', 
            iw.Name AS 'Almacen', 
            ipdd.Quantity AS 'Cantidad', 
            pfu.Name AS 'UnidadFuncional'
     FROM Inventory.PharmaceuticalDispensing AS ipd
          LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
          INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
          INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
          INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
          INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
          LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
          INNER JOIN dbo.INPACIENT p ON p.IPCODPACI = ad.IPCODPACI;