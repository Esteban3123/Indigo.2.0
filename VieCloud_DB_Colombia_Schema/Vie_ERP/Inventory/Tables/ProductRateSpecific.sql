CREATE TABLE [Inventory].[ProductRateSpecific] (
    [Id]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]   INT             NOT NULL,
    [ProductCode] VARCHAR (20)    NOT NULL,
    [SalesValue]  NUMERIC (18, 2) NOT NULL,
    CONSTRAINT [PK_ProductRateSpecific__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor de venta del producto específico, precio unitario en tarifa particular (NUMERIC 18,2), base para facturación y cálculo de honorarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor de venta', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'SalesValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'SalesValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código alfanumérico único del producto (VARCHAR 20), identificador de medicamento, dispositivo, insumo o servicio en inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico del producto, clave foránea a Inventory.InventoryProduct, vincula la tarifa específica al catálogo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY), clave primaria de la tabla ProductRateSpecific', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tarifas o precios específicos por producto del inventario. Registra el valor de venta asignado a cada ítem, permitiendo manejar precios diferenciados por producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductRateSpecific';
