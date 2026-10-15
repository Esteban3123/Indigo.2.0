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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de distribucion base', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

