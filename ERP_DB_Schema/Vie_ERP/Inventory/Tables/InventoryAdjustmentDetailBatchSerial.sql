CREATE TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryAdjustmentDetailId] INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    [BatchSerialId]               INT NULL,
    CONSTRAINT [PK_InventoryAdjustmentDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_InventoryAdjustmentDetail] FOREIGN KEY ([InventoryAdjustmentDetailId]) REFERENCES [Inventory].[InventoryAdjustmentDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_InventoryAdjustmentDetail];


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryAdjustmentDetailBatchSerial]
    ON [Inventory].[InventoryAdjustmentDetailBatchSerial]([InventoryAdjustmentDetailId] ASC, [BatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del lote/serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad del producto con lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera del detalle del ajuste al inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

