

CREATE VIEW [Inventory].[ViewPharmaceuticalDispensing]
AS

SELECT	
	CAST(NEWID() AS VARCHAR(40)) 'id',
	pd.Type,
	pd.IdPharmaceuticalDispensing,
	pd.Codigo,
	pd.UnidadOperativa,
	pd.Ingreso,
	pd.NombreCompletoPaciente,
	pd.FechaDispensacion,
	pd.AfectaInventario,
	pd.Estado,
	pd.UsuarioCreado,
	pd.FechaCreacion,
	pd.ProductId,
	pd.IdProducto,
	pd.Producto,
	pd.IdAlmacen,
	pd.Almacen,
	pd.Cantidad,
	pd.UnidadFuncional
FROM 
(
	SELECT
	1 Type,
	ipd.Id AS 'IdPharmaceuticalDispensing',
	ipd.Code AS 'Codigo',
	cou.UnitName AS 'UnidadOperativa',
	ipd.AdmissionNumber AS 'Ingreso',
	p.IPNOMCOMP AS 'NombreCompletoPaciente',
	ipd.DocumentDate AS 'FechaDispensacion', 
	case when ipd.AffectInventory = 1 then 'Si'when ipd.AffectInventory = 0 then 'No' end AS 'AfectaInventario',
	case when ipd.Status = 1 then 'registrado' when ipd.Status = 2 then 'confirmado' else 'anulado' end AS 'Estado',
	ipd.CreationUser AS 'UsuarioCreado',
	ipd.CreationDate AS 'FechaCreacion',
	iip.Id AS 'ProductId',
	iip.Code AS 'IdProducto',
	iip.Name AS 'Producto',
	iw.Id AS 'IdAlmacen',
	iw.Name AS 'Almacen',
	ipdd.Quantity AS 'Cantidad',
	pfu.Name AS 'UnidadFuncional'
	FROM Inventory.PharmaceuticalDispensing AS ipd
	INNER JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
	INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
	INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
	INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
	INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
	LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
	INNER JOIN dbo.INPACIENT p on p.IPCODPACI = ad.IPCODPACI

UNION ALL 

	SELECT DISTINCT
	2 Type,
	ipd.Id AS 'IdPharmaceuticalDispensing',
	ipd.Code AS 'Codigo',
	cou.UnitName AS 'UnidadOperativa',
	ipd.AdmissionNumber AS 'Ingreso',
	p.IPNOMCOMP AS 'NombreCompletoPaciente',
	ipd.DocumentDate AS 'FechaDispensacion', 
	case when ipd.AffectInventory = 1 then 'Si'when ipd.AffectInventory = 0 then 'No' end AS 'AfectaInventario',
	case when ipd.Status = 1 then 'registrado' when ipd.Status = 2 then 'confirmado' else 'anulado' end AS 'Estado',
	ipd.CreationUser AS 'UsuarioCreado',
	ipd.CreationDate AS 'FechaCreacion',
	0 AS 'ProductId',
	NULL AS 'IdProducto',
	NULL AS 'Producto',
	iw.Id AS 'IdAlmacen',
	iw.Name AS 'Almacen',
	0 AS 'Cantidad',
	pfu.Name AS 'UnidadFuncional'
	FROM Inventory.PharmaceuticalDispensing AS ipd
	INNER JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
	INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
	INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
	INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
	INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
	LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
	INNER JOIN dbo.INPACIENT p on p.IPCODPACI = ad.IPCODPACI
) pd