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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea la factura de productos este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Documento de Detalle factura de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

