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
ALTER TABLE [Billing].[InvoiceDetailProductSales] NOCHECK CONSTRAINT [CC_InvoiceDetailProductSales_Balance];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Saldo pendiente del detalle de producto en factura. Afectado por notas de cartera, glosas y procesos de preauditoría. Tipo: NUMERIC(18,2), ≥0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Corresponde al saldo del detalle. Este campo es afectado por notas de cartera de tipo preauditoría', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Balance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Balance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio total de venta por línea (cantidad × precio unitario). Tipo: NUMERIC(18,2). Dinero facturado por producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio total', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'TotalSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta del producto en la factura. Tipo: NUMERIC(18,2). Valor por unidad vendida.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio Unitario', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'UnitSalesPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto vendido en esta línea de factura. Tipo: INT, ≥0.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK del detalle de venta de producto originado en Inventory. Vincula a DocumentInvoiceProductSalesDetail.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de factura de venta de producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'DocumentInvoiceProductSalesDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador FK de la factura de venta a la que pertenece este detalle. Vincula a Billing.Invoice.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Factura', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'InvoiceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (IDENTITY) de la línea de detalle de factura de venta de productos. Clave primaria.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de productos vendidos asociados a una factura de venta. Registra cada ítem facturado con sus cantidades, precios unitarios, totales y saldo pendiente por cobrar.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'InvoiceDetailProductSales';
