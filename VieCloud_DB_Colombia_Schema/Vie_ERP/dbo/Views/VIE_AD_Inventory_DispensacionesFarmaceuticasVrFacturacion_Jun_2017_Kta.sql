
CREATE VIEW [dbo].[VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta]
AS

SELECT        UO.UnitName AS Sede, I.NUMINGRES AS Ingreso, i.iestadoin AS EstadoIng, D .Code AS Dispensacion, O.Code AS Orden, DI.ServiceDate AS Fecha, CASE WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Ene2017' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Feb2017' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Mar2017' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Abr2017' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'May2017' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Jun2017' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Jul2017' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Ago2017' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Sep2017' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Oct2017' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Nov2017' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Dic2017' WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Ene2016' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Feb2016' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Mar2016' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Abr2016' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'May2016' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Jun2016' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Jul2016' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Ago2016' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Sep2016' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Oct2016' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Nov2016' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Dic2016' WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Ene2015' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Feb2015' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Mar2015' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Abr2015' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'May2015' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Jun2015' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Jul2015' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Ago2015' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Sep2015' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Oct2015' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Nov2015' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Dic2015' END AS MesCosto, 
                         CASE d .status WHEN '1' THEN 'Registrada' WHEN '2' THEN 'Confirmada' END AS Estado, F.InvoiceNumber AS Factura, F.InvoiceDate AS FechaFactura, CASE WHEN (MONTH(f.invoicedate)) = '1' AND 
                         (YEAR(f.invoicedate)) = '2017' THEN 'Ene2017' WHEN (MONTH(f.invoicedate)) = '2' AND (YEAR(f.invoicedate)) = '2017' THEN 'Feb2017' WHEN (MONTH(f.invoicedate)) = '3' AND (YEAR(f.invoicedate)) 
                         = '2017' THEN 'Mar2017' WHEN (MONTH(f.invoicedate)) = '4' AND (YEAR(f.invoicedate)) = '2017' THEN 'Abr2017' WHEN (MONTH(f.invoicedate)) = '5' AND (YEAR(f.invoicedate)) 
                         = '2017' THEN 'May2017' WHEN (MONTH(f.invoicedate)) = '6' AND (YEAR(f.invoicedate)) = '2017' THEN 'Jun2017' WHEN (MONTH(f.invoicedate)) = '7' AND (YEAR(f.invoicedate)) 
                         = '2017' THEN 'Jul2017' WHEN (MONTH(f.invoicedate)) = '8' AND (YEAR(f.invoicedate)) = '2017' THEN 'Ago2017' WHEN (MONTH(f.invoicedate)) = '9' AND (YEAR(f.invoicedate)) 
                         = '2017' THEN 'Sep2017' WHEN (MONTH(f.invoicedate)) = '10' AND (YEAR(f.invoicedate)) = '2017' THEN 'Oct2017' WHEN (MONTH(f.invoicedate)) = '11' AND (YEAR(f.invoicedate)) 
                         = '2017' THEN 'Nov2017' WHEN (MONTH(f.invoicedate)) = '12' AND (YEAR(f.invoicedate)) = '2017' THEN 'Dic2017' WHEN (MONTH(f.invoicedate)) = '1' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'Ene2016' WHEN (MONTH(f.invoicedate)) = '2' AND (YEAR(f.invoicedate)) = '2016' THEN 'Feb2016' WHEN (MONTH(f.invoicedate)) = '3' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'Mar2016' WHEN (MONTH(f.invoicedate)) = '4' AND (YEAR(f.invoicedate)) = '2016' THEN 'Abr2016' WHEN (MONTH(f.invoicedate)) = '5' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'May2016' WHEN (MONTH(f.invoicedate)) = '6' AND (YEAR(f.invoicedate)) = '2016' THEN 'Jun2016' WHEN (MONTH(f.invoicedate)) = '7' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'Jul2016' WHEN (MONTH(f.invoicedate)) = '8' AND (YEAR(f.invoicedate)) = '2016' THEN 'Ago2016' WHEN (MONTH(f.invoicedate)) = '9' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'Sep2016' WHEN (MONTH(f.invoicedate)) = '10' AND (YEAR(f.invoicedate)) = '2016' THEN 'Oct2016' WHEN (MONTH(f.invoicedate)) = '11' AND (YEAR(f.invoicedate)) 
                         = '2016' THEN 'Nov2016' WHEN (MONTH(f.invoicedate)) = '12' AND (YEAR(f.invoicedate)) = '2016' THEN 'Dic2016' END AS MesFactura, pr.Code AS Cod_Producto, pr.Name AS Producto, 
                         pr.CodeAlternativeTwo AS [Código alterno 2], sg.Code + ' - ' + sg.Name AS Subgrupo, a.Name AS AlmacenDespacho, 
                         CASE WHEN pr.POSProduct = '1' THEN 'POS' WHEN pr.POSProduct = '0' THEN 'No_POS' END AS TipoProducto, DI.Quantity AS CantSolicitada, DI.ReturnedQuantity AS CantDevuelta, 
                         DI.Quantity - DI.ReturnedQuantity AS Cantidad, dev.Code AS Devolucion, un.Name AS Unidad_Destino, DI.AverageCost AS CostoPromedio, (DI.Quantity - DI.ReturnedQuantity) * DI.AverageCost AS CostoTotal,
