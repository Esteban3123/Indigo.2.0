CREATE TABLE [Inventory].[ClosedMonthInventory] (
    [Id]               INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Quantity]         INT             NOT NULL,
    [ProductCost]      DECIMAL (18, 2) NULL,
    [FinalProductCost] NUMERIC (18, 2) NULL,
    [SellingPrice]     NUMERIC (18, 2) NULL,
    [ProductId]        INT             NOT NULL,
    [ClosedMonthId]    INT             NOT NULL,
    [WareHouseId]      INT             NULL,
    CONSTRAINT [PK_ClosedMonthInventory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth] FOREIGN KEY ([ClosedMonthId]) REFERENCES [Inventory].[ClosedMonth] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventory_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_ClosedMonth];


GO
ALTER TABLE [Inventory].[ClosedMonthInventory] NOCHECK CONSTRAINT [FK_ClosedMonthInventory_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'WareHouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del mes cerrado de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ClosedMonthId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio de venta del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'SellingPrice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el precio del ultimo costo del producto, este es el ultimo valor con el que se compro', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'FinalProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el costo promedio del producto, este se actualiza cada vez que se realiza una entrada del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'ProductCost';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventory', @level2type = N'COLUMN', @level2name = N'Id';

