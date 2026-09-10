/*
================================================================================
 OnboardErp84_01_CreatePlatformSchemaIfMissing.sql
================================================================================
 Script operativo (NO reemplaza el DACPAC) para crear el schema `Platform` y sus
 4 tablas en INDIGOSECV2 (o la base de Control Plane que confirme el DBA con
 OnboardErp84_00_FindControlPlaneSchema.sql), EXTRAIDO TAL CUAL del proyecto
 fuente `INDIGOSEC.sqlproj` (carpeta `Platform\` de ese proyecto):

   - Security/Platform.sql                    -> CREATE SCHEMA [Platform]
   - Platform/Tables/TenantCatalog.sql
   - Platform/Tables/TenantOutboxCursor.sql
   - Platform/Tables/TenantOutboxLease.sql
   - Platform/Tables/PoolThrottlingConfig.sql

 POR QUE ESTE SCRIPT Y NO UN PUBLISH DEL DACPAC COMPLETO
 --------------------------------------------------------
 INDIGOSECV2 ya tiene otros schemas de ese mismo proyecto (Application, Audit,
 Chat, Common, Feature, Management, Marketplace, Prometheus, Security,
 SelfService, WebAppointment) que pertenecen a otros sistemas/equipos. Publicar
 el DACPAC completo ahi mismo arriesga que SSDT detecte drift en esos otros
 schemas (cambios hechos directo en la base, fuera de este repo) y proponga
 alterarlos o borrarlos. Este script SOLO toca `Platform` (lo que necesita
 Autorizaciones), sin evaluar nada mas.

 Si el DBA prefiere el camino "oficial" en vez de este script, la alternativa es:
   1. msbuild INDIGOSEC.sqlproj /p:Configuration=Release
   2. sqlpackage /Action:Publish /SourceFile:<...>.dacpac
        /TargetServerName:ssindigodev.database.windows.net
        /TargetDatabaseName:INDIGOSECV2
      revisando el reporte de "what-if" / drift ANTES de aplicar, para
      confirmar que no toca nada fuera de Platform.

 CUANDO EJECUTAR
 ---------------
 Antes de OnboardErp84ControlPlaneUsers.sql. Es el paso "0.5": primero se ubica
 la base (script 00), luego se asegura que el schema exista (este script), y
 recien ahi se crean usuarios/grants/cursores (script 1 propiamente dicho).

 DONDE EJECUTAR
 --------------
 La base de Control Plane confirmada (hoy, candidato: INDIGOSECV2).

 IDEMPOTENCIA
 ------------
 Cada CREATE va guardado con IF NOT EXISTS. Seguro re-ejecutar.
================================================================================
*/

-- ------------------------------------------------------------------
-- 1. Schema Platform.
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'Platform')
BEGIN
    EXEC('CREATE SCHEMA [Platform] AUTHORIZATION [dbo];');
    PRINT 'Schema [Platform] creado.';
END
ELSE
    PRINT 'Schema [Platform] ya existia (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 2. Platform.TenantCatalog
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id) = 'Platform' AND name = 'TenantCatalog')
BEGIN
    CREATE TABLE [Platform].[TenantCatalog] (
        [TenantId]        UNIQUEIDENTIFIER NOT NULL,
        [TenantCode]      NVARCHAR (50)    NOT NULL,
        [DatabaseName]    NVARCHAR (128)   NOT NULL,
        [ServerName]      NVARCHAR (255)   NOT NULL,
        [ElasticPoolName] NVARCHAR (128)   NULL,
        [Region]          NVARCHAR (50)    NOT NULL,
        [IsActive]        BIT              DEFAULT ((1)) NOT NULL,
        [Status]          NVARCHAR (50)    DEFAULT ('ACTIVE') NOT NULL,
        [CreatedAtUtc]    DATETIME2 (7)    NOT NULL,
        [UpdatedAtUtc]    DATETIME2 (7)    NOT NULL,
        CONSTRAINT [PK_TenantCatalog] PRIMARY KEY CLUSTERED ([TenantId] ASC),
        CONSTRAINT [UQ_TenantCatalog_Code] UNIQUE NONCLUSTERED ([TenantCode] ASC)
    );
    PRINT 'Tabla [Platform].[TenantCatalog] creada.';
