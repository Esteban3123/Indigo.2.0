CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipment] (
    [Id]                        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrder] INT           NOT NULL,
    [IdInventoryType]           INT           NOT NULL,
    [IdEquipmentType]           INT           NOT NULL,
    [IdEquipment]               INT           NOT NULL,
    [IdIvaCode]                 INT           NOT NULL,
    [Quantity]                  INT           NOT NULL,
    [OutstandingQuantity]       INT           NOT NULL,
    [ProductValue]              NUMERIC (18)  NOT NULL,
    [TotalValue]                NUMERIC (18)  NOT NULL,
    [TaxesValue]                NUMERIC (18)  NOT NULL,
    [IdTrademark]               INT           NOT NULL,
    [Model]                     VARCHAR (100) NOT NULL,
    [IdPolize]                  INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderEquipment] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderEquipment_FixedAssetInventoryType];

