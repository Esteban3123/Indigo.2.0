CREATE TABLE [MixingStation].[CMConfigurationSchedule] (
    [Id]                INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CMConfigurationId] INT      NOT NULL,
    [DayId]             TINYINT  NOT NULL,
    [StartTime]         DATETIME NOT NULL,
    [EndTime]           DATETIME NOT NULL,
    CONSTRAINT [PK_CMConfigurationSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CMConfigurationSchedule_CMConfiguration] FOREIGN KEY ([CMConfigurationId]) REFERENCES [MixingStation].[CMConfiguration] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final, fecha y hora de término de la programación de la central de mezcla (DATETIME). Marca el cierre del intervalo de operación en el día especificado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial, fecha y hora de inicio de la programación de la central de mezcla (DATETIME). Define cuándo comienza la operación en el día asignado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del día de la semana (TINYINT): 1=Lunes, 2=Martes, 3=Miércoles, 4=Jueves, 5=Viernes, 6=Sábado, 7=Domingo, 8=Festivos. Determina el día en que aplica el horario de la central.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'DayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de día: 1:Lunes, 2:Martes, 3:Miercoles, 4:Jueves, 5:Viernes, 6:Sabado, 7:Domingo, 8:Festivos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'DayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'DayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la configuración de la central de mezcla (INT, FK→CMConfiguration.Id). Vincula el horario a la cabecera de configuración de la central de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de central de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la programación de horario (INT IDENTITY). Clave principal de la tabla CMConfigurationSchedule, auto-incrementada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Horarios programados para cada configuración de la estación de mezcla (Mixing Station). Registra los días y franjas horarias de inicio y fin en que una configuración específica está activa o disponible.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule';
