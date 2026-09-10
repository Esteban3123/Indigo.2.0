CREATE VIEW [dbo].[RH_V_INN_Medicamentos]
AS
SELECT M.Id,DCI.Code AS CodDCI,  DCI.Name AS DCI, M.Code, M.Name AS Medicamento, M.AbbreviationName,VA.Code AS Via, VA.Name AS ViAdministracion, GF.Code as Grupo, GF.Name as GrupoFarmacologico, M.Concentration, NR.Code as CodNivel, NR.Name as NivelRiesgo, 
case M.FormulationType WHEN 1 then 'Peso' WHEN 2 THEN 'Volumen' WHEN 3 THEN 'Peso-Volumen'WHEN 4 THEN 'UnidadAdmin' END AS TipoFormula,  M.Weight AS Peso,U.Name AS UnidadPeso, M.Volume, U1.Name AS UnidadVolumen,
U2.Name AS UnidadD, CASE M.POSProduct WHEN 1 THEN 'POS' WHEN 0 THEN 'NO POS' END AS Tipo,  CASE M.AllPOSPathologies WHEN 1 THEN 'SI' WHEN 0 THEN 'NO' END AS TodasPatalogias,
CASE M.AutomaticCalculation WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS 'Calculo Automatico',
CASE M.TransferSurplusProduct WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS TrasladaSobrante,
CASE M.DiluentProduct WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS Diluyente,
CASE M.JustificationForSpecialDrugs WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS 'Justificacion Medicamento Especial',
CASE M.JustificationOfInputs WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS 'Justifica Insumo/Dispositivo',
CASE M.IndicatorDrug WHEN 1 THEN 'Si'
WHEN 0 THEN 'No' END AS 'Medicamento Trazador',
A.Code AS ATC
FROM INVENTORY.ATC M JOIN
INVENTORY.DCI ON M.DCIId = DCI.Id and M.Status =1 JOIN
Inventory.AdministrationRoute VA ON M.AdministrationRouteId = VA.Id JOIN
Inventory.PharmacologicalGroup GF ON M.PharmacologicalGroupId = GF.Id JOIN
Inventory.InventoryRiskLevel NR ON M.InventoryRiskLevelId = NR.Id LEFT JOIN
Inventory.InventoryMeasurementUnit U ON M.WeightMeasureUnit = U.Id LEFT JOIN
Inventory.InventoryMeasurementUnit U1 ON M.VolumeMeasureUnit = U1.Id LEFT JOIN
Inventory.InventoryMeasurementUnit U2 ON M.AdministrationUnitId = U2.Id JOIN
Inventory.ATCEntity A ON M.ATCEntityId = A.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo consolidado de medicamentos activos del inventario farmacéutico. Integra la información técnica y clínica de cada medicamento: código y nombre de la Denominación Común Internacional (DCI o principio activo genérico), código ATC, vía de administración, grupo farmacológico, concentración, tipo de formulación (peso, volumen, peso-volumen o unidad de administración), nivel de riesgo farmacéutico, unidades de medida de peso y volumen, y clasificadores de gestión como si pertenece al POS, si aplica para todas las patologías POS, si tiene cálculo automático de dosis, si es diluyente, si es medicamento trazador, si traslada sobrante, y si requiere justificación especial. Sirve como fuente principal para reportes de farmacia, formulación, gestión de inventario de medicamentos e insumos, y validaciones clínicas relacionadas con prescripción y dispensación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RH_V_INN_Medicamentos';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'RH_V_INN_Medicamentos';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Vista que consolida el catálogo de medicamentos activos con su descripción farmacológica completa: DCI, vía de administración, grupo, nivel de riesgo, presentación, clasificación POS y atributos clínicos.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Cada medicamento debe tener asociado DCI, vía de administración, grupo farmacológico, nivel de riesgo y entidad ATC (joins INNER obligatorios).; Las unidades de medida de peso, volumen pueden ser opcionales (LEFT JOIN); la unidad de administración es obligatoria.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo expone medicamentos activos (Status=1).; Todo medicamento expuesto está clasificado en ATC, DCI, vía de administración, grupo farmacológico y nivel de riesgo.; La clasificación POS es binaria (POS/NO POS).; Los flags clínicos (cálculo automático, sobrante, diluyente, justificaciones, trazador) se normalizan a ''Si''/''No''.; El tipo de formulación se restringe a cuatro valores: Peso, Volumen, Peso-Volumen, UnidadAdmin.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Medicamento; DCI (Denominación Común Internacional); Vía de administración; Grupo farmacológico; Nivel de riesgo de inventario; Clasificación ATC; POS (Plan Obligatorio de Salud); Patologías POS; Medicamento trazador; Diluyente; Medicamento especial; Insumo/Dispositivo; Tipo de formulación (peso/volumen); Concentración; Cálculo automático de dosis; Traslado de sobrante', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] INVENTORY.ATC: Solo se retornan medicamentos con Status = 1 (activos).', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si FormulationType = 1/2/3/4 → Se traduce a ''Peso'', ''Volumen'', ''Peso-Volumen'' o ''UnidadAdmin'' respectivamente.; si POSProduct = 1 → Se etiqueta como ''POS'' else Se etiqueta como ''NO POS''; si AllPOSPathologies = 1 → Aplica a todas las patologías (''SI'') else No aplica a todas (''NO''); si AutomaticCalculation = 1 → Cálculo automático ''Si'' else ''No''; si TransferSurplusProduct = 1 → Traslada sobrante ''Si'' else ''No''; si DiluentProduct = 1 → Es diluyente ''Si'' else ''No''; si JustificationForSpecialDrugs = 1 → Requiere justificación de medicamento especial ''Si'' else ''No''; si JustificationOfInputs = 1 → Justifica insumo/dispositivo ''Si'' else ''No''; si IndicatorDrug = 1 → Es medicamento trazador ''Si'' else ''No''', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'INVENTORY.ATC; INVENTORY.DCI; Inventory.AdministrationRoute; Inventory.PharmacologicalGroup; Inventory.InventoryRiskLevel; Inventory.InventoryMeasurementUnit; Inventory.ATCEntity', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'VIEW', @level1name=N'RH_V_INN_Medicamentos';
GO
