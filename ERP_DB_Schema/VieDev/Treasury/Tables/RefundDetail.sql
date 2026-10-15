CREATE TABLE [Treasury].[RefundDetail] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RefundId]             INT NOT NULL,
    [VoucherTransactionId] INT NOT NULL,
    CONSTRAINT [PK_RefundDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RefundDetail_Refunds] FOREIGN KEY ([RefundId]) REFERENCES [Treasury].[Refunds] ([Id]),
    CONSTRAINT [FK_RefundDetail_VoucherTransaction] FOREIGN KEY ([VoucherTransactionId]) REFERENCES [Treasury].[VoucherTransaction] ([Id])
);


GO
ALTER TABLE [Treasury].[RefundDetail] NOCHECK CONSTRAINT [FK_RefundDetail_Refunds];


GO
ALTER TABLE [Treasury].[RefundDetail] NOCHECK CONSTRAINT [FK_RefundDetail_VoucherTransaction];

