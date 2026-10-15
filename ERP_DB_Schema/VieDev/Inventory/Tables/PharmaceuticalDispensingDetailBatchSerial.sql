CREATE TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] (
    [Id]                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]              INT NULL,
    [Quantity]                         INT NOT NULL,
    [OutstandingQuantity]              INT NOT NULL,
    [PhysicalInventoryCustodyId]       INT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDetailBatchSerial__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial] CHECK ([OutstandingQuantity]>=(0)),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail] FOREIGN KEY ([PharmaceuticalDispensingDetailId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetail] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [CK_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PharmaceuticalDispensingDetail];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody];

