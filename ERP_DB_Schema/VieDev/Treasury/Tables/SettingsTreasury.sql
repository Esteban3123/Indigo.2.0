CREATE TABLE [Treasury].[SettingsTreasury] (
    [Id]                                           INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdOperatingUnit]                              INT          NOT NULL,
    [BudgetConfirm]                                BIT          NOT NULL,
    [SettingBudgetforConcept]                      BIT          NOT NULL,
    [CheckBookControl]                             BIT          NOT NULL,
    [BuildPaymentOrderAutomatic]                   BIT          NOT NULL,
    [AllowModifyDocumentsDateBank]                 BIT          NOT NULL,
    [NumberDayBank]                                INT          CONSTRAINT [DF_SettingsTreasury_NumberDayBank] DEFAULT ((0)) NOT NULL,
    [JournalVoucherTypeCashReceipts]               INT          NOT NULL,
    [JournalVoucherTypeVoucherTransaction]         INT          NOT NULL,
    [JournalVoucherTypeBankAppropriations]         INT          NOT NULL,
    [JournalVoucherTypeTreasuryNotes]              INT          NOT NULL,
    [JournalVoucherTypeVoucherTransactionCrossing] INT          NOT NULL,
    [SaturdaySkillful]                             BIT          NOT NULL,
    [SundaySkillful]                               BIT          NOT NULL,
    [GetThirdPartyCashRegister]                    TINYINT      CONSTRAINT [DF_SettingsTreasury_GetThirdPartyCashRegister] DEFAULT ((1)) NOT NULL,
    [GetThirdPartyBank]                            TINYINT      CONSTRAINT [DF_SettingsTreasury_GetThirdPartyBank] DEFAULT ((1)) NOT NULL,
    [CreationUser]                                 VARCHAR (20) NOT NULL,
    [CreationDate]                                 DATETIME     NOT NULL,
    [ModificationUser]                             VARCHAR (20) NULL,
    [ModificationDate]                             DATETIME     NULL,
    [TimeStamp]                                    ROWVERSION   NOT NULL,
    [JournalVoucherTypeConstitutionCashId]         INT          NULL,
    CONSTRAINT [PK_SettingsTreasury] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypeBankAppropriations]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes1] FOREIGN KEY ([JournalVoucherTypeCashReceipts]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes2] FOREIGN KEY ([JournalVoucherTypeTreasuryNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes3] FOREIGN KEY ([JournalVoucherTypeVoucherTransaction]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes4] FOREIGN KEY ([JournalVoucherTypeVoucherTransactionCrossing]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes5] FOREIGN KEY ([JournalVoucherTypeConstitutionCashId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes];


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes1];


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes2];


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes3];


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes4];


GO
ALTER TABLE [Treasury].[SettingsTreasury] NOCHECK CONSTRAINT [FK_SettingsTreasury_JournalVoucherTypes5];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la cuenta bancaria  1 - Tercero Cuenta Bancaria  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la Caja  1 - Tercero Caja  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias hacia atras que se puede modificar la fecha del documento    Nota: este campo solo se llena con un valor diferente a cero si el campo AllowModifyDocumentDateBank esta en true', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'NumberDayBank';

