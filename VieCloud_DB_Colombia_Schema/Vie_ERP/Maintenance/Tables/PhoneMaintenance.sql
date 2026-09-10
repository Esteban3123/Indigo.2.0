CREATE TABLE [Maintenance].[PhoneMaintenance] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierMaintenanceId] INT          NOT NULL,
    [Phone]                 VARCHAR (15) NULL,
    [State]                 BIT          NULL,
    [Synchronized]          CHAR (1)     NULL,
    [PhoneType]             SMALLINT     NULL,
    CONSTRAINT [PK_PhoneMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhoneMaintenance_PhoneMaintenance] FOREIGN KEY ([SupplierMaintenanceId]) REFERENCES [Maintenance].[SupplierMaintenance] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación del tipo de contacto telefónico: 1=Móvil, 2=Fijo, 3=Fax, 4=Oficina, 5=PIN; SMALLINT para categorización', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'PhoneType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de telefono  1-Movil  2-Fijo  3-Fax  4-Oficina  5-PIN', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'PhoneType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'PhoneType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de sincronización (CHAR(1)): S=Sincronizado, N=No sincronizado; refleja estado de replicación con sistemas externos', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'sincronizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del teléfono: 1=Activo, 0=Inactivo; BIT que indica si el contacto está vigente en la organización', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado | 1 = Activo | 0 = Inactivo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de teléfono del proveedor; formato VARCHAR(15), admite móvil, fijo, fax u oficina según PhoneType', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Teléfono', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Phone';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Phone';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el Id del registro de mantenimiento del proveedor en tabla SupplierMaintenance', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de registro de teléfono en mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de teléfonos o números de contacto asociados a proveedores de mantenimiento. Permite gestionar múltiples líneas telefónicas (fija, móvil, fax) por cada proveedor, con control de estado activo/inactivo y sincronización con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PhoneMaintenance';
