CREATE TABLE [Inventory].[PharmaceuticalDispensingTransferDetail] (
    [Id]                                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingTransferId]          INT NOT NULL,
    [PharmaceuticalDispensingDetailBatchSerialId] INT NOT NULL,
    [Quantity]                                    INT NOT NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingTransferDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingDetailBatchSerial] FOREIGN KEY ([PharmaceuticalDispensingDetailBatchSerialId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingTransfer] FOREIGN KEY ([PharmaceuticalDispensingTransferId]) REFERENCES [Inventory].[PharmaceuticalDispensingTransfer] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingTransferDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingTransferDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingTransferDetail_PharmaceuticalDispensingTransfer];

