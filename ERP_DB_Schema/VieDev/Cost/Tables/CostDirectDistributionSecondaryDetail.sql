CREATE TABLE [Cost].[CostDirectDistributionSecondaryDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DirectDistributionSecondaryId] INT             NOT NULL,
    [ProductionCenterId]            INT             NOT NULL,
    [MeasurementUnitId]             INT             NULL,
    [Percentage]                    NUMERIC (5, 2)  CONSTRAINT [DF_CostDirectDistributionSecondaryDetail_Percentage] DEFAULT ((0)) NOT NULL,
    [Value]                         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CostDirectDistributionSecondaryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostDirectDistributionSecondary] FOREIGN KEY ([DirectDistributionSecondaryId]) REFERENCES [Cost].[CostDirectDistributionSecondary] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id]),
    CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostDirectDistributionSecondary];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_CostProductionCenter];


GO
ALTER TABLE [Cost].[CostDirectDistributionSecondaryDetail] NOCHECK CONSTRAINT [FK_CostDirectDistributionSecondaryDetail_InventoryMeasurementUnit];

