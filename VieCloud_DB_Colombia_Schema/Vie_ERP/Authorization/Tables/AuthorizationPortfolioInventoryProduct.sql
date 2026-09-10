CREATE TABLE [Authorization].[AuthorizationPortfolioInventoryProduct] (
    [Id]                       INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AuthorizationPortfolioId] INT NOT NULL,
    [AuthorizationGroupId]     INT NOT NULL,
    [InventoryProductId]       INT NOT NULL,
    CONSTRAINT [PK_AuthorizationPortfolioInventoryProduct__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_AuthorizationGroup] FOREIGN KEY ([AuthorizationGroupId]) REFERENCES [Authorization].[AuthorizationGroup] ([Id]),
    CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_AuthorizationPortfolio] FOREIGN KEY ([AuthorizationPortfolioId]) REFERENCES [Authorization].[AuthorizationPortfolio] ([Id]),
    CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);




GO



GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del producto en inventario (medicamento, insumo, dispositivo médico). Referencia a [Inventory].[InventoryProduct]. Clave foránea que vincula el producto específico autorizado.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryProductId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del grupo de autorización. Referencia a [Authorization].[AuthorizationGroup]. Agrupa permisos y políticas de autorización para productos en el portafolio.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Grupo de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del portafolio de autorización. Referencia a [Authorization].[AuthorizationPortfolio]. Define el conjunto de productos autorizados para prestación de servicios, contrato o centro de atención.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro (IDENTITY INT). Clave primaria que relaciona un producto específico con su grupo y portafolio de autorización en el inventario.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los productos de inventario (medicamentos, insumos o materiales) que pertenecen a un portafolio de autorización y a un grupo de autorización específico. Permite definir qué ítems de inventario están habilitados o cubiertos dentro de cada portafolio y grupo para el proceso de autorizaciones.', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct';
