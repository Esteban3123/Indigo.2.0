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

