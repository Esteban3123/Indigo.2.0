CREATE TABLE [Inventory].[InventoryProductAttribute] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]              INT           NOT NULL,
    [AttributeProductTypeId] INT           NOT NULL,
    [Value]                  VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_InventoryProductAttribute] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryProductAttribute_AttributeProductType] FOREIGN KEY ([AttributeProductTypeId]) REFERENCES [Inventory].[AttributeProductType] ([Id]),
    CONSTRAINT [FK_InventoryProductAttribute_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryProductAttribute] NOCHECK CONSTRAINT [FK_InventoryProductAttribute_AttributeProductType];


GO
ALTER TABLE [Inventory].[InventoryProductAttribute] NOCHECK CONSTRAINT [FK_InventoryProductAttribute_InventoryProduct];

