CREATE TABLE [Taxes].[TaxesLiquidationConcept] (
    [Id]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]            VARCHAR (20)  NOT NULL,
    [Name]            VARCHAR (200) NOT NULL,
    [ConceptType]     TINYINT       NOT NULL,
    [DebitAccountId]  INT           NOT NULL,
    [CreditAccountId] INT           NOT NULL,
    [BudgetId]        INT           NULL,
    CONSTRAINT [PK_TaxesLiquidationConcept] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TaxesLiquidationConcept_Budget] FOREIGN KEY ([BudgetId]) REFERENCES [Budget].[Budget] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts] FOREIGN KEY ([DebitAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts1] FOREIGN KEY ([CreditAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [Taxes].[TaxesLiquidationConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationConcept_Budget];


GO
ALTER TABLE [Taxes].[TaxesLiquidationConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts];


GO
ALTER TABLE [Taxes].[TaxesLiquidationConcept] NOCHECK CONSTRAINT [FK_TaxesLiquidationConcept_MainAccounts1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de presupuesto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'BudgetId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta Credito del concepto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'CreditAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cuenta Debito del concepto', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'DebitAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de concepto   1 - Predial  2 - Bomberil  3 - CAM', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'ConceptType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TaxesLiquidationConcept', @level2type = N'COLUMN', @level2name = N'Id';

