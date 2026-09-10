CREATE VIEW [dbo].[VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS]
AS
     SELECT ipd.Code AS 'Codigo', 
            pfu.Name AS 'UnidadFuncional', 
            ipd.AdmissionNumber AS 'Ingreso', 
            ad.IFECHAING AS 'FechaIngreso',
            CASE
                WHEN p.IPTIPODOC = 1
                THEN 'Cédula de Ciudadanía'
                WHEN p.IPTIPODOC = 2
                THEN 'Cédula de Extranjería'
                WHEN p.IPTIPODOC = 3
                THEN 'Tarjeta de Identidad'
                WHEN p.IPTIPODOC = 4
                THEN 'Registro Civil'
                WHEN p.IPTIPODOC = 5
                THEN 'Pasaporte'
                WHEN p.IPTIPODOC = 6
                THEN 'Adulto Sin Identificación'
                WHEN p.IPTIPODOC = 7
                THEN 'Menor Sin Identificación'
                WHEN p.IPTIPODOC = 8
                THEN 'Número único de identificación personal'
                WHEN p.IPTIPODOC = 9
                THEN 'Certificado Nacido Vivo'
                WHEN p.IPTIPODOC = 10
                THEN 'Carnet Diplomático (Aplica para extranjeros)'
                WHEN p.IPTIPODOC = 11
                THEN 'Salvoconducto (Aplica para extranjeros)'
                ELSE 'Permiso especial de Permanencia (Aplica para extranjeros)'
            END AS 'TipoDocumento', 
            p.IPCODPACI AS 'DocumentoPaciente', 
            p.IPNOMCOMP AS 'NombreCompletoPaciente', 
            ipd.DocumentDate AS 'FechaDispensacion',
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
            atc.Code AS 'CódigoATC', 
            iip.Name AS 'Producto',
            CASE
                WHEN iip.POSProduct = 1
                THEN 'Pos'
                ELSE 'NoPos'
            END AS 'Pos_NoPos', 
            ipdd.Quantity AS 'Cantidad', 
            ipddv.Quantity AS 'CantidadDevuelta', 
            e.Name AS 'Entidad',
            CASE
                WHEN e.EntityType = 1
                THEN 'EPS Contributivo'
                WHEN e.EntityType = 2
                THEN 'EPS Subsidiado'
                WHEN e.EntityType = 3
                THEN 'ET Vinculados Municipios'
                WHEN e.EntityType = 4
                THEN 'ET Vinculados Departamentos'
                WHEN e.EntityType = 5
                THEN 'ARL Riesgos Laborales'
                WHEN e.EntityType = 6
                THEN 'MP Medicina Prepagada'
                WHEN e.EntityType = 7
                THEN 'IPS Privada'
                WHEN e.EntityType = 8
                THEN 'IPS Publica'
                WHEN e.EntityType = 9
                THEN 'Regimen Especial'
                WHEN e.EntityType = 10
                THEN 'Accidentes de transito'
                WHEN e.EntityType = 11
                THEN 'Fosyga'
                ELSE 'Otros'
            END AS 'TipoEntidad'
     FROM Inventory.PharmaceuticalDispensing AS ipd WITH(NOLOCK)
          LEFT JOIN Inventory.PharmaceuticalDispensingDetail AS ipdd WITH(NOLOCK) ON ipdd.PharmaceuticalDispensingId = ipd.Id
          INNER JOIN Inventory.PharmaceuticalDispensingDetailBatchSerial AS ipddbs WITH(NOLOCK) ON ipddbs.PharmaceuticalDispensingDetailId = ipdd.Id
          LEFT JOIN Inventory.PharmaceuticalDispensingDevolutionDetail AS ipddv WITH(NOLOCK) ON ipddv.PharmaceuticalDispensingDetailBatchSerialId = ipddbs.Id
          INNER JOIN Inventory.InventoryProduct AS iip WITH(NOLOCK) ON ipdd.ProductId = iip.Id
          LEFT JOIN Inventory.ATC AS atc WITH(NOLOCK) ON atc.Id = iip.ATCId
          INNER JOIN Payroll.FunctionalUnit AS pfu WITH(NOLOCK) ON ipdd.FunctionalUnitId = pfu.Id
          LEFT JOIN dbo.ADINGRESO ad WITH(NOLOCK) ON ipd.AdmissionNumber = ad.NUMINGRES
          INNER JOIN dbo.INPACIENT p WITH(NOLOCK) ON p.IPCODPACI = ad.IPCODPACI
          INNER JOIN Contract.HealthAdministrator e WITH(NOLOCK) ON e.Code = ad.CODENTIDA;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida la información de medicamentos dispensados clasificados como No POS (fuera del Plan Obligatorio de Salud) para pacientes hospitalizados o en atención. Integra los registros de dispensación farmacéutica con el detalle de ítems entregados, lotes utilizados y devoluciones realizadas, cruzando con el catálogo de productos (incluyendo su clasificación ATC), la unidad funcional que ordenó la dispensación, los datos del ingreso o admisión del paciente (número de ingreso, fecha de ingreso), la información completa del paciente (cédula, nombre, tipo de documento) y la entidad o aseguradora responsable del pago (EPS, ARL, medicina prepagada, etc.). Permite hacer seguimiento y auditoría de medicamentos No POS ordenados y dispensados por paciente, ingreso, unidad funcional y entidad pagadora, con cantidades dispensadas y devueltas, siendo útil para reportes de glosas, facturación, control de inventario farmacéutico y gestión de medicamentos de alto costo no incluidos en el POS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida las dispensaciones farmacéuticas a pacientes ingresados, mostrando datos del paciente, producto (POS/NoPOS), cantidades dispensadas y devueltas, unidad funcional y entidad responsable de pago.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El ingreso debe existir en ADINGRESO para cruzar paciente y entidad (INPACIENT y HealthAdministrator se unen por INNER JOIN a través de ADINGRESO).; El producto dispensado debe existir en InventoryProduct y la unidad funcional en Payroll.FunctionalUnit.; Debe existir al menos un registro de lote/serial (PharmaceuticalDispensingDetailBatchSerial) asociado al detalle para que la dispensación aparezca.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se listan dispensaciones que tengan al menos un detalle con lote/serial registrado (INNER JOIN con BatchSerial).; Solo se incluyen dispensaciones cuyo ingreso tenga paciente y entidad administradora válidos (INNER JOIN con INPACIENT y HealthAdministrator).; Las lecturas se realizan con NOLOCK, asumiendo tolerancia a lecturas sucias.; El nombre ''NOPOS'' del objeto sugiere foco en NoPOS, pero la vista no filtra por POSProduct y retorna ambos tipos clasificados.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Paciente; Ingreso/admisión hospitalaria; Dispensación farmacéutica; Devolución de medicamentos; Medicamento POS / NoPOS; Clasificación ATC; Unidad funcional; Tipo de documento de identificación; Entidad administradora de salud (EPS, ARL, IPS, Fosyga, Medicina Prepagada, Régimen Especial); Lote y serial de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve una fila por cada combinación de dispensación-detalle-lote/serial; si existen devoluciones asociadas al lote/serial se incluye la cantidad devuelta, de lo contrario queda nula (LEFT JOIN a PharmaceuticalDispensingDevolutionDetail).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si IPTIPODOC entre 1 y 11 → Traduce el código numérico al nombre del tipo de documento (Cédula, TI, RC, Pasaporte, etc.) else Cualquier otro valor se etiqueta como ''Permiso especial de Permanencia (Aplica para extranjeros)''; si STATUS de la dispensación = 1 o 2 → Se etiqueta como ''registrado'' o ''confirmado'' respectivamente else Cualquier otro estado se considera ''anulado''; si InventoryProduct.POSProduct = 1 → Producto se marca como ''Pos'' else Producto se marca como ''NoPos''; si EntityType entre 1 y 11 → Traduce el tipo de entidad (EPS Contributivo, Subsidiado, ARL, IPS, Fosyga, etc.) else Cualquier otro valor se etiqueta como ''Otros''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.PharmaceuticalDispensing; Inventory.PharmaceuticalDispensingDetail; Inventory.PharmaceuticalDispensingDetailBatchSerial; Inventory.PharmaceuticalDispensingDevolutionDetail; Inventory.InventoryProduct; Inventory.ATC; Payroll.FunctionalUnit; dbo.ADINGRESO; dbo.INPACIENT; Contract.HealthAdministrator', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'VIEW_PACIENTES_MEDICAMENTOS_ORDENADOS_NOPOS';
GO
