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
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItemParts] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItemParts_FixedAssetInitialBalanceItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetInitialBalanceItemParts] NOCHECK CONSTRAINT [FK_FixedAssetInitialBalanceItemParts_FixedAssetPartsAccesoriesConsumables];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Valor de la parte o componente, este campo solo se llena si el campo DepreciatePart esta en 1', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'HistoricalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la parte se debe depreciar', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'DepreciatePart';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de consumibles de accesorios de piezas', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'PartAccesoriesConsumablesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de artículo de saldo inicial de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'FixedAssetInitialBalanceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetInitialBalanceItemParts', @level2type = N'COLUMN', @level2name = N'Id';