dev.documentdate as FechaDevolucion

FROM            Inventory.PharmaceuticalDispensing AS D WITH (nolock) INNER JOIN
                         Inventory.PharmaceuticalDispensingDetail AS DI WITH (nolock) ON DI.PharmaceuticalDispensingId = D .Id AND D .Status <> '3' INNER JOIN
                         dbo.ADINGRESO AS I WITH (nolock) ON I.NUMINGRES = D .AdmissionNumber INNER JOIN
                         Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = DI.ProductId INNER JOIN
                         Inventory.ProductSubGroup AS sg WITH (nolock) ON sg.Id = pr.ProductSubGroupId INNER JOIN
                         Payroll.FunctionalUnit AS un WITH (nolock) ON un.Id = DI.FunctionalUnitId INNER JOIN
                         Inventory.Warehouse AS a WITH (nolock) ON a.Id = DI.WarehouseId AND a.Code <> '220' INNER JOIN
                         Common.OperatingUnit AS UO WITH (nolock) ON UO.Id = D .OperatingUnitId INNER JOIN
                         Billing.ServiceOrder AS O WITH (nolock) ON O.EntityCode = D .Code AND O.EntityId = D .Id AND O.EntityName = 'PharmaceuticalDispensing' INNER JOIN
                         Billing.ServiceOrderDetail AS bsod WITH (nolock) ON bsod.ServiceOrderId = O.Id AND bsod.ProductId = pr.Id LEFT OUTER JOIN
                         Inventory.ATC AS catc WITH (nolock) ON catc.Id = pr.ATCId INNER JOIN
                         Inventory.PharmaceuticalDispensingDetailBatchSerial AS bs WITH (nolock) ON bs.PharmaceuticalDispensingDetailId = DI.Id LEFT OUTER JOIN
                         Inventory.PharmaceuticalDispensingDevolutionDetail AS devd WITH (nolock) ON devd.PharmaceuticalDispensingDetailBatchSerialId = bs.Id LEFT OUTER JOIN
                         Inventory.PharmaceuticalDispensingDevolution AS dev WITH (nolock) ON dev.Id = devd.PharmaceuticalDispensingDevolutionId AND dev.AdmissionNumber = I.NUMINGRES LEFT OUTER JOIN
                         Billing.Invoice AS F WITH (nolock) ON F.AdmissionNumber = D .AdmissionNumber AND F.Status <> '2' INNER JOIN
                         Billing.InvoiceDetail AS bid WITH (nolock) ON bid.InvoiceId = F.Id AND bid.ServiceOrderDetailId = bsod.Id
