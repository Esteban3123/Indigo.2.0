CREATE TABLE [Inventory].[TransferOrderDetailBatchSerial] (
    [Id]                    INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderDetailId] INT NOT NULL,
    [PhysicalInventoryId]   INT NOT NULL,
    [Quantity]              INT NOT NULL,
    [OutstandingQuantity]   INT CONSTRAINT [DF_TransferOrderDetailBatchSerial_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_TransferOrderDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_TransferOrderDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id]),
    CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail] FOREIGN KEY ([TransferOrderDetailId]) REFERENCES [Inventory].[TransferOrderDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_PhysicalInventory];


GO
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad pendiente del Item, Cuando se crea un traslado este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad que se va a trasladar de ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del inventario fisico, este campo solo se llena si el prestamo es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de detalle de orden de transferencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';

