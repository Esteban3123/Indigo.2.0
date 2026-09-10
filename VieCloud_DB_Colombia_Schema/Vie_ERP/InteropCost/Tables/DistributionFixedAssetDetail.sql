CREATE TABLE [InteropCost].[DistributionFixedAssetDetail] (
    [Id]                       INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DistributionFixedAssetId] INT            NOT NULL,
    [ProductionCenterId]       INT            NOT NULL,
    [Proportion]               NUMERIC (5, 2) NOT NULL,
    [DepreciationValue]        NUMERIC (18)   NOT NULL,
    CONSTRAINT [PK_DistributionFixedAssetDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DistributionFixedAssetDetail_DistributionFixedAsset] FOREIGN KEY ([DistributionFixedAssetId]) REFERENCES [InteropCost].[DistributionFixedAsset] ([Id]),
    CONSTRAINT [FK_DistributionFixedAssetDetail_ProductionCenter] FOREIGN KEY ([ProductionCenterId]) REFERENCES [InteropCost].[ProductionCenter] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor numérico (NUMERIC 18) de depreciación del activo fijo en el mes y año correspondiente. Referencia a tablas AFNDEPRECI y AFNCALDEP. Cálculo de amortización contable del bien.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de la depreciacion del activo fijo en el mes y año correspondiente    Las tablas de depreciacion son AFNDEPRECI y AFNCALDEP', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DepreciationValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de proporción (NUMERIC 5,2) a distribuir del activo fijo hacia el centro de producción. Valor decimal de distribución costos.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de proporcion que se va a distribuir', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Proportion';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del centro de producción destino. Clave foránea a InteropCost.ProductionCenter. Unidad funcional, área operativa o centro de costo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del centro de produccion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'ProductionCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera/registro principal de distribución de activo fijo. Clave foránea a InteropCost.DistributionFixedAsset. Agrupa detalles de distribución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la distribucion', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'DistributionFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del detalle de distribución de activos fijos. Clave primaria. Número secuencial de la línea de distribución.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la distribucion de activos fijos', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de la distribución de activos fijos entre centros de producción o costo, registrando la proporción asignada y el valor de depreciación correspondiente a cada centro para efectos de costeo.', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'InteropCost', @level1type = N'TABLE', @level1name = N'DistributionFixedAssetDetail';
