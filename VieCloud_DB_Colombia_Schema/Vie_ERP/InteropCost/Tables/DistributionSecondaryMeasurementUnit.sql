CREATE TABLE [InteropCost].[DistributionSecondaryMeasurementUnit] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionSecondaryBaseId] INT NOT NULL,
    [MeasurementUnitId]           INT NOT NULL,
    CONSTRAINT [PK_DistributionSecondaryMeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_DistributionSecondaryBase] FOREIGN KEY ([DistributionSecondaryBaseId]) REFERENCES [InteropCost].[DistributionSecondaryBase] ([Id]),
    CONSTRAINT [FK_DistributionSecondaryMeasurementUnit_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la unidad de medida asociada a la distribución secundaria. Solo se permiten unidades de tipo 4 (sistema de costos). Referencia a Inventory.InventoryMeasurementUnit. Tipo: INT, FK obligatoria.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la unidad de medida, solo pueden ser de tipo 4 - sistema de costos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la base de distribución secundaria a la cual se asigna la unidad de medida. Referencia a InteropCost.DistributionSecondaryBase. Define el vínculo entre distribuciones de costos secundarias y sus unidades de medida. Tipo: INT, FK obligatoria.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la base de distribucion secundaria', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'DistributionSecondaryBaseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de la asociación entre distribución secundaria y unidad de medida. Clave primaria de la tabla. Tipo: INT, PK clustered.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona las unidades de medida secundarias con una base de distribución secundaria de costos, permitiendo definir qué unidades de medida se usan en cada configuración de distribución dentro del módulo de costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionSecondaryMeasurementUnit';
