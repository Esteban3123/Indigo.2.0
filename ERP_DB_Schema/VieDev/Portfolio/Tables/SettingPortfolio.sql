CREATE TABLE [Portfolio].[SettingPortfolio] (
    [Id]                                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OperatingUnitId]                               INT           NOT NULL,
    [JournalVoucherTypeCreditNotesId]               INT           NOT NULL,
    [JournalVoucherTypeDebitNotesId]                INT           NOT NULL,
    [JournalVoucherTypeTranslationId]               INT           NOT NULL,
    [JournalVoucherTypeProvisionId]                 INT           NOT NULL,
    [JournalVoucherTypeFilingAccountId]             INT           NOT NULL,
    [JournalVoucherTypeDocumentAccountReceivableId] INT           NOT NULL,
    [NameMinimumAgeRange]                           VARCHAR (100) NOT NULL,
    [NameMaximumAgeRange]                           VARCHAR (100) NOT NULL,
    [MaximunAgeRange]                               INT           NOT NULL,
    [State]                                         BIT           NOT NULL,
    [CreationUser]                                  VARCHAR (20)  NOT NULL,
    [CreationDate]                                  DATETIME      NOT NULL,
    [ModificationUser]                              VARCHAR (20)  NULL,
    [ModificationDate]                              DATETIME      NULL,
    [TimeStamp]                                     ROWVERSION    NOT NULL,
    [JournalVoucherTypeHardCollectionId]            INT           NOT NULL,
    [JournalVoucherTypeDeteriorationAccountId]      INT           NULL,
    [LegalCollection]                               BIT           CONSTRAINT [DF_SettingPortfolio_LegalCollection] DEFAULT ((1)) NOT NULL,
    [DependencyId]                                  INT           NULL,
    [UnReconciledInvoice]                           BIT           NULL,
    [Transfers]                                     BIT           NOT NULL,
    [NotesDebitCreditPortfolio]                     BIT           NOT NULL,
    CONSTRAINT [PK_SettingPortfolio] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingPortfolio_Dependency] FOREIGN KEY ([DependencyId]) REFERENCES [Budget].[Dependency] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeDocumentAccountReceivableId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeHardCollectionId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeDeteriorationAccountId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeCreditNotesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeDebitNotesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeFilingAccountId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes3] FOREIGN KEY ([JournalVoucherTypeProvisionId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes4] FOREIGN KEY ([JournalVoucherTypeTranslationId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsPortfolio_OperatingUnit] FOREIGN KEY ([OperatingUnitId]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_Dependency];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes1];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingPortfolio_JournalVoucherTypes2];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes1];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes2];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes3];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_JournalVoucherTypes4];


GO
ALTER TABLE [Portfolio].[SettingPortfolio] NOCHECK CONSTRAINT [FK_SettingsPortfolio_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo booleano que especifica si tiene colección legal, 1 - Si, 0 - No.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'SettingPortfolio', @level2type = N'COLUMN', @level2name = N'LegalCollection';

