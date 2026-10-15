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

