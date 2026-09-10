CREATE VIEW [MixingStation].[ViewListHCFARMEPDtoConfirmationUnitDose]
AS

SELECT
    pd.Id AS Id,
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
FROM MedicalHistory.PharmaDose pd
JOIN Inventory.ATC atc ON pd.ProductCode = atc.Code
JOIN Inventory.InventoryMeasurementUnit mu ON pd.MeasurementUnitCode = mu.CrystalMeasurementUnit
LEFT JOIN Inventory.InventoryMeasurementUnit imuw ON atc.WeightMeasureUnit = imuw.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuv ON atc.VolumeMeasureUnit = imuv.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuc ON atc.ConcentrationMeasureUnitId = imuc.Id
LEFT JOIN HCNUTPAREC hcn ON pd.GroupingCodeDose = hcn.AGRUPAQUETE
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
FROM MedicalHistory.PharmaDose pd 
JOIN HCNUTPAREC hcn ON pd.GroupingCodeDose = hcn.AGRUPAQUETE
JOIN HCNUTPAREND hcnd ON hcn.ID = hcnd.IDHCNUTPAREC
JOIN Inventory.ATC atc ON hcnd.CODPRODUC = atc.Code
JOIN Inventory.InventoryMeasurementUnit mu ON hcnd.CODUNIMED = mu.CrystalMeasurementUnit
LEFT JOIN Inventory.InventoryMeasurementUnit imuw ON atc.WeightMeasureUnit = imuw.Id
LEFT JOIN Inventory.InventoryMeasurementUnit imuv ON atc.VolumeMeasureUnit = imuv.Id;
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista consolidada de medicamentos y componentes de dosis unitaria pendientes de confirmación en la estación de mezclas farmacéuticas. Combina dos fuentes: las dosis farmacéuticas prescritas (PharmaDose) que no pertenecen a una pauta de nutrición parenteral, y los componentes nutricionales de pautas de nutrición parenteral (HCNUTPAREND/HCNUTPAREC) asociados a dosis agrupadas. Para cada ítem calcula la dosis efectiva (considerando volumen de nutrición parenteral o dosis estándar según origen), la unidad de medida con su abreviatura, y las propiedades físicas del medicamento según el catálogo ATC (peso, volumen, tipo de formulación), diferenciando si el componente proviene de una orden regular o de una nutrición parenteral total (NPT). Sirve como fuente de datos para el proceso de confirmación y preparación de dosis unitarias en la farmacia hospitalaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Consolida los componentes medicamentosos (incluyendo aquellos provenientes de mezclas de nutrición parenteral) en formato de dosis unitaria para la confirmación en estación de mezclas, unificando datos de peso, volumen y concentración del producto.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'La PharmaDose debe tener ProductCode existente en Inventory.ATC y MeasurementUnitCode equivalente a un CrystalMeasurementUnit en Inventory.InventoryMeasurementUnit.; Para la rama de nutrición parenteral, la PharmaDose debe estar vinculada vía GroupingCodeDose a HCNUTPAREC.AGRUPAQUETE y existir detalle en HCNUTPAREND.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'ComponentType siempre es 1 (''Medicamento'') en ambas ramas; esta vista no expone otros tipos de componente.; Las filas no asociadas a NPT marcan NPT=0; las provenientes del paquete parenteral marcan NPT=1.; ComponentTypeName se etiqueta literalmente como ''Medicamento''.; Dosage nunca es NULL: se aplica ISNULL(...,0).; Una PharmaDose vinculada a un paquete parenteral (AGRUPAQUETE coincidente) se excluye de la primera rama mediante el filtro hcn.AGRUPAQUETE IS NULL para evitar duplicidad con la rama NPT.; El Id de salida usa PharmaDose.Id en negativo para la rama no-NPT y HCNUTPAREND.ID en positivo para la rama NPT, evitando colisiones entre ramas sin calcular ROW_NUMBER.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Dosis farmacéutica; Medicamento; Clasificación ATC; Unidad de medida (peso, volumen, concentración); Formulación; Nutrición parenteral (NPT); Estación de mezclas; Paquete de mezcla (AGRUPAQUETE); Producto susceptible de mezcla', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] (resultset): Retorna filas tipo medicamento (ComponentType=1, NPT=0) para PharmaDose no asociadas a paquete de nutrición parenteral (hcn.AGRUPAQUETE IS NULL).; [RETURN_RESULT] (resultset): Retorna filas asociadas a NPT (NPT=1, ComponentType=1) por cada detalle HCNUTPAREND ligado al paquete de nutrición parenteral cuya AGRUPAQUETE coincide con PharmaDose.GroupingCodeDose.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si k.Origin = ''HCNUTPAREC'' (la dosis proviene de una mezcla parenteral susceptible) → Dosage = TRY_CAST(k.CORRPURGACAL AS DECIMAL(18,2)) tomando la cantidad corregida de purga/calculada del registro de nutrición parenteral else Dosage = pd.Dose (dosis original de la prescripción farmacéutica); si todo es NULL, se retorna 0; si atc.FormulationType = 4 AND imuc.UnitType = 1 (formulación tipo 4 con unidad de concentración de tipo peso) → Weight, WeightMeasureUnit y descripciones se toman de los campos de Concentración del ATC (ConcentrationQuantity, ConcentrationMeasureUnitId, imuc) else Weight y unidades se toman de los campos de Peso del ATC (Weight, WeightMeasureUnit, imuw); si atc.FormulationType = 4 AND imuc.UnitType = 2 (formulación tipo 4 con unidad de concentración de tipo volumen) → Volume, VolumeMeasureUnit y descripciones se toman de los campos de Concentración del ATC else Volume y unidades se toman de los campos de Volumen del ATC (Volume, VolumeMeasureUnit, imuv); si hcn.AGRUPAQUETE IS NULL en la primera consulta del UNION → Se incluye la dosis como medicamento individual no perteneciente a un paquete NPT else Se excluye de la primera rama y solo aparecerá vía la segunda consulta del UNION ALL como componente NPT', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MedicalHistory.PharmaDose; Inventory.ATC; Inventory.InventoryMeasurementUnit; HCNUTPAREC; HCNUTPAREND; MedicalHistory.ProductSusceptibleMixingStation', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewListHCFARMEPDtoConfirmationUnitDose';
GO
