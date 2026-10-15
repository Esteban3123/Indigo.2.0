CREATE TABLE [Inventory].[LoanMerchandiseDetail] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LoanMerchandiseId]   INT             NOT NULL,
    [ProductId]           INT             NOT NULL,
    [Quantity]            INT             NOT NULL,
    [UnitValue]           NUMERIC (18, 2) NOT NULL,
    [OutstandingQuantity] INT             CONSTRAINT [DF_LoanMerchandiseDetail_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_LoanMerchandiseDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CK_LoanMerchandiseDetail_ValidateOutstandingQuantity] CHECK ([OutstandingQuantity]>=(0)),
    CONSTRAINT [FK_LoanMerchandiseDetail_InventoryProduct] FOREIGN KEY ([ProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_LoanMerchandiseDetail_LoanMerchandise] FOREIGN KEY ([LoanMerchandiseId]) REFERENCES [Inventory].[LoanMerchandise] ([Id])
);


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [CK_LoanMerchandiseDetail_ValidateOutstandingQuantity];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[LoanMerchandiseDetail] NOCHECK CONSTRAINT [FK_LoanMerchandiseDetail_LoanMerchandise];

