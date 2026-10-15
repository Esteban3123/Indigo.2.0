CREATE TABLE [Inventory].[TransferOrderDetail] (
    [Id]                            INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [TransferOrderId]               INT             NOT NULL,
    [InventoryRequestDetailId]      INT             NULL,
    [ProductId]                     INT             NOT NULL,
    [InventoryQuantity]             INT             NOT NULL,
    [Quantity]                      INT             NOT NULL,
    [Value]                         DECIMAL (18, 2) CONSTRAINT [DF_TransferOrderDetail_Value] DEFAULT ((0)) NOT NULL,
    [Description]                   VARCHAR (300)   NULL,
    [InventoryRequestDetailOtherId] INT             NULL,
    CONSTRAINT [PK_TransferOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_KindsAgreements_InventoryRequestOtherDetail] FOREIGN KEY ([InventoryRequestDetailOtherId]) REFERENCES [Inventory].[InventoryRequestDetailOther] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_InventoryRequestDetail] FOREIGN KEY ([InventoryRequestDetailId]) REFERENCES [Inventory].[InventoryRequestDetail] ([Id]),
    CONSTRAINT [FK_TransferOrderDetail_TransferOrder] FOREIGN KEY ([TransferOrderId]) REFERENCES [Inventory].[TransferOrder] ([Id])
);


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_KindsAgreements_InventoryRequestOtherDetail];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_InventoryRequestDetail];


GO
ALTER TABLE [Inventory].[TransferOrderDetail] NOCHECK CONSTRAINT [FK_TransferOrderDetail_TransferOrder];


GO
CREATE NONCLUSTERED INDEX [IX_TransferOrderDetail_TransferOrderId_Quantity_Value]
    ON [Inventory].[TransferOrderDetail]([TransferOrderId] ASC)
    INCLUDE([Quantity], [Value]);

