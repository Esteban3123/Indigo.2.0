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



GO



GO



GO



GO





GO



GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la causa/motivo de devolución. Clave foránea a Inventory.DevolutionCause. Clasifica razón: defecto, caducidad, rotura, error administrativo, no conformidad, etc.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la causa de devolución asociada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'DevolutionCauseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de remisión en consignación. Especifica lote y serial del producto en régimen de consignación. Clave foránea para productos bajo acuerdos de consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision de inventario en consignación en el cual se encuentra especificado el item y el lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ConsignmentInventoryRemissionDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de remisión de salida/egreso físico. Vincula la devolución al movimiento de salida que se revierte o ajusta en inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputDetailPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del item de la remision de salida', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputDetailPhysicalId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionOutputDetailPhysicalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del detalle de remisión de entrada vinculado. Especifica lote, serie y número de entrada original. Permite trazabilidad reversa al ingreso inicial del producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision de entrada en el cual se encuentra especificaco el item y el lote', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionEntranceDetailBatchSerialId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica (INT) de unidades devueltas del producto. Refleja volumen de la devolución por artículo específico.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto/artículo devuelto. Clave foránea a Inventory.InventoryProduct. Permite rastrear qué insumos, medicamentos o equipos se devolvieron.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de devolución de remisión. Clave foránea que vincula al documento maestro de devolución. Agrupa todos los artículos devueltos en una sola transacción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera de la devolucion de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionDevolutionId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'RemissionDevolutionId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de remisión de devolución. Clave primaria (INT IDENTITY). Referencia para auditoría de devoluciones y trazabilidad de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la remision', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de las devoluciones asociadas a remisiones de inventario. Registra cada producto devuelto, la cantidad, el origen del movimiento (entrada o salida) y la causa de la devolución, permitiendo trazabilidad de mercancía retornada por lote, serial o consignación.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RemissionDevolutionDetail';
