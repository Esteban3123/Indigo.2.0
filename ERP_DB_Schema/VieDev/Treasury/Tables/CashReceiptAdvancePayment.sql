CREATE TABLE [Treasury].[CashReceiptAdvancePayment] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CashReceiptDetailId]   INT             NOT NULL,
    [AdvancePaymentId]      INT             NOT NULL,
    [AdvancePaymentCode]    VARCHAR (20)    NOT NULL,
    [PaymentValue]          DECIMAL (18, 2) NOT NULL,
    [ValueInCurrencyHeader] NUMERIC (20, 5) CONSTRAINT [DF__CashRecei__Value__5F1B3F67] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_CashReceiptAdvancePayment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CashReceiptAdvancePayment_AdvancePayments] FOREIGN KEY ([AdvancePaymentId]) REFERENCES [Payments].[AdvancePayments] ([Id]),
    CONSTRAINT [FK_CashReceiptAdvancePayment_CashReceiptDetails] FOREIGN KEY ([CashReceiptDetailId]) REFERENCES [Treasury].[CashReceiptDetails] ([Id])
);


GO
ALTER TABLE [Treasury].[CashReceiptAdvancePayment] NOCHECK CONSTRAINT [FK_CashReceiptAdvancePayment_AdvancePayments];


GO
ALTER TABLE [Treasury].[CashReceiptAdvancePayment] NOCHECK CONSTRAINT [FK_CashReceiptAdvancePayment_CashReceiptDetails];

