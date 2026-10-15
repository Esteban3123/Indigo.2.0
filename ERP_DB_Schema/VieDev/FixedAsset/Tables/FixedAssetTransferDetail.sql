CREATE TABLE [FixedAsset].[FixedAssetTransferDetail] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetTransferId] INT NOT NULL,
    [PhysicalAssetId]      INT NOT NULL,
    CONSTRAINT [PK_FixedAssetTransferDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetPhysicalAsset] FOREIGN KEY ([PhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetTransfer] FOREIGN KEY ([FixedAssetTransferId]) REFERENCES [FixedAsset].[FixedAssetTransfer] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetTransferDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetPhysicalAsset];


GO
ALTER TABLE [FixedAsset].[FixedAssetTransferDetail] NOCHECK CONSTRAINT [FK_FixedAssetTransferDetail_FixedAssetTransfer];