WHERE        (D .Status <> '3')  AND (D .DocumentDate BETWEEN '06/01/2017 00:00:00' AND '06/30/2017 23:59:59')
UNION ALL
SELECT        UO.UnitName AS Sede, d .AdmissionNumber, i.iestadoin AS EstadoIng, D .Code AS Dispensacion, O.Code AS Orden, DI.ServiceDate AS Fecha, CASE WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Ene2017' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Feb2017' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Mar2017' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Abr2017' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'May2017' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Jun2017' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Jul2017' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Ago2017' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Sep2017' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Oct2017' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2017' THEN 'Nov2017' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2017' THEN 'Dic2017' WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Ene2016' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Feb2016' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Mar2016' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Abr2016' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'May2016' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Jun2016' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Jul2016' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Ago2016' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Sep2016' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Oct2016' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2016' THEN 'Nov2016' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2016' THEN 'Dic2016' WHEN (MONTH(D .DocumentDate)) = '1' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Ene2015' WHEN (MONTH(D .DocumentDate)) = '2' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Feb2015' WHEN (MONTH(D .DocumentDate)) = '3' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Mar2015' WHEN (MONTH(D .DocumentDate)) = '4' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Abr2015' WHEN (MONTH(D .DocumentDate)) = '5' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'May2015' WHEN (MONTH(D .DocumentDate)) = '6' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Jun2015' WHEN (MONTH(D .DocumentDate)) = '7' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Jul2015' WHEN (MONTH(D .DocumentDate)) = '8' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Ago2015' WHEN (MONTH(D .DocumentDate)) = '9' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Sep2015' WHEN (MONTH(D .DocumentDate)) = '10' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Oct2015' WHEN (MONTH(D .DocumentDate)) = '11' AND 
                         (YEAR(D .DocumentDate)) = '2015' THEN 'Nov2015' WHEN (MONTH(D .DocumentDate)) = '12' AND (YEAR(D .DocumentDate)) = '2015' THEN 'Dic2015' END AS MesCosto, 
                         CASE d .status WHEN '1' THEN 'Registrada' WHEN '2' THEN 'Confirmada' END AS Estado, 'Sin facturar' AS Factura, '01/01/1900' AS FechaFactura, 'NA' AS MesFactura, pr.Code AS Cod_Producto, 
                         pr.Name AS Producto, pr.CodeAlternativeTwo AS [Código alterno 2], sg.Code + ' - ' + sg.Name AS Subgrupo, a.Name AS AlmacenDespacho, 
                         CASE WHEN pr.POSProduct = '1' THEN 'POS' WHEN pr.POSProduct = '0' THEN 'No_POS' END AS TipoProducto, DI.Quantity AS CantSolicitada, bsod.DevolutionQuantity AS CantDevuelta, 
                         bsod.InvoicedQuantity AS Cantidad, dev.Code AS Devolucion, un.Name AS Unidad_Destino, DI.AverageCost AS CostoPromedio, (DI.Quantity - bsod.DevolutionQuantity) * DI.AverageCost AS CostoTotal,
dev.documentdate as FechaDevolucion
FROM            Inventory.PharmaceuticalDispensing AS D WITH (nolock) INNER JOIN
                         Inventory.PharmaceuticalDispensingDetail AS DI WITH (nolock) ON DI.PharmaceuticalDispensingId = d .Id AND d .Status <> 3 INNER JOIN
                         dbo.ADINGRESO AS I WITH (nolock) ON I.NUMINGRES = D .AdmissionNumber AND i.iestadoin <> 'A' AND i.iestadoin <> 'F' INNER JOIN
                         Billing.ServiceOrder AS O WITH (nolock) ON O.EntityId = D .Id AND O.EntityCode = D .Code AND o.Status <> 3 AND O.EntityName = 'PharmaceuticalDispensing' INNER JOIN
                         Billing.ServiceOrderDetail AS bsod ON bsod.ServiceOrderId = O.Id AND DI.ProductId = bsod.ProductId INNER JOIN
                         Inventory.InventoryProduct AS pr WITH (nolock) ON pr.Id = DI.ProductId INNER JOIN
                         Inventory.ProductSubGroup AS sg WITH (nolock) ON sg.Id = pr.ProductSubGroupId INNER JOIN
                         Payroll.FunctionalUnit AS un WITH (nolock) ON un.Id = DI.FunctionalUnitId INNER JOIN
                         Inventory.Warehouse AS a WITH (nolock) ON a.Id = DI.WarehouseId AND a.Code <> '220' INNER JOIN
                         Common.OperatingUnit AS UO WITH (nolock) ON UO.Id = D .OperatingUnitId LEFT OUTER JOIN
                         Inventory.ATC AS catc WITH (nolock) ON catc.Id = pr.ATCId INNER JOIN
                         Inventory.PharmaceuticalDispensingDetailBatchSerial AS bs WITH (nolock) ON bs.PharmaceuticalDispensingDetailId = DI.Id LEFT OUTER JOIN
                         Inventory.PharmaceuticalDispensingDevolutionDetail AS devd WITH (nolock) ON devd.PharmaceuticalDispensingDetailBatchSerialId = bs.Id LEFT OUTER JOIN
                         Inventory.PharmaceuticalDispensingDevolution AS dev WITH (nolock) ON dev.Id = devd.PharmaceuticalDispensingDevolutionId AND dev.AdmissionNumber = D .AdmissionNumber
