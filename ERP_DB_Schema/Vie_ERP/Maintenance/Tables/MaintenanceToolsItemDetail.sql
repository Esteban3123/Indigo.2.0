CREATE TABLE [Maintenance].[MaintenanceToolsItemDetail] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceToolsId] INT NOT NULL,
    [ItemAsset]          INT NOT NULL,
    CONSTRAINT [PK_MaintenanceToolsItemDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Maintenance_ItemAsset] FOREIGN KEY ([ItemAsset]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK__Maintenance_Tools] FOREIGN KEY ([MaintenanceToolsId]) REFERENCES [Maintenance].[MaintenanceTools] ([Id])
);


GO
ALTER TABLE [Maintenance].[MaintenanceToolsItemDetail] NOCHECK CONSTRAINT [FK__Maintenance_ItemAsset];


GO
ALTER TABLE [Maintenance].[MaintenanceToolsItemDetail] NOCHECK CONSTRAINT [FK__Maintenance_Tools];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del articulo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'ItemAsset';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de las herramientas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceToolsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'Id';

