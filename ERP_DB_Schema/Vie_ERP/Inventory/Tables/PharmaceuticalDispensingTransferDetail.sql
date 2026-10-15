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


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PharmaceuticalDispensingTransferDetail]
    ON [Inventory].[PharmaceuticalDispensingTransferDetail]([PharmaceuticalDispensingTransferId] ASC, [PharmaceuticalDispensingDetailBatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de serie del lote de distribución farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del traslado de dispensación farmacéutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'PharmaceuticalDispensingTransferId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del detalle de traslado de dispensacion farmaceutica', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PharmaceuticalDispensingTransferDetail', @level2type = N'COLUMN', @level2name = N'Id';

