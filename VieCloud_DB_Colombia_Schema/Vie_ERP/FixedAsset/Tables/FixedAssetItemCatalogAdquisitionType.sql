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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cuenta contable principal (FK a GeneralLedger.MainAccounts). Código de cuenta en mayor general para registrar el activo fijo adquirido. Búsqueda: cuenta contable, cuenta mayor, código contable.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'MainAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro legal o libro de actas (FK a GeneralLedger.LegalBook). Registro contable y legal donde se documenta la adquisición del activo fijo. Búsqueda: libro contable, libro legal, libro de registro.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de adquisición del activo fijo (TINYINT). Valores: 1=Compra Directa, 3=Comodato, 4=Donación, 5=Traspaso de Bienes, 6=Otro Concepto, 7=Leasing Financiero, 8=Comodato Tercerizado, 9=Renting Financiero, 10=Renting Operativo. Define la forma de ingreso del bien.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Tipo de Adquisicion   1 - Compra Directa  3 - Comodato  4 - Donacion  5 - Traspaso de Bienes  6 - Otro Concepto  7 - Leasing Financiero  8 - Comodato Tercerizado  9 - Renting Financiero  10 - Renting Operativo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'AdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'AdquisitionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del ítem en catálogo de activos fijos (FK a FixedAsset.FixedAssetItemCatalog). Referencia al activo específico en el catálogo institucional. Búsqueda: catálogo de bienes, activo, equipo.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del catalogo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'ItemCatalogId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (INT IDENTITY). Clave primaria que identifica unívocamente cada combinación de tipo de adquisición para un ítem de catálogo. Búsqueda: registro, Id.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra los tipos de adquisición asociados a cada ítem del catálogo de activos fijos, vinculando cada combinación con el libro legal contable y la cuenta principal de contabilidad correspondiente.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetItemCatalogAdquisitionType';
