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


GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventoryCustody__WarehouseId]
    ON [Inventory].[PhysicalInventoryCustody]([WarehouseId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventoryCustody__BatchSerialId]
    ON [Inventory].[PhysicalInventoryCustody]([BatchSerialId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhysicalInventoryCustody__ProductId]
    ON [Inventory].[PhysicalInventoryCustody]([ProductId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero del ingreso del paciente', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del almacen', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de inventario fisico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PhysicalInventoryCustody', @level2type = N'COLUMN', @level2name = N'Id';

