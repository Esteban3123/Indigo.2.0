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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del concepto de egresos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del concepto de recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'Id';

