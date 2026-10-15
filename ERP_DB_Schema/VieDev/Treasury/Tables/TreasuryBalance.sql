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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo del Documento que genera el movimiento:  1 - Recibo de Caja  2 - Comrpobante de Egreso  3 - Consignaciones  4 - Notas  5 - Fondo de Caja Menor', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'TreasuryBalance', @level2type = N'COLUMN', @level2name = N'DocumentType';

