CREATE TABLE [Inventory].[InventoryControlDetailBatchSerial] (
    [Id]                       INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryControlDetailId] INT     NOT NULL,
    [BatchSerialId]            INT     NULL,
    [InventoryQuantity]        INT     NOT NULL,
    [Quantity]                 INT     NOT NULL,
    [Status]                   TINYINT NOT NULL,
    CONSTRAINT [PK_InventoryControlDetailBatchSerial] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryControlDetailBatchSerial_BatchSerial] FOREIGN KEY ([BatchSerialId]) REFERENCES [Inventory].[BatchSerial] ([Id]),
    CONSTRAINT [FK_InventoryControlDetailBatchSerial_InventoryControlDetail] FOREIGN KEY ([InventoryControlDetailId]) REFERENCES [Inventory].[InventoryControlDetail] ([Id])
);




GO



GO



GO
CREATE NONCLUSTERED INDEX [IX_InventoryControlDetailBatchSerial_InventoryControlDetailId]
    ON [Inventory].[InventoryControlDetailBatchSerial]([InventoryControlDetailId] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del control de inventarios: 0=No Aplica (saldos iniciales), 1=No Ajustado (control pendiente), 2=Ajustado (control completado). Tipo: TINYINT, indica etapa de validación física vs contable del lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Estado del control de inventarios  0 - No Aplica (Para Saldos Iniciales)  1 - No Ajustado (Control de Inventarios)  2 - Ajustado (Control de Inventarios)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Status';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del artículo asociadas al lote/serial en este detalle de control. Tipo: INT, representa la cantidad a conciliar o ajustar.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de los items con el lote y serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades registradas en inventario físico al momento de crear el documento de control. Tipo: INT, equivalente a cantidad en existencia o stock disponible detectado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la cantidad de items que se encuantran en el inventario fisico en el momento en que se crea el documento', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del lote o número serial del artículo. Tipo: INT, referencia a Inventory.BatchSerial, vincula trazabilidad y seguimiento del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del lote o serial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'BatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) del detalle del control de inventarios padre. Tipo: INT, referencia a Inventory.InventoryControlDetail, relaciona línea de control con su documento.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle del control de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'InventoryControlDetailId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (PK) de la tabla InventoryControlDetailBatchSerial. Tipo: INT IDENTITY, clave primaria del registro de control por lote/serial.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de lotes y números de serie asociados al control de inventario. Registra las cantidades en inventario y las cantidades reales por cada lote o serial dentro de un movimiento de control de inventario, permitiendo trazabilidad de productos farmacéuticos o de almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryControlDetailBatchSerial';
