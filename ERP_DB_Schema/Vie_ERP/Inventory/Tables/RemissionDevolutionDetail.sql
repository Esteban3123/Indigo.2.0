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


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la causa de devolución asociada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision de inventario en consignación en el cual se encuentra especificado el item y el lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del item de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputDetailPhysicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision de entrada en el cual se encuentra especificaco el item y el lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la cabecera de la devolucion de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';

