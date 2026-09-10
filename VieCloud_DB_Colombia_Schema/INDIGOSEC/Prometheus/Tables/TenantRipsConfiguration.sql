CREATE TABLE [Prometheus].[TenantRipsConfiguration] (
    [TenantId]                         UNIQUEIDENTIFIER NOT NULL,
    [EnvironmentCode]                  VARCHAR (4) NOT NULL,
    [IsActive]                         BIT              NOT NULL CONSTRAINT [DF_TenantRipsConfiguration_IsActive] DEFAULT ((1)),
    [SisproIdentificationType]         NVARCHAR (20) NOT NULL,
    [SisproIdentificationNumber]       NVARCHAR (50) NOT NULL,
    [SisproNit]                        NVARCHAR (50) NOT NULL,
    [SisproPassword]                   NVARCHAR (512) NOT NULL,
    [SisproValidationMechanism]        NVARCHAR (100) NOT NULL,
    [CosmosDatabaseName]               NVARCHAR (128) NOT NULL,
    [LegacyCosmosDatabaseName]         NVARCHAR (128) NULL,
    [LegacyCosmosCutoverUtc]           DATETIME2 (7) NULL,
    [CreatedAtUtc]                     DATETIME2 (7) NOT NULL CONSTRAINT [DF_TenantRipsConfiguration_CreatedAtUtc] DEFAULT (SYSUTCDATETIME()),
    [CreatedBy]                        NVARCHAR (255) NOT NULL CONSTRAINT [DF_TenantRipsConfiguration_CreatedBy] DEFAULT (SUSER_SNAME()),
    [UpdatedAtUtc]                     DATETIME2 (7) NOT NULL CONSTRAINT [DF_TenantRipsConfiguration_UpdatedAtUtc] DEFAULT (SYSUTCDATETIME()),
    [UpdatedBy]                        NVARCHAR (255) NOT NULL CONSTRAINT [DF_TenantRipsConfiguration_UpdatedBy] DEFAULT (SUSER_SNAME()),
    [RowVersion]                       ROWVERSION       NOT NULL,
    CONSTRAINT [PK_TenantRipsConfiguration] PRIMARY KEY CLUSTERED ([TenantId] ASC, [EnvironmentCode] ASC),
    CONSTRAINT [FK_TenantRipsConfiguration_TenantCatalog] FOREIGN KEY ([TenantId]) REFERENCES [Platform].[TenantCatalog] ([TenantId]),
    CONSTRAINT [CK_TenantRipsConfiguration_EnvironmentCode] CHECK ([EnvironmentCode] IN ('QA', 'PROD'))
);
GO

EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración de RIPS por tenant y ambiente. Contiene las credenciales de SISPRO y los nombres de base Cosmos usados durante la migración Outbox RIPS. La llave primaria compuesta garantiza una sola configuración por (TenantId, EnvironmentCode).', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del tenant en el Control Plane. Referencia a Platform.TenantCatalog.TenantId.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'TenantId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ambiente de la configuración. Valores permitidos: QA o PROD.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'EnvironmentCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si esta configuración puede ser usada por el flujo RIPS.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'IsActive';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación requerido por SISPRO.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'SisproIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de identificación requerido por SISPRO.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'SisproIdentificationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT usado para autenticación ante SISPRO.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'SisproNit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contraseña de SISPRO.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'SisproPassword';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Mecanismo de validación configurado para SISPRO.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'SisproValidationMechanism';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base Cosmos vigente para la operación RIPS del ambiente.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'CosmosDatabaseName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la base Cosmos anterior durante la transición. NULL cuando no existe una base legacy.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'LegacyCosmosDatabaseName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Instante UTC a partir del cual deja de usarse LegacyCosmosDatabaseName. NULL cuando no hay cutover programado.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'LegacyCosmosCutoverUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de creación de la configuración.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identidad que creó la configuración. El valor por defecto es SUSER_SNAME().', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'CreatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora UTC de la última actualización. El servicio debe actualizarla junto con cada cambio.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'UpdatedAtUtc';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario o identidad que realizó la última actualización. El servicio debe actualizarla junto con cada cambio.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'UpdatedBy';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión binaria automática para control de concurrencia optimista. No debe ser asignada por la aplicación.', @level0type = N'SCHEMA', @level0name = N'Prometheus', @level1type = N'TABLE', @level1name = N'TenantRipsConfiguration', @level2type = N'COLUMN', @level2name = N'RowVersion';
GO
