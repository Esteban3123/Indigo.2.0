CREATE TABLE [Inventory].[TransferOrderDevolutionDetail] (
    [Id]                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderDevolutionId]        INT NOT NULL,
    [TransferOrderDetailBatchSerialId] INT NOT NULL,
    [Quantity]                         INT NOT NULL,
    CONSTRAINT [PK_TransferOrderDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrderDevolutionDetail_TransferOrderDetailBatchSerial] FOREIGN KEY ([TransferOrderDetailBatchSerialId]) REFERENCES [Inventory].[TransferOrderDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_TransferOrderDevolutionDetail_TransferOrderDevolution] FOREIGN KEY ([TransferOrderDevolutionId]) REFERENCES [Inventory].[TransferOrderDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_TransferOrderDevolutionDetail_TransferOrderDetailBatchSerial];


GO
ALTER TABLE [Inventory].[TransferOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_TransferOrderDevolutionDetail_TransferOrderDevolution];

