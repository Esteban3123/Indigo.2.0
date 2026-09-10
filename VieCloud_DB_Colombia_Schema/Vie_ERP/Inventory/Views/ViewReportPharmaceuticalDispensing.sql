
CREATE VIEW [Inventory].[ViewReportPharmaceuticalDispensing]
AS
SELECT  pdd.Row,
		pd.Id, 
		pd.Code as DispensacionCode, 
		pd.DocumentDate, 
		pdd.ServiceDate, 
		pd.AdmissionNumber, 
		pac.IPCODPACI as Codepacient, 
		pac.IPNOMCOMP as NamePacient,
		pdd.FunctionalUnitName as UnidadFuncional, 
		ISNULL(bed.NUMCAMHOS, '') AS Bed,
		ISNULL(pdd.ProfessionalName,'Medicamento de custodia')as Profesional, 
		pdd.WarehouseCodeName,
		pdd.ProductCode as ProductCode, 
		SUBSTRING(pdd.ProductName,1,45) as ProductName,
		pdd.BatchCode,
		pdd.ExpirationDate,
		pdd.Quantity, 
		pd.CreationUser 
FROM [Inventory].[PharmaceuticalDispensing] pd WITH (NOLOCK)
JOIN dbo.ADINGRESO ing ON pd.AdmissionNumber = ing.NUMINGRES
JOIN [dbo].INPACIENT pac WITH (NOLOCK) ON ing.IPCODPACI = pac.IPCODPACI
JOIN
(
	SELECT	CONCAT(MIN(pdd.Id), '-', MIN(pddbs.Id)) Row,
			pdd.PharmaceuticalDispensingId,
			pdd.ServiceDate,
			fu.Name FunctionalUnitName,
			tp.Name ProfessionalName,
			CONCAT(w.Code, ' - ', w.Name) WarehouseCodeName,
			ip.Code ProductCode,
			ip.Name ProductName,
			bs.BatchCode, 
			bs.ExpirationDate, 
			SUM(pddbs.Quantity) Quantity
	FROM Inventory.Warehouse w WITH (NOLOCK)
	JOIN [Inventory].[PharmaceuticalDispensingDetail] pdd WITH (NOLOCK) ON w.Id = pdd.WarehouseId
	JOIN [Inventory].[InventoryProduct] ip WITH (NOLOCK) ON pdd.ProductId = ip.Id
	JOIN [Payroll].[FunctionalUnit] fu WITH (NOLOCK) ON pdd.FunctionalUnitId = fu.Id
	LEFT JOIN [Common].[ThirdParty] tp WITH (NOLOCK) ON pdd.OrderedHealthProfessionalThirdPartyId = tp.Id
	LEFT JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial pddbs WITH (NOLOCK) ON pdd.Id = pddbs.PharmaceuticalDispensingDetailId
	LEFT JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON pddbs.PhysicalInventoryId = phi.Id
	LEFT JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
	GROUP BY pdd.PharmaceuticalDispensingId, pdd.ServiceDate, fu.Name, tp.Name, w.Code, w.Name, ip.Code, ip.Name, bs.BatchCode, bs.ExpirationDate
) pdd ON pd.Id = pdd.PharmaceuticalDispensingId
LEFT JOIN dbo.CHCAMASHO bed ON ISNULL(CASE WHEN ing.CODCAMACT = 0 THEN '' ELSE CAST(ing.CODCAMACT AS VARCHAR(15)) END, '') = bed.CODICAMAS
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de reporte de dispensación farmacéutica que integra información del despacho de medicamentos a pacientes ingresados. Combina el encabezado del documento de dispensación con el detalle de cada medicamento entregado, incluyendo producto, lote, fecha de vencimiento, cantidad dispensada por lote, bodega de origen, unidad funcional, cama hospitalaria y profesional que ordenó la fórmula. Cruza datos de admisiones y del maestro de pacientes para mostrar la cédula y nombre del paciente asociado a cada ingreso. Está diseñada para reportería y trazabilidad del circuito del medicamento: qué se dispensó, a quién, desde qué bodega, en qué fecha de servicio y bajo la orden de qué profesional de la salud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensing';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewReportPharmaceuticalDispensing';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista de reporte que consolida la dispensación farmacéutica a pacientes ingresados, mostrando datos del paciente, cama, unidad funcional, profesional, bodega, producto, lote y cantidad despachada.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La dispensación debe estar asociada a un ingreso existente en ADINGRESO y a un paciente en INPACIENT; Cada detalle de dispensación debe tener bodega, producto y unidad funcional válidos', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El nombre del producto se trunca a 45 caracteres en el reporte; Si no se identifica al profesional ordenante, la dispensación se etiqueta como ''Medicamento de custodia''; La cantidad reportada es la suma de las cantidades por lote/serial de cada línea de dispensación; La columna Row se construye como concatenación del menor Id de detalle y menor Id de detalle-lote para identificar cada agrupación; Solo se incluyen dispensaciones cuyo ingreso (AdmissionNumber) exista en ADINGRESO y cuyo paciente exista en INPACIENT (JOIN interno)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Paciente; Ingreso/Admisión hospitalaria; Cama hospitalaria; Unidad funcional; Profesional de la salud ordenante; Medicamento de custodia; Bodega/Almacén; Producto/Medicamento; Lote y fecha de vencimiento; Inventario físico', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Devuelve una fila por combinación de dispensación, fecha de servicio, unidad funcional, profesional, bodega, producto, lote y fecha de vencimiento, agregando la cantidad con SUM(pddbs.Quantity)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si pdd.OrderedHealthProfessionalThirdPartyId es NULL (no hay profesional asociado) → Se reporta el profesional como ''Medicamento de custodia'' vía ISNULL(pdd.ProfessionalName,''Medicamento de custodia''); si ing.CODCAMACT = 0 o NULL → Se trata como cadena vacía para no enlazar a una cama (LEFT JOIN con CHCAMASHO devuelve Bed='''') else Se convierte el código de cama a VARCHAR(15) y se cruza con CHCAMASHO.CODICAMAS para obtener NUMCAMHOS', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; dbo.ADINGRESO; dbo.INPACIENT; Inventory.Warehouse; Inventory.PharmaceuticalDispensingDetail; Inventory.InventoryProduct; Payroll.FunctionalUnit; Common.ThirdParty; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PhysicalInventory; Inventory.BatchSerial; dbo.CHCAMASHO', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewReportPharmaceuticalDispensing';
GO
