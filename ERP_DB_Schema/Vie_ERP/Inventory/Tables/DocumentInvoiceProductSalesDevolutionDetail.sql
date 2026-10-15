CREATE TABLE [Inventory].[DocumentInvoiceProductSalesDevolutionDetail] (
    [Id]                                             INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentInvoiceProductSalesDevolutionId]        INT NOT NULL,
    [DocumentInvoiceProductSalesDetailBatchSerialId] INT NOT NULL,
    [Quantity]                                       INT NOT NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSalesDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDevolutionDetail_DocumentInvoiceProductSalesDetailBatchSerial] FOREIGN KEY ([DocumentInvoiceProductSalesDetailBatchSerialId]) REFERENCES [Inventory].[DocumentInvoiceProductSalesDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDevolutionDetail_DocumentInvoiceProductSalesDevolution] FOREIGN KEY ([DocumentInvoiceProductSalesDevolutionId]) REFERENCES [Inventory].[DocumentInvoiceProductSalesDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDevolutionDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDevolutionDetail_DocumentInvoiceProductSalesDetailBatchSerial];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDevolutionDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDevolutionDetail_DocumentInvoiceProductSalesDevolution];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que se va a vender', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote de la factura de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

