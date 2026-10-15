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