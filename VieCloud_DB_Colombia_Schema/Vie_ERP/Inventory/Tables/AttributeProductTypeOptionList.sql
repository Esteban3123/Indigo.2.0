CREATE TABLE [Inventory].[AttributeProductTypeOptionList] (
    [Id]                     INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AttributeProductTypeId] INT           NOT NULL,
    [Code]                   VARCHAR (20)  NOT NULL,
    [Name]                   VARCHAR (100) NOT NULL,
    CONSTRAINT [PK_AttributeProductTypeOptionList] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AttributeProductTypeOptionList_AttributeProductType] FOREIGN KEY ([AttributeProductTypeId]) REFERENCES [Inventory].[AttributeProductType] ([Id])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de la opción de lista de atributos de producto; descripción o etiqueta legible de cada valor posible que puede adoptar un atributo de tipo de producto (ej: talla, color, presentación)', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre de la opcion de lista', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la opción de lista; identificador único alfanumérico (VARCHAR 20) que representa cada opción de atributo de producto para referencia técnica y búsqueda', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la opcion de la lista', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id del atributo del tipo de producto; clave foránea (FK) que referencia a [Inventory].[AttributeProductType] para asociar cada opción a su atributo padre', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'AttributeProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del atributo del tipo de producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'AttributeProductTypeId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'AttributeProductTypeId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la lista de opciones de atributos; identificador único (INT IDENTITY) que registra cada opción disponible en el catálogo de atributos de productos del inventario', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id de la lista de opciones de los atributos', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lista de opciones o valores posibles para cada atributo de tipo de producto en inventario. Permite definir las alternativas seleccionables (por ejemplo tallas, colores, presentaciones) que corresponden a un atributo específico de un tipo de producto.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'AttributeProductTypeOptionList';
