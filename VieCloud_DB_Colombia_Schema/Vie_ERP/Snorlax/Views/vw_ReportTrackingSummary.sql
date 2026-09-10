CREATE   VIEW [Snorlax].[vw_ReportTrackingSummary]
AS
SELECT
    CAST([CreatedAtUtc] AS DATE)                                    AS [DiaUtc],
    [ReportType],
    [Status],
    COUNT_BIG(*)                                                    AS [Cantidad],
    AVG(DATEDIFF(MILLISECOND, [CreatedAtUtc], [CompletedAtUtc]))    AS [PromedioMs],
    MAX([Attempts])                                                 AS [MaxIntentos]
FROM [Snorlax].[ReportTracking]
GROUP BY CAST([CreatedAtUtc] AS DATE), [ReportType], [Status];
GO
