CREATE TABLE [Cost].[CostDistributionFixedAssetDetail] (
    [Id]                       INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionFixedAssetId] INT             NOT NULL,
    [ProductionCenterId]       INT             NOT NULL,
    [Proportion]               NUMERIC (7, 4)  NOT NULL,
    [DepreciationValue]        DECIMAL (18, 2) NOT NULL,
    [HoursQuantity]            INT             CONSTRAINT [DF_CostDistributionFixedAssetDetail_HoursQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_CostDistributionFixedAssetDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostDistributionFixedAsset] FOREIGN KEY ([DistributionFixedAssetId]) REFERENCES [Cost].[CostDistributionFixedAsset] ([Id]),
    CONSTRAINT [FK_CostDistributionFixedAssetDetail_CostProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [Cost].[CostProductionCenter] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_CostDistributionFixedAssetDetail_DistributionFixedAssetId]
    ON [Cost].[CostDistributionFixedAssetDetail]([DistributionFixedAssetId])
    INCLUDE ([Id], [ProductionCenterId], [HoursQuantity], [Proportion], [DepreciationValue]);


GO
CREATE NONCLUSTERED INDEX [IX_CostDistributionFixedAssetDetail_DistributionFixedAssetId_ProductionCenterId]
    ON [Cost].[CostDistributionFixedAssetDetail]([DistributionFixedAssetId], [ProductionCenterId])
    INCLUDE ([Id]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de horas (INT, default 0) de uso o disponibilidad del activo fijo en el centro de producción para el período; usado en cálculos de prorrateo de gastos de depreciación.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Horas usadas en el centro de producción', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'HoursQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (DECIMAL 18,2) de la depreciación del activo fijo correspondiente al mes y año; calculado según tablas de depreciación AFNDEPRECI y AFNCALDEP; base para costeo de centros de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de proporción (NUMERIC 7,4) que se distribuirá del valor de depreciación del activo fijo al centro de producción especificado; suma de proporciones debe totalizar 100%.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que identifica el centro de producción, unidad funcional o área operativa receptora de la distribución de costos; referencia [Cost].[CostProductionCenter].', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (INT) que referencia la cabecera/encabezado de distribución de activos fijos en [Cost].[CostDistributionFixedAsset]; agrupa los detalles de depreciación mensual.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del detalle de distribución de activos fijos; clave primaria de la línea de asignación de depreciación y horas a centros de producción.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion de activos fijos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de activos fijos por centro de producción en el módulo de costos. Registra la proporción, el valor de depreciación y las horas asignadas a cada centro productivo como parte del proceso de distribución de costos de activos fijos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostDistributionFixedAssetDetail';
