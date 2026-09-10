CREATE TABLE [Inventory].[PurchaseOrderDevolutionDetail] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderDevolutionId] INT NOT NULL,
    [PurchaseOrderDetailId]     INT NOT NULL,
    [Quantity]                  INT NOT NULL,
    CONSTRAINT [PK_PurchaseOrderDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDetail] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDevolution] FOREIGN KEY ([PurchaseOrderDevolutionId]) REFERENCES [Inventory].[PurchaseOrderDevolution] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades a devolver/cancelar del artículo en esa línea de compra original', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a cancelar', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle original de orden de compra a devolver (FK a PurchaseOrderDetail); referencia a línea comprada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de compra que se va a realizar la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de devolución (FK a PurchaseOrderDevolution); agrupa múltiples líneas devueltas', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'PurchaseOrderDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador autonumérico (INT IDENTITY) único de la línea de devolución de orden de compra', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de órdenes de compra: registra cada ítem devuelto al proveedor, indicando a qué devolución pertenece, qué línea de la orden de compra original se está devolviendo y la cantidad devuelta.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDevolutionDetail';
