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
				from MixingStation.PackagePersonalized pp
				join MixingStation.PackagePersonalizedDetail ppd on pp.Id= ppd.PackagePersonalizedId
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
				from MixingStation.package p
				join MixingStation.PackageDetail pd on p.Id= pd.PackageId
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
from MixingStation.Readjustments r
JOIN MixingStation.RequestPackageDetailStatus rpds on r.RequestPackageDetailStatusId=rpds.Id
JOIN Inventory.PharmaceuticalDispensingDevolution pdd on r.EntityId=pdd.Id and r.EntityName='PharmaceuticalDispensingDevolution'
JOIN Common.OperatingUnit ou ON pdd.OperatingUnitId = ou.Id
JOIN MixingStation.RequestMixingStationDetail rms on rms.Id= rpds.RequestMixingStationDetailId
JOIN MixingStation.CampaignDetail cd on rms.CampaignDetailId=cd.Id
JOIN MixingStation.Campaign c on cd.CampaignId=c.Id
JOIN MixingStation.CMConfiguration cmc on c.CMConfigurationId=cmc.Id
----------------------------------------------------------------------------------------
JOIN CTE_Package pdM on rpds.PackageId=pdM.PackageId and pdM.MainMedicine=1
left JOIN CTE_Package pdV on rpds.PackageId=pdV.PackageId and pdV.Vehicle=1
LEFT JOIN CTE_Package pdT on rpds.PackageId = pdt.PackageId AND pdT.Thinner = 1
--JOIN MixingStation.Package p on rms.PackageId=p.Id
JOIN Inventory.InventoryProduct ip ON pdM.ProductId=ip.Id
LEFT JOIN CTE_PackagePersonalizedDetail ppdM on rpds.PackagePersonalizedId=ppdM.PackagePersonalizedId and ppdM.MainMedicine=1
LEFT JOIN CTE_PackagePersonalizedDetail ppdV on rpds.PackagePersonalizedId=ppdV.PackagePersonalizedId and ppdV.Vehicle=1
----------------------------------
LEFT JOIN Inventory.BatchSerial bs ON rpds.BatchCode=bs.BatchCode
LEFT JOIN MedicalHistory.DetailPhysicalCUM dpc ON dpc.BatchCode = rpds.BatchCode
LEFT JOIN .HCDEVMEDD devf ON devf.IdDetailPhysicalCUM = dpc.Id
LEFT JOIN Payroll.FunctionalUnit fu ON devf.UFUCODIGO = fu.Code
LEFT JOIN .HCMOANULB mot ON devf.IdHCMOANULB = mot.CODMOTANU
LEFT JOIN .INPACIENT pac ON devf.IPCODPACI = pac.IPCODPACI
LEFT JOIN MixingStation.UnitDoseType udt ON rms.UnitDoseTypeId = udt.Id
WHERE (r.IsReadjustment = 0 AND r.status = 0) or (r.IsReadjustment = 1 AND r.status IN (0,1))
----------------------------------