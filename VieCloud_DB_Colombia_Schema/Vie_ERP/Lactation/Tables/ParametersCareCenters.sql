CREATE TABLE [Lactation].[ParametersCareCenters] (
    [Id]                        INT          IDENTITY (1, 1) NOT NULL,
    [ParametersConfigurationId] INT          NOT NULL,
    [CenterCode]                VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareCenters_Config] FOREIGN KEY ([ParametersConfigurationId]) REFERENCES [Lactation].[ParametersConfiguration] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención, unidad funcional o sede donde opera el lactario; identificador único de la institución prestadora de servicios (IPS) o unidad de salud.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'CenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'CenterCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'CenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de referencia a la configuración principal del lactario (FK a ParametersConfiguration); vincula cada centro con su set de parámetros operacionales.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centros de atención asociados a la configuración del lactario; vincula unidades funcionales/sedes con parámetros de operación de bancos de leche.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Centros de atención asociados a la configuración del lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de centro de atención asociado a la configuración de parámetros de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'Id';
