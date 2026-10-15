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
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_DocumentInvoiceProductSales];


GO
ALTER TABLE [Inventory].[DocumentInvoiceProductSalesDetail] NOCHECK CONSTRAINT [FK_DocumentInvoiceProductSalesDetail_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo del intatio del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo de la fuente de importacion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la fuente de la importacion : 1- remision de salida; null si no es importado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ImportSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Impuesto Distrital', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DistrictTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del IVA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingTax';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la retencion del ICA', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'WithholdingICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la sumatoria de la retencion en la fuente de todos los items', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del iva del producto. Se calcula multiplicando el subtotal * el porcentaje de iva', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al producto. ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad por precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si el producto maneja Lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'HandlesBatch';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto que se va a vender', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la factura', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'DocumentInvoiceProductSalesDetail', @level2type = N'COLUMN', @level2name = N'Id';

