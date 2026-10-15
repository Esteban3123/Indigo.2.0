CREATE TABLE [Inventory].[WarehouseStock] (
    [Id]               INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WarehouseId]      INT          NOT NULL,
    [ProductId]        INT          NOT NULL,
    [MinimumStock]     INT          NOT NULL,
    [MaximumStock]     INT          NOT NULL,
    [RepositionPoint]  INT          NOT NULL,
    [CreationUser]     VARCHAR (20) NOT NULL,
    [CreationDate]     DATETIME     NOT NULL,
    [ModificationUser] VARCHAR (20) NULL,
    [ModificationDate] DATETIME     NULL,
    CONSTRAINT [PK_WarehouseStock] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WarehouseStock_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_WarehouseStock_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[WarehouseStock] NOCHECK CONSTRAINT [FK_WarehouseStock_InventoryProduct];


GO
ALTER TABLE [Inventory].[WarehouseStock] NOCHECK CONSTRAINT [FK_WarehouseStock_Warehouse];

