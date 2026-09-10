CREATE TABLE [MixingStation].[ProductionBasketsDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductionBasketsId] INT             NOT NULL,
    [ComponentType]       TINYINT         NOT NULL,
    [ProductId]           INT             NULL,
    [AtcId]               INT             NULL,
    [SupplieId]           INT             NULL,
    [Quantity]            DECIMAL (18, 2) NOT NULL,
    [MeasurementUnitId]   INT             NOT NULL,
    CONSTRAINT [PK_ProductionBasketsDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductionBasketsDetail_ATC] FOREIGN KEY ([AtcId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_InventorySupplie] FOREIGN KEY ([SupplieId]) REFERENCES [Inventory].[InventorySupplie] ([Id]),
    CONSTRAINT [FK_ProductionBasketsDetail_ProductionBaskets] FOREIGN KEY ([ProductionBasketsId]) REFERENCES [MixingStation].[ProductionBaskets] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de medida del componente (ml, mg, unidad, etc.). FK a InventoryMeasurementUnit. Define escala/volumen para Quantity.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de medida del componente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'MeasurementUnitId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad del componente en la unidad de medida especificada. DECIMAL(18,2). Volumen, dosis o número de unidades a producir.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad del componente', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Quantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del insumo/suministro usado en el componente. FK a InventorySupplie. Null si no aplica (cuando ComponentType es Medicamento o Producto).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de insumo', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'SupplieId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del medicamento por código ATC (Anatomical Therapeutic Chemical). FK a ATC. Null si ComponentType es Insumo o Producto.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de medicamento', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'AtcId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del producto terminado o semielaborado. FK a InventoryProduct. Null si ComponentType es Medicamento o Insumo.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del producto', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de componente en la mezcla: 1=Medicamento (código ATC), 2=Insumo/Suministro (supplies), 3=Producto otro (producto terminado o semielaborado).', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tipo de componente:   1: Medicamento  2: Insumo  3: Producto de tipo otro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ComponentType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cabecera/encabezado de producción. FK a ProductionBaskets. Agrupa todos los componentes de un lote de mezcla.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductionBasketsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la cabecera', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductionBasketsId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'ProductionBasketsId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del detalle de componente en la canasta de producción. INT IDENTITY. Clave primaria del registro.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del registro', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de componentes incluidos en cada canasta de producción de la estación de mezclas. Registra los insumos, productos o principios activos (ATC) que conforman una preparación, junto con sus cantidades y unidades de medida.', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'MixingStation', @level1type = N'TABLE', @level1name = N'ProductionBasketsDetail';
