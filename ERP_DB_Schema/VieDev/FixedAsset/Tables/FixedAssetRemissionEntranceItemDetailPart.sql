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
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] NOCHECK CONSTRAINT [FK_FixedAssetPartAccesoriesConsumiblesInputRemission_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItemDetailPart] NOCHECK CONSTRAINT [FK_PartAccesoriesConsumiblesInputRemission_InputRemissionEquipment];

