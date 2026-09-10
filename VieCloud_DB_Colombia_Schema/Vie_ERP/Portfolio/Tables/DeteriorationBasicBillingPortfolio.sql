CREATE TABLE [Portfolio].[DeteriorationBasicBillingPortfolio] (
    [Id]                            INT IDENTITY (1, 1) NOT NULL,
    [SettingPortfolioId]            INT NOT NULL,
    [DebitDeteriorationAccount]     INT NOT NULL,
    [CreditDeteriorationAccount]    INT NOT NULL,
    [ReversalDeteriorationAccount]  INT NOT NULL,
    [PreviousPeriodReversalAccount] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DBBP_CreditAccount] FOREIGN KEY ([CreditDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_DebitAccount] FOREIGN KEY ([DebitDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_PreviousReversal] FOREIGN KEY ([PreviousPeriodReversalAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_ReversalAccount] FOREIGN KEY ([ReversalDeteriorationAccount]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_DBBP_SettingPortfolio] FOREIGN KEY ([SettingPortfolioId]) REFERENCES [Portfolio].[SettingPortfolio] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Llave foránea a los parámetros del portafolio', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'SettingPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable en el Mayor General (débito) para registrar el deterioro de cartera, glosa o incobrabilidad; vinculada a GeneralLedger.MainAccounts (FK).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'DebitDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de deterioro (débito). Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'DebitDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'DebitDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable en el Mayor General (crédito) para registrar el deterioro de cartera, glosa o incobrabilidad; vinculada a GeneralLedger.MainAccounts (FK).', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'CreditDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de deterioro (crédito). Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'CreditDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'CreditDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable en el Mayor General para registrar la reversión (ajuste negativo) del deterioro de cartera en el período actual; FK a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'ReversalDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de reversión de deterioro. Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'ReversalDeteriorationAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'ReversalDeteriorationAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cuenta contable en el Mayor General para registrar la reversión del deterioro de cartera que corresponde a períodos anteriores (ajuste acumulado); FK a GeneralLedger.MainAccounts.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cuenta contable de reversión de deterioro del periodo anterior. Foreign Key a GeneralLedger.MainAccounts', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'PreviousPeriodReversalAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Configuración contable del deterioro de cartera: define las cuentas contables (débito, crédito, reversión y reversión de período anterior) que se usan al registrar el deterioro de la cartera de facturación.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de configuración de deterioro de cartera.', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Portfolio', @level1type = N'TABLE', @level1name = N'DeteriorationBasicBillingPortfolio', @level2type = N'COLUMN', @level2name = N'Id';
