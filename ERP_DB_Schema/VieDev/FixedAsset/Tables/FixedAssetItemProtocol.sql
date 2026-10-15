CREATE TABLE [FixedAsset].[FixedAssetItemProtocol] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetItemId]      INT NOT NULL,
    [MaintenanceProtocolId] INT NOT NULL,
    CONSTRAINT [PK_FixedAssetItemProtocol__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemProtocol_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetItemProtocol_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [UQ_FixedAssetItemProtocol__FixedAssetItemId__MaintenanceProtocolId] UNIQUE NONCLUSTERED ([FixedAssetItemId] ASC, [MaintenanceProtocolId] ASC)
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemProtocol] NOCHECK CONSTRAINT [FK_FixedAssetItemProtocol_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemProtocol] NOCHECK CONSTRAINT [FK_FixedAssetItemProtocol_MaintenanceProtocol];

