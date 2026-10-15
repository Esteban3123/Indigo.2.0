

    /*******************************************************************************************************************
Nombre: [Report].[ViewInventarioEnConsignacion]
Tipo:Vista
Observacion:Inventario fisico en consignacion 
Profesional:
Fecha:
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Version 
Persona que modifico: Nilsson Miguel Galindo Lopez
Fecha:22-09-2023
Ovservaciones: Se coloca la codicion en la tabla Inventory.ConsignmentInventoryRemission para que solo tenga en cuenta remisiones de inventarios en consignación
			   con estado confirmado.
--------------------------------------
Version 3
Persona que modifico:
Observacion:
Fecha:
--***********************************************************************************************************************************/




CREATE view [Report].[ViewInventarioEnConsignacion]
as

--declare @FechaIni as date  = '2022-05-01',
--        @FechaFin as date ='2022-06-23',
--        @Nit  varchar(25)  ='900539662'
SELECT 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
CIR.Code,
th.Nit AS [Nit Proveedor],
th.Name AS [Proveedor],
w.Code AS [Codigo Almacen],
w.Name AS Almacen,
ip.Code AS [Codigo Producto],
ip.Name AS Producto,
b.BatchCode AS Lote,
cird.UnitValue AS Valor,
case ip.IVAId when '002' then cird.UnitValue*(0.19) else 0 end as IVA,
b.ExpirationDate AS Vence,
cirdb.Quantity AS Inicial,
cirdb.ReturnedQuantity AS Devolucion,
cirdb.OutstandingQuantity AS Disponible,
cirdb.UsedQuantity AS Usada,
cirdb.LegalizedQuantity AS Legalizada,
(cirdb.UsedQuantity - cirdb.LegalizedQuantity) AS ALegalizar,
 1 AS 'CANTIDAD', 
 CAST(cir.RemissionDate  AS DATE) AS [FECHA BUSQUEDA],
 YEAR(cir.RemissionDate ) AS 'AÑO FECHA BUSQUEDA',
 MONTH(cir.RemissionDate ) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(cir.RemissionDate ) 
	WHEN 1 THEN 'ENERO'
	WHEN 2 THEN 'FEBRERO'
	WHEN 3 THEN 'MARZO'
	WHEN 4 THEN 'ABRIL'
	WHEN 5 THEN 'MAYO'
	WHEN 6 THEN 'JUNIO'
	WHEN 7 THEN 'JULIO'
	WHEN 8 THEN 'AGOSTO'
	WHEN 9 THEN 'SEPTIEMBRE'
	WHEN 10 THEN 'OCTUBRE'
	WHEN 11 THEN 'NOVIEMBRE'
	WHEN 12 THEN 'DICIEMBRE'
  END AS 'MES NOMBRE FECHA BUSQUEDA', 
  FORMAT(DAY(cir.RemissionDate ), '00') AS 'DIA FECHA BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from Inventory.ConsignmentInventoryRemissionDetailBatchSerial cirdb
JOIN Inventory.ConsignmentInventoryRemissionDetail cird on cird.Id=cirdb.ConsignmentInventoryRemissionDetailId
join Inventory.ConsignmentInventoryRemission cir on cird.ConsignmentInventoryRemissionId=cir.Id AND CIR.Status=2
join Inventory.Warehouse w on cir.WarehouseId=w.Id
join Common.Supplier s on cir.SupplierId=s.Id
JOIN Common.ThirdParty th on s.IdThirdParty=th.Id
join Inventory.BatchSerial b on cirdb.BatchSerialId=b.Id
join Inventory.InventoryProduct ip on b.ProductId=ip.Id --left join
--GeneralLedger.GeneralLedgerIVA iva on ip.IVAId=iva.Id
where cirdb.OutstandingQuantity >= 0 AND cirdb.Quantity!=cirdb.LegalizedQuantity --and ip.Name like '%pregabalina%'