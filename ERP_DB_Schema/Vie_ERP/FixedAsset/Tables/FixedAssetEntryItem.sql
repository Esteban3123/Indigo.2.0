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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la observación de los artículos, esta observación es la que posteriormente podemos ver en la tabla de Activos Fijos (PhysicalAsset)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Observation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor de la retefuente que se cobrara al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el porcentaje de la retefuente que se le aplica al item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RTFPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Total del registro, Se obtiene tomando el (SubTotalValue - DiscountValue + IvaValue)', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del descuento, se obtiene multiplicando el SubTotalValue por el porcentaje del descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje de descuento', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'DiscountPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valor del porcentaje del iva, se obtiene multiplicando el Subtotalvalue por el porcentaje del IVA', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Porcentaje del iva que se va a cobrar al Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IvaPercentage';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Subtotal del detalle de la orden de compra. Es la multiplicacion de la cantidad por el valor unitario', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SubTotalValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el valor Unitario del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'UnitValue';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Pendiente', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'OutstandingQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Cantidad Solicitada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Quantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la poliza', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PolicyId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Model';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la marca', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'TrademarkId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del IVA del producto', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'IVAId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Articulo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'ItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de comprobante de entrada, este campo solo se llena si el item fue importado de un comprobante de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionEntranceItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el Id del detalle de la orden de compra, este campo solo se llena si el item fue importado de una remision de entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'PurchaseOrderItemId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el codigo de del documento el cua fue importado el item', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'SourceCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica el origen de la remision  1 - Ninguna  2 - Orden de compra  3 - Remision de Entrada', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'RemissionSource';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Ingreso del Activo', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'FixedAssetEntryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'FixedAsset', @level1type = N'TABLE', @level1name = N'FixedAssetEntryItem', @level2type = N'COLUMN', @level2name = N'Id';

