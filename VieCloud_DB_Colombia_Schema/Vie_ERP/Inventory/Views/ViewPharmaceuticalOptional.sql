

CREATE VIEW [Inventory].[ViewPharmaceuticalOptional]
AS

SELECT
ipdd.Id AS 'id',
ipd.Id AS 'IdPharmaceuticalDispensing', 
ipd.Code AS 'Consecutivo',
ipd.CreationDate,
p.IPCODPACI,
ad.NUMINGRES,
iw.Code AS 'IdAlmacen',
iw.Name AS 'Almacen',
ipd.CreationUser,
per.Fullname,
iip.Code AS 'IdProducto',
iip.Name AS 'Producto',
ipdd.ServiceDate,
ipdd.Quantity,
ipdd.TotalSalesPrice,
ipdd.GrandTotalSalesPrice,
cg.Code,
atc.Code AS 'CodeAtc',
atc.Concentration,
p.IPNOMCOMP AS 'NombreCompletoPaciente',
p.IPPRINOMB,
p.IPSEGNOMB,
p.IPPRIAPEL,
p.IPSEGAPEL,
thi.Name AS 'NombreMedico',
ipdd.OrderedHealthProfessionalCode,
cg.Name,
cg.EntityType,
iim.Name AS 'UnidadProducto',
pfu.Name AS 'UnidadFuncional'
FROM Inventory.PharmaceuticalDispensing AS ipd with(nolock)
LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd with(nolock) ON ipd.Id = ipdd.PharmaceuticalDispensingId
INNER JOIN Common.ThirdParty AS thi with(nolock) ON thi.Id = ipdd.OrderedHealthProfessionalThirdPartyId
INNER JOIN Common.OperatingUnit AS cou with(nolock) ON cou.Id = ipd.OperatingUnitId
INNER JOIN Inventory.InventoryProduct AS iip with(nolock) ON iip.Id = ipdd.ProductId
INNER JOIN Contract.CareGroup AS cg with(nolock) ON cg.Id = ipdd.CareGroupId
LEFT JOIN Inventory.ATC AS atc with(nolock) ON atc.Id = iip.ATCId
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim with(nolock) ON iim.Id = iip.MeasurementUnitId
INNER JOIN Inventory.Warehouse AS iw with(nolock) ON iw.Id = ipdd.WarehouseId
INNER JOIN Payroll.FunctionalUnit AS pfu with(nolock) ON pfu.Id = ipdd.FunctionalUnitId
LEFT JOIN dbo.ADINGRESO AS ad with(nolock) ON ad.NUMINGRES = ipd.AdmissionNumber
INNER JOIN dbo.INPACIENT AS p with(nolock) on p.IPCODPACI = ad.IPCODPACI
LEFT JOIN Security.[UserInt] AS u ON u.UserCode = ipd.CreationUser
INNER JOIN Security.PersonInt AS per ON per.Id = u.IdPerson
WHERE iip.MeasurementUnitId is null
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida las dispensaciones farmacéuticas de medicamentos e insumos que tienen un producto sin unidad de medida asignada (casos opcionales o pendientes de completar). Integra el encabezado del despacho farmacéutico con el detalle de cada ítem dispensado, datos del paciente (cédula, nombre completo), número de ingreso o admisión, bodega de origen, médico que ordenó la fórmula, grupo de atención del contrato, clasificación ATC del medicamento y unidad funcional. Sirve para identificar y gestionar dispensaciones con información de producto incompleta, apoyando la conciliación de inventario, la trazabilidad de medicamentos entregados a pacientes hospitalizados o en urgencias, y la revisión de calidad del catálogo farmacéutico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalOptional';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalOptional';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de dispensaciones farmacéuticas de productos cuya unidad de medida no está definida, integrando datos del paciente, médico ordenante, almacén, grupo de atención y unidad funcional para revisión/corrección.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada detalle de dispensación debe estar asociado a un profesional ordenante, unidad operativa, producto, grupo de atención, almacén y unidad funcional existentes (INNER JOIN).; El paciente debe existir en INPACIENT vinculado al ingreso (ADINGRESO) cuando exista admisión.; El usuario creador debe tener registro en Security.UserInt y Security.PersonInt para mostrar nombre completo.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen dispensaciones de productos sin unidad de medida (MeasurementUnitId NULL).; Toda fila tiene profesional ordenante, unidad operativa, producto, grupo de atención, almacén y unidad funcional asociados.; El número de ingreso y los datos del paciente pueden ser nulos cuando no hay admisión vinculada (LEFT JOIN a ADINGRESO).; Las consultas se realizan con NOLOCK, permitiendo lecturas sucias.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Paciente; Ingreso/Admisión; Médico ordenante; Almacén/Bodega; Producto/Medicamento; Clasificación ATC; Unidad de medida; Grupo de atención (CareGroup); Unidad funcional; Unidad operativa (sede)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensingDetail: Solo retorna detalles de dispensación cuyo producto tiene MeasurementUnitId NULL (productos sin unidad de medida asignada).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si iip.MeasurementUnitId IS NULL → Se incluye la fila en el resultado (productos sin unidad de medida configurada). else Se excluye del resultado.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Common.ThirdParty; Common.OperatingUnit; Inventory.InventoryProduct; Contract.CareGroup; Inventory.ATC; Inventory.InventoryMeasurementUnit; Inventory.Warehouse; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalOptional';
GO
