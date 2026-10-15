

CREATE VIEW [Inventory].[ViewPharmaceutical]
AS

SELECT
ipdd.Id AS 'id',
ipd.Id AS 'IdPharmaceuticalDispensing', 
ipd.Code AS 'Consecutivo',
ipd.CreationDate,
p.IPCODPACI,
ad.NUMINGRES,
iw.Id AS 'IdAlmacen',
iw.Name AS 'Almacen',
ipd.CreationUser,
per.Fullname,
iip.Id AS 'IdProduct',
iip.Code AS 'IdProducto',
iip.Name AS 'Producto',
ipdd.ServiceDate,
ipdd.Quantity,
ipdd.TotalSalesPrice,
ipdd.GrandTotalSalesPrice,
cg.Code,
atc.Code AS 'CodeAtc',
atc.Concentration,
p.IPNOMCOMP AS 'NombreCompletoPaciente',
p.IPPRINOMB,
p.IPSEGNOMB,
p.IPPRIAPEL,
p.IPSEGAPEL,
thi.Name AS 'NombreMedico',
ipdd.OrderedHealthProfessionalCode,
cg.Name,
case when cg.EntityType = 1 then 'EPS Contributivo' when cg.EntityType = 2 then 'EPS Subsidiado' when cg.EntityType = 3 then 'ET Vinculados Municipios' when cg.EntityType = 4 then 'ET Vinculados Departamentos' when cg.EntityType = 5 then 'ARL Riesgos Laborales'  else 'MP Medicina Prepagada' end AS 'Regimen',
iim.Name AS 'UnidadProducto',
pfu.Name AS 'UnidadFuncional'
FROM Inventory.PharmaceuticalDispensing AS ipd
inner JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
INNER JOIN Common.ThirdParty AS thi ON ipdd.OrderedHealthProfessionalThirdPartyId = thi.Id
INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
INNER JOIN Contract.CareGroup AS cg ON ipdd.CareGroupId = cg.Id
INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
left JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
LEFT JOIN dbo.ADINGRESO AS ad ON ipd.AdmissionNumber = ad.NUMINGRES
left JOIN dbo.INPACIENT AS p on p.IPCODPACI = ad.IPCODPACI
LEFT JOIN Security.[UserInt] AS u ON ipd.CreationUser= u.UserCode
left JOIN Security.PersonInt AS per ON u.IdPerson = per.Id
where ipd.Status = 2