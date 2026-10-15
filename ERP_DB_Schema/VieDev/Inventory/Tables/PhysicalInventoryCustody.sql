CREATE TABLE [Inventory].[PhysicalInventoryCustody] (
    [Id]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WarehouseId]     INT       NOT NULL,
    [ProductId]       INT       NOT NULL,
    [BatchSerialId]   INT       NULL,
    [Quantity]        INT       NOT NULL,
    [AdmissionNumber] CHAR (10) NULL,
    CONSTRAINT [PK_PhysicalInventoryCustody__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PhysicalInventoryCustody_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_PhysicalInventoryCustody_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PhysicalInventoryCustody_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[PhysicalInventoryCustody] NOCHECK CONSTRAINT [FK_PhysicalInventoryCustody_BatchSerial];


GO
ALTER TABLE [Inventory].[PhysicalInventoryCustody] NOCHECK CONSTRAINT [FK_PhysicalInventoryCustody_InventoryProduct];


GO
ALTER TABLE [Inventory].[PhysicalInventoryCustody] NOCHECK CONSTRAINT [FK_PhysicalInventoryCustody_Warehouse];

