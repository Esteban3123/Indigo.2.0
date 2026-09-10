CREATE TABLE [Maintenance].[MaintenanceToolsItemDetail] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [MaintenanceToolsId] INT NOT NULL,
    [ItemAsset]          INT NOT NULL,
    CONSTRAINT [PK_MaintenanceToolsItemDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__Maintenance_ItemAsset] FOREIGN KEY ([ItemAsset]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK__Maintenance_Tools] FOREIGN KEY ([MaintenanceToolsId]) REFERENCES [Maintenance].[MaintenanceTools] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del artículo o activo fijo asociado a la herramienta de mantenimiento (referencia a FixedAssetItem)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'ItemAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del articulo', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'ItemAsset';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'ItemAsset';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del conjunto o registro de herramientas de mantenimiento al cual pertenece este detalle (referencia a MaintenanceTools)', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceToolsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de las herramientas', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceToolsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'MaintenanceToolsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) que identifica cada relación entre herramienta y artículo en el detalle de mantenimiento', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de ítems o activos asociados a cada herramienta de mantenimiento. Relaciona las herramientas registradas en el sistema con los activos o elementos específicos que las componen o utilizan.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'MaintenanceToolsItemDetail';
