CREATE TABLE [Billing].[InvoiceDetailProductSales] (
    [Id]                                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InvoiceId]                           INT             NOT NULL,
    [DocumentInvoiceProductSalesDetailId] INT             NOT NULL,
    [Quantity]                            INT             CONSTRAINT [DF_InvoiceDetailProductSales_Quantity] DEFAULT ((0)) NOT NULL,
    [UnitSalesPrice]                      NUMERIC (18, 2) CONSTRAINT [DF_InvoiceDetailProductSales_UnitSalesPrice] DEFAULT ((0)) NOT NULL,
    [TotalSalesPrice]                     NUMERIC (18, 2) CONSTRAINT [DF_InvoiceDetailProductSales_TotalSalesPrice] DEFAULT ((0)) NOT NULL,
    [Balance]                             NUMERIC (18, 2) CONSTRAINT [DF_InvoiceDetailProductSales_Balance] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InvoiceDetailProductSales] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CC_InvoiceDetailProductSales_Balance] CHECK ([Balance]>=(0)),
    CONSTRAINT [FK_InvoiceDetailProductSales_DocumentInvoiceProductSalesDetail] FOREIGN KEY ([DocumentInvoiceProductSalesDetailId]) REFERENCES [Inventory].[DocumentInvoiceProductSalesDetail] ([Id]),
    CONSTRAINT [FK_InvoiceDetailProductSales_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Billing].[Invoice] ([Id])
);


GO
ALTER TABLE [Billing].[InvoiceDetailProductSales] NOCHECK CONSTRAINT [CC_InvoiceDetailProductSales_Balance];


GO
ALTER TABLE [Billing].[InvoiceDetailProductSales] NOCHECK CONSTRAINT [FK_InvoiceDetailProductSales_DocumentInvoiceProductSalesDetail];


GO
ALTER TABLE [Billing].[InvoiceDetailProductSales] NOCHECK CONSTRAINT [FK_InvoiceDetailProductSales_Invoice];

