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
ALTER TABLE [Inventory].[WarehouseRestrictedConditions] NOCHECK CONSTRAINT [FK_WarehouseRestrictedConditions_Product];


GO
ALTER TABLE [Inventory].[WarehouseRestrictedConditions] NOCHECK CONSTRAINT [FK_WarehouseRestrictedConditions_ProductGroup];


GO
ALTER TABLE [Inventory].[WarehouseRestrictedConditions] NOCHECK CONSTRAINT [FK_WarehouseRestrictedConditions_ProductSubGroup];


GO
ALTER TABLE [Inventory].[WarehouseRestrictedConditions] NOCHECK CONSTRAINT [FK_WarehouseRestrictedConditions_ProductType];


GO
ALTER TABLE [Inventory].[WarehouseRestrictedConditions] NOCHECK CONSTRAINT [FK_WarehouseRestrictedConditions_Warehouse];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de restriccion que tiene la condicion  1 - Habilita los productos  2 - Restringe los productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'RestrictionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de subgrupo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductSubgroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de grupo de productos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación del tipo del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el tipo de condicion  1 - Tipo de producto  2 - Grupo de producto  3 - Subgrupo de producto  4 - Producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'ConditionType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación  del almacén', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'WarehouseId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificación de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'WarehouseRestrictedConditions', @level2type = N'COLUMN', @level2name = N'Id';

