CREATE TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] (
    [Id]                            INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceItemDetailId] INT          NOT NULL,
    [PartAccesoriesConsumiblesId]   INT          NOT NULL,
    [DepreciatePart]                BIT          NOT NULL,
    [Value]                         NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_FixedAssetPartAccesoriesConsumiblesInputRemission] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPartAccesoriesConsumiblesInputRemission_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_PartAccesoriesConsumiblesInputRemission_InputRemissionEquipment] FOREIGN KEY ([RemissionEntranceItemDetailId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItemDetail] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor monetario (NUMERIC 18) de la parte, accesorio o consumible. Se requiere solo si DepreciatePart=1 (depreciación activa). Usado para cálculo de depreciación contable de activos fijos.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Valor de la parte o componente, este campo solo se llena si el campo DepreciatePart esta en 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Value';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador booleano (BIT) que determina si la parte, accesorio o consumible debe ser depreciado contablemente como componente del activo fijo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'DepreciatePart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) de la parte, accesorio o consumible asociado. Referencia a FixedAssetPartsAccesoriesConsumables para detalles del componente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumiblesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico (INT, FK) que vincula al detalle del elemento de entrada/remisión de activo fijo. Referencia a FixedAssetRemissionEntranceItemDetail.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle del elemento de entrada de remisión', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico autonumérico (INT IDENTITY) y clave primaria de la tabla. Identificador único del registro de parte en remisión de entrada.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de partes, accesorios o consumibles asociados a cada ítem de una remisión de entrada de activos fijos, indicando si la parte es depreciable y su valor.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItemDetailPart';
