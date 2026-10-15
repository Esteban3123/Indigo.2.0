CREATE TABLE [Inventory].[ClosedMonthInventoryDetail] (
    [Id]                     INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Quantity]               INT             NOT NULL,
    [WarehouseId]            INT             NOT NULL,
    [BatchSerialId]          INT             NULL,
    [ClosedMonthInventoryId] INT             NOT NULL,
    [CostTotal]              DECIMAL (18, 2) CONSTRAINT [DF__ClosedMon__CostT__45C97A6B] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_Inventory_ClosedMonthInventoryDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_ClosedMonthInventory] FOREIGN KEY ([ClosedMonthInventoryId]) REFERENCES [Inventory].[ClosedMonthInventory] ([Id]),
    CONSTRAINT [FK_ClosedMonthInventoryDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[ClosedMonthInventoryDetail] NOCHECK CONSTRAINT [FK_ClosedMonthInventoryDetail_BatchSerial];


GO
ALTER TABLE [Inventory].[ClosedMonthInventoryDetail] NOCHECK CONSTRAINT [FK_ClosedMonthInventoryDetail_ClosedMonthInventory];


GO
ALTER TABLE [Inventory].[ClosedMonthInventoryDetail] NOCHECK CONSTRAINT [FK_ClosedMonthInventoryDetail_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Costo total', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'CostTotal';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto en el mes cerrado de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'ClosedMonthInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ClosedMonthInventoryDetail', @level2type = N'COLUMN', @level2name = N'Id';

