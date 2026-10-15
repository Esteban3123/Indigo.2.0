CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] (
    [Id]                        INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetPurchaseOrderId] INT             NOT NULL,
    [AvailabilityDetailId]      INT             NOT NULL,
    [Value]                     DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderAvailability] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_AvailabilityDetail] FOREIGN KEY ([AvailabilityDetailId]) REFERENCES [Budget].[AvailabilityDetail] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_FixedAssetPurchaseOrder] FOREIGN KEY ([FixedAssetPurchaseOrderId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrder] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_AvailabilityDetail];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderAvailability] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderAvailability_FixedAssetPurchaseOrder];

