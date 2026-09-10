

create VIEW [Inventory].[ViewPharmaceuticalDispensingWithINDIGO008]
AS

SELECT
ipdd.Id AS 'id',
ipd.Id AS 'IdPharmaceuticalDispensing',
ipd.Code AS 'Codigo',
cou.UnitName AS 'UnidadOperativa',
ipd.AdmissionNumber AS 'Ingreso',
p.IPNOMCOMP AS 'NombreCompletoPaciente',
ipd.DocumentDate AS 'FechaDispensacion', 
case when ipd.AffectInventory = 1 then 'Si'when ipd.AffectInventory = 0 then 'No' end AS 'AfectaInventario',
case when ipd.Status = 1 then 'registrado' when ipd.Status = 2 then 'confirmado' else 'anulado' end AS 'Estado',
ipd.CreationUser AS 'UsuarioCreado',
ipd.CreationDate AS 'FechaCreacion',
iip.Code AS 'IdProducto',
iip.Name AS 'Producto',
iw.Code AS 'IdAlmacen',
iw.Name AS 'Almacen',
ipdd.Quantity AS 'Cantidad',
pfu.Name AS 'UnidadFuncional' 
FROM Inventory.PharmaceuticalDispensing AS ipd
LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd ON ipdd.PharmaceuticalDispensingId = ipd.Id
INNER JOIN Common.OperatingUnit AS cou ON ipd.OperatingUnitId = cou.Id
INNER JOIN Inventory.InventoryProduct AS iip ON ipdd.ProductId = iip.Id
INNER JOIN Inventory.Warehouse AS iw ON ipdd.WarehouseId = iw.Id
INNER JOIN Payroll.FunctionalUnit AS pfu ON ipdd.FunctionalUnitId = pfu.Id
LEFT JOIN dbo.ADINGRESO ad ON ipd.AdmissionNumber = ad.NUMINGRES
INNER JOIN dbo.INPACIENT p on p.IPCODPACI = ad.IPCODPACI
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información completa de dispensación farmacéutica cruzando los documentos de despacho de medicamentos con el detalle de cada ítem dispensado, la sede o unidad operativa donde se realizó, los datos del paciente (nombre completo) y su número de ingreso u hospitalización. Integra además el producto o medicamento entregado, la bodega de origen, la cantidad dispensada, la unidad funcional responsable y el estado del documento (registrado, confirmado o anulado). Está orientada a reportería y auditoría de dispensación de medicamentos por paciente ingresado, permitiendo rastrear qué medicamentos fueron entregados, desde qué almacén, en qué fecha y si el despacho afectó el inventario físico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de dispensaciones farmacéuticas con datos del documento, paciente, unidad operativa, producto, almacén y unidad funcional, traduciendo estados y afectación de inventario a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe existir el ingreso (ADINGRESO) referenciado por AdmissionNumber para resolver el paciente; si no existe, los datos del paciente quedarán nulos.; Todo detalle debe tener producto, almacén y unidad funcional válidos; de lo contrario la fila se excluye por los INNER JOIN.; La cabecera de dispensación debe tener una unidad operativa válida.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada detalle de dispensación se asocia obligatoriamente a una unidad operativa, producto, almacén y unidad funcional (INNER JOIN).; El paciente se obtiene siempre vía el ingreso (ADINGRESO) ligado al maestro INPACIENT.; El estado del documento se normaliza a tres valores: registrado, confirmado o anulado.; La afectación de inventario se expresa como ''Si''/''No'' a partir de un flag binario.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; paciente; ingreso/admisión; unidad operativa; almacén/bodega; producto de inventario; unidad funcional; afectación de inventario; estado de documento (registrado/confirmado/anulado)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.PharmaceuticalDispensingDetail: Devuelve una fila por cada detalle de dispensación con su cabecera, traduciendo Status (1→registrado, 2→confirmado, otro→anulado) y AffectInventory (1→Si, 0→No).', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AffectInventory = 1 → se reporta ''Si'' como afectación de inventario else si AffectInventory = 0 se reporta ''No''; si Status = 1 → se etiqueta como ''registrado'' else Status = 2 → ''confirmado''; cualquier otro valor → ''anulado''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Common.OperatingUnit; Inventory.InventoryProduct; Inventory.Warehouse; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO008';
GO
