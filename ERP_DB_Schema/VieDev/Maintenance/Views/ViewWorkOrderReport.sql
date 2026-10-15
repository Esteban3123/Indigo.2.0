




CREATE VIEW [Maintenance].[ViewWorkOrderReport]
AS
	SELECT	wo.Id,
			wo.Consecutive AS WorkOrderCode,
			wo.RequestDate AS RequestDate,
			wo.ProgramDate AS ProgramDate,
			wo.State WorkOrderState,
			CASE wo.State
				WHEN 1 THEN 'Registrado'
				WHEN 2 THEN 'Confirmado'
				WHEN 3 THEN 'Anulado'
				WHEN 4 THEN 'Aprobado'
				WHEN 5 THEN 'Rechazado'
			END AS WorkOrderStateName,
			mp.Id AS ProtocolId,
			CONCAT(mp.Code, ' - ', mp.Name) AS ProtocolCodeName,
			mr.Id AS MaintenanceResponsibleId,
			mr.ReponsibleTypeId,
			CONCAT(mtp.Nit, ' - ', mtp.Name) AS MaintenanceResponsibleCodeName,
			mr.ResponsibleRole,
			-------------------------------------------------------------------
			fapa.Id AS PhysicalAssetId,
			fapa.Plate,
			fapa.Serie,
			fapa.Model,
			fai.Id AS ItemId,
			fai.ItemTypeId,
			CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
			CONCAT(fapac.Code, ' - ', fapac.Name) PartCodeDescription,
			fait.InventoryTypeId,
			fal.Id AS LocationId,
			CONCAT(fal.code, ' - ', fal.Name) AS LocationCodeName,
			bo.Id AS BranchOfficeId,
			CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
			CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
			far.Id AS FixedAssetResponsibleId,
			CONCAT(ftp.Nit, ' - ', ftp.Name) AS FixedAssetResponsibleCodeName,
			u.UserCode FixedAssetUserCode,
			-------------------------------------------------------------------
			CONCAT(mfr.Observation,' - ',mfrd.Description) AS ObservationFailure,
			Iif(mfr.Report = 1,CONCAT(tp.Nit,' - ',tp.Name),mfr.NameOther) AS Requested,
			p.Name AS Position,
			tp.DigitalSignature As DigitalSignature,
			CAST(ProtocolActivities.Hours AS DECIMAL(10,2)) As Hours,
			Iif(wo.State = 4,CONCAT(tpre.Nit,' - ',tpre.Name), NULL) AS Received,
			pp.Name AS PositionReceived,
			REPLACE(Activities.Activity,'&#x0D;','') AS Activity,
			--------------------------------------
			mpp.Id AS ProgramatedDateId,
			(
				SELECT TOP 1 DateProgramated 
				FROM Maintenance.MaintenancePlanProgramated WITH (NOLOCK)
				WHERE MaintenancePlanAndMetrologyId = mpm.Id AND FixedAssetPhysicalId = fapa.Id AND DateProgramated < Common.GETDATE() AND ProgramType = 1
				ORDER BY DateProgramated DESC
			) AS InitialDateMaintenance, 
			ppd.DateProgramated AS EndDateMaintenance
	FROM Maintenance.WorkOrder wo WITH (NOLOCK)
	LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id	
	JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
	JOIN Common.ThirdParty mtp WITH (NOLOCK) ON mr.ThirdPartyId = mtp.Id
	---------------------------------------------------------------------------
	JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
	JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
	JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
	JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
	JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
	JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
	JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
	JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
	JOIN Common.ThirdParty ftp WITH (NOLOCK) ON far.ThirdPartyId = ftp.Id
	---------------------------------------------------------------------------
	LEFT JOIN Security.Person sp ON ftp.Nit = sp.Identification
	LEFT JOIN Security.[User] u ON sp.Id = u.IdPerson
	---------------------------------------------------------------------------	
	LEFT JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON wo.EntityId = mfrd.Id 
	LEFT JOIN Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK) ON mfrd.MaintenanceFailureRequestId = mfr.Id 	
	LEFT JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
	LEFT JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
		---------------------------------------------------------------------------	
	LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON  mfr.CreationUser = tp.Nit
	LEFT JOIN Payroll.Employee e  WITH (NOLOCK) ON tp.Id = e.ThirdPartyId 
	LEFT JOIN Payroll.Contract c  WITH (NOLOCK) ON e.Id = c.EmployeeId AND c.ContractInitialDate <= CAST(wo.RequestDate AS DATE) AND c.ContractEndingDate <= CAST(wo.RequestDate AS DATE)
	LEFT JOIN Payroll.Position p  WITH (NOLOCK) ON c.PositionId = p.Id 	 	
	---------------------------------------------------------------------------	
	LEFT JOIN
    (
        SELECT  	
				pa.MaintenanceProtocolId,	
				SUM( CASE 
						WHEN pa.Unit  = 1 THEN CAST(pa.Time AS decimal(4,2)) / CAST(60 AS decimal(4,2))
						WHEN pa.Unit  = 2 THEN pa.Time
						WHEN pa.Unit  = 3 THEN pa.Time * 24
						ELSE 0
					END 
				) As Hours					
        FROM  Maintenance.ProtocolActivities pa  WITH (NOLOCK) 
        GROUP BY pa.MaintenanceProtocolId
    ) ProtocolActivities ON mp.Id = MaintenanceProtocolId 
	---------------------------------------------------------------------------	
	LEFT JOIN Common.ThirdParty tpre WITH (NOLOCK) ON  wo.ModificationUser = tpre.Nit
	LEFT JOIN Payroll.Employee ee  WITH (NOLOCK) ON tp.Id = ee.ThirdPartyId 
	LEFT JOIN Payroll.Contract cc  WITH (NOLOCK) ON ee.Id = cc.EmployeeId AND cc.ContractInitialDate <= CAST(wo.RequestDate AS DATE) AND cc.ContractEndingDate <= CAST(wo.RequestDate AS DATE)
	LEFT JOIN Payroll.Position pp  WITH (NOLOCK) ON cc.PositionId = pp.Id 	 	
	---------------------------------------------------------------------------	
	LEFT JOIN
		(
			SELECT MIN(pp.DateProgramated) AS DateProgramated, pp.FixedAssetPhysicalId, pp.MaintenancePlanAndMetrologyId 
			FROM Maintenance.MaintenancePlanProgramated pp WITH (NOLOCK)
			WHERE pp.DateProgramated >= Common.GETDATE() and pp.ProgramType = 1
			GROUP BY FixedAssetPhysicalId, MaintenancePlanAndMetrologyId
		) ppd ON ppd.FixedAssetPhysicalId = fapa.Id
	LEFT JOIN Maintenance.MaintenancePlanAndMetrology mpm WITH (NOLOCK) ON ppd.MaintenancePlanAndMetrologyId = mpm.Id
		LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON ppd.DateProgramated = mpp.DateProgramated and mpp.FixedAssetPhysicalId = fapa.Id
	---------------------------------------------------------------------------	
	LEFT JOIN
    (
        SELECT  pa.MaintenanceProtocolId As ProcotolId,
			SUBSTRING
			( 
				( SELECT  '- ' +a.Activity  				
				 FROM Maintenance.ProtocolActivities a
				 WHERE pa.MaintenanceProtocolId = a.MaintenanceProtocolId
				 ORDER BY pa.MaintenanceProtocolId
				 FOR XML PATH ('')
			), 1, 1000) [Activity]				
        FROM  Maintenance.ProtocolActivities pa  WITH (NOLOCK) 
        GROUP BY pa.MaintenanceProtocolId
    ) Activities  ON mp.Id = ProcotolId