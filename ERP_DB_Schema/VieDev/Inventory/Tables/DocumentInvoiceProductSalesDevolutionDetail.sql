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

