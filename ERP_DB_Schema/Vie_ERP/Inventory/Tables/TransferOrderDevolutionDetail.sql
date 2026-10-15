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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolucion de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

