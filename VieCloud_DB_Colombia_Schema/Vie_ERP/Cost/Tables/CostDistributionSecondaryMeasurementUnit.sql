CREATE TABLE [Cost].[CostDistributionSecondaryMeasurementUnit] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT NOT NULL,
    [MeasurementUnitId]           INT NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_CostDistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [Cost].[CostDistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_CostDistributionSecondaryMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida (tipo 4 - sistema de costos). Referencia a Inventory.InventoryMeasurementUnit. Solo unidades válidas para distribución y costeo de servicios/procedimientos sanitarios.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la base de distribución secundaria de costos. Referencia a Cost.CostDistributionSecondaryBase. Vínculo a reglas de reparto de gastos operacionales entre centros de atención, servicios o unidades funcionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (IDENTITY) de la asociación entre unidad de medida y base de distribución secundaria en el sistema de costeo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las bases secundarias de distribución de costos con sus unidades de medida correspondientes, permitiendo definir qué unidad se usa para calcular o prorratear cada componente secundario del costo.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionSecondaryMeasurementUnit';
