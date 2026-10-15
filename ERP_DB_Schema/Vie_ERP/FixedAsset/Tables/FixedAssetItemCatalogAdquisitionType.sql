CREATE TABLE [FixedAsset].[FixedAssetItemCatalogAdquisitionType] (
    [Id]              INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ItemCatalogId]   INT     NOT NULL,
    [AdquisitionType] TINYINT NOT NULL,
    [LegalBookId]     INT     NOT NULL,
    [MainAccountId]   INT     NOT NULL,
    CONSTRAINT [PK_FixedAssetItemCatalogAdquisitionType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_FixedAssetItemCatalog] FOREIGN KEY ([ItemCatalogId]) REFERENCES [FixedAsset].[FixedAssetItemCatalog] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogAdquisitionType] NOCHECK CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_FixedAssetItemCatalog];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogAdquisitionType] NOCHECK CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_LegalBook];


GO
ALTER TABLE [FixedAsset].[FixedAssetItemCatalogAdquisitionType] NOCHECK CONSTRAINT [FK_FixedAssetItemCatalogAdquisitionType_MainAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del catalogo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'Id';

