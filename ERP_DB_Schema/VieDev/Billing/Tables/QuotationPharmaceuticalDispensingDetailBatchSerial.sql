CREATE TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] (
    [Id]                                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [QuotationPharmaceuticalDispensingDetailId] INT NOT NULL,
    [PhysicalInventoryId]                       INT NULL,
    [Quantity]                                  INT NOT NULL,
    [OutstandingQuantity]                       INT NOT NULL,
    [PhysicalInventoryCustodyId]                INT NULL,
    CONSTRAINT [PK_QuotationPharmaceuticalDispensingDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody] FOREIGN KEY ([PhysicalInventoryCustodyId]) REFERENCES [Inventory].[PhysicalInventoryCustody] ([Id]),
    CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_QuotationPharmaceuticalDispensingDetail] FOREIGN KEY ([QuotationPharmaceuticalDispensingDetailId]) REFERENCES [Billing].[QuotationPharmaceuticalDispensingDetail] ([Id])
);


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_PhysicalInventoryCustody];


GO
ALTER TABLE [Billing].[QuotationPharmaceuticalDispensingDetailBatchSerial] NOCHECK CONSTRAINT [FK_QuotationPharmaceuticalDispensingDetailBatchSerial_QuotationPharmaceuticalDispensingDetail];

