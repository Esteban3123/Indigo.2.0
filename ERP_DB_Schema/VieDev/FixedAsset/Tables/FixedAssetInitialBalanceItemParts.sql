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

