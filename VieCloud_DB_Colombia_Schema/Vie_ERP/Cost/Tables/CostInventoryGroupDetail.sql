CREATE TABLE [Cost].[CostInventoryGroupDetail] (
    [Id]                   INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CostInventoryGroupId] INT             NOT NULL,
    [InventoryProductId]   INT             NOT NULL,
    [Quantity]             DECIMAL (24, 6) NOT NULL,
    CONSTRAINT [PK_CostInventoryGroupDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CostInventoryGroupDetail_CostInventoryGroup] FOREIGN KEY ([CostInventoryGroupId]) REFERENCES [Cost].[CostInventoryGroup] ([Id]),
    CONSTRAINT [FK_CostInventoryGroupDetail_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto en el grupo de costos. Decimal(24,6) para precisión en fracciones de medicamentos, insumos o dispositivos médicos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del producto de inventario (medicamento, insumo, dispositivo médico). FK a Inventory.InventoryProduct para trazabilidad de existencias.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Producto', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de costos al que pertenece el producto. FK a Cost.CostInventoryGroup para agrupación de análisis de costos y gastos operacionales.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Grupo de Productos', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'CostInventoryGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro de detalle (INT IDENTITY). Clave primaria de la línea de producto dentro del grupo de costos.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de los productos de inventario que componen cada grupo de costo, incluyendo la cantidad asociada a cada producto dentro del grupo. Permite conocer qué ítems de inventario (insumos, medicamentos, materiales) integran un agrupador de costos y en qué proporción o cantidad.', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Cost', @level1type = N'TABLE', @level1name = N'CostInventoryGroupDetail';
