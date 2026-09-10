CREATE VIEW [MixingStation].[ViewReadjustmentsToBeAssigned]
AS
WITH CTE_PackagePersonalizedDetail AS	(
		SELECT	pp.Id PackagePersonalizedId,
				pp.Concentration,
				pp.ConcentrationMeasurementUnitId,
				pp.VolumeTotalOrder,
				pp.VolumeTotalOrderMeasurementUnitId,
				ppd.Id, 
				ppd.AtcId,
				ppd.Vehicle,
				ppd.MainMedicine,
				ppd.Thinner
				--------------
				, ppd.Quantity
				, ppd.MeasurementUnitId
		FROM MixingStation.PackagePersonalized pp WITH(NOLOCK)
		JOIN MixingStation.PackagePersonalizedDetail ppd WITH(NOLOCK) ON pp.Id = ppd.PackagePersonalizedId
		),
CTE_Package AS	(
		SELECT	p.Id PackageId,
				p.Concentration,
				p.ConcentrationMeasurementUnitId,
				p.VolumeTotalOrder,
				p.VolumeTotalOrderMeasurementUnitId,
				pd.Id,
				pd.AtcId,
				pd.Vehicle,
				pd.MainMedicine,
				pd.Thinner,
				p.ProductId,
				p.[Description]
				--------------------
				, pd.Quantity
				, pd.MeasurementUnitId
		FROM MixingStation.package p WITH(NOLOCK)
		JOIN MixingStation.PackageDetail pd WITH(NOLOCK) on p.Id = pd.PackageId
		)
SELECT 	r.Id
		, r.IsReadjustment
		, r.Status
		, r.RequestPackageDetailStatusId
		, [ip].Code ProductCode
		, [ip].[Name] ProductName
		, rpds.BatchCode
		, r.TechnicalConceptDate
		, CONCAT(udt.Code, ' - ', udt.[Description]) DosageDescription
		----------------------------------------------------------------------
		, rpds.PackageId  StandardPackageId
		, ISNULL(ppdM.AtcId, pdM.AtcId) AtcMainMedicine
		, ISNULL(ppdV.AtcId, pdV.AtcId) AtcVehicle
		, ISNULL(ppdT.AtcId, pdT.AtcId) AtcThinner
		----------------------------------------------------------------------
		, ISNULL(ppdM.Quantity, pdM.Quantity) QuantityMainMedicine
		, ISNULL(ppdV.Quantity, pdV.Quantity) QuantityVehicle
		, ISNULL(ppdT.Quantity, pdT.Quantity) QuantityThinner
		----------------------------------------------------------------------
		, ISNULL(ppdM.MeasurementUnitId, pdM.MeasurementUnitId) MeasurementUnitIdMainMedicine
		, ISNULL(ppdV.MeasurementUnitId, pdV.MeasurementUnitId) MeasurementUnitIdVehicle
		, ISNULL(ppdT.MeasurementUnitId, pdT.MeasurementUnitId) MeasurementUnitIdThinner
		----------------------------------------------------------------------
		, ISNULL(ppdM.Concentration, pdM.Concentration) Concentration
		, ISNULL(ppdM.ConcentrationMeasurementUnitId, pdM.ConcentrationMeasurementUnitId) ConcentrationMeasurementUnitId
		, ISNULL(ppdM.VolumeTotalOrder, pdM.VolumeTotalOrder) VolumeTotalOrder
		, ISNULL(ppdM.VolumeTotalOrderMeasurementUnitId, pdM.VolumeTotalOrderMeasurementUnitId) VolumeTotalOrderMeasurementUnitId
		---------------------------------------------------------------------
		, IIF (r.sendTo = 1 AND r.[Status] = 0, NULL, ISNULL(bs.ExpirationDate, r.ExpiratedDate))  ExpirationDate
FROM MixingStation.Readjustments r WITH(NOLOCK)
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) ON r.RequestPackageDetailStatusId = rpds.Id
JOIN MixingStation.RequestMixingStationDetail rms WITH(NOLOCK) ON rms.Id = rpds.RequestMixingStationDetailId
----------------------------------------------------------------------------------------
JOIN CTE_Package pdM WITH(NOLOCK) ON rpds.PackageId = pdM.PackageId AND pdM.MainMedicine = 1
LEFT JOIN CTE_Package pdV WITH(NOLOCK) ON rpds.PackageId = pdV.PackageId AND pdV.Vehicle = 1
LEFT JOIN CTE_Package pdT WITH (NOLOCK) ON rpds.PackageId = pdt.PackageId AND pdT.Thinner = 1
JOIN Inventory.InventoryProduct [ip] WITH(NOLOCK) ON pdM.ProductId = [ip].Id
LEFT JOIN CTE_PackagePersonalizedDetail ppdM ON rpds.PackagePersonalizedId = ppdM.PackagePersonalizedId AND ppdM.MainMedicine = 1
LEFT JOIN CTE_PackagePersonalizedDetail ppdV ON rpds.PackagePersonalizedId = ppdV.PackagePersonalizedId AND ppdV.Vehicle = 1
LEFT JOIN CTE_PackagePersonalizedDetail ppdT ON rpds.PackagePersonalizedId = ppdT.PackagePersonalizedId AND ppdT.Thinner = 1
----------------------------------
LEFT JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON rms.UnitDoseTypeId = udt.Id
LEFT JOIN Inventory.BatchSerial bs WITH(NOLOCK) ON rpds.BatchCode = bs.BatchCode
WHERE (r.IsReadjustment = 0 AND r.status = 0) or (r.IsReadjustment = 1 AND r.status IN (0,1))

