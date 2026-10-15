

CREATE VIEW  [MixingStation].[ViewComponentsNPT]
AS
SELECT concat(HAD.ID , '' ) Id
, hpc.id IdHCPARNUTC
,atc.Id AtcId
,had.CODPRODUC Code
,atc.Name
,(SELECT TOP 1 DCI.Name from Inventory.DCI DCI
join Inventory.ATC ATC ON ATC.DCIId = DCI.Id
WHERE had.CODPRODUC = ATC.Code) AbbreviationName
,had.CODUNIMED MeasurementUnitId
,HAD.TIPOVOLUMEN UnitType
,HAD.INDICATIONS
FROM HCPARNUTC HPC
JOIN HCPARNUTD HAD ON HAD.HCPARNUTCID = HPC.ID
JOIN Inventory.ATC ATC on ATC.Code = had.CODPRODUC