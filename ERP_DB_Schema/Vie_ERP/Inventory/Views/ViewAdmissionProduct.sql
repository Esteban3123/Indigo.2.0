








CREATE VIEW [Inventory].[ViewAdmissionProduct]
AS
SELECT pdd.FunctionalUnitId,pdd.ProductId,pd.AdmissionNumber, p.Code + ' - ' + p.Name AS ProductName, 
fu.Code + ' - ' + fu.Name AS FunctionalUnitName, SUM(pdd.Quantity) AS DispensingQuantity, 
SUM(pdd.ReturnedQuantity) AS DevolutionQuantity, SUM(pdd.Quantity) - SUM(pdd.ReturnedQuantity) AS Quantity
FROM            Inventory.PharmaceuticalDispensingDetail AS pdd with (nolock)
INNER JOIN Inventory.PharmaceuticalDispensing AS pd with (nolock) ON pdd.PharmaceuticalDispensingId = pd.Id 
INNER JOIN  Inventory.InventoryProduct AS p with (nolock) ON pdd.ProductId = p.Id                          
INNER JOIN  Payroll.FunctionalUnit AS fu  with (nolock) ON pdd.FunctionalUnitId = fu.Id		     
 where pd.Status = 2
GROUP BY pdd.ProductId, p.Code, p.Name, fu.Code, fu.Name, pdd.FunctionalUnitId, pd.AdmissionNumber