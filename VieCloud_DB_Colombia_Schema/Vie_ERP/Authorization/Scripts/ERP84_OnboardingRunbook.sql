/*
================================================================================
 ERP84_OnboardingRunbook.sql
================================================================================
 Runbook de onboarding SQL para un ambiente nuevo de ERP-84 (Autorizaciones
 Intrahospitalarias): QA, otro tenant, etc. Script operativo (NO DACPAC).

 ORDEN DE EJECUCION (bases distintas, correr cada seccion en su base):
   PARTE 1 -> Control Plane (base con schema Platform, ej. INDIGOSECV2)
   PARTE 2 -> Base del tenant (ej. INDIGO636)

 Antes de correr: reemplazar los placeholders <relay-app-name>,
 <normalizer-app-name>, <authz-app-name> por los nombres reales de las
 Function Apps del ambiente (Managed Identity System-Assigned = nombre de la
 Function App; cambian por ambiente, ej. "-qa-" en vez de "-dev-").

 Solo Relay necesita usuario en el Control Plane. Normalizer y
 AuthorizationControl solo tocan la base del tenant.
================================================================================
*/


-- ================================================================================
-- PARTE 1 — CONTROL PLANE (correr en la base con schema Platform, ej. INDIGOSECV2)
-- Requiere ser AAD Admin del servidor (no basta db_owner).
-- ================================================================================

-- ---- 1.1 Schema + tablas Platform (solo si no existen ya) --------------------
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'Platform')
    EXEC('CREATE SCHEMA [Platform] AUTHORIZATION [dbo];');
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id)='Platform' AND name='TenantCatalog')
CREATE TABLE [Platform].[TenantCatalog] (
    [TenantId]        UNIQUEIDENTIFIER NOT NULL,
    [TenantCode]      NVARCHAR(50)     NOT NULL,
    [DatabaseName]    NVARCHAR(128)    NOT NULL,
    [ServerName]      NVARCHAR(255)    NOT NULL,
    [ElasticPoolName] NVARCHAR(128)    NULL,
    [Region]          NVARCHAR(50)     NOT NULL,
    [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
    [Status]          NVARCHAR(50)     DEFAULT ('ACTIVE') NOT NULL,
    [CreatedAtUtc]    DATETIME2(7)     NOT NULL,
    [UpdatedAtUtc]    DATETIME2(7)     NOT NULL,
    CONSTRAINT [PK_TenantCatalog] PRIMARY KEY CLUSTERED ([TenantId] ASC),
    CONSTRAINT [UQ_TenantCatalog_Code] UNIQUE NONCLUSTERED ([TenantCode] ASC)
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id)='Platform' AND name='TenantOutboxCursor')
CREATE TABLE [Platform].[TenantOutboxCursor] (
    [TenantId]         UNIQUEIDENTIFIER NOT NULL,
    [OutboxName]       NVARCHAR(100)    DEFAULT ('Clinical.OutboxEvent') NOT NULL,
    [LastSyncVersion]  BIGINT           DEFAULT ((-1)) NOT NULL,
    [LastPollAtUtc]    DATETIME2(7)     NULL,
    [LastSuccessAtUtc] DATETIME2(7)     NULL,
    [ErrorCount]       INT              DEFAULT ((0)) NOT NULL,
    [UpdatedAtUtc]     DATETIME2(7)     NOT NULL,
    CONSTRAINT [PK_TenantOutboxCursor] PRIMARY KEY CLUSTERED ([TenantId] ASC, [OutboxName] ASC)
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id)='Platform' AND name='TenantOutboxLease')
CREATE TABLE [Platform].[TenantOutboxLease] (
    [TenantId]          UNIQUEIDENTIFIER NOT NULL,
    [LeaseOwner]        NVARCHAR(255)    NOT NULL,
    [LeaseExpiresAtUtc] DATETIME2(7)     NOT NULL,
    [AcquiredAtUtc]     DATETIME2(7)     NOT NULL,
    CONSTRAINT [PK_TenantOutboxLease] PRIMARY KEY CLUSTERED ([TenantId] ASC)
);
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id)='Platform' AND name='PoolThrottlingConfig')
CREATE TABLE [Platform].[PoolThrottlingConfig] (
    [ElasticPoolName]      NVARCHAR(128) NOT NULL,
    [MaxConcurrentTenants] INT           DEFAULT ((5)) NOT NULL,
    [BatchSizePerTenant]   INT           DEFAULT ((100)) NOT NULL,
    [PollIntervalActiveMs] INT           DEFAULT ((5000)) NOT NULL,
    [PollIntervalIdleMs]   INT           DEFAULT ((30000)) NOT NULL,
    [UpdatedAtUtc]         DATETIME2(7)  NOT NULL,
    CONSTRAINT [PK_PoolThrottlingConfig] PRIMARY KEY CLUSTERED ([ElasticPoolName] ASC)
);
GO

