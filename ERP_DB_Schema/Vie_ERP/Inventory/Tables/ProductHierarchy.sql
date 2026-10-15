CREATE TABLE [Inventory].[ProductHierarchy] (
    [Id]                      INT    IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [HierarchyProductFinalId] INT    CONSTRAINT [DF_ProductHierarchy_HierarchyProductFinalId] DEFAULT ((9)) NOT NULL,
    [ProductId]               INT    NOT NULL,
    [ParentProductId]         INT    NOT NULL,
    [ConversionUnit]          BIGINT NOT NULL,
    CONSTRAINT [PK_ProductHierarchy] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductHierarchy_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductHierarchy_InventoryProduct1] FOREIGN KEY ([ParentProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_ProductHierarchy_InventoryProduct2] FOREIGN KEY ([HierarchyProductFinalId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductHierarchy] NOCHECK CONSTRAINT [FK_ProductHierarchy_InventoryProduct];


GO
ALTER TABLE [Inventory].[ProductHierarchy] NOCHECK CONSTRAINT [FK_ProductHierarchy_InventoryProduct1];


GO
ALTER TABLE [Inventory].[ProductHierarchy] NOCHECK CONSTRAINT [FK_ProductHierarchy_InventoryProduct2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de conversion entre padre e hijo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductHierarchy', @level2type = N'COLUMN', @level2name = N'ConversionUnit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto padre que siempre va ser un producto de tipo grupo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductHierarchy', @level2type = N'COLUMN', @level2name = N'ParentProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductHierarchy', @level2type = N'COLUMN', @level2name = N'ProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del producto al cual va aplicar la jerarquia, es decir que siempre va ir el item final este campo es para filtrar de una forma mas rapida    Ejem:  01 - Ibuprofeno  02 - Split  01 - Caja    1 - 01 - 01 - 02 - 10  2 - 01 - 02 - 03 - 100    De esta forma estariamos diciendo que cada Split tiene 10 pastas y cada caja tiene 100 Split  ', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductHierarchy', @level2type = N'COLUMN', @level2name = N'HierarchyProductFinalId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la jerarquia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'ProductHierarchy', @level2type = N'COLUMN', @level2name = N'Id';

