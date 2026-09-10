CREATE TABLE [Maintenance].[EmailMaintenance] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SupplierMaintenanceId] INT          NOT NULL,
    [Email]                 VARCHAR (60) NULL,
    [State]                 BIT          NULL,
    [Synchronized]          CHAR (1)     NULL,
    CONSTRAINT [PK_EmailMaintenance__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EmailMaintenance_SupplierMaintenance] FOREIGN KEY ([SupplierMaintenanceId]) REFERENCES [Maintenance].[SupplierMaintenance] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sincronizado | Bandera CHAR(1) que indica si el registro de correo electrónico del proveedor fue sincronizado con sistemas externos o ERP', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sincronizado', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Synchronized';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado | 1 = Activo (correo vigente) | 0 = Inactivo (correo descontinuado) | Bit que controla la disponibilidad del contacto', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado | 1 = Activo | 0 = Inactivo ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico | Dirección de email VARCHAR(60) del proveedor para mantenimiento, contacto y notificaciones | Identificador PII sensible', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electrónico ', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Email';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Mantenimiento de proveedores | Clave foránea INT que referencia [Maintenance].[SupplierMaintenance] ([Id]) | Vincula el correo al registro de proveedor', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Mantenimiento de proveedores', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'SupplierMaintenanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id autoincrementable INT IDENTITY(1,1) de la tabla | Clave primaria que identifica de forma única cada correo electrónico de proveedor en mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correos electrónicos de contacto asociados a proveedores de mantenimiento. Permite registrar uno o varios emails por proveedor, con su estado activo/inactivo y si ya fue sincronizado con otros sistemas.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'EmailMaintenance';
