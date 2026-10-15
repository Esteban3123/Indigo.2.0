CREATE TABLE [Treasury].[EntityBankAccountUser] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBankAccount] INT           NOT NULL,
    [CodUser]             VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_EntityBankAccountUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccountUser_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[EntityBankAccountUser] NOCHECK CONSTRAINT [FK_EntityBankAccountUser_EntityBankAccounts];

