CREATE TABLE [dbo].[OrderDelivery] (
    [Id]                INT  IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [OrderDetailId]     INT  NOT NULL,
    [EntryDate]         DATE NOT NULL,
    [AmountEntering]    INT  NOT NULL,
    [OutstandingAmount] INT  NOT NULL,
    [DaysDifference]    INT  NOT NULL,
    CONSTRAINT [PK_OrderDelivery__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDelivery_OrderDetail] FOREIGN KEY ([OrderDetailId]) REFERENCES [dbo].[OrderDetail] ([Id])
);


GO
ALTER TABLE [dbo].[OrderDelivery] NOCHECK CONSTRAINT [FK_OrderDelivery_OrderDetail];

