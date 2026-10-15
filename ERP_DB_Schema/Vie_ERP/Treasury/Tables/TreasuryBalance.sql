CREATE TABLE [Treasury].[TreasuryBalance] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentNumber]      VARCHAR (20)    NOT NULL,
    [DocumentDate]        DATETIME        NOT NULL,
    [DocumentType]        TINYINT         NOT NULL,
    [Nature]              TINYINT         NOT NULL,
    [CashRegisterId]      INT             NULL,
    [EntityBankAccountId] INT             NULL,
    [PreviousBalance]     DECIMAL (18, 2) NOT NULL,
    [ValueMovement]       DECIMAL (18, 2) NOT NULL,
    [CreationDate]        DATETIME        CONSTRAINT [DF_TreasuryBalance_CreationDate] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_TreasuryBalance] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TreasuryBalance_CashRegisters] FOREIGN KEY ([CashRegisterId]) REFERENCES [Treasury].[CashRegisters] ([Id]),
    CONSTRAINT [FK_TreasuryBalance_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[TreasuryBalance] NOCHECK CONSTRAINT [FK_TreasuryBalance_CashRegisters];


GO
ALTER TABLE [Treasury].[TreasuryBalance] NOCHECK CONSTRAINT [FK_TreasuryBalance_EntityBankAccounts];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_TreasuryBalance]
    ON [Treasury].[TreasuryBalance]([DocumentNumber] ASC, [DocumentType] ASC, [Nature] ASC, [CashRegisterId] ASC, [EntityBankAccountId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fecha de la creacion del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'ValueMovement';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Saldo anterior de la caja o cuenta bancaria, el saldo actual se calcularia sumando el saldo anterior y el valor del movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'PreviousBalance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria donde se realizo el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'EntityBankAccountId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la caja donde se realizo el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'CashRegisterId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Naturaleza del documento que genera movimiento, 1 - Debito, 2 - Credito', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Nature';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del Documento que genera el movimiento:  1 - Recibo de Caja  2 - Comrpobante de Egreso  3 - Consignaciones  4 - Notas  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del documento que genera el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del documento que genera el movimiento', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro de saldos', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'Id';

