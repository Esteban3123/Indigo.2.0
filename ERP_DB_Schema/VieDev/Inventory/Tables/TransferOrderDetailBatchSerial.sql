CREATE TABLE [Inventory].[TransferOrderDetailBatchSerial] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderDetailId] INT NOT NULL,
    [PhysicalInventoryId]   INT NOT NULL,
    [Quantity]              INT NOT NULL,
    [OutstandingQuantity]   INT CONSTRAINT [DF_TransferOrderDetailBatchSerial_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TransferOrderDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrderDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail] FOREIGN KEY ([TransferOrderDetailId]) REFERENCES [Inventory].[TransferOrderDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail];

