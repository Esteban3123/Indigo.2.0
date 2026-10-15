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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Activo Fijo, este debe ir definido si el tipo de detalle es Activos Fijos', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'PhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de transferencia de activo fijo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'FixedAssetTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

