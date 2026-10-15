CREATE TABLE [FixedAsset].[FixedAssetReclassification] (
    [Id]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]       INT           NOT NULL,
    [Code]                  VARCHAR (20)  NOT NULL,
    [DocumentDate]          DATETIME      NOT NULL,
    [ReclassificationType]  TINYINT       NOT NULL,
    [Detail]                VARCHAR (300) NOT NULL,
    [Status]                TINYINT       NOT NULL,
    [ItemIdPrevious]        INT           NOT NULL,
    [ItemCatalogIdPrevious] INT           NOT NULL,
    [ItemId]                INT           NULL,
    [ItemCatalogId]         INT           NOT NULL,
    [CreationUser]          VARCHAR (20)  NOT NULL,
    [CreationDate]          DATETIME      NOT NULL,
    [ModificationUser]      VARCHAR (20)  NULL,
    [ModificationDate]      DATETIME      NULL,
    [ConfirmationUser]      VARCHAR (20)  NULL,
    [ConfirmationDate]      DATETIME      NULL,
    [AnnulmentUser]         VARCHAR (20)  NULL,
    [AnnulmentDate]         DATETIME      NULL,
    [TimeStamp]             ROWVERSION    NULL,
    CONSTRAINT [PK_FixedAssetReclassification] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemCatalogPrevious] FOREIGN KEY ([ItemCatalogIdPrevious]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemPrevious] FOREIGN KEY ([ItemIdPrevious]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetReclassification_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassification] NOCHECK CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassification] NOCHECK CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemCatalog];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassification] NOCHECK CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemCatalogPrevious];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassification] NOCHECK CONSTRAINT [FK_FixedAssetReclassification_FixedAssetItemPrevious];


GO
ALTER TABLE [FixedAsset].[FixedAssetReclassification] NOCHECK CONSTRAINT [FK_FixedAssetReclassification_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catálogo a asignar.  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassification', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del artículo a asignar.  Si el tipo de reclasificación es de artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassification', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del artículo que se tenía antes de la reclasificación.  ', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassification', @level2type = N'COLUMN', @level2name = N'ItemIdPrevious';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado de la reclasificación del artículo  1 - Registrado  2 - Confirmado  3 - Anulado', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassification', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de reclasificación  1 - Corrección Catálogo  2 - Corrección Artículo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetReclassification', @level2type = N'COLUMN', @level2name = N'ReclassificationType';

