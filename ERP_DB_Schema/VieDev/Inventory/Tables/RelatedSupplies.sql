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
ALTER TABLE [Inventory].[RelatedSupplies] NOCHECK CONSTRAINT [FK_RelatedSupplies_ATC];


GO
ALTER TABLE [Inventory].[RelatedSupplies] NOCHECK CONSTRAINT [FK_RelatedSupplies_Product];

