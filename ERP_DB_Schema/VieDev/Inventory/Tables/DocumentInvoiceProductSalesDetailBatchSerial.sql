CREATE TABLE [Inventory].[DocumentInvoiceProductSalesDetailBatchSerial] (
    [Id]                                  INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentInvoiceProductSalesDetailId] INT NOT NULL,
    [PhysicalInventoryId]                 INT NOT NULL,
    [Quantity]                            INT NOT NULL,
    [OutstandingQuantity]                 INT CONSTRAINT [DF_DocumentInvoiceProductSalesDetailBatchSerial_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    [RemissionOutputPhysicalId]           INT NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSalesDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_DocumentInvoiceProductSalesDetail] FOREIGN KEY ([DocumentInvoiceProductSalesDetailId]) REFERENCES [Inventory].[DocumentInvoiceProductSalesDetail] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_RemissionOutputDetailPhysical] FOREIGN KEY ([RemissionOutputPhysicalId]) REFERENCES [Inventory].[RemissionOutputDetailPhysical] ([Id])
);


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetailBatchSerial] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_DocumentInvoiceProductSalesDetail];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetailBatchSerial] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetailBatchSerial] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetailBatchSerial_RemissionOutputDetailPhysical];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la Salida de remisión física', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionOutputPhysicalId';

