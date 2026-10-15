CREATE TABLE [Treasury].[CheckCashingControl] (
    [Id]              INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityAccount] INT             NOT NULL,
    [DueDate]         DATETIME        NOT NULL,
    [Observation]     VARCHAR (MAX)   NULL,
    [BalanceAccount]  DECIMAL (18, 2) NOT NULL,
    [CreationUser]    VARCHAR (20)    NOT NULL,
    [CreationDate]    DATETIME        NOT NULL,
    CONSTRAINT [PK_CheckCashingControl] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckCashingControl_EntityBankAccount] FOREIGN KEY ([IdEntityAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[CheckCashingControl] NOCHECK CONSTRAINT [FK_CheckCashingControl_EntityBankAccount];

