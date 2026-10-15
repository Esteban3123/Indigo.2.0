CREATE TABLE [Portfolio].[DeteriorationBasicBillingPortfolio] (
    [Id]                                INT             IDENTITY (1, 1) NOT NULL,
    [SettingPortfolioId]                INT             NOT NULL,
    [DebitDeteriorationAccount]         INT             NOT NULL,
    [CreditDeteriorationAccount]        INT             NOT NULL,
    [ReversalDeteriorationAccount]      INT             NOT NULL,
    [PreviousPeriodReversalAccount]     INT             NOT NULL,
    CONSTRAINT [PK_DeteriorationBasicBillingPortfolio__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DBBP_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id]),
    CONSTRAINT [FK_DBBP_DebitAccount] FOREIGN KEY ([DebitDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_CreditAccount] FOREIGN KEY ([CreditDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_ReversalAccount] FOREIGN KEY ([ReversalDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_PreviousReversal] FOREIGN KEY ([PreviousPeriodReversalAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foránea a los parámetros del portafolio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de deterioro (débito). Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'DebitDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de deterioro (crédito). Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'CreditDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de reversión de deterioro. Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'ReversalDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta contable de reversión de deterioro del periodo anterior. Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccount';


