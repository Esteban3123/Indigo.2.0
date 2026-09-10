CREATE TABLE [MixingStation].[CMExternalCareCenter] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId]    INT NOT NULL,
    [CustomerId]           INT NOT NULL,
    [ProductionLineId]     INT NOT NULL,
    [ExternalCareCenterId] INT NOT NULL,
    [Status]               BIT NOT NULL,
    CONSTRAINT [PK_CMExternalCareCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMExternalCareCenter_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Common].[Customer] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_ExternalCareCenter] FOREIGN KEY ([ExternalCareCenterId]) REFERENCES [MixingStation].[ExternalCareCenter] ([Id]),
    CONSTRAINT [FK_CMExternalCareCenter_ProductionLine] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo del centro de atención externo en la configuración de mezcla; bit booleano (1=activo, 0=inactivo)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del centro de atención externo (FK a ExternalCareCenter); referencia a unidad funcional, clínica, hospital o prestador externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de atención externo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ExternalCareCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la línea de producción asociada (FK a ProductionLine); línea de procesamiento o atención clínica', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del cliente/institución contratante (FK a Customer); entidad responsable de la configuración', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del cliente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CustomerId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CustomerId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de configuración de mezcla (FK a CMConfiguration); encabezado que agrupa parámetros y reglas', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria, identificador único del registro de asociación entre centro externo y configuración de mezcla (IDENTITY INT)', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de centros de atención externos asociados a una estación de mezcla o línea de producción. Permite configurar qué centros de atención externos (IPS, clínicas, sedes) están habilitados para operar con una configuración y cliente específicos.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMExternalCareCenter';
