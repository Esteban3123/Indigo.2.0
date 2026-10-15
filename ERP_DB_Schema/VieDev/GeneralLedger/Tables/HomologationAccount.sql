CREATE TABLE [GeneralLedger].[HomologationAccount] (
    [Id]                    INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OfficialMainAccountId] INT          NOT NULL,
    [MainAccountId]         INT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    CONSTRAINT [PK_HomologationAccount__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_HomologationAccount_MainAccounts] FOREIGN KEY ([OfficialMainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id]),
    CONSTRAINT [FK_HomologationAccount_MainAccounts1] FOREIGN KEY ([MainAccountId]) REFERENCES [GeneralLedger].[MainAccounts] ([Id])
);


GO
ALTER TABLE [GeneralLedger].[HomologationAccount] NOCHECK CONSTRAINT [FK_HomologationAccount_MainAccounts];


GO
ALTER TABLE [GeneralLedger].[HomologationAccount] NOCHECK CONSTRAINT [FK_HomologationAccount_MainAccounts1];

