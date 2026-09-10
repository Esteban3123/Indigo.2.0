CREATE TABLE [Inventory].[RelatedSupplies] (
    [Id]          INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ATCId]       INT NOT NULL,
    [ProductId]   INT NOT NULL,
    [Amount]      INT NOT NULL,
    [DefaultLoad] BIT NOT NULL,
    CONSTRAINT [PK_RelatedSupplies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RelatedSupplies_ATC] FOREIGN KEY ([ATCId]) REFERENCES [Inventory].[ATC] ([Id]),
    CONSTRAINT [FK_RelatedSupplies_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (bit) que especifica si este insumo/producto se carga por defecto en la composición del atendimiento, procedimiento o protocolo ATC. Predeterminado = 1 (sí carga), 0 (no carga automáticamente).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'DefaultLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica si carga por defecto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'DefaultLoad';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'DefaultLoad';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad numérica de unidades del insumo o producto relacionado que se requiere o dispensa según la clase y protocolización del ATC (medicamentos, materiales, equipos consumibles).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Amount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Amount';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Amount';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) del producto clasificado como insumo en el inventario. Referencia a Inventory.InventoryProduct filtrando solo registros cuya clase de producto sea insumo (medicamento, material médico, consumible).', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del insumo, este es un producto pero se aplica el filtro por la clase del tipo de producto que sea insumo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) de la clasificación ATC (Anatomical Therapeutic Chemical) que agrupa insumos relacionados por indicación terapéutica o procedimiento médico. Referencia a Inventory.ATC.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del atc el cual contiene los insumos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ATCId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'ATCId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave primaria (INT IDENTITY). Identificador único de cada relación entre un ATC y su insumo/producto asociado en la tabla de composición de insumos relacionados.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Tabla de insumos relacionados', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación entre insumos o suministros complementarios y un producto principal dentro del inventario. Permite definir qué insumos adicionales están asociados a un producto (por ejemplo, materiales que se usan juntos) y la cantidad requerida de cada uno, indicando si forman parte de la carga predeterminada.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RelatedSupplies';
