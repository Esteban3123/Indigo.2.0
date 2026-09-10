CREATE TABLE [Billing].[BasicBillingDetailItem] (
    [Id]                   INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [BasicBillingDetailId] INT NOT NULL,
    [PhysicalInventoryId]  INT NOT NULL,
    [Quantity]             INT NOT NULL,
    CONSTRAINT [PK_BasicBillingDetailItem] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_BasicBillingDetailItem_BasicBillingDetail] FOREIGN KEY ([BasicBillingDetailId]) REFERENCES [Billing].[BasicBillingDetail] ([Id]),
    CONSTRAINT [FK_BasicBillingDetailItem_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades, artículos o servicios facturados en el detalle del renglón de facturación básica', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del inventario físico (FK a Inventory.PhysicalInventory), referencia al artículo, medicamento, insumo o dispositivo médico inventariado', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera del detalle de facturación básica (FK a Billing.BasicBillingDetail), vinculación al renglón de factura o reclamación', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la Cabecera - Factura Basica Detalle', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'BasicBillingDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y primaria de la tabla BasicBillingDetailItem, generado automáticamente (IDENTITY)', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ítems o líneas individuales de detalle de facturación básica, asociando cada ítem de inventario físico (medicamento, insumo o producto) con su cantidad facturada dentro de un detalle de factura. Permite desglosar los productos o insumos incluidos en cada concepto de cobro.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'BasicBillingDetailItem';
