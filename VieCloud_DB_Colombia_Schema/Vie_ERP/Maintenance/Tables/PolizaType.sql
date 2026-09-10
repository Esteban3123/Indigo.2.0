CREATE TABLE [Maintenance].[PolizaType] (
    [Id]        TINYINT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]      VARCHAR (20) NOT NULL,
    [Name]      VARCHAR (60) NOT NULL,
    [State]     BIT          NOT NULL,
    [TimeStamp] ROWVERSION   NOT NULL,
    CONSTRAINT [PK_PolizaType__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_PolizaType__State]
    ON [Maintenance].[PolizaType]([State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo TIMESTAMP para control de concurrencia optimista (row version) en actualizaciones simultáneas de tipos de póliza.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Columna para controlar la concurrencia.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT: 1=Activo, 0=Inactivo) del tipo de póliza; indica si está disponible para uso en contratos y facturación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del tipo de poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción o nombre del tipo de póliza (ej: Póliza de Salud, Póliza de Responsabilidad Civil, Póliza Integral); usado en reportes y contratación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del tipo de poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único VARCHAR(20) del tipo de póliza; identificador legible para búsqueda en procesos de contratación, RIPS y facturación.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del tipo de la poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autoincrementable TINYINT (1-255) y clave primaria de tipos de póliza; referencia en contratos y facturas de ERP.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla de tipos de poliza', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de tipos de póliza disponibles en el sistema, como pólizas de salud, vida, accidentes u otros seguros, con su código, nombre y estado activo o inactivo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'PolizaType';
