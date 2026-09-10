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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo final o revaluado (NUMERIC 18,2) de la caja o cuenta bancaria al término del período. Resultado de: Saldo Inicial - Movimientos + Ajustes. Expresado en la moneda de la caja/cuenta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'NewBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el saldo final de la caja al final del periodo de revalorizacion, La formula seria Saldo Inicial - Movimientos  Nota: estos saldos son en la moneda de la caja', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'NewBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'NewBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo inicial o anterior (NUMERIC 18,2) de la caja o cuenta bancaria al iniciar el período de revalorización. Base para cálculo de variaciones, en la moneda de la caja/cuenta.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'LastBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Saldo inicial de la caja o cuenta bancaria al comenzar el periodo  de revalorizacion', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'LastBalance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'LastBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) opcional que referencia la cuenta bancaria (EntityBankAccounts). Identifica cuál cuenta bancaria de la entidad se está revalorizando.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'BankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la cuenta bancaria que se va a revalorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'BankAccountId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'BankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) opcional que referencia la caja (CashRegisters). Identifica cuál caja/fondo de efectivo se está revalorizando en este período.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la caja que se va a revalorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'CashRegisterId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que vincula a TreasuryRevaluationControl. Identifica el período de revalorización, cierre contable o ejercicio fiscal al que pertenece este ajuste de saldo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de control para saber a que periodo pertenece la revalorizacion  ', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationControlId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'TreasuryRevaluationControlId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de revalorización de tesorería. Clave primaria que rastrea cada ajuste individual de saldo.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de revaluación de tesorería: ajustes de saldo por diferencia en cambio o actualización de valor en cajas y cuentas bancarias. Permite comparar el saldo anterior con el nuevo saldo tras el proceso de revaluación.', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryRevaluation';
