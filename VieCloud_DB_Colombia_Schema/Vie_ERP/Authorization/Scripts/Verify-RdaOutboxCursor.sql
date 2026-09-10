/*
================================================================================
 Verify-RdaOutboxCursor.sql
================================================================================
 Verificacion rapida post-onboarding de rda.OutboxEvent.
 Primera consulta: ejecutar en la BASE TENANT (confirma filas/estado de Change
 Tracking). Segunda consulta: ejecutar en el CONTROL PLANE (confirma el cursor
 del relay para ese tenant/outbox).
================================================================================
*/

-- 1) BASE TENANT — filas en la outbox y version actual de Change Tracking.
SELECT *
FROM   [rda].[OutboxEvent]
ORDER BY [OutboxId] DESC;

SELECT CHANGE_TRACKING_CURRENT_VERSION() AS CurrentChangeTrackingVersion;

SELECT
    OBJECT_SCHEMA_NAME(ct.object_id) AS SchemaName,
    OBJECT_NAME(ct.object_id)        AS TableName,
    ct.is_track_columns_updated_on,
    ct.min_valid_version
FROM sys.change_tracking_tables ct
WHERE ct.object_id = OBJECT_ID(N'[rda].[OutboxEvent]');

-- 2) CONTROL PLANE — cursor del relay para el tenant/outbox (ajustar @TenantId).
-- DECLARE @TenantId UNIQUEIDENTIFIER = '3E9BF075-2FC3-4151-A747-B4B7D848D24A'; -- DEV-COLOMBIA
-- SELECT *
-- FROM   [Platform].[TenantOutboxCursor]
-- WHERE  [OutboxName] = 'rda.OutboxEvent'
--   AND  [TenantId]   = @TenantId;
