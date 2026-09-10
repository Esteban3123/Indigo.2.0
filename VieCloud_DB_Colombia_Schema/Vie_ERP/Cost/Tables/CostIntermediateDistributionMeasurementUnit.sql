CREATE TABLE [Cost].[CostIntermediateDistributionMeasurementUnit] (
    [Id]                             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IntermediateDistributionBaseId] INT NOT NULL,
    [MeasurementUnitId]              INT NOT NULL,
    CONSTRAINT [PK_CostIntermediateDistributionMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostIntermediateDistributionMeasurementUnit_CostIntermediateDistributionBase] FOREIGN KEY ([IntermediateDistributionBaseId]) REFERENCES [Cost].[CostIntermediateDistributionBase] ([Id]),
    CONSTRAINT [FK_CostIntermediateDistributionMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (solo tipo 4 - sistema de costos). FK a Inventory.InventoryMeasurementUnit. Define la unidad (kg, ml, unidad, etc.) para costeo de insumos, medicamentos y servicios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la base de distribución intermedia o secundaria de costos. FK a Cost.CostIntermediateDistributionBase. Vincula el centro de costo con su base de reparto para asignación de gastos indirectos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'IntermediateDistributionBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) y clave primaria de la tabla. Identifica de forma única cada relación entre una unidad de medida y una base de distribución intermedia.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las bases de distribución intermedia de costos con sus unidades de medida, permitiendo definir qué unidad de medida aplica a cada base de distribución en el proceso de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostIntermediateDistributionMeasurementUnit';
