CREATE TABLE [Cost].[CostActivityStepInventory] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostActivityStepId]   INT             NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [Quantity]             DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostActivityStepInventory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostActivityStepInventory_CostActivityStep] FOREIGN KEY ([CostActivityStepId]) REFERENCES [Cost].[CostActivityStep] ([Id]),
    CONSTRAINT [FK_CostActivityStepInventory_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id])
);


GO
ALTER TABLE [Cost].[CostActivityStepInventory] NOCHECK CONSTRAINT [FK_CostActivityStepInventory_CostActivityStep];


GO
ALTER TABLE [Cost].[CostActivityStepInventory] NOCHECK CONSTRAINT [FK_CostActivityStepInventory_CostInventoryGroup];

