CREATE TABLE [Inventory].[WarehouseUser] (
    [Id]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [WarehouseId] INT          NOT NULL,
    [UserId]      INT          NOT NULL,
    [UserCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK_WarehouseUser] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WarehouseUser_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);


GO
ALTER TABLE [Inventory].[WarehouseUser] NOCHECK CONSTRAINT [FK_WarehouseUser_Warehouse];

