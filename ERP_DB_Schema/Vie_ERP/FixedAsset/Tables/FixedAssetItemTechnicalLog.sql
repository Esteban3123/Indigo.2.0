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


GO
CREATE NONCLUSTERED INDEX [IX_FixedAssetItemTechnicalLog__FixedAssetItemId__TechnicalLogId]
    ON [FixedAsset].[FixedAssetItemTechnicalLog]([FixedAssetItemId] ASC, [TechnicalLogId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro tecnico', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'TechnicalLogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemTechnicalLog', @level2type = N'COLUMN', @level2name = N'Id';

