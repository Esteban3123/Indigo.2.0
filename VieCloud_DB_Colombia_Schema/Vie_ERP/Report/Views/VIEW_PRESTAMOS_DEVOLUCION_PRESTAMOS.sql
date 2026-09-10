
CREATE view [Report].[VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS] as

with cte_prestamo as
(
SELECT	ev.DocumentNumber [NUMERO COMPROBANTE],ev.DocumentDate [FECHA DOCUMENTO],CAST(ev.ConfirmationDate AS datetime ) [FECHA CONFIRMACION],ev.DocumentNumber [COMPROBANTE],
ev.LoanType [TIPO PRESTAMO],ev.UnitName [SEDE],
ev.Warehouse [BODEGA],ev.Operation [OPERACION],EV.tercero [TERCERO],
ev.ProductType [TIPO ACTIVIDAD],ev.CODIGO_PADRE [CODIGO PADRE],ev.NOMBRE_PADRE [NOMBRE PADRE],ev.HealthRegistration [REGISTRO SANITARIO],ev.ExpirationDate [FECHA EXPIRA REGISTRO],
EV.Code [CODIGO PRODUCTO],
ev.ProductName [PRODUCTO],ev.BatchCode [LOTE],ev.fechasanitario  [FECHA VENCIMIENTO],
ev.Quantity [CANTIDAD],ev.UnitValue [VALOR UNITARIO],(ev.Quantity * ev.UnitValue) [VALOR TOTAL],ev.CostCenter [CENTRO DE COSTO], 'PRESTAMOS DE INVENTARIOS' [TIPO]
FROM
(
	SELECT lm.Code DocumentNumber,lm.DocumentDate,lm.ConfirmationDate ,ou.UnitName,case lm.LoanType  when 1 then 'Entrada' when 2 then 'Salida' end LoanType,
	CONCAT(w.Code, ' - ', w.Name) Warehouse,IIF(lm.LoanType = 1, 'Suma', 'Resta') Operation,TP.Nit + ' - ' + tp.Name as tercero ,
	ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
pt.Name ProductType,ip.HealthRegistration,ip.ExpirationDate fechasanitario  ,ip.Code ,ip.Name ProductName,bs.BatchCode,bs.ExpirationDate,lmdbs.Quantity,lmd.UnitValue,cc.Name CostCenter
	FROM Common.OperatingUnit ou WITH (NOLOCK)
	JOIN Inventory.LoanMerchandise lm WITH (NOLOCK) ON ou.Id = lm.OperatingUnitId
	JOIN Inventory.LoanMerchandiseDetail lmd WITH (NOLOCK) ON lm.Id = lmd.LoanMerchandiseId
	JOIN Inventory.LoanMerchandiseDetailBatchSerial lmdbs WITH (NOLOCK) ON lmd.Id = lmdbs.LoanMerchandiseDetailId
	---------------------------------------------------------------------------------------------------------------
	JOIN Inventory.Warehouse w WITH (NOLOCK) ON lm.WarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON lmd.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory.ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
    LEFT JOIN Inventory.InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON lmdbs.BatchSerialId = bs.Id
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = w.CostCenterId
	LEFT JOIN Common.ThirdParty AS TP WITH (NOLOCK) ON TP.Id =lm.ThirdPartyId 
	WHERE lm.Status = 2 
) ev
UNION ALL
SELECT	ev.DocumentNumber [NUMERO COMPROBANTE],ev.DocumentDate [FECHA DOCUMENTO],ev.ConfirmationDate [FECHA CONFIRMACION],ev.documentoDevolver [COMPROBANTE],
'' [TIPO PRESTAMO],
ev.UnitName [SEDE],ev.Warehouse [BODEGA],ev.Operation [OPERACION],EV.tercero [TERCERO],
ev.ProductType [TIPO ACTIVIDAD],
ev.CODIGO_PADRE [CODIGO PADRE],ev.NOMBRE_PADRE [NOMBRE PADRE],ev.HealthRegistration [REGISTRO SANITARIO],ev.ExpirationDate [FECHA EXPIRA REGISTRO],EV.Code [CODIGO PRODUCTO],
ev.ProductName [PRODUCTO],ev.BatchCode [LOTE],ev.ExpirationDate [FECHA VENCIMIENTO],
ev.Quantity [CANTIDAD],ev.UnitValue [VALOR UNITARIO],(ev.Quantity * ev.UnitValue) [VALOR TOTAL],ev.CostCenter [CENTRO DE COSTO], 'DEVOLUCION PRESTAMOS DE INVENTARIOS' [TIPO]
FROM
(
	SELECT lmdev.Code DocumentNumber,lmdev.DocumentDate,lmdev.ConfirmationDate,lm.Code documentoDevolver ,ou.UnitName,CONCAT(w.Code, ' - ', w.Name) Warehouse,IIF(lm.LoanType = 1, 'Resta', 'Suma') Operation,
	TP.Nit + ' - ' + tp.Name as tercero ,
	ISNULL(ATC.Code,ISS.Code ) AS CODIGO_PADRE,ISNULL(ATC.Name,ISS.SupplieName ) AS NOMBRE_PADRE,
pt.Name ProductType,ip.HealthRegistration,ip.ExpirationDate fechasanitario ,ip .Code ,ip.Name ProductName,bs.BatchCode,bs.ExpirationDate,lmdevdbs.Quantity,lmd.UnitValue,cc.Name CostCenter
	FROM Inventory.LoanMerchandiseDevolution lmdev WITH (NOLOCK)
	JOIN Inventory.LoanMerchandiseDevolutionDetail lmdevd WITH (NOLOCK) ON lmdev.Id = lmdevd.LoanMerchandiseDevolutionId
	JOIN Inventory.LoanMerchandiseDevolutionDetailBatchSerial lmdevdbs WITH (NOLOCK) ON lmdevd.Id = lmdevdbs.LoanMerchandiseDevolutionDetailId
	JOIN Inventory.LoanMerchandiseDetail lmd WITH (NOLOCK) ON lmdevd.LoanMerchandiseDetaillId = lmd.Id
	JOIN Inventory.LoanMerchandise lm WITH (NOLOCK) ON lmd.LoanMerchandiseId = lm.Id
	JOIN Common.OperatingUnit ou WITH (NOLOCK) ON lm.OperatingUnitId = ou.Id
	---------------------------------------------------------------------------------------------------------------
	JOIN Inventory.Warehouse w WITH (NOLOCK) ON lm.WarehouseId = w.Id
	JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON lmd.ProductId = ip.Id
	JOIN Inventory.ProductType pt WITH (NOLOCK) ON ip.ProductTypeId = pt.Id
	LEFT JOIN Inventory .ATC AS ATC WITH (NOLOCK) ON ATC.ID =IP.ATCId 
    LEFT JOIN Inventory .InventorySupplie AS ISS WITH (NOLOCK) ON ISS.ID =IP.SupplieId
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN Inventory.PhysicalInventory phy WITH (NOLOCK) ON lmdevdbs.PhysicalInventoryId = phy.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phy.BatchSerialId = bs.Id
	---------------------------------------------------------------------------------------------------------------
	LEFT JOIN Payroll.CostCenter cc WITH (NOLOCK) ON cc.Id = w.CostCenterId
	LEFT JOIN Common .ThirdParty AS TP WITH (NOLOCK) ON TP.Id =lm.ThirdPartyId 
	WHERE lmdev.Status = 2
) ev

)

