CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [PurchaseOrderId]         INT             NOT NULL,
    [ItemId]                  INT             NOT NULL,
    [TrademarkId]             INT             NOT NULL,
    [Model]                   VARCHAR (100)   NOT NULL,
    [IVAId]                   INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [CancelledQuantity]       INT             NOT NULL,
    [UnitValue]               DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [BranchOfficeId]          INT             NULL,
    [FunctionalUnitId]        INT             NULL,
    [PurchaseRequestDetailId] INT             NULL,
    [OrderSource]             TINYINT         CONSTRAINT [DF_FixedAssetPurchaseOrderItem_OrderSource] DEFAULT ((0)) NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetEquipment] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetPurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrder] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_BranchOffice] FOREIGN KEY ([BranchOfficeId]) REFERENCES [Payroll].[BranchOffice] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_FunctionalUnit] FOREIGN KEY ([FunctionalUnitId]) REFERENCES [Payroll].[FunctionalUnit] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderItem_PurchaseRequestDetail] FOREIGN KEY ([PurchaseRequestDetailId]) REFERENCES [Inventory].[PurchaseRequestDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetEquipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetPurchaseOrder];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_GeneralLedgerIVA];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderItem_BranchOffice];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderItem_FunctionalUnit];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderItem] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderItem_PurchaseRequestDetail];

