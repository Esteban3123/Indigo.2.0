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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto a devolver en esta línea de devolución. Tipo: INT. Representa el número de artículos retirados del inventario por devolución de ventas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto que se va a vender', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del lote/serie específico de la línea de factura de venta. Vincula a DocumentInvoiceProductSalesDetailBatchSerial. Tipo: INT. Permite rastrear el lote/número de serie del producto devuelto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote de la factura de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la cabecera/encabezado de la devolución de venta a la que pertenece este detalle. Vincula a DocumentInvoiceProductSalesDevolution. Tipo: INT. Agrupa múltiples líneas de devolución bajo un mismo documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolución', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) del registro de detalle de devolución de producto. Tipo: INT IDENTITY. Clave primaria secuencial que identifica cada línea de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos incluidos en una devolución de factura de venta, registrando qué ítems (con su lote o serial específico) fueron devueltos y en qué cantidad.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDevolutionDetail';
