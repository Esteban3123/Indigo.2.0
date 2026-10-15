CREATE TABLE [FixedAsset].[FixedAssetEntryItem] (
    [Id]                      INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FixedAssetEntryId]       INT             NOT NULL,
    [RemissionSource]         TINYINT         NOT NULL,
    [SourceCode]              VARCHAR (20)    NULL,
    [PurchaseOrderItemId]     INT             NULL,
    [RemissionEntranceItemId] INT             NULL,
    [ItemId]                  INT             NOT NULL,
    [IVAId]                   INT             NULL,
    [TrademarkId]             INT             NOT NULL,
    [Model]                   VARCHAR (100)   NOT NULL,
    [PolicyId]                INT             NOT NULL,
    [Quantity]                INT             NOT NULL,
    [OutstandingQuantity]     INT             NOT NULL,
    [UnitValue]               DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]           DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]           NUMERIC (5, 2)  NOT NULL,
    [IvaValue]                DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]      NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]           DECIMAL (18, 2) NOT NULL,
    [TotalValue]              DECIMAL (18, 2) NOT NULL,
    [RTFPercentage]           NUMERIC (5, 2)  CONSTRAINT [DF_FixedAssetEntryItem_RTFPercentage] DEFAULT ((0)) NOT NULL,
    [RTFValue]                NUMERIC (18, 2) NOT NULL,
    [Observation]             VARCHAR (1000)  NULL,
    CONSTRAINT [PK_FixedAssetIngressEquipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetItem] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPolicy] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPurchaseOrderItem] FOREIGN KEY ([PurchaseOrderItemId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetRemissionEntranceItem] FOREIGN KEY ([RemissionEntranceItemId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntranceItem] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetTrademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id]),
    CONSTRAINT [FK_FixedAssetEntryItem_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetIngressEquipment] FOREIGN KEY ([FixedAssetEntryId]) REFERENCES [FixedAsset].[FixedAssetEntry] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPolicy];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetPurchaseOrderItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetRemissionEntranceItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_FixedAssetTrademark];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetEntryItem_GeneralLedgerIVA];


GO
ALTER TABLE [FixedAsset].[FixedAssetEntryItem] NOCHECK CONSTRAINT [FK_FixedAssetIngressEquipment_FixedAssetIngressEquipment];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Remision de Entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';

