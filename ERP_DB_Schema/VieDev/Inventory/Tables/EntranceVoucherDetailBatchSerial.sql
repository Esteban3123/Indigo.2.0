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

