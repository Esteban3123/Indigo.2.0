
--  CREATE PROCEDURE [Inventory].[SP_MEDICAMENTOS_PROXIMOS_A_VENCER]
--AS
CREATE view [Report].[UploadCubeVieSCMMedicationsExpiring] as

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		RTRIM(WH.Code) + ' - ' + RTRIM(WH.Name) 'ALMACEN',--[Almacen],
		PRO.Code 'CODIGO PRODUCTO',--[CodigoProducto],
		PRO.CodeCUM 'CUM',--[CUM],
		PRO.Name 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
		CASE BS.Type WHEN 1 THEN 'LOTE' WHEN 2 THEN 'SERIAL' ELSE 'NINGUNO' END 'TIPO',--[Tipo],
		BS.BatchCode AS 'LOTE SERIAL',--[LoteSerial],
		BS.ExpirationDate AS 'FECHA VENCIMIENTO',--[FechaVencimiento],
		PT.Name AS 'TIPOPRODUCTO',--[TipoProducto],
		MF.Code + '- ' +  MF.Name AS 'PROVEEDOR',--[Proveedor],
		BG.Name AS 'GRUPO FACTURACION',--[GrupoFacturacion],
		CAST(BS.ExpirationDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
		--DATEADD(MM, -6, getdate()) AS [Mes]
		--INTO INDIGODWH.INVENTORY.STG_PROXIMOS_A_VENCER
	FROM Inventory.BatchSerial AS BS WITH (NOLOCK)
	JOIN Inventory .InventoryProduct AS PRO WITH (NOLOCK) ON BS.ProductId =PRO.Id  
	JOIN Inventory .PhysicalInventory AS FIS WITH (NOLOCK) ON FIS.BatchSerialId =BS.Id AND FIS.ProductId =BS.ProductId 
	JOIN Inventory .Warehouse AS WH WITH (NOLOCK) ON WH.Id =FIS.WarehouseId 
	JOIN Inventory .ProductType AS PT ON PRO.ProductTypeId =PT.Id 
	LEFT JOIN Inventory .Manufacturer AS MF ON PRO.ManufacturerId =MF.Id 
	LEFT JOIN Billing .BillingGroup BG ON PRO.BillingGroupId =BG.ID
	WHERE  BS.ExpirationDate >=  getdate() AND BS.ExpirationDate <= DATEADD(MM, 6, getdate())