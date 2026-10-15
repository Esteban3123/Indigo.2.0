
--CREATE PROCEDURE [dbo].[ODO_Remisiones_Entrada_Devoluciones]
--declare	@inidate DATETIME='2024-05-01';
--declare	@enddate DATETIME='2024-05-31';

CREATE view [Report].[UploadCubeVieSCMRemissionAndDevolutionV2] as

SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	re.code AS 'NRO DOCUMENTO',--[NroDocumento],
	re.remissiondate AS 'FECHA REMISION',--[FechaRemision],
	re.remissionnumber AS 'NRO REMISION',--[NroRemision], 
	re.description AS 'DETALLE',--[Detalle],
	re.code AS 'DOCUMENTO ORIGEN',--[DocumentoOrigen],
	su.code  + ' - ' + su.name as 'TERCERO',--[Tercero],
	ou.unitname AS 'SEDE',--[Sede],
	CONCAT(w.code, ' - ', w.name) AS 'BODEGA',--[Bodega],
	'Suma' 'OPERACION',--[Operacion],
	pt.name AS 'TIPO',--[Tipo],
	ISNULL(atc.code, iss.code ) AS 'CODIGO PADRE',--[CodigoPadre],
	ISNULL(atc.name, iss.suppliename ) AS 'DESCRIPCION PADRE',--[DescripcionPadre],
	ip.healthregistration AS 'REGISTRO SANITARIO',--[RegistroSanitario],
	ip.code AS 'CODIGO PRODUCTO',--[CodigoProducto],
	ip.name 'NOMBRE PRODUCTO',--[NombreProducto],
	bs.batchcode AS 'LOTE',--Lote,
	bs.expirationdate AS 'FECHA VENCIMIENTO',--[FechaVencimiento],
	redbs.quantity AS 'CANTIDAD',--Cantidad,
	red.unitvalue AS 'VALOR UNITARIO',--[ValorUnitario],
	(redbs.quantity * red.unitvalue) AS 'VALOR TOTAL',--[ValorTotal],
	cc.name AS 'CENTRO COSTO',--[CentroCosto],
	'Remision de Entrada' AS 'TIPO OPERACION',--[TipoOperacion]
	CAST(re.remissiondate AS DATE) 'FECHA BUSQUEDA',
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM inventory.remissionentrance AS re  WITH (NOLOCK)
INNER JOIN inventory.remissionentrancedetail AS red WITH (NOLOCK) ON re.id = red.remissionentranceid
INNER JOIN inventory.remissionentrancedetailbatchserial AS redbs WITH (NOLOCK) ON red.id = redbs.remissionentrancedetailid
INNER JOIN common.operatingunit AS ou WITH (NOLOCK) ON re.operatingunitid = ou.id
-- ==========
INNER JOIN inventory.warehouse w WITH (NOLOCK) ON re.warehouseid = w.id
INNER JOIN inventory.inventoryproduct ip WITH (NOLOCK) ON red.productid = ip.id
INNER JOIN inventory.producttype pt WITH (NOLOCK) ON ip.Producttypeid = pt.id
LEFT JOIN inventory.ATC AS ATC WITH (NOLOCK) ON ATC.id =IP.atcid 
LEFT JOIN inventory.inventorysupplie AS iss WITH (NOLOCK) ON iss.id = ip.supplieid
LEFT JOIN inventory.batchserial bs WITH (NOLOCK) ON redbs.batchserialid = bs.id
-- ==========
LEFT JOIN inventory.settinginventory si WITH (NOLOCK) ON re.operatingunitid = si.operatingunitid
LEFT JOIN inventory.productgroup pg WITH (NOLOCK) ON ip.productgroupid = pg.id
LEFT JOIN payroll.costcenter cc WITH (NOLOCK) ON cc.id = CASE si.associatecostcenter WHEN 2 THEN pg.costcenterid WHEN 3 THEN w.costcenterid END
LEFT JOIN common.supplier AS su WITH (NOLOCK) ON su.id =re.supplierid 
WHERE re.status = 2 --AND re.remissiondate BETWEEN @inidate AND @enddate

UNION ALL

SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	red.code AS [Numero de Comprobante],
	red.remissiondate AS [Fecha del Comprobante],
	ree.remissionnumber AS [NroRemision],
	ree.description AS [Detalle],
	red.code AS [Comprobante de Origen],
	su.code  + ' - ' + su.name as Tercero,
	ou.unitname AS Sede,
	CONCAT(w.code, ' - ', w.name) AS Bodega,
	'Resta' Operación,
	pt.name AS Tipo,
	ISNULL(atc.code, iss.code ) AS [Código Padre],
	ISNULL(atc.name, iss.suppliename ) AS [[Descripción Padre],
	ip.healthregistration AS [Registro Sanitario],
	ip.code AS [Código Producto],
	ip.name [Nombre Producto],
	bs.batchcode AS Lote,
	bs.expirationdate AS [Fecha de Vencimiento],
	reebs.quantity AS Cantidad,
	reed.unitvalue AS [Valor Unitario],
	(reebs.quantity * reed.unitvalue) AS [Valor Total],
	cc.name AS [Centro de Costo],
	'Devolucion Remision de Entrada' AS [Tipo de Operación],
	CAST(red.remissiondate AS DATE) 'FECHA BUSQUEDA',
	CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
FROM inventory.remissiondevolution AS red  WITH (NOLOCK)
INNER JOIN inventory.remissiondevolutiondetail AS redd WITH (NOLOCK) ON red.id = redd.remissiondevolutionid
INNER JOIN inventory.remissionentrancedetailbatchserial AS reebs WITH (NOLOCK) ON redd.remissionentrancedetailbatchserialid = reebs.id
INNER JOIN inventory.remissionentrancedetail AS reed WITH (NOLOCK) ON reebs.remissionentrancedetailid = reed.id
INNER JOIN Inventory.RemissionEntrance ree WITH (NOLOCK) ON reed.remissionentranceid = ree.id
INNER JOIN common.operatingunit AS ou WITH (NOLOCK) ON ree.operatingunitid = ou.id
-- ==========
INNER JOIN inventory.warehouse w WITH (NOLOCK) ON red.warehouseid = w.id
INNER JOIN inventory.inventoryproduct ip WITH (NOLOCK) ON redd.productid = ip.id
INNER JOIN inventory.producttype pt WITH (NOLOCK) ON ip.Producttypeid = pt.id
LEFT JOIN inventory.ATC AS ATC WITH (NOLOCK) ON ATC.id =IP.atcid 
LEFT JOIN inventory.inventorysupplie AS iss WITH (NOLOCK) ON iss.id = ip.supplieid
LEFT JOIN inventory.batchserial bs WITH (NOLOCK) ON reebs.batchserialid = bs.id
-- ==========
LEFT JOIN inventory.settinginventory si WITH (NOLOCK) ON red.operatingunitid = si.operatingunitid
LEFT JOIN inventory.productgroup pg WITH (NOLOCK) ON ip.productgroupid = pg.id
LEFT JOIN payroll.costcenter cc WITH (NOLOCK) ON cc.id = CASE si.associatecostcenter WHEN 2 THEN pg.costcenterid WHEN 3 THEN w.costcenterid END
LEFT JOIN common.supplier AS su WITH (NOLOCK) ON su.id = ree.supplierid 
WHERE red.status = 2 --AND red.remissiondate BETWEEN @inidate AND @enddate