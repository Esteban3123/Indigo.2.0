CREATE TABLE [Portfolio].[RevaluationDetail] (
    [Id]                         INT             IDENTITY (1, 1) NOT NULL,
    [RevaluationId]              INT             NOT NULL,
    [DocumentType]               TINYINT         CONSTRAINT [DF_RevaluationDetail_DocumentType] DEFAULT ((1)) NOT NULL,
    [PortfolioAdvanceId]         INT             NULL,
    [AccountReceivableId]        INT             NULL,
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
    [ProfitLostValue]            NUMERIC (18, 2) CONSTRAINT [DF_RevaluationDetail_ProfitLostValue] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RevaluationDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RevaluationDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_CostCenter] FOREIGN KEY ([CostCenterId]) REFERENCES [Payroll].[CostCenter] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_Currency] FOREIGN KEY ([CurrencyId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_CurrencyConverter] FOREIGN KEY ([CurrencyConverterId]) REFERENCES [Common].[Currency] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_LegalBook] FOREIGN KEY ([LegalBookId]) REFERENCES [GeneralLedger].[LegalBook] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_MainAccounts] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_PortfolioAdvance] FOREIGN KEY ([PortfolioAdvanceId]) REFERENCES [Portfolio].[PortfolioAdvance] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_Revaluation] FOREIGN KEY ([RevaluationId]) REFERENCES [Portfolio].[Revaluation] ([Id]),
    CONSTRAINT [FK_RevaluationDetail_ThirdParty] FOREIGN KEY ([ThirdPartyId]) REFERENCES [Common].[ThirdParty] ([Id])
);


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_AccountReceivable];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_CostCenter];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_Currency];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_CurrencyConverter];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_LegalBook];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_MainAccounts];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_PortfolioAdvance];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_Revaluation];


GO
ALTER TABLE [Portfolio].[RevaluationDetail] NOCHECK CONSTRAINT [FK_RevaluationDetail_ThirdParty];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor perdido de ganancias', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ProfitLostValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convertidor de saldo actual', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualBalanceConverter';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Convertir saldo', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'BalanceConverted';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del libro legal', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'LegalBookId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta contable', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'MainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor real Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Moneda valor real', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ActualValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor Moneda Inversa', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrencyReverse';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de la moneda  ', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ValueCurrency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Convertidor de divisas', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyConverterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la moneda', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CurrencyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Balance', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Value';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Centro de costos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'CostCenterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del tercero', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'ThirdPartyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cuenta por cobrar', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'AccountReceivableId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Avance de cartera', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'PortfolioAdvanceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de documento   1 - Cuentas por cobrar  2 - Anticipos', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Revalorización', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'RevaluationId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'RevaluationDetail', @level2type = N'COLUMN', @level2name = N'Id';

