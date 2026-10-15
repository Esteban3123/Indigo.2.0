CREATE TABLE [Budget].[Budget] (
    [Id]                      INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BudgetHeaderId]          INT          NOT NULL,
    [CategoryId]              INT          NOT NULL,
    [RevenueTypeId]           INT          NOT NULL,
    [InitialValue]            NUMERIC (18) NOT NULL,
    [DebitValueModification]  NUMERIC (18) NOT NULL,
    [CreditValueModification] NUMERIC (18) NOT NULL,
    [DebitValueTransfer]      NUMERIC (18) NOT NULL,
    [CreditValueTransfer]     NUMERIC (18) NOT NULL,
    [TotalBudget]             NUMERIC (18) NOT NULL,
    [ExecutedValue]           NUMERIC (18) NOT NULL,
    [SuspendedValue]          NUMERIC (18) NOT NULL,
    [Balance]                 NUMERIC (18) NOT NULL,
    [CreationUser]            VARCHAR (20) CONSTRAINT [DF_Budget_CreationUser] DEFAULT ((999)) NOT NULL,
    [CreationDate]            DATETIME     CONSTRAINT [DF_Budget_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    [ModificationUser]        VARCHAR (20) NULL,
    [ModificationDate]        DATETIME     NULL,
    [TimeStamp]               ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Budget__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_Budget_Budget_ValidateValues] CHECK ((((([InitialValue]-[DebitValueModification])+[CreditValueModification])-[DebitValueTransfer])+[CreditValueTransfer])=[TotalBudget] AND ([TotalBudget]-[ExecutedValue])=[Balance]),
    CONSTRAINT [FK_Budget_BudgetHeader] FOREIGN KEY ([BudgetHeaderId]) REFERENCES [Budget].[BudgetHeader] ([Id]),
    CONSTRAINT [FK_Budget_Category] FOREIGN KEY ([CategoryId]) REFERENCES [Budget].[Category] ([Id]),
    CONSTRAINT [FK_Budget_RevenueType] FOREIGN KEY ([RevenueTypeId]) REFERENCES [Budget].[RevenueType] ([Id]),
    CONSTRAINT [UQ_Budget__CategoryId__RevenueTypeId] UNIQUE NONCLUSTERED ([CategoryId] ASC, [RevenueTypeId] ASC)
);


GO
ALTER TABLE [Budget].[Budget] NOCHECK CONSTRAINT [CK_Budget_Budget_ValidateValues];


GO
ALTER TABLE [Budget].[Budget] NOCHECK CONSTRAINT [FK_Budget_BudgetHeader];


GO
ALTER TABLE [Budget].[Budget] NOCHECK CONSTRAINT [FK_Budget_Category];


GO
ALTER TABLE [Budget].[Budget] NOCHECK CONSTRAINT [FK_Budget_RevenueType];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo del presupuesto el cual se obtiene de la siguiente manera     TotalBudget - ExecutedValue - SuspendValue', @level0type = N'SCHEMA', @level0name = N'Budget', @level1type = N'TABLE', @level1name = N'Budget', @level2type = N'COLUMN', @level2name = N'Balance';

