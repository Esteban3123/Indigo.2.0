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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final programada de la última preparación del turno (TIME SQL, formato HH:MM:SS). Límite superior de la ventana de preparación láctea en el periodo laboral.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'FinalPreparationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora programada de la preparación FINAL', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'FinalPreparationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'FinalPreparationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial programada de la primera preparación del turno (TIME SQL, formato HH:MM:SS). Punto de partida de la secuencia de preparaciones lácteas.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'InitialPreparationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora programada de la preparación INICIAL', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'InitialPreparationTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'InitialPreparationTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número secuencial o consecutivo de la preparación dentro del turno (INT). Identificador ordinal de cada toma o alimentación programada.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Número consecutivo de la preparación', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationNumber';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Intervalo de frecuencia en horas entre preparaciones sucesivas (INT). Define cada cuántas horas se debe preparar la siguiente alimentación o biberón.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationFrequencyHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia entre preparaciones (en horas)', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationFrequencyHours';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'PreparationFrequencyHours';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de finalización del turno o jornada laboral (TIME SQL, formato HH:MM:SS). Límite máximo del rango horario de atención.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora final del turno laboral', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora de inicio del turno o jornada laboral (TIME SQL, formato HH:MM:SS). Límite mínimo del rango horario de atención.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inicial del turno laboral', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia la configuración padre en [Lactation].[ParametersConfiguration] (INT, NOT NULL). Vincula cada horario a su matriz de parámetros de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de referencia a la configuración principal', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'ParametersConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horarios de preparación de lactancia definidos por rango horario, frecuencia de alimentación e intervalo entre preparaciones. Almacena los parámetros de configuración temporal para preparación de biberones o alimentos lácteos en unidades de maternidad, pediatría o lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horarios de preparación definidos por rango horario y frecuencia', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de parámetros de preparación de lactancia.', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Lactation', @level1type = N'TABLE', @level1name = N'ParametersPreparations', @level2type = N'COLUMN', @level2name = N'Id';
