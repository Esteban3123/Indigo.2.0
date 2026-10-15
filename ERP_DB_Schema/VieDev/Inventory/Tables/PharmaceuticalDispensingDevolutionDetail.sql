CREATE TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] (
    [Id]                                          INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PharmaceuticalDispensingDevolutionId]        INT           NOT NULL,
    [PharmaceuticalDispensingDetailBatchSerialId] INT           NOT NULL,
    [Quantity]                                    INT           NOT NULL,
    [EntityId]                                    INT           NULL,
    [EntityName]                                  VARCHAR (250) NULL,
    CONSTRAINT [PK_PharmaceuticalDispensingDevolutionDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDetailBatchSerial] FOREIGN KEY ([PharmaceuticalDispensingDetailBatchSerialId]) REFERENCES [Inventory].[PharmaceuticalDispensingDetailBatchSerial] ([Id]),
    CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolution] FOREIGN KEY ([PharmaceuticalDispensingDevolutionId]) REFERENCES [Inventory].[PharmaceuticalDispensingDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDetailBatchSerial];


GO
ALTER TABLE [Inventory].[PharmaceuticalDispensingDevolutionDetail] NOCHECK CONSTRAINT [FK_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolution];


GO
CREATE NONCLUSTERED INDEX [IDX_PharmaceuticalDispensingDevolutionDetail_PharmaceuticalDispensingDevolutionId]
    ON [Inventory].[PharmaceuticalDispensingDevolutionDetail]([PharmaceuticalDispensingDevolutionId] ASC)
    INCLUDE([PharmaceuticalDispensingDetailBatchSerialId]);

