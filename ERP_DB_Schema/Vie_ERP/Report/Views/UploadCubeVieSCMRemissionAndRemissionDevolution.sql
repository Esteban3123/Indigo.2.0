
--CREATE PROCEDURE [Inventory].[ReporteRemisonesyDevolucionDeRemisiones]
--	@OperatingUnitCode VARCHAR(20),
--DECLARE	@DateStart DATETIME='2024-05-01';
--DECLARE	@DateEnd DATETIME='2024-05-31';
--AS
CREATE view [Report].[UploadCubeVieSCMRemissionAndRemissionDevolution] as

	WITH entrances AS 
	(

		SELECT
			evd.remissionentrancedetailbatchserialid,
			STRING_AGG(ev.code, ' - ') AS [entrance],
			SUM(evd.quantity) AS quantity
		FROM inventory.entrancevoucher ev
		JOIN inventory.entrancevoucherdetail evd ON ev.id = evd.entrancevoucherid
		WHERE ev.status IN (2) AND evd.remissionentrancedetailbatchserialid IS NOT NULL --AND CAST( ev.confirmationdate  AS DATE)  > @DateStart
		GROUP BY evd.remissionentrancedetailbatchserialid

	), devolutions AS
	(

		SELECT
			rdevd.remissionentrancedetailbatchserialid,
			STRING_AGG(RTRIM(rdev.code), ' - ') AS [devolution]
		FROM inventory.remissiondevolution AS rdev 
		INNER JOIN inventory.remissiondevolutiondetail AS rdevd ON rdev.id = rdevd.remissiondevolutionid AND rdevd.remissionentrancedetailbatchserialid IS NOT NULL
		WHERE  rdev.status IN (2) --AND CAST(rdev.confirmationdate AS DATE) > @DateStart
		GROUP BY rdevd.remissionentrancedetailbatchserialid

	)



	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ev.DocumentNumber 'NRO DOCUMENTO',--[NroDocumento],
		ev.DocumentDate 'FECHA DOCUMENTO',--[FechaDocumento],
		ev.remissionnumber AS 'NRO REMISION',--[NroRemision],
		ev.description AS 'DETALLE',--[Detalle],
		NULL 'COMPROBANTE ORIGEN',--[ComprobanteOrigen],
		ev.Tercero 'TERCERO',--[Tercero],
		ev.UnitName 'SEDE',--[Sede],
		ev.Warehouse 'BODEGA',--[Bodega],
		ev.Operation 'OPERACION',--[Operacion],
		ev.ProductType 'TIPO',--[Tipo],
		ev.CODIGO_PADRE 'CODIGO PADRE',--[CodigoPadre],
		ev.NOMBRE_PADRE 'DESCRIPCION PADRE',--[DescripcionPadre],
		EV.Code 'CODIGO PRODUCTO',--[CodigoProducto],
		ev.ProductName 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
		ev.HealthRegistration 'REGISTRO SANITARIO',--[RegistroSanitario],
		ev.BatchCode 'LOTE',--[Lote],
		ev.ExpirationDate 'FECHA VENCIMIENTO',--[FechaVencimiento],
		ev.Quantity 'CANTIDAD',--[Cantidad],
		ev.UnitValue 'VALOR UNITARIO',--[ValorUnitario],
		(ev.Quantity * ev.UnitValue) 'VALOR TOTAL',--[ValorTotal],
		ev.CostCenter 'CENTRO COSTO',--[CentroCostos], 
		'REMISION DE ENTRADA' 'TIPO OPERACION',--[TipoOperacion],
		ev.quantity - ISNULL(ev.cantidadlegalizar, 0) AS 'CANTIDAD LEGALIZAR',--[CantidadLegalizar],
		ev.entrance AS 'COMPROBANTE ENTRADA',--[ComprobanteEntrada],
		ev.devolution AS 'DEVOLUCION REMISION',--[DevolucionRemision]
		CAST(ev.DocumentDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
	FROM
	(
		SELECT	
			re.RemissionDate DocumentDate,re.Code DocumentNumber,
			re.remissionnumber,
			re.description,
			ou.UnitName,CONCAT(w.Code, ' - ', w.Name) Warehouse,'Suma' Operation,
			SU.Code  + ' - ' + SU.Name as Tercero,
			ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
			pt.Name ProductType,
			ip.HealthRegistration,ip.Code ,ip.Name ProductName,bs.BatchCode,bs.ExpirationDate,redbs.Quantity,red.UnitValue,cc.Name CostCenter,
			ent.quantity AS cantidadLegalizar,
			ent.entrance, 
			dev.devolution 
		FROM Common.OperatingUnit ou WITH (NOLOCK)
		JOIN Inventory.RemissionEntrance re WITH (NOLOCK) ON ou.Id = re.OperatingUnitId
		JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON re.Id = red.RemissionEntranceId
		JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON red.Id = redbs.RemissionEntranceDetailId
		---------------------------------------------------------------------------------------------------------------
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON re.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON red.ProductId = ip.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
		LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
		LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
		LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON redbs.BatchSerialId = bs.Id
		---------------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.SettingInventory si WITH (NOLOCK) ON re.OperatingUnitId = si.OperatingUnitId
		LEFT JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = CASE si.AssociateCostCenter WHEN 2 THEN pg.CostCenterId WHEN 3 THEN w.CostCenterId END
		LEFT JOIN Common.Supplier AS SU WITH (NOLOCK) ON SU.Id =re.SupplierId 
		LEFT JOIN entrances AS ent ON redbs.id = ent.remissionentrancedetailbatchserialid

		LEFT JOIN devolutions AS dev ON redbs.id = dev.remissionentrancedetailbatchserialid
 		WHERE re.Status = 2 --AND CAST(re.RemissionDate AS DATE) BETWEEN @DateStart AND @DateEnd
	) AS ev


	UNION 


	SELECT	CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		ev.DocumentNumber [NUMERO COMPROBANTE],ev.DocumentDate [FECHA DOCUMENTO],
		ev.remissionnumber,
		ev.description,
		ev.DocumentoDevuleto  [COMPROBANTE ORIGEN],ev.Tercero [TERCERO],ev.UnitName [SEDE],ev.Warehouse [BODEGA],ev.Operation [OPERACION],ev.ProductType [TIPO],
		ev.CODIGO_PADRE [CODIGO PADRE],ev.NOMBRE_PADRE [NOMBRE PADRE], EV.Code [CODIGO PRODUCTO],
		ev.ProductName [PRODUCTO],ev.HealthRegistration [REGISTRO SANITARIO], ev.BatchCode [LOTE],ev.ExpirationDate [FECHA VENCIMIENTO],ev.Quantity [CANTIDAD],
		ev.UnitValue [VALOR UNITARIO],(ev.Quantity * ev.UnitValue) [VALOR TOTAL],ev.CostCenter [CENTRO DE COSTO],'DEVOLUCION REMISION DE ENTRADA' [TIPO OPERACION], 
		NULL AS [CantidadLegalizar],
		NULL AS [ComprobanteEntrada],
		NULL AS [DevolucionRemision],
		CAST(ev.DocumentDate AS DATE) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUA
		--INTO INDIGODWH.PBI.STG_DEVOLUCION_REMISIONES_DE_ENTRADA
	FROM
	(
		SELECT	rdev.RemissionDate DocumentDate,
		re.remissionnumber,
		re.description, 
		rdev.Code DocumentNumber,re.Code DocumentoDevuleto,ou.UnitName,CONCAT(w.Code, ' - ', w.Name) Warehouse,'Resta' Operation,
		SU.Code  + ' - ' + SU.Name as Tercero,
		ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
		pt.Name ProductType,ip.HealthRegistration,ip.Code ,ip.Name ProductName,bs.BatchCode,bs.ExpirationDate,rdevd.Quantity,red.UnitValue,cc.Name CostCenter
		FROM Inventory.RemissionDevolution rdev WITH (NOLOCK)
		JOIN Inventory.RemissionDevolutionDetail rdevd WITH (NOLOCK) ON rdev.Id = rdevd.RemissionDevolutionId
		JOIN Inventory.RemissionEntranceDetailBatchSerial redbs WITH (NOLOCK) ON rdevd.RemissionEntranceDetailBatchSerialId = redbs.Id
		JOIN Inventory.RemissionEntranceDetail red WITH (NOLOCK) ON redbs.RemissionEntranceDetailId = red.Id
		JOIN Inventory.RemissionEntrance re WITH (NOLOCK) ON red.RemissionEntranceId = re.Id
		JOIN Common.OperatingUnit ou WITH (NOLOCK) ON re.OperatingUnitId = ou.Id
		---------------------------------------------------------------------------------------------------------------
		JOIN Inventory.Warehouse w WITH (NOLOCK) ON re.WarehouseId = w.Id
		JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON red.ProductId = ip.Id
		JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
		LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
		LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
		LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON redbs.BatchSerialId = bs.Id
		---------------------------------------------------------------------------------------------------------------
		LEFT JOIN Inventory.SettingInventory si WITH (NOLOCK) ON re.OperatingUnitId = si.OperatingUnitId
		LEFT JOIN Inventory.ProductGroup pg WITH (NOLOCK) ON ip.ProductGroupId = pg.Id
		LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = CASE si.AssociateCostCenter WHEN 2 THEN pg.CostCenterId	WHEN 3 THEN w.CostCenterId END
		LEFT JOIN Common.Supplier AS SU WITH (NOLOCK) ON SU.Id =re.SupplierId 
		WHERE rdev.Status = 2 --AND CAST(rdev.RemissionDate AS DATE) BETWEEN @DateStart AND @DateEnd
	) AS ev