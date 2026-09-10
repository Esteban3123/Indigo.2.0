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
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catalogo maestro de tenants (clientes) del Control Plane. Una fila por cliente/base de datos. Es la fuente de verdad que usa TenantResolver.cs (Relay/Normalizer/AuthorizationControl) para resolver, por tenant, la conexion a su propia base ({ServerName}/{DatabaseName} sustituidos en Tenant:ConnectionStringTemplate) y para saber si el tenant debe procesarse (IsActive/Status). El relay tambien la usa como fuente de tenants ACTIVE al registrar/avanzar cursores en Platform.TenantOutboxCursor.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador unico del tenant (GUID). PK y FK logica desde Platform.TenantOutboxCursor y Platform.TenantOutboxLease (TenantId).', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo corto/legible del tenant (unico). Identificador de negocio para humanos (soporte, scripts de onboarding); el sistema usa TenantId como clave real.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'TenantCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base de datos Azure SQL del tenant. Sustituye el token {DatabaseName} en el app setting Tenant:ConnectionStringTemplate de Relay/Normalizer/AuthorizationControl.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'DatabaseName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FQDN del logical server de Azure SQL donde vive la base del tenant (ej. servidor.database.windows.net). Sustituye el token {ServerName} en Tenant:ConnectionStringTemplate. Distintos tenants pueden vivir en servidores distintos — NO asumir que todos comparten el mismo servidor que el Control Plane.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'ServerName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del Elastic Pool de Azure SQL donde vive la base del tenant, si aplica (NULL si la base es standalone). Es la clave de join hacia Platform.PoolThrottlingConfig.ElasticPoolName, usada para limitar cuantos tenants del mismo pool procesa el relay en paralelo.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'ElasticPoolName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Region de Azure donde vive la base del tenant (ej. eastus2). Informativo/operativo (capacity planning, latencia); no participa en la resolucion de conexion.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'Region';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Flag booleano de actividad del tenant. Complementa a Status; en la practica el relay filtra por Status = ACTIVE (ver OnboardErp84ControlPlaneUsers.sql), pero IsActive se mantiene como bandera rapida adicional para otros consumidores del catalogo.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'IsActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del tenant (ej. ACTIVE, INACTIVE, SUSPENDED). El relay y los scripts de onboarding (OnboardErp84*.sql) solo procesan/registran cursores para tenants con Status = ACTIVE.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de alta del tenant en el catalogo. Auditoria.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la ultima modificacion de la fila (cambio de Status, ElasticPoolName, etc.). Auditoria.', @level0type = N'SCHEMA', @level0name = N'Platform', @level1type = N'TABLE', @level1name = N'TenantCatalog', @level2type = N'COLUMN', @level2name = N'UpdatedAtUtc';
GO

