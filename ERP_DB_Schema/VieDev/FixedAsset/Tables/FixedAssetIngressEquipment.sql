CREATE TABLE [FixedAsset].[FixedAssetIngressEquipment] (
    [Id]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IdFixedAssetIngress] INT           NOT NULL,
    [IdInventoryType]     INT           NOT NULL,
    [IdEquipmentType]     INT           NOT NULL,
    [IdEquipment]         INT           NOT NULL,
    [IdIvaCode]           INT           NOT NULL,
    [Quantity]            INT           NOT NULL,
    [ProductValue]        NUMERIC (18)  NOT NULL,
    [TotalValue]          NUMERIC (18)  NOT NULL,
    [TaxesValue]          NUMERIC (18)  NOT NULL,
    [IdTrademark]         INT           NOT NULL,
    [Model]               VARCHAR (100) NOT NULL,
    [IdPolize]            INT           NOT NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipment_1] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipment] FOREIGN KEY ([IdEquipment]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipmentType] FOREIGN KEY ([IdEquipmentType]) REFERENCES [FixedAsset].[FixedAssetItemType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetInventoryType] FOREIGN KEY ([IdInventoryType]) REFERENCES [FixedAsset].[FixedAssetInventoryType] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetPoliza] FOREIGN KEY ([IdPolize]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetTrademark] FOREIGN KEY ([IdTrademark]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_GeneralLedgerIVA] FOREIGN KEY ([IdIvaCode]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetEquipmentType];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetInventoryType];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetPoliza];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetIngressEquipment] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_GeneralLedgerIVA];

