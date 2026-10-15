CREATE TABLE [Inventory].[PhysicalInventory] (
    [Id]            INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WarehouseId]   INT NOT NULL,
    [ProductId]     INT NOT NULL,
    [BatchSerialId] INT NULL,
    [Quantity]      INT NOT NULL,
    CONSTRAINT [PK_PhysicalInventory__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhysicalInventory_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_PhysicalInventory_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PhysicalInventory_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PhysicalInventory] NOCHECK CONSTRAINT [FK_PhysicalInventory_BatchSerial];


GO
ALTER TABLE [Inventory].[PhysicalInventory] NOCHECK CONSTRAINT [FK_PhysicalInventory_InventoryProduct];


GO
ALTER TABLE [Inventory].[PhysicalInventory] NOCHECK CONSTRAINT [FK_PhysicalInventory_Warehouse];


GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventory_WarehouseId_Quantity_ProductId]
    ON [Inventory].[PhysicalInventory]([WarehouseId] ASC, [Quantity] ASC)
    INCLUDE([ProductId]);

