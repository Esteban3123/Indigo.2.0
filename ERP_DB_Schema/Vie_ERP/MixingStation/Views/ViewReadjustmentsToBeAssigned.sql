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
		FROM MixingStation.PackagePersonalized pp
		JOIN MixingStation.PackagePersonalizedDetail ppd ON pp.Id = ppd.PackagePersonalizedId
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
		FROM MixingStation.package p
		JOIN MixingStation.PackageDetail pd on p.Id = pd.PackageId
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
FROM MixingStation.Readjustments r
JOIN MixingStation.RequestPackageDetailStatus rpds ON r.RequestPackageDetailStatusId = rpds.Id
JOIN MixingStation.RequestMixingStationDetail rms ON rms.Id = rpds.RequestMixingStationDetailId
----------------------------------------------------------------------------------------
JOIN CTE_Package pdM ON rpds.PackageId = pdM.PackageId AND pdM.MainMedicine = 1
LEFT JOIN CTE_Package pdV ON rpds.PackageId = pdV.PackageId AND pdV.Vehicle = 1
LEFT JOIN CTE_Package pdT ON rpds.PackageId = pdt.PackageId AND pdT.Thinner = 1
JOIN Inventory.InventoryProduct [ip] ON pdM.ProductId = [ip].Id
LEFT JOIN CTE_PackagePersonalizedDetail ppdM ON rpds.PackagePersonalizedId = ppdM.PackagePersonalizedId AND ppdM.MainMedicine = 1
LEFT JOIN CTE_PackagePersonalizedDetail ppdV ON rpds.PackagePersonalizedId = ppdV.PackagePersonalizedId AND ppdV.Vehicle = 1
LEFT JOIN CTE_PackagePersonalizedDetail ppdT ON rpds.PackagePersonalizedId = ppdT.PackagePersonalizedId AND ppdT.Thinner = 1
----------------------------------
LEFT JOIN MixingStation.UnitDoseType udt ON rms.UnitDoseTypeId = udt.Id
LEFT JOIN Inventory.BatchSerial bs ON rpds.BatchCode = bs.BatchCode
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