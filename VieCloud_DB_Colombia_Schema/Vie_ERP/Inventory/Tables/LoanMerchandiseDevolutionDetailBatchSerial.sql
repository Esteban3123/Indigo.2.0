CREATE TABLE [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial] (
    [Id]                                INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDevolutionDetailId] INT NOT NULL,
    [PhysicalInventoryId]               INT NULL,
    [Quantity]                          INT NOT NULL,
    [BatchSerialId]                     INT NULL,
    CONSTRAINT [PK_LoanMerchandiseDevolutionDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_LoanMerchandiseDevolutionDetail] FOREIGN KEY ([LoanMerchandiseDevolutionDetailId]) REFERENCES [Inventory].[LoanMerchandiseDevolutionDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDevolutionDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);




GO



GO



GO



GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_LoanMerchandiseDevolutionDetailBatchSerial]
    ON [Inventory].[LoanMerchandiseDevolutionDetailBatchSerial]([LoanMerchandiseDevolutionDetailId] ASC, [PhysicalInventoryId] ASC, [BatchSerialId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a BatchSerial. Identificador del lote o número serial de la mercancía. Se completa solo en entradas por devolución de préstamo de salida; referencia al registro de lote/serial del cual se devuelve inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote/serial, este campo solo se llena si es una entrada por devolución de un prestamo de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) de unidades a devolver del lote/serial específico. Especifica el volumen exacto de mercancía que retorna dentro de este lote en la devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad que se va a deolver del lote especifico', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a PhysicalInventory. ID del inventario físico. Se completa solo en salidas por devolución de préstamo de entrada; permite rastrear el movimiento físico del inventario relacionado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico, este campo solo se llena si es una salida por devolución de un prestamo de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'FK a LoanMerchandiseDevolutionDetail. Identificador del detalle de la devolución de mercancía en préstamo. Vínculo obligatorio que especifica a qué devolución pertenece este lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la devolucion', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDevolutionDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementado (INT IDENTITY) de la devolución de lote/serial. Clave primaria de la tabla.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de lotes y seriales asociados a las devoluciones de mercancía prestada (préstamos de inventario). Registra qué cantidades de cada lote o número de serie fueron devueltas en cada línea de devolución.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDevolutionDetailBatchSerial';
