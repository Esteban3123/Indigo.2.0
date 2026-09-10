

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista destinada a la carga de un cubo analítico (OLAP/BI) con el inventario físico real de productos en bodega. Aplanada para consumo de reporting, expone por cada producto con existencia positiva: su clasificación (medicamentos, insumos u otros), código ATC o de insumo, almacén, lote, fecha de vencimiento, registro INVIMA, forma farmacéutica, fabricante y costos. Identifica la empresa mediante el nombre de la base de datos y registra la marca temporal de última actualización en zona horaria de Pakistán.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el inventario físico vigente por bodega, producto y lote para alimentar un cubo de reportes con clasificación, costos, vencimientos y datos regulatorios.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de productos con ProductTypeId válido en Inventory.ProductType (clases 2, 3 o 4); Cada conteo físico debe estar asociado a una bodega y producto existentes (joins INNER con Warehouse e InventoryProduct)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Los productos se clasifican exclusivamente como MEDICAMENTOS, INSUMOS u OTROS según ProductType.Class (2/3/4); El identificador de compañía expuesto siempre corresponde al nombre de la base de datos actual truncado a 9 caracteres (DB_NAME()); La marca de tiempo de actualización (ULT_ACTUAL) siempre se calcula convirtiendo GETDATE() a la zona horaria ''Pakistan Standard Time''; Un producto pertenece a la jerarquía ATC o a InventorySupplie, priorizando ATC cuando ambos existen; La forma farmacéutica se obtiene a través de la relación ATC → PharmaceuticalForm, no directamente del producto; Lote y forma farmacéutica se exponen como cadena vacía cuando no existen (ISNULL ... ''''); Solo se consideran existencias positivas (Quantity > 0)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario físico; Bodega/Almacén; Medicamentos; Insumos; Lote; Fecha de vencimiento; Registro INVIMA; Forma farmacéutica; Clasificación ATC; Fabricante; Costo de producto; Último costo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMPhysicalInventory: Retorna solo registros de inventario físico con Quantity > 0 (WHERE phy.Quantity > 0), excluyendo existencias en cero o negativas', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si PT.Class = 2 → Tipo de producto se reporta como ''MEDICAMENTOS''; si PT.Class = 3 → Tipo de producto se reporta como ''INSUMOS''; si PT.Class = 4 → Tipo de producto se reporta como ''OTROS'' else NULL para clases distintas de 2, 3 o 4; si ATC.Code IS NOT NULL → El código y descripción ''padre'' se toma de Inventory.ATC else Se toma de Inventory.InventorySupplie (ISNULL sobre ATC primero, luego InventorySupplie)', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.InventoryProduct; Inventory.ProductType; Inventory.BatchSerial; Inventory.ATC; Inventory.InventorySupplie; Inventory.PharmaceuticalForm; Inventory.Manufacturer', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMPhysicalInventory';
GO
