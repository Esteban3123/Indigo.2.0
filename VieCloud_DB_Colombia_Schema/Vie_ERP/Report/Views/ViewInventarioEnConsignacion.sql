

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
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte orientada a consulta y seguimiento del inventario físico en consignación. Consolida, por lote y serial, las cantidades iniciales, devueltas, disponibles, usadas y legalizadas de cada producto remisionado, filtrando únicamente remisiones con estado confirmado (Status=2) y con cantidades no totalmente legalizadas. Calcula el IVA del 19% según el código de IVA del producto, e incluye dimensiones de fecha (año, mes, día) para análisis temporal en herramientas de reporting.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el inventario físico en consignación con saldos por lote/serial (inicial, devuelto, disponible, usado, legalizado y por legalizar) junto con datos de proveedor, almacén, producto y la fecha de remisión.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las remisiones de inventario en consignación deben existir con Status=2 (confirmado) para ser incluidas.; Cada detalle de remisión debe tener su lote/serial asociado en ConsignmentInventoryRemissionDetailBatchSerial.; El producto debe tener IVAId definido para calcular IVA (se compara contra ''002'').', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se incluyen remisiones en consignación con Status=2 (confirmado).; Se excluyen lotes totalmente legalizados (Quantity = LegalizedQuantity).; Se excluyen lotes con OutstandingQuantity negativa.; La cantidad por fila siempre es 1 (columna fija ''CANTIDAD'').; El IVA solo aplica tarifa del 19% cuando el producto tiene IVAId=''002''; cualquier otro código resulta en IVA cero.; ID_COMPANY se obtiene del nombre de la base de datos actual truncado a 9 caracteres.; ULT_ACTUAL se calcula con GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; ALegalizar se calcula como UsedQuantity - LegalizedQuantity.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Inventario en consignación; Remisión de inventario; Proveedor; Almacén/Bodega; Lote y serial; Fecha de vencimiento; IVA del producto; Cantidad disponible; Cantidad legalizada; Cantidad por legalizar; Devolución de mercancía', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.ViewInventarioEnConsignacion: Devuelve filas solo cuando cirdb.OutstandingQuantity >= 0 AND cirdb.Quantity != cirdb.LegalizedQuantity, es decir, lotes con saldo no negativo y aún no totalmente legalizados.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ip.IVAId = ''002'' → Calcula IVA como UnitValue * 0.19 (19%). else IVA se reporta como 0.; si MONTH(cir.RemissionDate) entre 1 y 12 → Traduce el mes numérico al nombre del mes en español (ENERO..DICIEMBRE) para la columna ''MES NOMBRE FECHA BUSQUEDA''.; si cir.Status = 2 → Solo considera remisiones de inventario en consignación con estado confirmado (filtro en el JOIN según comentario de versión 22-09-2023).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ConsignmentInventoryRemissionDetailBatchSerial; Inventory.ConsignmentInventoryRemissionDetail; Inventory.ConsignmentInventoryRemission; Inventory.Warehouse; Common.Supplier; Common.ThirdParty; Inventory.BatchSerial; Inventory.InventoryProduct', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioEnConsignacion';
GO
