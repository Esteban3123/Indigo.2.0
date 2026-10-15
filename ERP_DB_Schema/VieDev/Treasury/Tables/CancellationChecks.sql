CREATE TABLE [Treasury].[CancellationChecks] (
    [Id]               INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdEntityAccount]  INT           NOT NULL,
    [IdCheckBook]      INT           NOT NULL,
    [CancellationDate] DATETIME      NOT NULL,
    [CheckNumber]      VARCHAR (100) NOT NULL,
    [Description]      VARCHAR (MAX) NOT NULL,
    [CreationUser]     VARCHAR (20)  NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [ModificationUser] VARCHAR (20)  NULL,
    [ModificationDate] DATETIME      NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CancellationCheck] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CancellationCheck_EntityBankAccount] FOREIGN KEY ([IdEntityAccount]) REFERENCES [Treasury].[EntityBankAccounts] ([Id]),
    CONSTRAINT [FK_CancellationChecks_Checkbooks] FOREIGN KEY ([IdCheckBook]) REFERENCES [Treasury].[Checkbooks] ([Id])
);


GO
ALTER TABLE [Treasury].[CancellationChecks] NOCHECK CONSTRAINT [FK_CancellationCheck_EntityBankAccount];


GO
ALTER TABLE [Treasury].[CancellationChecks] NOCHECK CONSTRAINT [FK_CancellationChecks_Checkbooks];

