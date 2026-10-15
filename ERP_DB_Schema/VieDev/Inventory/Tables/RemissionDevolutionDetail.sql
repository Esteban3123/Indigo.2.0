CREATE TABLE [Inventory].[RemissionDevolutionDetail] (
    [Id]                                               INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionDevolutionId]                            INT NOT NULL,
    [ProductId]                                        INT NOT NULL,
    [Quantity]                                         INT NOT NULL,
    [RemissionEntranceDetailBatchSerialId]             INT NULL,
    [RemissionOutputDetailPhysicalId]                  INT NULL,
    [ConsignmentInventoryRemissionDetailBatchSerialId] INT NULL,
    [DevolutionCauseId]                                INT NULL,
    CONSTRAINT [PK_RemissionDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RemissionDevolutionDetail_ConsignmentInventoryRemissionDetailBatchSerial] FOREIGN KEY ([ConsignmentInventoryRemissionDetailBatchSerialId]) REFERENCES [Inventory].[ConsignmentInventoryRemissionDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_RemissionDevolutionDetail_DevolutionCause] FOREIGN KEY ([DevolutionCauseId]) REFERENCES [Inventory].[DevolutionCause] ([Id]),
    CONSTRAINT [FK_RemissionDevolutionDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_RemissionDevolutionDetail_RemissionDevolution] FOREIGN KEY ([RemissionDevolutionId]) REFERENCES [Inventory].[RemissionDevolution] ([Id]),
    CONSTRAINT [FK_RemissionDevolutionDetail_RemissionEntranceDetailBatchSerial] FOREIGN KEY ([RemissionEntranceDetailBatchSerialId]) REFERENCES [Inventory].[RemissionEntranceDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_RemissionDevolutionDetail_RemissionOutputDetailPhysical] FOREIGN KEY ([RemissionOutputDetailPhysicalId]) REFERENCES [Inventory].[RemissionOutputDetailPhysical] ([Id])
);


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_ConsignmentInventoryRemissionDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_DevolutionCause];


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_RemissionDevolution];


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_RemissionEntranceDetailBatchSerial];


GO
ALTER TABLE [Inventory].[RemissionDevolutionDetail] NOCHECK CONSTRAINT [FK_RemissionDevolutionDetail_RemissionOutputDetailPhysical];

