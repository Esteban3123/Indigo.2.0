CREATE TABLE [Lactation].[ParametersCareCenters] (
    [Id]                        INT          IDENTITY (1, 1) NOT NULL,
    [ParametersConfigurationId] INT          NOT NULL,
    [CenterCode]                VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CareCenters_Config] FOREIGN KEY ([ParametersConfigurationId]) REFERENCES [Lactation].[ParametersConfiguration] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro de atención', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'CenterCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Centros de atención asociados a la configuración del lactario', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersCareCenters';

