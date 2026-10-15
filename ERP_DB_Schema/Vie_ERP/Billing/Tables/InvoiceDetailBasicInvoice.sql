CREATE TABLE [Billing].[InvoiceDetailBasicInvoice] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]            INT             NOT NULL,
    [BasicBillingDetailId] INT             NOT NULL,
    [Quantity]             INT             NOT NULL,
    [UnitSalesPrice]       NUMERIC (18, 2) NOT NULL,
    [TotalSalesPrice]      NUMERIC (18, 2) NOT NULL,
    [Balance]              NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_InvoiceDetailBasicInvoice] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CC_InvoiceDetailBasicInvoice_Balance] CHECK ([Balance]>=(0)),
    CONSTRAINT [FK_InvoiceDetailBasicInvoice_BasicBillingDetail] FOREIGN KEY ([BasicBillingDetailId]) REFERENCES [Billing].[BasicBillingDetail] ([Id]),
    CONSTRAINT [FK_InvoiceDetailBasicInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [CC_InvoiceDetailBasicInvoice_Balance];


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [FK_InvoiceDetailBasicInvoice_BasicBillingDetail];


GO
ALTER TABLE [Billing].[InvoiceDetailBasicInvoice] NOCHECK CONSTRAINT [FK_InvoiceDetailBasicInvoice_Invoice];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio total', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle de la factura básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailBasicInvoice', @level2type = N'COLUMN', @level2name = N'Id';

