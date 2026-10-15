CREATE TABLE [Inventory].[InventoryControlDetail] (
    [Id]                 INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [InventoryControlId] INT NOT NULL,
    [ProductId]          INT NOT NULL,
    [Quantity]           INT NOT NULL,
    CONSTRAINT [PK_InventoryControlDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_InventoryControlDetail_InventoryControl] FOREIGN KEY ([InventoryControlId]) REFERENCES [Inventory].[InventoryControl] ([Id]),
    CONSTRAINT [FK_InventoryControlDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id])
);


GO
ALTER TABLE [Inventory].[InventoryControlDetail] NOCHECK CONSTRAINT [FK_InventoryControlDetail_InventoryControl];


GO
ALTER TABLE [Inventory].[InventoryControlDetail] NOCHECK CONSTRAINT [FK_InventoryControlDetail_InventoryProduct];

