CREATE TABLE [Inventory].[EntranceVoucherDevolutionDetail] (
    [Id]                                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [EntranceVoucherDevolutionId]        INT NOT NULL,
    [EntranceVoucherDetailBatchSerialId] INT NOT NULL,
    [Quantity]                           INT NOT NULL,
    [DevolutionCauseId]                  INT NULL,
    CONSTRAINT [PK_EntranceVoucherDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_DevolutionCause] FOREIGN KEY ([DevolutionCauseId]) REFERENCES [Inventory].[DevolutionCause] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDetailBatchSerial] FOREIGN KEY ([EntranceVoucherDetailBatchSerialId]) REFERENCES [Inventory].[EntranceVoucherDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDevolution] FOREIGN KEY ([EntranceVoucherDevolutionId]) REFERENCES [Inventory].[EntranceVoucherDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionDetail_DevolutionCause];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDetailBatchSerial];


GO
ALTER TABLE [Inventory].[EntranceVoucherDevolutionDetail] NOCHECK CONSTRAINT [FK_EntranceVoucherDevolutionDetail_EntranceVoucherDevolution];

