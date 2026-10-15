CREATE TABLE [Inventory].[EntranceVoucherDetailBatchSerial] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDetailId] INT NOT NULL,
    [BatchSerialId]           INT NULL,
    [Quantity]                INT NOT NULL,
    [OutstandingQuantity]     INT NOT NULL,
    CONSTRAINT [PK_EntranceVoucherDetailBatchSerial__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDetailBatchSerial_EntranceVoucherDetail] FOREIGN KEY ([EntranceVoucherDetailId]) REFERENCES [Inventory].[EntranceVoucherDetail] ([Id]),
    CONSTRAINT [UQ_EntranceVoucherDetailBatchSerial__EntranceVoucherDetailId__BatchSerialId] UNIQUE NONCLUSTERED ([EntranceVoucherDetailId] ASC, [BatchSerialId] ASC)
);


GO
ALTER TABLE [Inventory].[EntranceVoucherDetailBatchSerial] NOCHECK CONSTRAINT [FK_EntranceVoucherDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[EntranceVoucherDetailBatchSerial] NOCHECK CONSTRAINT [FK_EntranceVoucherDetailBatchSerial_EntranceVoucherDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea el comprobante de entrada este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del serial o loto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

