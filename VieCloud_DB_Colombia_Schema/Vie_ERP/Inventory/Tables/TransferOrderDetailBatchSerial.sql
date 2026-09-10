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
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail];




GO



GO
ALTER TABLE [Inventory].[TransferOrderDetailBatchSerial] NOCHECK CONSTRAINT [FK_TransferOrderDetailBatchSerial_TransferOrderDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad pendiente (INT, default=0) de unidades aún no devueltas o recibidas. Inicia igual a Quantity al crear el traslado; disminuye con cada devolución o recepción parcial registrada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad pendiente del Item, Cuando se crea un traslado este campo es igual a la Cantidad (Quantity), pero este campo va disminuyendo cada vez que se haga una devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total (INT) de unidades, lotes o series programados para trasladar en este detalle de orden de transferencia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad que se va a trasladar de ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a PhysicalInventory (Id). Identificador del inventario físico origen del traslado; se completa cuando el movimiento es salida o préstamo de almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico, este campo solo se llena si el prestamo es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a TransferOrderDetail (Id). Identificador del detalle de orden de transferencia, traslado o movimiento de inventario asociado a este registro de lote/serie.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de orden de transferencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) de registro de lote/serie en traslado de inventario. Clave primaria de la tabla TransferOrderDetailBatchSerial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidades de lotes o seriales asociados a cada línea de una orden de traslado de inventario. Registra cuántas unidades de un lote/serial específico están incluidas en el detalle del traslado y cuántas unidades quedan pendientes por procesar o despachar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDetailBatchSerial';
