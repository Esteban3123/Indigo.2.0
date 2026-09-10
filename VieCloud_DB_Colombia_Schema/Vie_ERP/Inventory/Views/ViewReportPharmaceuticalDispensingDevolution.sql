

CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensingDevolution]
AS
SELECT	pddevd.Row,
		pddev.Id PharmaceuticalDispensingDevolutionId,
		pddev.Code as DevolutionCode,
		pddev.DocumentDate,
		pddev.AdmissionNumber,
		pac.IPCODPACI as Codepacient, 
		pac.IPNOMCOMP as NamePacient,
		ISNULL(bed.NUMCAMHOS, '') AS Bed,
		CONCAT(w.Code, ' - ', w.Name) AS WareHouse, 
		pddev.Observation, 
		pddevd.ProductCode,
		SUBSTRING(pddevd.ProductName,1,45) as Name, 
		pddevd.BatchCode,
		pddevd.ExpirationDate,
		pddevd.Quantity,
		pddev.CreationUser
FROM [Inventory].[Warehouse] w WITH (NOLOCK)
JOIN [Inventory].[PharmaceuticalDispensingDevolution] pddev WITH (NOLOCK) ON w.Id = pddev.WarehouseId
JOIN dbo.ADINGRESO ing ON pddev.AdmissionNumber = ing.NUMINGRES
JOIN [dbo].INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN
(
	SELECT	CONCAT(MIN(pddevd.Id), '-', MIN(pdd.Id), '-', MIN(pddbs.Id)) Row,
			pddevd.PharmaceuticalDispensingDevolutionId,
			ip.Code ProductCode,
			ip.Name ProductName,
			bs.BatchCode, 
			bs.ExpirationDate, 
			SUM(pddevd.Quantity) Quantity
	FROM [Inventory].[PharmaceuticalDispensingDevolutionDetail] pddevd WITH (NOLOCK)
	JOIN [Inventory].[PharmaceuticalDispensingDetailBatchSerial] pddbs WITH (NOLOCK) ON pddevd.PharmaceuticalDispensingDetailBatchSerialId = pddbs.Id
	JOIN [Inventory].[PharmaceuticalDispensingDetail] pdd WITH (NOLOCK) ON pddbs.PharmaceuticalDispensingDetailId = pdd.Id
	JOIN [Inventory].[InventoryProduct] ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
	JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON ISNULL(pddbs.PhysicalInventoryId,pddbs.PhysicalInventoryCustodyId) = phi.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
	GROUP BY pddevd.PharmaceuticalDispensingDevolutionId, pdd.ServiceDate, ip.Code, ip.Name, bs.BatchCode, bs.ExpirationDate
) pddevd ON pddev.Id = pddevd.PharmaceuticalDispensingDevolutionId
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte que consolida las devoluciones de medicamentos dispensados a pacientes hospitalizados. Integra el documento de devolución farmacéutica con los datos del ingreso o admisión, la información del paciente (cédula, nombre completo), la cama hospitalaria asignada, la bodega receptora y el detalle de cada medicamento devuelto (código de producto, nombre, lote, fecha de vencimiento y cantidad). Agrupa los ítems devueltos por producto y lote para presentar una fila consolidada por medicamento en cada devolución. Está diseñada para reportería y auditoría del proceso de devolución de dispensación farmacéutica, permitiendo rastrear qué medicamentos fueron regresados, por qué paciente, en qué ingreso y desde qué almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensingDevolution';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida la información de devoluciones de dispensación farmacéutica para reportes, mostrando datos del documento, paciente, cama, bodega y detalle de productos devueltos por lote.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La devolución debe tener bodega válida en Inventory.Warehouse; El número de admisión debe existir en dbo.ADINGRESO y estar asociado a un paciente en dbo.INPACIENT; Cada detalle de devolución debe enlazar con un lote/serial de dispensación (PharmaceuticalDispensingDetailBatchSerial), su detalle de dispensación, producto y un PhysicalInventory (propio o en custodia)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre del producto se trunca a los primeros 45 caracteres; La cama se muestra como cadena vacía cuando no existe coincidencia o el código es 0; La cantidad reportada por línea es la suma agrupada por devolución, fecha de servicio, producto y lote; La columna Row se construye concatenando los IDs mínimos del detalle de devolución, detalle de dispensación y lote/serial; El campo WareHouse concatena código y nombre de la bodega separados por '' - ''; Solo se incluyen devoluciones con paciente (admisión) válido por uso de INNER JOIN', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Devolución de dispensación farmacéutica; Paciente; Admisión hospitalaria; Cama hospitalaria; Bodega de inventario; Producto/medicamento; Lote y fecha de vencimiento; Inventario físico; Inventario en custodia; Dispensación farmacéutica', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] N/A: Devuelve filas agrupadas por devolución, fecha de servicio, producto, lote y fecha de vencimiento, sumando la cantidad devuelta', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si ing.CODCAMACT = 0 → Se trata como cama vacía ('''') al cruzar contra dbo.CHCAMASHO else Se convierte el código de cama a VARCHAR(15) para hacer el LEFT JOIN con CHCAMASHO; si pddbs.PhysicalInventoryId es NULL → Se usa pddbs.PhysicalInventoryCustodyId para enlazar con PhysicalInventory (inventario en custodia) else Se usa pddbs.PhysicalInventoryId (inventario propio)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.Warehouse; Inventory.PharmaceuticalDispensingDevolution; dbo.ADINGRESO; dbo.INPACIENT; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Inventory.PhysicalInventory; Inventory.BatchSerial; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensingDevolution';
GO
