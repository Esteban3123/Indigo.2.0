
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

GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los lotes/seriales de productos de inventario cuya fecha de vencimiento está dentro de los próximos 6 meses, para alimentación de cubo de reportes.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Existencia de inventario físico (PhysicalInventory) asociado al lote/serial y al producto; El producto debe tener tipo de producto (ProductType) registrado; BatchSerial.ExpirationDate debe estar definida', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen productos con existencia física en algún almacén (JOIN PhysicalInventory y Warehouse); Se excluyen productos ya vencidos (ExpirationDate < hoy) y los que vencen en más de 6 meses; El proveedor (Manufacturer) y grupo de facturación (BillingGroup) son opcionales (LEFT JOIN); La fecha de última actualización se calcula con la zona horaria ''Pakistan Standard Time''; El identificador de compañía se obtiene del nombre de la base de datos actual truncado a 9 caracteres', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Lote; Serial; Fecha de vencimiento; Almacén; Producto; Tipo de producto; Proveedor/Fabricante; Grupo de facturación; Inventario físico; Medicamentos próximos a vencer', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.UploadCubeVieSCMMedicationsExpiring: Devuelve solo lotes/seriales con ExpirationDate entre la fecha actual y 6 meses posteriores (BS.ExpirationDate >= getdate() AND BS.ExpirationDate <= DATEADD(MM, 6, getdate()))', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si BatchSerial.Type = 1 → Clasifica el registro como ''LOTE''; si BatchSerial.Type = 2 → Clasifica el registro como ''SERIAL'' else Clasifica como ''NINGUNO''', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.BatchSerial; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.Warehouse; Inventory.ProductType; Inventory.Manufacturer; Billing.BillingGroup', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'UploadCubeVieSCMMedicationsExpiring';
GO