-- ---- 1.2 Registrar el tenant nuevo (si todavia no existe en el catalogo) -----
-- Ojo: la base del tenant puede vivir en un servidor DISTINTO al del Control
-- Plane (paso con INDIGO636 vs INDIGOSECV2) -- confirmar ServerName real.
DECLARE @NewTenantCode NVARCHAR(50) = N'<codigo-tenant>';
IF NOT EXISTS (SELECT 1 FROM [Platform].[TenantCatalog] WHERE [TenantCode] = @NewTenantCode)
INSERT INTO [Platform].[TenantCatalog]
    ([TenantId],[TenantCode],[DatabaseName],[ServerName],[ElasticPoolName],[Region],[IsActive],[Status],[CreatedAtUtc],[UpdatedAtUtc])
VALUES
    (NEWID(), @NewTenantCode, N'<DatabaseName>', N'<ServerName>', NULL, N'<region>', 1, 'ACTIVE', SYSUTCDATETIME(), SYSUTCDATETIME());
GO

-- ---- 1.3 Usuario Relay + grants sobre Platform.* + cursores ------------------
DECLARE @RelayUser NVARCHAR(128) = N'<relay-app-name>';

IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @RelayUser)
    EXEC sp_executesql N'CREATE USER [' + @RelayUser + N'] FROM EXTERNAL PROVIDER;';

DECLARE @sqlControlPlaneGrants NVARCHAR(MAX) = N'
GRANT SELECT ON [Platform].[TenantCatalog] TO [' + @RelayUser + N'];
GRANT SELECT, INSERT, UPDATE ON [Platform].[TenantOutboxCursor] TO [' + @RelayUser + N'];
GRANT SELECT, INSERT, UPDATE, DELETE ON [Platform].[TenantOutboxLease] TO [' + @RelayUser + N'];
GRANT SELECT ON [Platform].[PoolThrottlingConfig] TO [' + @RelayUser + N'];
';
EXEC sp_executesql @sqlControlPlaneGrants;

-- Cursor de outbox para cada tenant ACTIVE (Clinical/Hospitalization/Billing)
INSERT INTO [Platform].[TenantOutboxCursor] ([TenantId],[OutboxName],[LastSyncVersion],[ErrorCount],[UpdatedAtUtc])
SELECT tc.[TenantId], v.[OutboxName], -1, 0, SYSUTCDATETIME()
FROM [Platform].[TenantCatalog] tc
CROSS JOIN (VALUES ('Clinical.OutboxEvent'),('Hospitalization.OutboxEvent'),('Billing.OutboxEvent')) AS v([OutboxName])
WHERE tc.[Status] = 'ACTIVE'
  AND NOT EXISTS (SELECT 1 FROM [Platform].[TenantOutboxCursor] c WHERE c.[TenantId]=tc.[TenantId] AND c.[OutboxName]=v.[OutboxName]);
GO


-- ================================================================================
-- PARTE 2 — BASE DEL TENANT (correr en la base del tenant, ej. INDIGO636)
-- ================================================================================

-- ---- 2.1 Change Tracking a nivel de base (no falla si ya esta activo) --------
IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_databases WHERE database_id = DB_ID())
    ALTER DATABASE CURRENT SET CHANGE_TRACKING = ON (CHANGE_RETENTION = 7 DAYS, AUTO_CLEANUP = ON);
GO

-- ---- 2.2 Change Tracking sobre las 3 tablas outbox que consume el relay ------
IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('Clinical.OutboxEvent'))
    ALTER TABLE [Clinical].[OutboxEvent] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);
