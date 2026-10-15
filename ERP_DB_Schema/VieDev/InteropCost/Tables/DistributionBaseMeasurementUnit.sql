CREATE TABLE [InteropCost].[DistributionBaseMeasurementUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT NOT NULL,
    [MeasurementUnitId]  INT NOT NULL,
    CONSTRAINT [PK_DistributionBaseMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionBaseMeasurementUnit_DistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [InteropCost].[DistributionBase] ([Id]),
    CONSTRAINT [FK_DistributionBaseMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [InteropCost].[DistributionBaseMeasurementUnit] NOCHECK CONSTRAINT [FK_DistributionBaseMeasurementUnit_DistributionBase];


GO
ALTER TABLE [InteropCost].[DistributionBaseMeasurementUnit] NOCHECK CONSTRAINT [FK_DistributionBaseMeasurementUnit_InventoryMeasurementUnit];

