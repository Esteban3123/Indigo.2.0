

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