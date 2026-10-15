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
ALTER TABLE [MixingStation].[ProductionLineSchedule] NOCHECK CONSTRAINT [FK_ProductionLineSchedule_ProductionLineId];

