CREATE view [MixingStation].[ViewListFinalControlProduct] 
as

WITH Cte_requestDetail AS (
	SELECT  psms.Id,
			psms.CodeSusceptibleMixingStation,
			psms.MainDrugCode,
			udt.MSClass,
			pd.IDHCFARMEPC,
			hpd.SourceTable,
			hpd.IdSourceTable
	FROM MedicalHistory.ProductSusceptibleMixingStation psms
	JOIN MedicalHistory.PharmaDose pd ON psms.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation
	JOIN MixingStation.UnitDoseType udt ON pd.UnitDoseTypeId = udt.Id
	JOIN HCFARMEPD hpd ON psms.CodeSusceptibleMixingStation = hpd.CodeSusceptibleMixingStation AND hpd.SENDTO = 2
	GROUP BY psms.Id,
			psms.CodeSusceptibleMixingStation,
			psms.MainDrugCode,
			udt.MSClass,
			pd.IDHCFARMEPC,
			hpd.SourceTable,
			hpd.IdSourceTable
),

request_data AS (
	SELECT DISTINCT b.EntityId, b.EntityName
		, b.RequestMixingStationDetailId		
		, pc.CODBODEGA
		, wh.ID As WarehouseId
		, Concat(wh.Code, ' - ', wh.name) WarehouseCodeName
		, adc.NOMCENATE
		, inu.UFUDESCRI
		, inp.IPCODPACI
		, inp.IPNOMCOMP  
	FROM MixingStation.RequestMixingStationDetailPatients b
	JOIN Cte_requestDetail cte_r ON b.EntityId = cte_r.Id AND cte_r.MSClass NOT IN (2)
	JOIN dbo.HCFARMEPD pd ON cte_r.CodeSusceptibleMixingStation = pd.CodeSusceptibleMixingStation and cte_r.MainDrugCode = pd.CODPRODUC
	JOIN dbo.HCFARMEPC pc ON pd.CODCONCEC = pc.CODCONCEC
	JOIN dbo.INPACIENT inp ON B.PatientCode = inp.IPCODPACI
	JOIN Inventory.Warehouse wh WITH (NOLOCK) ON pc.CODBODEGA = wh.Code
	JOIN dbo.ADCENATEN adc ON pc.CODCENATE = adc.CODCENATE
	JOIN dbo.INUNIFUNC inu ON pc.UFUCODIGO = inu.UFUCODIGO
	WHERE B.EntityName = 'ProductSusceptibleMixingStation'
),
Cte_MainDataNPT AS(
	SELECT b.EntityId, b.EntityName
		, b.RequestMixingStationDetailId		
		, hc.CODBODEGA
		, wh.ID As WarehouseId
		, Concat(wh.Code, ' - ', wh.name) WarehouseCodeName
		, adc.NOMCENATE
		, inu.UFUDESCRI
		, inp.IPCODPACI
		, inp.IPNOMCOMP
		, cte_r.IDHCFARMEPC
	FROM MixingStation.RequestMixingStationDetailPatients b
	JOIN Cte_requestDetail cte_r ON b.EntityId = cte_r.Id AND cte_r.MSClass IN (2)
	JOIN HCNUTPAREC n WITH (NOLOCK) ON cte_r.IdSourceTable = n.ID
	JOIN HCPARNUTC nc WITH (NOLOCK) ON n.IDHCPARNUTC = nc.Id
	JOIN HCFARMEPC hc WITH(NOLOCK) ON cte_r.IDHCFARMEPC = hc.CODCONCEC
	JOIN dbo.INPACIENT inp ON B.PatientCode = inp.IPCODPACI
	JOIN Inventory.Warehouse wh WITH (NOLOCK) ON hc.CODBODEGA = wh.Code
	JOIN dbo.ADCENATEN adc ON hc.CODCENATE = adc.CODCENATE
	JOIN dbo.INUNIFUNC inu ON hc.UFUCODIGO = inu.UFUCODIGO
	WHERE cte_r.MainDrugCode = nc.FinishedProductCode 
		AND  B.EntityName = 'ProductSusceptibleMixingStation'
)

