CREATE TABLE [Inventory].[WarehouseRestrictedConditions] (
    [Id]                INT     IDENTITY (1, 1) NOT NULL,
    [WarehouseId]       INT     NOT NULL,
    [ConditionType]     TINYINT NOT NULL,
    [ProductTypeId]     INT     NULL,
    [ProductGroupId]    INT     NULL,
    [ProductSubgroupId] INT     NULL,
    [ProductId]         INT     NULL,
    [RestrictionType]   TINYINT NOT NULL,
    CONSTRAINT [PK_WarehouseRestrictedConditions] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WarehouseRestrictedConditions_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_WarehouseRestrictedConditions_ProductGroup] FOREIGN KEY ([ProductGroupId]) REFERENCES [Inventory].[ProductGroup] ([Id]),
    CONSTRAINT [FK_WarehouseRestrictedConditions_ProductSubGroup] FOREIGN KEY ([ProductSubgroupId]) REFERENCES [Inventory].[ProductSubGroup] ([Id]),
    CONSTRAINT [FK_WarehouseRestrictedConditions_ProductType] FOREIGN KEY ([ProductTypeId]) REFERENCES [Inventory].[ProductType] ([Id]),
    CONSTRAINT [FK_WarehouseRestrictedConditions_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);




GO



GO



GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de restricción aplicada (TINYINT). Especifica si la condición habilita (1) o restringe (2) el acceso/disponibilidad de productos en el almacén. Controla permiso o bloqueo de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'RestrictionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de restriccion que tiene la condicion  1 - Habilita los productos  2 - Restringe los productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'RestrictionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'RestrictionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del producto (FK→InventoryProduct.Id, INT, nullable). Clave foránea que referencia el producto específico cuando la restricción aplica a nivel de artículo individual. Búsqueda: código producto, SKU, medicamento, insumo.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del subgrupo de producto (FK→ProductSubGroup.Id, INT, nullable). Clave foránea que referencia la subcategoría cuando la restricción aplica a nivel de clasificación detallada. Búsqueda: subgrupo, clasificación secundaria.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de subgrupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductSubgroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del grupo de productos (FK→ProductGroup.Id, INT, nullable). Clave foránea que referencia la categoría general cuando la restricción aplica a nivel de agrupación primaria. Búsqueda: categoría, grupo, familia producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del tipo de producto (FK→ProductType.Id, INT, nullable). Clave foránea que referencia la tipología cuando la restricción aplica al nivel más genérico. Búsqueda: tipo, clase producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación del tipo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de condición o nivel de aplicación (TINYINT). Especifica a qué nivel aplica la restricción: 1=Tipo de producto, 2=Grupo de productos, 3=Subgrupo de producto, 4=Producto individual. Define granularidad de control.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Especifica el tipo de condicion  1 - Tipo de producto  2 - Grupo de producto  3 - Subgrupo de producto  4 - Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ConditionType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del almacén (FK→Warehouse.Id, INT, NOT NULL). Clave foránea que referencia el depósito/centro de distribución donde se aplica la condición restrictiva. Búsqueda: almacén, depósito, centro distribución, bodega.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación  del almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'WarehouseId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación única de la condición restrictiva del almacén (INT IDENTITY, PK, NOT NULL). Identificador principal de la tabla WarehouseRestrictedConditions. Búsqueda: ID condición, registro restricción.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Condiciones de restricción aplicadas a bodegas o almacenes: define qué tipos de productos, grupos, subgrupos o artículos específicos tienen restricciones de ingreso, almacenamiento o despacho en cada bodega, y el tipo de restricción que aplica.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions';
