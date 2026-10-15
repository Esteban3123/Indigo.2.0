CREATE TABLE [Inventory].[WarehouseUserRequest] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [WarehouseId] INT          NOT NULL,
    [UserId]      INT          NOT NULL,
    [UserCode]    VARCHAR (20) NOT NULL,
    CONSTRAINT [PK__Warehous__3214EC07A114346F] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_WarehouseUserRequest_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [Inventory].[Warehouse] ([Id])
);

