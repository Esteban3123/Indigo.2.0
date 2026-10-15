
CREATE VIEW [Inventory].[ViewListProductCustody]
AS



SELECT 
	f.Id AS ID, P.Code AS ProductCode, P.Name AS ProductName, IIF(P.MeasurementUnitId IS NOT NULL,IMU.Name, IIF( P.ATCId IS NOT NULL,rout.name,'')) AS  PharmaceuticalForm, BS.BatchCode , 
	bS.ExpirationDate,f.Quantity, f.AdmissionNumber, RTRIM(LTRIM(pa.IPCODPACI)) + ' - ' + RTRIM(LTRIM(pa.IPNOMCOMP)) AS Paciente,a.Code AS WarehouseCode, a.Name WareHouseName    
FROM Inventory.PhysicalInventoryCustody f INNER JOIN
Inventory.InventoryProduct p ON p.Id = f.ProductId INNER JOIN
Inventory.Warehouse a ON a.Id = f.WarehouseId   INNER JOIN
--Billing.RevenueControl RC on RC.Id = f.RevenueControlId inner join  
dbo.ADINGRESO RC  ON f.AdmissionNumber=RC.NUMINGRES INNER JOIN 
 Inventory.BatchSerial BS ON BS.Id = f.BatchSerialId INNER JOIN 
dbo.INPACIENT Pa ON pa.IPCODPACI = RC.IPCODPACI LEFT JOIN
Inventory.InventoryMeasurementUnit IMU ON IMU.Id = P.MeasurementUnitId LEFT JOIN
Inventory.ATC AS atc ON atc.id = p.ATCId LEFT JOIN
Inventory.AdministrationRoute AS rout ON rout.Id = atc.AdministrationRouteId