SELECT 
	rpds.Id,
	NULL RequestPackageDetailStatusIds,
	SP.Fullname UserName, 
	ps.Code ProductionScheduleCode,
	inp.id ItemId,
	inp.Code ItemCode,  
	inp.Name ItemCodeName,
	bs.Id BatchSerialId,
	bs.BatchCode BatchCode,
	IIF(wh.Id is NULL, rd.WarehouseId, wh.Id) DispensingWarehouseId,
	IIF(wh.Id is Null, rd.WarehouseCodeName, wh.Code + ' - ' + wh.Name) DispensingWarehouseCodeName,
	rmsd.Source,
	CASE rmsd.Source
		WHEN 1 THEN 'Orden Médica'
		WHEN 2 THEN 'Solicitud Externa Paciente'
		WHEN 3 THEN 'Solicitud Externa Maquila'
		WHEN 4 THEN 'Solicitud Inventario'
		ELSE ''
	END AS SourceName,
	rd.EntityId HCFARMEPDId,
	rd.NOMCENATE,
	rd.UFUDESCRI,
	rd.IPCODPACI,
	rd.IPNOMCOMP,
	CASE rpds.status
            WHEN '3' THEN 'Producto Liberado'
            WHEN '4' THEN 'Producto Reproceso'
			WHEN '6' THEN 'Producto Anulado'
         End  As RequestPackageDetailstatus,
	cd.CampaignNumber,
	phi.Quantity AS InventoryQuantity,
	phi.WarehouseId AS PhysicalInventoryWarehouseId,
	cd.Id CampaignDetailId,
	inp.ProductCost,
	pud.Name AS PackageUnitDescription,
	rpds.PhysicalInventoryId,
	rpds.RequestMixingStationDetailId,
	rmsd.ProductionLineId,
	ca.CMconfigurationId,
	rms.RequestDate,
	ISNULL(pp.Code + ' - ' + pp.Description, p.Code + ' - ' + p.Description) PackageDescription
