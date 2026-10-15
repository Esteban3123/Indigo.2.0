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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle de la orden de compra. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del IVA del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la orden de compra, este campo solo se llena si el item fue importado de una remision de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo de del documento el cua fue importado el item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la remision de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'RemissionEntranceId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetRemissionEntranceItem', @level2type = N'COLUMN', @level2name = N'Id';

