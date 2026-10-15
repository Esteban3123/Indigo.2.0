CREATE TABLE [Cost].[CostProductionCenterCostCenter] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostCenterId]       INT NOT NULL,
    [ProductionCenterId] INT NOT NULL,
    CONSTRAINT [PK_CostProductionCenterCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostProductionCenterCostCenter_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_CostProductionCenterCostCenter_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [UQ_CostProductionCenterCostCenter__CostCenterId] UNIQUE NONCLUSTERED ([CostCenterId] ASC)
);


GO
ALTER TABLE [Cost].[CostProductionCenterCostCenter] NOCHECK CONSTRAINT [FK_CostProductionCenterCostCenter_CostCenter];


GO
ALTER TABLE [Cost].[CostProductionCenterCostCenter] NOCHECK CONSTRAINT [FK_CostProductionCenterCostCenter_CostProductionCenter];

