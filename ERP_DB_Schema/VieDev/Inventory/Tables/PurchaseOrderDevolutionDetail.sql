CREATE TABLE [Inventory].[PurchaseOrderDevolutionDetail] (
    [Id]                        INT IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderDevolutionId] INT NOT NULL,
    [PurchaseOrderDetailId]     INT NOT NULL,
    [Quantity]                  INT NOT NULL,
    CONSTRAINT [PK_PurchaseOrderDevolutionDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDetail] FOREIGN KEY ([PurchaseOrderDetailId]) REFERENCES [Inventory].[PurchaseOrderDetail] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDevolution] FOREIGN KEY ([PurchaseOrderDevolutionId]) REFERENCES [Inventory].[PurchaseOrderDevolution] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderDevolutionDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDevolutionDetail_PurchaseOrderDevolution];

