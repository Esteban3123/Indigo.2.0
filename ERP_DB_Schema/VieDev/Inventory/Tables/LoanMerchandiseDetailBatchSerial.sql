CREATE TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDetailId] INT NOT NULL,
    [BatchSerialId]           INT NULL,
    [PhysicalInventoryId]     INT NULL,
    [Quantity]                INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetailId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_LoanMerchandiseDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] SET (LOCK_ESCALATION = AUTO);

