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

