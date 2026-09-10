CREATE VIEW [MixingStation].[ViewReadjustments] 
as 
	with	CTE_PackagePersonalizedDetail as	(
				select	pp.Id PackagePersonalizedId,
						pp.Concentration,
						pp.ConcentrationMeasurementUnitId,
						pp.VolumeTotalOrder,
						pp.VolumeTotalOrderMeasurementUnitId,
						ppd.Id, 
						ppd.AtcId,
						ppd.Vehicle,
						ppd.MainMedicine,
						ppd.Thinner
				from MixingStation.PackagePersonalized pp WITH(NOLOCK)
				join MixingStation.PackagePersonalizedDetail ppd WITH(NOLOCK) on pp.Id= ppd.PackagePersonalizedId
				),
		CTE_Package as	(
				select	p.Id PackageId,
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
						p.Description
				from MixingStation.package p WITH(NOLOCK)
				join MixingStation.PackageDetail pd WITH(NOLOCK) on p.Id= pd.PackageId
				)

SELECT	 r.Id
		,cd.CampaignNumber
		,pac.IPCODPACI NumberDocument
		,pac.IPNOMCOMP NamePacient
		,pdd.DocumentDate
		,rpds.BatchCode
		, Iif (r.sendTo = 1 And r.Status = 0, NULL, ISNULL(bs.ExpirationDate, r.ExpiratedDate))  ExpirationDate
		,ip.Code ProductCode
		,ip.Name ProductName
		,pdM.Description PackageDescription
		,rpds.PackageId
		,CONCAT(udt.Code, ' - ', udt.Description) DosageDescription
		,udt.MSClass as UnitDoseClass
		,r.Number
		,r.SendTo
		,c.CMConfigurationId
		,rms.ProductionLineId
		,r.Status
		,r.IsReadjustment
		,IIF(
			r.SendTo = 0,
			CASE r.Status WHEN 0 THEN 'Pendiente' when 1 then 'Readecuado' END, --Estado en el dashboard Readecuaciones
			--Estado en la pestaña readecuaciones - control calidad
			CASE r.Status
				WHEN 0 THEN 'Sin verificar'
				WHEN 1 THEN 'Aceptado'
				Else 'N/A'
			END
		) StatusName
		,mot.DESMOTANU ReasonDevolution
		,devf.DevolutionObservations
		,CONCAT(ou.UnitCode, ' - ', ou.UnitName) OperatingUnitDescription
		,CONCAT(fu.Code, ' - ',fu.[Name]) FunctionalUnitDescription
		,fu.Code FunctionalUnitCode
		,r.EntityId
		,r.EntityName
		,r.RequestPackageDetailStatusId
		,r.BatchCode BatchCodeOriginal
		,r.CreationUser
		,r.CreationDate
		,r.ModificationUser
		,r.ModificationDate
		,ISNULL(ppdM.Concentration,pdM.Concentration) Concentration
		,ISNULL(ppdM.ConcentrationMeasurementUnitId,pdM.ConcentrationMeasurementUnitId) ConcentrationMeasurementUnitId
		,ISNULL(ppdM.VolumeTotalOrder,pdM.VolumeTotalOrder) VolumeTotalOrder
		,ISNULL(ppdM.VolumeTotalOrderMeasurementUnitId, pdM.VolumeTotalOrderMeasurementUnitId) VolumeTotalOrderMeasurementUnitId
		,ISNULL(ppdM.AtcId,pdM.AtcId) AtcMainMedicine
		,ISNULL(ppdV.AtcId,pdV.AtcId) AtcVehicle
		--, ISNULL (ppdv.AtcId, pdT.AtcId) AtcThinner
		,r.TechnicalConceptDate
