CREATE TABLE [Maintenance].[ProtocolTools] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceProtocolId]     INT NOT NULL,
    [FixedAssetPhysicalAssetId] INT NOT NULL,
    CONSTRAINT [PK_ProtocolTools__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProtocolTools_FixedAssetPhysicalAsset] FOREIGN KEY ([FixedAssetPhysicalAssetId]) REFERENCES [FixedAsset].[FixedAssetPhysicalAsset] ([Id]),
    CONSTRAINT [FK_ProtocolTools_MaintenanceProtocol] FOREIGN KEY ([MaintenanceProtocolId]) REFERENCES [Maintenance].[MaintenanceProtocol] ([Id]),
    CONSTRAINT [IX_ProtocolTools] UNIQUE NONCLUSTERED ([FixedAssetPhysicalAssetId] ASC, [MaintenanceProtocolId] ASC),
    CONSTRAINT [UQ_ProtocolTools__FixedAssetPhysicalAssetId__MaintenanceProtocolId] UNIQUE NONCLUSTERED ([FixedAssetPhysicalAssetId] ASC, [MaintenanceProtocolId] ASC)
);


GO
ALTER TABLE [Maintenance].[ProtocolTools] NOCHECK CONSTRAINT [FK_ProtocolTools_FixedAssetPhysicalAsset];


GO
ALTER TABLE [Maintenance].[ProtocolTools] NOCHECK CONSTRAINT [FK_ProtocolTools_MaintenanceProtocol];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la herramienta utilizada (creada como activo fijo)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'FixedAssetPhysicalAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del protocolo de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'MaintenanceProtocolId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ProtocolTools', @level2type = N'COLUMN', @level2name = N'Id';

