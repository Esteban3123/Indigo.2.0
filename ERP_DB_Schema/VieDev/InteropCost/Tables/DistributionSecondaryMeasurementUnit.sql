CREATE TABLE [InteropCost].[DistributionSecondaryMeasurementUnit] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT NOT NULL,
    [MeasurementUnitId]           INT NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_DistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [InteropCost].[DistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionSecondaryMeasurementUnit] NOCHECK CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_DistributionSecondaryBase];


GO
ALTER TABLE [InteropCost].[DistributionSecondaryMeasurementUnit] NOCHECK CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_InventoryMeasurementUnit];