--LO QUE MUESTRA:
---NOMBRE DEL MEDICAMENTO
---LOTE
---FECHA DE VENCIMIENTO
---TIPO DE DOSIS UNITARIA

/*LO QUE VALIDA
  - StandartPackageId --> PackageId
  - AtcMainMedicine
  - AtcVehicle
	   
  - Concentration
  - ConcentrationMeasurementUnitId
  - VolumeTotalOrder
  - VolumeTotalOrderMeasurementUnitId

*/
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que lista los reajustes de preparaciones magistrales (mezclas) pendientes de asignación en la estación de mezclas. Consolida información del medicamento principal, vehículo y diluyente (thinner) tomando primero el paquete personalizado y, si no existe, el paquete estándar. Muestra datos clave como nombre y código del producto farmacéutico, número de lote, fecha de vencimiento, tipo de dosis unitaria, concentración, volumen total de la orden y los códigos ATC de cada componente de la mezcla. Filtra únicamente los reajustes que están en estado pendiente (sin asignar) o en proceso, sirviendo como fuente de trabajo para que el personal de farmacia o la estación de mezclas asigne y procese los reajustes de fórmulas magistrales.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReadjustmentsToBeAssigned';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReadjustmentsToBeAssigned';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Lista los reajustes y solicitudes pendientes de asignación en la estación de mezclas, consolidando datos del paquete (estándar o personalizado), su medicamento principal, vehículo, diluyente, lote y fecha de vencimiento.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Todo registro de RequestPackageDetailStatus debe referenciar un PackageId con al menos un PackageDetail marcado MainMedicine = 1 (JOIN no LEFT); El producto referenciado por el paquete estándar debe existir en Inventory.InventoryProduct; El RequestPackageDetailStatus debe estar enlazado a un RequestMixingStationDetail válido', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila siempre corresponde a un paquete estándar con un componente MainMedicine = 1 (JOIN obligatorio sobre pdM); Los componentes Vehicle y Thinner son opcionales tanto en el paquete estándar como en el personalizado (LEFT JOIN); Los datos del paquete personalizado prevalecen sobre los del paquete estándar cuando ambos existen (patrón ISNULL(ppd*, pd*)); La fecha de vencimiento priorizada proviene del lote en Inventory.BatchSerial; sólo si no se encuentra se usa la fecha del reajuste; Se excluyen reajustes cuyo estado no cumpla el filtro WHERE, garantizando que sólo se listen los pendientes de asignación; Concentración, unidad de concentración, volumen total y su unidad siempre se reportan respecto al componente MainMedicine', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Reajuste de preparación farmacéutica; Estación de mezclas; Paquete estándar de preparación; Paquete personalizado (mezcla magistral); Medicamento principal (MainMedicine); Vehículo (Vehicle); Diluyente/Thinner; Lote y fecha de vencimiento; Tipo de dosis unitaria; Código ATC; Concentración y volumen total de orden; Concepto técnico farmacéutico', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.Readjustments: Devuelve únicamente reajustes que cumplan (IsReadjustment=0 AND status=0) OR (IsReadjustment=1 AND status IN (0,1)); [RETURN_RESULT] Inventory.BatchSerial: ExpirationDate = NULL cuando sendTo=1 y Status=0; en caso contrario, ISNULL(bs.ExpirationDate, r.ExpiratedDate); [RETURN_RESULT] MixingStation.PackagePersonalizedDetail: Si existe paquete personalizado asociado, sus valores (AtcId, Quantity, MeasurementUnitId, Concentration, VolumeTotalOrder) reemplazan a los del paquete estándar mediante ISNULL; [RETURN_RESULT] MixingStation.UnitDoseType: DosageDescription se construye como CONCAT(udt.Code, '' - '', udt.Description) cuando existe tipo de dosis unitaria asociado al detalle de la solicitud', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si r.sendTo = 1 AND r.[Status] = 0 → ExpirationDate se expone como NULL (no se entrega fecha de vencimiento cuando el reajuste fue enviado pero aún no procesado) else Se devuelve ISNULL(bs.ExpirationDate, r.ExpiratedDate): prioriza la fecha de vencimiento del lote en Inventory.BatchSerial y, si no existe, la fecha registrada en el propio reajuste; si r.IsReadjustment = 0 AND r.status = 0 → Se incluye el registro (solicitudes originales pendientes de asignación); si r.IsReadjustment = 1 AND r.status IN (0,1) → Se incluye el registro (reajustes en estado 0 o 1) else Cualquier otra combinación de IsReadjustment/status se excluye del resultado; si Existe PackagePersonalizedId asociado en RequestPackageDetailStatus → Se toman ATC, cantidades, unidades, concentración y volumen desde el paquete personalizado (ppdM/ppdV/ppdT) else Se toman desde el paquete estándar (pdM/pdV/pdT) vía ISNULL', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail; MixingStation.package; MixingStation.PackageDetail; MixingStation.Readjustments; MixingStation.RequestPackageDetailStatus; MixingStation.RequestMixingStationDetail; Inventory.InventoryProduct; MixingStation.UnitDoseType; Inventory.BatchSerial', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustmentsToBeAssigned';
GO