END
ELSE
    PRINT 'Tabla [Platform].[TenantCatalog] ya existia (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 3. Platform.TenantOutboxCursor
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id) = 'Platform' AND name = 'TenantOutboxCursor')
BEGIN
    CREATE TABLE [Platform].[TenantOutboxCursor] (
        [TenantId]         UNIQUEIDENTIFIER NOT NULL,
        [OutboxName]       NVARCHAR (100)   DEFAULT ('Clinical.OutboxEvent') NOT NULL,
        [LastSyncVersion]  BIGINT           DEFAULT ((-1)) NOT NULL,
        [LastPollAtUtc]    DATETIME2 (7)    NULL,
        [LastSuccessAtUtc] DATETIME2 (7)    NULL,
        [ErrorCount]       INT              DEFAULT ((0)) NOT NULL,
        [UpdatedAtUtc]     DATETIME2 (7)    NOT NULL,
        CONSTRAINT [PK_TenantOutboxCursor] PRIMARY KEY CLUSTERED ([TenantId] ASC, [OutboxName] ASC)
    );
    PRINT 'Tabla [Platform].[TenantOutboxCursor] creada.';
END
ELSE
    PRINT 'Tabla [Platform].[TenantOutboxCursor] ya existia (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 4. Platform.TenantOutboxLease
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id) = 'Platform' AND name = 'TenantOutboxLease')
BEGIN
    CREATE TABLE [Platform].[TenantOutboxLease] (
        [TenantId]          UNIQUEIDENTIFIER NOT NULL,
        [LeaseOwner]        NVARCHAR (255)   NOT NULL,
        [LeaseExpiresAtUtc] DATETIME2 (7)    NOT NULL,
        [AcquiredAtUtc]     DATETIME2 (7)    NOT NULL,
        CONSTRAINT [PK_TenantOutboxLease] PRIMARY KEY CLUSTERED ([TenantId] ASC)
    );
    PRINT 'Tabla [Platform].[TenantOutboxLease] creada.';
END
ELSE
    PRINT 'Tabla [Platform].[TenantOutboxLease] ya existia (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 5. Platform.PoolThrottlingConfig
-- ------------------------------------------------------------------
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE SCHEMA_NAME(schema_id) = 'Platform' AND name = 'PoolThrottlingConfig')
BEGIN
    CREATE TABLE [Platform].[PoolThrottlingConfig] (
        [ElasticPoolName]      NVARCHAR (128) NOT NULL,
        [MaxConcurrentTenants] INT            DEFAULT ((5)) NOT NULL,
        [BatchSizePerTenant]   INT            DEFAULT ((100)) NOT NULL,
        [PollIntervalActiveMs] INT            DEFAULT ((5000)) NOT NULL,
        [PollIntervalIdleMs]   INT            DEFAULT ((30000)) NOT NULL,
        [UpdatedAtUtc]         DATETIME2 (7)  NOT NULL,
        CONSTRAINT [PK_PoolThrottlingConfig] PRIMARY KEY CLUSTERED ([ElasticPoolName] ASC)
    );
    PRINT 'Tabla [Platform].[PoolThrottlingConfig] creada.';
END
ELSE
    PRINT 'Tabla [Platform].[PoolThrottlingConfig] ya existia (sin cambios).';
GO

-- ------------------------------------------------------------------
-- 6. Verificacion final.
-- ------------------------------------------------------------------
SELECT
    s.name AS Schema_,
    t.name AS Tabla,
    p.rows AS FilasAprox
FROM sys.tables t
JOIN sys.schemas s ON s.schema_id = t.schema_id
JOIN sys.partitions p ON p.object_id = t.object_id AND p.index_id IN (0,1)
WHERE s.name = 'Platform'
ORDER BY t.name;
GO
