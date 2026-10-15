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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeConstitutionCashId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sello de tiempo. Guarda el instante tiempo de la creación, registro o modificación de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la cuenta bancaria  1 - Tercero Cuenta Bancaria  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica de donde se obtiene el Tercero cuando se realicen movimientos de la Caja  1 - Tercero Caja  2 - Tercero Movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'GetThirdPartyCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Domingo Hábil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SundaySkillful';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Sábado Hábil', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SaturdaySkillful';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante Comprobantes de Egreso - Cruce', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransactionCrossing';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante Notas de Tesoreria', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeTreasuryNotes';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante Consignaciones Bancarias', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeBankAppropriations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante Comprobantes de Egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeVoucherTransaction';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de comprobante Recibos de Caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'JournalVoucherTypeCashReceipts';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de dias hacia atras que se puede modificar la fecha del documento    Nota: este campo solo se llena con un valor diferente a cero si el campo AllowModifyDocumentDateBank esta en true', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'NumberDayBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Permitir modificar las fechas de documentos de Bancos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'AllowModifyDocumentsDateBank';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Generar Orden de Pago Automaticamente', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BuildPaymentOrderAutomatic';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Controla el consecutivo de las chequeras de cuentas bancarias', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'CheckBookControl';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Parametriza Presupuesto por Concepto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'SettingBudgetforConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Confirma Presupuesto', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'BudgetConfirm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la unidad operativa', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'IdOperatingUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'SettingsTreasury', @level2type = N'COLUMN', @level2name = N'Id';

