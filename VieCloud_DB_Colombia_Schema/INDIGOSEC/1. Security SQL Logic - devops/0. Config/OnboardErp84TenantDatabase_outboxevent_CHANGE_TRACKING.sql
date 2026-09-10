/*
================================================================================
 OnboardErp84TenantDatabaseUsers.sql
================================================================================
 Script operativo (NO DACPAC) para el onboarding de la BASE TENANT del ticket
 ERP-84 (infra provisionada por DevOps con Terraform el 2026-07-14).

 Habilita Change Tracking (si no esta activo) y crea los 3 usuarios de Azure AD
 (Managed Identity System-Assigned = nombre de la Function App) con los grants
 documentados en erp84-resultado.json → pendiente.sql:

   - ehrco-relay-dev-erp84d          -> SELECT + VIEW CHANGE TRACKING en
                                        Clinical.OutboxEvent y Hospitalization.OutboxEvent
   - ehrco-normalizer-dev-erp84d     -> SELECT ON SCHEMA::Clinical
   - ehrco-authz-dev-erp84d          -> SELECT ON SCHEMA::Clinical +
                                        SELECT/INSERT/UPDATE en Authorization.AuthorizationControl,
                                        Authorization.AuthorizationControlTrace,
                                        Authorization.ProcessedInbox

 CUANDO EJECUTAR
 ---------------
 Una vez por base tenant (empezar por el tenant de prueba/DEV), DESPUES de
 OnboardErp84ControlPlaneUsers.sql y ANTES del smoke test end-to-end.

 DONDE EJECUTAR
 --------------
 Base del TENANT (la que tiene Clinical.OutboxEvent / Hospitalization.OutboxEvent /
 Authorization.*). NO ejecutar en INDIGOSEC.

 IDEMPOTENCIA
 ------------
 Change Tracking y CREATE USER guardados con checks de existencia. Seguro
 re-ejecutar (por ejemplo si ya se corrio ChangeTracking-enablement.sql de RDA
 en la misma base, este script detecta que la BD ya tiene CT activo y no
 falla).
================================================================================
*/

-- ------------------------------------------------------------------
-- 1. Change Tracking a nivel de base (no falla si ya esta activo).
-- ------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.change_tracking_databases
    WHERE database_id = DB_ID()
)
BEGIN
    ALTER DATABASE CURRENT SET CHANGE_TRACKING = ON
        (CHANGE_RETENTION = 7 DAYS, AUTO_CLEANUP = ON);
    PRINT 'Change Tracking habilitado a nivel de base.';
END
ELSE
BEGIN
    PRINT 'Change Tracking ya estaba habilitado a nivel de base (sin cambios).';
END
GO

-- ------------------------------------------------------------------
-- 2. Change Tracking sobre las 2 tablas outbox que consume el relay.
-- ------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM sys.change_tracking_tables
    WHERE object_id = OBJECT_ID('Clinical.OutboxEvent')
)
BEGIN
    ALTER TABLE [Clinical].[OutboxEvent] ENABLE CHANGE_TRACKING
        WITH (TRACK_COLUMNS_UPDATED = OFF);
    PRINT 'Change Tracking habilitado en Clinical.OutboxEvent.';
END
ELSE
    PRINT 'Clinical.OutboxEvent ya tenia Change Tracking (sin cambios).';
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.change_tracking_tables
    WHERE object_id = OBJECT_ID('Hospitalization.OutboxEvent')
)
BEGIN
    ALTER TABLE [Hospitalization].[OutboxEvent] ENABLE CHANGE_TRACKING
        WITH (TRACK_COLUMNS_UPDATED = OFF);
    PRINT 'Change Tracking habilitado en Hospitalization.OutboxEvent.';
END
ELSE
    PRINT 'Hospitalization.OutboxEvent ya tenia Change Tracking (sin cambios).';
GO

