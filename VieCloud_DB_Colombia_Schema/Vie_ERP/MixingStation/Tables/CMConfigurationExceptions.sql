CREATE TABLE [MixingStation].[CMConfigurationExceptions] (
    [Id]                INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT           NOT NULL,
    [StopDate]          DATETIME      NOT NULL,
    [ReasonForStop]     VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_CMConfigurationExceptions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMConfigurationExceptions_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);




GO


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Excepciones o interrupciones registradas para configuraciones de estación de mezcla (Mixing Station). Guarda los motivos y fechas en que una configuración fue detenida o suspendida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno de la excepción o interrupción registrada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la configuración de estación de mezcla que fue interrumpida o suspendida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora en que se detuvo o suspendió la configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'StopDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'StopDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Motivo o justificación por el cual se interrumpió o detuvo la configuración.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'ReasonForStop';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationExceptions', @level2type = N'COLUMN', @level2name = N'ReasonForStop';
