CREATE TABLE [Maintenance].[ResponsibleCatalogOfArticles] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ResponsibleId] INT NOT NULL,
    [ItemCatalogId] INT NOT NULL,
    CONSTRAINT [PK_ResponsibleFunctionalUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ResponsibleCatalogOfArticles_ItemCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_ResponsibleCatalogOfArticles_MaintenanceResponsible] FOREIGN KEY ([ResponsibleId]) REFERENCES [Maintenance].[MaintenanceResponsible] ([Id])
);


GO
ALTER TABLE [Maintenance].[ResponsibleCatalogOfArticles] NOCHECK CONSTRAINT [FK_ResponsibleCatalogOfArticles_ItemCatalog];


GO
ALTER TABLE [Maintenance].[ResponsibleCatalogOfArticles] NOCHECK CONSTRAINT [FK_ResponsibleCatalogOfArticles_MaintenanceResponsible];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catalogo del producto', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Responsable', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'ResponsibleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Maintenance', @level1type = N'TABLE', @level1name = N'ResponsibleCatalogOfArticles', @level2type = N'COLUMN', @level2name = N'Id';

