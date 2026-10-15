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
ALTER TABLE [MixingStation].[CMConfigurationSchedule] NOCHECK CONSTRAINT [FK_CMConfigurationSchedule_CMConfiguration];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha final', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'EndTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hora inicial', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'StartTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de día: 1:Lunes, 2:Martes, 3:Miercoles, 4:Jueves, 5:Viernes, 6:Sabado, 7:Domingo, 8:Festivos', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'DayId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de central de mezcla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'CMConfigurationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'CMConfigurationSchedule', @level2type = N'COLUMN', @level2name = N'Id';

