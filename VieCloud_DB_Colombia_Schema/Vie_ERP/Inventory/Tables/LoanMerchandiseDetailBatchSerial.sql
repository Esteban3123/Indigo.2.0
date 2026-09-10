CREATE TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] (
    [Id]                      INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseDetailId] INT NOT NULL,
    [BatchSerialId]           INT NULL,
    [PhysicalInventoryId]     INT NULL,
    [Quantity]                INT NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_LoanMerchandiseDetail] FOREIGN KEY ([LoanMerchandiseDetailId]) REFERENCES [Inventory].[LoanMerchandiseDetail] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetailBatchSerial_PhysicalInventory] FOREIGN KEY ([PhysicalInventoryId]) REFERENCES [Inventory].[PhysicalInventory] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetailBatchSerial] SET (LOCK_ESCALATION = AUTO);







GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (INT) de unidades del lote/serial asociadas a este movimiento de préstamo de mercancía o inventario físico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el inventario físico en la tabla PhysicalInventory; se completa solo para préstamos de salida (egreso de mercancía del almacén).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del inventario fisico, este campo solo se llena si el prestamo es de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'PhysicalInventoryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el lote o número de serie del producto en la tabla BatchSerial; se completa solo para préstamos de entrada (recepción de mercancía).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote/serial, este campo solo se llena si es de entrada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que referencia el detalle específico del préstamo de mercancía en la tabla LoanMerchandiseDetail; identifica qué línea de préstamo contiene este lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de detalle de la mercancía del préstamo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'LoanMerchandiseDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único y autoincrementable (IDENTITY) de cada registro de lote/serial en préstamo de mercancía.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registra el detalle de lotes y seriales asociados a cada ítem de un préstamo de mercancía, indicando la cantidad de unidades por lote o serial y su vínculo con el inventario físico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'LoanMerchandiseDetailBatchSerial';
