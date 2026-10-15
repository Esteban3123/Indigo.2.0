CREATE TABLE [Cost].[CostGeneralExpense] (
    [Id]                            INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                          VARCHAR (20)  NOT NULL,
    [Name]                          VARCHAR (100) NOT NULL,
    [CostGeneralExpenseCategoryId]  INT           NOT NULL,
    [ElementCostType]               TINYINT       CONSTRAINT [DF_CostGeneralExpense_ElementCostType] DEFAULT ((5)) NOT NULL,
    [ExpenditureType]               TINYINT       NOT NULL,
    [Status]                        BIT           NOT NULL,
    [CreationUser]                  VARCHAR (20)  NOT NULL,
    [CreationDate]                  DATETIME      NOT NULL,
    [ModificationUser]              VARCHAR (20)  NULL,
    [ModificationDate]              DATETIME      NULL,
    [ConfirmUser]                   VARCHAR (20)  NULL,
    [ConfirmDate]                   DATETIME      NULL,
    [TimeStamp]                     ROWVERSION    NOT NULL,
    [DistributionType]              TINYINT       CONSTRAINT [DF__CostGener__Distr__24056691] DEFAULT ((1)) NOT NULL,
    [GenerateAccountPayable]        BIT           CONSTRAINT [DF__CostGener__Gener__11728061] DEFAULT ((1)) NULL,
    [JournalVoucherTypesId]         INT           NULL,
    [ReversalJournalVoucherTypesId] INT           NULL,
    CONSTRAINT [PK_CostGeneralExpense] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostGeneralExpense_CostGeneralExpenseCategory] FOREIGN KEY ([CostGeneralExpenseCategoryId]) REFERENCES [Cost].[CostGeneralExpenseCategory] ([Id]),
    CONSTRAINT [FK_CostGeneralExpense_JournalVoucherTypes] FOREIGN KEY ([JournalVoucherTypesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id]),
    CONSTRAINT [FK_CostGeneralExpense_ReversalJournalVoucherTypes] FOREIGN KEY ([ReversalJournalVoucherTypesId]) REFERENCES [GeneralLedger].[JournalVoucherTypes] ([Id])
);


GO
ALTER TABLE [Cost].[CostGeneralExpense] NOCHECK CONSTRAINT [FK_CostGeneralExpense_CostGeneralExpenseCategory];

