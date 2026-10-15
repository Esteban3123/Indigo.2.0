CREATE TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] (
    [Id]                                       INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetPurchaseOrderEquipmentDetail] INT          NOT NULL,
    [IdEquipment]                              INT          NOT NULL,
    [IdPartAccesoriesConsumibles]              INT          NOT NULL,
    [DepreciatePart]                           BIT          NULL,
    [Value]                                    NUMERIC (18) NULL,
    CONSTRAINT [PK_FixedAssetPurchaseOrderPartAccesoriesConsumibles] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables] FOREIGN KEY ([IdPartAccesoriesConsumibles]) REFERENCES [FixedAsset].[FixedAssetPartsAccesoriesConsumables] ([Id]),
    CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPurchaseOrderEquipment] FOREIGN KEY ([IdFixedAssetPurchaseOrderEquipmentDetail]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderEquipmentDetail] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPartsAccesoriesConsumables];


GO
ALTER TABLE [FixedAsset].[FixedAssetPurchaseOrderPartAccesoriesConsumibles] NOCHECK CONSTRAINT [FK_FixedAssetPurchaseOrderPartAccesoriesConsumibles_FixedAssetPurchaseOrderEquipment];

