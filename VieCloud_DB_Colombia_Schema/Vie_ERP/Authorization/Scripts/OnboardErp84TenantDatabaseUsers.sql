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
                                        Clinical.OutboxEvent, Hospitalization.OutboxEvent
                                        y Billing.OutboxEvent
   - ehrco-normalizer-dev-erp84d     -> SELECT ON SCHEMA::Clinical
   - ehrco-authz-dev-erp84d          -> SELECT ON SCHEMA::Clinical +
                                        SELECT/INSERT/UPDATE en Authorization.AuthorizationControl,
                                        Authorization.AuthorizationControlTrace,
                                        Authorization.ProcessedInbox

 IMPORTANTE — Billing.OutboxEvent (agregado 2026-07-15): esta tabla ahora forma
 parte del DefaultTables de OutboxTableCatalog.cs (el relay la drena por
 defecto). Este script habilita su Change Tracking y sus grants igual que las
 otras 2. Falta ADEMAS agregar 'billing.stay-committed.v1' al app setting
 Relay:AllowedEventTypes de ehrco-relay-dev-erp84d en Azure (fuera del alcance
 de este script SQL) — sin eso el relay lee pero rechaza publicar esos eventos.

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
-- 2. Change Tracking sobre las 3 tablas outbox que consume el relay.
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

IF NOT EXISTS (
    SELECT 1 FROM sys.change_tracking_tables
    WHERE object_id = OBJECT_ID('Billing.OutboxEvent')
)
BEGIN
    ALTER TABLE [Billing].[OutboxEvent] ENABLE CHANGE_TRACKING
        WITH (TRACK_COLUMNS_UPDATED = OFF);
    PRINT 'Change Tracking habilitado en Billing.OutboxEvent.';
END
ELSE
    PRINT 'Billing.OutboxEvent ya tenia Change Tracking (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 3. Usuarios Azure AD (Managed Identity) + grants.
--    CREATE USER via dynamic SQL (no se puede parametrizar el identificador
--    en DDL); cada bloque es idempotente por separado.
-- ------------------------------------------------------------------

-- --- Relay ---------------------------------------------------------
DECLARE @RelayUser NVARCHAR(128) = N'ehrco-relay-dev-erp84d';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @RelayUser)
    EXEC sp_executesql N'CREATE USER [' + @RelayUser + N'] FROM EXTERNAL PROVIDER;';

DECLARE @sqlRelayGrants NVARCHAR(MAX) = N'
GRANT SELECT ON [Clinical].[OutboxEvent] TO [' + @RelayUser + N'];
GRANT VIEW CHANGE TRACKING ON [Clinical].[OutboxEvent] TO [' + @RelayUser + N'];
GRANT SELECT ON [Hospitalization].[OutboxEvent] TO [' + @RelayUser + N'];
GRANT VIEW CHANGE TRACKING ON [Hospitalization].[OutboxEvent] TO [' + @RelayUser + N'];
GRANT SELECT ON [Billing].[OutboxEvent] TO [' + @RelayUser + N'];
GRANT VIEW CHANGE TRACKING ON [Billing].[OutboxEvent] TO [' + @RelayUser + N'];
';
EXEC sp_executesql @sqlRelayGrants;
PRINT CONCAT('Relay onboarded: ', @RelayUser);
GO

-- --- Normalizer ------------------------------------------------------
DECLARE @NormalizerUser NVARCHAR(128) = N'ehrco-normalizer-dev-erp84d';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @NormalizerUser)
    EXEC sp_executesql N'CREATE USER [' + @NormalizerUser + N'] FROM EXTERNAL PROVIDER;';

-- CORREGIDO (smoke test ERP-84, 2026-07-16): el grant original (SELECT ON
-- SCHEMA::Clinical) no cubre NADA de lo que lee HcReader/EmergencyInitialCareReader.
-- Segun el docblock de ClinicalHcReadDbContext.cs, las 9 tablas de ordenes por folio +
-- detalle Qx + maestros (HCORDLABO, HCORDPATO, HCORDIMAG, HCORDPRON, HCORDPROQ,
-- HCPRESCRA, HCSOLINSC, HCINFLIQA, HCORHEMCO, HCORDPROQD, HCSOLINSD, INCUPSIPS,
-- IHLISTPRO, HCHISPACA, INPROFSAL, INESPECIA, INDIAGNOS, HCORHEMSER, HCINFCONC,
-- ADINGRESO, ADATEINIU, ACCOUNTCONTROLSTAY) viven en el schema dbo, y
-- careGroupCode/payerCode (CareGroup, HealthAdministrator, CupsEntity,
-- CupsEntityContractDescription, ContractDescription) viven en el schema Contract.
-- "Clinical" es un schema NUEVO, solo para Clinical.OutboxEvent (ADR-002/011); no
-- tiene relacion con las tablas clinicas legacy. Se detecto via SqlException
-- "SELECT permission denied" repetido tabla por tabla (ADATEINIU, luego
-- Contract.HealthAdministrator) durante el smoke test end-to-end.
EXEC sp_executesql N'GRANT SELECT ON SCHEMA::dbo TO [' + @NormalizerUser + N'];';
EXEC sp_executesql N'GRANT SELECT ON SCHEMA::Contract TO [' + @NormalizerUser + N'];';
EXEC sp_executesql N'GRANT SELECT ON SCHEMA::Clinical TO [' + @NormalizerUser + N'];';
PRINT CONCAT('Normalizer onboarded (dbo + Contract + Clinical): ', @NormalizerUser);
GO

-- --- AuthorizationControl --------------------------------------------
DECLARE @AuthzUser NVARCHAR(128) = N'ehrco-authz-dev-erp84d';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @AuthzUser)
    EXEC sp_executesql N'CREATE USER [' + @AuthzUser + N'] FROM EXTERNAL PROVIDER;';

-- CORREGIDO (smoke test ERP-84, 2026-07-16): el grant original (SELECT ON SCHEMA::Clinical +
-- SELECT/INSERT/UPDATE en 3 tablas de Authorization) no cubre lo que lee
-- AuthorizationOrderConsumer.cs vía SusceptibilityReadDbContext.cs:
--   - dbo: ADCONFSER, ADCONFSERD, ADINGRESO, CHREGESTA, CHTIPESTA, INPACIENT, INENTIDAD,
--     ADATEINIU, HCHISPACA, INUNIFUNC, INPROFSAL, INESPECIA, INDIAGNOS, ADTIPOIDENTIFICA,
--     HCESPSERU, INCUPSIPS, IHLISTPRO
--   - Contract: CareGroup, CUPSEntity
--   - Inventory: InventoryProduct
--   - Authorization: AuthorizationPortfolio, AuthorizationPortfolioCareCenter,
--     AuthorizationPortfolioCUPSEntity, AuthorizationPortfolioInventoryProduct,
--     ConfigurationServicesAmbulatory, ConfigurationServicesAmbulatoryExceptions
--     (ademas de AuthorizationControl/AuthorizationControlTrace/ProcessedInbox, que
--     requieren tambien INSERT/UPDATE porque el writer los modifica).
-- Detectado via SqlException "SELECT permission denied" en dbo.ADINGRESO durante el
-- smoke test end-to-end (mismo patron que el Normalizer).
DECLARE @sqlAuthzGrants NVARCHAR(MAX) = N'
GRANT SELECT ON SCHEMA::dbo TO [' + @AuthzUser + N'];
GRANT SELECT ON SCHEMA::Contract TO [' + @AuthzUser + N'];
GRANT SELECT ON SCHEMA::Inventory TO [' + @AuthzUser + N'];
GRANT SELECT ON SCHEMA::Authorization TO [' + @AuthzUser + N'];
GRANT SELECT ON SCHEMA::Clinical TO [' + @AuthzUser + N'];
GRANT INSERT, UPDATE ON [Authorization].[AuthorizationControl] TO [' + @AuthzUser + N'];
GRANT INSERT, UPDATE ON [Authorization].[AuthorizationControlTrace] TO [' + @AuthzUser + N'];
GRANT INSERT, UPDATE ON [Authorization].[ProcessedInbox] TO [' + @AuthzUser + N'];
';
EXEC sp_executesql @sqlAuthzGrants;
PRINT CONCAT('AuthorizationControl onboarded (dbo + Contract + Inventory + Authorization + Clinical): ', @AuthzUser);
GO
