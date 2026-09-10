/*
================================================================================
 Verify-AdmissionOutboxCursor.sql
================================================================================
 Verificacion rapida post-onboarding de Admissions.OutboxEvent.
 Primera consulta: ejecutar en la BASE TENANT (confirma filas/estado de Change
 Tracking). Segunda consulta: ejecutar en el CONTROL PLANE (confirma el cursor
 del relay para ese tenant/outbox). Tercera consulta: ejecutar en la BASE TENANT
 (confirma que el log de asignacion esta recibiendo filas).
================================================================================
*/

-- 1) BASE TENANT — filas en la outbox y version actual de Change Tracking.
SELECT TOP 100 *
FROM   [Admissions].[OutboxEvent]
ORDER BY [OutboxId] DESC;

SELECT CHANGE_TRACKING_CURRENT_VERSION() AS CurrentChangeTrackingVersion;

SELECT
    OBJECT_SCHEMA_NAME(ct.object_id) AS SchemaName,
    OBJECT_NAME(ct.object_id)        AS TableName,
    ct.is_track_columns_updated_on,
    ct.min_valid_version
FROM sys.change_tracking_tables ct
WHERE ct.object_id = OBJECT_ID(N'[Admissions].[OutboxEvent]');

-- 2) CONTROL PLANE — cursor del relay para el tenant/outbox (ajustar @TenantId).
-- DECLARE @TenantId UNIQUEIDENTIFIER = '3E9BF075-2FC3-4151-A747-B4B7D848D24A'; -- DEV-COLOMBIA
-- SELECT *
-- FROM   [Platform].[TenantOutboxCursor]
-- WHERE  [OutboxName] = 'Admissions.OutboxEvent'
--   AND  [TenantId]   = @TenantId;

-- 3) BASE TENANT — asignaciones vigentes registradas por el consumer de
--    distribucion de autorizadores (confirma que el flujo completo funciono
--    de punta a punta: outbox -> relay -> normalizer -> consumer).
SELECT TOP 100 *
FROM   [Authorization].[AdmissionAuthorizerAssignmentLog]
WHERE  [IsCurrent] = 1
ORDER BY [AssignmentDate] DESC;
