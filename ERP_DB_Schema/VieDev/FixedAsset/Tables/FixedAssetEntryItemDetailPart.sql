CREATE TABLE [FixedAsset].[FixedAssetEntryItemDetailPart] (
    [Id]                          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryItemDetailId] INT          NOT NULL,
    [PartAccesoriesConsumiblesId] INT          NOT NULL,
    [DepreciatePart]              BIT          NULL,
    [Value]                       NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetIngressPartsAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryItemDetailPart_FixedAssetEntryItemDetail] FOREIGN KEY ([FixedAssetEntryItemDetailId]) REFERENCES [FixedAsset].[FixedAssetEntryItemDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartAccesoriesConsumiblesId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetailPart] NOCHECK CONSTRAINT [FK_FixedAssetEntryItemDetailPart_FixedAssetEntryItemDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItemDetailPart] NOCHECK CONSTRAINT [FK_FixedAssetIngressPartsAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables];

