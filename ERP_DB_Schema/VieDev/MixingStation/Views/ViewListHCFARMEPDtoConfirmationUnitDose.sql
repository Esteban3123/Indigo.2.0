CREATE VIEW [MixingStation].[ViewListHCFARMEPDtoConfirmationUnitDose]
AS

SELECT
    ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS Id,
    atc.Name AS ServiceName,
    atc.id AS ServiceId,
    CONCAT(atc.Code, ' - ', atc.Name) AS ServiceDescription,
    ISNULL(IIF(k.Origin = 'HCNUTPAREC', TRY_CAST(k.CORRPURGACAL AS DECIMAL(18,2)), pd.Dose), 0) AS Dosage,
    mu.Id AS MeasurementUnitId,
    CONCAT(mu.Code, ' - ', mu.Name) AS MeasurementUnitDescription,
    mu.Name AS MeasurementUnitName,
    CAST(1 AS TINYINT) AS ComponentType,
    'Medicamento' AS ComponentTypeName,
    CAST(pd.GroupingCodeDose AS VARCHAR(36)) AS AGRUPAQUETE,
    mu.UnitType,
    mu.Abbreviation AS MeasurementUnitAbbreviation,
    atc.FormulationType,
    imu.Weight,
    imu.WeightMeasureUnit,
    imu.WeightMeasureAbbreviation,
    imu.MeasurementUnitDescriptionWeight,
    imu.Volume,
    imu.VolumeMeasureUnit,
    imu.VolumeMeasureAbbreviation,
    imu.MeasurementUnitDescriptionVolume,
    CAST(0 AS TINYINT) AS NPT,
    k.IDHCPARNUTC AS NPTId
FROM MedicalHistory.PharmaDose pd WITH (NOLOCK)
JOIN Inventory.ATC atc WITH (NOLOCK) ON pd.ProductCode = atc.Code
JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON pd.MeasurementUnitCode = mu.CrystalMeasurementUnit
LEFT JOIN Inventory.InventoryMeasurementUnit imuw WITH(NOLOCK) ON atc.WeightMeasureUnit = imuw.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuv WITH(NOLOCK) ON atc.VolumeMeasureUnit = imuv.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuc WITH(NOLOCK) ON atc.ConcentrationMeasureUnitId = imuc.Id
LEFT JOIN HCNUTPAREC hcn WITH(NOLOCK) ON pd.GroupingCodeDose = hcn.AGRUPAQUETE
LEFT JOIN (
    SELECT
        ph.Id,
        hcnt.CORRPURGACAL,
        hcnt.VOLUMENCAL,
        hcnt.CODPRODUC,
        psms.Origin,
        hcnt.IDHCPARNUTC
    FROM HCNUTPAREND hcnt
    JOIN MedicalHistory.ProductSusceptibleMixingStation psms ON psms.Origin = 'HCNUTPAREC' AND psms.IdOrigin = hcnt.IDHCNUTPAREC
    JOIN MedicalHistory.PharmaDose ph ON ph.CodeSusceptibleMixingStation = psms.CodeSusceptibleMixingStation
) AS k ON k.Id = pd.Id AND k.CODPRODUC = pd.ProductCode AND pd.Dose = k.VOLUMENCAL
CROSS APPLY (
    SELECT
        -- Lógica para Peso (Weight)
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 1 THEN atc.ConcentrationQuantity
            ELSE atc.Weight
        END AS [Weight],
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 1 THEN atc.ConcentrationMeasureUnitId
            ELSE atc.WeightMeasureUnit
        END AS WeightMeasureUnit,
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 1 THEN imuc.Abbreviation
            ELSE imuw.Abbreviation
        END AS WeightMeasureAbbreviation,
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 1 THEN CONCAT(imuc.Code, ' - ', imuc.Name)
            ELSE CONCAT(imuw.Code, ' - ', imuw.Name)
        END AS MeasurementUnitDescriptionWeight,
        
        -- Lógica para Volumen (Volume)
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 2 THEN atc.ConcentrationQuantity
            ELSE atc.Volume
        END AS Volume,
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 2 THEN atc.ConcentrationMeasureUnitId
            ELSE atc.VolumeMeasureUnit
        END AS VolumeMeasureUnit,
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 2 THEN imuv.Abbreviation
            ELSE imuv.Abbreviation 
        END AS VolumeMeasureAbbreviation,
        CASE
            WHEN atc.FormulationType = 4 AND imuc.UnitType = 2 THEN CONCAT(imuc.Code, ' - ', imuc.Name) 
            ELSE CONCAT(imuv.Code, ' - ', imuv.Name)
        END AS MeasurementUnitDescriptionVolume

) AS imu
WHERE hcn.AGRUPAQUETE IS NULL

UNION ALL

SELECT
    hcnd.ID,
    atc.Name,
    atc.id,
    CONCAT(atc.Code, ' - ', atc.Name),
    ISNULL(pd.Dose, 0),
    mu.Id,
    CONCAT(mu.Code, ' - ', mu.Name),
    mu.Name,
    CAST(1 AS TINYINT),
    'Medicamento',
    CAST(pd.GroupingCodeDose AS VARCHAR(36)),
    mu.UnitType,
    mu.Abbreviation,
    atc.FormulationType,
    atc.Weight,
    atc.WeightMeasureUnit,
    imuw.Abbreviation,
    CONCAT(imuw.Code, ' - ', imuw.Name),
    atc.Volume,
    atc.VolumeMeasureUnit,
    imuv.Abbreviation,
    CONCAT(imuv.Code, ' - ', imuv.Name),
    CAST(1 AS TINYINT),
    hcn.IDHCPARNUTC
FROM MedicalHistory.PharmaDose pd WITH (NOLOCK)
JOIN HCNUTPAREC hcn WITH(NOLOCK) ON pd.GroupingCodeDose = hcn.AGRUPAQUETE
JOIN HCNUTPAREND hcnd WITH(NOLOCK) ON hcn.ID = hcnd.IDHCNUTPAREC
JOIN Inventory.ATC atc WITH (NOLOCK) ON hcnd.CODPRODUC = atc.Code
JOIN Inventory.InventoryMeasurementUnit mu WITH(NOLOCK) ON hcnd.CODUNIMED = mu.CrystalMeasurementUnit
LEFT JOIN Inventory.InventoryMeasurementUnit imuw WITH(NOLOCK) ON atc.WeightMeasureUnit = imuw.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuv WITH(NOLOCK) ON atc.VolumeMeasureUnit = imuv.Id;