GO
IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('Hospitalization.OutboxEvent'))
    ALTER TABLE [Hospitalization].[OutboxEvent] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);
GO
IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('Billing.OutboxEvent'))
    ALTER TABLE [Billing].[OutboxEvent] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = OFF);
GO

-- ---- 2.3 Usuario Relay (lee los 3 outbox) ------------------------------------
DECLARE @Relay NVARCHAR(128) = N'<relay-app-name>';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @Relay)
    EXEC sp_executesql N'CREATE USER [' + @Relay + N'] FROM EXTERNAL PROVIDER;';
EXEC sp_executesql N'
GRANT SELECT ON [Clinical].[OutboxEvent] TO [' + @Relay + N'];
GRANT VIEW CHANGE TRACKING ON [Clinical].[OutboxEvent] TO [' + @Relay + N'];
GRANT SELECT ON [Hospitalization].[OutboxEvent] TO [' + @Relay + N'];
GRANT VIEW CHANGE TRACKING ON [Hospitalization].[OutboxEvent] TO [' + @Relay + N'];
GRANT SELECT ON [Billing].[OutboxEvent] TO [' + @Relay + N'];
GRANT VIEW CHANGE TRACKING ON [Billing].[OutboxEvent] TO [' + @Relay + N'];';
GO

-- ---- 2.4 Usuario Normalizer ---------------------------------------------------
-- Lee dbo (9 tablas de ordenes por folio + maestros) y Contract
-- (CareGroup/HealthAdministrator/CUPS) -- NO solo Clinical (ver
-- ClinicalHcReadDbContext.cs). Este era el bug encontrado en el smoke test:
-- el grant original solo daba SCHEMA::Clinical y fallaba tabla por tabla.
--
-- EXECUTE sobre dbo.PatientDestiny (2026-07-23): los readers de estancia
-- (BillingStayControlReader y StayControlReader) llaman la funcion escalar
-- dbo.PatientDestiny(IPCODPACI, NUMINGRES, FECINIEST). GRANT SELECT NO cubre
-- EXECUTE de funciones -> sin este grant el flujo de estancias (Stella) y el de
-- hospitalizacion fallan con error 229 "The EXECUTE permission was denied on the
-- object 'PatientDestiny'". Encontrado al activar Stella en INDIGO636.
DECLARE @Normalizer NVARCHAR(128) = N'<normalizer-app-name>';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @Normalizer)
    EXEC sp_executesql N'CREATE USER [' + @Normalizer + N'] FROM EXTERNAL PROVIDER;';
EXEC sp_executesql N'
GRANT SELECT ON SCHEMA::dbo TO [' + @Normalizer + N'];
GRANT SELECT ON SCHEMA::Contract TO [' + @Normalizer + N'];
GRANT SELECT ON SCHEMA::Clinical TO [' + @Normalizer + N'];
GRANT SELECT ON SCHEMA::Billing TO [' + @Normalizer + N'];
GRANT EXECUTE ON OBJECT::dbo.PatientDestiny TO [' + @Normalizer + N'];';
GO

-- ---- 2.5 Usuario AuthorizationControl -----------------------------------------
-- Lee dbo (susceptibilidad/admision/maestros), Contract, Inventory y
-- Authorization (portafolio/config) -- NO solo Clinical + 3 tablas propias
-- (ver SusceptibilityReadDbContext.cs). Mismo bug que Normalizer, encontrado
-- despues en el mismo smoke test.
DECLARE @Authz NVARCHAR(128) = N'<authz-app-name>';
IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @Authz)
    EXEC sp_executesql N'CREATE USER [' + @Authz + N'] FROM EXTERNAL PROVIDER;';
EXEC sp_executesql N'
GRANT SELECT ON SCHEMA::dbo TO [' + @Authz + N'];
GRANT SELECT ON SCHEMA::Contract TO [' + @Authz + N'];
GRANT SELECT ON SCHEMA::Inventory TO [' + @Authz + N'];
GRANT SELECT ON SCHEMA::Authorization TO [' + @Authz + N'];
GRANT SELECT ON SCHEMA::Clinical TO [' + @Authz + N'];
GRANT INSERT, UPDATE ON [Authorization].[AuthorizationControl] TO [' + @Authz + N'];
GRANT INSERT, UPDATE ON [Authorization].[AuthorizationControlTrace] TO [' + @Authz + N'];
GRANT INSERT, UPDATE ON [Authorization].[ProcessedInbox] TO [' + @Authz + N'];';
GO

