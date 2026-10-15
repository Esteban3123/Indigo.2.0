CREATE TABLE [Treasury].[CashReceiptDetailAccountPayable] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptDetailId] INT             NOT NULL,
    [AccountPayableId]    INT             NOT NULL,
    [RefundValue]         DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_CashReceiptDetailAccountPatable] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptDetailAccountPayable_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [Payments].[AccountPayable] ([Id]),
    CONSTRAINT [FK_CashReceiptDetailAccountPayable_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptDetailAccountPayable] NOCHECK CONSTRAINT [FK_CashReceiptDetailAccountPayable_AccountPayable];


GO
ALTER TABLE [Treasury].[CashReceiptDetailAccountPayable] NOCHECK CONSTRAINT [FK_CashReceiptDetailAccountPayable_CashReceiptDetails];

