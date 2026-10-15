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
ALTER TABLE [FixedAsset].[SettingFixedAssetByLegalBook] NOCHECK CONSTRAINT [FK_SettingFixedAssetByLegalBook_LegalBook];


GO
ALTER TABLE [FixedAsset].[SettingFixedAssetByLegalBook] NOCHECK CONSTRAINT [FK_SettingFixedAssetByLegalBook_SettingFixedAsset];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indica si se aplica o no la depreciación por menor (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'HandlesMinimumAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Incluir IVA al costo, como un mayor valor del activo (1 - SI, 0 - NO)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'IvaCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del libro contable', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'SettingFixedAssetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'SettingFixedAssetByLegalBook', @level2type = N'COLUMN', @level2name = N'Id';

