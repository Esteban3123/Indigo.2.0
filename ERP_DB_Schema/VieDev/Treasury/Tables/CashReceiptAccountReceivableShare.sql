CREATE TABLE [Treasury].[CashReceiptAccountReceivableShare] (
    [Id]                             INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptAccountReceivableId] INT             NOT NULL,
    [AccountReceivableShareId]       INT             NOT NULL,
    [Value]                          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CashReceiptAccountReceivableShare] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_AccountReceivableShare] FOREIGN KEY ([AccountReceivableShareId]) REFERENCES [Portfolio].[AccountReceivableShare] ([Id]),
    CONSTRAINT [FK_CashReceiptAccountReceivableShare_CashReceiptAccountReceivable] FOREIGN KEY ([CashReceiptAccountReceivableId]) REFERENCES [Treasury].[CashReceiptAccountReceivable] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivableShare] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivableShare_AccountReceivableShare];


GO
ALTER TABLE [Treasury].[CashReceiptAccountReceivableShare] NOCHECK CONSTRAINT [FK_CashReceiptAccountReceivableShare_CashReceiptAccountReceivable];

