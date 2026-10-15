CREATE TABLE [InteropCost].[DistributionDirectCostDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionDirectCostId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [MainAccountId]            INT             NOT NULL,
    [CostCenterId]             INT             NULL,
    [MeasurementUnitId]        INT             NULL,
    [Percentage]               NUMERIC (5, 2)  CONSTRAINT [DF_DistributionDirectCostDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Count]                    NUMERIC (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_Count] DEFAULT ((0)) NOT NULL,
    [CostValue]                DECIMAL (18, 2) CONSTRAINT [DF_DistributionDirectCostDetail_CostValue] DEFAULT ((0)) NOT NULL,
    [Value]                    NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_DistributionDirectCostDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionDirectCostDetail_DistributionDirectCost] FOREIGN KEY ([DistributionDirectCostId]) REFERENCES [InteropCost].[DistributionDirectCost] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_DistributionDirectCostDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_DistributionDirectCost];


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [InteropCost].[DistributionDirectCostDetail] NOCHECK CONSTRAINT [FK_DistributionDirectCostDetail_ProductionCenter];

