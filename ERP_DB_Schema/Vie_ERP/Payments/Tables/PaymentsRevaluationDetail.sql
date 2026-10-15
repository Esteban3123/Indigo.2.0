CREATE TABLE [Payments].[PaymentsRevaluationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [PaymentsRevaluationId]      INT             NOT NULL,
    [DocumentType]               TINYINT         CONSTRAINT [DF_PaymentsRevaluationDetail_DocumentType] DEFAULT ((1)) NOT NULL,
    [AccountPayableId]           INT             NULL,
    [AdvancePaymentId]           INT             NULL,
    [ThirdPartyId]               INT             NULL,
    [CostCenterId]               INT             NULL,
    [Value]                      NUMERIC (18, 2) NOT NULL,
    [Balance]                    NUMERIC (18, 2) NOT NULL,
    [CurrencyId]                 INT             NOT NULL,
    [CurrencyConverterId]        INT             NOT NULL,
    [ValueCurrency]              NUMERIC (20, 5) NOT NULL,
    [ValueCurrencyReverse]       NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrency]        NUMERIC (20, 5) NOT NULL,
    [ActualValueCurrencyReverse] NUMERIC (20, 5) NOT NULL,
    [MainAccountId]              INT             NOT NULL,
    [LegalBookId]                INT             NOT NULL,
    [BalanceConverted]           NUMERIC (18, 2) NOT NULL,
    [ActualBalanceConverter]     NUMERIC (18, 2) NOT NULL,
    [ProfitLostValue]            NUMERIC (18, 2) CONSTRAINT [DF_PaymentsRevaluationDetail_ProfitLostValue] DEFAULT ((0)) NOT NULL,
    [DeferredCausationId]        INT             NULL,
    CONSTRAINT [PK_RevaluationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PaymentsRevaluation_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluation_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_CurrencyConverter] FOREIGN KEY ([CurrencyConverterId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_DeferredCausation] FOREIGN KEY ([DeferredCausationId]) REFERENCES [Payments].[DeferredCausation] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_PaymentsRevaluation] FOREIGN KEY ([PaymentsRevaluationId]) REFERENCES [Payments].[PaymentsRevaluation] ([Id]),
    CONSTRAINT [FK_PaymentsRevaluationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluation_AccountPayable];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluation_AdvancePayments];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_CostCenter];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_Currency];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_CurrencyConverter];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_DeferredCausation];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_LegalBook];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_MainAccounts];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_PaymentsRevaluation];


GO
ALTER TABLE [Payments].[PaymentsRevaluationDetail] NOCHECK CONSTRAINT [FK_PaymentsRevaluationDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relacion con el documento de causacion diferida de cxp', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DeferredCausationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor perdido de ganancias', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convertidor de saldo actual', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance convertido', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del libro legal', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Moneda de valor real', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del conversor de moneda ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del centro de costos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del pago por adelantado', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AdvancePaymentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por pagar', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountPayableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Especifica el tipo de documento   1 - Cuentas por Pagar   2 - Anticipos de CxP. 
3 - Diferidos ', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la revaluación de pagos', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'PaymentsRevaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Payments', @level1type = N'TABLE', @level1name = N'PaymentsRevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';