SELECT 
 CAST(DB_NAME() AS VARCHAR(9)) AS ID_COMPANY, 
  *,
 CAST([FECHA CONFIRMACION] AS date) AS 'FECHA BUSQUEDA',
 YEAR([FECHA CONFIRMACION]) AS 'AÑO BUSQUEDA',
 MONTH([FECHA CONFIRMACION]) AS 'MES BUSQUEDA',
 CONCAT(FORMAT(MONTH([FECHA CONFIRMACION]), '00') ,' - ', 
	   CASE MONTH([FECHA CONFIRMACION]) 
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
	    WHEN 12 THEN 'DICIEMBRE' END) 'MES NOMBRE BUSQUEDA',
 CONVERT(DATETIME,GETDATE() AT TIME ZONE 'Pakistan Standard Time',1) AS ULT_ACTUAL
FROM 
 cte_prestamo
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporting que consolida, mediante UNION ALL, los movimientos confirmados (estado 2) de préstamos de inventario y sus respectivas devoluciones por lote/serial. Permite a los consumidores analizar cantidades, valores unitarios y totales por sede, bodega, tercero, producto y centro de costo, distinguiendo si el registro corresponde a un préstamo (entrada/salida) o a una devolución (con operación invertida). Incluye columnas de fecha, año, mes y nombre del mes derivadas de la fecha de confirmación para facilitar el filtrado y agrupación en herramientas de BI.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida en un único reporte los movimientos de préstamos de inventario y sus devoluciones confirmados, enriquecidos con datos de producto, lote, bodega, tercero, centro de costo y dimensiones de fecha para análisis.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los préstamos (Inventory.LoanMerchandise) deben tener Status = 2 (confirmado) para ser incluidos.; Las devoluciones (Inventory.LoanMerchandiseDevolution) deben tener Status = 2 (confirmado) para ser incluidas.; Cada detalle de préstamo/devolución debe tener al menos un registro en su tabla de lotes/seriales asociada (JOIN obligatorio).; El producto debe existir en Inventory.InventoryProduct con un ProductType válido.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se reportan documentos confirmados (Status = 2) tanto de préstamos como de devoluciones; nunca documentos en estado borrador o anulado.; Cada fila representa una combinación préstamo/devolución × producto × lote/serial (granularidad de LoanMerchandiseDetailBatchSerial o su equivalente en devoluciones).; Las devoluciones siempre se vinculan a un préstamo origen vía LoanMerchandiseDetaillId, por lo que conservan trazabilidad al documento de préstamo en [COMPROBANTE].; La operación contable de una devolución es siempre la inversa de la operación del préstamo que la originó.; El reporte cubre solo movimientos cuyo producto tenga ProductType definido (JOIN no LEFT) y bodega válida.; [VALOR UNITARIO] de la devolución se hereda del préstamo original (no se recalcula).', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: Devuelve la unión (UNION ALL) de préstamos confirmados y sus devoluciones confirmadas, marcando la columna [TIPO] como ''PRESTAMOS DE INVENTARIOS'' o ''DEVOLUCION PRESTAMOS DE INVENTARIOS'' según el origen.; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: Para préstamos: cuando LoanType = 1 → [TIPO PRESTAMO]=''Entrada'' y [OPERACION]=''Suma''; cuando LoanType = 2 → ''Salida'' y ''Resta''.; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: Para devoluciones: la operación se invierte respecto del préstamo origen (LoanType=1 → ''Resta'', LoanType=2 → ''Suma'') y [TIPO PRESTAMO] queda en cadena vacía.; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: [VALOR TOTAL] se calcula como Quantity * UnitValue del detalle del préstamo (en devoluciones se reutiliza el UnitValue del LoanMerchandiseDetail original).; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: [CODIGO PADRE]/[NOMBRE PADRE] se toman de Inventory.ATC y, si no existen, se reemplazan con los de Inventory.InventorySupplie (ISNULL(ATC, ISS)).; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: [TERCERO] se compone como ''Nit - Name'' del ThirdParty asociado al préstamo (concatenación directa, sin manejo de NULL).; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: ID_COMPANY se materializa como DB_NAME() truncado a VARCHAR(9) para identificar la base de datos origen del registro.; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: ULT_ACTUAL se calcula como GETDATE() convertido a la zona horaria ''Pakistan Standard Time''.; [RETURN_RESULT] Report.VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS: Se generan dimensiones de fecha (FECHA BUSQUEDA, AÑO BUSQUEDA, MES BUSQUEDA, MES NOMBRE BUSQUEDA con etiqueta ''NN - NOMBRE_MES'') a partir de [FECHA CONFIRMACION].', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si lm.Status = 2 (préstamo confirmado) → Se incluye el movimiento en la rama de préstamos del UNION ALL. else Se excluye del reporte.; si lmdev.Status = 2 (devolución confirmada) → Se incluye el movimiento en la rama de devoluciones del UNION ALL. else Se excluye del reporte.; si lm.LoanType = 1 en la rama de préstamos → Etiqueta como ''Entrada'' con operación ''Suma''. else Etiqueta como ''Salida'' con operación ''Resta''.; si lm.LoanType = 1 en la rama de devoluciones → Operación ''Resta'' (inversa al préstamo original). else Operación ''Suma''.; si ATC.Code/Name no nulo → Se usa la información del catálogo ATC como código/nombre padre del producto. else Se usa Inventory.InventorySupplie (Code/SupplieName) como padre.; si MONTH([FECHA CONFIRMACION]) entre 1 y 12 → Se mapea al nombre del mes en español (ENERO..DICIEMBRE) precedido por el número de mes con dos dígitos.', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Report', @level1type=N'VIEW', @level1name=N'VIEW_PRESTAMOS_DEVOLUCION_PRESTAMOS';
GO
