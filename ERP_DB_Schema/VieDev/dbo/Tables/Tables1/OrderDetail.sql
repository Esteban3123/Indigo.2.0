CREATE TABLE [dbo].[OrderDetail] (
    [Id]                INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderId]           INT     NOT NULL,
    [SupplierId]        INT     NOT NULL,
    [WarehouseId]       INT     NOT NULL,
    [QuantityOrdered]   INT     NOT NULL,
    [QuantityDelivered] INT     NOT NULL,
    [OutstandingAmount] INT     NOT NULL,
    [Status]            TINYINT CONSTRAINT [DF_OrderDetail_Status] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_OrderDetail__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([Id]),
    CONSTRAINT [FK_OrderDetail_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([Id]),
    CONSTRAINT [FK_OrderDetail_Warehouse] FOREIGN KEY ([WarehouseId]) REFERENCES [dbo].[Warehouse] ([Id])
);


GO
ALTER TABLE [dbo].[OrderDetail] NOCHECK CONSTRAINT [FK_OrderDetail_Order];


GO
ALTER TABLE [dbo].[OrderDetail] NOCHECK CONSTRAINT [FK_OrderDetail_Supplier];


GO
ALTER TABLE [dbo].[OrderDetail] NOCHECK CONSTRAINT [FK_OrderDetail_Warehouse];