from MixingStation.Readjustments r WITH(NOLOCK)
JOIN MixingStation.RequestPackageDetailStatus rpds WITH(NOLOCK) on r.RequestPackageDetailStatusId=rpds.Id
JOIN Inventory.PharmaceuticalDispensingDevolution pdd WITH(NOLOCK) on r.EntityId=pdd.Id and r.EntityName='PharmaceuticalDispensingDevolution'
JOIN Common.OperatingUnit ou WITH(NOLOCK) ON pdd.OperatingUnitId = ou.Id
JOIN MixingStation.RequestMixingStationDetail rms WITH(NOLOCK) on rms.Id= rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd WITH(NOLOCK) on rms.CampaignDetailId=cd.Id
JOIN MixingStation.Campaign c WITH(NOLOCK) on cd.CampaignId=c.Id
JOIN MixingStation.CMConfiguration cmc WITH(NOLOCK) on c.CMConfigurationId=cmc.Id
----------------------------------------------------------------------------------------
JOIN CTE_Package pdM WITH(NOLOCK) on rpds.PackageId=pdM.PackageId and pdM.MainMedicine=1
left JOIN CTE_Package pdV WITH(NOLOCK) on rpds.PackageId=pdV.PackageId and pdV.Vehicle=1
LEFT JOIN CTE_Package pdT WITH (NOLOCK) on rpds.PackageId = pdt.PackageId AND pdT.Thinner = 1
--JOIN MixingStation.Package p WITH(NOLOCK) on rms.PackageId=p.Id
JOIN Inventory.InventoryProduct ip WITH(NOLOCK) ON pdM.ProductId=ip.Id
LEFT JOIN CTE_PackagePersonalizedDetail ppdM on rpds.PackagePersonalizedId=ppdM.PackagePersonalizedId and ppdM.MainMedicine=1
LEFT JOIN CTE_PackagePersonalizedDetail ppdV on rpds.PackagePersonalizedId=ppdV.PackagePersonalizedId and ppdV.Vehicle=1
----------------------------------
LEFT JOIN Inventory.BatchSerial bs WITH(NOLOCK) ON rpds.BatchCode=bs.BatchCode
LEFT JOIN MedicalHistory.DetailPhysicalCUM dpc WITH(NOLOCK) ON dpc.BatchCode = rpds.BatchCode
LEFT JOIN .HCDEVMEDD devf WITH(NOLOCK) ON devf.IdDetailPhysicalCUM = dpc.Id
LEFT JOIN Payroll.FunctionalUnit fu WITH(NOLOCK) ON devf.UFUCODIGO = fu.Code
LEFT JOIN .HCMOANULB mot WITH(NOLOCK) ON devf.IdHCMOANULB = mot.CODMOTANU
LEFT JOIN .INPACIENT pac WITH(NOLOCK) ON devf.IPCODPACI = pac.IPCODPACI
LEFT JOIN MixingStation.UnitDoseType udt WITH(NOLOCK) ON rms.UnitDoseTypeId = udt.Id
WHERE (r.IsReadjustment = 0 AND r.status = 0) or (r.IsReadjustment = 1 AND r.status IN (0,1))
----------------------------------
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Vista que consolida los registros de readecuaciones (reajustes) de mezclas intravenosas pendientes y procesadas en la estación de mezclas, integrando devoluciones farmacéuticas, datos del paciente (cédula, nombre), información del lote, fechas de vencimiento, fórmula del paquete de medicamento (principio activo, vehículo, diluyente, concentración y volumen total) y unidad funcional de atención. Combina paquetes estándar (Package) y paquetes personalizados (PackagePersonalized) con sus respectivos detalles de componentes, dando prioridad al paquete personalizado cuando existe. Sirve para el seguimiento y control de calidad de las readecuaciones en el dashboard de la estación de mezclas, mostrando el estado de cada readecuación (Pendiente, Readecuado, Sin verificar, Aceptado) tanto para el área de producción como para el área de control de calidad, junto con el motivo de devolución y las observaciones asociadas al paciente ingresado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReadjustments';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'backport_from_v25.47c_2026-05-05', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'VIEW', @level1name = N'ViewReadjustments';
GO
GO
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Expone los reajustes/readecuaciones vigentes de la estación de mezclas, enriquecidos con datos del paciente, devolución farmacéutica, lote, paquete (estándar o personalizado), unidad operativa/funcional y estado legible para dashboards y control de calidad.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'El reajuste debe estar vinculado a un RequestPackageDetailStatus existente.; El EntityName del reajuste debe ser ''PharmaceuticalDispensingDevolution'' y EntityId debe corresponder a un Inventory.PharmaceuticalDispensingDevolution válido.; Debe existir una jerarquía completa: RequestMixingStationDetail → CampaignDetail → Campaign → CMConfiguration.; El paquete asociado debe tener al menos un detalle marcado como medicamento principal (MainMedicine=1) en MixingStation.PackageDetail.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Cada fila representa un reajuste asociado obligatoriamente a una devolución de dispensación farmacéutica (EntityName fijo = ''PharmaceuticalDispensingDevolution'').; El medicamento principal del paquete (MainMedicine=1) es la fuente del ProductId que identifica el InventoryProduct expuesto como ProductCode/ProductName.; Cuando hay paquete personalizado, sus atributos prevalecen sobre los del paquete estándar para concentración, volumen y ATC.; Reajustes con IsReadjustment=0 solo son visibles mientras estén pendientes (Status=0); una vez procesados desaparecen de la vista.; La descripción del paquete proviene siempre del paquete estándar (pdM.Description), no del personalizado.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'readecuación/reajuste de mezclas; devolución de dispensación farmacéutica; paciente; lote y fecha de vencimiento; paquete personalizado vs estándar (mezcla magistral); medicamento principal; vehículo; diluyente (thinner); ATC; campaña de preparación; línea de producción; unidad operativa; unidad funcional; dosis unitaria; motivo de anulación/devolución; control de calidad de mezclas', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] MixingStation.Readjustments: Solo retorna filas donde (IsReadjustment=0 AND Status=0) o (IsReadjustment=1 AND Status IN (0,1)); reajustes en otros estados quedan excluidos.', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si r.SendTo = 1 AND r.Status = 0 → ExpirationDate se expone como NULL (lote pendiente de envío sin verificar) else ExpirationDate = ISNULL(bs.ExpirationDate, r.ExpiratedDate): se prefiere la fecha de vencimiento del lote en Inventory.BatchSerial; si no existe, se usa la registrada en el reajuste.; si r.SendTo = 0 (dashboard de Readecuaciones) → StatusName = ''Pendiente'' si Status=0, ''Readecuado'' si Status=1. else Pestaña control de calidad: StatusName = ''Sin verificar'' si Status=0, ''Aceptado'' si Status=1, ''N/A'' en otros casos.; si Existe PackagePersonalizedId en RequestPackageDetailStatus → Concentración, volumen y ATC de medicamento principal/vehículo se toman del paquete personalizado (CTE_PackagePersonalizedDetail). else Si no hay paquete personalizado, los valores caen al paquete estándar (CTE_Package) vía ISNULL.; si IsReadjustment = 0 → Solo se incluyen registros con Status=0 (pendientes de readecuar). else Si IsReadjustment=1, se incluyen Status 0 y 1 (sin verificar y aceptados).', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'MixingStation.PackagePersonalized; MixingStation.PackagePersonalizedDetail; MixingStation.Package; MixingStation.PackageDetail; MixingStation.Readjustments; MixingStation.RequestPackageDetailStatus; Inventory.PharmaceuticalDispensingDevolution; Common.OperatingUnit; MixingStation.RequestMixingStationDetail; MixingStation.CampaignDetail; MixingStation.Campaign; MixingStation.CMConfiguration; Inventory.InventoryProduct; Inventory.BatchSerial; MedicalHistory.DetailPhysicalCUM; HCDEVMEDD; Payroll.FunctionalUnit; HCMOANULB; INPACIENT; MixingStation.UnitDoseType', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'MixingStation', @level1type=N'VIEW', @level1name=N'ViewReadjustments';
GO
