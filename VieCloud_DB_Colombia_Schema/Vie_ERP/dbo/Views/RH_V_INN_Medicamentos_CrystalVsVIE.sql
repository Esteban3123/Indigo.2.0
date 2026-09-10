CREATE VIEW [dbo].[RH_V_INN_Medicamentos_CrystalVsVIE]
as
SELECT M.Code, 
       M.Name AS Medicamento, 
       DCI.Code AS CodDCI, 
       DCI.Name AS DCI, 
       ATC.Code AS CodATC, 
       ATC.Name AS ATC, 
       M.Presentations AS Presentacion, 
       '' AS Forma, 
       v.Name AS Via, 
       G.Name AS GrupoFarmaco, 
       M.Concentration,
       CASE M.FormulationType
           WHEN 1
           THEN 'Peso'
           WHEN 2
           THEN 'Volumnen'
           WHEN 3
           THEN 'Peso/Volumen'
           WHEN 4
           THEN 'Unidad'
       END AS TipoFormulacion, 
       Volumen = CONVERT(CHAR(12), M.Volume) + ' ' + CONVERT(CHAR(12), U.[Name]), 
       Peso = CONVERT(CHAR(12), M.[Weight]) + ' ' + CONVERT(CHAR(12), U1.[Name]), 
       U2.[Name] AS Unidad, 
       'Vie' AS Indigo
FROM Inventory.ATC M
     JOIN Inventory.AdministrationRoute V ON M.AdministrationRouteId = V.Id
                                                   AND M.STATUS = 1
     JOIN Inventory.PharmacologicalGroup G ON M.PharmacologicalGroupId = G.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U ON M.VolumeMeasureUnit = U.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U1 ON M.WeightMeasureUnit = U1.Id
     LEFT JOIN Inventory.InventoryMeasurementUnit U2 ON M.AdministrationUnitId = U2.Id
     JOIN Inventory.DCI ON M.DCIId = DCI.Id
     JOIN Inventory.ATCEntity ATC ON M.ATCEntityId = ATC.Id
UNION
SELECT P.CODPRODUC AS Code, 
       P.DESPRODUC AS MEDICAMENTO, 
       DCI.CODDCIMED AS CodDCI, 
       DCI.DESDCIMED AS DCI, 
       '' AS CodATC, 
       '' AS ATC, 
       P.PRESENMED AS Presentacion, 
       F.DESFORMED AS Forma, 
       v.DESVIAADM AS Via, 
       G.DESGRUFAR AS Grupo, 
       P.CONCENMED AS Concentration,
       CASE P.TIPFORMED
           WHEN 1
           THEN 'Peso'
           WHEN 2
           THEN 'Volumnen'
           WHEN 3
           THEN 'Peso/Volumen'
           WHEN 4
           THEN 'Unidad'
       END AS TipoFormulacion, 
       CONVERT(CHAR(12), P.VOLTOTMED) + '' + CONVERT(CHAR(12), U.DESUNIMED) AS Volumen, 
       CONVERT(CHAR(12), P.PESTOTMED) + '' + CONVERT(CHAR(12), U1.DESUNIMED) AS Peso, 
       U2.DESUNIMED AS Unidad, 
       'Crystal' AS Indigo
FROM .IHLISTPRO P
     JOIN .HCVIAADMI V ON P.CODVIAADM = V.CODVIAADM
                                    AND P.TIPPRODUC <> 2
                                    AND P.PROESTADO = 1
     JOIN .IHGRUFARM G ON P.CODGRUFAR = G.CODGRUFAR
     JOIN .IHFORMEDI F ON P.CODFORMED = F.CODFORMED
     LEFT JOIN .INUNIMEDI U ON P.CODUNIVOL = U.CODUNIMED
     LEFT JOIN .INUNIMEDI U1 ON P.CODUNIPES = U1.CODUNIMED
     LEFT JOIN .INUNIMEDI U2 ON P.CODUNIADM = U2.CODUNIMED
     JOIN .IHDCIMEDI DCI ON P.CODDCIMED = DCI.CODDCIMED;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida el catálogo completo de medicamentos e insumos farmacéuticos provenientes de dos fuentes del sistema: la base de datos VIE (módulo Inventory) y la base de datos legacy Crystal (tablas IHLISTPRO y relacionadas). Para cada medicamento combina información de denominación común internacional (DCI o principio activo genérico), clasificación ATC, presentación, vía de administración, grupo farmacológico, concentración, tipo de formulación (peso, volumen, peso/volumen, unidad), y unidades de medida de volumen, peso y administración. La columna ''Indigo'' identifica el origen del registro (''Vie'' o ''Crystal''), permitiendo comparar, auditar diferencias y unificar el maestro de medicamentos entre ambas plataformas para reportería farmacéutica, conciliación de catálogos y generación de informes en Crystal Reports.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Unifica el catálogo de medicamentos del sistema nuevo (Inventory/VIE) con el del sistema legado (Crystal) en una sola lista comparable, mostrando DCI, ATC, vía, grupo farmacológico, presentación, formulación, volumen, peso y unidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Las tablas del esquema Inventory deben tener llaves foráneas válidas hacia AdministrationRoute, PharmacologicalGroup, DCI y ATCEntity (JOIN obligatorio).; Las tablas del sistema legado (IHLISTPRO, HCVIAADMI, IHGRUFARM, IHFORMEDI, IHDCIMEDI) deben ser accesibles vía nombre sin esquema.; Los códigos de unidad de medida (volumen, peso, administración) pueden ser nulos (LEFT JOIN).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen medicamentos activos en ambos orígenes (STATUS=1 en VIE; PROESTADO=1 en Crystal).; Productos del legado con TIPPRODUC=2 nunca se incluyen.; Cada fila se identifica por su origen mediante la columna Indigo (''Vie'' o ''Crystal'').; Los registros del lado VIE no exponen ''Forma'' (siempre cadena vacía); los del lado Crystal no exponen código/nombre ATC (siempre cadena vacía).; El tipo de formulación se codifica con los mismos cuatro valores en ambos sistemas (1=Peso, 2=Volumen, 3=Peso/Volumen, 4=Unidad).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; DCI (Denominación Común Internacional); Clasificación ATC; Vía de administración; Grupo farmacológico; Forma farmacéutica; Presentación; Concentración; Tipo de formulación (Peso/Volumen/Peso-Volumen/Unidad); Unidad de medida de inventario', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] : Devuelve la unión (UNION, elimina duplicados) de medicamentos activos de Inventory.ATC y de la tabla legada IHLISTPRO, etiquetando el origen como ''Vie'' o ''Crystal''.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si Inventory.ATC.STATUS = 1 → Incluye el medicamento del sistema VIE en el resultado else Se excluye del resultado; si IHLISTPRO.TIPPRODUC <> 2 AND IHLISTPRO.PROESTADO = 1 → Incluye el producto del sistema Crystal en el resultado else Se excluye (productos tipo 2 o inactivos no se listan); si FormulationType / TIPFORMED = 1,2,3,4 → Traduce a etiqueta ''Peso'', ''Volumnen'', ''Peso/Volumen'' o ''Unidad'' respectivamente else Devuelve NULL en TipoFormulacion', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Inventory.ATC; Inventory.AdministrationRoute; Inventory.PharmacologicalGroup; Inventory.InventoryMeasurementUnit; Inventory.DCI; Inventory.ATCEntity; IHLISTPRO; HCVIAADMI; IHGRUFARM; IHFORMEDI; INUNIMEDI; IHDCIMEDI', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos_CrystalVsVIE';
GO
