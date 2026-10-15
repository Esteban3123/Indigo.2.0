CREATE TABLE [Billing].[BillingConceptAccount] (
    [Id]                             INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BillingConceptId]               INT     NOT NULL,
    [UnitType]                       TINYINT NOT NULL,
    [EntityIncomeAccountId]          INT     NOT NULL,
    [IndividualIncomeAccountId]      INT     NOT NULL,
    [FeesExpensesAccountId]          INT     NULL,
    [IncomeRecognitionMainAccountId] INT     NULL,
    [DiscountAccountId]              INT     NULL,
    CONSTRAINT [PK_BillingConceptAccount] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BillingConceptAccount_BillingConcept] FOREIGN KEY ([BillingConceptId]) REFERENCES [Billing].[BillingConcept] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_DiscountAccount] FOREIGN KEY ([DiscountAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts] FOREIGN KEY ([EntityIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts1] FOREIGN KEY ([IndividualIncomeAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts2] FOREIGN KEY ([FeesExpensesAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_BillingConceptAccount_MainAccounts3] FOREIGN KEY ([IncomeRecognitionMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_BillingConcept];


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_DiscountAccount];


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_MainAccounts];


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_MainAccounts1];


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_MainAccounts2];


GO
ALTER TABLE [Billing].[BillingConceptAccount] NOCHECK CONSTRAINT [FK_BillingConceptAccount_MainAccounts3];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable, donde se registra el valor de los movimientos de descuento', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'DiscountAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cuenta nif para reconocimiento de ingresos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IncomeRecognitionMainAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable para Gastos de Honorarios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'FeesExpensesAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable para ingresos a particulares', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'IndividualIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta contable de ingresos de la entidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'EntityIncomeAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de unidad  1 - Urgencias  2 - Hospitalizacion  3 - Quirofanos  4 - Servicios Ambulatorios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'UnitType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de facturacion', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'BillingConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BillingConceptAccount', @level2type = N'COLUMN', @level2name = N'Id';

