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

