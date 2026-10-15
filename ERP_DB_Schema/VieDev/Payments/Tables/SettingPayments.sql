CREATE TABLE [Payments].[SettingPayments] (
    [Id]                             INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdJournalVoucherAccountPayable] INT           NOT NULL,
    [IdJournalVoucherTranslation]    INT           NOT NULL,
    [IdJournalVoucherCreditNotes]    INT           NOT NULL,
    [IdJournalVoucherDebitNotes]     INT           NOT NULL,
    [IdJournalVocuherAmortization]   INT           CONSTRAINT [DF_SettingPayments_IdJournalVocuherAmortization] DEFAULT ((7)) NOT NULL,
    [ObligationDebitValue]           BIT           NOT NULL,
    [BudgetInterface]                BIT           CONSTRAINT [DF_SettingPayments_BudgetInterface] DEFAULT ((0)) NOT NULL,
    [OnlyObligation]                 BIT           NOT NULL,
    [ObligationBudgetInterface]      BIT           NOT NULL,
    [IdOperatingUnit]                INT           NOT NULL,
    [NameMinimumAgeRange]            VARCHAR (100) NOT NULL,
    [NameMaximumAgeRange]            VARCHAR (100) NOT NULL,
    [MaximunAgeRange]                INT           NOT NULL,
    [State]                          BIT           NOT NULL,
    [CreationUser]                   VARCHAR (20)  NOT NULL,
    [CreationDate]                   DATETIME      NOT NULL,
    [ModificationUser]               VARCHAR (20)  NULL,
    [ModificationDate]               DATETIME      NULL,
    [TimeStamp]                      ROWVERSION    NOT NULL,
    [PostulateBudgetInterfaceBy]     TINYINT       CONSTRAINT [DF_SettingPayments_PostulateBudgetInterfaceBy] DEFAULT ((1)) NOT NULL,
    [AutomaticConfirmation]          BIT           CONSTRAINT [DF_SettingPayments_AutomaticConfirmation] DEFAULT ((0)) NOT NULL,
    [IdAccountingFactoring]          INT           NULL,
    [IdCashFlowConcept]              INT           NULL,
    [IdCashFlowConceptExpense]       INT           NULL,
    CONSTRAINT [PK_ParametersPayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher] FOREIGN KEY ([IdJournalVoucherAccountPayable]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher1] FOREIGN KEY ([IdJournalVoucherTranslation]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher2] FOREIGN KEY ([IdJournalVoucherCreditNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_ParametersPayment_JournalVoucher3] FOREIGN KEY ([IdJournalVoucherDebitNotes]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_CashFlowConcept] FOREIGN KEY ([IdCashFlowConcept]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_SettingPayments_CashFlowConceptExpense] FOREIGN KEY ([IdCashFlowConceptExpense]) REFERENCES [Treasury].[CashFlowConcept] ([Id]),
    CONSTRAINT [FK_SettingPayments_JournalVoucherTypes] FOREIGN KEY ([IdAccountingFactoring]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_JournalVoucherTypes_Amortization] FOREIGN KEY ([IdJournalVocuherAmortization]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_SettingPayments_OperatingUnit] FOREIGN KEY ([IdOperatingUnit]) REFERENCES [Common].[OperatingUnit] ([Id])
);


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_ParametersPayment_JournalVoucher];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_ParametersPayment_JournalVoucher1];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_ParametersPayment_JournalVoucher2];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_ParametersPayment_JournalVoucher3];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_SettingPayments_CashFlowConcept];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_SettingPayments_CashFlowConceptExpense];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_SettingPayments_JournalVoucherTypes];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_SettingPayments_JournalVoucherTypes_Amortization];


GO
ALTER TABLE [Payments].[SettingPayments] NOCHECK CONSTRAINT [FK_SettingPayments_OperatingUnit];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del concepto de gatos del flujo de efectivo', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'IdCashFlowConceptExpense';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valida si el factoring debe tener Confirmación automatica.  0 => No  1=> Si', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'AutomaticConfirmation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si la interfaz presupuestal se postula en:  1 - La Factura  2 - La Cuenta por Pagar    En desuso por actualización. Esto debido a que, como una cuenta por pagar puede tener muchos rubros presupuestales asociados, estos deben ser definidos factura a factura.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'PostulateBudgetInterfaceBy';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se crea una sola obligacion por el grupo de facturas asociada a la cuenta por pagar.    En desuso por actualización. Esto debido a que, como una cuenta por pagar puede tener muchos rubros presupuestales asociados, estos deben ser definidos factura a factura. Y el control se debe realizar una a una.', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'SettingPayments', @level2type = N'COLUMN', @level2name = N'OnlyObligation';

