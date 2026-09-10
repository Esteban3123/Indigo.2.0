
CREATE VIEW  [MixingStation].[ViewQuantityRemainingLow]
AS
 SELECT 
    ROW_NUMBER() OVER(ORDER BY qr.Id) AS RowNumber,
    qr.Id,
    qr.CampaignDetailId,
    ip.Id AS ProductId,
    ip.Code + ' - ' + ip.Name AS ProductFullName,
    bs.Id AS BatchSerialId,
    bs.BatchCode AS BatchSerialCode,
    CONVERT(VARCHAR, qr.OpeningDate, 103) AS OpeningDate,
    CONVERT(VARCHAR, 
        CASE 
            WHEN qr.UnitTimeStability = 1 THEN DATEADD(HOUR, qr.Stability, qr.OpeningDate)
            ELSE DATEADD(DAY, qr.Stability, qr.OpeningDate)
        END, 
    103) AS VctoStability,
    qr.Stability,
    CONVERT(VARCHAR, 
        CASE 
            WHEN bs.ExpirationDate IS NULL THEN ip.ExpirationDate 
            ELSE bs.ExpirationDate 
        END, 
    103) AS ExpirationDate,
    FORMAT(qr.Concentration, 'N2') AS Concentration,
	--CONVERT(VARCHAR,FORMAT(qr.Concentration, 'N2')) + ' ' + imum.Abbreviation + '/' + imuv.Abbreviation AS ConcentrationWithMeasurementUnit,
    CONVERT(VARCHAR, FORMAT(qr.Concentration, 'N2')) + ' mg/ml' AS ConcentrationWithMeasurementUnit,
    FORMAT(qr.RemnantVolume, 'N2') AS RemnantVolume,
    FORMAT(ROUND(qr.Quantity, 2), 'N2') AS Quantity,
    crr.Id AS CauseReprocessingRejectionId,
    crr.Code AS CauseReprocessingRejectionCode,
    crr.Name AS CauseReprocessingRejectionName,
    qr.Observations,
    w1.Code + ' - ' + w1.Name AS WareHouseRemaining,
    c1.Name AS MixingStation,
    w.IdMixingStation,
    w.IdWarehouse,
    qr.Status,
    qp.Fullname AS QP,
    qc.Fullname AS QC
