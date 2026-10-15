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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio total', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de factura de venta de producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Id';

