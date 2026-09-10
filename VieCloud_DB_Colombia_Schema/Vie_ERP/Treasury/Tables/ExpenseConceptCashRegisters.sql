CREATE TABLE [Treasury].[ExpenseConceptCashRegisters] (
    [Id]               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdExpenseConcept] INT NOT NULL,
    [IdCashRegister]   INT NOT NULL,
    CONSTRAINT [PK_ExpenseConceptCashRegister] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ExpenseConceptCashRegister_CashRegister] FOREIGN KEY ([IdCashRegister]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_ExpenseConceptCashRegister_ExpenseConcept] FOREIGN KEY ([IdExpenseConcept]) REFERENCES [Treasury].[ExpenseConcepts] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la caja registradora o punto de caja (FK a Treasury.CashRegisters). Referencia la caja donde se registran movimientos de efectivo y egresos.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdCashRegister';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdCashRegister';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del concepto de egreso, gasto o movimiento de salida (FK a Treasury.ExpenseConcepts). Clasifica el tipo de gasto o concepto contable asociado.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id del concepto de egresos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'IdExpenseConcept';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de relación entre concepto de egreso y caja registradora. Vincula conceptos de gasto a cajas específicas para control de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del concepto de recibo de caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre conceptos de gasto y cajas registradoras habilitadas para manejarlos; indica qué tipos de egresos o pagos pueden registrarse en cada caja del módulo de tesorería.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'ExpenseConceptCashRegisters';
