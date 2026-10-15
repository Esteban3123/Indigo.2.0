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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Protocolo de Mantenimiento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Equipo/Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'FixedAssetItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave principal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemProtocol', @level2type = N'COLUMN', @level2name = N'Id';

