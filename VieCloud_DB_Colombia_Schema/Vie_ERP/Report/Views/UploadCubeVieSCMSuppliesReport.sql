

--CREATE PROCEDURE [Inventory].[ReporteInsumos]
--as

CREATE view [Report].[UploadCubeVieSCMSuppliesReport] as

	WITH movimientos AS 
	(
		SELECT DISTINCT productid FROM inventory.physicalinventory
	)



	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ISU.Code 'CODIGO INSUMO',--CodigoInsumo, 
		ISU.SupplieName AS 'DESCRIPCION INSUMO',--DescripcionInsumo, 
		P.Code as 'CODIGO PRODUCTO',--CodigoProducto, 
		p.name as 'NOMBRE PRODUCTO',--NombreProducto, 
		P.CodeAlternative as 'CODIGO ALTERNATIVO',--CodigoAlternativo, 
		P.CodeAlternativeTwo AS 'CODIGO ALTERNATIVO2',--CodigoAlternativo2, 
		CASE p.status WHEN 1 THEN 'Activo' WHEN 0 THEN 'Inactivo' END AS 'ESTADO PRODUCTO',--[EstadoProducto],
		PG.Code AS 'CODIGO GRUPO',--CodigoGrupo,
		PG.Name as 'NOMBRE GRUPO',--NombreGrupo, 
		PSG.Code as 'CODIGO SUBGRUPO',--CodigoSubGrupo,
		PSG.Name as 'NOMBRE SUBGRUPO',-- NombreSubGrupo, 
		PU.Code as 'CODIGO UNIDAD EMPAQUE',--CodigoUnidadEmpaque,
		PU.Name as 'NOMBRE UNIDAD EMPAQUE',--NombreUnidadEmpaque, 
		M.Code AS 'CODIGO PROVEEDOR',--CodigoProveedor,
		M.Name AS 'PROVEEDOR',--Proveedor, 
		IVA.Code as 'CODIGO IVA',--CodigoIva,
		IVA.Name as 'NOMBRE IVA',--NombreIva, 
		CASE HandlesHealthRegistration 
			WHEN 1 THEN 'Si' else 'No' end as 'MANEJO REGISTRO',--ManejaRegistro, 
		HealthRegistration as 'REGISTRO',--Registro, 
		ExpirationDate as 'FECHA VENCIMIENTO',--FechaVencimiento, 
		BG.Code AS 'CODIGO GRUPO FACTURACION',--CodigoGrupoFacacturacion,
		BG.Name as 'NOMBRE GRUPO FACTURACION',--NombreGrupoFacturacion, 
		case ProductControl when 1 then 'Si' else 'No' end 'INSUMO CONTROL',--InsumoControl, 
		case P.POSProduct when 1 then 'Si' else 'no' end as 'PRODUCTO POS',--ProductoPos, 
		ProductCost 'COSTO PROMEDIO',--CostoPromedio, 
		FinalProductCost 'ULTIMO COSTO',--UltimoCosto, 
		SellingPrice as 'PRECIO VENTA',--PrecioVenta, 
		ControlCostPercentage as '% CONTROL',--PorcentajeControl, 
		R.Code AS 'CODIGO RIESGO',--CodigoRiesgo,
		R.Name as 'NOMBRE RIESGO',--NombreRiesgo, 
		SerialNumber AS 'SERIAL',--Serial, 
		case P.Consumption when 1 then 'Si' else 'No' end as 'PRODUCTO CONSUMO',--ProductoConsumo, 
		case ISU.OsteosynthesisMaterial when 1 then 'Si' else 'No' end as 'MOS',--MOS,
		CASE WHEN mov.productid IS NULL THEN 'No' ELSE 'Si' END AS 'TIENE ROTACION',--[TieneRotacion],
		cast(GETDATE() as date) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
	FROM Inventory.InventoryProduct AS P LEFT JOIN 
	Inventory.InventorySupplie as ISU on ISU.Id =P.SupplieId LEFT OUTER JOIN
	Inventory.ProductGroup AS PG ON P.ProductGroupId =PG.Id LEFT OUTER JOIN
	Inventory.ProductSubGroup  AS PSG ON P.ProductSubGroupId =PSG.Id LEFT OUTER JOIN
	Inventory.PackagingUnit AS PU ON P.PackagingUnitId =PU.Id LEFT OUTER JOIN
	Inventory.Manufacturer AS M ON P.ManufacturerId =M.Id LEFT OUTER JOIN
	GeneralLedger.GeneralLedgerIVA AS IVA ON P.IVAId =IVA.ID LEFT OUTER JOIN
	Billing.BillingGroup AS BG ON P.BillingGroupId =BG.Id LEFT OUTER JOIN
	Inventory.InventoryRiskLevel AS R ON P.InventoryRiskLevelId =R.Id 
	LEFT JOIN movimientos AS mov ON p.id = mov.productid
	WHERE ProductTypeId not in(1) --and P.Status =1

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el catálogo de productos de inventario (excluyendo un tipo específico) con sus atributos comerciales, contables, de riesgo e indicador de rotación, para alimentar un cubo/reporte de insumos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas maestras de producto, insumo, grupos, subgrupos, unidad de empaque, fabricante, IVA, grupo de facturación y nivel de riesgo deben existir y estar relacionadas por sus IDs.; Debe existir la tabla inventory.physicalinventory para evaluar la rotación del producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Se excluyen siempre los productos con ProductTypeId = 1.; La rotación se determina únicamente por la existencia (DISTINCT) del productid en physicalinventory, sin considerar fechas ni cantidades.; La fecha de búsqueda se fija a la fecha actual del servidor y la última actualización se ajusta a la zona horaria ''Pakistan Standard Time''.; El identificador de compañía se toma del nombre de la base de datos actual, truncado a 9 caracteres.; Las relaciones con catálogos (insumo, grupo, subgrupo, empaque, fabricante, IVA, facturación, riesgo) son opcionales (LEFT JOIN), por lo que un producto se reporta aun sin estos atributos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Insumo; Producto de inventario; Grupo y subgrupo de producto; Unidad de empaque; Proveedor/Fabricante; IVA; Registro sanitario; Fecha de vencimiento; Grupo de facturación; Insumo de control; Producto POS; Costo promedio; Último costo; Precio de venta; Porcentaje de control; Nivel de riesgo; Material de osteosíntesis (MOS); Rotación de inventario; Producto de consumo', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMSuppliesReport: Devuelve un registro por cada producto cuyo ProductTypeId no sea 1, enriquecido con datos de insumo, grupo, subgrupo, empaque, fabricante, IVA, grupo de facturación y nivel de riesgo.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ProductTypeId NOT IN (1) → El producto se incluye en el resultado else El producto se excluye del reporte; si p.status = 1 → Se reporta como ''Activo'' else Si status = 0 se reporta como ''Inactivo''; si HandlesHealthRegistration = 1 → ''MANEJO REGISTRO'' = ''Si'' else ''MANEJO REGISTRO'' = ''No''; si ProductControl = 1 → ''INSUMO CONTROL'' = ''Si'' else ''INSUMO CONTROL'' = ''No''; si P.POSProduct = 1 → ''PRODUCTO POS'' = ''Si'' else ''PRODUCTO POS'' = ''no''; si P.Consumption = 1 → ''PRODUCTO CONSUMO'' = ''Si'' else ''PRODUCTO CONSUMO'' = ''No''; si ISU.OsteosynthesisMaterial = 1 → ''MOS'' = ''Si'' else ''MOS'' = ''No''; si Existe registro del producto en inventory.physicalinventory → ''TIENE ROTACION'' = ''Si'' else ''TIENE ROTACION'' = ''No''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'inventory.physicalinventory; Inventory.InventoryProduct; Inventory.InventorySupplie; Inventory.ProductGroup; Inventory.ProductSubGroup; Inventory.PackagingUnit; Inventory.Manufacturer; GeneralLedger.GeneralLedgerIVA; Billing.BillingGroup; Inventory.InventoryRiskLevel', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMSuppliesReport';
GO
