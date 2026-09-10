CREATE TABLE [FixedAsset].[SettingFixedAssetByLegalBook] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingFixedAssetId]  INT NOT NULL,
    [LegalBookId]          INT NOT NULL,
    [IvaCost]              BIT NOT NULL,
    [HandlesMinimumAmount] BIT NOT NULL,
    CONSTRAINT [PK_SettingFixedAssetByLegalBook] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingFixedAssetByLegalBook_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_SettingFixedAssetByLegalBook_SettingFixedAsset] FOREIGN KEY ([SettingFixedAssetId]) REFERENCES [FixedAsset].[SettingFixedAsset] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=SÍ, 0=NO) de si aplica depreciación por menor cuantía o umbral mínimo según normativa del libro legal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'HandlesMinimumAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Indica si se aplica o no la depreciación por menor (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'HandlesMinimumAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'HandlesMinimumAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT: 1=SÍ, 0=NO) de si incluir el IVA en el costo del activo fijo, aumentando su base de depreciación contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Incluir IVA al costo, como un mayor valor del activo (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'IvaCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del libro contable legal (FK a GeneralLedger.LegalBook), define el contexto normativo y fiscal para el activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del libro contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera de parámetros de activos fijos (FK a FixedAsset.SettingFixedAsset), enlaza reglas generales de depreciación', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'SettingFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'SettingFixedAssetId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'SettingFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, clave primaria, auto-incremental) de la configuración de activo fijo por libro legal', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración que asocia activos fijos con libros legales contables, definiendo si el costo incluye IVA y si se maneja un monto mínimo de activación para cada combinación de configuración y libro legal.', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook';