FROM MixingStation.RequestPackageDetailStatus rpds WITH (NOLOCK)
JOIN MixingStation.RequestMixingStationDetail rmsd WITH (NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.id
JOIN MixingStation.UnitDoseType ud ON ud.Id = rmsd.UnitDoseTypeId
JOIN MixingStation.RequestMixingStation rms WITH (NOLOCK) ON rmsd.RequestMixingStationId = rms.Id
JOIN MixingStation.CampaignDetailUsers cdu WITH (NOLOCK) ON rmsd.CampaignDetailId = cdu.CampaignDetailId And cdu.UserRole = 2
JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON rmsd.CampaignDetailId  = cd.Id
JOIN MixingStation.Campaign ca WITH (NOLOCK) ON cd.CampaignId = ca.Id
JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON rpds.PhysicalInventoryId = phi.Id
JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
JOIN Inventory.InventoryProduct inp WITH (NOLOCK) ON phi.ProductId = inp.Id
LEFT JOIN request_data rd ON rd.RequestMixingStationDetailId = rmsd.id and rmsd.[Source] = 1
LEFT JOIN (
	Select WarehouseId, ruid.Id from MixingStation.RequestUnitDoseInventoryDetail AS ruid
	join MixingStation.RequestUnitDoseInventory rudi ON rudi.Id = ruid.RequestUnitDoseInventoryId
) ru ON ru.Id = rmsd.EntityId
LEFT JOIN Security.Person AS SP ON cdu.UserId = SP.Id 
LEFT JOIN MixingStation.PackagePersonalized pp WITH (NOLOCK) ON rpds.PackagePersonalizedId = pp.Id
LEFT JOIN MixingStation.Package p with(nolock) on pp.AssociatedPackageId = p.id
LEFT JOIN MixingStation.ProductionScheduleDetail psd WITH (NOLOCK) ON cd.Id = psd.CampaignDetailId
LEFT JOIN MixingStation.ProductionSchedule ps WITH (NOLOCK) ON psd.ProductionScheduleId = ps.Id
LEFT JOIN Inventory.Warehouse wh WITH (NOLOCK) ON  wh.Id = COALESCE(rpds.DispensingWarehouseId, ru.WarehouseId)
LEFT JOIN Inventory.PackagingUnit pud WITH (NOLOCK) On inp.PackagingUnitId = pud.Id
WHERE rpds.status IN (3,4,6) AND rpds.QualityStatus = 1 and cd.CampaignStatus = 6 
		AND rpds.SendTo = 0 AND ud.MSClass NOT IN (2,5,7) --Se inabilita porque pueden venir readecuaciones por lo tanto ya se ha realizado una orden de translado

UNION ALL

	SELECT 
		rmsd.Id,
		STRING_AGG(TRY_CONVERT(VARCHAR(MAX), rpds.Id), ',') RequestPackageDetailStatusIds,
		SP.Fullname UserName, 
		ps.Code ProductionScheduleCode,
		inp.id ItemId,
		inp.Code ItemCode,  
		inp.Name ItemCodeName,
		bs.Id BatchSerialId,
		bs.BatchCode BatchCode,
		IIF(wh.Id is NULL, rd.WarehouseId, wh.Id) DispensingWarehouseId,
		IIF(wh.Id is Null, rd.WarehouseCodeName, wh.Code + ' - ' + wh.Name) DispensingWarehouseCodeName,
		rmsd.Source,
		CASE rmsd.Source
			WHEN 1 THEN 'Orden Médica'
			WHEN 2 THEN 'Solicitud Externa Paciente'
			WHEN 3 THEN 'Solicitud Externa Maquila'
			WHEN 4 THEN 'Solicitud Inventario'
			ELSE ''
		END AS SourceName,
		rd.EntityId HCFARMEPDId,
		rd.NOMCENATE,
		rd.UFUDESCRI,
		rd.IPCODPACI,
		rd.IPNOMCOMP,
		CASE rpds.status
				WHEN '3' THEN 'Producto Liberado'
				WHEN '4' THEN 'Producto Reproceso'
				WHEN '6' THEN 'Producto Anulado'
			 End  As RequestPackageDetailstatus,
		cd.CampaignNumber,
		phi.Quantity AS InventoryQuantity,
		phi.WarehouseId AS PhysicalInventoryWarehouseId,
		cd.Id CampaignDetailId,
		inp.ProductCost,
		pud.Name AS PackageUnitDescription,
		rpds.PhysicalInventoryId,
		rpds.RequestMixingStationDetailId,
		rmsd.ProductionLineId,
		ca.CMconfigurationId,
		rms.RequestDate,
		NULL PackageDescription
	FROM MixingStation.RequestMixingStationDetail rmsd WITH (NOLOCK)
	JOIN MixingStation.RequestPackageDetailStatus rpds WITH (NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
	JOIN MixingStation.UnitDoseType ud ON ud.Id = rmsd.UnitDoseTypeId
	JOIN MixingStation.RequestMixingStation rms WITH (NOLOCK) ON rmsd.RequestMixingStationId = rms.Id
	JOIN MixingStation.CampaignDetailUsers cdu WITH (NOLOCK) ON rmsd.CampaignDetailId = cdu.CampaignDetailId And cdu.UserRole = 2
	JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON rmsd.CampaignDetailId  = cd.Id
	JOIN MixingStation.Campaign ca WITH (NOLOCK) ON cd.CampaignId = ca.Id
	JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON rpds.PhysicalInventoryId = phi.Id
	JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
	JOIN Inventory.InventoryProduct inp WITH (NOLOCK) ON phi.ProductId = inp.Id
	LEFT JOIN request_data rd ON rd.RequestMixingStationDetailId = rmsd.id and rmsd.[Source] = 1
	LEFT JOIN (
		Select WarehouseId, ruid.Id from MixingStation.RequestUnitDoseInventoryDetail AS ruid
		join MixingStation.RequestUnitDoseInventory rudi ON rudi.Id = ruid.RequestUnitDoseInventoryId
	) ru ON ru.Id = rmsd.EntityId
	LEFT JOIN Security.Person AS SP ON cdu.UserId = SP.Id 
	LEFT JOIN MixingStation.ProductionScheduleDetail psd WITH (NOLOCK) ON cd.Id = psd.CampaignDetailId
	LEFT JOIN MixingStation.ProductionSchedule ps WITH (NOLOCK) ON psd.ProductionScheduleId = ps.Id
	LEFT JOIN Inventory.Warehouse wh WITH (NOLOCK) ON  wh.Id = COALESCE(rpds.DispensingWarehouseId, ru.WarehouseId)
	LEFT JOIN Inventory.PackagingUnit pud WITH (NOLOCK) On inp.PackagingUnitId = pud.Id
	WHERE rpds.status IN (3,4,6) AND rpds.QualityStatus = 1 and cd.CampaignStatus = 6 
			AND rpds.SendTo = 0 AND ud.MSClass IN (5,7) --Se inabilita porque pueden venir readecuaciones por lo tanto ya se ha realizado una orden de translado
	GROUP BY	rmsd.Id, SP.Fullname, ps.Code, inp.Id, inp.Code, inp.Name, bs.Id, bs.BatchCode, wh.Id,
				rd.WarehouseId, rd.WarehouseCodeName, wh.Code, wh.Name, rmsd.Source, rd.EntityId, 
				rd.NOMCENATE, rd.UFUDESCRI, rd.IPCODPACI, rd.IPNOMCOMP, rpds.Status, cd.CampaignNumber, 
				phi.Quantity, phi.WarehouseId, cd.Id, inp.ProductCost, pud.Name, rpds.RequestMixingStationDetailId,
				rmsd.ProductionLineId, ca.CMConfigurationId, rms.RequestDate, rpds.PhysicalInventoryId

UNION ALL

SELECT 
		rmsd.Id,
		STRING_AGG(TRY_CONVERT(VARCHAR(MAX), rpds.Id), ',') RequestPackageDetailStatusIds,
		cduQP.Nombre UserName, 
		ps.Code ProductionScheduleCode,
		inp.id ItemId,
		inp.Code ItemCode,  
		inp.Name ItemCodeName,
		bs.Id BatchSerialId,
		bs.BatchCode BatchCode,
		cte_n.WarehouseId DispensingWarehouseId,
		cte_n.WarehouseCodeName DispensingWarehouseCodeName,
		rmsd.Source,
		CASE rmsd.Source
			WHEN 1 THEN 'Orden Médica'
			WHEN 2 THEN 'Solicitud Externa Paciente'
			WHEN 3 THEN 'Solicitud Externa Maquila'
			WHEN 4 THEN 'Solicitud Inventario'
			ELSE ''
		END AS SourceName,
		cte_n.EntityId HCFARMEPDId,
		cte_n.NOMCENATE,
		cte_n.UFUDESCRI,
		cte_n.IPCODPACI,
		cte_n.IPNOMCOMP,
		CASE rpds.status
				WHEN '3' THEN 'Producto Liberado'
				WHEN '4' THEN 'Producto Reproceso'
				WHEN '6' THEN 'Producto Anulado'
			 End  As RequestPackageDetailstatus,
		cd.CampaignNumber,
		phi.Quantity AS InventoryQuantity,
		phi.WarehouseId AS PhysicalInventoryWarehouseId,
		cd.Id CampaignDetailId,
		inp.ProductCost,
		pud.Name AS PackageUnitDescription,
		rpds.PhysicalInventoryId,
		rpds.RequestMixingStationDetailId,
		rmsd.ProductionLineId,
		c.CMconfigurationId,
		rms.RequestDate,
		COALESCE(CONCAT(pp.Code, ' - ', pp.Name), CONCAT(p.Code, ' - ', p.Name),'') PackageDescription
FROM MixingStation.RequestMixingStationDetail rmsd WITH (NOLOCK)
JOIN MixingStation.RequestMixingStation rms WITH(NOLOCK) ON rmsd.RequestMixingStationId = rms.Id
JOIN MixingStation.CampaignDetail cd WITH (NOLOCK) ON rmsd.CampaignDetailId = cd.Id
JOIN MixingStation.Campaign c WITH(NOLOCK) ON cd.CampaignId = c.Id
JOIN MixingStation.RequestPackageDetailStatus rpds WITH (NOLOCK) ON rpds.RequestMixingStationDetailId = rmsd.Id
JOIN MixingStation.UnitDoseType ud ON ud.Id = rmsd.UnitDoseTypeId
JOIN Inventory.PhysicalInventory phi WITH (NOLOCK) ON rpds.PhysicalInventoryId = phi.Id
JOIN Inventory.InventoryProduct inp WITH (NOLOCK) ON phi.ProductId = inp.Id
JOIN Inventory.PackagingUnit pud WITH (NOLOCK) On inp.PackagingUnitId = pud.Id
JOIN Inventory.BatchSerial bs WITH (NOLOCK) ON phi.BatchSerialId = bs.Id
JOIN [MixingStation].[ViewUsersRolsCM] cduQP (NOLOCK) ON cd.Id = cduQP.CampaignDetailId AND cduQp.UserRole = 2 -- QF Producción 
JOIN MixingStation.ProductionScheduleDetail psd WITH (NOLOCK) ON cd.Id = psd.CampaignDetailId
JOIN MixingStation.ProductionSchedule ps WITH (NOLOCK) ON psd.ProductionScheduleId = ps.Id
JOIN Cte_MainDataNPT cte_n ON rpds.RequestMixingStationDetailId = cte_n.RequestMixingStationDetailId  and rmsd.[Source] = 1
LEFT JOIN MixingStation.Package p ON rmsd.PackageId = p.Id
LEFT JOIN MixingStation.PackagePersonalized pp ON rmsd.PackagePersonalizedId = pp.Id
WHERE rpds.status IN (3,4,6) AND rpds.QualityStatus = 1 and cd.CampaignStatus = 6 
	AND rpds.SendTo = 0 AND ud.MSClass IN (2) --Se inabilita porque pueden venir readecuaciones por lo tanto ya se ha realizado una orden de translado
GROUP BY rmsd.Id,
		rpds.Id,
		cduQP.Nombre, 
		ps.Code,
		inp.id,
		inp.Code,  
		inp.Name,
		bs.Id,
		bs.BatchCode,
		cte_n.WarehouseId,
		cte_n.WarehouseCodeName,
		rmsd.Source,
		cte_n.EntityId,
		cte_n.NOMCENATE,
		cte_n.UFUDESCRI,
		cte_n.IPCODPACI,
		cte_n.IPNOMCOMP,
		rpds.status,
		cd.CampaignNumber,
		phi.Quantity,
		phi.WarehouseId,
		cd.Id,
		inp.ProductCost,
		pud.Name,
		rpds.PhysicalInventoryId,
		rpds.RequestMixingStationDetailId,
		rmsd.ProductionLineId,
		c.CMconfigurationId,
		rms.RequestDate,
		p.Code, p.Name,
		pp.Code, pp.Name