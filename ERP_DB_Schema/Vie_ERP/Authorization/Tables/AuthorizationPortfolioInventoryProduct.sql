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
ALTER TABLE [Authorization].[AuthorizationPortfolioInventoryProduct] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_AuthorizationGroup];


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioInventoryProduct] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_AuthorizationPortfolio];


GO
ALTER TABLE [Authorization].[AuthorizationPortfolioInventoryProduct] NOCHECK CONSTRAINT [FK_AuthorizationPortfolioInventoryProduct_InventoryProduct];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Producto', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'InventoryProductId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Grupo de autorización', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationGroupId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del portafolio', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'AuthorizationPortfolioId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identificador del Registro', @level0type = N'SCHEMA', @level0name = N'Authorization', @level1type = N'TABLE', @level1name = N'AuthorizationPortfolioInventoryProduct', @level2type = N'COLUMN', @level2name = N'Id';

