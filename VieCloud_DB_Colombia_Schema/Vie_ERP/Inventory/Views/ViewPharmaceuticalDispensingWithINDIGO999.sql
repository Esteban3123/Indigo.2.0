CREATE VIEW [Inventory].[ViewPharmaceuticalDispensingWithINDIGO999]
AS
     SELECT ipdd.Id AS 'id', 
            ipd.Id AS 'IdPharmaceuticalDispensing', 
            ipd.Code AS 'Codigo', 
            cou.UnitName AS 'UnidadOperativa', 
            ipd.AdmissionNumber AS 'Ingreso', 
            p.IPNOMCOMP AS 'NombreCompletoPaciente', 
            ipd.DocumentDate AS 'FechaDispensacion',
            CASE
                WHEN ipd.AffectInventory = 1
                THEN 'Si'
                WHEN ipd.AffectInventory = 0
                THEN 'No'
            END AS 'AfectaInventario',
            CASE
                WHEN ipd.STATUS = 1
                THEN 'registrado'
                WHEN ipd.STATUS = 2
                THEN 'confirmado'
                ELSE 'anulado'
            END AS 'Estado', 
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
          INNER JOIN dbo.INPACIENT p ON p.IPCODPACI = ad.IPCODPACI;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información completa de dispensaciones farmacéuticas, integrando el encabezado del documento de despacho de medicamentos, el detalle de cada ítem dispensado (producto, almacén, cantidad y unidad funcional), la unidad operativa o sede donde se realizó la dispensación, y los datos del paciente (nombre completo) obtenidos a partir del número de ingreso u admisión. Combina registros de dispensación con el catálogo de productos, bodegas, unidades funcionales, admisiones y la tabla maestra de pacientes, permitiendo trazabilidad completa del despacho de medicamentos por paciente ingresado. Sirve para reportería y consulta de dispensaciones farmacéuticas mostrando el estado del documento (registrado, confirmado o anulado), si afectó inventario, el usuario que lo creó y la fecha de dispensación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'VIEW', @level1name = N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone el detalle de dispensaciones farmacéuticas a pacientes ingresados, enriquecido con datos del producto, almacén, unidad funcional, unidad operativa y paciente, traduciendo estados y banderas a etiquetas legibles.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada dispensación debe estar asociada a una unidad operativa existente en Common.OperatingUnit; Cada detalle debe referenciar producto, almacén y unidad funcional existentes; El paciente asociado al ingreso debe existir en INPACIENT (INNER JOIN)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'El estado del documento solo puede presentarse como ''registrado'', ''confirmado'' o ''anulado''; La bandera de afectación de inventario se normaliza a ''Si''/''No'' (otros valores quedan en NULL); Solo se incluyen dispensaciones cuyo ingreso tenga paciente registrado en el maestro INPACIENT; Las dispensaciones sin detalle aparecen igualmente (LEFT JOIN con detalle), aunque el INNER JOIN posterior con producto/almacén/unidad funcional las filtra cuando el detalle es NULL', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'dispensación farmacéutica; paciente; ingreso/admisión; producto/medicamento; almacén/bodega; unidad operativa; unidad funcional; afectación de inventario; estado del documento (registrado/confirmado/anulado)', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Inventory.ViewPharmaceuticalDispensingWithINDIGO999: Devuelve una fila por cada detalle de dispensación cruzado con su cabecera y datos del paciente del ingreso', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si AffectInventory = 1 → Se etiqueta como ''Si'' (afecta inventario) else Si AffectInventory = 0 se etiqueta como ''No''; si STATUS = 1 → Estado se muestra como ''registrado'' else Si STATUS = 2 se muestra ''confirmado''; cualquier otro valor se muestra como ''anulado''', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Common.OperatingUnit; Inventory.InventoryProduct; Inventory.Warehouse; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'VIEW', @level1name=N'ViewPharmaceuticalDispensingWithINDIGO999';
GO
