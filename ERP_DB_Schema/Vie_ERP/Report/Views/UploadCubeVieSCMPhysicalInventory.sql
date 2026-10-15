


CREATE view [Report].[UploadCubeVieSCMPhysicalInventory]
as
SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
	CASE PT.Class  WHEN 2 THEN 'MEDICAMENTOS' WHEN 3 THEN 'INSUMOS' WHEN 4 THEN 'OTROS' END 'TIPO PRODUCTO',--[TipoProducto],
	ip.Code AS 'CODIGO PRODUCTO',--[CodigoProducto],
	ip.Name as 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
	ISNULL(ATC.Code,is2.Code ) 'CODIGO PADRE',--[CodigoPadre],
	ISNULL(ATC.NAME,is2.SupplieName ) 'DESCRIPCION PADRE',--[DescripcionPadre],
	w.Code 'CODIGO ALAMCEN',--[CodigoAlmacen],
	w.Name 'ALMACEN',--[Almacen],
		ISNULL(bs.BatchCode, '') 'LOTE',-- [Lote],
		bs.ExpirationDate 'FECHA VENCIMIENTO',--[FechaVencimiento],
		ip.HealthRegistration 'REGISTRO INVIMA',--[RegistroINVIMA],
		IP.ExpirationDate 'FECHA VENCIMIENTO REGISTRO',--[FechaVencimientoRegistro],
		ISNULL(pf.Name, '') 'FORMA FARMACEUTICA',--[FormaFarmaceutica],
		CONCAT(m.Code, ' - ', m.Name) 'FABRICANTE',--[Fabricante],
		phy.Quantity 'CANTIDAD',--[Cantidad], 
		ip.ProductCost 'COSTO',--[Costo],
		ip.FinalProductCost 'ULTIMO COSTO',--[UltimoCosto]
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM Inventory.PhysicalInventory phy WITH (NOLOCK)
JOIN Inventory.Warehouse w WITH (NOLOCK) ON phy.WarehouseId = w.Id
JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON phy.ProductId = ip.Id
JOIN Inventory.ProductType AS PT WITH (NOLOCK) ON ip.ProductTypeId =PT.Id 
LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
LEFT JOIN Inventory.ATC atc WITH (NOLOCK) ON ip.ATCId = atc.Id
LEFT JOIN Inventory.InventorySupplie is2 WITH (NOLOCK) ON is2.id=ip.SupplieId 
LEFT JOIN Inventory.PharmaceuticalForm pf WITH (NOLOCK) ON atc.PharmaceuticalFormId = pf.Id
LEFT JOIN Inventory.Manufacturer m WITH (NOLOCK) ON ip.ManufacturerId = m.Id
WHERE phy.Quantity > 0