WHERE        d .DocumentDate BETWEEN '06/01/2017 00:00:00' AND '06/30/2017 23:59:59' AND DI.Quantity - bsod.DevolutionQuantity <> 0 AND bsod.Id NOT IN
                             (SELECT        ServiceOrderDetailId
                               FROM            Billing.InvoiceDetail) AND bsod.Packaging <> 1
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Vista de reporte para el área de facturación y farmacia, acotada a dispensaciones farmacéuticas con fecha de documento en junio de 2017. Cruza dispensaciones con sus órdenes de servicio, facturas emitidas y detalle de facturación, permitiendo conciliar el costo de inventario (costo promedio por ítem dispensado, neto de devoluciones) frente al valor facturado por ingreso y sede. Etiqueta cada registro con el mes-año de la dispensación y de la factura (2015-2017) para análisis comparativo mensual de costos versus facturación de medicamentos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Reporta dispensaciones farmacéuticas de junio 2017 cruzadas con su facturación, mostrando producto, cantidades despachadas/devueltas, costos y, cuando existe, la factura asociada o marcando ''Sin facturar'' si aún no se ha facturado.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Los registros de dispensación deben tener DocumentDate entre 01/06/2017 y 30/06/2017; El estado de la dispensación (Status) debe ser distinto de 3 (no anulada); La bodega de despacho debe tener Code distinto de ''220''; Para el primer bloque, debe existir orden de servicio con EntityName=''PharmaceuticalDispensing'' enlazada por EntityCode/EntityId al documento de dispensación; Para el segundo bloque (sin facturar), el ingreso del paciente no debe estar en estado ''A'' ni ''F'', la orden de servicio no debe estar anulada (Status<>3), la cantidad neta (solicitada-devuelta) debe ser distinta de cero, el detalle de la orden no debe existir en InvoiceDetail y su Packaging debe ser distinto de 1', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Nunca se incluyen dispensaciones con Status=3 (anuladas); Nunca se incluyen movimientos de la bodega con Code=''220''; Nunca se incluyen facturas con Status=''2'' en el bloque facturado; En el bloque ''Sin facturar'' nunca se incluyen ingresos con estado ''A'' ni ''F''; En el bloque ''Sin facturar'' nunca se incluyen detalles de orden con Packaging=1 ni detalles ya presentes en InvoiceDetail; La cantidad neta dispensada se calcula como Quantity - ReturnedQuantity (o Quantity - DevolutionQuantity en el segundo bloque); El CostoTotal siempre se calcula como cantidad neta * AverageCost; Las devoluciones se asocian solo si pertenecen al mismo número de ingreso de la dispensación', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Devolución de medicamentos; Ingreso/admisión del paciente; Orden de servicio; Factura; Detalle de factura; Producto POS / No POS; Subgrupo de producto; Bodega/almacén de despacho; Unidad funcional; Sede / unidad operativa; Costo promedio; Cantidad facturada / devuelta; Empaque (Packaging); Clasificación ATC', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Cuando la dispensación tiene factura no anulada (Invoice.Status<>''2'') ligada al ingreso y al detalle de orden, se retorna la fila con número y fecha de factura reales; [RETURN_RESULT] (resultset): Cuando el detalle de orden de servicio NO existe en Billing.InvoiceDetail y Packaging<>1, se retorna la fila con Factura=''Sin facturar'', FechaFactura=''01/01/1900'' y MesFactura=''NA''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si MONTH/YEAR de DocumentDate de la dispensación → Se etiqueta MesCosto con el periodo correspondiente entre Ene2015 y Dic2017 else NULL si la fecha está fuera de 2015-2017; si MONTH/YEAR de InvoiceDate → Se etiqueta MesFactura con el periodo correspondiente entre Ene2016 y Dic2017 else NULL si está fuera de rango / ''NA'' en bloque sin facturar; si Status de la dispensación → 1 → ''Registrada'', 2 → ''Confirmada'' else NULL para otros estados (excluido el 3); si POSProduct del producto → 1 → ''POS'', 0 → ''No_POS''; si Existencia de factura para el ingreso vs. ausencia en InvoiceDetail → Primer SELECT entrega la dispensación facturada; segundo SELECT (UNION ALL) entrega la dispensación pendiente de facturar', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDevolution; Inventory.PharmaceuticalDispensingDevolutionDetail; dbo.ADINGRESO; Inventory.InventoryProduct; Inventory.ProductSubGroup; Inventory.ATC; Inventory.Warehouse; Payroll.FunctionalUnit; Common.OperatingUnit; Billing.ServiceOrder; Billing.ServiceOrderDetail; Billing.Invoice; Billing.InvoiceDetail', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIE_AD_Inventory_DispensacionesFarmaceuticasVrFacturacion_Jun_2017_Kta';
GO
