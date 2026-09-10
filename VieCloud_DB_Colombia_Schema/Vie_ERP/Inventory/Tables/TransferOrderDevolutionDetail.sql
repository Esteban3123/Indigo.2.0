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



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades a devolver en la devolución de orden de traslado. Tipo: INT. Representa el número de artículos, lotes o series que se regresan del inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad que se va a devolver', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de lote/serie de la orden de traslado. FK a [Inventory].[TransferOrderDetailBatchSerial]. Vincula la devolución al artículo específico transferido.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de devolución de orden de traslado. FK a [Inventory].[TransferOrderDevolution]. Agrupa los detalles de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolucion de la orden de traslado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'TransferOrderDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (clave primaria, IDENTITY INT). Registro individual de detalle en la devolución de traslado de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones de órdenes de traslado de inventario. Registra cada ítem devuelto indicando el lote o serial del medicamento/insumo y la cantidad retornada dentro de una devolución de traslado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'TransferOrderDevolutionDetail';
