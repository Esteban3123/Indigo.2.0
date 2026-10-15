

--CREATE PROCEDURE [Inventory].[SP_ORDENES_TRASLADO_DEVOLUCION_ORDENES_TRASLADO]
--	@OperatingUnitCode VARCHAR(20),
--DECLARE	@DateStart DATETIME='2024-05-01';
--DECLARE	@DateEnd DATETIME='2024-05-16';
--AS
CREATE view [Report].[UploadCubeVieSCMTransferOrdersAndReturnTransferOrders] as

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		tro.Code AS 'NRO DOCUMENTO',--[NroDocumento],
		tro.DocumentDate 'FECHA DOCUMENTO',--[FechaDocumento],
		tro.ConfirmationDate 'FECHA CONFIRMACION',--[FechaConfirmacion], 
		tro.Code 'DOCUMENTO ORIGEN',--[Documento Origen],
		ou.UnitName AS 'SEDE',--[Sede],
		CASE OrderType WHEN 1 THEN 'Traslado' WHEN 2 THEN 'Consumo' when 3 then 'Traslado en Transito' end 'TIPO ORDEN',--[TipoOrden],
		case DispatchTo when 1 then 'Almacen' when 2 then 'Unidad Funcional' end 'DESPACHO A',--[Despachado a],
		CONCAT(w.Code, ' - ', w.Name) 'ALMACEN ORIGEN',--[AlmacenOrigen],
		CONCAT(w2.Code, ' - ', w2.Name) 'ALAMCEN DESTINO',--[AlmacenDestino],
		FU.CODE + ' - ' + FU.Name 'UNIDAD FUNCIONAL DESTINO',--[UnidadFuncionalDestino],
		AC.Code + ' - ' + ac.Name 'CONCEPTO MOVIMIENTO',--[ConceptoMovimiento],
		TP.Nit + ' - ' + TP.Name 'TERCERO',--[Tercero],
		'Resta' 'OPERACION',--[Operacion],
		CASE TRO.Status WHEN 1 THEN 'Registrado' WHEN 2 THEN 'Confirmado' when 3 then 'Anulado' end as 'ESTADO',--[Estado],
		ISNULL(ATC.Code,ISS.Code ) AS 'CODIGO PADRE',--[CodigoPadre],
		ISNULL(ATC.Name,ISS.SupplieName ) AS 'DESCRIPCION PADRE',--[DescripcionPadre],
		pt.Name 'TIPO PRODUCTO',--[TipoProducto],
		ip.Code 'CODIGO PRODUCTO',--[CodigoProducto] ,
		ip.Name 'DESCRIPCION PRODUCTO',--[DescripcionProducto],
		ip.HealthRegistration 'REGISTRO SANITARIO',--[RegistroSanitario],
		bs.BatchCode 'LOTE',--[Lote],
		bs.ExpirationDate 'FECHA VENCIMIENTO',--[FechaVencimiento],
		trodbs.Quantity 'CANTIDAD',--[Cantidad],
		trod.Value 'VALOR UNITARIO',--[ValorUnitario],
		cc.Name 'CENTRO COSTO',--[CentroCosto],
		IVA.Name AS 'IVA',--[IVA], 
		'ORDENES DE TRASLADO' 'TIPO',--[Tipo]
		cast(tro.ConfirmationDate as date) 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM Common.OperatingUnit ou WITH (NOLOCK)
	JOIN Inventory.TransferOrder tro WITH (NOLOCK) ON ou.Id = tro.OperatingUnitId
	JOIN Inventory.TransferOrderDetail trod WITH (NOLOCK) ON tro.Id = trod.TransferOrderId
	JOIN Inventory.TransferOrderDetailBatchSerial trodbs WITH (NOLOCK) ON trod.Id = trodbs.TransferOrderDetailId
	JOIN Inventory.Warehouse w WITH (NOLOCK) ON tro.SourceWarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON trod.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.Warehouse w2 WITH (NOLOCK) ON tro.TargetWarehouseId = w2.Id
	LEFT JOIN Payroll .FunctionalUnit AS FU WITH (NOLOCK) ON tro .TargetFunctionalUnitId =FU.Id 
	LEFT JOIN Inventory.AdjustmentConcept ac WITH (NOLOCK) ON tro.AdjustmentConceptId = ac.Id
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON ac.CostCenterId = cc.Id
	LEFT JOIN Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =tro.ThirdPartyId 
	LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
	LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON trodbs.PhysicalInventoryId = phy.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
	LEFT JOIN GeneralLedger.GeneralLedgerIVA AS IVA WITH (NOLOCK) ON ip.IVAId =IVA.Id 
	where YEAR(tro.ConfirmationDate)>=2022
	--cast(tro.ConfirmationDate as date) BETWEEN @DateStart AND @DateEnd

	union all

	SELECT CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
		trodev.Code  'NRO DOCUMENTO',--[DOCUMENTO],
		trodev.DocumentDate 'FECHA DOCUMENTO',--[FECHA DOCUMENTO],
		trodev.ConfirmationDate 'FECHA CONFIRMACION',--[FECHA CONFIRMACIO],
		tro.Code 'DOCUMENTO ORIGEN',--[DOCUMENTO ORIGEN] ,
		ou.UnitName 'SEDE',--[SEDE],
		'' 'TIPO ORDEN',--[TIPO ORDEN],
		'' 'DESPACHO A',--[DESPACHADO A],
		CONCAT(w.Code, ' - ', w.Name) 'ALMACEN ORIGEN',--[ALMACEN ORIGEN],
		'' 'ALAMCEN DESTINO',--[ALMACEN DESTINO],
		'' 'UNIDAD FUNCIONAL DESTINO',--[UNIDAD FUNCIONAL DESTINO] ,
		'' 'CONCEPTO DE MOVIMIENTO',--[CONCEPTO DE MOVIMIENTO],
		TP.Nit + ' - ' + TP.Name 'TERCERO',--[TERCERO],
		'Suma' 'OPERACION',--[OPERACION],
		CASE trodev.Status WHEN 1 THEN 'Registrado' when 2 then 'Confirmado' when 3 then 'Anulado' end 'ESTADO',--[ESTADO],
		ISNULL(ATC.Code,ISS.Code ) AS 'CODIGO PADRE',--[CODIGO PADRE],
		ISNULL(ATC.Name,ISS.SupplieName ) AS 'NOMBRE PADRE',--[NOMBRE PADRE],
		pt.Name 'TIPO PRODUCTO',--[TIPO PRODUCTO],
		IP.Code 'CODIGO PRODUCTO',--[CODIGO PRODUCTO],
		ip.Name 'NOMBRE PRODUCTO',--[NOMBRE PRODUCTO], 
		ip.HealthRegistration 'REGISTRO SANITARIO',--[REGISTRO SANITARIO], 
		bs.BatchCode 'LOTE',--[LOTE],
		bs.ExpirationDate 'FECHA VENCIMIENTO',--[FECHA VENCIMIENTO],
		trodevd.Quantity 'CANTIDAD',--[CANTIDAD],
		trod.Value 'VALOR UNITARIO',--[VALOR UNITARIO],
		cc.Name 'CENTRO DE COSTO',--[CENTRO DE COSTO],
		IVA.Name AS 'IVA',--[IVA], 
		'DEVOLUCION DE ORDENES DE TRASLADO' 'TIPO',--[TIPO]
		cast(trodev.ConfirmationDate as date) AS 'FECHA BUSQUEDA',
		CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL

	FROM Inventory.TransferOrderDevolution trodev WITH (NOLOCK)
	JOIN Inventory.TransferOrderDevolutionDetail trodevd WITH (NOLOCK) ON trodev.Id = trodevd.TransferOrderDevolutionId
	JOIN Inventory.TransferOrderDetailBatchSerial trodbs WITH (NOLOCK) ON trodevd.TransferOrderDetailBatchSerialId = trodbs.Id
	JOIN Inventory.TransferOrderDetail trod WITH (NOLOCK) ON trodbs.TransferOrderDetailId = trod.Id
	JOIN Inventory.TransferOrder tro WITH (NOLOCK) ON trod.TransferOrderId = tro.Id
	JOIN Common.OperatingUnit ou WITH (NOLOCK) ON tro.OperatingUnitId = ou.Id
	---------------------------------------------------------------------------------------------------------------
	JOIN Inventory.AdjustmentConcept ac WITH (NOLOCK) ON tro.AdjustmentConceptId = ac.Id
	JOIN Payroll.CostCenter cc WITH (NOLOCK) ON ac.CostCenterId = cc.Id
	---------------------------------------------------------------------------------------------------------------
	JOIN Inventory.Warehouse w WITH (NOLOCK) ON tro.SourceWarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON trod.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
	LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON trodbs.PhysicalInventoryId = phy.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
	LEFT JOIN Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =tro.ThirdPartyId
	LEFT JOIN GeneralLedger.GeneralLedgerIVA AS IVA WITH (NOLOCK) ON ip.IVAId =IVA.Id 
	where YEAR(trodev.ConfirmationDate)>=2022
	--cast(trodev.ConfirmationDate as date) BETWEEN @DateStart AND @DateEnd