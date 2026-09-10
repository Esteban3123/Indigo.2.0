CREATE TABLE [Inventory].[DocumentInvoiceProductSalesDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [DocumentInvoiceProductSalesId] INT             NOT NULL,
    [ProductId]                     INT             NOT NULL,
    [Quantity]                      INT             NOT NULL,
    [HandlesBatch]                  BIT             NOT NULL,
    [SalePrice]                     DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]                 DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]            NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]                 DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]                 NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                      DECIMAL (18, 2) NOT NULL,
    [TotalValue]                    DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]                 DECIMAL (6, 3)  CONSTRAINT [DF__DocumentI__RTFPe__585AC9ED] DEFAULT ((0)) NOT NULL,
    [RTFValue]                      DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__RTFVa__594EEE26] DEFAULT ((0)) NOT NULL,
    [WithholdingICA]                DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5B373698] DEFAULT ((0)) NOT NULL,
    [WithholdingTax]                DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Withh__5C2B5AD1] DEFAULT ((0)) NOT NULL,
    [DistrictTax]                   DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Distr__5D1F7F0A] DEFAULT ((0)) NOT NULL,
    [ImportSource]                  TINYINT         NULL,
    [SourceCode]                    VARCHAR (20)    NULL,
    [InventoryProductCost]          DECIMAL (18, 2) CONSTRAINT [DF__DocumentI__Inven__765213CC] DEFAULT ((0)) NOT NULL,
    [EconomicActivityId]            INT             NULL,
    CONSTRAINT [PK_DocumentInvoiceProductSalesDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_DocumentInvoiceProductSales] FOREIGN KEY ([DocumentInvoiceProductSalesId]) REFERENCES [Inventory].[DocumentInvoiceProductSales] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_EconomicActivity] FOREIGN KEY ([EconomicActivityId]) REFERENCES [Common].[EconomicActivity] ([Id]),
    CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo del inventario del producto en el momento de la venta. DECIMAL(18,2). Valor de referencia para análisis de margen y rentabilidad del artículo vendido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Costo del intatio del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductCost';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la fuente de importación del producto. VARCHAR(20). Trazabilidad de origen en remisiones de entrada o transferencias entre sedes.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el codigo de la fuente de importacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Origen de la importación: 1=Remisión de salida/entrada, NULL=No importado. TINYINT. Indica si el artículo proviene de traslado entre centros o es venta directa.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ImportSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la fuente de la importacion : 1- remision de salida; null si no es importado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ImportSource';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ImportSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto distrital o municipal aplicado al item. DECIMAL(18,2). Retención o gravamen territorial según ubicación de la venta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Impuesto Distrital', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DistrictTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención en la fuente del IVA del item. DECIMAL(18,2). Valor descontado por obligación fiscal en transacciones sujetas a retención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la retencion del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Retención del ICA (Impuesto de Comercio) del item. DECIMAL(18,2). Gravamen municipal sobre actividad comercial retenido en la factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la retencion del ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sumatoria de retención en la fuente de todos los items de la factura. DECIMAL(18,2). Total acumulado de descuentos por retención fiscal.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la sumatoria de la retencion en la fuente de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de retención en la fuente aplicado al item. DECIMAL(6,3). Tasa fiscal que determina la retención según tipo de producto/servicio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del item: (SubTotalValue - DiscountValue + IvaValue). DECIMAL(18,2). Monto final a facturar por el producto tras aplicar descuentos e impuestos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del IVA del item calculado como SubTotalValue × IvaPercentage. DECIMAL(18,2). Impuesto al valor agregado en pesos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del iva del producto. Se calcula multiplicando el subtotal * el porcentaje de iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de IVA aplicado al producto. NUMERIC(5,2). Tasa impositiva del impuesto al valor agregado (19%, 5%, 0% según régimen).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje del iva que se va a cobrar al producto. ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del descuento calculado como SubTotalValue × DiscountPercentage. DECIMAL(18,2). Monto en pesos del descuento comercial aplicado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de descuento comercial del item. NUMERIC(5,2). Reducción de precio ofrecida al cliente.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Subtotal del item: Quantity × SalePrice. DECIMAL(18,2). Base imponible antes de descuentos e impuestos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad por precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta del producto. DECIMAL(18,2). Valor que el cliente paga por cada unidad vendida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador si el producto maneja lotes de control. BIT (0=No, 1=Sí). Trazabilidad de lote/número de serie en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'HandlesBatch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Si el producto maneja Lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'HandlesBatch';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'HandlesBatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto vendidas en el item. INT. Número de unidades facturadas.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto que se va a vender', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al producto vendido. INT, FK a Inventory.InventoryProduct. Identificador único del artículo/medicamento/insumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a la cabecera/maestro de la factura de venta. INT, FK a Inventory.DocumentInvoiceProductSales. Enlace a documento completo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de la factura. INT IDENTITY, PK. Clave primaria de cada línea de venta en la factura.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos vendidos en cada factura de venta de inventario. Registra por cada línea de factura: el producto, la cantidad, el precio de venta, descuentos, impuestos (IVA, RTF, ICA, retención en la fuente, impuesto de industria y comercio) y el costo de inventario asociado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la actividad económica asociada al producto vendido, usada para clasificar la transacción según el código de actividad económica (CIIU) para efectos tributarios y de retención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'EconomicActivityId';
