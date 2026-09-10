/*
================================================================================
 OnboardErp84ControlPlaneUsers.sql
================================================================================
 Script operativo (NO DACPAC) para el onboarding del CONTROL PLANE del ticket
 ERP-84 (Autorizaciones Intrahospitalarias, infra provisionada por DevOps con
 Terraform el 2026-07-14, ambiente rg-erp84-clinical-dev / VIE Development).

 Crea los usuarios de Azure AD (Managed Identity) de las 3 Function Apps
 desplegadas y les otorga los permisos sobre Platform.* documentados en el
 resultado del provisionamiento (erp84-resultado.json → pendiente.sql).
 Registra ademas el cursor de relay para Clinical.OutboxEvent y
 Hospitalization.OutboxEvent, para cada tenant ACTIVE.

 NOMBRES REALES (Managed Identity System-Assigned = nombre de la Function App):
   - ehrco-relay-dev-erp84d          (Indigo.AzClinicalOutboxRelay)
   - ehrco-normalizer-dev-erp84d     (Indigo.AzClinicalOrderNormalizer)  -- no requiere acceso a INDIGOSECV2
   - ehrco-authz-dev-erp84d          (Indigo.AzClinicalAuthorizationControl) -- no requiere acceso a INDIGOSECV2

 Solo Relay necesita usuario en el Control Plane (Normalizer y AuthorizationControl
 solo tocan la base tenant — ver OnboardErp84TenantDatabaseUsers.sql).

 CUANDO EJECUTAR
 ---------------
 Despues de que DevOps confirme que las 3 Function Apps existen (paso 1 del
 plan) y ANTES del smoke test end-to-end.

 DONDE EJECUTAR
 --------------
 Base INDIGOSECV2 (Control Plane). NO ejecutar en la base tenant.

 IDEMPOTENCIA
 ------------
 CREATE USER guardado con IF NOT EXISTS sobre sys.database_principals.
 Cursores con INSERT ... WHERE NOT EXISTS. Seguro re-ejecutar.
================================================================================
*/


-- ------------------------------------------------------------------
-- 3. Usuarios Azure AD (Managed Identity) + grants.
--    CREATE USER via dynamic SQL (no se puede parametrizar el identificador
--    en DDL); cada bloque es idempotente por separado.
-- ------------------------------------------------------------------
-- --- Relay ---------------------------------------------------------
DECLARE @RelayUser sysname = N'ehrco-relay-dev-erp84d';

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_principals
    WHERE name = @RelayUser
)
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = N'CREATE USER ' + QUOTENAME(@RelayUser) + N' FROM EXTERNAL PROVIDER;';
    EXEC sp_executesql @sql;
END;

DECLARE @sqlRelayGrants NVARCHAR(MAX);

SET @sqlRelayGrants =
      N'GRANT SELECT ON [Clinical].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';'
    + NCHAR(13) + NCHAR(10)
    + N'GRANT VIEW CHANGE TRACKING ON [Clinical].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';'
    + NCHAR(13) + NCHAR(10)
    + N'GRANT SELECT ON [Hospitalization].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';'
    + NCHAR(13) + NCHAR(10)
    + N'GRANT VIEW CHANGE TRACKING ON [Hospitalization].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';'
    + NCHAR(13) + NCHAR(10)
    + N'GRANT SELECT ON [Billing].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';'
    + NCHAR(13) + NCHAR(10)
    + N'GRANT VIEW CHANGE TRACKING ON [Billing].[OutboxEvent] TO ' + QUOTENAME(@RelayUser) + N';';

EXEC sp_executesql @sqlRelayGrants;

PRINT CONCAT('Relay onboarded: ', @RelayUser);
GO
-- --- Normalizer ------------------------------------------------------
-- --- Normalizer ---------------------------------------------------------
DECLARE @NormalizerUser sysname = N'ehrco-normalizer-dev-erp84d';

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_principals
    WHERE name = @NormalizerUser
)
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = N'CREATE USER ' + QUOTENAME(@NormalizerUser) + N' FROM EXTERNAL PROVIDER;';
    EXEC sp_executesql @sql;
END;

DECLARE @sqlGrant NVARCHAR(MAX);

SET @sqlGrant = N'GRANT SELECT ON SCHEMA::Clinical TO ' + QUOTENAME(@NormalizerUser) + N';';

EXEC sp_executesql @sqlGrant;

PRINT CONCAT('Normalizer onboarded: ', @NormalizerUser);
GO
-- --- AuthorizationControl --------------------------------------------
-- --- Authorization ---------------------------------------------------------
DECLARE @AuthzUser sysname = N'ehrco-authz-dev-erp84d';

IF NOT EXISTS (
    SELECT 1
    FROM sys.database_principals
    WHERE name = @AuthzUser
)
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    SET @sql = N'CREATE USER ' + QUOTENAME(@AuthzUser) + N' FROM EXTERNAL PROVIDER;';
    EXEC sp_executesql @sql;
END;

DECLARE @sqlAuthzGrants NVARCHAR(MAX);

SET @sqlAuthzGrants =
      N'GRANT SELECT ON SCHEMA::Clinical TO ' + QUOTENAME(@AuthzUser) + N';'
    + CHAR(13) + CHAR(10)
    + N'GRANT SELECT, INSERT, UPDATE ON [Authorization].[AuthorizationControl] TO ' + QUOTENAME(@AuthzUser) + N';'
    + CHAR(13) + CHAR(10)
    + N'GRANT SELECT, INSERT, UPDATE ON [Authorization].[AuthorizationControlTrace] TO ' + QUOTENAME(@AuthzUser) + N';'
    + CHAR(13) + CHAR(10)
    + N'GRANT SELECT, INSERT, UPDATE ON [Authorization].[ProcessedInbox] TO ' + QUOTENAME(@AuthzUser) + N';';

EXEC sp_executesql @sqlAuthzGrants;

PRINT CONCAT('AuthorizationControl onboarded: ', @AuthzUser);
GO