CREATE TABLE [Portfolio].[AccountReceivableProvision] (
    [Id]                  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AccountReceivableId] INT          NOT NULL,
    [ProvisionDate]       DATE         NOT NULL,
    [Value]               NUMERIC (18) NOT NULL,
    [Balance]             NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_AccountReceivableProvision] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AccountReceivableProvision_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [Portfolio].[AccountReceivable] ([Id])
);


GO
ALTER TABLE [Portfolio].[AccountReceivableProvision] NOCHECK CONSTRAINT [FK_AccountReceivableProvision_AccountReceivable];

