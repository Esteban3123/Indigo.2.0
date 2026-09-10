CREATE TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] (
    [Id]                          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryAdjustmentDetailId] INT NOT NULL,
    [Quantity]                    INT NOT NULL,
    [BatchSerialId]               INT NULL,
    CONSTRAINT [PK_InventoryAdjustmentDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_InventoryAdjustmentDetail] FOREIGN KEY ([InventoryAdjustmentDetailId]) REFERENCES [Inventory].[InventoryAdjustmentDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_BatchSerial];




GO
ALTER TABLE [Inventory].[InventoryAdjustmentDetailBatchSerial] NOCHECK CONSTRAINT [FK_InventoryAdjustmentDetailBatchSerial_BatchSerial];


GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_InventoryAdjustmentDetailBatchSerial]
    ON [Inventory].[InventoryAdjustmentDetailBatchSerial]([InventoryAdjustmentDetailId] ASC, [BatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del lote o número serial del producto en inventario. Referencia a la tabla BatchSerial para rastrear trazabilidad, caducidad y origen del medicamento o insumo (FK). Nulo si el ajuste no requiere lote específico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote/serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad o número de unidades del producto asociado al lote/serial que se ajusta en inventario. Valor entero que refleja incremento o decremento en existencias.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del producto con lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle del ajuste de inventario. Referencia (FK) a InventoryAdjustmentDetail que agrupa los movimientos de entrada, salida o corrección de stock por producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera del detalle del ajuste al inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) de la línea de ajuste de inventario por lote/serial. Clave primaria de la tabla InventoryAdjustmentDetailBatchSerial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de lotes y números de serie asociados a cada línea de un ajuste de inventario, permitiendo rastrear qué lotes o seriales específicos fueron afectados y en qué cantidad durante un ajuste de stock.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetailBatchSerial';
