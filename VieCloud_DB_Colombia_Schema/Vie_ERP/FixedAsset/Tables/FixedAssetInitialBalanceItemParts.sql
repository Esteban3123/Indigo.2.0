CREATE TABLE [FixedAsset].[FixedAssetInitialBalanceItemParts] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetInitialBalanceItemId] INT             NOT NULL,
    [PartAccesoriesConsumablesId]    INT             NOT NULL,
    [DepreciatePart]                 BIT             NOT NULL,
    [HistoricalValue]                DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetInitialBalanceItemParts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetInitialBalanceItemParts_FixedAssetInitialBalanceItem] FOREIGN KEY ([FixedAssetInitialBalanceItemId]) REFERENCES [FixedAsset].[FixedAssetInitialBalanceItem] ([Id]),
    CONSTRAINT [FK_FixedAssetInitialBalanceItemParts_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumablesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor histórico o costo original (DECIMAL 18,2) de la parte, componente o accesorio; se utiliza solo cuando DepreciatePart=1 para calcular depreciación acumulada y valor en libros del bien de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de la parte o componente, este campo solo se llena si el campo DepreciatePart esta en 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT: 0=No, 1=Sí) que especifica si la parte, componente o accesorio debe ser depreciado contablemente; controla si HistoricalValue se procesa para cálculo de depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia FixedAssetPartsAccesoriesConsumables; referencia la parte, accesorio o consumible específico que compone el activo fijo (tipo, descripción técnica)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumablesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de consumibles de accesorios de piezas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumablesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumablesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) hacia FixedAssetInitialBalanceItem; identifica el artículo/bien de activo fijo padre al que pertenece esta parte, componente o accesorio en el saldo inicial', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de artículo de saldo inicial de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico único (INT IDENTITY) de la línea de parte/componente/accesorio en el saldo inicial de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Partes, accesorios o consumibles asociados a un ítem del balance inicial de activos fijos. Permite registrar si cada parte se deprecia de forma independiente y cuál es su valor histórico al momento del ingreso inicial.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts';
