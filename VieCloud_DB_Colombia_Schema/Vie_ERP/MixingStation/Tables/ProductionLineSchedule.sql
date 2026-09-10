CREATE TABLE [MixingStation].[ProductionLineSchedule] (
    [Id]               INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionLineId] INT      NOT NULL,
    [DayId]            TINYINT  NOT NULL,
    [StartTime]        DATETIME NOT NULL,
    [EndTime]          DATETIME NOT NULL,
    CONSTRAINT [PK__ProductionLineSchedule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLineSchedule_ProductionLineId] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_ProductionLineSchedule_ProductionLineId]
    ON [MixingStation].[ProductionLineSchedule]([ProductionLineId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora final o fecha de finalización de turno (DATETIME); marca cierre de operaciones programadas en la línea de producción.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora inicial de turno o jornada de producción (DATETIME); marca inicio de operaciones en la línea.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico de día de semana (TINYINT 1-8): 1=Lunes, 2=Martes, 3=Miércoles, 4=Jueves, 5=Viernes, 6=Sábado, 7=Domingo, 8=Festivos; define día de operación programado.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'DayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de día: 1:Lunes, 2:Martes, 3:Miercoles, 4:Jueves, 5:Viernes, 6:Sabado, 7:Domingo, 8:Festivos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'DayId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'DayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de línea de producción (FK a ProductionLine.Id); referencia a la línea de manufactura o mezclado asignada.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'ProductionLineId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de la tabla ProductionLineSchedule; clave primaria.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Programa de turnos o franjas horarias asignadas a cada línea de producción en la estación de mezcla. Registra en qué días y en qué horario (inicio y fin) opera cada línea.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineSchedule';
