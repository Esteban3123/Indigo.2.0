CREATE TABLE [Inventory].[RemissionOutputDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionOutputId]      INT             NOT NULL,
    [ProductId]              INT             NOT NULL,
    [ProductDescription]     VARCHAR (300)   NOT NULL,
    [Quantity]               INT             NOT NULL,
    [OutstandingAmount]      INT             CONSTRAINT [DF_RemissionOutputDetail_OutstandingAmount] DEFAULT ((0)) NOT NULL,
    [VariationType]          TINYINT         NOT NULL,
    [PercentageVariation]    NUMERIC (5, 2)  NOT NULL,
    [SalePrice]              DECIMAL (18, 2) NOT NULL,
    [SalesPriceOriginal]     DECIMAL (18, 2) NOT NULL,
    [DetailDescription]      VARCHAR (300)   NULL,
    [SalePriceWithDiscount]  DECIMAL (18, 2) NOT NULL,
    [TotalPriceWithDiscount] DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_RemissionOutputDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionOutputDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RemissionOutputDetail_RemissionOutput] FOREIGN KEY ([RemissionOutputId]) REFERENCES [Inventory].[RemissionOutput] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Importe total facturado por línea con descuentos aplicados: SalePriceWithDiscount × Quantity. Valor decimal usado en facturación y auditoría de remisiones de salida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'TotalPriceWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio total de los items con descuentos, es decir (SalePriceWithDiscount * Quantity)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'TotalPriceWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'TotalPriceWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta final después de aplicar variación (aumento o descuento): (SalePrice × PercentageVariation / 100) + SalePrice. Decimal 18,2 usado en cálculo de totales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePriceWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el valor de venta con descuento, es decir que es ((SalePrice * PercentageVariation / 100) + SalePrice)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePriceWithDiscount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePriceWithDiscount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas adicionales del ítem despachado en la remisión: detalles técnicos, lotes, vencimientos, o condiciones especiales del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'DetailDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la descripcion del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'DetailDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'DetailDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta base del producto sin alteraciones, variaciones ni descuentos. Referencia inmutable para auditoría y comparación de cambios de precio.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesPriceOriginal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Este es el precio de venta del producto sin modificarse su valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesPriceOriginal';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalesPriceOriginal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio unitario de venta del producto en el momento del despacho; puede ser modificado manualmente por el usuario respecto al valor base original.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el precio de venta del producto, el usuario podria alterar este valor', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de variación (aumento o descuento) aplicado al precio: valores positivos para aumentos, negativos para rebajas. Numérico 5,2.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'PercentageVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el porcentaje de aumento o descuento que se le va aplicar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'PercentageVariation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'PercentageVariation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de variación: 1=Aumento de precio, 2=Descuento. TINYINT que define si la PercentageVariation suma o resta al SalePrice.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'VariationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de variacion del producto  1 - Aumento  2 - Descuento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'VariationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'VariationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente de entregar o devolver del ítem. Inicia igual a Quantity; disminuye con cada devolución parcial registrada contra la remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea la remision este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'OutstandingAmount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto a despachar en la remisión de salida. INT, base para cálculos de totales y seguimiento de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o descripción comercial del producto despachado. Varchar 300 usado en etiquetas, reportes RIPS y documentación de remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductDescription';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductDescription';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del producto (FK a Inventory.InventoryProduct). Vincula el detalle al maestro de productos para trazabilidad e inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto que se va a despachar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la remisión de salida padre (FK a Inventory.RemissionOutput). Agrupa todos los detalles de una única remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de remisión (PK). Identity INT, clave para auditoría y referencias cruzadas en devoluciones.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision  de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos incluidos en cada remisión de salida de inventario. Registra los ítems despachados, cantidades, precios de venta, descuentos aplicados y variaciones de precio por cada línea del documento de remisión.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionOutputDetail';
