CREATE TABLE [Billing].[BasicBillingGiftsItem] (
    [Id]                  INT IDENTITY (1, 1) NOT NULL,
    [BasicBillingGiftsId] INT NOT NULL,
    [PhysicalInventoryId] INT NOT NULL,
    [Quantity]            INT NOT NULL,
    CONSTRAINT [PK_BasicBillingGiftsItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingGiftsItem_BasicBillingGifts] FOREIGN KEY ([BasicBillingGiftsId]) REFERENCES [Billing].[BasicBillingGifts] ([Id]),
    CONSTRAINT [FK_BasicBillingGiftsItem_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de artículos/obsequios facturados; unidades numéricas del ítem de regalo en la factura básica de obsequios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del inventario físico (FK a Inventory.PhysicalInventory); referencia al artículo/producto en stock del centro de atención', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/factura básica de obsequios (FK a Billing.BasicBillingGifts); enlace al documento de facturación de regalos o donaciones', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'BasicBillingGiftsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cabecera - Factura Basica Obsequios', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'BasicBillingGiftsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'BasicBillingGiftsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la línea/ítem en la tabla BasicBillingGiftsItem; clave primaria de la tabla (IDENTITY INT)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de ítems o productos incluidos como obsequios o cortesías en una factura de facturación. Cada registro asocia un obsequio de facturación con un artículo del inventario físico y la cantidad entregada.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingGiftsItem';
