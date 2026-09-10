CREATE TABLE [Maintenance].[ResponsibleCatalogOfArticles] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ResponsibleId] INT NOT NULL,
    [ItemCatalogId] INT NOT NULL,
    CONSTRAINT [PK_ResponsibleFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResponsibleCatalogOfArticles_ItemCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_ResponsibleCatalogOfArticles_MaintenanceResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [Maintenance].[MaintenanceResponsible] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del catálogo de artículos/productos de activo fijo. Referencia a FixedAssetItemCatalog. Vincula el artículo mantenible al catálogo de inventario de bienes.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del catalogo del producto', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del responsable de mantenimiento. Referencia a MaintenanceResponsible. Persona o unidad funcional asignada al cuidado y control del artículo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ResponsibleId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria de la relación entre responsable y artículo de catálogo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de artículos asignados a responsables: relaciona cada responsable o encargado con los artículos o ítems del catálogo de mantenimiento que tiene a su cargo.', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles';
