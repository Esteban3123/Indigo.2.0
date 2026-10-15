CREATE TABLE [Treasury].[ExpenseConceptCashRegisters] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdExpenseConcept] INT NOT NULL,
    [IdCashRegister]   INT NOT NULL,
    CONSTRAINT [PK_ExpenseConceptCashRegister] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExpenseConceptCashRegister_CashRegister] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_ExpenseConceptCashRegister_ExpenseConcept] FOREIGN KEY ([IdExpenseConcept]) REFERENCES [Treasury].[ExpenseConcepts] ([Id])
);


GO
ALTER TABLE [Treasury].[ExpenseConceptCashRegisters] NOCHECK CONSTRAINT [FK_ExpenseConceptCashRegister_CashRegister];


GO
ALTER TABLE [Treasury].[ExpenseConceptCashRegisters] NOCHECK CONSTRAINT [FK_ExpenseConceptCashRegister_ExpenseConcept];

