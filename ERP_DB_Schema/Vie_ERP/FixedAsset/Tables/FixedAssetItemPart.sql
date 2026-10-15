CREATE TABLE [FixedAsset].[FixedAssetItemPart] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [PartId]           INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemPart__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemPart_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemPart_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([PartId]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemPart__FixedAssetItemId__PartId] UNIQUE NONCLUSTERED ([FixedAssetItemId] ASC, [PartId] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemPart] NOCHECK CONSTRAINT [FK_FixedAssetItemPart_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemPart] NOCHECK CONSTRAINT [FK_FixedAssetItemPart_FixedAssetPartsAccesoriesConsumables];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la parte', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'PartId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemPart', @level2type = N'COLUMN', @level2name = N'Id';

