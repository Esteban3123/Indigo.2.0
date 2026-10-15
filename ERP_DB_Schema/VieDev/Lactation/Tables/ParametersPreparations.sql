CREATE TABLE [Lactation].[ParametersPreparations] (
    [Id]                        INT      IDENTITY (1, 1) NOT NULL,
    [ParametersConfigurationId] INT      NOT NULL,
    [StartTime]                 TIME (7) NOT NULL,
    [EndTime]                   TIME (7) NOT NULL,
    [PreparationFrequencyHours] INT      NOT NULL,
    [PreparationNumber]         INT      NOT NULL,
    [InitialPreparationTime]    TIME (7) NOT NULL,
    [FinalPreparationTime]      TIME (7) NOT NULL,
    CONSTRAINT [PK__Paramete__3214EC07C86A635F] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Preparations_Config] FOREIGN KEY ([ParametersConfigurationId]) REFERENCES [Lactation].[ParametersConfiguration] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora programada de la preparación FINAL', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'FinalPreparationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora programada de la preparación INICIAL', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'InitialPreparationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número consecutivo de la preparación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia entre preparaciones (en horas)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationFrequencyHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final del turno laboral', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial del turno laboral', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horarios de preparación definidos por rango horario y frecuencia', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations';

