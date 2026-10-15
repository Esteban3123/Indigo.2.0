CREATE TABLE [Cost].[CostIntermediateDistributionMeasurementUnit] (
    [Id]                             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IntermediateDistributionBaseId] INT NOT NULL,
    [MeasurementUnitId]              INT NOT NULL,
    CONSTRAINT [PK_CostIntermediateDistributionMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostIntermediateDistributionMeasurementUnit_CostIntermediateDistributionBase] FOREIGN KEY ([IntermediateDistributionBaseId]) REFERENCES [Cost].[CostIntermediateDistributionBase] ([Id]),
    CONSTRAINT [FK_CostIntermediateDistributionMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

