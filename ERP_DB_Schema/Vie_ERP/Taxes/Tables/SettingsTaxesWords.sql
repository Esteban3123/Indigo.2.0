CREATE TABLE [Taxes].[SettingsTaxesWords] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SettingsTaxesId] INT           NOT NULL,
    [Word]            VARCHAR (200) NOT NULL,
    [ThirdPartyId]    INT           NOT NULL,
    CONSTRAINT [PK_SettingsTaxesWords] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsTaxesWords_SettingsTaxes] FOREIGN KEY ([SettingsTaxesId]) REFERENCES [Taxes].[SettingsTaxes] ([Id]),
    CONSTRAINT [FK_SettingsTaxesWords_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Taxes].[SettingsTaxesWords] NOCHECK CONSTRAINT [FK_SettingsTaxesWords_SettingsTaxes];


GO
ALTER TABLE [Taxes].[SettingsTaxesWords] NOCHECK CONSTRAINT [FK_SettingsTaxesWords_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Palabra', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Word';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id ajuste de impuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'SettingsTaxesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'SettingsTaxesWords', @level2type = N'COLUMN', @level2name = N'Id';

