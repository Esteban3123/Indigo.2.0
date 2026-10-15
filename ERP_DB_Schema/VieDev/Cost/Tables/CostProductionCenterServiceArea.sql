CREATE TABLE [Cost].[CostProductionCenterServiceArea] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionCenterId] INT NOT NULL,
    [FunctionalUnitId]   INT NOT NULL,
    CONSTRAINT [PK_CostProductionCenterServiceArea] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterServiceArea_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterServiceArea_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_CostProductionCenterServiceArea_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostProductionCenterServiceArea] NOCHECK CONSTRAINT [FK_CostProductionCenterServiceArea_FunctionalUnit];

