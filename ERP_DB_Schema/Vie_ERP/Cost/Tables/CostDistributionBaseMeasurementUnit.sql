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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de distribucion base', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionBaseMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

