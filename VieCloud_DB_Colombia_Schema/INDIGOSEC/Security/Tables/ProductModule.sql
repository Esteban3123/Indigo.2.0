CREATE TABLE [Security].[ProductModule] (
    [Id]        INT      IDENTITY (1, 1) NOT NULL,
    [IdProduct] SMALLINT NOT NULL,
    [IdModule]  INT      NOT NULL,
    CONSTRAINT [PK_ProductModule] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductModule_Module] FOREIGN KEY ([IdModule]) REFERENCES [Security].[Module] ([Id]),
    CONSTRAINT [FK_ProductModule_Product] FOREIGN KEY ([IdProduct]) REFERENCES [Security].[ProductCatalog] ([Id])
);