FROM MixingStation.QuantityRemaining qr WITH (NOLOCK)
JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON qr.BatchSerialId = bs.Id AND qr.Status = 2
JOIN Inventory.InventoryMeasurementUnit imum WITH (NOLOCK) ON qr.UnitQuantity = imum.Id
JOIN Inventory.InventoryMeasurementUnit imuv WITH (NOLOCK) ON qr.UnitRemnantVolume = imuv.Id
JOIN Inventory.InventoryProduct ip WITH (NOLOCK) ON qr.ProductId = ip.Id
LEFT JOIN MixingStation.CauseReprocessingRejection crr WITH (NOLOCK) ON qr.CauseReprocessingRejectionId = crr.Id
JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON qr.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c WITH (NOLOCK) ON cd.CampaignId = c.Id
JOIN MixingStation.CMConfiguration c1 WITH (NOLOCK) ON c.CMConfigurationId = c1.Id
JOIN MixingStation.CMWarehouse w WITH (NOLOCK) ON qr.WareHouseId = w.IdWarehouse AND c1.Id = w.IdMixingStation
JOIN Inventory.Warehouse w1 WITH (NOLOCK) ON w.IdWarehouse = w1.Id
JOIN MixingStation.CampaignDetailUsers cdu_qp WITH (NOLOCK) ON cd.Id = cdu_qp.CampaignDetailId AND cdu_qp.UserRole = 1
JOIN [Security].[User] su_qp ON cdu_qp.UserId = su_qp.Id
JOIN [Security].Person qp ON su_qp.IdPerson = qp.Id
JOIN MixingStation.CampaignDetailUsers cdu_qc WITH (NOLOCK) ON cd.Id = cdu_qc.CampaignDetailId AND cdu_qc.UserRole = 2
JOIN [Security].[User] su_qc ON cdu_qc.UserId = su_qc.Id
JOIN [Security].Person qc ON su_qc.IdPerson = qc.Id
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Muestra los remanentes (sobrantes) de medicamentos en estado activo (status = 2) dentro de la estación de mezclas farmacéuticas que tienen cantidad o volumen bajo. Integra información del producto (código y nombre), lote o serial, fechas de apertura, vencimiento de estabilidad y fecha de expiración del lote, concentración con unidad de medida (mg/ml), volumen remanente y cantidad disponible. También incluye el almacén donde se encuentra el remanente, la estación de mezclas asociada a través de la campaña farmacéutica, la causa de reprocesamiento o rechazo si aplica, y los profesionales responsables de control de calidad (QP y QC). Se usa para monitorear y alertar sobre sobrantes de preparaciones farmacéuticas con stock bajo, facilitando decisiones de reprocesamiento, descarte o aprovechamiento dentro del ciclo de producción de mezclas.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQuantityRemainingLow';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewQuantityRemainingLow';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista las cantidades remanentes de productos en estaciones de mezcla con stock bajo, mostrando lote, vencimientos, estabilidad, concentración, almacén, estación y los responsables QP/QC de la campaña.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El registro de QuantityRemaining debe tener Status = 2 para ser incluido (filtro en JOIN con BatchSerial).; Cada CampaignDetail debe tener al menos un usuario con UserRole=1 (QP) y otro con UserRole=2 (QC); de lo contrario la fila se excluye por los INNER JOIN.; Las unidades de medida de cantidad y volumen remanente deben existir en Inventory.InventoryMeasurementUnit.; El almacén referenciado en QuantityRemaining debe estar asociado a la estación de mezcla a través de CMWarehouse (qr.WareHouseId = w.IdWarehouse AND c1.Id = w.IdMixingStation).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se exponen remanentes cuyo Status sea 2 (estado activo/vigente para esta vista).; La unidad de concentración mostrada está fija como ''mg/ml'' (texto literal), aunque internamente se vinculen las unidades de cantidad y volumen remanente.; El vencimiento de estabilidad se calcula desde la fecha de apertura usando horas o días según UnitTimeStability (1=horas, otro=días).; La fecha de vencimiento efectiva prioriza la del lote (BatchSerial) y, si no existe, cae en la del producto.; Cada fila representa la combinación única de un remanente con su QP (rol 1) y QC (rol 2) del detalle de campaña.; Las fechas se entregan formateadas como VARCHAR en formato británico dd/mm/yyyy (estilo 103) y los numéricos con formato ''N2''.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Cantidad remanente / sobrante; Estación de mezcla (Mixing Station); Campaña de preparación; Lote / BatchSerial; Estabilidad del producto abierto (en horas o días); Fecha de apertura; Fecha de vencimiento del lote/producto; Concentración (mg/ml); Volumen remanente; Causa de reproceso/rechazo; Almacén / Bodega; QP (Quality Person); QC (Quality Control); Producto de inventario', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.ViewQuantityRemainingLow: Devuelve únicamente remanentes con qr.Status = 2 (filtrado en el JOIN a BatchSerial), enumerados con ROW_NUMBER ordenado por qr.Id.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si qr.UnitTimeStability = 1 → VctoStability = OpeningDate + qr.Stability horas (DATEADD HOUR) else VctoStability = OpeningDate + qr.Stability días (DATEADD DAY); si bs.ExpirationDate IS NULL → Se reporta ExpirationDate del producto (ip.ExpirationDate) else Se reporta ExpirationDate del lote/serial (bs.ExpirationDate); si cdu.UserRole = 1 → El usuario asociado al CampaignDetail se expone como QP (Quality Person/Persona Cualificada) else Si UserRole = 2 se expone como QC (Quality Control)', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.QuantityRemaining; Inventory.BatchSerial; Inventory.InventoryMeasurementUnit; Inventory.InventoryProduct; MixingStation.CauseReprocessingRejection; MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; MixingStation.CMWarehouse; Inventory.Warehouse; MixingStation.CampaignDetailUsers; Security.User; Security.Person', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewQuantityRemainingLow';
GO
