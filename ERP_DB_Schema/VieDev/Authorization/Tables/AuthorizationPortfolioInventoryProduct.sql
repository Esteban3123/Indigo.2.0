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

