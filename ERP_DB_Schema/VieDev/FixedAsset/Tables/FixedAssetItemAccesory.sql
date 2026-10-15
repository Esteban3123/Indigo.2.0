CREATE TABLE [FixedAsset].[FixedAssetItemAccesory] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [AccesoryId]       INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemAccesory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemAccesory_Accessory] FOREIGN KEY ([AccesoryId]) REFERENCES [Maintenance].[Accessory] ([Id]),
    CONSTRAINT [FK_FixedAssetItemAccesory_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemAccesory__AccesoryId__FixedAssetItemId] UNIQUE NONCLUSTERED ([AccesoryId] ASC, [FixedAssetItemId] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemAccesory] NOCHECK CONSTRAINT [FK_FixedAssetItemAccesory_Accessory];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemAccesory] NOCHECK CONSTRAINT [FK_FixedAssetItemAccesory_FixedAssetItem];

