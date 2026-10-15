CREATE TABLE [Treasury].[Checkbooks] (
    [Id]                   INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityBanckAccount] INT         NOT NULL,
    [InitialNumber]        BIGINT      NOT NULL,
    [EndNumber]            BIGINT      NOT NULL,
    [CurrentNumber]        BIGINT      NOT NULL,
    [Prefix]               VARCHAR (5) NOT NULL,
    [Status]               TINYINT     NOT NULL,
    [TimeStamp]            ROWVERSION  NOT NULL,
    CONSTRAINT [PK_Voucher] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Voucher_EntityBankAccount] FOREIGN KEY ([IdEntityBanckAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id])
);


GO
ALTER TABLE [Treasury].[Checkbooks] NOCHECK CONSTRAINT [FK_Voucher_EntityBankAccount];

