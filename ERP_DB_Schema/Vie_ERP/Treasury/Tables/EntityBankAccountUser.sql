CREATE TABLE [Treasury].[EntityBankAccountUser] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBankAccount] INT           NOT NULL,
    [CodUser]             VARCHAR (250) NOT NULL,
    CONSTRAINT [PK_EntityBankAccountUser__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntityBankAccountUser_EntityBankAccounts] FOREIGN KEY ([IdEntityBankAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[EntityBankAccountUser] NOCHECK CONSTRAINT [FK_EntityBankAccountUser_EntityBankAccounts];


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_EntityBankAccountUser_CodUser_IdEntiyBankAccount]
    ON [Treasury].[EntityBankAccountUser]([CodUser] ASC, [IdEntityBankAccount] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del usuario a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cuenta bancaria a autorizar', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'IdEntityBankAccount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del registro', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'EntityBankAccountUser', @level2type = N'COLUMN', @level2name = N'Id';

