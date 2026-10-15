CREATE VIEW [Maintenance].[ViewWorkOrderReport]
AS
WITH ProtocolActivitiesSummary AS
(
    SELECT
        pa.MaintenanceProtocolId,
        CAST(
            SUM(
                CASE 
                    WHEN pa.Unit = 1 THEN pa.Time / 60.0
                    WHEN pa.Unit = 2 THEN pa.Time * 1.0
                    WHEN pa.Unit = 3 THEN pa.Time * 24.0
                    ELSE 0.0
                END
            ) AS DECIMAL(10,2)
        ) AS Hours
    FROM Maintenance.ProtocolActivities pa WITH (NOLOCK)
    GROUP BY pa.MaintenanceProtocolId
),
ProtocolActivitiesText AS
(
    SELECT
        pa.MaintenanceProtocolId,
        LEFT(
            STUFF(
                (
                    SELECT ' - ' + a.Activity
                    FROM Maintenance.ProtocolActivities a WITH (NOLOCK)
                    WHERE a.MaintenanceProtocolId = pa.MaintenanceProtocolId
                    ORDER BY a.Id
                    FOR XML PATH(''), TYPE
                ).value('.', 'NVARCHAR(MAX)'),
                1,
                3,
                ''
            ),
            1000
        ) AS Activity
    FROM Maintenance.ProtocolActivities pa WITH (NOLOCK)
    GROUP BY pa.MaintenanceProtocolId
),
NextProgramatedDate AS
(
    SELECT
        pp.FixedAssetPhysicalId,
        pp.MaintenancePlanAndMetrologyId,
        MIN(pp.DateProgramated) AS DateProgramated
    FROM Maintenance.MaintenancePlanProgramated pp WITH (NOLOCK)
    WHERE pp.DateProgramated >= Common.GETDATE() AND pp.ProgramType = 1
    GROUP BY
        pp.FixedAssetPhysicalId,
        pp.MaintenancePlanAndMetrologyId
)
SELECT
    wo.Id,
    wo.Consecutive AS WorkOrderCode,
    wo.RequestDate,
    wo.ProgramDate,
    wo.State AS WorkOrderState,
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
    fapa.Id AS PhysicalAssetId,
    fapa.Plate,
    fapa.Serie,
    fapa.Model,
    fai.Id AS ItemId,
    fai.ItemTypeId,
    CONCAT(fai.Code, ' - ', fai.Description) AS ItemCodeName,
    CONCAT(fapac.Code, ' - ', fapac.Name) AS PartCodeDescription,
    fait.InventoryTypeId,
    fal.Id AS LocationId,
    CONCAT(fal.Code, ' - ', fal.Name) AS LocationCodeName,
    bo.Id AS BranchOfficeId,
    CONCAT(bo.Code, ' - ', bo.Name) AS BranchOfficeCodeName,
    CONCAT(fat.Code, ' - ', fat.Name) AS TrademarkCodeName,
    far.Id AS FixedAssetResponsibleId,
    CONCAT(ftp.Nit, ' - ', ftp.Name) AS FixedAssetResponsibleCodeName,
    u.UserCode AS FixedAssetUserCode,
    CONCAT(mfr.Observation, ' - ', mfrd.Description) AS ObservationFailure,
    IIF(
        mfr.Report = 1,
        CONCAT(tp.Nit, ' - ', tp.Name),
        mfr.NameOther
    ) AS Requested,
    requesterPosition.Name AS Position,
    tp.DigitalSignature AS DigitalSignature,
    ISNULL(pas.Hours, 0.00) AS Hours,
    IIF(
        wo.State = 4,
        CONCAT(tpre.Nit, ' - ', tpre.Name),
        NULL
    ) AS Received,
    receiverPosition.Name AS PositionReceived,
    REPLACE(
        REPLACE(ISNULL(pat.Activity, ''), CHAR(13), ''),
        CHAR(10),
        ''
    ) AS Activity,
    mpp.Id AS ProgramatedDateId,
    previousMaintenance.DateProgramated AS InitialDateMaintenance,
    npd.DateProgramated AS EndDateMaintenance
