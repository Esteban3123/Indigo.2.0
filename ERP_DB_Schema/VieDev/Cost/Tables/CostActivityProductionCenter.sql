CREATE TABLE [Cost].[CostActivityProductionCenter] (
    [Id]                     INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityId]         INT NOT NULL,
    [CostProductionCenterId] INT NOT NULL,
    CONSTRAINT [PK_CostActivityProductionCenter] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityProductionCenter_CostActivity] FOREIGN KEY ([CostActivityId]) REFERENCES [Cost].[CostActivity] ([Id]),
    CONSTRAINT [FK_CostActivityProductionCenter_CostProductionCenter] FOREIGN KEY ([CostProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityProductionCenter] NOCHECK CONSTRAINT [FK_CostActivityProductionCenter_CostActivity];


GO
ALTER TABLE [Cost].[CostActivityProductionCenter] NOCHECK CONSTRAINT [FK_CostActivityProductionCenter_CostProductionCenter];

