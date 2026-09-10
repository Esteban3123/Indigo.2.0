

    /*******************************************************************************************************************
Nombre: [Report].ViewInventarioSolicitudVsOrdenTraslado
Tipo:Vista
Observacion:Vista de solicitudes Vs ordenes de traslado
Profesional: Nilsson Miguel Galindo Lopez
Fecha:19-07-2022
---------------------------------------------------------------------------
Modificaciones
_____________________________________________________________________________
Vercion 1
Persona que modifico: 
Fecha:
Ovservaciones: 
--------------------------------------
Vercion 2
Persona que modifico:
Fecha:
***********************************************************************************************************************************/
CREATE view [Report].[ViewInventarioSolicitudVsOrdenTraslado] as

WITH
CTE_TRASLADO AS
(
SELECT
ORTD.ID,
ORTD.InventoryRequestDetailId,
ORT.Code AS [Codigo Orden Traslado],
ORT.ConfirmationDate AS [Fecha Orden Traslado],
CASE ORT.Status WHEN 1 THEN 'Registrado' 
				WHEN 2 THEN 'Confirmado/Entregado' 
				WHEN 3 THEN 'Anulado' 
				WHEN 4 THEN 'En Transito' END AS [Estado Orden Traslado],
ORTD.Quantity AS [Cntidad Despachada]
FROM 
Inventory.TransferOrderDetail ORTD INNER JOIN
Inventory.TransferOrder ORT ON ORTD.TransferOrderId=ORT.Id AND ORT.Status=2
),
CTE_DEVOLUCION AS
(
SELECT 
BAT.TransferOrderDetailId,
BAT.Quantity,
OD.Code
FROM
Inventory.TransferOrderDetailBatchSerial BAT INNER JOIN
Inventory.TransferOrderDevolutionDetail ODD ON BAT.Id=ODD.TransferOrderDetailBatchSerialId INNER JOIN
Inventory.TransferOrderDevolution OD ON ODD.TransferOrderDevolutionId=OD.Id AND OD.Status=2
),
--------------------CTE CATIDAD ACTUAL DE PRODUCTOS------------------------------
CTE_INVETARIO_FISICO AS
(
SELECT
ProductId,
SUM(Quantity)AS CANTIDADP
FROM Inventory.PhysicalInventory 
GROUP BY ProductId
)

select 
CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY,
UOP.UnitCode+' - '+UOP.UnitName AS [Unidad Operativa],
SOL.Code AS[Codigo De Solicitud],
SOL.ConfirmationDate AS [Fecha de Solicitud],
CASE SOL.RequestType WHEN 1 THEN 'UNIDAD FUNCIONAL' ELSE 'ALMACEN' END AS [Tipo de Solicitud],
CASE WHEN SOL.TargetFunctionalUnitId IS NOT NULL THEN UFU.Code+' - '+UFU.Name ELSE ALM.Code+' - '+ALM.Name END AS [Solicitado Desde],
INV.Code AS [Codigo Producto],
INV.Name AS [Producto],
SOLD.Quantity AS [Cantidad Solicitada],
TRA.[Codigo Orden Traslado],
TRA.[Fecha Orden Traslado],
TRA.[Estado Orden Traslado],
TRA.[Cntidad Despachada],
SOLD.OutstandingQuantity AS [Cantidad Faltante],
DEV.Code AS [Codigo Devolucion],
DEV.Quantity AS [Cantidad Devuelta],
INVF.CANTIDADP AS [Cantidad Fisica],
 1 'CANTIDAD',
 CAST(SOL.ConfirmationDate AS date) AS 'FECHA BUSQUEDA',-- fecha confirmacion de la solicitud
 YEAR(SOL.ConfirmationDate) AS 'AÑO FECHA BUSQUEDA',
 MONTH(SOL.ConfirmationDate) AS 'MES AÑO FECHA BUSQUEDA',
 CASE MONTH(SOL.ConfirmationDate) WHEN 1 THEN 'ENERO'
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
							   WHEN 12 THEN 'DICIEMBRE' END AS 'MES NOMBRE FECHA BUSQUEDA',
FORMAT(DAY(SOL.ConfirmationDate), '00') AS 'DIA FECHA BUSQUEDA',
CONCAT(FORMAT(MONTH(SOL.ConfirmationDate), '00') ,' - ', 
CASE MONTH(SOL.ConfirmationDate) WHEN 1 THEN 'ENERO'
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
							  WHEN 12 THEN 'DICIEMBRE' END) MES_LABEL_INGRESO,
CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
from 
Inventory.InventoryRequest SOL INNER JOIN
Inventory.InventoryRequestDetail SOLD ON SOL.Id=SOLD.InventoryRequestId AND SOLD.OutstandingQuantity !=0 AND SOL.Status=2 JOIN
Inventory.InventoryProduct INV ON SOLD.InventoryProductId=INV.Id LEFT JOIN
CTE_TRASLADO TRA ON SOLD.Id=TRA.InventoryRequestDetailId LEFT JOIN
Common.OperatingUnit UOP ON SOL.OperatingUnitId=UOP.Id LEFT JOIN
Payroll.FunctionalUnit UFU ON SOL.TargetFunctionalUnitId=UFU.Id LEFT JOIN
Inventory.Warehouse ALM ON SOL.TargetWarehouseId=ALM.Id LEFT JOIN
CTE_DEVOLUCION DEV ON TRA.ID=DEV.TransferOrderDetailId LEFT JOIN
CTE_INVETARIO_FISICO INVF ON SOLD.InventoryProductId=INVF.ProductId
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que cruza solicitudes de inventario confirmadas con cantidad pendiente de despacho contra sus órdenes de traslado confirmadas, devoluciones y stock físico actual por producto. Permite analizar la brecha entre lo solicitado y lo despachado por unidad operativa, bodega o unidad funcional, incluyendo cantidades devueltas. Expone dimensiones temporales detalladas (año, mes, día) de la fecha de confirmación de la solicitud para uso en herramientas de BI o reportes de gestión de inventario.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que cruza solicitudes de inventario con sus órdenes de traslado, devoluciones e inventario físico para monitorear el cumplimiento de despachos y cantidades pendientes por producto y unidad operativa.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las solicitudes de inventario deben estar en estado 2 (confirmadas) para ser incluidas.; Las líneas de detalle de la solicitud deben tener OutstandingQuantity distinto de cero (cantidad pendiente).; Solo se consideran órdenes de traslado en estado 2 (Confirmado/Entregado).; Solo se consideran devoluciones de traslado en estado 2.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan solicitudes con saldo pendiente (OutstandingQuantity != 0).; Las órdenes de traslado anuladas, registradas o en tránsito no se cruzan con la solicitud (solo Status=2).; Las devoluciones no confirmadas (Status<>2) se excluyen del cruce.; El inventario físico se agrega sumando Quantity por ProductId, sin discriminar bodega/lote.; La marca temporal ULT_ACTUAL se calcula convirtiendo GETDATE() a zona horaria ''Pakistan Standard Time''.; ID_COMPANY se obtiene del nombre de la base de datos actual (DB_NAME) truncado a 9 caracteres.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Solicitud de inventario; Orden de traslado; Devolución de traslado; Inventario físico; Cantidad pendiente / faltante; Unidad operativa; Unidad funcional; Almacén/Bodega; Producto/medicamento/insumo; Lote y serial', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] resultset: Devuelve un dataset por línea de solicitud pendiente, enriquecido con datos de la orden de traslado asociada (LEFT JOIN), su devolución y el inventario físico actual del producto.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ORT.Status IN (1,2,3,4) → Mapea a etiquetas: 1=''Registrado'', 2=''Confirmado/Entregado'', 3=''Anulado'', 4=''En Transito''.; si SOL.RequestType = 1 → Tipo de Solicitud = ''UNIDAD FUNCIONAL'' else Tipo de Solicitud = ''ALMACEN''; si SOL.TargetFunctionalUnitId IS NOT NULL → Origen ''Solicitado Desde'' se toma de Payroll.FunctionalUnit (Code+Name) else Origen ''Solicitado Desde'' se toma de Inventory.Warehouse (Code+Name); si MONTH(SOL.ConfirmationDate) entre 1 y 12 → Se traduce a nombre de mes en español (ENERO..DICIEMBRE) para etiquetas de reporte.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.TransferOrderDetail; Inventory.TransferOrder; Inventory.TransferOrderDetailBatchSerial; Inventory.TransferOrderDevolutionDetail; Inventory.TransferOrderDevolution; Inventory.PhysicalInventory; Inventory.InventoryRequest; Inventory.InventoryRequestDetail; Inventory.InventoryProduct; Common.OperatingUnit; Payroll.FunctionalUnit; Inventory.Warehouse', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'ViewInventarioSolicitudVsOrdenTraslado';
GO
