CREATE TABLE [Inventory].[InventoryAdjustmentDetail] (
    [Id]                    INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryAdjustmentId] INT             NOT NULL,
    [ProductId]             INT             NOT NULL,
    [Quantity]              INT             NOT NULL,
    [UnitValue]             NUMERIC (18, 2) NOT NULL,
    [AdjustmentConceptId]   INT             NULL,
    CONSTRAINT [PK_InventoryAdjustmentDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryAdjustmentDetail_AdjustmentConceptId] FOREIGN KEY ([AdjustmentConceptId]) REFERENCES [Inventory].[AdjustmentConcept] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryAdjustment] FOREIGN KEY ([InventoryAdjustmentId]) REFERENCES [Inventory].[InventoryAdjustment] ([Id]),
    CONSTRAINT [FK_InventoryAdjustmentDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concepto o motivo (INT, nullable) del ajuste de inventario; referencia a AdjustmentConcept que clasifica el tipo de movimiento (pérdida, ganancia, revaluación, auditoría, etc.)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concepto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'AdjustmentConceptId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Costo unitario promedio (NUMERIC 18,2) que tenía el producto al momento de ejecutar el ajuste; valor base para cálculo de impacto financiero del ajuste de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el costo promedio que tenia el producto al momento de ejecutar el ajuste del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad (INT) del producto ajustado, sin discriminar por lote o vencimiento; representa unidades agregadas en el movimiento de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica la Cantidad del producto sin importar los lotes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del producto o artículo inventariado; referencia a InventoryProduct que especifica qué medicamento, insumo o bien se está ajustando', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) de la cabecera o maestro del ajuste de inventarios; referencia a InventoryAdjustment para agrupar múltiples productos en un mismo ajuste', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el id de la cabecera del ajuste de inventarios', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'InventoryAdjustmentId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT) del registro de detalle en la tabla InventoryAdjustmentDetail; clave primaria que distingue cada línea de ajuste de inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del detalle de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los ajustes de inventario: registra cada producto afectado en un ajuste, con la cantidad modificada, el valor unitario y el concepto que justifica el movimiento (entrada, salida, devolución, merma, etc.).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'InventoryAdjustmentDetail';
