CREATE TABLE [Inventory].[PurchaseRequestDetail] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseRequestId]   INT           NOT NULL,
    [InventoryProductId]  INT           NULL,
    [FixedAssetItemId]    INT           NULL,
    [OtherRequest]        VARCHAR (300) NULL,
    [TrademarkId]         INT           NULL,
    [Model]               VARCHAR (100) NULL,
    [MeasurementUnitId]   INT           NULL,
    [Quantity]            INT           CONSTRAINT [DF_PurchaseRequestDetail_Quantity] DEFAULT ((0)) NOT NULL,
    [OutstandingQuantity] INT           CONSTRAINT [DF_PurchaseRequestDetail_OutstandingQuantity] DEFAULT ((0)) NOT NULL,
    [Description]         VARCHAR (300) NULL,
    [Status]              TINYINT       CONSTRAINT [DF_PurchaseRequestDetail_Status] DEFAULT ((0)) NOT NULL,
    [ApproveObservation]  VARCHAR (300) NULL,
    [ApproveUser]         VARCHAR (20)  NULL,
    [ApproveDate]         DATETIME      NULL,
    CONSTRAINT [PK_PurchaseRequestDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetItem] FOREIGN KEY ([FixedAssetItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_InventoryMeasurementUnit] FOREIGN KEY ([MeasurementUnitId]) REFERENCES [Inventory].[InventoryMeasurementUnit] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_InventoryProduct] FOREIGN KEY ([InventoryProductId]) REFERENCES [Inventory].[InventoryProduct] ([Id]),
    CONSTRAINT [FK_PurchaseRequestDetail_PurchaseRequest] FOREIGN KEY ([PurchaseRequestId]) REFERENCES [Inventory].[PurchaseRequest] ([Id])
);


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetItem];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_FixedAssetTrademark];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_InventoryMeasurementUnit];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_InventoryProduct];


GO
ALTER TABLE [Inventory].[PurchaseRequestDetail] NOCHECK CONSTRAINT [FK_PurchaseRequestDetail_PurchaseRequest];

