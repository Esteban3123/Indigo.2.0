CREATE TABLE [FixedAsset].[FixedAssetItemConsumible] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [ConsumibleId]     INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemConsumible__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemConsumible_Consumable] FOREIGN KEY ([ConsumibleId]) REFERENCES [Maintenance].[Consumable] ([Id]),
    CONSTRAINT [FK_FixedAssetItemConsumible_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemConsumible__ConsumibleId__FixedAssetItemId] UNIQUE NONCLUSTERED ([ConsumibleId] ASC, [FixedAssetItemId] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemConsumible] NOCHECK CONSTRAINT [FK_FixedAssetItemConsumible_Consumable];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemConsumible] NOCHECK CONSTRAINT [FK_FixedAssetItemConsumible_FixedAssetItem];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del consumible', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemConsumible', @level2type = N'COLUMN', @level2name = N'ConsumibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemConsumible', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemConsumible', @level2type = N'COLUMN', @level2name = N'Id';

