CREATE TABLE [InteropCost].[ProductionCenterCostCenter] (
    [Id]                 INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostCenterId]       INT          NOT NULL,
    [CostCenterCode]     VARCHAR (50) NOT NULL,
    [ProductionCenterId] INT          NOT NULL,
    CONSTRAINT [PK_ProductionCenterCostCenter__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionCenterCostCenter_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id]),
    CONSTRAINT [UQ_ProductionCenterCostCenter__CostCenterId] UNIQUE NONCLUSTERED ([CostCenterId] ASC)
);


GO
ALTER TABLE [InteropCost].[ProductionCenterCostCenter] NOCHECK CONSTRAINT [FK_ProductionCenterCostCenter_ProductionCenter];

