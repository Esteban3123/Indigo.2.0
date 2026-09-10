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



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a RemissionOutputDetailPhysical (nullable): referencia al documento de salida/remisión física cuando se produce el despacho o devolución del lote/serie', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionOutputPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Salida de remisión física', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionOutputPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'RemissionOutputPhysicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente por surtir/devolver (INT, default=0): inicia igual a Quantity en creación de factura; disminuye con cada devolución, control de pendientes y glosas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la factura de productos este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total (INT) del producto asignado desde el lote/serie de inventario físico al detalle de factura de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a PhysicalInventory: identificador del lote, serie o movimiento de inventario físico asociado al producto facturado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a DocumentInvoiceProductSalesDetail: referencia al detalle del producto en la factura de venta, línea de facturación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Documento de Detalle factura de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT, PK) del registro de lote/serie en el detalle de factura de venta con seguimiento de inventario físico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de lotes y seriales asociados a cada línea de producto en una factura de venta, controlando las cantidades despachadas y pendientes por lote o número de serie, y vinculando la salida física del inventario correspondiente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetailBatchSerial';
