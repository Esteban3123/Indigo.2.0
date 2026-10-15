CREATE TABLE [FixedAsset].[FixedAssetItemTechnicalLog] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId] INT NOT NULL,
    [TechnicalLogId]   INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemTechnicalLog__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemTechnicalLog_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemTechnicalLog_TechnicalLog] FOREIGN KEY ([TechnicalLogId]) REFERENCES [Maintenance].[TechnicalLog] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemTechnicalLog] NOCHECK CONSTRAINT [FK_FixedAssetItemTechnicalLog_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemTechnicalLog] NOCHECK CONSTRAINT [FK_FixedAssetItemTechnicalLog_TechnicalLog];

