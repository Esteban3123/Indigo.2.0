CREATE TABLE [Treasury].[BankReconciliation] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                   VARCHAR (20)    NOT NULL,
    [EntityBankAccountId]    INT             NOT NULL,
    [DocumentDate]           DATETIME        NOT NULL,
    [EntityBankAccountValue] DECIMAL (18, 2) NOT NULL,
    [ExtractValue]           DECIMAL (18, 2) NOT NULL,
    [Status]                 TINYINT         NOT NULL,
    [CreationUser]           VARCHAR (20)    NOT NULL,
    [CreationDate]           DATETIME        NOT NULL,
    [ModificationUser]       VARCHAR (20)    NULL,
    [ModificationDate]       DATETIME        NULL,
    [TimeStamp]              ROWVERSION      NOT NULL,
    CONSTRAINT [PK_BankReconciliation__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BankReconciliation_EntityBankAccounts] FOREIGN KEY ([EntityBankAccountId]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[BankReconciliation] NOCHECK CONSTRAINT [FK_BankReconciliation_EntityBankAccounts];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado  1-Registrado  2-Confirmado  3-Anulado', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'BankReconciliation', @level2type = N'COLUMN', @level2name = N'Status';

