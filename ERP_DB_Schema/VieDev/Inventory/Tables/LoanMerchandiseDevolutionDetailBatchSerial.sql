CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] (
    [Id]                                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionDetailId] INT NOT NULL,
    [PhysicalInventoryId]               INT NULL,
    [Quantity]                          INT NOT NULL,
    [BatchSerialId]                     INT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_LoanMerchandiseDevolutionDetail] FOREIGN KEY ([LoanMerchandiseDevolutionDetailId]) REFERENCES [Inventory].[LoanMerchandiseDevolutionDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_LoanMerchandiseDevolutionDetail];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] NOCHECK CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_PhysicalInventory];