-- ---- 2.6 Fuera de SQL (recordatorio, no ejecutar aqui) ------------------------
-- App setting Relay:AllowedEventTypes de la Function App del Relay debe
-- incluir 'billing.stay-committed.v1' -- si no, el relay LEE Billing.OutboxEvent
-- pero RECHAZA publicarlo (falla cerrada, ver ServiceBusPublisher.cs).


-- ================================================================================
-- PARTE 3 — DIAGNOSTICO / VERIFICACION (correr donde corresponda cada query)
-- ================================================================================

-- ---- 3.1 Tenant activo y cursores avanzando (Control Plane) ------------------
-- SELECT TenantId, TenantCode, Status FROM [Platform].[TenantCatalog];
--
-- SELECT TenantId, OutboxName, LastSyncVersion, ErrorCount, UpdatedAtUtc
-- FROM [Platform].[TenantOutboxCursor]
-- ORDER BY UpdatedAtUtc DESC;
-- Status debe ser ACTIVE. Si quedo en REQUIRES_RESYNC, ver 3.5.

-- ---- 3.2 Change Tracking habilitado y con version valida (base del tenant) ---
-- SELECT ct.object_id, OBJECT_NAME(ct.object_id) AS Tabla, ct.min_valid_version
-- FROM sys.change_tracking_tables ct
-- WHERE ct.object_id IN (OBJECT_ID('Clinical.OutboxEvent'), OBJECT_ID('Hospitalization.OutboxEvent'), OBJECT_ID('Billing.OutboxEvent'));
--
-- SELECT CHANGE_TRACKING_CURRENT_VERSION();
-- Si min_valid_version sale NULL para alguna tabla, esa tabla nunca tuvo CT
-- habilitado (repetir 2.2 para ella).

-- ---- 3.3 Grants efectivos de un usuario (base del tenant) --------------------
-- SELECT pr.name AS Usuario, pe.class_desc, s.name AS Schema_, o.name AS Objeto, pe.permission_name, pe.state_desc
-- FROM sys.database_permissions pe
-- JOIN sys.database_principals pr ON pr.principal_id = pe.grantee_principal_id
-- LEFT JOIN sys.objects o ON o.object_id = pe.major_id
-- LEFT JOIN sys.schemas s ON s.schema_id = COALESCE(o.schema_id, pe.major_id)
-- WHERE pr.name IN (N'<relay-app-name>', N'<normalizer-app-name>', N'<authz-app-name>')
-- ORDER BY pr.name, s.name, o.name;

-- ---- 3.4 Smoke test end-to-end (base del tenant, tras mandar un evento real) -
-- SELECT TOP 5 * FROM Authorization.AuthorizationControl ORDER BY Id DESC;

-- ---- 3.5 Troubleshooting conocido (ya vivido en dev) -------------------------
-- "0 tenants activos" en el Relay
--   -> Platform.TenantCatalog.Status = REQUIRES_RESYNC casi siempre es porque
--      a alguna tabla outbox le falta Change Tracking (ver 3.2). Sin eso el
--      cursor no tiene min_valid_version y el relay marca el tenant para resync.
--
-- "SELECT permission denied" tabla por tabla
--   -> el grant esta incompleto (schema equivocado). Los grants de 2.3/2.4/2.5
--      ya cubren todo lo que leen ClinicalHcReadDbContext.cs (Normalizer) y
--      SusceptibilityReadDbContext.cs (AuthorizationControl) -- NO agregar
--      tabla por tabla, es el mismo error que ya se cometio una vez.
--
-- AuthorizationControl vacio aunque el Normalizer procese OK ("Ordenes=0")
--   -> el evento de prueba fue sintetico (solo fila en Clinical.OutboxEvent,
--      sin orden real detras). Se necesita un folio con orden real en dbo.
--
-- Mensajes en dead-letter que no se reprocesan solos
--   -> Service Bus no reintenta automaticamente tras MaxDeliveryCount. Hay
--      que resubmitirlos manualmente (Service Bus Explorer -> Dead-letter ->
--      Resubmit) o mandar un evento nuevo.
