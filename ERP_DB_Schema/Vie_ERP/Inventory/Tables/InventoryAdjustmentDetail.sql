CREATE TABLE [Inventory].[InventoryAdjustmentDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryAdjustmentId] INT             NOT NULL,
    [ProductId]             INT             NOT NULL,
    [Quantity]              INT             NOT NULL,
    [UnitValue]             NUMERIC (18, 2) NOT NULL,
    [AdjustmentConceptId]   INT             NULL,
    CONSTRAINT [PK_InventoryAdjustmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustmentDetail_AdjustmentConceptId] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryAdjustment] FOREIGN KEY ([InventoryAdjustmentId]) REFERENCES [Inventory].[InventoryAdjustment] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetail] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetail_AdjustmentConceptId];


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetail] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryAdjustment];


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetail] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Concepto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio que tenia el producto al momento de ejecutar el ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad del producto sin importar los lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la cabecera del ajuste de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Id';

