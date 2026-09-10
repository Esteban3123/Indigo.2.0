CREATE TABLE [Billing].[BasicBillingGifts] (
    [Id]             INT IDENTITY (1, 1) NOT NULL,
    [BasicBillingId] INT NOT NULL,
    [ProductId]      INT NULL,
    [Quantity]       INT NOT NULL,
    [WarehouseId]    INT NULL,
    CONSTRAINT [PK_BasicBillingGifts] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingGifts_BasicBilling] FOREIGN KEY ([BasicBillingId]) REFERENCES [Billing].[BasicBilling] ([Id]),
    CONSTRAINT [FK_BasicBillingGifts_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del almacén o bodega de origen del regalo/obsequio, FK a [Inventory].[Warehouse]; rastrea inventario de regalos por ubicación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Almacen', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto regalado o incluido sin cargo; cantidad numérica (INT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto/artículo obsequiado o regalo; tipo INT, nullable; referencia a catálogo de productos/insumos', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la factura básica (cabecera), FK a [Billing].[BasicBilling]; vincula el regalo al documento de facturación, ingreso o atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'BasicBillingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cabecera - Factura Basica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'BasicBillingId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'BasicBillingId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de regalo/obsequio en facturación básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Regalos o bonificaciones asociados a una facturación básica (billing). Registra los productos entregados como obsequio o cortesía en una factura, con su cantidad y el almacén de origen.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGifts';
