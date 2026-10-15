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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

