CREATE TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] (
    [Id]                  INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [RemissionEntranceId] INT             NOT NULL,
    [RemissionSource]     TINYINT         NOT NULL,
    [SourceCode]          VARCHAR (20)    NULL,
    [PurchaseOrderItemId] INT             NULL,
    [ItemId]              INT             NOT NULL,
    [IVAId]               INT             NOT NULL,
    [TrademarkId]         INT             NOT NULL,
    [Model]               VARCHAR (100)   NOT NULL,
    [PolicyId]            INT             NOT NULL,
    [Quantity]            INT             NOT NULL,
    [OutstandingQuantity] INT             NOT NULL,
    [UnitValue]           DECIMAL (18, 2) NOT NULL,
    [SubTotalValue]       DECIMAL (18, 2) NOT NULL,
    [IvaPercentage]       NUMERIC (5, 2)  NOT NULL,
    [IvaValue]            DECIMAL (18, 2) NOT NULL,
    [DiscountPercentage]  NUMERIC (5, 2)  NOT NULL,
    [DiscountValue]       DECIMAL (18, 2) NOT NULL,
    [TotalValue]          DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_InputRemissionEquipment] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FixedAssetRemissionEntranceItem_FixedAssetPurchaseOrderItem] FOREIGN KEY ([PurchaseOrderItemId]) REFERENCES [FixedAsset].[FixedAssetPurchaseOrderItem] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipment_Equipment] FOREIGN KEY ([ItemId]) REFERENCES [FixedAsset].[FixedAssetItem] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipment_GeneralLedgerIVA] FOREIGN KEY ([IVAId]) REFERENCES [GeneralLedger].[GeneralLedgerIVA] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipment_InputRemission] FOREIGN KEY ([RemissionEntranceId]) REFERENCES [FixedAsset].[FixedAssetRemissionEntrance] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipment_Poliza] FOREIGN KEY ([PolicyId]) REFERENCES [FixedAsset].[FixedAssetPolicy] ([Id]),
    CONSTRAINT [FK_InputRemissionEquipment_Trademark] FOREIGN KEY ([TrademarkId]) REFERENCES [FixedAsset].[FixedAssetTrademark] ([Id])
);


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_FixedAssetRemissionEntranceItem_FixedAssetPurchaseOrderItem];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_Equipment];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_GeneralLedgerIVA];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_InputRemission];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_Poliza];


GO
ALTER TABLE [FixedAsset].[FixedAssetRemissionEntranceItem] NOCHECK CONSTRAINT [FK_InputRemissionEquipment_Trademark];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';