FROM Maintenance.WorkOrder wo WITH (NOLOCK)
LEFT JOIN Maintenance.MaintenanceProtocol mp WITH (NOLOCK) ON wo.ProtocolId = mp.Id
JOIN Maintenance.MaintenanceResponsible mr WITH (NOLOCK) ON wo.MaintenanceResponsibleId = mr.Id
JOIN Common.ThirdParty mtp WITH (NOLOCK) ON mr.ThirdPartyId = mtp.Id
JOIN FixedAsset.FixedAssetPhysicalAsset fapa WITH (NOLOCK) ON wo.PhysicalAssetId = fapa.Id
JOIN FixedAsset.FixedAssetItem fai WITH (NOLOCK) ON fapa.ItemId = fai.Id
JOIN FixedAsset.FixedAssetItemType fait WITH (NOLOCK) ON fai.ItemTypeId = fait.Id
JOIN FixedAsset.FixedAssetLocation fal WITH (NOLOCK) ON fapa.LocationId = fal.Id
JOIN Payroll.FunctionalUnit fu WITH (NOLOCK) ON fal.FunctionalUnitId = fu.Id
JOIN Payroll.BranchOffice bo WITH (NOLOCK) ON fu.BranchOfficeId = bo.Id
JOIN FixedAsset.FixedAssetTrademark fat WITH (NOLOCK) ON fapa.TrademarkId = fat.Id
JOIN FixedAsset.FixedAssetResponsible far WITH (NOLOCK) ON fapa.ResponsibleId = far.Id
JOIN Common.ThirdParty ftp WITH (NOLOCK) ON far.ThirdPartyId = ftp.Id
LEFT JOIN Security.Person sp ON ftp.Nit = sp.Identification
LEFT JOIN Security.[User] u ON sp.Id = u.IdPerson
LEFT JOIN Maintenance.MaintenanceFailureRequestDetail mfrd WITH (NOLOCK) ON wo.EntityId = mfrd.Id
LEFT JOIN Maintenance.MaintenanceFailureRequest mfr WITH (NOLOCK) ON mfrd.MaintenanceFailureRequestId = mfr.Id
LEFT JOIN FixedAsset.FixedAssetPhysicalAssetParts fapap WITH (NOLOCK) ON mfrd.PhysicalAssetPartsId = fapap.Id
LEFT JOIN FixedAsset.FixedAssetPartsAccesoriesConsumables fapac WITH (NOLOCK) ON fapap.PartAccesoriesConsumiblesId = fapac.Id
LEFT JOIN Common.ThirdParty tp WITH (NOLOCK) ON mfr.CreationUser = tp.Nit
OUTER APPLY
(
    SELECT TOP 1
        p.Name
    FROM Payroll.Employee e WITH (NOLOCK)
    JOIN Payroll.Contract c WITH (NOLOCK) ON e.Id = c.EmployeeId
    JOIN Payroll.Position p WITH (NOLOCK) ON c.PositionId = p.Id
    WHERE e.ThirdPartyId = tp.Id
      AND c.ContractInitialDate <= CAST(wo.RequestDate AS DATE)
      AND (
            c.ContractEndingDate IS NULL
            OR c.ContractEndingDate >= CAST(wo.RequestDate AS DATE)
          )
    ORDER BY c.ContractInitialDate DESC
) requesterPosition
LEFT JOIN ProtocolActivitiesSummary pas ON mp.Id = pas.MaintenanceProtocolId
LEFT JOIN Common.ThirdParty tpre WITH (NOLOCK) ON wo.ModificationUser = tpre.Nit
OUTER APPLY
(
    SELECT TOP 1
        p.Name
    FROM Payroll.Employee e WITH (NOLOCK)
    JOIN Payroll.Contract c WITH (NOLOCK) ON e.Id = c.EmployeeId
    JOIN Payroll.Position p WITH (NOLOCK) ON c.PositionId = p.Id
    WHERE e.ThirdPartyId = tpre.Id
      AND c.ContractInitialDate <= CAST(wo.RequestDate AS DATE)
      AND (
            c.ContractEndingDate IS NULL
            OR c.ContractEndingDate >= CAST(wo.RequestDate AS DATE)
          )
    ORDER BY c.ContractInitialDate DESC
) receiverPosition

LEFT JOIN NextProgramatedDate npd ON npd.FixedAssetPhysicalId = fapa.Id
LEFT JOIN Maintenance.MaintenancePlanAndMetrology mpm WITH (NOLOCK) ON npd.MaintenancePlanAndMetrologyId = mpm.Id
LEFT JOIN Maintenance.MaintenancePlanProgramated mpp WITH (NOLOCK) ON npd.DateProgramated = mpp.DateProgramated
   AND mpp.FixedAssetPhysicalId = fapa.Id
   AND mpp.MaintenancePlanAndMetrologyId = npd.MaintenancePlanAndMetrologyId
   AND mpp.ProgramType = 1
OUTER APPLY
(
    SELECT TOP 1 mppPrevious.DateProgramated
    FROM Maintenance.MaintenancePlanProgramated mppPrevious WITH (NOLOCK)
    WHERE mppPrevious.MaintenancePlanAndMetrologyId = mpm.Id
      AND mppPrevious.FixedAssetPhysicalId = fapa.Id
      AND mppPrevious.DateProgramated < Common.GETDATE()
      AND mppPrevious.ProgramType = 1
    ORDER BY mppPrevious.DateProgramated DESC
) previousMaintenance
LEFT JOIN ProtocolActivitiesText pat ON mp.Id = pat.MaintenanceProtocolId;