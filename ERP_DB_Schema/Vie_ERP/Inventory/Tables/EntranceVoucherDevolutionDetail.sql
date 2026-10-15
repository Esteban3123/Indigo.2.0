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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causa de devolución asociada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id del detalle del comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del comprobante de devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'EntranceVoucherDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'EntranceVoucherDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

