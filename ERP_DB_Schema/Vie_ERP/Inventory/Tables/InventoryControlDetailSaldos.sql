CREATE TABLE [Inventory].[InventoryControlDetailSaldos] (
    [ProductId] VARCHAR (20) NOT NULL,
    [Quantity]  INT          NOT NULL,
    [Almacen]   VARCHAR (4)  NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailSaldos', @level2type = N'COLUMN', @level2name = N'Almacen';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailSaldos', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailSaldos', @level2type = N'COLUMN', @level2name = N'ProductId';

