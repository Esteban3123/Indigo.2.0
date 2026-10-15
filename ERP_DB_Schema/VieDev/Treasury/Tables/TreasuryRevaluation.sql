CREATE TABLE [Treasury].[TreasuryRevaluation] (
    [Id]                           INT             IDENTITY (1, 1) NOT NULL,
    [TreasuryRevaluationControlId] INT             NOT NULL,
    [CashRegisterId]               INT             NULL,
    [BankAccountId]                INT             NULL,
    [LastBalance]                  NUMERIC (18, 2) NOT NULL,
    [NewBalance]                   NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_TreasuryRevaluation] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryRevaluation_CashRegister] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_TreasuryRevaluation_EntityBankAccounts] FOREIGN KEY ([BankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_TreasuryRevaluation_TreasuryRevaluationControl] FOREIGN KEY ([TreasuryRevaluationControlId]) REFERENCES [Treasury].[TreasuryRevaluationControl] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasuryRevaluation] NOCHECK CONSTRAINT [FK_TreasuryRevaluation_CashRegister];


GO
ALTER TABLE [Treasury].[TreasuryRevaluation] NOCHECK CONSTRAINT [FK_TreasuryRevaluation_EntityBankAccounts];


GO
ALTER TABLE [Treasury].[TreasuryRevaluation] NOCHECK CONSTRAINT [FK_TreasuryRevaluation_TreasuryRevaluationControl];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el saldo final de la caja al final del periodo de revalorizacion, La formula seria Saldo Inicial - Movimientos  Nota: estos saldos son en la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'NewBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo inicial de la caja o cuenta bancaria al comenzar el periodo  de revalorizacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'LastBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la cuenta bancaria que se va a revalorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'BankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja que se va a revalorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de control para saber a que periodo pertenece la revalorizacion  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'Id';

