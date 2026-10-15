CREATE TABLE [Cost].[CostDistributionBaseMeasurementUnit] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionBaseId] INT NOT NULL,
    [MeasurementUnitId]  INT NOT NULL,
    CONSTRAINT [PK_DistributionBaseMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionBaseMeasurementUnit_CostDistributionBase] FOREIGN KEY ([DistributionBaseId]) REFERENCES [Cost].[CostDistributionBase] ([Id]),
    CONSTRAINT [FK_DistributionBaseMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
ALTER TABLE [Cost].[CostDistributionBaseMeasurementUnit] NOCHECK CONSTRAINT [FK_CostDistributionBaseMeasurementUnit_CostDistributionBase];


GO
ALTER TABLE [Cost].[CostDistributionBaseMeasurementUnit] NOCHECK CONSTRAINT [FK_DistributionBaseMeasurementUnit_InventoryMeasurementUnit];

