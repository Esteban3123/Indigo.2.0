CREATE TABLE [Billing].[ProductFeeDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT NULL,
    [ProductAndServiceFeeId] INT             NOT NULL,
    [ProductId]              INT             NOT NULL,
    [RateType]               BIT             NOT NULL,
    [PercentageType]         BIT             NULL,
    [Observations]           VARCHAR (MAX)   NOT NULL,
    [InitialDate]            DATETIME        NOT NULL,
    [FinalDate]              DATETIME        NOT NULL,
    [SalePrice]              NUMERIC (18, 2) NULL,
    [Percentage]             NUMERIC (5, 2)  NULL,
    CONSTRAINT [PK_ProductFeeDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductFeeDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductFeeDetail_ProductAndServiceFee] FOREIGN KEY ([ProductAndServiceFeeId]) REFERENCES [Billing].[ProductAndServiceFee] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Porcentaje de la tarifa aplicado al producto o servicio. Valor numérico (5,2) que representa el margen o descuento en porcentaje cuando RateType=2 (Porcentaje). Ej: 10.50 = 10.5%.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Porcentaje de la tarifa', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Percentage';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Percentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Precio de venta del producto o servicio en la tarifa. Valor numérico (18,2) que define el costo unitario de facturación cuando RateType=1 (Tarifa Fija). Referencia a Inventory.InventoryProduct.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Precio de venta', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'SalePrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha límite final de vigencia de la tarifa del producto. DATETIME que marca cuándo expira o deja de aplicarse esta configuración de precio en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha limite final ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'FinalDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha límite inicial de vigencia de la tarifa del producto. DATETIME que marca cuándo comienza a aplicarse esta configuración de precio en facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'fecha limite inicial ', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas del detalle de tarifa. VARCHAR(MAX) para registrar aclaraciones, condiciones especiales o comentarios sobre la aplicación de la tarifa al producto.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del detalle', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de porcentaje base para cálculo: 1=Costo Promedio Ponderado, 2=Último Costo. BIT que define cuál base de costo se usa cuando PercentageType está activo.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'PercentageType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de porcentaje:      1. Costo Promedio Ponderado      2. Último Costo', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'PercentageType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'PercentageType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de tarifa aplicada al producto: 1=Tarifa Fija (SalePrice), 2=Porcentaje (Percentage). BIT que determina si el cobro es por valor fijo o porcentual.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de tarifa:      1. Tarifa Fija      2. Porcentaje', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'RateType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'RateType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del Producto vinculado a la tarifa. INT, clave foránea a Inventory.InventoryProduct. Identifica qué producto o servicio tiene esta configuración de precio.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera de Tarifa de Productos y Servicios. INT, clave foránea a Billing.ProductAndServiceFee. Agrupa detalles de tarifa por contrato, plan o esquema de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cabecera - Tarifa de productos y servicios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'ProductAndServiceFeeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de tarifa. INT IDENTITY(1,1), clave primaria de ProductFeeDetail. Clave técnica para referenciar esta configuración específica.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de tarifas por producto o servicio dentro de un contrato o lista de precios. Registra el precio de venta o porcentaje aplicable a cada producto/servicio en un rango de fechas vigente, permitiendo gestionar tarifarios y condiciones comerciales de facturación.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'ProductFeeDetail';
