CREATE TABLE [MixingStation].[ProductionLineScheduleException] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionLineId] INT           NOT NULL,
    [StopDate]         DATETIME      NOT NULL,
    [ReasonForStop]    VARCHAR (200) NOT NULL,
    CONSTRAINT [PK__ProductionLineScheduleException] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionLineScheduleException_ProductionLineId] FOREIGN KEY ([ProductionLineId]) REFERENCES [MixingStation].[ProductionLine] ([Id])
);


GO
ALTER TABLE [MixingStation].[ProductionLineScheduleException] NOCHECK CONSTRAINT [FK_ProductionLineScheduleException_ProductionLineId];


GO
CREATE NONCLUSTERED INDEX [IX_ProductionLineScheduleException_ProductionLineId]
    ON [MixingStation].[ProductionLineScheduleException]([ProductionLineId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Motivo por el cual no se labora', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ReasonForStop';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha que no se labora', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'StopDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de línea de producción', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'ProductionLineId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionLineScheduleException', @level2type = N'COLUMN', @level2name = N'Id';

