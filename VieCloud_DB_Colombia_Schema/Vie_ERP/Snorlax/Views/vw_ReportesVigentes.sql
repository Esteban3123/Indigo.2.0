CREATE   VIEW [Snorlax].[vw_ReportesVigentes]
AS
SELECT
    t.[PatientCode],
    t.[AdmissionNumber],
    t.[FolioNumber],
    t.[StoryType],
    t.[ReportType],
    t.[ClinicalHistoryCode],
    t.[CareCenter],
    t.[BlobUrl],
    t.[CompletedAtUtc],
    t.[EventId]
FROM [Snorlax].[ReportTracking] AS t
WHERE t.[Status] = 'Completed'
  AND NOT EXISTS (
        SELECT 1
        FROM [Snorlax].[ReportTracking] AS mas_nuevo
        WHERE mas_nuevo.[Status] = 'Completed'
          AND mas_nuevo.[PatientCode] = t.[PatientCode]
          AND mas_nuevo.[AdmissionNumber] = t.[AdmissionNumber]
          AND mas_nuevo.[FolioNumber] = t.[FolioNumber]
          AND mas_nuevo.[ReportType] = t.[ReportType]
          AND mas_nuevo.[CompletedAtUtc] > t.[CompletedAtUtc]);
GO


