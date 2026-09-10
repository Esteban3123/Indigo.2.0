CREATE TABLE [InteropCost].[DistributionIntermediateDetail] (
    [Id]                         INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionIntermediateId] INT            NOT NULL,
    [ProductionCenterId]         INT            NOT NULL,
    [Proportion]                 NUMERIC (5, 2) NOT NULL,
    CONSTRAINT [PK_DistributionIntermediateDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionIntermediateDetail_DistributionIntermediate] FOREIGN KEY ([DistributionIntermediateId]) REFERENCES [InteropCost].[DistributionIntermediate] ([Id]),
    CONSTRAINT [FK_DistributionIntermediateDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje o proporción numérica (0-100) que se distribuirá del costo intermedio al centro de producción; valor decimal con 2 decimales (NUMERIC 5,2)', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del centro de producción o unidad funcional destino; referencia a ProductionCenter.Id para asignación de costos indirectos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) de la cabecera o registro padre de distribución intermedia; referencia a DistributionIntermediate.Id que agrupa los detalles', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion intermedia', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'DistributionIntermediateId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) autonumérico del detalle de distribución intermedia; clave primaria para vincular proporción con centro de costo', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de distribución intermedia de costos entre centros de producción. Registra qué proporción del costo de cada distribución intermedia corresponde a cada centro de producción, permitiendo el prorrateo de costos en el proceso de costeo por centro de atención o unidad funcional.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionIntermediateDetail';
