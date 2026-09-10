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
 Registra ademas el cursor de relay para Clinical.OutboxEvent,
 Hospitalization.OutboxEvent y Billing.OutboxEvent, para cada tenant ACTIVE.

 IMPORTANTE — Billing.OutboxEvent (agregado 2026-07-15): OutboxTableCatalog.cs
 (Indigo.AzClinicalOutboxRelay) trae Billing.OutboxEvent en su DefaultTables
 desde el PR de RDA — el relay lo drena por defecto aunque no se configure
 Relay:OutboxTables explicito. Por eso el cursor se registra tambien para esta
 tabla. Ademas de este script, hacen falta 2 cosas mas (ver
 OnboardErp84TenantDatabaseUsers.sql y HANDOFF_DBA_ERP84_ONBOARDING.md):
   1. Change Tracking + grants sobre Billing.OutboxEvent en la base tenant.
   2. Agregar 'billing.stay-committed.v1' al app setting Relay:AllowedEventTypes
      de ehrco-relay-dev-erp84d (hoy no lo incluye) — sin esto el relay LEE pero
      RECHAZA publicar esos eventos (falla cerrada, ver ServiceBusPublisher.cs).

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

DECLARE @RelayUser NVARCHAR(128) = N'ehrco-relay-dev-erp84d';

-- ------------------------------------------------------------------
-- 1. Usuario Azure AD para Relay (Managed Identity System-Assigned).
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @RelayUser)
BEGIN
    DECLARE @sqlCreateUser NVARCHAR(400) =
        N'CREATE USER [' + @RelayUser + N'] FROM EXTERNAL PROVIDER;';
    EXEC sp_executesql @sqlCreateUser;
    PRINT CONCAT('Usuario creado: ', @RelayUser);
END
ELSE
BEGIN
    PRINT CONCAT('Usuario ya existe (sin cambios): ', @RelayUser);
END
GO

DECLARE @RelayUser2 NVARCHAR(128) = N'ehrco-relay-dev-erp84d';

-- ------------------------------------------------------------------
-- 2. Grants sobre Platform.* (segun erp84-resultado.json → pendiente.sql).
-- ------------------------------------------------------------------
DECLARE @sqlGrants NVARCHAR(MAX) = N'
GRANT SELECT ON [Platform].[TenantCatalog] TO [' + @RelayUser2 + N'];
GRANT SELECT, INSERT, UPDATE ON [Platform].[TenantOutboxCursor] TO [' + @RelayUser2 + N'];
GRANT SELECT, INSERT, UPDATE, DELETE ON [Platform].[TenantOutboxLease] TO [' + @RelayUser2 + N'];
GRANT SELECT ON [Platform].[PoolThrottlingConfig] TO [' + @RelayUser2 + N'];
';
EXEC sp_executesql @sqlGrants;
PRINT CONCAT('Grants aplicados sobre Platform.* para: ', @RelayUser2);
GO

-- ------------------------------------------------------------------
-- 3. Registrar cursores de relay para Clinical.OutboxEvent,
--    Hospitalization.OutboxEvent y Billing.OutboxEvent, para cada tenant ACTIVE.
--    LastSyncVersion = -1 -> el relay lo inicializa en su primer ciclo
--    (no reprocesa historial previo al alta del cursor).
-- ------------------------------------------------------------------
INSERT INTO [Platform].[TenantOutboxCursor]
    ([TenantId], [OutboxName], [LastSyncVersion], [ErrorCount], [UpdatedAtUtc])
SELECT
    tc.[TenantId],
    v.[OutboxName],
    -1,
    0,
    SYSUTCDATETIME()
FROM [Platform].[TenantCatalog] tc
CROSS JOIN (VALUES ('Clinical.OutboxEvent'), ('Hospitalization.OutboxEvent'), ('Billing.OutboxEvent')) AS v([OutboxName])
WHERE tc.[Status] = 'ACTIVE'
  AND NOT EXISTS (
        SELECT 1
        FROM [Platform].[TenantOutboxCursor] c
        WHERE c.[TenantId] = tc.[TenantId]
          AND c.[OutboxName] = v.[OutboxName]
      );

PRINT CONCAT(@@ROWCOUNT, ' cursor(es) nuevo(s) insertado(s) (0 si ya existian para todos los tenants ACTIVE).');
GO
