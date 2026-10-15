CREATE TABLE [Inventory].[RemissionEntranceDetailBatchSerial] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceDetailId] INT NOT NULL,
    [BatchSerialId]             INT NULL,
    [Quantity]                  INT NOT NULL,
    [OutstandingQuantity]       INT NOT NULL,
    CONSTRAINT [PK_RemissionEntranceDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_RemissionEntranceDetail] FOREIGN KEY ([RemissionEntranceDetailId]) REFERENCES [Inventory].[RemissionEntranceDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [CK_RemissionEntranceDetailBatchSerial_1];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_BatchSerial];


GO
ALTER TABLE [Inventory].[RemissionEntranceDetailBatchSerial] NOCHECK CONSTRAINT [FK_RemissionEntranceDetailBatchSerial_RemissionEntranceDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identidicacion  de la  serie de lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionEntranceDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';

