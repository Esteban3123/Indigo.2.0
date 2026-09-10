

CREATE VIEW [Inventory].[ViewPharmaceutical]
AS

SELECT
ipdd.Id AS 'id',
ipd.Id AS 'IdPharmaceuticalDispensing', 
ipd.Code AS 'Consecutivo',
ipd.CreationDate,
p.IPCODPACI,
ad.NUMINGRES,
iw.Id AS 'IdAlmacen',
iw.Name AS 'Almacen',
ipd.CreationUser,
per.Fullname,
iip.Id AS 'IdProduct',
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
case when cg.EntityType = 1 then 'EPS Contributivo' when cg.EntityType = 2 then 'EPS Subsidiado' when cg.EntityType = 3 then 'ET Vinculados Municipios' when cg.EntityType = 4 then 'ET Vinculados Departamentos' when cg.EntityType = 5 then 'ARL Riesgos Laborales'  else 'MP Medicina Prepagada' end AS 'Regimen',
iim.Name AS 'UnidadProducto',
pfu.Name AS 'UnidadFuncional'
FROM Inventory.PharmaceuticalDispensing AS ipd
inner JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
INNER JOIN Common.ThirdParty AS thi ON ipdd.OrderedHealthProfessionalThirdPartyId = thi.Id
INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
INNER JOIN Contract.CareGroup AS cg ON ipdd.CareGroupId = cg.Id
INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
LEFT JOIN Inventory.ATC AS atc ON iip.ATCId = atc.Id
LEFT JOIN Inventory.InventoryMeasurementUnit AS iim ON iip.MeasurementUnitId = iim.Id
left JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
LEFT JOIN dbo.ADINGRESO AS ad ON ipd.AdmissionNumber = ad.NUMINGRES
left JOIN dbo.INPACIENT AS p on p.IPCODPACI = ad.IPCODPACI
LEFT JOIN Security.[UserInt] AS u ON ipd.CreationUser= u.UserCode
left JOIN Security.PersonInt AS per ON u.IdPerson = per.Id
where ipd.Status = 2
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista de dispensación farmacéutica que consolida cada ítem de medicamento entregado a un paciente ingresado, integrando datos del despacho (consecutivo, fecha, bodega), del medicamento (código, nombre, clasificación ATC, concentración, unidad de medida), del paciente (cédula, nombre completo), del ingreso o admisión (número de ingreso), del médico que ordenó la fórmula, del grupo de atención o contrato (EPS, ARL, régimen), y de la unidad funcional donde se generó la dispensación. Incluye únicamente documentos en estado aprobado o finalizado (Status = 2). Sirve principalmente para reportería de consumo de medicamentos, trazabilidad de la dispensación por paciente y admisión, auditoría farmacéutica, análisis de costos por aseguradora o régimen, y generación de informes de RIPS o facturación de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceutical';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceutical';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el detalle de dispensaciones farmacéuticas confirmadas, integrando datos del paciente, ingreso, médico, almacén, producto, clasificación ATC, contrato/régimen y unidad funcional para reportes.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las dispensaciones deben existir con Status = 2 para ser visibles; Cada detalle debe estar asociado a un encabezado de dispensación, profesional ordenante, unidad operativa, producto, grupo de atención (CareGroup) y bodega', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen dispensaciones con Status = 2; El régimen siempre se clasifica en una de seis categorías según EntityType del CareGroup, con ''MP Medicina Prepagada'' como valor por defecto; Datos de admisión, paciente, ATC, unidad de medida, unidad funcional y usuario creador son opcionales (LEFT JOIN); el resto son obligatorios (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dispensación farmacéutica; Paciente; Ingreso/Admisión; Médico ordenante; Almacén/Bodega; Producto/Medicamento; Clasificación ATC; Grupo de atención (CareGroup); Régimen de salud (EPS Contributivo, EPS Subsidiado, ET Vinculados, ARL, Medicina Prepagada); Unidad funcional; Unidad de medida', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensing: Solo retorna filas cuya dispensación tiene Status = 2 (dispensaciones confirmadas/aprobadas)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si cg.EntityType = 1 → Régimen = ''EPS Contributivo''; si cg.EntityType = 2 → Régimen = ''EPS Subsidiado''; si cg.EntityType = 3 → Régimen = ''ET Vinculados Municipios''; si cg.EntityType = 4 → Régimen = ''ET Vinculados Departamentos''; si cg.EntityType = 5 → Régimen = ''ARL Riesgos Laborales'' else Régimen = ''MP Medicina Prepagada''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Common.ThirdParty; Common.OperatingUnit; Inventory.InventoryProduct; Contract.CareGroup; Inventory.Warehouse; Inventory.ATC; Inventory.InventoryMeasurementUnit; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT; Security.UserInt; Security.PersonInt', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceutical';
GO
