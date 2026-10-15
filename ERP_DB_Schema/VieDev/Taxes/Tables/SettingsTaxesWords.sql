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

