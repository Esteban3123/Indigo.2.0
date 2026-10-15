CREATE TABLE [Cost].[CostDistributionSecondaryMeasurementUnit] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT NOT NULL,
    [MeasurementUnitId]           INT NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_CostDistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [Cost].[CostDistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionSecondaryMeasurementUnit] NOCHECK CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_CostDistributionSecondaryBase];


GO
ALTER TABLE [Cost].[CostDistributionSecondaryMeasurementUnit] NOCHECK CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_InventoryMeasurementUnit];

