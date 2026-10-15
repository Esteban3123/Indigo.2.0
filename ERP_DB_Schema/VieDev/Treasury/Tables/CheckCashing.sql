CREATE TABLE [Treasury].[CheckCashing] (
    [Id]                      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]                    VARCHAR (20)  NOT NULL,
    [VoucherTransactionId]    INT           NOT NULL,
    [CurrentCheckNumber]      BIGINT        NOT NULL,
    [NextCheckNumber]         BIGINT        NOT NULL,
    [CancellationDate]        DATE          NOT NULL,
    [CancellationDescription] VARCHAR (300) NOT NULL,
    [CancellationCheckId]     INT           NOT NULL,
    [CreationUser]            VARCHAR (20)  NOT NULL,
    [CreationDate]            DATETIME      NOT NULL,
    [ModificationUser]        VARCHAR (20)  NULL,
    [ModificationDate]        DATETIME      NULL,
    [TimeStamp]               ROWVERSION    NOT NULL,
    CONSTRAINT [PK_CheckCashing] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CheckCashing_CancellationChecks] FOREIGN KEY ([CancellationCheckId]) REFERENCES [Treasury].[CancellationChecks] ([Id]),
    CONSTRAINT [FK_CheckCashing_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[CheckCashing] NOCHECK CONSTRAINT [FK_CheckCashing_CancellationChecks];


GO
ALTER TABLE [Treasury].[CheckCashing] NOCHECK CONSTRAINT [FK_CheckCashing_VoucherTransaction];

