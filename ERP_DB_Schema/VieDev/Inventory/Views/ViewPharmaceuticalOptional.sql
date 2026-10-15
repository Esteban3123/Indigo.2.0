


CREATE VIEW [Inventory].[ViewPharmaceuticalOptional]
AS

SELECT
ipdd.Id AS 'id',
ipd.Id AS 'IdPharmaceuticalDispensing', 
ipd.Code AS 'Consecutivo',
ipd.CreationDate,
p.IPCODPACI,
ad.NUMINGRES,
iw.Code AS 'IdAlmacen',
iw.Name AS 'Almacen',
ipd.CreationUser,
per.Fullname,
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
cg.EntityType,
iim.Name AS 'UnidadProducto',
pfu.Name AS 'UnidadFuncional'
FROM Inventory.PharmaceuticalDispensing AS ipd with(nolock)
LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd with(nolock) ON ipd.Id = ipdd.PharmaceuticalDispensingId
INNER JOIN Common.ThirdParty AS thi with(nolock) ON thi.Id = ipdd.OrderedHealthProfessionalThirdPartyId
INNER JOIN Common.OperatingUnit AS cou with(nolock) ON cou.Id = ipd.OperatingUnitId
INNER JOIN Inventory.InventoryProduct AS iip with(nolock) ON iip.Id = ipdd.ProductId
INNER JOIN Contract.CareGroup AS cg with(nolock) ON cg.Id = ipdd.CareGroupId
LEFT JOIN Inventory.ATC AS atc with(nolock) ON atc.Id = iip.ATCId
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim with(nolock) ON iim.Id = iip.MeasurementUnitId
INNER JOIN Inventory.Warehouse AS iw with(nolock) ON iw.Id = ipdd.WarehouseId
INNER JOIN Payroll.FunctionalUnit AS pfu with(nolock) ON pfu.Id = ipdd.FunctionalUnitId
LEFT JOIN dbo.ADINGRESO AS ad with(nolock) ON ad.NUMINGRES = ipd.AdmissionNumber
INNER JOIN dbo.INPACIENT AS p with(nolock) on p.IPCODPACI = ad.IPCODPACI
LEFT JOIN Security.[UserInt] AS u ON u.UserCode = ipd.CreationUser
INNER JOIN Security.PersonInt AS per ON per.Id = u.IdPerson
WHERE iip.MeasurementUnitId is null