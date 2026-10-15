CREATE TABLE [Inventory].[PurchaseOrderDetail] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]         INT             NOT NULL,
    [ProductId]               INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [CancelledQuantity]       INT             NOT NULL,
    [Value]                   DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [PurchaseRequestDetailId] INT             NULL,
    [OrderSource]             TINYINT         CONSTRAINT [DF_PurchaseOrderDetail_OrderSource] DEFAULT ((0)) NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    [ProductHierarchyId]      INT             NULL,
    [HierarchyQuantity]       INT             NULL,
    CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_PurchaseOrderDetail] CHECK ([Quantity]>(0)),
    CONSTRAINT [CK_PurchaseOrderDetail_1] CHECK ([OutstandingQuantity]>=(0) AND [OutstandingQuantity]<=[Quantity]),
    CONSTRAINT [FK_Inventory.PurchaseOrderDetail_ProductHierarchy] FOREIGN KEY ([ProductHierarchyId]) REFERENCES [Inventory].[ProductHierarchy] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [Inventory].[PurchaseOrder] ([Id]),
    CONSTRAINT [FK_PurchaseOrderDetail_PurchaseRequestDetail] FOREIGN KEY ([PurchaseRequestDetailId]) REFERENCES [Inventory].[PurchaseRequestDetail] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [CK_PurchaseOrderDetail_1];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_Inventory.PurchaseOrderDetail_ProductHierarchy];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder];


GO
ALTER TABLE [Inventory].[PurchaseOrderDetail] NOCHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseRequestDetail];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de Jerarquía', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'HierarchyQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificacion  de la  jerarquía del producto', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'PurchaseOrderDetail', @level2type = N'COLUMN', @level2name = N'ProductHierarchyId';

