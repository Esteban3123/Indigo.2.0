CREATE TABLE [Payments].[MovementAccountPayables] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdAccountPayable]      INT             NOT NULL,
    [IdAccountPayableShare] INT             NOT NULL,
    [EntityId]              INT             NOT NULL,
    [EntityCode]            VARCHAR (20)    NOT NULL,
    [EntityName]            VARCHAR (250)   NOT NULL,
    [MovementDate]          DATETIME        NOT NULL,
    [Value]                 DECIMAL (18, 2) NOT NULL,
    [TimeStamp]             ROWVERSION      NOT NULL,
    CONSTRAINT [PK_MovementAccountPayables] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayable] FOREIGN KEY ([IdAccountPayable]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_MovementAccountPayables_AccountPayableShares] FOREIGN KEY ([IdAccountPayableShare]) REFERENCES [Payments].[AccountPayableShares] ([Id])
);


GO
ALTER TABLE [Payments].[MovementAccountPayables] NOCHECK CONSTRAINT [FK_MovementAccountPayables_AccountPayable];


GO
ALTER TABLE [Payments].[MovementAccountPayables] NOCHECK CONSTRAINT [FK_MovementAccountPayables_AccountPayableShares];

