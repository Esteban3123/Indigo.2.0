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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de egreso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'VoucherTransactionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de ll cabecera del reembolso', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'RefundId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Treasury', @level1type = N'TABLE', @level1name = N'RefundDetail', @level2type = N'COLUMN', @level2name = N'Id';

