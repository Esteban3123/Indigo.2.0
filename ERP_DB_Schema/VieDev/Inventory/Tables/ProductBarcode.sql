CREATE TABLE [Inventory].[ProductBarcode] (
    [Id]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [ProductId]    INT           NOT NULL,
    [CreationDate] DATETIME      NOT NULL,
    [Barcode]      VARCHAR (300) NOT NULL,
    CONSTRAINT [PK_ProductBarcode__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ProductBarcode_Product] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[ProductBarcode] NOCHECK CONSTRAINT [FK_ProductBarcode_Product];

