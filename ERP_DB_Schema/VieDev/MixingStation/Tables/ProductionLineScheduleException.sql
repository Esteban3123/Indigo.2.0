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

